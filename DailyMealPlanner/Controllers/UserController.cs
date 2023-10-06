using DailyMealPlanner.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DailyMealPlanner.Controllers
{
    [Authorize]
    public class UserController : Controller
    {        
        DBContext dB = new DBContext("server=localhost; port=3306; database=productsdb; user=root; password=5F108q~12");
        public IActionResult Index() 
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User user = dB.GetUserById(userId);
            if (user != null)
                return View(user);
            return NotFound();
        }
        [HttpGet]
        public IActionResult UpdateUser()
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User user = dB.GetUserById(userId);
            return View(user);
        }
        [HttpPost]
        public IActionResult UpdateUser(User user)
        {
            long userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            dB.UpdateUser(user, userId);
            return RedirectToAction("Index");
        }
    }
}
