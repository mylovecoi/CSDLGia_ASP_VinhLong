using CSDLGia_ASP.Database;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class RegistersController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public RegistersController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DoanhNghiep/DangKy/Lvkd/Store")]
        [HttpPost]
        public JsonResult Store(string Madv, string Manghe, string Manghanh, string Madiaban)
        {
            var model = new CompanyLvCc
            {
                Madv = Madv,
                Manghe = Manghe,
                Manganh = Manghanh,
                Macqcq = Madiaban,
                Trangthai = "CXD",
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };

            _db.CompanyLvCc.Add(model);
            _db.SaveChanges();
            string result = this.GetData(Madv);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("DoanhNghiep/DangKy/Lvkd/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.CompanyLvCc.FirstOrDefault(p => p.Id == Id);
            var dmnganhkd = _db.DmNganhKd.ToList();
            var dmnghekd = _db.DmNgheKd.ToList();
            var dsdiaban = _db.DsDiaBan.Where(t => t.Level != "ADMIN").ToList();
            var dsdonvi = _db.DsDonVi.ToList();

            if (model != null)
            {
                string result = "<div class='row text-left' id='edit_thongtin'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Ngành - Nghề</label>";
                result += "<select class='form-control' id='manghe_edit' name='manghe_edit'>";
                foreach (var nganh in dmnganhkd)
                {
                    result += "<optgroup label='" + nganh.Tennganh + "'>";
                    var dmnganhnghekd = dmnghekd.Where(t => t.Manganh == nganh.Manganh).ToList();
                    foreach (var nghe in dmnganhnghekd)
                    {
                        result += "<option value='" + nghe.Manghe + "'" + ((string)model.Manghe == nghe.Manghe ? "selected" : "") + ">" + nghe.Tennghe + "</option>";
                    }
                    result += "</optgroup>";
                }
                result += "</select>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Địa bàn kinh doanh</label>";
                result += "<select class='form-control' id='madiaban_edit' name='madiaban_edit'>";
                result += "<option value= '' '" + ((string)model.Macqcq == "" ? "selected" : "") + "'>--Chọn địa bàn kinh doanh--</option>";
                foreach (var item in dsdiaban)
                {
                    result += "<option value= '" + item.MaDiaBan + "'" + ((string)model.Macqcq == item.MaDiaBan ? "selected" : "") + ">" + item.TenDiaBan + "</ option >";
                }
                result += "</select>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Đơn vị nhận hồ sơ</label>";
                result += "<select class='form-control' id='madvhs_edit' name='madvhs_edit'>";
                result += "<option value= ''>--Chọn đơn vị nhận hồ sơ--</option>";
                foreach (var db in dsdiaban)
                {
                    foreach (var item in dsdonvi.Where(t => t.MaDiaBan == db.MaDiaBan && t.ChucNang == "NHAPLIEU").ToList())
                    {
                        result += "<option value= '" + item.MaDv + "' >" + item.TenDv + "</ option >";
                    }

                }
                result += "</select>";
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

        [Route("DoanhNghiep/DangKy/Lvkd/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Madiaban, string Madv, string Manghe)
        {
            var model = _db.CompanyLvCc.FirstOrDefault(t => t.Id == Id);
            model.Manghe = Manghe;
            model.Madv = Madv;
            model.Macqcq = Madiaban;
            model.Updated_at = DateTime.Now;
            _db.CompanyLvCc.Update(model);
            _db.SaveChanges();
            string result = this.GetData(Madv);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("DoanhNghiep/DangKy/Lvkd/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.CompanyLvCc.FirstOrDefault(t => t.Id == Id);
            _db.CompanyLvCc.Remove(model);
            _db.SaveChanges();
            string result = this.GetData(model.Madv);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        //Get
        public string GetData(string Madv)
        {
            var model = _db.CompanyLvCc.Where(t => t.Madv == Madv).ToList();
            var model_join = (from cty in model
                              join dmnghe in _db.DmNgheKd on cty.Manghe equals dmnghe.Manghe
                              select new VMCompanyLvCc
                              {
                                  Id = cty.Id,
                                  Mahs = cty.Mahs,
                                  Madv = cty.Madv,
                                  Manganh = cty.Manganh,
                                  Manghe = cty.Manghe,
                                  Tennghe = dmnghe.Tennghe,
                                  Macqcq = cty.Macqcq,
                                  Trangthai = cty.Trangthai,

                              });

            int record = 1;
            string result = "<div class='mb-12' id='lvkd_data'>";
            result += "<table class='table table-striped table-bordered table-hover table-responsive' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th width='2%'>#</th>";
            result += "<th>Tên nghề kinh doanh</th>";
            result += "<th>Đơn vị quản lý</th>";
            result += "<th width='15%'>Thao tác</th>";
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";

            foreach (var item in model_join)
            {
                result += "<tr>";
                result += "<td style='text-align:center'>" + (record++) + "</td>";
                result += "<td style='font-weight:bold'>" + item.Tennghe + "</td>";
                result += "<td style='text-align:center'>" + "</td>";
                result += "<td>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Chỉnh sửa'";
                result += " data-target='#Edit_Lvkd_Modal' data-toggle='modal' onclick='SetEditLvkd(`" + item.Id + "`)'>";
                result += "<i class='icon-lg la la-edit text-primary'></i>";
                result += "</button>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Xóa' data-toggle='modal'";
                result += " data-target='#Delete_Lvkd_Modal' onclick='GetDeleteLvkd(`" + item.Id + "`, `" + item.Tennghe + "`)'>";
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
