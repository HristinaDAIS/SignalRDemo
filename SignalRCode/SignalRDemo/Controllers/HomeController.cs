using Microsoft.AspNetCore.Mvc;
using SignalRDemo.Models.ViewModels;
using SignalRDemo.Providers.Accounts;
using System.Diagnostics;

namespace SignalRDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
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
