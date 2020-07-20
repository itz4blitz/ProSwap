using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProSwap.UI.Startup))]
namespace ProSwap.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
