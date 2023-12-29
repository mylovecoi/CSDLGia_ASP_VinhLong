using Microsoft.AspNetCore.Mvc;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class ThietLapTrangCongBoController : Controller
    {
        public IActionResult Index()
        {
            return View("Views/Admin/Systems/ThietLapTrangCongBo/Index.cshtml");
        }
    }
}
