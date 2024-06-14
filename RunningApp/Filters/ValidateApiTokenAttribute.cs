using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using RunningApp.Areas.Identity.Data;

namespace RunningApp.Filters
{
    public class ValidateApiTokenAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
            var apiToken = context.HttpContext.Request.Query["token"];

            // Assuming you have a logged-in user
            var user = userManager.GetUserAsync(context.HttpContext.User).Result;

            if (user == null || user.ApiToken != apiToken)
            {
                context.Result = new Microsoft.AspNetCore.Mvc.JsonResult(new { error = "Invalid API token." })
                    { StatusCode = 401 };
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}