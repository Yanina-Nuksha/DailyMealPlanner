using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DailyMealPlanner.Models
{
    public class MealProduct
    {
        public long MealProductId { get; set; }  
        public long ProductId { get; set; }
        public long UserId { get; set; }
        public string Type { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Граммов меньше 1")]
        public int Gramms { get; set; }
    }
}
