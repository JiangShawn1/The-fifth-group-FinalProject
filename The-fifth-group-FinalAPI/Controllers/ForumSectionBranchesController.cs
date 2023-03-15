using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using The_fifth_group_FinalAPI.DTOs;
using The_fifth_group_FinalAPI.Models;

namespace The_fifth_group_FinalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumSectionBranchesController : ControllerBase
    {
        private readonly TheFifthGroupOfTopicsContext _context;

        public ForumSectionBranchesController(TheFifthGroupOfTopicsContext context)
        {
            _context = context;
        }

        // GET: api/ForumSectionBranches
        [HttpGet]
        public async Task<IEnumerable<Forum_FirstDTO>> GetForum_First()
        {
            var test = _context.ForumSectionBranches.Include(x => x.SectionNameId == x.Id);
            var forum_First =(from a in test
                             select  new Forum_FirstDTO
                              {
                                  Id = a.Id,
                                  SectionNameId = a.SectionNameId,
                                  AdministratorId = a.AdministratorId,
                           
                              }).ToListAsync(); 
              
            return await forum_First;
        }

        // GET: api/ForumSectionBranches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ForumSectionBranches>> GetForumSectionBranches(int id)
        {
            var forumSectionBranches = await _context.ForumSectionBranches.FindAsync(id);

            if (forumSectionBranches == null)
            {
                return NotFound();
            }

            return forumSectionBranches;
        }

        // PUT: api/ForumSectionBranches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutForumSectionBranches(int id, ForumSectionBranches forumSectionBranches)
        {
            if (id != forumSectionBranches.Id)
            {
                return BadRequest();
            }

            _context.Entry(forumSectionBranches).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ForumSectionBranchesExists(id))
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

        // POST: api/ForumSectionBranches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ForumSectionBranches>> PostForumSectionBranches(ForumSectionBranches forumSectionBranches)
        {
            _context.ForumSectionBranches.Add(forumSectionBranches);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetForumSectionBranches", new { id = forumSectionBranches.Id }, forumSectionBranches);
        }

        // DELETE: api/ForumSectionBranches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteForumSectionBranches(int id)
        {
            var forumSectionBranches = await _context.ForumSectionBranches.FindAsync(id);
            if (forumSectionBranches == null)
            {
                return NotFound();
            }

            _context.ForumSectionBranches.Remove(forumSectionBranches);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ForumSectionBranchesExists(int id)
        {
            return _context.ForumSectionBranches.Any(e => e.Id == id);
        }
    }
}
