using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Etron.Startup))]
namespace Etron
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
