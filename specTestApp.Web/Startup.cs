using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(specTestApp.Web.Startup))]
namespace specTestApp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
