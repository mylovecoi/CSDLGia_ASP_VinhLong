using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaThueTaiNguyen
{
    public class GiaThueTaiNguyenDmController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _env;

        public GiaThueTaiNguyenDmController(CSDLGiaDBContext db, IWebHostEnvironment hostingEnv)
        {
            _db = db;
            _env = hostingEnv;
        }

        [Route("GiaThueTaiNguyenDmCt")]
        [HttpGet]
        public IActionResult Index(string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetn.danhmuc", "Index"))
                {
                    var model = _db.GiaThueTaiNguyenDm.Where(t => t.Manhom == Manhom).ToList();

                    ViewData["Manhom"] = Manhom;
                    ViewData["Tennhom"] = _db.GiaThueTaiNguyenNhom.FirstOrDefault(t => t.Manhom == Manhom).Tennhom;
                    ViewData["Title"] = "Thông tin chi tiết mặt hàng thuế tài nguyên";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgthuetn";
                    ViewData["MenuLv3"] = "menu_dgthuetn_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaThueTaiNguyen/DanhMuc/ChiTiet/Index.cshtml", model);
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

        [Route("GiaThueTaiNguyenDmCt/Store")]
        [HttpPost]
        public JsonResult Store(string Manhom, string Level, string Cap1, string Cap2, string Cap3, string Cap4, string Cap5, string Dvt, string Tennhom, string Theodoi)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetn.danhmuc", "Create"))
                {
                    var request = new GiaThueTaiNguyenDm
                    {
                        Manhom = Manhom,
                        Level = Level,
                        Cap1 = Cap1,
                        Cap2 = Cap2,
                        Cap3 = Cap3,
                        Cap4 = Cap4,
                        Cap5 = Cap5,
                        Dvt = Dvt,
                        Ten = Tennhom,
                        Theodoi = Theodoi,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaThueTaiNguyenDm.Add(request);
                    _db.SaveChanges();

                    var data = new { status = "success", message = "Thêm mới thành công!" };
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

        [Route("GiaThueTaiNguyenDmCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetn.danhmuc", "Edit"))
                {
                    var model = _db.GiaThueTaiNguyenDm.FirstOrDefault(p => p.Id == Id);
                    if (model != null)
                    {
                        string result = "<div class='row' id='edit_thongtin'>";
                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Level</label>";
                        result += "<select id='level_edit' name='level_edit' class='form-control'>";
                        for (var i = 1; i <= 5; i++)
                        {
                            result += "<option value='" + i + "' " + ((string)model.Level == i.ToString() ? "selected" : "") + ">" + i + "</option>";
                        }
                        result += "</select>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Mã cấp I*</label>";
                        result += "<input type='text' id='macap1_edit' name='macap1_edit' class='form-control' value='" + model.Cap1 + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Mã cấp II*</label>";
                        result += "<input type='text' id='macap2_edit' name='macap2_edit' class='form-control' value='" + model.Cap2 + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Mã cấp III*</label>";
                        result += "<input type='text' id='macap3_edit' name='macap3_edit' class='form-control' value='" + model.Cap3 + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Mã cấp IV*</label>";
                        result += "<input type='text' id='macap4_edit' name='macap4_edit' class='form-control' value='" + model.Cap4 + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Mã cấp V*</label>";
                        result += "<input type='text' id='macap5_edit' name='macap5_edit' class='form-control' value='" + model.Cap5 + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-6'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Đơn vị tính*</label>";
                        result += "<input type='text' id='dvt_edit' name='dvt_edit' class='form-control' value='" + model.Dvt + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-6'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Trạng thái</label>";
                        result += "<select id='theodoi_edit' name='theodoi_edit' class='form-control'>";
                        result += "<option value='TD' " + ((string)model.Theodoi == "TD" ? "selected" : "") + ">Theo dõi</option>";
                        result += "<option value='KTD' " + ((string)model.Theodoi == "KTD" ? "selected" : "") + ">Không theo dõi</option>";
                        result += "</select>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Tên nhóm, loại tài nguyên*</label>";
                        result += "<input type='text' id='tennhom_edit' name='tennhom_edit' class='form-control' value='" + model.Ten + "'/>";
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

        [Route("GiaThueTaiNguyenDmCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Level, string Cap1, string Cap2, string Cap3, string Cap4, string Cap5, string Dvt, string Tennhom, string Theodoi)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetn.danhmuc", "Edit"))
                {
                    var model = _db.GiaThueTaiNguyenDm.FirstOrDefault(t => t.Id == Id);
                    model.Level = Level;
                    model.Cap1 = Cap1;
                    model.Cap2 = Cap2;
                    model.Cap3 = Cap3;
                    model.Cap4 = Cap4;
                    model.Cap5 = Cap5;
                    model.Dvt = Dvt;
                    model.Ten = Tennhom;
                    model.Theodoi = Theodoi;
                    model.Updated_at = DateTime.Now;
                    _db.GiaThueTaiNguyenDm.Update(model);
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

        [Route("GiaThueTaiNguyenDmCt/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetn.danhmuc", "Delete"))
                {
                    var model = _db.GiaThueTaiNguyenDm.FirstOrDefault(p => p.Id == id_delete);
                    _db.GiaThueTaiNguyenDm.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaThueTaiNguyenDm", new { Manhom = model.Manhom });
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

        [Route("GiaThueTaiNguyenDmCt/Lock")]
        [HttpPost]
        public IActionResult Lock(string Manhom, string Theodoi)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetn.danhmuc", "Edit"))
                {
                    var model = _db.GiaThueTaiNguyenDm.Where(t => t.Manhom == Manhom).ToList();
                    model.ForEach(t => { t.Theodoi = Theodoi; });
                    _db.SaveChanges();

                    var data = new { status = "success", message = "Khóa/mở khóa danh mục thành công!" };
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

        [HttpGet("GiaThueTaiNguyenDmCt/NhanExcel")]
        public IActionResult NhanExcel(string Manhom)
        {

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetn.danhmuc", "Create"))
                {
                    var model = new VMImportExcel
                    {
                        MaNhom = Manhom,
                        Sheet = 1,
                        LineStart = 3,
                        LineStop = 1000
                    };
                    ViewData["Title"] = "Thông tin chi tiết mặt hàng thuế tài nguyên";
                    return View("Views/Admin/Manages/DinhGia/GiaThueTaiNguyen/DanhMuc/ChiTiet/Excel.cshtml", model);
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
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                //if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetn.danhmuc", "Create"))
                //{
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
                                var rowcount = worksheet.Dimension.Rows;
                                requests.LineStop = requests.LineStop > rowcount ? rowcount : requests.LineStop;
                                //Regex trimmer = new Regex(@"\s\s+"); // Xóa khoảng trắng thừa trong chuỗi
                                var list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaThueTaiNguyenDm>();
                                int line = 1;
                                for (int row = requests.LineStart; row <= requests.LineStop; row++)
                                {

                                    list_add.Add(new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueTaiNguyenDm
                                    {
                                        Level = worksheet.Cells[row, 1].Value != null ?
                                                    worksheet.Cells[row, 1].Value.ToString().Trim() : "",
                                        Cap1 = worksheet.Cells[row, 2].Value != null ?
                                                    worksheet.Cells[row, 2].Value.ToString().Trim() : "",
                                        Cap2 = worksheet.Cells[row, 3].Value != null ?
                                                        worksheet.Cells[row, 3].Value.ToString().Trim() : "",
                                        Cap3 = worksheet.Cells[row, 4].Value != null ?
                                                        worksheet.Cells[row, 4].Value.ToString().Trim() : "",
                                        Cap4 = worksheet.Cells[row, 5].Value != null ?
                                                        worksheet.Cells[row, 5].Value.ToString().Trim() : "",
                                        Cap5 = worksheet.Cells[row, 6].Value != null ?
                                                        worksheet.Cells[row, 6].Value.ToString().Trim() : "",
                                        Cap6 = worksheet.Cells[row, 7].Value != null ?
                                                        worksheet.Cells[row, 7].Value.ToString().Trim() : "",
                                        Ten = worksheet.Cells[row, 8].Value != null ?
                                                        worksheet.Cells[row, 8].Value.ToString().Trim() : "",
                                        Dvt = worksheet.Cells[row, 9].Value != null ?
                                                        worksheet.Cells[row, 9].Value.ToString().Trim() : "",
                                        Manhom = requests.MaNhom,
                                        Theodoi = "TD",
                                        Sapxep = (line++).ToString(),
                                    });
                                    line++;
                                }
                                _db.GiaThueTaiNguyenDm.AddRange(list_add);
                                _db.SaveChanges();
                            }
                        }
                    }

                    return RedirectToAction("Index", "GiaThueTaiNguyenDm", new { Manhom = requests.MaNhom });
                //}
                //else
                //{
                //    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                //    return View("Views/Admin/Error/Page.cshtml");
                //}
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

        [HttpPost]
        public IActionResult RemoveRange(string manhom_removeRange)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetn.danhmuc", "Delete"))
                {
                    var model = _db.GiaThueTaiNguyenDm.Where(t => t.Manhom == manhom_removeRange);
                    if (model.Any())
                    {
                        _db.GiaThueTaiNguyenDm.RemoveRange(model);
                        _db.SaveChanges();
                    }
                    return RedirectToAction("Index", "GiaThueTaiNguyenDm", new { Manhom = manhom_removeRange });
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
    }
}
