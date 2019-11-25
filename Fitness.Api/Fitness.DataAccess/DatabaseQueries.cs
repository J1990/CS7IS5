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
        public const string SELECT_RECIPES_WITH_CALORIES_FOR_USER =
            @"SELECT TOP 5 rec.*, ISNULL(userFeedback.FeedbackType, 0) AS FeedbackType
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
ORDER BY rec.recipe_id, userFeedback.FeedbackType, ABS(CarbsCalories-{2}), ABS(ProteinCalories-{3}), ABS(FatCalories-{4}) ASC";

        public const string SELECT_USER_PROFILE_FOR_USER = "SELECT TOP 1 * FROM [dbo].[UserProfile] WHERE UserId = {0} ORDER BY LastUpdatedTimeInTicks DESC";
    }
}
