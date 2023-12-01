using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaThueTsc
{
    public class GiaThueTSanCongBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaThueTSanCongBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("BaoCaoDgThueTsc")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetaisancong.baocao", "Index"))
                {
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");

                    ViewData["Title"] = "Báo cáo tổng hợp định giá thuê tài sản công";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtsc";
                    ViewData["MenuLv3"] = "menu_dgtsc_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaThueTsc/BaoCao/Index.cshtml");
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

        [Route("BaoCaoDgThueTsc/Bc1")]
        [HttpPost]
        public IActionResult Bc(DateTime tungay, DateTime denngay, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetaisancong.baocao", "Index"))
                {

                    var model = from dg in _db.GiaThueTaiSanCong.Where(t => t.Trangthai == "HT" && t.Madv == Madv)
                                    //join com in _db.Company on dg.Madv equals com.Madv
                                join ct in _db.GiaThueTaiSanCongCt on dg.Mahs equals ct.Mahs
                                join dm in _db.GiaThueTaiSanCongDm on ct.Mataisan equals dm.Mataisan
                                select new VMDinhGiaThueTsc
                                {
                                    Id = dg.Id,
                                    Mahs = dg.Mahs,
                                    Thoidiem = dg.Thoidiem,

                                    Dvthue = ct.Dvthue,
                                    Tentaisan = dm.Tentaisan,
                                    Diachi = ct.Diachi,
                                    Soqdpd = ct.Soqdpd,
                                    Thoigianpd = ct.Thoigianpd,
                                    Soqddg = ct.Soqddg,
                                    Thoigiandg = ct.Thoigiandg,

                                    Sotienthuenam = ct.Sotienthuenam,
                                    Hdthue = ct.Hdthue,
                                    Ththue = ct.Ththue,
                                    Thuetungay = ct.Thuetungay,
                                    Thuedenngay = ct.Thuedenngay,
                                };

                    if (tungay.ToString("yyMMdd") != "010101")
                    {
                        model = model.Where(t => t.Thoidiem >= tungay);
                    }
                    if (denngay.ToString("yyMMdd") != "010101")
                    {
                        model = model.Where(t => t.Thoidiem <= denngay);
                    }

                    ViewData["tungay"] = tungay;
                    ViewData["denngay"] = denngay;
                    ViewData["Title"] = "Báo cáo tổng hợp định giá thuê tài sản công";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtsc";
                    ViewData["MenuLv3"] = "menu_dgtsc_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaThueTsc/BaoCao/Bc1.cshtml", model);
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
