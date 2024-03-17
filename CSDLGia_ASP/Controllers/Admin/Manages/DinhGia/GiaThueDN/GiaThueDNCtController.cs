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

        [Route("GiaThueMatDatMatNuocCt/Store")]
        [HttpPost]
        public JsonResult Store(string Mahs, string Vitri, string Diemdau, string Diemcuoi, string Mota, double Dientich, double Dongia, string PhanLoaiDatNuoc)
        {
            var model = new GiaThueMatDatMatNuocCt
            {
                PhanLoaiDatNuoc = PhanLoaiDatNuoc,
                Mahs = Mahs,
                Vitri = Vitri,
                Diemdau = Diemdau,
                Diemcuoi = Diemcuoi,
                Mota = Mota,
                Dientich = Dientich,
                Dongia1 = Dongia,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaThueMatDatMatNuocCt.Add(model);
            _db.SaveChanges();

            string result = GetData(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("GiaThueMatDatMatNuocCt/Edit")]
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
                result += "<label><b>Vị trí, địa bàn</b></label>";
                result += "<input type='text' id='loaidat_edit' name='loaidat_edit' value='" + model.LoaiDat + "' class='form-control'/>";
                result += "</div>";
                result += "</div>"; 

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Ngành, nghề đặc biệt ưu đãi đầu tư</label>";
                result += "<input type='text' id='dongia1_edit' name='dongia1_edit' class='form-control money text-right' style='font-weight: bold' value='" + model.Dongia1 + "'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Ngành, nghề ưu đãi đầu tư</label>";
                result += "<input type='text' id='dongia2_edit' name='dongia2_edit' class='form-control money text-right' style='font-weight: bold' value='" + model.Dongia2 + "'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Ngành, nghề khác</label>";
                result += "<input type='text' id='dongia3_edit' name='dongia3_edit' class='form-control money text-right' style='font-weight: bold' value='" + model.Dongia3 + "'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>STT báo cáo</label>";
                result += "<input type='text' id='hienthi_edit' name='hienthi_edit' value='" + model.HienThi + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Sắp xếp</label>";
                result += "<input type='text' id='sapxep_edit' name='sapxep_edit' value='" + model.SapXep + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "<input hidden type='text' id='id_edit' name='id_edit' value='" + Id + "' class='form-control'/>";

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

        [Route("GiaThueMatDatMatNuocCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string loaiDat, string hienThi, double sapXep, double Dongia1, double Dongia2, double Dongia3)
        {
            var model = _db.GiaThueMatDatMatNuocCt.FirstOrDefault(t => t.Id == Id);

            model.LoaiDat = loaiDat;
            model.HienThi = hienThi;
            model.SapXep = sapXep;           
            model.Dongia1 = Dongia1;
            model.Dongia2 = Dongia2;
            model.Dongia3 = Dongia3;
            model.Updated_at = DateTime.Now;
            _db.GiaThueMatDatMatNuocCt.Update(model);
            _db.SaveChanges();
            string result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("GiaThueMatDatMatNuocCt/Delete")]
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
            result += "<table class='table table-striped table-bordered table-hover' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th rowspan='2'>STT</th>";
            result += "<th rowspan='2'>Vị trí, địa bàn</th>";
            result += "<th colspan='3'>Tỷ lệ %</th>";
            result += "<th rowspan='2'>Thao tác</th>";
            result += "</tr>";
            result += "<tr style='text-align:center'>";
            result += "<th>Ngành, nghề đặc<br />biệt ưu đãi đầu tư</th>";
            result += "<th>Ngành, nghề ưu<br />đãi đầu tư</th>";
            result += "<th>gành, nghề khác</th>";
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";

            foreach (var item in model.OrderBy(x => x.SapXep))
            {
                result += "<tr  style='text-align:center'>";
                result += "<td>" + item.SapXep + "</td>";
                result += "<td class='active'>" + item.LoaiDat + "</td>";
                result += "<td style='text-align:right; font-weight:bold'>" + Helpers.ConvertDbToStrDecimal( item.Dongia1) + "</td>";
                result += "<td style='text-align:right; font-weight:bold'>" + Helpers.ConvertDbToStrDecimal(item.Dongia2) + "</td>";
                result += "<td style='text-align:right; font-weight:bold'>" + Helpers.ConvertDbToStrDecimal(item.Dongia3) + "</td>";
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
    }
}
