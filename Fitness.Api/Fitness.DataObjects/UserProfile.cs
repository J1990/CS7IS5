using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.DataObjects
{
    public class UserProfile
    {
        public int UserProfileId { get; set; }

        public int UserId { get; set; }

        public double HeightInCm { get; set; }

        public double WeightInKg { get; set; }

        public GenderType Gender { get; set; }

        public int AgeInYears { get; set; }

        public FitnessGoalType FitnessGoal { get; set; }

        public DateTime LastUpdatedDate { get; set; }

        public string Nationality { get; set; }

        public double BMR { get; set; }
    }
}
