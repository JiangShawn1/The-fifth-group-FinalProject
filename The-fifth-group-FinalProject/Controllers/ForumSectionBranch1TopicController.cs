using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using The_fifth_group_FinalProject.Models;

namespace The_fifth_group_FinalProject.Controllers
{
    public class ForumSectionBranch1TopicController : Controller
    {
        private readonly TheFifthGroupOfTopicsContext _context;

        public ForumSectionBranch1TopicController(TheFifthGroupOfTopicsContext context)
        {
            _context = context;
        }

        // GET: ForumSectionBranch1Topic
        public async Task<IActionResult> Index()
        {
            var theFifthGroupOfTopicsContext = _context.ForumSectionBranch1Topics.Include(f => f.Branch).Where(f=>f.BranchId ==1);
			

			return View(await theFifthGroupOfTopicsContext.ToListAsync());
        }

		public async Task<IActionResult> Index2()
		{
			var theFifthGroupOfTopicsContext = _context.ForumSectionBranch1Topics.Include(f => f.Branch).Where(f => f.BranchId == 4);


			return View(await theFifthGroupOfTopicsContext.ToListAsync());
		}

		// GET: ForumSectionBranch1Topic/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ForumSectionBranch1Topics == null)
            {
                return NotFound();
            }

            var forumSectionBranch1Topic = await _context.ForumSectionBranch1Topics
                .Include(f => f.Branch)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (forumSectionBranch1Topic == null)
            {
                return NotFound();
            }

            return View(forumSectionBranch1Topic);
        }

        // GET: ForumSectionBranch1Topic/Create
        public IActionResult Create()
        {
            ViewData["BranchId"] = new SelectList(_context.ForumSectionBranches, "Id", "Id");
            return View();
        }

        // POST: ForumSectionBranch1Topic/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BranchId,Topic,State")] ForumSectionBranch1Topic forumSectionBranch1Topic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(forumSectionBranch1Topic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchId"] = new SelectList(_context.ForumSectionBranches, "Id", "Id", forumSectionBranch1Topic.BranchId);
            return View(forumSectionBranch1Topic);
        }

        // GET: ForumSectionBranch1Topic/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ForumSectionBranch1Topics == null)
            {
                return NotFound();
            }

            var forumSectionBranch1Topic = await _context.ForumSectionBranch1Topics.FindAsync(id);
            if (forumSectionBranch1Topic == null)
            {
                return NotFound();
            }
            ViewData["BranchId"] = new SelectList(_context.ForumSectionBranches, "Id", "Id", forumSectionBranch1Topic.BranchId);
            return View(forumSectionBranch1Topic);
        }

        // POST: ForumSectionBranch1Topic/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BranchId,Topic,State")] ForumSectionBranch1Topic forumSectionBranch1Topic)
        {
            if (id != forumSectionBranch1Topic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(forumSectionBranch1Topic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ForumSectionBranch1TopicExists(forumSectionBranch1Topic.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchId"] = new SelectList(_context.ForumSectionBranches, "Id", "Id", forumSectionBranch1Topic.BranchId);
            return View(forumSectionBranch1Topic);
        }

        // GET: ForumSectionBranch1Topic/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ForumSectionBranch1Topics == null)
            {
                return NotFound();
            }

            var forumSectionBranch1Topic = await _context.ForumSectionBranch1Topics
                .Include(f => f.Branch)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (forumSectionBranch1Topic == null)
            {
                return NotFound();
            }

            return View(forumSectionBranch1Topic);
        }

        // POST: ForumSectionBranch1Topic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ForumSectionBranch1Topics == null)
            {
                return Problem("Entity set 'TheFifthGroupOfTopicsContext.ForumSectionBranch1Topics'  is null.");
            }
            var forumSectionBranch1Topic = await _context.ForumSectionBranch1Topics.FindAsync(id);
            if (forumSectionBranch1Topic != null)
            {
                _context.ForumSectionBranch1Topics.Remove(forumSectionBranch1Topic);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ForumSectionBranch1TopicExists(int id)
        {
          return (_context.ForumSectionBranch1Topics?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
