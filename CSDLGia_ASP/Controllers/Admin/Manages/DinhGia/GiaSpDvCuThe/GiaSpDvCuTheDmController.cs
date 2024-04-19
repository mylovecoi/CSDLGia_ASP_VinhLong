using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;
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
//using OfficeOpenXml;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaSpDvCuThe
{
    public class GiaSpDvCuTheDmController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _env;

        public GiaSpDvCuTheDmController(CSDLGiaDBContext db, IWebHostEnvironment hostingEnv)
        {
            _db = db;
            _env = hostingEnv;
        }

        [Route("GiaSpDvCuTheDmCt")]
        [HttpGet]
        public IActionResult Index(string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.danhmuc", "Index"))
                {
                    var model = _db.GiaSpDvCuTheDm.Where(t => t.Manhom == Manhom).ToList();

                    ViewData["Manhom"] = Manhom;
                    ViewData["Tennhom"] = _db.GiaSpDvCuTheNhom.FirstOrDefault(t => t.Manhom == Manhom).Tennhom;
                    ViewData["Title"] = "Danh mục chi tiết nhóm sản phẩm dịch vụ cụ thể";
                    ViewData["MenuLv1"] = "menu_spdvcuthe";
                    ViewData["MenuLv2"] = "menu_spdvcuthe_dm";
                    ViewData["Donvitinh"] = _db.DmDvt.ToList();
                    ViewData["PhanLoaiDichVu"] = _db.GiaSpDvCuTheDm.ToList();
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCuThe/DanhMuc/ChiTiet/Index.cshtml", model);
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

        [Route("GiaSpDvCuTheCt/GetMaxSapXep")]
        [HttpPost]

        public JsonResult GetMaxSapXep(string Manhom)
        {
            var i = 0;
            var data = _db.GiaSpDvCuTheDm.Where(t => t.Manhom == Manhom);

            if (data.Any())
            {
                i = data.Max(x => x.Sapxep);
            }

            return Json(i);
        }

        [Route("GiaSpDvCuTheDmCt/Store")]
        [HttpPost]
        public JsonResult Store(string manhom, string tt, string tenspdv, string dvt, string mucgia1, string mucgia2, int sapxep)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.danhmuc", "Create"))
                {

                    var request = new GiaSpDvCuTheDm
                    {
                        Manhom = manhom,
                        Tt = tt,
                        Maspdv = DateTime.Now.ToString("yyMMddfffssmmHH"),
                        Tenspdv = tenspdv,
                        Dvt = dvt,                       
                        Sapxep = sapxep,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaSpDvCuTheDm.Add(request);
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

        [Route("GiaSpDvCuTheDmCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.danhmuc", "Edit"))
                {
                    var model = _db.GiaSpDvCuTheDm.FirstOrDefault(p => p.Id == Id);
                    var phanloai = _db.GiaSpDvCuTheDm;
                    
                    if (model != null)
                    {
                        string result = "<div class='row' id='edit_thongtin'>";

                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>TT</label>";
                        result += "<input type='text' id='tt_edit' name='tt_edit' class='form-control' value='" + model.Tt + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Tên sản phẩm dịch vụ</label>";
                        result += "<input type='text' id='tenspdv_edit' name='tenspdv_edit' class='form-control' value='" + model.Tenspdv + "'/>";
                        result += "</div>";
                        result += "</div>";                        

                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Đơn vị tính</label>";
                        result += "<input type='text' id='dvt_edit' name='dvt_edit' class='form-control' value='" + model.Dvt + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Sắp xếp</label>";
                        result += "<input type='text' id='sapxep_edit' name='sapxep_edit' class='form-control' value='" + model.Sapxep + "'/>";
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

        [Route("GiaSpDvCuTheDmCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string tt, string tenspdv, string dvt, string mucgia1, string mucgia2, int sapxep)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.danhmuc", "Edit"))
                {
                    var model = _db.GiaSpDvCuTheDm.FirstOrDefault(t => t.Id == Id);

                    model.Tt = tt;
                    model.Tenspdv = tenspdv;
                    model.Dvt = dvt;                  
                    model.Sapxep = sapxep;
                  
                    model.Updated_at = DateTime.Now;
                    _db.GiaSpDvCuTheDm.Update(model);
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

        [Route("GiaSpDvCuTheDmCt/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.danhmuc", "Delete"))
                {
                    var model = _db.GiaSpDvCuTheDm.FirstOrDefault(p => p.Id == id_delete);
                    _db.GiaSpDvCuTheDm.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaSpDvCuTheDm", new { Manhom = model.Manhom });
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

        [HttpGet("GiaSpDvCuTheDmCt/NhanExcel")]
        public IActionResult NhanExcel(string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.danhmuc", "Create"))
                {
                    var model = new CSDLGia_ASP.ViewModels.VMImportExcel
                    {
                        LineStart = 4,
                        LineStop = 1000,
                        Sheet = 1,
                        MaNhom = Manhom,
                        TenNhom = _db.GiaSpDvCuTheNhom.FirstOrDefault(t => t.Manhom == Manhom)?.Tennhom ?? ""
                    };

                    ViewData["Title"] = "Danh mục chi tiết nhóm sản phẩm dịch vụ cụ thể";
                    ViewData["MenuLv1"] = "menu_spdvcuthe";
                    ViewData["MenuLv2"] = "menu_spdvcuthe_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCuThe/DanhMuc/Excel.cshtml", model);

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
                            var list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCuTheDm>();
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

                                list_add.Add(new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCuTheDm
                                {
                                    Sapxep = line,
                                    Tt = worksheet.Cells[row, 1].Value != null ?
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
                            _db.GiaSpDvCuTheDm.AddRange(list_add);
                            _db.SaveChanges();
                        }
                    }
                }

                return RedirectToAction("Index", "GiaSpDvCuTheDmCt", new { Manhom = requests.MaNhom });
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

    }
}
