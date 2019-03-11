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
    public class EmergencyController : ControllerBase
    {
        private readonly WijkagentContext _context;

        public EmergencyController(WijkagentContext context)
        {
            _context = context;
        }

        //fetch active emergencies
        // GET: api/Emergencies
        [HttpGet]
        public IEnumerable<Emergency> GetEmergency()
        {
            return _context.Emergency.Where(x => x.status == "A").AsEnumerable();
        }

        // GET: api/Emergencies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmergency([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var emergency = await _context.Emergency.FindAsync(id);

            if (emergency == null)
            {
                return NotFound();
            }

            return Ok(emergency);
        }

        // POST: api/Emergencies
        [HttpPost]
        public async Task<IActionResult> PostEmergency([FromBody] Emergency emergency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var officer = await _context.Officer.FindAsync(emergency.officerId);
            var person = await _context.Person.FindAsync(officer.personId);

            _context.Emergency.Add(emergency);
            try
            {
                await _context.SaveChangesAsync();

                var hub = new Hub();
                var pushMessage = hub.CreateMessagePackage("emergency", JsonConvert.SerializeObject(emergency), $"{person.firstName} {person.lastName}");
                var result = await hub.SendFcmNativeNotificationAsync(pushMessage);
                Console.Write(result.State.ToString());
            }
            catch (DbUpdateException)
            {
                if (EmergencyExists(emergency.emergencyId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEmergency", new { id = emergency.emergencyId }, emergency);
        }

        //PATCH/PUT ID
        [HttpPut("{id}")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchtestTable([FromRoute] Guid id, [FromBody] Emergency emergency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != emergency.emergencyId)
            {
                return BadRequest();
            }

            _context.Entry(emergency).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmergencyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(emergency);
        }

        private bool EmergencyExists(Guid id)
        {
            return _context.Emergency.Any(e => e.emergencyId == id);
        }
    }
}