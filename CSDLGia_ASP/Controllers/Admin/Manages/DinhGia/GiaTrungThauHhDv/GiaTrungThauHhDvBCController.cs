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
                    ViewData["Nam"] = DateTime.Now.Year;
                    ViewData["Title"] = "Báo cáo giá trúng thầu hàng hóa được mua sắm theo quy định của pháp luật về đấu thầu";
                    ViewData["MenuLv1"] = "menu_mts";
                    ViewData["MenuLv2"] = "menu_giamts_bc";
                    ViewData["DanhSachHoSo"] = _db.GiaMuaTaiSan.Where(t => t.Thoidiem.Year == DateTime.Now.Year);
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

                    var model =(from hoso in _db.GiaMuaTaiSan.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT")
                                join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                select new CSDLGia_ASP.Models.Manages.DinhGia.GiaMuaTaiSan
                                { 
                                    //TenDonVi = donvi.TenDv,
                                    Mahs = hoso.Mahs,
                                    Soqd = hoso.Soqd,                                   

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
        public IActionResult BcCT(DateTime ngaytu, DateTime ngayden, string MaHsTongHop, string chucdanhky, string hotennguoiky)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.muataisan.baocao", "Index"))
                {
                    var model = _db.GiaMuaTaiSan.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && t.Trangthai == "HT");
                    if (MaHsTongHop != "all")
                    {
                        model = model.Where(t => t.Mahs == MaHsTongHop);
                    }
                    List<string> list_hoso = model.Select(t => t.Mahs).ToList();
                    List<string> list_donvi = model.Select(t => t.Madv).ToList();
                    var model_ct = _db.GiaMuaTaiSanCt.Where(t => list_hoso.Contains(t.Mahs));
                    var model_donvi = _db.DsDonVi.Where(t => list_donvi.Contains(t.MaDv));
                    ViewData["HoSoCt"] = model_ct;
                    ViewData["DonVis"] = model_donvi;
                    ViewData["Title"] = "Báo cáo giá trúng thầu hàng hóa được mua sắm theo quy định của pháp luật về đấu thầu";
                    ViewData["ThoiDiemKX"] = "Từ ngày " + Helpers.ConvertDateToStr(ngaytu) + " đến ngày " + Helpers.ConvertDateToStr(ngayden);
                    ViewData["ChucDanhNguoiKy"] = chucdanhky;
                    ViewData["HoTenNguoiKy"] = hotennguoiky;
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
