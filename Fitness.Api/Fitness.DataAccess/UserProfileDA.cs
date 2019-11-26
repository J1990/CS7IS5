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
    public class UserProfileDA
    {
        string connectionString = string.Empty;

        public UserProfileDA()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
        }

        public UserProfile GetLatestUserProfileForUser(int userId)
        {
            using (var dbConnection = new SqlConnection(connectionString))
            {
                using (var sqlAdapter = new SqlDataAdapter(string.Format(CultureInfo.InvariantCulture, DatabaseQueries.SELECT_USER_PROFILE_FOR_USER, userId), dbConnection))
                {
                    var userTable = new DataTable();
                    sqlAdapter.Fill(userTable);

                    if (userTable.Rows.Count > 0)
                    {
                        var userProfile = new UserProfile
                        {
                            UserProfileId = userTable.Rows[0].Field<int>("UserProfileId"),
                            UserId = userTable.Rows[0].Field<int>("UserId"),
                            HeightInCm = userTable.Rows[0].Field<double>("HeightInCm"),
                            WeightInKg = userTable.Rows[0].Field<double>("WeightInKg"),
                            Gender = (GenderType)userTable.Rows[0].Field<short>("Gender"),
                            FitnessGoal = (FitnessGoalType)userTable.Rows[0].Field<short>("FitnessGoal"),
                            AgeInYears = userTable.Rows[0].Field<int>("AgeInYears"),
                            Nationality = userTable.Rows[0].Field<string>("Nationality"),
                            Injuries = userTable.Rows[0].Field<string>("Injuries"),
                            LastUpdatedDate = new DateTime(userTable.Rows[0].Field<long>("LastUpdatedTimeInTicks"))
                        };

                        return userProfile;
                    }
                }
            }

            return new UserProfile();
        }
    }
}
