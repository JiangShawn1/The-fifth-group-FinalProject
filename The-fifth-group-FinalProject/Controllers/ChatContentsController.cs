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
    public class ChatContentsController : Controller
    {
        private readonly TheFifthGroupOfTopicsContext _context;

        public ChatContentsController(TheFifthGroupOfTopicsContext context)
        {
            _context = context;
        }

        // GET: ChatContents
        public async Task<IActionResult> Index()
        {
            var theFifthGroupOfTopicsContext = _context.ChatContents.Include(c => c.ChatRoom).Include(c => c.Employee);
            return View(await theFifthGroupOfTopicsContext.ToListAsync());
        }

        // GET: ChatContents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ChatContents == null)
            {
                return NotFound();
            }

            var chatContent = await _context.ChatContents
                .Include(c => c.ChatRoom)
                .Include(c => c.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chatContent == null)
            {
                return NotFound();
            }

            return View(chatContent);
        }

        // GET: ChatContents/Create
        public IActionResult Create()
        {
            ViewData["ChatRoomId"] = new SelectList(_context.ChatRooms, "Id", "Id");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id");
            return View();
        }

        // POST: ChatContents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SentTime,ChatContent1,MemberId,ChatRoomId,EmployeeId")] ChatContent chatContent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chatContent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChatRoomId"] = new SelectList(_context.ChatRooms, "Id", "Id", chatContent.ChatRoomId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", chatContent.EmployeeId);
            return View(chatContent);
        }

        // GET: ChatContents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ChatContents == null)
            {
                return NotFound();
            }

            var chatContent = await _context.ChatContents.FindAsync(id);
            if (chatContent == null)
            {
                return NotFound();
            }
            ViewData["ChatRoomId"] = new SelectList(_context.ChatRooms, "Id", "Id", chatContent.ChatRoomId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", chatContent.EmployeeId);
            return View(chatContent);
        }

        // POST: ChatContents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SentTime,ChatContent1,MemberId,ChatRoomId,EmployeeId")] ChatContent chatContent)
        {
            if (id != chatContent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chatContent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChatContentExists(chatContent.Id))
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
            ViewData["ChatRoomId"] = new SelectList(_context.ChatRooms, "Id", "Id", chatContent.ChatRoomId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", chatContent.EmployeeId);
            return View(chatContent);
        }

        // GET: ChatContents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ChatContents == null)
            {
                return NotFound();
            }

            var chatContent = await _context.ChatContents
                .Include(c => c.ChatRoom)
                .Include(c => c.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chatContent == null)
            {
                return NotFound();
            }

            return View(chatContent);
        }

        // POST: ChatContents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ChatContents == null)
            {
                return Problem("Entity set 'TheFifthGroupOfTopicsContext.ChatContents'  is null.");
            }
            var chatContent = await _context.ChatContents.FindAsync(id);
            if (chatContent != null)
            {
                _context.ChatContents.Remove(chatContent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChatContentExists(int id)
        {
          return _context.ChatContents.Any(e => e.Id == id);
        }
    }
}
