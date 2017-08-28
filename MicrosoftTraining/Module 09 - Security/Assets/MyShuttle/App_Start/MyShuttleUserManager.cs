using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using MyShuttle.Data;
using MyShuttle.Model;

namespace MyShuttle.Web
{
    public class MyShuttleUserManager : UserManager<ApplicationUser>
    {
        public MyShuttleUserManager(IUserStore<ApplicationUser> store) : base(store)
        {
        }

        public static MyShuttleUserManager Create(IdentityFactoryOptions<MyShuttleUserManager> options, IOwinContext context)
        {
            var manager = new MyShuttleUserManager(new UserStore<ApplicationUser>(context.Get<MyShuttleContext>()));

            // By default, ASP.NET Identity uses MinimumLengthValidator of 6 chars as a password validator
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            return manager;
        }
    }
}