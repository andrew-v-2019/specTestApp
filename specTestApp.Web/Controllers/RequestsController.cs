using specTestApp.Web.Models;
using System.Web.Mvc;

namespace specTestApp.Web.Controllers
{
    public class RequestsController : ControllerBase
    {
        public async System.Threading.Tasks.Task<ActionResult> Create()
        {
            var userName = User.Identity.Name;
            var user = await UserManager.FindByEmailAsync(userName);
            var model = new CreateRequestViewModel()
            {
                EmailConfirmed = user.EmailConfirmed
            };
            return View(model);
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Create(CreateRequestViewModel model)
        {
            var userName = User.Identity.Name;
            var user = await UserManager.FindByEmailAsync(userName);
            return View(model);
        }
    }
}