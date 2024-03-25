
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giadatdb.baocao", "Index"))
                {
                    ViewData["Nam"] = DateTime.Now.Year;
                    ViewData["Title"] = "Báo cáo trúng thầu quyền sử dụng đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_GiaTrungThauDat";
                    ViewData["MenuLv3"] = "menu_GiaTrungThauDat_bc";
                    ViewData["DanhSachHoSo"] = _db.GiaDauGiaDat.Where(t => t.Thoidiem.Year == DateTime.Now.Year);
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giadatdb.baocao", "Index"))
                {

                    var model = (from hoso in _db.GiaDauGiaDat.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT")
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaDauGiaDat
                                 {

                                     Mahs = hoso.Mahs,
                                     //Soqd = hoso.Soqd,

                                 });

                    ViewData["Title"] = "Báo cáo tổng hợp trúng thầu quyền sử dụng đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_GiaTrungThauDat";
                    ViewData["MenuLv3"] = "menu_GiaTrungThauDat_bc";
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
        public IActionResult BcCT(DateTime ngaytu, DateTime ngayden, string MaHsTongHop, string chucdanhky, string hotennguoiky)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giadatdb.baocao", "Index"))
                {
                    var model = _db.GiaDauGiaDat.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && t.Trangthai == "HT");
                    if (MaHsTongHop != "all")
                    {
                        model = model.Where(t => t.Mahs == MaHsTongHop);
                    }
                    List<string> list_hoso = model.Select(t => t.Mahs).ToList();
                    List<string> list_donvi = model.Select(t => t.Madv).ToList();
                    var model_ct = _db.GiaDauGiaDatCt.Where(t => list_hoso.Contains(t.Mahs));
                    var model_donvi = _db.DsDonVi.Where(t => list_donvi.Contains(t.MaDv));
                    ViewData["HoSoCt"] = model_ct;
                    ViewData["DonVis"] = model_donvi;

                    ViewData["ThoiDiemKX"] = "Từ ngày " + Helpers.ConvertDateToStr(ngaytu) + " đến ngày " + Helpers.ConvertDateToStr(ngayden);

                    ViewData["ChucDanhNguoiKy"] = chucdanhky;
                    ViewData["HoTenNguoiKy"] = hotennguoiky;
                    ViewData["Title"] = "Báo cáo chi tiết giá trúng thầu quyền sử dụng đất";
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


    }
}
