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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaThueDN
{
    public class GiaThueDNCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaThueDNCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DinhGiaThueMatDatMatNuocCt/Store")]
        [HttpPost]
        public JsonResult Store(string Mahs,int Vitri, string Diemdau, string Diemcuoi, string Mota,double Dientich,double Dongia)
        {
            var model = new GiaThueMatDatMatNuocCt
            {
                Mahs = Mahs,
                Vitri = Vitri,
                Diemdau = Diemdau,
                Diemcuoi = Diemcuoi,
                Mota = Mota,
                Dientich = Dientich,
                Dongia = Dongia,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaThueMatDatMatNuocCt.Add(model);
            _db.SaveChanges();

            string result = GetData(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }
        [Route("DinhGiaThueMatDatMatNuocCt/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaThueMatDatMatNuocCt.FirstOrDefault(t => t.Id == Id);
            _db.GiaThueMatDatMatNuocCt.Remove(model);
            _db.SaveChanges();
            var result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }
        public string GetData(string Mahs)
        {
            var model = _db.GiaThueMatDatMatNuocCt.Where(t => t.Mahs == Mahs).ToList();

            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table - bordered table - hover' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th>STT</th>";
            result += "<th>Vị trí</th>";
            result += "<th>Địa giới từ</th>";
            result += "<th>Địa giới đến</th>";
            result += "<th>Mô tả</th>";
            result += "<th>Diện tích</th>";
            result += "<th>Đơn giá</th>";
            result += "<th>Thao tác</th>";
            result += "</tr></thead><tbody>";

            foreach (var item in model)
            {
                result += "<tr>";
                result += "<td style='text-align:center'>"+(record++)+"</td>";
                result += "<td class='active'>" + item.Vitri + "</td>";
                result += "<td>" + item.Diemdau + "</td>";
                result += "<td>" + item.Diemcuoi + "</td>";
                result += "<td>" + item.Mota + "</td>";
                result += "<td>" + item.Dientich + "</td>";
                result += "<td>" + item.Dongia + "</td>";
                result += "<td>";
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
        [Route("DinhGiaThueMatDatMatNuocCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaThueMatDatMatNuocCt.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Vị trí</b></label>";
                result += "<input type='text' id='vitri_edit' name='vitri_edit' value='"+model.Vitri+"' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Địa giới từ</b></label>";
                result += "<input type='text' id='diemdau_edit' name='diemdau_edit' value='"+model.Diemdau+"' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Địa giới đến</b></label>";
                result += "<input type='text' id='diemcuoi_edit' name='diemcuoi_edit' value='"+model.Diemcuoi+"' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='row'><div class='col-xl-12'><div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mô tả</b></label>";
                result += "<input type='text' id='mota_edit' name='mota_edit' value='"+model.Mota+"' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'><div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Diện tích</b></label>";
                result += "<input type='text' id='dientich_edit' name='dientich_edit' class='form-control money text-right' style='font-weight: bold' value='"+model.Dientich+"'/>";
                result += "</div></div><div class='col-xl-6'><div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Đơn giá</b></label>";
                result += "<input type='text' id='dongia_edit' name='dongia_edit' class='form-control money text-right' style='font-weight: bold' value='"+model.Dongia+"'/>";
                result += "<input type='text' id='id_edit' name='id_edit' value='" + Id + "' class='form-control'/>";
                result += "</div></div></div>";
                

                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Không tìm thấy thông tin cần chỉnh sửa!!!" };
                return Json(data);
            }
        }
        [Route("DinhGiaThueMatDatMatNuocCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Mahs, int Vitri, string Diemdau, string Diemcuoi, string Mota,double Dientich,double Dongia)
        {
            var model = _db.GiaThueMatDatMatNuocCt.FirstOrDefault(t => t.Id == Id);
            model.Mahs = Mahs;
            model.Vitri = Vitri;
            model.Diemdau = Diemdau;
            model.Diemcuoi = Diemcuoi;
            model.Mota = Mota;
            model.Dientich=Dientich;
            model.Dongia=Dongia;
            model.Updated_at = DateTime.Now;
            _db.GiaThueMatDatMatNuocCt.Update(model);
            _db.SaveChanges();
            string result = GetData(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }
    }
}
