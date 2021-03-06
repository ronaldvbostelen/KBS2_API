﻿using System;
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
    public class OfficialReportController : ControllerBase
    {
        private readonly WijkagentContext _context;

        public OfficialReportController(WijkagentContext context)
        {
            _context = context;
        }

        //based on reportId
        // GET: OfficialReports/{reportId}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOfficialReport([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var officialReport = await _context.OfficialReport.FindAsync(id);
//            var officialReport = _context.OfficialReport.Where(x => x.reportId.Equals(id));

            if (officialReport == null)
            {
                return NotFound();
            }

            return Ok(officialReport);
        }

        // POST: api/OfficialReports
        [HttpPost]
        public async Task<IActionResult> PostOfficialReport([FromBody] OfficialReport officialReport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.OfficialReport.Add(officialReport);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OfficialReportExists(officialReport.reportId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOfficialReport", new { id = officialReport.reportId }, officialReport);
        }

        // DELETE: api/OfficialReports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOfficialReport([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var officialReport = await _context.OfficialReport.FindAsync(id);
            if (officialReport == null)
            {
                return NotFound();
            }

            _context.OfficialReport.Remove(officialReport);
            await _context.SaveChangesAsync();

            return Ok(officialReport);
        }


        //PATCH/PUT ID
        [HttpPut("{id}")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchtestTable([FromRoute] Guid id, [FromBody] OfficialReport officialReport)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != officialReport.reportId)
            {
                return BadRequest();
            }

            _context.Entry(officialReport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfficialReportExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(officialReport);
        }

        private bool OfficialReportExists(Guid id)
        {
            return _context.OfficialReport.Any(e => e.reportId == id);
        }
    }
}