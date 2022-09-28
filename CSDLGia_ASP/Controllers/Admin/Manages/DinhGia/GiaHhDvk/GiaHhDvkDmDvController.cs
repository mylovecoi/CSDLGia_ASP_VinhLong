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
using OfficeOpenXml;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.EntityFrameworkCore;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaHhDvk
{
    public class GiaHhDvkDmDvController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _env;

        public GiaHhDvkDmDvController(CSDLGiaDBContext db, IWebHostEnvironment hostingEnv)
        {
            _db = db;
            _env = hostingEnv;
        }

        [Route("GiaHhDvkDmDonvi")]
        [HttpGet]
        public IActionResult Index(string Madv, string Matt)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.dmdv", "Index"))
                {
                    var dsdonvi = (from db in _db.DsDiaBan.Where(t => t.Level != "ADMIN")
                                   join dv in _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI") on db.MaDiaBan equals dv.MaDiaBan
                                   select new VMDsDonVi
                                   {
                                       Id = dv.Id,
                                       TenDiaBan = db.TenDiaBan,
                                       MaDiaBan = dv.MaDiaBan,
                                       TenDv = dv.TenDv,
                                       MaDv = dv.MaDv,
                                   }).ToList();
                    var dstt = _db.GiaHhDvkNhom.ToList();
                    if (dsdonvi.Count > 0)
                    {
                        if(dstt.Count > 0)
                        {
                            if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") != null)
                            {
                                Madv = Helpers.GetSsAdmin(HttpContext.Session, "Madv");
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(Madv))
                                {
                                    Madv = dsdonvi.OrderBy(t => t.Id).Select(t => t.MaDv).First();
                                }
                            }
                            var model = _db.GiaHhDvkDmDv.Where(t => t.Madv == Madv).ToList();

                            if (string.IsNullOrEmpty(Matt))
                            {
                                Matt = _db.GiaHhDvkNhom.OrderBy(t => t.Id).Select(t => t.Matt).First();
                            }

                            model = model.Where(t => t.Matt == Matt).ToList();

                            var check = model.Select(t => t.Mahhdv).ToArray();

                            var dmhhdvk = _db.GiaHhDvkDm.Where(a => a.Matt == Matt && !check.Contains(a.Mahhdv)).ToList();

                            ViewData["Madv"] = Madv;
                            ViewData["Matt"] = Matt;
                            ViewData["Tentt"] = _db.GiaHhDvkNhom.FirstOrDefault(t => t.Matt == Matt).Tentt;
                            ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "ADMIN").OrderByDescending(t => t.Level);
                            ViewData["DsDonVi"] = dsdonvi;
                            ViewData["DsTt"] = dstt;
                            ViewData["DmHhDvk"] = dmhhdvk;
                            ViewData["Title"] = "Thông tin chi tiết hàng hóa dịch vụ theo đơn vị";
                            ViewData["MenuLv1"] = "menu_hhdvk";
                            ViewData["MenuLv2"] = "menu_hhdvk_dmdv";
                            return View("Views/Admin/Manages/DinhGia/GiaHhDvk/DanhMuc/DonVi/Index.cshtml", model);
                        }
                        else
                        {
                            ViewData["Messages"] = "Hệ thống chưa danh mục nhóm hàng hóa dịch vụ.";
                            ViewData["Title"] = "Thông tin chi tiết hàng hóa dịch vụ theo đơn vị";
                            ViewData["MenuLv1"] = "menu_hhdvk";
                            ViewData["MenuLv2"] = "menu_hhdvk_dmdv";
                            return View("Views/Admin/Error/ThongBaoLoi.cshtml");
                        }
                    }
                    else
                    {
                        ViewData["Messages"] = "Hệ thống chưa có đơn vị định giá hàng hóa dịch vụ khác.";
                        ViewData["Title"] = "Thông tin chi tiết hàng hóa dịch vụ theo đơn vị";
                        ViewData["MenuLv1"] = "menu_hhdvk";
                        ViewData["MenuLv2"] = "menu_hhdvk_dmdv";
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

        [Route("GiaHhDvkDmDonvi/Store")]
        [HttpPost]
        public JsonResult Store(string Matt, string Madv, string[] DmHhDvkDv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.dmdv", "Create"))
                {
                    var model = _db.GiaHhDvkDm.Where(t => t.Matt == Matt);
                    if (DmHhDvkDv != null || !string.IsNullOrEmpty(DmHhDvkDv.ToString()))
                    {
                        model = _db.GiaHhDvkDm.Where(t => t.Matt == Matt && DmHhDvkDv.Contains(t.Mahhdv)).OrderBy(t => t.Mahhdv);
                    }
                    else
                    {
                        model = _db.GiaHhDvkDm.Where(a => a.Matt == Matt).OrderBy(t => t.Mahhdv);
                    }

                    var dmdv = new List<GiaHhDvkDmDv>();

                    var check = _db.GiaHhDvkDmDv.Where(t => t.Madv == Madv && t.Matt == Matt).Select(t => t.Mahhdv).ToArray();

                    foreach (var item in model)
                    {
                        if (check.Contains(item.Mahhdv))
                        {
                            continue;
                        }
                        dmdv.Add(new GiaHhDvkDmDv()
                        {
                            Manhom = item.Manhom,
                            Matt = item.Matt,
                            Mahhdv = item.Mahhdv,
                            Tenhhdv = item.Tenhhdv,
                            Dacdiemkt = item.Dacdiemkt,
                            Dvt = item.Dvt,
                            Xuatxu = item.Xuatxu,
                            Theodoi = item.Theodoi,
                            Madv = Madv,
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        });
                    }
                    _db.GiaHhDvkDmDv.AddRange(dmdv);
                    _db.SaveChanges();

                    var data = new { status = "success", message = "Thêm mới thành công!" };
                    return Json(data);
                }
                else
                {
                    var data = new { status = "error", message = "Bạn không có quyền thực hiện chức năng này!!!" };
                    return Json(data);
                }
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }

        [Route("GiaHhDvkDmDonvi/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.dmdv", "Delete"))
                {
                    var model = _db.GiaHhDvkDmDv.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaHhDvkDmDv.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaHhDvkDmDonvi", new { Madv = model.Madv, Matt = model.Matt });
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
