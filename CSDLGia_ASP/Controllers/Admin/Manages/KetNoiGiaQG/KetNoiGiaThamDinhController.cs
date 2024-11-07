using Azure.Core;
using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.KetNoiGiaQG
{
    public class KetNoiGiaThamDinhController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public KetNoiGiaThamDinhController(CSDLGiaDBContext db)
        {
            _db = db;
        }       

        [Route("KetNoiGiaThamDinh/KhaiThac")]
        [HttpGet]
        public IActionResult KhaiThac()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {

                ViewData["Maso"] = "thamdinhgia";
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
                ViewData["Title"] = "Khai thác giá tài sản thẩm định giá";
                ViewData["MenuLv1"] = "menu_giaqg";
                ViewData["MenuLv2"] = "menu_giaqg_thamdinh";
                ViewData["MenuLv3"] = "menu_giaqg_thamdinh_khaithac";

                return View("Views/Admin/Manages/KetNoiGiaQG/KetNoiGiaThamDinh/KhaiThac.cshtml", model);

            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

        [Route("KetNoiGiaThamDinh/LayKhaiThac")]
        [HttpPost]
        public IActionResult LayKhaiThac(KetNoiAPI_DanhSach request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {               
                return RedirectToAction("KhaiThac", "KetNoiGiaThamDinh");
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

        [Route("KetNoiGiaThamDinh/DanhMuc")]
        [HttpGet]
        public IActionResult DanhMuc()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                ViewData["Maso"] = "thamdinhgiahd";
                var model = _db.ThamDinhGiaHD.ToList();
                var chk = _db.KetNoiAPI_DanhSach.FirstOrDefault(t => t.Maso == (string)ViewData["Maso"]);

                if (chk == null)
                {
                    var a_chucnang = Helpers.getDSChucNangCSDLQG();
                    var tenCN = a_chucnang.ContainsKey((string)ViewData["Maso"]) ? a_chucnang[(string)ViewData["Maso"]] : (string)ViewData["Maso"];

                    ViewData["Messages"] = "Hệ thống chưa thiết lập kết nối cho: "+ tenCN;
                    return View("Views/Admin/Error/Error.cshtml");
                }
                ViewData["KetNoiAPI_DanhSach"] = chk;
                ViewData["Title"] = "Hội đồng thẩm định giá";
                ViewData["MenuLv1"] = "menu_giaqg";
                ViewData["MenuLv2"] = "menu_giaqg_thamdinh";
                ViewData["MenuLv3"] = "menu_giaqg_thamdinh_danhmuc";

                return View("Views/Admin/Manages/KetNoiGiaQG/KetNoiGiaThamDinh/DanhMuc.cshtml", model);
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

        [Route("KetNoiGiaThamDinh/HoSo")]
        [HttpGet]
        public IActionResult HoSo()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                ViewData["Maso"] = "thamdinhgia";
                var model = _db.ThamDinhGia.ToList();
                var chk = _db.KetNoiAPI_DanhSach.FirstOrDefault(t => t.Maso == (string)ViewData["Maso"]);

                if (chk == null)
                {
                    var a_chucnang = Helpers.getDSChucNangCSDLQG();
                    var tenCN = a_chucnang.ContainsKey((string)ViewData["Maso"]) ? a_chucnang[(string)ViewData["Maso"]] : (string)ViewData["Maso"];

                    ViewData["Messages"] = "Hệ thống chưa thiết lập kết nối cho: " + tenCN;
                    return View("Views/Admin/Error/Error.cshtml");
                }
                ViewData["KetNoiAPI_DanhSach"] = chk;
                ViewData["Title"] = "Giá tài sản thẩm định giá";
                ViewData["MenuLv1"] = "menu_giaqg";
                ViewData["MenuLv2"] = "menu_giaqg_thamdinh";
                ViewData["MenuLv3"] = "menu_giaqg_thamdinh_hoso";

                return View("Views/Admin/Manages/KetNoiGiaQG/KetNoiGiaThamDinh/HoSo.cshtml", model);
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }
    }
}
