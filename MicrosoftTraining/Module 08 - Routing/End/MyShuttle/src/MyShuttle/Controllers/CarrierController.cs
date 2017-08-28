
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using MyShuttle.Web.Models;

namespace MyShuttle.Web.Controllers
{
    public class CarrierController : Controller
    {
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
            try
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Register", "Carrier");
                }
            }
            catch (System.Exception ex)
            {

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> LogOff()
        {
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
            try
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction("Login", "Carrier");
                }
                else
                {
                    return RedirectToAction("Register", "Carrier");
                }
            }
            catch (System.Exception ex)
            {

            }
            return RedirectToAction("Register", "Carrier");
        }
    }
}