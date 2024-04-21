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

        [Route("CongBo/KeKhaiGia")]
        [HttpGet]
        public IActionResult KeKhaiGia(int Nam,string MaNghe)
        {
            var model_nghe = _db.DmNgheKd.Where(t => t.Theodoi == "TD").ToList();
            List<string> list = model_nghe.Select(t=>t.Manghe).ToList();

            MaNghe = string.IsNullOrEmpty(MaNghe) ? "all" : MaNghe;

            IEnumerable<CSDLGia_ASP.Models.Manages.KeKhaiDangKyGia.KeKhaiDangKyGia> model = _db.KeKhaiDangKyGia.Where(t => t.TrangThai == "CB" && list.Contains(t.MaNghe));

            if (Nam != 0)
            {
                model = model.Where(t => t.NgayQD.Year == Nam);
            }

            if (MaNghe != "all")
            {
                model = model.Where(t => t.MaNghe == MaNghe);
            }

            var model_jon = from hoso in model
                            join cskd in _db.KeKhaiDangKyGiaCSKD on hoso.MaCsKd equals cskd.MaCsKd
                        join dn in _db.Company on cskd.MaDv equals dn.Madv
                        join donvi in _db.DsDonVi on hoso.MaCqCq equals donvi.MaDv
                        join dmnghe in _db.DmNgheKd on hoso.MaNghe equals dmnghe.Manghe
                        select new CSDLGia_ASP.Models.Manages.KeKhaiDangKyGia.KeKhaiDangKyGia
                        {
                            Id = hoso.Id,
                            Mahs = hoso.Mahs,
                            TenCsKd = cskd.TenCsKd,
                            TenDv = dn.Tendn,
                            SoQD = hoso.SoQD,
                            NgayQD = hoso.NgayQD,
                            NgayChuyen = hoso.NgayChuyen,
                            ThongTinNguoiChuyen = hoso.ThongTinNguoiChuyen,
                            LyDo = hoso.LyDo,
                            TrangThai = hoso.TrangThai,
                            NgayDuyet = hoso.NgayDuyet,
                            TenCqCq = donvi.TenDv,
                            MaCqCq = hoso.MaCqCq,
                            MaNghe = hoso.MaNghe,
                            TenNghe = dmnghe.Phanloai + " " + dmnghe.Tennghe
                        };

            ViewData["DsNghe"] = model_nghe;
            ViewData["Nam"] = Nam;
            ViewData["MaNghe"] = MaNghe;
            ViewData["Title"] = "Công bố kê khai - đăng ký giá";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_kknygiacb";
            ViewBag.bSession = false;
            return View("Views/Admin/CongBo/KeKhaiGia/KeKhaiGia.cshtml", model_jon);
        }

    }
}
