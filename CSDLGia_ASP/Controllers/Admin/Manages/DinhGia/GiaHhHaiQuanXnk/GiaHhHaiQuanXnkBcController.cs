using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaHhHaiQuanXnk
{
    public class GiaHhHaiQuanXnkBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GiaHhHaiQuanXnkBcController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("GiaHhHaiQuanXnkBc")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.hqxnk.baocao", "Index"))
                {

                    ViewData["Nam"] = DateTime.Now.Year;
                    ViewData["Title"] = "Báo cáo tổng hợp hàng hoá hải quan trong xuất nhập khẩu";
                    ViewData["Danhmuc"] = _db.GiaHhHaiQuanXnkDm;
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_dghqxnk";
                    ViewData["MenuLv3"] = "menu_dghqxnk_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaHhHaiQuanXnk/BaoCao/Index.cshtml");
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
        [Route("GiaHhHaiQuanXnkBc/BcTH")]
        [HttpPost]
        public IActionResult BcTH(DateTime beginTime, DateTime endTime, string danhmuc)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.hqxnk.baocao", "Index"))
                {
                    var model = (from pl in _db.GiaHhHaiQuanXnk.Where(t => t.Thoidiem >= beginTime
                                 && t.Thoidiem <= endTime && t.Trangthai == "HT" && t.Manhom == danhmuc)
                                 join db in _db.DsDiaBan on pl.Madiaban equals db.MaDiaBan
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhHaiQuanXnk
                                 {
                                     Id = pl.Id,
                                     Mahs = pl.Mahs,
                                     Tendiaban = db.TenDiaBan,
                                     Soqd = pl.Soqd,
                                     Thoidiem = pl.Thoidiem,
                                 });
                    ViewData["tungay"] = beginTime;
                    ViewData["denngay"] = endTime;
                    ViewData["Title"] = "Báo cáo tổng hợp giá hàng hoá hải quan trong xuất nhập khẩu";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_dghqxnk";
                    ViewData["MenuLv3"] = "menu_dghqxnk_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaHhHaiQuanXnk/BaoCao/BcTh.cshtml", model);
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
        [Route("GiaHhHaiQuanXnkBc/BcCT")]
        [HttpPost]
        public IActionResult BcCT(DateTime beginTime, DateTime endTime, string danhmuc)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.hqxnk.baocao", "Index"))
                {
                    var model = (from pl in _db.GiaHhHaiQuanXnk.Where(t => t.Thoidiem >= beginTime
                                 && t.Thoidiem <= endTime && t.Trangthai == "HT" && t.Manhom == danhmuc)
                                 join db in _db.DsDiaBan on pl.Madiaban equals db.MaDiaBan
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhHaiQuanXnk
                                 {
                                     Id = pl.Id,
                                     Mahs = pl.Mahs,
                                     Tendiaban = db.TenDiaBan,
                                     Soqd = pl.Soqd,
                                     Thoidiem = pl.Thoidiem,
                                 });
                    /*var model = (from a in _db.GiaHhHaiQuanXnk.Where(t => t.Thoidiem >= beginTime 
                                 && t.Thoidiem <= endTime && t.Trangthai == "HT" && t.Manhom==danhmuc)
                                 join b in _db.GiaHhHaiQuanXnkCt on a.Mahs equals b.Mahs
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhHaiQuanXnk
                                 {
                                     Id = a.Id,
                                     Mahs = a.Mahs,

                                 });
                    */
                    ViewData["tungay"] = beginTime;
                    ViewData["denngay"] = endTime;
                    ViewData["ct"] = _db.GiaHhHaiQuanXnkCt.ToList();
                    ViewData["Title"] = "Báo cáo tổng hợp giá hàng hoá hải quan trong xuất nhập khẩu";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_dghqxnk";
                    ViewData["MenuLv3"] = "menu_dghqxnk_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaHhHaiQuanXnk/BaoCao/BcCt.cshtml", model);
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
