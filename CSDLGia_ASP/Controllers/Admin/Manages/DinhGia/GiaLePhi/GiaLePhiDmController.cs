using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CSDLGia_ASP.Database;
using System.Security.Cryptography;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CSDLGia_ASP.Models.Systems;
using OfficeOpenXml;
using System.Text.RegularExpressions;
using OfficeOpenXml.Style;
using System.Text;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaLePhi
{
    public class GiaLePhiDmController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaLePhiDmController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DanhMucLePhi")]
        [HttpGet]
        public IActionResult Index(string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.lephi.danhmuc", "Index"))
                {
                    var model = _db.GiaPhiLePhiDm.Where(t => t.Manhom == Manhom);
                    ViewData["TenNhom"] = _db.GiaPhiLePhiNhom.FirstOrDefault(t => t.Manhom == Manhom)?.Tennhom ?? "";
                    ViewData["SapXep"] = model.Any() ? model.Max(t => t.Stt) : 0;
                    ViewData["Manhom"] = Manhom;
                    ViewData["Title"] = "Danh mục giá lệ phí trước bạ";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_dglp";
                    ViewData["MenuLv3"] = "menu_dglp_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaLePhi/DanhMuc/DanhMuc.cshtml", model);

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


        [Route("DanhMucLePhi/Store")]
        [HttpPost]
        public JsonResult Store(int Stt, string HienThi, string SttHienthi, string Manhom, string[] Style)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                string str_style = Style.Count() > 0 ? string.Join(",", Style.ToArray()) : "";
                var model = new GiaPhiLePhiDm
                {
                    Stt = Stt,
                    HienThi = HienThi,
                    SttHienthi = SttHienthi,
                    Manhom = Manhom,
                    Style = str_style,
                    Created_at = DateTime.Now,
                    Updated_at = DateTime.Now,
                };
                _db.GiaPhiLePhiDm.Add(model);
                _db.SaveChanges();

                var data = new { status = "success", message = "Thành công" };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }


        [Route("DanhMucLePhi/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.GiaPhiLePhiDm.FirstOrDefault(t => t.Id == Id);
                if (model != null)
                {
                    _db.GiaPhiLePhiDm.Remove(model);
                    _db.SaveChanges();

                    var data = new { status = "success", message = "Thành công" };
                    return Json(data);
                }
                else
                {
                    var data = new { status = "error", message = "Không tìm thấy thông tin cập nhật! Bạn cần kiểm tra lại" };
                    return Json(data);
                }
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }

        [HttpPost("DanhMucLePhi/Remove")]
        public IActionResult Remove(string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.lephi.thongtin", "Delete"))
                {
                    var model = _db.GiaPhiLePhiDm.Where(t => t.Manhom == Manhom);
                    if (model.Any())
                    {
                        _db.GiaPhiLePhiDm.RemoveRange(model);
                        _db.SaveChanges();
                        var data = new { status = "success", message = "Thành công" };
                        return Json(data);
                    }
                    else
                    {
                        ViewData["Messages"] = "Không tìm thấy thông tin!";
                        return View("Views/Admin/Error/Page.cshtml");
                    }
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


        [Route("DanhMucLePhi/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaPhiLePhiDm.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                List<string> list_style = !string.IsNullOrEmpty(model.Style) ? new List<string>(model.Style.Split(',')) : new List<string>();
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row'>";
                result += "<div class='col-xl-2'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label style='font-weight:bold;color:blue'>Sắp xếp:</label>";
                result += "<input type='number' id='stt_edit' name='stt_edit' class='form-control' step='1' value='" + model.Stt + "' />";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-2'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label style='font-weight:bold;color:blue'>STT hiển thị</label> ";
                result += "<input type='text' id='stthienthi_edit' name='stthienthi_edit' class='form-control' value='" + model.SttHienthi + "'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-8'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label style='font-weight:bold;color:blue'>Kiểu in hiển thị: </label>";
                result += "<select class='form-control select2multi' multiple='multiple' id='style_edit' name='style_edit' style='width:100%'>";
                result += "<option value='Chữ in đậm'" + (list_style.Contains("Chữ in đậm") ? "selected" : "") + ">Chữ in đậm</option >";
                result += "<option value='Chữ in nghiêng'" + (list_style.Contains("Chữ in nghiêng") ? "selected" : "") + ">Chữ in nghiêng</option >";
                result += "</select>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Nội dung hiển thị:</label>";
                result += "<input type='text' id='hienthi_edit' name='hienthi_edit' value='" + model.HienThi + "' class='form-control'/>";

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
        [Route("DanhMucLePhi/Update")]

        [HttpPost]
        public JsonResult Update(int Stt, string HienThi, string SttHienthi, int Id, string[] Style)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.GiaPhiLePhiDm.FirstOrDefault(t => t.Id == Id);
                if (model != null)
                {
                    string str_style = Style.Count() > 0 ? string.Join(",", Style.ToArray()) : "";
                    model.Stt = Stt;
                    model.HienThi = HienThi;
                    model.SttHienthi = SttHienthi;
                    model.Style = str_style;

                    _db.GiaPhiLePhiDm.Update(model);
                    _db.SaveChanges();

                    var data = new { status = "success", message = "Thành công" };
                    return Json(data);
                }
                else
                {
                    var data = new { status = "error", message = "Không tìm thấy thông tin cập nhật! Bạn cần kiểm tra lại" };
                    return Json(data);
                }
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }

        [HttpGet("DanhMucPhiLePhi/NhanExcel")]
        public IActionResult NhanExcel(string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.lephi.danhmuc", "Create"))
                {
                    var model = new CSDLGia_ASP.ViewModels.VMImportExcel
                    {
                        LineStart = 2,
                        LineStop = 1000,
                        Sheet = 1,
                        MaNhom = Manhom,
                        TenNhom = _db.GiaPhiLePhiNhom.FirstOrDefault(t => t.Manhom == Manhom)?.Tennhom ?? ""
                    };

                    ViewData["Title"] = "Danh mục giá lệ phí trước bạ";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_dglp";
                    ViewData["MenuLv3"] = "menu_dglp_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaLePhi/DanhMuc/Excel.cshtml", model);

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
        public async Task<IActionResult> ImportExcel(CSDLGia_ASP.ViewModels.VMImportExcel requests)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                requests.LineStart = requests.LineStart == 0 ? 1 : requests.LineStart;
                int sheet = requests.Sheet == 0 ? 0 : (requests.Sheet - 1);

                using (var stream = new MemoryStream())
                {
                    await requests.FormFile.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[sheet];
                        if (worksheet != null)
                        {
                            int rowcount = worksheet.Dimension.Rows;
                            requests.LineStop = requests.LineStop > rowcount ? rowcount : requests.LineStop;
                            Regex trimmer = new Regex(@"\s\s+"); // Xóa khoảng trắng thừa trong chuỗi
                            var list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaPhiLePhiDm>();
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

                                list_add.Add(new CSDLGia_ASP.Models.Manages.DinhGia.GiaPhiLePhiDm
                                {
                                    Stt = line,
                                    SttHienthi = worksheet.Cells[row, 1].Value != null ?
                                                 worksheet.Cells[row, 1].Value.ToString().Trim() : "",
                                    HienThi = worksheet.Cells[row, 2].Value != null ?
                                                 worksheet.Cells[row, 2].Value.ToString().Trim() : "",
                                    Style = strStyle.ToString(),
                                    Manhom = requests.MaNhom
                                });
                                line++;
                            }
                            _db.GiaPhiLePhiDm.AddRange(list_add);
                            _db.SaveChanges();
                        }
                    }
                }

                return RedirectToAction("Index", "GiaLePhiDm", new { Manhom = requests.MaNhom });
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }
    }
}
