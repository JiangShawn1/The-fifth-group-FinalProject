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
    public class ForumSectionBranch1TopicsThreadController : Controller
    {
        private readonly TheFifthGroupOfTopicsContext _context;

        public ForumSectionBranch1TopicsThreadController(TheFifthGroupOfTopicsContext context)
        {
            _context = context;
        }

        // GET: ForumSectionBranch1TopicsThread
        public async Task<IActionResult> Index()
        {
            var theFifthGroupOfTopicsContext = _context.ForumSectionBranch1TopicsThreads.Include(f => f.ReplyMember).Include(f => f.Topic).Where(f => f.TopicId == 1);

            return View(await theFifthGroupOfTopicsContext.ToListAsync());
        }



        public async Task<IActionResult> Index2()
        {
            var theFifthGroupOfTopicsContext = _context.ForumSectionBranch1TopicsThreads.Include(f => f.ReplyMember).Include(f => f.Topic).Where(f => f.TopicId == 2);

            return View(await theFifthGroupOfTopicsContext.ToListAsync());
        }

        // GET: ForumSectionBranch1TopicsThread/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ForumSectionBranch1TopicsThreads == null)
            {
                return NotFound();
            }

            var forumSectionBranch1TopicsThread = await _context.ForumSectionBranch1TopicsThreads
                .Include(f => f.ReplyMember)
                .Include(f => f.Topic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (forumSectionBranch1TopicsThread == null)
            {
                return NotFound();
            }

            return View(forumSectionBranch1TopicsThread);
        }

        // GET: ForumSectionBranch1TopicsThread/Create
        public IActionResult Create()
        {
            ViewData["ReplyMemberId"] = new SelectList(_context.Members, "MemberId", "MemberId");
            ViewData["TopicId"] = new SelectList(_context.ForumSectionBranch1Topics, "Id", "Id");
            return View();
        }

        // POST: ForumSectionBranch1TopicsThread/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TopicId,TopicState,ReplyNumber,ReplyContent,ReplyTime,ReplyState,ReplyMemberId")] ForumSectionBranch1TopicsThread forumSectionBranch1TopicsThread)
        {



            //forumSectionBranch1TopicsThread.ReplyMember =
            //    forumSectionBranch1TopicsThread.Topic = 


			if (ModelState.IsValid)
            {
                _context.Add(forumSectionBranch1TopicsThread);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReplyMemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", forumSectionBranch1TopicsThread.ReplyMemberId);
            ViewData["TopicId"] = new SelectList(_context.ForumSectionBranch1Topics, "Id", "Id", forumSectionBranch1TopicsThread.TopicId);
            return View(forumSectionBranch1TopicsThread);
        }

        // GET: ForumSectionBranch1TopicsThread/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ForumSectionBranch1TopicsThreads == null)
            {
                return NotFound();
            }

            var forumSectionBranch1TopicsThread = await _context.ForumSectionBranch1TopicsThreads.FindAsync(id);
            if (forumSectionBranch1TopicsThread == null)
            {
                return NotFound();
            }
            ViewData["ReplyMemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", forumSectionBranch1TopicsThread.ReplyMemberId);
            ViewData["TopicId"] = new SelectList(_context.ForumSectionBranch1Topics, "Id", "Id", forumSectionBranch1TopicsThread.TopicId);
            return View(forumSectionBranch1TopicsThread);
        }

        // POST: ForumSectionBranch1TopicsThread/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TopicId,TopicState,ReplyNumber,ReplyContent,ReplyTime,ReplyState,ReplyMemberId")] ForumSectionBranch1TopicsThread forumSectionBranch1TopicsThread)
        {
            if (id != forumSectionBranch1TopicsThread.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(forumSectionBranch1TopicsThread);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ForumSectionBranch1TopicsThreadExists(forumSectionBranch1TopicsThread.Id))
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
            ViewData["ReplyMemberId"] = new SelectList(_context.Members, "MemberId", "MemberId", forumSectionBranch1TopicsThread.ReplyMemberId);
            ViewData["TopicId"] = new SelectList(_context.ForumSectionBranch1Topics, "Id", "Id", forumSectionBranch1TopicsThread.TopicId);
            return View(forumSectionBranch1TopicsThread);
        }

        // GET: ForumSectionBranch1TopicsThread/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ForumSectionBranch1TopicsThreads == null)
            {
                return NotFound();
            }

            var forumSectionBranch1TopicsThread = await _context.ForumSectionBranch1TopicsThreads
                .Include(f => f.ReplyMember)
                .Include(f => f.Topic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (forumSectionBranch1TopicsThread == null)
            {
                return NotFound();
            }

            return View(forumSectionBranch1TopicsThread);
        }

        // POST: ForumSectionBranch1TopicsThread/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ForumSectionBranch1TopicsThreads == null)
            {
                return Problem("Entity set 'TheFifthGroupOfTopicsContext.ForumSectionBranch1TopicsThreads'  is null.");
            }
            var forumSectionBranch1TopicsThread = await _context.ForumSectionBranch1TopicsThreads.FindAsync(id);
            if (forumSectionBranch1TopicsThread != null)
            {
                _context.ForumSectionBranch1TopicsThreads.Remove(forumSectionBranch1TopicsThread);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ForumSectionBranch1TopicsThreadExists(int id)
        {
          return (_context.ForumSectionBranch1TopicsThreads?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
