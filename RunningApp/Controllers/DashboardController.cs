using Microsoft.AspNetCore.Mvc;

namespace RunningApp.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}