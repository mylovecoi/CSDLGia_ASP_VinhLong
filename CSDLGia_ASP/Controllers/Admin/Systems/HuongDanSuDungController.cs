using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class HuongDanSuDungController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public HuongDanSuDungController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("HuongDanSuDung")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.hdsd", "Index"))
                {
                    var model = _db.HuongDanSuDung.ToList();
                    ViewData["Title"] = "Danh sách tài liệu hướng dẫn sử dụng";
                    ViewData["MenuLv1"] = "menu_hethong";
                    ViewData["MenuLv2"] = "menu_qthethong";
                    ViewData["MenuLv3"] = "menu_hdsd";
                    return View("Views/Admin/Systems/HuongDanSuDung/Index.cshtml", model);
                }
                else
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }
    }
}
