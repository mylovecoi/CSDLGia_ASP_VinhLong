using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.ThamDinhGia
{
    public class ThamDinhGiaDmHhController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public ThamDinhGiaDmHhController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("ThamDinhGia/DanhMuc")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.hh", "Index"))
                {
                    var model = _db.DmNhomHh.Where(t => t.Phanloai == "THAMDINHGIA").ToList();

                    ViewData["Title"] = "Danh mục nhóm hàng hóa";
                    ViewData["MenuLv1"] = "menu_tdg";
                    ViewData["MenuLv2"] = "menu_tdg_hh";
                    return View("Views/Admin/Manages/ThamDinhGia/DmHh/Index.cshtml", model);
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

        [Route("ThamDinhGia/DanhMuc/Store")]
        [HttpPost]
        public JsonResult Store(string Tennhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.hh", "Create"))
                {
                    var request = new DmNhomHh
                    {
                        Manhom = DateTime.Now.ToString("yyMMddssmmHH"),
                        Tennhom = Tennhom,
                        Theodoi = "TD",
                        Phanloai = "THAMDINHGIA",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };

                    _db.DmNhomHh.Add(request);
                    _db.SaveChanges();

                    var data = new { status = "success", message = "Thêm mới nhóm hàng hóa thành công!" };
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

        [Route("ThamDinhGia/DanhMuc/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.hh", "Edit"))
                {
                    var model = _db.DmNhomHh.FirstOrDefault(p => p.Id == Id);
                    if (model != null)
                    {
                        string result = "<div class='modal-body' id='frm_edit'>";
                        result += "<div class='row'>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Tên nhóm*</label>";
                        result += "<input type='text' class='form-control' id='tennhom_edit' name='tennhom_edit' value='" + model.Tennhom + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "</div>";
                        result += "<input hidden class='form-control' id='id_edit' name='id_edit' value='" + model.Id + "'/>";
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

        [Route("ThamDinhGia/DanhMuc/Update")]
        [HttpPost]
        public IActionResult Update(int Id, string Tennhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.hh", "Edit"))
                {
                    var model = _db.DmNhomHh.FirstOrDefault(t => t.Id == Id);
                    model.Tennhom = Tennhom;
                    model.Updated_at = DateTime.Now;

                    _db.DmNhomHh.Update(model);
                    _db.SaveChanges();

                    var data = new { status = "success", message = "Cập nhật thành công!" };
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

        [Route("ThamDinhGia/DanhMuc/Delete")]
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.hh", "Edit"))
                {
                    var model = _db.ThamDinhGiaDmHh.FirstOrDefault(p => p.Id == Id);
                    _db.ThamDinhGiaDmHh.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "ThamDinhGiaDmHhCt", new { Manhom = model.Manhom });
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
        public IActionResult DeleteNhom(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.hh", "Edit"))
                {
                    var model = _db.DmNhomHh.FirstOrDefault(p => p.Id == Id);
                    _db.DmNhomHh.Remove(model);
                    var model_ct = _db.ThamDinhGiaDmHh.Where(p => p.Manhom == model.Manhom).ToList();
                    _db.ThamDinhGiaDmHh.RemoveRange(model_ct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "ThamDinhGiaDmHh");
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

        //Nhận Excel cũ

        [HttpGet]
        public IActionResult Excel(string Manhom)
        {
            var tennhom = _db.DmNhomHh.FirstOrDefault(t => t.Manhom == Manhom && t.Phanloai == "THAMDINHGIA").Tennhom;

            var model = _db.ThamDinhGiaDmHh.Where(t => t.Manhom == Manhom).ToList();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Danh sách hàng hóa");
            workSheet.Cells.Style.Font.Name = "Times New Roman";
            workSheet.Cells.Style.Font.Size = 12;
            workSheet.Cells[1, 1].Value = tennhom + " - Mã nhóm: " + Manhom;
            using (var r = workSheet.Cells[1, 1, 1, 8])
            {
                r.Style.Font.Size = 15;
                r.Merge = true;
                r.Style.Font.Bold = true;
                r.Style.Font.Color.SetColor(Color.Green);
                r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                r.Style.Fill.BackgroundColor.SetColor(Color.Lavender);
            }
            workSheet.Cells[2, 1].Value = "STT";
            workSheet.Cells[2, 2].Value = "Mã hàng hóa";
            workSheet.Cells[2, 3].Value = "Tên hàng hóa";
            workSheet.Cells[2, 4].Value = "Thông số kỹ thuật";
            workSheet.Cells[2, 5].Value = "Xuất xứ";
            workSheet.Cells[2, 6].Value = "Đơn vị tính";
            workSheet.Cells[2, 7].Value = "Đơn giá";
            workSheet.Cells[2, 8].Value = "Ghi chú";
            workSheet.Cells[2, 1, 2, 8].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[2, 1, 2, 8].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[2, 1, 2, 8].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[2, 1, 2, 8].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            int record_index = 3;
            foreach (var item in model)
            {
                workSheet.Cells[record_index, 1].Value = (record_index - 2).ToString();
                workSheet.Cells[record_index, 2].Value = item.Mahanghoa;
                workSheet.Cells[record_index, 3].Value = item.Tenhanghoa;
                workSheet.Cells[record_index, 4].Value = item.Thongsokt;
                workSheet.Cells[record_index, 5].Value = item.Xuatxu;
                workSheet.Cells[record_index, 6].Value = item.Dvt;
                workSheet.Cells[record_index, 7].Value = "";
                workSheet.Cells[record_index, 8].Value = "";
                workSheet.Cells[record_index, 1, record_index, 8].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[record_index, 1, record_index, 8].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[record_index, 1, record_index, 8].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[record_index, 1, record_index, 8].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                record_index++;
            }
            workSheet.Row(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(2).Style.Font.Bold = true;
            workSheet.Cells.AutoFitColumns();
            workSheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            string excelName = "DMHANGHOA-" + Manhom + ".xlsx";
            using (var stream = new MemoryStream())
            {
                excel.SaveAs(stream);
                var content = stream.ToArray();
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }

        }

        [Route("ThamDinhGia/DanhMuc/Excel")]
        public IActionResult NhanExcel(string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.lephi.danhmuc", "Create"))
                {
                    var model = new CSDLGia_ASP.ViewModels.VMImportExcel
                    {
                        LineStart = 4,
                        LineStop = 1000,
                        Sheet = 1,
                        MaNhom = Manhom,
                        TenNhom = _db.DmNhomHh.FirstOrDefault(t => t.Manhom == Manhom)?.Tennhom ?? ""
                    };

                    ViewData["Title"] = "Danh mục hàng hóa thẩm định giá";
                    ViewData["MenuLv1"] = "menu_tdg";
                    ViewData["MenuLv2"] = "menu_tdg_hh";
                    return View("Views/Admin/Manages/ThamDinhGia/DmHh/Excel.cshtml", model);

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
                            var list_add = new List<CSDLGia_ASP.Models.Manages.ThamDinhGia.ThamDinhGiaDmHh>();
                            int line = 1;
                            string maHH = DateTime.Now.ToString("yyMMddssmmHH");
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

                                list_add.Add(new CSDLGia_ASP.Models.Manages.ThamDinhGia.ThamDinhGiaDmHh
                                {
                                    Mahanghoa = maHH + "_" + (line++).ToString(),
                                    Tenhanghoa = worksheet.Cells[row, 3].Value != null ?
                                                 worksheet.Cells[row, 3].Value.ToString().Trim() : "",
                                    Thongsokt = worksheet.Cells[row, 4].Value != null ?
                                                 worksheet.Cells[row, 4].Value.ToString().Trim() : "",
                                    Xuatxu = worksheet.Cells[row, 5].Value != null ?
                                                 worksheet.Cells[row, 5].Value.ToString().Trim() : "",
                                    Dvt = worksheet.Cells[row, 6].Value != null ?
                                                 worksheet.Cells[row, 6].Value.ToString().Trim() : "",

                                    Manhom = requests.MaNhom
                                });
                                line++;
                            }
                            _db.ThamDinhGiaDmHh.AddRange(list_add);
                            _db.SaveChanges();
                        }
                    }
                }

                return RedirectToAction("Index", "ThamDinhGiaDmHhCt", new { Manhom = requests.MaNhom });
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

    }
}
