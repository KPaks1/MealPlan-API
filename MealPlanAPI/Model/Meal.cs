using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MealPlanAPI.Model;

namespace MealPlanAPI.Model
{
    public partial class Meal
    {
        public int MealId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public DateTime CreateTimestamp { get; set; }
        public DateTime UpdateTimestamp { get; set; }
    }
}
    

