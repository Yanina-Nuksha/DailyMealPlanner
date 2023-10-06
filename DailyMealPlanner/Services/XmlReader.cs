using DailyMealPlanner.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DailyMealPlanner.Services
{
    public class XmlReader
    {
        public static Categories ReadProducts(string fileName)
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = customCulture;
            var serializer = new XmlSerializer(typeof(Categories));
            Categories categories = new Categories();
            using (var stream = File.OpenRead(fileName))
            {
                categories = (Categories)serializer.Deserialize(stream);
            }
            return categories;
        }
    }
}
