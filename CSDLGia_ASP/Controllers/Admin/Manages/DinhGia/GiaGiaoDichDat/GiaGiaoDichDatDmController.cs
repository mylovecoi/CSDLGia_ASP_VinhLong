using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaGiaoDichDat
{
    public class GiaGiaoDichDatDmController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _env;

        public GiaGiaoDichDatDmController(CSDLGiaDBContext db, IWebHostEnvironment hostingEnv)
        {
            _db = db;
            _env = hostingEnv;
        }

        [Route("GiaGiaoDichDatDmCt")]
        [HttpGet]
        public IActionResult Index(string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.danhmuc", "Index"))
                {
                    var model = _db.GiaGiaoDichDatDm.Where(t => t.Manhom == Manhom).ToList();
                    ViewData["danhmucdonvitinh"] = _db.DmDvt;
                    ViewData["Manhom"] = Manhom;
                    ViewData["Tennhom"] = _db.GiaGiaoDichDatNhom.FirstOrDefault(t => t.Manhom == Manhom).Tennhom;
                    ViewData["Title"] = "Thông tin chi tiết giá giao dịch đất thực tế trên thị trường";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dg_giaodichdattrenthitruong";
                    ViewData["MenuLv3"] = "menu_dg_giaodichdattrenthitruong_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichDat/DanhMuc/ChiTiet/Index.cshtml", model);
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

        [Route("GiaGiaoDichDatDmCt/Store")]
        [HttpPost]
        public JsonResult Store(string Manhom, string Tennhom, string Theodoi, string donvitinh)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.danhmuc", "Create"))
                {
                    var request = new GiaGiaoDichDatDm
                    {
                        Manhom = Manhom,
                        Ten = Tennhom,
                        Theodoi = Theodoi,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                        Dvt = donvitinh,
                    };
                    _db.GiaGiaoDichDatDm.Add(request);
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

        [Route("GiaGiaoDichDatDmCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.danhmuc", "Edit"))
                {                   
                    var dmdvt = _db.DmDvt;
                    var model = _db.GiaGiaoDichDatDm.FirstOrDefault(p => p.Id == Id);
                    if (model != null)
                    {
                        string result = "<div class='row' id='edit_thongtin'>";

                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Thông tin đất thực tế trên thị trường<span class='text-danger'>*</span></label>";
                        result += "<input type='text' id='tennhom_edit' name='tennhom_edit' class='form-control' value='" + model.Ten + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Đơn vị tính</label>";
                        result += "<select id='donvitinh_edit' name='donvitinh_edit' class='form-control select2basic' style='width:100%'>";
                        foreach (var item in dmdvt)
                        {
                            result += "<option value ='" + item.Dvt + "'" + ((string)model.Dvt == item.Dvt ? "selected" : "") + " >" + item.Dvt + "</ option >";
                        }
                        result += "</select>";

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

        [Route("GiaGiaoDichDatDmCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Tennhom, string Theodoi , string donvitinh)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.danhmuc", "Edit"))
                {
                    var model = _db.GiaGiaoDichDatDm.FirstOrDefault(t => t.Id == Id);
                    model.Ten = Tennhom;
                    model.Theodoi = Theodoi;
                    model.Dvt= donvitinh;
                    model.Updated_at = DateTime.Now;
                    _db.GiaGiaoDichDatDm.Update(model);
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

        [Route("GiaGiaoDichDatDmCt/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.danhmuc", "Delete"))
                {
                    var model = _db.GiaGiaoDichDatDm.FirstOrDefault(p => p.Id == id_delete);
                    _db.GiaGiaoDichDatDm.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaGiaoDichDatDm", new { Manhom = model.Manhom });
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

        [Route("GiaGiaoDichDatDmCt/Lock")]
        [HttpPost]
        public IActionResult Lock(string Manhom, string Theodoi)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.danhmuc", "Edit"))
                {
                    var model = _db.GiaGiaoDichDatDm.Where(t => t.Manhom == Manhom).ToList();
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
        [HttpGet("GiaGiaoDichDatDm/NhanExcel")]
        public IActionResult NhanExcel(string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.r.dm", "Create"))
                {
                    var model = new GiaGiaoDichDatDm
                    {
                        LineStart = 2,
                        LineStop = 1000,
                        Sheet = 1,
                        Manhom = Manhom,
                        Tennhom = _db.GiaGiaoDichDatNhom.FirstOrDefault(x => x.Manhom == Manhom).Tennhom,
                    };
                    ViewData["Title"] = "Danh mục giao dich đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dg_giaodichdattrenthitruong";
                    ViewData["MenuLv3"] = "menu_dg_giaodichdattrenthitruong_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichDat/DanhMuc/ChiTiet/Excell.cshtml", model);

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
        public async Task<IActionResult> ImportExcel(GiaGiaoDichDatDm requests)
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
                            //Regex trimmer = new Regex(@"\s\s+"); // Xóa khoảng trắng thừa trong chuỗi
                            var list_add = new List<GiaGiaoDichDatDm>();
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

                                list_add.Add(new GiaGiaoDichDatDm
                                {
                                    Sapxep = line.ToString(),
                                    Ten = worksheet.Cells[row, 1].Value != null ?
                                                 worksheet.Cells[row, 1].Value.ToString().Trim() : "",
                                    Dvt = worksheet.Cells[row, 2].Value != null ?
                                                 worksheet.Cells[row, 2].Value.ToString().Trim() : "",
                                    Manhom = requests.Manhom,
                                    Theodoi = "TD",
                                });
                                line++;
                            }
                            _db.GiaGiaoDichDatDm.AddRange(list_add);
                            _db.SaveChanges();
                        }
                    }
                }

                return RedirectToAction("Index", "GiaGiaoDichDatDm", new { Manhom = requests.Manhom });
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

        //[Route("GiaGiaoDichDatDmCt/Excel")]
        //[HttpPost]
        //public async Task<JsonResult> Excel(string Manhom, string Level, string Cap1, string Cap2, string Cap3,
        //    string Cap4, string Cap5, string Dvt, string Ten, int Sheet, int LineStart, int LineStop, IFormFile FormFile)
        //{
        //    if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
        //    {
        //        if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.danhmuc", "Edit"))
        //        {
        //            LineStart = LineStart == 0 ? 1 : LineStart;
        //            var list_add = new List<GiaGiaoDichDatDm>();
        //            int sheet = Sheet == 0 ? 0 : (Sheet - 1);
        //            using (var stream = new MemoryStream())
        //            {
        //                await FormFile.CopyToAsync(stream);
        //                using (var package = new ExcelPackage(stream))
        //                {
        //                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        //                    ExcelWorksheet worksheet = package.Workbook.Worksheets[sheet];
        //                    var rowcount = worksheet.Dimension.Rows;
        //                    LineStop = LineStop > rowcount ? rowcount : LineStop;

        //                    for (int row = LineStart; row <= LineStop; row++)
        //                    {
        //                        list_add.Add(new GiaGiaoDichDatDm
        //                        {
        //                            Manhom = Manhom,
        //                            Theodoi = "TD",
        //                            Created_at = DateTime.Now,
        //                            Updated_at = DateTime.Now,

        //                            Level = worksheet.Cells[row, Int16.Parse(Level)].Value.ToString() != null ?
        //                                        worksheet.Cells[row, Int16.Parse(Level)].Value.ToString().Trim() : "",

        //                            Cap1 = worksheet.Cells[row, Int16.Parse(Cap1)].Value != null ?
        //                                        worksheet.Cells[row, Int16.Parse(Cap1)].Value.ToString().Trim() : "",

        //                            Cap2 = worksheet.Cells[row, Int16.Parse(Cap2)].Value != null ?
        //                                        worksheet.Cells[row, Int16.Parse(Cap2)].Value.ToString().Trim() : "",

        //                            Cap3 = worksheet.Cells[row, Int16.Parse(Cap3)].Value != null ?
        //                                        worksheet.Cells[row, Int16.Parse(Cap3)].Value.ToString().Trim() : "",

        //                            Cap4 = worksheet.Cells[row, Int16.Parse(Cap4)].Value != null ?
        //                                        worksheet.Cells[row, Int16.Parse(Cap4)].Value.ToString().Trim() : "",

        //                            Cap5 = worksheet.Cells[row, Int16.Parse(Cap5)].Value != null ?
        //                                        worksheet.Cells[row, Int16.Parse(Cap5)].Value.ToString().Trim() : "",

        //                            Ten = worksheet.Cells[row, Int16.Parse(Ten)].Value != null ?
        //                                        worksheet.Cells[row, Int16.Parse(Ten)].Value.ToString().Trim() : "",

        //                            Dvt = worksheet.Cells[row, Int16.Parse(Dvt)].Value != null ?
        //                                        worksheet.Cells[row, Int16.Parse(Dvt)].Value.ToString().Trim() : "",
        //                        });
        //                    }

        //                }
        //            }
        //            _db.GiaGiaoDichDatDm.AddRange(list_add);
        //            _db.SaveChanges();

        //            var data = new { status = "success" };
        //            return Json(data);
        //        }
        //        else
        //        {
        //            var data = new { status = "error", message = "Bạn không có quyền thực hiện chức năng này!!!" };
        //            return Json(data);
        //        }
        //    }
        //    else
        //    {
        //        var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
        //        return Json(data);
        //    }
        //}
    }
}
