using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaXayDungMoi
{
    public class GiaXayDungMoiBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaXayDungMoiBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaXayDungMoi/BaoCao")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.xaydungmoi.baocao", "Index"))
                {
                    var nhomtn = _db.GiaXayDungMoiNhom.Where(t => t.Theodoi == "TD").ToList();

                    ViewData["NhomTn"] = nhomtn;
                    ViewData["Nam"] = DateTime.Now.Year;
                    ViewData["Title"] = "Báo cáo tổng hợp giá xây dựng mới";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dg_xaydungmoi";
                    ViewData["MenuLv3"] = "menu_dg_xaydungmoi_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaXayDungMoi/BaoCao/Index.cshtml");
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

        [Route("GiaXayDungMoi/Bc1")]
        [HttpPost]
        public IActionResult Bc1(string manhom, int namlk, int nambc)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.xaydungmoi.baocao", "Index"))
                {
                    var model = _db.GiaXayDungMoiDm.Where(t => t.Manhom == manhom).ToList();

                    var modellk = _db.GiaXayDungMoi.FirstOrDefault(t => t.Thoidiem.Year == namlk && t.Manhom == manhom);

                    var modelbc = _db.GiaXayDungMoi.FirstOrDefault(t => t.Thoidiem.Year == nambc && t.Manhom == manhom);

                    ViewData["TenNhom"] = _db.GiaXayDungMoiNhom.FirstOrDefault(t => t.Manhom == manhom).Tennhom;
                    ViewData["Namlk"] = namlk;
                    ViewData["Nambc"] = nambc;
                    ViewData["Title"] = "Báo cáo tổng hợp giá xây dựng mới";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dg_xaydungmoi";
                    ViewData["MenuLv3"] = "menu_dg_xaydungmoi_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaXayDungMoi/BaoCao/Bc1.cshtml", model);
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
