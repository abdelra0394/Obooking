using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Obooking.Startup))]
namespace Obooking
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
