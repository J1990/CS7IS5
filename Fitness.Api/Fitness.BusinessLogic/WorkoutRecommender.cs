﻿using Fitness.DataAccess;
using Fitness.DataObjects;
using System;
using System.Collections.Generic;

namespace Fitness.BusinessLogic
{
    public class WorkoutRecommender
    {
        UserProfileBL _userProfileBL = null;
        WorkoutDA _workoutDA = null;
        public WorkoutRecommender(WorkoutDA workoutDA)
        {
            _userProfileBL = new UserProfileBL();
            _workoutDA = workoutDA;
        }
        public List<Workout> GetExercisesForUser(int userId)
        {
            List<Workout> workout = new List<Workout>();
            var userProfile = _userProfileBL.GetLatestUserProfileForUser(userId);

            var userGoal = userProfile.FitnessGoal;
            var userInjury = userProfile.Injuries;

            //var workoutDA = _workoutDA.getExercise();


            //switch(userGoal)
            //{
            //    case (FitnessGoalType.WeightGain):
            //        workout = _workoutDA.getExerciseForWeightGain();
            //        switch(userInjury)
            //        {
            //            case ("Lower Body"):
            //                workout=WorkoutDA.
            //                break;
            //        }
            //        break;
            //    case (FitnessGoalType.WeightLoss):
            //        workout = _workoutDA.getExerciseForWeightLoss();
            //        break;
            //}

            workout = _workoutDA.getExercise(userGoal, userInjury);
            return workout;
        }
    }
}