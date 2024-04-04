using CSDLGia_ASP.Database;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaTaiSanCong
{
    public class TaiSanCongCbController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public TaiSanCongCbController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaTaiSanCong/CongBo")]
        [HttpGet]
        public IActionResult Index(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaTaiSanCong> model = _db.GiaTaiSanCong.Where(t => t.Congbo == "DACONGBO");

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
            ViewData["Title"] = "Công bố giá tài sản công";
            ViewBag.bSession = true;
            return View("Views/Admin/Manages/DinhGia/GiaTaiSanCong/CongBo/Index.cshtml", model);
        }

        [Route("GiaTaiSanCong/CongBo/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            var model = _db.GiaTaiSanCong.FirstOrDefault(t => t.Mahs == Mahs);
            var modelct = _db.GiaTaiSanCongCt.Where(t => t.Mahs == Mahs);
            model.GiaTaiSanCongCt = modelct.ToList();
            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            ViewData["Title"] = "Chi tiết giá tài sản công";
            return View("Views/Admin/Manages/DinhGia/GiaTaiSanCong/DanhSach/Show.cshtml", model);
        }
    }
}
