using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Aries.Startup))]
namespace Aries
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
