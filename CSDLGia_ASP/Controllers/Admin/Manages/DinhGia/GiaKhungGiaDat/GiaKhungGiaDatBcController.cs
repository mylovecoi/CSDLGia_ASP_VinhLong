using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaKhungGiaDat
{
    public class GiaKhungGiaDatBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaKhungGiaDatBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaKhungGiaDat/BaoCao")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khunggd.baocao", "Index"))
                {

                    ViewData["Nam"] = DateTime.Now.Year;
                    ViewData["Title"] = "Báo cáo tổng hợp giá khung giá đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgkhunggd";
                    ViewData["MenuLv3"] = "menu_dgkhunggd_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaKhungGiaDat/BaoCao/Index.cshtml");
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

        /*[Route("GiaKhungGiaDat/Bc1")]
        [HttpPost]
        public IActionResult Bc1(int nambc)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khunggd.baocao", "Index"))
                {

                    var model = _db.GiaKhungGiaDat.FirstOrDefault(t => t.Thoidiem.Year == nambc);

               
                    ViewData["Nambc"] = nambc;
                    ViewData["Title"] = "Báo cáo tổng hợp giá khung giá đất";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgkhunggd";
                    ViewData["MenuLv3"] = "menu_dgkhunggd_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaKhungGiaDat/BaoCao/Bc1.cshtml", model);
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
        }*/

        [Route("GiaKhungGiaDat/Bc2")]
        [HttpPost]
        public IActionResult Bc2(int nambc)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khunggd.baocao", "Index"))
                {

                    var model = _db.GiaKhungGiaDat.Where(t => t.Thoidiem.Year == nambc);

                    ViewData["Ct"] = _db.GiaKhungGiaDatCt.ToList();
                    ViewData["Nambc"] = nambc;
                    ViewData["Title"] = "Báo cáo chi tiết giá khung giá đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgkhunggd";
                    ViewData["MenuLv3"] = "menu_dgkhunggd_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaKhungGiaDat/BaoCao/Bc2.cshtml", model);
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
