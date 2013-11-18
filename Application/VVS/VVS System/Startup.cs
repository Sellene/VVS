using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VVS_System.Startup))]
namespace VVS_System
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
