﻿using Microsoft.AspNetCore.Mvc;
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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.Giarung
{
    public class GiarungCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiarungCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaRungCt/Store")]
        [HttpPost]
        public JsonResult Store(string Mahs, string Dvthue, string Diachi, string Noidung, string Manhom, string Phanloai, double Dientich, double Dientichsd, string Dvt, string Soqdpd, DateTime Thoigianpd, string Soqdgkd, DateTime Thoigiangkd, DateTime Thuetungay, DateTime Thuedenngay, double Dongia, double Giakhoidiem, double Giatri)
        {
            var model = new GiaRungCt
            {
                Mahs = Mahs,
                Dvthue = Dvthue,
                Diachi = Diachi,
                Noidung = Noidung,
                Manhom = Manhom,
                Phanloai = Phanloai,
                Dientich = Dientich,
                Dientichsd = Dientichsd,
                Dvt = Dvt,
                Soqdpd = Soqdpd,
                Thoigianpd = Thoigianpd,
                Soqdgkd = Soqdgkd,
                Thoigiangkd = Thoigiangkd,
                Thuetungay = Thuetungay,
                Thuedenngay = Thuedenngay,
                Dongia = Dongia,
                Giakhoidiem = Giakhoidiem,
                Giatri = Giatri,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaRungCt.Add(model);
            _db.SaveChanges();

            string result = GetData(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("GiaRungCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaRungCt.FirstOrDefault(p => p.Id == Id);
            var dmdvt = _db.DmDvt.ToList();

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<input hidden type='text' id='id_edit' name='id_edit' value='" + model.Id + "'/>";
                result += "<input hidden type='text' id='mahs_edit' name='mahs_edit' value='" + model.Mahs + "'/>";

                result += "<div class='row'>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Đơn vị khai thác(thuê)</b></label>";
                result += "<input type='text' id='dvthue_edit' name='dvthue_edit' value='" + model.Dvthue + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Địa chỉ</b></label>";
                result += "<input type='text' id='diachi_edit' name='diachi_edit' value='" + model.Diachi + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Nội dung chi tiết</label>";
                result += "<textarea type='text' rows='3' id='noidung_edit' name='noidung_edit' class='form-control'>" + model.Noidung + "</textarea>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Phân loại</label>";
                result += "<select id='phanloai_edit' name='phanloai_edit' class='form-control'>";
                result += "<option value='Thuế môi trường' " + (model.Phanloai == "Thuế môi trường" ? "selected" : "") + ">Thuế môi trường</option>";
                result += "<option value='Khai thác' " + (model.Phanloai == "Khai thác" ? "selected" : "") + ">Khai thác</option>";
                result += "<option value='Thanh lý' " + (model.Phanloai == "Thanh lý" ? "selected" : "") + ">Thanh lý</option>";
                result += "</select>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Loại rừng</label>";
                result += "<select class='form-control' id='loairung_edit' name='loairung_edit'>";
                var loairung = _db.GiaRungDm.ToList();
                foreach (var item in loairung)
                {
                    result += "<option value ='" + item.Manhom + "' " + (model.Manhom == item.Manhom ? "selected" : "") + ">" + item.Tennhom + "</ option >";
                }
                result += "</select>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Diện tích</label>";
                result += "<input type='text' id='dt_edit' name='dt_edit' value='" + model.Dientich + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Diện tích khai thác (thuê)</label>";
                result += "<input type='text' id='dtthue_edit' name='dtthue_edit' value='" + model.Dientichsd + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Đơn vị tính</b></label>";
                result += "<select id='dvtinh_edit' name='dvtinh_edit' class='form-control'>";
                foreach (var dvt in dmdvt)
                {
                    result += "<option value='" + dvt.Dvt + "' " + (model.Dvt == dvt.Dvt ? "selected" : "") + ">" + dvt.Dvt + "</option>";
                }
                result += "</select>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Số quyết khai thác(thuê)</label>";
                result += "<input type='text' id='sqthue_edit' name='sqthue_edit' value='" + model.Soqdpd + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Thời điểm khai thác(thuê)</label>";
                result += "<input type='date' id='tdthue_edit' name='tdthue_edit' value='" + Helpers.ConvertDateToFormView(model.Thoigianpd) + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Số quyết giá khởi điểm</label>";
                result += "<input type='text' id='sqkd_edit' name='sqkd_edit' value='" + model.Soqdgkd + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";


                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Thời điểm giá khởi điểm</label>";
                result += "<input type='date' id='tdkd_edit' name='tdkd_edit' value='" + Helpers.ConvertDateToFormView(model.Thoigiangkd) + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Thời gian - Từ ngày</label>";
                result += "<input type='date' id='tungay_edit' name='tungay_edit' value='" + Helpers.ConvertDateToFormView(model.Thuetungay) + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Thời gian - Đến ngày</label>";
                result += "<input type='date' id='denngay_edit' name='denngay_edit' value='" + Helpers.ConvertDateToFormView(model.Thuedenngay) + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'></div>";

                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Đơn giá</label>";
                result += "<input type='text' id='dongia_edit' name='dongia_edit' value='" + model.Dongia + "' class='form-control money text-right'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Giá khởi điểm*</label>";
                result += "<input type='text' id='giakd_edit' name='giakd_edit' value='" + model.Giakhoidiem + "' class='form-control money text-right'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Giá khai thác(thuê)*</label>";
                result += "<input type='text' id='giathue_edit' name='giathue_edit' value='" + model.Giatri + "' class='form-control money text-right'/>";
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

        [Route("GiaRungCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Mahs, string Dvthue, string Diachi, string Noidung, string Manhom, string Phanloai, string Dvt, string Soqdpd, DateTime Thoigianpd, string Soqdgkd, DateTime Thoigiangkd, DateTime Thuetungay, DateTime Thuedenngay, string Mota, double Dientich, double Dientichsd, double Dongia, double Giakhoidiem, double Giatri)
        {
            var model = _db.GiaRungCt.FirstOrDefault(t => t.Id == Id);
            model.Dvthue = Dvthue;
            model.Diachi = Diachi;
            model.Noidung = Noidung;
            model.Manhom = Manhom;
            model.Phanloai = Phanloai;
            model.Dientich = Dientich;
            model.Dientichsd = Dientichsd;
            model.Dvt = Dvt;
            model.Soqdpd = Soqdpd;
            model.Thoigianpd = Thoigianpd;
            model.Soqdgkd = Soqdgkd;
            model.Thoigiangkd = Thoigiangkd;
            model.Thuetungay = Thuetungay;
            model.Thuedenngay = Thuedenngay;
            model.Dongia = Dongia;
            model.Giakhoidiem = Giakhoidiem;
            model.Giatri = Giatri;
            model.Updated_at = DateTime.Now;
            _db.GiaRungCt.Update(model);
            _db.SaveChanges();
            string result = GetData(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("GiaRungCt/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaRungCt.FirstOrDefault(t => t.Id == Id);
            _db.GiaRungCt.Remove(model);
            _db.SaveChanges();
            var result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        public string GetData(string Mahs)
        {
            var model = _db.GiaRungCt.Where(t => t.Mahs == Mahs).ToList();
            var loairung = _db.GiaRungDm.ToList();
            int record = 1;

            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th>STT</th>";
            result += "<th>Phân loại</th>";
            result += "<th>Loại rừng</th>";
            result += "<th>Đơn vị khac thác(thuê)</th>";
            result += "<th>Nội dung chi tiết</th>";
            result += "<th>Diện tích rừng</th>";
            result += "<th>Diện tích sử dụng</th>";
            result += "<th>Đơn vị tính</th>";
            result += "<th>Giá trị</th>";
            result += "<th>Thao tác</th>";
            result += "</tr></thead><tbody>";
            foreach (var item in model)
            {
                result += "<tr>";
                result += "<td style='text-align:center'>" + (record++) + "</td>";
                result += "<td class='active'>" + item.Phanloai + "</td>";
                foreach (var lr in loairung)
                {
                    if (item.Manhom == lr.Manhom)
                    {
                        result += "<td>" + lr.Tennhom + "</td>";
                    }
                }
                result += "<td>" + item.Dvthue + "</td>";
                result += "<td>" + item.Noidung + "</td>";
                result += "<td style='text-align:right; font-weight: bold'>" + item.Dientich + "</td>";
                result += "<td style='text-align:right; font-weight: bold'>" + item.Dientichsd + "</td>";
                result += "<td>" + item.Dvt + "</td>";
                result += "<td style='text-align:right; font-weight: bold'>" + item.Giatri + "</td>";
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
            return result;
        }
    }
}