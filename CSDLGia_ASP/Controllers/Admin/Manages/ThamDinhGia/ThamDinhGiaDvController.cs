using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.ThamDinhGia;
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
using System.Security.Policy;

namespace CSDLGia_ASP.Controllers.Admin.Manages.ThamDinhGia
{
    public class ThamDinhGiaDvController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public ThamDinhGiaDvController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("ThamDinhGia/Donvi")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.dv", "Index"))
                {
                    var model = _db.ThamDinhGiaDv.ToList();

                    ViewData["Title"] = "Thông tin đơn vị thẩm định giá";
                    ViewData["MenuLv1"] = "menu_tdg";
                    ViewData["MenuLv2"] = "menu_dm_dv";
                    return View("Views/Admin/Manages/ThamDinhGia/DonVi/Index.cshtml", model);
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

        [Route("ThamDinhGia/Donvi/Store")]
        [HttpPost]
        public JsonResult Store(string Tendv, string Diachi, string Nguoidaidien, string Chucvu, string Sothe, DateTime Ngaycap, string Soqddungtd, DateTime Ngaydungtd)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.dv", "Create"))
                {
                    var request = new ThamDinhGiaDv
                    {
                        Maso = DateTime.Now.ToString("yyMMddssmmHH"),
                        Tendv = Tendv,
                        Diachi = Diachi,
                        Nguoidaidien = Nguoidaidien,
                        Chucvu = Chucvu,
                        Sothe = Sothe,
                        Ngaycap = Ngaycap,
                        Soqddungtd = Soqddungtd,
                        Ngaydungtd = Ngaydungtd,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.ThamDinhGiaDv.Add(request);
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

        [Route("ThamDinhGia/Donvi/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.dv", "Edit"))
                {
                    var model = _db.ThamDinhGiaDv.FirstOrDefault(p => p.Id == Id);
                    if (model != null)
                    {
                        string result = "<div class='row' id='edit_thongtin'>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Tên đơn vị*</label>";
                        result += "<input type='text' id='tendv_edit' name='tendv_edit' class='form-control' value='" + model.Tendv + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Địa chỉ trụ sở*</label>";
                        result += "<input type='text' id='diachi_edit' name='diachi_edit' class='form-control' value='" + model.Diachi + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-6'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Người đại diện*</label>";
                        result += "<input type='text' id='nguoidaidien_edit' name='nguoidaidien_edit' class='form-control' value='" + model.Nguoidaidien + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-6'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Chức danh đăng ký hành nghề</label>";
                        result += "<input type='text' id='chucvu_edit' name='chucvu_edit' class='form-control' value='" + model.Chucvu + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-6'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Số thẻ</label>";
                        result += "<input type='text' id='sothe_edit' name='sothe_edit' class='form-control' value='" + model.Sothe + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-6'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Ngày cấp</label>";
                        result += "<input type='date' id='ngaycap_edit' name='ngaycap_edit' class='form-control' value='" + Helpers.ConvertDateToStrAjax(model.Ngaycap) + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-6'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Số quyết định dừng theo dõi</label>";
                        result += "<input type='text' id='soqddungtd_edit' name='soqddungtd_edit' class='form-control' value='" + model.Soqddungtd + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-6'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Ngày dừng theo dõi</label>";
                        result += "<input type='date' id='ngaydungtd_edit' name='ngaydungtd_edit' class='form-control' value='" + Helpers.ConvertDateToStrAjax(model.Ngaydungtd) + "'/>";
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

        [Route("ThamDinhGia/Donvi/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Tendv, string Diachi, string Nguoidaidien, string Chucvu, string Sothe, DateTime Ngaycap, string Soqddungtd, DateTime Ngaydungtd)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.dv", "Edit"))
                {
                    var model = _db.ThamDinhGiaDv.FirstOrDefault(t => t.Id == Id);
                    model.Tendv = Tendv;
                    model.Diachi = Diachi;
                    model.Nguoidaidien = Nguoidaidien;
                    model.Chucvu = Chucvu;
                    model.Sothe = Sothe;
                    model.Ngaycap = Ngaycap;
                    model.Soqddungtd = Soqddungtd;
                    model.Ngaydungtd = Ngaydungtd;
                    model.Updated_at = DateTime.Now;
                    _db.ThamDinhGiaDv.Update(model);
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

        [Route("ThamDinhGia/Donvi/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.dv", "Delete"))
                {
                    var model = _db.ThamDinhGiaDv.FirstOrDefault(p => p.Id == id_delete);
                    _db.ThamDinhGiaDv.Remove(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "ThamDinhGiaDv");
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
                            var list_add = new List<CSDLGia_ASP.Models.Manages.ThamDinhGia.ThamDinhGiaDv>();
                          
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

                                list_add.Add(new CSDLGia_ASP.Models.Manages.ThamDinhGia.ThamDinhGiaDv
                                {

                                    Maso = worksheet.Cells[row, 2].Value != null ?
                                                 worksheet.Cells[row,2].Value.ToString().Trim() : "",
                                    Tendv = worksheet.Cells[row, 3].Value != null ?
                                                 worksheet.Cells[row, 3].Value.ToString().Trim() : "",
                                    Nguoidaidien = worksheet.Cells[row, 4].Value != null ?
                                                 worksheet.Cells[row, 4].Value.ToString().Trim() : "",
                                    Sothe = worksheet.Cells[row, 5].Value != null ?
                                                 worksheet.Cells[row, 5].Value.ToString().Trim() : "",
                                    Chucvu = worksheet.Cells[row, 6].Value != null ?
                                                 worksheet.Cells[row, 6].Value.ToString().Trim() : "",
                                    Created_at = DateTime.Now,
                                    Updated_at = DateTime.Now,
                                  
                                });
                               
                            }
                            _db.ThamDinhGiaDv.AddRange(list_add);
                            _db.SaveChanges();
                        }
                    }
                }

                return RedirectToAction("Index", "ThamDinhGiaDv");
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

        }
    }
}
