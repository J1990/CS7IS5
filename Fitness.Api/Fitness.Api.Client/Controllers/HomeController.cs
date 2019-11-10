using Fitness.Api.Client.Models;
using Fitness.DataObjects;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Fitness.Api.Client.Controllers
{
    public class HomeController : Controller
    {
        List<User> registeredUsers = AllUsers.Instance.Users;
        static string selectedUserMessage = "No User Selected";        

        public ActionResult Index()
        {
            ViewBag.Message = selectedUserMessage;
            return View(registeredUsers);
        }

        public ActionResult ChangeUserContext(int userId)
        {
            var user = AllUsers.Instance.GetUserById(userId);

            UserContext.SetUserContext(user);

            selectedUserMessage = $"Current User: {user.UserName}";

            return RedirectToAction("Index");
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
    }
}