using Fitness.Api.Client.Models;
using Fitness.DataObjects;
using System.Collections.Generic;
using System.Web.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using Newtonsoft.Json;

namespace Fitness.Api.Client.Controllers
{
    public class NutritionController : Controller
    {
        //List<Recipe> recipes = AllRecipes.Instance.Recipes;
        // GET: Nutrition

        static RecipeViewModel _recipeViewModel = new RecipeViewModel();

        public ActionResult Recipe()
        {
            if (string.IsNullOrEmpty(_recipeViewModel.SearchQuery) && UserContext.UserId != 0)
            {
                _recipeViewModel.Recipes = GetRecipes(string.Empty);
            }
            else if(!string.IsNullOrEmpty(_recipeViewModel.SearchQuery))
            {
                _recipeViewModel.Recipes = GetRecipes(_recipeViewModel.SearchQuery);
                _recipeViewModel.SearchQuery = string.Empty;
            }

            return View("Recipe", _recipeViewModel);
        }        

        public ActionResult LikeRecipe(int recipeId)
        {
            RecordUserFeedback(recipeId, UserFeedbackType.Like);

            return RedirectToAction("Recipe");
        }

        public ActionResult DislikeRecipe(int recipeId)
        {
            RecordUserFeedback(recipeId, UserFeedbackType.Dislike);

            return RedirectToAction("Recipe");
        }
        
        [HttpPost]
        public ActionResult SearchRecipes(RecipeViewModel recipeViewModel)
        {
            _recipeViewModel = recipeViewModel;
            return RedirectToAction("Recipe");
        }

        private List<Recipe> GetRecipes(string searchQuery)
        {            
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4564/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                string apiUrl = string.Empty;

                if (string.IsNullOrEmpty(searchQuery))
                {
                    apiUrl = "/api/Recipe?userId=" + UserContext.UserId;
                }
                else
                {
                    apiUrl = "/api/Recipe?searchQuery=" + _recipeViewModel.SearchQuery;
                }

                var responseTask = client.GetAsync(apiUrl);
                responseTask.Wait(3000);

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Dictionary<IdealMealTime, List<Recipe>>>();
                    readTask.Wait();

                    return readTask.Result.Values.SelectMany(x => x).ToList();
                }
            }

            return new List<Recipe>();
        }

        private void RecordUserFeedback(int recipeId, UserFeedbackType feedbackType)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4564/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                UserFeedback feedback = new UserFeedback
                {
                    UserId = UserContext.UserId,
                    FeedbackTime = DateTime.Now,
                    FeedbackType = feedbackType,
                    ItemId = recipeId,
                    ItemType = ItemType.Recipe
                };

                var myContent = JsonConvert.SerializeObject(feedback);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var responseTask = client.PostAsync("/api/UserFeedback", byteContent);
                responseTask.Wait(3000);

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    //var readTask = result.Content.ReadAsAsync<Dictionary<IdealMealTime, List<Recipe>>>();
                    //readTask.Wait();

                    //return readTask.Result.Values.SelectMany(x => x).ToList();
                }
            }
        }
    }
}