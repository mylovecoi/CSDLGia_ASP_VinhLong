using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaHangHoaTaiSieuThi
{
    public class GiaHangHoaTaiSieuThiDmController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaHangHoaTaiSieuThiDmController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DanhMucSieuThi")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giasieuthi.danhmuchanghoataisieuthi", "Index"))
                {
                    var model = _db.GiaHangHoaTaiSieuThiDm.ToList();
                    ViewData["Title"] = "Danh mục siêu thị";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgsieuthi";
                    ViewData["MenuLv3"] = "menu_dgnsh_dmhhtaisieuthi";
                    return View("Views/Admin/Manages/DinhGia/GiaHangHoaTaiSieuThi/DanhMuc/Index.cshtml", model);
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
        public JsonResult Store(string Tentt)
        {
            var model = new GiaHangHoaTaiSieuThiDm
            {
                Matt = DateTime.Now.ToString("yyMMddssmmHH"),
                Tentt = Tentt,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaHangHoaTaiSieuThiDm.Add(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }

        [Route("DanhMucSieuThi/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaHangHoaTaiSieuThiDm.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<div class='row'>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Tên thông tư</label>";
                result += "<input type='text' id='tentt_edit' name='tentt_edit' value='" + model.Tentt + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "<input type='hidden' id='id_edit' name='id_edit' value='" + Id + "' class='form-control'/>";
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
        public JsonResult Update(int Id, string Tentt)
        {
            var model = _db.GiaHangHoaTaiSieuThiDm.FirstOrDefault(t => t.Id == Id);
            model.Tentt = Tentt;
            model.Updated_at = DateTime.Now;
            _db.GiaHangHoaTaiSieuThiDm.Update(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }

        [Route("DanhMucSieuThi/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaHangHoaTaiSieuThiDm.FirstOrDefault(t => t.Id == Id);
            _db.GiaHangHoaTaiSieuThiDm.Remove(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }
    }

}
