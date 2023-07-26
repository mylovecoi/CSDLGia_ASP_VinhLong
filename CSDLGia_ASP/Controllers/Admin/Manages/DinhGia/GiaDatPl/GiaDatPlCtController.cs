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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDatPl
{
    public class GiaDatPlCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaDatPlCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaDatCuThe/Store")]
        [HttpPost]
        public JsonResult Store(string Mahs, string Maloaidat, int Vitri, double Banggiadat, double Giacuthe, double Hesodc)
        {
            var model = new GiaDatPhanLoaiCt
            {
                Mahs = Mahs,
                Maloaidat = Maloaidat,
                Vitri = Vitri,
                Banggiadat = Banggiadat,
                Giacuthe = Giacuthe,
                Hesodc = Hesodc,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaDatPhanLoaiCt.Add(model);
            _db.SaveChanges();

            string result = GetData(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("GiaDatCuThe/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaDatPhanLoaiCt.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<input type='text' id='id_edit' name='id_edit' value='"+Id+"' class='form-control'/>";
                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên đường, giới hạn, khu vực</b></label>";
                result += "<input type='text' id='tenduong_edit' name='tenduong_edit' value='"+@model.Khuvuc+"' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Loại đất</b></label>";
                result += "<select class='form-control' id='mld_edit' name='mld_edit'>";
                var maloaidat= _db.GiaDatPhanLoaiDm.ToList();
                foreach(var item in maloaidat)
                {
                    result+="<option value ='"+@item.Maloaidat+"'>"+@item.Loaidat+"</ option >";
                }
                result += "</select></div></div></div>";
                result += "<div class='row'><div class='col-xl-3'><div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Vị trí</b></label>";
                result += "<select id='vt_edit' name='vt_edit' class='form-control'>";
                result += "<option value='1'>1</option>";
                result += "<option value='2'>2</option>";
                result += "<option value='3'>3</option>";
                result += "<option value='4'>4</option>";
                result += "</select></div></div>";
                result += "<div class='col-xl-3'><div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Giá tại bảng giá</b></label>";
                result += "<input type='text' id='giabg_create' name='giabg_create' class='form-control money text-right' style='font-weight: bold' value='"+@model.Banggiadat+"'/>";
                result += "</div></div><div class='col-xl-3'><div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Giá đất cụ thể</b></label>";
                result += "<input type='text' id='giact_create' name='giact_create' class='form-control money text-right' style='font-weight: bold' value='"+@model.Giacuthe+"'/>";
                result += "</div></div><div class='col-xl-3'><div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Hệ số điều chỉnh</b></label>";
                result += "<input type='text' id='hs_create' name='hs_create' class='form-control money text-right' style='font-weight: bold' value='"+@model.Hesodc+"'/>";
                result += "</div></div></div></div>";
                

                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Không tìm thấy thông tin cần chỉnh sửa!!!" };
                return Json(data);
            }
        }

        [Route("GiaDatCuThe/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Mahs, string Maloaidat, int Vitri, double Banggiadat, double Giacuthe, double Hesodc)
        {
            var model = _db.GiaDatPhanLoaiCt.FirstOrDefault(t => t.Id == Id);
            model.Mahs = Mahs;
            model.Maloaidat = Maloaidat;
            model.Vitri = Vitri;
            model.Banggiadat = Banggiadat;
            model.Giacuthe = Giacuthe;
            model.Hesodc = Hesodc;
            model.Updated_at = DateTime.Now;
            _db.GiaDatPhanLoaiCt.Update(model);
            _db.SaveChanges();
            string result = GetData(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("GiaDatCuThe/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaDatPhanLoaiCt.FirstOrDefault(t => t.Id == Id);
            _db.GiaDatPhanLoaiCt.Remove(model);
            _db.SaveChanges();
            var result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        public string GetData(string Mahs)
        {
            var model = _db.GiaDatPhanLoaiCt.Where(t => t.Mahs == Mahs).ToList();

            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table - bordered table - hover' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th>STT</th>";
            result += "<th>Tên đường, giới hạn, khu vực</th>";
            result += "<th>Loại đất</th>";
            result += "<th>Vị trí</th>";
            result += "<th>Giá đất tại bảng giá</th>";
            result += "<th>Giá đất cụ thể</th>";
            result += "<th>Hệ số điều chỉnh</th>";
            result += "<th>Thao tác</th>";
            result += "</tr></thead><tbody>";

            foreach (var item in model)
            {
                result += "<tr>";
                result += "<td style='text-align:center'></td>";
                result += "<td class='active'>" + item.Khuvuc + "</td>";
                result += "<td>" + item.Maloaidat + "</td>";
                result += "<td>" + item.Vitri + "</td>";
                result += "<td>" + Helpers.ConvertDbToStr(item.Banggiadat) + "</td>";
                result += "<td>" + Helpers.ConvertDbToStr(item.Giacuthe) + "</td>";
                result += "<td>" + Helpers.ConvertDbToStr(item.Hesodc) + "</td>";
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
            result += "</tr></thead><tbody>";
            return result;
        }
    }
}
