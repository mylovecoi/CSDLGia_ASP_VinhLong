using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Hosting;
using CSDLGia_ASP.ViewModels.Manages.KeKhaiGia;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class DuLieuTapHuanController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IWebHostEnvironment _env;

        public DuLieuTapHuanController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment, IWebHostEnvironment hostingEnv)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
            _env = hostingEnv;
        }

        [Route("DuLieuTapHuan")]
        [HttpGet]
        public IActionResult Index()
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            //{
            //    if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.dlth", "Index"))
            //    {
            var model = _db.DuLieuTapHuan.ToList();
            string wwwRootPath = _env.WebRootPath;
            if (Directory.Exists(wwwRootPath + "/UpLoad/File/HuongDanSuDung"))
            {
                //Lấy danh sách tất cả các tập tin trong thư mục
                string[] files = Directory.GetFiles(wwwRootPath + "/UpLoad/File/HuongDanSuDung");
                ViewData["FileHDSD"] = Path.GetExtension(files[0]);
            }
            ViewData["Title"] = "Danh sách dữ liệu tập huấn";
            ViewData["MenuLv1"] = "menu_hethong";
            ViewData["MenuLv2"] = "menu_qthethong";
            ViewData["MenuLv3"] = "menu_dlth";
            return View("Views/Admin/Systems/DuLieuTapHuan/Index.cshtml", model);
            //    }
            //    else
            //    {
            //        ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
            //        return View("Views/Admin/Error/Page.cshtml");
            //    }
            //}
            //else
            //{
            //    return View("Views/Admin/Error/SessionOut.cshtml");
            //}
        }

        [Route("DuLieuTapHuan/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.dlth", "Create"))
                {
                    ViewData["SapXep"] = _db.DuLieuTapHuan.Any() ? _db.DuLieuTapHuan.Max(t => t.STTSapxep) : 0;
                    ViewData["Title"] = "Thêm mới dữ liệu tập huấn";
                    ViewData["MenuLv1"] = "menu_hethong";
                    ViewData["MenuLv2"] = "menu_qthethong";
                    ViewData["MenuLv3"] = "menu_dlth";
                    return View("Views/Admin/Systems/DuLieuTapHuan/Create.cshtml");
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

        [HttpPost]
        public async Task<IActionResult> Store(DuLieuTapHuan request, IFormFile FileGocUpload, IFormFile FileMauUpload)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.dlth", "Create"))
                {
                    if (FileGocUpload != null && FileGocUpload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(FileGocUpload.FileName);
                        string extension = Path.GetExtension(FileGocUpload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DuLieuTapHuan/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await FileGocUpload.CopyToAsync(FileStream);
                        }
                        request.FileGoc = filename;
                    }

                    if (FileMauUpload != null && FileMauUpload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(FileMauUpload.FileName);
                        string extension = Path.GetExtension(FileMauUpload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DuLieuTapHuan/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await FileMauUpload.CopyToAsync(FileStream);
                        }
                        request.FileMau = filename;
                    }

                    var model = new DuLieuTapHuan
                    {
                        STTSapxep = request.STTSapxep,
                        TenChucNang = request.TenChucNang,
                        NoiDung = request.NoiDung,
                        FileMau = request.FileMau,
                        FileGoc = request.FileGoc,
                        Created_At = DateTime.Now,
                        Updated_At = DateTime.Now,
                    };
                    _db.DuLieuTapHuan.Add(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "DuLieuTapHuan");
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

        [Route("DuLieuTapHuan/Edit")]
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.dlth", "Edit"))
                {
                    var model = _db.DuLieuTapHuan.FirstOrDefault(t => t.Id == Id);

                    ViewData["Title"] = "Chỉnh sửa dữ liệu tập huấn";
                    ViewData["MenuLv1"] = "menu_hethong";
                    ViewData["MenuLv2"] = "menu_qthethong";
                    ViewData["MenuLv3"] = "menu_dlth";
                    return View("Views/Admin/Systems/DuLieuTapHuan/Edit.cshtml", model);
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

        [HttpPost]
        public async Task<IActionResult> Update(DuLieuTapHuan request, IFormFile FileGocUpload, IFormFile FileMauUpload)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.dlth", "Edit"))
                {
                    if (FileGocUpload != null && FileGocUpload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(FileGocUpload.FileName);
                        string extension = Path.GetExtension(FileGocUpload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DuLieuTapHuan/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await FileGocUpload.CopyToAsync(FileStream);
                        }
                        request.FileGoc = filename;
                    }

                    if (FileMauUpload != null && FileMauUpload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(FileMauUpload.FileName);
                        string extension = Path.GetExtension(FileMauUpload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DuLieuTapHuan/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await FileMauUpload.CopyToAsync(FileStream);
                        }
                        request.FileMau = filename;
                    }

                    var model = _db.DuLieuTapHuan.FirstOrDefault(t => t.Id == request.Id);
                    model.TenChucNang = request.TenChucNang;
                    model.NoiDung = request.NoiDung;
                    model.STTSapxep = request.STTSapxep;
                    model.FileGoc = request.FileGoc;
                    model.FileMau = request.FileMau;
                    model.Updated_At = DateTime.Now;

                    _db.DuLieuTapHuan.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "DuLieuTapHuan");
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

        [Route("DuLieuTapHuan/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.dlth", "Delete"))
                {
                    var model = _db.DuLieuTapHuan.FirstOrDefault(t => t.Id == id_delete);
                    _db.DuLieuTapHuan.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "DuLieuTapHuan");
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

        [Route("DuLieuTapHuan/Show")]
        [HttpPost]
        public JsonResult Show(int Id)
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            //{
            var model = _db.DuLieuTapHuan.FirstOrDefault(t => t.Id == Id);
            string result = "<div class='modal-body' id='frm_show'>";
            result += "<div class='row'>";
            result += "<div class='col-xl-12'>";
            result += "<div class='form-group fv-plugins-icon-container'>";
            result += "<label style='font-weight:bold'>Tên chức năng: </label>";
            result += "<span style='color:blue'>" + model.TenChucNang + "</span>";
            result += "</div>";
            result += "</div>";
            result += "<div class='col-xl-12'>";
            result += "<div class='form-group fv-plugins-icon-container'>";
            result += "<label style='font-weight:bold'>Nội dung: </label>";
            result += "<span style='color:blue'>" + model.NoiDung + "</span>";
            result += "</div>";
            result += "</div>";
            result += "<div class='col-xl-12'>";
            result += "<div class='form -group fv-plugins-icon-container'>";
            result += "<label style='font-weight:bold'>File gốc</label>";

            if (model.FileGoc != null && model.FileGoc.Length > 0)
            {
                result += "<p>";
                result += "-";
                result += "<a href='/UpLoad/File/DuLieuTapHuan/" + model.FileGoc + "' target='_blank' class='btn btn-link'";
                result += " onclick='window.open(`/UpLoad/File/DuLieuTapHuan/" + model.FileGoc + "`, `mywin`, `left=20,top=20,width=500,height=500,toolbar=1,resizable=0`); return false;'>";
                result += model.FileGoc + "</a>";
                result += "</p>";
            }

            result += "</div>";
            result += "</div>";

            result += "<div class='col-xl-12'>";
            result += "<div class='form -group fv-plugins-icon-container'>";
            result += "<label style='font-weight:bold'>File mẫu</label>";

            if (model.FileMau != null && model.FileMau.Length > 0)
            {
                result += "<p>";
                result += "-";
                result += "<a href='/UpLoad/File/DuLieuTapHuan/" + model.FileMau + "' target='_blank' class='btn btn-link'";
                result += " onclick='window.open(`/UpLoad/File/DuLieuTapHuan/" + model.FileMau + "`, `mywin`, `left=20,top=20,width=500,height=500,toolbar=1,resizable=0`); return false;'>";
                result += model.FileMau + "</a>";
                result += "</p>";
            }

            result += "</div>";
            result += "</div>";

            result += "</div>";
            result += "</div>";

            var data = new { status = "success", message = result };
            return Json(data);
            //}
            //else
            //{
            //    var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
            //    return Json(data);
            //}
        }
    }
}
