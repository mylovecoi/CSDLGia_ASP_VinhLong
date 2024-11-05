
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



            var countGiaThueDatNuocHT = _db.GiaThueMatDatMatNuoc.Where(x =>(x.Trangthai == "HT" || x.Trangthai == "CB") && (string.IsNullOrEmpty(Madv) || Madv == "all" || x.Madv == Madv));

            if (Nam > 0) // Nếu Nam lớn hơn 0 thì lọc theo năm
            {
                countGiaThueDatNuocHT = countGiaThueDatNuocHT.Where(x => x.Thoidiem.Year == Nam);
            }

            if (Thang > 0) // Nếu Thang lớn hơn 0 thì lọc theo tháng
            {
                countGiaThueDatNuocHT = countGiaThueDatNuocHT.Where(x => x.Thoidiem.Month == Thang);
            }

            int count = countGiaThueDatNuocHT.Count();

            var countGiaThueDatNuocCHT = _db.GiaThueMatDatMatNuoc.Where(x => x.Trangthai == "CHT" && x.Madv == Madv && x.Thoidiem.Year == Nam).Count();




            var viewModel = new VMThongKe
            {
                countGiaThueDatNuocHT = count,
                countGiaThueDatNuocCHT = countGiaThueDatNuocCHT,

            };

            ViewData["Title"] = " Thống kê hồ sơ của đơn vị hành chính";
            ViewData["MenuLv1"] = "menu_thongke";
            ViewData["MenuLv2"] = "menu_thongke_donvi";
            ViewData["DsDonvi"] = _db.DsDonVi;
            return View("Views/Admin/Systems/ThongKe/Index.cshtml", viewModel);
        }


    }
}




