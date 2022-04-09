using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProCart.WebUI.Startup))]
namespace ProCart.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
