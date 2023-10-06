using System.Collections.Generic;
using System.Xml.Serialization;

namespace DailyMealPlanner.Models
{
    public class Category
    {

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "Product")]
        public List<Product> Products { get; set; }

        [XmlAttribute(AttributeName = "description")]
        public string Description { get; set; }
        public Category()
        {
            Products = new List<Product>();
        }
    }
}