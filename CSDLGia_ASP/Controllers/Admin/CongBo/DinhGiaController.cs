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
            ViewData["MenuLv2"] = "menu_dgcb";
            ViewData["MenuLv3"] = "menu_giathuedncb";
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
            ViewData["MenuLv2"] = "menu_dgcb";
            ViewData["MenuLv3"] = "menu_giarungcb";
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
            ViewData["MenuLv2"] = "menu_dgcb";
            ViewData["MenuLv3"] = "menu_giathuemuanhaxhcb";
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

        [Route("CongBo/GiaTaiSanCong")]
        [HttpGet]
        public IActionResult GiaTaiSanCong(string Madv, int Nam)
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

            ViewData["DsDonVi"] = _db.DsDonVi;
            ViewData["DsDiaBan"] = _db.DsDiaBan;
            ViewData["Madv"] = Madv;
            ViewData["Nam"] = Nam;
            ViewData["Title"] = "Công bố giá tài sản công";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_dgcb";
            ViewData["MenuLv3"] = "menu_giatscongcb";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/DinhGia/GiaTaiSanCong.cshtml", model);
        }

        [Route("CongBo/GiaTaiSanCong/Show")]
        [HttpGet]
        public IActionResult GiaTaiSanCongShow(string Mahs)
        {
            var model = _db.GiaTaiSanCong.FirstOrDefault(t => t.Mahs == Mahs);
            var modelct = _db.GiaTaiSanCongCt.Where(t => t.Mahs == Mahs);
            model.GiaTaiSanCongCt = modelct.ToList();
            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            ViewData["Title"] = "Chi tiết giá tài sản công";
            return View("Views/Admin/Manages/DinhGia/GiaTaiSanCong/DanhSach/Show.cshtml", model);
        }

        [Route("CongBo/GiaGiaoDichBDS")]
        [HttpGet]
        public IActionResult GiaGiaoDichBDS(string Madv, int Nam)
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
            return View("Views/Admin/CongBo/DinhGia/GiaGiaoDichBDS.cshtml", model);
        }

        [Route("CongBo/GiaGiaoDichBDS/Show")]
        [HttpGet]
        public IActionResult GiaGiaoDichBDSShow(string Mahs)
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
            ViewData["MenuLv2"] = "menu_dgcb";
            ViewData["MenuLv3"] = "menu_giancshcb";
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

        [Route("CongBo/GiaThueTaiSanCong")]
        [HttpGet]
        public IActionResult GiaThueTaiSanCong(string Madv, int Nam)
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
            return View("Views/Admin/CongBo/DinhGia/GiaThueTsCong.cshtml", model);
        }

        [Route("CongBo/GiaThueTaiSanCong/Show")]
        [HttpGet]
        public IActionResult GiaThueTaiSanCongShow(string Mahs)
        {
            var model = _db.GiaThueTaiSanCong.FirstOrDefault(t => t.Mahs == Mahs);
            var modelct = _db.GiaThueTaiSanCongCt.Where(t => t.Mahs == Mahs);
            model.GiaThueTaiSanCongCt = modelct.ToList();
            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            ViewData["Title"] = "Chi tiết giá thuê tài sản công";
            return View("Views/Admin/Manages/DinhGia/GiaThueTsc/Show.cshtml", model);
        }

        [Route("CongBo/GiaSpDvCi")]
        [HttpGet]
        public IActionResult GiaSpDvCi(string Madv, int Nam)
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
            ViewData["MenuLv2"] = "menu_dgcb";
            ViewData["MenuLv3"] = "menu_giaspdvcicb";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/DinhGia/GiaSpDvCi.cshtml", model);
        }

        [Route("CongBo/GiaSpDvCi/Show")]
        [HttpGet]
        public IActionResult GiaSpDvCiShow(string Mahs)
        {
            var model = _db.GiaSpDvCongIch.FirstOrDefault(t => t.Mahs == Mahs);
            var modelct = _db.GiaSpDvCongIchCt.Where(t => t.Mahs == Mahs);
            model.GiaSpDvCongIchCt = modelct.ToList();
            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            ViewData["Title"] = "Chi tiết giá sản phẩm dịch vụ công ích";
            return View("Views/Admin/Manages/DinhGia/GiaSpDvCongIch/Show.cshtml", model);
        }

        [Route("CongBo/GiaDvGiaoDucDaoTao")]
        [HttpGet]
        public IActionResult GiaDvGiaoDucDaoTao(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaDvGdDt> model = _db.GiaDvGdDt.Where(t => t.Congbo == "DACONGBO");

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
            ViewData["Title"] = "Công bố giá dịch vụ giáo dục đào tạo";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_dgcb";
            ViewData["MenuLv3"] = "menu_giagddtcb";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/DinhGia/GiaDvGiaoDucDaoTao.cshtml", model);
        }

        [Route("CongBo/GiaDichVuDaoTao/Show")]
        [HttpGet]
        public IActionResult GiaDvGiaoDucDaoTaoShow(string Mahs)
        {
            var model = _db.GiaDvGdDt.FirstOrDefault(t => t.Mahs == Mahs);
            model.GiaDvGdDtCt = _db.GiaDvGdDtCt.Where(t => t.Mahs == model.Mahs).ToList();

            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            ViewData["Title"] = "Xem chi tiết giá dịch vụ giáo dục đào tạo";
            return View("Views/Admin/Manages/DinhGia/GiaoDucDaoTao/Show.cshtml", model);
        }
    }
}
