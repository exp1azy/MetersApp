using MetersApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MetersApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly MeterService _meterService;

        public HomeController(MeterService meterService)
        {
            _meterService = meterService;
        }

        public IActionResult Index()
        {
            return View(_meterService.GetCurrentResources());
        }

        public IActionResult Resource(int id)
        {
            return View(_meterService.GetCurrentResourceIndications(id));
        }

        [HttpGet]
        public IActionResult New()
        {
            ViewBag.CounterList = _meterService.GetCounters();
            return View(null);
        }

        [HttpPost("Home/New")]
        public IActionResult NewPost(NewIndication model)
        {
            if (ModelState.IsValid)
            {
                _meterService.SaveData(model);
                return RedirectToAction("Index");
            }

            ViewBag.CounterList = _meterService.GetCounters();
            return View("New", model);
        }

        public IActionResult History(int id)
        {
            return View(_meterService.GetHistoryOfIndications(id));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}