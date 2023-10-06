using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DailyMealPlanner.Models
{
    [XmlRoot("Db")]
    public class Categories
    {
        [XmlElement(ElementName = "Category")]
        public List<Category> CategoriesList { get; set; }
        public Categories()
        {
            CategoriesList = new List<Category>();
        }
    }
}