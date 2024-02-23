using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDatDiaBan
{
    public class GiaDatDiaBanBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaDatDiaBanBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaDatDiaBan/BaoCao")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giadatdb.baocao", "Index"))
                {

                    ViewData["Nam"] = DateTime.Now.Year;
                    ViewData["Title"] = "Báo cáo tổng hợp bảng giá đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_giadatdiaban";
                    ViewData["MenuLv3"] = "menu_giadatdiaban_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaDatDiaBan/BaoCao/Index.cshtml");
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

        /*[Route("GiaDatDiaBan/Bc1")]
        [HttpPost]
        public IActionResult Bc1(int nambc)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.khunggd.baocao", "Index"))
                {

                    var model = _db.GiaDatDiaBan.FirstOrDefault(t => t.Thoidiem.Year == nambc);

               
                    ViewData["Nambc"] = nambc;
                    ViewData["Title"] = "Báo cáo tổng hợp bảng giá đất";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgkhunggd";
                    ViewData["MenuLv3"] = "menu_dgkhunggd_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaDatDiaBan/BaoCao/Bc1.cshtml", model);
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

        [Route("GiaDatDiaBan/Bc2")]
        [HttpPost]
        public IActionResult Bc2(int nambc)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giadatdb.baocao", "Index"))
                {

                    var model = _db.GiaDatDiaBan.Where(t => t.Thoidiem.Year == nambc);

                    ViewData["Ct"] = _db.GiaDatDiaBanCt.ToList();
                    ViewData["Nambc"] = nambc;
                    ViewData["Title"] = "Báo cáo chi tiết bảng giá đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_giadatdiaban";
                    ViewData["MenuLv3"] = "menu_giadatdiaban_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaDatDiaBan/BaoCao/Bc2.cshtml", model);
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
