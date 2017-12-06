using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SocialTap.WEB.Startup))]
namespace SocialTap.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
