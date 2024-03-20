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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaTaiSanCong
{
    public class TaiSanCongBcController : Controller
    {

        private readonly CSDLGiaDBContext _db;

        public TaiSanCongBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("BaoCaoTaiSanCong")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.taisancong.baocao", "Index"))
                {
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["DanhSachHoSo"] = _db.GiaTaiSanCong.Where(t => t.Thoidiem.Year == DateTime.Now.Year);
                    ViewData["Title"] = "Báo cáo tổng hợp tài sản công";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_tsc";
                    ViewData["MenuLv3"] = "menu_giatsc_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaTaiSanCong/BaoCao/Index.cshtml");
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

        [Route("GiaTaiSanCong/BaoCao/BcTH")]
        [HttpPost]
        public IActionResult BcTH(DateTime tungay, DateTime denngay, string chucdanhky, string hotennguoiky)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.taisancong.baocao", "Index"))
                {

                    var model = (from pl in _db.GiaTaiSanCong.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT")
                                 join dv in _db.DsDonVi on pl.Madv equals dv.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaTaiSanCong
                                 {
                                     Id = pl.Id,
                                     Mahs = pl.Mahs,
                                     Soqd = pl.Soqd,
                                     Thoidiem = pl.Thoidiem,
                                     Tentaisan = dv.TenDv,
                                 });
                    ViewData["tungay"] = tungay;
                    ViewData["denngay"] = denngay;
                    ViewData["ChucDanhNguoiKy"] = chucdanhky;
                    ViewData["HoTenNguoiKy"] = hotennguoiky;
                    ViewData["Title"] = "Báo cáo tổng hợp giá tài sản công";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_tsc";
                    ViewData["MenuLv3"] = "menu_giatsc_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaTaiSanCong/BaoCao/BcTH.cshtml", model);
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

        [Route("GiaTaiSanCong/BaoCao/BcCT")]
        [HttpPost]
        public IActionResult BcCT(DateTime ngaytu, DateTime ngayden, string MaHsTongHop, string chucdanhky, string hotennguoiky)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.taisancong.baocao", "Index"))
                {
                    var model = _db.GiaTaiSanCong.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && t.Trangthai == "HT");
                    if(MaHsTongHop != "all")
                    {
                        model = model.Where(t=>t.Mahs == MaHsTongHop);
                    }
                    List<string> list_hoso = model.Select(t => t.Mahs).ToList();
                    var model_ct = _db.GiaTaiSanCongCt.Where(t => list_hoso.Contains(t.Mahs));
                    List<string> list_donvi = model.Select(t=>t.Madv).ToList();
                    var model_donvi = _db.DsDonVi.Where(t => list_donvi.Contains(t.MaDv));
                
                    ViewData["HoSoCt"] = model_ct;
                    ViewData["DonVis"] = model_donvi;
                    ViewData["Title"] = "Báo cáo tổng hợp giá tài sản công";
                    ViewData["ThoiDiemKX"] = "Từ ngày " + Helpers.ConvertDateToStr(ngaytu) + " đến ngày " + Helpers.ConvertDateToStr(ngayden);
                    ViewData["ChucDanhNguoiKy"] = chucdanhky;
                    ViewData["HoTenNguoiKy"] = hotennguoiky;
                    return View("Views/Admin/Manages/DinhGia/GiaTaiSanCong/BaoCao/BcCT.cshtml", model);
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

        [HttpPost("GiaTaiSanCong/BaoCao/GetListHoSo")]
        public JsonResult GetListHoSo(DateTime ngaytu, DateTime ngayden)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.GiaTaiSanCong.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && t.Trangthai == "HT");
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
