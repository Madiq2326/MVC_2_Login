using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_2.Startup))]
namespace MVC_2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
