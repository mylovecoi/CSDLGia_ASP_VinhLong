using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaHhDvk
{
    public class GiaHhDvkBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaHhDvkBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaHhDvk/BaoCao")]
        [HttpGet]
        public IActionResult Index(string Matt)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia..baocao", "Index"))
                {
                    var nhomtn = _db.GiaHhDvkNhom.Where(t => t.Theodoi == "TD").ToList();
                    if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                    {
                        ViewData["Matt"] = nhomtn.FirstOrDefault().Matt;
                    }
                    else
                    {
                        ViewData["Matt"] = Matt;
                    }
                    var model = _db.GiaHhDvkTh;
                    ViewData["DsDonVi"] = _db.DsDonVi.Where(x => x.ChucNang != "QUANTRI");
                    ViewData["NhomHhDv"] = nhomtn;
                    ViewData["Nam"] = DateTime.Now.Year;
                    ViewData["Title"] = "Báo cáo tổng hợp giá HH-DV khác";
                    ViewData["MenuLv1"] = "menu_hhdvk";
                    ViewData["MenuLv2"] = "menu_hhdvk_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaHhDvk/BaoCao/Index.cshtml", model);
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
        [Route("GiaHhDvk/Bc1")]
        [HttpPost]
        public IActionResult Bc1(string dv, DateTime tungay, DateTime denngay, string matt)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.baocao", "Index"))
                {
                    var getHs = _db.GiaHhDvk.Where(x => x.Matt == matt && x.Madv == dv
                                                && x.Thoidiem >= tungay && x.Thoidiem <= denngay
                                                && x.Trangthai == "HT").Select(x => x.Mahs).ToArray();

                    var getHsCt = _db.GiaHhDvkCt.Where(x => getHs.Contains(x.Mahs) && x.Gia != 0);
                    ViewData["Title"] = "Báo cáo tổng hợp";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "";
                    ViewData["MenuLv3"] = "";
                    return View("Views/Admin/Manages/DinhGia/GiaHhDvk/BaoCao/Bc1.cshtml", getHsCt);
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
        [Route("GiaHhDvk/Bc2")]
        [HttpPost]
        public IActionResult Bc2(string kybc, string kybclk)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia..baocao", "Index"))
                {
                    var getHs = _db.GiaHhDvkCtTh.Where(x => x.Mahs == kybc);

                    var getHsLk = _db.GiaHhDvkCtTh.Where(x => x.Mahs == kybclk);
                    foreach (var hs in getHs)
                    {
                        foreach (var hslk in getHsLk)
                        {
                            if (hs.Mahhdv == hslk.Mahhdv)
                            {
                                hs.Gialk = hslk.Gia;
                                _db.GiaHhDvkCtTh.Update(hs);
                            }
                        }
                    }
                    _db.SaveChanges();
                    ViewData["Title"] = "Báo cáo tổng hợp";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "";
                    ViewData["MenuLv3"] = "";
                    return View("Views/Admin/Manages/DinhGia/GiaHhDvk/BaoCao/Bc2.cshtml", getHs);
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
