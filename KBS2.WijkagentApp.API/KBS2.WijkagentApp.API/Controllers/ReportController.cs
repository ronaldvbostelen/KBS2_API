using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using KBS2.WijkagentApp.API.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KBS2.WijkagentApp.API.Models;
using KBS2.WijkagentApp.API.NotificationHub;
using Newtonsoft.Json;

namespace KBS2.WijkagentApp.API.Controllers
{
    [Route("api/tables/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly WijkagentContext _context;

        public ReportController(WijkagentContext context)
        {
            _context = context;
        }

        // GET: returns all reports except with status D (done)
        [HttpGet]
        public IEnumerable<Report> GetReport()
        {
            return _context.Report.Where(x => x.status != "D");
        }

        // GET: api/Reports/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReport([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var report = await _context.Report.FindAsync(id);

            if (report == null)
            {
                return NotFound();
            }

            return Ok(report);
        }

        // PUT: api/Reports/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReport([FromRoute] Guid id, [FromBody] Report report)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != report.reportId)
            {
                return BadRequest();
            }

            _context.Entry(report).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(id))
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

        // POST: api/Reports
        [HttpPost]
        public async Task<IActionResult> PostReport([FromBody] Report report)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Report.Add(report);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ReportExists(report.reportId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            try
            {
                // send the added report to all the clients
                var hub = new Hub();
                var pushMessage = hub.CreateMessagePackage("report", JsonConvert.SerializeObject(report));
                var result = await hub.SendFcmNativeNotificationAsync(pushMessage);
                Debug.Write(result.State.ToString());
            }
            catch (Exception e)
            {
                Debug.WriteLine("PUSHMSG ERROR: " + e);
            }

            return CreatedAtAction("GetReport", new { id = report.reportId }, report);
        }
        
        //PATCH ID
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchReport([FromRoute] Guid id, [FromBody] Report report)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != report.reportId)
            {
                return BadRequest();
            }

            _context.Entry(report).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(report);
        }

        private bool ReportExists(Guid id)
        {
            return _context.Report.Any(e => e.reportId == id);
        }
    }
}