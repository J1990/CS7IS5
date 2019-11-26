using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Fitness.BusinessLogic;
using Fitness.DataObjects;

namespace Fitness.Api.Controllers
{
    public class WorkoutController : ApiController
    {
        public List<Workout> Get(int userId)
        {
            WorkoutBL workoutBL = new WorkoutBL();
            return workoutBL.getExerciseForUser(userId);
        }
    }
}
