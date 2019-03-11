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
using Microsoft.Azure.NotificationHubs;
using Newtonsoft.Json;

namespace KBS2.WijkagentApp.API.Controllers
{
    [Route("api/tables/[controller]")]
    [Route("api/reportquery")]
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

        // GET: query all reports for keywords, seperated by a underscore
        [HttpGet("/reportquery/{parms}")]
        public IActionResult QueryReports([FromRoute] string parms)
        {   
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var words = parms.Split('_');

            var lookupReports = new List<Report>();

            foreach (var word in words)
            {
                var lookup =
                from report in _context.Report
                where report.location.Contains(word) || report.type.Contains(word) || report.comment.Contains(word)
                select report;

                foreach (var report in lookup)
                {
                    if (!lookupReports.Contains(report)) lookupReports.Add(report);
                }
            }
            
            if (!lookupReports.Any())       
            {
                return NotFound();
            }

            return Ok(lookupReports);
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
                await SendReportPushMessage(report);
            }
            catch (Exception e)
            {
                Console.WriteLine("PUSHMSG ERROR: " + e);
            }

            return CreatedAtAction("GetReport", new { id = report.reportId }, report);
        }

        //PATCH/PUT ID
        [HttpPut("{id}")]
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

            try
            {
                await SendReportPushMessage(report);
            }
            catch (Exception e)
            {
                Console.WriteLine("PUSHMSG ERROR: " + e);
            }

            return Ok(report);
        }

        private async Task<NotificationOutcomeState> SendReportPushMessage(Report report)
        {
            var hub = new Hub();
            var pushMessage = hub.CreateMessagePackage("Report", JsonConvert.SerializeObject(report));
            var result = await hub.SendFcmNativeNotificationAsync(pushMessage);
            return result.State;
        }

        private bool ReportExists(Guid id)
        {
            return _context.Report.Any(e => e.reportId == id);
        }
    }
}