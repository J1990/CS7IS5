using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.DataObjects
{
    public class Workout
    {
        //public int Workout_ID { get; set; }   
        public string Workout_Name { get; set; }

        public Int32 FitnessGoal { get; set; }

        public string BodyPart { get; set; }

        public string  Description { get; set; }

        public int Sets { get; set; }

        public double Calories_Burned { get; set; }

    }
}
