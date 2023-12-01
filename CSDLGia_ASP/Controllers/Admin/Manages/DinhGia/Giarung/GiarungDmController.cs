using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

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
            string result = GetData();
            var data = new { status = "success", message = result };
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
            model.Manhom = Manhom;
            model.Tennhom = Tennhom;
            model.Updated_at = DateTime.Now;
            _db.GiaRungDm.Update(model);
            _db.SaveChanges();
            string result = GetData();
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("DanhMucGiaRung/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaRungDm.FirstOrDefault(t => t.Id == Id);
            _db.GiaRungDm.Remove(model);
            _db.SaveChanges();
            var result = GetData();
            var data = new { status = "success", message = result };
            return Json(data);
        }

        public string GetData()
        {
            var model = _db.GiaRungDm.ToList();

            int record = 1;

            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover' id='sample_3'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th>STT</th>";
            result += "<th>Mã nhóm</th>";
            result += "<th>Tên nhóm</th>";
            result += "<th>Thao tác</th>";
            result += "</tr></thead><tbody>";

            foreach (var item in model)
            {
                result += "<tr>";
                result += "<td style='text-align:center'>" + (record++) + "</td>";
                result += "<td class='active'>" + item.Manhom + "</td>";
                result += "<td>" + item.Tennhom + "</td>";
                result += "<td>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Chỉnh sửa'";
                result += " data-target='#Edit_Modal' data-toggle='modal' onclick='SetEdit(`" + item.Id + "`)'>";
                result += "<i class='icon-lg la la-edit text-primary'></i>";
                result += "</button>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Xóa'";
                result += "data-target='#Delete_Modal' data-toggle='modal' onclick='GetDelete(`" + item.Id + "`)'>";
                result += "<i class='icon-lg la la-trash text-danger'></i>";
                result += "</button></td></tr>";
            }
            result += "</tr></thead><tbody>";
            return result;
        }
    }
}
