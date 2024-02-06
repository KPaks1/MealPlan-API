using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MealPlanAPI.Data.Model
{
    public partial class MealIngredient
    {
        public int MealId { get; set; }

        public int IngredientId { get; set; }
    }
}


