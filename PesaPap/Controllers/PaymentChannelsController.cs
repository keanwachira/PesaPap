using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PesaPap.Data;
using PesaPap.Entities;

namespace PesaPap.Controllers
{
    
    public class PaymentChannelsController : BaseApiController
    {
        private readonly PaymentsDbContext _context;
        private readonly ILogger<PaymentChannelsController> _logger;

        //injection using constructor
        public PaymentChannelsController(PaymentsDbContext context, ILogger<PaymentChannelsController> logger)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// Get all payment channels
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetPaymentChannels()
        {
            try
            {
                if (_context.PaymentChannels == null)
                {
                    return NotFound();
                }
                return Ok(await _context.PaymentChannels.ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Sorry An Error ocurred");
            }
        }

        /// <summary>
        /// Get payment channel by Id GET: api/PaymentChannels/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetPaymentChannels(int id)
        {
            try
            {
                if (_context.PaymentChannels == null)
                {
                    return NotFound();
                }
                var paymentChannels = await _context.PaymentChannels.FindAsync(id);

                if (paymentChannels == null)
                {
                    return NotFound();
                }

                return Ok(paymentChannels);
            }
            catch (Exception ex) { _logger.LogError(ex, ex.Message); return StatusCode(500, "Sorry An Error ocurred"); }
        }

        /// <summary>
        /// Update payment channel PUT: api/PaymentChannels/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="paymentChannels"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaymentChannels(int id, PaymentChannels paymentChannels)
        {
            try
            {
                if (id != paymentChannels.ChannelId)
                {
                    return BadRequest();
                }

                _context.Entry(paymentChannels).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentChannelsExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("GetPaymentChannels", new { id = paymentChannels.ChannelId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Sorry An Error ocurred");
            }
        }

        /// <summary>
        /// Create payment channel POST: api/PaymentChannels
        /// </summary>
        /// <param name="paymentChannels"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<ActionResult> CreatePaymentChannels(PaymentChannels paymentChannels)
        {
            try
            {
                if (_context.PaymentChannels == null)
                {
                    return Problem("Entity set 'PaymentsDbContext.PaymentChannels'  is null.");
                }
                _context.PaymentChannels.Add(paymentChannels);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetPaymentChannels", new { id = paymentChannels.ChannelId }, paymentChannels);
            }
            catch (Exception ex) { _logger.LogError(ex, ex.Message); return StatusCode(500, "Sorry An Error ocurred"); }
        }

        /// <summary>
        /// Check if payment channel exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool PaymentChannelsExists(int id)
        {
            try
            {
                return (_context.PaymentChannels?.Any(e => e.ChannelId == id)).GetValueOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }
    }
}
