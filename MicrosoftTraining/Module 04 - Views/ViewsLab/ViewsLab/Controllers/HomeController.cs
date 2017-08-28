using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ViewsLab.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "I am learning ASP.NET MVC!";
            return View();
        }

        public ActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            var movie = new Models.Movie
            {
                ID = 1,
                Title = "Follow the Wind",
                ReleaseDate = new DateTime(2017, 01, 21)
            };
            return View(movie);
        }

        [HttpPost]
        public ActionResult About(Models.Movie movieIncoming)
        {
            // Your logic here

            return View(movieIncoming);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}