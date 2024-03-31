using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaHangHoaTaiSieuThi
{
    public class GiaHangHoaTaiSieuThiCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaHangHoaTaiSieuThiCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaHangHoaTaiSieuThiCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaHangHoaTaiSieuThiCt.FirstOrDefault(p => p.Id == Id);
            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<div class='row'>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Giá từ</label>";
                result += "<input type='text' id='giatu_edit' name='gia_edit' value='" + model.Giatu + "' class='form-control money text-right' />";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Giá đến</label>";
                result += "<input type='text' id='giaden_edit' name='gia1_edit' value='" + model.Giaden + "' class='form-control money text-right' />";
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

        [Route("GiaHangHoaTaiSieuThiCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, double Giatu, double Giaden)
        {
            var model = _db.GiaHangHoaTaiSieuThiCt.FirstOrDefault(t => t.Id == Id);
            model.Giatu = Giatu;
            model.Giaden = Giaden;
            model.Updated_at = DateTime.Now;

            _db.GiaHangHoaTaiSieuThiCt.Update(model);
            _db.SaveChanges();
            string result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        public string GetData(string Mahs)
        {
            var model = _db.GiaHangHoaTaiSieuThiCt.Where(t => t.Mahs == Mahs).ToList();
            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th rowspan='2'>STT</th>";
            result += "<th rowspan='2'>Mã hàng hóa</th>";
            result += "<th rowspan='2'>Tên hàng hóa</th>";
            result += "<th rowspan='2'>Đơn vị tính</th>";
            result += "<th colspan='2'>Đơn giá</th>";
            result += "<th rowspan='2'>Thao tác</th>";
            result += "</tr>";
            result += "<tr style='text-align:center'>";
            result += "<th>Giá từ</th>";
            result += "<th>Giá đến</th>";
            result += "</tr></thead><tbody>";

            foreach (var item in model)
            {
                result += "<tr>";
                result += "<td style='text-align:center'>" + (record++) + "</td>";
                result += "<td style='text-align:center'>" + item.Mahanghoa + "</td>";
                result += "<td class='success'>" + item.Tenhanghoa + "</td>";
                result += "<td style='text-align:center'>" + item.Dvt + "</td>";
                result += "<td style='text-align: right;font-weight:bold'>" + (item.Giatu != 0 ? Helpers.ConvertDbToStr(item.Giatu) : null) + "</td>";
                result += "<td style='text-align: right;font-weight:bold'>" + (item.Giaden != 0 ? Helpers.ConvertDbToStr(item.Giaden) : null) + "</td>";
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
