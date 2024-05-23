using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;
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
//using OfficeOpenXml;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaSpDvKhungGia
{
    public class GiaSpDvKhungGiaDmController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _env;

        public GiaSpDvKhungGiaDmController(CSDLGiaDBContext db, IWebHostEnvironment hostingEnv)
        {
            _db = db;
            _env = hostingEnv;
        }

        [Route("GiaSpDvKhungGiaDmCt")]
        [HttpGet]
        public IActionResult Index(string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvkhunggia.danhmuc", "Index"))
                {
                    var model = _db.GiaSpDvKhungGiaDm.Where(t => t.Manhom == Manhom);

                    ViewData["Manhom"] = Manhom;
                    ViewData["Tennhom"] = _db.GiaSpDvKhungGiaNhom.FirstOrDefault(t => t.Manhom == Manhom).Tennhom;
                    ViewData["Title"] = "Thông tin chi tiết nhóm sản phẩm dịch vụ khung giá";
                    ViewData["MenuLv1"] = "menu_spdvkhunggia";
                    ViewData["MenuLv2"] = "menu_spdvkhunggia_dm";
                    ViewData["Donvitinh"] = _db.DmDvt.ToList();
                    ViewData["PhanLoaiDichVu"] = _db.GiaSpDvKhungGiaDm.ToList();
                    var chkSTT = _db.GiaSpDvKhungGiaDm.Where(t => t.Manhom == Manhom);
                    if (chkSTT.Any())
                    {
                        ViewData["STT"] = chkSTT.Max(x=>x.SapXep) + 1;
                    }
                    else
                    {
                        ViewData["STT"] = 1;
                    }
                    
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvKhungGia/DanhMuc/ChiTiet/Index.cshtml", model.ToList());
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

        [Route("GiaSpDvKhungGiaDmCt/Store")]
        [HttpPost]
        public JsonResult Store(string Manhom, string Tenspdv, string Dvt, string HienThi, double SapXep, string[] Style)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvkhunggia.danhmuc", "Create"))
                {
                    string str_style = Style.Count() > 0 ? string.Join(",", Style.ToArray()) : "";
                    var request = new GiaSpDvKhungGiaDm
                    {
                        Manhom = Manhom,
                        Dvt = Dvt,
                        Tenspdv = Tenspdv,
                        HienThi = HienThi,
                        SapXep = SapXep,
                        Style = str_style,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaSpDvKhungGiaDm.Add(request);
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

        [Route("GiaSpDvKhungGiaDmCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvkhunggia.danhmuc", "Edit"))
                {
                    var dvt = _db.DmDvt.ToList();
                    var model = _db.GiaSpDvKhungGiaDm.FirstOrDefault(p => p.Id == Id);
                    List<string> list_style = !string.IsNullOrEmpty(model.Style) ? new List<string>(model.Style.Split(',')) : new List<string>();
                    if (model != null)
                    {
                        string result = "<div class='row' id='edit_thongtin'>";

                        result += "<div class='col-xl-3'>";
                        result += "<div class='form -group fv-plugins-icon-container'>";
                        result += "<label>STT báo cáo</label>";
                        result += "<input type='text' id='hienthi_edit' name='hienthi_edit' class='form-control' value='" + model.HienThi + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-3'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Sắp xếp</label>";
                        result += "<input type='text' id='sapxep_edit' name='sapxep_edit' class='form-control' value='" + model.SapXep + "'/>";
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

                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Tên đối tượng</label>";
                        result += "<input type='text' id='tenspdv_edit' name='tenspdv_edit' class='form-control' value='" + model.Tenspdv + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-12'>";
                        result += "<label class='form-control-label'>Đơn vị tính</label>";
                        result += "<select id='dvt_edit' name='dvt_edit' class='form-control select2basic'";
                        result += "<option value =''>--Chọn đơn vị tính--</option>";
                        foreach (var item in dvt)
                        {
                            if (model.Dvt == item.Dvt)
                            {
                                result += "<option value ='" + @item.Dvt + "' selected >" + @item.Dvt + "</ option >";
                            }
                            else
                            {
                                result += "<option value ='" + @item.Dvt + "' >" + @item.Dvt + "</ option >";
                            }
                        }
                        result += "</select>";
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

        [Route("GiaSpDvKhungGiaDmCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Tenspdv, string Dvt, string HienThi, double SapXep, string[] Style)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvkhunggia.danhmuc", "Edit"))
                {
                    string str_style = Style.Count() > 0 ? string.Join(",", Style.ToArray()) : "";
                    var model = _db.GiaSpDvKhungGiaDm.FirstOrDefault(t => t.Id == Id);
                    model.Tenspdv = Tenspdv;                   
                    model.Dvt = Dvt;
                    model.HienThi = HienThi;
                    model.SapXep = SapXep;
                    model.Style = str_style;
                    model.Updated_at = DateTime.Now;
                    _db.GiaSpDvKhungGiaDm.Update(model);
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

        [Route("GiaSpDvKhungGiaDmCt/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvkhunggia.danhmuc", "Delete"))
                {
                    var model = _db.GiaSpDvKhungGiaDm.FirstOrDefault(p => p.Id == id_delete);
                    _db.GiaSpDvKhungGiaDm.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaSpDvKhungGiaDm", new { Manhom = model.Manhom });
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

        [Route("GiaSpDvKhungGiaDmCt/Lock")]
        [HttpPost]
        public IActionResult Lock(string Manhom, string Theodoi)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvkhunggia.danhmuc", "Edit"))
                {
                    var model = _db.GiaSpDvKhungGiaDm.Where(t => t.Manhom == Manhom).ToList();
                    model.ForEach(t => { t.HienTrang = Theodoi; });
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

        public IActionResult NhanExcel(string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvkhunggia.danhmuc", "Create"))
                {
                    var model = new CSDLGia_ASP.ViewModels.VMImportExcel
                    {
                        LineStart = 4,
                        LineStop = 1000,
                        Sheet = 1,
                        MaNhom = Manhom,
                        TenNhom = _db.GiaSpDvKhungGiaNhom.FirstOrDefault(t => t.Manhom == Manhom)?.Tennhom ?? ""
                    };

                    ViewData["Title"] = "Danh mục chi tiết nhóm khung giá sản phẩm dịch vụ";
                    ViewData["MenuLv1"] = "menu_spdvkhunggia";
                    ViewData["MenuLv2"] = "menu_spdvkhunggia_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvKhungGia/DanhMuc/Excel.cshtml", model);

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
                            var list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvKhungGiaDm>();
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

                                list_add.Add(new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvKhungGiaDm
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
                            _db.GiaSpDvKhungGiaDm.AddRange(list_add);
                            _db.SaveChanges();
                        }
                    }
                }

                return RedirectToAction("Index", "GiaSpDvKhungGiaDmCt", new { Manhom = requests.MaNhom });
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

        }
    }
}
