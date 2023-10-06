using DailyMealPlanner.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DailyMealPlanner.ViewModels
{
    public class MealProducts
    {
        public IEnumerable<Product> Products { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public string Category { get; set; }
        public SelectList Categories { get; set; }
        public MealProducts(List<Product> products, long id, List<string> categories)
        {
            Products = products;
            UserId = id;
            Categories = new SelectList(categories);
        }
        public MealProducts(List<Product> products, User user)
        {
            Products = products;
            User = user;
        }
        public MealProducts() { }
    }
}
