using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fitness.Api.Client.Controllers
{
    public class NutritionController : Controller
    {
        // GET: Nutrition
        public ActionResult Recipe()
        {
            return View();
        }
    }
}