using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fitness.DataObjects;
using System.Net.Http;
using System.Net.Http.Headers;


namespace Fitness.Api.Client.Models
{
    public class AllWorkouts
    {
        public static AllWorkouts obj = new AllWorkouts();
        public List<Workout> Workouts { get; set; }
        private AllWorkouts()
        {
            LoadAllWorkouts();
        }
        private void LoadAllWorkouts()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4564/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var responseTask = client.GetAsync("/api/Workout");
                responseTask.Wait(3000);

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Workout[]>();
                    readTask.Wait();

                    Workouts = new List<Workout>(readTask.Result);
                }
            }
        }

    }
}