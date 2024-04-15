using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.PhiLePhi
{
    public class PhiLePhiDmController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _env;

        public PhiLePhiDmController(CSDLGiaDBContext db, IWebHostEnvironment hostingEnv)
        {
            _db = db;
            _env = hostingEnv;
        }

        [Route("PhiLePhiDmCt")]
        [HttpGet]
        public IActionResult Index(string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.philephi.danhmuc", "Index"))
                {

                    var model = _db.PhiLePhiDm.Where(t => t.Manhom == Manhom);
                    var chkSTT = _db.PhiLePhiDm.Where(t => t.Manhom == Manhom);
                    if (chkSTT.Any())
                    {
                        ViewData["STT"] = chkSTT.Max(x => x.SapXep) + 1;
                    }
                    else
                    {
                        ViewData["STT"] = 1;
                    }


                    ViewData["Manhom"] = Manhom;
                    ViewData["Tennhom"] = _db.PhiLePhiNhom.FirstOrDefault(t => t.Manhom == Manhom).Tennhom;
                    ViewData["Title"] = "Thông tin chi tiết nhóm phí, lệ phí";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_plp";
                    ViewData["MenuLv3"] = "menu_plp_dm";
                    return View("Views/Admin/Manages/DinhGia/PhiLePhi/DanhMuc/ChiTiet/Index.cshtml", model.ToList());
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

        [Route("PhiLePhiDmCt/Store")]
        [HttpPost]
        public JsonResult Store(string Manhom, string Tenspdv, string Dvt, string Hientrang, string HienThi, double SapXep, string[] Style)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.philephi.danhmuc", "Create"))
                {
                    string str_style = Style.Count() > 0 ? string.Join(",", Style.ToArray()) : "";
                    var request = new PhiLePhiDm
                    {
                        Manhom = Manhom,
                        Dvt = Dvt,
                        Tenspdv = Tenspdv,
                        HienTrang = Hientrang,
                        HienThi = HienThi,
                        SapXep = SapXep,
                        Style = str_style,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.PhiLePhiDm.Add(request);
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

        [Route("PhiLePhiDmCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.philephi.danhmuc", "Edit"))
                {
                    var model = _db.PhiLePhiDm.FirstOrDefault(p => p.Id == Id);
                    if (model != null)
                    {
                        List<string> list_style = !string.IsNullOrEmpty(model.Style) ? new List<string>(model.Style.Split(',')) : new List<string>();

                        string result = "<div class='row' id='edit_thongtin'>";
                        result += "<div class='col -xl-2'>";
                        result += "<div class='form -group fv-plugins-icon-container'>";
                        result += "<label>STT báo cáo</label>";
                        result += "<input type='text' id='hienthi_edit' name='hienthi_edit' class='form-control' value='" + model.HienThi + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col -xl-2'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Sắp xếp</label>";
                        result += "<input type='text' id='sapxep_edit' name='sapxep_edit' class='form-control' value='" + model.SapXep + "'/>";
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
                        result += "<label>Đối tượng cụ thể</label>";
                        result += "<input type='text' id='tenspdv_edit' name='tenspdv_edit' class='form-control' value='" + model.Tenspdv + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-12'>";
                        result += "<label class='form-control-label'>Đơn vị tính</label>";
                        result += "<input id='dvt_edit' name='dvt_edit' class='form-control' value='" + model.Dvt + "'>";
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

        [Route("PhiLePhiDmCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Tenspdv, string Dvt, string Hientrang, string HienThi, double SapXep, string[] Style)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.philephi.danhmuc", "Edit"))
                {
                    var model = _db.PhiLePhiDm.FirstOrDefault(t => t.Id == Id);
                    string str_style = Style.Count() > 0 ? string.Join(",", Style.ToArray()) : "";
                    model.Tenspdv = Tenspdv;
                    model.Dvt = Dvt;
                    model.HienThi = HienThi;
                    model.SapXep = SapXep;
                    model.Style = str_style;
                    model.Updated_at = DateTime.Now;
                    _db.PhiLePhiDm.Update(model);
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

        [Route("PhiLePhiDmCt/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.philephi.danhmuc", "Delete"))
                {
                    var model = _db.PhiLePhiDm.FirstOrDefault(p => p.Id == id_delete);
                    _db.PhiLePhiDm.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "PhiLePhiDm", new { Manhom = model.Manhom });
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
        public IActionResult RemoveRange(string manhom_removerange)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.philephi.danhmuc", "Delete"))
                {
                    var model = _db.PhiLePhiDm.Where(t => t.Manhom == manhom_removerange);
                    if (model.Any())
                    {
                        _db.PhiLePhiDm.RemoveRange(model);
                        _db.SaveChanges();
                    }
                    return RedirectToAction("Index", "PhiLePhiDm", new { Manhom = manhom_removerange });

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

        [HttpGet("PhiLePhiDmCt/NhanExcel")]
        public IActionResult NhanExcel(string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.philephi", "Create"))
                {
                    var model = new CSDLGia_ASP.ViewModels.VMImportExcel
                    {
                        LineStart = 3,
                        LineStop = 1000,
                        Sheet = 1,
                        MaNhom = Manhom,
                        TenNhom = _db.PhiLePhiNhom.FirstOrDefault(t => t.Manhom == Manhom)?.Tennhom ?? ""
                    };

                    ViewData["Manhom"] = Manhom;
                    ViewData["Tennhom"] = _db.PhiLePhiNhom.FirstOrDefault(t => t.Manhom == Manhom).Tennhom;
                    ViewData["Title"] = "Thông tin chi tiết nhóm phí, lệ phí";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_plp";
                    ViewData["MenuLv3"] = "menu_plp_dm";

                    return View("Views/Admin/Manages/DinhGia/PhiLePhi/DanhMuc/ChiTiet/Excel.cshtml", model);

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
        public async Task<IActionResult> Import(VMImportExcel requests)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.philephi.danhmuc", "Create"))
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
                                var list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.PhiLePhiDm>();
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

                                    list_add.Add(new CSDLGia_ASP.Models.Manages.DinhGia.PhiLePhiDm
                                    {
                                        SapXep = line,
                                        HienThi = worksheet.Cells[row, 1].Value != null ?
                                                     worksheet.Cells[row, 1].Value.ToString().Trim() : "",
                                        Tenspdv = worksheet.Cells[row, 2].Value != null ?
                                                     worksheet.Cells[row, 2].Value.ToString().Trim() : "",
                                        Dvt = worksheet.Cells[row, 3].Value != null ?
                                                     worksheet.Cells[row, 3].Value.ToString().Trim() : "",
                                        Style = strStyle.ToString(),
                                        Manhom = requests.MaNhom
                                    });
                                    line++;
                                }
                                _db.PhiLePhiDm.AddRange(list_add);
                                _db.SaveChanges();
                            }
                        }
                    }
                    return RedirectToAction("Index", "PhiLePhiDm", new { Manhom = requests.MaNhom });

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
