using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ServicesApp.Website.Startup))]
namespace ServicesApp.Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
