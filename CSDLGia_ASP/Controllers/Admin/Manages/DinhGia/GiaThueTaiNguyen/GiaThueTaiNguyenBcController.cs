﻿using Microsoft.AspNetCore.Mvc;
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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaThueTaiNguyen
{
    public class GiaThueTaiNguyenBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaThueTaiNguyenBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaThueTaiNguyenBc/BaoCao")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetn.baocao", "Index"))
                {
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    DateTime nowDate = DateTime.Now;
                    DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
                    DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);
                    ViewData["ngaytu"] = firstDayCurrentYear.ToString("yyyy-MM-dd");
                    ViewData["ngayden"] = lastDayCurrentYear.ToString("yyyy-MM-dd");

                    ViewData["Title"] = "Báo cáo giá thuế tài nguyên";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgthuetn";
                    ViewData["MenuLv3"] = "menu_dgthuetn_bc";
                    ViewData["DanhSachHoSo"] = _db.GiaThueTaiNguyen.Where(t => t.Thoidiem >= firstDayCurrentYear && t.Thoidiem <= lastDayCurrentYear 
                                                                                && list_trangthai.Contains(t.Trangthai));
                    return View("Views/Admin/Manages/DinhGia/GiaThueTaiNguyen/BaoCao/Index.cshtml");
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

        [Route("GiaThueTaiNguyenBc/BaoCao/BcTH")]
        [HttpPost]
        public IActionResult BcTH(DateTime tungay, DateTime denngay, string chucdanhky, string hotennguoiky, string MaNhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetn.baocao", "Index"))
                {
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    var model = (from hoso in _db.GiaThueTaiNguyen.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && list_trangthai.Contains(t.Trangthai))
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueTaiNguyen
                                 {
                                     TenDonVi = donvi.TenDv,
                                     Mahs = hoso.Mahs,
                                     Soqd = hoso.Soqd,
                                     Ghichu = hoso.Ghichu,
                                     Thoidiem = hoso.Thoidiem,
                                 });
                    ViewData["Title"] = "Báo cáo giá thuế tài nguyên";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgthuetn";
                    ViewData["MenuLv3"] = "menu_dgthuetn_bc";
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
                    return View("Views/Admin/Manages/DinhGia/GiaThueTaiNguyen/BaoCao/BcTH.cshtml", model);
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

        [Route("GiaThueTaiNguyenBc/BaoCao/BcCT")]
        [HttpPost]
        public IActionResult BcCT(DateTime? ngaytu, DateTime? ngayden, string MaHsTongHop, string chucdanhky, string hotennguoiky, string MaNhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetn.baocao", "Index"))
                {
                    DateTime nowDate = DateTime.Now;
                    DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
                    DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);
                    MaNhom = string.IsNullOrEmpty(MaNhom) ? "all" : MaNhom;
                    ngaytu = ngaytu.HasValue ? ngaytu : firstDayCurrentYear;
                    ngayden = ngayden.HasValue ? ngayden : lastDayCurrentYear;

                    var model = (from hosoct in _db.GiaThueTaiNguyenCt
                                 join hoso in _db.GiaThueTaiNguyen on hosoct.Mahs equals hoso.Mahs
                                 join nhom in _db.GiaThueTaiNguyenNhom on hosoct.Manhom equals nhom.Manhom
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueTaiNguyenCt
                                 {
                                     Madv = hoso.Madv,
                                     Tendv = donvi.TenDv,
                                     Tennhom = nhom.Tennhom,
                                     SoQD = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                     Cap1 = hosoct.Cap1,
                                     Cap2 = hosoct.Cap2,
                                     Cap3 = hosoct.Cap3,
                                     Cap4 = hosoct.Cap4,
                                     Cap5 = hosoct.Cap5,
                                     Cap6 = hosoct.Cap6,
                                     Ten = hosoct.Ten,
                                     Dvt = hosoct.Dvt,
                                     Gia = hosoct.Gia,
                                     Manhom = hosoct.Manhom,
                                     Trangthai = hoso.Trangthai,
                                     Mahs = hoso.Mahs
                                 });

                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model = model.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && list_trangthai.Contains(t.Trangthai));

                    if (MaHsTongHop != "all") { model = model.Where(t => t.Mahs == MaHsTongHop); }

                    List<string> list_madv = model.Select(t => t.Madv).ToList();
                    var model_donvi = _db.DsDonVi.Where(t => list_madv.Contains(t.MaDv));

                    List<string> list_mahs = model.Select(t => t.Mahs).ToList();
                    var model_hoso = _db.GiaThueTaiNguyen.Where(t => list_mahs.Contains(t.Mahs));

                    ViewData["DonVis"] = model_donvi;
                    ViewData["ChiTietHs"] = model_hoso;
                    ViewData["ngaytu"] = ngaytu;
                    ViewData["ngayden"] = ngayden;
                    ViewData["HoTenNguoiKy"] = hotennguoiky;
                    ViewData["ChucDanhNguoiKy"] = chucdanhky;
                    ViewData["Title"] = "Báo cáo giá thuế tài nguyên";
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
                    return View("Views/Admin/Manages/DinhGia/GiaThueTaiNguyen/BaoCao/BcCT.cshtml", model);
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


        [HttpPost("GiaThueTaiNguyenBc/GetListHoSo")]
        public JsonResult GetListHoSo(DateTime ngaytu, DateTime ngayden)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                var model = _db.GiaThueTaiNguyen.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && list_trangthai.Contains(t.Trangthai));
                string result = "<select class='form-control' id='MaHsTongHop' name='MaHsTongHop'>";
                result += "<option value='all'>--Tất cả---</option>";

                if (model.Any())
                {
                    foreach (var item in model)
                    {
                        result += "<option value='" + @item.Mahs + "'>Ký hiệu văn bản: " + @item.Soqd + " - Thời điểm: " + @Helpers.ConvertDateToStr(item.Thoidiem) + "</option>";
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

