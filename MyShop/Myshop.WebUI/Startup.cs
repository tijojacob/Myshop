using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Myshop.WebUI.Startup))]
namespace Myshop.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
