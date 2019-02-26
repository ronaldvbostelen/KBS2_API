using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KBS2.WijkagentApp.API.Assets;
using KBS2.WijkagentApp.API.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KBS2.WijkagentApp.API.Models;

namespace KBS2.WijkagentApp.API.Controllers
{
    [Route("api/tables/[controller]")]
    [Route("")]
    [ApiController]
    public class OfficerController : ControllerBase
    {
        private readonly WijkagentContext _context;
        private PasswordManager passwordManager;

        public OfficerController(WijkagentContext context)
        {
            _context = context;
            passwordManager = new PasswordManager();
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOfficer([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var officer = await _context.Officer.FindAsync(id);

            if (officer == null)
            {
                return NotFound();
            }

            officer.userName = null;
            officer.passWord = null;
            officer.salt = null;
            

            return Ok(officer);
        }

        //get logincredentials based on username/password (POST METHOD) URL = /login (NOT with api/tables)
        [HttpPost("/login")]
        public async Task<IActionResult> CheckOfficer([FromBody] Officer officer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lookUp = await Task.Run(() => (_context.Officer.Where(x => x.userName.Equals(officer.userName))));

            if (lookUp == null || !lookUp.Any())
            {
                return NotFound();
            }

            var lookUpOfficer = lookUp.First();

            if (passwordManager.VerifyPassword(officer.passWord, lookUpOfficer.passWord, lookUpOfficer.salt))
            {
                //we arnt sending confidential info back
                lookUpOfficer.passWord = lookUpOfficer.salt = string.Empty;

                return Ok(lookUpOfficer);
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/Officers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOfficer([FromRoute] Guid id, [FromBody] Officer officer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != officer.officerId)
            {
                return BadRequest();
            }

            _context.Entry(officer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfficerExists(id))
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

        // POST: api/Officers
        [HttpPost]
        public async Task<IActionResult> PostOfficer([FromBody] Officer officer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //first add a personentry for the officer
            var person = _context.Person.Add(new Person {firstName = officer.userName});
            //have to save because personId is server-side generated
            await _context.SaveChangesAsync();

             //generate hashes
            string passwordHash;
            string saltHash;
            passwordManager.GenerateSaltedHash(officer.passWord,out passwordHash, out saltHash);

            //update object
            officer.passWord = passwordHash;
            officer.salt = saltHash;
            officer.personId = person.Entity.personId;

            //insert officer
            _context.Officer.Add(officer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OfficerExists(officer.officerId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }
            //we arnt sending confidential info back
            officer.passWord = officer.salt = String.Empty;

            return CreatedAtAction("CheckOfficer", new { id = officer.officerId }, officer);
        }

        // DELETE: api/Officers/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteOfficer([FromRoute] Guid id)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }
//
//            var officer = await _context.Officer.FindAsync(id);
//            if (officer == null)
//            {
//                return NotFound();
//            }
//
//            _context.Officer.Remove(officer);
//            await _context.SaveChangesAsync();
//
//            return Ok(officer);
//        }


        //PATCH ID
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchtestTable([FromRoute] Guid id, [FromBody] Officer officer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != officer.officerId)
            {
                return BadRequest();
            }

            _context.Entry(officer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfficerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(officer);
        }

        private bool OfficerExists(Guid id)
        {
            return _context.Officer.Any(e => e.officerId == id);
        }
    }
}