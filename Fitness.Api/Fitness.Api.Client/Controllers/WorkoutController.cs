using System.Web.Mvc;
using Fitness.Api.Client.Models;
using Fitness.DataObjects;
using System.Collections.Generic;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;

namespace Fitness.Api.Client.Controllers
{
    public class WorkoutController : Controller
    {
        // GET: Workout
        public ActionResult Exercise()
        {
            var exercise = GetExercisesForUser();

            return View("Exercise", exercise);
            //return View();
        }
        private List<Workout> GetExercisesForUser()
        {
            if (UserContext.UserId != 0)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:4564/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));

                    var responseTask = client.GetAsync("/api/Workout?userId=" + UserContext.UserId);
                    responseTask.Wait(3000);

                    var result = responseTask.Result;
                    Console.WriteLine(result);

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<Workout>>();
                        readTask.Wait();

                        return readTask.Result;
                    }
                }
            }
            return new List<Workout>();
        }
    }
}