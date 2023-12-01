using CSDLGia_ASP.Database;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.Giathuemuanhaxh
{
    public class GiathuemuanhaxhCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiathuemuanhaxhCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DinhGiaThueMuaNhaXhCt/Store")]
        [HttpPost]
        public JsonResult Store(string Mahs, string Dvthue, string Diachi, string Tennha, string Soqdpd, DateTime Thoigianpd, string Soqddg, DateTime Thoigiandg, string Hdthue, string Ththue, DateTime Tungay, DateTime Denngay, string Dvt, double Dongia, double Dongiathue)
        {

            var model = new GiaThueMuaNhaXhCt
            {
                Mahs = Mahs,
                Dvthue = Dvthue,
                Diachi = Diachi,
                Soqdpd = Soqdpd,
                Maso = Tennha,
                Thoigianpd = Thoigianpd,
                Soqddg = Soqddg,
                Thoigiandg = Thoigiandg,
                Hdthue = Hdthue,
                Ththue = Ththue,
                Tungay = Tungay,
                Denngay = Denngay,
                Dongia = Dongia,
                Dvt = Dvt,
                Dongiathue = Dongiathue,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaThueMuaNhaXhCt.Add(model);
            _db.SaveChanges();

            string result = GetData(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }
        [Route("DinhGiaThueMuaNhaXhCt/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaThueMuaNhaXhCt.FirstOrDefault(t => t.Id == Id);
            _db.GiaThueMuaNhaXhCt.Remove(model);
            _db.SaveChanges();
            var result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }
        public string GetData(string Mahs)
        {
            var model = _db.GiaThueMuaNhaXhCt.Where(t => t.Mahs == Mahs).ToList();
            var tennha = _db.GiaThueMuaNhaXhDm.ToList();
            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th>STT</th>";
            result += "<th>Tên nhà </th>";
            result += "<th>Đơn vị thuê</th>";
            result += "<th>Đơn vị tính</th>";
            result += "<th>Giá bán</th>";
            result += "<th>Giá thuê</th>";
            result += "<th>Thao tác</th>";
            result += "</tr></thead><tbody>";

            foreach (var item in model)
            {
                result += "<tr style='text-align:center'>";
                result += "<td style='text-align:center'>" + (record++) + "</td>";

                result += "<td style='text-align:center'>";
                foreach (var tn in tennha)
                {
                    if (item.Maso == tn.Maso)
                    {
                        result += "<span>" + tn.Tennha + "</span>";
                    }
                }
                result += "</td>";

                result += "<td>" + item.Dvthue + "</td>";
                result += "<td>" + item.Dvt + "</td>";
                result += "<td>" + item.Dongia + "</td>";
                result += "<td>" + item.Dongiathue + "</td>";
                result += "<td>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Chỉnh sửa'";
                result += " data-target='#Edit_Modal' data-toggle='modal' onclick='SetEdit(`" + item.Id + "`)'>";
                result += "<i class='icon-lg la la-edit text-primary'></i>";
                result += "</button>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Xóa'";
                result += " data-target='#Delete_Modal' data-toggle='modal' onclick='GetDelete(`" + item.Id + "`)'>";
                result += "<i class='icon-lg la la-trash text-danger'></i>";
                result += "</button></td></tr>";
            }
            result += "</tbody>";
            return result;
        }
        [Route("DinhGiaThueMuaNhaXhCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaThueMuaNhaXhCt.FirstOrDefault(p => p.Id == Id);
            var tennha = _db.GiaThueMuaNhaXhDm.ToList();
            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row'>"; // Mở row
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Tên nhà(DinhGiaThueMuaNhaXhCt/Edit)</label>";
                result += "<select id='tennha_edit' name='tennha_edit' class='form-control'>";
                foreach (var tn in tennha)
                {
                    result += "<option value='" + @tn.Maso + "'>" + @tn.Tennha + "</option>";
                }
                result += "</select>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Đơn vị thuê</label>";
                result += "<input type='text' id='donvithue_edit' name='donvithue_edit' value='" + model.Dvthue + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Địa chỉ</label>";
                result += "<input type='text' id='diachi_edit' name='diachi_edit' value='" + model.Dvthue + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "</div>"; // Đóng row

                result += "<div class='row'>"; // Mở row

                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Số QĐ phê duyệt chủ trương</label>";
                result += "<input type='text' id='sqdct_edit' name='sqdct_edit' value='" + model.Soqdpd + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Thời điểm PD chủ trương</label>";
                result += "<input type='date' id='tdqdct_edit' name='tdqdct_edit' class='form-control' value='" + model.Thoigianpd + "'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Số QĐ phê duyệt giá</label>";
                result += "<input type='text' id='sqdg_edit' name='sqdg_edit' class='form-control' value='" + model.Soqdpd + "'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Thời điểm PD giá</label>";
                result += "<input type='date' id='tdqdg_edit' name='tdqdg_edit' class='form-control' value='" + model.Thoigiandg + "'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Hợp đồng số</label>";
                result += "<input type='text' id='hdso_edit' name='hdso_edit' class='form-control money text-right' style='font-weight: bold' value='" + model.Hdthue + "'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Thời hạn</label>";
                result += "<input type='text' id='thoihan_edit' name='thoihan_edit' class='form-control money text-right' style='font-weight: bold' value='" + model.Ththue + "'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Từ ngày</label>";
                result += "<input type='date' id='tungay_edit' name='tungay_edit' class='form-control' value='" + model.Tungay + "'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Đến ngày</label>";
                result += "<input type='date' id='denngay_edit' name='denngay_edit' class='form-control' value='" + model.Denngay + "'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-3'>";
                result += "<label class='form-control-label'>Đơn vị tính</label>";
                result += "<select id='dvt_edit' name='dvt_edit' class='form-control select2me select2-offscreen' tabindex='-1' title=''>";

                var dvt = _db.DmDvt.ToList();
                foreach (var item in dvt)
                {
                    result += "<option value ='" + @item.Dvt + "'>" + @item.Dvt + "</ option >";
                }
                result += "</select>";
                result += "</div>";

                result += "<div class='col-xl-1' style='padding-left: 0px;'>";
                result += "<label class='control-label'>Thêm</label>";
                result += "<button type='button' class='btn btn-default' data-target='#Dvt_Modal_edit' data-toggle='modal'><i class='la la-plus'></i>";
                result += "</button>";
                result += "</div>";

                result += "<div class='col-xl-4'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Giá bán*</b></label>";
                result += "<input type='text' id='giaban_edit' name='giaban_edit' class='form-control money text-right' style='font-weight: bold' value='" + model.Dongia + "'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-4'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Giá thuê*</b></label>";
                result += "<input type='text' id='giathue_edit' name='giathue_edit' class='form-control money text-right' style='font-weight: bold' value='" + model.Dongiathue + "'/>";
                result += "</div>";
                result += "</div>";

                result += "</div>"; // Đóng row

                result += "<input type='hidden' id='id_edit' name='id_edit' value='" + Id + "'/>";
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
        [Route("DinhGiaThueMuaNhaXhCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Dvthue, string Tennha, string Soqdpd, DateTime Thoigianpd, string Soqddg, DateTime Thoigiandg, string Hdthue, string Ththue, DateTime Tungay, DateTime Denngay, string Dvt, double Dongia, double Dongiathue)
        {
            var model = _db.GiaThueMuaNhaXhCt.FirstOrDefault(t => t.Id == Id);

            model.Dvthue = Dvthue;
            model.Soqdpd = Soqdpd;
            model.Maso = Tennha;
            model.Thoigianpd = Thoigianpd;
            model.Soqddg = Soqddg;
            model.Thoigiandg = Thoigiandg;
            model.Hdthue = Hdthue;
            model.Ththue = Ththue;
            model.Tungay = Tungay;
            model.Denngay = Denngay;
            model.Dongia = Dongia;
            model.Dvt = Dvt;
            model.Dongiathue = Dongiathue;
            model.Updated_at = DateTime.Now;
            _db.GiaThueMuaNhaXhCt.Update(model);
            _db.SaveChanges();
            string result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }
    }
}
