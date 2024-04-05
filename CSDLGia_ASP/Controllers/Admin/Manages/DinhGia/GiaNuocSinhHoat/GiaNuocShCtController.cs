using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaNuocSinhHoat
{
    public class GiaNuocShCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaNuocShCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }


        [Route("GiaNuocShCtNew/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaNuocShCt.FirstOrDefault(p => p.Id == Id);
            if (model != null)
            {
                List<string> list_style = !string.IsNullOrEmpty(model.Style) ? new List<string>(model.Style.Split(',')) : new List<string>();

                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<div class='row'>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Đối tượng</label>";
                result += "<label>" + model.Doituongsd + "</label>";
                result += "</div>";
                result += "</div>";

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

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Tỷ trọng tiêu thụ</label>";
                result += "<input type='number' id='tytrongtieuthu_edit' name='tytrongtieuthu_edit' class='form-control' value='" + model.TyTrongTieuThu + "'  />";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Sản lượng</label>";
                result += "<input type='number' id='sanluong_edit' name='sanluong_edit' value='" + model.SanLuong + "' class='form-control' />";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Đơn giá chưa thuế:</label>";
                result += "<input type='number' id='dongia1_edit' name='dongia1_edit' value='" + model.DonGia1 + "' class='form-control text-right' />";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Đơn giá đã bao gồm thuế GTGT (đồng/m3)</label>";
                result += "<input type='number' id='dongia2_edit' name='dongia2_edit' value='" + model.DonGia2 + "' class='form-control text-right' step='0.1'/>";
                result += "</div>";
                result += "</div>";


                result += "<input hidden type='text' id='id_edit' name='id_edit' value='" + model.Id + "'/>";
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



        [Route("GiaNuocShCtNew/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string TyTrongTieuThu, string SanLuong, double DonGia1, double DonGia2, string[] Style)
        {
            string str_style = Style.Count() > 0 ? string.Join(",", Style.ToArray()) : "";
            var model = _db.GiaNuocShCt.FirstOrDefault(t => t.Id == Id);
            model.TyTrongTieuThu = TyTrongTieuThu;
            model.SanLuong = SanLuong;
            model.DonGia1 = DonGia1;
            model.DonGia2 = DonGia2;
            model.Updated_at = DateTime.Now;
            model.Style = str_style;

            _db.GiaNuocShCt.Update(model);
            _db.SaveChanges();
            string result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        public string GetData(string Mahs)
        {
            var model = _db.GiaNuocShCt.Where(t => t.Mahs == Mahs).ToList();
            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th>STT</th>";
            result += "<th>STT hiển thị</th>";
            result += "<th>Mục đích sử dụng</th>";
            result += "<th>Tỷ trọng tiêu thụ (%)</th>";
            result += "<th>Sản lượng (m3)</th>";
            result += "<th>Đơn giá chưa bao gồm thuế GTGT(đồng/m3)</th>";
            result += "<th>Đơn giá đã bao gồm thuế GTGT (đồng/m3)</th>";
            result += "<th>Thao tác</th>";
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";

            foreach (var item in model)
            {
                string HtmlStyle = Helpers.ConvertStrToStyle(item.Style);
                result += "<tr>";
                result += "<td style='text-align:center;" + HtmlStyle + "'>" + item.STTSapxep + "</td>";
                result += "<td style='text-align:center;" + HtmlStyle + "'>" + item.STTHienthi + "</td>";
                result += "<td style='text-align:left;" + HtmlStyle + "'>" + item.Doituongsd + "</td>";
                result += "<td style='text-align:center;" + HtmlStyle + "'>" + item.TyTrongTieuThu + "</td>";
                result += "<td style='text-align:center;" + HtmlStyle + "'>" + item.SanLuong + "</td>";
                result += "<td style='text-align:right;" + HtmlStyle + "'>" + Helpers.ConvertDbToStr(item.DonGia1) + "</td>";
                result += "<td style='text-align:right;" + HtmlStyle + "'>" + Helpers.ConvertDbToStr(item.DonGia2) + "</td>";
                result += "<td>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Chỉnh sửa'";
                result += " data-target='#Edit_Modal' data-toggle='modal' onclick='SetEdit(`" + item.Id + "`)'>";
                result += "<i class='icon-lg la la-edit text-primary'></i>";
                result += "</button>";
                result += "</td>";
                result += "</tr>";
            }
            result += "</tbody></table></div>";
            return result;
        }
    }
}
