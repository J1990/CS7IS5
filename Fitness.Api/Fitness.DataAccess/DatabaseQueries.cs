namespace Fitness.DataAccess
{
    public class DatabaseQueries
    {
        public const string SELECT_ALL_USERS = "SELECT * FROM [dbo].[User]";
        public const string SELECT_USER_BY_ID = "SELECT * FROM [dbo].[User] WHERE Id={0}";
        public const string INSERT_NEW_USER = "INSERT INTO [dbo].[User] (UserName, Email, ContactNumber) VALUES ({0},{1},{2})";
        public const string SELECT_RECIPE_FEEDBACK_FOR_USER = "SELECT * FROM [dbo].[UserFeedback] WHERE UserId = {0}";
        public const string RECORD_USER_FEEDBACK_FOR_RECIPE = "INSERT INTO [dbo].[RecipeUserFeedback] (UserId, RecipeId, FeedbackType, FeedbackTimeInTicks) VALUES ({0},{1},{2},{3})";
        public const string RECORD_USER_FEEDBACK_FOR_WORKOUT = "INSERT INTO [dbo].[WorkoutUserFeedback] (UserId, WorkoutId, FeedbackType, FeedbackTimeInTicks) VALUES ({0},{1},{2},{3})";

        public const string SELECT_ALL_RECIPES = "SELECT TOP 10 * FROM [dbo].[Recipe]";
        public const string SELECT_RECIPES_WITH_RECIPE_IDS = "SELECT * FROM [dbo].[Recipe] WHERE recipe_id IN ({0})";
        public const string SELECT_RECIPES_BASED_ON_SEARCH_QUERY = "SELECT TOP 20 * FROM [dbo].[Recipe] WHERE ingredients LIKE '%{0}%' OR Cuisine LIKE '%{1}%'";
        public const string SELECT_RECIPES_WITH_CALORIES_FOR_USER =
            @"SELECT TOP 5 rec.*, ISNULL(userFeedback.FeedbackType, 1) AS FeedbackType
FROM Recipe rec 
LEFT JOIN
(SELECT t.RecipeId,t.FeedbackType
FROM [RecipeUserFeedback] t
INNER JOIN 
(SELECT RecipeId, MAX(FeedbackTimeInTicks) as max_date
FROM [dbo].[RecipeUserFeedback] 
WHERE UserId = {0}
GROUP BY RecipeId) a
ON a.RecipeId = t.RecipeId AND a.max_date = FeedbackTimeInTicks)
as userFeedback
ON rec.recipe_id = userFeedback.RecipeId
WHERE IdealMealTime = {1}
ORDER BY FeedbackType ASC, ABS(CarbsCalories-{2}) ASC, ABS(ProteinCalories-{3}) ASC, ABS(FatCalories-{4}) ASC";

        public const string SELECT_ALL_RECIPES_WITH_USER_FEEDBACK =
        @"SELECT rec.recipe_id
		, rec.instructions
		, rec.ingredients
		, rec.ingredients_NER
		, rec.rating_stars
		, rec.review_count
		, ISNULL(userFeedback.UserId, 0) AS user_id
		, ISNULL(userFeedback.FeedbackType, 0) AS feedback_type
FROM Recipe rec 
LEFT JOIN
(
	SELECT a.UserId, t.RecipeId,t.FeedbackType
	FROM [RecipeUserFeedback] t
	INNER JOIN 
	(
		SELECT UserId, RecipeId, MAX(FeedbackTimeInTicks) as max_date
		FROM [dbo].[RecipeUserFeedback] 
		GROUP BY RecipeId, UserId
	) a
	ON a.RecipeId = t.RecipeId AND a.max_date = FeedbackTimeInTicks
)
as userFeedback
ON rec.recipe_id = userFeedback.RecipeId";

        public const string SELECT_USER_PROFILE_FOR_USER = "SELECT TOP 1 * FROM [dbo].[UserProfile] WHERE UserId = {0} ORDER BY LastUpdatedTimeInTicks DESC";
        //public const string SELECT_EXERCISE_FOR_USER = "SELECT Workout_Name FROM [dbo].[Workout]";
        public const string SELECT_EXERCISE_FOR_WEIGHTGAIN = "SELECT TOP 5 Workout_Name, BodyPart, Description, Sets, Calories_Burned FROM [dbo].[Workout] WHERE FitnessGoal=1";
        public const string SELECT_EXERCISE_FOR_WEIGHTLOSS = "SELECT TOP 5 Workout_Name, BodyPart, Description, Sets, Calories_Burned FROM [dbo].[Workout] WHERE FitnessGoal=2";
        
        public const string SELECT_EXERCISE_FOR_WEIGHTGAIN_INJURY_LOWERBODYPART = "SELECT Workout_Name, BodyPart, Description, Sets, Calories_Burned FROM [dbo].[Workout] w join [dbo].UserProfile up on w.BodyPart=up.Injuries where w.FitnessGoal=1 and w.BodyPart='Upper Body'";
        public const string SELECT_EXERCISE_FOR_WEIGHTGAIN_INJURY_UPPERBODYPART = "SELECT Workout_Name, BodyPart, Description, Sets, Calories_Burned FROM [dbo].[Workout] w join [dbo].UserProfile up on w.BodyPart=up.Injuries where w.FitnessGoal=1 and w.BodyPart='Lower Body'";
        public const string SELECT_EXERCISE_FOR_WEIGHTLOSS_INJURY_LOWERBODYPART = "SELECT Workout_Name, BodyPart, Description, Sets, Calories_Burned FROM [dbo].[Workout] w join [dbo].UserProfile up on w.BodyPart=up.Injuries where w.FitnessGoal=2 and w.BodyPart='Upper Body'";
        public const string SELECT_EXERCISE_FOR_WEIGHTLOSS_INJURY_UPPERBODYPART = "SELECT Workout_Name, BodyPart, Description, Sets, Calories_Burned FROM [dbo].[Workout] w join [dbo].UserProfile up on w.BodyPart=up.Injuries where w.FitnessGoal=2 and w.BodyPart='Lower Body'";
    }
}
