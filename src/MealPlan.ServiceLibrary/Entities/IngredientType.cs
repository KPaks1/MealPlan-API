namespace MealPlan.ServiceLibrary.Entities
{
    public partial class IngredientType
    {
        public int IngredientTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateTimestamp { get; set; }
        public DateTime UpdateTimestamp { get; set; }
    }
}


