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
    public class KetNoiGiaDaoTaoController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public KetNoiGiaDaoTaoController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("KetNoiGiaDaoTao/ThietLap")]
        [HttpGet]
        public IActionResult ThietLap()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {

                ViewData["Maso"] = "giadaotao";
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
                ViewData["Title"] = "Thiết lập giá dịch vụ đào tạo";
                ViewData["MenuLv1"] = "menu_giaqg";
                ViewData["MenuLv2"] = "menu_giaqg_giaoduc";
                ViewData["MenuLv3"] = "menu_giaqg_giaoduc_thietlap";

                return View("Views/Admin/Manages/KetNoiGiaQG/KetNoiGiaDaoTao/ThietLap.cshtml", model);

            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

        [Route("KetNoiGiaDaoTao/LuuThietLap")]
        [HttpPost]
        public IActionResult Update(KetNoiAPI_DanhSach request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.KetNoiAPI_DanhSach.FirstOrDefault(t => t.Maso == request.Maso);
                model.LinkNhanGet = request.LinkNhanGet;
                model.LinkTruyenPost = request.LinkTruyenPost;
                model.NguoiDuyet = request.NguoiDuyet;
                model.NguoiTao = request.NguoiTao;
                model.MaDonVi = request.MaDonVi;
                model.MaDiaBan = request.MaDiaBan;
                _db.KetNoiAPI_DanhSach.Update(model);
                _db.SaveChanges();
                return RedirectToAction("ThietLap", "KetNoiGiaDaoTao");
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

        [Route("KetNoiGiaDaoTao/KhaiThac")]
        [HttpGet]
        public IActionResult KhaiThac()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {

                ViewData["Maso"] = "giadaotao";
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
                ViewData["Title"] = "Khai thác giá dịch vụ đào tạo";
                ViewData["MenuLv1"] = "menu_giaqg";
                ViewData["MenuLv2"] = "menu_giaqg_giaoduc";
                ViewData["MenuLv3"] = "menu_giaqg_giaoduc_khaithac";

                return View("Views/Admin/Manages/KetNoiGiaQG/KetNoiGiaDaoTao/KhaiThac.cshtml", model);

            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

        [Route("KetNoiGiaDaoTao/LayKhaiThac")]
        [HttpPost]
        public IActionResult LayKhaiThac(KetNoiAPI_DanhSach request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {               
                return RedirectToAction("KhaiThac", "KetNoiGiaDaoTao");
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

        [Route("KetNoiGiaDaoTao/DanhMuc")]
        [HttpGet]
        public IActionResult DanhMuc()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                ViewData["Maso"] = "giadaotao";
                var model = _db.GiaDvGdDtNhom.ToList();
                ViewData["KetNoiAPI_DanhSach"] = _db.KetNoiAPI_DanhSach.FirstOrDefault(t => t.Maso == (string)ViewData["Maso"]);
                ViewData["Title"] = "Giá dịch vụ đào tạo";
                ViewData["MenuLv1"] = "menu_giaqg";
                ViewData["MenuLv2"] = "menu_giaqg_giaoduc";
                ViewData["MenuLv3"] = "menu_giaqg_giaoduc_danhmuc";

                return View("Views/Admin/Manages/KetNoiGiaQG/KetNoiGiaDaoTao/DanhMuc.cshtml", model);
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

        [Route("KetNoiGiaDaoTao/HoSo")]
        [HttpGet]
        public IActionResult HoSo()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                ViewData["Maso"] = "giadaotao";
                var model = _db.GiaDvGdDtNhom.ToList();
                ViewData["KetNoiAPI_DanhSach"] = _db.KetNoiAPI_DanhSach.FirstOrDefault(t => t.Maso == (string)ViewData["Maso"]);
                ViewData["Title"] = "Giá dịch vụ đào tạo";
                ViewData["MenuLv1"] = "menu_giaqg";
                ViewData["MenuLv2"] = "menu_giaqg_giaoduc";
                ViewData["MenuLv3"] = "menu_giaqg_giaoduc_hoso";

                return View("Views/Admin/Manages/KetNoiGiaQG/KetNoiGiaDaoTao/HoSo.cshtml", model);
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }
    }
}
