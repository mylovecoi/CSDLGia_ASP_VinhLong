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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaThueDN
{
    public class GiaThueDNBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaThueDNBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaThueDNBc/BaoCao")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.baocao", "Index"))
                {
                    DateTime nowDate = DateTime.Now;
                    DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
                    DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);
                    ViewData["ngaytu"] = firstDayCurrentYear.ToString("yyyy-MM-dd");
                    ViewData["ngayden"] = lastDayCurrentYear.ToString("yyyy-MM-dd");

                    ViewData["Title"] = "Báo cáo giá thuê mặt đất mặt nước";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmdmn";
                    ViewData["MenuLv3"] = "menu_dgtmdmn_bc";
                    ViewData["DanhSachHoSo"] = _db.GiaThueMatDatMatNuoc.Where(t => t.Thoidiem >= firstDayCurrentYear && t.Thoidiem <= lastDayCurrentYear && t.Trangthai == "HT");
                    ViewData["DanhSachNhom"] = _db.GiaThueMatDatMatNuocNhom;
                    return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/BaoCao/Index.cshtml");
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

        [Route("GiaThueDNBc/BaoCao/BcTH")]
        [HttpPost]
        public IActionResult BcTH(DateTime tungay, DateTime denngay, string chucdanhky, string hotennguoiky)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.baocao", "Index"))
                {

                    var model = (from hoso in _db.GiaThueMatDatMatNuoc.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT")
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv    
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMatDatMatNuoc
                                 {
                                     TenDonVi = donvi.TenDv,
                                     Mahs = hoso.Mahs,
                                     Soqd = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                     Ghichu = hoso.Ghichu,
                                 });

                    ViewData["Title"] = "Báo cáo giá thuê mặt đất mặt nước";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmdmn";
                    ViewData["MenuLv3"] = "menu_dgtmdmn_bc";
                    ViewData["NgayTu"] = tungay;
                    ViewData["NgayDen"] = denngay;
                    ViewData["ChucDanhNguoiKy"] = chucdanhky;
                    ViewData["HoTenNguoiKy"] = hotennguoiky;
                    return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/BaoCao/BcTH.cshtml", model);
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

        [Route("GiaThueDNBc/BaoCao/BcCT")]
        [HttpPost]
        public IActionResult BcCT(DateTime? ngaytu, DateTime? ngayden, string MaHsTongHop, string chucdanhky, string hotennguoiky, string MaNhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.baocao", "Index"))
                {
                    DateTime nowDate = DateTime.Now;
                    DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
                    DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);
                    MaNhom = string.IsNullOrEmpty(MaNhom) ? "all" : MaNhom;
                    ngaytu = ngaytu.HasValue ? ngaytu : firstDayCurrentYear;
                    ngayden = ngayden.HasValue ? ngayden : lastDayCurrentYear;
                  
                    var model = (from hosoct in _db.GiaThueMatDatMatNuocCt
                                 join hoso in _db.GiaThueMatDatMatNuoc on hosoct.Mahs equals hoso.Mahs
                                 join nhom in _db.GiaThueMatDatMatNuocNhom on hosoct.MaNhom equals nhom.Manhom
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMatDatMatNuocCt
                                 {
                                     Madv = hoso.Madv,
                                     Tendv = donvi.TenDv,
                                     Tennhom = nhom.Tennhom,
                                     SoQD = hoso.Soqd,
                                     ThoiDiem = hoso.Thoidiem,
                                     LoaiDat = hosoct.LoaiDat,
                                     TyLe1 = hosoct.TyLe1,
                                     TyLe2 = hosoct.TyLe2,
                                     TyLe3 = hosoct.TyLe3,
                                     Dongia1 = hosoct.Dongia1,
                                     MaNhom = hosoct.MaNhom,
                                     Trangthai = hoso.Trangthai,
                                     Mahs = hoso.Mahs
                                 });

                    model = model.Where(t => t.ThoiDiem >= ngaytu && t.ThoiDiem <= ngayden && t.Trangthai == "HT");
                    if (MaNhom != "all") { model = model.Where(t => t.MaNhom == MaNhom); }
                    if (MaHsTongHop != "all") {model = model.Where(t => t.Mahs == MaHsTongHop); }

                    List<string> list_madv = model.Select(t => t.Madv).ToList();
                    var model_donvi = _db.DsDonVi.Where(t => list_madv.Contains(t.MaDv));

                    List<string> list_mahs = model.Select(t => t.Mahs).ToList();
                    var model_hoso = _db.GiaThueMatDatMatNuoc.Where(t => list_mahs.Contains(t.Mahs));

                    ViewData["DonVis"] = model_donvi;
                    ViewData["ChiTietHs"] = model_hoso;
                    ViewData["NgayTu"] = ngaytu;
                    ViewData["NgayDen"] = ngayden;
                    ViewData["DmNhom"] = _db.GiaThueMatDatMatNuocNhom;
                    ViewData["HoTenNguoiKy"] = hotennguoiky;
                    ViewData["ChucDanhNguoiKy"] = chucdanhky;
                    ViewData["Title"] = "Báo cáo giá thuê mặt đất mặt nước";
                    return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/BaoCao/BcCT.cshtml", model);
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

        [HttpPost("GiaThueDNBc/BaoCao/GetListHoSo")]
        public JsonResult GetListHoSo(DateTime ngaytu, DateTime ngayden)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.GiaThueMatDatMatNuoc.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && t.Trangthai == "HT");
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
