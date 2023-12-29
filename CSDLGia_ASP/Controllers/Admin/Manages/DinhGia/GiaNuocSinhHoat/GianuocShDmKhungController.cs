using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaNuocsh
{
    public class GiaNuocShDmKhungController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaNuocShDmKhungController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DanhMucGiaNuocShKhung")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.danhmuckhung", "Index"))
                {
                    var model = _db.GiaNuocShDmKhung.ToList();
                    ViewData["Title"] = "Danh mục khung giá sạch sinh hoạt";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgnsh";
                    ViewData["MenuLv3"] = "menu_dgnsh_dmkhung";
                    return View("Views/Admin/Manages/DinhGia/GiaNuocSh/DanhMuc/IndexKhung.cshtml", model);
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
        [Route("DanhMucGiaNuocShKhung/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.danhmuckhung", "Create"))
                {
                    ViewData["Title"] = "Thêm mới danh mục khung giá sạch sinh hoạt";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgnsh";
                    ViewData["MenuLv3"] = "menu_dgnsh_dmkhung";
                    return View("Views/Admin/Manages/DinhGia/GiaNuocSh/Create.cshtml");
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
        [Route("DanhMucGiaNuocShKhung/Store")]
        [HttpPost]
        public JsonResult Store(string Doituongsd)
        {
            var model = new GiaNuocShDmKhung
            {
                Doituongsd = Doituongsd,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaNuocShDmKhung.Add(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }

        [Route("DanhMucGiaNuocShKhung/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaNuocShDmKhung.FirstOrDefault(t => t.Id == Id);
            _db.GiaNuocShDmKhung.Remove(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }

        [Route("DanhMucGiaNuocShKhung/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaNuocShDmKhung.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";

                result += "<label>Đối tượng sử dụng</label>";
                result += "<input type='text' id='doituongsd_edit' name='manhom_edit' value='" + model.Doituongsd + "' class='form-control'/>";

                result += "<input type='hidden' id='id_edit' name='id_edit' value='" + Id + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
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
        [Route("DanhMucGiaNuocShKhung/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Doituongsd)
        {
            var model = _db.GiaNuocShDmKhung.FirstOrDefault(t => t.Id == Id);
            model.Doituongsd = Doituongsd;
            model.Updated_at = DateTime.Now;
            _db.GiaNuocShDmKhung.Update(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }


    }

}
