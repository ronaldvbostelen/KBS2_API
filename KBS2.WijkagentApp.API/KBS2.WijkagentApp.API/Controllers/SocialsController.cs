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
    public class SocialsController : ControllerBase
    {
        private readonly WijkagentContext _context;

        public SocialsController(WijkagentContext context)
        {
            _context = context;
        }

        // GET: api/Socials
        [HttpGet]
        public IEnumerable<Socials> GetSocials()
        {
            return _context.Socials;
        }

        // GET: api/Socials/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSocials([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var socials = await _context.Socials.FindAsync(id);

            if (socials == null)
            {
                return NotFound();
            }

            return Ok(socials);
        }

        // PUT: api/Socials/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSocials([FromRoute] Guid id, [FromBody] Socials socials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != socials.socialsId)
            {
                return BadRequest();
            }

            _context.Entry(socials).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SocialsExists(id))
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

        // POST: api/Socials
        [HttpPost]
        public async Task<IActionResult> PostSocials([FromBody] Socials socials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Socials.Add(socials);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SocialsExists(socials.socialsId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSocials", new { id = socials.socialsId }, socials);
        }

        // DELETE: api/Socials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSocials([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var socials = await _context.Socials.FindAsync(id);
            if (socials == null)
            {
                return NotFound();
            }

            _context.Socials.Remove(socials);
            await _context.SaveChangesAsync();

            return Ok(socials);
        }

        private bool SocialsExists(Guid id)
        {
            return _context.Socials.Any(e => e.socialsId == id);
        }
    }
}