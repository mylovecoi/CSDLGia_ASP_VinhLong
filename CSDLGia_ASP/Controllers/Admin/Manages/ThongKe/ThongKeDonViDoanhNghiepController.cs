using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Services;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CSDLGia_ASP.Models.Manages.KeKhaiDangKyGia;

namespace CSDLGia_ASP.Controllers.Admin.Manages.ThongKe
{
    public class ThongKeDonViDoanhNghiepController : Controller
    {

        private readonly CSDLGiaDBContext _db;

        public ThongKeDonViDoanhNghiepController(CSDLGiaDBContext db)
        {
            _db = db;
        }


        [Route("ThongKeDonViDoanhNghiep/DanhSach")]
        public IActionResult Index(int Nam, string Madv, int Thang)
        {

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "thongke.thongkehethong.nhaplieudonvidoanhnghiep", "Index"))
                {
                    // Giá vật liệu xây dựng
                    ViewData["Title"] = " Thống kê hồ sơ của đơn vị doanh nghiệp";
                    ViewData["MenuLv1"] = "menu_thongke";
                    ViewData["MenuLv2"] = "menu_thongke_doanhnghiep";
                    ViewData["DsDoanhNghiep"] = _db.Users.Where(x => x.Level == "DN");
                    ViewData["Nam"] = Nam;
                    ViewData["Nam"] = Nam == 0 ? DateTime.Now.Year : Nam;
                    ViewData["Thang"] = Thang == 0 ? DateTime.Now.Month : Thang;

                    return View("Views/Admin/Manages/ThongKe/DonViDoanhNghiep/Index.cshtml");

                }
                else
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

        }//
    }
}
