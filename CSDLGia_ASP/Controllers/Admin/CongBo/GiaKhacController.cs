using CSDLGia_ASP.Database;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.CongBo
{
    public class GiaKhacController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaKhacController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("CongBo/TrungThauHangHoaDichVu")]
        [HttpGet]
        public IActionResult TrungThauHangHoaDichVu(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaMuaTaiSan> model = _db.GiaMuaTaiSan.Where(t => t.Trangthai == "CB");

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
            ViewData["Title"] = "Công bố trúng thầu hàng hóa dịch vụ";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_giatrungthauhanghoadichvucb";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/GiaKhac/TrungThauHangHoaDichVu.cshtml", model);
        }

        [Route("CongBo/TrungThauHangHoaDichVu/Show")]
        [HttpGet]
        public IActionResult TrungThauHangHoaDichVuShow(string Mahs)
        {
            var model = _db.GiaMuaTaiSan.FirstOrDefault(t => t.Mahs == Mahs);
            model.GiaMuaTaiSanCt = _db.GiaMuaTaiSanCt.Where(t => t.Mahs == Mahs).ToList();
            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level == "T");
            ViewData["Title"] = " Chi tiết hồ sơ giá trúng thầu hàng hóa dịch vụ";
            return View("Views/Admin/Manages/DinhGia/GiaTrungThauHhDv/DanhSach/Show.cshtml", model);
        }

        [Route("CongBo/HangHoaDichVuKhac")]
        [HttpGet]
        public IActionResult HangHoaDichVuKhac(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvk> model = _db.GiaHhDvk.Where(t => t.Trangthai == "CB");

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
            ViewData["Title"] = "Công bố giá hàng hóa dịch vụ khác";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_hhdvkcb";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/GiaKhac/GiaHangHoaDichVuKhac.cshtml", model);
        }

        [Route("CongBo/HangHoaDichVuKhac/Show")]
        [HttpGet]
        public IActionResult HangHoaDichVuKhacShow(string Mahs)
        {
            var nhomhh = _db.DmNhomHh.Where(t => t.Phanloai == "GIAHHDVK");
            var model = _db.GiaHhDvk.FirstOrDefault(t => t.Mahs == Mahs);
            model.GiaHhDvkCt = (from ct in _db.GiaHhDvkCt.Where(t => t.Mahs == model.Mahs)
                                join dm in _db.GiaHhDvkDm.Where(x => x.Matt == model.Matt) on ct.Mahhdv equals dm.Mahhdv
                                select new GiaHhDvkCt
                                {
                                    Id = ct.Id,
                                    Manhom = ct.Manhom,
                                    Mahhdv = ct.Mahhdv,
                                    Mahs = ct.Mahs,
                                    Gia = ct.Gia,
                                    Gialk = ct.Gialk,
                                    Loaigia = ct.Loaigia,
                                    Nguontt = ct.Nguontt,
                                    Ghichu = ct.Ghichu,      
                                    Tenhhdv = dm.Tenhhdv,
                                    Dacdiemkt = dm.Dacdiemkt,
                                    Dvt = dm.Dvt,
                                }).ToList();

            ViewData["Nhomhh"] = nhomhh;
            ViewData["DmDvt"] = _db.DmDvt.ToList();
            ViewData["Title"] = "Thông tin giá hàng hóa dịch vụ khác chi tiết";
            return View("Views/Admin/Manages/DinhGia/GiaHhDvk/DanhSach/Show.cshtml", model);
        }


        [Route("CongBo/GiaHangHoaDichVuChuyenNghanh")]
        [HttpGet]
        public IActionResult GiaHangHoaDichVuChuyenNghanh(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvCn> model = _db.GiaHhDvCn.Where(t => t.Trangthai == "CB");

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
            ViewData["Title"] = "Công bố giá hàng hóa dịch vụ khác theo quy định của pháp luật chuyên ngành";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_giakhaccb";
            ViewData["MenuLv3"] = "menu_giahanghoadichvuchuyennghanhcb";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/GiaKhac/GiaHangHoaDichVuChuyenNghanh.cshtml", model);
        }

        [Route("CongBo/GiaHangHoaDichVuChuyenNghanh/Show")]
        [HttpGet]
        public IActionResult GiaHangHoaDichVuChuyenNghanhShow(string Mahs)
        {
            var model = _db.GiaHhDvCn.FirstOrDefault(t => t.Mahs == Mahs);
            var model_ct = _db.GiaHhDvCnCt.Where(t => t.Mahs == model.Mahs);
            model.GiaHhDvCnCt = model_ct.ToList();
            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            ViewData["Title"] = "Xem chi tiết giá hàng hóa dịch vụ khác theo quy định của pháp luật chuyên ngành";
            return View("Views/Admin/Manages/DinhGia/GiaHhDvCn/Show.cshtml", model);
        }


        [Route("CongBo/GiaLePhiTruocBa")]
        [HttpGet]
        public IActionResult GiaLePhiTruocBa(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaPhiLePhi> model = _db.GiaPhiLePhi.Where(t => t.Trangthai == "CB");

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
            ViewData["Title"] = "Công bố giá lệ phí trước bạ";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_giakhaccb";
            ViewData["MenuLv3"] = "menu_gialephitruocbacb";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/GiaKhac/GiaLePhiTruocBa.cshtml", model);
        }

        [Route("CongBo/GiaLePhiTruocBa/Show")]
        [HttpGet]
        public IActionResult GiaLePhiTruocBaShow(string Mahs)
        {

            var model = _db.GiaPhiLePhi.FirstOrDefault(t => t.Mahs == Mahs);
            var model_new = new GiaPhiLePhi
            {
                Soqd = model.Soqd,
                Thoidiem = model.Thoidiem,
                Thongtin = model.Thongtin,
                Mota = model.Mota,
                Ghichu = model.Ghichu,
            };
            var model_ct = _db.GiaPhiLePhiCt.Where(t => t.Mahs == Mahs);

            model_new.GiaPhiLePhiCt = model_ct.ToList();

            ViewData["DsNhom"] = _db.GiaPhiLePhiNhom;
            ViewData["Title"] = " Thông tin hồ sơ giá lệ phí trước bạ";
            ViewData["MenuLv1"] = "menu_giakhac";
            ViewData["MenuLv2"] = "menu_dglp";
            ViewData["MenuLv3"] = "menu_dglp_tt";


            return View("Views/Admin/Manages/DinhGia/GiaLePhi/DanhSach/Show.cshtml", model_new);
        }

        [Route("CongBo/GiaPhiLePhi")]
        [HttpGet]
        public IActionResult GiaPhiLePhi(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.PhiLePhi> model = _db.PhiLePhi.Where(t => t.Trangthai == "CB");

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
            ViewData["Title"] = "Công bố giá phí lệ phí";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_giakhaccb";
            ViewData["MenuLv3"] = "menu_giaphilephicb";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/GiaKhac/GiaPhiLePhi.cshtml", model);
        }

        [Route("CongBo/GiaPhiLePhi/Show")]
        [HttpGet]
        public IActionResult GiaPhiLePhiShow(string Mahs)
        {
            var model = _db.PhiLePhi.FirstOrDefault(t => t.Mahs == Mahs);
            model.PhiLePhiCt = _db.PhiLePhiCt.Where(t => t.Mahs == model.Mahs).ToList();

            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            ViewData["Title"] = "Bảng giá phí, lệ phí";
            ViewData["MenuLv1"] = "menu_giakhac";
            ViewData["MenuLv2"] = "menu_plp";
            ViewData["MenuLv3"] = "menu_plp_tt";
            if (model.PhanLoaiHoSo == "HOSONHANEXCEL")
            {
                return View("Views/Admin/Manages/DinhGia/PhiLePhi/DanhSach/ShowExcel.cshtml", model);
            }
            return View("Views/Admin/Manages/DinhGia/PhiLePhi/DanhSach/Show.cshtml", model);
        }

        [Route("CongBo/GiaVatLieuXayDung")]
        [HttpGet]
        public IActionResult GiaVatLieuXayDung(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaVatLieuXayDung> model = _db.GiaVatLieuXayDung.Where(t => t.Trangthai == "CB");
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
            ViewData["Title"] = "Công bố giá vật liệu xây dựng";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_giakhaccb";
            ViewData["MenuLv3"] = "menu_giavatlieuxaydungcb";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/GiaKhac/GiaVatLieuXayDung.cshtml", model);
        }

        [Route("CongBo/GiaVatLieuXayDung/Show")]
        [HttpGet]
        public IActionResult GiaVatLieuXayDungShow(string Mahs)
        {
            var model = _db.GiaVatLieuXayDung.FirstOrDefault(t => t.Mahs == Mahs);
            model.GiaVatLieuXayDungCt = _db.GiaVatLieuXayDungCt.Where(t => t.Mahs == model.Mahs).ToList();
            ViewData["Title"] = "Xem chi tiết giá vật liệu xây dựng";
            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            return View("Views/Admin/Manages/DinhGia/GiaVatLieuXayDung/DanhSach/Show.cshtml", model);

        }
    }
}
