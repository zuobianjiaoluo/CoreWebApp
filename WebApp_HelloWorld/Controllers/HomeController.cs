using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp_HelloWorld.lib;
using WebApp_HelloWorld.Models;

namespace WebApp_HelloWorld.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //var dbService = new DBService();
            //var tb = dbService.Single<Tb>("select * from Tb limit 1").Result;

            //ViewData["Message"] = string.Format("Idx:{0},Name:{1}", tb.Idx, tb.Name);
            //ViewData["Message"] = "Hello World";

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
