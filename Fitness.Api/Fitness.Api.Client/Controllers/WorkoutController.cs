using System.Web.Mvc;

namespace Fitness.Api.Client.Controllers
{
    public class WorkoutController : Controller
    {
        // GET: Workout
        public ActionResult Exercise()
        {
            return View();
        }
    }
}