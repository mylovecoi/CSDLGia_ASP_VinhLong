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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaTrungThauHhDv
{
    public class GiaTrungThauHhDvBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaTrungThauHhDvBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaTrungThauHhDv/BaoCao")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.muataisan.baocao", "Index"))
                {
                    DateTime nowDate = DateTime.Now;
                    DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
                    DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);
                    ViewData["ngaytu"] = firstDayCurrentYear.ToString("yyyy-MM-dd");
                    ViewData["ngayden"] = lastDayCurrentYear.ToString("yyyy-MM-dd");

                    ViewData["Title"] = "Báo cáo giá trúng thầu hàng hóa được mua sắm theo quy định của pháp luật về đấu thầu";
                    ViewData["MenuLv1"] = "menu_mts";
                    ViewData["MenuLv2"] = "menu_giamts_bc";
                    ViewData["DanhSachHoSo"] = _db.GiaMuaTaiSan.Where(t => t.Thoidiem >= firstDayCurrentYear && t.Thoidiem <= lastDayCurrentYear && t.Trangthai == "HT");
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauHhDv/BaoCao/Index.cshtml");
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

        [Route("GiaTrungThauHhDv/BaoCao/BcTH")]
        [HttpPost]
        public IActionResult BcTH(DateTime tungay, DateTime denngay, string chucdanhky, string hotennguoiky)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.muataisan.baocao", "Index"))
                {

                    var model = (from hoso in _db.GiaMuaTaiSan.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT")
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaMuaTaiSan
                                 {
                                     TenDonVi = donvi.TenDv,
                                     Mahs = hoso.Mahs,
                                     Soqd = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                     Trangthai = hoso.Trangthai,
                                     Thongtinqd = hoso.Thongtinqd,
                });

                    ViewData["Title"] = "Báo cáo giá trúng thầu hàng hóa được mua sắm theo quy định của pháp luật về đấu thầu";
                    ViewData["MenuLv1"] = "menu_mts";
                    ViewData["MenuLv2"] = "menu_giamts_bc";
                    ViewData["tungay"] = tungay;
                    ViewData["denngay"] = denngay;
                    ViewData["ChucDanhNguoiKy"] = chucdanhky;
                    ViewData["HoTenNguoiKy"] = hotennguoiky;
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauHhDv/BaoCao/BcTH.cshtml", model);
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

        [Route("GiaTrungThauHhDv/BaoCao/BcCT")]
        [HttpPost]
        public IActionResult BcCT(DateTime? ngaytu, DateTime? ngayden, string MaHsTongHop, string chucdanhky, string hotennguoiky)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.lephi.baocao", "Index"))
                {
                    DateTime nowDate = DateTime.Now;
                    DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
                    DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);
                    ngaytu = ngaytu.HasValue ? ngaytu : firstDayCurrentYear;
                    ngayden = ngayden.HasValue ? ngayden : lastDayCurrentYear;

                     var model = (from chitiet in _db.GiaMuaTaiSanCt
                                 join hoso in _db.GiaMuaTaiSan on chitiet.Mahs equals hoso.Mahs
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new GiaMuaTaiSanCt
                                 {
                                     Id = chitiet.Id,
                                     Dvt = chitiet.Dvt,
                                     Mahs = chitiet.Mahs,
                                     Madv = hoso.Madv,
                                     Thoidiem = hoso.Thoidiem,                                    
                                     Soqd = hoso.Soqd,
                                     DonGia = chitiet.DonGia,
                                     KhoiLuong = chitiet.KhoiLuong,
                                     Thongtinqd = hoso.Thongtinqd,
                                     Mota = chitiet.Mota,
                                     Madiaban = hoso.Madiaban,
                                     Tennhathau = hoso.Tennhathau,
                                     TrangThai = hoso.Trangthai,
                                    
                                 });

                    model = model.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && t.TrangThai == "HT");

                    //if (MaNhom != "all") { model = model.Where(t => t.Manhom == MaNhom); }
                    if (MaHsTongHop != "all") { model = model.Where(t => t.Mahs == MaHsTongHop); }
                   

                    List<string> list_madv = model.Select(t => t.Madv).ToList();

                    var model_donvi = _db.DsDonVi.Where(t => list_madv.Contains(t.MaDv));

                    List<string> list_mahs = model.Select(t => t.Mahs).ToList();
                    var model_hoso = _db.GiaMuaTaiSan.Where(t => list_mahs.Contains(t.Mahs));

                    ViewData["DonVis"] = model_donvi;
                    ViewData["ChiTietHs"] = model_hoso;
                    ViewData["NgayTu"] = ngaytu;
                    ViewData["NgayDen"] = ngayden;

                    ViewData["HoTenNguoiKy"] = hotennguoiky;
                    ViewData["ChucDanhNguoiKy"] = chucdanhky;
                    ViewData["Title"] = "Báo cáo giá trúng thầu hàng hóa được mua sắm theo quy định của pháp luật về đấu thầu";
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauHhDv/BaoCao/BcCT.cshtml", model);
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
