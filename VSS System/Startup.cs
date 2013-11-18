using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VSS_System.Startup))]
namespace VSS_System
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
