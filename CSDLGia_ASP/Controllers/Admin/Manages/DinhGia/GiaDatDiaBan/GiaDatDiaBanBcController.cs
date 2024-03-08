using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
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


        [Route("BaoCaoGiaDatDiaBan")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giadatdb.baocao", "Index"))
                {

                    ViewData["Nam"] = DateTime.Now.Year;
                    ViewData["Title"] = "Báo cáo tổng hợp giá đất địa bàn";
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
    
        [Route("BaoCaoGiaDatDiaBan/BcTH")]
        [HttpPost]
        public IActionResult BcTH(DateTime tungay, DateTime denngay)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giadatdb.baocao", "Index"))
                {
                    var model = (from pl in _db.GiaDatDiaBan.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT")
                                 join db in _db.DsDiaBan on pl.Madiaban equals db.MaDiaBan
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaDatDiaBan
                                 {
                                     Id = pl.Id,
                                     Mahs = pl.Mahs,
                                     Soqd = pl.Soqd,
                                     Thoidiem = pl.Thoidiem,
                                 });
                    ViewData["tungay"] = tungay;
                    ViewData["denngay"] = denngay;
                    ViewData["Title"] = "Báo cáo tổng hợp giá đất địa bàn";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_giadatdiaban";
                    ViewData["MenuLv3"] = "menu_giadatdiaban_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaDatDiaBan/BaoCao/BcTH.cshtml", model);
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

        [Route("BaoCaoGiaDatDiaBan/BcCT")]
        [HttpPost]
        public IActionResult BcCT(DateTime tungay, DateTime denngay)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giadatdb.baocao", "Index"))
                {
                    var model = _db.GiaDatDiaBan.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT");
                    ViewData["tungay"] = tungay;
                    ViewData["denngay"] = denngay;
                    ViewData["ct"] = _db.GiaDatDiaBanCt;
                    ViewData["Title"] = "Báo cáo chi tiết giá đất địa bàn";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_giadatdiaban";
                    ViewData["MenuLv3"] = "menu_giadatdiaban_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaDatDiaBan/BaoCao/BcCT.cshtml", model);
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
