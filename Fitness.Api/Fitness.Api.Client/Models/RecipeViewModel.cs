using Fitness.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fitness.Api.Client.Models
{
    public class RecipeViewModel
    {
        public List<Recipe> Recipes { get; set; }

        public string SearchQuery { get; set; }

        public RecipeViewModel()
        {
            Recipes = new List<Recipe>();
            SearchQuery = string.Empty;
        }
    }
}