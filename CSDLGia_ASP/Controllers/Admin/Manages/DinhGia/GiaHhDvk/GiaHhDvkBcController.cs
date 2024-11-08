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
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.Services;
using Microsoft.Extensions.Hosting;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaHhDvk
{
    public class GiaHhDvkBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IDsDonviService _dsDonviService;

        public GiaHhDvkBcController(CSDLGiaDBContext db, IDsDonviService dsDonviService)
        {
            _db = db;
            _dsDonviService = dsDonviService;
        }


        [Route("GiaHhDvk/BaoCao")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.bc", "Index"))
                {
                    DateTime nowDate = DateTime.Now;
                    DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
                    DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);
                    ViewData["ngaytu"] = firstDayCurrentYear.ToString("yyyy-MM-dd");
                    ViewData["ngayden"] = lastDayCurrentYear.ToString("yyyy-MM-dd");

                    ViewData["Title"] = "Báo cáo tổng hợp giá giá hàng hóa dịch vụ khác";
                    ViewData["MenuLv1"] = "menu_hhdvk";
                    ViewData["MenuLv2"] = "menu_hhdvk_bc";
                    ViewData["DanhSachHoSo"] = _db.GiaHhDvk.Where(t => t.Thoidiem >= firstDayCurrentYear && t.Thoidiem <= lastDayCurrentYear && t.Trangthai == "HT");
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
        public IActionResult BcTH(DateTime tungay, DateTime denngay, string chucdanhky, string hotennguoiky)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.bc", "Index"))
                {
                    var model_donvi = _dsDonviService.GetListDonvi(Helpers.GetSsAdmin(HttpContext.Session, "Madv"));
                    List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();

                    var model = (from hoso in _db.GiaHhDvk.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT")
                                 join donvi in _db.DsDonVi.Where(t=>list_madv.Contains(t.MaDv)) on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvk
                                 {
                                     TenDonVi = donvi.TenDv,
                                     Mahs = hoso.Mahs,
                                     Soqd = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                     Ghichu = hoso.Ghichu,
                                 });

                    ViewData["Title"] = "Báo cáo tổng hợp giá  giá giá hàng hóa dịch vụ khác";
                    ViewData["MenuLv1"] = "menu_hhdvk";
                    ViewData["MenuLv2"] = "menu_hhdvk_bc";
                    ViewData["tungay"] = tungay;
                    ViewData["denngay"] = denngay;
                    ViewData["ChucDanhNguoiKy"] = chucdanhky;
                    ViewData["HoTenNguoiKy"] = hotennguoiky;
                    //Định danh
                    var today = DateTime.Now;
                    ViewData["NgayTaoBaoCao"] = $"Ngày {today.Day} Tháng {today.Month} Năm {today.Year}";
                    var Madv = Helpers.GetSsAdmin(HttpContext.Session, "Madv");
                    var donVi = _db.Users.FirstOrDefault(x => x.Madv == Madv);
                    if (donVi != null)
                    {
                        ViewData["DinhDanh"] = donVi.Name;
                    }
                    else
                    {
                        ViewData["DinhDanh"] = "Lỗi";
                    }
                    //End Định danh
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

                    var model_donvi = _dsDonviService.GetListDonvi(Helpers.GetSsAdmin(HttpContext.Session, "Madv"));
                    List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();

                    var model = (from ct in _db.GiaHhDvkCt
                                 join kk in _db.GiaHhDvk on ct.Mahs equals kk.Mahs
                                 join nhom in _db.GiaHhDvkNhom on kk.Matt equals nhom.Matt
                                 //join dm in _db.GiaHhDvkDm on ct.Mahhdv equals dm.Mahhdv 
                                 join dm in _db.GiaHhDvkDm on new { ct.Mahhdv, nhom.Matt } equals new { dm.Mahhdv, dm.Matt }
                                 join dv in _db.DsDonVi.Where(t => list_madv.Contains(t.MaDv)) on kk.Madv equals dv.MaDv
                                 select new GiaHhDvkCt
                                 {
                                     Id = ct.Id,
                                     Mahs = ct.Mahs,
                                     Mahhdv = ct.Mahhdv,
                                     Gialk = ct.Gialk,
                                     Gia = ct.Gia,
                                     Ghichu = ct.Ghichu,
                                     Manhom = ct.Manhom,
                                     Tenhhdv = dm.Tenhhdv,
                                     Dacdiemkt = dm.Dacdiemkt,
                                     Dvt = dm.Dvt,
                                     Madv = kk.Madv,
                                     Matt = kk.Matt,
                                     Thoidiem = kk.Thoidiem,
                                     Tendv = dv.TenDv,
                                     Tentt = nhom.Tentt,
                                     Trangthai = kk.Trangthai
                                 });

                    model = model.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && t.Trangthai == "HT");

                    if (Matt != "all") { model = model.Where(t => t.Matt == Matt); }

                    if (MaHsTongHop != "all") { model = model.Where(t => t.Mahs == MaHsTongHop); }

                    List<string> list_madv_hs = model.Select(t => t.Madv).ToList();

                    var model_donvi_hs = _db.DsDonVi.Where(t => list_madv_hs.Contains(t.MaDv));

                    List<string> list_mahs = model.Select(t => t.Mahs).ToList();
                    var model_hoso = _db.GiaHhDvk.Where(t => list_mahs.Contains(t.Mahs));

                    ViewData["DonVis"] = model_donvi_hs;
                    ViewData["ChiTietHs"] = model_hoso;
                    ViewData["NgayTu"] = ngaytu;
                    ViewData["NgayDen"] = ngayden;
                    ViewData["DmDvt"] = _db.DmDvt.ToList();
                    ViewData["HoTenNguoiKy"] = hotennguoiky;
                    ViewData["ChucDanhNguoiKy"] = chucdanhky;
                    ViewData["Title"] = "Báo cáo giá sản phẩm dịch vụ khung giá";
                    //Định danh
                    var today = DateTime.Now;
                    ViewData["NgayTaoBaoCao"] = $"Ngày {today.Day} Tháng {today.Month} Năm {today.Year}";
                    var Madv = Helpers.GetSsAdmin(HttpContext.Session, "Madv");
                    var donVi = _db.Users.FirstOrDefault(x => x.Madv == Madv);
                    if (donVi != null)
                    {
                        ViewData["DinhDanh"] = donVi.Name;
                    }
                    else
                    {
                        ViewData["DinhDanh"] = "Lỗi";
                    }
                    //End Định danh
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

        [HttpPost("GiaHhDvk/BaoCao/GetListHoSo")]
        public JsonResult GetListHoSo(DateTime ngaytu, DateTime ngayden)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.GiaHhDvk.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && t.Trangthai == "HT");
                string result = "<select class='form-control' id='MaHsTongHop' name='MaHsTongHop'>";
                result += "<option value='all'>--Tất cả---</option>";

                if (model.Any())
                {
                    foreach (var item in model)
                    {
                        result += "<option value='" + @item.Mahs + "'>Số QĐ: " + @item.Soqd + " - Thời điểm: " + @Helpers.ConvertDateToStr(item.Thoidiem) + "</option>";
                    }
                }
                result += "</select>";
                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Phiên đăng nhập kết thúc, Bạn cần đăng nhập lại!!!" };
                return Json(data);
            }
        }

    }
}
