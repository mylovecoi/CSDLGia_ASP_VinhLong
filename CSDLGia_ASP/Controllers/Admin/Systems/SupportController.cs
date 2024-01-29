using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class SupportController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public SupportController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DsHoTro")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.dshotro", "Index"))
                {
                    var model = _db.Supports.ToList();
                    ViewData["Title"] = "Danh sách hỗ trợ";
                    ViewData["MenuLv1"] = "menu_hethong";
                    ViewData["MenuLv2"] = "menu_qthethong";
                    ViewData["MenuLv3"] = "menu_dshotro";
                    return View("Views/Admin/Systems/Support/Index.cshtml", model);
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

        [Route("DsHoTro/Store")]
        [HttpPost]
        public JsonResult Store(string Hoten, string Sdt, string Phanloai)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.dshotro", "Create"))
                {

                    var model = new Supports
                    {
                        Hoten = Hoten,
                        Sdt = Sdt,
                        Phanloai = Phanloai,
                    };

                    _db.Supports.Add(model);
                    _db.SaveChanges();
                    var data = new { status = "success", message = "Thêm mới loại cán bộ thành công!" };
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

        [Route("DsHoTro/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.dshotro", "Edit"))
                {
                    var model = _db.Supports.FirstOrDefault(p => p.Id == Id);
                    if (model != null)
                    {
                        string result = "<div class='modal-body' id='frm_edit'>";
                        result += "<div class='row'>";

                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Họ tên<span class='require'>*</span></label>";
                        result += "<input type='text' class='form-control' id='hoten_edit' name='hoten_edit' value='" + model.Hoten + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-6'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Số điện thoại<span class='require'>*</span></label>";
                        result += "<input type='text' class='form-control' id='sdt_edit' name='sdt_edit' value='" + model.Sdt + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-6'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Phân loại<span class='require'>*</span></label>";
                        result += "<select class='form-control' id='pl_edit' name='pl_edit'>";
                        if (model.Phanloai == "Miền Bắc")
                        {
                            result += "<option value='Miền Bắc' selected>Miền Bắc</option>";
                            result += "<option value='Miền Nam'>Dừng theo dõi</option>";
                            result += "<option value='Phụ trách khối KT'>Phụ trách khối KT</option>";
                        }
                        else if(model.Phanloai == "Miền Nam")
                        {
                            result += "<option value='Miền Bắc'>Miền Bắc</option>";
                            result += "<option value='Miền Nam' selected>Dừng theo dõi</option>";
                            result += "<option value='Phụ trách khối KT'>Phụ trách khối KT</option>";
                        }
                        else
                        {
                            result += "<option value='Miền Bắc'>Miền Bắc</option>";
                            result += "<option value='Miền Nam'>Dừng theo dõi</option>";
                            result += "<option value='Phụ trách khối KT' selected>Phụ trách khối KT</option>";
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

        [Route("DsHoTro/Update")]
        [HttpPost]
        public IActionResult Update(int Id, string Hoten, string Sdt, string Phanloai)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.hethong.dshotro", "Edit"))
                {

                    var model = _db.Supports.FirstOrDefault(t => t.Id == Id);

                    model.Hoten = Hoten;
                    model.Sdt = Sdt;
                    model.Phanloai = Phanloai;
                    _db.Supports.Update(model);
                    _db.SaveChanges();

                    var data = new { status = "success", message = "Cập nhật thành công!" };
                    return Json(data);
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

        [Route("DsHoTro/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.dshotro", "Delete"))
                {
                    var model = _db.Supports.FirstOrDefault(t => t.Id == id_delete);
                    _db.Supports.Remove(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Support");
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
