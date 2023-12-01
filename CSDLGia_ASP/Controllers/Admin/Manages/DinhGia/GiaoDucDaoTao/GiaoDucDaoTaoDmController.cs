using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaoDucDaoTao
{
    public class GiaoDucDaoTaoDmController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaoDucDaoTaoDmController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DanhMucGdDt")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaoducdaotao.danhmuc", "Index"))
                {
                    var model = _db.GiaDvGdDtDm.ToList();
                    ViewData["Title"] = "Danh mục giá dịch vụ giáo dục đạo tạo";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dggddt";
                    ViewData["MenuLv3"] = "menu_dggddt_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaoDucDaoTao/DanhMuc/Index.cshtml", model);
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


        [Route("DanhMucGdDt/Store")]
        [HttpPost]
        public JsonResult Store(string Tenspdv)
        {
            var model = new GiaDvGdDtDm
            {
                Tenspdv = Tenspdv,
                Maspdv = DateTime.Now.ToString("yyMMddssmmHH"),
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaDvGdDtDm.Add(model);
            _db.SaveChanges();
            var data = new { status = "success" };
            return Json(data);
        }


        [Route("DanhMucGdDt/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaDvGdDtDm.FirstOrDefault(t => t.Id == Id);
            _db.GiaDvGdDtDm.Remove(model);
            _db.SaveChanges();
            var data = new { status = "success" };
            return Json(data);
        }


        [Route("DanhMucGdDt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaDvGdDtDm.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên dịch vụ*</b></label>";
                result += "<input type='text' id='tenspdv_edit' name='tenspdv_edit' value='" + model.Tenspdv + "' class='form-control'/>";
                result += "<input hidden id='id_edit' name='id_edit'  value='" + model.Id + "' /> ";
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
        [Route("DanhMucGdDt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Tenspdv)
        {
            var model = _db.GiaDvGdDtDm.FirstOrDefault(t => t.Id == Id);
            model.Tenspdv = Tenspdv;
            model.Updated_at = DateTime.Now;
            _db.GiaDvGdDtDm.Update(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }


    }
}
