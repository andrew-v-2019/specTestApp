using specTestApp.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace specTestApp.Web
{
    public class MvcApplication : HttpApplication
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            Logger.Info("Application Start");

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer(new DbInitializer());
        }

        protected void Application_Error()
        {
            Logger.Info("Application Error");
        }


        protected void Application_End()
        {
            Logger.Info("Application End");
        }
    }
}
