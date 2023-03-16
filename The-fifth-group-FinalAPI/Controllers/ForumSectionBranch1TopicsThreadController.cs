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
    public class ForumSectionBranch1TopicsThreadController : ControllerBase
    {
        private readonly TheFifthGroupOfTopicsContext _context;

        public ForumSectionBranch1TopicsThreadController(TheFifthGroupOfTopicsContext context)
        {
            _context = context;
        }

        // GET: api/ForumSectionBranch1TopicsThread
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ForumSectionBranch1TopicsThread>>> GetForumSectionBranch1TopicsThread()
        {
            return await _context.ForumSectionBranch1TopicsThread.ToListAsync();
        }

        // GET: api/ForumSectionBranch1TopicsThread/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ForumSectionBranch1TopicsThread>> GetForumSectionBranch1TopicsThread(int id)
        {
            var forumSectionBranch1TopicsThread = await _context.ForumSectionBranch1TopicsThread.FindAsync(id);

            if (forumSectionBranch1TopicsThread == null)
            {
                return NotFound();
            }

            return forumSectionBranch1TopicsThread;
        }

        // PUT: api/ForumSectionBranch1TopicsThread/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutForumSectionBranch1TopicsThread(int id, ForumSectionBranch1TopicsThread forumSectionBranch1TopicsThread)
        {
            if (id != forumSectionBranch1TopicsThread.Id)
            {
                return BadRequest();
            }

            _context.Entry(forumSectionBranch1TopicsThread).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ForumSectionBranch1TopicsThreadExists(id))
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

        // POST: api/ForumSectionBranch1TopicsThread
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ForumSectionBranch1TopicsThread>> PostForumSectionBranch1TopicsThread(ForumSectionBranch1TopicsThread forumSectionBranch1TopicsThread)
        {
            _context.ForumSectionBranch1TopicsThread.Add(forumSectionBranch1TopicsThread);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetForumSectionBranch1TopicsThread", new { id = forumSectionBranch1TopicsThread.Id }, forumSectionBranch1TopicsThread);
        }

        // DELETE: api/ForumSectionBranch1TopicsThread/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteForumSectionBranch1TopicsThread(int id)
        {
            var forumSectionBranch1TopicsThread = await _context.ForumSectionBranch1TopicsThread.FindAsync(id);
            if (forumSectionBranch1TopicsThread == null)
            {
                return NotFound();
            }

            _context.ForumSectionBranch1TopicsThread.Remove(forumSectionBranch1TopicsThread);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ForumSectionBranch1TopicsThreadExists(int id)
        {
            return _context.ForumSectionBranch1TopicsThread.Any(e => e.Id == id);
        }
    }
}
