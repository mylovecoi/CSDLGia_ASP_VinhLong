using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaHangHoaTaiSieuThi
{
    public class GiaHangHoaTaiSieuThiDmCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaHangHoaTaiSieuThiDmCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DanhMucHangHoaTaiSieuThi")]
        [HttpGet]
        public IActionResult Index(string Matt)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giasieuthi.danhmuchanghoataisieuthi", "Index"))
                {
                    var model = _db.GiaHangHoaTaiSieuThiDmCt.Where(t=>t.Matt == Matt).ToList();

                    ViewData["Dmnhomhh"] = _db.DmNhomHh.Where(t => t.Phanloai == "GIAHHDVKHAC").ToList();
                    ViewData["Dvt"] = _db.DmDvt;
                    ViewData["Matt"] = Matt;
                    ViewData["Title"] = "Danh mục hàng hóa tại siêu thị";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgsieuthi";
                    ViewData["MenuLv3"] = "menu_dgnsh_dmhhtaisieuthi";
                    return View("Views/Admin/Manages/DinhGia/GiaHangHoaTaiSieuThi/DanhMuc/IndexCt.cshtml", model);
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
        public JsonResult Store(string Matt, string Mahanghoa, string Tenhanghoa, string Dvt)
        {
            var model = new GiaHangHoaTaiSieuThiDmCt
            {
                Matt = Matt,
                Mahanghoa = Mahanghoa,
                Tenhanghoa = Tenhanghoa,
                Dvt = Dvt,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaHangHoaTaiSieuThiDmCt.Add(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }

        [Route("DanhMucHangHoaTaiSieuThi/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaHangHoaTaiSieuThiDmCt.FirstOrDefault(p => p.Id == Id);
            var dvt = _db.DmDvt;

            if (model != null)
            {
                string result = "<div class='row' id='edit_thongtin'>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Mã hàng hóa</label>";
                result += "<input type='text' id='mahh_edit' name='manhom_edit' value='" + model.Mahanghoa + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Tên hàng hóa</label>";
                result += "<input type='text' id='tenhh_edit' name='manhom_edit' value='" + model.Tenhanghoa + "' class='form-control'/>";
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

                result += "<input type='hidden' id='id_edit' name='id_edit' value='" + Id + "' class='form-control'/>";
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
        public JsonResult Update(int Id, string Mahanghoa, string Tenhanghoa, string Dvt)
        {
            var model = _db.GiaHangHoaTaiSieuThiDmCt.FirstOrDefault(t => t.Id == Id);
            model.Mahanghoa = Mahanghoa;
            model.Tenhanghoa = Tenhanghoa;
            model.Dvt = Dvt;
            model.Updated_at = DateTime.Now;
            _db.GiaHangHoaTaiSieuThiDmCt.Update(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }

        [Route("DanhMucHangHoaTaiSieuThi/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaHangHoaTaiSieuThiDmCt.FirstOrDefault(t => t.Id == Id);
            _db.GiaHangHoaTaiSieuThiDmCt.Remove(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }
    }
}
