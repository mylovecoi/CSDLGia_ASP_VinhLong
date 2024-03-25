using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;
using System;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.Giarung
{
    public class GiarungDmCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiarungDmCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DanhMucGiaRungCt")]
        [HttpGet]
        public IActionResult Index(string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.r.dm", "Index"))
                {
                    var model = _db.GiaRungDmCt.Where(t => t.Manhom == Manhom);
                    ViewData["TenNhom"] = _db.GiaRungDm.FirstOrDefault(t => t.Manhom == Manhom)?.Tennhom ?? "";
                    ViewData["SapXep"] = model.Any() ? model.Max(t => t.STTSapXep) : 0;
                    ViewData["Manhom"] = Manhom;
                    ViewData["Title"] = "Danh mục giá rừng";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgr";
                    ViewData["MenuLv3"] = "menu_dgr_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaRung/DanhMuc/DanhMucCt.cshtml", model);

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


        [Route("DanhMucGiaRungCt/Store")]
        [HttpPost]
        public JsonResult Store(int STTSapXep, string STTHienThi, string MoTa,  string Manhom, string[] Style)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                string str_style = Style.Count() > 0 ? string.Join(",", Style.ToArray()) : "";
                var model = new GiaRungDmCt
                {
                    STTSapXep = STTSapXep,
                    STTHienThi = STTHienThi,
                    MoTa = MoTa,
                    Manhom = Manhom,
                    Style = str_style,                   
                };
                _db.GiaRungDmCt.Add(model);
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


        [Route("DanhMucGiaRungCt/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.GiaRungDmCt.FirstOrDefault(t => t.Id == Id);
                if (model != null)
                {
                    _db.GiaRungDmCt.Remove(model);
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

        [HttpPost("DanhMucGiaRungCt/Remove")]
        public IActionResult Remove(string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.r.dm", "Delete"))
                {
                    var model = _db.GiaRungDmCt.Where(t => t.Manhom == Manhom);
                    if (model.Any())
                    {
                        _db.GiaRungDmCt.RemoveRange(model);
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


        [Route("DanhMucGiaRungCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaRungDmCt.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                List<string> list_style = !string.IsNullOrEmpty(model.Style) ? new List<string>(model.Style.Split(',')) : new List<string>();
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row'>";
                result += "<div class='col-xl-2'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label style='font-weight:bold;color:blue'>Sắp xếp:</label>";
                result += "<input type='number' id='STTSapXep_edit' name='STTSapXep_edit' class='form-control' step='1' value='" + model.STTSapXep + "' />";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-2'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label style='font-weight:bold;color:blue'>STT hiển thị</label> ";
                result += "<input type='text' id='STTHienThi_edit' name='STTHienThi_edit' class='form-control' value='" + model.STTHienThi + "'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-8'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label style='font-weight:bold;color:blue'>Kiểu in hiển thị: </label>";
                result += "<select class='form-control select2multi' multiple='multiple' id='Style_edit' name='Style_edit' style='width:100%'>";
                result += "<option value='Chữ in đậm'" + (list_style.Contains("Chữ in đậm") ? "selected" : "") + ">Chữ in đậm</option >";
                result += "<option value='Chữ in nghiêng'" + (list_style.Contains("Chữ in nghiêng") ? "selected" : "") + ">Chữ in nghiêng</option >";
                result += "</select>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Nội dung hiển thị:</label>";
                result += "<input type='text' id='MoTa_edit' name='MoTa_edit' value='" + model.MoTa + "' class='form-control'/>";

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
        [Route("DanhMucGiaRungCt/Update")]

        [HttpPost]
        public JsonResult Update(int STTSapXep, string STTHienThi, string MoTa, int Id, string[] Style)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.GiaRungDmCt.FirstOrDefault(t => t.Id == Id);
                if (model != null)
                {
                    string str_style = Style.Count() > 0 ? string.Join(",", Style.ToArray()) : "";
                    model.STTSapXep = STTSapXep;
                    model.STTHienThi = STTHienThi;
                    model.MoTa = MoTa;
                    model.Style = str_style;

                    _db.GiaRungDmCt.Update(model);
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

        [HttpGet("DanhMucGiaRungCt/NhanExcel")]
        public IActionResult NhanExcel(string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.r.dm", "Create"))
                {
                    var model = new CSDLGia_ASP.ViewModels.VMImportExcel
                    {
                        LineStart = 5,
                        LineStop = 1000,
                        Sheet = 1,
                        MaNhom = Manhom,
                        TenNhom = _db.GiaRungDm.FirstOrDefault(t => t.Manhom == Manhom)?.Tennhom ?? ""
                    };

                    ViewData["Title"] = "Danh mục giá rừng";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgr";
                    ViewData["MenuLv3"] = "menu_dgr_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaRung/DanhMuc/Excel.cshtml", model);

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
                            var list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaRungDmCt>();
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

                                list_add.Add(new CSDLGia_ASP.Models.Manages.DinhGia.GiaRungDmCt
                                {
                                    STTSapXep = line,
                                    STTHienThi = worksheet.Cells[row, 1].Value != null ?
                                                 worksheet.Cells[row, 1].Value.ToString().Trim() : "",
                                    MoTa = worksheet.Cells[row, 2].Value != null ?
                                                 worksheet.Cells[row, 2].Value.ToString().Trim() : "",
                                    Style = strStyle.ToString(),
                                    Manhom = requests.MaNhom
                                });
                                line++;
                            }
                            _db.GiaRungDmCt.AddRange(list_add);
                            _db.SaveChanges();
                        }
                    }
                }

                return RedirectToAction("Index", "GiarungDmCt", new { Manhom = requests.MaNhom });
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }
    }
}

