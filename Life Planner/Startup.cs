using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Life_Planner.Startup))]
namespace Life_Planner
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
