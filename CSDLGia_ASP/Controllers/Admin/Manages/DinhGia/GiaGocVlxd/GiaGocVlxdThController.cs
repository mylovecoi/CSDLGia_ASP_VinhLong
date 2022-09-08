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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaGocVlxd
{
    public class GiaGocVlxdThController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GiaGocVlxdThController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("GiaGocVlxd/TongHop")]
        [HttpGet]
        public IActionResult Index(string Nam, string Thang)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giagocvlxd.tonghop", "Index"))
                {
                    if (string.IsNullOrEmpty(Nam))
                    {
                        Nam = Helpers.ConvertYearToStr(DateTime.Now.Year);
                    }

                    if (string.IsNullOrEmpty(Thang))
                    {
                        Thang = Helpers.ConvertMonthToStr(DateTime.Now.Month);
                    }

                    var model = _db.GiaGocVlxdTh.Where(t => t.Nam == Nam && t.Thang == Thang);

                    ViewData["Nam"] = Nam;
                    ViewData["Thang"] = Thang;
                    ViewData["Title"] = "Thông tin hồ sơ giá vật liệu xây dựng";
                    ViewData["MenuLv1"] = "menu_giagocvlxd";
                    ViewData["MenuLv2"] = "menu_giagocvlxd_th";
                    return View("Views/Admin/Manages/DinhGia/GiaGocVlxd/TongHop/Index.cshtml", model);

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

        [Route("GiaGocVlxd/TongHop/Create")]
        [HttpGet]
        public IActionResult Create(string Nam, string Thang)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giagocvlxd.tonghop", "Create"))
                {
                    ViewData["Nam"] = Nam;
                    ViewData["Thang"] = Thang;
                    ViewData["Title"] = "Tổng hợp giá gốc vật liệu xây dựng";
                    ViewData["MenuLv1"] = "menu_giagocvlxd";
                    ViewData["MenuLv2"] = "menu_giagocvlxd_th";
                    return View("Views/Admin/Manages/DinhGia/GiaGocVlxd/TongHop/Create.cshtml");
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

        [Route("GiaGocVlxd/TongHop/Store")]
        [HttpPost]
        public async Task<IActionResult> Store(GiaGocVlxdTh request, IFormFile Ipf1upload)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giagocvlxd.tonghop", "Create"))
                {
                    if (Ipf1upload != null && Ipf1upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf1upload.FileName);
                        string extension = Path.GetExtension(Ipf1upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf1upload.CopyToAsync(FileStream);
                        }
                        request.Ipf1 = filename;
                    }

                    var model = new GiaGocVlxdTh
                    {
                        Mahs = DateTime.Now.ToString("yyMMddssmmHH"),
                        Nam = request.Nam,
                        Sobc = request.Sobc,
                        Noidung = request.Noidung,
                        Thang = request.Thang,
                        Ipf1 = request.Ipf1,
                        Congbo = "CHUACONGBO",
                        Trangthai = "CHT",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaGocVlxdTh.Add(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaGocVlxdTh");
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

        [Route("GiaGocVlxd/TongHop/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giagocvlxd.tonghop", "Edit"))
                {
                    var model = _db.GiaGocVlxdTh.FirstOrDefault(t => t.Mahs == Mahs);

                    ViewData["Nam"] = model.Nam;
                    ViewData["Thang"] = model.Thang;
                    ViewData["Title"] = "Tổng hợp giá gốc vật liệu xây dựng";
                    ViewData["MenuLv1"] = "menu_giagocvlxd";
                    ViewData["MenuLv2"] = "menu_giagocvlxd_th";
                    return View("Views/Admin/Manages/DinhGia/GiaGocVlxd/TongHop/Edit.cshtml", model);
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

        [Route("GiaGocVlxd/TongHop/Update")]
        [HttpPost]
        public async Task<IActionResult> Update(GiaGocVlxdTh request, IFormFile Ipf1upload)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giagocvlxd.tonghop", "Edit"))
                {
                    if (Ipf1upload != null && Ipf1upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf1upload.FileName);
                        string extension = Path.GetExtension(Ipf1upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf1upload.CopyToAsync(FileStream);
                        }
                        request.Ipf1 = filename;
                    }

                    var model = _db.GiaGocVlxdTh.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Thang = request.Thang;
                    model.Nam = request.Nam;
                    model.Sobc = request.Sobc;
                    model.Noidung = request.Noidung;
                    model.Ipf1 = request.Ipf1;
                    model.Updated_at = DateTime.Now;

                    _db.GiaGocVlxdTh.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaGocVlxdTh");
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

        [Route("GiaGocVlxd/TongHop/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giagocvlxd.tonghop", "Delete"))
                {
                    var model = _db.GiaGocVlxdTh.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaGocVlxdTh.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaGocVlxdTh");
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

        [Route("GiaGocVlxd/TongHop/Show")]
        [HttpPost]
        public JsonResult Show(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.GiaGocVlxdTh.FirstOrDefault(t => t.Id == Id);
                string result = "<div class='modal-body' id='frm_show'>";
                result += "<div class='row'>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label style='font-weight:bold'>Tháng: </label>";
                result += "<span style='color:blue'>" + model.Thang + "</span>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label style='font-weight:bold'>Năm: </label>";
                result += "<span style='color:blue'>" + model.Nam + "</span>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label style='font-weight:bold'>Số quyết định: </label>";
                result += "<span style='color:blue'>" + model.Sobc + "</span>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label style='font-weight:bold'>Nội dung: </label>";
                result += "<span style='color:blue'>" + model.Noidung + "</span>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form -group fv-plugins-icon-container'>";
                result += "<label style='font-weight:bold'>File đính kèm</label>";
                if (model.Ipf1 != null && model.Ipf1.Length > 0)
                {
                    result += "<p>";
                    result += "<a href='/UpLoad/File/DinhGia/" + model.Ipf1 + "' target='_blank' class='btn btn-link'";
                    result += " onclick='window.open(`/UpLoad/File/DinhGia/" + model.Ipf1 + "`, `mywin`, `left=20,top=20,width=500,height=500,toolbar=1,resizable=0`); return false;'>";
                    result += model.Ipf1 + "</a>";
                    result += "</p>";
                }
                result += "</div>";
                result += "</div>";
                result += "</div>";
                result += "</div>";

                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }

        public IActionResult CongBo(string mahs_cb)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giagocvlxd.tonghop", "Approve"))
                {
                    var model = _db.GiaGocVlxdTh.FirstOrDefault(t => t.Mahs == mahs_cb);

                    model.Trangthai = "CB";
                    model.Congbo = "DACONGBO";

                    _db.GiaGocVlxdTh.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaGocVlxdTh");
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

        public IActionResult HuyCongBo(string mahs_hcb)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giagocvlxd.tonghop", "Approve"))
                {
                    var model = _db.GiaGocVlxdTh.FirstOrDefault(t => t.Mahs == mahs_hcb);

                    model.Trangthai = "HCB";
                    model.Congbo = "CHUACONGBO";

                    _db.GiaGocVlxdTh.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaGocVlxdTh");
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
