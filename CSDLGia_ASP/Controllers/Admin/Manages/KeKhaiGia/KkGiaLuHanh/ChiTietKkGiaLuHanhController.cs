using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.KeKhaiGia;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.KeKhaiGia.KkGiaLuHanh
{
    public class ChiTietKkGiaLuHanhController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public ChiTietKkGiaLuHanhController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("KkGiaLuHanhCt/Store")]
        [HttpPost]
        public JsonResult Store(string Mahs, string Madv, string Stthienthi, int Sttsapxep, string Tendvcu, string Qccl,
            string Dodaitgian, double Giakk, DateTime Thoigian)
        {
            var model = new KkGiaLuHanhCt
            {
                Mahs = Mahs,
                Madv = Madv,
                STTHienthi = Stthienthi,
                STTSapxep = Sttsapxep,
                Tendvcu = Tendvcu,
                Dodaitgian = Dodaitgian,
                Thoigian = Thoigian,
                Giakk = Giakk,
                Qccl = Qccl,
                Trangthai = "CXD",
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.KkGiaLuHanhCt.Add(model);
            _db.SaveChanges();
            string result = GetData(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("KkGiaLuHanhCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.KkGiaLuHanhCt.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row'>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>STT sắp xếp*</b></label>";
                result += "<input type='number' id='sttsx_edit' name='sttsx_edit' value='" + model.STTSapxep + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>STT hiển thị*</b></label>";
                result += "<input type='text' id='sttht_edit' name='sttht_edit' value='" + model.STTHienthi + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên dịch vụ cung ứng*</b></label>";
                result += "<input type='text' id='tendvcu_edit' name='tendvcu_edit' value='" + model.Tendvcu + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Độ dài thời gian chương trình hoặc phục vụ</b></label>";
                result += "<input type='text' id='dodaitgian_edit' name='dodaitgian_edit' value='" + model.Dodaitgian + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Thời gian thực hiện mức giá(Từ ngày)</b></label>";
                result += "<input type='date' id='thoigian_edit' name='thoigian_edit' value='" + Helpers.ConvertDateToStrAjax(model.Thoigian) + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mức giá kê khai *</b></label>";
                result += "<input type='text' id='giakk_edit' name='giakk_edit' value='" + Helpers.ConvertDbToStr(model.Giakk) + "' class='form-control money text-right' style='font-weight: bold'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Hình thức kinh doanh</b></label>";
                result += "<input type='text' id='qccl_edit' name='qccl_edit' value='" + model.Qccl + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "</div>";

                result += "<input hidden type='text' id='id_edit' name='id_edit' value='" + model.Id + "'/>";
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

        [Route("KkGiaLuHanhCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Stthienthi, int Sttsapxep, string Tendvcu, string Qccl,
            string Dodaitgian, double Giakk, DateTime Thoigian)
        {
            var model = _db.KkGiaLuHanhCt.FirstOrDefault(t => t.Id == Id);
            model.STTSapxep = Sttsapxep;
            model.STTHienthi = Stthienthi;
            model.Tendvcu = Tendvcu;
            model.Qccl = Qccl;
            model.Thoigian = Thoigian;
            model.Dodaitgian = Dodaitgian;
            model.Giakk = Giakk;
            model.Updated_at = DateTime.Now;
            _db.KkGiaLuHanhCt.Update(model);
            _db.SaveChanges();
            string result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("KkGiaLuHanhCt/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.KkGiaLuHanhCt.FirstOrDefault(t => t.Id == Id);
            _db.KkGiaLuHanhCt.Remove(model);
            _db.SaveChanges();
            string result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        public string GetData(string Mahs)
        {
            var model = _db.KkGiaLuHanhCt.Where(t => t.Mahs == Mahs).ToList();

            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover table-responsive' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th width='2%'>#</th>";
            result += "<th width='2%'>STT</th>";
            result += "<th>Tên dịch vụ cung ứng</th>";
            result += "<th>Độ dài thời gian<br />chương trình hoặc<br />dịch vụ</th>";
            result += "<th>Thời gian thực hiện<br />mức giá(Từ ngày)</th>";
            result += "<th>Mức giá<br />kê khai</th>";
            result += "<th>Hình thức kinh doanh</th>";
            result += "<th width='9%'>Thao tác</th>";
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";

            foreach (var item in model.OrderBy(t => t.STTSapxep))
            {
                result += "<tr>";
                result += "<td style='text-align:center'>" + item.STTSapxep + "</td>";
                result += "<td style='text-align:center'>" + item.STTHienthi + "</td>";
                result += "<td class='active'>" + item.Tendvcu + "</td>";
                result += "<td>" + item.Dodaitgian + "</td>";
                result += "<td style='text-align:center'>" + Helpers.ConvertDateToStr(item.Thoigian) + "</td>";
                result += "<td style='text-align:right'>" + Helpers.ConvertDbToStr(item.Giakk) + "</td>";
                result += "<td>" + item.Qccl + "</td>";
                result += "<td>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Chỉnh sửa'";
                result += " data-target='#Edit_Modal' data-toggle='modal' onclick='SetEdit(`" + item.Id + "`)'>";
                result += "<i class='icon-lg la la-edit text-primary'></i>";
                result += "</button>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Xóa'";
                result += " data-target='#Delete_Modal' data-toggle='modal' onclick='GetDelete(`" + item.Id + "`)'>";
                result += "<i class='icon-lg la la-trash text-danger'></i>";
                result += "</button>";
                result += "</td>";
                result += "</tr>";
            }
            result += "</tbody>";
            result += "</table>";
            result += "</div>";

            return result;

        }
    }
}
