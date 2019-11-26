using Fitness.DataObjects;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

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
                        recipes.Add(GetRecipeObject(row));
                    }
                }
            }

            return recipes;
        }

        public List<Recipe> GetRecipesWithRecipeIds(string recipeIds)
        {
            List<Recipe> recipes = new List<Recipe>();

            using (var dbConnection = new SqlConnection(connectionString))
            {
                var query = string.Format(CultureInfo.InvariantCulture, DatabaseQueries.SELECT_RECIPES_WITH_RECIPE_IDS, recipeIds);

                using (var sqlAdapter = new SqlDataAdapter(query, dbConnection))
                {
                    var recipeTable = new DataTable();
                    sqlAdapter.Fill(recipeTable);

                    foreach (DataRow row in recipeTable.Rows)
                    {
                        recipes.Add(GetRecipeObject(row));
                    }
                }
            }

            return recipes;
        }

        public Dictionary<IdealMealTime, List<Recipe>> GetRecipes(string searchQuery)
        {
            List<Recipe> recipes = new List<Recipe>();

            using (var dbConnection = new SqlConnection(connectionString))
            {
                var query = string.Format(CultureInfo.InvariantCulture, DatabaseQueries.SELECT_RECIPES_BASED_ON_SEARCH_QUERY, searchQuery);

                using (var sqlAdapter = new SqlDataAdapter(query, dbConnection))
                {
                    var recipeTable = new DataTable();
                    sqlAdapter.Fill(recipeTable);

                    foreach (DataRow row in recipeTable.Rows)
                    {
                        recipes.Add(GetRecipeObject(row));
                    }
                }
            }

            return recipes.GroupBy(x => x.IdealMealTime).ToDictionary(y => y.Key, y => y.ToList());
        }

        public List<MLRecommenderParams> GetRecipeParamsForMLTraining()
        {
            List<MLRecommenderParams> parameters = new List<MLRecommenderParams>();

            using (var dbConnection = new SqlConnection(connectionString))
            {
                var query = string.Format(CultureInfo.InvariantCulture, DatabaseQueries.SELECT_ALL_RECIPES_WITH_USER_FEEDBACK);

                using (var sqlAdapter = new SqlDataAdapter(query, dbConnection))
                {
                    var recipeTable = new DataTable();
                    sqlAdapter.Fill(recipeTable);

                    foreach (DataRow row in recipeTable.Rows)
                    {
                        var parameter = new MLRecommenderParams
                        {
                            RecipeId = row.Field<int>("recipe_id"),
                            Ingredients = row.Field<string>("ingredients"),
                            IngredientsNER = row.Field<string>("ingredients_NER"),
                            Instructions = row.Field<string>("instructions"),
                            RatingStars = row.Field<double>("rating_stars"),
                            ReviewCount = row.Field<int>("review_count"),
                            UserId = row.Field<int>("user_id"),
                            FeedbackType = (UserFeedbackType)row.Field<short>("feedback_type")
                        };
                        parameters.Add(parameter);
                    }
                }
            }

            return parameters;
        }

        private Recipe GetRecipeObject(DataRow row)
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
                    FeedbackType = UserFeedbackType.None
                };

            if (row.Table.Columns.Contains("FeedbackType"))
            {
                recipe.FeedbackType = (UserFeedbackType)row.Field<short>("FeedbackType");
            }

            return recipe;
        }
    }
}
