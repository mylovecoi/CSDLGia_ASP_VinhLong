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
            string result = "<div class='card-body' id='frm_data'>";
            var model = _db.GiaPhiLePhiCt.Where(t => t.Mahs == Mahs).ToList();
            var model_nhom = _db.GiaPhiLePhiNhom;
            foreach (var nhom in model_nhom)
            {
                var data_chitiet = model.Where(t => t.Phanloai == nhom.Manhom);
                if (data_chitiet.Any())
                {
                    result += "<div class='mb-3 font-weight-bold font-size-lg'>";
                    result += "<label style='text-decoration-line: underline; font-weight: bold'>" + nhom.Tennhom + "</label>";
                    result += "</div>";
                    result += "<table class='table table-striped table-bordered table-hover class-nosort'>";
                    result += "<thead>";
                    result += "<tr class='text-center'>";
                    result += "<th width='5%'>STT</th>";
                    result += "<th width='10%'>Nhãn hiệu</th>";
                    result += "<th width='10%'>Nước sản xuất/ lắp ráp</th>";
                    result += "<th>Kiểu loại</th>";
                    result += "<th width='10%'>Thể tích</th>";
                    result += "<th width='10%'>Số người/ Tải trọng</th>";
                    result += "<th width='10%'>Giá tính LPTB</th>";
                    result += "<th width='10%'>Thao tác</th>";
                    result += "</tr>";
                    result += "</thead><tbody>";

                    foreach (var item in model)
                    {
                        string HtmlStyle = Helpers.ConvertStrToStyle(item.Style);
                        result += "<tr>";
                        result += "<td style='text-align:center;" + HtmlStyle + "'>" + item.STTHienthi + "</td>";
                        result += "<td style='text-align:center;" + HtmlStyle + "'>" + item.NhanHieu + "</td>";
                        result += "<td style='text-align:center;" + HtmlStyle + "'>" + item.NuocSxLr + "</td>";
                        result += "<td style='text-align:left;" + HtmlStyle + "'>" + item.Ptcp + "</td>";
                        result += "<td style='text-align:center;" + HtmlStyle + "'>" + item.TheTich + "</td>";
                        result += "<td style='text-align:center;" + HtmlStyle + "'>" + item.SoNguoiTaiTrong + "</td>";
                        result += "<td style='text-align:right;" + HtmlStyle + "'>" + Helpers.ConvertDbToStr(item.Mucthutu) + "</td>";
                        result += "<td>";
                        result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Chỉnh sửa'";
                        result += " data-target='#Edit_Modal' data-toggle='modal' onclick='GetEdit(`" + item.Id + "`)'>";
                        result += "<i class='icon-lg la la-edit text-primary'></i>";
                        result += "</button>";
                        result += "</td></tr>";
                    }
                    result += "</tbody>";
                }
            }
            result += "</div>";
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
                result += "<label>Kiểu loại:</label>";
                result += "<label class='form-control'>" + @model.Ptcp + "</label>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Kiểu in hiển thị: </label>";
                result += "<select class='form-control select2multi' multiple='multiple' id='style_edit' name='style_edit' style='width:100%'>";
                result += "<option value='Chữ in đậm'" + (list_style.Contains("Chữ in đậm") ? "selected" : "") + ">Chữ in đậm</option >";
                result += "<option value='Chữ in nghiêng'" + (list_style.Contains("Chữ in nghiêng") ? "selected" : "") + ">Chữ in nghiêng</option >";
                result += "</select>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Giá tính LPTB:</label>";
                result += "<input type='text' id='Mucthutu_edit' name='Mucthutu_edit' value='" + @model.Mucthutu + "' class='form-control money-decimal-mask' style='font-weight: bold'/>";
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
        public JsonResult Update(double Mucthutu, string[] Style, int Id)
        {
            var model = _db.GiaPhiLePhiCt.FirstOrDefault(t => t.Id == Id);
            if (model != null)
            {
                string str_style = Style.Count() > 0 ? string.Join(",", Style.ToArray()) : "";
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
