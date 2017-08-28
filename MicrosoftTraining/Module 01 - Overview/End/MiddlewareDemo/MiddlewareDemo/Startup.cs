using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MiddlewareDemo.Startup))]
namespace MiddlewareDemo
{
    public static class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
            app.UseRequestLogger();
        }
    }
}
