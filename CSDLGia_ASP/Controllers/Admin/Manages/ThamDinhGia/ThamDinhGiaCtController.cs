using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.ThamDinhGia;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.ThamDinhGia
{
    public class ThamDinhGiaCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public ThamDinhGiaCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("ThamDinhGiaCt/Store")]
        [HttpPost]
        public JsonResult Store(string Mahs, string Mats, string Tents, string Qccl, string Tskt, string Xuatxu, string Dvt, string Sl, double Ngiadn, double Giadn, double Ngiatd, double Giatd, string Ghichu)
        {
            var model = new ThamDinhGiaCt
            {
                Mahs = Mahs,
                Mats = Mats,
                Tents = Tents,
                Dacdiempl = Qccl,
                Thongsokt = Tskt,
                Nguongoc = Xuatxu,
                Dvt = Dvt,
                Sl = Sl,
                Nguyengiadenghi = Ngiadn,
                Giadenghi = Giadn,
                Nguyengiathamdinh = Ngiatd,
                Giatritstd = Giatd,
                Ghichu = Ghichu,
                Trangthai = "CXD",
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.ThamDinhGiaCt.Add(model);
            _db.SaveChanges();
            string result = GetData(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("ThamDinhGiaCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.ThamDinhGiaCt.FirstOrDefault(p => p.Id == Id);
            var dvt = _db.DmDvt.ToList();

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row'>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Mã hàng hóa*</label>";
                result += "<input type='text' id='mats_edit' name='mats_edit' value='" + model.Mats + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-8'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Tên hàng hóa*</label>";
                result += "<input type='text' id='tents_edit' name='tents_edit' value='" + model.Tents + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-1'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>&nbsp;&nbsp;&nbsp;</label><br />";
                result += "<button type='button' class='btn btn-default' data-target='#Hh_Edit_Modal' data-toggle='modal'><i class='la la-plus'></i></button>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Quy cách chất lượng</label>";
                result += "<input type='text' id='qccl_edit' name='qccl_edit' class='form-control' value='" + model.Dacdiempl + "' />";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Thông số kỹ thuật</label>";
                result += "<input type='text' id='tskt_edit' name='tskt_edit' class='form-control' value='" + model.Thongsokt + "' />";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Xuất xứ</label>";
                result += "<input type='text' id='xuatxu_edit' name='xuatxu_edit' class='form-control' value='" + model.Nguongoc + "' />";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Đơn vị tính</label>";
                result += "<select id='dvt_edit' name='dvt_edit' class='form-control'>";
                result += "<option value=''></option>";
                foreach (var item in dvt)
                {
                    result += "<option value='" + item.Dvt + "' " + ((string)model.Dvt == item.Dvt ? "selected" : "") + ">" + item.Dvt + "</option>";
                }
                result += "</select>";
                result += "</div>";
                result += "</div>";
                /*result += "<div class='col-xl-1'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>&nbsp;&nbsp;&nbsp;</label><br />";
                result += "<button type='button' class='btn btn-default' data-target='#Dvt_Edit_Modal' data-toggle='modal'><i class='la la-plus'></i></button>";
                result += "</div>";
                result += "</div>";*/
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Số lượng</label>";
                result += "<input type='number' id='sl_edit' name='sl_edit' class='form-control' value='" + model.Sl + "' />";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Đơn giá đề nghị*</label>";
                result += "<input type='text' id='nguyengiadenghi_edit' name='nguyengiadenghi_edit' value='" + Helpers.ConvertDbToStr(model.Nguyengiadenghi) + "' class='form-control money text-right' style='font-weight: bold'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Giá trị đề nghị*</label>";
                result += "<input type='text' id='giadenghi_edit' name='giadenghi_edit' value='" + Helpers.ConvertDbToStr(model.Giadenghi) + "' class='form-control money text-right' style='font-weight: bold'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Đơn giá thẩm định*</label>";
                result += "<input type='text' id='nguyengiathamdinh_edit' name='nguyengiathamdinh_edit' value='" + Helpers.ConvertDbToStr(model.Nguyengiathamdinh) + "' class='form-control money text-right' style='font-weight: bold'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Giá trị tài sản thẩm định*</label>";
                result += "<input type='text' id='giatritstd_edit' name='giatritstd_edit' value='" + Helpers.ConvertDbToStr(model.Giatritstd) + "' class='form-control money text-right' style='font-weight: bold'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Ghi chú</label>";
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

        [Route("ThamDinhGiaCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Mats, string Tents, string Qccl, string Tskt, string Xuatxu, string Dvt, string Sl, double Ngiadn, double Giadn, double Ngiatd, double Giatd, string Ghichu)
        {
            var model = _db.ThamDinhGiaCt.FirstOrDefault(t => t.Id == Id);
            model.Mats = Mats;
            model.Tents = Tents;
            model.Dacdiempl = Qccl;
            model.Thongsokt = Tskt;
            model.Nguongoc = Xuatxu;
            model.Dvt = Dvt;
            model.Sl = Sl;
            model.Nguyengiadenghi = Ngiadn;
            model.Giadenghi = Giadn;
            model.Nguyengiathamdinh = Ngiatd;
            model.Giatritstd = Giatd;
            model.Ghichu = Ghichu;
            model.Updated_at = DateTime.Now;
            _db.ThamDinhGiaCt.Update(model);
            _db.SaveChanges();
            string result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("ThamDinhGiaCt/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.ThamDinhGiaCt.FirstOrDefault(t => t.Id == Id);
            _db.ThamDinhGiaCt.Remove(model);
            _db.SaveChanges();
            string result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        public string GetData(string Mahs)
        {
            var model = _db.ThamDinhGiaCt.Where(t => t.Mahs == Mahs).ToList();

            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover table-responsive' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th width='2%'>#</th>";
            result += "<th>Mã hàng hóa</th>";
            result += "<th>Tên hàng hóa-Quy cách</th>";
            result += "<th>Thông số kỹ thuật</th>";
            result += "<th>Xuất xứ</th>";
            result += "<th>Đơn vị tính</th>";
            result += "<th>Số lượng</th>";
            result += "<th>Đơn giá đề nghị</th>";
            result += "<th>Giá trị đề nghị</th>";
            result += "<th>Đơn giá thẩm định</th>";
            result += "<th>Giá trị thẩm định</th>";
            result += "<th>Ghi chú</th>";
            result += "<th width='9%'>Thao tác</th>";
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";

            foreach (var item in model)
            {
                result += "<tr>";
                result += "<td style='text-align:center'>" + record++ + "</td>";
                result += "<td style='text-align:center'>" + item.Mats + "</td>";
                result += "<td class='active'>" + item.Tents + "</td>";
                result += "<td>" + item.Thongsokt + "</td>";
                result += "<td>" + item.Nguongoc + "</td>";
                result += "<td style='text-align:center'>" + item.Dvt + "</td>";
                result += "<td style='text-align:center'>" + item.Sl + "</td>";
                result += "<td style='text-align:right; font-weight:bold'>" + Helpers.ConvertDbToStr(item.Nguyengiadenghi) + "</td>";
                result += "<td style='text-align:right; font-weight:bold'>" + Helpers.ConvertDbToStr(item.Giadenghi) + "</td>";
                result += "<td style='text-align:right; font-weight:bold'>" + Helpers.ConvertDbToStr(item.Nguyengiathamdinh) + "</td>";
                result += "<td style='text-align:right; font-weight:bold'>" + Helpers.ConvertDbToStr(item.Giatritstd) + "</td>";
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
