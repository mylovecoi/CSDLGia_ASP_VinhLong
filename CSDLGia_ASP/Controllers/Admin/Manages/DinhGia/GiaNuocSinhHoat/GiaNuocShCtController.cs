using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public JsonResult Edit(int Id, string Nam, string Nam1, string Nam2, string Nam3, string Nam4)
        {
            var nam_start = DateTime.Now.Year - 10;
            var nam_stop = DateTime.Now.Year + 10;
            var model = _db.GiaNuocShCt.FirstOrDefault(p => p.Id == Id);
            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<div class='row'>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Đối tượng</label>";
                result += "<input type='text' id='doituongsd' name='doituongsd' value='" + model.Doituongsd + "' class='form-control' readonly />";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Năm áp dụng 1</label>";
                result += "<input type='number' id='nam_edit' name='nam_edit' class='form-control' value='" + (model.Namchuathue == null ? Nam : model.Namchuathue) + "'  />";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Đơn giá 1</label>";
                result += "<input type='text' id='gia_edit' name='gia_edit' value='" + model.Giachuathue + "' class='form-control money text-right' />";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Năm áp dụng 2</label>";
                result += "<input type='number' id='nam1_edit' name='nam1_edit' class='form-control' value='" + (model.Namchuathue1 == null ? Nam1 : model.Namchuathue1) + "' />";

                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Đơn giá 2</label>";
                result += "<input type='text' id='gia1_edit' name='gia1_edit' value='" + model.Giachuathue1 + "' class='form-control money text-right' />";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Năm áp dụng 3</label>";
                result += "<input type='number' id='nam2_edit' name='nam2_edit' class='form-control' value='" + (model.Namchuathue2 == null ? Nam2 : model.Namchuathue2) + "' />";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Đơn giá 3</label>";
                result += "<input type='text' id='gia2_edit' name='gia2_edit' value='" + model.Giachuathue2 + "' class='form-control money text-right' />";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Năm áp dụng 4</label>";
                result += "<input type='number' id='nam3_edit' name='nam3_edit' class='form-control' value='" + (model.Namchuathue3 == null ? Nam3 : model.Namchuathue3) + "' />";

                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Đơn giá 4</label>";
                result += "<input type='text' id='gia3_edit' name='gia3_edit' value='" + model.Giachuathue3 + "' class='form-control money text-right' />";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Năm áp dụng 5</label>";
                result += "<input type='number' id='nam4_edit' name='nam4_edit' class='form-control' value='" + (model.Namchuathue4 == null ? Nam4 : model.Namchuathue4) + "'/>";
              
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Đơn giá 5</label>";
                result += "<input type='text' id='gia4_edit' name='gia4_edit' value='" + model.Giachuathue4 + "' class='form-control money text-right' />";
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
        public JsonResult Update(int Id, string Namchuathue, string Namchuathue1, string Namchuathue2, string Namchuathue3, string Namchuathue4,
            double Giachuathue, double Giachuathue1, double Giachuathue2, double Giachuathue3, double Giachuathue4)
        {
            var model = _db.GiaNuocShCt.FirstOrDefault(t => t.Id == Id);
            model.Namchuathue = Namchuathue;
            model.Namchuathue1 = Namchuathue1;
            model.Namchuathue2 = Namchuathue2;
            model.Namchuathue3 = Namchuathue3;
            model.Namchuathue4 = Namchuathue4;
            model.Giachuathue = Giachuathue;
            model.Giachuathue1 = Giachuathue1;
            model.Giachuathue2 = Giachuathue2;
            model.Giachuathue3 = Giachuathue3;
            model.Giachuathue4 = Giachuathue4;
            model.Updated_at = DateTime.Now;

            _db.GiaNuocShCt.Update(model);
            _db.SaveChanges();
            string result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        public string GetData(string Mahs)
        {
            var model = _db.GiaNuocShCt.Where(t => t.Mahs == Mahs).ToList();
            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th rowspan='2'>STT</th>";
            result += "<th rowspan='2'>Mục đích sử dụng</th>";
            result += "<th colspan='2'>Đơn giá</th>";
            result += "<th colspan='2'>Đơn giá</th>";
            result += "<th colspan='2'>Đơn giá</th>";
            result += "<th colspan='2'>Đơn giá</th>";
            result += "<th colspan='2'>Đơn giá</th>";
            result += "<th rowspan='2'>Thao tác</th>";
            result += "</tr>";
            result += "<tr style='text-align:center'>";
            result += "<th>Năm áp dụng</th>";
            result += "<th>Giá tiền</th>";
            result += "<th>Năm áp dụng</th>";
            result += "<th>Giá tiền</th>";
            result += "<th>Năm áp dụng</th>";
            result += "<th>Giá tiền</th>";
            result += "<th>Năm áp dụng</th>";
            result += "<th>Giá tiền</th>";
            result += "<th>Năm áp dụng</th>";
            result += "<th>Giá tiền</th>";
            result += "</tr></thead><tbody>";

            foreach (var item in model)
            {
                result += "<tr>";
                result += "<td style='text-align:center'>" + (record++) + "</td>";
                result += "<td class='success'>" + item.Doituongsd + "</td>";
                result += "<td>" + item.Namchuathue + "</td>";
                result += "<td style='text-align: right;font-weight:bold'>" + (item.Giachuathue != 0 ? Helpers.ConvertDbToStr(item.Giachuathue) : null) + "</td>";
                result += "<td>" + item.Namchuathue1 + "</td>";
                result += "<td style='text-align: right;font-weight:bold'>" + (item.Giachuathue1 != 0 ? Helpers.ConvertDbToStr(item.Giachuathue1) : null) + "</td>";
                result += "<td>" + item.Namchuathue2 + "</td>";
                result += "<td style='text-align: right;font-weight:bold'>" + (item.Giachuathue2 != 0 ? Helpers.ConvertDbToStr(item.Giachuathue2) : null) + "</td>";
                result += "<td>" + item.Namchuathue3 + "</td>";
                result += "<td style='text-align: right;font-weight:bold'>" + (item.Giachuathue3 != 0 ? Helpers.ConvertDbToStr(item.Giachuathue3) : null) + "</td>";
                result += "<td>" + item.Namchuathue4 + "</td>";
                result += "<td style='text-align: right;font-weight:bold'>" + (item.Giachuathue4 != 0 ? Helpers.ConvertDbToStr(item.Giachuathue4) : null) + "</td>";
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
