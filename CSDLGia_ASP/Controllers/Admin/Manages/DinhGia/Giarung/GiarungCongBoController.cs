using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiarungCongBo
{
    public class GiarungCongBoController : Controller
    {


        private readonly CSDLGiaDBContext _db;

        public GiarungCongBoController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaRung/CongBo")]
        [HttpGet]
        public IActionResult Index(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaRung> model = _db.GiaRung.Where(t => t.Congbo == "DACONGBO");

            if (Madv != "all")
            {
                model = model.Where(t => t.Madv == Madv);
            }

            if (Nam != 0)
            {
                model = model.Where(t => t.Thoidiem.Year == Nam).ToList();
            }

            ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
            ViewData["DsDiaBan"] = _db.DsDiaBan;
            ViewData["DsCqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
            ViewData["Madv"] = Madv;
            ViewData["Nam"] = Nam;
            ViewData["Title"] = "Công bố giá rừng";
            ViewBag.bSession = true;
            return View("Views/Admin/Systems/CongBo/GiaRungCongBo.cshtml", model);
        }

        [Route("GiaRung/CongBo/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            var model = _db.GiaRung.FirstOrDefault(t => t.Mahs == Mahs);
            var modelct = _db.GiaRungCt.Where(t => t.Mahs == Mahs);
            model.GiaRungCt = modelct.ToList();
            ViewData["NhomDm"] = _db.GiaRungDm;
            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            ViewData["Title"] = "Chi tiết giá rừng";
            return View("Views/Admin/Manages/DinhGia/GiaRung/Show.cshtml", model);
        }
    }
}

