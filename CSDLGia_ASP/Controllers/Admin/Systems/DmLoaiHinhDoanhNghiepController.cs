﻿using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class DmLoaiHinhDoanhNghiepController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public DmLoaiHinhDoanhNghiepController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [HttpGet("DmLoaiHinhDoanhNghiep")]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmloaihinhdoanhnghiep", "Index"))
                {
                    var model = _db.DmLoaiHinhDoanhNghiep;
                    int max_sttsapxep = model.Any() ? model.Max(t => t.STTSapxep) : 1;
                    ViewData["Title"] = "Danh mục loại hình doanh nghiệp";
                    ViewData["SapXep"] = max_sttsapxep;
                    
                    ViewData["MenuLv1"] = "menu_qtdanhmuc";
                    ViewData["MenuLv2"] = "menu_dmloaihinhdoanhnghiep";
                    return View("~/Views/Admin/Systems/DmLoaiHinhDoanhNghiep/Index.cshtml", model);
                }
                else
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

        [HttpPost("DmLoaiHinhDoanhNghiep/Store")]
        public JsonResult Store(string STTHienthi, int STTSapxep, string[] Style, string MaLoaiHinhDoanhNghiep, string TenLoaiHinhDoanhNghiep)
        {
            string str_style = Style.Count() > 0 ? string.Join(",", Style.ToArray()) : "";
            var model = new DmLoaiHinhDoanhNghiep
            {
                STTHienthi = STTHienthi,
                STTSapxep = STTSapxep,
                Style = str_style,
                MaLoaiHinhDoanhNghiep = MaLoaiHinhDoanhNghiep,
                TenLoaiHinhDoanhNghiep = TenLoaiHinhDoanhNghiep,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.DmLoaiHinhDoanhNghiep.Add(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }

        [HttpPost("DmLoaiHinhDoanhNghiep/Delete")]
        public JsonResult Delete(int Id)
        {
            var model = _db.DmLoaiHinhDoanhNghiep.FirstOrDefault(t => t.Id == Id);
            _db.DmLoaiHinhDoanhNghiep.Remove(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }

        [HttpPost("DmLoaiHinhDoanhNghiep/Edit")]
        public JsonResult Edit(int Id)
        {
            var model = _db.DmLoaiHinhDoanhNghiep.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                List<string> list_style = !string.IsNullOrEmpty(model.Style) ? new List<string>(model.Style.Split(',')) : new List<string>();
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row'>";
                result += "<div class='col-xl-2'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label style='font-weight:bold;color:blue'>Sắp xếp:</label>";
                result += "<input type='number' id='sapxep_edit' name='sapxep_edit' class='form-control' step='1' value='" + model.STTSapxep + "' />";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-2'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label style='font-weight:bold;color:blue'>STT hiển thị</label> ";
                result += "<input type='text' id='stt_edit' name='stt_edit' class='form-control' value='" + model.STTHienthi + "'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label style='font-weight:bold;color:blue'>Kiểu in hiển thị: </label>";
                result += "<select class='form-control select2multi' multiple='multiple' id='style_edit' name='style_edit' style='width:100%'>";
                result += "<option value='Chữ in hoa'" + (list_style.Contains("Chữ in hoa") ? "selected" : "") + ">Chữ in hoa</option >";
                result += "<option value='Chữ in đậm'" + (list_style.Contains("Chữ in đậm") ? "selected" : "") + ">Chữ in đậm</option >";
                result += "<option value='Chữ in nghiêng'" + (list_style.Contains("Chữ in nghiêng") ? "selected" : "") + ">Chữ in nghiêng</option >";
                result += "</select>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-12'>"; // Chỉnh sửa phần chứa mã hàng hóa
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Mã loại hình doanh nghiệp</label>";
                result += "<input type='text' id='maloaihinhdoanhnghiep_edit' name='maloaihinhdoanhnghiep_edit' value='" + model.MaLoaiHinhDoanhNghiep + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-12'>"; // Chỉnh sửa phần chứa tên hàng hóa
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Tên loại hình doanh nghiệp</label>";
                result += "<input type='text' id='tenloaihinhdoanhnghiep_edit' name='tenloaihinhdoanhnghiep_edit' value='" + model.TenLoaiHinhDoanhNghiep + "' class='form-control'/>";
                result += "</div>"; // Đảm bảo đóng đúng các thẻ
                result += "</div>";

                result += "<input type='hidden' id='id_edit' name='id_edit' value='" + Id + "' class='form-control'/>";

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

        [HttpPost("DmLoaiHinhDoanhNghiep/Update")]
        public JsonResult Update(int Id, string STTHienthi, int STTSapxep, string[] Style, string MaLoaiHinhDoanhNghiep, string TenLoaiHinhDoanhNghiep)
        {
            var model = _db.DmLoaiHinhDoanhNghiep.FirstOrDefault(t => t.Id == Id);
            string str_style = Style.Count() > 0 ? string.Join(",", Style.ToArray()) : "";
            model.STTSapxep = STTSapxep;
            model.STTHienthi = STTHienthi;
            model.Style = str_style;
            model.MaLoaiHinhDoanhNghiep = MaLoaiHinhDoanhNghiep;
            model.TenLoaiHinhDoanhNghiep = TenLoaiHinhDoanhNghiep;

            model.Updated_at = DateTime.Now;
            _db.DmLoaiHinhDoanhNghiep.Update(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }

        [HttpPost("DmLoaiHinhDoanhNghiep/Remove")]
        public JsonResult Remove()
        {
            var model = _db.DmLoaiHinhDoanhNghiep;
         
            _db.DmLoaiHinhDoanhNghiep.RemoveRange(model);
            _db.SaveChanges();

            var data = new { status = "success" };
            return Json(data);
        }

        [HttpGet("DmLoaiHinhDoanhNghiep/NhanExcel")]
        public IActionResult NhanExcel()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmloaihinhdoanhnghiep", "Index"))
                {
                    var model = new VMImportExcel()
                    {
                        Sheet = 1,
                        LineStart = 4,
                        LineStop = 1000
                    };
                    ViewData["Title"] = "Danh mục loại hình doanh nghiệp";
                    
                    ViewData["MenuLv1"] = "menu_qtdanhmuc";
                    ViewData["MenuLv2"] = "menu_dmloaihinhdoanhnghiep";
                    return View("~/Views/Admin/Systems/DmLoaiHinhDoanhNghiep/Excel.cshtml", model);
                }
                else
                {
                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                    return View("Views/Admin/Error/Page.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ImportExcel(VMImportExcel requests)
        {
            requests.LineStart = requests.LineStart == 0 ? 1 : requests.LineStart;
            int sheet = requests.Sheet == 0 ? 0 : (requests.Sheet - 1);
            string Mahs = requests.MaDv + "_" + DateTime.Now.ToString("yyMMddssmmHH");
            using (var stream = new MemoryStream())
            {
                await requests.FormFile.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[sheet];
                    var rowcount = worksheet.Dimension.Rows;
                    requests.LineStop = requests.LineStop > rowcount ? rowcount : requests.LineStop;
                    Regex trimmer = new Regex(@"\s\s+"); // Xóa khoảng trắng thừa trong chuỗi
                    var list_add = new List<CSDLGia_ASP.Models.Systems.DmLoaiHinhDoanhNghiep>();
                    int line = 1;
                    for (int row = requests.LineStart; row <= requests.LineStop; row++)
                    {
                        ExcelStyle style = worksheet.Cells[row, 2].Style;
                        // Kiểm tra xem font chữ có được đánh dấu là đậm không
                        bool isBold = style.Font.Bold;
                        // Kiểm tra xem font chữ có được đánh dấu là nghiêng không
                        bool isItalic = style.Font.Italic;
                        StringBuilder strStyle = new StringBuilder();
                        if (isBold) { strStyle.Append("Chữ in đậm,"); }
                        if (isItalic) { strStyle.Append("Chữ in nghiêng,"); }

                        list_add.Add(new CSDLGia_ASP.Models.Systems.DmLoaiHinhDoanhNghiep
                        {                          
                            STTSapxep = line,
                            STTHienthi = worksheet.Cells[row, 1].Value != null ?
                                        worksheet.Cells[row, 1].Value.ToString().Trim() : "",
                            MaLoaiHinhDoanhNghiep = worksheet.Cells[row, 2].Value != null ?
                                        worksheet.Cells[row, 2].Value.ToString().Trim() : "",
                            TenLoaiHinhDoanhNghiep = worksheet.Cells[row, 3].Value != null ?
                                        worksheet.Cells[row, 3].Value.ToString().Trim() : "",

                            Style = strStyle.ToString()
                        });
                        line = line++;
                    }
                    _db.DmLoaiHinhDoanhNghiep.AddRange(list_add);
                    _db.SaveChanges();                    
                }
            }
            return RedirectToAction("Index", "DmLoaiHinhDoanhNghiep");
        }
    }
}