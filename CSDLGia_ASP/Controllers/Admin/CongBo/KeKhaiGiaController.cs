using CSDLGia_ASP.Database;
using CSDLGia_ASP.ViewModels.Manages.KeKhaiGia;
using Microsoft.AspNetCore.Mvc;
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

        [Route("CongBo/GiaDiVuLuuTru")]
        [HttpGet]
        public IActionResult DichVuLuuTru(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.KeKhaiGia.KkGia> model = _db.KkGia.Where(t => t.Trangthai == "DACONGBO" && t.Manghe == "DVLT");

            if (Madv != "all")
            {
                model = model.Where(t => t.Madv == Madv);
            }


            if (Nam != 0)
            {
                model = model.Where(t => t.Ngaynhap.Year == Nam).ToList();
            }

            ViewData["DsDonVi"] = _db.DsDonVi;
            ViewData["DsDiaBan"] = _db.DsDiaBan;
            ViewData["Madv"] = Madv;
            ViewData["Nam"] = Nam;
            ViewData["Title"] = "Công bố giá dịch vụ lưu trú ";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_dgcb";
            ViewData["MenuLv3"] = "menu_giathuedncb";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/DinhGia/GiaDichVuLuuTru.cshtml", model);
        }

        [Route("CongBo/DichVuLuuTru/Show")]
        [HttpGet]
        public IActionResult DichVuLuuTruShow(string Mahs)
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

            var modelcskd = _db.KkGiaDvLtCskd.FirstOrDefault(t => t.Macskd == model.Macskd);
            if (modelcskd != null)
            {
                hoso_kk.Tencskd = modelcskd.Tencskd;
                hoso_kk.Diachikd = modelcskd.Diachikd;
                hoso_kk.Loaihang = modelcskd.Loaihang;
            }

            var modeldv = _db.DsDonVi.FirstOrDefault(t => t.MaDv == model.Macqcq);
            if (modeldv != null)
            {
                hoso_kk.Tendvhienthi = modeldv.TenDvHienThi;
            }

            var modelct = _db.KkGiaDvLtCt.Where(t => t.Mahs == model.Mahs);
            if (modelct != null)
            {
                hoso_kk.KkGiaDvLtCt = modelct.ToList();
            }

            ViewData["Title"] = "Kê khai giá dịch vụ lưu trú";
            ViewData["MenuLv1"] = "menu_kknygia";
            ViewData["MenuLv2"] = "menu_kkgdvlt";
            ViewData["MenuLv3"] = "menu_giakkdvlt";
            return View("Views/Admin/Manages/KeKhaiGia/KkGiaDvLt/Show.cshtml", hoso_kk);
        }
    }
}
