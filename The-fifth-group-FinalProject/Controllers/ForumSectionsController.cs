using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using The_fifth_group_FinalAPI.DTOs;
using The_fifth_group_FinalProject.Models;
using The_fifth_group_FinalAPI;

namespace The_fifth_group_FinalProject.Controllers
{
    public class ForumSectionsController : Controller
    {
        private readonly TheFifthGroupOfTopicsContext _context;

        public ForumSectionsController(TheFifthGroupOfTopicsContext context)
        {
            _context = context;
        }

        // GET: ForumSections
        public async Task<IActionResult> Index()
        {
            var forum_first = (from a in _context.ForumSectionBranches
                               join b in _context.ForumSections on a.SectionNameId equals b.Id
                               select new Forum_FirstDTO
                               {
                                   Id = b.Id,
                                   SectionName = b.SectionName,
                                   SectionNameId = b.Id,
                                   BranchName = a.BranchName,
                                   AdministratorId = a.AdministratorId
                               });
              return View(await forum_first.ToListAsync());
        }

        // GET: ForumSections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ForumSections == null)
            {
                return NotFound();
            }

            var forumSection = await _context.ForumSections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (forumSection == null)
            {
                return NotFound();
            }

            return View(forumSection);
        }

        // GET: ForumSections/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ForumSections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SectionName")] ForumSection forumSection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(forumSection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(forumSection);
        }

        // GET: ForumSections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ForumSections == null)
            {
                return NotFound();
            }

            var forumSection = await _context.ForumSections.FindAsync(id);
            if (forumSection == null)
            {
                return NotFound();
            }
            return View(forumSection);
        }

        // POST: ForumSections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SectionName")] ForumSection forumSection)
        {
            if (id != forumSection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(forumSection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ForumSectionExists(forumSection.Id))
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
            return View(forumSection);
        }

        // GET: ForumSections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ForumSections == null)
            {
                return NotFound();
            }

            var forumSection = await _context.ForumSections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (forumSection == null)
            {
                return NotFound();
            }

            return View(forumSection);
        }

        // POST: ForumSections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ForumSections == null)
            {
                return Problem("Entity set 'TheFifthGroupOfTopicsContext.ForumSections'  is null.");
            }
            var forumSection = await _context.ForumSections.FindAsync(id);
            if (forumSection != null)
            {
                _context.ForumSections.Remove(forumSection);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ForumSectionExists(int id)
        {
          return _context.ForumSections.Any(e => e.Id == id);
        }
    }
}
