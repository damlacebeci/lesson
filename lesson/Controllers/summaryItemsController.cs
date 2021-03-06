using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lesson.Data;
using lesson.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace lesson.Controllers
{
    [Authorize]
    public class summaryItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<lessonUser> _userManager;

        public summaryItemsController(ApplicationDbContext context, UserManager<lessonUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        // GET: summaryItems
        public async Task<IActionResult> Index(SearchViewModel searchModel)
        {
            var lessonUser = await _userManager.GetUserAsync(HttpContext.User);
            var query = _context.Summaries.Include(s => s.lessonName).Where(t=> t.lessonUserId== lessonUser.Id);
                if (!searchModel.ShowAll)
            {
                query = query.Where(t => !t.Worked);
            }
            if (!String.IsNullOrWhiteSpace(searchModel.SearchText))
            {
                query = query.Where(t => t.Title.Contains(searchModel.SearchText));
            }
            searchModel.Result = await query.ToListAsync();
            return View(searchModel);
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
        [Authorize]
        public IActionResult Create()
        {
            ViewBag.lessonNameSelectList = new SelectList(_context.LessonNames, "Id", "Name");
            return View();
        }

        // POST: summaryItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,Worked,lessonNameId")] summaryItem summaryItem)
        {
            var lessonUser = await _userManager.GetUserAsync(HttpContext.User);
            summaryItem.lessonUserId = lessonUser.Id;
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
          
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (summaryItem.lessonUserId != currentUser.Id)
            {
                return Unauthorized();

            }

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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,Worked,lessonNameId,lessonUserId")] summaryItem summaryItem)
        {
            if (id != summaryItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var oldSummary = await _context.Summaries.FindAsync(id);
                    var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                    if(oldSummary.lessonUserId != currentUser.Id)
                    {
                        return Unauthorized();

                    }
                    oldSummary.Title = summaryItem.Title;
                    oldSummary.lessonNameId = summaryItem.lessonNameId;
                    oldSummary.Worked = summaryItem.Worked;
                    oldSummary.Content = summaryItem.Content;
                    
                    _context.Update(oldSummary);
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
        public async Task<IActionResult>MakeWorked(int id, bool showAll)
        {
            return await ChangeStatus(id, true, showAll);
        }
        public async Task<IActionResult> MakeNotWorked(int id, bool showAll)
        {
            return await ChangeStatus(id, false,showAll);
        }
        private async Task<IActionResult> ChangeStatus(int id, bool status, bool currentShowallValue)
        {
            var summaryItemItem = _context.Summaries.FirstOrDefault(t => t.Id == id);
            if (summaryItemItem == null)
            {
                return NotFound();
            }
            summaryItemItem.Worked = status;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { showall=currentShowallValue});
        }

      
        private bool summaryItemExists(int id)
        {
            return _context.Summaries.Any(e => e.Id == id);
        }
    }
}
