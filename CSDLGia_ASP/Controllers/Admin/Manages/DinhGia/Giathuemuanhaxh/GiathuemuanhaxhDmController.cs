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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaThueMuaNhaXh
{
    public class GiaThueMuaNhaXhDmController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaThueMuaNhaXhDmController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaThueMuaNhaXh/DanhMuc")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanha.danhmuc", "Index"))
                {
                    var model = _db.GiaThueMuaNhaXhDm.ToList();
                    ViewData["Title"] = "Danh mục giá thuê,thuê mua nhà ở";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmnxh";
                    ViewData["MenuLv3"] = "menu_dgtmnxh_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaThueMuaNhaXh/DanhMuc/Index.cshtml", model);
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

        [Route("GiaThueMuaNhaXh/DanhMuc/Store")]
        [HttpPost]
        public JsonResult Store(DateTime Thoigian, string Tennha, string Diachi, double Dientich, string Hientrang, string Phanloai, string Ghichu)
        {

            var model = new GiaThueMuaNhaXhDm
            {
                Thoigian = Thoigian,
                Tennha = Tennha,
                Diachi = Diachi,
                Dientich = Dientich,
                Hientrang = Hientrang,
                Phanloai = Phanloai,
                Ghichu = Ghichu,               
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaThueMuaNhaXhDm.Add(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }

        [Route("GiaThueMuaNhaXh/DanhMuc/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaThueMuaNhaXhDm.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<div class='row'>";

                result += "<div class='col-xl-4'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Thời gian hoàn thành</b></label>";
                result += "<input type='date' id='thoigian_edit' name='thoigian_edit' value='" + Helpers.ConvertDateToFormView(model.Thoigian) + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";             

                result += "<div class='col-xl-8'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên nhà xã hội*</b></label>";
                result += "<input type='text' id='tennha_edit' name='tennha_edit' value='" + model.Tennha + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Địa chỉ</b></label>";
                result += "<input type='text' id='diachi_edit' name='diachi_edit' value='" + model.Diachi + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Diện tích</b></label>";
                result += "<input type='text' id='dientich_edit' name='dientich_edit' value='" + model.Dientich + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Hiện trạng</b></label>";
                result += "<select id='hientrang_edit' name='hientrang_edit' class='form-control'>";
                result += "<option value='Đang cho thuê' " + ((string)model.Hientrang == "Đang cho thuê" ? "selected" : "") + ">Đang cho thuê</option>";
                result += "<option value='Đang sử dụng' " + ((string)model.Hientrang == "Đang sử dụng" ? "selected" : "") + ">Đang sử dụng</option>";
                result += "<option value='Đã bán' " + ((string)model.Hientrang == "Đã bán" ? "selected" : "") + ">Đã bán</option>";
                result += "<option value='Chưa sử dụng' " + ((string)model.Hientrang == "Chưa sử dụng" ? "selected" : "") + ">Chưa sử dụng</option>";
                result += "</select>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Phân loại</b></label>";
                result += "<select id='phanloai_edit' name='phanloai_edit' class='form-control'>";
                result += "<option value='Nhà ở xã hội' " + ((string)model.Phanloai == "Nhà ở xã hội" ? "selected" : "") + ">Nhà ở xã hội</option>";
                result += "<option value='Nhà ở công vụ' " + ((string)model.Phanloai == "Nhà ở công vụ" ? "selected" : "") + ">Nhà ở công vụ</option>";
                result += "<option value='Nhà ở thuộc sở hữu của nhà nước' " + ((string)model.Phanloai == "Nhà ở thuộc sở hữu của nhà nước" ? "selected" : "") + ">Nhà ở thuộc sở hữu của nhà nước</option>";
                result += "<option value='Nhà ở khác' " + ((string)model.Phanloai == "Nhà ở khác" ? "selected" : "") + ">Nhà ở khác</option>";
                result += "</select>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Ghi chú</b></label>";
                result += "<textarea id='ghichu_edit' name='ghichu_edit' class='form-control' rows='2'>" + model.Ghichu + "</textarea>";
                result += "</div>";
                result += "</div>";

                result += "<input type='hidden' id='id_edit' name='id_edit' value='" + model.Id + "' />";

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

        [Route("GiaThueMuaNhaXh/DanhMuc/Update")]
        [HttpPost]
        public JsonResult Update(int Id, DateTime Thoigian, string Tennha, string Diachi, double Dientich, string Hientrang, string Phanloai, string Ghichu)
        {
            var model = _db.GiaThueMuaNhaXhDm.FirstOrDefault(t => t.Id == Id);          
            model.Thoigian = Thoigian;
            model.Tennha = Tennha;
            model.Diachi = Diachi;
            model.Dientich = Dientich;
            model.Hientrang = Hientrang;
            model.Phanloai = Phanloai;
            model.Ghichu = Ghichu;
            model.Updated_at = DateTime.Now;
            _db.GiaThueMuaNhaXhDm.Update(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }

        [Route("GiaThueMuaNhaXh/DanhMuc/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaThueMuaNhaXhDm.FirstOrDefault(t => t.Id == Id);
            _db.GiaThueMuaNhaXhDm.Remove(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }


    }
}
