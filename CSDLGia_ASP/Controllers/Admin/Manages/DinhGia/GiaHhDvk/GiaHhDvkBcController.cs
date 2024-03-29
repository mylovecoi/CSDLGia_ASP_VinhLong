
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CSDLGia_ASP.Database;
using System.Security.Cryptography;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using CSDLGia_ASP.Models.Systems;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaHhDvk
{
    public class GiaHhDvkBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaHhDvkBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaHhDvk/BaoCao")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.bc", "Index"))
                {
                    ViewData["Nam"] = DateTime.Now.Year;
                    ViewData["Title"] = "Báo cáo giá HH-DV khác";
                    ViewData["MenuLv1"] = "menu_hhdvk";
                    ViewData["MenuLv2"] = "menu_hhdvk_bc";
                    ViewData["DanhSachHoSo"] = _db.GiaHhDvk.Where(t => t.Thoidiem.Year == DateTime.Now.Year);
                    ViewData["DanhSachNhom"] = _db.GiaHhDvkNhom;
                    return View("Views/Admin/Manages/DinhGia/GiaHhDvk/BaoCao/Index.cshtml");
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

        [Route("GiaHhDvk/BaoCao/BcTH")]
        [HttpPost]
        public IActionResult BcTH(DateTime tungay, DateTime denngay, string chucdanhky, string hotennguoiky, string MaNhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.bc", "Index"))
                {

                    var model = (from hoso in _db.GiaHhDvk.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT")
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvk
                                 {
                                     TenDonVi = donvi.TenDv,
                                     Mahs = hoso.Mahs,
                                     Soqd = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                 });

                    ViewData["Title"] = "Báo cáo giá HH-DV khác";
                    ViewData["MenuLv1"] = "menu_hhdvk";
                    ViewData["MenuLv2"] = "menu_hhdvk_bc";
                    ViewData["NgayTu"] = tungay;
                    ViewData["NgayDen"] = denngay;
                    ViewData["ChucDanhNguoiKy"] = chucdanhky;
                    ViewData["HoTenNguoiKy"] = hotennguoiky;
                    return View("Views/Admin/Manages/DinhGia/GiaHhDvk/BaoCao/BcTH.cshtml", model);
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

        [Route("GiaHhDvk/BaoCao/BcCT")]
        [HttpPost]
        public IActionResult BcCT(DateTime? ngaytu, DateTime? ngayden, string MaHsTongHop, string chucdanhky, string hotennguoiky, string Matt)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.bc", "Index"))
                {
                    var model = (from ct in _db.GiaHhDvkCt
                                 join kk in _db.GiaHhDvk on ct.Mahs equals kk.Mahs
                                 join dm in _db.GiaHhDvkDm on ct.Mahhdv equals dm.Mahhdv
                                 join dv in _db.DsDonVi on kk.Madv equals dv.MaDv
                                 join nhom in _db.GiaHhDvkNhom on kk.Matt equals nhom.Matt
                                 select new GiaHhDvkCt
                                 {
                                     Id = ct.Id,
                                     Mahs = ct.Mahs,
                                     Mahhdv = ct.Mahhdv,
                                     Gialk = ct.Gialk,
                                     Gia = ct.Gia,
                                     Ghichu = ct.Ghichu,
                                     Matt = ct.Matt,
                                     Tenhhdv = dm.Tenhhdv,
                                     Dacdiemkt = dm.Dacdiemkt,
                                     Dvt = dm.Dvt,
                                     Madv = kk.Madv,
                                     Thoidiem = kk.Thoidiem,
                                     Tendv = dv.TenDv,
                                     Tentt = nhom.Tentt,
                                     Trangthai = kk.Trangthai
                                 }); ;
                    model = model.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && t.Trangthai == "HT");

                    if (MaHsTongHop != "all") { model = model.Where(t => t.Mahs == MaHsTongHop); }
                    List<string> list_madv = model.Select(t => t.Madv).ToList();

                    var model_donvi = _db.DsDonVi.Where(t => list_madv.Contains(t.MaDv));

                    List<string> list_mahs = model.Select(t => t.Mahs).ToList();
                    var model_hoso = _db.GiaHhDvk.Where(t => list_mahs.Contains(t.Mahs));

                    ViewData["DonVis"] = model_donvi;
                    ViewData["ChiTietHs"] = model_hoso;
                    ViewData["NgayTu"] = ngaytu;
                    ViewData["NgayDen"] = ngayden;
                    ViewData["DmDvt"] = _db.DmDvt.ToList();
                    ViewData["HoTenNguoiKy"] = hotennguoiky;
                    ViewData["ChucDanhNguoiKy"] = chucdanhky;
                    ViewData["Title"] = "Báo cáo giá HH-DV khác";
                    return View("Views/Admin/Manages/DinhGia/GiaHhDvk/BaoCao/BcCT.cshtml", model);
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
