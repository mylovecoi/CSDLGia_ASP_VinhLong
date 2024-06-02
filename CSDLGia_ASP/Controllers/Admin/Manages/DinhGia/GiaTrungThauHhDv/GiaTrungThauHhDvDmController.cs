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
using Azure.Core;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaTrungThauHhDv
{
    public class GiaTrungThauHhDvDmController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaTrungThauHhDvDmController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaTrungThauHhDv/DanhMuc")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.muataisan.danhmuc", "Index"))
                {
                    var model = _db.GiaMuaTaiSanDm.ToList();
                    ViewData["Title"] = "Danh mục giá trúng thầu của HH-DV được mua sắm theo QĐ của PL về đấu thầu";
                    ViewData["MenuLv1"] = "menu_mts";
                    ViewData["MenuLv2"] = "menu_giamts_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauHhDv/DanhMuc/Index.cshtml", model);
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

        [Route("GiaTrungThauHhDv/DanhMuc/Store")]
        [HttpPost]
        public JsonResult Store(GiaMuaTaiSanDm request)
        {

            var model = new GiaMuaTaiSanDm
            {
                Mota = request.Mota,
                Dvt = request.Dvt,
                KhoiLuong = request.KhoiLuong,

                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaMuaTaiSanDm.Add(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }

        [Route("GiaTrungThauHhDv/DanhMuc/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaMuaTaiSanDm.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<div class='row'>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên công tác</b></label>";
                result += "<input type='text' id='mota_edit' name='mota_edit' value='" + model.Mota + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Đơn vị</b></label>";
                result += "<input type='text' id='dvt_edit' name='dvt_edit' value='" + model.Dvt + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Khối lượng mời thầu</b></label>";
                result += "<input type='text' id='khoiluong_edit' name='khoiluong_edit' value='" + model.KhoiLuong + "' class='form-control'/>";
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

        [Route("GiaTrungThauHhDv/DanhMuc/Update")]
        [HttpPost]
        public JsonResult Update(GiaMuaTaiSanDm request)
        {
            var model = _db.GiaMuaTaiSanDm.FirstOrDefault(t => t.Id == request.Id);
            model.Mota = request.Mota;
            model.Dvt = request.Dvt;
            model.KhoiLuong = request.KhoiLuong;
        
            model.Updated_at = DateTime.Now;
            _db.GiaMuaTaiSanDm.Update(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }

        [Route("GiaTrungThauHhDv/DanhMuc/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaMuaTaiSanDm.FirstOrDefault(t => t.Id == Id);
            _db.GiaMuaTaiSanDm.Remove(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }


    }
}
