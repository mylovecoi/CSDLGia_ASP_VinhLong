using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CSDLGia_ASP.Database;
using System.Security.Cryptography;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.Giarung
{
    public class GiarungDmController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiarungDmController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DanhMucGiaRung")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.rung.danhmuc", "Index"))
                {
                    var model = _db.GiaRungDm.ToList();
                    ViewData["Title"] = "Danh mục giá rừng";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgr";
                    ViewData["MenuLv3"] = "menu_dgr_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaRung/DanhMuc/Index.cshtml", model);
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

        [Route("DanhMucGiaRung/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.rung.danhmuc", "Create"))
                {
                    ViewData["Title"] = "Thêm mới danh mục giá rừng";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgr";
                    ViewData["MenuLv3"] = "menu_dgr_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaRung/Create.cshtml");
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

        [Route("DanhMucGiaRung/Store")]
        [HttpPost]
        public JsonResult Store(string Manhom, string Tennhom)
        {
            var model = new GiaRungDm
            {
                Manhom = Manhom,
                Tennhom = Tennhom,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaRungDm.Add(model);
            _db.SaveChanges();
            var data = new { status = "success", message = "Thành công" };
            return Json(data);
        }

        [Route("DanhMucGiaRung/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaRungDm.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mã nhóm*</b></label>";
                result += "<input type='text' id='manhom_edit' name='manhom_edit' value='" + model.Manhom + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên nhóm*</b></label>";
                result += "<input type='text' id='tennhom_edit' name='tennhom_edit' value='" + model.Tennhom + "' class='form-control'/>";
                result += "<input type='text' hidden id='id_edit' name='id_edit' value='" + model.Id + "' class='form-control'/>";
                result += "</div></div></div></div>";


                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Không tìm thấy thông tin cần chỉnh sửa!!!" };
                return Json(data);
            }
        }

        [Route("DanhMucGiaRung/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Manhom, string Tennhom)
        {
            var model = _db.GiaRungDm.FirstOrDefault(t => t.Id == Id);
            if (model != null)
            {
                model.Manhom = Manhom;
                model.Tennhom = Tennhom;
                model.Updated_at = DateTime.Now;
                _db.GiaRungDm.Update(model);
                _db.SaveChanges();

                var data = new { status = "success", message = "Thành công" };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Không tìm thấy thông tin cần chỉnh sửa!!!" };
                return Json(data);
            }
        }

        [Route("DanhMucGiaRung/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaRungDm.FirstOrDefault(t => t.Id == Id);
            if (model != null)
            {
                var modelct = _db.GiaRungDmCt.Where(t => t.Manhom == model.Manhom);
                if (modelct.Any())
                {
                    _db.GiaRungDmCt.RemoveRange(modelct);
                }
                _db.GiaRungDm.Remove(model);
                _db.SaveChanges();
                var data = new { status = "success", message = "Thành công" };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Không tìm thấy thông tin cần chỉnh sửa!!!" };
                return Json(data);
            }
        }

    }
}
