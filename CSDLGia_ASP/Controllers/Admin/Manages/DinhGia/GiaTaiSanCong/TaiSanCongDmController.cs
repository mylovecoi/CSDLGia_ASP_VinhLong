using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CSDLGia_ASP.Database;
using System.Security.Cryptography;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CSDLGia_ASP.ViewModels;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Text.RegularExpressions;
using System.Text;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaTaiSanCong
{
    public class TaiSanCongDmController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public TaiSanCongDmController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DanhMucTaiSanCong")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.taisancong.danhmuc", "Index"))
                {
                    var model = _db.GiaTaiSanCongDm.ToList();
                    ViewData["Title"] = "Danh mục giá tài sản công";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_tsc";
                    ViewData["MenuLv3"] = "menu_giatsc_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaTaiSanCong/DanhMuc/Index.cshtml", model);
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

        [Route("DanhMucTaiSanCong/Store")]
        [HttpPost]
        public JsonResult Store(string Manhom, string Tennhom)
        {
            var model = new GiaTaiSanCongDm
            {
                Mataisan = Manhom,
                Tentaisan = Tennhom,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            _db.GiaTaiSanCongDm.Add(model);
            _db.SaveChanges();
            var data = new { status = "success", message = "Thành công" };
            return Json(data);
        }

        [Route("DanhMucTaiSanCong/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaTaiSanCongDm.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Mã tài sản*</b></label>";
                result += "<input type='text' id='manhom_edit' name='manhom_edit' value='" + model.Mataisan + "' class='form-control'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên tài sản*</b></label>";
                result += "<input type='text' id='tennhom_edit' name='tennhom_edit' value='" + model.Tentaisan + "' class='form-control'/>";
                result += "<input type='text' hidden id='id_edit' name='id_edit' value='" + model.Id + "' class='form-control'/>";
                result += "</div></div></div></div>";


                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Không tìm thấy thông tin cần chỉnh sửa!!!" };
                return Json(data);
            }
        }

        [Route("DanhMucTaiSanCong/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Manhom, string Tennhom)
        {
            var model = _db.GiaTaiSanCongDm.FirstOrDefault(t => t.Id == Id);
            model.Mataisan = Manhom;
            model.Tentaisan = Tennhom;
            model.Updated_at = DateTime.Now;
            _db.GiaTaiSanCongDm.Update(model);
            _db.SaveChanges();
            var data = new { status = "success", message = "Thành công" };
            return Json(data);
        }

        [Route("DanhMucTaiSanCong/Delete")]
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var model = _db.GiaTaiSanCongDm.FirstOrDefault(t => t.Id == Id);
            _db.GiaTaiSanCongDm.Remove(model);
            _db.SaveChanges();
            var data = new { status = "success", message = "Thành công" };
            return Json(data);
        }

        [HttpPost]
        public IActionResult RemoveRange()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.taisancong.danhmuc", "Delete"))
                {
                    var model = _db.GiaTaiSanCongDm;
                    _db.GiaTaiSanCongDm.RemoveRange(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "TaiSanCongDm");
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


        [HttpGet("DanhMucTaiSanCong/NhanExcel")]
        public IActionResult NhanExcel()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.taisancong.danhmuc", "Create"))
                {
                    var model = new VMImportExcel
                    {
                        Sheet = 1,
                        LineStart = 3,
                        LineStop = 1000
                    };
                    ViewData["Title"] = "Danh mục giá tài sản công";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_tsc";
                    ViewData["MenuLv3"] = "menu_giatsc_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaTaiSanCong/DanhMuc/Excel.cshtml", model);
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.taisancong.danhmuc", "Create"))
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
                                var list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaTaiSanCongDm>();
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

                                    list_add.Add(new CSDLGia_ASP.Models.Manages.DinhGia.GiaTaiSanCongDm
                                    {
                                        Mataisan = worksheet.Cells[row, 1].Value != null ?
                                                     worksheet.Cells[row, 1].Value.ToString().Trim() : "",
                                        Tentaisan = worksheet.Cells[row, 2].Value != null ?
                                                     worksheet.Cells[row, 2].Value.ToString().Trim() : "",                                     
                                    });
                                }
                                _db.GiaTaiSanCongDm.AddRange(list_add);
                                _db.SaveChanges();
                            }
                        }
                    }
                    return RedirectToAction("Index", "TaiSanCongDm");
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
