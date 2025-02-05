using Forum_RP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Forum_RP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.CurrentUser = HttpContext.Session.GetString("UserName");
            return View();
        }

        public IActionResult Privacy()
        {
            ViewBag.CurrentUser = HttpContext.Session.GetString("UserName");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewBag.CurrentUser = HttpContext.Session.GetString("UserName");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
