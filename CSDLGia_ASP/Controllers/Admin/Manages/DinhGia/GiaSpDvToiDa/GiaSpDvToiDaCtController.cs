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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading;

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
                Mota = Mota,
                Dvt = Dvt,
                Dongia = Dongia,
                Phanloaidv= Phanloaidv,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaSpDvToiDaCt.Add(model);
            _db.SaveChanges();

            string result = GetData(Mahs);
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

                result += "<div class='row'>";
                result += "<div class='col-md-11' style='padding-left: 0px;'>";
                result += "<label>Phân loại sản phẩm, dịch vụ</label>";
                result += "<select class='form-control' id='phanloaidv_edit' name='phanloaidv_edit'>";
                var phanloaispdv = _db.GiaSpDvToiDaCt.ToList();
                foreach (var item in phanloaispdv)
                {
                    result += "<option value ='" + item.Phanloaidv + "'>" + item.Phanloaidv + "</ option >";
                }
                result += "</select>";
                result += "</div>";
                result += "</div>";

                result += "<br />";

                result += "<div class='row'>";
                result += "<div class='col-md-11' style='padding-left: 0px;'>";
                result += "<label>Tên sản phẩm dịch vụ</label>";
                result += "<input type='text' id='mota_edit' name='mota_edit' value='" + model.Mota + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "<br />";

                result += "<div class='row'>";
                result += "<div class='col-xl-6' style='padding-left: 0px;'>";
                result += "<label>Đơn vị tính</label>";
                result += "<select id='dvt_edit' name='dvt_edit' class='form-control'>";
                var donvitinh = _db.DmDvt.ToList();
                foreach (var item in donvitinh)
                {
                    result += "<option value ='" + item.Dvt + "'>" + item.Dvt + "</ option >";
                }
                result += "</select>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += " <div class='form-group fv-plugins-icon-container'>";
                result += "<label>Giá tối đa</label>";
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

        [Route("DinhGiaSpDvToiDaCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Mahs, string phanloaispvd, string mota, string dvt, double dongia)
        {
            var model = _db.GiaSpDvToiDaCt.FirstOrDefault(t => t.Id == Id);
            model.Mahs = Mahs;
            model.Phanloaidv = phanloaispvd;
            model.Mota = mota;
            model.Dvt = dvt;
            model.Dongia = dongia;

            model.Updated_at = DateTime.Now;
            _db.GiaSpDvToiDaCt.Update(model);
            _db.SaveChanges();
            string result = GetData(Mahs); // lấy dữ liệu theo mã hồ sơ
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
            var result = GetData(model.Mahs);
            var data = new { status = "success", message = result };

            return Json(data);
        }


        public string GetData(string Mahs)
        {
            var model = _db.GiaSpDvToiDaCt.Where(t => t.Mahs == Mahs).ToList();

            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";

            result += "<table class='table table-striped table-bordered table-hover table-responsive' id='datatable_4'>";

            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th width='2%'>STT</th>";
            result += "<th>Phân loại sản phẩm dịch vụ</th>";
            result += "<th>Tên sản phẩm dịch vụ</th>";
            result += "<th>Đơn vị tính</th>";
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
