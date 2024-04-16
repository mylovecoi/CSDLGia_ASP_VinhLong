using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.ViewModels.Manages.KeKhaiGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.CongBo
{
    public class KeKhaiGiaController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public KeKhaiGiaController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("CongBo/DichVuLuHanh")]
        [HttpGet]
        public IActionResult DichVuLuHanh(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.KeKhaiGia.KkGia> model = _db.KkGia.Where(t => t.Congbo == "DACONGBO" && t.Manghe == "LUHANH");

            if (Madv != "all")
            {
                model = model.Where(t => t.Madv == Madv);
            }


            if (Nam != 0)
            {
                model = model.Where(t => t.Ngaynhap.Year == Nam).ToList();
            }

            var dsdonvi = (from com in _db.Company
                           join lvkd in _db.CompanyLvCc.Where(t => t.Manghe == "LUHANH") on com.Mahs equals lvkd.Mahs
                           select new VMCompany
                           {
                               Id = com.Id,
                               Manghe = lvkd.Manghe,
                               Madv = com.Madv,
                               Madiaban = com.Madiaban,
                               Mahs = com.Mahs,
                               Tendn = com.Tendn,
                               Trangthai = com.Trangthai
                           }).ToList();

            ViewData["DsDonVi"] = dsdonvi;
            ViewData["DsDiaBan"] = _db.DsDiaBan;
            ViewData["Madv"] = Madv;
            ViewData["Nam"] = Nam;
            ViewData["Title"] = "Công bố giá dịch vụ lữ hành";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_kknygiacb";
            ViewData["MenuLv3"] = "menu_luhanhcongbo";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/KeKhaiGia/KkDvLuHanh.cshtml", model);
        }

        [Route("CongBo/DichVuLuHanh/Show")]
        [HttpGet]
        public IActionResult DichVuLuHanhShow(string Mahs)
        {
            var model = _db.KkGia.FirstOrDefault(t => t.Mahs == Mahs);
            var hoso_kk = new VMKkGiaShow
            {
                Id = model.Id,
                Mahs = model.Mahs,
                Madv = model.Madv,
                Macskd = model.Macskd,
                Socv = model.Socv,
                Ngaynhap = model.Ngaynhap,
                Ngayhieuluc = model.Ngayhieuluc,
                Ttnguoinop = model.Ttnguoinop,
                Dtll = model.Dtll,
                Sohsnhan = model.Sohsnhan,
                Ngaychuyen = model.Ngaychuyen,
                Ngaynhan = model.Ngaynhan,
                Ptnguyennhan = model.Ptnguyennhan,
                Chinhsachkm = model.Chinhsachkm,
            };

            var modeldn = _db.Company.FirstOrDefault(t => t.Madv == model.Madv);
            if (modeldn != null)
            {
                hoso_kk.Tendn = modeldn.Tendn;
                hoso_kk.Diadanh = modeldn.Diadanh;
                hoso_kk.Diachi = modeldn.Diachi;
            }

            var modeldv = _db.DsDonVi.FirstOrDefault(t => t.MaDv == model.Macqcq);
            if (modeldv != null)
            {
                hoso_kk.Tendvhienthi = modeldv.TenDvHienThi;
            }

            var modelct = _db.KkGiaLuHanhCt.Where(t => t.Mahs == model.Mahs);
            if (modelct != null)
            {
                hoso_kk.KkGiaLuHanhCt = modelct.ToList();
            }

            ViewData["Title"] = "Xem chi tiết hồ sơ kê khai giá dịch vụ lữ hành";
            ViewData["MenuLv1"] = "menu_kknygia";
            ViewData["MenuLv2"] = "menu_kkgluhanh";
            ViewData["MenuLv3"] = "menu_ttluhanh";
            return View("Views/Admin/Manages/KeKhaiGia/KkGiaLuHanh/DanhSach/Show.cshtml", hoso_kk);
        }
    }
}
