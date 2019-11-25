using Fitness.DataAccess;
using Fitness.DataObjects;
using System;
using System.Collections.Generic;

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
            var userProfile = _userProfileBL.GetLatestUserProfileForUser(userId);

            var dailyCalorieNeedsOfUser = userProfile.BMR;

            var recommendedCalorieIntake = GetRecommendedCalorieIntakeForUserGoal(dailyCalorieNeedsOfUser, userProfile.FitnessGoal);

            return FindRecipesForCalorieIntake(userId, recommendedCalorieIntake, _preferredNumberOfMealsPerDayForUser, userProfile.FitnessGoal);
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
