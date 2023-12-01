using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class DsDiaBanController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public DsDiaBanController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DsDiaBan")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.dsdiaban", "Index"))
                {
                    var dsdiaban = _db.DsDiaBan.ToList();
                    ViewData["Title"] = "Danh sách địa bàn";
                    ViewData["MenuLv1"] = "menu_hethong";
                    ViewData["MenuLv2"] = "menu_qthethong";
                    ViewData["MenuLv3"] = "menu_dsdiaban";
                    return View("Views/Admin/Systems/DsDiaBan/Index.cshtml", dsdiaban);
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

        [Route("DsDiaBan/Store")]
        [HttpPost]
        public JsonResult Store(string TenDiaBan, string PhanLoai)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.dsdiaban", "Create"))
                {
                    if (!string.IsNullOrEmpty(TenDiaBan))
                    {
                        var request = new DsDiaBan
                        {
                            MaDiaBan = DateTime.Now.ToString("yyMMddssmmHH"),
                            TenDiaBan = TenDiaBan,
                            Level = PhanLoai,
                            Created_At = DateTime.Now,
                            Updated_At = DateTime.Now,
                        };
                        _db.DsDiaBan.Add(request);
                        _db.SaveChanges();

                        var data = new { status = "success", message = "Cập nhật thành công!" };
                        return Json(data);
                    }
                    else
                    {
                        var data = new { status = "error", message = "Tên địa bàn không được bỏ trống" };
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

        [Route("DsDiaBan/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.dsdiaban", "Edit"))
                {
                    var model = _db.DsDiaBan.FirstOrDefault(p => p.Id == Id);
                    if (model != null)
                    {
                        string result = "<div class='row' id='edit_thongtin'>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Mã địa bàn: </label>";
                        result += "<input type='text' id='madiaban_edit' name='madiaban_edit' class='form-control' value='" + model.MaDiaBan + "' readonly='readonly'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Tên địa bàn<span class='required'>*</span>: </label>";
                        result += "<input type='text' id='tendiaban_edit' name='tendiaban_edit' class='form-control' value='" + model.TenDiaBan + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Phân loại: </label>";
                        result += "<select id='phanloai_edit' name='phanloai_edit' class='form-control'>";

                        if (model.Level == "ADMIN")
                        {
                            result += "<option value='ADMIN' selected>Đơn vị tổng hợp toàn Tỉnh</option>";
                            result += "<option value='T'>Đơn vị hành chính cấp Tỉnh</option>";
                            result += "<option value='H'>Đơn vị hành chính cấp Huyện</option>";
                        }
                        if (model.Level == "T")
                        {
                            result += "<option value='ADMIN'>Đơn vị tổng hợp toàn Tỉnh</option>";
                            result += "<option value='T' selected>Đơn vị hành chính cấp Tỉnh</option>";
                            result += "<option value='H'>Đơn vị hành chính cấp Huyện</option>";
                        }
                        if (model.Level == "H")
                        {
                            result += "<option value='ADMIN'>Đơn vị tổng hợp toàn Tỉnh</option>";
                            result += "<option value='T'>Đơn vị hành chính cấp Tỉnh</option>";
                            result += "<option value='H' selected>Đơn vị hành chính cấp Huyện</option>";
                        }

                        result += "</select>";
                        result += "</div>";
                        result += "</div>";
                        result += "<input hidden type='text' id='id_edit' name='id_edit' value='" + model.Id + "'/>";
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

        [Route("DsDiaBan/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string TenDiaBan, string PhanLoai)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.dsdiaban", "Edit"))
                {
                    if (!string.IsNullOrEmpty(TenDiaBan))
                    {
                        var model = _db.DsDiaBan.FirstOrDefault(t => t.Id == Id);
                        model.TenDiaBan = TenDiaBan;
                        model.Level = PhanLoai;
                        model.Updated_At = DateTime.Now;
                        _db.DsDiaBan.Update(model);
                        _db.SaveChanges();

                        var data = new { status = "success", message = "Cập nhật thành công!" };
                        return Json(data);
                    }
                    else
                    {
                        var data = new { status = "error", message = "Tên địa bàn không được bỏ trống" };
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

        [Route("DsDiaBan/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.dsdiaban", "Delete"))
                {
                    var model = _db.DsDiaBan.FirstOrDefault(p => p.Id == id_delete);
                    _db.DsDiaBan.Remove(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "DsDiaBan");
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
