using System.Collections.Generic;
using Fitness.DataAccess;
using Fitness.DataObjects;

namespace Fitness.BusinessLogic
{
    public class RecipeBL
    {
        RecipeDA _recipeDA = null;
        RecipeRecommender _recipeRecommender = null;

        public RecipeBL()
        {
            _recipeDA = new RecipeDA();
            _recipeRecommender = new RecipeRecommender(_recipeDA);
        }

        public List<Recipe> GetAllRecipes()
        {
            return _recipeDA.GetAllRecipes();
        }

        public Dictionary<IdealMealTime, List<Recipe>> GetRecipes(int userId)
        {
            return _recipeRecommender.GetRecipesForUser(userId);
        }

        public Dictionary<IdealMealTime, List<Recipe>> GetRecipes(string searchQuery)
        {
            return _recipeDA.GetRecipes(searchQuery);
        }
    }
}

