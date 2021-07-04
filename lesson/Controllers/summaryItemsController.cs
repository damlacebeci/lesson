using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lesson.Data;
using lesson.Models;

namespace lesson.Controllers
{
    public class summaryItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public summaryItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: summaryItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Summaries.Include(s => s.lessonName);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: summaryItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var summaryItem = await _context.Summaries
                .Include(s => s.lessonName)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (summaryItem == null)
            {
                return NotFound();
            }

            return View(summaryItem);
        }

        // GET: summaryItems/Create
        public IActionResult Create()
        {
            ViewData["lessonNameId"] = new SelectList(_context.LessonNames, "Id", "Name");
            return View();
        }

        // POST: summaryItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,Worked,lessonNameId")] summaryItem summaryItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(summaryItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["lessonNameId"] = new SelectList(_context.LessonNames, "Id", "Name", summaryItem.lessonNameId);
            return View(summaryItem);
        }

        // GET: summaryItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var summaryItem = await _context.Summaries.FindAsync(id);
            if (summaryItem == null)
            {
                return NotFound();
            }
            ViewData["lessonNameId"] = new SelectList(_context.LessonNames, "Id", "Name", summaryItem.lessonNameId);
            return View(summaryItem);
        }

        // POST: summaryItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,Worked,lessonNameId")] summaryItem summaryItem)
        {
            if (id != summaryItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(summaryItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!summaryItemExists(summaryItem.Id))
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
            ViewData["lessonNameId"] = new SelectList(_context.LessonNames, "Id", "Name", summaryItem.lessonNameId);
            return View(summaryItem);
        }

        // GET: summaryItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var summaryItem = await _context.Summaries
                .Include(s => s.lessonName)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (summaryItem == null)
            {
                return NotFound();
            }

            return View(summaryItem);
        }

        // POST: summaryItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var summaryItem = await _context.Summaries.FindAsync(id);
            _context.Summaries.Remove(summaryItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool summaryItemExists(int id)
        {
            return _context.Summaries.Any(e => e.Id == id);
        }
    }
}
