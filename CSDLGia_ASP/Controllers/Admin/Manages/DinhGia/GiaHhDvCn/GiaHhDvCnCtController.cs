using CSDLGia_ASP.Database;
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
        public JsonResult Store(string Maspdv, string Mahs, string Dongia)
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
            var dmhh = _db.GiaHhDvCnDm.ToList();

            if (model != null)
            {

                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<input  type='text' id='mahs_edit' name='mahs_edit' value='" + model.Mahs + "' class='form-control'/>";
                result += "<div class='row text-left'>";
                result += "<div class='col-md-12'>";
                result += "<label><b>Tên sản phẩm, dịch vụ</b></label>";
                result += "<select class='form-control' id='maspdv_edit' name='maspdv_edit'>";
                //var dmhh = _db.GiaHhDvCnCt.ToList();
                foreach (var item in dmhh)
                {
                    result += "<option value ='" + item.Maspdv + "'>" + item.Tenspdv + "</ option >";
                }
                result += "</select>";
                result += "</div>";
                result += "</br>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Đơn giá</label>";
                result += "<input type='text' id='dongia_edit' name='dongia_edit' value='" + model.Dongia + "' class='form-control'/>";
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

        [Route("GiaHhDvCnCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Mahs, string Maspdv, string Dongia)
        {
            var model = _db.GiaHhDvCnCt.FirstOrDefault(t => t.Id == Id);
            model.Id = Id;
            model.Maspdv = Maspdv;
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
            //var model = _db.GiaHhDvCnCt.Where(t => t.Mahs == Mahs).ToList();
            // Tìm bản ghi dựa vào Mahs sau đó join với Dm
            var model = from ct in _db.GiaHhDvCnCt.Where(t => t.Mahs == Mahs)
                        join dm in _db.GiaHhDvCnDm on ct.Maspdv equals dm.Maspdv
                        select new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvCnCt
                        {
                            Id = ct.Id,
                            Tenspdv = dm.Tenspdv,
                            Dongia = ct.Dongia,
                            Maspdv = ct.Maspdv,
                            Trangthai = ct.Trangthai,
                           
                            Mahs = ct.Mahs,
                        };

            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover table-responsive' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th width='2%'>STT</th>";
            result += "<th>Tên sản phẩm dịch vụ</th>";
            result += "<th>Đơn giá</th>";
            result += "<th>Thao tác</th>";
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";
            foreach (var item in model)
            {
                result += "<tr>";
                result += "<td style='text-align:center'>" + record++ + "</td>";
                result += "<td class='active'>" + item.Tenspdv + "</td>";
                result += "<td style='text-align:center'>" + item.Dongia + "</td>";
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
