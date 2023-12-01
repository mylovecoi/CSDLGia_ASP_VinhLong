using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.KeKhaiDkg
{
    public class MhBogController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public MhBogController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("BinhOnGia/MatHang")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.bog.phanloai", "Index"))
                {
                    var model = _db.DmNgheKd.Where(t => t.Manganh == "BOG").ToList();

                    ViewData["Title"] = "Thông tin mặt hàng bình ổn giá";
                    ViewData["MenuLv1"] = "menu_bog";
                    ViewData["MenuLv2"] = "menu_ttdnbog";
                    return View("Views/Admin/Manages/KeKhaiDkg/KkMhBog/MatHang/Index.cshtml", model);
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

        [Route("BinhOnGia/MatHang/Edit")]
        [HttpPost]
        public IActionResult Edit(int Id)
        {
            var model = _db.DmNgheKd.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Phân loại kê khai</b></label>";
                result += "<select class='form-control' id='phanloai_edit' name='phanloai_edit'>";
                if (model.Phanloai == "KK")
                {
                    result += "<option value='DKG'>Đăng ký giá</option>";
                    result += "<option value='KK' selected>Kê khai giá</option>";
                }
                else
                {
                    result += "<option value='DKG' selected>Đăng ký giá</option>";
                    result += "<option value='KK'>Kê khai giá</option>";
                }
                result += "</select>";
                result += "</div>";
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

        [Route("BinhOnGia/MatHang/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Phanloai)
        {
            var model = _db.DmNgheKd.FirstOrDefault(t => t.Id == Id);
            model.Phanloai = Phanloai;
            model.Updated_at = DateTime.Now;
            _db.DmNgheKd.Update(model);
            _db.SaveChanges();
            var data = new { status = "success" };
            return Json(data);
        }
    }
}
