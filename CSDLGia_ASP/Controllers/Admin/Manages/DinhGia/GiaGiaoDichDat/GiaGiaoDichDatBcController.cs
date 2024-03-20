using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaGiaoDichDat
{
    public class GiaGiaoDichDatBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaGiaoDichDatBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaGiaoDichDat/BaoCao")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.baocao", "Index"))
                {
                    var nhomtn = _db.GiaGiaoDichDatNhom.Where(t => t.Theodoi == "TD").ToList();

                    ViewData["NhomTn"] = nhomtn;
                    ViewData["Nam"] = DateTime.Now.Year;
                    ViewData["Title"] = "Báo cáo tổng hợp giá đất giao dịch thực tế trên thị trường";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dg_giaodichdattrenthitruong";
                    ViewData["MenuLv3"] = "menu_dg_giaodichdattrenthitruong_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichDat/BaoCao/Index.cshtml");
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
        }

        [Route("GiaGiaoDichDat/Bc1")]
        [HttpPost]
        public IActionResult Bc1(string manhom, int namlk, int nambc, string tenthutruong, string chucvu)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.baocao", "Index"))
                {
                    var model = _db.GiaGiaoDichDatDm.Where(t => t.Manhom == manhom).ToList();

                    var modellk = _db.GiaGiaoDichDat.FirstOrDefault(t => t.Thoidiem.Year == namlk && t.Manhom == manhom);

                    var modelbc = _db.GiaGiaoDichDat.FirstOrDefault(t => t.Thoidiem.Year == nambc && t.Manhom == manhom);

                    ViewData["TenNhom"] = _db.GiaGiaoDichDatNhom.FirstOrDefault(t => t.Manhom == manhom).Tennhom;
                    ViewData["Namlk"] = namlk;
                    ViewData["Nambc"] = nambc;
                    ViewData["tenthutruong"] = tenthutruong;
                    ViewData["chucvu"] = chucvu;
                    ViewData["Title"] = "Báo cáo tổng hợp giá đất giao dịch thực tế trên thị trường";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dg_giaodichdattrenthitruong";
                    ViewData["MenuLv3"] = "menu_dg_giaodichdattrenthitruong_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichDat/BaoCao/Bc1.cshtml", model);
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
        }
    }
}
