using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaKhungGiaDat
{
    public class GiaKhungGiaDatCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaKhungGiaDatCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaKhungGiaDatCt/Store")]
        [HttpPost]
        public JsonResult Store(string Mahs, string Vungkt, double Giattdb, double Giatddb
            , double Giatttd, double Giatdtd, double Giattmn, double Giatdmn)
        {
            var model = new GiaKhungGiaDatCt
            {
                Mahs = Mahs,
                Vungkt = Vungkt,
                Giattdb = Giattdb,
                Giatddb = Giatddb,
                Giatttd = Giatttd,
                Giatdtd = Giatdtd,
                Giattmn = Giattmn,
                Giatdmn = Giatdmn,
                Trangthai = "CXD",
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaKhungGiaDatCt.Add(model);
            _db.SaveChanges();

            string result = GetData(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("GiaKhungGiaDatCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaKhungGiaDatCt.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Vùng kinh tế</b></label>";
                result += "<input type='text' id='vungkt_edit' name='vungkt_edit' value='" + @model.Vungkt + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "</div>";

                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Xã đồng bằng:</b></label>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Giá tối thiểu</b></label>";
                result += "<input type='text' id='giattdb_edit' name='giattdb_edit' class='form-control money text-right' style='font-weight: bold' value='" + model.Giattdb + "'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Giá tối đa</b></label>";
                result += "<input type='text' id='giatddb_edit' name='giatddb_edit' class='form-control money text-right' style='font-weight: bold' value='" + model.Giatddb + "'/>";
                result += "</div>";
                result += "</div>";
                result += "</div>";

                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Xã trung du:</b></label>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Giá tối thiểu</b></label>";
                result += "<input type='text' id='giatttd_edit' name='giatttd_edit' class='form-control money text-right' style='font-weight: bold' value='" + model.Giatttd + "'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Giá tối đa</b></label>";
                result += "<input type='text' id='giatdtd_edit' name='giatdtd_edit' class='form-control money text-right' style='font-weight: bold' value='" + model.Giatdtd + "'/>";
                result += "</div>";
                result += "</div>";
                result += "</div>";

                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Xã miền núi:</b></label>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Giá tối thiểu</b></label>";
                result += "<input type='text' id='giattmn_edit' name='giattmn_edit' class='form-control money text-right' style='font-weight: bold' value='" + model.Giattmn + "'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Giá tối đa</b></label>";
                result += "<input type='text' id='giatdmn_edit' name='giatdmn_edit' class='form-control money text-right' style='font-weight: bold' value='" + model.Giatdmn + "'/>";
                result += "</div>";
                result += "</div>";
                result += "</div>";

                result += "<input hidden type='text' id='id_edit' name='id_edit' value='" + Id + "' class='form-control'/>";
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
        [Route("GiaKhungGiaDatCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Mahs, string Vungkt, double Giattdb, double Giatddb
            , double Giatttd, double Giatdtd, double Giattmn, double Giatdmn)
        {
            var model = _db.GiaKhungGiaDatCt.FirstOrDefault(t => t.Id == Id);
            model.Mahs = Mahs;
            model.Vungkt = Vungkt;
            model.Giatddb = Giatdtd;
            model.Giattdb = Giattdb;
            model.Giatdtd = Giatdtd;
            model.Giatttd = Giatttd;
            model.Giatdmn = Giatdmn;
            model.Giattmn = Giattmn;
            model.Updated_at = DateTime.Now;
            _db.GiaKhungGiaDatCt.Update(model);
            _db.SaveChanges();
            string result = GetData(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("GiaKhungGiaDatCt/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaKhungGiaDatCt.FirstOrDefault(t => t.Id == Id);
            _db.GiaKhungGiaDatCt.Remove(model);
            _db.SaveChanges();
            var result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        public string GetData(string Mahs)
        {
            var model = _db.GiaKhungGiaDatCt.Where(t => t.Mahs == Mahs).ToList();
            var record = 1;
            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th rowspan='2'>STT</th>";
            result += "<th rowspan='2'>Vùng kinh tế</th>";
            result += "<th coldspan='2'>Xã đồng bằng</th>";
            result += "<th coldspan='2'>Xã trung du</th>";
            result += "<th coldspan='2'>Xã miền núi</th>";
            result += "<th rowspan='2'>Thao tác</th>";
            result += "</tr>";
            result += "<tr>";
            result += "<th>Giá tối thiểu</th>";
            result += "<th>Giá tối đa</th>";
            result += "<th>Giá tối thiểu</th>";
            result += "<th>Giá tối đa</th>";
            result += "<th>Giá tối thiểu</th>";
            result += "<th>Giá tối đa</th>";
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";

            foreach (var item in model)
            {
                result += "<tr>";
                result += "<td style='text-align:center'>" + record++ + "</td>";
                result += "<td class='active'>" + item.Vungkt + "</td>";
                result += "<td style='text-align:right'>" + Helpers.ConvertDbToStr(item.Giattdb) + "</td>";
                result += "<td style='text-align:right'>" + Helpers.ConvertDbToStr(item.Giatddb) + "</td>";
                result += "<td style='text-align:right'>" + Helpers.ConvertDbToStr(item.Giatttd) + "</td>";
                result += "<td style='text-align:right'>" + Helpers.ConvertDbToStr(item.Giatdtd) + "</td>";
                result += "<td style='text-align:right'>" + Helpers.ConvertDbToStr(item.Giattmn) + "</td>";
                result += "<td style='text-align:right'>" + Helpers.ConvertDbToStr(item.Giatdmn) + "</td>";
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
            result += "</tbody></table></div>";
            return result;
        }
    }
}
