using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using MessagePack.Formatters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PesaPap.BLL;
using PesaPap.Data;
using PesaPap.DTOs;
using PesaPap.Entities;

namespace PesaPap.Controllers
{

    public class PaymentNotificationsController : BaseApiController
    {
        private readonly PaymentsDbContext _context;
        private readonly ILogger<PaymentNotificationsController> _logger;
        private ILogger<PaymentNotificationsController> _logger2;

        //injection using constructor
        public PaymentNotificationsController(PaymentsDbContext context, ILogger<PaymentNotificationsController> logger, ILogger<PaymentNotificationsController> logger2)
        {
            _context = context;
            _logger = logger;
            _logger2 = logger2;
        }


        /// <summary>
        /// Returns all payment Notifications
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<PaymentNotifications>>> GetPaymentNotifications(int pageSize = 10, int PageNo = 0)
        {

            if (_context.PaymentNotifications == null)
            {
                return NotFound();
            }
            return await _context.PaymentNotifications.Include(x => x.Institution).Include(a => a.Channel).Take(pageSize).Skip(PageNo * pageSize).ToListAsync();
        }
        [HttpGet("studentNo")]
        public async Task<ActionResult> ValidateStudentNo(string studentNo)
        {
            PaymentNotificationProcessing PnProcessing = new PaymentNotificationProcessing(_context, _logger2);
           return Ok(PnProcessing.ValidateStudentNo(studentNo));
        }
        /// <summary>
        /// Gets payment Notification by ID GET: api/PaymentNotifications/v1/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetPaymentNotifications(int id)
        {
            if (_context.PaymentNotifications == null)
            {
                return NotFound();
            }
            var paymentNotifications = await _context.PaymentNotifications.Include(a => a.Institution).Include(b => b.Channel).FirstOrDefaultAsync(x => x.Id == id);

            if (paymentNotifications == null)
            {
                return NotFound();
            }

            return Ok(paymentNotifications);
        }


        /// <summary>
        /// Create new Payment Notification POST: api/PaymentNotifications/v1
        /// </summary>
        /// <param name="paymentNotifications"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<ActionResult> CreatePaymentNotification(PaymentNotificationsDTO paymentNotifications)
        {
            //create recurring job to send api notifications to client
            //var jobId = BackgroundJob.Schedule(() => Console.WriteLine("Hello and welcome!"), TimeSpan.FromSeconds(20));

            //RecurringJob.AddOrUpdate(() => Console.WriteLine("Hello and Welcome"), "*/20 * * * * *");




            if (_context.PaymentNotifications == null)
            {
                return Problem("Entity set 'PaymentsDbContext.PaymentNotifications'  is null.");
            }
            Institution? institution = _context.Institutions.FirstOrDefault(a => a.Id == paymentNotifications.InstitutionId);
            if (institution == null) return NotFound($"Institution with Id {paymentNotifications.InstitutionId} not found");

            PaymentChannels? channel = _context.PaymentChannels.FirstOrDefault(a => a.ChannelId == paymentNotifications.ChannelID);
            if (channel == null) return NotFound($"Payment Channel with Id {paymentNotifications.ChannelID} not found");


            PaymentNotifications newPaymentNotification = new PaymentNotifications()
            {
                Institution = institution,
                AccountNumber = paymentNotifications.AccountNumber,
                Channel = channel,
                PayerName = paymentNotifications.PayerName,
                IPNSent = false,
                StudentName = paymentNotifications.StudentName,
                StudentNo = paymentNotifications.StudentNo,
                TransactionRef = paymentNotifications.TransactionRef,
                PaymentNarration = paymentNotifications.PaymentNarration
            };
            _context.PaymentNotifications.Add(newPaymentNotification);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaymentNotifications", new { id = newPaymentNotification.Id }, newPaymentNotification);
        }

        /// <summary>
        /// check if payment notification exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        private bool PaymentNotificationsExists(int id)
        {
            return (_context.PaymentNotifications?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
