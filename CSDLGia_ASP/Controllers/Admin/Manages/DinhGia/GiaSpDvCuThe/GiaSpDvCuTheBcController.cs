using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaSpDvCuThe
{
    public class GiaSpDvCuTheBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaSpDvCuTheBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("BaoCaoDgSpDvCuThe")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.spdvcuthe.baocao", "Index"))
                {

                    ViewData["Nam"] = DateTime.Now.Year;
                    ViewData["Title"] = "Báo cáo tổng hợp giá sản phẩm dịch vụ cụ thể";
                    ViewData["MenuLv1"] = "menu_spdvcuthe";
                    ViewData["MenuLv2"] = "menu_spdvcuthe_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCuThe/BaoCao/Index.cshtml");
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
      
        [Route("BaoCaoDgSpDvCuThe/BcTH")]
        [HttpPost]
        public IActionResult BcTH(DateTime tungay, DateTime denngay)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.spdvcuthe.baocao", "Index"))
                {
                    var model = _db.GiaSpDvCuThe.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT");
                    ViewData["tungay"] = tungay;
                    ViewData["denngay"] = denngay;
                    ViewData["Title"] = "Báo cáo tổng hợp giá sản phẩm dịch vụ cụ thể";
                    ViewData["MenuLv1"] = "menu_spdvcuthe";
                    ViewData["MenuLv2"] = "menu_spdvcuthe_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCuThe/BaoCao/BcTH.cshtml", model);
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

        [Route("BaoCaoDgSpDvCuThe/BcCT")]
        [HttpPost]
        public IActionResult BcCT(DateTime tungay, DateTime denngay)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.spdvcuthe.baocao", "Index"))
                {
                    var model = _db.GiaSpDvCuThe.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT");
                    ViewData["tungay"] = tungay;
                    ViewData["denngay"] = denngay;
                    ViewData["ct"] = _db.GiaSpDvCuTheCt.ToList();
                    ViewData["Title"] = "Báo cáo tổng hợp giá sản phẩm dịch vụ cụ thể";
                    ViewData["MenuLv1"] = "menu_spdvcuthe";
                    ViewData["MenuLv2"] = "menu_spdvcuthe_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCuThe/BaoCao/BcCT.cshtml", model);
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
