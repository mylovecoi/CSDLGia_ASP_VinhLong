using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;


namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDvKhamChuaBenh
{
    public class DvKhamChuaBenhNhomController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public DvKhamChuaBenhNhomController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("NhomDvKcb")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khamchuabenh.danhmuc", "Index"))
                {
                    var model = _db.GiaDvKcbNhom.ToList();
                    ViewData["Title"] = "Danh mục giá dịch vụ khám chữa bệnh";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgkcb";
                    ViewData["MenuLv3"] = "menu_dgkcb_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaDvKhamChuaBenh/Nhom/Index.cshtml", model);
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


        [Route("NhomDvKcb/Store")]
        [HttpPost]
        public JsonResult Store(string Tennhom, string Hientrang, string Manhom)
        {
            var model = new GiaDvKcbNhom
            {
                Tennhom = Tennhom,
                Manhom = Manhom,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaDvKcbNhom.Add(model);
            _db.SaveChanges();
            var data = new { status = "success" };
            return Json(data);
        }


        [Route("NhomDvKcb/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaDvKcbNhom.FirstOrDefault(t => t.Id == Id);
            _db.GiaDvKcbNhom.Remove(model);
            _db.SaveChanges();
            var data = new { status = "success" };
            return Json(data);
        }


        [Route("NhomDvKcb/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaDvKcbNhom.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mã nhóm</b></label>";
                result += "<input type='text' id='manhom_edit' name='manhom_edit' value='" + model.Manhom + "' class='form-control' required/>";
                result += "</div></div>";              
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên nhóm</b></label>";
                result += "<input type='text' id='tennhom_edit' name='tennhom_edit' value='" + model.Tennhom + "' class='form-control' required/>";
                result += "<input hidden id='id_edit' name='id_edit'  value='" + model.Id + "' /> ";
                result += "</div></div>";
              
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
        [Route("NhomDvKcb/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Tennhom, string Manhom)
        {
            var model = _db.GiaDvKcbNhom.FirstOrDefault(t => t.Id == Id);
            model.Tennhom = Tennhom;
            model.Manhom = Manhom;
            model.Updated_at = DateTime.Now;
            _db.GiaDvKcbNhom.Update(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }
    }
}
