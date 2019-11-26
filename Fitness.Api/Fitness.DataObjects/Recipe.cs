namespace Fitness.DataObjects
{
    public class Recipe
    {
        public int RecipeId { get; set; }

        public string Author { get; set; }

        public int CookTimeMinutes { get; set; }

        public string Description { get; set; }

        public string Error { get; set; }

        public string Footnotes { get; set; }

        public string Ingredients { get; set; }

        public string Instructions { get; set; }

        public string PhotoUrl { get; set; }

        public int PrepTimeMinutes { get; set; }

        public double RatingStars { get; set; }

        public int ReviewCount { get; set; }

        public long TimeScraped { get; set; }

        public string Title { get; set; }

        public int TotalTimeMinutes { get; set; }

        public string Url { get; set; }

        public string IngredientsNER { get; set; }

        public IdealMealTime IdealMealTime { get; set; }

        public double CarbsCalories { get; set; }

        public double ProteinCalories { get; set; }

        public double FatCalories { get; set; }

        public UserFeedbackType FeedbackType { get; set; }

        public string Cuisine { get; set; }
    }
}
