using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CSDLGia_ASP.Database;
using System.Security.Cryptography;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.ThamDinhGia;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.Extensions.Hosting;
using System.Net.WebSockets;
using CSDLGia_ASP.Models.Systems;

namespace CSDLGia_ASP.Controllers.Admin.Manages.ThamDinhGia
{
    public class ThamDinhGiaController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ThamDinhGiaController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("ThamDinhGia/DanhSach")]
        [HttpGet]
        public IActionResult Index(string Nam, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.tt", "Index"))
                {
                    var dsdonvi = (from db in _db.DsDiaBan
                                   join dv in _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI") on db.MaDiaBan equals dv.MaDiaBan
                                   select new VMDsDonVi
                                   {
                                       Id = dv.Id,
                                       TenDiaBan = db.TenDiaBan,
                                       MaDiaBan = dv.MaDiaBan,
                                       TenDv = dv.TenDv,
                                       MaDv = dv.MaDv,
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
                                Madv = dsdonvi.OrderBy(t => t.Id).Select(t => t.MaDv).First();
                            }
                        }

                        var model = _db.ThamDinhGia.Where(t => t.Madv == Madv).ToList();

                        if (string.IsNullOrEmpty(Nam))
                        {
                            Nam = Helpers.ConvertYearToStr(DateTime.Now.Year);
                            model = model.Where(t => Helpers.ConvertYearToStr(t.Thoidiem.Year) == Nam).ToList();
                        }
                        else
                        {
                            if (Nam != "all")
                            {
                                model = model.Where(t => Helpers.ConvertYearToStr(t.Thoidiem.Year) == Nam).ToList();
                            }
                            else
                            {
                                model = model.ToList();
                            }
                        }

                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                        {
                            ViewData["DsDonVi"] = dsdonvi;
                        }
                        else
                        {
                            ViewData["DsDonVi"] = dsdonvi.Where(t => t.MaDv == Madv);
                        }
                        ViewData["DsDiaBan"] = _db.DsDiaBan;
                        ViewData["Nam"] = Nam;
                        ViewData["Madv"] = Madv;
                        ViewData["Title"] = "Thông tin hồ sơ thẩm định giá";
                        ViewData["MenuLv1"] = "menu_tdg";
                        ViewData["MenuLv2"] = "menu_tdg_tt";
                        return View("Views/Admin/Manages/ThamDinhGia/DanhSach/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Title"] = "Thông tin hồ sơ thẩm định giá";
                        ViewData["Messages"] = "Hệ thống chưa có đơn vị thẩm định giá.";
                        ViewData["MenuLv1"] = "menu_tdg";
                        ViewData["MenuLv2"] = "menu_tdg_tt";
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

        [Route("ThamDinhGia/DanhSach/Create")]
        [HttpGet]
        public IActionResult Create(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.tt", "Create"))
                {
                    var check = _db.ThamDinhGiaCt.Where(t => t.Trangthai == "CXD");
                    if (check != null)
                    {
                        _db.ThamDinhGiaCt.RemoveRange(check);
                        _db.SaveChanges();
                    }

                    var model = new CSDLGia_ASP.Models.Manages.ThamDinhGia.ThamDinhGia
                    {
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Madv = Madv,
                        Thoidiem = DateTime.Now,
                        Songaykq = 30,
                    };

                    ViewData["Mahs"] = model.Mahs;
                    ViewData["Madv"] = Madv;
                    ViewData["TdgDonvi"] = _db.ThamDinhGiaDv.ToList();
                    ViewData["TdgDmHh"] = _db.ThamDinhGiaDmHh.ToList();
                    ViewData["Dvt"] = _db.DmDvt.ToList();
                    ViewData["Title"] = "Hồ sơ thẩm định giá";
                    ViewData["MenuLv1"] = "menu_tdg";
                    ViewData["MenuLv2"] = "menu_tdg_tt";
                    return View("Views/Admin/Manages/ThamDinhGia/DanhSach/Create.cshtml", model);

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

        [Route("ThamDinhGia/DanhSach/Store")]
        [HttpPost]
        public async Task<IActionResult> Store(CSDLGia_ASP.Models.Manages.ThamDinhGia.ThamDinhGia request, 
            IFormFile Ipf1upload, IFormFile Ipf2upload, IFormFile Ipf3upload, IFormFile Ipf4upload)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.tt", "Create"))
                {
                    if (Ipf1upload != null && Ipf1upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf1upload.FileName);
                        string extension = Path.GetExtension(Ipf1upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/ThamDinhGia/", filename);
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
                        string path = Path.Combine(wwwRootPath + "/Upload/File/ThamDinhGia/", filename);
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
                        string path = Path.Combine(wwwRootPath + "/Upload/File/ThamDinhGia/", filename);
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
                        string path = Path.Combine(wwwRootPath + "/Upload/File/ThamDinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf4upload.CopyToAsync(FileStream);
                        }
                        request.Ipf4 = filename;
                    }

                    var model = new CSDLGia_ASP.Models.Manages.ThamDinhGia.ThamDinhGia
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Dvyeucau = request.Dvyeucau,
                        Hosotdgia = request.Hosotdgia,
                        Tttstd = request.Tttstd,
                        Thoidiem = request.Thoidiem,
                        Diadiem = request.Diadiem,
                        Sotbkl = request.Sotbkl,
                        Songaykq = request.Songaykq,
                        Thoihan = request.Thoidiem,
                        Ghichu = request.Ghichu,
                        Trangthai = "CHT",
                        Congbo = "CHUACONGBO",
                        Ipf1 = request.Ipf1,
                        Ipf2 = request.Ipf2,
                        Ipf3 = request.Ipf3,
                        Ipf4 = request.Ipf4,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.ThamDinhGia.Add(model);
                    _db.SaveChanges();

                    var modelct = _db.ThamDinhGiaCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Trangthai = "XD";
                        }
                    }
                    _db.ThamDinhGiaCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "ThamDinhGia", new { request.Madv });
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

        [Route("ThamDinhGia/DanhSach/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.tt", "Edit"))
                {
                    var model = _db.ThamDinhGia.FirstOrDefault(t => t.Mahs == Mahs);

                    var model_ct = _db.ThamDinhGiaCt.Where(t => t.Mahs == model.Mahs);

                    model.ThamDinhGiaCt = model_ct.ToList();

                    ViewData["Mahs"] = model.Mahs;
                    ViewData["Madv"] = model.Madv;
                    ViewData["TdgDonvi"] = _db.ThamDinhGiaDv.ToList();
                    ViewData["TdgDmHh"] = _db.ThamDinhGiaDmHh.ToList();
                    ViewData["Dvt"] = _db.DmDvt.ToList();
                    ViewData["Title"] = "Hồ sơ thẩm định giá";
                    ViewData["MenuLv1"] = "menu_tdg";
                    ViewData["MenuLv2"] = "menu_tdg_tt";
                    return View("Views/Admin/Manages/ThamDinhGia/DanhSach/Edit.cshtml", model);

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

        [Route("ThamDinhGia/DanhSach/Update")]
        [HttpPost]
        public async Task<IActionResult> Update(CSDLGia_ASP.Models.Manages.ThamDinhGia.ThamDinhGia request,
            IFormFile Ipf1upload, IFormFile Ipf2upload, IFormFile Ipf3upload, IFormFile Ipf4upload)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.tt", "Edit"))
                {
                    if (Ipf1upload != null && Ipf1upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf1upload.FileName);
                        string extension = Path.GetExtension(Ipf1upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/ThamDinhGia/", filename);
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
                        string path = Path.Combine(wwwRootPath + "/Upload/File/ThamDinhGia/", filename);
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
                        string path = Path.Combine(wwwRootPath + "/Upload/File/ThamDinhGia/", filename);
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
                        string path = Path.Combine(wwwRootPath + "/Upload/File/ThamDinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf4upload.CopyToAsync(FileStream);
                        }
                        request.Ipf4 = filename;
                    }

                    var model = _db.ThamDinhGia.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Dvyeucau = request.Dvyeucau;
                    model.Hosotdgia = request.Hosotdgia;
                    model.Tttstd = request.Tttstd;
                    model.Thoidiem = request.Thoidiem;
                    model.Diadiem = request.Diadiem;
                    model.Sotbkl = request.Sotbkl;
                    model.Songaykq = request.Songaykq;
                    model.Thoihan = request.Thoidiem;
                    model.Ghichu = request.Ghichu;
                    model.Ipf1 = request.Ipf1;
                    model.Ipf2 = request.Ipf2;
                    model.Ipf3 = request.Ipf3;
                    model.Ipf4 = request.Ipf4;
                    model.Updated_at = DateTime.Now;

                    if (model.Ipf1 != null && System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Upload", "File", "ThamDinhGia", model.Ipf1)))
                    {
                        //xoa anh
                        System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Upload", "File", "ThamDinhGia", model.Ipf1));
                    }

                    _db.ThamDinhGia.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "ThamDinhGia", new { request.Madv });
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

        [Route("ThamDinhGia/DanhSach/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.tt", "Delete"))
                {
                    var model = _db.ThamDinhGia.FirstOrDefault(t => t.Id == id_delete);
                    _db.ThamDinhGia.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.ThamDinhGiaCt.Where(t => t.Mahs == model.Mahs);
                    _db.ThamDinhGiaCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "ThamDinhGia", new { model.Madv });
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

        [Route("ThamDinhGia/DanhSach/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.tt", "Index"))
                {
                    var model = _db.ThamDinhGia.FirstOrDefault(t => t.Mahs == Mahs);

                    var model_ct = _db.ThamDinhGiaCt.Where(t => t.Mahs == model.Mahs);

                    model.ThamDinhGiaCt = model_ct.ToList();

                    ViewData["Mahs"] = model.Mahs;
                    ViewData["Madv"] = model.Madv;
                    ViewData["TdgDonvi"] = _db.ThamDinhGiaDv.ToList();
                    ViewData["TdgDmHh"] = _db.ThamDinhGiaDmHh.ToList();
                    ViewData["Dvt"] = _db.DmDvt.ToList();
                    ViewData["Title"] = "Hồ sơ thẩm định giá";
                    ViewData["MenuLv1"] = "menu_tdg";
                    ViewData["MenuLv2"] = "menu_tdg_tt";
                    return View("Views/Admin/Manages/ThamDinhGia/DanhSach/Show.cshtml", model);

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

        [Route("ThamDinhGia/DanhSach/ShowFile")]
        [HttpPost]
        public JsonResult ShowFile(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.ThamDinhGia.FirstOrDefault(t => t.Id == Id);
                string result = "<div class='modal-body' id='frm_file'>";
                result += "<div class='row'>";
                /*result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label style='font-weight:bold'>Nội dung: </label>";
                result += "<span style='color:blue'>" + model.Tieude + "</span>";
                result += "</div>";
                result += "</div>";*/
                result += "<div class='col-xl-12'>";
                result += "<div class='form -group fv-plugins-icon-container'>";
                result += "<label style='font-weight:bold'>File đính kèm</label>";

                if (model.Ipf1 != null && model.Ipf1.Length > 0)
                {
                    result += "<p>";
                    result += "1. ";
                    result += "<a href='/UpLoad/File/ThamDinhGia/" + model.Ipf1 + "' target='_blank' class='btn btn-link'";
                    result += " onclick='window.open(`/UpLoad/File/ThamDinhGia/" + model.Ipf1 + "`, `mywin`, `left=20,top=20,width=500,height=500,toolbar=1,resizable=0`); return false;'>";
                    result += model.Ipf1 + "</a>";
                    result += "</p>";
                }
                if (model.Ipf2 != null && model.Ipf2.Length > 0)
                {
                    result += "<p>";
                    result += "2. ";
                    result += "<a href='/UpLoad/File/ThamDinhGia/" + model.Ipf2 + "' target='_blank' class='btn btn-link'";
                    result += " onclick='window.open(`/UpLoad/File/ThamDinhGia/" + model.Ipf2 + "`, `mywin`, `left=20,top=20,width=500,height=500,toolbar=1,resizable=0`); return false;'>";
                    result += model.Ipf2 + "</a>";
                    result += "</p>";
                }
                if (model.Ipf3 != null && model.Ipf3.Length > 0)
                {
                    result += "<p>";
                    result += "3. ";
                    result += "<a href='/UpLoad/File/ThamDinhGia/" + model.Ipf3 + "' target='_blank' class='btn btn-link'";
                    result += " onclick='window.open(`/UpLoad/File/ThamDinhGia/" + model.Ipf3 + "`, `mywin`, `left=20,top=20,width=500,height=500,toolbar=1,resizable=0`); return false;'>";
                    result += model.Ipf3 + "</a>";
                    result += "</p>";
                }
                if (model.Ipf4 != null && model.Ipf4.Length > 0)
                {
                    result += "<p>";
                    result += "4. ";
                    result += "<a href='/UpLoad/File/ThamDinhGia/" + model.Ipf4 + "' target='_blank' class='btn btn-link'";
                    result += " onclick='window.open(`/UpLoad/File/ThamDinhGia/" + model.Ipf4 + "`, `mywin`, `left=20,top=20,width=500,height=500,toolbar=1,resizable=0`); return false;'>";
                    result += model.Ipf4 + "</a>";
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
