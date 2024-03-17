using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaLePhi
{
    public class GiaLePhiCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaLePhiCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

       
        public string GetData(string Mahs)
        {
            var model = _db.GiaPhiLePhiCt.Where(t => t.Mahs == Mahs).ToList();
            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover' id='datatable_4'>";
            result += "<thead>";
            result += "<tr class='text-center'>";
            result += "<th width='5%'>STT</th>";
            result += "<th>STT hiển thị</th>";
            result += "<th>Tên phí, lệ phí</th>";
            result += "<th>Phần trăm</th>";
            result += "<th>Mức thu</th>";
            result += "<th width='18%'>Thao tác</th>";
            result += "</tr>";          
            result += "</thead><tbody>";

            foreach (var item in model)
            {
                string HtmlStyle = Helpers.ConvertStrToStyle(item.Style);
                result += "<tr>";
                result += "<td style='text-align:center;" + HtmlStyle + "'>" + (record++) + "</td>";
                result += "<td style='text-align:center;" + HtmlStyle + "'>" + item.STTHienthi + "</td>";
                result += "<td style='text-align:left;" + HtmlStyle + "'>" + item.Ptcp + "</td>";
                result += "<td style='text-align:center;" + HtmlStyle + "'>" + item.Phantram + "</td>";
                result += "<td style='text-align:center;" + HtmlStyle + "'>" + item.Mucthutu + "</td>";
                result += "<td>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Chỉnh sửa'";
                result += " data-target='#Edit_Modal' data-toggle='modal' onclick='GetEdit(`" + item.Id + "`)'>";
                result += "<i class='icon-lg la la-edit text-primary'></i>";
                result += "</button>";
               result += "</td></tr>";
            }
            result += "</tbody>";
            return result;
        }

        [Route("DinhGiaLePhiCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaPhiLePhiCt.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                List<string> list_style = !string.IsNullOrEmpty(model.Style) ? new List<string>(model.Style.Split(',')) : new List<string>();
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<input hidden id='id_edit' name='id_edit' value='" + Id + "' class='form-control'/>";
                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
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
                result += "<label><b>Tên phí lệ phí</b></label>";
                result += "<input type='text' id='Ptcp_edit' name='Ptcp_edit' value='" + @model.Ptcp + "' class='form-control' />";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-4'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Phần trăm</b></label>";
                result += "<input type='text' id='Phantram_edit' name='Phantram_edit' value='" + @model.Phantram + "' class='form-control text-right' style = 'font-weight: bold'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-8'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mức thu từ</b></label>";
                result += "<input type='number' id='Mucthutu_edit' name='Mucthutu_edit' value='" + @model.Mucthutu + "' class='form-control money text-right' style = 'font-weight: bold'/>";
                result += "</div>";
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
        [Route("DinhGiaLePhiCt/Update")]
        [HttpPost]
        public JsonResult Update(string Ptcp, string Phantram, double Mucthutu, string[] Style, int Id)
        {
            var model = _db.GiaPhiLePhiCt.FirstOrDefault(t => t.Id == Id);
            if (model != null)
            {
                string str_style = Style.Count() > 0 ? string.Join(",", Style.ToArray()) : "";
                model.Ptcp = Ptcp;
                model.Phantram = Phantram;
                model.Mucthutu = Mucthutu;
                model.Style = str_style;                        
                model.Updated_at = DateTime.Now;
                _db.GiaPhiLePhiCt.Update(model);
                _db.SaveChanges();
                string result = GetData(model.Mahs);
                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Không tìm thấy thông tin cần chỉnh sửa!!!" };
                return Json(data);
            }
        }

    }
}
