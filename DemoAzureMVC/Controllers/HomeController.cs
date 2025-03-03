using System.Diagnostics;
using DemoAzureMVC.Models;
using Microsoft.AspNetCore.Mvc;


namespace DemoAzureMVC.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
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
