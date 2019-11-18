namespace Fitness.DataObjects
{
    public class Recipe
    {
        public string RecipeId { get; set; }
        public string Author { get; set; }
        public string CookTimeMinutes { get; set; }
        public string Description { get; set; }
        public string Error { get; set; }
        public string Footnotes { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; }
        public string PhotoUrl { get; set; }
        public string PrepTimeMinutes { get; set; }
        public string RatingStars { get; set; }
        public string ReviewCount { get; set; }
        public string TimeScraped { get; set; }
        public string Title { get; set; }
        public string TotalTimeMinutes { get; set; }
        public string Url { get; set; }
        public string IngredientsNER { get; set; }
        public string UserResponse { get; set; }

    }
}
