using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunningApp.Data;
using RunningApp.Models;

namespace RunningApp.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string userName, string eventName)
        {
            var query = _context.Results.Include(r => r.User).Include(r => r.Event).AsQueryable();;

            if (!string.IsNullOrEmpty(userName))
            {
                query = query.Where(r => r.User.FirstName.Contains(userName)).Where(r => r.User.LastName.Contains(userName));
            }

            if (!string.IsNullOrEmpty(eventName))
            {
                query = query.Where(r => r.Event.Name.Contains(eventName));
            }

            var results = await query.ToListAsync();
            return View(results);
        }
    }
}