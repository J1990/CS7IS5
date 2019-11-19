namespace Fitness.DataAccess
{
    public class DatabaseQueries
    {
        public const string SELECT_ALL_USERS = "SELECT * FROM [dbo].[User]";
        public const string SELECT_USER_BY_ID = "SELECT * FROM [dbo].[User] WHERE Id={0}";
        public const string INSERT_NEW_USER = "INSERT INTO [dbo].[User] (UserName, Email, ContactNumber) VALUES ({0},{1},{2})";
        public const string SELECT_FEEDBACK_FOR_USER = "SELECT * FROM [dbo].[UserFeedback] WHERE UserId = {0}";
        public const string RECORD_USER_FEEDBACK = "INSERT INTO [dbo].[UserFeedback] (UserId, ItemId, ItemType, IsLike, FeedbackTime) VALUES ({0},{1},{2},{3},{4})";
    }
}
