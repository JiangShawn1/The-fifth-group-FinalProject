using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using The_fifth_group_FinalAPI.Models;

namespace The_fifth_group_FinalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContestsController : ControllerBase
    {
        private readonly TheFifthGroupOfTopicsContext _context;

        public ContestsController(TheFifthGroupOfTopicsContext context)
        {
            _context = context;
        }

        // GET: api/Contests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contests>>> GetContests()
        {
            return await _context.Contests.ToListAsync();
        }

        // GET: api/Contests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contests>> GetContests(int id)
        {
            var contests = await _context.Contests.FindAsync(id);

            if (contests == null)
            {
                return NotFound();
            }

            return contests;
        }

        // PUT: api/Contests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContests(int id, Contests contests)
        {
            if (id != contests.Id)
            {
                return BadRequest();
            }

            _context.Entry(contests).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContestsExists(id))
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

        // POST: api/Contests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Contests>> PostContests(Contests contests)
        {
            _context.Contests.Add(contests);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContests", new { id = contests.Id }, contests);
        }

        // DELETE: api/Contests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContests(int id)
        {
            var contests = await _context.Contests.FindAsync(id);
            if (contests == null)
            {
                return NotFound();
            }

            _context.Contests.Remove(contests);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContestsExists(int id)
        {
            return _context.Contests.Any(e => e.Id == id);
        }
    }
}
