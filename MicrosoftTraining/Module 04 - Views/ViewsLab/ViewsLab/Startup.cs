using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ViewsLab.Startup))]
namespace ViewsLab
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
