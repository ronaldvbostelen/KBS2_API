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
    public class SoundRecordController : ControllerBase
    {
        private readonly WijkagentContext _context;

        public SoundRecordController(WijkagentContext context)
        {
            _context = context;
        }

        // GET: api/SoundRecords
        [HttpGet]
        public IEnumerable<SoundRecord> GetSoundRecord()
        {
            return _context.SoundRecord;
        }

        // GET: api/SoundRecords/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSoundRecord([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var soundRecord = await _context.SoundRecord.FindAsync(id);

            if (soundRecord == null)
            {
                return NotFound();
            }

            return Ok(soundRecord);
        }

        // PUT: api/SoundRecords/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSoundRecord([FromRoute] Guid id, [FromBody] SoundRecord soundRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != soundRecord.soundRecordId)
            {
                return BadRequest();
            }

            _context.Entry(soundRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SoundRecordExists(id))
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

        // POST: api/SoundRecords
        [HttpPost]
        public async Task<IActionResult> PostSoundRecord([FromBody] SoundRecord soundRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SoundRecord.Add(soundRecord);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SoundRecordExists(soundRecord.soundRecordId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSoundRecord", new { id = soundRecord.officialReportId }, soundRecord);
        }

        // DELETE: api/SoundRecords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSoundRecord([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var soundRecord = await _context.SoundRecord.FindAsync(id);
            if (soundRecord == null)
            {
                return NotFound();
            }

            _context.SoundRecord.Remove(soundRecord);
            await _context.SaveChangesAsync();

            return Ok(soundRecord);
        }


        //PATCH ID
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchtestTable([FromRoute] Guid id, [FromBody] SoundRecord soundRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != soundRecord.soundRecordId)
            {
                return BadRequest();
            }

            _context.Entry(soundRecord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SoundRecordExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(soundRecord);
        }

        private bool SoundRecordExists(Guid id)
        {
            return _context.SoundRecord.Any(e => e.soundRecordId == id);
        }
    }
}