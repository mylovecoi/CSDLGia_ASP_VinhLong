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

namespace CSDLGia_ASP.Controllers.Admin.Manages.KeKhaiGia.KkGiaDvlt
{
    public class KkGiaDvltBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public KkGiaDvltBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("BaoCaoKkGiaDvlt")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgdvlt.giakkbc", "Index"))
                {
                    ViewData["Title"] = "Báo cáo tổng hợp kê khai giá dịch vụ lưu trú";
                    ViewData["MenuLv1"] = "menu_kknygia";
                    ViewData["MenuLv2"] = "menu_kkgdvlt";
                    ViewData["MenuLv3"] = "menu_giakkbcdvlt";
                    return View("Views/Admin/Manages/KeKhaiGia/KkGiaDvlt/BaoCao/Index.cshtml");
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

        [Route("BaoCaoKkGiaDvlt/Bc1")]
        [HttpPost]
        public IActionResult Bc1(DateTime tungay_bc, DateTime denngay_bc, string loaihang, string phanloai)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgdvlt.giakkbc", "Index"))
                {
                    var model = (from kk in _db.KkGia.Where(t => t.Manghe == "DVLT" && (t.Ngaynhan >= tungay_bc && t.Ngaynhan <= denngay_bc))
                                 join cskd in _db.KkGiaDvLtCskd on kk.Macskd equals cskd.Macskd
                                 select new VMKkGia
                                 {
                                     Id = kk.Id,
                                     Tencskd = cskd.Tencskd,
                                     Loaihang = cskd.Loaihang,
                                     Diachi = cskd.Diachikd,
                                     Sdt = cskd.Telkd,
                                     Socv = kk.Socv,
                                     Ngaychuyen = kk.Ngaychuyen,
                                     Ngaynhan = kk.Ngaynhan,
                                     Ngayhieuluc = kk.Ngayhieuluc,
                                     Thoihan = kk.Thoihan,
                                 });

                    if(loaihang != "all")
                    {
                        model = model.Where(t => t.Loaihang == loaihang);
                    }

                    if(phanloai != "all")
                    {
                        model = model.Where(t => t.Thoihan == phanloai);
                    }

                    ViewData["tungay_bc"] = tungay_bc;
                    ViewData["denngay_bc"] = denngay_bc;
                    ViewData["loaihang"] = loaihang;
                    ViewData["phanloai"] = phanloai;
                    ViewData["Title"] = "Báo cáo tổng hợp kê khai giá dịch vụ lưu trú";
                    ViewData["MenuLv1"] = "menu_kknygia";
                    ViewData["MenuLv2"] = "menu_kkgdvlt";
                    ViewData["MenuLv3"] = "menu_giakkbcdvlt";
                    return View("Views/Admin/Manages/KeKhaiGia/KkGiaDvlt/BaoCao/Bc1.cshtml", model);
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

        [Route("BaoCaoKkGiaDvlt/Bc2")]
        [HttpPost]
        public IActionResult Bc2(DateTime tungay, DateTime denngay)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgdvlt.giakkbc", "Index"))
                {
                    var model = _db.KkGiaDvLtCskd;
                    foreach(var tt in model)
                    {
                        var modelkk = _db.KkGia.Where(t => t.Manghe == "DVLT" && t.Macskd == tt.Macskd && t.Trangthai == "DD" && (t.Ngaynhan >= tungay && t.Ngaynhan <= denngay)).ToList();
                        tt.Lankk = modelkk.Count;
                        if(modelkk.Count > 0)
                        {
                            var modelkkgn = _db.KkGia.Where(t => t.Manghe == "DVLT" && t.Macskd == tt.Macskd && t.Trangthai == "DD" && (t.Ngaynhan >= tungay && t.Ngaynhan <= denngay)).Max(t => t.Id);
                            var modelgn = _db.KkGia.FirstOrDefault(t => t.Manghe == "DVLT" && t.Id == modelkkgn);
                            tt.Kklc = "Số CV" + modelgn.Socv + ", ngày hiệu lực: " + Helpers.ConvertDateToStr(modelgn.Ngayhieuluc);
                        }
                    }

                    ViewData["tungay"] = tungay;
                    ViewData["denngay"] = denngay;
                    ViewData["Title"] = "Báo cáo thống kê các đơn vị kê khai giá trong khoảng thời gian";
                    ViewData["MenuLv1"] = "menu_kknygia";
                    ViewData["MenuLv2"] = "menu_kkgdvlt";
                    ViewData["MenuLv3"] = "menu_giakkbcdvlt";
                    return View("Views/Admin/Manages/KeKhaiGia/KkGiaDvlt/BaoCao/Bc2.cshtml", model);
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
