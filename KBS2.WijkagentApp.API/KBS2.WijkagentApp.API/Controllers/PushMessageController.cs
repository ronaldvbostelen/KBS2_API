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
    public class PushMessageController : ControllerBase
    {
        private readonly WijkagentContext _context;

        public PushMessageController(WijkagentContext context)
        {
            _context = context;
        }

        // GET: api/PushMessages
        [HttpGet]
        public IEnumerable<PushMessage> GetPushMessage()
        {
            return _context.PushMessage;
        }

        // GET: api/PushMessages/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPushMessage([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pushMessage = await _context.PushMessage.FindAsync(id);

            if (pushMessage == null)
            {
                return NotFound();
            }

            return Ok(pushMessage);
        }

        // PUT: api/PushMessages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPushMessage([FromRoute] Guid id, [FromBody] PushMessage pushMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pushMessage.pushMessageId)
            {
                return BadRequest();
            }

            _context.Entry(pushMessage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PushMessageExists(id))
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

        // POST: api/PushMessages
        [HttpPost]
        public async Task<IActionResult> PostPushMessage([FromBody] PushMessage pushMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.PushMessage.Add(pushMessage);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PushMessageExists(pushMessage.pushMessageId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPushMessage", new { id = pushMessage.pushMessageId }, pushMessage);
        }

        // DELETE: api/PushMessages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePushMessage([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pushMessage = await _context.PushMessage.FindAsync(id);
            if (pushMessage == null)
            {
                return NotFound();
            }

            _context.PushMessage.Remove(pushMessage);
            await _context.SaveChangesAsync();

            return Ok(pushMessage);
        }


        //PATCH ID
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchtestTable([FromRoute] Guid id, [FromBody] PushMessage pushMessage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pushMessage.pushMessageId)
            {
                return BadRequest();
            }

            _context.Entry(pushMessage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PushMessageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(pushMessage);
        }

        private bool PushMessageExists(Guid id)
        {
            return _context.PushMessage.Any(e => e.pushMessageId == id);
        }
    }
}