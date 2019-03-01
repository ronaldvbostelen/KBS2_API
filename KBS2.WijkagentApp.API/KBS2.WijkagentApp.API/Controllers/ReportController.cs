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

        // GET: api/Reports/5
        [HttpGet("/reportquery/{parms}")]
        public IActionResult QueryReports([FromRoute] string parms)
        {   
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var words = parms.Split('_');

            var activeReportsOnly = words[words.Length - 1] != "A";

            words[words.Length - 1] = null;

            var lookupReports = new List<Report>();

            foreach (var word in words)
            {
                var lookup =
                from report in _context.Report
                where (report.status == "A" || activeReportsOnly) && (report.location.Contains(word) || report.type.Contains(word) || report.comment.Contains(word))
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

            if (report.status == "D")
            {
                try
                {
                    var outcomeState = await SendDeleteReportMessage(report);
                    Debug.Write(outcomeState.ToString());
                }
                catch (Exception e)
                {
                    Debug.WriteLine("PUSHMSG ERROR: " + e);
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
                var pushMessage = hub.CreateMessagePackage("addReport", JsonConvert.SerializeObject(report));
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
            
            if (report.status == "D")
            {
                try
                {
                    var outcomeState = await SendDeleteReportMessage(report);
                    Debug.Write(outcomeState.ToString());
                }
                catch (Exception e)
                {
                    Debug.WriteLine("PUSHMSG ERROR: " + e);
                }
            }
            return Ok(report);
        }

        private bool ReportExists(Guid id)
        {
            return _context.Report.Any(e => e.reportId == id);
        }

        private async Task<NotificationOutcomeState> SendDeleteReportMessage(Report report)
        {
            // inform report is deleted
            var hub = new Hub();
            var pushMessage = hub.CreateMessagePackage("deleteReport", "\"" + report.reportId.ToString() + "\"");
            var result = await hub.SendFcmNativeNotificationAsync(pushMessage);
            return result.State;
        }
    }
}