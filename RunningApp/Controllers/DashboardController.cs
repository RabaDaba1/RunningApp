﻿using Microsoft.AspNetCore.Authorization;
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

        public async Task<IActionResult> Index(string athleteFirstName, string athleteLastName, string eventName)
        {
            var query = _context.Results.Include(r => r.Athlete).Include(r => r.Event).AsQueryable();;

            if (!string.IsNullOrEmpty(athleteFirstName))
            {
                query = query.Where(r => r.Athlete.FirstName.Contains(athleteFirstName));
            }
            
            if (!string.IsNullOrEmpty(athleteLastName))
            {
                query = query.Where(r => r.Athlete.LastName.Contains(athleteLastName));
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