using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaLePhi
{
    public class GiaLePhiNhomController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaLePhiNhomController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [HttpGet("DanhMucLePhiNhom")]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.lephi.danhmuc", "Index"))
                {
                    var model = _db.GiaPhiLePhiNhom;

                    ViewData["Title"] = "Danh mục hồ sơ giá lệ phí trước bạ";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_dglp";
                    ViewData["MenuLv3"] = "menu_dglp_dm";

                    return View("Views/Admin/Manages/DinhGia/GiaLePhi/DanhMuc/Index.cshtml", model);
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

        [HttpPost("DanhMucLePhiNhom/Store")]
        public JsonResult Store(string Manhom, string Tennhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var check = _db.GiaPhiLePhiNhom.FirstOrDefault(t => t.Manhom == Manhom);
                if(check == null)
                {
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaPhiLePhiNhom
                    {
                        Manhom = Manhom,
                        Tennhom = Tennhom
                    };
                    _db.GiaPhiLePhiNhom.Add(model);
                    _db.SaveChanges();

                    var data = new { status = "success", message = "Thành công" };
                    return Json(data);
                }
                else
                {
                    var data = new { status = "error", message = "Mã nhóm đã tồn tại! Bạn cần kiểm tra lại" };
                    return Json(data);
                }
            }    
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }

        [HttpPost("DanhMucLePhiNhom/Edit")]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.GiaPhiLePhiNhom.FirstOrDefault(t => t.Id == Id);
                if (model != null)
                {
                    string result = "<div class='modal-body' id='edit_thongtin'>";
                    result += "<div class='row'>";
                    result += "<div class='col-xl-12'>";
                    result += "<div class='form-group fv-plugins-icon-container'>";
                    result += "<label>Mã hồ sơ:</label>";
                    result += "<label type='text' class='form-control'>" + model.Manhom + "</label>";
                    result += "</div>";
                    result += "</div>";
                    result += "<div class='col-xl-12'>";
                    result += "<div class='form-group fv-plugins-icon-container'>";
                    result += "<label>Phân loại hồ sơ:</label>";
                    result += "<input type='text' id='TenNhom_Edit' name='TenNhom_Edit' class='form-control required' value='" + model.Tennhom + "'/>";
                    result += "</div>";
                    result += "</div>";
                    result += "</div>";
                    result += "<input hidden id='id_edit' name='id_edit' value='" + model.Id + "'";
                    result += "</div>";

                    var data = new { status = "success", message = result };
                    return Json(data);
                }
                else
                {
                    var data = new { status = "error", message = "Không tìm thấy thông tin cập nhật! Bạn cần kiểm tra lại" };
                    return Json(data);
                }
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }

        [HttpPost("DanhMucLePhiNhom/Update")]
        public JsonResult Update(int Id, string Tennhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.GiaPhiLePhiNhom.FirstOrDefault(t => t.Id == Id);
                if (model != null)
                {
                    model.Tennhom = Tennhom;
                    _db.GiaPhiLePhiNhom.Update(model);
                    _db.SaveChanges();

                    var data = new { status = "success", message = "Thành công" };
                    return Json(data);
                }
                else
                {
                    var data = new { status = "error", message = "Không tìm thấy thông tin cập nhật! Bạn cần kiểm tra lại" };
                    return Json(data);
                }
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }


        [HttpPost("DanhMucLePhiNhom/Delete")]
        public JsonResult Delete(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.GiaPhiLePhiNhom.FirstOrDefault(t => t.Id == Id);
                if (model != null)
                {
                    _db.GiaPhiLePhiNhom.Remove(model);
                    _db.SaveChanges();

                    var data = new { status = "success", message = "Thành công" };
                    return Json(data);
                }
                else
                {
                    var data = new { status = "error", message = "Không tìm thấy thông tin cập nhật! Bạn cần kiểm tra lại" };
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
