using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaoDucDaoTao
{
    public class GiaoDucDaoTaoBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaoDucDaoTaoBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("BaoCaoDinhGiaGdDt")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaoducdaotao.baocao", "Index"))
                {
                    ViewData["Nam"] = DateTime.Now.Year;
                    ViewData["Title"] = "Báo cáo tổng hợp mức giá dịch vụ đào tạo";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dggddt";
                    ViewData["MenuLv3"] = "menu_dggddt_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaoDucDaoTao/BaoCao/Index.cshtml");
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

        //GiaDvGdDt
        //GiaDvGdDtCt
        // GiaDvGdDtDm
        [Route("BaoCaoDinhGiaGdDtBcTH")]
        [HttpPost]
        public IActionResult BcTH(DateTime tungay, DateTime denngay, string tenthutruong, string chucvu)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaoducdaotao.baocao", "Index"))
                {
                    var model = _db.GiaDvGdDt.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT");
                    ViewData["tungay"] = tungay;
                    ViewData["denngay"] = denngay;
                    ViewData["tenthutruong"] = tenthutruong;
                    ViewData["chucvu"] = chucvu;

                    ViewData["Title"] = "Báo cáo tổng hợp giá dịch vụ đào tạo";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dggddt";
                    ViewData["MenuLv3"] = "menu_dggddt_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaoDucDaoTao/BaoCao/BcTH.cshtml", model);
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

        [Route("BaoCaoDinhGiaGdDt/BcCT")]
        [HttpPost]
        public IActionResult BcCT(DateTime tungay, DateTime denngay, string tenthutruong, string chucvu)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaoducdaotao.baocao", "Index"))
                {
                    var model = _db.GiaDvGdDt.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT");

                    var modelct = (from a in _db.GiaDvGdDtCt                             
                                   select new GiaDvGdDtCt
                                   {
                                       Tenspdv = a.Mota,
                                       Mahs = a.Mahs,
                                       Namapdung1 = a.Namapdung1,
                                       Giamiennui1 = a.Giamiennui1,
                                       Giathanhthi1 = a.Giathanhthi1,
                                       Gianongthon1 = a.Gianongthon1,                                     

                                   }); ;

                    ViewData["tungay"] = tungay;
                    ViewData["denngay"] = denngay;
                    ViewData["tenthutruong"] = tenthutruong;
                    ViewData["chucvu"] = chucvu;

                    ViewData["ct"] = modelct;
                    ViewData["Title"] = "Báo cáo chi tiết giá dịch vụ đào tạo";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dggddt";
                    ViewData["MenuLv3"] = "menu_dggddt_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaoDucDaoTao/BaoCao/BcCT.cshtml", model);
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
