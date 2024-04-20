using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;
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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaHhDvCn
{
    public class GiaHhDvCnDmController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaHhDvCnDmController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaHhDvCnDm")]
        [HttpGet]
        public IActionResult Index(string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.hhdvcn.danhmuc", "Index"))
                {
                    var model = _db.GiaHhDvCnDm.Where(t => t.Manhom == Manhom).ToList();
                    ViewData["STT"] = model.Any() ? (model.Max(t => t.Sapxep) + 1) : 1;
                    ViewData["Donvitinh"] = _db.DmDvt.ToList();
                    ViewData["Manhom"] = Manhom;
                    ViewData["Title"] = "Nhóm hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_hhdvcn";
                    ViewData["MenuLv3"] = "menu_hhdvcn_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaHhDvCn/DanhMuc/ChiTiet/Index.cshtml", model);
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

        [Route("GiaHhDvCnDm/Store")]
        [HttpPost]
        public JsonResult Store(string Manhom, string Tenspdv, string Dvt, string HienThi, int SapXep)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.hhdvcn.danhmuc", "Create"))
                {
                    var request = new GiaHhDvCnDm
                    {
                        Maspdv = DateTime.Now.ToString("yyMMddssmmHH"),
                        Tenspdv = Tenspdv,
                        Dvt = Dvt,
                        Manhom = Manhom,
                        HienThi = HienThi,
                        Sapxep = SapXep,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaHhDvCnDm.Add(request);
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

        [Route("GiaHhDvCnDm/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.hhdvcn.danhmuc", "Delete"))
                {
                    var model = _db.GiaHhDvCnDm.FirstOrDefault(p => p.Id == id_delete);
                    _db.GiaHhDvCnDm.Remove(model);
                    _db.SaveChanges();

                   
                    return RedirectToAction("Index", "GiaHhDvCnDm", new { Manhom = model.Manhom });
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

        [Route("GiaHhDvCnDm/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.hhdvcn.danhmuc", "Edit"))
                {
                    var model = _db.GiaHhDvCnDm.FirstOrDefault(p => p.Id == Id);
                    if (model != null)
                    {
                        string result = "<div class='row' id='edit_thongtin'>";

                        result += "<div class='col-xl-6'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>TT</label>";
                        result += "<input type='text' id='sapxep_edit' name='sapxep_edit' class='form-control' value='" + model.Sapxep + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-6'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>STT Hiển Thị</label>";
                        result += "<input type='text' id='hienthi_edit' name='hienthi_edit' class='form-control' value='" + model.HienThi + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Tên hàng hóa:</label>";
                        result += "<input type='text' id='tenspdv_edit' name='tenspdv_edit' class='form-control' value='" + model.Tenspdv + "'/>";
                        result += "</div>";
                        result += "</div>";


                        result += "<div class='col-xl-4'>";
                        result += "<label class='form-control-label'>Đơn vị tính</label>";
                        result += "<select id='dvt_edit' name='dvt_edit' class='form-control select2me select2-offscreen' tabindex='-1' title=''>";

                        var dvt = _db.DmDvt.ToList();
                        foreach (var item in dvt)
                        {
                            result += "<option value ='" + @item.Dvt + "'>" + @item.Dvt + "</ option >";
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

        [Route("GiaHhDvCnDm/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Tenspdv, string Dvt, int SapXep, string HienThi)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.hhdvcn.danhmuc", "Edit"))
                {
                    var model = _db.GiaHhDvCnDm.FirstOrDefault(t => t.Id == Id);
                    model.Tenspdv = Tenspdv;                    
                    model.Dvt = Dvt;
                    model.HienThi = HienThi;
                    model.Sapxep = SapXep;
                    model.Updated_at = DateTime.Now;
                    _db.GiaHhDvCnDm.Update(model);
                    _db.SaveChanges();
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_hhdvcn";
                    ViewData["MenuLv3"] = "menu_hhdvcn_dm";
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

        public IActionResult NhanExcel(string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.hhdvcn.danhmuc", "Create"))
                {
                    var model = new CSDLGia_ASP.ViewModels.VMImportExcel
                    {
                        LineStart = 4,
                        LineStop = 1000,
                        Sheet = 1,
                        MaNhom = Manhom,
                        TenNhom = _db.GiaHhDvCnNhom.FirstOrDefault(t => t.Manhom == Manhom)?.Tennhom ?? ""
                    };

                    ViewData["Title"] = "Nhóm hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_hhdvcn";
                    ViewData["MenuLv3"] = "menu_hhdvcn_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaHhDvCn/DanhMuc/Excel.cshtml", model);

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
                            var list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvCnDm>();
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

                                list_add.Add(new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvCnDm
                                {
                                    Sapxep = line,
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
                            _db.GiaHhDvCnDm.AddRange(list_add);
                            _db.SaveChanges();
                        }
                    }
                }

                return RedirectToAction("Index", "GiaHhDvCnDm", new { Manhom = requests.MaNhom });
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

        }
    }
}
