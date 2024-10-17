using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.ViewModels;
using CSDLGia_ASP.Services;
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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaSpDvCongIch
{
    public class GiaSpDvCongIchDmController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IDsDonviService _dsDonviService;
        private readonly ITrangThaiHoSoService _trangThaiHoSoService;

        public GiaSpDvCongIchDmController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment, IDsDonviService dsDonviService, ITrangThaiHoSoService trangThaiHoSoService)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
            _dsDonviService = dsDonviService;
            _trangThaiHoSoService = trangThaiHoSoService;
        }

        [Route("GiaSpDvCongIchDmCt")]
        [HttpGet]
        public IActionResult Index(string Madv, string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.danhmuc", "Index"))
                {
                    var model = _db.GiaSpDvCongIchDm.Where(t => t.Manhom == Manhom && t.Madv == Madv).ToList();
                    int max_sttsapxep = model.Any() ? model.Max(t => t.Sapxep) : 1;
                    ViewData["SapXep"] = max_sttsapxep;
                    ViewData["Manhom"] = Manhom;
                    ViewData["Madv"] = Madv;
                    ViewData["Tennhom"] = _db.GiaSpDvCongIchNhom.FirstOrDefault(t => t.Manhom == Manhom)?.Tennhom ?? "";
                    ViewData["Title"] = "Danh mục chi tiết nhóm sản phẩm dịch vụ công ích";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdvci";
                    ViewData["MenuLv3"] = "menu_dgdvci_dm";
                    ViewData["Donvitinh"] = _db.DmDvt.ToList();
                    ViewData["PhanLoaiDichVu"] = _db.GiaSpDvCongIchDm.ToList();
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCongIch/DanhMuc/ChiTiet/Index.cshtml", model);
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

        [Route("GiaSpDvCongIchDmCt/Store")]
        [HttpPost]
        public JsonResult Store(string Madv, string Manhom, string Tenspdv,string LoaiDoThi, string Dvt, string HienThi, int Sapxep, string[] Style)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.danhmuc", "Create"))
                {
                    string str_style = Style.Count() > 0 ? string.Join(",", Style.ToArray()) : "";
                    var request = new GiaSpDvCongIchDm
                    {
                        Madv = Madv,
                        Manhom = Manhom,
                        Maspdv = DateTime.Now.ToString("yyMMddfffssmmHH"),
                        Tenspdv = Tenspdv,
                        LoaiDoThi = LoaiDoThi,
                        Dvt = Dvt,
                        HienThi = HienThi,
                        Sapxep = Sapxep,
                        Style = str_style,

                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaSpDvCongIchDm.Add(request);
                    _db.SaveChanges();

                    var checkdvt = _db.DmDvt.FirstOrDefault(t => t.Dvt == Dvt); // kiểm tra đơn vị tính nếu không có giá trị thì thêm vào
                    if (checkdvt == null)
                    {
                        var dvt = new DmDvt
                        {
                            Dvt = Dvt,
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        };
                        _db.DmDvt.Add(dvt);
                        _db.SaveChanges();

                    }

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

        [Route("GiaSpDvCongIchDmCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.danhmuc", "Edit"))
                {
                    var model = _db.GiaSpDvCongIchDm.FirstOrDefault(p => p.Id == Id);
                    var phanloai = _db.GiaSpDvCongIchDm;

                    if (model != null)
                    {

                        List<string> list_style = !string.IsNullOrEmpty(model.Style) ? new List<string>(model.Style.Split(',')) : new List<string>();

                        string result = "<div class='row' id='edit_thongtin'>";

                        result += "<div class='col-xl-3'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Sắp xếp</label>";
                        result += "<input type='text' id='sapxep_edit' name='sapxep_edit' class='form-control' value='" + model.Sapxep + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-3'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Hiển thị</label>";
                        result += "<input type='text' id='hienthi_edit' name='hienthi_edit' class='form-control' value='" + model.HienThi + "'/>";
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

                        result += "<div class='col-xl-8'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Tên sản phẩm dịch vụ</label>";
                        result += "<input type='text' id='tenspdv_edit' name='tenspdv_edit' class='form-control' value='" + model.Tenspdv + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-8'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Loại đô thị</label>";
                        result += "<input type='text' id='loaidothi_edit' name='loaidothi_edit' class='form-control' value='" + model.LoaiDoThi + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Đơn vị tính</label>";
                        result += "<input type='text' id='dvt_edit' name='dvt_edit' class='form-control' value='" + model.Dvt + "'/>";
                        result += "</div>";
                        result += "</div>";

                       

                        result += "<input hidden type='text' id='id_edit' name='id_edit' value='" + model.Id + "'/>";
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

        [Route("GiaSpDvCongIchDmCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Dvt, string Tenspdv, string LoaiDoThi, int Sapxep, string HienThi, string[] Style)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.danhmuc", "Edit"))
                {
                    string str_style = Style.Count() > 0 ? string.Join(",", Style.ToArray()) : "";
                    var model = _db.GiaSpDvCongIchDm.FirstOrDefault(t => t.Id == Id);
                    model.HienThi = HienThi;
                    model.Tenspdv = Tenspdv;
                    model.LoaiDoThi = LoaiDoThi;
                    model.Dvt = Dvt;
                    model.Sapxep = Sapxep;
                    model.Style = str_style;
                    model.Updated_at = DateTime.Now;
                    _db.GiaSpDvCongIchDm.Update(model);
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

        [Route("GiaSpDvCongIchDmCt/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.danhmuc", "Delete"))
                {
                    var model = _db.GiaSpDvCongIchDm.FirstOrDefault(p => p.Id == id_delete);
                    _db.GiaSpDvCongIchDm.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaSpDvCongIchDm", new { Manhom = model.Manhom });
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
        public IActionResult Remove(string manhom_remove, string madv_remove)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.danhmuc", "Delete"))
                {
                    var model = _db.GiaSpDvCongIchDm.Where(p => p.Manhom == manhom_remove && p.Madv == madv_remove);
                    _db.GiaSpDvCongIchDm.RemoveRange(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaSpDvCongIchDm", new { Manhom = manhom_remove });
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

        [HttpGet("GiaSpDvCongIchDmCt/NhanExcel")]
        public IActionResult NhanExcel (string Manhom, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.danhmuckhung", "Index"))
                {
                    var model = new VMImportExcel()
                    {
                        Sheet = 1,
                        LineStart = 2,
                        LineStop = 1000,
                        MaNhom = Manhom,
                        MaDv = Madv,
                    };
                    ViewData["Title"] = "Danh mục chi tiết nhóm sản phẩm dịch vụ công ích";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdvci";
                    ViewData["MenuLv3"] = "menu_dgdvci_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCongIch/DanhMuc/ChiTiet/Excel.cshtml", model);
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
                    var list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCongIchDm>();
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

                        list_add.Add(new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCongIchDm
                        {
                            Madv = requests.MaDv,
                            Manhom = requests.MaNhom,
                            Sapxep = line,
                            HienThi = worksheet.Cells[row, 1].Value != null ?
                                        worksheet.Cells[row, 1].Value.ToString().Trim() : "",
                            Tenspdv = worksheet.Cells[row, 2].Value != null ?
                                        worksheet.Cells[row, 2].Value.ToString().Trim() : "",
                            Dvt = worksheet.Cells[row, 3].Value != null ?
                                        worksheet.Cells[row, 3].Value.ToString().Trim() : "",

                            Style = strStyle.ToString()
                        });
                        line = line + 1;
                    }
                    _db.GiaSpDvCongIchDm.AddRange(list_add);
                    _db.SaveChanges();
                }
            }

            return RedirectToAction("Index", "GiaSpDvCongIchDm", new { Manhom = requests.MaNhom, Madv = requests.MaDv});
        }
    }
}
