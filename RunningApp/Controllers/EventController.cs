using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunningApp.Areas.Identity.Data;
using RunningApp.Data;
using RunningApp.Models;

namespace RunningApp.Controllers;

[Authorize(Roles = "Organizer")]
public class EventController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    
    public EventController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);

        var events = await _context.Events
            .Where(e => e.OrganizerId == userId)
            .ToListAsync();

        return View(events);
    }
    
    [HttpPost("CreateEvent")]
    public async Task<IActionResult> CreateEvent(string name, string location, string date, string distance)
    {
        var userId = _userManager.GetUserId(User);

        var e = new Event
        {
            Name = name,
            Location = location,
            Date = date,
            Distance = distance,
            OrganizerId = userId
        };

        _context.Events.Add(e);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}