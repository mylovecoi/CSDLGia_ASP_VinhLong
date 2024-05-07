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
    public class HuongDanSuDungController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HuongDanSuDungController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("HuongDanSuDung")]
        [HttpGet]
        public IActionResult Index()
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            //{
            //    if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.hdsd", "Index"))
            //    {
            var model = _db.HuongDanSuDung.ToList();
            ViewData["Title"] = "Danh sách tài liệu hướng dẫn sử dụng";
            ViewData["MenuLv1"] = "menu_hethong";
            ViewData["MenuLv2"] = "menu_qthethong";
            ViewData["MenuLv3"] = "menu_hdsd";
            return View("Views/Admin/Systems/HuongDanSuDung/Index.cshtml", model);
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

        [Route("HuongDanSuDung/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.hdsd", "Create"))
                {

                    ViewData["Title"] = "Thêm mới tài liệu hướng dẫn sử dụng";
                    ViewData["MenuLv1"] = "menu_hethong";
                    ViewData["MenuLv2"] = "menu_qthethong";
                    ViewData["MenuLv3"] = "menu_hdsd";
                    return View("Views/Admin/Systems/HuongDanSuDung/Create.cshtml");
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

        [Route("HuongDanSuDung/Store")]
        [HttpPost]
        public async Task<IActionResult> Store(HuongDanSuDung request, IFormFile FileGocUpload, IFormFile FileMauUpload)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.hdsd", "Create"))
                {
                    if (FileGocUpload != null && FileGocUpload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(FileGocUpload.FileName);
                        string extension = Path.GetExtension(FileGocUpload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/HuongDanSuDung/", filename);
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
                        string path = Path.Combine(wwwRootPath + "/Upload/File/HuongDanSuDung/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await FileMauUpload.CopyToAsync(FileStream);
                        }
                        request.FileMau = filename;
                    }

                    var model = new HuongDanSuDung
                    {
                        STTSapxep = request.STTSapxep,
                        TenChucNang = request.TenChucNang,
                        NoiDung = request.NoiDung,
                        FileMau = request.FileMau,
                        FileGoc = request.FileGoc,
                        Created_At = DateTime.Now,
                        Updated_At = DateTime.Now,
                    };
                    _db.HuongDanSuDung.Add(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "HuongDanSuDung");
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

        [Route("HuongDanSuDung/Edit")]
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.hdsd", "Edit"))
                {
                    var model = _db.HuongDanSuDung.FirstOrDefault(t => t.Id == Id);

                    ViewData["Title"] = "Chỉnh sửa tài liệu hướng dẫn sử dụng";
                    ViewData["MenuLv1"] = "menu_hethong";
                    ViewData["MenuLv2"] = "menu_qthethong";
                    ViewData["MenuLv3"] = "menu_hdsd";
                    return View("Views/Admin/Systems/HuongDanSuDung/Edit.cshtml", model);
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

        [Route("HuongDanSuDung/Update")]
        [HttpPost]
        public async Task<IActionResult> Update(HuongDanSuDung request, IFormFile FileGocUpload, IFormFile FileMauUpload)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.hdsd", "Edit"))
                {
                    if (FileGocUpload != null && FileGocUpload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(FileGocUpload.FileName);
                        string extension = Path.GetExtension(FileGocUpload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/HuongDanSuDung/", filename);
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
                        string path = Path.Combine(wwwRootPath + "/Upload/File/HuongDanSuDung/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await FileMauUpload.CopyToAsync(FileStream);
                        }
                        request.FileMau = filename;
                    }

                    var model = _db.HuongDanSuDung.FirstOrDefault(t => t.Id == request.Id);
                    model.TenChucNang = request.TenChucNang;
                    model.NoiDung = request.NoiDung;
                    model.STTSapxep = request.STTSapxep;
                    model.FileGoc = request.FileGoc;
                    model.FileMau = request.FileMau;
                    model.Updated_At = DateTime.Now;

                    _db.HuongDanSuDung.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "HuongDanSuDung");
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

        [Route("HuongDanSuDung/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.hdsd", "Delete"))
                {
                    var model = _db.HuongDanSuDung.FirstOrDefault(t => t.Id == id_delete);
                    _db.HuongDanSuDung.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "HuongDanSuDung");
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

        [Route("HuongDanSuDung/Show")]
        [HttpPost]
        public JsonResult Show(int Id)
        {
            //if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            //{
            var model = _db.HuongDanSuDung.FirstOrDefault(t => t.Id == Id);
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
                result += "<a href='/UpLoad/File/HuongDanSuDung/" + model.FileGoc + "' target='_blank' class='btn btn-link'";
                result += " onclick='window.open(`/UpLoad/File/VbQlNn/" + model.FileGoc + "`, `mywin`, `left=20,top=20,width=500,height=500,toolbar=1,resizable=0`); return false;'>";
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
                result += "<a href='/UpLoad/File/HuongDanSuDung/" + model.FileMau + "' target='_blank' class='btn btn-link'";
                result += " onclick='window.open(`/UpLoad/File/VbQlNn/" + model.FileMau + "`, `mywin`, `left=20,top=20,width=500,height=500,toolbar=1,resizable=0`); return false;'>";
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
