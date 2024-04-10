using CSDLGia_ASP.Database;
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
            ViewData["Title"] = "Công bố giá dịch ";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_dgcb";
            ViewData["MenuLv3"] = "menu_giathuedncb";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/DinhGia/GiaThueMatDatMatNuoc.cshtml", model);
        }

        [Route("CongBo/DichVuLuuTru/Show")]
        [HttpGet]
        public IActionResult DichVuLuuTruShow(string Mahs)
        {
            var model = _db.GiaThueMatDatMatNuoc.FirstOrDefault(t => t.Mahs == Mahs);
            var modelct = _db.GiaThueMatDatMatNuocCt.Where(t => t.Mahs == Mahs);
            model.GiaThueMatDatMatNuocCt = modelct.ToList();
            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            ViewData["DsNhom"] = _db.GiaThueMatDatMatNuocNhom;
            ViewData["Title"] = "Chi tiết giá thuê mặt đất mặt nước";
            return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/Show.cshtml", model);
        }
    }
}
