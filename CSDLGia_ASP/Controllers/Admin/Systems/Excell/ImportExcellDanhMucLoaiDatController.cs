using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System;
using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems.Excell;
using System.Linq;
using CSDLGia_ASP.Models.Systems;

namespace CSDLGia_ASP.Controllers.Admin.Systems.Excell
{
    public class ImportExcellDanhMucLoaiDatController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public ImportExcellDanhMucLoaiDatController(CSDLGiaDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.datcuthe.danhmuc", "Index"))
                {                                       
                    var models = _db.ExcellDanhMucLoaiDat;    
                    return View("Views/Admin/Systems/Excell/DanhMucLoaiDat/Index.cshtml", models);
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
        public IActionResult Create()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.datcuthe.danhmuc", "Create"))
                {                    
                    var del = _db.ExcellDanhMucLoaiDat;
                    _db.ExcellDanhMucLoaiDat.RemoveRange(del);
                    _db.SaveChanges();
                    var model = new ExcellDanhMucLoaiDat()
                    {
                        Maloaidat="1",
                        Loaidat="2",
                        LineStart = 2,
                        LineStop = 100,
                        Sheet = 1
                    };
                    ViewData["Title"] = "Quản lý nhận dữ liệu từ file Excel";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgdct";
                    ViewData["MenuLv3"] = "menu_dgdct_dm";
                    return View("Views/Admin/Systems/Excell/DanhMucLoaiDat/Create.cshtml", model);
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
        public async Task<IActionResult> Store(ExcellDanhMucLoaiDat requests)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.datcuthe.danhmuc", "Create"))
                {                    
                    requests.LineStart = requests.LineStart == 0 ? 1 : requests.LineStart;
                    var list_add = new List<ExcellDanhMucLoaiDat>();
                    var list_str = new List<string>();
                    int sheet = requests.Sheet == 0 ? 0 : (requests.Sheet - 1);
                    using (var stream = new MemoryStream())
                    {
                        await requests.FormFile.CopyToAsync(stream);
                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[sheet];
                            var rowcount = worksheet.Dimension.Rows;
                            requests.LineStop = requests.LineStop > rowcount ? rowcount : requests.LineStop;
                            Regex trimmer = new Regex(@"\s\s+"); // Xóa khoảng trắng thừa trong chuỗi
                            for (int row = requests.LineStart; row <= requests.LineStop; row++)
                            {
                                list_add.Add(new ExcellDanhMucLoaiDat
                                {                      
                                    Maloaidat = worksheet.Cells[row, Int16.Parse(requests.Maloaidat!)].Value != null ?
                                                worksheet.Cells[row, Int16.Parse(requests.Maloaidat!)].Value.ToString()!.Trim() : "",
                                    Loaidat = worksheet.Cells[row, Int16.Parse(requests.Loaidat!)].Value != null ?
                                                worksheet.Cells[row, Int16.Parse(requests.Loaidat!)].Value.ToString()!.Trim() : "",                                         
                                });
                            }

                        }
                    }
                    _db.ExcellDanhMucLoaiDat.AddRange(list_add);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "ImportExcellDanhMucLoaiDat");
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
        public IActionResult Edit(int Id )
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.datcuthe.danhmuc", "Edit"))
                {
                    var model = _db.ExcellDanhMucLoaiDat.FirstOrDefault(x => x.Id == Id);
                    if (model == null)
                    {
                        ViewData["Messages"] = "Không tìm thấy tài sản cần sửa!";
                        return View("Views/Admin/Error/Page.cshtml");
                    }
                    return View("Views/Admin/Systems/Excell/DanhMucLoaiDat/Edit.cshtml", model);
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
        public IActionResult Update(ExcellDanhMucLoaiDat request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.datcuthe.danhmuc", "Create"))
                {
                   var model = _db.ExcellDanhMucLoaiDat.FirstOrDefault(x=>x.Id == request.Id);
                    if (model == null)
                    {
                        ViewData["Messages"] = "Không tồn tại tài sản cần cập nhật !!!";
                        return View("Views/Admin/Error/Page.cshtml");
                    }
                    model.Maloaidat = request.Maloaidat;
                    model.Loaidat = request.Loaidat;                    
                    _db.SaveChanges();
                    return RedirectToAction("Index", "ImportExcellDanhMucLoaiDat");
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
        public IActionResult Delete(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.datcuthe.danhmuc", "Create"))
                {                    
                    var model = _db.ExcellDanhMucLoaiDat.FirstOrDefault(x => x.Id == Id);
                    if (model == null)
                    {
                        ViewData["Messages"] = "Không tồn tại tài sản cần xóa!";
                        return View("Views/Admin/Error/Page.cshtml");
                    }
                    _db.ExcellDanhMucLoaiDat.Remove(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "ImportExcellDanhMucLoaiDat");
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
        public IActionResult ChuyenQuanLy()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.datcuthe.danhmuc", "Create"))
                {
                    var models = _db.ExcellDanhMucLoaiDat;
                    if (!models.Any())
                    {
                        ViewData["Messages"] = "Không tìm thấy tài sản cần chuyển quản lý !!!";
                        return View("Views/Admin/Error/Page.cshtml");
                    }
                    foreach (var item in models)
                    {             
                        var model = new DmLoaiDat()
                        {
                           Maloaidat = item.Maloaidat,
                           Loaidat = item.Loaidat,
                           Created_at= DateTime.Now,
                           Updated_at= DateTime.Now,
                        };
                        _db.DmLoaiDat.Add(model);
                    }
                    _db.SaveChanges();
                    return RedirectToAction("Index", "DmLoaiDat");
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
