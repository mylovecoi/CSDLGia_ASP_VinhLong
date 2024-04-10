using CSDLGia_ASP.Database;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.CongBo
{
    public class GiaDatController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaDatController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("CongBo/GiaDatCuThe")]
        [HttpGet]
        public IActionResult GiaDatCuThe(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaDatPhanLoai> model = _db.GiaDatPhanLoai.Where(t => t.Congbo == "DACONGBO");

            if (Madv != "all")
            {
                model = model.Where(t => t.Madv == Madv);
            }


            if (Nam != 0)
            {
                model = model.Where(t => t.Thoidiem.Year == Nam).ToList();
            }

            ViewData["DsDonVi"] = _db.DsDonVi;
            ViewData["DsDiaBan"] = _db.DsDiaBan;
            ViewData["Madv"] = Madv;
            ViewData["Nam"] = Nam;
            ViewData["Title"] = "Công bố giá đất cụ thể";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_giadatcb";
            ViewData["MenuLv3"] = "menu_giadatcuthecongbo";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/GiaDat/GiaDatCuThe.cshtml", model);
        }

        [Route("CongBo/GiaDatCuThe/Show")]
        [HttpGet]
        public IActionResult GiaDatCuTheShow(string Mahs)
        {
            var model = _db.GiaDatPhanLoai.FirstOrDefault(t => t.Mahs == Mahs);
            var model_new = new VMDinhGiaDat
            {
                Madv = model.Madv,
                Mahs = model.Mahs,
                Madiaban = model.Madiaban,
                Soqd = model.Soqd,
                Thoidiem = model.Thoidiem,
                Thongtin = model.Thongtin,
                Ghichu = model.Ghichu,
                Phanloai = model.Phanloai,

            };

            var model_ct = _db.GiaDatPhanLoaiCt.Where(t => t.Mahs == model_new.Mahs);

            model_new.GiaDatPhanLoaiCt = model_ct.ToList();

            ViewData["Madv"] = model.Madv;
            ViewData["Mahs"] = model.Mahs;
            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            ViewData["Dmloaidat"] = _db.DmLoaiDat.ToList();
            ViewData["Title"] = "Xem chi tiết giá đất cụ thể";
            return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Show.cshtml", model_new);
        }
    }
}
