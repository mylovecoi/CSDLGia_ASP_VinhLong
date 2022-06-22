using CSDLGia_ASP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CSDLGia_ASP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                return RedirectToAction("Index", "CongBo");
            }
            else
            {
                ViewData["Title"] = "Trang chủ";
                ViewData["MenuLv1"] = "menu_home";
                return View("Views/Admin/Home/Index.cshtml");
            }
            /*if (string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                ViewData["Title"] = "Login";
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ViewData["Title"] = "Trang chủ";
                ViewData["MenuLv1"] = "menu_home";
                return View("Views/Admin/Home/Index.cshtml");
            }*/
        }

        /*public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/
    }
}
