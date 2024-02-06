using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MealPlanAPI.Data.Model
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


