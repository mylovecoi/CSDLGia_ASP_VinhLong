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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.Giarung
{
    public class GiarungCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiarungCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaRungCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaRungCt.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<input hidden type='text' id='id_edit' name='id_edit' value='" + model.Id + "'/>";


                result += "<label style='color: blue; text-decoration-line: underline; font-weight: bold'>Giá rừng đặc dụng</label>";
                result += "<div class='row'>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tối thiểu</b></label>";
                result += "<input type='text' id='GiaRung1_edit' name='GiaRung1_edit' value='" + Helpers.ConvertDbToStr(model.GiaRung1) + "' class='form-control money-decimal-mask'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tối đa</b></label>";
                result += "<input type='text' id='GiaRung2_edit' name='GiaRung2_edit' value='" + Helpers.ConvertDbToStr(model.GiaRung2) + "' class='form-control money-decimal-mask'/>";
                result += "</div>";
                result += "</div>";
                result += "</div>";

                result += "<label style='color: blue; text-decoration-line: underline; font-weight: bold'>Giá rừng phòng hộ</label>";
                result += "<div class='row'>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tối thiểu</b></label>";
                result += "<input type='text' id='GiaRung3_edit' name='GiaRung3_edit' value='" + Helpers.ConvertDbToStr(model.GiaRung3) + "' class='form-control money-decimal-mask'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tối đa</b></label>";
                result += "<input type='text' id='GiaRung4_edit' name='GiaRung4_edit' value='" + Helpers.ConvertDbToStr(model.GiaRung4) + "' class='form-control money-decimal-mask'/>";
                result += "</div>";
                result += "</div>";
                result += "</div>";

                result += "<label style='color: blue; text-decoration-line: underline; font-weight: bold'>Giá rừng sản xuất</label>";
                result += "<div class='row'>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tối thiểu</b></label>";
                result += "<input type='text' id='GiaRung5_edit' name='GiaRung5_edit' value='" + Helpers.ConvertDbToStr(model.GiaRung5) + "' class='form-control money-decimal-mask'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tối đa</b></label>";
                result += "<input type='text' id='GiaRung6_edit' name='GiaRung6_edit' value='" + Helpers.ConvertDbToStr(model.GiaRung6) + "' class='form-control money-decimal-mask'/>";
                result += "</div>";
                result += "</div>";
                result += "</div>";

                result += "<label style='color: blue; text-decoration-line: underline; font-weight: bold'>Giá cho thuê</b></label>";
                result += "<div class='row'>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tối thiểu</b></label>";
                result += "<input type='text' id='GiaChoThue1_edit' name='GiaChoThue1_edit' value='" + Helpers.ConvertDbToStr(model.GiaChoThue1) + "' class='form-control money-decimal-mask'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tối đa</b></label>";
                result += "<input type='text' id='GiaChoThue2_edit' name='GiaChoThue2_edit' value='" + Helpers.ConvertDbToStr(model.GiaChoThue2) + "' class='form-control money-decimal-mask'/>";
                result += "</div>";
                result += "</div>";
                result += "</div>";


                result += "<label style='color: blue; text-decoration-line: underline; font-weight: bold'>Giá bồi thường rừng đặc dụng</b></label>";
                result += "<div class='row'>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tối thiểu</b></label>";
                result += "<input type='text' id='GiaBoiThuong1_edit' name='GiaBoiThuong1_edit' value='" + Helpers.ConvertDbToStr(model.GiaBoiThuong1) + "' class='form-control money-decimal-mask'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tối đa</b></label>";
                result += "<input type='text' id='GiaBoiThuong2_edit' name='GiaBoiThuong2_edit' value='" + Helpers.ConvertDbToStr(model.GiaBoiThuong2) + "' class='form-control money-decimal-mask'/>";
                result += "</div>";
                result += "</div>";
                result += "</div>";

                result += "<label style='color: blue; text-decoration-line: underline; font-weight: bold'>Giá bồi thường rừng phòng hộ</b></label>";
                result += "<div class='row'>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tối thiểu</b></label>";
                result += "<input type='text' id='GiaBoiThuong3_edit' name='GiaBoiThuong3_edit' value='" + Helpers.ConvertDbToStr(model.GiaBoiThuong3) + "' class='form-control money-decimal-mask'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tối đa</b></label>";
                result += "<input type='text' id='GiaBoiThuong4_edit' name='GiaBoiThuong4_edit' value='" + Helpers.ConvertDbToStr(model.GiaBoiThuong4) + "' class='form-control money-decimal-mask'/>";
                result += "</div>";
                result += "</div>";
                result += "</div>";

                result += "<label style='color: blue; text-decoration-line: underline; font-weight: bold'>Giá bồi thường rừng sản xuất</b></label>";
                result += "<div class='row'>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tối thiểu</b></label>";
                result += "<input type='text' id='GiaBoiThuong5_edit' name='GiaBoiThuong5_edit' value='" + Helpers.ConvertDbToStr(model.GiaBoiThuong5) + "' class='form-control money-decimal-mask'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tối đa</b></label>";
                result += "<input type='text' id='GiaBoiThuong6_edit' name='GiaBoiThuong6_edit' value='" + Helpers.ConvertDbToStr(model.GiaBoiThuong6) + "' class='form-control money-decimal-mask'/>";
                result += "</div>";
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
        public JsonResult Update(int Id, string Mahs, double GiaRung1, double GiaRung2, double GiaRung3, double GiaRung4, double GiaRung5, double GiaRung6,
                                double GiaChoThue1, double GiaChoThue2, double GiaBoiThuong1, double GiaBoiThuong2, double GiaBoiThuong3, double GiaBoiThuong4,
                                double GiaBoiThuong5, double GiaBoiThuong6)
        {
            var model = _db.GiaRungCt.FirstOrDefault(t => t.Id == Id);
            model.GiaRung1 = GiaRung1;
            model.GiaRung2 = GiaRung2;
            model.GiaRung3 = GiaRung3;
            model.GiaRung4 = GiaRung4;
            model.GiaRung5 = GiaRung5;
            model.GiaRung6 = GiaRung6;
            model.GiaChoThue1 = GiaChoThue1;
            model.GiaChoThue2 = GiaChoThue2;
            model.GiaBoiThuong1 = GiaBoiThuong1;
            model.GiaBoiThuong2 = GiaBoiThuong2;
            model.GiaBoiThuong3 = GiaBoiThuong3;
            model.GiaBoiThuong4 = GiaBoiThuong4;
            model.GiaBoiThuong5 = GiaBoiThuong5;
            model.GiaBoiThuong6 = GiaBoiThuong6;
            model.Updated_at = DateTime.Now;
            _db.GiaRungCt.Update(model);
            _db.SaveChanges();
            string result = GetData(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }


        public string GetData(string Mahs)
        {
            var model = _db.GiaRungCt.Where(t => t.Mahs == Mahs).ToList();
            var NhomDm = _db.GiaRungDm;
            string result = "<div class='card-body' id='frm_data'>";
            foreach (var nhom in NhomDm)
            {
                var data_nhom = model.Where(t => t.Manhom == nhom.Manhom);
                if (data_nhom.Any())
                {
                    result += "<p style='text-align:center; font-size:16px; text-transform:uppercase; font-weight:bold'>" + nhom.Tennhom + "</p>";
                    result += "<table class='table table-striped table-bordered table-hover class-nosort'>";
                    result += "<thead>";
                    result += "<tr>";
                    result += "<th rowspan='3' width='2%' style='text-align:center'>TT</th> ";
                    result += "<th rowspan='3' style='text-align:center'>Mô tả</th>";
                    result += "<th colspan='6' width='30%'style='text-align:center'>Giá rừng</th> ";
                    result += "<th colspan='2' rowspan='2' width='10%' style='text-align:center'>Giá cho thuê quyền sử dụng rừng/năm</th> ";
                    result += "<th colspan='6' width='30%' style='text-align:center'>Giá bồi thường thiệt hại đối với rừng tự nhiên</th> ";
                    result += "<td rowspan='3' width='5%' style='text-align:center'>Thao tác</td> ";
                    result += "</tr>";
                    result += "<tr>";
                    result += "<td colspan='2' style='text-align:center'>Đặc dụng</td>";
                    result += "<td colspan='2' style='text-align:center'>Phòng hộ</td>";
                    result += "<td colspan='2' style='text-align:center'>Sản xuất</td>";
                    result += "<td colspan='2' style='text-align:center'>Đặc dụng</td>";
                    result += "<td colspan='2' style='text-align:center'>Phòng hộ</td>";
                    result += "<td colspan='2' style='text-align:center'>Sản xuất</td>";
                    result += "</tr>";
                    result += "<tr>";
                    result += "<td style='text-align:center'>Tối thiểu</td>";
                    result += "<td style='text-align:center'>Tối đa</td>";
                    result += "<td style='text-align:center'>Tối thiểu</td>";
                    result += "<td style='text-align:center'>Tối đa</td>";
                    result += "<td style='text-align:center'>Tối thiểu</td>";
                    result += "<td style='text-align:center'>Tối đa</td>";
                    result += "<td style='text-align:center'>Tối thiểu</td>";
                    result += "<td style='text-align:center'>Tối đa</td>";
                    result += "<td style='text-align:center'>Tối thiểu</td>";
                    result += "<td style='text-align:center'>Tối đa</td>";
                    result += "<td style='text-align:center'>Tối thiểu</td>";
                    result += "<td style='text-align:center'>Tối đa</td>";
                    result += "<td style='text-align:center'>Tối thiểu</td>";
                    result += "<td style='text-align:center'>Tối đa</td>";
                    result += "</tr>";
                    result += "</thead>";
                    result += "<tbody>";
                    foreach (var item in data_nhom.OrderBy(t => t.STTSapXep))
                    {
                        result += "<tr>";
                        result += "<td style='text-align:center'>" + item.STTHienThi + "</td>";
                        result += "<td style='text-align:left'>" + item.MoTa + "</td>";
                        result += "<td style='text-align:center'>" + Helpers.ConvertDbToStr(item.GiaRung1) + "</td>";
                        result += "<td style='text-align:center'>" + Helpers.ConvertDbToStr(item.GiaRung2) + "</td>";
                        result += "<td style='text-align:center'>" + Helpers.ConvertDbToStr(item.GiaRung3) + "</td>";
                        result += "<td style='text-align:center'>" + Helpers.ConvertDbToStr(item.GiaRung4) + "</td>";
                        result += "<td style='text-align:center'>" + Helpers.ConvertDbToStr(item.GiaRung5) + "</td>";
                        result += "<td style='text-align:center'>" + Helpers.ConvertDbToStr(item.GiaRung6) + "</td>";
                        result += "<td style='text-align:center'>" + Helpers.ConvertDbToStr(item.GiaChoThue1) + "</td>";
                        result += "<td style='text-align:center'>" + Helpers.ConvertDbToStr(item.GiaChoThue2) + "</td>";
                        result += "<td style='text-align:center'>" + Helpers.ConvertDbToStr(item.GiaBoiThuong1)  + "</td>";
                        result += "<td style='text-align:center'>" + Helpers.ConvertDbToStr(item.GiaBoiThuong2) + "</td>";
                        result += "<td style='text-align:center'>" + Helpers.ConvertDbToStr(item.GiaBoiThuong3) + "</td>";
                        result += "<td style='text-align:center'>" + Helpers.ConvertDbToStr(item.GiaBoiThuong4) + "</td>";
                        result += "<td style='text-align:center'>" + Helpers.ConvertDbToStr(item.GiaBoiThuong5) + "</td>";
                        result += "<td style='text-align:center'>" + Helpers.ConvertDbToStr(item.GiaBoiThuong6) + "</td>";
                        result += "<td style='text-align:center'>";
                        result += "<button type='button' data-target='#Edit_Modal' data-toggle='modal'";
                        result += " onclick='SetEdit(`" + item.Id + "`)' class='btn btn-sm btn-clean btn-icon' title='Chỉnh sửa'>";
                        result += "<i class='icon-lg la la-edit text-primary'></i>";
                        result += "</button>";
                        result += "</td>";
                        result += "</tr>";
                    }
                    result += "</tbody>";
                    result += "</table>";
                }
            }
            result += "</div>";


            return result;
        }
    }
}
