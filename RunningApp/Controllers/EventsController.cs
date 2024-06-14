using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunningApp.Data;
using RunningApp.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using RunningApp.Areas.Identity.Data;
using RunningApp.Filters;

namespace RunningApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ValidateApiTokenAttribute _validateApiToken;

        public EventsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ValidateApiTokenAttribute validateApiToken)
        {
            _context = context;
            _userManager = userManager;
            _validateApiToken = validateApiToken;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents([FromQuery] string token)
        {
            // Validate the token
            if (!(await ValidateToken(token)))
            {
                return Unauthorized(new { error = "Invalid API token." });
            }

            return await _context.Events.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id, [FromQuery] string token)
        {
            // Validate the token
            if (!(await ValidateToken(token)))
            {
                return Unauthorized(new { error = "Invalid API token." });
            }

            var eventItem = await _context.Events.FindAsync(id);

            if (eventItem == null)
            {
                return NotFound();
            }

            return eventItem;
        }

        // Method to validate the token
        private async Task<bool> ValidateToken(string token)
        {
            // Get the authenticated user
            var user = await _userManager.GetUserAsync(User);
            Console.Out.WriteLine("TOken" +token);
            Console.Out.WriteLine("User token "+ user.ApiToken);
            
            if ( "1234567890abcdef" != token)
            {
                return false;
            }

            return true;
        }
    }
}
