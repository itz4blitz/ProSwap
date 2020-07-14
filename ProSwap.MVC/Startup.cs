using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProSwap.MVC.Startup))]
namespace ProSwap.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
