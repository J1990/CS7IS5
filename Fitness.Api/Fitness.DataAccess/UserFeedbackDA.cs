using Fitness.DataObjects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.DataAccess
{
    public class UserFeedbackDA
    {
        string connectionString = string.Empty;

        public UserFeedbackDA()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;            
        }

        public List<UserFeedback> GetRecipeFeedbackByUser(int userId)
        {
            List<UserFeedback> userFeedbacks = new List<UserFeedback>();

            var query = string.Format(CultureInfo.InvariantCulture, DatabaseQueries.SELECT_RECIPE_FEEDBACK_FOR_USER, userId);

            using (var dbConnection = new SqlConnection(connectionString))
            {
                using (var sqlAdapter = new SqlDataAdapter(query, dbConnection))
                {
                    var userFeedbackTable = new DataTable();
                    sqlAdapter.Fill(userFeedbackTable);

                    foreach (DataRow row in userFeedbackTable.Rows)
                    {
                        var userFeedback = new UserFeedback
                        {
                            UserId = row.Field<int>("UserId"),
                            ItemId = row.Field<int>("RecipeId"),
                            ItemType = ItemType.Recipe,
                            FeedbackType = row.Field<UserFeedbackType>("FeedbackType"),
                            FeedbackTime = new DateTime(row.Field<long>("FeedbackTimeInTicks"))
                        };
                        userFeedbacks.Add(userFeedback);
                    }
                }
            }

            return userFeedbacks;
        }

        public void RecordUserFeedback(UserFeedback userFeedback)
        {
            using (var dbConnection = new SqlConnection(connectionString))
            {
                string insertQuery = string.Empty;
                switch (userFeedback.ItemType)
                {
                    case ItemType.Recipe:
                        insertQuery = string.Format(CultureInfo.InvariantCulture, DatabaseQueries.RECORD_USER_FEEDBACK_FOR_RECIPE, userFeedback.UserId, userFeedback.ItemId,
                            userFeedback.FeedbackType, userFeedback.FeedbackTime.Ticks);
                        break;

                    case ItemType.Workout:
                        insertQuery = string.Format(CultureInfo.InvariantCulture, DatabaseQueries.RECORD_USER_FEEDBACK_FOR_WORKOUT, userFeedback.UserId, userFeedback.ItemId,
                            userFeedback.FeedbackType, userFeedback.FeedbackTime.Ticks);
                        break;
                }

                
                using (var command = new SqlCommand(insertQuery, dbConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
