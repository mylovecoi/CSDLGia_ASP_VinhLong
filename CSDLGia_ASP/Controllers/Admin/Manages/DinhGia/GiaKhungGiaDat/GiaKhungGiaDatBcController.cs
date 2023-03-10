using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CSDLGia_ASP.Database;
using System.Security.Cryptography;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSDLGia_ASP.ViewModels.Systems;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using CSDLGia_ASP.Models.Manages.KeKhaiDkg;
using CSDLGia_ASP.ViewModels.Manages.KeKhaiDkg;
using Microsoft.Extensions.Hosting;
using CSDLGia_ASP.Models.Systems;
using System.Net.WebSockets;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaKhungGiaDat
{
    public class GiaKhungGiaDatBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaKhungGiaDatBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaKhungGiaDat/BaoCao")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khunggd.baocao", "Index"))
                {

                    ViewData["Nam"] = DateTime.Now.Year;
                    ViewData["Title"] = "Báo cáo tổng hợp giá khung giá đất";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgkhunggd";
                    ViewData["MenuLv3"] = "menu_dgkhunggd_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaKhungGiaDat/BaoCao/Index.cshtml");
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

        /*[Route("GiaKhungGiaDat/Bc1")]
        [HttpPost]
        public IActionResult Bc1(int nambc)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khunggd.baocao", "Index"))
                {

                    var model = _db.GiaKhungGiaDat.FirstOrDefault(t => t.Thoidiem.Year == nambc);

               
                    ViewData["Nambc"] = nambc;
                    ViewData["Title"] = "Báo cáo tổng hợp giá khung giá đất";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgkhunggd";
                    ViewData["MenuLv3"] = "menu_dgkhunggd_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaKhungGiaDat/BaoCao/Bc1.cshtml", model);
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
        }*/

        [Route("GiaKhungGiaDat/Bc2")]
        [HttpPost]
        public IActionResult Bc2(int nambc)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khunggd.baocao", "Index"))
                {

                    var model = _db.GiaKhungGiaDat.Where(t => t.Thoidiem.Year == nambc);

                    ViewData["Ct"] = _db.GiaKhungGiaDatCt.ToList();
                    ViewData["Nambc"] = nambc;
                    ViewData["Title"] = "Báo cáo chi tiết giá khung giá đất";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgkhunggd";
                    ViewData["MenuLv3"] = "menu_dgkhunggd_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaKhungGiaDat/BaoCao/Bc2.cshtml", model);
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
