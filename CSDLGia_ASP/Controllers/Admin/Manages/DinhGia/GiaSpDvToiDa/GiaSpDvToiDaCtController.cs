using CSDLGia_ASP.Database;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaSpDvToiDa
{
    public class GiaSpdvToiDaCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaSpdvToiDaCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        //ok
        [Route("DinhGiaSpDvToiDaCt/Store")] // Sinh ra 1 form mới thay thế form có id='frm_data'> 
        [HttpPost]
        public JsonResult Store(string Mahs, string Mota, string Dvt, double Dongia, string Phanloaidv)
        {
            var model = new GiaSpDvToiDaCt
            {
                Mahs = Mahs,
                //Mota = Mota,
                Dvt = Dvt,
                Dongia = Dongia,
                Phanloaidv = Phanloaidv,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaSpDvToiDaCt.Add(model);
            _db.SaveChanges();

            string result = GetDataCt(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("DinhGiaSpDvToiDaCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaSpDvToiDaCt.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Giá(đồng)</label>";
                result += "<input type='text' id='gia_edit' name='gia_edit' value='" + model.Dongia + "' class='form-control money-decimal-mask' style='font-weight: bold'/>";
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

        [Route("DinhGiaSpDvToiDaCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, double Gia)
        {
            var model = _db.GiaSpDvToiDaCt.FirstOrDefault(t => t.Id == Id);
            model.Dongia = Gia;
            model.Updated_at = DateTime.Now;
            _db.GiaSpDvToiDaCt.Update(model);
            _db.SaveChanges();
            string result = GetDataCt(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("DinhGiaSpDvToiDaCt/Delete")] // Xóa dữ liệu trên 
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaSpDvToiDaCt.FirstOrDefault(t => t.Id == Id);
            _db.GiaSpDvToiDaCt.Remove(model);
            _db.SaveChanges();
            var result = GetDataCt(model.Mahs);
            var data = new { status = "success", message = result };

            return Json(data);
        }


        public string GetDataCt(string Mahs)
        {
            var model = _db.GiaSpDvToiDaCt.Where(t => t.Mahs == Mahs).ToList();

            string result = "<div class='card-body' id='frm_data'>";

            result += "<table class='table table-striped table-bordered table-hover table-responsive' id='datatable_4'>";

            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th width='2%'>STT</th>";
            result += "<th>STT<br />báo cáo</th>";
            result += "<th>Tên sản phẩm dịch vụ</th>";
            result += "<th>Đơn vị tính</th>";
            result += "<th>Mức giá tối đa</th>";
            result += "<th>Thao tác</th>";
            result += "</tr>";
            result += "</thead>";

            result += "<tbody>";
            foreach (var item in model.OrderBy(x => x.Sapxep))
            {

                result += "<tr>";
                result += "<td style='text-align:center'>" + item.Sapxep + "</td>";
                result += "<td>" + item.HienThi + "</td>";
                result += "<td style='text-align:center'>" + item.Tenspdv + "</td>";
                result += "<td style='text-align:center'>" + item.Dvt + "</td>";
                result += "<td style='text-align:right'>" + Helper.Helpers.ConvertDbToStr(item.Dongia) + "</td>";

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
