using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GTroGiaTroCuoc
{
    public class TroGiaTroCuocCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public TroGiaTroCuocCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DinhGiaTGTCCt/Store")]
        [HttpPost]
        public JsonResult Store(string Mahs, string Maspdv, double Dongia)
        {
            var model = new GiaTroGiaTroCuocCt
            {
                Mahs = Mahs,
                Maspdv = Maspdv,
                Dongia = Dongia,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaTroGiaTroCuocCt.Add(model);
            _db.SaveChanges();

            string result = GetData(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }
        [Route("DinhGiaTGTCCt/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaTroGiaTroCuocCt.FirstOrDefault(t => t.Id == Id);
            _db.GiaTroGiaTroCuocCt.Remove(model);
            _db.SaveChanges();
            var result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }
        public string GetData(string Mahs)
        {
            var model = _db.GiaTroGiaTroCuocCt.Where(t => t.Mahs == Mahs).ToList();
            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th width='2%'>#</th>";
            result += "<th>Tên sản phẩm dịch vụ</th>";
            result += "<th width='10%'>Đơn vị tính</th>";
            result += "<th width='10%'>Đơn giá </th>";
            result += "<th width='5%'>Thao tác</th>";
            result += "</tr></thead><tbody>";

            foreach (var item in model)
            {
                result += "<tr>";
                result += "<td style='text-align:center'>" + (record++) + "</td>";
                result += "<td style='text-align:left'>" + item.Mota + "</td>";
                result += "<td style='text-align:center'>" + item.Dvt + "</td>";
                result += "<td style='text-align:right'>" + Helpers.ConvertDbToStr(item.Dongia) + "</td>";
                result += "<td style='text-align:center'>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Chỉnh sửa'";
                result += " data-target='#Edit_Modal' data-toggle='modal' onclick='SetEdit(`" + item.Id + "`)'>";
                result += "<i class='icon-lg la la-edit text-primary'></i>";
                result += "</button>";
                result+="</td></tr>";
            }
            result += "</tbody>";
            return result;
        }
        [Route("DinhGiaTGTCCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaTroGiaTroCuocCt.FirstOrDefault(p => p.Id == Id);
            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<input type='Hidden' id='id_edit' name='id_edit' value='" + Id + "' class='form-control'/>";
                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên sản phẩm dịch vụ</b></label>";
                result += "<lable class='form-control'>" + model.Mota + "</label>";            
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Đơn vị tính</b></label>";
                result += "<lable class='form-control'>" + model.Dvt + "</label>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Đơn giá</b></label>";
                result += "<input type='text' id='Dongia_edit' name='Dongia_edit' value='" + model.Dongia + "' class='form-control money-decimal-mask' style='font-weight: bold'/>";
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
        [Route("DinhGiaTGTCCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Maspdv, double Dongia)
        {
            var model = _db.GiaTroGiaTroCuocCt.FirstOrDefault(t => t.Id == Id);
            model.Dongia = Dongia;
            model.Updated_at = DateTime.Now;
            _db.GiaTroGiaTroCuocCt.Update(model);
            _db.SaveChanges();
            string result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }
    }
}
