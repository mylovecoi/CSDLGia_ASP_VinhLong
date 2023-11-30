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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaGiaoDichBDS
{
    public class GiaGiaoDichBDSBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaGiaoDichBDSBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaGiaoDichBDS/BaoCao")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaodichbds.baocao", "Index"))
                {
                    var nhomtn = _db.GiaGiaoDichBDSNhom.Where(t => t.Theodoi == "TD").ToList();

                    ViewData["NhomTn"] = nhomtn;
                    ViewData["Nam"] = DateTime.Now.Year;
                    ViewData["Title"] = "Báo cáo tổng hợp giá nhà cho thuê sinh viên";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dg_giaodichbds";
                    ViewData["MenuLv3"] = "menu_dg_giaodichbds_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichBDS/BaoCao/Index.cshtml");
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

        [Route("GiaGiaoDichBDS/Bc1")]
        [HttpPost]
        public IActionResult Bc1(string manhom, int namlk, int nambc)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaodichbds.baocao", "Index"))
                {
                    var model = _db.GiaGiaoDichBDSDm.Where(t => t.Manhom == manhom).ToList();

                    var modellk = _db.GiaGiaoDichBDS.FirstOrDefault(t => t.Thoidiem.Year == namlk && t.Manhom == manhom);

                    var modelbc = _db.GiaGiaoDichBDS.FirstOrDefault(t => t.Thoidiem.Year == nambc && t.Manhom == manhom);

                    ViewData["TenNhom"] = _db.GiaGiaoDichBDSNhom.FirstOrDefault(t => t.Manhom == manhom).Tennhom;
                    ViewData["Namlk"] = namlk;
                    ViewData["Nambc"] = nambc;
                    ViewData["Title"] = "Báo cáo tổng hợp giá nhà cho thuê sinh viên";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dg_giaodichbds";
                    ViewData["MenuLv3"] = "menu_dg_giaodichbds_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichBDS/BaoCao/Bc1.cshtml", model);
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
