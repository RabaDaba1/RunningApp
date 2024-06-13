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
            .Include(r => r.Event)
            .ToListAsync();

        return View(results);
    }
    
    [HttpPost("CreateResult")]
    public async Task<IActionResult> CreateResult(string userFirstName, string userLastName, string eventName, TimeSpan time)
    {
        var e = await _context.Events.FirstOrDefaultAsync(e => e.Name == eventName);
        var userId = _userManager.GetUserId(User);
       // var user = await _authContext.Users.FirstOrDefaultAsync(u => u.FirstName == userFirstName && u.LastName == userLastName);
        
        if (e == null)
        {
            return BadRequest("Event not found.");
        }
        
        if (userId == null)
        {
            return BadRequest("User not found.");
        }
     
        
        Console.WriteLine(userId);
        Console.WriteLine(e.Id);

        var athlete = await _context.Athletes
            .FirstOrDefaultAsync(a => a.FirstName == userFirstName && a.LastName == userLastName);

        if (athlete == null)
        {
            // If athlete does not exist, create a new one
            athlete = new Athlete
            {  
                FirstName = userFirstName,
                LastName = userLastName
            };
        
            _context.Athletes.Add(athlete);
        }

        // Create the result
        var result = new Result
        {   
            EventId = e.Id,
            AthleteId = athlete.Id,
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