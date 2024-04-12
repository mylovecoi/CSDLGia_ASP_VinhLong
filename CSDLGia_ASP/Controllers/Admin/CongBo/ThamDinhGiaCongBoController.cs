using CSDLGia_ASP.Database;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.CongBo
{
    public class ThamDinhGiaCongBoController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public ThamDinhGiaCongBoController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("CongBo/ThamDinhGia")]
        [HttpGet]
        public IActionResult ThamDinhGia(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.ThamDinhGia.ThamDinhGia> model = _db.ThamDinhGia.Where(t => t.Congbo == "DACONGBO");

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
            ViewData["Title"] = "Công bố thẩm định giá";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_tdgcb";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/ThamDinhGia/ThamDinhGia.cshtml", model);
        }

        [Route("CongBo/ThamDinhGia/Show")]
        [HttpGet]
        public IActionResult ThamDinhGiaShow(string Mahs)
        {
            var model = _db.ThamDinhGia.FirstOrDefault(t => t.Mahs == Mahs);

            var model_ct = _db.ThamDinhGiaCt.Where(t => t.Mahs == model.Mahs);

            model.ThamDinhGiaCt = model_ct.ToList();

            ViewData["Mahs"] = model.Mahs;
            ViewData["Madv"] = model.Madv;
            ViewData["TdgDonvi"] = _db.ThamDinhGiaDv.ToList();
            ViewData["TdgDmHh"] = _db.ThamDinhGiaDmHh.ToList();
            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            ViewData["Dvt"] = _db.DmDvt.ToList();
            ViewData["Title"] = "Xem chi tiết hồ sơ thẩm định giá";
            return View("Views/Admin/Manages/ThamDinhGia/DanhSach/Show.cshtml", model);
        }

        [Route("CongBo/DonViThamDinhGia")]
        [HttpGet]
        public IActionResult DonViThamDinhGia(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.ThamDinhGia.ThamDinhGiaDv> model = _db.ThamDinhGiaDv;           

            ViewData["DsDonVi"] = _db.DsDonVi;
            ViewData["DsDiaBan"] = _db.DsDiaBan;
            ViewData["Madv"] = Madv;
            ViewData["Nam"] = Nam;
            ViewData["Title"] = "Công bố thông tin đơn vị thẩm định giá";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_dvtdgcb";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/ThamDinhGia/DonViThamDinhGia.cshtml", model);
        }
    }
}
