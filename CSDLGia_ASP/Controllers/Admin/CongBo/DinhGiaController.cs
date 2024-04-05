using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.CongBo
{
    public class DinhGiaController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public DinhGiaController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("CongBo/GiaThueMatDatMatNuoc")]
        [HttpGet]
        public IActionResult GiaThueMatDatMatNuoc(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMatDatMatNuoc> model = _db.GiaThueMatDatMatNuoc.Where(t => t.Congbo == "DACONGBO");

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
            ViewData["Title"] = "Công bố giá thuê mặt đất mặt nước";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_dg";
            ViewData["MenuLv3"] = "menu_giathuedn";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/DinhGia/GiaThueMatDatMatNuoc.cshtml", model);
        }

        [Route("CongBo/GiaThueMatDatMatNuoc/Show")]
        [HttpGet]
        public IActionResult GiaThueMatDatMatNuocShow(string Mahs)
        {
            var model = _db.GiaThueMatDatMatNuoc.FirstOrDefault(t => t.Mahs == Mahs);
            var modelct = _db.GiaThueMatDatMatNuocCt.Where(t => t.Mahs == Mahs);
            model.GiaThueMatDatMatNuocCt = modelct.ToList();
            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            ViewData["Title"] = "Chi tiết giá thuê mặt dất mặt nước";
            return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/Show.cshtml", model);
        }

        [Route("CongBo/GiaRung")]
        [HttpGet]
        public IActionResult GiaRung(string Madv, int Nam)
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

            ViewData["DsDonVi"] = _db.DsDonVi;
            ViewData["DsDiaBan"] = _db.DsDiaBan;
            ViewData["Madv"] = Madv;
            ViewData["Nam"] = Nam;
            ViewData["Title"] = "Công bố giá rừng";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_dg";
            ViewData["MenuLv3"] = "menu_giarung";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/DinhGia/GiaRung.cshtml", model);
        }

        [Route("CongBo/GiaRung/Show")]
        [HttpGet]
        public IActionResult GiaRungShow(string Mahs)
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

        [Route("CongBo/GiaThueMuaNhaXh")]
        [HttpGet]
        public IActionResult GiaThueMuaNhaXh(string Madv, int Nam)
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

            ViewData["DsDonVi"] = _db.DsDonVi;
            ViewData["DsDiaBan"] = _db.DsDiaBan;
            ViewData["Madv"] = Madv;
            ViewData["Nam"] = Nam;
            ViewData["Title"] = "Công bố giá thuê mua nhà xã hội";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_dg";
            ViewData["MenuLv3"] = "menu_giathuemuanhaxh";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/DinhGia/GiaThueMuaNhaXh.cshtml", model);
        }

        [Route("CongBo/GiaThueMuaNhaXh/Show")]
        [HttpGet]
        public IActionResult GiaThueMuaNhaXhShow(string Mahs)
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

        [Route("CongBo/GiaNuocSh")]
        [HttpGet]
        public IActionResult GiaNuocSh(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaNuocSh> model = _db.GiaNuocSh.Where(t => t.Congbo == "DACONGBO");

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
            ViewData["Title"] = "Công bố giá nước sinh hoạt";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_dg";
            ViewData["MenuLv3"] = "menu_giancsh";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/DinhGia/GiaNuocSh.cshtml", model);
        }

        [Route("CongBo/GiaNuocSh/Show")]
        [HttpGet]
        public IActionResult GiaNuocShShow(string Mahs)
        {
            var model = _db.GiaNuocSh.FirstOrDefault(t => t.Mahs == Mahs);
            model.GiaNuocShCt = _db.GiaNuocShCt.Where(t => t.Mahs == model.Mahs).ToList();

            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            ViewData["Title"] = "Xem chi tiết giá nước sinh hoạt";
            return View("Views/Admin/Manages/DinhGia/GiaNuocSh/Show.cshtml", model);
        }
    }
}
