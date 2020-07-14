using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProSwap.Startup))]
namespace ProSwap
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
