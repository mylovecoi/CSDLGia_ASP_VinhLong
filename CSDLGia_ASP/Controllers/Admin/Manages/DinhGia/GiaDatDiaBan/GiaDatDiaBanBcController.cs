
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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDatDiaBan
{
    public class GiaDatDiaBanBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaDatDiaBanBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaDatDiaBan/BaoCao")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giadatdb.baocao", "Index"))
                {
                    DateTime nowDate = DateTime.Now;
                    DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
                    DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);
                    ViewData["ngaytu"] = firstDayCurrentYear.ToString("yyyy-MM-dd");
                    ViewData["ngayden"] = lastDayCurrentYear.ToString("yyyy-MM-dd");

                    ViewData["DanhSachLoaiDat"] = _db.DmLoaiDat;
                    ViewData["Title"] = "Báo cáo giá đất địa bàn";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_giadatdiaban";
                    ViewData["MenuLv3"] = "menu_giadatdiaban_bc";
                    ViewData["DanhSachHoSo"] = _db.GiaDatDiaBan.Where(t => t.Thoidiem >= firstDayCurrentYear && t.Thoidiem <= lastDayCurrentYear && t.Trangthai == "HT");
                    return View("Views/Admin/Manages/DinhGia/GiaDatDiaBan/BaoCao/Index.cshtml");
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

        [Route("GiaDatDiaBan/BaoCao/BcTH")]
        [HttpPost]
        public IActionResult BcTH(DateTime tungay, DateTime denngay, string chucdanhky, string hotennguoiky)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giadatdb.baocao", "Index"))
                {

                    var model = (from hoso in _db.GiaDatDiaBan.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT")
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaDatDiaBan
                                 {
                                     TenDonVi = hoso.TenDonVi,
                                     Mahs = hoso.Mahs,
                                     Soqd = hoso.Soqd,
                                     GhiChu = hoso.GhiChu,
                                     Thoidiem = hoso.Thoidiem,
                                 });

                    ViewData["Title"] = "Báo cáo giá đất địa bàn";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_giadatdiaban";
                    ViewData["MenuLv3"] = "menu_giadatdiaban_bc";
                    ViewData["tungay"] = tungay;
                    ViewData["denngay"] = denngay;
                    ViewData["ChucDanhNguoiKy"] = chucdanhky;
                    ViewData["HoTenNguoiKy"] = hotennguoiky;
                    return View("Views/Admin/Manages/DinhGia/GiaDatDiaBan/BaoCao/BcTH.cshtml", model);
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

        [Route("GiaDatDiaBan/BaoCao/BcCT")]
        [HttpPost]
        public IActionResult BcCT(DateTime? ngaytu, DateTime? ngayden, string MaHsTongHop, string chucdanhky, string hotennguoiky, string MaloaidatTongHop)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giadatdb.baocao", "Index"))
                {

                    var model = from dgct in _db.GiaDatDiaBanCt
                                join dg in _db.GiaDatDiaBan on dgct.Mahs equals dg.Mahs
                                join dm in _db.DmLoaiDat on dgct.Maloaidat equals dm.Maloaidat
                                join diaban in _db.DsDiaBan on dg.Madiaban equals diaban.MaDiaBan
                                select new CSDLGia_ASP.Models.Manages.DinhGia.GiaDatDiaBanCt
                                {
                                    Id = dg.Id,
                                    Mahs = dg.Mahs,
                                    Soqd = dg.Soqd,
                                    Loaidat = dm.Loaidat,
                                    Mota = dgct.Mota,
                                    Diemdau = dgct.Diemdau,
                                    Diemcuoi = dgct.Diemcuoi,
                                    Loaiduong = dgct.Loaiduong,
                                    Thoidiem = dg.Thoidiem,
                                    Hesok = dgct.Hesok,
                                    MaDv = dgct.MaDv,
                                    Giavt1 = dgct.Giavt1,
                                    Giavt2 = dgct.Giavt2,
                                    Giavt3 = dgct.Giavt3,
                                    Giavt4 = dgct.Giavt4,
                                    Giavt5 = dgct.Giavt5,
                                    Trangthai = dg.Trangthai,
                                    Madiaban = dg.Madiaban,
                                    TenDiaBan = diaban.TenDiaBan,
                                    Maloaidat = dgct.Maloaidat

                                };

                    model = model.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && t.Trangthai == "HT");

                    if (MaHsTongHop != "all") { model = model.Where(t => t.Mahs == MaHsTongHop); }
                    if (MaloaidatTongHop != "all") { model = model.Where(t => t.Maloaidat == MaloaidatTongHop); }

                    List<string> list_madv = model.Select(t => t.MaDv).ToList();
                    var model_donvi = _db.DsDonVi.Where(t => list_madv.Contains(t.MaDv));

                    List<string> list_mahs = model.Select(t => t.Mahs).ToList();
                    var model_hoso = _db.GiaDatDiaBan.Where(t => list_mahs.Contains(t.Mahs));
                    ViewData["DonVis"] = model_donvi;
                    ViewData["ChiTietHs"] = model_hoso;
                    ViewData["NgayTu"] = ngaytu;
                    ViewData["NgayDen"] = ngayden;

                    ViewData["HoTenNguoiKy"] = hotennguoiky;
                    ViewData["ChucDanhNguoiKy"] = chucdanhky;
                    ViewData["Title"] = "Báo cáo giá đất địa bàn";
                    return View("Views/Admin/Manages/DinhGia/GiaDatDiaBan/BaoCao/BcCT.cshtml", model);
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

        [HttpPost("GiaDatDiaBan/BaoCao/GetListHoSo")]
        public JsonResult GetListHoSo(DateTime ngaytu, DateTime ngayden)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.GiaDatDiaBan.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && t.Trangthai == "HT");
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
