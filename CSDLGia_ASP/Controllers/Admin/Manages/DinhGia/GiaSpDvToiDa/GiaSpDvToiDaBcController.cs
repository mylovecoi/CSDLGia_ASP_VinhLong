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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaSpDvToiDa
{
    public class GiaSpDvToiDaBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaSpDvToiDaBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaSpDvToiDa/BaoCao")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvtoida.baocao", "Index"))
                {
                    DateTime nowDate = DateTime.Now;
                    DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
                    DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);
                    ViewData["ngaytu"] = firstDayCurrentYear.ToString("yyyy-MM-dd");
                    ViewData["ngayden"] = lastDayCurrentYear.ToString("yyyy-MM-dd");

                    ViewData["Title"] = "Báo cáo giá sản phẩm dịch vụ tối đa";
                    ViewData["MenuLv1"] = "menu_spdvtoida";
                    ViewData["MenuLv2"] = "menu_spdvtoida_baocao";
                    ViewData["DanhSachHoSo"] = _db.GiaSpDvToiDa.Where(t => t.Thoidiem >= firstDayCurrentYear && t.Thoidiem <= lastDayCurrentYear && t.Trangthai == "HT");

                    ViewData["DanhSachNhom"] = _db.GiaSpDvToiDaNhom;
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvToiDa/BaoCao/Index.cshtml");
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

        [Route("GiaSpDvToiDa/BaoCao/BcTH")]
        [HttpPost]
        public IActionResult BcTH(DateTime tungay, DateTime denngay, string chucdanhky, string hotennguoiky, string MaNhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvtoida.baocao", "Index"))
                {

                    var model = (from hoso in _db.GiaSpDvToiDa.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT")
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvToiDa
                                 {
                                     TenDonVi = donvi.TenDv,
                                     Mahs = hoso.Mahs,
                                     Soqd = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                     Ttqd = hoso.Ttqd,
                                 });

                    ViewData["Title"] = "Báo cáo giá sản phẩm dịch vụ tối đa";
                    ViewData["MenuLv1"] = "menu_spdvtoida";
                    ViewData["MenuLv2"] = "menu_spdvtoida_baocao";
                    ViewData["NgayTu"] = tungay;
                    ViewData["NgayDen"] = denngay;
                    ViewData["ChucDanhNguoiKy"] = chucdanhky;
                    ViewData["HoTenNguoiKy"] = hotennguoiky;
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvToiDa/BaoCao/BcTH.cshtml", model);
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

        [Route("GiaSpDvToiDa/BaoCao/BcCT")]
        [HttpPost]
        public IActionResult BcCT(DateTime? ngaytu, DateTime? ngayden, string MaHsTongHop, string chucdanhky, string hotennguoiky, string MaNhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvtoida.baocao", "Index"))
                {
                    DateTime nowDate = DateTime.Now;
                    DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
                    DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);
                    MaNhom = string.IsNullOrEmpty(MaNhom) ? "all" : MaNhom;
                    ngaytu = ngaytu.HasValue ? ngaytu : firstDayCurrentYear;
                    ngayden = ngayden.HasValue ? ngayden : lastDayCurrentYear;

                    var model = (from giaspdvtoidact in _db.GiaSpDvToiDaCt
                                 join giaspdvtoida in _db.GiaSpDvToiDa on giaspdvtoidact.Mahs equals giaspdvtoida.Mahs
                                 join donvi in _db.DsDonVi on giaspdvtoida.Madv equals donvi.MaDv
                                 select new GiaSpDvToiDaCt
                                 {
                                     Id = giaspdvtoidact.Id,
                                     Dvt = giaspdvtoidact.Dvt,
                                     Mahs = giaspdvtoidact.Mahs,
                                     Madv = giaspdvtoida.Madv,
                                     Thoidiem = giaspdvtoida.Thoidiem,
                                     Tendv = donvi.TenDv,
                                     Ttqd = giaspdvtoida.Ttqd,
                                     Tenspdv = giaspdvtoidact.Tenspdv,
                                     Dongia = giaspdvtoidact.Dongia,
                                     Ghichu = giaspdvtoida.Ttqd,
                                     Madiaban = giaspdvtoidact.Madiaban,
                                     Manhom = giaspdvtoidact.Manhom,
                                     Trangthai = giaspdvtoida.Trangthai,

                                 });


                    model = model.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && t.Trangthai == "HT");
                    if (MaNhom != "all") { model = model.Where(t => t.Manhom == MaNhom); }
                    if (MaHsTongHop != "all") { model = model.Where(t => t.Mahs == MaHsTongHop); }

                    List<string> list_madv = model.Select(t => t.Madv).ToList();
                    var model_donvi = _db.DsDonVi.Where(t => list_madv.Contains(t.MaDv));

                    List<string> list_mahs = model.Select(t => t.Mahs).ToList();
                    var model_hoso = _db.GiaSpDvToiDa.Where(t => list_mahs.Contains(t.Mahs));

                    ViewData["DonVis"] = model_donvi;
                    ViewData["ChiTietHs"] = model_hoso;
                    ViewData["NgayTu"] = ngaytu;
                    ViewData["NgayDen"] = ngayden;

                    ViewData["HoTenNguoiKy"] = hotennguoiky;
                    ViewData["ChucDanhNguoiKy"] = chucdanhky;
                    ViewData["Title"] = "Báo cáo giá sản phẩm dịch vụ tối đa";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvToiDa/BaoCao/BcCT.cshtml", model);
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
