using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.DataObjects
{
    public class MLRecommenderParams
    {
        public int RecipeId { get; set; }

        public string Ingredients { get; set; }

        public string Instructions { get; set; }

        public double RatingStars { get; set; }

        public int ReviewCount { get; set; }

        public string IngredientsNER { get; set; }

        public int UserId { get; set; }

        public UserFeedbackType FeedbackType { get; set; }
    }
}
