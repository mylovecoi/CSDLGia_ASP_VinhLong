using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDatDiaBan.GiaDatDiaBanCt
{
    public class GiaDatDiaBanCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaDatDiaBanCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaDatDiaBanCt/Store")]
        [HttpPost]
        public JsonResult Store(string Mahs, string Mota, string Maloaidat, string Khuvuc, string Maxp, string Loaiduong, string Diemdau, string Diemcuoi, Double Hesok, Double Giavt1, Double Giavt2, Double Giavt3, Double Giavt4, Double Giavt5)
        {
            // Tạo 1 bản nghi mới trang thái CXD nếu thêm chi tiết xong quay lại
            var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaDatDiaBanCt
            {
                Mahs = Mahs,
                //Maloaidat = Maloaidat,
                //Maxp = Maxp,
                Khuvuc = Khuvuc,
                Loaiduong = Loaiduong,
                Diemdau = Diemdau,
                Diemcuoi = Diemcuoi,
                Hesok = Hesok,
                Giavt1 = Giavt1,
                Giavt2 = Giavt2,
                Giavt3 = Giavt3,
                Giavt4 = Giavt4,
                Giavt5 = Giavt5,
                Mota = Mota,
                Trangthai = "CXD",
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };

            _db.GiaDatDiaBanCt.Add(model);
            _db.SaveChanges();
            string result = GetData(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        //Xóa 1 bản ghi phần chi tiết
        [Route("GiaDatDiaBanCt/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaDatDiaBanCt.FirstOrDefault(t => t.Id == Id);
            _db.GiaDatDiaBanCt.Remove(model);
            _db.SaveChanges();
            string result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        //Xóa tất cả phần chi tiết
        [Route("GiaDatDiaBanCt/Delete2")]
        [HttpPost]
        public JsonResult Delete2(string Mahs)
        {
            var model2 = _db.GiaDatDiaBanCt.Where(t => t.Mahs == Mahs).ToList();
            _db.GiaDatDiaBanCt.RemoveRange(model2);
            _db.SaveChanges();
            string result = GetData(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("GiaDatDiaBanCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaDatDiaBanCt.FirstOrDefault(p => p.Id == Id);


            if (model != null)
            {

                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row'>";

                //result += "<div class='col-xl-6'>";
                //result += "<div class='form-group fv-plugins-icon-container'>";
                //result += "<label>Loại đất</label>";
                //result += "<select id='maloaidat_edit' name='maloaidat_edit' class='form-control select2me select2-offscreen' tabindex='-1' title=''>";
                //var dsloaidat = _db.DmLoaiDat.ToList();
                //foreach (var item in dsloaidat)
                //{
                //    result += "<option value ='" + @item.Maloaidat + "'>" + @item.Loaidat + "</ option >";
                //}
                //result += "</select>";
                //result += "</div>";
                //result += "</div>";

                //result += "<div class='col-xl-6'>";
                //result += "<div class='form-group fv-plugins-icon-container'>";
                //result += "<label>Xã phường</label>";
                //result += "<input type='text' id='xaphuong_edit' name='xaphuong_edit' value='" + model.Maxp + "' class='form-control'>";
                //result += "</div>";
                //result += "</div>";

                result += "<div class='col-xl-10'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Khu vực tên đường phố</label>";
                result += "<input type='text' id='tenduongpho_edit' name='tenduongpho_edit' value='" + model.Khuvuc + "' class='form-control'>";
                result += "</div>";
                result += "</div>";


                result += "<div class='col-xl-1' style='padding-left: 0px;'>";
                result += "<label class='control-label'>Thêm</label>";
                result += "<button type='button' class='btn btn-default' data-target='#Dvt_Modal_edit' data-toggle='modal'><i class='la la-plus'></i>";
                result += "</button>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Điểm đầu</label>";
                result += "<input type='text' id='diemdau_edit' name='diemdau_edit' value='" + model.Diemdau + "' class='form-control'>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Điểm cuối</label>";
                result += "<input type='text' id='diemcuoi_edit' name='diemcuoi_edit' value='" + model.Diemcuoi + "' class='form-control'>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-4'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Loại đường</label>";
                result += "<input type='text' id='loaiduong_edit' name='loaiduong_edit' value='" + model.Loaiduong + "' class='form-control'>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-4'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Hệ số K</label>";
                result += "<input type='text' id='hesok_edit' name='hesok_edit' value='" + model.Hesok + "' class='form-control'>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-4'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Giá vị trí I</label>";
                result += "<input type='text' id='giavt1_edit' name='giavt1_edit' value='" + model.Giavt1 + "' class='form-control'>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-4'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Giá vị trí II</label>";
                result += "<input type='text' id='giavt2_edit' name='giavt2_edit' value='" + model.Giavt2 + "' class='form-control'>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-4'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Giá vị trí III</label>";
                result += "<input type='text' id='giavt3_edit' name='giavt3_edit' value='" + model.Giavt3 + "' class='form-control'>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-4'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Giá vị trí IV</label>";
                result += "<input type='text' id='giavt4_edit' name='giavt4_edit' value='" + model.Giavt4 + "' class='form-control'>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-4'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Giá vị trí V</label>";
                result += "<input type='text' id='giavt5_edit' name='giavt5_edit' value='" + model.Giavt5 + "' class='form-control'>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Ghi chú</label>";
                result += "<input type='text' id='ghichu_edit' name='ghichu_edit' value='" + model.Mota + "' class='form-control'>";
                result += "</div>";
                result += "</div>";

                result += "<input hidden type='text' id='id_edit' name='id_edit' value='" + model.Id + "'/>";
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

        [Route("GiaDatDiaBanCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Maloaidat, string Maxp, string Loaiduong, string Diemdau, string Diemcuoi, Double Hesok, Double Giavt1, Double Giavt2, Double Giavt3, Double Giavt4, Double Giavt5)
        {
            //Tìm id cần sửa
            //Update dữ liệu mới
            var model = _db.GiaDatDiaBanCt.FirstOrDefault(t => t.Id == Id);
            model.Id = Id;
            //model.Maloaidat = Maloaidat;
            //model.Maxp = Maxp;
            model.Loaiduong = Loaiduong;
            model.Diemdau = Diemdau;
            model.Diemcuoi = Diemcuoi;
            model.Hesok = Hesok;
            model.Giavt1 = Giavt1;
            model.Giavt2 = Giavt2;
            model.Giavt3 = Giavt3;
            model.Giavt4 = Giavt4;
            model.Giavt5 = Giavt5;
            model.Updated_at = DateTime.Now;
            _db.GiaDatDiaBanCt.Update(model);
            _db.SaveChanges();

            //Trả về kết quả với dữ liệu mới
            string result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);

        }


        [Route("GiaDatDiaBanCt/Excel")]
        [HttpPost]
        public async Task<JsonResult> Excel(string Maloaidat, string Maxp, string Mahs, string Khuvuc, string Diemdau, string Diemcuoi, double Giavt1, double Giavt2, double Giavt3, double Giavt4, double Hesok,
          int Sheet, int LineStart, int LineStop, IFormFile FormFile)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giadatdb.danhmuc", "Edit"))
                {
                    LineStart = LineStart == 0 ? 1 : LineStart;
                    var list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaDatDiaBanCt>();
                    int sheet = Sheet == 0 ? 0 : (Sheet - 1);
                    using (var stream = new MemoryStream())
                    {
                        await FormFile.CopyToAsync(stream);
                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[sheet];
                            var rowcount = worksheet.Dimension.Rows;
                            LineStop = LineStop > rowcount ? rowcount : LineStop;

                            for (int row = LineStart; row <= LineStop; row++)
                            {
                                list_add.Add(new CSDLGia_ASP.Models.Manages.DinhGia.GiaDatDiaBanCt
                                {

                                    Mahs = Mahs,
                                    Trangthai = "CXD",
                                    Created_at = DateTime.Now,
                                    Updated_at = DateTime.Now,


                                    //Maloaidat = worksheet.Cells[row, Int16.Parse(Maloaidat)].Value.ToString() != null ?
                                    //            worksheet.Cells[row, Int16.Parse(Maloaidat)].Value.ToString().Trim() : "",

                                    //Maxp = worksheet.Cells[row, Int16.Parse(Maxp)].Value.ToString() != null ?
                                    //            worksheet.Cells[row, Int16.Parse(Maxp)].Value.ToString().Trim() : "",

                                    Khuvuc = worksheet.Cells[row, Int16.Parse(Khuvuc)].Value.ToString() != null ?
                                                worksheet.Cells[row, Int16.Parse(Khuvuc)].Value.ToString().Trim() : "",

                                    Diemdau = worksheet.Cells[row, Int16.Parse(Diemdau)].Value != null ?
                                                worksheet.Cells[row, Int16.Parse(Diemdau)].Value.ToString().Trim() : "",

                                    Diemcuoi = worksheet.Cells[row, Int16.Parse(Diemcuoi)].Value != null ?
                                                worksheet.Cells[row, Int16.Parse(Diemcuoi)].Value.ToString().Trim() : "",

                                    Giavt1 = Convert.ToDouble(worksheet.Cells[row, Int16.Parse(Giavt1.ToString())].Value) != 0 ?
                                                Convert.ToDouble(worksheet.Cells[row, Int16.Parse(Giavt1.ToString())].Value) : 0,

                                    Giavt2 = Convert.ToDouble(worksheet.Cells[row, Int16.Parse(Giavt2.ToString())].Value) != 0 ?
                                                Convert.ToDouble(worksheet.Cells[row, Int16.Parse(Giavt2.ToString())].Value) : 0,

                                    Giavt3 = Convert.ToDouble(worksheet.Cells[row, Int16.Parse(Giavt3.ToString())].Value) != 0 ?
                                                Convert.ToDouble(worksheet.Cells[row, Int16.Parse(Giavt3.ToString())].Value) : 0,

                                    Giavt4 = Convert.ToDouble(worksheet.Cells[row, Int16.Parse(Giavt4.ToString())].Value) != 0 ?
                                                Convert.ToDouble(worksheet.Cells[row, Int16.Parse(Giavt4.ToString())].Value) : 0,

                                    Hesok = Convert.ToDouble(worksheet.Cells[row, Int16.Parse(Hesok.ToString())].Value) != 0 ?
                                                Convert.ToDouble(worksheet.Cells[row, Int16.Parse(Hesok.ToString())].Value) : 0,


                                });
                            }

                        }
                    }
                    _db.GiaDatDiaBanCt.AddRange(list_add);
                    _db.SaveChanges();
                    string result = GetData(Mahs);
                    var data = new { status = "success", message = result };
                    return Json(data);

                }
                else
                {
                    var data = new { status = "error", message = "Bạn không có quyền thực hiện chức năng này!!!" };
                    return Json(data);
                }
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }

        public string GetData(string Mahs)
        {
            var model = _db.GiaDatDiaBanCt.Where(t => t.Mahs == Mahs).ToList();
            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover table-responsive' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";

            result += "<th width='2%'>STT</th>";
            //result += "<th>Xã phường</th>";
            //result += "<th>Loại đất</th>";
            result += "<th>Khu vực tên đường phố</th>";
            result += "<th>Địa giới</th>";
            result += "<th>VT1</th>";
            result += "<th>VT2</th>";
            result += "<th>VT3</th>";
            result += "<th>VT4</th>";
            result += "<th>VT5</th>";
            result += "<th>Thao tác</th>";

            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";
            foreach (var item in model)
            {
                result += "<tr>";

                result += "<td style='text-align:center'>" + record++ + "</td>";
                //result += "<td style='text-align:center'>" + item.Maxp + "</td>";
                //result += "<td style='text-align:center'>" + item.Maloaidat + "</td>";
                result += "<td style='text-align:center'>" + item.Khuvuc + "</td>";
                result += "<td style='text-align:center'>" + item.Diemdau + " " + "đến" + " " + item.Diemcuoi + "</td>";
                result += "<td style='text-align:center'>" + item.Giavt1 + "</td>";
                result += "<td style='text-align:center'>" + item.Giavt2 + "</td>";
                result += "<td style='text-align:center'>" + item.Giavt3 + "</td>";
                result += "<td style='text-align:center'>" + item.Giavt4 + "</td>";
                result += "<td style='text-align:center'>" + item.Giavt5 + "</td>";

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
