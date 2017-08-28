using AngularLab.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngularLab.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpGet]
        public ActionResult LoadVehicles()
        {
            return Json(new VehicleViewModel[]
                {
                    new VehicleViewModel() { make = "Ferrari", model = "California", colour="Lusso", registration="F45T 0NE", year="2012" },
                    new VehicleViewModel() { make = "TVR", model="Chimaera 500", colour ="Starmist Blue", registration = "R86 0EL", year="1998" }
                }, JsonRequestBehavior.AllowGet);
        }
    }
}