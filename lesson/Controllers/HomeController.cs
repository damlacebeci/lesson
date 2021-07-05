using lesson.Data;
using lesson.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace lesson.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<lessonUser> userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext, UserManager<lessonUser> userManager)
        {
            _logger = logger;
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            List<summaryItem> result;
                if (User.Identity.IsAuthenticated)
            {
                 var lessonUser = await userManager.GetUserAsync(HttpContext.User);
                var query = dbContext.Summaries
                    .Include(t=> t.lessonName)
                    .Where(t =>t.lessonUserId== lessonUser.Id && !t.Worked);
                result = await query.ToListAsync();
            }
            else
            {
                result = new List<summaryItem>();
            }
          
        
            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
