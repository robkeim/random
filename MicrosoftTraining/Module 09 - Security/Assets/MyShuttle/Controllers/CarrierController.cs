
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using MyShuttle.Web.Models;
using Microsoft.AspNet.Identity;
using MyShuttle.Model;
using Microsoft.AspNet.Identity.Owin;

namespace MyShuttle.Web.Controllers
{
    [Authorize]
    public class CarrierController : Controller
    {
        // It's better here to let the Dependancy Resolver (DI) resolve both properties.
        public MyShuttleSignInManager SignInManager { get; } = System.Web.HttpContext.Current.GetOwinContext().Get<MyShuttleSignInManager>();
        public MyShuttleUserManager UserManager { get; } = System.Web.HttpContext.Current.GetOwinContext().Get<MyShuttleUserManager>();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var signInStatus = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
            if (signInStatus == SignInStatus.Success)
            {
                if (string.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }

                return RedirectToLocal(returnUrl);
            }

            ModelState.AddModelError("", "Invalid username or password.");
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> LogOff()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser { UserName = model.UserName, Email = model.UserName };
            var result = await UserManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await SignInManager.SignInAsync(user, false, false);
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            // Adds the erros which came from the user creation process to the model to show it in the form.
            result.Errors.ToList().ForEach(e => ModelState.AddModelError(string.Empty, e));
            return View(model);

        }
    }
}