using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fitness.DataObjects;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Fitness.DataAccess
{
    public class WorkoutDA
    {
        string connectionString = string.Empty;

        public WorkoutDA()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
        }

        //public List<Workout> getExercise()
        //{
        //    List<Workout> exercise = new List<Workout>();
        //    using (var dbConnection = new SqlConnection(connectionString))
        //    {
        //        //var query = string.Format(CultureInfo.InvariantCulture, DatabaseQueries.SELECT_EXERCISE_FOR_USER);
        //        using (var sqlAdapter = new SqlDataAdapter(DatabaseQueries.SELECT_EXERCISE_FOR_USER, dbConnection))
        //        {
        //            var exerciseTable = new DataTable();
        //            sqlAdapter.Fill(exerciseTable);

        //            foreach (DataRow row in exerciseTable.Rows)
        //            {
        //                var workout = new Workout
        //                { 

        //                    Workout_ID = row.Field<int>("Workout_ID"),
        //                    Workout_Name = row.Field<string>("Workout_Name"),
        //                    FitnessGoal = row.Field<Int32>("FitnessGoal"),
        //                    BodyPart = row.Field<string>("BodyPart")
        //                };
        //                exercise.Add(workout);

        //            }

        //        }

        //        return exercise;
        //    }
        public List<Workout> getExerciseForWeightGain()
        {
            List<Workout> exerciseGain = new List<Workout>();
            using (var dbConnection = new SqlConnection(connectionString))
            {
                //var query = string.Format(CultureInfo.InvariantCulture, DatabaseQueries.SELECT_EXERCISE_FOR_USER);
                using (var sqlAdapter = new SqlDataAdapter(DatabaseQueries.SELECT_EXERCISE_FOR_WEIGHTGAIN, dbConnection))
                {
                    var exerciseTable = new DataTable();
                    sqlAdapter.Fill(exerciseTable);

                    foreach (DataRow row in exerciseTable.Rows)
                    {
                        var workout = new Workout
                        {
                            Workout_Name = row.Field<string>("Workout_Name"),
                            BodyPart = row.Field<string>("BodyPart"),
                            Description = row.Field<string>("Description"),
                            Sets = row.Field<int>("Sets"),
                            Calories_Burned = row.Field<double>("Calories_Burned")
                        };
                        exerciseGain.Add(workout);
                        //exerciseGain.Add(workout.Workout_Name);
                    }
                }
                return exerciseGain;
            }
        }
        public List<Workout> getExerciseForWeightLoss()
        {
            List<Workout> exerciseLoss = new List<Workout>();
            using (var dbConnection = new SqlConnection(connectionString))
            {
                //var query = string.Format(CultureInfo.InvariantCulture, DatabaseQueries.SELECT_EXERCISE_FOR_USER);
                using (var sqlAdapter = new SqlDataAdapter(DatabaseQueries.SELECT_EXERCISE_FOR_WEIGHTLOSS, dbConnection))
                {
                    var exerciseTable = new DataTable();
                    sqlAdapter.Fill(exerciseTable);

                    foreach (DataRow row in exerciseTable.Rows)
                    {
                        var workout = new Workout
                        {
                            Workout_Name = row.Field<string>("Workout_Name"),
                            BodyPart = row.Field<string>("BodyPart"),
                            Description = row.Field<string>("Description"),
                            Sets = row.Field<int>("Sets"),
                            Calories_Burned = row.Field<double>("Calories_Burned")

                        };
                        exerciseLoss.Add(workout);
                        //exerciseLoss.Add(workout.Workout_Name);
                    }
                }
                return exerciseLoss;
            }
            //List<Workout> GetWorkoutsByGoal(int goal)
            //{
            //    List<Workout> exercises = new List<Workout>();
            //    using (var dbConnection = new SqlConnection(connectionString))
            //    {
            //        using (var sqlAdapter = new SqlDataAdapter(DatabaseQueries.SELECT_EXERCISE_FOR_USER, dbConnection))
            //        {

            //        }
            //    }

            //        return exercises;
            //}



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
}
