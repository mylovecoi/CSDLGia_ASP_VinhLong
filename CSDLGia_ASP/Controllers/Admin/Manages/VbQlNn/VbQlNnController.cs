using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.VbQlNn
{
    public class VbQlNnController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public VbQlNnController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("VanBanQlNnVeGia")]
        [HttpGet]
        public IActionResult Index(string Phanloai, string Loaivb)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.vbqlnn.ds", "Index"))
                {
                    if (string.IsNullOrEmpty(Phanloai))
                    {
                        Phanloai = "gia";
                    }
                    if (string.IsNullOrEmpty(Loaivb))
                    {
                        Loaivb = "all";
                    }
                    var model = _db.VbQlNn.Where(t => t.Phanloai == Phanloai);
                    if (Loaivb != "all")
                    {
                        model = model.Where(t => t.Loaivb == Loaivb);
                    }

                    ViewData["Phanloai"] = Phanloai;
                    ViewData["Loaivb"] = Loaivb;
                    ViewData["Title"] = "Văn bản quản lý nhà nước về giá";
                    ViewData["MenuLv1"] = "menu_vbql";
                    ViewData["MenuLv2"] = "menu_vbqlds";
                    return View("Views/Admin/Manages/VbQlNn/Index.cshtml", model);
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

        [Route("VanBanQlNnVeGia/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.vbqlnn.ds", "Create"))
                {
                    ViewData["Dsdonvi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Title"] = "Thêm mới văn bản quản lý nhà nước về giá";
                    ViewData["MenuLv1"] = "menu_vbql";
                    ViewData["MenuLv2"] = "menu_vbqlds";
                    return View("Views/Admin/Manages/VbQlNn/Create.cshtml");
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

        [Route("VanBanQlNnVeGia/Store")]
        [HttpPost]
        public async Task<IActionResult> Store(CSDLGia_ASP.Models.Manages.VbQlNn.VbQlNn request,
            IFormFile Ipf1upload, IFormFile Ipf2upload, IFormFile Ipf3upload, IFormFile Ipf4upload, IFormFile Ipf5upload)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.vbqlnn.ds", "Create"))
                {
                    if (Ipf1upload != null && Ipf1upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf1upload.FileName);
                        string extension = Path.GetExtension(Ipf1upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/VbQlNn/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf1upload.CopyToAsync(FileStream);
                        }
                        request.Ipf1 = filename;
                    }

                    if (Ipf2upload != null && Ipf2upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf2upload.FileName);
                        string extension = Path.GetExtension(Ipf2upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/VbQlNn/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf2upload.CopyToAsync(FileStream);
                        }
                        request.Ipf2 = filename;
                    }

                    if (Ipf3upload != null && Ipf3upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf3upload.FileName);
                        string extension = Path.GetExtension(Ipf3upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/VbQlNn/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf3upload.CopyToAsync(FileStream);
                        }
                        request.Ipf3 = filename;
                    }

                    if (Ipf4upload != null && Ipf4upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf4upload.FileName);
                        string extension = Path.GetExtension(Ipf4upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/VbQlNn/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf4upload.CopyToAsync(FileStream);
                        }
                        request.Ipf4 = filename;
                    }

                    if (Ipf5upload != null && Ipf5upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf5upload.FileName);
                        string extension = Path.GetExtension(Ipf5upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/VbQlNn/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf5upload.CopyToAsync(FileStream);
                        }
                        request.Ipf5 = filename;
                    }

                    var model = new CSDLGia_ASP.Models.Manages.VbQlNn.VbQlNn
                    {
                        Mahs = request.Phanloai + "_" + request.Loaivb + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Madv = request.Madv,
                        Phanloai = request.Phanloai,
                        Loaivb = request.Loaivb,
                        Dvbanhanh = request.Dvbanhanh,
                        Kyhieuvb = request.Kyhieuvb,
                        Thoidiem = request.Thoidiem,
                        Ngayapdung = request.Ngayapdung,
                        Ngaybanhanh = request.Ngaybanhanh,
                        Tieude = request.Tieude,
                        Ghichu = request.Ghichu,
                        Ipf1 = request.Ipf1,
                        Ipf2 = request.Ipf2,
                        Ipf3 = request.Ipf3,
                        Ipf4 = request.Ipf4,
                        Ipf5 = request.Ipf5,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.VbQlNn.Add(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "VbQlNn");
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

        [Route("VanBanQlNnVeGia/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.vbqlnn.ds", "Edit"))
                {
                    var model = _db.VbQlNn.FirstOrDefault(t => t.Mahs == Mahs);

                    ViewData["Dsdonvi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Title"] = "Chỉnh sửa văn bản quản lý nhà nước về giá";
                    ViewData["MenuLv1"] = "menu_vbql";
                    ViewData["MenuLv2"] = "menu_vbqlds";
                    return View("Views/Admin/Manages/VbQlNn/Edit.cshtml", model);
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

        [Route("VanBanQlNnVeGia/Update")]
        [HttpPost]
        public async Task<IActionResult> Update(CSDLGia_ASP.Models.Manages.VbQlNn.VbQlNn request,
            IFormFile Ipf1upload, IFormFile Ipf2upload, IFormFile Ipf3upload, IFormFile Ipf4upload, IFormFile Ipf5upload)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.vbqlnn.ds", "Edit"))
                {
                    if (Ipf1upload != null && Ipf1upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf1upload.FileName);
                        string extension = Path.GetExtension(Ipf1upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/VbQlNn/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf1upload.CopyToAsync(FileStream);
                        }
                        request.Ipf1 = filename;
                    }

                    if (Ipf2upload != null && Ipf2upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf2upload.FileName);
                        string extension = Path.GetExtension(Ipf2upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/VbQlNn/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf2upload.CopyToAsync(FileStream);
                        }
                        request.Ipf2 = filename;
                    }

                    if (Ipf3upload != null && Ipf3upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf3upload.FileName);
                        string extension = Path.GetExtension(Ipf3upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/VbQlNn/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf3upload.CopyToAsync(FileStream);
                        }
                        request.Ipf3 = filename;
                    }

                    if (Ipf4upload != null && Ipf4upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf4upload.FileName);
                        string extension = Path.GetExtension(Ipf4upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/VbQlNn/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf4upload.CopyToAsync(FileStream);
                        }
                        request.Ipf4 = filename;
                    }

                    if (Ipf5upload != null && Ipf5upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf5upload.FileName);
                        string extension = Path.GetExtension(Ipf5upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/VbQlNn/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf5upload.CopyToAsync(FileStream);
                        }
                        request.Ipf5 = filename;
                    }

                    var model = _db.VbQlNn.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Phanloai = request.Phanloai;
                    model.Loaivb = request.Loaivb;
                    model.Dvbanhanh = request.Dvbanhanh;
                    model.Kyhieuvb = request.Kyhieuvb;
                    model.Thoidiem = request.Thoidiem;
                    model.Ngayapdung = request.Ngayapdung;
                    model.Ngaybanhanh = request.Ngaybanhanh;
                    model.Tieude = request.Tieude;
                    model.Ghichu = request.Ghichu;
                    model.Ipf1 = request.Ipf1;
                    model.Ipf2 = request.Ipf2;
                    model.Ipf3 = request.Ipf3;
                    model.Ipf4 = request.Ipf4;
                    model.Ipf5 = request.Ipf5;
                    model.Updated_at = DateTime.Now;

                    _db.VbQlNn.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "VbQlNn");
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

        [Route("VanBanQlNnVeGia/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.vbqlnn.ds", "Delete"))
                {
                    var model = _db.VbQlNn.FirstOrDefault(t => t.Id == id_delete);
                    _db.VbQlNn.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "VbQlNn");
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

        [Route("VanBanQlNnVeGia/Show")]
        [HttpPost]
        public JsonResult Show(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.VbQlNn.FirstOrDefault(t => t.Id == Id);
                string result = "<div class='modal-body' id='frm_show'>";
                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label style='font-weight:bold'>Nội dung: </label>";
                result += "<span style='color:blue'>" + model.Tieude + "</span>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form -group fv-plugins-icon-container'>";
                result += "<label style='font-weight:bold'>File đính kèm</label>";

                if (model.Ipf1 != null && model.Ipf1.Length > 0)
                {
                    result += "<p>";
                    result += "-";
                    result += "<a href='/UpLoad/File/VbQlNn/" + model.Ipf1 + "' target='_blank' class='btn btn-link'";
                    result += " onclick='window.open(`/UpLoad/File/VbQlNn/" + model.Ipf1 + "`, `mywin`, `left=20,top=20,width=500,height=500,toolbar=1,resizable=0`); return false;'>";
                    result += model.Ipf1 + "</a>";
                    result += "</p>";
                }
                if (model.Ipf2 != null && model.Ipf2.Length > 0)
                {
                    result += "<p>";
                    result += "-";
                    result += "<a href='/UpLoad/File/VbQlNn/" + model.Ipf2 + "' target='_blank' class='btn btn-link'";
                    result += " onclick='window.open(`/UpLoad/File/VbQlNn/" + model.Ipf2 + "`, `mywin`, `left=20,top=20,width=500,height=500,toolbar=1,resizable=0`); return false;'>";
                    result += model.Ipf2 + "</a>";
                    result += "</p>";
                }
                if (model.Ipf3 != null && model.Ipf3.Length > 0)
                {
                    result += "<p>";
                    result += "-";
                    result += "<a href='/UpLoad/File/VbQlNn/" + model.Ipf3 + "' target='_blank' class='btn btn-link'";
                    result += " onclick='window.open(`/UpLoad/File/VbQlNn/" + model.Ipf3 + "`, `mywin`, `left=20,top=20,width=500,height=500,toolbar=1,resizable=0`); return false;'>";
                    result += model.Ipf3 + "</a>";
                    result += "</p>";
                }
                if (model.Ipf4 != null && model.Ipf4.Length > 0)
                {
                    result += "<p>";
                    result += "-";
                    result += "<a href='/UpLoad/File/VbQlNn/" + model.Ipf4 + "' target='_blank' class='btn btn-link'";
                    result += " onclick='window.open(`/UpLoad/File/VbQlNn/" + model.Ipf4 + "`, `mywin`, `left=20,top=20,width=500,height=500,toolbar=1,resizable=0`); return false;'>";
                    result += model.Ipf4 + "</a>";
                    result += "</p>";
                }
                if (model.Ipf5 != null && model.Ipf5.Length > 0)
                {
                    result += "<p>";
                    result += "-";
                    result += "<a href='/UpLoad/File/VbQlNn/" + model.Ipf5 + "' target='_blank' class='btn btn-link'";
                    result += " onclick='window.open(`/UpLoad/File/VbQlNn/" + model.Ipf5 + "`, `mywin`, `left=20,top=20,width=500,height=500,toolbar=1,resizable=0`); return false;'>";
                    result += model.Ipf5 + "</a>";
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
    }
}
