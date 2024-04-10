using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class HeThongChungController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public HeThongChungController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var model = _db.tblHeThong.First();

            ViewData["Title"] = "Cấu hình hệ thống";
            ViewData["MenuLv1"] = "menu_hethong";
            ViewData["MenuLv2"] = "menu_qthethong";
            ViewData["MenuLv3"] = "menu_hethongchung";
            return View("Views/Admin/Systems/HeThongChung/Index.cshtml", model);
        }

        public IActionResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.hethongchung", "Edit"))
                {
                    var model = _db.tblHeThong.FirstOrDefault(t => t.Id == Id);
                    return Ok(model);
                    ViewData["Title"] = "Cấu hình hệ thống";
                    ViewData["MenuLv1"] = "menu_hethong";
                    ViewData["MenuLv2"] = "menu_qthethong";
                    ViewData["MenuLv3"] = "menu_hethongchung";
                    return View("Views/Admin/Systems/HeThongChung/Edit.cshtml", model);
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
