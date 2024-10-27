using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDatCuThe
{
    public class GiaDatCuTheVlBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaDatCuTheVlBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaDatCuTheVl/BaoCao")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthevinhlong.baocao", "Index"))
                {
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    DateTime nowDate = DateTime.Now;
                    DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
                    DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);
                    ViewData["ngaytu"] = firstDayCurrentYear.ToString("yyyy-MM-dd");
                    ViewData["ngayden"] = lastDayCurrentYear.ToString("yyyy-MM-dd");
                    ViewData["DanhSachHoSo"] = _db.GiaDatCuTheVl.Where(t => t.Thoidiem >= firstDayCurrentYear && t.Thoidiem <= lastDayCurrentYear && list_trangthai.Contains(t.Trangthai));
                    ViewData["DanhMucPp"] = _db.GiaDatCuTheVlDmPPDGDat;
                    ViewData["Title"] = "Báo cáo giá đất cụ thể";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgdctvl";
                    ViewData["MenuLv3"] = "menu_dgdctvl_bc";
                    
                    return View("Views/Admin/Manages/DinhGia/GiaDatCuTheVl/BaoCao/Index.cshtml");
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

        [Route("GiaDatCuTheVl/BaoCao/BcTH")]
        [HttpPost]
        public IActionResult BcTH(DateTime tungay, DateTime denngay, string chucdanhky, string hotennguoiky)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthevinhlong.baocao", "Index"))
                {
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    var model = (from hoso in _db.GiaDatCuTheVl.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay
                                            && list_trangthai.Contains(t.Trangthai))
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaDatCuTheVl
                                 {
                                     TenDonVi = donvi.TenDv,
                                     Mahs = hoso.Mahs,
                                     Soqd = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                     Ghichu = hoso.Ghichu,
                                 });

                    ViewData["Title"] = "Báo cáo giá đất cụ thể";
                    ViewData["NgayTu"] = tungay;
                    ViewData["NgayDen"] = denngay;
                    ViewData["ChucDanhNguoiKy"] = chucdanhky;
                    ViewData["HoTenNguoiKy"] = hotennguoiky;
                    return View("Views/Admin/Manages/DinhGia/GiaDatCuTheVl/BaoCao/BcTH.cshtml", model);
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

        [Route("GiaDatCuTheVl/BaoCao/BcCT")]
        [HttpPost]
        public IActionResult BcCT(DateTime? ngaytu, DateTime? ngayden, string MaHsTongHop, string chucdanhky, string hotennguoiky, string Mapp)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthevinhlong.baocao", "Index"))
                {
                    DateTime nowDate = DateTime.Now;
                    DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
                    DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);
                    Mapp = string.IsNullOrEmpty(Mapp) ? "all" : Mapp;
                    ngaytu = ngaytu.HasValue ? ngaytu : firstDayCurrentYear;
                    ngayden = ngayden.HasValue ? ngayden : lastDayCurrentYear;

                    var model = (from hosoct in _db.GiaDatCuTheVlCt
                                 join hoso in _db.GiaDatCuTheVl on hosoct.Mahs equals hoso.Mahs
                                 join dm in _db.GiaDatCuTheVlDmPPDGDat on hosoct.Mapp equals dm.Mapp
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaDatCuTheVlCt
                                 {
                                     Madv = hoso.Madv,
                                     Tendv = donvi.TenDv,
                                     Tenpp = dm.Tenpp,
                                     Soqd = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                     Noidungcv = hosoct.Noidungcv,
                                     ChiPhiNhanCong = hosoct.ChiPhiNhanCong,
                                     ChiPhiDungCu = hosoct.ChiPhiDungCu,
                                     ChiPhiNangLuong = hosoct.ChiPhiNangLuong,
                                     ChiPhiKhauHao = hosoct.ChiPhiKhauHao,
                                     ChiPhiVatLieu = hosoct.ChiPhiVatLieu,
                                     ChiPhiTrucTiepKkh = hosoct.ChiPhiTrucTiepKkh,
                                     ChiPhiTrucTiepCkh = hosoct.ChiPhiTrucTiepCkh,
                                     ChiPhiQlChungKkh = hosoct.ChiPhiQlChungKkh,
                                     ChiPhiQlChungCkh = hosoct.ChiPhiQlChungCkh,
                                     DonGiaKkh = hosoct.DonGiaKkh,
                                     DonGiaCkh = hosoct.DonGiaCkh,
                                     Mapp = hosoct.Mapp,
                                     Trangthai = hoso.Trangthai,
                                     Mahs = hoso.Mahs,
                                     STTSapXep = hosoct.STTSapXep,
                                     STTHienThi = hosoct.STTHienThi,
                                 });
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model = model.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && list_trangthai.Contains(t.Trangthai));
                    if (Mapp != "all") { model = model.Where(t => t.Mapp == Mapp); }
                    if (MaHsTongHop != "all") { model = model.Where(t => t.Mahs == MaHsTongHop); }

                    List<string> list_madv = model.Select(t => t.Madv).ToList();
                    var model_donvi = _db.DsDonVi.Where(t => list_madv.Contains(t.MaDv));

                    List<string> list_mahs = model.Select(t => t.Mahs).ToList();
                    var model_hoso = _db.GiaDatCuTheVl.Where(t => list_mahs.Contains(t.Mahs));

                    ViewData["DonVis"] = model_donvi;
                    ViewData["ChiTietHs"] = model_hoso;
                    ViewData["NgayTu"] = ngaytu;
                    ViewData["NgayDen"] = ngayden;
                    ViewData["DanhMucPp"] = _db.GiaDatCuTheVlDmPPDGDat;
                    ViewData["HoTenNguoiKy"] = hotennguoiky;
                    ViewData["ChucDanhNguoiKy"] = chucdanhky;
                    ViewData["Title"] = "Báo cáo giá đất cụ thể";
                    return View("Views/Admin/Manages/DinhGia/GiaDatCuTheVl/BaoCao/BcCT.cshtml", model);
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

        [HttpPost("GiaDatCuTheVl/BaoCao/GetListHoSo")]
        public JsonResult GetListHoSo(DateTime ngaytu, DateTime ngayden)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                var model = _db.GiaDatCuTheVl.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && list_trangthai.Contains(t.Trangthai));
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
