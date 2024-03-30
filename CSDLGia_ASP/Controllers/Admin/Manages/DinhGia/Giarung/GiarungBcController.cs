//using Microsoft.AspNetCore.Mvc;
//using System.Linq;
//using Microsoft.AspNetCore.Http;
//using CSDLGia_ASP.Database;
//using System.Security.Cryptography;
//using CSDLGia_ASP.Helper;
//using CSDLGia_ASP.Models.Manages.KeKhaiGia;
//using CSDLGia_ASP.ViewModels.Systems;
//using CSDLGia_ASP.ViewModels.Manages.KeKhaiGia;
//using Microsoft.AspNetCore.Hosting;
//using System.IO;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using CSDLGia_ASP.ViewModels.Manages.DinhGia;
//using CSDLGia_ASP.Models.Manages.DinhGia;

//namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.Giarung
//{
//    public class GiarungBcController : Controller
//    {

//        private readonly CSDLGiaDBContext _db;

//        public GiarungBcController(CSDLGiaDBContext db)
//        {
//            _db = db;
//        }


//        [Route("GiarungBc/BaoCao")]
//        [HttpGet]
//        public IActionResult Index()
//        {
//            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
//            {
//                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.rung.baocao", "Index"))
//                {
//                    ViewData["Nam"] = DateTime.Now.Year;
//                    ViewData["Title"] = "Báo cáo giá rừng";
//                    ViewData["MenuLv1"] = "menu_dg";
//                    ViewData["MenuLv2"] = "menu_dgr";
//                    ViewData["MenuLv3"] = "menu_dgr_bc";
//                    ViewData["DanhSachHoSo"] = _db.GiaRung.Where(t => t.Thoidiem.Year == DateTime.Now.Year);
//                    return View("Views/Admin/Manages/DinhGia/GiaRung/BaoCao/Index.cshtml");
//                }
//                else
//                {
//                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
//                    return View("Views/Admin/Error/Page.cshtml");
//                }
//            }
//            else
//            {
//                return View("Views/Admin/Error/SessionOut.cshtml");
//            }
//        }


//        [Route("GiarungBc/BaoCao/BcTH")]
//        [HttpPost]
//        public IActionResult BcTH(DateTime tungay, DateTime denngay, string chucdanhky, string hotennguoiky, string MaNhom)
//        {
//            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
//            {
//                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.rung.baocao", "Index"))
//                {

//                    var model = (from hoso in _db.GiaRung.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT")
//                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
//                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaRung
//                                 {
//                                     TenDonVi = donvi.TenDv,
//                                     Mahs = hoso.Mahs,
//                                     Soqd = hoso.Soqd,
//                                 });

//                    ViewData["Title"] = "Báo cáo giá rừng";
//                    ViewData["MenuLv1"] = "menu_dg";
//                    ViewData["MenuLv2"] = "menu_dgr";
//                    ViewData["MenuLv3"] = "menu_dgr_bc";
//                    ViewData["tungay"] = tungay;
//                    ViewData["denngay"] = denngay;
//                    ViewData["ChucDanhNguoiKy"] = chucdanhky;
//                    ViewData["HoTenNguoiKy"] = hotennguoiky;
//                    return View("Views/Admin/Manages/DinhGia/GiaRung/BaoCao/BcTH.cshtml", model);
//                }
//                else
//                {
//                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
//                    return View("Views/Admin/Error/Page.cshtml");
//                }
//            }
//            else
//            {
//                return View("Views/Admin/Error/SessionOut.cshtml");
//            }
//        }


//        [Route("GiaRung/BaoCao/BcCT")]
//        [HttpPost]
//        public IActionResult BcCT(DateTime? ngaytu, DateTime? ngayden, string MaHsTongHop, string chucdanhky, string hotennguoiky)
//        {
//            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
//            {
//                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.rung.baocao", "Index"))
//                {
//                    DateTime nowDate = DateTime.Now;
//                    DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
//                    DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);
//                    ngaytu = ngaytu.HasValue ? ngaytu : firstDayCurrentYear;
//                    ngayden = ngayden.HasValue ? ngayden : lastDayCurrentYear;

//                    var model = (from hosoct in _db.GiaRungCt
//                                 join hoso in _db.GiaRung on hosoct.Mahs equals hoso.Mahs
//                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
//                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaRungCt
//                                 {
//                                     Madv = hoso.Madv,
//                                     Tendv = donvi.TenDv,
//                                     Soqdpd = hoso.Soqd,
//                                     Thoidiem = hoso.Thoidiem,
//                                     LoaiDat = hosoct.LoaiDat,
//                                     TyLe1 = hosoct.TyLe1,
//                                     TyLe2 = hosoct.TyLe2,
//                                     TyLe3 = hosoct.TyLe3,
//                                     Dongia1 = hosoct.Dongia1,
//                                     MaNhom = hosoct.MaNhom,
//                                     Trangthai = hoso.Trangthai,
//                                     Mahs = hoso.Mahs
//                                 });

//                    model = model.Where(t => t.ThoiDiem >= ngaytu && t.ThoiDiem <= ngayden && t.Trangthai == "HT");
//                    if (MaHsTongHop != "all") { model = model.Where(t => t.Mahs == MaHsTongHop); }

//                    List<string> list_madv = model.Select(t => t.Madv).ToList();
//                    var model_donvi = _db.DsDonVi.Where(t => list_madv.Contains(t.MaDv));

//                    List<string> list_mahs = model.Select(t => t.Mahs).ToList();
//                    var model_hoso = _db.GiaRung.Where(t => list_mahs.Contains(t.Mahs));

//                    ViewData["DonVis"] = model_donvi;
//                    ViewData["ChiTietHs"] = model_hoso;

//                    ViewData["HoTenNguoiKy"] = hotennguoiky;
//                    ViewData["ChucDanhNguoiKy"] = chucdanhky;
//                    ViewData["ThoiDiemKX"] = "Từ ngày " + ngaytu + " đến ngày " + ngayden;
//                    ViewData["Title"] = "Báo cáo giá rừng";
//                    return View("Views/Admin/Manages/DinhGia/GiaRung/BaoCao/BcCT.cshtml", model);
//                }
//                else
//                {
//                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
//                    return View("Views/Admin/Error/Page.cshtml");
//                }
//            }
//            else
//            {
//                return View("Views/Admin/Error/SessionOut.cshtml");
//            }
//        }


//    }

//    [Route("BaoCaoDgrung/BaoCao")]
//    [HttpPost]
//    public IActionResult BaoCao(string phanloai, DateTime tungay, DateTime denngay, string Madv, string tenthutruong, string chucvu)
//    {
//        if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
//        {
//            if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.rung.baocao", "Index"))
//            {

//                if (phanloai == "Thuê môi trường")
//                {
//                    var model = from dg in _db.GiaRung.Where(t => t.Madv == Madv)
//                                join ct in _db.GiaRungCt.Where(t => t.Phanloai == "Thuê môi trường") on dg.Mahs equals ct.Mahs
//                                select new CSDLGia_ASP.Models.Manages.DinhGia.GiaRungCt
//                                {
//                                    Thoidiem = dg.Thoidiem,
//                                    Dientich = ct.Dientich,
//                                    Dientichsd = ct.Dientichsd,
//                                    Dvthue = ct.Dvthue,
//                                    Diachi = ct.Diachi,
//                                    Soqdpd = ct.Soqdpd,
//                                    Thoigianpd = ct.Thoigianpd,
//                                    Soqdgkd = ct.Soqdgkd,
//                                    Thoigiangkd = ct.Thoigiangkd,
//                                    Giatri = ct.Giatri,
//                                    Dongia = ct.Dongia,

//                                    Thuetungay = ct.Thuetungay,
//                                    Thuedenngay = ct.Thuedenngay,
//                                };

//                    if (tungay.ToString("yyMMdd") != "010101")
//                    {
//                        model = model.Where(t => t.Thoidiem >= tungay);
//                    }
//                    if (denngay.ToString("yyMMdd") != "010101")
//                    {
//                        model = model.Where(t => t.Thoidiem <= denngay);
//                    }

//                    double sum_dientichSd = 0;
//                    foreach (var item in model)
//                    {
//                        sum_dientichSd += item.Dientichsd;
//                    }
//                    double sum_dientich = 0;
//                    foreach (var item in model)
//                    {
//                        sum_dientich += item.Dientich;
//                    }
//                    double sum_dongia = 0;
//                    foreach (var item in model)
//                    {
//                        sum_dongia += item.Dongia;
//                    }
//                    double sum_giatri = 0;
//                    foreach (var item in model)
//                    {
//                        sum_giatri += item.Giatri;
//                    }

//                    ViewData["tenthutruong"] = tenthutruong;
//                    ViewData["chucvu"] = chucvu;

//                    ViewData["tungay"] = Helpers.ConvertDateToStr(tungay);
//                    ViewData["denngay"] = Helpers.ConvertDateToStr(denngay);
//                    ViewData["sum_dientich"] = sum_dientich;
//                    ViewData["sum_dientichSd"] = sum_dientichSd;
//                    ViewData["sum_giatri"] = sum_giatri;
//                    ViewData["sum_dongia"] = sum_dongia;
//                    ViewData["Title"] = "Báo cáo tổng hợp định giá rừng";
//                    ViewData["MenuLv1"] = "menu_dg";
//                    ViewData["MenuLv2"] = "menu_dgr";
//                    ViewData["MenuLv3"] = "menu_dgr_bc";
//                    return View("Views/Admin/Manages/DinhGia/GiaRung/BaoCao/BcMoiTruong.cshtml", model);
//                }
//                else
//                {
//                    var model = from dg in _db.GiaRung.Where(t => t.Madv == Madv)
//                                join ct in _db.GiaRungCt.Where(t => t.Phanloai == "Thanh lý" || t.Phanloai == "Khai thác") on dg.Mahs equals ct.Mahs
//                                select new CSDLGia_ASP.Models.Manages.DinhGia.GiaRungCt
//                                {
//                                    Thoidiem = dg.Thoidiem,
//                                    Dientich = ct.Dientich,
//                                    Dientichsd = ct.Dientichsd,
//                                    Dvthue = ct.Dvthue,
//                                    Diachi = ct.Diachi,
//                                    Soqdpd = ct.Soqdpd,
//                                    Thoigianpd = ct.Thoigianpd,
//                                    Soqdgkd = ct.Soqdgkd,
//                                    Thoigiangkd = ct.Thoigiangkd,
//                                    Giakhoidiem = ct.Giakhoidiem,
//                                    Giatri = ct.Giatri,
//                                };

//                    if (tungay.ToString("yyMMdd") != "010101")
//                    {
//                        model = model.Where(t => t.Thoidiem >= tungay);
//                    }
//                    if (denngay.ToString("yyMMdd") != "010101")
//                    {
//                        model = model.Where(t => t.Thoidiem <= denngay);
//                    }

//                    double sum_dientichSd = 0;
//                    foreach (var item in model)
//                    {
//                        sum_dientichSd += item.Dientichsd;
//                    }
//                    double sum_giakhoidiem = 0;
//                    foreach (var item in model)
//                    {
//                        sum_giakhoidiem += item.Giakhoidiem;
//                    }
//                    double sum_giatri = 0;
//                    foreach (var item in model)
//                    {
//                        sum_giatri += item.Giatri;
//                    }

//                    ViewData["tenthutruong"] = tenthutruong;
//                    ViewData["chucvu"] = chucvu;
//                    ViewData["tungay"] = Helpers.ConvertDateToStr(tungay);
//                    ViewData["denngay"] = Helpers.ConvertDateToStr(denngay);
//                    ViewData["sum_dientichSd"] = sum_dientichSd;
//                    ViewData["sum_giakhoidiem"] = sum_giakhoidiem;
//                    ViewData["sum_giatri"] = sum_giatri;
//                    ViewData["Title"] = "Báo cáo tổng hợp định giá rừng";
//                    ViewData["MenuLv1"] = "menu_dg";
//                    ViewData["MenuLv2"] = "menu_dgr";
//                    ViewData["MenuLv3"] = "menu_dgr_bc";
//                    return View("Views/Admin/Manages/DinhGia/GiaRung/BaoCao/BcKhaiThacThanhLy.cshtml", model);
//                }
//            }
//            else
//            {
//                ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
//                return View("Views/Admin/Error/Page.cshtml");
//            }
//        }
//        else
//        {
//            return View("Views/Admin/Error/SessionOut.cshtml");
//        }
//    }


//}
//}


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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.Giarung
{
    public class GiarungBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiarungBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiarungBc/BaoCao")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.rung.baocao", "Index"))
                {
                    ViewData["Nam"] = DateTime.Now.Year;
                    ViewData["Title"] = "Báo cáo giá rừng";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgr";
                    ViewData["MenuLv3"] = "menu_dgr_bc";
                    ViewData["DanhSachHoSo"] = _db.GiaRung.Where(t => t.Thoidiem.Year == DateTime.Now.Year);
                    ViewData["DanhSachNhom"] = _db.GiaRungDm;
                    return View("Views/Admin/Manages/DinhGia/GiaRung/BaoCao/Index.cshtml");
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

        [Route("GiarungBc/BaoCao/BcTH")]
        [HttpPost]
        public IActionResult BcTH(DateTime tungay, DateTime denngay, string chucdanhky, string hotennguoiky)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.rung.baocao", "Index"))
                {

                    var model = (from hoso in _db.GiaThueMatDatMatNuoc.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT")
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaRung
                                 {
                                     TenDonVi = donvi.TenDv,
                                     Mahs = hoso.Mahs,
                                     Soqd = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                 });

                    ViewData["Title"] = "Báo cáo giá rừng";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgr";
                    ViewData["MenuLv3"] = "menu_dgr_bc";
                    ViewData["NgayTu"] = tungay;
                    ViewData["NgayDen"] = denngay;
                    ViewData["ChucDanhNguoiKy"] = chucdanhky;
                    ViewData["HoTenNguoiKy"] = hotennguoiky;
                    return View("Views/Admin/Manages/DinhGia/GiaRung/BaoCao/BcTH.cshtml", model);
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

        [Route("GiarungBc/BaoCao/BcCT")]
        [HttpPost]
        public IActionResult BcCT(DateTime? ngaytu, DateTime? ngayden, string MaHsTongHop, string chucdanhky, string hotennguoiky, string MaNhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.rung.baocao", "Index"))
                {
                    DateTime nowDate = DateTime.Now;
                    DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
                    DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);
                    MaNhom = string.IsNullOrEmpty(MaNhom) ? "all" : MaNhom;
                    ngaytu = ngaytu.HasValue ? ngaytu : firstDayCurrentYear;
                    ngayden = ngayden.HasValue ? ngayden : lastDayCurrentYear;

                    var model = (from hosoct in _db.GiaRungCt
                                 join hoso in _db.GiaRung on hosoct.Mahs equals hoso.Mahs
                                 join nhom in _db.GiaRungDm on hosoct.Manhom equals nhom.Manhom
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaRungCt
                                 {
                                     Madv = hoso.Madv,
                                     Tendv = donvi.TenDv,
                                     Tennhom = nhom.Tennhom,
                                     SoQD = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                     MoTa = hosoct.MoTa,
                                     GiaRung1 = hosoct.GiaRung1,
                                     GiaRung2 = hosoct.GiaRung2,
                                     GiaRung3 = hosoct.GiaRung3,
                                     GiaRung4 = hosoct.GiaRung4,
                                     GiaRung5 = hosoct.GiaRung5,
                                     GiaRung6 = hosoct.GiaRung6,
                                     GiaChoThue1 = hosoct.GiaChoThue1,
                                     GiaChoThue2 = hosoct.GiaChoThue2,
                                     GiaBoiThuong1 = hosoct.GiaBoiThuong1,
                                     GiaBoiThuong2 = hosoct.GiaBoiThuong2,
                                     GiaBoiThuong3 = hosoct.GiaBoiThuong3,
                                     GiaBoiThuong4 = hosoct.GiaBoiThuong4,
                                     GiaBoiThuong5 = hosoct.GiaBoiThuong5,
                                     GiaBoiThuong6 = hosoct.GiaBoiThuong6,
                                     Manhom = hosoct.Manhom,
                                     Trangthai = hoso.Trangthai,
                                     Mahs = hoso.Mahs
                                 });

                    model = model.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && t.Trangthai == "HT");
                    if (MaNhom != "all") { model = model.Where(t => t.Manhom == MaNhom); }
                    if (MaHsTongHop != "all") { model = model.Where(t => t.Mahs == MaHsTongHop); }

                    List<string> list_madv = model.Select(t => t.Madv).ToList();
                    var model_donvi = _db.DsDonVi.Where(t => list_madv.Contains(t.MaDv));

                    List<string> list_mahs = model.Select(t => t.Mahs).ToList();
                    var model_hoso = _db.GiaRung.Where(t => list_mahs.Contains(t.Mahs));

                    ViewData["DonVis"] = model_donvi;
                    ViewData["ChiTietHs"] = model_hoso;
                    ViewData["NgayTu"] = ngaytu;
                    ViewData["NgayDen"] = ngayden;

                    ViewData["HoTenNguoiKy"] = hotennguoiky;
                    ViewData["ChucDanhNguoiKy"] = chucdanhky;
                    ViewData["Title"] = "Báo cáo giá rừng";
                    return View("Views/Admin/Manages/DinhGia/GiaRung/BaoCao/BcCT.cshtml", model);
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

        [HttpPost("GiaRung/BaoCao/GetListHoSo")]
        public JsonResult GetListHoSo(DateTime ngaytu, DateTime ngayden)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.GiaRung.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && t.Trangthai == "HT");
                string result = "<select class='form-control' id='mahs' name='mahs'>";
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
