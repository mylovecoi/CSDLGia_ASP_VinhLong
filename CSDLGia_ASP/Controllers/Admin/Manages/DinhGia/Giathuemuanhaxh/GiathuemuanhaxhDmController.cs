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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.Giathuemuanhaxh
{
    public class GiathuemuanhaxhDmController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiathuemuanhaxhDmController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DanhMucGiaThueMuaNhaO")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.tmnxh.danhmuc", "Index"))
                {
                    var model = _db.GiaThueMuaNhaXhDm.ToList();
                    ViewData["Title"] = "Danh mục giá cho thuê, thuê mua nhà ở xã hội";
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
        [Route("DanhMucGiaThueMuaNhaO/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.tmnxh.danhmuc", "Create"))
                {
                    ViewData["Title"] = "Thêm mới danh mục giá thuê,thuê mua nhà ở";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmnxh";
                    ViewData["MenuLv3"] = "menu_dgtmnxh_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaThueMuaNhaXh/Create.cshtml");
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
        [Route("DanhMucGiaThueMuaNhaO/Store")]
        [HttpPost]
        public JsonResult Store(DateTime Thoigian, string Tennha,string Diachi,double Dientich,string Hientrang,string Phanloai,string Ghichu)
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
                Maso = DateTime.Now.ToString("yyMMddssmmHH"),
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaThueMuaNhaXhDm.Add(model);
            _db.SaveChanges();
          
            var data = new { status = "success" };
            return Json(data);
        }

        [Route("DanhMucGiaThueMuaNhaO/Edit")]
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
                result += "<input type='date' id='thoigian_edit' name='thoigian_edit' value='"+model.Thoigian+"' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-8'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên nhà xã hội*</b></label>";
                result += "<input type='text' id='tennha_edit' name='tennha_edit' value='"+model.Tennha+"' class='form-control'/>";
                result += "</div></div>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Địa chỉ</b></label>";
                result += "<input type='text' id='diachi_edit' name='diachi_edit' value='"+model.Diachi+"' class='form-control'/>";
                result += "</div></div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Diện tích</b></label>";
                result += "<input type='text' id='dientich_edit' name='dientich_edit' value='"+ model.Dientich+"' class='form-control'/>";
                result += "</div></div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Hiện trạng</b></label>";
                result+= "<select id='hientrang_edit' name='hientrang_edit' class='form-control'>";
                result += "<option '"+model.Hientrang+"'='dct' ? selected='selected' : '' value='DCT'>Đang cho thuê</option>";
                result += "<option '"+model.Hientrang+"'='dsd' ? selected='selected' : '' value='DSD'>Đang sử dụng</option>";
                result += "<option '"+model.Hientrang+"'='db' ? selected='selected' '' value='DB'>Đã bán</option>";
                result += "<option '"+model.Hientrang+"'='csd' ? selected='selected' : '' value='CSD'>Chưa sử dụng</option>";
                result += "</select>";
                result += "</div></div>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Phân loại</b></label>";
                result += "<select id='phanloai_edit' name='phanloai_edit' class='form-control'>";
                result += "<option "+model.Phanloai+"=='XH' ? 'selected' : '') value='XH'>Nhà ở xã hội</!option>";
                result += "<option "+model.Phanloai+"=='CV' ? 'selected' : '') value='CV'>Nhà ở công vụ</!option>";
                result += "<option "+model.Phanloai+"=='NN' ? 'selected' : '') value='NN'>Nhà ở thuộc sở hữu của nhà nướ</!option>";
                result += "<option "+model.Phanloai+"=='K' ? 'selected' : '') value='K'>Nhà ở khác</!option>";
                result += "</div></div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Diện tích</b></label>";
                result += "<input type='text' id='dientich_edit' name='dientich_edit' value='" + model.Dientich + "' class='form-control'/>";
                result += "<input type='hidden' id='id_edit' name='id_edit' value='" + model.Id + "' />";
                result += "</div></div></div>";

                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Không tìm thấy thông tin cần chỉnh sửa!!!" };
                return Json(data);
            }
        }

        [Route("DanhMucGiaThueMuaNhaO/Update")]
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

        [Route("DanhMucGiaThueMuaNhaO/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaThueMuaNhaXhDm.FirstOrDefault(t => t.Id == Id);
            _db.GiaThueMuaNhaXhDm.Remove(model);
            _db.SaveChanges();
           
            var data = new { status = "success"};
            return Json(data);
        }

      
    }
}
