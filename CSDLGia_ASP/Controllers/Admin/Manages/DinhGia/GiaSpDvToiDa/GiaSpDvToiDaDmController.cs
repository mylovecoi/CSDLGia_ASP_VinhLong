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
using System.Security.Cryptography.Xml;
//using OfficeOpenXml;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaSpDvToiDa
{
    public class GiaSpDvToiDaDmController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _env;

        public GiaSpDvToiDaDmController(CSDLGiaDBContext db, IWebHostEnvironment hostingEnv)
        {
            _db = db;
            _env = hostingEnv;
        }

        [Route("GiaSpDvToiDaDmCt")]
        [HttpGet]
        public IActionResult Index(string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvtoida.danhmuc", "Index"))
                {
                    var model = _db.GiaSpDvToiDaDm.Where(t => t.Manhom == Manhom).ToList();

                    ViewData["Manhom"] = Manhom;
                    ViewData["Tennhom"] = _db.GiaSpDvToiDaNhom.FirstOrDefault(t => t.Manhom == Manhom).Tennhom;
                    ViewData["Title"] = "Danh mục chi tiết nhóm sản phẩm dịch vụ tối đa";
                    ViewData["MenuLv1"] = "menu_spdvtoida";
                    ViewData["MenuLv2"] = "menu_spdvtoida_dm";
                    ViewData["Donvitinh"] = _db.DmDvt.ToList();
                    ViewData["PhanLoaiDichVu"] = _db.GiaSpDvToiDaDm.ToList();
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvToiDa/DanhMuc/ChiTiet/Index.cshtml", model);
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

        [Route("GiaSpDvToiDaDmCt/Store")]
        [HttpPost]
        public IActionResult Store(CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvToiDaDm request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.GiaSpDvToiDaDm.FirstOrDefault(x => x.Id == request.Id);
                if (model != null)
                {
                    model.Tenspdv = request.Tenspdv;
                    model.Dvt = request.Dvt;
                    model.HienThi = request.HienThi;
                    model.Sapxep = request.Sapxep;
                    _db.GiaSpDvToiDaDm.Update(model);
                }
                else
                {
                    var chitiet = new GiaSpDvToiDaDm
                    {
                        Manhom = request.Manhom,
                        Dvt = request.Dvt,
                        Tenspdv = request.Tenspdv,
                        HienThi = request.HienThi,
                        Sapxep = request.Sapxep,
                        Maspdv = DateTime.Now.ToString("yyMMddfffssmmHH"),
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaSpDvToiDaDm.Add(chitiet);
                }
                _db.SaveChanges();
                return RedirectToAction("Index", "GiaSpDvToiDaDm", new { Manhom = request.Manhom });
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

        [Route("GiaSpDvToiDaDmCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaSpDvToiDaDm.FirstOrDefault(t => t.Id == Id);
            // Trả về JSON cho JavaScript
            return Json(model);
        }

        //[Route("GiaSpDvToiDaDmCt/Update")]
        //[HttpPost]
        //public JsonResult Update(int Id, string Tenspdv, string Dvt)
        //{
        //    if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
        //    {
        //        if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvtoida.danhmuc", "Edit"))
        //        {
        //            var model = _db.GiaSpDvToiDaDm.FirstOrDefault(t => t.Id == Id);
        //            model.Tenspdv = Tenspdv;

        //            model.Dvt = Dvt;

        //            model.Updated_at = DateTime.Now;
        //            _db.GiaSpDvToiDaDm.Update(model);
        //            _db.SaveChanges();

        //            var data = new { status = "success", message = "Cập nhật thành công!" };
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

        [Route("GiaSpDvToiDaDmCt/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvtoida.danhmuc", "Delete"))
                {
                    var model = _db.GiaSpDvToiDaDm.FirstOrDefault(p => p.Id == id_delete);
                    _db.GiaSpDvToiDaDm.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaSpDvToiDaDm", new { Manhom = model.Manhom });
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

        [Route("GiaSpDvToiDaDmCt/Lock")]
        [HttpPost]
        public IActionResult Lock(string Manhom, string Theodoi)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvtoida.danhmuc", "Edit"))
                {
                    var model = _db.GiaSpDvToiDaDm.Where(t => t.Manhom == Manhom).ToList();
                    model.ForEach(t => { t.Hientrang = Theodoi; });
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvtoida.danhmuc", "Create"))
                {
                    var model = new CSDLGia_ASP.ViewModels.VMImportExcel
                    {
                        LineStart = 4,
                        LineStop = 1000,
                        Sheet = 1,
                        MaNhom = Manhom,
                        TenNhom = _db.GiaSpDvToiDaNhom.FirstOrDefault(t => t.Manhom == Manhom)?.Tennhom ?? ""
                    };

                    ViewData["Title"] = "Danh mục chi tiết nhóm sản phẩm dịch vụ tối đa";
                    ViewData["MenuLv1"] = "menu_spdvtoida";
                    ViewData["MenuLv2"] = "menu_spdvtoida_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvToiDa/DanhMuc/Excel.cshtml", model);

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
                            var list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvToiDaDm>();
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

                                list_add.Add(new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvToiDaDm
                                {
                                    Sapxep = line,
                                    HienThi = worksheet.Cells[row, 1].Value != null ?
                                                 worksheet.Cells[row, 1].Value.ToString().Trim() : "",
                                    Tenspdv = worksheet.Cells[row, 2].Value != null ?
                                                 worksheet.Cells[row, 2].Value.ToString().Trim() : "",
                                    Dvt = worksheet.Cells[row, 3].Value != null ?
                                                 worksheet.Cells[row, 3].Value.ToString().Trim() : "",
                                    Style = strStyle.ToString(),
                                    Manhom = requests.MaNhom,
                                    Maspdv = requests.MaNhom + line,
                                });
                                line++;
                            }
                            _db.GiaSpDvToiDaDm.AddRange(list_add);
                            _db.SaveChanges();
                        }
                    }
                }

                return RedirectToAction("Index", "GiaSpDvToiDaDm", new { Manhom = requests.MaNhom });
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }

        }
    }
}
