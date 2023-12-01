using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.Giathuemuanhaxh
{
    public class GiathuemuanhaxhBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiathuemuanhaxhBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("BaoCaoDgThueMuaNhaXh")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanhaxh.baocao", "Index"))
                {
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");

                    ViewData["Title"] = "Báo cáo tổng hợp định giá thuê thuê mua nhà xh";
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

        [Route("BaoCaoDgThueMuaNhaXh/Bc1")]
        [HttpPost]
        public IActionResult Bc(DateTime tungay, DateTime denngay, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanhaxh.baocao", "Index"))
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

                    ViewData["Title"] = "Báo cáo tổng hợp định giá thuê thuê mua nhà xh";
                    ViewData["ct"] = modelCt;
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmnxh";
                    ViewData["MenuLv3"] = "menu_dgtmnxh_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaThueMuaNhaXh/BaoCao/Bc1.cshtml", model);
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
