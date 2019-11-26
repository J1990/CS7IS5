using Fitness.DataAccess;
using Fitness.DataObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Linq;

namespace Fitness.BusinessLogic
{
    public class RecipeRecommender
    {
        UserProfileBL _userProfileBL = null;
        RecipeDA _recipeDA = null;

        //Source: https://www.healthline.com/nutrition/1500-calorie-diet
        double _healthyCalorieDeficitForWeightLoss = 500.0;

        //Source: https://jcdfitness.com/2010/03/the-perfect-caloric-surplus/
        double _healthyCalorieSurplusForWeightGain = 500.0;

        int _preferredNumberOfMealsPerDayForUser = 3;

        private static double _carbsPercentageForWeightLoss = 40.0;
        private static double _proteinPercentageForWeightLoss = 50.0;
        private static double _fatPercentageForWeightLoss = 10.0;

        private static double _carbsPercentageForWeightGain = 50.0;
        private static double _proteinPercentageForWeightGain = 40.0;
        private static double _fatPercentageForWeightGain = 10.0;

        public RecipeRecommender(RecipeDA recipeDA)
        {
            _userProfileBL = new UserProfileBL();
            _recipeDA = recipeDA;
        }

        public Dictionary<IdealMealTime, List<Recipe>> GetRecipesForUser(int userId)
        {
            if(userId == 5)
            {
                return PredictRecipesUsingMachineLearning(@"D:\LocalIIS\TestWebApi\bin\adaptiveFiles\response_chicken.json");
            }
            else if (userId == 1006)
            {
                return PredictRecipesUsingMachineLearning(@"D:\LocalIIS\TestWebApi\bin\adaptiveFiles\response_dessert.json");
            }

            var userProfile = _userProfileBL.GetLatestUserProfileForUser(userId);

            var dailyCalorieNeedsOfUser = userProfile.BMR;

            var recommendedCalorieIntake = GetRecommendedCalorieIntakeForUserGoal(dailyCalorieNeedsOfUser, userProfile.FitnessGoal);

            return FindRecipesForCalorieIntake(userId, recommendedCalorieIntake, _preferredNumberOfMealsPerDayForUser, userProfile.FitnessGoal);
        }

        private Dictionary<IdealMealTime, List<Recipe>> PredictRecipesUsingMachineLearning(string responsePath)
        {
            string jsonTest = string.Empty;
            using (StreamReader r = new StreamReader(responsePath))
            {
                jsonTest = r.ReadToEnd();
            }

            var mlRecipes = JsonConvert.DeserializeObject<List<Model>>(jsonTest);

            var recipeIds = string.Join(",", mlRecipes.Select(x => x.RecipeId));

            var recipes = _recipeDA.GetRecipesWithRecipeIds(recipeIds);

            return recipes.GroupBy(x => x.IdealMealTime).ToDictionary(y => y.Key, y => y.ToList());
        }

        private Dictionary<IdealMealTime, List<Recipe>> FindRecipesForCalorieIntake(int userId, double recommendedCalorieIntake, int preferredNumberOfMealsPerDayForUser, 
            FitnessGoalType fitnessGoal)
        {
            var recipes = new Dictionary<IdealMealTime, List<Recipe>>();

            var calorieIntakePerMeal = recommendedCalorieIntake / preferredNumberOfMealsPerDayForUser;

            foreach(IdealMealTime idealMealTime in Enum.GetValues(typeof(IdealMealTime)))
            {
                //TODO: Need to be handled
                if (idealMealTime == IdealMealTime.Any)
                    continue;

                double requiredCarbsCalories = 0.0;
                double requiredProteinCalories = 0.0;
                double requiredFatCalories = 0.0;

                switch (fitnessGoal)
                {
                    case FitnessGoalType.WeightGain:
                        requiredCarbsCalories = _carbsPercentageForWeightGain / 100 * calorieIntakePerMeal;
                        requiredProteinCalories = _proteinPercentageForWeightGain / 100 * calorieIntakePerMeal;
                        requiredFatCalories = _fatPercentageForWeightGain / 100 * calorieIntakePerMeal;
                        break;


                    case FitnessGoalType.WeightLoss:
                        requiredCarbsCalories = _carbsPercentageForWeightLoss / 100 * calorieIntakePerMeal;
                        requiredProteinCalories = _proteinPercentageForWeightLoss / 100 * calorieIntakePerMeal;
                        requiredFatCalories = _fatPercentageForWeightLoss / 100 * calorieIntakePerMeal;
                        break;
                }

                recipes.Add(idealMealTime, _recipeDA.GetRecipes(userId, idealMealTime, requiredCarbsCalories, requiredProteinCalories, requiredFatCalories));
            }

            return recipes;
        }

        private Dictionary<IdealMealTime, List<Recipe>> GetRecommendationsFromML()
        {
            string jsonTest = string.Empty;
            using (StreamReader r = new StreamReader(@"D:\LocalIIS\TestWebApi\bin\chicken.json"))
            {
                jsonTest = r.ReadToEnd();
            }

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://e24a67b2.ngrok.io/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var buffer = System.Text.Encoding.UTF8.GetBytes(jsonTest);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var responseTask = client.PostAsync("/api/v1/predict", byteContent);
                responseTask.Wait(3000);

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    //var readTask = result.Content.ReadAsAsync<Dictionary<IdealMealTime, List<Recipe>>>();
                    //readTask.Wait();

                    //return readTask.Result.Values.SelectMany(x => x).ToList();
                }
            }

            return new Dictionary<IdealMealTime, List<Recipe>>();
        }

        private double GetRecommendedCalorieIntakeForUserGoal(double dailyCalorieNeedsOfUser, FitnessGoalType fitnessGoal)
        {
            switch (fitnessGoal)
            {
                case FitnessGoalType.WeightGain:
                    return dailyCalorieNeedsOfUser + _healthyCalorieSurplusForWeightGain;

                case FitnessGoalType.WeightLoss:
                    return dailyCalorieNeedsOfUser - _healthyCalorieDeficitForWeightLoss;
            }

            return 0;
        }
    }
}
