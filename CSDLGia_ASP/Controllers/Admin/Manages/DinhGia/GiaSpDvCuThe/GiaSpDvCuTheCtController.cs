using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
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
            var model = _db.GiaSpDvCuTheCt.FirstOrDefault(t => t.Id == Id);
            // Trả về JSON cho JavaScript
            return Json(model);
        }

        [Route("GiaSpDvCuTheCt/GetMaxSapXep")]
        [HttpPost]
        public JsonResult GetMaxSapXep(string Manhom)
        {
            var i = 0;
            var data = _db.GiaSpDvCuTheDm.Where(t => t.Manhom == Manhom);

            if (data.Any())
            {
                i = data.Max(x => x.Sapxep);
            }

            return Json(i);
        }

        [Route("DinhGiaSpDvCuTheCt/Update")]
        [HttpPost]
        public JsonResult UpdateCt(GiaSpDvCuTheCt requets)
        {
            var model = _db.GiaSpDvCuTheCt.FirstOrDefault(t => t.Id == requets.Id);
            model.Mucgia1 = requets.Mucgia1;
            model.Mucgia2 = requets.Mucgia2;
            model.Mucgia3 = requets.Mucgia3;
            model.Mucgia4 = requets.Mucgia4;
            model.Updated_at = DateTime.Now;
            _db.GiaSpDvCuTheCt.Update(model);
            _db.SaveChanges();
            string result = GetDataCt(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
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

        public string GetDataCt(string Mahs)
        {
            var model = _db.GiaSpDvCuTheCt.Where(t => t.Mahs == Mahs).ToList();
            var hoSo = _db.GiaSpDvCuThe.FirstOrDefault(t => t.Mahs == Mahs);
            string result = "<div class='card-body' id='frm_data'>";
            if(hoSo.PhanLoaiHoSo == "CHO") {
                //Hồ sơ giá chợ
                result += "<table class='table table-striped table-bordered table-hover' id=\"datatable_4\">";
                result += "<thead>";
                result += "<tr style='text-align:center'>";
                result += "<th width='2%'>STT</th>";
                result += "<th width='2%'>Hiển thị</th>";
                result += "<th>Tên sản phẩm dịch vụ</th>";
                result += "<th>Đơn vị tính</th>";
                result += "<th>Thành phố</th>";
                result += "<th>Thị xã</th>";
                result += "<th>Các huyện<br />đồng bằng</th>";
                result += "<th>Các huyện<br />miền núi</th>";
                result += "<th>Thao tác</th>";
                result += "</tr>";
                result += "</thead>";

                result += "<tbody>";
                foreach (var item in model.OrderBy(x => x.Sapxep))
                {

                    result += "<tr>";
                    result += "<td style='text-align:center'>" + item.Sapxep + "</td>";
                    result += "<td style='text-align:left'>" + item.Tt + "</td>";
                    result += "<td style='text-align:left'>" + item.TenSpDv + "</td>";
                    result += "<td style='text-align:center'>" + item.Dvt + "</td>";
                    result += "<td style='text-align:center'>" + Helpers.ConvertDbToStr(item.Mucgia1) + "</td>";
                    result += "<td style='text-align:center'>" + Helpers.ConvertDbToStr(item.Mucgia2) + "</td>";
                    result += "<td style='text-align:center'>" + Helpers.ConvertDbToStr(item.Mucgia3) + "</td>";
                    result += "<td style='text-align:center'>" + Helpers.ConvertDbToStr(item.Mucgia4) + "</td>";
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
            else {
                //Hồ sơ giá gửi xe
                result += "<table class='table table-striped table-bordered table-hover' id=\"datatable_4\">";
                result += "<thead>";
                result += "<tr style='text-align:center'>";
                result += "<th width='2%'>STT</th>";
                result += "<th width='2%'>Hiển thị</th>";
                result += "<th>Tên sản phẩm dịch vụ</th>";
                result += "<th>Đơn vị tính</th>";
                result += "<th>Điểm giữ xe thông<br />thường và danh lam<br />thắng cảnh</th>";
                result += "<th>Điểm giữ xe có nhu<br />cầu sử dụng lớn</th>";              
                result += "<th>Thao tác</th>";
                result += "</tr>";
                result += "</thead>";

                result += "<tbody>";
                foreach (var item in model.OrderBy(x => x.Sapxep))
                {

                    result += "<tr>";
                    result += "<td style='text-align:center'>" + item.Sapxep + "</td>";
                    result += "<td style='text-align:left'>" + item.Tt + "</td>";
                    result += "<td style='text-align:left'>" + item.TenSpDv + "</td>";
                    result += "<td style='text-align:center'>" + item.Dvt + "</td>";
                    result += "<td style='text-align:center'>" + Helpers.ConvertDbToStr(item.Mucgia1) + "</td>";
                    result += "<td style='text-align:center'>" + Helpers.ConvertDbToStr(item.Mucgia2) + "</td>";                   
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
    }
}
