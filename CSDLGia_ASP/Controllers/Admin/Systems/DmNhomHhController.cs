using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CSDLGia_ASP.Database;
using System.Security.Cryptography;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class DmNhomHhController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public DmNhomHhController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DmNhomHh")]
        [HttpGet]
        public IActionResult Index(string Phanloai)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmnhomhh", "Index"))
                {
                    var model = _db.DmNhomHh.ToList();

                    if (!string.IsNullOrEmpty(Phanloai))
                    {
                        if(Phanloai != "all")
                        {
                            model = _db.DmNhomHh.Where(t => t.Phanloai == Phanloai).ToList();
                        }
                        else
                        {
                            model = _db.DmNhomHh.OrderBy(t => t.Phanloai).ToList();
                        }
                    }

                    ViewData["Phanloai"] = Phanloai;
                    ViewData["Title"] = "Danh mục nhóm hàng hóa";
                    ViewData["MenuLv1"] = "menu_hethong";
                    ViewData["MenuLv2"] = "menu_qtdanhmuc";
                    ViewData["MenuLv3"] = "menu_dmnhomhh";
                    return View("Views/Admin/Systems/DmNhomHh/Index.cshtml", model);
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

        [Route("DmNhomHh/Store")]
        [HttpPost]
        public JsonResult Store(string Manhom, string Tennhom, string Theodoi, string Phanloai)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmnhomhh", "Create"))
                {
                    var check = _db.DmNhomHh.FirstOrDefault(t => t.Manhom == Manhom && t.Phanloai == Phanloai);
                    if (check == null)
                    {
                        var request = new DmNhomHh
                        {
                            Manhom = Manhom,
                            Tennhom = Tennhom,
                            Theodoi = Theodoi,
                            Phanloai = Phanloai,
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        };

                        _db.DmNhomHh.Add(request);
                        _db.SaveChanges();

                        var data = new { status = "success", message = "Thêm mới nhóm hàng hóa thành công!" };
                        return Json(data);
                    }
                    else
                    {
                        var data = new { status = "error", message = "Mã nhóm này đã tồn tại!" };
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

        [Route("DmNhomHh/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmnhomhh", "Edit"))
                {
                    var model = _db.DmNhomHh.FirstOrDefault(p => p.Id == Id);
                    if (model != null)
                    {
                        string result = "<div class='modal-body' id='frm_edit'>";
                        result += "<div class='row'>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Mã nhóm*</label>";
                        result += "<input type='text' class='form-control' id='manhom_edit' name='manhom_edit' value='" + model.Manhom + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Tên nhóm*</label>";
                        result += "<input type='text' class='form-control' id='tennhom_edit' name='tennhom_edit' value='" + model.Tennhom + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Theo dõi</label>";
                        result += "<select class='form-control' id='theodoi_edit' name='theodoi_edit'>";
                        if (model.Theodoi == "TD")
                        {
                            result += "<option value='TD' selected>Theo dõi</option>";
                            result += "<option value='DTD'>Dừng theo dõi</option>";
                        }
                        else
                        {
                            result += "<option value='TD'>Theo dõi</option>";
                            result += "<option value='DTD' selected>Dừng theo dõi</option>";
                        }
                        result += "</select>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Phân loại</label>";
                        result += "<select class='form-control' id='phanloai_edit' name='phanloai_edit'>";
                        if (model.Phanloai == "THAMDINHGIA")
                        {
                            result += "<option value='THAMDINHGIA' selected>Thẩm định giá</option>";
                            result += "<option value='GIAHHDVKHAC'>Giá HH-DV khác</option>";
                        }
                        else
                        {
                            result += "<option value='THAMDINHGIA'>Thẩm định giá</option>";
                            result += "<option value='GIAHHDVKHAC' selected>Giá HH-DV khác</option>";
                        }
                        result += "</select>";
                        result += "</div>";
                        result += "</div>";
                        result += "</div>";
                        result += "<input hidden class='form-control' id='id_edit' name='id_edit' value='" + model.Id + "'/>";
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

        [Route("DmNhomHh/Update")]
        [HttpPost]
        public IActionResult Update(int Id, string Manhom, string Tennhom, string Theodoi, string Phanloai)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmnhomhh", "Edit"))
                {
                    int check = _db.DmNhomHh.Where(t => t.Manhom == Manhom && t.Phanloai == Phanloai && t.Id != Id).Count();
                    if (check == 0)
                    {
                        var model = _db.DmNhomHh.FirstOrDefault(t => t.Id == Id);
                        model.Manhom = Manhom;
                        model.Tennhom = Tennhom;
                        model.Theodoi = Theodoi;
                        model.Phanloai = Phanloai;
                        model.Updated_at = DateTime.Now;

                        _db.DmNhomHh.Update(model);
                        _db.SaveChanges();

                        var data = new { status = "success", message = "Cập nhật thành công!" };
                        return Json(data);
                    }
                    else
                    {
                        var data = new { status = "error", message = "Mã nhóm hàng hóa đã tồn tại!!!" };
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
    }
}
