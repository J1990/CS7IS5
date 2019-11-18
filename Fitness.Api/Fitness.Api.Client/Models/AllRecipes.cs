using Fitness.DataObjects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Fitness.Api.Client.Models
{
    public class AllRecipes
    {
        public static AllRecipes Instance = new AllRecipes();
        public List<Recipe> Recipes { get; set; }

        private AllRecipes()
        {
            LoadAllRecipes();
        }

        private void LoadAllRecipes()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4564/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var responseTask = client.GetAsync("/api/Recipe");
                responseTask.Wait(3000);

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Recipe[]>();
                    readTask.Wait();

                    Recipes = new List<Recipe>(readTask.Result);
                }
            }
        }
    }
}