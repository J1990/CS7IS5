using Fitness.DataObjects;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

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
                            RecipeId = row.Field<int>("recipe_id"),
                            Ingredients = row.Field<string>("ingredients")
                        };
                        allRecipes.Add(recipe);
                    }
                }
            }

            return allRecipes;
        }

        public List<Recipe> GetRecipes(int userId, IdealMealTime idealMealTime, double requiredCarbCalories, double requiredProteinCalories, double requiredFatCalories)
        {
            List<Recipe> recipes = new List<Recipe>();

            using (var dbConnection = new SqlConnection(connectionString))
            {
                var query = string.Format(CultureInfo.InvariantCulture, DatabaseQueries.SELECT_RECIPES_WITH_CALORIES_FOR_USER, userId, (short)idealMealTime, requiredCarbCalories, 
                    requiredProteinCalories, requiredFatCalories);

                using (var sqlAdapter = new SqlDataAdapter(query, dbConnection))
                {
                    var recipeTable = new DataTable();
                    sqlAdapter.Fill(recipeTable);

                    foreach (DataRow row in recipeTable.Rows)
                    {
                        var recipe = new Recipe
                        {
                            RecipeId = row.Field<int>("recipe_id"),
                            TotalTimeMinutes = row.Field<int>("total_time_minutes"),
                            Description = row.Field<string>("description"),
                            Ingredients = row.Field<string>("ingredients"),
                            Instructions = row.Field<string>("instructions"),
                            PhotoUrl = row.Field<string>("photo_url"),
                            RatingStars = row.Field<double>("rating_stars"),
                            ReviewCount = row.Field<int>("review_count"),
                            IdealMealTime = (IdealMealTime)row.Field<short>("IdealMealTime"),
                            CarbsCalories = row.Field<double>("CarbsCalories"),
                            ProteinCalories = row.Field<double>("ProteinCalories"),
                            FatCalories = row.Field<double>("FatCalories"),
                            FeedbackType = (UserFeedbackType)row.Field<short>("FeedbackType")
                        };
                        recipes.Add(recipe);
                    }
                }
            }

            return recipes;
        }
    }
}
