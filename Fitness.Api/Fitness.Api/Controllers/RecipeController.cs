using Fitness.BusinessLogic;
using Fitness.DataObjects;
using System.Collections.Generic;
using System.Web.Http;

namespace Fitness.Api.Controllers
{
    public class RecipeController : ApiController
    {
        public IEnumerable<Recipe> Get()
        {
            RecipeBL recipeBL = new RecipeBL();
            return recipeBL.GetAllRecipes();
        }

        public Dictionary<IdealMealTime, List<Recipe>> Get(int userId)
        {
            RecipeBL recipeBL = new RecipeBL();
            return recipeBL.GetRecipes(userId);
        }

        public Dictionary<IdealMealTime, List<Recipe>> Get(string searchQuery)
        {
            RecipeBL recipeBL = new RecipeBL();
            return recipeBL.GetRecipes(searchQuery);
        }
    }
}
