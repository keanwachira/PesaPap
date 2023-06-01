using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using PesaPap.Controllers;
using PesaPap.Data;
using PesaPap.DTOs;
using PesaPap.Entities;

namespace PesaPap.BLL
{
    public class PaymentNotificationProcessing
    {
        private readonly PaymentsDbContext _context;
        public readonly ILogger<PaymentNotificationsController> _logger;

        public PaymentNotificationProcessing(PaymentsDbContext context, ILogger<PaymentNotificationsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool processNotification(string transactionRef, string studentNo, string studentName, string notificationUrl, decimal Amount, int notificationChannel)
        {
            //http web request to send notification to specified URL

            bool success = true;

            return success;
        }

        /// <summary>
        /// Send notifications to Customer
        /// </summary>
        public void SendApiNotifications()
        {  
            List<PaymentNotifications> notifications =  _context.PaymentNotifications.Where(a => a.IPNSent == false)
                .Include(c => c.Institution)
                .Include(d => d.Channel)
                .Take(10)
                .ToList();

            //get only ipn settings for institutions included in notifications
            List<IPNSettings> iPNSettings = _context.IPNSettings.Include(b => b.Institution).
                Where(a => notifications.Select(c => c.Institution.Id).Contains(a.Institution.Id)).ToList();

            List<Task> ? tasks = new List<Task>();
            //process the notifications asynchronously
            notifications.ForEach(a =>
            {
                tasks.Add(new Task(() =>
                {
                    IPNSettings? ipnSetting = iPNSettings.Where(a => a.Institution.Id == a.Institution.Id).FirstOrDefault();
                    processNotification(a.TransactionRef, a.StudentNo, a.StudentName, ipnSetting.NotificationUrl, a.Amount, a.Channel.ChannelId);
                }));
               
            });

            //run all the tasks queued
            tasks.ForEach(x => x.Start());
            Task.WaitAll(tasks.ToArray());

            _logger.LogInformation($"Sending IPNs for {notifications.Count()} transactions");
        }
        public ValidationResponse ValidateStudentNo(string studentNo)
        {
            // http request to validation endpoint
            
            return new ValidationResponse() { Exists= true , StudentName = "John Doe", StudentNo = "123456"};

        }


    }
}
