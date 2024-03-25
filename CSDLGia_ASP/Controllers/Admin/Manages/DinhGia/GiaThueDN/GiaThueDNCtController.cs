using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CSDLGia_ASP.Database;
using System.Security.Cryptography;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaThueDN
{
    public class GiaThueDNCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaThueDNCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaThueMatDatMatNuocCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaThueMatDatMatNuocCt.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                List<string> list_style = !string.IsNullOrEmpty(model.Style) ? new List<string>(model.Style.Split(',')) : new List<string>();
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Vị trí, địa bàn</b></label>";
                result += "<label class='form-control'>" + model.LoaiDat + "</label>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label style='font-weight:bold;color:blue'>Kiểu in hiển thị: </label>";
                result += "<select class='form-control select2multi' multiple='multiple' id='style_edit' name='style_edit' style='width:100%'>";
                result += "<option value='Chữ in đậm'" + (list_style.Contains("Chữ in đậm") ? "selected" : "") + ">Chữ in đậm</option >";
                result += "<option value='Chữ in nghiêng'" + (list_style.Contains("Chữ in nghiêng") ? "selected" : "") + ">Chữ in nghiêng</option >";
                result += "</select>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Ngành, nghề đặc biệt ưu đãi đầu tư:</label>";
                result += "<input type='text' id='tyle1_edit' name='tyle1_edit' class='form-control' style='font-weight: bold' value='" + model.TyLe1 + "'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Ngành, nghề ưu đãi đầu tư:</label>";
                result += "<input type='text' id='tyle2_edit' name='tyle2_edit' class='form-control' style='font-weight: bold' value='" + model.TyLe2 + "'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Ngành, nghề khác:</label>";
                result += "<input type='text' id='tyle3_edit' name='tyle3_edit' class='form-control' style='font-weight: bold' value='" + model.TyLe3 + "'/>";
                result += "</div>";
                result += "</div>";


                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Đơn giá:</label>";
                result += "<input type='number' id='dongia1_edit' name='dongia1_edit' class='form-control' style='font-weight: bold' value='" + model.Dongia1 + "'/>";
                result += "</div>";
                result += "</div>";

                result += "<input hidden type='text' id='id_edit' name='id_edit' value='" + Id + "' class='form-control'/>";

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

        [Route("GiaThueMatDatMatNuocCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string[] Style, double Dongia1, string TyLe1, string TyLe2, string TyLe3)
        {
            var model = _db.GiaThueMatDatMatNuocCt.FirstOrDefault(t => t.Id == Id);
            string str_style = Style.Count() > 0 ? string.Join(",", Style.ToArray()) : "";

            model.Dongia1 = Dongia1;
            model.TyLe1 = TyLe1;
            model.TyLe2 = TyLe2;
            model.TyLe3 = TyLe3;
            model.Style = str_style;
            model.Updated_at = DateTime.Now;
            _db.GiaThueMatDatMatNuocCt.Update(model);
            _db.SaveChanges();
            string result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("GiaThueMatDatMatNuocCt/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaThueMatDatMatNuocCt.FirstOrDefault(t => t.Id == Id);
            _db.GiaThueMatDatMatNuocCt.Remove(model);
            _db.SaveChanges();
            var result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        public string GetData(string Mahs)
        {
            var model = _db.GiaThueMatDatMatNuocCt.Where(t => t.Mahs == Mahs).ToList();
            var model_nhom = _db.GiaThueMatDatMatNuocNhom;
            string result = "<div class='card-body' id='frm_data'>";
            foreach (var nhom in model_nhom)
            {
                var data = model.Where(t => t.MaNhom == nhom.Manhom);
                if (data.Any())
                {
                    result += "<p style='text-align:center; font-size:16px; text-transform:uppercase; font-weight:bold'>" + nhom.Tennhom + "</p>";

                    result += "<table class='table table-striped table-bordered table-hover class-nosort'>";
                    result += "<thead>";
                    result += "<tr>";
                    result += "<th rowspan='2' style='text-align:center' width='2%'>STT</th>";
                    result += "<th rowspan='2' style='text-align:center'>Vị trí, địa bàn</th>";
                    result += "<th colspan='3' style='text-align:center' width='30%'>Tỷ lệ %</th>";
                    result += "<th rowspan='2' style='text-align:center' width='10%'>Đơn giá</th>";
                    result += "<th rowspan='2' style='text-align:center' width='5%'>Thao tác</th>";
                    result += "</tr>";
                    result += "<tr>";
                    result += "<th style='text-align:center'>Ngành,nghề đặc biệt ưu đãi đầu tư</th>";
                    result += "<th style='text-align:center'>Ngành,nghề ưu đãi đầu tư</th>";
                    result += "<th style='text-align:center'>Ngành,nghề khác</th>";
                    result += "</tr>";
                    result += "</thead>";
                    result += "<tbody>";

                    foreach (var item in data.OrderBy(x => x.SapXep))
                    {
                        string HtmlStyle = Helpers.ConvertStrToStyle(item.Style);
                        result += "<tr>";
                        result += "<td style='text-align:center;" + HtmlStyle + "'>" + item.SapXep + "</td>";
                        result += "<td style='text-align:left;" + HtmlStyle + "'>" + item.LoaiDat + "</td>";
                        result += "<td style='text-align:center;" + HtmlStyle + "'>" + item.TyLe1 + "</td>";
                        result += "<td style='text-align:center;" + HtmlStyle + "'>" + item.TyLe2 + "</td>";
                        result += "<td style='text-align:center;" + HtmlStyle + "'>" + item.TyLe3 + "</td>";
                        result += "<td style='text-align:right;" + HtmlStyle + "'>" + Helpers.ConvertDbToStr(item.Dongia1) + "</td>";
                        result += "<td>";
                        result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Chỉnh sửa'";
                        result += " data-target='#Edit_Modal' data-toggle='modal' onclick='SetEdit(`" + item.Id + "`)'>";
                        result += "<i class='icon-lg la la-edit text-primary'></i>";
                        result += "</button>";
                        result += "</td></tr>";
                    }
                    result += "</tbody>";
                    result += "</table>";
                    result += "<hr width='70%' align='center' />";
                }
            }
            result += "</div>";
            return result;
        }
    }
}
