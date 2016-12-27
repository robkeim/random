//-----------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="RobKeim">
//     Rob Keim Corporation
// </copyright>
//-----------------------------------------------------------------------
namespace Robkeim
{
    using System.Data.Entity;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Robkeim.Models;

    //// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    //// visit http://go.microsoft.com/?LinkId=9394801

    /// <summary>
    /// The overall MvcApplication class
    /// </summary>
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Registers the global filters
        /// </summary>
        /// <param name="filters">Collection of filters</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
        }

        /// <summary>
        /// Registers the default routes
        /// </summary>
        /// <param name="routes">The collection of routes to initialize</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }); // Parameter defaults
        }

        /// <summary>
        /// Executed when the application first starts up
        /// </summary>
        protected void Application_Start()
        {
            Database.SetInitializer<TransactionDBContext>(new HistoryInitializer());
            Database.SetInitializer<RelationshipDBContext>(new RelationshipInitializer());
            Database.SetInitializer<RegistrationDBContext>(new RegistrationInitializer());

            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}