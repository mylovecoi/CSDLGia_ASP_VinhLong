using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaHangHoaTaiSieuThi
{
    public class GiaHangHoaTaiSieuThiDmSieuThiController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaHangHoaTaiSieuThiDmSieuThiController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DanhMucSieuThi")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.danhmuckhung", "Index"))
                {
                    var model = _db.GiaHangHoaTaiSieuThiDmSieuThi.ToList();
                    ViewData["Title"] = "Danh mục siêu thị";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgsieuthi";
                    ViewData["MenuLv3"] = "menu_dgnsh_dmsieuthi";
                    return View("Views/Admin/Manages/DinhGia/GiaHangHoaTaiSieuThi/DanhMuc/IndexDmSieuThi.cshtml", model);
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
        [Route("DanhMucSieuThi/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.danhmuckhung", "Create"))
                {
                    ViewData["Title"] = "Thêm mới danh mục siêu thị";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgsieuthi";
                    ViewData["MenuLv3"] = "menu_dgnsh_dmsieuthi";
                    return View("Views/Admin/Manages/DinhGia/GiaHangHoaTaiSieuThi/Create.cshtml");
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
        [Route("DanhMucSieuThi/Store")]
        [HttpPost]
        public JsonResult Store(string masieuthi, String tensieuthi  )
        {
            var model = new GiaHangHoaTaiSieuThiDmSieuThi
            {
                Masieuthi = masieuthi,
                Tensieuthi = tensieuthi,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaHangHoaTaiSieuThiDmSieuThi.Add(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }

        [Route("DanhMucSieuThi/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaHangHoaTaiSieuThiDmSieuThi.FirstOrDefault(t => t.Id == Id);
            _db.GiaHangHoaTaiSieuThiDmSieuThi.Remove(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }

        [Route("DanhMucSieuThi/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaHangHoaTaiSieuThiDmSieuThi.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";

                result += "<label>Mã siêu thị</label>";
                result += "<input type='text' id='masieuthi_edit' name='masieuthi_edit' value='" + model.Masieuthi + "' class='form-control'/>";


                result += "<label>Tên siêu thị</label>";
                result += "<input type='text' id='tensieuthi_edit' name='tensieuthi_edit' value='" + model.Tensieuthi + "' class='form-control'/>";

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
        [Route("DanhMucSieuThi/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string tensieuthi, string masieuthi)
        {
            var model = _db.GiaHangHoaTaiSieuThiDmSieuThi.FirstOrDefault(t => t.Id == Id);
            model.Masieuthi = masieuthi;
            model.Tensieuthi = tensieuthi;
            model.Updated_at = DateTime.Now;
            _db.GiaHangHoaTaiSieuThiDmSieuThi.Update(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }


    }

}
