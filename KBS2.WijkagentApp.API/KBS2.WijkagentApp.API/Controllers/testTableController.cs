using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KBS2.WijkagentApp.API.Context;
using KBS2.WijkagentApp.API.Models;

namespace KBS2.WijkagentApp.API.Controllers
{
    [Route("api/tables/[controller]")]
    [ApiController]
    public class testTableController : ControllerBase
    {
        private readonly WijkagentContext _context;

        public testTableController(WijkagentContext context)
        {
            _context = context;
        }

        // GET: api/testTable
        [HttpGet]
        public IEnumerable<testTable> GettestTable()
        {
            return _context.testTable;
        }

        // GET: api/testTable/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GettestTable([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var testTable = await _context.testTable.FindAsync(id);

            if (testTable == null)
            {
                return NotFound();
            }

            return Ok(testTable);
        }

        // PUT: api/testTable/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PuttestTable([FromRoute] Guid id, [FromBody] testTable testTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != testTable.id)
            {
                return BadRequest();
            }

            _context.Entry(testTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!testTableExists(id))
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

        // POST: api/testTable
        [HttpPost]
        public async Task<IActionResult> PosttestTable([FromBody] testTable testTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.testTable.Add(testTable);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (testTableExists(testTable.id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GettestTable", new { id = testTable.id }, testTable);
        }

        // DELETE: api/testTable/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletetestTable([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var testTable = await _context.testTable.FindAsync(id);
            if (testTable == null)
            {
                return NotFound();
            }

            _context.testTable.Remove(testTable);
            await _context.SaveChangesAsync();

            return Ok(testTable);
        }

        //PATCH ID
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchtestTable([FromRoute] Guid id, [FromBody] testTable testTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != testTable.id)
            {
                return BadRequest();
            }

            _context.Entry(testTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!testTableExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(testTable);
        }

        private bool testTableExists(Guid id)
        {
            return _context.testTable.Any(e => e.id == id);
        }
    }
}