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
        public ActionResult Recipe()
        {
            var recipes = GetRecipes();

            return View("Recipe", recipes);
        }

        private List<Recipe> GetRecipes()
        {
            if (UserContext.UserId != 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:4564/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    var responseTask = client.GetAsync("/api/Recipe?userId=" + UserContext.UserId);
                    responseTask.Wait(3000);

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Dictionary<IdealMealTime, List<Recipe>>>();
                        readTask.Wait();

                        return readTask.Result.Values.SelectMany(x => x).ToList();
                    }
                }
            }

            return new List<Recipe>();
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