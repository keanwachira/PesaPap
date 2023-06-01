using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PesaPap.Data;
using PesaPap.DTOs;
using PesaPap.Entities;

namespace PesaPap.Controllers
{

    public class IPNSettingsController : BaseApiController
    {
        private readonly PaymentsDbContext _context;
        private readonly ILogger<IPNSettingsController> _logger;

        //injection using constructor
        public IPNSettingsController(PaymentsDbContext context, ILogger<IPNSettingsController> logger)
        {
            _context = context;
            _logger = logger;
        }



        /// <summary>
        /// Returns all IPN settings GET: api/IPNSettings
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetIPNSettings()
        {
            try
            {
                if (_context.IPNSettings == null)
                {
                    return NotFound();
                }
                return Ok(await _context.IPNSettings.Include(a => a.Institution).ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Sorry An Error ocurred");
            }
        }

        /// <summary>
        /// Retruns IPN settings by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetIPNSettings(int id)
        {
            try
            {
                if (_context.IPNSettings == null)
                {
                    return NotFound();
                }

                var iPNSettings = _context.IPNSettings.Include(a => a.Institution).FirstOrDefaultAsync(x => x.Id == id);

                if (iPNSettings == null)
                {
                    return NotFound();
                }

                return Ok(iPNSettings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Sorry An Error ocurred");
            }
        }
        /// <summary>
        /// Retruns IPN settings by InsititutionID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("ipnsettingbyinstitution/{institutionid}")]
        public async Task<ActionResult<IPNSettings>> GetIPNSettingsbyInsitution(int institutionId)
        {
            try
            {
                if (_context.IPNSettings == null)
                {
                    return NotFound();
                }
                List<IPNSettings> iPNSettings = await _context.IPNSettings.Where(a => a.Institution.Id == institutionId).ToListAsync();

                if (iPNSettings == null)
                {
                    return NotFound();
                }

                return Ok(iPNSettings);
            }
            catch (Exception ex) { _logger.LogError(ex, ex.Message); return StatusCode(500, "Sorry An Error ocurred"); }
        }

        /// <summary>
        /// Allows updating of IPN settings PUT: api/IPNSettings/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="iPNSettings"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIPNSettings(int id, IPNSettings iPNSettings)
        {
            try
            {
                if (id != iPNSettings.Id)
                {
                    return BadRequest();
                }

                _context.Entry(iPNSettings).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IPNSettingsExists(id))
                    {
                        return NotFound($"Setting with ID {id} not found");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("GetIPNSettings", new { id = iPNSettings.Id });
            }
            catch (Exception ex) { _logger.LogError(ex, ex.Message); return StatusCode(500, "Sorry An Error ocurred"); }

        }

        /// <summary>
        /// Create new IPN setting POST: api/IPNSettings
        /// </summary>
        /// <param name="iPNSettings"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<ActionResult> CreateIPNSetting(IPNSettingsDTO iPNSettings)
        {
            try
            {
                if (_context.IPNSettings == null)
                {
                    return Problem("Entity set 'PaymentsDbContext.IPNSettings'  is null.");
                }

                Institution institution = _context.Institutions.FirstOrDefault(x => x.Id == iPNSettings.InstitutionId);
                if (institution == null) return NotFound($"Institution with ID {iPNSettings.InstitutionId} not found");

                IPNSettings newIpnSetting = new IPNSettings() { Institution = institution, NotificationChannel = iPNSettings.NotificationChannel, NotificationUrl = iPNSettings.NotificationUrl, ValidationUrl = iPNSettings.ValidationUrl };
                _context.IPNSettings.Add(newIpnSetting);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetIPNSettings", new { id = newIpnSetting.Id }, newIpnSetting);
            }
            catch (Exception ex) { _logger.LogError(ex, ex.Message); return StatusCode(500, "Sorry An Error ocurred"); }
        }


        /// <summary>
        /// Check if setting exists by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        private bool IPNSettingsExists(int id)
        {
            try
            {
                return (_context.IPNSettings?.Any(e => e.Id == id)).GetValueOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }
    }
}
