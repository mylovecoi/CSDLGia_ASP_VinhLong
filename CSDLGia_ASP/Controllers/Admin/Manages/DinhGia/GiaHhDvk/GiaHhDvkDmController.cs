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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaHhDvk
{
    public class GiaHhDvkDmController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _env;

        public GiaHhDvkDmController(CSDLGiaDBContext db, IWebHostEnvironment hostingEnv)
        {
            _db = db;
            _env = hostingEnv;
        }

        [Route("GiaHhDvkDmCt")]
        [HttpGet]
        public IActionResult Index(string Matt)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.dm", "Index"))
                {
                    var model = _db.GiaHhDvkDm.Where(t => t.Matt == Matt).ToList();

                    ViewData["Matt"] = Matt;
                    ViewData["Tentt"] = _db.GiaHhDvkNhom.FirstOrDefault(t => t.Matt == Matt).Tentt;
                    ViewData["Dmnhomhh"] = _db.DmNhomHh.Where(t => t.Phanloai == "GIAHHDVK");
                    ViewData["Dvt"] = _db.DmDvt;
                    ViewData["Title"] = "Thông tin chi tiết hàng hóa dịch vụ";
                    ViewData["MenuLv1"] = "menu_hhdvk";
                    ViewData["MenuLv2"] = "menu_hhdvk_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaHhDvk/DanhMuc/ChiTiet/Index.cshtml", model);
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

        [Route("GiaHhDvkDmCt/Store")]
        [HttpPost]
        public JsonResult Store(string Manhom, string Matt, string Mahhdv, string Tenhhdv, string Dacdiemkt, string Dvt)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.dm", "Create"))
                {
                    var check = _db.GiaHhDvkDm.FirstOrDefault(t => t.Manhom == Manhom && t.Matt == Matt && t.Mahhdv == Mahhdv);
                    if (check == null)
                    {
                        var request = new GiaHhDvkDm
                        {
                            Manhom = Manhom,
                            Matt = Matt,
                            Mahhdv = Manhom + "." + Mahhdv,
                            Tenhhdv = Tenhhdv,
                            Dacdiemkt = Dacdiemkt,
                            Dvt = Dvt,
                            Theodoi = "TD",
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        };
                        _db.GiaHhDvkDm.Add(request);
                        _db.SaveChanges();

                        var check_dvt = _db.DmDvt.FirstOrDefault(t => t.Dvt == Dvt);

                        if (check_dvt == null)
                        {
                            var dvt = new DmDvt
                            {
                                Dvt = Dvt,
                                Created_at = DateTime.Now,
                                Updated_at = DateTime.Now,
                            };
                            _db.DmDvt.Add(dvt);
                            _db.SaveChanges();
                        }

                        var data = new { status = "success", message = "Thêm mới thành công!" };
                        return Json(data);
                    }
                    else
                    {
                        var data = new { status = "error", message = "Mã hàng hóa dịch vụ này đã tồn tại!" };
                        return Json(data);
                    }
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

        [Route("GiaHhDvkDmCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.dm", "Edit"))
                {
                    var model = _db.GiaHhDvkDm.FirstOrDefault(p => p.Id == Id);
                    var dmnhomhh = _db.DmNhomHh.Where(t => t.Phanloai == "GIAHHDVK");
                    var dvt = _db.DmDvt;

                    if (model != null)
                    {
                        string result = "<div class='row' id='edit_thongtin'>";

                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Nhóm hàng hóa, dịch vụ*</label>";
                        result += "<select id='manhom_edit' name='manhom_edit' class='form-control'>";
                        result += "<option value=''>--Chọn nhóm hàng hóa, dịch vụ--</option>";
                        foreach (var item in dmnhomhh)
                        {
                            result += "<option value='" + item.Manhom + "' " + ((string)model.Manhom == item.Manhom ? "selected" : "") + ">" + item.Tennhom + "</option>";
                        }
                        result += "</select>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Mã hàng hóa dịch vụ*</label>";
                        result += "<input type='text' id='mahhdv_edit' name='mahhdv_edit' class='form-control' value='" + model.Mahhdv + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Tên hàng hóa dịch vụ*</label>";
                        result += "<input type='text' id='tenhhdv_edit' name='tenhhdv_edit' class='form-control' value='" + model.Tenhhdv + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Đặc điểm kỹ thuật*</label>";
                        result += "<input type='text' id='dacdiemkt_edit' name='dacdiemkt_edit' class='form-control' value='" + model.Dacdiemkt + "'/>";
                        result += "</div>";
                        result += "</div>";
                        
                        result += "<div class='col-xl-10'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Đơn vị tính</label>";
                        result += "<select id='dvt_edit' name='dvt_edit' class='form-control'>";
                        foreach (var item in dvt)
                        {
                            result += "<option value='" + item.Dvt + "' " + ((string)model.Dvt == item.Dvt ? "selected" : "") + ">" + item.Dvt + "</option>";
                        }
                        result += "</select>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-2'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>&nbsp;&nbsp;&nbsp;</label>";
                        result += "<button type='button' class='btn btn-default' data-target='#Dvt_Modal_Edit' data-toggle='modal'><i class='la la-plus'></i></button>";
                        result += "</div>";
                        result += "</div>";

                        result += "<input hidden type='text' id='id_edit' name='id_edit' value='" + model.Id + "'/>";
                        result += "<input hidden type='text' id='matt_edit' name='matt_edit' value='" + model.Matt + "'/>";
                        result += "</div>";

                        var data = new { status = "success", message = result };
                        return Json(data);
                    }
                    else
                    {
                        var data = new { status = "error", message = "Không tìm thấy thông tin cần chỉnh sửa!!!" };
                        return Json(data);
                    }
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

        [Route("GiaHhDvkDmCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Manhom, string Matt, string Mahhdv, string Tenhhdv, string Dacdiemkt, string Dvt)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.dm", "Edit"))
                {
                    int check = _db.GiaHhDvkDm.Where(t => t.Manhom == Manhom && t.Matt == Matt && t.Mahhdv == Mahhdv && t.Id != Id).Count();
                    if(check == 0)
                    {
                        var model = _db.GiaHhDvkDm.FirstOrDefault(t => t.Id == Id);
                        model.Manhom = Manhom;
                        model.Mahhdv = Manhom + "." + Mahhdv;
                        model.Tenhhdv = Tenhhdv;
                        model.Dacdiemkt = Dacdiemkt;
                        model.Dvt = Dvt;
                        model.Updated_at = DateTime.Now;
                        _db.GiaHhDvkDm.Update(model);
                        _db.SaveChanges();

                        var check_dvt = _db.DmDvt.FirstOrDefault(t => t.Dvt == Dvt);

                        if (check_dvt == null)
                        {
                            var dvt = new DmDvt
                            {
                                Dvt = Dvt,
                                Created_at = DateTime.Now,
                                Updated_at = DateTime.Now,
                            };
                            _db.DmDvt.Add(dvt);
                            _db.SaveChanges();
                        }

                        var data = new { status = "success", message = "Cập nhật thành công!" };
                        return Json(data);
                    }
                    else
                    {
                        var data = new { status = "error", message = "Mã hàng hóa dịch vụ đã tồn tại!!!" };
                        return Json(data);
                    }
                    
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

        [Route("GiaHhDvkDmCt/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.dm", "Delete"))
                {
                    var model = _db.GiaHhDvkDm.FirstOrDefault(p => p.Id == id_delete);
                    _db.GiaHhDvkDm.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaHhDvkDm", new {Matt = model.Matt});
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
