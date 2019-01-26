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
    public class OfficersController : ControllerBase
    {
        private readonly WijkagentContext _context;

        public OfficersController(WijkagentContext context)
        {
            _context = context;
        }

        // GET: api/Officers
        [HttpGet]
        public IEnumerable<Officer> GetOfficer()
        {
            return _context.Officer;
        }

        // GET: api/Officers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOfficer([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var officer = await _context.Officer.FindAsync(id);

            if (officer == null)
            {
                return NotFound();
            }

            return Ok(officer);
        }

        // PUT: api/Officers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOfficer([FromRoute] Guid id, [FromBody] Officer officer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != officer.officerId)
            {
                return BadRequest();
            }

            _context.Entry(officer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfficerExists(id))
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

        // POST: api/Officers
        [HttpPost]
        public async Task<IActionResult> PostOfficer([FromBody] Officer officer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Officer.Add(officer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OfficerExists(officer.officerId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOfficer", new { id = officer.officerId }, officer);
        }

        // DELETE: api/Officers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOfficer([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var officer = await _context.Officer.FindAsync(id);
            if (officer == null)
            {
                return NotFound();
            }

            _context.Officer.Remove(officer);
            await _context.SaveChangesAsync();

            return Ok(officer);
        }

        private bool OfficerExists(Guid id)
        {
            return _context.Officer.Any(e => e.officerId == id);
        }
    }
}