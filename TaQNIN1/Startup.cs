using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TaQNIN1.Startup))]
namespace TaQNIN1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
