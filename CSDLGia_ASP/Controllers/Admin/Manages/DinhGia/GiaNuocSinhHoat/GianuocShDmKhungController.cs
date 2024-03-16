using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaNuocsh
{
    public class GiaNuocShDmKhungController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaNuocShDmKhungController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [HttpGet("DanhMucGiaNuocShKhung")]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.danhmuckhung", "Index"))
                {
                    var model = _db.GiaNuocShDmKhung;
                    int max_sttsapxep = model.Any() ? model.Max(t => t.STTSapxep) : 0;
                    ViewData["Title"] = "Danh mục khung giá sạch sinh hoạt";
                    ViewData["SapXep"] = max_sttsapxep;
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgnsh";
                    ViewData["MenuLv3"] = "menu_dgnsh_dmkhung";
                    return View("Views/Admin/Manages/DinhGia/GiaNuocSh/DanhMuc/IndexKhung.cshtml", model);
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

        [HttpPost("DanhMucGiaNuocShKhung/Store")]
        public JsonResult Store(string STTHienthi, int STTSapxep, string[] Style, string Doituongsd)
        {
            string str_style = Style.Count() > 0 ? string.Join(",", Style.ToArray()) : "";
            var model = new GiaNuocShDmKhung
            {
                STTHienthi = STTHienthi,
                STTSapxep = STTSapxep,
                Style = str_style,
                Doituongsd = Doituongsd,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaNuocShDmKhung.Add(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }

        [HttpPost("DanhMucGiaNuocShKhung/Delete")]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaNuocShDmKhung.FirstOrDefault(t => t.Id == Id);
            _db.GiaNuocShDmKhung.Remove(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }

        [HttpPost("DanhMucGiaNuocShKhung/Edit")]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaNuocShDmKhung.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                List<string> list_style = !string.IsNullOrEmpty(model.Style) ? new List<string>(model.Style.Split(',')) : new List<string>();
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row'>";
                result += "<div class='col-xl-2'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label style='font-weight:bold;color:blue'>Sắp xếp:</label>";
                result += "<input type='number' id='sapxep_edit' name='sapxep_edit' class='form-control' step='1' value='" + model.STTSapxep + "' />";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-2'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label style='font-weight:bold;color:blue'>STT hiển thị</label> ";
                result += "<input type='text' id='stt_edit' name='stt_edit' class='form-control' value='" + model.STTHienthi + "'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label style='font-weight:bold;color:blue'>Kiểu in hiển thị: </label>";
                result += "<select class='form-control select2multi' multiple='multiple' id='style_edit' name='style_edit' style='width:100%'>";
                result += "<option value='Chữ in hoa'" + (list_style.Contains("Chữ in hoa") ? "selected" : "") + ">Chữ in hoa</option >";
                result += "<option value='Chữ in đậm'" + (list_style.Contains("Chữ in đậm") ? "selected" : "") + ">Chữ in đậm</option >";
                result += "<option value='Chữ in nghiêng'" + (list_style.Contains("Chữ in nghiêng") ? "selected" : "") + ">Chữ in nghiêng</option >";
                result += "</select>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Đối tượng sử dụng</label>";
                result += "<input type='text' id='doituongsd_edit' name='manhom_edit' value='" + model.Doituongsd + "' class='form-control'/>";

                result += "<input type='hidden' id='id_edit' name='id_edit' value='" + Id + "' class='form-control'/>";
                result += "</div>";
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

        [HttpPost("DanhMucGiaNuocShKhung/Update")]
        public JsonResult Update(int Id, string STTHienthi, int STTSapxep, string[] Style, string Doituongsd)
        {
            var model = _db.GiaNuocShDmKhung.FirstOrDefault(t => t.Id == Id);
            string str_style = Style.Count() > 0 ? string.Join(",", Style.ToArray()) : "";
            model.STTSapxep = STTSapxep;
            model.STTHienthi = STTHienthi;
            model.Style = str_style;
            model.Doituongsd = Doituongsd;
            model.Updated_at = DateTime.Now;
            _db.GiaNuocShDmKhung.Update(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }


    }

}
