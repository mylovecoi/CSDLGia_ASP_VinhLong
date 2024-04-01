using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Manages.ThamDinhGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.ThamDinhGia
{
    public class ThamDinhGiaBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public ThamDinhGiaBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("ThamDinhGia/BaoCao")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.bc", "Index"))
                {
                    DateTime nowDate = DateTime.Now;
                    DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
                    DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);
                    ViewData["ngaytu"] = firstDayCurrentYear.ToString("yyyy-MM-dd");
                    ViewData["ngayden"] = lastDayCurrentYear.ToString("yyyy-MM-dd");

                    ViewData["Dsdiaban"] = _db.DsDiaBan;
                    ViewData["Dsdonvi"] = _db.DsDonVi.Where(t => t.ChucNang == "TONGHOP");
                    ViewData["Title"] = "Báo cáo tổng hợp tài sản thẩm định giá";
                    ViewData["MenuLv1"] = "menu_tdg";
                    ViewData["MenuLv2"] = "menu_tdg_bc";
                    ViewData["DanhSachHoSo"] = _db.ThamDinhGia.Where(t => t.Thoidiem >= firstDayCurrentYear && t.Thoidiem <= lastDayCurrentYear && t.Trangthai == "HT");
                    return View("Views/Admin/Manages/ThamDinhGia/BaoCao/Index.cshtml");
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

        [Route("ThamDinhGia/BaoCao/Bc1")]
        [HttpPost]
        public IActionResult Bc1(string madv, DateTime tungay, DateTime denngay)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.bc", "Index"))
                {
                    var model = (from tdgct in _db.ThamDinhGiaCt
                                 join tdg in _db.ThamDinhGia on tdgct.Mahs equals tdg.Mahs
                                 select new ThamDinhGiaCt
                                 {
                                     Id = tdgct.Id,
                                     Mahs = tdgct.Mahs,
                                     Madv = tdg.Madv,
                                     Tents = tdgct.Tents,
                                     Dacdiempl = tdgct.Dacdiempl,
                                     Thongsokt = tdgct.Thongsokt,
                                     Diadiem = tdg.Diadiem,
                                     Thoidiem = tdg.Thoidiem,
                                     Ppthamdinh = tdg.Ppthamdinh,
                                     Mucdich = tdg.Mucdich,
                                     Dvyeucau = tdg.Dvyeucau,
                                     Giatritstd = tdgct.Giatritstd,
                                     Thoihan = tdg.Thoihan,
                                     Ghichu = tdgct.Ghichu,
                                 });

                    /*return Ok(madv);*/

                    if (madv != "all")
                    {
                        model = model.Where(t => t.Madv == madv);
                    }

                    /*return Ok(model);*/

                    if (tungay.ToString("yyMMdd") != "010101")
                    {
                        model = model.Where(t => t.Thoidiem >= tungay);
                    }

                    if (denngay.ToString("yyMMdd") != "010101")
                    {
                        model = model.Where(t => t.Thoidiem <= denngay);
                    }

                    ViewData["Madv"] = madv;
                    ViewData["Tungay"] = tungay;
                    ViewData["Denngay"] = denngay;
                    ViewData["Title"] = "Báo cáo tổng hợp tài sản thẩm định giá";
                    ViewData["MenuLv1"] = "menu_tdg";
                    ViewData["MenuLv2"] = "menu_tdg_bc";
                    return View("Views/Admin/Manages/ThamDinhGia/BaoCao/Bc1.cshtml", model);
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



        [Route("ThamDinhGia/BaoCao/BcTH")]
        [HttpPost]
        public IActionResult BcTH(DateTime tungay, DateTime denngay, string chucdanhky, string hotennguoiky, string MaNhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.bc", "Index"))
                {

                    var model = (from hoso in _db.ThamDinhGia.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT")
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.ThamDinhGia.ThamDinhGia
                                 {
                                     TenDonVi = donvi.TenDv,
                                     Mahs = hoso.Mahs,
                                     Soqdpheduyet = hoso.Soqdpheduyet,
                                     Thoidiem = hoso.Thoidiem,
                                 });

                    ViewData["Title"] = "Báo cáo thẩm định giá";
                    ViewData["MenuLv1"] = "menu_tdg";
                    ViewData["MenuLv2"] = "menu_tdg_bc";
                    ViewData["NgayTu"] = tungay;
                    ViewData["NgayDen"] = denngay;
                    ViewData["ChucDanhNguoiKy"] = chucdanhky;
                    ViewData["HoTenNguoiKy"] = hotennguoiky;
                    return View("Views/Admin/Manages/ThamDinhGia/BaoCao/BcTH.cshtml", model);
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

        [Route("ThamDinhGia/BaoCao/BcCT")]
        [HttpPost]
        public IActionResult BcCT(DateTime? ngaytu, DateTime? ngayden, string MaHsTongHop, string chucdanhky, string hotennguoiky, string MaNhom, string PhanLoaiHoSo)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.bc", "Index"))
                {

                    var model = (from tdgct in _db.ThamDinhGiaCt
                                 join tdg in _db.ThamDinhGia on tdgct.Mahs equals tdg.Mahs
                                 join dv in _db.DsDonVi on tdg.Madv equals dv.MaDv
                                 select new ThamDinhGiaCt
                                 {
                                     Id = tdgct.Id,
                                     Mahs = tdgct.Mahs,
                                     Tents = tdgct.Tents,
                                     Sl = tdgct.Sl,
                                     Giadenghi = tdgct.Giadenghi,
                                     Giatritstd = tdgct.Giatritstd,
                                     Madv = tdg.Madv,
                                     Thoidiem = tdg.Thoidiem,
                                     Tendv = dv.TenDv,
                                     Tttstd = tdg.Tttstd,
                                     Dvyeucau = tdg.Dvyeucau,
                                     Dvthamdinh = tdg.Dvthamdinh,
                                     Trangthai = tdg.Trangthai,
                                 });

                    model = model.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && t.Trangthai == "HT");
                    //if (MaNhom != "all") { model = model.Where(t => t.Manhom == MaNhom); }
                    if (MaHsTongHop != "all") { model = model.Where(t => t.Mahs == MaHsTongHop);}
                    List<string> list_madv = model.Select(t => t.Madv).ToList();
                    var model_donvi = _db.DsDonVi.Where(t => list_madv.Contains(t.MaDv));

                    List<string> list_mahs = model.Select(t => t.Mahs).ToList();
                    var model_hoso = _db.ThamDinhGia.Where(t => list_mahs.Contains(t.Mahs));

                    ViewData["DonVis"] = model_donvi;
                    ViewData["ChiTietHs"] = model_hoso;
                    ViewData["NgayTu"] = ngaytu;
                    ViewData["NgayDen"] = ngayden;

                    ViewData["HoTenNguoiKy"] = hotennguoiky;
                    ViewData["ChucDanhNguoiKy"] = chucdanhky;
                    ViewData["Title"] = "Báo cáo thẩm định giá";
                    return View("Views/Admin/Manages/ThamDinhGia/BaoCao/BcCT.cshtml", model);
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

        [HttpPost("ThamDinhGia/BaoCao/GetListHoSo")]
        public JsonResult GetListHoSo(DateTime ngaytu, DateTime ngayden)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.ThamDinhGia.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && t.Trangthai == "HT");
                string result = "<select class='form-control' id='mahs' name='mahs'>";
                result += "<option value='all'>--Tất cả---</option>";

                if (model.Any())
                {
                    foreach (var item in model)
                    {
                        result += "<option value='" + @item.Mahs + "'>Ký hiệu văn bản: " + @item.Soqdpheduyet + " - Thời điểm: " + @Helpers.ConvertDateToStr(item.Thoidiem) + "</option>";
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
