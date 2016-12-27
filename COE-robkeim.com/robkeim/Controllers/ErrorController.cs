//-----------------------------------------------------------------------
// <copyright file="ErrorController.cs" company="RobKeim">
//     Rob Keim Corporation
// </copyright>
//-----------------------------------------------------------------------
namespace Robkeim.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// This controller handles all of the errors
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// The index method for the error controller
        /// </summary>
        /// <returns>The ActionResult</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// The 404 method for the error controller
        /// </summary>
        /// <returns>The ActionResult</returns>
        public ActionResult Error404()
        {
            return View();
        }
    }
}
