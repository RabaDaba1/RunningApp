using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using RunningApp.Areas.Identity.Data;
using RunningApp.Data;
using RunningApp.Models;

namespace RunningApp.Controllers;

[Authorize]
public class ResultController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;
    private readonly AuthDbContext _authContext;
    
    public ResultController(ApplicationDbContext context, AuthDbContext authContext, UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        _authContext = authContext;
        _context = context;
    }
    
    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);

        var results = await _context.Results
            .Where(r => r.UserId == userId)
            .Include(r => r.Event)
            .ToListAsync();

        return View(results);
    }
    
    [HttpPost("CreateResult")]
    public async Task<IActionResult> CreateResult(string userFirstName, string userLastName, string eventName, TimeSpan time)
    {
        var e = await _context.Events.FirstOrDefaultAsync(e => e.Name == eventName);
        var user = await _authContext.Users.FirstOrDefaultAsync(u => u.FirstName == userFirstName && u.LastName == userLastName);
        
        if (e == null)
        {
            return BadRequest("Event not found.");
        }
        
        if (user == null)
        {
            return BadRequest("User not found.");
        }
        
        Console.WriteLine(user.Id);
        Console.WriteLine(e.Id);
        
        var result = new Result
        {
            UserId = user.Id,
            EventId = e.Id,
            Time = time
        };
        
        if (!TryValidateModel(result))
        {
            return BadRequest("Invalid model.");
        }
        
        _context.Results.Add(result);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Event");
    }
}