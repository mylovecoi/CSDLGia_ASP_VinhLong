using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaSpDvCongIch
{
    public class GiaSpDvCongIchCongBoController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaSpDvCongIchCongBoController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaSpDvCi/CongBo")]
        [HttpGet]
        public IActionResult Index(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCongIch> model = _db.GiaSpDvCongIch.Where(t => t.Congbo == "DACONGBO");

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
            ViewData["Title"] = "Công bố giá sản phẩm dịch vụ công ích";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_dg";
            ViewData["MenuLv3"] = "menu_giaspdvci";
            ViewBag.bSession = false;
            return View("Views/Admin/Systems/CongBo/GiaSpDvCongIchCongBo.cshtml", model);
        }

        [Route("GiaSpDvCi/CongBo/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            var model = _db.GiaSpDvCongIch.FirstOrDefault(t => t.Mahs == Mahs);
            var modelct = _db.GiaSpDvCongIchCt.Where(t => t.Mahs == Mahs);
            model.GiaSpDvCongIchCt = modelct.ToList();
            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            ViewData["Title"] = "Chi tiết giá sản phẩm dịch vụ công ích";
            return View("Views/Admin/Manages/DinhGia/GiaSpDvCongIch/DanhSach/Show.cshtml", model);
        }
    }
}
