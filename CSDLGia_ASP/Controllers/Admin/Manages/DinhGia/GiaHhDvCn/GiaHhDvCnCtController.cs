using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaHhDvCn.GiaHhDvCnCt
{
    public class GiaHhDvCnCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaHhDvCnCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaHhDvCnCt/Store")]
        [HttpPost]
        public JsonResult Store(string Maspdv, string Mahs, double Dongia)
        {
            var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvCnCt
            {
                Mahs = Mahs,
                Maspdv = Maspdv,
                Dongia = Dongia,
                Trangthai = "CXD",
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaHhDvCnCt.Add(model);
            _db.SaveChanges();

            string result = GetData(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }




        [Route("GiaHhDvCnCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaHhDvCnCt.FirstOrDefault(p => p.Id == Id);
            string result = " <div class='modal-body' id='frm_edit'>";
            result += "<div class='row'>";
            result += "<div class='col-xl-12'>";
            result += "<div class='form-group fv-plugins-icon-container'>";
            result += "<label>Tên sản phẩm, dịch vụ*</label>";
            result += "<label class='form-control'>" + model.Tenspdv+ "</label>";
            result += "</div>";
            result += "</div>";
            result += "<div class='col-xl-12'>";
            result += "<div class='form-group fv-plugins-icon-container'>";
            result += "<label>Đơn giá*</label>";
            result += "<input type='text' name='dongia_edit' id='dongia_edit' class='form-control money-decimal-mask' value='" + Helpers.ConvertDbToStr(model.Dongia)+ "'/>";
            result += "</div>";
            result += "</div>";
            result += "</div>";
            result += "<input type='text' name='id_edit' id='id_edit' value='" + model.Id + "' hidden/>";
            result += "</div>";
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("GiaHhDvCnCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, double Dongia)
        {
            var model = _db.GiaHhDvCnCt.FirstOrDefault(t => t.Id == Id);           
            model.Dongia = Dongia;
            model.Updated_at = DateTime.Now;
            _db.GiaHhDvCnCt.Update(model);
            _db.SaveChanges();
            string result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("GiaHhDvCnCt/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaHhDvCnCt.FirstOrDefault(t => t.Id == Id);
            _db.GiaHhDvCnCt.Remove(model);
            _db.SaveChanges();
            string result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }


        public string GetData(string Mahs)
        {
            var model = _db.GiaHhDvCnCt.Where(t => t.Mahs == Mahs).ToList();
            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover table-responsive' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th width='2%'>STT</th>";
            result += "<th>Tên sản phẩm, dịch vụ</th>";
            result += "<th width='10%'>Đơn vị tính</th>";
            result += "<th width='10%'>Đơn giá</th>";
            result += "<th width='10%'>Thao tác</th>";
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";
            foreach (var item in model.OrderBy(x => x.Sapxep))
            {
                result += "<tr>";
                result += "<td style='text-align:center'>" + item.HienThi + "</td>";
                result += "<td style='text-align:left'>" + item.Tenspdv + "</td>";
                result += "<td style='text-align:center'>" + item.Dvt + "</td>";
                result += "<td style='text-align:right'>" + Helpers.ConvertDbToStr(item.Dongia) + "</td>";
                result += "<td>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Chỉnh sửa'";
                result += " data-target='#Edit_Modal' data-toggle='modal' onclick='SetEdit(`" + item.Id + "`)'>";
                result += "<i class='icon-lg la la-edit text-primary'></i>";
                result += "</button>";
                //result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Xóa'";
                //result += " data-target='#Delete_Modal' data-toggle='modal' onclick='GetDelete(`" + item.Id + "`)'>";
                //result += "<i class='icon-lg la la-trash text-danger'></i>";
                //result += "</button>";
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
