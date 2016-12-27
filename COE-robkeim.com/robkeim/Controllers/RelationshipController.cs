//-----------------------------------------------------------------------
// <copyright file="RelationshipController.cs" company="RobKeim">
//     Rob Keim Corporation
// </copyright>
//-----------------------------------------------------------------------
namespace Robkeim.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Robkeim.Models;

    /// <summary>
    /// This is the controller for all of the relationship operations
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class RelationshipController : Controller
    {
        /// <summary>
        /// The database context
        /// </summary>
        private RelationshipDBContext db = new RelationshipDBContext();

        /// <summary>
        /// The index method for the relationship controller
        /// </summary>
        /// <returns>The ViewResult</returns>
        public ViewResult Index()
        {
            return View(this.db.Relationships.ToList());
        }

        /// <summary>
        /// Dispose of the object
        /// </summary>
        /// <param name="disposing">Bool indicating if the object is being disposed</param>
        protected override void Dispose(bool disposing)
        {
            this.db.Dispose();
            base.Dispose(disposing);
        }
    }
}