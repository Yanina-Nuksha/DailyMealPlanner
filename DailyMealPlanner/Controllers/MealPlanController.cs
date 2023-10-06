using DailyMealPlanner.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DailyMealPlanner.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using System.IO;

namespace DailyMealPlanner.Controllers
{
    [Authorize]
    public class MealPlanController : Controller
    {
        DBContext dB = new DBContext("server = localhost; port=3306;database=productsdb;user=root;password=5F108q~12");
        public IActionResult Index() 
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User user = dB.GetUserById(userId);
            user.GetMealPlan();
            return View(user);
        }
        public IActionResult ProductList(string? category, string? name)
        {
            //Db db = XmlReader.ReadProducts("..\\DailyMealPlanner\\Services\\FoodProducts.xml");
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            List<string> categories = dB.GetCategories();
            categories.Insert(0, "Все");
            MealProducts mealProducts = null;
            if (!String.IsNullOrEmpty(category) && !category.Equals("Все") && !String.IsNullOrEmpty(name))
            {
                mealProducts = new MealProducts(dB.GetProductsByNameCategory(name, category), userId, categories);
            }
            else if (!String.IsNullOrEmpty(name) && (String.IsNullOrEmpty(category) || category.Equals("Все")))
            {
                mealProducts = new MealProducts(dB.GetProductsByName(name), userId, categories);
            }
            else if (String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(category) && !category.Equals("Все"))
            {
                mealProducts = new MealProducts(dB.GetProductsByCategory(category), userId, categories);
            }
            else
                mealProducts = new MealProducts(dB.GetAllProducts(), userId, categories);
            return View(mealProducts);
        }
        public IActionResult AddBreakfast(long productId, string type)
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            dB.AddMealProductToDB("breakfast", productId, userId);
            return RedirectToAction("Index", new { id = userId });
        }
        public IActionResult AddLunch(long productId, string type)
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            dB.AddMealProductToDB("lunch", productId, userId);
            return RedirectToAction("Index", new { id = userId });
        }
        public IActionResult AddDinner(long productId, string type)
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            dB.AddMealProductToDB("dinner", productId, userId);
            return RedirectToAction("Index", new { id = userId });
        }
        [HttpGet]
        public IActionResult Edit(long productId)
        {
            MealProduct mealProduct = dB.GetMealProductById(productId);            
            return View(mealProduct);
        }
        [HttpPost]
        public IActionResult Edit(MealProduct mealProduct)
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            dB.UpdateMealProduct(mealProduct.Gramms, mealProduct.ProductId);
            return RedirectToAction("Index", new { id = userId });
        }
        public IActionResult Delete(long productId, string type)
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            MealProduct product = dB.GetMealProduct(productId, userId, type);
            dB.DeleteMealProductFromDB(product.MealProductId);
            return RedirectToAction("Index", new { id = userId });
        }
        public IActionResult ExportToXml()
        {
            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            MemoryStream stream = Services.XmlExport.ExportToXML(userId);
            return File(stream, "application/xml", "mealplan.xml"); 
        }
    }
}
