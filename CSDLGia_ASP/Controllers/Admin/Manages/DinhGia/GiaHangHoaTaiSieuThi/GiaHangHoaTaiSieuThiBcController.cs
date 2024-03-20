using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CSDLGia_ASP.Database;
using System.Security.Cryptography;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.KeKhaiGia;
using CSDLGia_ASP.ViewModels.Systems;
using CSDLGia_ASP.ViewModels.Manages.KeKhaiGia;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using CSDLGia_ASP.Models.Manages.DinhGia;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaHangHoaTaiSieuThi
{
    public class GiaHangHoaTaiSieuThiBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaHangHoaTaiSieuThiBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaHangHoaTaiSieuThi/BaoCao")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giasieuthi.baocao", "Index"))
                {
                    ViewData["Nam"] = DateTime.Now.Year;
                    ViewData["Title"] = "Báo cáo tổng hợp giá hàng hóa tại siêu thị";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgsieuthi";
                    ViewData["MenuLv3"] = "menu_dgsieuthi_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaHangHoaTaiSieuThi/BaoCao/Index.cshtml");
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

        [Route("GiaHangHoaTaiSieuThi/BaoCao/BcTH")]
        [HttpPost]
        public IActionResult BcTH(DateTime tungay, DateTime denngay, string tenthutruong, string chucvu)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giasieuthi.baocao", "Index"))
                {

                    var model = (from pl in _db.GiaHangHoaTaiSieuThi.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT")
                                 join db in _db.DsDiaBan on pl.Madiaban equals db.MaDiaBan
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaHangHoaTaiSieuThi
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
                    ViewData["Title"] = "Báo cáo tổng hợp giá giá hàng hóa tại siêu thị";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgsieuthi";
                    ViewData["MenuLv3"] = "menu_dgsieuthi_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaHangHoaTaiSieuThi/BaoCao/BcTH.cshtml", model);
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

        [Route("GiaHangHoaTaiSieuThi/BaoCao/BcCT")]
        [HttpPost]
        public IActionResult BcCT(DateTime tungay, DateTime denngay, string tenthutruong, string chucvu)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giasieuthi.baocao", "Index"))
                {
                    var model = _db.GiaHangHoaTaiSieuThi.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT");
                            
                    ViewData["tungay"] = tungay;
                    ViewData["denngay"] = denngay;
                    ViewData["tenthutruong"] = tenthutruong;
                    ViewData["chucvu"] = chucvu;
                    ViewData["Chitiet"] = _db.GiaHangHoaTaiSieuThiCt;
                    ViewData["Title"] = "Báo cáo tổng hợp giá giá hàng hóa tại siêu thị";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgsieuthi";
                    ViewData["MenuLv3"] = "menu_dgsieuthi_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaHangHoaTaiSieuThi/BaoCao/BcCT.cshtml", model);
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
