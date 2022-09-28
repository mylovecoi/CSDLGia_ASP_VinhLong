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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaThueTsc
{
    public class GiaThueTSanCongCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaThueTSanCongCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DinhGiaThueTscCt/Store")]
        [HttpPost]
        public JsonResult Store(string Mahs, string Mataisan, string Dvthue, string Diachi, string Soqdpd, DateTime Thoigianpd, string Soqddg,
            DateTime Thoigiandg, string Hdthue, string Ththue, DateTime Thuetungay, DateTime Thuedenngay, string Dvt, double Dongiathue, double Sotienthuenam)
        {
            var model = new GiaThueTaiSanCongCt
            {
                Mahs = Mahs,
                Mataisan = Mataisan,
                Dvthue = Dvthue,
                Diachi = Diachi,
                Soqdpd = Soqdpd,
                Thoigianpd = Thoigianpd,
                Soqddg = Soqddg,
                Thoigiandg = Thoigiandg,
                Hdthue = Hdthue,
                Ththue = Ththue,
                Thuetungay = Thuetungay,
                Thuedenngay = Thuedenngay,
                Dvt = Dvt,
                Dongiathue = Dongiathue,
                Sotienthuenam = Sotienthuenam,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaThueTaiSanCongCt.Add(model);
            _db.SaveChanges();

            string result = GetData(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }
        [Route("DinhGiaThueTscCt/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaThueTaiSanCongCt.FirstOrDefault(t => t.Id == Id);
            _db.GiaThueTaiSanCongCt.Remove(model);
            _db.SaveChanges();
            var result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }
        public string GetData(string Mahs)
        {
            var model = _db.GiaThueTaiSanCongCt.Where(t => t.Mahs == Mahs).ToList();
            var GiaThueTaiSanCongDm = _db.GiaThueTaiSanCongDm.ToList();
            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th>STT</th>";
            result += "<th>Tên tài sản</th>";
            result += "<th> Đơn vị thuê</th>";
            result += "<th> Hợp đồng số </th>";
            result += "<th> Thờ hạn</th>";
            result += "<th>Đơn vị <br/> tính</th>";
            result += "<th> Đơn giá </th>";
            result += "<th> Thanh tiền</th>";
            result += "<th>Thao tác</th>";
            result += "</tr></thead><tbody>";

            foreach (var item in model)
            {
                result += "<tr>";
                result += "<td style='text-align:center'>" + (record++) + "</td>";
                result += "<td style='text-align:center'>";
                if (GiaThueTaiSanCongDm != null)
                {
                    foreach (var dm in GiaThueTaiSanCongDm)
                    {
                        if (item.Mataisan == dm.Mataisan)
                        {
                            result += "<span>" + dm.Tentaisan + "</span>";

                        }
                    }
                }
                result += "</td>";

                result += "<td style='text-align:center'>" + item.Dvthue + "</td>";
                result += "<td style='text-align:center'>" + item.Hdthue + "</td>";
                result += "<td style='text-align:center'>" + item.Ththue + "</td>";
                result += "<td style='text-align:center'>" + item.Dvt + "</td>";
                result += "<td style='text-align:center'>" + item.Dongiathue + "</td>";
                result += "<td style='text-align:center'>" + item.Sotienthuenam + "</td>";

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
        [Route("DinhGiaThueTscCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaThueTaiSanCongCt.FirstOrDefault(p => p.Id == Id);
            var GiaThueTaiSanCongDm = _db.GiaThueTaiSanCongDm.ToList();
            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<input type='Hidden' id='id_edit' name='id_edit' value='" + Id + "' class='form-control'/>";
                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên tài sản</b></label>";
                result += "<select class='form-control' id='Mataisan_edit' name='Mataisan_edit'>";
                if (GiaThueTaiSanCongDm != null)
                {
                    foreach (var item in GiaThueTaiSanCongDm)
                    {
                        result += "<option value='" + item.Mataisan + "' " + (item.Mataisan == model.Mataisan ? "selected" : "") + ">" + item.Tentaisan + " </option>";
                    }
                }
                result += "</select>";
                result += "</div></div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Đơn vị thuê</b></label>";
                result += "<input type='text' id='Dvthue_edit' name='Dvthue_edit' value='" + @model.Dvthue + "' class='form-control'/>";
                result += "</div></div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Địa chỉ</b></label>";
                result += "<input type='text' id='Diachi_edit' name='Diachi_edit' value='" + @model.Diachi + "' class='form-control'/>";
                result += "</div></div>";


                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Số quyết định phê duyệt</b></label>";
                result += "<input type='text' id='Soqdpd_edit' name='Soqdpd_edit' value='" + @model.Soqdpd + "' class='form-control'/>";
                result += "</div></div>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Thời điểm phê duyệt</b></label>";
                result += "<input type='date' id='Thoigianpd_edit' name='Thoigianpd_edit' value='" + @model.Thoigianpd + "' class='form-control'/>";
                result += "</div></div>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Số quyết định đấu giá</b></label>";
                result += "<input type='text' id='Soqddg_edit' name='Soqddg_edit' value='" + @model.Soqddg + "' class='form-control'/>";
                result += "</div></div>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Thời điểm đấu giá</b></label>";
                result += "<input type='date' id='Thoigiandg_edit' name='Thoigiandg_edit' value='" + @model.Thoigiandg + "' class='form-control'/>";
                result += "</div></div>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Hợp đồng số</b></label>";
                result += "<input type='text' id='Hdthue_edit' name='Hdthue_edit' value='" + @model.Hdthue + "' class='form-control'/>";
                result += "</div></div>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Thời hạn thuê</b></label>";
                result += "<input type='text' id='Ththue_edit' name='Ththue_edit' value='" + @model.Ththue + "' class='form-control'/>";
                result += "</div></div>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Thuê từ ngày</b></label>";
                result += "<input type='date' id='Thuetungay_edit' name='Thuetungay_edit' value='" + @model.Thuetungay + "' class='form-control'/>";
                result += "</div></div>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Thuê đến ngày</b></label>";
                result += "<input type='date' id='Thuedenngay_edit' name='Thuedenngay_edit' value='" + @model.Thuedenngay + "' class='form-control'/>";
                result += "</div></div>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Đơn vị tính</b></label>";
                result += "<input type='text' id='Dvt_edit' name='Dvt_edit' value='" + @model.Dvt + "' class='form-control'/>";
                result += "</div></div>";
                result += "<div class='col-xl-3'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Đơn giá thuê</b></label>";
                result += "<input type='text' id='Dongiathue_edit' name='Dongiathue_edit' value='" + @model.Dongiathue + "' class='form-control money text-right' style = 'font-weight: bold'/>";
                result += "</div></div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Thành tiền</b></label>";
                result += "<input type='text' id='Sotienthuenam_edit' name='Sotienthuenam_edit' value='" + @model.Sotienthuenam + "' class='form-control money text-right' style = 'font-weight: bold'/>";
                result += "</div></div>";
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
        [Route("DinhGiaThueTscCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Mataisan, string Dvthue, string Diachi, string Soqdpd, DateTime Thoigianpd, string Soqddg,
            DateTime Thoigiandg, string Hdthue, string Ththue, DateTime Thuetungay, DateTime Thuedenngay, string Dvt, double Dongiathue, double Sotienthuenam)
        {
            var model = _db.GiaThueTaiSanCongCt.FirstOrDefault(t => t.Id == Id);
            model.Mataisan = Mataisan;
            model.Dvthue = Dvthue;
            model.Diachi = Diachi;
            model.Soqdpd = Soqdpd;
            model.Thoigianpd = Thoigianpd;
            model.Soqddg = Soqddg;
            model.Thoigiandg = Thoigiandg;
            model.Hdthue = Hdthue;
            model.Ththue = Ththue;
            model.Thuetungay = Thuetungay;
            model.Thuedenngay = Thuedenngay;
            model.Dvt = Dvt;
            model.Dongiathue = Dongiathue;
            model.Sotienthuenam = Sotienthuenam;
            model.Updated_at = DateTime.Now;
            _db.GiaThueTaiSanCongCt.Update(model);
            _db.SaveChanges();
            string result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }
    }
}
