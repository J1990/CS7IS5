using Fitness.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fitness.Api.Client.Models
{
    public class UserViewModel
    {
        public List<User> RegisteredUsers { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string UserEmailAddress { get; set; }

        public double HeightInCm { get; set; }

        public double WeightInKg { get; set; }

        public GenderType Gender { get; set; }

        public int AgeInYears { get; set; }

        public FitnessGoalType FitnessGoal { get; set; }

        public DateTime LastUpdatedDate { get; set; }

        public string Nationality { get; set; }

        public double BMR { get; set; }

        public string Injuries { get; set; }
    }
}