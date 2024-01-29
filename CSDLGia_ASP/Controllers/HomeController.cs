using CSDLGia_ASP.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace CSDLGia_ASP.Controllers
{
    public class HomeController : Controller
    {
       
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
            
        }

       
    }
}
