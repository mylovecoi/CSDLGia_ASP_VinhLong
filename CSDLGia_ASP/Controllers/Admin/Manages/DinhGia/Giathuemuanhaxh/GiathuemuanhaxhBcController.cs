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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaThueMuaNhaXh
{
    public class GiaThueMuaNhaXhBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaThueMuaNhaXhBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaThueMuaNhaXh/BaoCao")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.tmnxh.bc", "Index"))
                {
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Title"] = "Báo cáo định giá thuê thuê mua nhà xã hội";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmnxh";
                    ViewData["MenuLv3"] = "menu_dgtmnxh_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaThueMuaNhaXh/BaoCao/Index.cshtml");
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

        [Route("GiaThueMuaNhaXh/BaoCao/BcTH")]
        [HttpPost]
        public IActionResult BcTH(DateTime tungay, DateTime denngay, string tenthutruong, string chucvu)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.tmnxh.bc", "Index"))
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
                    ViewData["Title"] = "Báo cáo tổng hợp định giá thuê, mua nhà xã hội";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmnxh";
                    ViewData["MenuLv3"] = "menu_dgtmnxh_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaThueMuaNhaXh/BaoCao/BcTH.cshtml");
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

        [Route("GiaThueMuaNhaXh/BaoCao/BcCT")]
        [HttpPost]
        public IActionResult BcCT(DateTime tungay, DateTime denngay, string Madv, string chucvu, string tenthutruong)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.tmnxh.bc", "Index"))
                {
                    var tendv = _db.DsDonVi.Where(t => t.MaDv == Madv).FirstOrDefault();
                    var modelCt = from ct in _db.GiaThueMuaNhaXhCt
                                  join dm in _db.GiaThueMuaNhaXhDm on ct.Maso equals dm.Maso
                                  select new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMuaNhaXhCt
                                  {
                                      //Thoidiem = dg.Thoidiem,
                                      Tendv = tendv.TenDv,
                                      //Ghichu = dg.Ghichu,
                                      Diachi = ct.Diachi,
                                      Soqdpd = ct.Soqdpd,
                                      Thoigianpd = ct.Thoigianpd,
                                      Soqddg = ct.Soqddg,
                                      Thoigiandg = ct.Thoigiandg,
                                      Dvthue = ct.Dvthue,
                                      Dongia = ct.Dongia,
                                      Dongiathue = ct.Dongiathue,
                                      Hdthue = ct.Hdthue,
                                      Ththue = ct.Ththue,
                                      Tungay = ct.Tungay,
                                      Denngay = ct.Denngay,
                                      Mahs = ct.Mahs,
                                      Dientich = dm.Dientich,
                                      Tennha = dm.Tennha,
                                  };

                    var model = _db.GiaThueMuaNhaXh.AsQueryable();

                    if (tungay.ToString("yyMMdd") != "010101")
                    {
                        model = model.Where(t => t.Thoidiem >= tungay);
                    }
                    if (denngay.ToString("yyMMdd") != "010101")
                    {
                        model = model.Where(t => t.Thoidiem <= denngay);
                    }

                    ViewData["Title"] = "Báo cáo chi tiết định giá thuê, mua nhà xã hội";
                    ViewData["ct"] = modelCt;
                    ViewData["tenthutruong"] = tenthutruong;
                    ViewData["chucvu"] = chucvu;
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmnxh";
                    ViewData["MenuLv3"] = "menu_dgtmnxh_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaThueMuaNhaXh/BaoCao/BcCT.cshtml", model);
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
