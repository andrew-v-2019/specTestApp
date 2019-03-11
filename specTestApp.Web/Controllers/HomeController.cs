using System.Web.Mvc;

namespace specTestApp.Web.Controllers
{
    [LogException]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}