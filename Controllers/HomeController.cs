using CrudTest.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceStack.Redis;
using System.Diagnostics;
using System.Text;

namespace CrudTest.Controllers
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
            //string username;
            using (var connection = new RedisClient())
            {
                var username =connection.Get("userName");
                if (username != null)
                    ViewBag.Username = ASCIIEncoding.ASCII.GetString(username);
            }
            //username = TempData["UserName"]?.ToString();

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