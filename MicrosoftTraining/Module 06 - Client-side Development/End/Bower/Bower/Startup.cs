using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bower.Startup))]
namespace Bower
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
