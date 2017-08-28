using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using MyShuttle.Model;

namespace MyShuttle.Web
{
    public class MyShuttleSignInManager : SignInManager<ApplicationUser, string>
    {
        public MyShuttleSignInManager(UserManager<ApplicationUser, string> userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
        }

        public static MyShuttleSignInManager Create(IdentityFactoryOptions<MyShuttleSignInManager> options, IOwinContext context)
        {
            return new MyShuttleSignInManager(context.GetUserManager<MyShuttleUserManager>(), context.Authentication);
        }
    }
}