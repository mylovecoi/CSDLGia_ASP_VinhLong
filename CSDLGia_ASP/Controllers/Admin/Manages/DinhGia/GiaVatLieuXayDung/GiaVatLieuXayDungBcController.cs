using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaVatLieuXayDung
{
    public class GiaVatLieuXayDungBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaVatLieuXayDungBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaVatLieuXayDung/BaoCao")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giavatlieuxaydung.baocao", "Index"))
                {
                    var nhomtn = _db.GiaVatLieuXayDungNhom.Where(t => t.Theodoi == "TD").ToList();

                    ViewData["NhomTn"] = nhomtn;
                    ViewData["Nam"] = DateTime.Now.Year;
                    ViewData["Title"] = "Báo cáo tổng hợp giá vật liệu xây dựng";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_giakhac_giavatlieuxaydung";
                    ViewData["MenuLv3"] = "menu_giakhac_giavatlieuxaydung_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaVatLieuXayDung/BaoCao/Index.cshtml");
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

        [Route("GiaVatLieuXayDung/Bc1")]
        [HttpPost]
        public IActionResult Bc1(string manhom, int namlk, int nambc)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giavatlieuxaydung.baocao", "Index"))
                {
                    var model = _db.GiaVatLieuXayDungDm.Where(t => t.Manhom == manhom).ToList();

                    var modellk = _db.GiaVatLieuXayDung.FirstOrDefault(t => t.Thoidiem.Year == namlk && t.Manhom == manhom);

                    var modelbc = _db.GiaVatLieuXayDung.FirstOrDefault(t => t.Thoidiem.Year == nambc && t.Manhom == manhom);

                    ViewData["TenNhom"] = _db.GiaVatLieuXayDungNhom.FirstOrDefault(t => t.Manhom == manhom).Tennhom;
                    ViewData["Namlk"] = namlk;
                    ViewData["Nambc"] = nambc;
                    ViewData["Title"] = "Báo cáo tổng hợp giá vật liệu xây dựng";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_giakhac_giavatlieuxaydung";
                    ViewData["MenuLv3"] = "menu_giakhac_giavatlieuxaydung_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaVatLieuXayDung/BaoCao/Bc1.cshtml", model);
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
