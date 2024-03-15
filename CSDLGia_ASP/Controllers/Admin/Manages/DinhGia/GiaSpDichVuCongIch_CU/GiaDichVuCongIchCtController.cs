using CSDLGia_ASP.Database;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaSpDichVuCongIch
{
    public class GiaDichVuCongIchCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaDichVuCongIchCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DinhGiaDvCiCt/Store")]
        [HttpPost]
        public JsonResult Store(string Mahs, string Maspdv, double Dongia)
        {
            var model = new GiaSpDvCiCt
            {
                Mahs = Mahs,
                Maspdv = Maspdv,
                Dongia = Dongia,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaSpDvCiCt.Add(model);
            _db.SaveChanges();

            string result = GetData(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }
        [Route("DinhGiaDvCiCt/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaSpDvCiCt.FirstOrDefault(t => t.Id == Id);
            _db.GiaSpDvCiCt.Remove(model);
            _db.SaveChanges();
            var result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }
        public string GetData(string Mahs)
        {
            var model = _db.GiaSpDvCiCt.Where(t => t.Mahs == Mahs).ToList();
            var GiaSpDvCiDm = _db.GiaSpDvCiDm.ToList();
            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table - bordered table - hover' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th>STT</th>";
            result += "<th>Tên sản phẩm dịch vụ</th>";
            result += "<th>Đơn giá </th>";
            result += "<th>Thao tác</th>";
            result += "</tr></thead><tbody>";

            foreach (var item in model)
            {
                result += "<tr>";
                result += "<td style='text-align:center'>" + (record++) + "</td>";
                result += "<td style='text-align:center'>";
                foreach (var dm in GiaSpDvCiDm)
                {
                    if (item.Maspdv == dm.Maspdv)
                    {
                        result += "<span>" + dm.Tenspdv + "</span>";
                    }
                }
                result += "</td>";
                result += "<td style='text-align:center'>" + item.Dongia + "</td>";
                result += "<td style='text-align:center'>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Chỉnh sửa'";
                result += " data-target='#Edit_Modal' data-toggle='modal' onclick='SetEdit(`" + item.Id + "`)'>";
                result += "<i class='icon-lg la la-edit text-primary'></i>";
                result += "</button>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Xóa'";
                result += " data-target='#Delete_Modal' data-toggle='modal' onclick='GetDelete(`" + item.Id + "`)'>";
                result += "<i class='icon-lg la la-trash text-danger'></i>";
                result += "</button></td></tr>";
            }
            result += "</tbody>";
            return result;
        }
        [Route("DinhGiaDvCiCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaSpDvCiCt.FirstOrDefault(p => p.Id == Id);
            var GiaSpDvCiDm = _db.GiaSpDvCiDm.ToList();
            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<input type='Hidden' id='id_edit' name='id_edit' value='" + Id + "' class='form-control'/>";
                result += "<div class='row'>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên sản phẩm dịch vụ</b></label>";
                result += "<select class='form-control' id='Maspdv_edit' name='Maspdv_edit'>";
                foreach (var item in GiaSpDvCiDm)
                {
                    result += "<option value='" + item.Maspdv + "' " + (item.Maspdv == model.Maspdv ? "selected" : "") + ">" + item.Tenspdv + " </option>";
                }
                result += "</select>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Đơn giá</b></label>";
                result += "<input type='text' id='Dongia_edit' name='Dongia_edit' value='" + @model.Dongia + "' class='form-control money text-right' style = 'font-weight: bold'/>";
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
        [Route("DinhGiaDvCiCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Maspdv, double Dongia)
        {
            var model = _db.GiaSpDvCiCt.FirstOrDefault(t => t.Id == Id);
            model.Dongia = Dongia;
            model.Maspdv = Maspdv;
            model.Updated_at = DateTime.Now;
            _db.GiaSpDvCiCt.Update(model);
            _db.SaveChanges();
            string result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }
    }
}

