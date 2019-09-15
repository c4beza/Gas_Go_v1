using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Gas_Go_v1.Startup))]
namespace Gas_Go_v1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
