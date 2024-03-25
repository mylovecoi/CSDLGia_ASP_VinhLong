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
                result += "<input type='text' id='Mucgia1_edit' name='Mucgia1_edit' value='" + model.Mucgia1 + "' class='form-control money text-right' style='font-weight: bold'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Mức giá đến</label>";
                result += "<input type='text' id='Mucgia2_edit' name='Mucgia2_edit' value='" + model.Mucgia2 + "' class='form-control money text-right' style='font-weight: bold'/>";
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

        [Route("GiaSpDvCuTheCt/GetMaxSapXep")]
        [HttpPost]

        public JsonResult GetMaxSapXep(string Manhom)
        {
            var i = 0;
            var data = _db.GiaSpDvCuTheDm.Where(t => t.Manhom == Manhom);

            if (data.Any())
            {
                i = data.Max(x=>x.Sapxep);
            }

            return Json(i);
        }

        [Route("GiaSpDvCuTheCt/Update")]
        [HttpPost]
        public JsonResult UpdateCt(int Id, double Mucgia1, double Mucgia2)
        {
            var model = _db.GiaSpDvCuTheCt.FirstOrDefault(t => t.Id == Id);
            model.Mucgia1 = Mucgia1;
            model.Mucgia2 = Mucgia2;
            model.Updated_at = DateTime.Now;
            _db.GiaSpDvCuTheCt.Update(model);
            _db.SaveChanges();
            string result = GetDataCt(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }


        public string GetDataCt(string Mahs)
        {
            var modeldanhmucsp = _db.GiaSpDvCuTheDm.ToList();
            var model = _db.GiaSpDvCuTheCt.Where(t => t.Mahs == Mahs).ToList();
            var modeldanhmucnhom = _db.GiaSpDvCuTheNhom.ToList();
            int record = 1;
            var groupmanhom2 = _db.GiaSpDvCuTheNhom.Where(item => model.Select(x => x.Manhom).Contains(item.Manhom)).Select(x => x.Manhom).ToList();

            string result = "<div class='card-body' id='frm_data'>";

            foreach (var manhom in groupmanhom2)

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
                result += "<th width='2%'>STT</th>";
                result += "<th width='2%'>Hiển thị</th>";
                result += "<th>Tên sản phẩm dịch vụ</th>";
                result += "<th>Đơn vị tính</th>";
                result += "<th>Mức giá từ</th>";
                result += "<th>Mức giá đến</th>";
                result += "<th>Thao tác</th>";
                result += "</tr>";
                result += "</thead>";

                result += "<tbody>";
                foreach (var item in model.Where(t => t.Manhom == manhom))
                {

                    result += "<tr>";
                    result += "<td style='text-align:center'>" + record++ + "</td>";
                    result += "<td style='text-align:left'>" + item.Tt + "</td>";

                    foreach (var dm in modeldanhmucsp)
                    {
                        if (item.Maspdv == dm.Maspdv)
                        {
                            result += "<td style='text-align:left'>" + dm.Tenspdv + "</td>";
                        }
                    }

                    result += "<td style='text-align:center'>" + item.Dvt + "</td>";
                    result += "<td style='text-align:center'>" + item.Mucgia1 + "</td>";
                    result += "<td style='text-align:center'>" + item.Mucgia2 + "</td>";
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
