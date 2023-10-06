using DailyMealPlanner.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DailyMealPlanner.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult About()
        {
            return Content("Authorized");
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return Content("не аутентифицирован");
        }
    }
}
