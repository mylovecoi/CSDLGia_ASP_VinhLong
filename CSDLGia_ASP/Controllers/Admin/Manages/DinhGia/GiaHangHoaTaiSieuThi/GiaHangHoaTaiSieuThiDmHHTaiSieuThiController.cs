using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaHangHoaTaiSieuThi
{
    public class GiaHangHoaTaiSieuThiDmHHTaiSieuThiController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaHangHoaTaiSieuThiDmHHTaiSieuThiController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DanhMucHangHoaTaiSieuThi")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giasieuthi.danhmuchanghoataisieuthi", "Index"))
                {
                    var model = _db.GiaHangHoaTaiSieuThiDmHHTaiSieuThi.ToList();
                    ViewData["Title"] = "Danh mục hàng hóa tại siêu thị";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgsieuthi";
                    ViewData["MenuLv3"] = "menu_dgnsh_dmhhtaisieuthi";
                    return View("Views/Admin/Manages/DinhGia/GiaHangHoaTaiSieuThi/DanhMuc/IndexDmHHTaiSieuThi.cshtml", model);
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
        [Route("DanhMucHangHoaTaiSieuThi/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giasieuthi.danhmuchanghoataisieuthi", "Create"))
                {
                    ViewData["Title"] = "Thêm mới danh mục hàng hóa tại siêu thị";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgsieuthi";
                    ViewData["MenuLv3"] = "menu_dgnsh_dmhhtaisieuthi";
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
        [Route("DanhMucHangHoaTaiSieuThi/Store")]
        [HttpPost]
        public JsonResult Store(string mahanghoa, string tenhanghoa)
        {
            var model = new GiaHangHoaTaiSieuThiDmHHTaiSieuThi
            {
                Mahanghoa = mahanghoa,
                Tenhanghoa = tenhanghoa,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaHangHoaTaiSieuThiDmHHTaiSieuThi.Add(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }

        [Route("DanhMucHangHoaTaiSieuThi/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaHangHoaTaiSieuThiDmHHTaiSieuThi.FirstOrDefault(t => t.Id == Id);
            _db.GiaHangHoaTaiSieuThiDmHHTaiSieuThi.Remove(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }

        [Route("DanhMucHangHoaTaiSieuThi/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaHangHoaTaiSieuThiDmHHTaiSieuThi.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";

                result += "<label>Mã hàng hóa</label>";
                result += "<input type='text' id='mahanghoa_edit' name='manhom_edit' value='" + model.Mahanghoa+ "' class='form-control'/>";
                result += "<label>Tên hàng hóa</label>";
                result += "<input type='text' id='tenhanghoa_edit' name='manhom_edit' value='" + model.Tenhanghoa + "' class='form-control'/>";

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
        [Route("DanhMucHangHoaTaiSieuThi/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string mahanghoa, string tenhanghoa)
        {
            var model = _db.GiaHangHoaTaiSieuThiDmHHTaiSieuThi.FirstOrDefault(t => t.Id == Id);
            model.Mahanghoa = mahanghoa;
            model.Tenhanghoa = tenhanghoa;
            model.Updated_at = DateTime.Now;
            _db.GiaHangHoaTaiSieuThiDmHHTaiSieuThi.Update(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }


    }
}
