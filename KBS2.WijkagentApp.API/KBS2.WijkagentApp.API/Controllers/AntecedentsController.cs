using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KBS2.WijkagentApp.API.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KBS2.WijkagentApp.API.Models;

namespace KBS2.WijkagentApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AntecedentsController : ControllerBase
    {
        private readonly WijkagentContext _context;

        public AntecedentsController(WijkagentContext context)
        {
            _context = context;
        }

        // GET: api/Antecedents
        [HttpGet]
        public IEnumerable<Antecedent> GetAntecedent()
        {
            return _context.Antecedent;
        }

        // GET: api/Antecedents/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAntecedent([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var antecedent = await _context.Antecedent.FindAsync(id);

            if (antecedent == null)
            {
                return NotFound();
            }

            return Ok(antecedent);
        }

        // PUT: api/Antecedents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAntecedent([FromRoute] Guid id, [FromBody] Antecedent antecedent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != antecedent.antecedentId)
            {
                return BadRequest();
            }

            _context.Entry(antecedent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AntecedentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Antecedents
        [HttpPost]
        public async Task<IActionResult> PostAntecedent([FromBody] Antecedent antecedent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Antecedent.Add(antecedent);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AntecedentExists(antecedent.antecedentId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAntecedent", new { id = antecedent.antecedentId }, antecedent);
        }

        // DELETE: api/Antecedents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAntecedent([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var antecedent = await _context.Antecedent.FindAsync(id);
            if (antecedent == null)
            {
                return NotFound();
            }

            _context.Antecedent.Remove(antecedent);
            await _context.SaveChangesAsync();

            return Ok(antecedent);
        }

        private bool AntecedentExists(Guid id)
        {
            return _context.Antecedent.Any(e => e.antecedentId == id);
        }
    }
}