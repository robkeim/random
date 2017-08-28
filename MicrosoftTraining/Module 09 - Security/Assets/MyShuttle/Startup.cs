using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MyShuttle.Web.Startup))]

namespace MyShuttle.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.CreatePerOwinContext(() => new Data.MyShuttleContext());
            app.CreatePerOwinContext<MyShuttleUserManager>(MyShuttleUserManager.Create);
            app.CreatePerOwinContext<MyShuttleSignInManager>(MyShuttleSignInManager.Create);
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Carrier/Login"),
            });
        }
    }
}
