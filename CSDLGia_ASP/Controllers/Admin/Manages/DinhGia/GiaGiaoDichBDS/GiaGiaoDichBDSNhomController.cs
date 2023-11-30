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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaGiaoDichBDS
{
    public class GiaGiaoDichBDSNhomController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaGiaoDichBDSNhomController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaGiaoDichBDSDm")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaodichbds.danhmuc", "Index"))
                {
                    var model = _db.GiaGiaoDichBDSNhom.ToList();
                    ViewData["Title"] = "Nhóm bất động sản";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dg_giaodichbds";
                    ViewData["MenuLv3"] = "menu_dg_giaodichbds_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichBDS/DanhMuc/Nhom/Index.cshtml", model);
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

        [Route("GiaGiaoDichBDSDm/Store")]
        [HttpPost]
        public JsonResult Store(string Manhom, string Tennhom, string Theodoi)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaodichbds.danhmuc", "Create"))
                {
                    var request = new GiaGiaoDichBDSNhom
                    {
                        Manhom = DateTime.Now.ToString("yyMMddssmmHH"),
                        Tennhom = Tennhom,
                        Theodoi = Theodoi,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaGiaoDichBDSNhom.Add(request);
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

        [Route("GiaGiaoDichBDSDm/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaodichbds.danhmuc", "Edit"))
                {
                    var model = _db.GiaGiaoDichBDSNhom.FirstOrDefault(p => p.Id == Id);
                    if (model != null)
                    {
                        string result = "<div class='row' id='edit_thongtin'>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Tên nhóm bất động sản*</label>";
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

        [Route("GiaGiaoDichBDSDm/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Tennhom, string Theodoi)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaodichbds.danhmuc", "Edit"))
                {
                    var model = _db.GiaGiaoDichBDSNhom.FirstOrDefault(t => t.Id == Id);
                    model.Tennhom = Tennhom;
                    model.Theodoi = Theodoi;
                    model.Updated_at = DateTime.Now;
                    _db.GiaGiaoDichBDSNhom.Update(model);
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

        [Route("GiaGiaoDichBDSDm/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaodichbds.danhmuc", "Delete"))
                {
                    var model = _db.GiaGiaoDichBDSNhom.FirstOrDefault(p => p.Id == id_delete);
                    _db.GiaGiaoDichBDSNhom.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.GiaGiaoDichBDSDm.Where(p => p.Manhom == model.Manhom).ToList();
                    _db.GiaGiaoDichBDSDm.RemoveRange(model_ct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaGiaoDichBDSNhom");
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
