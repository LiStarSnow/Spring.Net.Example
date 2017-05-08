using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Spring.Net.Example.Startup))]
namespace Spring.Net.Example
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
