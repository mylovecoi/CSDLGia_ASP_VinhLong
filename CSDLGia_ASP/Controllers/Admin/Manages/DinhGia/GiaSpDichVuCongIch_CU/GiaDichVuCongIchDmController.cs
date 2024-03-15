using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaSpDichVuCongIch
{
    public class GiaDichVuCongIchDmController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaDichVuCongIchDmController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DanhMucDvCi")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.danhmuc", "Index"))
                {
                    var model = _db.GiaSpDvCiDm.ToList();
                    ViewData["DmDvt"] = _db.DmDvt.ToList();
                    ViewData["Title"] = "Danh mục sản phẩm dịch vụ công ích";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdvci";
                    ViewData["MenuLv3"] = "menu_dgdvci_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDichVuCongIch/DanhMuc/Index.cshtml", model);
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


        [Route("DanhMucDvCi/Store")]
        [HttpPost]
        public JsonResult Store(GiaSpDvCiDm request)
        {

            var model = new GiaSpDvCiDm
            {
                Tenspdv = request.Tenspdv,
                Maspdv = DateTime.Now.ToString("yyMMddssmmHH"),
                Mota = request.Mota,
                Phanloai = request.Phanloai,
                Dvt = request.Dvt,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaSpDvCiDm.Add(model);
            _db.SaveChanges();

            if (request.Dvt != null)
            {
                var dvt = _db.DmDvt.Where(t => t.Dvt == request.Dvt).ToList();
                if (dvt.Count == 0)
                {
                    var new_dvt = new DmDvt
                    {
                        Dvt = request.Dvt,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.DmDvt.AddRange(new_dvt);
                    _db.SaveChanges();
                }
            }

            var data = new { status = "success" };
            return Json(data);
        }


        [Route("DanhMucDvCi/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaSpDvCiDm.FirstOrDefault(t => t.Id == Id);
            _db.GiaSpDvCiDm.Remove(model);
            _db.SaveChanges();
            var data = new { status = "success" };
            return Json(data);
        }


        [Route("DanhMucDvCi/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaSpDvCiDm.FirstOrDefault(p => p.Id == Id);
            var DmDvt = _db.DmDvt.ToList();
            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<div class='row'>";
                result += "<input hidden id='id_edit' name='id_edit'  value='" + model.Id + "' /> ";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên sản phẩm , dịch vụ*</b></label>";
                result += "<input type='text' id='Tenspdv_edit' name='Tenspdv_edit' value='" + model.Tenspdv + "' class='form-control'/>";
                result += "</div></div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mô tả*</b></label>";
                result += "<input type='text' id='Mota_edit' name='Mota_edit' value='" + model.Mota + "' class='form-control'/>";
                result += "</div></div>";
                result += "<div class='col-xl-5'>";
                result += "<label class='form-control-label'><b>Đơn vị tính*</b></label>";
                result += "<select type='text' id='Dvt_edit' name='Dvt_edit' class='form-control'>";
                result += " <option value = ''> ---Select--- </ option >";
                foreach (var item in DmDvt)
                {
                    result += " <option value = '" + item.Dvt + "' " + (item.Dvt == model.Dvt ? "selected" : "") + " >" + item.Dvt + "</ option >";
                }
                result += "</select>";
                result += "</div>";
                result += " <div class='col-md-1' style='padding-left: 0px'>";
                result += " <label class='control-label'>&nbsp;&nbsp;&nbsp;</label>";
                result += " <button type ='button' class='btn btn-default' style='border:rgba(0, 0, 0, 0.1) solid 0.05px' data-target='#Dvt_edit_Modal' data-toggle='modal'>";
                result += " <i class='fa fa-plus'></i>";
                result += " </button>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Phân loại sản phẩm*</b></label>";
                result += "<select id='Phanloai_edit' name='Phanloai_edit' class='form-control'>";
                foreach (string item in Helpers.PlDvCi())
                {
                    result += "<option value ='" + @item + "' " + (@item == model.Phanloai ? "selected" : "") + " >" + @item + "</option>";
                }
                result += "</select>";
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
        [Route("DanhMucDvCi/Update")]
        [HttpPost]
        public JsonResult Update(GiaSpDvCiDm request)
        {
            var model = _db.GiaSpDvCiDm.FirstOrDefault(t => t.Id == request.Id);
            model.Tenspdv = request.Tenspdv;
            model.Mota = request.Mota;
            model.Dvt = request.Dvt;
            model.Phanloai = request.Phanloai;
            model.Updated_at = DateTime.Now;
            _db.GiaSpDvCiDm.Update(model);
            _db.SaveChanges();

            if (request.Dvt != null)
            {
                var dvt = _db.DmDvt.Where(t => t.Dvt == request.Dvt).ToList();
                if (dvt.Count == 0)
                {
                    var new_dvt = new DmDvt
                    {
                        Dvt = request.Dvt,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.DmDvt.AddRange(new_dvt);
                    _db.SaveChanges();
                }
            }
            var data = new { status = "success" };
            return Json(data);
        }
    }
}
