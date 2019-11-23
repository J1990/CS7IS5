using Fitness.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Fitness.Api.Client.Models
{
    public class AllUsers
    {
        public static AllUsers Instance = new AllUsers();
        public List<User> Users { get; set; }

        private AllUsers()
        {
            LoadAllUsers();
        }

        public User GetUserById(int userId)
        {
            return Users.FirstOrDefault(x => x.Id == userId);
        }

        private void LoadAllUsers()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4564/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var responseTask = client.GetAsync("/api/user");
                responseTask.Wait(3000);

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<User[]>();
                    readTask.Wait();

                    Users = new List<User>(readTask.Result);
                }
            }
        }
    }
}