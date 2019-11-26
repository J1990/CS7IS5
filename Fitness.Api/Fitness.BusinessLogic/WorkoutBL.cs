using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fitness.DataAccess;
using Fitness.DataObjects;

namespace Fitness.BusinessLogic
{
    public class WorkoutBL
    {
        WorkoutDA _workoutDA = null;
        WorkoutRecommender _workoutRecommender = null;
        public WorkoutBL()
        {
            _workoutDA = new WorkoutDA();
            _workoutRecommender = new WorkoutRecommender(_workoutDA);
        }



        public List<Workout> getExerciseForUser(int userId)
        {
            return _workoutRecommender.GetExercisesForUser(userId);
        }
    }
}
