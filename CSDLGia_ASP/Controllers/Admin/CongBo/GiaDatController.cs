using CSDLGia_ASP.Database;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.CongBo
{
    public class GiaDatController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaDatController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("CongBo/GiaDatCuThe")]
        [HttpGet]
        public IActionResult GiaDatCuThe(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaDatPhanLoai> model = _db.GiaDatPhanLoai.Where(t => t.Congbo == "DACONGBO");

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
            ViewData["Title"] = "Công bố giá đất cụ thể";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_giadatcb";
            ViewData["MenuLv3"] = "menu_giadatcuthecongbo";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/GiaDat/GiaDatCuThe.cshtml", model);
        }

        [Route("CongBo/GiaDatCuThe/Show")]
        [HttpGet]
        public IActionResult GiaDatCuTheShow(string Mahs)
        {
            var model = _db.GiaDatPhanLoai.FirstOrDefault(t => t.Mahs == Mahs);
            var model_new = new VMDinhGiaDat
            {
                Madv = model.Madv,
                Mahs = model.Mahs,
                Madiaban = model.Madiaban,
                Soqd = model.Soqd,
                Thoidiem = model.Thoidiem,
                Thongtin = model.Thongtin,
                Ghichu = model.Ghichu,
                Phanloai = model.Phanloai,

            };

            var model_ct = _db.GiaDatPhanLoaiCt.Where(t => t.Mahs == model_new.Mahs);

            model_new.GiaDatPhanLoaiCt = model_ct.ToList();

            ViewData["Madv"] = model.Madv;
            ViewData["Mahs"] = model.Mahs;
            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            ViewData["Dmloaidat"] = _db.DmLoaiDat.ToList();
            ViewData["Title"] = "Xem chi tiết giá đất cụ thể";
            return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Show.cshtml", model_new);
        }

        [Route("CongBo/GiaKhungGiaDat")]
        [HttpGet]
        public IActionResult KhungGiaDat(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaKhungGiaDat> model = _db.GiaKhungGiaDat.Where(t => t.Congbo == "DACONGBO");

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
            ViewData["Title"] = "Công bố khung giá đất";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_giadatcb";
            ViewData["MenuLv3"] = "menu_khunggiadat";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/GiaDat/GiaKhungGiaDat.cshtml", model);
        }

        [Route("CongBo/KhungGiaDat/Show")]
        [HttpGet]
        public IActionResult KhungGiaDatShow(string Mahs)
        {
            var model = _db.GiaKhungGiaDat.FirstOrDefault(t => t.Mahs == Mahs);
            model.GiaKhungGiaDatCt = _db.GiaKhungGiaDatCt.Where(t => t.Mahs == model.Mahs).ToList();
            var DonVi = _db.DsDonVi.First(x => x.MaDv == model.Madv);

            ViewData["Title"] = "Xem chi tiết khung giá đất";
            ViewData["TenDonVi"] = DonVi.TenDv;
            ViewData["TenDiaBan"] = _db.DsDiaBan.First(x => x.MaDiaBan == DonVi.MaDiaBan).TenDiaBan;
            return View("Views/Admin/Manages/DinhGia/GiaKhungGiaDat/DanhSach/Show.cshtml", model);
        }

        [Route("CongBo/BangGiaDat")]
        [HttpGet]
        public IActionResult BangGiaDat(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaDatPhanLoai> model = _db.GiaDatPhanLoai.Where(t => t.Congbo == "DACONGBO");

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
            ViewData["Title"] = "Công bố bảng giá đất";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_giadatcb";
            ViewData["MenuLv3"] = "menu_banggiadat";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/GiaDat/BangGiaDat.cshtml", model);
        }

        [Route("CongBo/BangGiaDat/Show")]
        [HttpGet]
        public IActionResult BangGiaDatShow(string Mahs)
        {
            var model = _db.GiaDatDiaBan.FirstOrDefault(t => t.Mahs == Mahs);
            var model_ct = (from dat in _db.GiaDatDiaBanCt.Where(t => t.Mahs == Mahs)
                            join dm in _db.DmLoaiDat on dat.Maloaidat equals dm.Maloaidat
                            select new CSDLGia_ASP.Models.Manages.DinhGia.GiaDatDiaBanCt
                            {
                                Id = dat.Id,
                                HienThi = dat.HienThi,
                                Maloaidat = dat.Maloaidat,
                                Mota = dat.Mota,
                                Diemdau = dat.Diemdau,
                                Diemcuoi = dat.Diemcuoi,
                                Loaiduong = dat.Loaiduong,
                                Hesok = dat.Hesok,
                                Giavt1 = dat.Giavt1,
                                Giavt2 = dat.Giavt2,
                                Giavt3 = dat.Giavt3,
                                Giavt4 = dat.Giavt4,
                                Giavt5 = dat.Giavt5,
                                Loaidat = dm.Loaidat,
                                Sapxep = dat.Sapxep
                            });
            model.GiaDatDiaBanCt = model_ct.ToList();
            var huyen = _db.Districts.FirstOrDefault(x => x.Mahuyen == model.MaHuyen);
            var xa = _db.Towns.FirstOrDefault(x => x.Maxa == model.MaXa && x.Mahuyen == model.MaHuyen);
            ViewData["TenHuyen"] = huyen == null ? "" : huyen.Tenhuyen;
            ViewData["TenXa"] = xa == null ? "" : xa.Tenxa;
            ViewData["DsXaPhuong"] = _db.DsXaPhuong.ToList();
            ViewData["Dsloaidat"] = _db.DmLoaiDat.ToList();
            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            ViewData["DsDiaBanHuyen"] = _db.DsDiaBan.Where(t => t.Level == "H");
            ViewData["Madv"] = model.Madv;
            ViewData["Title"] = "Xem chi tiết bảng giá đất";
            return View("Views/Admin/Manages/DinhGia/GiaDatDiaBan/Show.cshtml", model);
        }

        [Route("CongBo/GiaGiaoDichDat")]
        [HttpGet]
        public IActionResult GiaGiaoDichDat(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaDatPhanLoai> model = _db.GiaDatPhanLoai.Where(t => t.Congbo == "DACONGBO");

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
            ViewData["Title"] = "Công bố giá giao dịch đất";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_giadatcb";
            ViewData["MenuLv3"] = "menu_giagiaodichdat";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/GiaDat/GiaGiaoDichDat.cshtml", model);
        }

        [Route("CongBo/GiaGiaoDichDat/Show")]
        [HttpGet]
        public IActionResult GiaGiaoDichDatShow(string Mahs)
        {
            var model = _db.GiaGiaoDichDat.FirstOrDefault(t => t.Mahs == Mahs);
            model.GiaGiaoDichDatCt = _db.GiaGiaoDichDatCt.Where(t => t.Mahs == model.Mahs).ToList();
            var donvi = _db.DsDonVi.First(x => x.MaDv == model.Madv);
            ViewData["Title"] = "Xem chi tiết giao dịch đất thực tế trên thị trường";
            ViewData["TenDiaBan"] = _db.DsDiaBan.First(x => x.MaDiaBan == donvi.MaDiaBan).TenDiaBan;
            ViewData["TenDonVi"] = donvi.TenDv;
            ViewData["DanhMucNhom"] = _db.GiaGiaoDichDatNhom;
            return View("Views/Admin/Manages/DinhGia/GiaGiaoDichDat/DanhSach/Show.cshtml", model);
        }

        [Route("CongBo/GiaTrungThauDat")]
        [HttpGet]
        public IActionResult GiaTrungThauDat(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaDatPhanLoai> model = _db.GiaDatPhanLoai.Where(t => t.Congbo == "DACONGBO");

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
            ViewData["Title"] = "Công bố giá trúng thầu đất";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_giadatcb";
            ViewData["MenuLv3"] = "menu_giatrungthaudat";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/GiaDat/GiaTrungThauDat.cshtml", model);
        }

        [Route("CongBo/GiaTrungThauDat/Show")]
        [HttpGet]
        public IActionResult GiaTrungThauDatShow(string Mahs)
        {

            var model = _db.GiaDauGiaDat.FirstOrDefault(t => t.Mahs == Mahs);
            var model_new = new GiaDauGiaDat
            {
                Madv = model.Madv,
                Mahs = model.Mahs,
                Madiaban = model.Madiaban,
                Thoidiem = model.Thoidiem,
                Thongtin = model.Thongtin,
                Tenduan = model.Tenduan,
                Soqddaugia = model.Soqddaugia,
                Soqdgiakhoidiem = model.Soqdgiakhoidiem,
                Soqdkqdaugia = model.Soqdkqdaugia,
                Soqdpagia = model.Soqdpagia,
                Phanloai = model.Phanloai,
                Maxp = model.Maxp,

            };
            var model_ct = _db.GiaDauGiaDatCt.Where(t => t.Mahs == Mahs);
            model_new.GiaDauGiaDatCt = model_ct.ToList();
            var donvi = _db.DsDonVi.First(t => t.MaDv == model.Madv);
            ViewData["TenDiaBan"] = _db.DsDiaBan.First(x => x.MaDiaBan == model.Madiaban).TenDiaBan; 
            ViewData["TenDonVi"] = donvi.TenDv;
            ViewData["XaPhuong"] = _db.DsXaPhuong.FirstOrDefault(x => x.Maxp == model.Maxp).Tenxp;
            return View("Views/Admin/Manages/DinhGia/GiaTrungThauDat/DanhSach/Show.cshtml", model_new);
        }


    }
}
