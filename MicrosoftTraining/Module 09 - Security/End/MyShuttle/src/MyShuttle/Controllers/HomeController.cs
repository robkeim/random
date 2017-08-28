using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShuttle.Web.Models;
using MyShuttle.Model;
using MyShuttle.Data;
using System.Threading.Tasks;

namespace MyShuttle.Web.Controllers
{
    public class HomeController : Controller
    {
        ICarrierRepository _carrierRepository;

        //public HomeController(ICarrierRepository carrierRepository)
        //{
        //    _carrierRepository = carrierRepository;
        //}

        // GET: Home
        public ActionResult Index()
        {
            var model = new MyShuttleViewModel()
            {
                MainMessage = "The Ultimate B2B Shuttle Service Solution"
            };
            return View(model);
        }

        //[HttpPost]
        //public async Task<int> Post(Carrier carrier)
        //{
        //    return await _carrierRepository.AddAsync(carrier);
        //}
    }
}
