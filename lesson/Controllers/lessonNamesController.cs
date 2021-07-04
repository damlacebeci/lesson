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
    public class lessonNamesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public lessonNamesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: lessonNames
        public async Task<IActionResult> Index()
        {
            return View(await _context.LessonNames.ToListAsync());
        }

        // GET: lessonNames/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessonName = await _context.LessonNames
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lessonName == null)
            {
                return NotFound();
            }

            return View(lessonName);
        }

        // GET: lessonNames/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: lessonNames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] lessonName lessonName)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lessonName);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lessonName);
        }

        // GET: lessonNames/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessonName = await _context.LessonNames.FindAsync(id);
            if (lessonName == null)
            {
                return NotFound();
            }
            return View(lessonName);
        }

        // POST: lessonNames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] lessonName lessonName)
        {
            if (id != lessonName.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lessonName);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!lessonNameExists(lessonName.Id))
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
            return View(lessonName);
        }

        // GET: lessonNames/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessonName = await _context.LessonNames
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lessonName == null)
            {
                return NotFound();
            }

            return View(lessonName);
        }

        // POST: lessonNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lessonName = await _context.LessonNames.FindAsync(id);
            _context.LessonNames.Remove(lessonName);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool lessonNameExists(int id)
        {
            return _context.LessonNames.Any(e => e.Id == id);
        }
    }
}
