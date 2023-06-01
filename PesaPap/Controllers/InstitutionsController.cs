using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PesaPap.Data;
using PesaPap.Entities;
using Serilog.Core;

namespace PesaPap.Controllers
{
   
    public class InstitutionsController : BaseApiController
    {
        private readonly PaymentsDbContext _context;
        private readonly ILogger<InstitutionsController> _logger;

        public InstitutionsController(PaymentsDbContext context, ILogger<InstitutionsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Returns a list of all registered institutions
        /// </summary>
        /// <returns></returns>
        [HttpGet]        
        public async Task<ActionResult<IEnumerable<Institution>>> GetAllInstitutions()
        {
            try
            {
                if (_context.Institutions == null)
                {
                    return NotFound();
                }

                return await _context.Institutions.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                return StatusCode(500,"Sorry An Error ocurred");
            }
        }


        /// <summary>
        /// Gets institution details by Id param in query string  GET: api/Institutions/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Institution>> GetInstitution(int id)
        {
            try
            {
                if (_context.Institutions == null)
                {
                    return NotFound();
                }
                var institution = await _context.Institutions.FindAsync(id);

                if (institution == null)
                {
                    return NotFound($"Institution with Id {id} was not found");
                }

                return institution;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Sorry An Error ocurred");
            }
        }

        /// <summary>
        /// Update institution // PUT: api/Institutions/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="institution"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInstitution(int id, Institution institution)
        {
            try
            {
                if (id != institution.Id)
                {
                    return BadRequest("Id must be equal to Institution Id");
                }

                _context.Entry(institution).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstitutionExists(id))
                    {
                        return NotFound($"Institution with Id {id} was not found");
                    }
                    else
                    {
                        throw;
                    }
                }

                return Ok(institution);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, "Sorry An Error ocurred");
            }
        }

        /// <summary>
        /// Create New Institution POST: api/Institutions
        /// </summary>
        /// <param name="institution"></param>
        /// <returns></returns>
        
        [HttpPost]
        public async Task<ActionResult<Institution>> CreateInstitution(Institution institution)
        {
            try
            {
                if (_context.Institutions == null)
                {
                    return Problem("Entity set 'PaymentsDbContext.Institutions'  is null.");
                }
                _context.Institutions.Add(institution);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetInstitution", new { id = institution.Id }, institution);
            }
            catch (Exception ex) { _logger.LogError(ex, ex.Message); return StatusCode(500, "Sorry An Error ocurred"); }
        }

        
        /// <summary>
        /// Deactivate Insitution by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("deactivate/{id}")]
        public async Task<IActionResult> DeactivateInstitution(int id)
        {
            try
            {
                if (_context.Institutions == null)
                {
                    return NotFound();
                }
                var institution = await _context.Institutions.FindAsync(id);
                if (institution == null)
                {
                    return NotFound($"Institution with Id {id} was not found");
                }

                institution.Active = false;
                await _context.SaveChangesAsync();

                return Ok("Deactivated Successfully");
            }
            catch (Exception ex) { _logger.LogError(ex, ex.Message); return StatusCode(500, "Sorry An Error ocurred"); }
        }


        /// <summary>
        /// check if Institution with specified Id exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool InstitutionExists(int id)
        {
            try
            {
                return (_context.Institutions?.Any(e => e.Id == id)).GetValueOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }
    }
}
