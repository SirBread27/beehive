using System.Diagnostics;
using Beehive.Models;
using Beehive.Models.DbRecords;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Beehive.Controllers
{
    public class HomeController(ILogger<HomeController> logger, ApplicationContext context) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly ApplicationContext db = context;

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
