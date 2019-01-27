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
    [Route("api/tables/[controller]")]
    [ApiController]
    public class AntecedentController : ControllerBase
    {
        private readonly WijkagentContext _context;

        public AntecedentController(WijkagentContext context)
        {
            _context = context;
        }

        //lookup antecedents with personId
        // GET: Antecedents/{personId}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAntecedent([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var antecedents = await Task.Run(() => _context.Antecedent.Where(x => x.personId.Equals(id)).AsEnumerable());

            if (antecedents == null || !antecedents.Any())
            {
                return NotFound();
            }

            return Ok(antecedents);
        }

        //update entry based on antecedentId
        // PUT: api/Antecedents/{antecedentId}
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

        //insert into
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

        // DELETE: based on antecentID
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

        //update based on antecentID
        //PATCH ID
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchtestTable([FromRoute] Guid id, [FromBody] Antecedent antecedent)
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

            return Ok(antecedent);
        }

        private bool AntecedentExists(Guid id)
        {
            return _context.Antecedent.Any(e => e.antecedentId == id);
        }
    }
}