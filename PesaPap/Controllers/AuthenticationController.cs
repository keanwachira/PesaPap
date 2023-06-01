using Hangfire;
using Hangfire.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using PesaPap.BLL;
using PesaPap.Data;
using PesaPap.DTOs;
using PesaPap.Entities;
using PesaPap.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using ConfigurationManager = PesaPap.Models.ConfigurationManager;


namespace PesaPap.Controllers
{
    [ApiController]
    [Route("api/[controller]/v1")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly PaymentsDbContext _context;
        private ILogger<PaymentNotificationsController> _logger2;

        //injection using constructor
        public AuthenticationController(ILogger<AuthenticationController> logger, PaymentsDbContext context, ILogger<PaymentNotificationsController> logger2)
        {
            _logger = logger;
            _context = context;
            _logger2 = logger2;
        }
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegistrationDTO registrationDetails)
        {
            UserLogin registered = _context.Users.FirstOrDefault(x => x.Email == registrationDetails.Email);
            if (registered != null) return BadRequest("Username is already taken");

            var hash =Utilities.HashPasword(registrationDetails.Password, out var salt);
            var hex = Convert.ToHexString(salt);

            UserLogin newUser = new UserLogin()
            {
                Email = registrationDetails.Email,
                Password = hash,
                Salt = hex
            };

            

            
            


            _context.Add(newUser);
            await _context.SaveChangesAsync();
            return Ok("user registered successfully");


        }
        [HttpPost("login")]
        public ActionResult Login(LoginDTO user)
        {

            try
            {

                PaymentNotificationProcessing PnProcessing = new PaymentNotificationProcessing(_context, _logger2);

                //start recurring postNotifications job for every 30 seconds
                RecurringJob.AddOrUpdate(() => PnProcessing.SendApiNotifications(), "*/30 * * * * *");

                if (user is null)
                {
                    return BadRequest("Invalid user request!!!");
                }

               var login = _context.Users.FirstOrDefault(a => a.Email == user.Email);
                if (login == null) return Unauthorized("Invalid Password or Email");

                var verified = Utilities.VerifyPassword(user.Password, login.Password, Enumerable.Range(0, login.Salt.Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => Convert.ToByte(login.Salt.Substring(x, 2), 16))
                     .ToArray());               


                if (verified)
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSetting["JWT:Secret"]));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokeOptions = new JwtSecurityToken(issuer: ConfigurationManager.AppSetting["JWT:ValidIssuer"], audience: ConfigurationManager.AppSetting["JWT:ValidAudience"], claims: new List<Claim>(), expires: DateTime.Now.AddMinutes(6), signingCredentials: signinCredentials);
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    return Ok(new JWTTokenResponse
                    {
                        Token = tokenString
                    });
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Sorry An Error ocurred");
            }
        }
        
    }
}
