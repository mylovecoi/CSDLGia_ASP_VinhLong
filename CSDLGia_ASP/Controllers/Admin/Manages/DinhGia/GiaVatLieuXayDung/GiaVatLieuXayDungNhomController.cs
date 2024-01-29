using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaVatLieuXayDung
{
    public class GiaVatLieuXayDungNhomController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaVatLieuXayDungNhomController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaVatLieuXayDungDm")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giavatlieuxaydung.danhmuc", "Index"))
                {
                    var model = _db.GiaVatLieuXayDungNhom.ToList();
                    ViewData["Title"] = "Nhóm vật liệu xây dựng";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_giakhac_giavatlieuxaydung";
                    ViewData["MenuLv3"] = "menu_giakhac_giavatlieuxaydung_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaVatLieuXayDung/DanhMuc/Nhom/Index.cshtml", model);
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

        [Route("GiaVatLieuXayDungDm/Store")]
        [HttpPost]
        public JsonResult Store(string Manhom, string Tennhom, string Theodoi)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giavatlieuxaydung.danhmuc", "Create"))
                {
                    var request = new GiaVatLieuXayDungNhom
                    {
                        Manhom = DateTime.Now.ToString("yyMMddssmmHH"),
                        Tennhom = Tennhom,
                        Theodoi = Theodoi,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaVatLieuXayDungNhom.Add(request);
                    _db.SaveChanges();

                    var data = new { status = "success", message = "Thêm mới thành công!" };
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

        [Route("GiaVatLieuXayDungDm/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giavatlieuxaydung.danhmuc", "Edit"))
                {
                    var model = _db.GiaVatLieuXayDungNhom.FirstOrDefault(p => p.Id == Id);
                    if (model != null)
                    {
                        string result = "<div class='row' id='edit_thongtin'>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Tên nhóm vật liệu xây dựng*</label>";
                        result += "<input type='text' id='tennhom_edit' name='tennhom_edit' class='form-control' value='" + model.Tennhom + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Trạng thái</label>";
                        result += "<select id='theodoi_edit' name='theodoi_edit' class='form-control'>";
                        result += "<option value='TD' " + ((string)model.Theodoi == "TD" ? "selected" : "") + ">Theo dõi</option>";
                        result += "<option value='KTD' " + ((string)model.Theodoi == "KTD" ? "selected" : "") + ">Không theo dõi</option>";
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

        [Route("GiaVatLieuXayDungDm/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Tennhom, string Theodoi)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giavatlieuxaydung.danhmuc", "Edit"))
                {
                    var model = _db.GiaVatLieuXayDungNhom.FirstOrDefault(t => t.Id == Id);
                    model.Tennhom = Tennhom;
                    model.Theodoi = Theodoi;
                    model.Updated_at = DateTime.Now;
                    _db.GiaVatLieuXayDungNhom.Update(model);
                    _db.SaveChanges();

                    var data = new { status = "success", message = "Cập nhật thành công!" };
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

        [Route("GiaVatLieuXayDungDm/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giavatlieuxaydung.danhmuc", "Delete"))
                {
                    var model = _db.GiaVatLieuXayDungNhom.FirstOrDefault(p => p.Id == id_delete);
                    _db.GiaVatLieuXayDungNhom.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.GiaVatLieuXayDungDm.Where(p => p.Manhom == model.Manhom).ToList();
                    _db.GiaVatLieuXayDungDm.RemoveRange(model_ct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaVatLieuXayDungNhom");
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
