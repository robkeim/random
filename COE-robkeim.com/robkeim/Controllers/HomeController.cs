//-----------------------------------------------------------------------
// <copyright file="HomeController.cs" company="RobKeim">
//     Rob Keim Corporation
// </copyright>
//-----------------------------------------------------------------------
namespace Robkeim.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// This is the controller for all of the non-database related operations
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// The index method for the home controller
        /// </summary>
        /// <returns>The ActionResult</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// The about method for the home controller
        /// </summary>
        /// <returns>The ActionResult</returns>
        [Authorize(Roles = "View")]
        public ActionResult About()
        {
            return View();
        }
    }
}
