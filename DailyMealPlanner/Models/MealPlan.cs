using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace DailyMealPlanner.Models
{
    public class MealPlan
    {
        public List<Product> ProductsBreakfast { get; set; }
        public List<Product> ProductsLunch { get; set; }
        public List<Product> ProductsDinner { get; set; }
        [XmlIgnore]
        public List<MealProduct> MealProducts { get; set; }
        public MealPlan()
        {
            ProductsBreakfast = new List<Product>();
            ProductsLunch = new List<Product>();
            ProductsDinner = new List<Product>();
            MealProducts = new List<MealProduct>();
        }
        public double GetAllCalories()
        {
            double allCalories = 0;
            foreach(Product product in ProductsBreakfast)
            {
                allCalories += product.Calories;
            }
            foreach (Product product in ProductsLunch)
            {
                allCalories += product.Calories;
            }
            foreach (Product product in ProductsDinner)
            {
                allCalories += product.Calories;
            }
            return allCalories;
        }
        public void AddProductsBreakfast(List<Product> products)
        {
            foreach (Product product in products)
                ProductsBreakfast.Add(product);
        }
        public void AddProductsLunch(List<Product> products)
        {
            foreach (Product product in products)
                ProductsLunch.Add(product);
        }
        public void AddProductsDinner(List<Product> products)
        {
            foreach (Product product in products)
                ProductsDinner.Add(product);
        }
        public int GetGrammsOfProduct(long productId)
        {
            int gramms = 0;
            foreach(MealProduct mealProduct in MealProducts)
            {
                if(mealProduct.ProductId == productId)
                    gramms = mealProduct.Gramms;
            }
            return gramms;
        }
        public MealProduct GetMealProduct(long productId) 
        {
            foreach (MealProduct mealProduct in MealProducts)
            {
                if (mealProduct.ProductId == productId)
                    return mealProduct;
            }
            return null;
        }
    }
}