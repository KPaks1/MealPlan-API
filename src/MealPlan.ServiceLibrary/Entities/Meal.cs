namespace MealPlan.ServiceLibrary.Entities
{
    public partial class Meal
    {
        public int MealId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreateTimestamp { get; set; }
        public DateTime UpdateTimestamp { get; set; }
    }
}


