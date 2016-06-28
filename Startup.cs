using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Discoverx.Startup))]
namespace Discoverx
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
