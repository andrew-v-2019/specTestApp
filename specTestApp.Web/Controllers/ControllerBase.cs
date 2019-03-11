using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;

namespace specTestApp.Web.Controllers
{
    public class ControllerBase : Controller
    {
        protected ApplicationSignInManager _signInManager;
        protected ApplicationUserManager _userManager;

        public ControllerBase()
        {
          
        }

        public ControllerBase(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        protected ApplicationSignInManager SignInManager
        {
            get => _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            private set => _signInManager = value;
        }

        protected ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }
    }
}