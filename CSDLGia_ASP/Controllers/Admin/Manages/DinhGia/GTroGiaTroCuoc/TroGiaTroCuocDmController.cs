using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GTroGiaTroCuoc
{
    public class TroGiaTroCuocDmController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public TroGiaTroCuocDmController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DanhMucTGTC")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.trogiatrocuoc.danhmuc", "Index"))
                {
                    var model = _db.GiaTroGiaTroCuocDm.ToList();
                    ViewData["DmDvt"] = _db.DmDvt.ToList();
                    ViewData["Title"] = "Danh mục hàng hóa trợ giá trợ giá trợ cước";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtgtc";
                    ViewData["MenuLv3"] = "menu_dgtgtc_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaTroGiaTroCuoc/DanhMuc/Index.cshtml", model);
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


        [Route("DanhMucTGTC/Store")]
        [HttpPost]
        public JsonResult Store(GiaTroGiaTroCuocDm request)
        {

            var model = new GiaTroGiaTroCuocDm
            {
                Tenspdv = request.Tenspdv,               
                Dvt = request.Dvt,               
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaTroGiaTroCuocDm.Add(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }


        [Route("DanhMucTGTC/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaTroGiaTroCuocDm.FirstOrDefault(t => t.Id == Id);
            _db.GiaTroGiaTroCuocDm.Remove(model);
            _db.SaveChanges();
            var data = new { status = "success" };
            return Json(data);
        }


        [Route("DanhMucTGTC/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaTroGiaTroCuocDm.FirstOrDefault(p => p.Id == Id);
            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<div class='row'>";
                result += "<input hidden id='id_edit' name='id_edit'  value='" + model.Id + "' /> ";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên dịch vụ*</b></label>";
                result += "<input type='text' id='Tenspdv_edit' name='Tenspdv_edit' value='" + model.Tenspdv + "' class='form-control'/>";
                result += "</div></div>"; 
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Đơn vị tính*</b></label>";
                result += "<input type='text' id='dvt_edit' name='dvt_edit' value='" + model.Dvt + "' class='form-control'/>";
                result += "</div></div>";
               
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
        [Route("DanhMucTGTC/Update")]
        [HttpPost]
        public JsonResult Update(GiaTroGiaTroCuocDm request)
        {
            var model = _db.GiaTroGiaTroCuocDm.FirstOrDefault(t => t.Id == request.Id);
            model.Tenspdv = request.Tenspdv;
            model.Dvt = request.Dvt;
            
            model.Updated_at = DateTime.Now;
            _db.GiaTroGiaTroCuocDm.Update(model);
            _db.SaveChanges();
       
            var data = new { status = "success" };
            return Json(data);
        }
    }
}
