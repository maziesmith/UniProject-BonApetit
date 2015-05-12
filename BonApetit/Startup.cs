using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BonApetit.Startup))]
namespace BonApetit
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
