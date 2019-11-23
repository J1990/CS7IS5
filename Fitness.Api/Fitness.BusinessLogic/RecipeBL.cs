using System.Collections.Generic;
using Fitness.DataAccess;
using Fitness.DataObjects;

namespace Fitness.BusinessLogic
{
    public class RecipeBL
    {
        RecipeDA recipeDA = null;

        public RecipeBL()
        {
            recipeDA = new RecipeDA();
        }

        public List<Recipe> GetAllRecipes()
        {
            return recipeDA.GetAllRecipes();
        }
    }
}

