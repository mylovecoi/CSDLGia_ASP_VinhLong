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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaHhDvk
{
    public class GiaHhDvkThController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GiaHhDvkThController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("GiaHhDvk/TongHop")]
        [HttpGet]
        public IActionResult Index(string Thang, string Nam, string Matt)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.th", "Index"))
                {
                    var dsnhom = _db.GiaHhDvkNhom.ToList();
                    if (dsnhom.Count > 0)
                    {
                        var model = _db.GiaHhDvkTh.ToList();
                        var hoso = _db.GiaHhDvk.ToList();
                        if (string.IsNullOrEmpty(Matt))
                        {
                            Matt = dsnhom.OrderBy(t => t.Id).Select(t => t.Matt).First();
                            model = _db.GiaHhDvkTh.Where(t => t.Matt == Matt).ToList();
                            hoso = _db.GiaHhDvk.Where(t => t.Matt == Matt).ToList();
                        }
                        else
                        {
                            model = _db.GiaHhDvkTh.Where(t => t.Matt == Matt).ToList();
                            hoso = _db.GiaHhDvk.Where(t => t.Matt == Matt).ToList();
                        }

                        if (string.IsNullOrEmpty(Nam))
                        {
                            Nam = Helpers.ConvertYearToStr(DateTime.Now.Year);
                            model = model.Where(t => t.Nam == Nam).ToList();
                            hoso = hoso.Where(t => t.Nam == Nam).ToList();
                        }
                        else
                        {
                            if (Nam != "all")
                            {
                                model = model.Where(t => t.Nam == Nam).ToList();
                                hoso = hoso.Where(t => t.Nam == Nam).ToList();
                            }
                            else
                            {
                                model = model.ToList();
                                hoso = hoso.ToList();
                            }
                        }

                        if (string.IsNullOrEmpty(Thang))
                        {
                            Thang = Helpers.ConvertYearToStr(DateTime.Now.Month);
                            model = model.Where(t => t.Thang == Thang).ToList();
                            hoso = hoso.Where(t => t.Thang == Thang).ToList();
                        }
                        else
                        {
                            if (Thang != "all")
                            {
                                model = model.Where(t => t.Thang == Thang).ToList();
                                hoso = hoso.Where(t => t.Thang == Thang).ToList();
                            }
                            else
                            {
                                model = model.ToList();
                                hoso = hoso.ToList();
                            }
                        }

                        var model_join = (from th in model
                                          join nhom in dsnhom on th.Matt equals nhom.Matt
                                          select new GiaHhDvkTh
                                          {
                                              Id = th.Id,
                                              Mahs = th.Mahs,
                                              Matt = th.Matt,
                                              Tentt = nhom.Tentt,
                                              Ngaybc = th.Ngaybc,
                                              Ngaychotbc = th.Ngaychotbc,
                                              Sobc = th.Sobc,
                                              Ttbc = th.Ttbc,
                                              Thang = th.Thang,
                                              Nam = th.Nam,
                                              Trangthai = th.Trangthai,
                                          }).ToList();

                        ViewData["Thang"] = Thang;
                        ViewData["Nam"] = Nam;
                        ViewData["Matt"] = Matt;
                        ViewData["Dsnhom"] = dsnhom;
                        ViewData["Hoso"] = hoso;
                        ViewData["Title"] = "Tổng hợp giá hàng hóa dịch vụ khác";
                        ViewData["MenuLv1"] = "menu_hhdvk";
                        ViewData["MenuLv2"] = "menu_hhdvk_th";
                        return View("Views/Admin/Manages/DinhGia/GiaHhDvk/TongHop/Index.cshtml", model_join);
                    }
                    else
                    {
                        ViewData["Title"] = "Tổng hợp giá hàng hóa dịch vụ khác";
                        ViewData["Messages"] = "Hệ thống chưa có danh mục nhóm hàng hóa dịch vụ.";
                        ViewData["MenuLv1"] = "menu_hhdvk";
                        ViewData["MenuLv2"] = "menu_hhdvk_th";
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

        [Route("GiaHhDvk/TongHop/Create")]
        [HttpGet]
        public IActionResult Create(string Matt, string Thang, string Nam, string[] Hoso)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.th", "Create"))
                {
                    var modelct = _db.GiaHhDvkCt.Where(t => Hoso.Contains(t.Mahs)).ToList();

                    /*return Ok(modelct);*/

                    ViewData["Title"] = "Tổng hợp giá hàng hóa dịch vụ khác thêm mới";
                    ViewData["MenuLv1"] = "menu_hhdvk";
                    ViewData["MenuLv2"] = "menu_hhdvk_th";
                    return View("Views/Admin/Manages/DinhGia/GiaHhDvk/TongHop/Create.cshtml", modelct);

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
