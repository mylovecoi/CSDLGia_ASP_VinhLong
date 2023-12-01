using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.KeKhaiGia;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.KeKhaiGia.KkGiaXmTxd
{
    public class KkGiaXmTxdCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public KkGiaXmTxdCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("KkGiaXmTxdCt/Store")]
        [HttpPost]
        public JsonResult Store(string Mahs, string Madv, string Tendvcu, string Qccl, string Dvt, double Gialk, double Giakk, string Ghichu)
        {
            var model = new KkGiaXmTxdCt
            {
                Mahs = Mahs,
                Madv = Madv,
                Tendvcu = Tendvcu,
                Qccl = Qccl,
                Dvt = Dvt,
                Giakk = Giakk,
                Gialk = Gialk,
                Ghichu = Ghichu,
                Trangthai = "CXD",
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.KkGiaXmTxdCt.Add(model);
            _db.SaveChanges();
            string result = GetData(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("KkGiaXmTxdCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.KkGiaXmTxdCt.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên dịch vụ cung ứng*</b></label>";
                result += "<input type='text' id='tendvcu_edit' name='tendvcu_edit' value='" + model.Tendvcu + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Quy cách chất lượng dịch vụ</b></label>";
                result += "<textarea type='text' id='qccl_edit' name='qccl_edit' cols='12' rows='3' class='form-control'>" + model.Qccl + "</textarea>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Đơn vị tính*</b></label>";
                result += "<input type='text' id='dvt_edit' name='dvt_edit' value='" + model.Dvt + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "</div>";
                result += "<div class='row text-left'>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Giá kê khai hiện hành*</b></label>";
                result += "<input type='text' id='gialk_edit' name='gialk_edit' value='" + Helpers.ConvertDbToStr(model.Gialk) + "' class='form-control money text-right' style='font-weight: bold'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Giá kê khai mới*</b></label>";
                result += "<input type='text' id='giakk_edit' name='giakk_edit' value='" + Helpers.ConvertDbToStr(model.Giakk) + "' class='form-control money text-right' style='font-weight: bold'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Ghi chú</b></label>";
                result += "<textarea type='text' id='ghichu_edit' name='ghichu_edit' cols='12' rows='2' class='form-control'>" + model.Ghichu + "</textarea>";
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

        [Route("KkGiaXmTxdCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Tendvcu, string Qccl, string Dvt, double Gialk, double Giakk, string Ghichu)
        {
            var model = _db.KkGiaXmTxdCt.FirstOrDefault(t => t.Id == Id);
            model.Tendvcu = Tendvcu;
            model.Qccl = Qccl;
            model.Dvt = Dvt;
            model.Gialk = Gialk;
            model.Giakk = Giakk;
            model.Ghichu = Ghichu;
            model.Updated_at = DateTime.Now;
            _db.KkGiaXmTxdCt.Update(model);
            _db.SaveChanges();
            string result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("KkGiaXmTxdCt/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.KkGiaXmTxdCt.FirstOrDefault(t => t.Id == Id);
            _db.KkGiaXmTxdCt.Remove(model);
            _db.SaveChanges();
            string result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        public string GetData(string Mahs)
        {
            var model = _db.KkGiaXmTxdCt.Where(t => t.Mahs == Mahs).ToList();

            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover table-responsive' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th width='2%'>#</th>";
            result += "<th>Tên dịch vụ cung ứng</th>";
            result += "<th>Quy cách chất lượng</th>";
            result += "<th>Đơn vị<br />tính</th>";
            result += "<th>Mức giá<br />liền kề</th>";
            result += "<th>Mức giá<br />kê khai</th>";
            result += "<th>Ghi chú</th>";
            result += "<th width='9%'>Thao tác</th>";
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";

            foreach (var item in model)
            {
                result += "<tr>";
                result += "<td style='text-align:center'>" + record++ + "</td>";
                result += "<td class='active'>" + item.Tendvcu + "</td>";
                result += "<td>" + item.Qccl + "</td>";
                result += "<td style='text-align:center'>" + item.Dvt + "</td>";
                result += "<td style='text-align:right'>" + Helpers.ConvertDbToStr(item.Gialk) + "</td>";
                result += "<td style='text-align:right'>" + Helpers.ConvertDbToStr(item.Giakk) + "</td>";
                result += "<td>" + item.Ghichu + "</td>";
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
