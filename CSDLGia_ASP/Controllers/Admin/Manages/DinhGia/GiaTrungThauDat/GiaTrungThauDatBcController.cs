
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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaTrungThauDat
{
    public class GiaTrungThauDatBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaTrungThauDatBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaTrungThauDat/BaoCao")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.trungthaudat.baocao", "Index"))
                {
                    DateTime nowDate = DateTime.Now;
                    DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
                    DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);
                    ViewData["ngaytu"] = firstDayCurrentYear.ToString("yyyy-MM-dd");
                    ViewData["ngayden"] = lastDayCurrentYear.ToString("yyyy-MM-dd");

                    ViewData["Title"] = "Báo cáo trúng thầu quyền sử dụng đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgd";
                    ViewData["MenuLv3"] = "menu_giadgd_bc";
                    ViewData["DanhSachHoSo"] = _db.GiaDauGiaDat.Where(t => t.Thoidiem >= firstDayCurrentYear && t.Thoidiem <= lastDayCurrentYear && t.Trangthai == "HT");
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauDat/BaoCao/Index.cshtml");
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

        [Route("GiaTrungThauDat/BaoCao/BcTH")]
        [HttpPost]
        public IActionResult BcTH(DateTime tungay, DateTime denngay, string chucdanhky, string hotennguoiky)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.trungthaudat.baocao", "Index"))
                {
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    var model = (from hoso in _db.GiaDauGiaDat.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && list_trangthai.Contains(t.Trangthai))
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaDauGiaDat
                                 {
                                     TenDonVi = donvi.TenDv,
                                     Mahs = hoso.Mahs,
                                     Soqdpagia = hoso.Soqdpagia,
                                     Thoidiem = hoso.Thoidiem,
                                     Thongtin = hoso.Thongtin,
                                 });

                    ViewData["Title"] = "Báo cáo tổng hợp trúng thầu quyền sử dụng đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgd";
                    ViewData["MenuLv3"] = "menu_giadgd_bc";
                    ViewData["tungay"] = tungay;
                    ViewData["denngay"] = denngay;
                    ViewData["ChucDanhNguoiKy"] = chucdanhky;
                    ViewData["HoTenNguoiKy"] = hotennguoiky;
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauDat/BaoCao/BcTH.cshtml", model);
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

        [Route("GiaTrungThauDat/BaoCao/BcCT")]
        [HttpPost]
        public IActionResult BcCT(DateTime? ngaytu, DateTime? ngayden, string MaHsTongHop, string chucdanhky, string hotennguoiky)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.trungthaudat.baocao", "Index"))
                {

                    var model = from dgct in _db.GiaDauGiaDatCt
                                     join dg in _db.GiaDauGiaDat on dgct.Mahs equals dg.Mahs
                                     join diaban in _db.DsDiaBan on dg.Madiaban equals diaban.MaDiaBan
                                     select new GiaDauGiaDatCt
                                     {
                                         Id = dgct.Id,
                                         Mahs = dgct.Mahs,
                                         MaDv = dgct.MaDv,
                                         TenDuAn = dg.Tenduan,
                                         Giakhoidiem = dgct.Giakhoidiem,
                                         Giadaugia = dgct.Giadaugia,
                                         ThoiDiem = dg.Thoidiem,
                                         TrangThai = dg.Trangthai,
                                         MaDiaBan=dgct.MaDiaBan,
                                         TenDiaBan=diaban.TenDiaBan,
                                         MaDiaBanCapHuyen = _db.DsDiaBan.FirstOrDefault(x => x.MaDiaBan == dgct.MaDiaBan).MaDiaBanCq,                                         
                                     };
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model = model.Where(t => t.ThoiDiem >= ngaytu && t.ThoiDiem <= ngayden && list_trangthai.Contains(t.TrangThai));

                    if (MaHsTongHop != "all") { model = model.Where(t => t.Mahs == MaHsTongHop); }

                    List<string> list_madv = model.Select(t => t.MaDv).ToList();
                    var model_donvi = _db.DsDonVi.Where(t => list_madv.Contains(t.MaDv));

                    List<string> list_mahs = model.Select(t => t.Mahs).ToList();
                    var model_hoso = _db.GiaDauGiaDat.Where(t => list_mahs.Contains(t.Mahs));
                    ViewData["TenTinh"] = _db.DsDiaBan.FirstOrDefault(x => string.IsNullOrEmpty(x.MaDiaBanCq)).TenDiaBan;
                    ViewData["DsDiaBanHuyen"] = _db.DsDiaBan.Where(x => x.Level == "H");
                    ViewData["DsDiaBanXa"] = _db.DsDiaBan.Where(x => x.Level == "X");
                    ViewData["DonVis"] = model_donvi;
                    ViewData["ChiTietHs"] = model_hoso;
                    ViewData["ngaytu"] = ngaytu;
                    ViewData["ngayden"] = ngayden;
                    ViewData["HoTenNguoiKy"] = hotennguoiky;
                    ViewData["ChucDanhNguoiKy"] = chucdanhky;
                    ViewData["Title"] = "Báo cáo tổng hợp trúng thầu quyền sử dụng đất";
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauDat/BaoCao/BcCT.cshtml", model);
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


        [HttpPost("GiaTrungThauDat/BaoCao/GetListHoSo")]
        public JsonResult GetListHoSo(DateTime ngaytu, DateTime ngayden)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.GiaDauGiaDat.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && t.Trangthai == "HT");
                string result = "<select class='form-control' id='mahs' name='mahs'>";
                result += "<option value='all'>--Tất cả---</option>";

                if (model.Any())
                {
                    foreach (var item in model)
                    {
                        result += "<option value='" + @item.Mahs + "'>Ký hiệu văn bản: " + @item.Soqddaugia + " - Thời điểm: " + @Helpers.ConvertDateToStr(item.Thoidiem) + "</option>";
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
