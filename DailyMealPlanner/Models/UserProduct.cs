namespace DailyMealPlanner.Models
{
    public class UserProduct
    {
        public int UserProductId { get; set; }
        public long UserId { get; set; }
        public long ProductId { get; set; }
    }
}
