using DailyMealPlanner.Models;
using DailyMealPlanner.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DailyMealPlanner.Services
{
    public class XmlExport
    {
        public static MemoryStream ExportToXML(long userId)
        {
            DBContext dB = new DBContext("server = localhost; port=3306;database=productsdb;user=root;password=5F108q~12");
            List<MealProduct> products = dB.GetMealProductsByUser(userId);
            MealPlan plan = new MealPlan();
            foreach(MealProduct mealProduct in products)
            {
                if (mealProduct.Type == "breakfast")
                {
                    Product product = dB.GetProductById(mealProduct.ProductId);
                    product.Gramms = mealProduct.Gramms;
                    plan.ProductsBreakfast.Add(product);
                }
            }
            foreach (MealProduct mealProduct in products)
            {
                if (mealProduct.Type == "lunch")
                {
                    Product product = dB.GetProductById(mealProduct.ProductId);
                    product.Gramms = mealProduct.Gramms;
                    plan.ProductsLunch.Add(product);
                }
            }
            foreach (MealProduct mealProduct in products)
            {
                if (mealProduct.Type == "dinner")
                {
                    Product product = dB.GetProductById(mealProduct.ProductId);
                    product.Gramms = mealProduct.Gramms;
                    plan.ProductsDinner.Add(product);
                }
            }
            XmlSerializer serializer = new XmlSerializer(plan.GetType());
            var stream = new MemoryStream();
            serializer.Serialize(stream, plan);
            stream.Position = 0;
            return stream;
        }
    }
}
