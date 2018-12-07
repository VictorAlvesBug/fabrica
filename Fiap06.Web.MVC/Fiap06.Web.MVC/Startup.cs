using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fiap06.Web.MVC.Startup))]
namespace Fiap06.Web.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
