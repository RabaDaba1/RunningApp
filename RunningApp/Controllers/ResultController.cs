using System.Globalization;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using RunningApp.Areas.Identity.Data;
using RunningApp.Data;
using RunningApp.Models;
using CsvHelper;
using CsvReader = CsvHelper.CsvReader;

namespace RunningApp.Controllers;

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
    
    [Authorize(Roles = "Runner")]
    public async Task<IActionResult> Index()
    {   
        var userId = _userManager.GetUserId(User);
        var user = await _authContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        
        var results = await _context.Results
            .Where(r => r.Athlete.FirstName == user.FirstName && r.Athlete.LastName == user.LastName)
            .Include(r => r.Event)
            .ToListAsync();

        return View(results);
    }
    
    [Authorize(Roles = "Organizer")]
    [HttpPost("CreateResult")]
    public async Task<IActionResult> CreateResult(string athleteFistName, string athleteLastName, string eventName, TimeSpan time)
    {
        var e = await _context.Events.FirstOrDefaultAsync(e => e.Name == eventName);
        var a = await _context.Athletes.FirstOrDefaultAsync(a => a.FirstName == athleteFistName && a.LastName == athleteLastName);
        
        if (e == null)
        {
            return BadRequest("Event not found.");
        }
        
        if (a == null)
        {
            a = new Athlete
            {
                FirstName = athleteFistName,
                LastName = athleteLastName
            };
            
            _context.Athletes.Add(a);
            await _context.SaveChangesAsync();
        }
        
        a = await _context.Athletes.FirstOrDefaultAsync(a => a.FirstName == athleteFistName && a.LastName == athleteLastName);
        
        var result = new Result
        {
            AthleteId = a.Id,
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
    
    [Authorize(Roles = "Organizer")]
    [HttpPost("UploadResults")]
    public async Task<IActionResult> UploadResults(string eventName, IFormFile csvFile)
    {
        if (csvFile == null || csvFile.Length == 0)
        {
            return BadRequest("Please upload a valid CSV file.");
        }

        var e = await _context.Events.FirstOrDefaultAsync(e => e.Name == eventName);
        if (e == null)
        {
            return BadRequest("Event not found.");
        }

        using (var stream = new StreamReader(csvFile.OpenReadStream()))
        using (var csv = new CsvReader(stream, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<CsvResultModel>().ToList();

            foreach (var record in records)
            {
                var a = await _context.Athletes.FirstOrDefaultAsync(a => a.FirstName == record.FirstName && a.LastName == record.LastName);
                if (a == null)
                {
                    a = new Athlete
                    {
                        FirstName = record.FirstName,
                        LastName = record.LastName
                    };
                    _context.Athletes.Add(a);
                    await _context.SaveChangesAsync();
                }

                a = await _context.Athletes.FirstOrDefaultAsync(a => a.FirstName == record.FirstName && a.LastName == record.LastName);
            
                var result = new Result
                {
                    AthleteId = a.Id,
                    EventId = e.Id,
                    Time = TimeSpan.Parse(record.Time)
                };

                if (!TryValidateModel(result))
                {
                    return BadRequest("Invalid model.");
                }

                _context.Results.Add(result);
            }

            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Index", "Event");
    }

    public class CsvResultModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Time { get; set; }
    }
}