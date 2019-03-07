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
    public class PictureController : ControllerBase
    {
        private readonly WijkagentContext _context;

        public PictureController(WijkagentContext context)
        {
            _context = context;
        }
        
        //officalreportId
        // GET: api/Pictures/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPicture([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var picture = await Task.Run(() => _context.Picture.Where(x => x.officialReportId.Equals(id)).AsEnumerable());

            if (picture == null || !picture.Any())
            {
                return NotFound();
            }

            return Ok(picture);
        }

        // POST: api/Pictures
        [HttpPost]
        public async Task<IActionResult> PostPicture([FromBody] Picture picture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Picture.Add(picture);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PictureExists(picture.officialReportId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPicture", new { id = picture.officialReportId }, picture);
        }

        // DELETE: api/Pictures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePicture([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var picture = await _context.Picture.FindAsync(id);
            if (picture == null)
            {
                return NotFound();
            }

            _context.Picture.Remove(picture);
            await _context.SaveChangesAsync();

            return Ok(picture);
        }


        //PATCH/PUT ID
        [HttpPut("{id}")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchtestTable([FromRoute] Guid id, [FromBody] Picture picture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != picture.pictureId)
            {
                return BadRequest();
            }

            _context.Entry(picture).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PictureExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(picture);
        }

        private bool PictureExists(Guid id)
        {
            return _context.Picture.Any(e => e.officialReportId == id);
        }
    }
}