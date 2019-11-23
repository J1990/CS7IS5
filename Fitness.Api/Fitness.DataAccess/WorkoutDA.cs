using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.DataAccess
{
    public class WorkoutDA
    {
        string connectionString = string.Empty;

        public WorkoutDA()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
        }

        //public List<UserFeedback> GetFeedbackByUser(int userId)
        //{
        //    List<UserFeedback> userFeedbacks = new List<UserFeedback>();

        //    var query = string.Format(CultureInfo.InvariantCulture, DatabaseQueries.SELECT_FEEDBACK_FOR_USER, userId);

        //    using (var dbConnection = new SqlConnection(connectionString))
        //    {
        //        using (var sqlAdapter = new SqlDataAdapter(query, dbConnection))
        //        {
        //            var userFeedbackTable = new DataTable();
        //            sqlAdapter.Fill(userFeedbackTable);

        //            foreach (DataRow row in userFeedbackTable.Rows)
        //            {
        //                var userFeedback = new UserFeedback
        //                {
        //                    UserId = row.Field<int>("UserId"),
        //                    ItemId = row.Field<int>("ItemId"),
        //                    ItemType = row.Field<ItemType>("ItemType"),
        //                    IsLike = row.Field<bool>("IsLike"),
        //                    FeedbackTime = row.Field<DateTime>("FeedbackTime")
        //                };
        //                userFeedbacks.Add(userFeedback);
        //            }
        //        }
        //    }

        //    return userFeedbacks;
        //}

    }
}
