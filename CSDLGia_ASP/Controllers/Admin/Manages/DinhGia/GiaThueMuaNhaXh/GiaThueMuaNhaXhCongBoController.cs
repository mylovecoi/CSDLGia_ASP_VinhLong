using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaThueMuaNhaXhCongBo
{
    public class GiaThueMuaNhaXhCongBoController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaThueMuaNhaXhCongBoController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaThueMuaNhaXh/CongBo")]
        [HttpGet]
        public IActionResult Index(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMuaNhaXh> model = _db.GiaThueMuaNhaXh.Where(t => t.Congbo == "DACONGBO");

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
            ViewData["Title"] = "Công bố giá thuê mua nhà xã hội";
            ViewBag.bSession = true;
            return View("Views/Admin/Systems/CongBo/GiaThueMuaNhaXh.cshtml", model);
        }

        [Route("GiaThueMuaNhaXh/CongBo/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            var model = _db.GiaThueMuaNhaXh.FirstOrDefault(t => t.Mahs == Mahs);
            var modelct = _db.GiaThueMuaNhaXhCt.Where(t => t.Mahs == Mahs);
            model.GiaThueMuaNhaXhCt = modelct.ToList();
            ViewData["GiaThueMuaNhaXhDm"] = _db.GiaThueMuaNhaXhDm.ToList();
            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            ViewData["Title"] = "Chi tiết giá thuê mua nhà xã hội";
            return View("Views/Admin/Manages/DinhGia/GiaThueMuaNhaXh/Show.cshtml", model);
        }
    }
}
