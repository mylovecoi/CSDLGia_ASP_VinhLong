
using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Services;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class ThongKeController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public ThongKeController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("ThongKe/DanhSach")]
        [HttpGet]
        public IActionResult Index(int Nam, string Madv, int Thang)
        {      

            var countGiaThueDatNuocCHTthang = _db.GiaThueMatDatMatNuoc.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            if (Nam > 0) { countGiaThueDatNuocCHTthang = countGiaThueDatNuocCHTthang.Where(x => x.Thoidiem.Year == Nam); }
            if (Thang > 0) { countGiaThueDatNuocCHTthang = countGiaThueDatNuocCHTthang.Where(x => x.Thoidiem.Month == Thang); }
            int countgiathuedatnuocCHTthang = countGiaThueDatNuocCHTthang.Count();

            var countGiaThueDatNuocHTthang = _db.GiaThueMatDatMatNuoc.Where(x => (x.Trangthai == "HT" || x.Trangthai == "CB") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            if (Nam > 0) { countGiaThueDatNuocHTthang = countGiaThueDatNuocHTthang.Where(x => x.Thoidiem.Year == Nam); }
            if (Thang > 0) { countGiaThueDatNuocHTthang = countGiaThueDatNuocHTthang.Where(x => x.Thoidiem.Month == Thang); }
            int countgiathuedatnuocHTthang = countGiaThueDatNuocHTthang.Count();

            var countGiaThueDatNuocCHT = _db.GiaThueMatDatMatNuoc.Where(x => (x.Trangthai == "CC") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));
            int countgiathuedatnuocCHT = countGiaThueDatNuocCHT.Count();

            var countGiaThueDatNuocHT = _db.GiaThueMatDatMatNuoc.Where(x => (x.Trangthai == "HT" || x.Trangthai == "CB") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv)); 
            int countgiathuedatnuocHT = countGiaThueDatNuocHT.Count();

            var viewModel = new VMThongKe
            {              
                countGiaThueDatNuocCHTthang = countgiathuedatnuocCHTthang,
                countGiaThueDatNuocHTthang = countgiathuedatnuocHTthang,
                countGiaThueDatNuocCHT = countgiathuedatnuocCHT,
                countGiaThueDatNuocHT = countgiathuedatnuocHT,
            };

            ViewData["Title"] = " Thống kê hồ sơ của đơn vị hành chính";
            ViewData["MenuLv1"] = "menu_thongke";
            ViewData["MenuLv2"] = "menu_thongke_donvi";
            ViewData["DsDonvi"] = _db.DsDonVi;
            ViewData["Nam"] = Nam;
            ViewData["Madv"] = Madv;
            ViewData["Thang"] = Thang;

            return View("Views/Admin/Systems/ThongKe/Index.cshtml", viewModel);
        }


    }
}




