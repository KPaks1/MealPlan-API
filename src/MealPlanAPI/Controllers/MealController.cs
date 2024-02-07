using MealPlanAPI.Data;
using MealPlan.ServiceLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MealPlanAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public MealController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Meals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Meal>>> GetMeals()
        {
          if (_context.Meals == null)
          {
              return NotFound();
          }
            return await _context.Meals.ToListAsync();
        }

        // GET: api/Meals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Meal>> GetMeal(int id)
        {
          if (_context.Meals == null)
          {
              return NotFound();
          }
            var meal = await _context.Meals.FindAsync(id);

            if (meal == null)
            {
                return NotFound();
            }

            return meal;
        }

        // GET: api/Meals/Ingredients/5
        [HttpGet("Ingredients/{id}")]
        public async Task<ActionResult<IEnumerable<Ingredient>>> GetMealIngredients(int id)
        {
            var meal = await GetMeal(id);

            var mealIngredients = await _context.MealIngredients.Where(i=>i.MealId == id).Select(i=>i.IngredientId).ToListAsync();

            if (mealIngredients == null || !mealIngredients.Any())
            {
                return NotFound();
            }

            IList<Ingredient> ingredients = new List<Ingredient>();
            Ingredient ingredient = null;

            foreach (int ingredientId in mealIngredients)
            {
                ingredient = await _context.Ingredients.FindAsync(ingredientId);

                if (ingredient != null)
                {
                    ingredients.Add(ingredient);
                }   
            }

            if (ingredients.Any())
            {
                return ingredients.ToList();
            }
            return NotFound();

        }

        // PUT: api/Meals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeal(int id, Meal meal)
        {
            if (id != meal.MealId)
            {
                return BadRequest();
            }

            _context.Entry(meal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MealExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Meals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Meal>> PostMeal(Meal meal)
        {
          if (_context.Meals == null)
          {
              return Problem("Entity set 'DatabaseContext.Meals'  is null.");
          }
            _context.Meals.Add(meal);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMeal", new { id = meal.MealId }, meal);
        }

        // DELETE: api/Meals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeal(int id)
        {
            if (_context.Meals == null)
            {
                return NotFound();
            }
            var meal = await _context.Meals.FindAsync(id);

            if (meal == null)
            {
                return NotFound();
            }

            _context.Meals.Remove(meal);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MealExists(int id)
        {
            return (_context.Meals?.Any(e => e.MealId == id)).GetValueOrDefault();
        }
    }
}
