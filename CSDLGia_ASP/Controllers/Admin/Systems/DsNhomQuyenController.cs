using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class DsNhomQuyenController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public DsNhomQuyenController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DsNhomQuyen")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.nhomquyen", "Index"))
                {
                    var model = _db.RoleList.OrderBy(t => t.STTSapXep);

                    ViewData["Title"] = "Danh sách nhóm quyền";
                    ViewData["MenuLv1"] = "menu_hethong";
                    ViewData["MenuLv2"] = "menu_qthethong";
                    ViewData["MenuLv3"] = "menu_nhomquyen";
                    return View("Views/Admin/Systems/DsNhomQuyen/Index.cshtml", model);
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

        [Route("DsNhomQuyen/Store")]
        [HttpPost]
        public JsonResult Store(string Name, string Role, string Magoc, string Trangthai, string Phanloai, int Level, int Sttsx)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.nhomquyen", "Create"))
                {
                    if (!string.IsNullOrEmpty(Name))
                    {
                        if (!string.IsNullOrEmpty(Role))
                        {
                            if (!string.IsNullOrEmpty(Magoc))
                            {
                                var request = new RoleList
                                {
                                    Name = Name,
                                    Role = Role,
                                    MaGoc = Magoc,
                                    TrangThai = Trangthai,
                                    PhanLoai = Phanloai,
                                    Level = Level + 1,
                                    STTSapXep = Sttsx,
                                };
                                _db.RoleList.Add(request);
                                _db.SaveChanges();

                                var data = new { status = "success", message = "Cập nhật thành công!" };
                                return Json(data);
                            }
                            else
                            {
                                var request = new RoleList
                                {
                                    Name = Name,
                                    Role = Role,
                                    MaGoc = Magoc,
                                    TrangThai = Trangthai,
                                    PhanLoai = Phanloai,
                                    Level = Level,
                                    STTSapXep = Sttsx,
                                };
                                _db.RoleList.Add(request);
                                _db.SaveChanges();

                                var data = new { status = "success", message = "Cập nhật thành công!" };
                                return Json(data);
                            }
                        }
                        else
                        {
                            var data = new { status = "error", message = "Name không được bỏ trống" };
                            return Json(data);
                        }
                    }
                    else
                    {
                        var data = new { status = "error", message = "Role không được bỏ trống" };
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

        [Route("DsNhomQuyen/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.nhomquyen", "Edit"))
                {
                    var model = _db.RoleList.FirstOrDefault(p => p.Id == Id);
                    if (model != null)
                    {
                        string result = "<div class='row' id='edit_thongtin'>";

                        //result += "<div class='col-xl-12'>";
                        //result += "<div class='form-group fv-plugins-icon-container'>";
                        //result += "<label>Nhóm quyền: </label>";
                        //result += "<input type='text' id='nhomquyen_edit' name='nhomquyen_edit' class='form-control' value='" + model.MaGoc + "' disable/>";
                        //result += "</div>";
                        //result += "</div>";

                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Name: </label>";
                        result += "<input type='text' id='name_edit' name='name_edit' class='form-control' value='" + model.Name + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Role: </label>";
                        result += "<input type='text' id='role_edit' name='role_edit' class='form-control' value='" + model.Role + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Phân loại: </label>";
                        result += "<select id='phanloai_edit' name='phanloai_edit' class='form-control'>";
                        result += "<option value='Nhóm chức năng' " + (model.PhanLoai == "Nhóm chức năng" ? "selected" : "") + ">Nhóm chức năng</option>";
                        result += "<option value='Chức năng'" + (model.PhanLoai == "Chức năng" ? "selected" : "") + ">Chức năng</option>";
                        result += "</select>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Trạng thái: </label>";
                        result += "<select id='trangthai_edit' name='trangthai_edit' class='form-control'>";
                        result += "<option value='Active' " + (model.TrangThai == "Active" ? "selected" : "") + ">Active</option>";
                        result += "<option value='Disable'" + (model.TrangThai == "Disable" ? "selected" : "") + ">Disable</option>";
                        result += "</select>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Sắp xếp: </label>";
                        result += "<input type='number' id='sttsx_edit' name='sttsx_edit' class='form-control' value='" + model.STTSapXep + "'/>";
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

        [Route("DsNhomQuyen/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Name, string Role, string Trangthai, string Phanloai)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.nhomquyen", "Edit"))
                {
                    if (!string.IsNullOrEmpty(Name))
                    {
                        if (!string.IsNullOrEmpty(Name))
                        {
                            var model = _db.RoleList.FirstOrDefault(t => t.Id == Id);
                            model.Name = Name;
                            model.Role = Role;
                            model.TrangThai = Trangthai;
                            model.PhanLoai = Phanloai;

                            _db.RoleList.Update(model);
                            _db.SaveChanges();

                            var data = new { status = "success", message = "Cập nhật thành công!" };
                            return Json(data);
                        }
                        else
                        {
                            var data = new { status = "error", message = "Role không được bỏ trống" };
                            return Json(data);
                        }
                    }
                    else
                    {
                        var data = new { status = "error", message = "Name không được bỏ trống" };
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

        [Route("DsNhomQuyen/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.nhomquyen", "Delete"))
                {
                    var model = _db.RoleList.FirstOrDefault(p => p.Id == id_delete);
                    _db.RoleList.Remove(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "DsNhomQuyen");
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
