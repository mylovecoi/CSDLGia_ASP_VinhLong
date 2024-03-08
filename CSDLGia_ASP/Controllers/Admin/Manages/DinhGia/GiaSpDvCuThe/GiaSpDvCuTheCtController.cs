using CSDLGia_ASP.Database;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaSpDvCuThe
{
    public class GiaSpdvCuTheCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaSpdvCuTheCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }


        [Route("DinhGiaSpDvCuTheCt/Edit")]
        [HttpPost]

        public JsonResult EditCt(int Id)
        {
            var model = _db.GiaSpDvCuTheCt.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Mức giá từ</label>";
                result += "<input type='text' id='gia_edit' name='gia_edit' value='" + model.Mucgiatu + "' class='form-control money text-right' style='font-weight: bold'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Mức giá đến</label>";
                result += "<input type='text' id='gia_edit' name='gia_edit' value='" + model.Mucgiaden + "' class='form-control money text-right' style='font-weight: bold'/>";
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


        [Route("GiaSpDvCuTheCt/Update")]
        [HttpPost]
        public JsonResult UpdateCt(int Id, double Mucgiatu, double Mucgiaden)
        {
            var model = _db.GiaSpDvCuTheCt.FirstOrDefault(t => t.Id == Id);
            model.Mucgiatu = Mucgiatu;
            model.Mucgiaden = Mucgiaden;
            model.Updated_at = DateTime.Now;
            _db.GiaSpDvCuTheCt.Update(model);
            _db.SaveChanges();
            string result = GetDataCt(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }


        public string GetDataCt(string Mahs)
        {
            var model = _db.GiaSpDvCuTheCt.Where(t => t.Mahs == Mahs).ToList();
            var modeldanhmuc = _db.GiaSpDvCuTheDm.ToList();

            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";

            result += "<table class='table table-striped table-bordered table-hover table-responsive' id='datatable_4'>";

            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th width='2%'>STT</th>";

            result += "<th>Vùng</th>";

            result += "<th>Biện pháp công trình</th>";
            result += "<th>Thao tác</th>";
            result += "</tr>";
            result += "</thead>";

            result += "<tbody>";
            foreach (var item in model)
            {

                result += "<tr>";
                result += "<td style='text-align:center'>" + record++ + "</td>";
                result += "<td style='text-align:left'>";
                foreach (var dm in modeldanhmuc)
                {
                    if (item.Maspdv == dm.Maspdv)
                    {
                        result += "<td style='text-align:center'>" + dm.Tenspdv + "</td>";
                    }
                } 
            
                result += "<td style='text-align:left'>" + item.Mota + "</td>";

                result += "<td style='text-align:center'>" + item.Mucgiatu + "</td>";

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
        [Route("DinhGiaSpDvCuTheCt/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaSpDvCuTheCt.FirstOrDefault(t => t.Id == Id);
            _db.GiaSpDvCuTheCt.Remove(model);
            _db.SaveChanges();
            var result = GetDataCt(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }
    }
}
