using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using CSDLGia_ASP.Database;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaVatLieuXayDung
{
    public class GiaVatLieuXayDungDmController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaVatLieuXayDungDmController(CSDLGiaDBContext db)
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
                    var model = _db.GiaVatLieuXayDungDm.ToList();

                    ViewData["DmDvt"] = _db.DmDvt.ToList();
                    ViewData["Title"] = "Danh mục vật liệu xây dựng";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_giakhac_giavatlieuxaydung";
                    ViewData["MenuLv3"] = "menu_giakhac_giavatlieuxaydung_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaVatLieuXayDung/DanhMuc/Index.cshtml", model);
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
        public JsonResult Store(string Tenvlxd, string Dvt, string Tieuchuan)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giavatlieuxaydung.danhmuc", "Create"))
                {
                    var request = new GiaVatLieuXayDungDm
                    {
                        Mavlxd = DateTime.Now.ToString("yyMMddssmmHH"),
                        Tenvlxd = Tenvlxd,
                        Dvt = Dvt,
                        Tieuchuan = Tieuchuan,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaVatLieuXayDungDm.Add(request);
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
                    var model = _db.GiaVatLieuXayDungDm.FirstOrDefault(p => p.Id == Id);
                    var dvt = _db.DmDvt;

                    if (model != null)
                    {
                        string result = "<div class='row' id='edit_thongtin'>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Tên vật liệu xây dựng*</label>";
                        result += "<input type='text' id='tenvlxd_edit' name='tenvlxd_edit' class='form-control' value='" + model.Tenvlxd + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Đơn vị tính</label>";
                        result += "<select id='dvt_edit' name='dvt_edit' class='form-control'>";
                        foreach (var item in dvt)
                        {
                            result += "<option value='" + item.Madvt + "' " + ((string)model.Dvt == item.Madvt ? "selected" : "") + ">" + item.Dvt + "</option>";
                        }
                        result += "</select>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Tiêu chuẩn*</label>";
                        result += "<input type='text' id='tieuchuan_edit' name='tieuchuan_edit' class='form-control' value='" + model.Tieuchuan + "'/>";
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
        public JsonResult Update(int Id, string Tenvlxd, string Dvt, string Tieuchuan)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giavatlieuxaydung.danhmuc", "Edit"))
                {
                    var model = _db.GiaVatLieuXayDungDm.FirstOrDefault(t => t.Id == Id);
                    model.Tenvlxd = Tenvlxd;
                    model.Dvt = Dvt;
                    model.Tieuchuan = Tieuchuan;
                    model.Updated_at = DateTime.Now;
                    _db.GiaVatLieuXayDungDm.Update(model);
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
                    var model = _db.GiaVatLieuXayDungDm.FirstOrDefault(p => p.Id == id_delete);
                    _db.GiaVatLieuXayDungDm.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaVatLieuXayDungDm");
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
