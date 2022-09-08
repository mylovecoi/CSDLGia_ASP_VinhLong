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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.Gianuocsh
{
    public class GianuocshDmController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GianuocshDmController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DanhMucGiaNuocSh")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.danhmuc", "Index"))
                {
                    var model = _db.GiaNuocShDm.ToList();
                    ViewData["Title"] = "Danh mục giá nước sạch sinh hoạt";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgnsh";
                    ViewData["MenuLv3"] = "menu_dgnsh_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaNuocSh/DanhMuc/Index.cshtml", model);
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
        [Route("DanhMucGiaNuocSh/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.danhmuc", "Create"))
                {
                    ViewData["Title"] = "Thêm mới danh mục giá nước sạch sinh hoạt";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgnsh";
                    ViewData["MenuLv3"] = "menu_dgnsh_dm";
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
        [Route("DanhMucGiaNuocSh/Store")]
        [HttpPost]
        public JsonResult Store(string Doituongsd)
        {
            var model = new GiaNuocShDm
            {
                Doituongsd = Doituongsd,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaNuocShDm.Add(model);
            _db.SaveChanges();
            string result = GetData();
            var data = new { status = "success", message = result };
            return Json(data);
        }
        
        [Route("DanhMucGiaNuocSh/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaNuocShDm.FirstOrDefault(t => t.Id == Id);
            _db.GiaNuocShDm.Remove(model);
            _db.SaveChanges();
            var result = GetData();
            var data = new { status = "success", message = result };
            return Json(data);
        }
        [Route("DanhMucGiaNuocSh/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaNuocShDm.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Đối tượng sử dụng</b></label>";
                result += "<input type='text' id='doituongsd_edit' name='manhom_edit' value='"+model.Madoituong+"' class='form-control'/>";
                result += "<input type='text' id='id_edit' name='id_edit' value='"+Id+"' class='form-control'/>";
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
        [Route("DanhMucGiaNuocSh/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Doituongsd)
        {
            var model = _db.GiaNuocShDm.FirstOrDefault(t => t.Id == Id);
            model.Doituongsd = Doituongsd;
            model.Updated_at = DateTime.Now;
            _db.GiaNuocShDm.Update(model);
            _db.SaveChanges();
            string result = GetData();
            var data = new { status = "success", message = result };
            return Json(data);
        }

        public string GetData()
        {
            var model = _db.GiaNuocShDm.ToList();

            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table - hover' id='sample_3'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th>STT</th>";
            result += "<th>Đối tượng sử dụng</th>";
            result += "<th>Thao tác</th>";
            result += "</tr></thead><tbody>";

            foreach (var item in model)
            {
                result += "<tr>";
                result += "<td style='text-align:center'>" + (record++) + "</td>";
                result += "<td class='active'>" + item.Doituongsd + "</td>";
                result += "<td>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Chỉnh sửa'";
                result += " data-target='#Edit_Modal' data-toggle='modal' onclick='SetEdit(`" + item.Id + "`)'>";
                result += "<i class='icon-lg la la-edit text-primary'></i>";
                result += "</button>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Xóa'";
                result += " data-target='#Delete_Modal' data-toggle='modal' onclick='GetDelete(`" + item.Id + "`)'>";
                result += "<i class='icon-lg la la-trash text-danger'></i>";
                result += "</button></td></tr>";
            }
            result += "</tr></thead><tbody>";
            return result;
        }
    }
}
