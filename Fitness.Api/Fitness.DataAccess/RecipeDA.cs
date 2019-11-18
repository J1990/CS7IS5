using Fitness.DataObjects;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Fitness.DataAccess
{
    public class RecipeDA
    {
        string connectionString = string.Empty;

        public RecipeDA()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
        }

        public List<Recipe> GetAllRecipes()
        {
            List<Recipe> allRecipes = new List<Recipe>();

            using (var dbConnection = new SqlConnection(connectionString))
            {
                using (var sqlAdapter = new SqlDataAdapter(DatabaseQueries.SELECT_ALL_RECIPES, dbConnection))
                {
                    var recipeTable = new DataTable();
                    sqlAdapter.Fill(recipeTable);

                    foreach (DataRow row in recipeTable.Rows)
                    {
                        var recipe = new Recipe
                        {
                            RecipeId = row.Field<string>("recipe_id"),
                            Ingredients = row.Field<string>("ingredients")
                        };
                        allRecipes.Add(recipe);
                    }
                }
            }

            return allRecipes;
        }

    }
}
