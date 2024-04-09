using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaTaiSanCong
{
    public class TaiSanCongCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public TaiSanCongCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaTaiSanCongCt/Store")]
        [HttpPost]
        public JsonResult Store(string Mahs, string Tentaisan, string Dacdiem, double Giathue, double Giaconlai, double Giapheduyet, double Giaban)
        {
            var model = new GiaTaiSanCongCt
            {
                Mahs = Mahs,
                Tentaisan = Tentaisan,
                Dacdiem = Dacdiem,
                Giathue = Giathue,
                Giaconlai = Giaconlai,
                Giapheduyet = Giapheduyet,
                Giaban = Giaban,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaTaiSanCongCt.Add(model);
            _db.SaveChanges();

            string result = GetData(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("GiaTaiSanCongCt/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaTaiSanCongCt.FirstOrDefault(t => t.Id == Id);
            _db.GiaTaiSanCongCt.Remove(model);
            _db.SaveChanges();
            var result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        public string GetData(string Mahs)
        {
            var model = _db.GiaTaiSanCongCt.Where(t => t.Mahs == Mahs).ToList();
            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th style='text-align:center'>STT</th>";
            result += "<th style='text-align:center'>Mã tài sản</th>";
            result += "<th style='text-align:center'>Tên tài sản</th>";
            result += "<th style='text-align:center'>Nguyên giá</th>";
            result += "<th style='text-align:center'>Giá còn lại</th>";
            result += "<th style='text-align:center'>Giá phê duyệt</th>";
            result += "<th style='text-align:center'>Giá bán<br />(thanh lý)</th>";
            result += "<th style='text-align:center'>Thao tác</th>";
            result += "</tr></thead><tbody>";
            if (model != null)
            {
                foreach (var item in model)
                {
                    result += "<tr>";
                    result += "<td style='text-align:center'>" + (record++) + "</td>";
                    result += "<td style='text-align:center'>" + item.Mataisan + "</td>";
                    result += "<td style='text-align:left'>" + item.Tentaisan + "</td>";
                    result += "<td style='text-align:ringt'>" + Helpers.ConvertDbToStr(item.Giathue) + "</td>";
                    result += "<td style='text-align:right'>" + Helpers.ConvertDbToStr(item.Giaconlai) + "</td>";
                    result += "<td style='text-align:right'>" + Helpers.ConvertDbToStr(item.Giapheduyet) + "</td>";
                    result += "<td style='text-align:right'>" + Helpers.ConvertDbToStr(item.Giaban) + "</td>";

                    result += "<td>";
                    result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Chỉnh sửa'";
                    result += " data-target='#Edit_Modal' data-toggle='modal' onclick='SetEdit(`" + item.Id + "`)'>";
                    result += "<i class='icon-lg la la-edit text-primary'></i>";
                    result += "</button>";
                    result += "</td></tr>";
                }
            }

            result += "</tbody>";
            return result;
        }

        [Route("GiaTaiSanCongCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaTaiSanCongCt.FirstOrDefault(p => p.Id == Id);
            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<input type='Hidden' id='id_edit' name='id_edit' value='" + Id + "' class='form-control'/>";
                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên tài sản</b></label>";
                result += "<label class='form-control'>" + @model.Tentaisan + "</label>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Nguyên giá</b></label>";
                result += "<input type='text' id='Giathue_edit' name='Giathue_edit' value='" + @model.Giathue + "'class='form-control money-decimal-mask' style='font-weight: bold'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Giá còn lại</b></label>";
                result += "<input type='text' id='Giaconlai_edit' name='Giaconlai_edit' value='" + @model.Giaconlai + "' class='form-control money-decimal-mask' style='font-weight: bold'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Giá phê duyệt</b></label>";
                result += "<input type='text' id='Giapheduyet_edit' name='Giapheduyet_edit' value='" + @model.Giapheduyet + "' class='form-control money-decimal-mask' style='font-weight: bold'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Giá bán (thanh lý)</b></label>";
                result += "<input type='text' id='Giaban_edit' name='Giaban_edit' value='" + @model.Giaban + "' class='form-control money-decimal-mask' style='font-weight: bold'/>";
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

        [Route("GiaTaiSanCongCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, double Giathue, double Giaconlai, double Giapheduyet, double Giaban)
        {
            var model = _db.GiaTaiSanCongCt.FirstOrDefault(t => t.Id == Id);
           
            model.Giathue = Giathue;
            model.Giaconlai = Giaconlai;
            model.Giapheduyet = Giapheduyet;
            model.Giaban = Giaban;
            model.Updated_at = DateTime.Now;
            _db.GiaTaiSanCongCt.Update(model);
            _db.SaveChanges();
            string result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }
    }
}
