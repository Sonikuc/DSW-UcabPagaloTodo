using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UCABPagaloTodoWeb.Models;

namespace UCABPagaloTodoWeb.Controllers
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
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            //SI EL USUARIO ESTA LOGUEADO, SE REDIRIGE A LA VISTA DE PRIVACY
            //SI EL USUARIO NO ESTA LOGUEADO, LO MANDA PARA LA VISTA HOME
            string userId =HttpContext.Session.GetString("UserId");
            if (userId != null) {
                return View();
            }
            return RedirectToAction("AccessDenied", "Home");
        }

        public IActionResult UserHome()
        {
            return View();
        }

        public IActionResult AccessDeniedView()
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