using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaSpDvCongIch
{
    public class GiaSpDvCongIchBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaSpDvCongIchBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("BcGiaSpDvCongIch")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.spdvci.baocao", "Index"))
                {

                    ViewData["Nam"] = DateTime.Now.Year;
                    ViewData["Title"] = "Báo cáo tổng hợp giá sản phẩm dịch vụ công ích";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdvci";
                    ViewData["MenuLv3"] = "menu_dgdvci_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCongIch/BaoCao/Index.cshtml");
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

        [Route("BcGiaSpDvCongIch/BcTH")]
        [HttpPost]
        public IActionResult BcTH(DateTime tungay, DateTime denngay, string tenthutruong, string chucvu)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.spdvci.baocao", "Index"))
                {
                    var model = (from pl in _db.GiaSpDvCongIch.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT")
                                 join db in _db.DsDiaBan on pl.Madiaban equals db.MaDiaBan
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCongIch
                                 {
                                     Id = pl.Id,
                                     Mahs = pl.Mahs,
                                     Tendiaban = db.TenDiaBan,
                                     Soqd = pl.Soqd,
                                     Thoidiem = pl.Thoidiem,
                                 });
                    ViewData["tungay"] = tungay;
                    ViewData["denngay"] = denngay;

                    ViewData["tenthutruong"] = tenthutruong;
                    ViewData["chucvu"] = chucvu;

                    ViewData["Title"] = "Báo cáo tổng hợp giá sản phẩm dịch vụ công ích";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdvci";
                    ViewData["MenuLv3"] = "menu_dgdvci_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCongIch/BaoCao/BcTH.cshtml", model);
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

        [Route("BaoCaoDgSpDvCongIch/BcCT")]
        [HttpPost]
        public IActionResult BcCT(DateTime tungay, DateTime denngay, string tenthutruong, string chucvu)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.spdvci.baocao", "Index"))
                {
                    var model = _db.GiaSpDvCongIch.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT");
                    ViewData["GiaSpDvCongIchDm"] = _db.GiaSpDvCongIchDm.ToList();
                    ViewData["tungay"] = tungay;
                    ViewData["denngay"] = denngay;

                    ViewData["tenthutruong"] = tenthutruong;
                    ViewData["chucvu"] = chucvu;

                    ViewData["ct"] = _db.GiaSpDvCongIchCt.ToList();
                    ViewData["Title"] = "Báo cáo tổng hợp giá sản phẩm dịch vụ công ích";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdvci";
                    ViewData["MenuLv3"] = "menu_dgdvci_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCongIch/BaoCao/BcCT.cshtml", model);
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
