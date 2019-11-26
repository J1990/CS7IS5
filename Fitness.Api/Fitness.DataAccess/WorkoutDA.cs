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
            //_userProfileBL = new UserProfileBL();
        }
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
        }
            public List<Workout> getExercise(FitnessGoalType goal, string injury)
            {
                List<Workout> exercises = new List<Workout>();
                using (var dbConnection = new SqlConnection(connectionString))
                {
                    switch (goal)
                    {
                        case (FitnessGoalType.WeightGain):
                            switch(injury)
                            {
                                case ("Upper Body"):
                                    using (var sqlAdapter = new SqlDataAdapter(DatabaseQueries.SELECT_EXERCISE_FOR_WEIGHTGAIN_INJURY_UPPERBODYPART, dbConnection))
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
                                            exercises.Add(workout);
                                        }
                                    }
                                break;
                            case ("Lower Body"):
                                using (var sqlAdapter = new SqlDataAdapter(DatabaseQueries.SELECT_EXERCISE_FOR_WEIGHTGAIN_INJURY_LOWERBODYPART, dbConnection))
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
                                        exercises.Add(workout);
                                    }
                                }
                                break;
                                case ("No Injury"):
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
                                        exercises.Add(workout);
                                    }
                                }
                                break;
                            }
                        break;
                        case (FitnessGoalType.WeightLoss):
                            switch (injury)
                            {
                                case ("Upper Body"):
                                using (var sqlAdapter = new SqlDataAdapter(DatabaseQueries.SELECT_EXERCISE_FOR_WEIGHTLOSS_INJURY_UPPERBODYPART, dbConnection))
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
                                        exercises.Add(workout);
                                    }
                                }
                                break;
                                case ("Lower Body"):
                                using (var sqlAdapter = new SqlDataAdapter(DatabaseQueries.SELECT_EXERCISE_FOR_WEIGHTLOSS_INJURY_LOWERBODYPART, dbConnection))
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
                                        exercises.Add(workout);
                                    }
                                }
                                break;
                                case ("No Injury"):
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
                                        exercises.Add(workout);
                                    }
                                }
                                break;
                            }
                            break;
                    }
                }
            return exercises;
        }
    }
}

