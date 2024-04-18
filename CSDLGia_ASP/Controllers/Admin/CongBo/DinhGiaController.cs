using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
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

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMatDatMatNuoc> model = _db.GiaThueMatDatMatNuoc.Where(t => t.Trangthai == "CB");

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
            ViewData["DsNhom"] = _db.GiaThueMatDatMatNuocNhom;
            ViewData["Title"] = "Chi tiết giá thuê mặt đất mặt nước";
            return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/Show.cshtml", model);
        }

        [Route("CongBo/GiaRung")]
        [HttpGet]
        public IActionResult GiaRung(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaRung> model = _db.GiaRung.Where(t => t.Trangthai == "CB");

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

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMuaNhaXh> model = _db.GiaThueMuaNhaXh.Where(t => t.Trangthai == "CB");

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

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaTaiSanCong> model = _db.GiaTaiSanCong.Where(t => t.Trangthai == "CB");

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

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaGiaoDichBDS> model = _db.GiaGiaoDichBDS.Where(t => t.Trangthai == "CB");

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

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaNuocSh> model = _db.GiaNuocSh.Where(t => t.Trangthai == "CB");

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

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaThueTaiSanCong> model = _db.GiaThueTaiSanCong.Where(t => t.Trangthai == "CB");

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
            var model_new = new VMDinhGiaThueTsc
            {
                Madiaban = model.Madiaban,
                Soqd = model.Soqd,
                Madv = model.Madv,
                Macqcq = model.Macqcq,
                Thoidiem = model.Thoidiem,
                Thongtin = model.Thongtin,
            };

            var model_ct = _db.GiaThueTaiSanCongCt.Where(t => t.Mahs == Mahs);

            model_new.GiaThueTaiSanCongCt = model_ct.ToList();
            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            ViewData["GiaThueTaiSanCongDm"] = _db.GiaThueTaiSanCongDm.ToList();
            ViewData["Title"] = " Thông tin hồ sơ thuê tài sản công";
            return View("Views/Admin/Manages/DinhGia/GiaThueTsc/Show.cshtml", model_new);
        }

        [Route("CongBo/GiaSpDvCi")]
        [HttpGet]
        public IActionResult GiaSpDvCi(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCongIch> model = _db.GiaSpDvCongIch.Where(t => t.Trangthai == "CB");

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

            ViewData["GiaSpDvCongIchNhom"] = _db.GiaSpDvCongIchNhom;
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

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaDvGdDt> model = _db.GiaDvGdDt.Where(t => t.Trangthai == "CB");

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

        [Route("CongBo/GiaDvGiaoDucDaoTao/Show")]
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

        [Route("CongBo/GiaDvKcb")]
        [HttpGet]
        public IActionResult GiaDvKcb(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaDvKcb> model = _db.GiaDvKcb.Where(t => t.Trangthai == "CB");

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
            ViewData["Title"] = "Công bố giá dịch vụ khám chữa bệnh";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_dgcb";
            ViewData["MenuLv3"] = "menu_giadvkcbcb";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/DinhGia/GiaDvKcb.cshtml", model);
        }

        [Route("CongBo/GiaDvKcb/Show")]
        [HttpGet]
        public IActionResult GiaDvKcbShow(string Mahs)
        {
            var model = _db.GiaDvKcb.FirstOrDefault(t => t.Mahs == Mahs);
            var model_new = new VMDinhGiaDvKcb
            {
                Madv = model.Madv,
                Mahs = model.Mahs,
                Manhom = model.Manhom,
                Soqd = model.Soqd,
                Thoidiem = model.Thoidiem,
                Mota = model.Mota

            };

            var model_ct = _db.GiaDvKcbCt.Where(t => t.Mahs == model_new.Mahs);

            model_new.GiaDvKcbCt = model_ct.ToList();

            var groupmanhom = _db.GiaDvKcbCt.Where(t => t.Mahs == model.Mahs).Select(item => item.Manhom).Distinct().ToList();
            ViewData["GroupMaNhom"] = groupmanhom;
            ViewData["GiaDvKcbNhom"] = _db.GiaDvKcbNhom.ToList();
            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            ViewData["Title"] = "Xem chi tiết giá dịch vụ khám chữa bệnh";
            return View("Views/Admin/Manages/DinhGia/GiaDvKhamChuaBenh/Show.cshtml", model_new);
        }

        [Route("CongBo/GiaTroGiaTroCuoc")]
        [HttpGet]
        public IActionResult GiaTroGiaTroCuoc(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaTroGiaTroCuoc> model = _db.GiaTroGiaTroCuoc.Where(t => t.Trangthai == "CB");

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
            ViewData["Title"] = "Công bố giá trợ giá trợ cước";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_dgcb";
            ViewData["MenuLv3"] = "menu_giatrogiatrocuoccb";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/DinhGia/GiaTroGiaTroCuoc.cshtml", model);
        }

        [Route("CongBo/GiaTroGiaTroCuoc/Show")]
        [HttpGet]
        public IActionResult GiaTroGiaTroCuocShow(string Mahs)
        {
            var model = _db.GiaTroGiaTroCuoc.FirstOrDefault(t => t.Mahs == Mahs);
            var model_new = new VMDinhGiaTroGiaTroCuoc
            {
                Madiaban = model.Madiaban,
                Soqd = model.Soqd,
                Thoidiem = model.Thoidiem,
                Thongtin = model.Thongtin,
            };
            var model_ct = _db.GiaTroGiaTroCuocCt.Where(t => t.Mahs == Mahs);

            model_new.GiaTroGiaTroCuocCt = model_ct.ToList();

            ViewData["GiaTroGiaTroCuocDm"] = _db.GiaTroGiaTroCuocDm.ToList();
            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            ViewData["Title"] = "Xem chi tiết giá trợ giá trợ cước";

            return View("Views/Admin/Manages/DinhGia/GiaTroGiaTroCuoc/Show.cshtml", model_new);
        }


        [Route("CongBo/GiaThueTaiNguyen")]
        [HttpGet]
        public IActionResult GiaThueTaiNguyen(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaThueTaiNguyen> model = _db.GiaThueTaiNguyen.Where(t => t.Trangthai == "CB");

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
            ViewData["Title"] = "Công bố giá thuế tài nguyên";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_dgcb";
            ViewData["MenuLv3"] = "menu_giathuetncb";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/DinhGia/GiaThueTaiNguyen.cshtml", model);
        }

        [Route("CongBo/GiaThueTaiNguyen/Show")]
        [HttpGet]
        public IActionResult GiaThueTaiNguyenShow(string Mahs)
        {
            var model = _db.GiaThueTaiNguyen.FirstOrDefault(t => t.Mahs == Mahs);
            model.GiaThueTaiNguyenCt = _db.GiaThueTaiNguyenCt.Where(t => t.Mahs == model.Mahs).ToList();
            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            ViewData["Title"] = "Xem chi tiết giá thuế tài nguyên";
            return View("Views/Admin/Manages/DinhGia/GiaThueTaiNguyen/DanhSach/Show.cshtml", model);
        }


        [Route("CongBo/GiaHangHoaTaiSieuThi")]
        [HttpGet]
        public IActionResult GiaHangHoaTaiSieuThi(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaHangHoaTaiSieuThi> model = _db.GiaHangHoaTaiSieuThi.Where(t => t.Trangthai == "CB");

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
            ViewData["Title"] = "Công bố giá hàng hóa tại siêu thị";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_dgcb";
            ViewData["MenuLv3"] = "menu_giahhstcb";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/DinhGia/GiaHangHoaTaiSieuThi.cshtml", model);
        }

        [Route("CongBo/GiaHangHoaTaiSieuThi/Show")]
        [HttpGet]
        public IActionResult GiaHangHoaTaiSieuThiShow(string Mahs)
        {
            var model = _db.GiaHangHoaTaiSieuThi.FirstOrDefault(t => t.Mahs == Mahs);

            var model_ct = _db.GiaHangHoaTaiSieuThiCt.Where(t => t.Mahs == model.Mahs);

            model.GiaHangHoaTaiSieuThiCt = model_ct.ToList();

            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["Title"] = "Xem chi tiết hàng hóa tại siêu thị";
            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            return View("Views/Admin/Manages/DinhGia/GiaHangHoaTaiSieuThi/Show.cshtml", model);
        }

        [Route("CongBo/GiaSpDvCuThe")]
        [HttpGet]
        public IActionResult GiaSpDvCuThe(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCuThe> model = _db.GiaSpDvCuThe.Where(t => t.Trangthai == "CB");

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
            ViewData["Title"] = "Công bố giá sản phẩm dịch vụ cụ thể";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_spdvcuthecb";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/DinhGia/GiaSpDvCuThe.cshtml", model);
        }

        [Route("CongBo/GiaSpDvCuThe/Show")]
        [HttpGet]
        public IActionResult GiaSpDvCuTheShow(string Mahs)
        {
            var model = _db.GiaSpDvCuThe.FirstOrDefault(t => t.Mahs == Mahs);
            var model_ct = _db.GiaSpDvCuTheCt.Where(t => t.Mahs == model.Mahs);
            model.GiaSpDvCuTheCt = model_ct.ToList();
            ViewData["Madv"] = model.Madv;
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["Ipf1"] = model.Ipf1;
            ViewData["Title"] = "Xem chi tiết sản phẩm dịch vụ cụ thể";
            return View("Views/Admin/Manages/DinhGia/GiaSpDvCuThe/Show.cshtml", model);
        }

        [Route("CongBo/GiaSpDvKhungGia")]
        [HttpGet]
        public IActionResult GiaSpDvKhungGia(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvKhungGia> model = _db.GiaSpDvKhungGia.Where(t => t.Trangthai == "CB");

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
            ViewData["Title"] = "Công bố giá sản phẩm dịch vụ khung giá";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_spdvkhunggiacb";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/DinhGia/GiaSpDvKhungGia.cshtml", model);
        }

        [Route("CongBo/GiaSpDvKhungGia/Show")]
        [HttpGet]
        public IActionResult GiaSpDvKhungGiaShow(string Mahs)
        {
            var model = _db.GiaSpDvKhungGia.FirstOrDefault(t => t.Mahs == Mahs);
            model.GiaSpDvKhungGiaCt = _db.GiaSpDvKhungGiaCt.Where(t => t.Mahs == model.Mahs).ToList();
            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            ViewData["Title"] = "Xem chi tiết sản phẩm dịch vụ khung giá";
            return View("Views/Admin/Manages/DinhGia/GiaSpDvKhungGia/Show.cshtml", model);
        }

        [Route("CongBo/GiaSpDvToiDa")]
        [HttpGet]
        public IActionResult GiaSpDvToiDa(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvToiDa> model = _db.GiaSpDvToiDa.Where(t => t.Trangthai == "CB");

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
            ViewData["Title"] = "Công bố giá sản phẩm dịch vụ tối đa";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_spdvtoidacb";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/DinhGia/GiaSpDvToiDa.cshtml", model);
        }

        [Route("CongBo/GiaSpDvToiDa/Show")]
        [HttpGet]
        public IActionResult GiaSpDvToiDaShow(string Mahs)
        {
            var model = _db.GiaSpDvToiDa.FirstOrDefault(t => t.Mahs == Mahs);
            model.GiaSpDvToiDaCt = _db.GiaSpDvToiDaCt.Where(t => t.Mahs == model.Mahs).ToList();
            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            ViewData["Title"] = "Xem chi tiết hàng hóa dịch vụ tối đa";
            return View("Views/Admin/Manages/DinhGia/GiaSpDvToiDa/Show.cshtml", model);
        }


    }
}
