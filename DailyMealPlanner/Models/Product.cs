using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace DailyMealPlanner.Models
{
    [XmlRoot("Product")]
    public class Product
    {
        public long ProductId { get; set; }
        [XmlElement(ElementName = "Name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "Gramms")]
        public int Gramms { get; set; }
        [XmlElement(ElementName = "Fats")]
        public double Fats { get; set; }
        [XmlElement(ElementName = "Protein")]
        public double Protein { get; set; }
        [XmlElement(ElementName = "Carbs")]
        public double Carbs { get; set; }
        [XmlElement(ElementName = "Calories")]
        public double Calories { get; set; }
        [XmlElement(ElementName = "Category")]
        public string Category { get; set; }
        public Product(string name, int gramms, double fats, double protein, double carbs, double calories)
        {
            Name = name;
            Gramms = gramms;
            Fats = fats;
            Protein = protein;
            Carbs = carbs;
            Calories = calories;
        }
        public Product() { }
    }
}