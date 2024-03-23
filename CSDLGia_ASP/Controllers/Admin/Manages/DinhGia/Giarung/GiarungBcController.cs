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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.Giarung
{
    public class GiarungBcController : Controller
    {

        private readonly CSDLGiaDBContext _db;

        public GiarungBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("BaoCaoDgrung")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.rung.baocao", "Index"))
                {
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");

                    ViewData["Title"] = "Báo cáo tổng hợp định giá rừng";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgr";
                    ViewData["MenuLv3"] = "menu_dgr_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaRung/BaoCao/Index.cshtml");
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

        [Route("BaoCaoDgrung/BcTH")]
        [HttpPost]
        public IActionResult BcTH(DateTime tungay, DateTime denngay, string tenthutruong, string chucvu)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgxmtxd.giakkbc", "Index"))
                {
                    //var model = (from pl in _db.GiaThueMatDatMatNuoc.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT")
                    //             join db in _db.DsDiaBan on pl.Madiaban equals db.MaDiaBan
                    //             select new GiaThueMatDatMatNuoc
                    //             {
                    //                 Id = pl.Id,
                    //                 Mahs = pl.Mahs,
                    //                 Tendiaban = db.TenDiaBan,
                    //                 Soqd = pl.Soqd,
                    //                 Thoidiem = pl.Thoidiem,
                    //             });
                    ViewData["tungay"] = tungay;
                    ViewData["denngay"] = denngay;
                    ViewData["tenthutruong"] = tenthutruong;
                    ViewData["chucvu"] = chucvu;
                    ViewData["Title"] = "Báo cáo tổng hợp định giá rừng";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgr";
                    ViewData["MenuLv3"] = "menu_dgr_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaRung/BaoCao/BcTH.cshtml");
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

        [Route("BaoCaoDgrung/BcCT")]
        [HttpPost]
        public IActionResult BcCT(DateTime tungay, DateTime denngay, string tenthutruong, string chucvu)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgxmtxd.giakkbc", "Index"))
                {
                    //var model = _db.GiaThueMatDatMatNuoc.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT");
                    ViewData["tungay"] = tungay;
                    ViewData["denngay"] = denngay;
                    ViewData["tenthutruong"] = tenthutruong;
                    ViewData["chucvu"] = chucvu;
                    //ViewData["ct"] = _db.GiaThueMatDatMatNuocCt;
                    ViewData["Title"] = "Báo cáo chi tiết định giá rừng";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgr";
                    ViewData["MenuLv3"] = "menu_dgr_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaRung/BaoCao/BcCT.cshtml");
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

        //[Route("BaoCaoDgrung/BaoCao")]
        //[HttpPost]
        //public IActionResult BaoCao(string phanloai, DateTime tungay, DateTime denngay, string Madv, string tenthutruong, string chucvu)
        //{
        //    if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
        //    {
        //        if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.rung.baocao", "Index"))
        //        {

        //            if (phanloai == "Thuê môi trường")
        //            {
        //                var model = from dg in _db.GiaRung.Where(t => t.Madv == Madv)
        //                            join ct in _db.GiaRungCt.Where(t => t.Phanloai == "Thuê môi trường") on dg.Mahs equals ct.Mahs
        //                            select new CSDLGia_ASP.Models.Manages.DinhGia.GiaRungCt
        //                            {
        //                                Thoidiem = dg.Thoidiem,
        //                                Dientich = ct.Dientich,
        //                                Dientichsd = ct.Dientichsd,
        //                                Dvthue = ct.Dvthue,
        //                                Diachi = ct.Diachi,
        //                                Soqdpd = ct.Soqdpd,
        //                                Thoigianpd = ct.Thoigianpd,
        //                                Soqdgkd = ct.Soqdgkd,
        //                                Thoigiangkd = ct.Thoigiangkd,
        //                                Giatri = ct.Giatri,
        //                                Dongia = ct.Dongia,

        //                                Thuetungay = ct.Thuetungay,
        //                                Thuedenngay = ct.Thuedenngay,
        //                            };

        //                if (tungay.ToString("yyMMdd") != "010101")
        //                {
        //                    model = model.Where(t => t.Thoidiem >= tungay);
        //                }
        //                if (denngay.ToString("yyMMdd") != "010101")
        //                {
        //                    model = model.Where(t => t.Thoidiem <= denngay);
        //                }

        //                double sum_dientichSd = 0;
        //                foreach (var item in model)
        //                {
        //                    sum_dientichSd += item.Dientichsd;
        //                }
        //                double sum_dientich = 0;
        //                foreach (var item in model)
        //                {
        //                    sum_dientich += item.Dientich;
        //                }
        //                double sum_dongia = 0;
        //                foreach (var item in model)
        //                {
        //                    sum_dongia += item.Dongia;
        //                }
        //                double sum_giatri = 0;
        //                foreach (var item in model)
        //                {
        //                    sum_giatri += item.Giatri;
        //                }

        //                ViewData["tenthutruong"] = tenthutruong;
        //                ViewData["chucvu"] = chucvu;

        //                ViewData["tungay"] = Helpers.ConvertDateToStr(tungay);
        //                ViewData["denngay"] = Helpers.ConvertDateToStr(denngay);
        //                ViewData["sum_dientich"] = sum_dientich;
        //                ViewData["sum_dientichSd"] = sum_dientichSd;
        //                ViewData["sum_giatri"] = sum_giatri;
        //                ViewData["sum_dongia"] = sum_dongia;
        //                ViewData["Title"] = "Báo cáo tổng hợp định giá rừng";
        //                ViewData["MenuLv1"] = "menu_dg";
        //                ViewData["MenuLv2"] = "menu_dgr";
        //                ViewData["MenuLv3"] = "menu_dgr_bc";
        //                return View("Views/Admin/Manages/DinhGia/GiaRung/BaoCao/BcMoiTruong.cshtml", model);
        //            }
        //            else
        //            {
        //                var model = from dg in _db.GiaRung.Where(t => t.Madv == Madv)
        //                            join ct in _db.GiaRungCt.Where(t => t.Phanloai == "Thanh lý" || t.Phanloai == "Khai thác") on dg.Mahs equals ct.Mahs
        //                            select new CSDLGia_ASP.Models.Manages.DinhGia.GiaRungCt
        //                            {
        //                                Thoidiem = dg.Thoidiem,
        //                                Dientich = ct.Dientich,
        //                                Dientichsd = ct.Dientichsd,
        //                                Dvthue = ct.Dvthue,
        //                                Diachi = ct.Diachi,
        //                                Soqdpd = ct.Soqdpd,
        //                                Thoigianpd = ct.Thoigianpd,
        //                                Soqdgkd = ct.Soqdgkd,
        //                                Thoigiangkd = ct.Thoigiangkd,
        //                                Giakhoidiem = ct.Giakhoidiem,
        //                                Giatri = ct.Giatri,
        //                            };

        //                if (tungay.ToString("yyMMdd") != "010101")
        //                {
        //                    model = model.Where(t => t.Thoidiem >= tungay);
        //                }
        //                if (denngay.ToString("yyMMdd") != "010101")
        //                {
        //                    model = model.Where(t => t.Thoidiem <= denngay);
        //                }

        //                double sum_dientichSd = 0;
        //                foreach (var item in model)
        //                {
        //                    sum_dientichSd += item.Dientichsd;
        //                }
        //                double sum_giakhoidiem = 0;
        //                foreach (var item in model)
        //                {
        //                    sum_giakhoidiem += item.Giakhoidiem;
        //                }
        //                double sum_giatri = 0;
        //                foreach (var item in model)
        //                {
        //                    sum_giatri += item.Giatri;
        //                }

        //                ViewData["tenthutruong"] = tenthutruong;
        //                ViewData["chucvu"] = chucvu;
        //                ViewData["tungay"] = Helpers.ConvertDateToStr(tungay);
        //                ViewData["denngay"] = Helpers.ConvertDateToStr(denngay);
        //                ViewData["sum_dientichSd"] = sum_dientichSd;
        //                ViewData["sum_giakhoidiem"] = sum_giakhoidiem;
        //                ViewData["sum_giatri"] = sum_giatri;
        //                ViewData["Title"] = "Báo cáo tổng hợp định giá rừng";
        //                ViewData["MenuLv1"] = "menu_dg";
        //                ViewData["MenuLv2"] = "menu_dgr";
        //                ViewData["MenuLv3"] = "menu_dgr_bc";
        //                return View("Views/Admin/Manages/DinhGia/GiaRung/BaoCao/BcKhaiThacThanhLy.cshtml", model);
        //            }
        //        }
        //        else
        //        {
        //            ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
        //            return View("Views/Admin/Error/Page.cshtml");
        //        }
        //    }
        //    else
        //    {
        //        return View("Views/Admin/Error/SessionOut.cshtml");
        //    }
        //}


    }
}
