using System.Threading.Tasks;
using System.Web.Mvc;
using MyShuttle.Data;
using MyShuttle.Model;
using MyShuttle.Web.Models;

namespace MyShuttle.Web.Controllers
{
    public class HomeController : Controller
    {
        ICarrierRepository _carrierRepository;

        public HomeController(ICarrierRepository carrierRepository)
        {
            _carrierRepository = carrierRepository;
        }

        // GET: Home
        public ActionResult Index()
        {
            var model = new MyShuttleViewModel()
            {
                MainMessage = "The Ultimate B2B Shuttle Service Solution"
            };
            return View(model);
        }

        [HttpPost]
        public async Task<int> Post(Carrier carrier)
        {
            return await _carrierRepository.AddAsync(carrier);
        }
    }
}