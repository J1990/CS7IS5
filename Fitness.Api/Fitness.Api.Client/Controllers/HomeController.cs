using Fitness.Api.Client.Models;
using Fitness.DataObjects;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System;

namespace Fitness.Api.Client.Controllers
{
    public class HomeController : Controller
    {
        static UserViewModel userViewModel = new UserViewModel();
        static string selectedUserMessage = "No User Selected";  
        
        public HomeController()
        {            
            userViewModel.RegisteredUsers = AllUsers.Instance.Users;
        }

        public ActionResult Index()
        {
            ViewBag.Message = selectedUserMessage;
            return View(userViewModel);
        }

        public ActionResult ChangeUserContext(int userId)
        {
            var user = AllUsers.Instance.GetUserById(userId);

            UserContext.SetUserContext(user);

            SetUserProfile();

            selectedUserMessage = $"Current User: {user.UserName}";

            return RedirectToAction("Index", userViewModel);
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        public ActionResult Contact()
        {
            ViewBag.Message = "Fitness App contact page.";

            return View();
        }

        private void SetUserProfile()
        {
            var profile = GetUserProfile(UserContext.UserId);

            userViewModel.UserId = UserContext.UserId;
            userViewModel.UserName = UserContext.UserName;
            userViewModel.UserEmailAddress = UserContext.UserEmailAddress;
            userViewModel.WeightInKg = profile.WeightInKg;
            userViewModel.HeightInCm = profile.HeightInCm;
            userViewModel.Nationality = profile.Nationality;
            userViewModel.Gender = profile.Gender;
            userViewModel.AgeInYears = profile.AgeInYears;
            userViewModel.LastUpdatedDate = profile.LastUpdatedDate;
            userViewModel.FitnessGoal = profile.FitnessGoal;
            userViewModel.BMR = profile.BMR;
        }

        private UserProfile GetUserProfile(int userId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:4564/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var responseTask = client.GetAsync("/api/UserProfile/" + userId);
                responseTask.Wait(3000);

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<UserProfile>();
                    readTask.Wait();

                    return readTask.Result;
                }
            }

            return new UserProfile();
        }
    }
}