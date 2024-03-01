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
using CSDLGia_ASP.Models.Systems;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaTrungThauHhDv
{
    public class GiaTrungThauHhDvDmController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaTrungThauHhDvDmController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaTrungThauHhDv")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.lephi.danhmuc", "Index"))
                {
                    var model = _db.GiaMuaTaiSanDm.ToList();

                    ViewData["Stt"] = model.Count() + 1;
                    ViewData["Phanloai"] = _db.GiaMuaTaiSanDm.Distinct();

                    ViewData["Title"] = "Danh mục giá giá trúng thầu hàng hóa dịch vụ";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_dglp";
                    ViewData["MenuLv3"] = "menu_dglp_dm";

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


        [Route("GiaTrungThauHhDv/Store")]
        [HttpPost]
        public JsonResult Store(GiaMuaTaiSanDm request)
        {

            var model = new GiaMuaTaiSanDm
            {
                Stt = request.Stt,
                Tennhom = request.Tennhom,
                Manhom = DateTime.Now.ToString("yyMMddssmmHH"),
                Phanloai = request.Phanloai,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaMuaTaiSanDm.Add(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }


        [Route("GiaTrungThauHhDv/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaMuaTaiSanDm.FirstOrDefault(t => t.Id == Id);
            _db.GiaMuaTaiSanDm.Remove(model);
            _db.SaveChanges();
            var data = new { status = "success" };
            return Json(data);
        }


        [Route("GiaTrungThauHhDv/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaMuaTaiSanDm.FirstOrDefault(p => p.Id == Id);
            var Phanloai = _db.GiaMuaTaiSanDm.ToList();
            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<div class='row'>";
                result += "<input hidden id='id_edit' name='id_edit'  value='" + model.Id + "' /> ";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Số thứu tự*</b></label>";
                result += "<input type='number' id='Stt_edit' name='Stt_edit' value='" + model.Stt + "' class='form-control'/>";
                result += "</div></div>";

                result += "<div class='col-xl-11'>";
                result += "<label class='form-control-label'><b>Tên phân loại nhóm phí, lệ phí*</b></label>";
                result += "<select type='text' id='Phanloai_edit' name='Phanloai_edit' class='form-control'>";
              
                foreach (var item in Phanloai)
                {
                    result += " <option value = '" + item.Phanloai + "' >" + item.Phanloai + "</ option >";
                }
                result += "</select>";
                result += "</div>";

                result += " <div class='col-md-1' style='padding-left: 0px'>";
                result += " <label class='control-label'>&nbsp;&nbsp;&nbsp;</label>";
                result += " <button type ='button' class='btn btn-default' style='border:rgba(0, 0, 0, 0.1) solid 0.05px' data-target='#Phanloai_edit_Modal' data-toggle='modal'>";
                result += " <i class='fa fa-plus'></i>";
                result += " </button>";
                result += "</div>";

                result += "<div class='col-xl-12'>";
                result += "<label class='form-control-label'><b>Tên phí, lệ phí*</b></label>";
                result += "<input type='text' id='Tennhom_edit' name='Tennhom_edit' value='" + model.Tennhom + "' class='form-control' required />";
                result += "</div>";


                result += "</div></div>";
                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Không tìm thấy thông tin cần chỉnh sửa!!!" };
                return Json(data);
            }
        }
        [Route("GiaTrungThauHhDv/Update")]
        [HttpPost]
        public JsonResult Update(GiaMuaTaiSanDm request)
        {
            var model = _db.GiaMuaTaiSanDm.FirstOrDefault(t => t.Id == request.Id);
            model.Stt = request.Stt;
            model.Tennhom = request.Tennhom;
            model.Phanloai = request.Phanloai;
            model.Updated_at = DateTime.Now;
            _db.GiaMuaTaiSanDm.Update(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }
    }
}
