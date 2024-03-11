using CSDLGia_ASP.Database;
using Microsoft.AspNetCore.Mvc;
using System;
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
                result += "<label>Mức giá từ</label>";
                result += "<input type='text' id='Mucgiatu_edit' name='Mucgiatu_edit' value='" + model.Mucgiatu + "' class='form-control money text-right' style='font-weight: bold'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Mức giá đến</label>";
                result += "<input type='text' id='Mucgiaden_edit' name='Mucgiaden_edit' value='" + model.Mucgiaden + "' class='form-control money text-right' style='font-weight: bold'/>";
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


        [Route("GiaSpDvCongIchCt/Update")]
        [HttpPost]
        public JsonResult UpdateCt(int Id, double Mucgiatu, double Mucgiaden)
        {
            var model = _db.GiaSpDvCongIchCt.FirstOrDefault(t => t.Id == Id);
            model.Mucgiatu = Mucgiatu;
            model.Mucgiaden = Mucgiaden;
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
            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";

            result += "<table class='table table-striped table-bordered table-hover table-responsive' id='datatable_4'>";

            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th width='2%'>STT</th>";
            result += "<th>Hiển thị</th>";
            result += "<th>Danh mục</th>";
            result += "<th>Đơn vị tính</th>";
            result += "<th>Mức giá từ</th>";
            result += "<th>Mức giá đến</th>";
            result += "<th>Thao tác</th>";
            result += "</tr>";
            result += "</thead>";

            result += "<tbody>";
            foreach (var item in model.Where(t => t.Capdo == "1"))
            {

                result += "<tr>";
                result += "<td style='text-align:center'>" + record++ + "</td>";
                result += "<td style='text-align:center'>" + item.HienThi + "</td>";
                result += "<td style='text-align:left'>" + item.Ten + "</td>";
                result += "<td style='text-align:center'>" + item.Dvt + "</td>";
                result += "<td style='text-align:center'>" + item.Mucgiatu + "</td>";
                result += "<td style='text-align:center'>" + item.Mucgiaden + "</td>";

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
                foreach (var item1 in model.Where(t => t.Magoc == item.Maso))
                {

                    result += "<tr>";
                    result += "<td style='text-align:center'>" + record++ + "</td>";
                    result += "<td style='text-align:center'>" + item1.HienThi + "</td>";
                    result += "<td style='text-align:left'>" + item1.Ten + "</td>";
                    result += "<td style='text-align:center'>" + item1.Dvt + "</td>";
                    result += "<td style='text-align:center'>" + item1.Mucgiatu + "</td>";
                    result += "<td style='text-align:center'>" + item1.Mucgiaden + "</td>";

                    result += "<td>";
                    result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Chỉnh sửa'";
                    result += " data-target='#Edit_Modal' data-toggle='modal' onclick='SetEdit(`" + item1.Id + "`)'>";
                    result += "<i class='icon-lg la la-edit text-primary'></i>";
                    result += "</button>";
                    result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Xóa'";
                    result += " data-target='#Delete_Modal' data-toggle='modal' onclick='GetDelete(`" + item1.Id + "`)'>";
                    result += "<i class='icon-lg la la-trash text-danger'></i>";
                    result += "</button>";
                    result += "</td>";
                    result += "</tr>";
                    foreach (var item2 in model.Where(t => t.Magoc == item1.Maso))
                    {

                        result += "<tr>";
                        result += "<td style='text-align:center'>" + record++ + "</td>";
                        result += "<td style='text-align:center'>" + item2.HienThi + "</td>";
                        result += "<td style='text-align:left'>" + item2.Ten + "</td>";
                        result += "<td style='text-align:center'>" + item2.Dvt + "</td>";
                        result += "<td style='text-align:center'>" + item2.Mucgiatu + "</td>";
                        result += "<td style='text-align:center'>" + item2.Mucgiaden + "</td>";

                        result += "<td>";
                        result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Chỉnh sửa'";
                        result += " data-target='#Edit_Modal' data-toggle='modal' onclick='SetEdit(`" + item2.Id + "`)'>";
                        result += "<i class='icon-lg la la-edit text-primary'></i>";
                        result += "</button>";
                        result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Xóa'";
                        result += " data-target='#Delete_Modal' data-toggle='modal' onclick='GetDelete(`" + item2.Id + "`)'>";
                        result += "<i class='icon-lg la la-trash text-danger'></i>";
                        result += "</button>";
                        result += "</td>";
                        result += "</tr>";
                    }
                }
            }
            result += "</tbody>";

            result += "</table>";
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
