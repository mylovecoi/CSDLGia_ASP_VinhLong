using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaThueTSanCongCongBo
{
    public class GiaThueTSanCongCongBoController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaThueTSanCongCongBoController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaThueTaiSanCong/CongBo")]
        [HttpGet]
        public IActionResult Index(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaThueTaiSanCong> model = _db.GiaThueTaiSanCong.Where(t => t.Congbo == "DACONGBO");

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
            ViewData["Title"] = "Công bố giá thuê tài sản công";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_dgcb";
            ViewData["MenuLv3"] = "menu_giathuetscongcb";
            ViewBag.bSession = false;
            return View("Views/Admin/Systems/CongBo/GiaThueTSCCongBo.cshtml", model);
        }

        [Route("GiaThueTaiSanCong/CongBo/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            var model = _db.GiaThueTaiSanCong.FirstOrDefault(t => t.Mahs == Mahs);
            var modelct = _db.GiaThueTaiSanCongCt.Where(t => t.Mahs == Mahs);
            model.GiaThueTaiSanCongCt = modelct.ToList();
            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            ViewData["Title"] = "Chi tiết giá thuê tài sản công";
            return View("Views/Admin/Manages/DinhGia/GiaThueTsc/DanhSach/Show.cshtml", model);
        }


    }
}
