using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CSDLGia_ASP.Database;
using System.Security.Cryptography;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaSpDvKhungGia
{
    public class GiaSpdvKhungGiaCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaSpdvKhungGiaCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DinhGiaSpDvKhungGiaCt/Store")] // Sinh ra 1 form mới thay thế form có id='frm_data'>
        [HttpPost]
        public JsonResult Store(string Mahs, string Mota, string Dvt, String Phanloaidv, double Giatoida, double Giatoithieu)
        {
            var model = new GiaSpDvKhungGiaCt
            {
                Mahs = Mahs,
                Mota = Mota,
                Dvt = Dvt,
                Phanloaidv = Phanloaidv,
                Giatoida = Giatoida,
                Giatoithieu = Giatoithieu,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaSpDvKhungGiaCt.Add(model);
            _db.SaveChanges();


            var dvt = new DmDvt
            {
                Dvt = Dvt,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.DmDvt.Add(dvt);
            _db.SaveChanges();

            string result = GetData(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("DinhGiaSpDvKhungGiaCt/Edit")]
        [HttpPost]

        public JsonResult EditCt(int Id)
        {
            var model = _db.GiaSpDvKhungGiaCt.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Giá tối thiểu</label>";
                result += "<input type='text' id='giatoithieu_edit' name='giatoithieu_edit' value='" + model.Giatoithieu + "' class='form-control money text-right' style='font-weight: bold'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Giá tối đa</label>";
                result += "<input type='text' id='giatoida_edit' name='giatoida_edit' value='" + model.Giatoida + "' class='form-control money text-right' style='font-weight: bold'/>";
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

        [Route("GiaSpDvKhungGiaCt/Update")]
        [HttpPost]
        public JsonResult UpdateCt(int Id, double Giatoithieu, double Giatoida)
        {
            var model = _db.GiaSpDvKhungGiaCt.FirstOrDefault(t => t.Id == Id);
            model.Giatoida = Giatoida;
            model.Giatoithieu = Giatoithieu;
            model.Updated_at = DateTime.Now;
            _db.GiaSpDvKhungGiaCt.Update(model);
            _db.SaveChanges();
            string result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }


        [Route("DinhGiaSpDvKhungGiaCt/Delete")] // Xóa dữ liệu trên 
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaSpDvKhungGiaCt.FirstOrDefault(t => t.Id == Id);
            _db.GiaSpDvKhungGiaCt.Remove(model);
            _db.SaveChanges();
            var result = GetData(model.Mahs);
            var data = new { status = "success", message = result };

            return Json(data);
        }

        public string GetData(string Mahs)
        {
            var model = _db.GiaSpDvKhungGiaCt.Where(t => t.Mahs == Mahs).ToList();

            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";

            result += "<table class='table table-striped table-bordered table-hover table-responsive' id='datatable_4'>";

            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th width='2%'>STT</th>";
            result += "<th>Phân loại sản phẩm dịch vụ</th>";
            result += "<th>Tên sản phẩm dịch vụ</th>";
            result += "<th>Đơn vị tính</th>";
            result += "<th>Mức giá tối thiểu</th>";
            result += "<th>Mức giá tối đa</th>";
            result += "<th>Thao tác</th>";
            result += "</tr>";
            result += "</thead>";

            result += "<tbody>";
            foreach (var item in model)
            {

                result += "<tr>";
                result += "<td style='text-align:center'>" + record++ + "</td>";
                result += "<td class='active'>" + item.Phanloaidv + "</td>";
                result += "<td style='text-align:center'>" + item.Mota + "</td>";
                result += "<td style='text-align:center'>" + item.Dvt + "</td>";
                result += "<td style='text-align:center'>" + item.Giatoithieu + "</td>";
                result += "<td style='text-align:center'>" + item.Giatoida+ "</td>";

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
