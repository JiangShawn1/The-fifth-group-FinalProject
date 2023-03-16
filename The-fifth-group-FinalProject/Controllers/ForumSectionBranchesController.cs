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
    public class ForumSectionBranchesController : Controller
    {
        private readonly TheFifthGroupOfTopicsContext _context;

        public ForumSectionBranchesController(TheFifthGroupOfTopicsContext context)
        {
            _context = context;
        }

        // GET: ForumSectionBranches
        public async Task<IActionResult> Index()
        {
            var theFifthGroupOfTopicsContext = _context.ForumSectionBranches.Include(f => f.Administrator).Include(f => f.SectionName);
            return View(await theFifthGroupOfTopicsContext.ToListAsync());

        }
		

			// GET: ForumSectionBranches/Details/5
			public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ForumSectionBranches == null)
            {
                return NotFound();
            }

            var forumSectionBranch = await _context.ForumSectionBranches
                .Include(f => f.Administrator)
                .Include(f => f.SectionName)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (forumSectionBranch == null)
            {
                return NotFound();
            }

            return View(forumSectionBranch);
        }

        // GET: ForumSectionBranches/Create
        public IActionResult Create()
        {
            ViewData["AdministratorId"] = new SelectList(_context.Members, "MemberId", "MemberId");
            ViewData["SectionNameId"] = new SelectList(_context.ForumSections, "Id", "Id");
            return View();
        }

        // POST: ForumSectionBranches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SectionNameId,BranchName,SectionAdministrator,AdministratorId")] ForumSectionBranch forumSectionBranch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(forumSectionBranch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdministratorId"] = new SelectList(_context.Members, "MemberId", "MemberId", forumSectionBranch.AdministratorId);
            ViewData["SectionNameId"] = new SelectList(_context.ForumSections, "Id", "Id", forumSectionBranch.SectionNameId);
            return View(forumSectionBranch);
        }

        // GET: ForumSectionBranches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ForumSectionBranches == null)
            {
                return NotFound();
            }

            var forumSectionBranch = await _context.ForumSectionBranches.FindAsync(id);
            if (forumSectionBranch == null)
            {
                return NotFound();
            }
            ViewData["AdministratorId"] = new SelectList(_context.Members, "MemberId", "MemberId", forumSectionBranch.AdministratorId);
            ViewData["SectionNameId"] = new SelectList(_context.ForumSections, "Id", "Id", forumSectionBranch.SectionNameId);
            return View(forumSectionBranch);
        }

        // POST: ForumSectionBranches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SectionNameId,BranchName,SectionAdministrator,AdministratorId")] ForumSectionBranch forumSectionBranch)
        {
            if (id != forumSectionBranch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(forumSectionBranch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ForumSectionBranchExists(forumSectionBranch.Id))
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
            ViewData["AdministratorId"] = new SelectList(_context.Members, "MemberId", "MemberId", forumSectionBranch.AdministratorId);
            ViewData["SectionNameId"] = new SelectList(_context.ForumSections, "Id", "Id", forumSectionBranch.SectionNameId);
            return View(forumSectionBranch);
        }

        // GET: ForumSectionBranches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ForumSectionBranches == null)
            {
                return NotFound();
            }

            var forumSectionBranch = await _context.ForumSectionBranches
                .Include(f => f.Administrator)
                .Include(f => f.SectionName)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (forumSectionBranch == null)
            {
                return NotFound();
            }

            return View(forumSectionBranch);
        }

        // POST: ForumSectionBranches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ForumSectionBranches == null)
            {
                return Problem("Entity set 'TheFifthGroupOfTopicsContext.ForumSectionBranches'  is null.");
            }
            var forumSectionBranch = await _context.ForumSectionBranches.FindAsync(id);
            if (forumSectionBranch != null)
            {
                _context.ForumSectionBranches.Remove(forumSectionBranch);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ForumSectionBranchExists(int id)
        {
          return _context.ForumSectionBranches.Any(e => e.Id == id);
        }
    }
}
