using Fitness.Api.Client.Models;
using Fitness.DataObjects;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Fitness.Api.Client.Controllers
{
    public class NutritionController : Controller
    {
        List<Recipe> recipes = AllRecipes.Instance.Recipes;
        // GET: Nutrition
        public ActionResult Recipe()
        {
            return View("Recipe", recipes);
        }
    }
}