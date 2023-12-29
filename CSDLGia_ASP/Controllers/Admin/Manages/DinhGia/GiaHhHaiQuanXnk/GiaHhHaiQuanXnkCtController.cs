using CSDLGia_ASP.Database;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaHhHaiQuanXnk
{
    public class GiaHhHaiQuanXnkCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaHhHaiQuanXnkCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaHhHaiQuanXnkCt/Store")]
        [HttpPost]
        public JsonResult Store(string Mahs, string TenHh, double Giachuathue,
            double Giasauthue, double Thue, string Dvt, string Loaithue)
        {
            var model = new GiaHhHaiQuanXnkCt
            {
                Mahs = Mahs,
                TenHh = TenHh,
                GiaTruocThue = Giachuathue,
                GiaSauThue = Giasauthue,
                PhanTramThue = Thue,
                Dvt = Dvt,
                MaThue = Loaithue,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaHhHaiQuanXnkCt.Add(model);
            _db.SaveChanges();

            string result = GetData(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }
        [Route("GiaHhHaiQuanXnkCt/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaHhHaiQuanXnkCt.FirstOrDefault(t => t.Id == Id);
            _db.GiaHhHaiQuanXnkCt.Remove(model);
            _db.SaveChanges();
            var result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }
        public string GetData(string Mahs)
        {
            int recordId = 1;
            //var model = _db.GiaHhHaiQuanXnkCt.Where(t => t.Mahs == Mahs).ToList();
            var model = from ct in _db.GiaHhHaiQuanXnkCt.Where(x => x.Mahs == Mahs)
                        join thue in _db.GiaHhHaiQuanXnkThue
                        on ct.MaThue equals thue.MaThue
                        select new GiaHhHaiQuanXnkCt
                        {
                            TenHh = ct.TenHh,
                            Dvt = ct.Dvt,
                            GiaTruocThue = ct.GiaTruocThue,
                            GiaSauThue = ct.GiaSauThue,
                            PhanTramThue = ct.PhanTramThue,
                            MaThue = ct.MaThue,
                            TenThue = thue.TenThue,
                        };

            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table - bordered table - hover' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th>STT</th>";
            result += "<th>Tên hàng hoá</th>";
            result += "<th>Đơn vị tính</th>";
            result += "<th>Giá trước thuế</th>";
            result += "<th>Thuế(%)</th>";
            result += "<th>Giá sau thuế</th>";
            result += "<th>Thao tác</th>";
            result += "</tr></thead><tbody>";

            foreach (var item in model)
            {
                result += "<tr>";
                result += "<td style='text-align:center'>" + (recordId++) + "</td>";
                result += "<td class='active'>" + item.TenHh + "</td>";
                result += "<td>" + item.Dvt + "</td>";
                result += "<td>" + item.GiaTruocThue + "</td>";
                result += "<td>" + item.TenThue + ": " + item.PhanTramThue + "%</td>";
                result += "<td>" + item.GiaSauThue + "</td>";
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
            result += "</tr></thead><tbody>";
            return result;
        }
        [Route("GiaHhHaiQuanXnkCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaHhHaiQuanXnkCt.FirstOrDefault(p => p.Id == Id);
            var Dvt = _db.DmDvt;
            var Thue = _db.GiaHhHaiQuanXnkThue;
            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<input type='text' id='id_edit' name='id_edit' value='" + Id + "' class='form-control'/>";
                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên hàng hoá</b></label>";
                result += "<input type='text' id='tenhh_edit' name='tenhh_edit' value='" + @model.TenHh + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Giá chưa thuế</b></label>";
                result += "<input type='text' id='giachuathue_edit' name='giachuathue_edit' value='" + @model.GiaTruocThue + "' class='form-control money'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Giá sau thuế</b></label>";
                result += "<input type='text' id='giasauthue_edit' name='giasauthue_edit' value='" + @model.GiaSauThue + "' class='form-control money'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Loại thuế</b></label>";
                result += "<select id='loaithue_edit' name='loaithue_edit' class='form-control'>";
                foreach (var thue in Thue)
                {
                    result += "<option value='" + thue.MaThue + "'" + (thue.MaThue == model.MaThue ? "selected" : "") + ">" + thue.TenThue + "</option>";
                }
                result += "</select>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Thuế</b></label>";
                result += "<input type='number' id='thue_edit' name='thue_edit' value='" + @model.PhanTramThue + "' class='form-control money'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-5'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Đơn vị tính</b></label>";
                result += "<select id='dvt_edit' name='dvt_edit' class='form-control'>";
                foreach (var dvt in Dvt)
                {
                    result += "<option value='" + dvt.Dvt + "'" + (dvt.Dvt == model.Dvt ? "selected" : "") + ">" + @dvt.Dvt + "</option>";
                }
                result += "</select>";
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
        [Route("GiaHhHaiQuanXnkCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string TenHh, double Giachuathue, double Giasauthue, double Thue, string Dvt)
        {
            var model = _db.GiaHhHaiQuanXnkCt.FirstOrDefault(t => t.Id == Id);
            model.TenHh = TenHh;
            model.GiaTruocThue = Giachuathue;
            model.GiaSauThue = Giasauthue;
            model.PhanTramThue = Thue;
            model.Dvt = Dvt;
            model.Updated_at = DateTime.Now;
            _db.GiaHhHaiQuanXnkCt.Update(model);
            _db.SaveChanges();
            string result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }
    }
}
