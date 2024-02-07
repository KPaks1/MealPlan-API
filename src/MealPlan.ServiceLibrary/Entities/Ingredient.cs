namespace MealPlan.ServiceLibrary.Entities
{
    public partial class Ingredient
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int IngredientTypeId { get; set; }
        public DateTime CreateTimestamp { get; set; }
        public DateTime UpdateTimestamp { get; set; }
    }
}


