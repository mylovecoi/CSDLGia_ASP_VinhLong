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
using Microsoft.Extensions.Hosting;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDatPl
{
    public class GiaDatPlBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaDatPlBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaDatPl/BaoCao")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthe.baocao", "Index"))
                {
                    DateTime nowDate = DateTime.Now;
                    DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
                    DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);
                    ViewData["ngaytu"] = firstDayCurrentYear.ToString("yyyy-MM-dd");
                    ViewData["ngayden"] = lastDayCurrentYear.ToString("yyyy-MM-dd");

                    ViewData["Title"] = "Báo cáo tổng hợp giá đất cụ thể";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgdct";
                    ViewData["MenuLv3"] = "menu_dgdct_bc";
                    ViewData["DanhSachHoSo"] = _db.GiaDatPhanLoai.Where(t => t.Trangthai == "HT");
                    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/BaoCao/Index.cshtml");
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

        [Route("GiaDatPl/BaoCao/BcTH")]
        [HttpPost]
        public IActionResult BcTH(DateTime tungay, DateTime denngay, string chucdanhky, string hotennguoiky)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthe", "Index"))
                {
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                  
                    var model = (from hoso in _db.GiaDatPhanLoai.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && list_trangthai.Contains(t.Trangthai))
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaDatPhanLoai
                                 {
                                     TenDonVi = donvi.TenDv,
                                     Mahs = hoso.Mahs,
                                     Soqd = hoso.Soqd,
                                     Ghichu = hoso.Ghichu,
                                     Thoidiem = hoso.Thoidiem,
                                 });
                    ViewData["Title"] = "Báo cáo tổng hợp giá đất cụ thể";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgdct";
                    ViewData["MenuLv3"] = "menu_dgdct_bc";
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

                    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/BaoCao/BcTH.cshtml", model);
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

        [Route("GiaDatPl/BaoCao/BcCT")]
        [HttpPost]
        public IActionResult BcCT(DateTime? ngaytu, DateTime? ngayden, string mahsth, string chucdanhky, string hotennguoiky, string phanloaihoso)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthe", "Index"))
                {
                    var model = (from giact in _db.GiaDatPhanLoaiCt
                                 join gia in _db.GiaDatPhanLoai on giact.Mahs equals gia.Mahs
                                 join donvi in _db.DsDonVi on gia.Madv equals donvi.MaDv
                                 join diaban in _db.DsDiaBan on gia.Madiaban equals diaban.MaDiaBan
                                 select new GiaDatPhanLoaiCt
                                 {
                                     Id = giact.Id,
                                     Madv = gia.Madv,
                                     Tendv = donvi.TenDv,
                                     Mahs = giact.Mahs,
                                     Thoidiem = gia.Thoidiem,
                                     Giacuthe = giact.Giacuthe,
                                     PhanLoai = gia.Phanloai,
                                     Khuvuc = giact.Khuvuc,
                                     Trangthai = gia.Trangthai,
                                     MaDiaBan = giact.MaDiaBan,     
                                     TenDiaBan = diaban.TenDiaBan,
                                     
                                 });
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model = model.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && list_trangthai.Contains(t.Trangthai));

                    if (mahsth != "all") { model = model.Where(t => t.Mahs == mahsth);}


                    if (phanloaihoso != "all") { model = model.Where(t => t.PhanLoai == phanloaihoso); }

            

                    List<string> list_madv = model.Select(t => t.Madv).ToList();
                    var model_donvi = _db.DsDonVi.Where(t => list_madv.Contains(t.MaDv));

                    List<string> list_mahs = model.Select(t => t.Mahs).ToList();
                    var model_hoso = _db.GiaDatPhanLoai.Where(t => list_mahs.Contains(t.Mahs));

                    ViewData["TenTinh"] = _db.DsDiaBan.FirstOrDefault(x => string.IsNullOrEmpty(x.MaDiaBanCq)).TenDiaBan;
                    ViewData["DsDiaBanHuyen"] = _db.DsDiaBan.Where(x => x.Level == "H");
                    ViewData["DsDiaBanXa"] = _db.DsDiaBan.Where(x => x.Level == "X");

                    ViewData["DonVis"] = model_donvi;
                    ViewData["ChiTietHs"] = model_hoso;
                    ViewData["NgayTu"] = ngaytu;
                    ViewData["NgayDen"] = ngayden;

                    ViewData["HoTenNguoiKy"] = hotennguoiky;
                    ViewData["ChucDanhNguoiKy"] = chucdanhky;
                    ViewData["Title"] = "Báo cáo giá đất cụ thể";

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

                    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/BaoCao/BcCT.cshtml", model);
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

        [Route("GiaDatPl/BaoCao/GetListHoSo")]
        public JsonResult GetListHoSo(DateTime ngaytu, DateTime ngayden)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.GiaDatPhanLoai.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && t.Trangthai == "HT");
                string result = "<select class='form-control' id='mahsth' name='mahsth'>";
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

