using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.KeKhaiGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.KeKhaiGia.KkGiaDvlt
{
    public class KkGiaDvltCskdController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public KkGiaDvltCskdController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("KeKhaiGiaDvlt/ThongTinCskd")]
        [HttpGet]
        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgdvlt.giakk", "Index") ||
                    Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var Manghe = "DVLT";
                    var dsdonvi = (from com in _db.Company
                                   join lvkd in _db.CompanyLvCc.Where(t => t.Manghe == Manghe) on com.Mahs equals lvkd.Mahs
                                   select new VMCompany
                                   {
                                       Id = com.Id,
                                       Manghe = lvkd.Manghe,
                                       Madv = com.Madv,
                                       Madiaban = com.Madiaban,
                                       Mahs = com.Mahs,
                                       Tendn = com.Tendn,
                                       Trangthai = com.Trangthai
                                   }).ToList();

                    if (dsdonvi.Count > 0)
                    {
                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") != null)
                        {
                            Madv = Helpers.GetSsAdmin(HttpContext.Session, "Madv");

                        }
                        else
                        {
                            if (string.IsNullOrEmpty(Madv))
                            {
                                Madv = dsdonvi.OrderBy(t => t.Id).Select(t => t.Madv).First();
                            }
                        }

                        var comct = _db.CompanyLvCc.Where(t => t.Manghe == Manghe && t.Madv == Madv).ToList();

                        if (comct.Count > 0)
                        {
                            var model = _db.KkGiaDvLtCskd.Where(t => t.Madv == Madv).ToList();

                            if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                            {
                                ViewData["DsDonVi"] = dsdonvi;
                            }
                            else
                            {
                                ViewData["DsDonVi"] = dsdonvi.Where(t => t.Madv == Madv);
                            }
                            ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "ADMIN");
                            ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang == "NHAPLIEU");
                            ViewData["Madv"] = Madv;
                            ViewData["Tendn"] = _db.Company.FirstOrDefault(t => t.Madv == Madv).Tendn;
                            ViewData["Title"] = "Danh sách cơ sở kinh doanh dịch vụ lưu trú";
                            ViewData["MenuLv1"] = "menu_kknygia";
                            ViewData["MenuLv2"] = "menu_kkgdvlt";
                            ViewData["MenuLv3"] = "menu_giakkdvltcskd";
                            return View("Views/Admin/Manages/KeKhaiGia/KkGiaDvlt/Cskd/Index.cshtml", model);
                        }
                        else
                        {
                            ViewData["Title"] = "Danh sách cơ sở kinh doanh dịch vụ lưu trú";
                            ViewData["Messages"] = "Cơ sơ kinh doanh kê khai giá dịch vụ lưu trú không thuộc quản lý của doanh nghiệp";
                            ViewData["MenuLv1"] = "menu_kknygia";
                            ViewData["MenuLv2"] = "menu_kkgdvlt";
                            ViewData["MenuLv3"] = "menu_giakkdvltcskd";
                            return View("Views/Admin/Error/ThongBaoLoi.cshtml");
                        }
                    }
                    else
                    {
                        ViewData["Title"] = "Danh sách cơ sở kinh doanh dịch vụ lưu trú";
                        ViewData["Messages"] = "Hệ thống chưa có doanh nghiệp kê khai giá dịch vụ lưu trú.";
                        ViewData["MenuLv1"] = "menu_kknygia";
                        ViewData["MenuLv2"] = "menu_kkgdvlt";
                        ViewData["MenuLv3"] = "menu_giakkdvltcskd";
                        return View("Views/Admin/Error/ThongBaoLoi.cshtml");
                    }

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

        [Route("KeKhaiGiaDvlt/ThongTinCskd/Create")]
        [HttpGet]
        public IActionResult Create(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgdvlt.giakk", "Create") ||
                    Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    ViewData["Madv"] = Madv;
                    ViewData["Tendn"] = _db.Company.FirstOrDefault(t => t.Madv == Madv).Tendn;
                    ViewData["Title"] = " Thêm mới cơ sở kinh doanh dịch vụ lưu trú";
                    ViewData["MenuLv1"] = "menu_kknygia";
                    ViewData["MenuLv2"] = "menu_kkgdvlt";
                    ViewData["MenuLv3"] = "menu_giakkdvltcskd";
                    return View("Views/Admin/Manages/KeKhaiGia/KkGiaDvlt/Cskd/Create.cshtml");
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

        [Route("KeKhaiGiaDvlt/ThongTinCskd/Store")]
        [HttpPost]
        public async Task<IActionResult> Store(KkGiaDvLtCskd request, IFormFile Avatarupload)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgdvlt.giakk", "Create") ||
                    Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    if (Avatarupload != null && Avatarupload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Avatarupload.FileName);
                        string extension = Path.GetExtension(Avatarupload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/Avatar/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Avatarupload.CopyToAsync(FileStream);
                        }
                        request.Avatar = filename;
                    }

                    var model = new KkGiaDvLtCskd
                    {
                        Macskd = request.Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Madv = request.Madv,
                        Tencskd = request.Tencskd,
                        Loaihang = request.Loaihang,
                        Diachikd = request.Diachikd,
                        Telkd = request.Telkd,
                        Link = request.Link,
                        Avatar = request.Avatar,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.KkGiaDvLtCskd.Add(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "KkGiaDvltCskd", new { request.Madv });
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

        [Route("KeKhaiGiaDvlt/ThongTinCskd/Edit")]
        [HttpGet]
        public IActionResult Edit(string Macskd)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgdvlt.giakk", "Edit") ||
                    Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var model = _db.KkGiaDvLtCskd.FirstOrDefault(t => t.Macskd == Macskd);

                    ViewData["Madv"] = model.Madv;
                    ViewData["Avatar"] = model.Avatar;
                    ViewData["Tendn"] = _db.Company.FirstOrDefault(t => t.Madv == model.Madv).Tendn;
                    ViewData["Title"] = " Chỉnh sửa cơ sở kinh doanh dịch vụ lưu trú";
                    ViewData["MenuLv1"] = "menu_kknygia";
                    ViewData["MenuLv2"] = "menu_kkgdvlt";
                    ViewData["MenuLv3"] = "menu_giakkdvltcskd";

                    return View("Views/Admin/Manages/KeKhaiGia/KkGiaDvlt/Cskd/Edit.cshtml", model);
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

        [Route("KeKhaiGiaDvlt/ThongTinCskd/Update")]
        [HttpPost]
        public async Task<IActionResult> Update(KkGiaDvLtCskd request, IFormFile Avatarupload)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgdvlt.giakk", "Edit") ||
                    Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    if (Avatarupload != null && Avatarupload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Avatarupload.FileName);
                        string extension = Path.GetExtension(Avatarupload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/Avatar/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Avatarupload.CopyToAsync(FileStream);
                        }
                        request.Avatar = filename;
                    }

                    var model = _db.KkGiaDvLtCskd.FirstOrDefault(t => t.Macskd == request.Macskd);
                    model.Tencskd = request.Tencskd;
                    model.Loaihang = request.Loaihang;
                    model.Diachikd = request.Diachikd;
                    model.Telkd = request.Telkd;
                    model.Link = request.Link;
                    model.Avatar = request.Avatar;
                    model.Updated_at = DateTime.Now;
                    _db.KkGiaDvLtCskd.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "KkGiaDvltCskd", new { request.Madv });
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
