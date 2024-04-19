using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.ViewModels.Manages.KeKhaiGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [Route("CongBo/DichVuLuHanh")]
        [HttpGet]
        public IActionResult DichVuLuHanh(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.KeKhaiGia.KkGia> model = _db.KkGia.Where(t => t.Congbo == "DACONGBO" && t.Manghe == "LUHANH");

            if (Madv != "all")
            {
                model = model.Where(t => t.Madv == Madv);
            }


            if (Nam != 0)
            {
                model = model.Where(t => t.Ngaynhap.Year == Nam).ToList();
            }

            var dsdonvi = (from com in _db.Company
                           join lvkd in _db.CompanyLvCc.Where(t => t.Manghe == "LUHANH") on com.Mahs equals lvkd.Mahs
                           select new VMCompany
                           {
                               Id = com.Id,
                               Manghe = lvkd.Manghe,
                               Madv = com.Madv,
                               Madiaban = com.Madiaban,
                               Mahs = com.Mahs,
                               Tendn = com.Tendn,
                               Trangthai = com.Trangthai
                           }).ToList();

            ViewData["DsDonVi"] = dsdonvi;
            ViewData["DsDiaBan"] = _db.DsDiaBan;
            ViewData["Madv"] = Madv;
            ViewData["Nam"] = Nam;
            ViewData["Title"] = "Công bố giá dịch vụ lữ hành";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_kknygiacb";
            ViewData["MenuLv3"] = "menu_luhanhcongbo";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/KeKhaiGia/DichVuLuHanh.cshtml", model);
        }

        [Route("CongBo/DichVuLuHanh/Show")]
        [HttpGet]
        public IActionResult DichVuLuHanhShow(string Mahs)
        {
            var model = _db.KkGia.FirstOrDefault(t => t.Mahs == Mahs);
            var hoso_kk = new VMKkGiaShow
            {
                Id = model.Id,
                Mahs = model.Mahs,
                Madv = model.Madv,
                Macskd = model.Macskd,
                Socv = model.Socv,
                Ngaynhap = model.Ngaynhap,
                Ngayhieuluc = model.Ngayhieuluc,
                Ttnguoinop = model.Ttnguoinop,
                Dtll = model.Dtll,
                Sohsnhan = model.Sohsnhan,
                Ngaychuyen = model.Ngaychuyen,
                Ngaynhan = model.Ngaynhan,
                Ptnguyennhan = model.Ptnguyennhan,
                Chinhsachkm = model.Chinhsachkm,
            };

            var modeldn = _db.Company.FirstOrDefault(t => t.Madv == model.Madv);
            if (modeldn != null)
            {
                hoso_kk.Tendn = modeldn.Tendn;
                hoso_kk.Diadanh = modeldn.Diadanh;
                hoso_kk.Diachi = modeldn.Diachi;
            }

            var modeldv = _db.DsDonVi.FirstOrDefault(t => t.MaDv == model.Macqcq);
            if (modeldv != null)
            {
                hoso_kk.Tendvhienthi = modeldv.TenDvHienThi;
            }

            var modelct = _db.KkGiaLuHanhCt.Where(t => t.Mahs == model.Mahs);
            if (modelct != null)
            {
                hoso_kk.KkGiaLuHanhCt = modelct.ToList();
            }

            ViewData["Title"] = "Xem chi tiết hồ sơ kê khai giá dịch vụ lữ hành";
            return View("Views/Admin/Manages/KeKhaiGia/KkGiaLuHanh/DanhSach/Show.cshtml", hoso_kk);
        }

        [Route("CongBo/DichVuLuuTru")]
        [HttpGet]
        public IActionResult DichVuLuuTru(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.KeKhaiGia.KkGia> model = _db.KkGia.Where(t => t.Congbo == "DACONGBO" && t.Manghe == "LUUTRU");

            if (Madv != "all")
            {
                model = model.Where(t => t.Madv == Madv);
            }

            if (Nam != 0)
            {
                model = model.Where(t => t.Ngaynhap.Year == Nam).ToList();
            }

            var dsdonvi = (from com in _db.Company
                           join lvkd in _db.CompanyLvCc.Where(t => t.Manghe == "DVLT") on com.Mahs equals lvkd.Mahs
                           select new VMCompany
                           {
                               Id = com.Id,
                               Manghe = lvkd.Manghe,
                               Madv = com.Madv,
                               Madiaban = com.Madiaban,
                               Mahs = com.Mahs,
                               Tendn = com.Tendn,
                               Trangthai = com.Trangthai
                           }).ToList();

            ViewData["DsDonVi"] = dsdonvi;
            ViewData["DsDiaBan"] = _db.DsDiaBan;
            ViewData["Madv"] = Madv;
            ViewData["Nam"] = Nam;
            ViewData["Title"] = "Công bố giá dịch vụ lưu trú";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_kknygiacb";
            ViewData["MenuLv3"] = "menu_dichvuluutrucongbo";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/KeKhaiGia/DichVuLuuTru.cshtml", model);
        }

        [Route("CongBo/DichVuLuuTru/Show")]
        [HttpGet]
        public IActionResult DichVuLuuTruShow(string Mahs)
        {
            var model = _db.KkGia.FirstOrDefault(t => t.Mahs == Mahs);
            var hoso_kk = new VMKkGiaShow
            {
                Id = model.Id,
                Mahs = model.Mahs,
                Madv = model.Madv,
                Macskd = model.Macskd,
                Socv = model.Socv,
                Ngaynhap = model.Ngaynhap,
                Ngayhieuluc = model.Ngayhieuluc,
                Ttnguoinop = model.Ttnguoinop,
                Dtll = model.Dtll,
                Sohsnhan = model.Sohsnhan,
                Ngaychuyen = model.Ngaychuyen,
                Ngaynhan = model.Ngaynhan,
                Ptnguyennhan = model.Ptnguyennhan,
                Chinhsachkm = model.Chinhsachkm,
            };

            var modeldn = _db.Company.FirstOrDefault(t => t.Madv == model.Madv);
            if (modeldn != null)
            {
                hoso_kk.Tendn = modeldn.Tendn;
                hoso_kk.Diadanh = modeldn.Diadanh;
                hoso_kk.Diachi = modeldn.Diachi;
            }

            var modeldv = _db.DsDonVi.FirstOrDefault(t => t.MaDv == model.Macqcq);
            if (modeldv != null)
            {
                hoso_kk.Tendvhienthi = modeldv.TenDvHienThi;
            }

            var modelct = _db.KkGiaDvLtCt.Where(t => t.Mahs == model.Mahs);
            if (modelct != null)
            {
                hoso_kk.KkGiaDvLtCt = modelct.ToList();
            }
            ViewData["Title"] = "Xem chi tiết hồ sơ kê khai giá dịch vụ lưu trú";
            return View("Views/Admin/Manages/KeKhaiGia/KkGiaDvlt/Show.cshtml", hoso_kk);
        }

        [Route("CongBo/XiMangThepXayDung")]
        [HttpGet]
        public IActionResult XiMangThepXayDung(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.KeKhaiGia.KkGia> model = _db.KkGia.Where(t => t.Congbo == "DACONGBO" && t.Manghe == "XMTXD");

            if (Madv != "all")
            {
                model = model.Where(t => t.Madv == Madv);
            }


            if (Nam != 0)
            {
                model = model.Where(t => t.Ngaynhap.Year == Nam).ToList();
            }

            var dsdonvi = (from com in _db.Company
                           join lvkd in _db.CompanyLvCc.Where(t => t.Manghe == "XMTXD") on com.Mahs equals lvkd.Mahs
                           select new VMCompany
                           {
                               Id = com.Id,
                               Manghe = lvkd.Manghe,
                               Madv = com.Madv,
                               Madiaban = com.Madiaban,
                               Mahs = com.Mahs,
                               Tendn = com.Tendn,
                               Trangthai = com.Trangthai
                           }).ToList();

            ViewData["DsDonVi"] = dsdonvi;
            ViewData["DsDiaBan"] = _db.DsDiaBan;
            ViewData["Madv"] = Madv;
            ViewData["Nam"] = Nam;
            ViewData["Title"] = "Công bố giá xi măng thép xây dựng";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_kknygiacb";
            ViewData["MenuLv3"] = "menu_ximangthepxaydungcongbo";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/KeKhaiGia/XiMangThepXayDung.cshtml", model);
        }

        [Route("CongBo/XiMangThepXayDung/Show")]
        [HttpGet]
        public IActionResult XiMangThepXayDungShow(string Mahs)
        {
            var model = _db.KkGia.FirstOrDefault(t => t.Mahs == Mahs);
            var hoso_kk = new VMKkGiaShow
            {
                Id = model.Id,
                Mahs = model.Mahs,
                Madv = model.Madv,
                Macskd = model.Macskd,
                Socv = model.Socv,
                Ngaynhap = model.Ngaynhap,
                Ngayhieuluc = model.Ngayhieuluc,
                Ttnguoinop = model.Ttnguoinop,
                Dtll = model.Dtll,
                Sohsnhan = model.Sohsnhan,
                Ngaychuyen = model.Ngaychuyen,
                Ngaynhan = model.Ngaynhan,
                Ptnguyennhan = model.Ptnguyennhan,
                Chinhsachkm = model.Chinhsachkm,
            };

            var modeldn = _db.Company.FirstOrDefault(t => t.Madv == model.Madv);
            if (modeldn != null)
            {
                hoso_kk.Tendn = modeldn.Tendn;
                hoso_kk.Diadanh = modeldn.Diadanh;
                hoso_kk.Diachi = modeldn.Diachi;
            }

            var modeldv = _db.DsDonVi.FirstOrDefault(t => t.MaDv == model.Macqcq);
            if (modeldv != null)
            {
                hoso_kk.Tendvhienthi = modeldv.TenDvHienThi;
            }

            var modelct = _db.KkGiaXmTxdCt.Where(t => t.Mahs == model.Mahs);
            if (modelct != null)
            {
                hoso_kk.KkGiaXmTxdCt = modelct.ToList();
            }
            ViewData["Title"] = "Xem chi tiết hồ sơ kê khai giá xi măng thép xây dựng";
            return View("Views/Admin/Manages/KeKhaiGia/KkGiaXmTxd/Show.cshtml", hoso_kk);
        }

        [Route("CongBo/Than")]
        [HttpGet]
        public IActionResult Than(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.KeKhaiGia.KkGia> model = _db.KkGia.Where(t => t.Congbo == "DACONGBO" && t.Manghe == "THAN");

            if (Madv != "all")
            {
                model = model.Where(t => t.Madv == Madv);
            }


            if (Nam != 0)
            {
                model = model.Where(t => t.Ngaynhap.Year == Nam).ToList();
            }

            var dsdonvi = (from com in _db.Company
                           join lvkd in _db.CompanyLvCc.Where(t => t.Manghe == "THAN") on com.Mahs equals lvkd.Mahs
                           select new VMCompany
                           {
                               Id = com.Id,
                               Manghe = lvkd.Manghe,
                               Madv = com.Madv,
                               Madiaban = com.Madiaban,
                               Mahs = com.Mahs,
                               Tendn = com.Tendn,
                               Trangthai = com.Trangthai
                           }).ToList();

            ViewData["DsDonVi"] = dsdonvi;
            ViewData["DsDiaBan"] = _db.DsDiaBan;
            ViewData["Madv"] = Madv;
            ViewData["Nam"] = Nam;
            ViewData["Title"] = "Công bố giá than";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_kknygiacb";
            ViewData["MenuLv3"] = "menu_thancongbo";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/KeKhaiGia/Than.cshtml", model);
        }

        [Route("CongBo/Than/Show")]
        [HttpGet]
        public IActionResult ThanShow(string Mahs)
        {
            var model = _db.KkGia.FirstOrDefault(t => t.Mahs == Mahs);
            var hoso_kk = new VMKkGiaShow
            {
                Id = model.Id,
                Mahs = model.Mahs,
                Madv = model.Madv,
                Macskd = model.Macskd,
                Socv = model.Socv,
                Ngaynhap = model.Ngaynhap,
                Ngayhieuluc = model.Ngayhieuluc,
                Ttnguoinop = model.Ttnguoinop,
                Dtll = model.Dtll,
                Sohsnhan = model.Sohsnhan,
                Ngaychuyen = model.Ngaychuyen,
                Ngaynhan = model.Ngaynhan,
                Ptnguyennhan = model.Ptnguyennhan,
                Chinhsachkm = model.Chinhsachkm,
            };

            var modeldn = _db.Company.FirstOrDefault(t => t.Madv == model.Madv);
            if (modeldn != null)
            {
                hoso_kk.Tendn = modeldn.Tendn;
                hoso_kk.Diadanh = modeldn.Diadanh;
                hoso_kk.Diachi = modeldn.Diachi;
            }

            var modeldv = _db.DsDonVi.FirstOrDefault(t => t.MaDv == model.Macqcq);
            if (modeldv != null)
            {
                hoso_kk.Tendvhienthi = modeldv.TenDvHienThi;
            }

            var modelct = _db.KkGiaThanCt.Where(t => t.Mahs == model.Mahs);
            if (modelct != null)
            {
                hoso_kk.KkGiaThanCt = modelct.ToList();
            }
            ViewData["Title"] = "Xem chi tiết hồ sơ kê khai giá than";
            return View("Views/Admin/Manages/KeKhaiGia/KkGiaThan/Show.cshtml", hoso_kk);
        }

        [Route("CongBo/ThucAnChanNuoi")]
        [HttpGet]
        public IActionResult ThucAnChanNuoi(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.KeKhaiGia.KkGia> model = _db.KkGia.Where(t => t.Congbo == "DACONGBO" && t.Manghe == "TACN");

            if (Madv != "all")
            {
                model = model.Where(t => t.Madv == Madv);
            }


            if (Nam != 0)
            {
                model = model.Where(t => t.Ngaynhap.Year == Nam).ToList();
            }

            var dsdonvi = (from com in _db.Company
                           join lvkd in _db.CompanyLvCc.Where(t => t.Manghe == "TACN") on com.Mahs equals lvkd.Mahs
                           select new VMCompany
                           {
                               Id = com.Id,
                               Manghe = lvkd.Manghe,
                               Madv = com.Madv,
                               Madiaban = com.Madiaban,
                               Mahs = com.Mahs,
                               Tendn = com.Tendn,
                               Trangthai = com.Trangthai
                           }).ToList();

            ViewData["DsDonVi"] = dsdonvi;
            ViewData["DsDiaBan"] = _db.DsDiaBan;
            ViewData["Madv"] = Madv;
            ViewData["Nam"] = Nam;
            ViewData["Title"] = "Công bố giá thức ăn chăn nuôi";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_kknygiacb";
            ViewData["MenuLv3"] = "menu_thucanchannuoicongbo";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/KeKhaiGia/ThucAnChanNuoi.cshtml", model);
        }

        [Route("CongBo/ThucAnChanNuoi/Show")]
        [HttpGet]
        public IActionResult ThucAnChanNuoiShow(string Mahs)
        {
            var model = _db.KkGia.FirstOrDefault(t => t.Mahs == Mahs);
            var hoso_kk = new VMKkGiaShow
            {
                Id = model.Id,
                Mahs = model.Mahs,
                Madv = model.Madv,
                Macskd = model.Macskd,
                Socv = model.Socv,
                Ngaynhap = model.Ngaynhap,
                Ngayhieuluc = model.Ngayhieuluc,
                Ttnguoinop = model.Ttnguoinop,
                Dtll = model.Dtll,
                Sohsnhan = model.Sohsnhan,
                Ngaychuyen = model.Ngaychuyen,
                Ngaynhan = model.Ngaynhan,
                Ptnguyennhan = model.Ptnguyennhan,
                Chinhsachkm = model.Chinhsachkm,
            };

            var modeldn = _db.Company.FirstOrDefault(t => t.Madv == model.Madv);
            if (modeldn != null)
            {
                hoso_kk.Tendn = modeldn.Tendn;
                hoso_kk.Diadanh = modeldn.Diadanh;
                hoso_kk.Diachi = modeldn.Diachi;
            }

            var modeldv = _db.DsDonVi.FirstOrDefault(t => t.MaDv == model.Macqcq);
            if (modeldv != null)
            {
                hoso_kk.Tendvhienthi = modeldv.TenDvHienThi;
            }

            var modelct = _db.KkGiaTaCnCt.Where(t => t.Mahs == model.Mahs);
            if (modelct != null)
            {
                hoso_kk.KkGiaTaCnCt = modelct.ToList();
            }
            ViewData["Title"] = "Xem chi tiết hồ sơ kê khai giá thức ăn chăn nuôi";
            return View("Views/Admin/Manages/KeKhaiGia/KkGiaTaCn/Show.cshtml", hoso_kk);
        }

        [Route("CongBo/Giay")]
        [HttpGet]
        public IActionResult Giay(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.KeKhaiGia.KkGia> model = _db.KkGia.Where(t => t.Congbo == "DACONGBO" && t.Manghe == "GIAY");

            if (Madv != "all")
            {
                model = model.Where(t => t.Madv == Madv);
            }


            if (Nam != 0)
            {
                model = model.Where(t => t.Ngaynhap.Year == Nam).ToList();
            }

            var dsdonvi = (from com in _db.Company
                           join lvkd in _db.CompanyLvCc.Where(t => t.Manghe == "GIAY") on com.Mahs equals lvkd.Mahs
                           select new VMCompany
                           {
                               Id = com.Id,
                               Manghe = lvkd.Manghe,
                               Madv = com.Madv,
                               Madiaban = com.Madiaban,
                               Mahs = com.Mahs,
                               Tendn = com.Tendn,
                               Trangthai = com.Trangthai
                           }).ToList();

            ViewData["DsDonVi"] = dsdonvi;
            ViewData["DsDiaBan"] = _db.DsDiaBan;
            ViewData["Madv"] = Madv;
            ViewData["Nam"] = Nam;
            ViewData["Title"] = "Công bố giá giấy";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_kknygiacb";
            ViewData["MenuLv3"] = "menu_giaycongbo";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/KeKhaiGia/Giay.cshtml", model);
        }

        [Route("CongBo/Giay/Show")]
        [HttpGet]
        public IActionResult GiayShow(string Mahs)
        {
            var model = _db.KkGia.FirstOrDefault(t => t.Mahs == Mahs);
            var hoso_kk = new VMKkGiaShow
            {
                Id = model.Id,
                Mahs = model.Mahs,
                Madv = model.Madv,
                Macskd = model.Macskd,
                Socv = model.Socv,
                Ngaynhap = model.Ngaynhap,
                Ngayhieuluc = model.Ngayhieuluc,
                Ttnguoinop = model.Ttnguoinop,
                Dtll = model.Dtll,
                Sohsnhan = model.Sohsnhan,
                Ngaychuyen = model.Ngaychuyen,
                Ngaynhan = model.Ngaynhan,
                Ptnguyennhan = model.Ptnguyennhan,
                Chinhsachkm = model.Chinhsachkm,
            };

            var modeldn = _db.Company.FirstOrDefault(t => t.Madv == model.Madv);
            if (modeldn != null)
            {
                hoso_kk.Tendn = modeldn.Tendn;
                hoso_kk.Diadanh = modeldn.Diadanh;
                hoso_kk.Diachi = modeldn.Diachi;
            }

            var modeldv = _db.DsDonVi.FirstOrDefault(t => t.MaDv == model.Macqcq);
            if (modeldv != null)
            {
                hoso_kk.Tendvhienthi = modeldv.TenDvHienThi;
            }

            var modelct = _db.KkGiaGiayCt.Where(t => t.Mahs == model.Mahs);
            if (modelct != null)
            {
                hoso_kk.KkGiaGiayCt = modelct.ToList();
            }
            ViewData["Title"] = "Xem chi tiết hồ sơ kê khai giá giấy";
            return View("Views/Admin/Manages/KeKhaiGia/KkGiaGiay/Show.cshtml", hoso_kk);
        }

        [Route("CongBo/SachGiaoKhoa")]
        [HttpGet]
        public IActionResult SachGiaoKhoa(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.KeKhaiGia.KkGia> model = _db.KkGia.Where(t => t.Congbo == "DACONGBO" && t.Manghe == "SACH");

            if (Madv != "all")
            {
                model = model.Where(t => t.Madv == Madv);
            }


            if (Nam != 0)
            {
                model = model.Where(t => t.Ngaynhap.Year == Nam).ToList();
            }

            var dsdonvi = (from com in _db.Company
                           join lvkd in _db.CompanyLvCc.Where(t => t.Manghe == "SACH") on com.Mahs equals lvkd.Mahs
                           select new VMCompany
                           {
                               Id = com.Id,
                               Manghe = lvkd.Manghe,
                               Madv = com.Madv,
                               Madiaban = com.Madiaban,
                               Mahs = com.Mahs,
                               Tendn = com.Tendn,
                               Trangthai = com.Trangthai
                           }).ToList();

            ViewData["DsDonVi"] = dsdonvi;
            ViewData["DsDiaBan"] = _db.DsDiaBan;
            ViewData["Madv"] = Madv;
            ViewData["Nam"] = Nam;
            ViewData["Title"] = "Công bố giá sách giáo khoa";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_kknygiacb";
            ViewData["MenuLv3"] = "menu_sachcongbo";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/KeKhaiGia/SachGiaoKhoa.cshtml", model);
        }

        [Route("CongBo/SachGiaoKhoa/Show")]
        [HttpGet]
        public IActionResult SachGiaoKhoaShow(string Mahs)
        {
            var model = _db.KkGia.FirstOrDefault(t => t.Mahs == Mahs);
            var hoso_kk = new VMKkGiaShow
            {
                Id = model.Id,
                Mahs = model.Mahs,
                Madv = model.Madv,
                Macskd = model.Macskd,
                Socv = model.Socv,
                Ngaynhap = model.Ngaynhap,
                Ngayhieuluc = model.Ngayhieuluc,
                Ttnguoinop = model.Ttnguoinop,
                Dtll = model.Dtll,
                Sohsnhan = model.Sohsnhan,
                Ngaychuyen = model.Ngaychuyen,
                Ngaynhan = model.Ngaynhan,
                Ptnguyennhan = model.Ptnguyennhan,
                Chinhsachkm = model.Chinhsachkm,
            };

            var modeldn = _db.Company.FirstOrDefault(t => t.Madv == model.Madv);
            if (modeldn != null)
            {
                hoso_kk.Tendn = modeldn.Tendn;
                hoso_kk.Diadanh = modeldn.Diadanh;
                hoso_kk.Diachi = modeldn.Diachi;
            }

            var modeldv = _db.DsDonVi.FirstOrDefault(t => t.MaDv == model.Macqcq);
            if (modeldv != null)
            {
                hoso_kk.Tendvhienthi = modeldv.TenDvHienThi;
            }

            var modelct = _db.KkGiaSachCt.Where(t => t.Mahs == model.Mahs);
            if (modelct != null)
            {
                hoso_kk.KkGiaSachCt = modelct.ToList();
            }
            ViewData["Title"] = "Xem chi tiết hồ sơ kê khai giá sách giáo khoa";
            return View("Views/Admin/Manages/KeKhaiGia/KkGiaSach/Show.cshtml", hoso_kk);
        }

        [Route("CongBo/Etanol")]
        [HttpGet]
        public IActionResult Etanol(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.KeKhaiGia.KkGia> model = _db.KkGia.Where(t => t.Congbo == "DACONGBO" && t.Manghe == "ETANOL");

            if (Madv != "all")
            {
                model = model.Where(t => t.Madv == Madv);
            }


            if (Nam != 0)
            {
                model = model.Where(t => t.Ngaynhap.Year == Nam).ToList();
            }

            var dsdonvi = (from com in _db.Company
                           join lvkd in _db.CompanyLvCc.Where(t => t.Manghe == "ETANOL") on com.Mahs equals lvkd.Mahs
                           select new VMCompany
                           {
                               Id = com.Id,
                               Manghe = lvkd.Manghe,
                               Madv = com.Madv,
                               Madiaban = com.Madiaban,
                               Mahs = com.Mahs,
                               Tendn = com.Tendn,
                               Trangthai = com.Trangthai
                           }).ToList();

            ViewData["DsDonVi"] = dsdonvi;
            ViewData["DsDiaBan"] = _db.DsDiaBan;
            ViewData["Madv"] = Madv;
            ViewData["Nam"] = Nam;
            ViewData["Title"] = "Công bố giá Etanol nhiên liệu không biến tính, khí tự nhiên hóa lỏng(LNG); khí thiên nhiên nén (CNG)";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_kknygiacb";
            ViewData["MenuLv3"] = "menu_etanolcongbo";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/KeKhaiGia/Etanol.cshtml", model);
        }

        [Route("CongBo/Etanol/Show")]
        [HttpGet]
        public IActionResult EtanolShow(string Mahs)
        {
            var model = _db.KkGia.FirstOrDefault(t => t.Mahs == Mahs);
            var hoso_kk = new VMKkGiaShow
            {
                Id = model.Id,
                Mahs = model.Mahs,
                Madv = model.Madv,
                Macskd = model.Macskd,
                Socv = model.Socv,
                Ngaynhap = model.Ngaynhap,
                Ngayhieuluc = model.Ngayhieuluc,
                Ttnguoinop = model.Ttnguoinop,
                Dtll = model.Dtll,
                Sohsnhan = model.Sohsnhan,
                Ngaychuyen = model.Ngaychuyen,
                Ngaynhan = model.Ngaynhan,
                Ptnguyennhan = model.Ptnguyennhan,
                Chinhsachkm = model.Chinhsachkm,
            };

            var modeldn = _db.Company.FirstOrDefault(t => t.Madv == model.Madv);
            if (modeldn != null)
            {
                hoso_kk.Tendn = modeldn.Tendn;
                hoso_kk.Diadanh = modeldn.Diadanh;
                hoso_kk.Diachi = modeldn.Diachi;
            }

            var modeldv = _db.DsDonVi.FirstOrDefault(t => t.MaDv == model.Macqcq);
            if (modeldv != null)
            {
                hoso_kk.Tendvhienthi = modeldv.TenDvHienThi;
            }

            var modelct = _db.KkGiaLuHanhCt.Where(t => t.Mahs == model.Mahs);
            if (modelct != null)
            {
                hoso_kk.KkGiaLuHanhCt = modelct.ToList();
            }
            ViewData["Title"] = "Xem chi tiết hồ sơ kê khai giá Etanol nhiên liệu không biến tính, khí tự nhiên hóa lỏng(LNG); khí thiên nhiên nén (CNG)";
            return View("Views/Admin/Manages/KeKhaiGia/KkGiaEtanol/Show.cshtml", hoso_kk);
        }

        [Route("CongBo/ThucPhamChucNang")]
        [HttpGet]
        public IActionResult ThucPhamChucNang(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.KeKhaiGia.KkGia> model = _db.KkGia.Where(t => t.Congbo == "DACONGBO" && t.Manghe == "TPCNTE6T");

            if (Madv != "all")
            {
                model = model.Where(t => t.Madv == Madv);
            }


            if (Nam != 0)
            {
                model = model.Where(t => t.Ngaynhap.Year == Nam).ToList();
            }

            var dsdonvi = (from com in _db.Company
                           join lvkd in _db.CompanyLvCc.Where(t => t.Manghe == "TPCNTE6T") on com.Mahs equals lvkd.Mahs
                           select new VMCompany
                           {
                               Id = com.Id,
                               Manghe = lvkd.Manghe,
                               Madv = com.Madv,
                               Madiaban = com.Madiaban,
                               Mahs = com.Mahs,
                               Tendn = com.Tendn,
                               Trangthai = com.Trangthai
                           }).ToList();

            ViewData["DsDonVi"] = dsdonvi;
            ViewData["DsDiaBan"] = _db.DsDiaBan;
            ViewData["Madv"] = Madv;
            ViewData["Nam"] = Nam;
            ViewData["Title"] = "Công bố giá thực phẩm chức năng cho trẻ em dưới 6 tuổi";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_kknygiacb";
            ViewData["MenuLv3"] = "menu_thucphamchucnangcongbo";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/KeKhaiGia/ThucPhamChucNang.cshtml", model);
        }

        [Route("CongBo/ThucPhamChucNang/Show")]
        [HttpGet]
        public IActionResult ThucPhamChucNangShow(string Mahs)
        {
            var model = _db.KkGia.FirstOrDefault(t => t.Mahs == Mahs);
            var hoso_kk = new VMKkGiaShow
            {
                Id = model.Id,
                Mahs = model.Mahs,
                Madv = model.Madv,
                Macskd = model.Macskd,
                Socv = model.Socv,
                Ngaynhap = model.Ngaynhap,
                Ngayhieuluc = model.Ngayhieuluc,
                Ttnguoinop = model.Ttnguoinop,
                Dtll = model.Dtll,
                Sohsnhan = model.Sohsnhan,
                Ngaychuyen = model.Ngaychuyen,
                Ngaynhan = model.Ngaynhan,
                Ptnguyennhan = model.Ptnguyennhan,
                Chinhsachkm = model.Chinhsachkm,
            };

            var modeldn = _db.Company.FirstOrDefault(t => t.Madv == model.Madv);
            if (modeldn != null)
            {
                hoso_kk.Tendn = modeldn.Tendn;
                hoso_kk.Diadanh = modeldn.Diadanh;
                hoso_kk.Diachi = modeldn.Diachi;
            }

            var modeldv = _db.DsDonVi.FirstOrDefault(t => t.MaDv == model.Macqcq);
            if (modeldv != null)
            {
                hoso_kk.Tendvhienthi = modeldv.TenDvHienThi;
            }

            var modelct = _db.KkGsCt.Where(t => t.Mahs == model.Mahs);
            if (modelct != null)
            {
                hoso_kk.KkGsCt = modelct.ToList();
            }
            ViewData["Title"] = "Xem chi tiết hồ sơ kê khai giá thực phẩm chức năng cho trẻ em dưới 6 tuổi";
            return View("Views/Admin/Manages/KeKhaiGia/KkGiaTpcn/Show.cshtml", hoso_kk);
        }

        [Route("CongBo/VanChuyenBangXeBuyt")]
        [HttpGet]
        public IActionResult VanChuyenBangXeBuyt(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.KeKhaiGia.KkGia> model = _db.KkGia.Where(t => t.Congbo == "DACONGBO" && t.Manghe == "VTXB");

            if (Madv != "all")
            {
                model = model.Where(t => t.Madv == Madv);
            }


            if (Nam != 0)
            {
                model = model.Where(t => t.Ngaynhap.Year == Nam).ToList();
            }

            var dsdonvi = (from com in _db.Company
                           join lvkd in _db.CompanyLvCc.Where(t => t.Manghe == "VTXB") on com.Mahs equals lvkd.Mahs
                           select new VMCompany
                           {
                               Id = com.Id,
                               Manghe = lvkd.Manghe,
                               Madv = com.Madv,
                               Madiaban = com.Madiaban,
                               Mahs = com.Mahs,
                               Tendn = com.Tendn,
                               Trangthai = com.Trangthai
                           }).ToList();

            ViewData["DsDonVi"] = dsdonvi;
            ViewData["DsDiaBan"] = _db.DsDiaBan;
            ViewData["Madv"] = Madv;
            ViewData["Nam"] = Nam;
            ViewData["Title"] = "Công bố giá cước vận tải hành khách bằng xe buýt theo tuyến cố định";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_kknygiacb";
            ViewData["MenuLv3"] = "menu_vanchuyenbangxebuytcongbo";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/KeKhaiGia/VanChuyenBangXeBuyt.cshtml", model);
        }

        [Route("CongBo/VanChuyenBangXeBuyt/Show")]
        [HttpGet]
        public IActionResult VanChuyenBangXeBuytShow(string Mahs)
        {
            var model = _db.KkGia.FirstOrDefault(t => t.Mahs == Mahs);
            var hoso_kk = new VMKkGiaShow
            {
                Id = model.Id,
                Mahs = model.Mahs,
                Madv = model.Madv,
                Macskd = model.Macskd,
                Socv = model.Socv,
                Ngaynhap = model.Ngaynhap,
                Ngayhieuluc = model.Ngayhieuluc,
                Ttnguoinop = model.Ttnguoinop,
                Dtll = model.Dtll,
                Sohsnhan = model.Sohsnhan,
                Ngaychuyen = model.Ngaychuyen,
                Ngaynhan = model.Ngaynhan,
                Ptnguyennhan = model.Ptnguyennhan,
                Chinhsachkm = model.Chinhsachkm,
            };

            var modeldn = _db.Company.FirstOrDefault(t => t.Madv == model.Madv);
            if (modeldn != null)
            {
                hoso_kk.Tendn = modeldn.Tendn;
                hoso_kk.Diadanh = modeldn.Diadanh;
                hoso_kk.Diachi = modeldn.Diachi;
            }

            var modeldv = _db.DsDonVi.FirstOrDefault(t => t.MaDv == model.Macqcq);
            if (modeldv != null)
            {
                hoso_kk.Tendvhienthi = modeldv.TenDvHienThi;
            }

            var modelct = _db.KkGiaVtXbCt.Where(t => t.Mahs == model.Mahs);
            if (modelct != null)
            {
                hoso_kk.KkGiaVtXbCt = modelct.ToList();
            }
            ViewData["Title"] = "Xem chi tiết hồ sơ kê khai giá cước vận tải hành khách bằng xe buýt theo tuyến cố định";
            return View("Views/Admin/Manages/KeKhaiGia/KkGiaVtXb/Show.cshtml", hoso_kk);
        }

        [Route("CongBo/VanChuyenBangOto")]
        [HttpGet]
        public IActionResult VanChuyenBangOto(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.KeKhaiGia.KkGia> model = _db.KkGia.Where(t => t.Congbo == "DACONGBO" && t.Manghe == "VTXK");

            if (Madv != "all")
            {
                model = model.Where(t => t.Madv == Madv);
            }


            if (Nam != 0)
            {
                model = model.Where(t => t.Ngaynhap.Year == Nam).ToList();
            }

            var dsdonvi = (from com in _db.Company
                           join lvkd in _db.CompanyLvCc.Where(t => t.Manghe == "VTXK") on com.Mahs equals lvkd.Mahs
                           select new VMCompany
                           {
                               Id = com.Id,
                               Manghe = lvkd.Manghe,
                               Madv = com.Madv,
                               Madiaban = com.Madiaban,
                               Mahs = com.Mahs,
                               Tendn = com.Tendn,
                               Trangthai = com.Trangthai
                           }).ToList();

            ViewData["DsDonVi"] = dsdonvi;
            ViewData["DsDiaBan"] = _db.DsDiaBan;
            ViewData["Madv"] = Madv;
            ViewData["Nam"] = Nam;
            ViewData["Title"] = "Công bố giá cước vận tải hành khách bằng ôtô tuyến cố định";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_kknygiacb";
            ViewData["MenuLv3"] = "menu_vanchuyenbangotocongbo";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/KeKhaiGia/VanChuyenBangOto.cshtml", model);
        }

        [Route("CongBo/VanChuyenBangOto/Show")]
        [HttpGet]
        public IActionResult VanChuyenBangOtoShow(string Mahs)
        {
            var model = _db.KkGia.FirstOrDefault(t => t.Mahs == Mahs);
            var hoso_kk = new VMKkGiaShow
            {
                Id = model.Id,
                Mahs = model.Mahs,
                Madv = model.Madv,
                Macskd = model.Macskd,
                Socv = model.Socv,
                Ngaynhap = model.Ngaynhap,
                Ngayhieuluc = model.Ngayhieuluc,
                Ttnguoinop = model.Ttnguoinop,
                Dtll = model.Dtll,
                Sohsnhan = model.Sohsnhan,
                Ngaychuyen = model.Ngaychuyen,
                Ngaynhan = model.Ngaynhan,
                Ptnguyennhan = model.Ptnguyennhan,
                Chinhsachkm = model.Chinhsachkm,
            };

            var modeldn = _db.Company.FirstOrDefault(t => t.Madv == model.Madv);
            if (modeldn != null)
            {
                hoso_kk.Tendn = modeldn.Tendn;
                hoso_kk.Diadanh = modeldn.Diadanh;
                hoso_kk.Diachi = modeldn.Diachi;
            }

            var modeldv = _db.DsDonVi.FirstOrDefault(t => t.MaDv == model.Macqcq);
            if (modeldv != null)
            {
                hoso_kk.Tendvhienthi = modeldv.TenDvHienThi;
            }

            var modelct = _db.KkGiaVtXkCt.Where(t => t.Mahs == model.Mahs);
            if (modelct != null)
            {
                hoso_kk.KkGiaVtXkCt = modelct.ToList();
            }
            ViewData["Title"] = "Xem chi tiết hồ sơ kê khai giá cước vận tải hành khách bằng ôtô tuyến cố định";
            return View("Views/Admin/Manages/KeKhaiGia/KkGiaVtXk/Show.cshtml", hoso_kk);
        }

        [Route("CongBo/VanChuyenBangTaxi")]
        [HttpGet]
        public IActionResult VanChuyenBangTaxi(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.KeKhaiGia.KkGia> model = _db.KkGia.Where(t => t.Congbo == "DACONGBO" && t.Manghe == "VTXTX");

            if (Madv != "all")
            {
                model = model.Where(t => t.Madv == Madv);
            }


            if (Nam != 0)
            {
                model = model.Where(t => t.Ngaynhap.Year == Nam).ToList();
            }

            var dsdonvi = (from com in _db.Company
                           join lvkd in _db.CompanyLvCc.Where(t => t.Manghe == "VTXTX") on com.Mahs equals lvkd.Mahs
                           select new VMCompany
                           {
                               Id = com.Id,
                               Manghe = lvkd.Manghe,
                               Madv = com.Madv,
                               Madiaban = com.Madiaban,
                               Mahs = com.Mahs,
                               Tendn = com.Tendn,
                               Trangthai = com.Trangthai
                           }).ToList();

            ViewData["DsDonVi"] = dsdonvi;
            ViewData["DsDiaBan"] = _db.DsDiaBan;
            ViewData["Madv"] = Madv;
            ViewData["Nam"] = Nam;
            ViewData["Title"] = "Công bố giá cước vận tải hành khách bằng xe taxi";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_kknygiacb";
            ViewData["MenuLv3"] = "menu_vanchuyenbangtaxicongbo";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/KeKhaiGia/VanChuyenBangTaxi.cshtml", model);
        }

        [Route("CongBo/VanChuyenBangTaxi/Show")]
        [HttpGet]
        public IActionResult VanChuyenBangTaxiShow(string Mahs)
        {
            var model = _db.KkGia.FirstOrDefault(t => t.Mahs == Mahs);
            var hoso_kk = new VMKkGiaShow
            {
                Id = model.Id,
                Mahs = model.Mahs,
                Madv = model.Madv,
                Macskd = model.Macskd,
                Socv = model.Socv,
                Ngaynhap = model.Ngaynhap,
                Ngayhieuluc = model.Ngayhieuluc,
                Ttnguoinop = model.Ttnguoinop,
                Dtll = model.Dtll,
                Sohsnhan = model.Sohsnhan,
                Ngaychuyen = model.Ngaychuyen,
                Ngaynhan = model.Ngaynhan,
                Ptnguyennhan = model.Ptnguyennhan,
                Chinhsachkm = model.Chinhsachkm,
            };

            var modeldn = _db.Company.FirstOrDefault(t => t.Madv == model.Madv);
            if (modeldn != null)
            {
                hoso_kk.Tendn = modeldn.Tendn;
                hoso_kk.Diadanh = modeldn.Diadanh;
                hoso_kk.Diachi = modeldn.Diachi;
            }

            var modeldv = _db.DsDonVi.FirstOrDefault(t => t.MaDv == model.Macqcq);
            if (modeldv != null)
            {
                hoso_kk.Tendvhienthi = modeldv.TenDvHienThi;
            }

            var modelct = _db.KkGiaVtXtxCt.Where(t => t.Mahs == model.Mahs);
            if (modelct != null)
            {
                hoso_kk.KkGiaVtXtxCt = modelct.ToList();
            }
            ViewData["Title"] = "Xem chi tiết hồ sơ kê khai giá cước vận tải hành khách bằng xe taxi";
            return View("Views/Admin/Manages/KeKhaiGia/KkGiaVtXtx/Show.cshtml", hoso_kk);
        }

        [Route("CongBo/VatLieuXayDung")]
        [HttpGet]
        public IActionResult VatLieuXayDung(string Madv, int Nam)
        {
            Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

            IEnumerable<CSDLGia_ASP.Models.Manages.KeKhaiGia.KkGia> model = _db.KkGia.Where(t => t.Congbo == "DACONGBO" && t.Manghe == "VLXD");

            if (Madv != "all")
            {
                model = model.Where(t => t.Madv == Madv);
            }


            if (Nam != 0)
            {
                model = model.Where(t => t.Ngaynhap.Year == Nam).ToList();
            }

            var dsdonvi = (from com in _db.Company
                           join lvkd in _db.CompanyLvCc.Where(t => t.Manghe == "VLXD") on com.Mahs equals lvkd.Mahs
                           select new VMCompany
                           {
                               Id = com.Id,
                               Manghe = lvkd.Manghe,
                               Madv = com.Madv,
                               Madiaban = com.Madiaban,
                               Mahs = com.Mahs,
                               Tendn = com.Tendn,
                               Trangthai = com.Trangthai
                           }).ToList();

            ViewData["DsDonVi"] = dsdonvi;
            ViewData["DsDiaBan"] = _db.DsDiaBan;
            ViewData["Madv"] = Madv;
            ViewData["Nam"] = Nam;
            ViewData["Title"] = "Công bố giá vật liệu xây dựng";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_kknygiacb";
            ViewData["MenuLv3"] = "menu_vatlieuxaydungcongbo";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/KeKhaiGia/VatLieuXayDung.cshtml", model);
        }

        [Route("CongBo/VatLieuXayDung/Show")]
        [HttpGet]
        public IActionResult VatLieuXayDungShow(string Mahs)
        {
            var model = _db.KkGia.FirstOrDefault(t => t.Mahs == Mahs);
            var hoso_kk = new VMKkGiaShow
            {
                Id = model.Id,
                Mahs = model.Mahs,
                Madv = model.Madv,
                Macskd = model.Macskd,
                Socv = model.Socv,
                Ngaynhap = model.Ngaynhap,
                Ngayhieuluc = model.Ngayhieuluc,
                Ttnguoinop = model.Ttnguoinop,
                Dtll = model.Dtll,
                Sohsnhan = model.Sohsnhan,
                Ngaychuyen = model.Ngaychuyen,
                Ngaynhan = model.Ngaynhan,
                Ptnguyennhan = model.Ptnguyennhan,
                Chinhsachkm = model.Chinhsachkm,
            };

            var modeldn = _db.Company.FirstOrDefault(t => t.Madv == model.Madv);
            if (modeldn != null)
            {
                hoso_kk.Tendn = modeldn.Tendn;
                hoso_kk.Diadanh = modeldn.Diadanh;
                hoso_kk.Diachi = modeldn.Diachi;
            }

            var modeldv = _db.DsDonVi.FirstOrDefault(t => t.MaDv == model.Macqcq);
            if (modeldv != null)
            {
                hoso_kk.Tendvhienthi = modeldv.TenDvHienThi;
            }

            var modelct = _db.KkGiaVlXdCt.Where(t => t.Mahs == model.Mahs);
            if (modelct != null)
            {
                hoso_kk.KkGiaVlXdCt = modelct.ToList();
            }
            ViewData["Title"] = "Xem chi tiết hồ sơ kê khai giá vật liệu xây dựng";
            return View("Views/Admin/Manages/KeKhaiGia/KkGiaVatLieuXayDung/Show.cshtml", hoso_kk);
        }
    }
}
