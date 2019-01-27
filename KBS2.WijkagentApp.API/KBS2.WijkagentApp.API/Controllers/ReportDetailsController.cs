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
    public class ReportDetailsController : ControllerBase
    {
        private readonly WijkagentContext _context;

        public ReportDetailsController(WijkagentContext context)
        {
            _context = context;
        }

        //all reportdetails based on reportId
        // GET: api/ReportDetails/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReportDetails([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reportDetails = await Task.Run(() => _context.ReportDetails.Where(x => x.reportId.Equals(id)).AsEnumerable());

            if (reportDetails == null || !reportDetails.Any())
            {
                return NotFound();
            }

            return Ok(reportDetails);
        }

        // PUT: api/ReportDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReportDetails([FromRoute] Guid id, [FromBody] ReportDetails reportDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reportDetails.reportId)
            {
                return BadRequest();
            }

            _context.Entry(reportDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportDetailsExists(id))
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

        // POST: api/ReportDetails
        [HttpPost]
        public async Task<IActionResult> PostReportDetails([FromBody] ReportDetails reportDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ReportDetails.Add(reportDetails);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ReportDetailsExists(reportDetails.reportId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetReportDetails", new { id = reportDetails.reportId }, reportDetails);
        }

        // DELETE: api/ReportDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReportDetails([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reportDetails = await _context.ReportDetails.FindAsync(id);
            if (reportDetails == null)
            {
                return NotFound();
            }

            _context.ReportDetails.Remove(reportDetails);
            await _context.SaveChangesAsync();

            return Ok(reportDetails);
        }


        //PATCH ID
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchtestTable([FromRoute] Guid id, [FromBody] ReportDetails reportDetails)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reportDetails.reportId)
            {
                return BadRequest();
            }

            _context.Entry(reportDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(reportDetails);
        }

        private bool ReportDetailsExists(Guid id)
        {
            return _context.ReportDetails.Any(e => e.reportId == id);
        }
    }
}