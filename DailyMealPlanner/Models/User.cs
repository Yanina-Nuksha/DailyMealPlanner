using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;
#nullable disable

namespace DailyMealPlanner.Models
{
    public class User : IdentityUser
    {
        public long UserId { get; set; }
        [Range(1, 200, ErrorMessage = "Weight 1-200 only")]
        public int Weight { get; set; }
        [Range(1, 300, ErrorMessage = "Height 1-300 only")]
        public int Height { get; set; }
        [Range(1, 100, ErrorMessage = "Ages 1-100 only")]
        public int Age { get; set; }
        public string Activity { get; set; }
        [Required(ErrorMessage = "Please enter your login")]
        public string Login { get; set; }
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{6,}$", ErrorMessage = "Password should have length from 6, numbers, latin letters, lowercase & uppercase characters")]
        public string Password { get; set; }
        public List<Product> Products;
        public double CaloriesRate { get => GetCaloriesRate(); }
        public MealPlan MealPlan { get => GetMealPlan(); }
        public List<UserProduct> UserProducts { get; set; } 
        public User(int weight, int height, int age, string activity, string password, string login)
        {
            Weight = weight;
            Height = height;
            Age = age;
            Activity = activity;
            Password = password;
            Login = login;
            //Products = new List<Product>();
        }
        public User() { }
        public List<Product> GetProducts()
        {
            DBContext dB = new DBContext("server = localhost; port=3306;database=productsdb;user=root;password=5F108q~12");
            return dB.GetAllUserProducts(UserId);
        }
        public MealPlan GetMealPlan()
        {
            DBContext dB = new DBContext("server = localhost; port=3306;database=productsdb;user=root;password=5F108q~12");
            MealPlan mealPlan = new MealPlan();
            List<MealProduct> mealProducts = dB.GetMealProductsByUser(UserId);
            mealPlan.MealProducts = mealProducts;
            foreach (MealProduct mealProduct in mealProducts)
                switch(mealProduct.Type)
                {
                    case "breakfast":
                        mealPlan.ProductsBreakfast.Add(dB.GetProductById(mealProduct.ProductId));
                        break;
                    case "lunch":
                        mealPlan.ProductsLunch.Add(dB.GetProductById(mealProduct.ProductId));
                        break;
                    case "dinner":
                        mealPlan.ProductsDinner.Add(dB.GetProductById(mealProduct.ProductId));
                        break;
                }
            return mealPlan;
        }
        public double GetCaloriesRate()
        {
            double caloriesRate = 0;
            double dailyActivity = 0;
            switch (Activity)
            {
                case "Low":
                    dailyActivity = 1.2;
                    break;
                case "Normal":
                    dailyActivity = 1.375;
                    break;
                case "Average":
                    dailyActivity = 1.55;
                    break;
                case "High":
                    dailyActivity = 1.725;
                    break;
            }
            caloriesRate = 444.593 + 9.247 * Weight + 3.098 * Height + 4.33 * Age + dailyActivity;
            return caloriesRate;
        }
    }
}