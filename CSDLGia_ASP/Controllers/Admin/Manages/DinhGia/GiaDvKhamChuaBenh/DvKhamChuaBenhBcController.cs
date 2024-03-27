//using CSDLGia_ASP.Database;
//using CSDLGia_ASP.Helper;
//using CSDLGia_ASP.Models.Manages.DinhGia;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDvKhamChuaBenh
//{
//    public class DvKhamChuaBenhBcController : Controller
//    {
//        private readonly CSDLGiaDBContext _db;

//        public DvKhamChuaBenhBcController(CSDLGiaDBContext db)
//        {
//            _db = db;
//        }

//        [Route("BaoCaoDinhGiaDvKcb")]
//        [HttpGet]
//        public IActionResult Index()
//        {
//            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
//            {
//                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khamchuabenh.baocao", "Index"))
//                {
//                    ViewData["Nam"] = DateTime.Now.Year;
//                    ViewData["Title"] = "Báo cáo tổng hợp dịch vụ khám chữa bệnh";
//                    ViewData["MenuLv1"] = "menu_dg";
//                    ViewData["MenuLv2"] = "menu_dgkcb";
//                    ViewData["MenuLv3"] = "menu_dgkcb_bc";
//                    return View("Views/Admin/Manages/DinhGia/GiaDvKhamChuaBenh/BaoCao/Index.cshtml");
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

//        //GiaDvKcb
//        //GiaDvKcbCt
//        //GiaDvKcbDm
//        //GiaDvKcbNhom
//        [Route("BaoCaoDinhGiaDvKcb/BcTH")]
//        [HttpPost]
//        public IActionResult BcTH(DateTime tungay, DateTime denngay, string tenthutruong, string chucvu)
//        {
//            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
//            {
//                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khamchuabenh.baocao", "Index"))
//                {
//                    var model = _db.GiaDvKcb.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT");
//                    ViewData["tungay"] = tungay;
//                    ViewData["denngay"] = denngay;
//                    ViewData["tenthutruong"] = tenthutruong;
//                    ViewData["chucvu"] = chucvu;
//                    ViewData["Title"] = "Báo cáo tổng hợp dịch vụ khám chữa bệnh";
//                    ViewData["MenuLv1"] = "menu_dg";
//                    ViewData["MenuLv2"] = "menu_dgkcb";
//                    ViewData["MenuLv3"] = "menu_dgkcb_bc";
//                    return View("Views/Admin/Manages/DinhGia/GiaDvKhamChuaBenh/BaoCao/BcTH.cshtml", model);
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

//        [Route("BaoCaoDinhGiaDvKcb/BcCT")]
//        [HttpPost]
//        public IActionResult BcCT(DateTime tungay, DateTime denngay, string tenthutruong, string chucvu)
//        {
//            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
//            {
//                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khamchuabenh.baocao", "Index"))
//                {
//                    var model = _db.GiaDvKcb.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT");
//                    var modelct = _db.GiaDvKcbCt.ToList();

//                    ViewData["tungay"] = tungay;
//                    ViewData["denngay"] = denngay;
//                    ViewData["ct"] = modelct;
//                    ViewData["tenthutruong"] = tenthutruong;
//                    ViewData["chucvu"] = chucvu;
//                    ViewData["Title"] = "Báo cáo chi tiết dịch vụ khám chữa bệnh";
//                    ViewData["MenuLv1"] = "menu_dg";
//                    ViewData["MenuLv2"] = "menu_dgkcb";
//                    ViewData["MenuLv3"] = "menu_dgkcb_bc";
//                    return View("Views/Admin/Manages/DinhGia/GiaDvKhamChuaBenh/BaoCao/BcCT.cshtml", model);
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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDvKhamChuaBenh
{
    public class DvKhamChuaBenhBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public DvKhamChuaBenhBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaDvKhamChuaBenh/BaoCao")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khamchuabenh.baocao", "Index"))
                {
                    ViewData["Nam"] = DateTime.Now.Year;
                    ViewData["Title"] = "Báo cáo giá dịch vụ khám chữa bệnh";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgkcb";
                    ViewData["MenuLv3"] = "menu_dgkcb_bc";
                    ViewData["DanhSachHoSo"] = _db.GiaDvKcb.Where(t => t.Thoidiem.Year == DateTime.Now.Year);
                    ViewData["DanhSachNhom"] = _db.GiaDvKcbNhom;
                    return View("Views/Admin/Manages/DinhGia/GiaDvKhamChuaBenh/BaoCao/Index.cshtml");
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

        [Route("GiaDvKhamChuaBenh/BaoCao/BcTH")]
        [HttpPost]
        public IActionResult BcTH(DateTime tungay, DateTime denngay, string chucdanhky, string hotennguoiky, string MaNhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khamchuabenh.baocao", "Index"))
                {

                    var model = (from hoso in _db.GiaDvKcb.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT")
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaDvKcb
                                 {
                                     TenDonVi = donvi.TenDv,
                                     Mahs = hoso.Mahs,
                                     Soqd = hoso.Soqd,
                                 });

                    ViewData["Title"] = "Báo cáo giá dịch vụ khám chữa bệnh";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgkcb";
                    ViewData["MenuLv3"] = "menu_dgkcb_bc";
                    ViewData["NgayTu"] = tungay;
                    ViewData["NgayDen"] = denngay;
                    ViewData["ChucDanhNguoiKy"] = chucdanhky;
                    ViewData["HoTenNguoiKy"] = hotennguoiky;
                    return View("Views/Admin/Manages/DinhGia/GiaDvKhamChuaBenh/BaoCao/BcTH.cshtml", model);
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

        [Route("GiaDvKhamChuaBenh/BaoCao/BcCT")]
        [HttpPost]
        public IActionResult BcCT(DateTime? ngaytu, DateTime? ngayden, string MaHsTongHop, string chucdanhky, string hotennguoiky, string MaNhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khamchuabenh.baocao", "Index"))
                {
                    DateTime nowDate = DateTime.Now;
                    DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
                    DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);
                    MaNhom = string.IsNullOrEmpty(MaNhom) ? "all" : MaNhom;
                    ngaytu = ngaytu.HasValue ? ngaytu : firstDayCurrentYear;
                    ngayden = ngayden.HasValue ? ngayden : lastDayCurrentYear;

                    var model = from dgct in _db.GiaDvKcbCt.Where(t => t.Ghichu == "XD")
                                     join dg in _db.GiaDvKcb on dgct.Mahs equals dg.Mahs
                                     //join dgdm in _db.GiaDvGdDtDm on dgct.Maspdv equals dgdm.Maspdv
                                     select new VMDinhGiaDvKcb
                                     {
                                         Id = dg.Id,
                                         Mahs = dg.Mahs,
                                         Madv = dg.Madv,
                                         Macqcq = dg.Macqcq,
                                         Thoidiem = dg.Thoidiem,
                                         Maspdv = dgct.Maspdv,
                                         Tenspdv = dgct.Tenspdv,
                                         Giadv = dgct.Giadv,
                                         Trangthai = dgct.Trangthai,
                                         Manhom = dgct.Manhom,
                                     };

               
                    //model = model.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && t.Trangthai == "HT");
                    //if (MaNhom != "all") { model = model.Where(t => t.MaNhom == MaNhom); }
                    if (MaHsTongHop != "all") { model = model.Where(t => t.Mahs == MaHsTongHop); }

                    List<string> list_madv = model.Select(t => t.Madv).ToList();
                    var model_donvi = _db.DsDonVi.Where(t => list_madv.Contains(t.MaDv));

                    List<string> list_mahs = model.Select(t => t.Mahs).ToList();
                    var model_hoso = _db.GiaDvKcb.Where(t => list_mahs.Contains(t.Mahs));

                    ViewData["DonVis"] = model_donvi;
                    ViewData["ChiTietHs"] = model_hoso;
                    ViewData["NgayTu"] = ngaytu;
                    ViewData["NgayDen"] = ngayden;

                    ViewData["HoTenNguoiKy"] = hotennguoiky;
                    ViewData["ChucDanhNguoiKy"] = chucdanhky;
                    ViewData["Title"] = "Báo cáo giá dịch vụ khám chữa bệnh";
                    return View("Views/Admin/Manages/DinhGia/GiaDvKhamChuaBenh/BaoCao/BcCT.cshtml", model);
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
