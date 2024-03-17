using CSDLGia_ASP.Database;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaSpDvCongIch
{
    public class GiaSpDvCongIchCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaSpDvCongIchCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DinhGiaSpDvCongIchCt/Edit")]
        [HttpPost]

        public JsonResult EditCt(int Id)
        {
            var model = _db.GiaSpDvCongIchCt.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Mức giá 1</label>";
                result += "<input type='text' id='Mucgiatu_edit' name='Mucgiatu_edit' value='" + model.Mucgiatu + "' class='form-control money text-right' style='font-weight: bold'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Mức giá 2</label>";
                result += "<input type='text' id='Mucgiaden_edit' name='Mucgiaden_edit' value='" + model.Mucgiaden + "' class='form-control money text-right' style='font-weight: bold'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Mức giá 3</label>";
                result += "<input type='text' id='Mucgia3_edit' name='Mucgia3_edit' value='" + model.Mucgia3 + "' class='form-control money text-right' style='font-weight: bold'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Mức giá 4</label>";
                result += "<input type='text' id='Mucgia4_edit' name='Mucgia4_edit' value='" + model.Mucgia4 + "' class='form-control money text-right' style='font-weight: bold'/>";
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

        [Route("GiaSpDvCongIchCt/Update")]
        [HttpPost]
        public JsonResult UpdateCt(int Id, double Mucgiatu, double Mucgiaden, double Mucgia3, double Mucgia4)
        {
            var model = _db.GiaSpDvCongIchCt.FirstOrDefault(t => t.Id == Id);
            model.Mucgiatu = Mucgiatu;
            model.Mucgiaden = Mucgiaden;
            model.Mucgia3 = Mucgia3;
            model.Mucgia4 = Mucgia4;
            model.Updated_at = DateTime.Now;
            _db.GiaSpDvCongIchCt.Update(model);
            _db.SaveChanges();
            string result = GetDataCt(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        public string GetDataCt(string Mahs)
        {
            var model = _db.GiaSpDvCongIchCt.Where(t => t.Mahs == Mahs).ToList();
            var modeldanhmuc = _db.GiaSpDvCongIchDm.ToList();
            var modeldanhmucnhom = _db.GiaSpDvCongIchNhom.ToList();
            int record = 1;

            var groupmanhom1 = _db.GiaSpDvCongIchCt.Select(item => item.Manhom).Distinct().ToList();

            string result = "<div class='card-body' id='frm_data'>";

            foreach (var manhom in groupmanhom1)

            {
                foreach (var dm in modeldanhmucnhom)
                {
                    if (manhom == dm.Manhom)
                    {
                        result += "<p style='text-align:center'>" + dm.Tennhom + "</p>";
                    }
                }

                result += "<table class='table table-striped table-bordered table-hover dulieubang'>";

                result += "<thead>";
                result += "<tr style='text-align:center'>";
                result += "<th rowspan='2' width='2%'>STT</th>";
                result += "<th rowspan='2' width='2%'>Hiển thị</th>";
                result += "<th rowspan='2'>Tên sản phẩm dịch vụ</th>";
                result += "<th rowspan='2'>Đơn vị tính</th>";
                result += "<th colspan='4' >Mức giá</th>";
                result += "<th rowspan='2'>Thao tác</th>";
                result += "</tr>";

                result += "<tr style='text-align:center'>";
                result += "<th>Mức giá 1</th>";
                result += "<th>Mức giá 2</th>";
                result += "<th>Mức giá 3</th>";
                result += "<th>Mức giá 4</th>";
                result += "</tr>";

                result += "</thead>";

                result += "<tbody>";
                foreach (var item in model.Where(t => t.Manhom == manhom))
                {

                    result += "<tr>";
                    result += "<td style='text-align:center'>" + record++ + "</td>";
                    result += "<td style='text-align:left'>" + item.HienThi + "</td>";
                    result += "<td style='text-align:center'>" + item.Ten + "</td>";
                    result += "<td style='text-align:center'>" + item.Dvt + "</td>";
                    result += "<td style='text-align:center'>" + item.Mucgiatu + "</td>";
                    result += "<td style='text-align:center'>" + item.Mucgiaden + "</td>";
                    result += "<td style='text-align:center'>" + item.Mucgia3 + "</td>";
                    result += "<td style='text-align:center'>" + item.Mucgia4 + "</td>";

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

            }
            result += "</div>";

            return result;
        }

        [Route("DinhGiaSpDvCongIchCt/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaSpDvCongIchCt.FirstOrDefault(t => t.Id == Id);
            _db.GiaSpDvCongIchCt.Remove(model);
            _db.SaveChanges();
            var result = GetDataCt(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }
    }
}
