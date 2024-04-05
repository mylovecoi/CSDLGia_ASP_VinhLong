using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaGiaoDichBDS
{ 
    public class GiaGiaoDichBDSCongBoController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaGiaoDichBDSCongBoController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaGiaoDichBDS/CongBo")]
        [HttpGet]
        public IActionResult Index(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaGiaoDichBDS> model = _db.GiaGiaoDichBDS.Where(t => t.Congbo == "DACONGBO");

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
            ViewData["Title"] = "Công bố giá giao dịch bất động sản";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_dgcb";
            ViewData["MenuLv3"] = "menu_giagdbdscb";
            ViewBag.bSession = false;
            return View("Views/Admin/Systems/CongBo/GiaGiaoDichBDSCongBo.cshtml", model);
        }

        [Route("GiaGiaoDichBDS/CongBo/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            var model = _db.GiaGiaoDichBDS.FirstOrDefault(t => t.Mahs == Mahs);
            var modelct = _db.GiaGiaoDichBDSCt.Where(t => t.Mahs == Mahs);
            model.GiaGiaoDichBDSCt = modelct.ToList();
            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            var donvi = _db.DsDonVi.First(x => x.MaDv == model.Madv);
            ViewData["DanhMucNhom"] = _db.GiaGiaoDichBDSNhom;
            ViewData["TenDiaBan"] = _db.DsDiaBan.First(x => x.MaDiaBan == donvi.MaDiaBan).TenDiaBan;
            ViewData["TenDonVi"] = donvi.TenDv;
            ViewData["Title"] = "Chi tiết giá giao dịch bất động sản";
            return View("Views/Admin/Manages/DinhGia/GiaGiaoDichBDS/DanhSach/Show.cshtml", model);
        }
    }
}

