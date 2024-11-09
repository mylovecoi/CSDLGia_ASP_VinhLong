using Azure.Core;
using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems.API;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.KetNoiGiaQG
{
    public class KetNoiGiaPhiLePhiController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public KetNoiGiaPhiLePhiController(CSDLGiaDBContext db)
        {
            _db = db;
        }       

        [Route("KetNoiGiaPhiLePhi/KhaiThac")]
        [HttpGet]
        public IActionResult KhaiThac()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {

                ViewData["Maso"] = "gialephi";
                var chk = _db.KetNoiAPI_DanhSach.FirstOrDefault(t => t.Maso == (string)ViewData["Maso"]);
                if (chk == null)
                {
                    var ketnoi = new KetNoiAPI_DanhSach
                    {
                        Maso = (string)ViewData["Maso"],
                    };
                    _db.KetNoiAPI_DanhSach.Add(ketnoi);
                    _db.SaveChanges();
                }
                var model = _db.KetNoiAPI_DanhSach.FirstOrDefault(t => t.Maso == (string)ViewData["Maso"]);
                ViewData["Title"] = "Khai thác giá phí, lệ phí trước bạ";
                ViewData["MenuLv1"] = "menu_giaqg";
                ViewData["MenuLv2"] = "menu_giaqg_philephi";
                ViewData["MenuLv3"] = "menu_giaqg_philephi_khaithac";

                return View("Views/Admin/Manages/KetNoiGiaQG/KetNoiGiaPhiLePhi/KhaiThac.cshtml", model);

            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

        [Route("KetNoiGiaPhiLePhi/LayKhaiThac")]
        [HttpPost]
        public IActionResult LayKhaiThac(KetNoiAPI_DanhSach request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {               
                return RedirectToAction("KhaiThac", "KetNoiGiaPhiLePhi");
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

        [Route("KetNoiGiaPhiLePhi/DanhMuc")]
        [HttpGet]
        public IActionResult DanhMuc()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                ViewData["Maso"] = "gialephidm";
                var model = _db.GiaPhiLePhiNhom.ToList();
                var chk = _db.KetNoiAPI_DanhSach.FirstOrDefault(t => t.Maso == (string)ViewData["Maso"]);

                if (chk == null)
                {
                    var a_chucnang = Helpers.getDSChucNangCSDLQG();
                    var tenCN = a_chucnang.ContainsKey((string)ViewData["Maso"]) ? a_chucnang[(string)ViewData["Maso"]] : (string)ViewData["Maso"];

                    ViewData["Messages"] = "Hệ thống chưa thiết lập kết nối cho: "+ tenCN;
                    return View("Views/Admin/Error/Error.cshtml");
                }
                ViewData["KetNoiAPI_DanhSach"] = chk;
                ViewData["Title"] = "Giá phí, lệ phí trước bạ";
                ViewData["MenuLv1"] = "menu_giaqg";
                ViewData["MenuLv2"] = "menu_giaqg_philephi";
                ViewData["MenuLv3"] = "menu_giaqg_philephi_danhmuc";

                return View("Views/Admin/Manages/KetNoiGiaQG/KetNoiGiaPhiLePhi/DanhMuc.cshtml", model);
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

        [Route("KetNoiGiaPhiLePhi/HoSo")]
        [HttpGet]
        public IActionResult HoSo()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                ViewData["Maso"] = "gialephi";
                var model = _db.GiaPhiLePhi.ToList();
                var chk = _db.KetNoiAPI_DanhSach.FirstOrDefault(t => t.Maso == (string)ViewData["Maso"]);

                if (chk == null)
                {
                    var a_chucnang = Helpers.getDSChucNangCSDLQG();
                    var tenCN = a_chucnang.ContainsKey((string)ViewData["Maso"]) ? a_chucnang[(string)ViewData["Maso"]] : (string)ViewData["Maso"];

                    ViewData["Messages"] = "Hệ thống chưa thiết lập kết nối cho: " + tenCN;
                    return View("Views/Admin/Error/Error.cshtml");
                }
                ViewData["KetNoiAPI_DanhSach"] = chk;
                ViewData["Title"] = "Giá phí, lệ phí trước bạ";
                ViewData["MenuLv1"] = "menu_giaqg";
                ViewData["MenuLv2"] = "menu_giaqg_philephi";
                ViewData["MenuLv3"] = "menu_giaqg_philephi_hoso";

                return View("Views/Admin/Manages/KetNoiGiaQG/KetNoiGiaPhiLePhi/HoSo.cshtml", model);
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }
    }
}
