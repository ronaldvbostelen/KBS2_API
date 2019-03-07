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
    public class SocialMessageController : ControllerBase
    {
        private readonly WijkagentContext _context;

        public SocialMessageController(WijkagentContext context)
        {
            _context = context;
        }

        //messages based on socialId
        // GET: api/SocialMessages/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSocialMessage([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var socialMessage = await Task.Run(() => _context.SocialMessage.Where(x => x.socialsId.Equals(id)).AsEnumerable());

            if (socialMessage == null || !socialMessage.Any())
            {
                return NotFound();
            }

            return Ok(socialMessage);
        }

        // POST: api/SocialMessages
        [HttpPost]
        public async Task<IActionResult> PostSocialMessage([FromBody] SocialMessage socialMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SocialMessage.Add(socialMessage);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SocialMessageExists(socialMessage.socialMessageId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSocialMessage", new { id = socialMessage.socialsId }, socialMessage);
        }

        // DELETE: api/SocialMessages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSocialMessage([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var socialMessage = await _context.SocialMessage.FindAsync(id);
            if (socialMessage == null)
            {
                return NotFound();
            }

            _context.SocialMessage.Remove(socialMessage);
            await _context.SaveChangesAsync();

            return Ok(socialMessage);
        }


        //PATCH/put ID
        [HttpPut("{id}")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchtestTable([FromRoute] Guid id, [FromBody] SocialMessage socialMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != socialMessage.socialMessageId)
            {
                return BadRequest();
            }

            _context.Entry(socialMessage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SocialMessageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(socialMessage);
        }

        private bool SocialMessageExists(Guid id)
        {
            return _context.SocialMessage.Any(e => e.socialMessageId == id);
        }
    }
}