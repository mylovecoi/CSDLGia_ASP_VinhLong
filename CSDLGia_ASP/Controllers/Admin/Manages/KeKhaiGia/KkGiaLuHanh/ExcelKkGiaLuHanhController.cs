using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using CSDLGia_ASP.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Linq;
using CSDLGia_ASP.ViewModels.Manages.KeKhaiGia;

namespace CSDLGia_ASP.Controllers.Admin.Manages.KeKhaiGia.KkGiaLuHanh
{
    public class ExcelKkGiaLuHanhController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ExcelKkGiaLuHanhController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }


        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgluhanh.giakk", "Create"))
                {
                    var modelct_cxd = _db.KkGiaLuHanhCt.Where(t => t.Madv == Madv && t.Trangthai == "CXD");
                    if (modelct_cxd.Any())
                    {
                        _db.KkGiaLuHanhCt.RemoveRange(modelct_cxd);
                        _db.SaveChanges();
                    }

                    var model_file_cxd = _db.ThongTinGiayTo.Where(t => t.Status == "CXD" && t.Madv == Madv);
                    if (model_file_cxd.Any())
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        foreach (var file in model_file_cxd)
                        {
                            string path_del = Path.Combine(wwwRootPath + "/UpLoad/File/ThongTinGiayTo/", file.FileName);
                            FileInfo fi = new FileInfo(path_del);
                            if (fi != null)
                            {
                                System.IO.File.Delete(path_del);
                                fi.Delete();
                            }
                        }
                        _db.ThongTinGiayTo.RemoveRange(model_file_cxd);
                        _db.SaveChanges();
                    }

                    var model = new CSDLGia_ASP.ViewModels.VMImportExcel
                    {
                        MaDv = Madv,
                        MaNghe = "LUHANH",
                        Sheet = 1,
                        LineStart = 5,
                        LineStop = 3000,
                    };

                    ViewData["Title"] = "Thêm mới hồ sơ kê khai giá dịch vụ lữ hành";
                    ViewData["MenuLv1"] = "menu_kknygia";
                    ViewData["MenuLv2"] = "menu_kkgluhanh";
                    ViewData["MenuLv3"] = "menu_ttluhanh";
                    return View("Views/Admin/Manages/KeKhaiGia/KkGiaLuHanh/Excels/Excel.cshtml", model);

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
                requests.LineStart = requests.LineStart == 0 ? 1 : requests.LineStart;
                int sheet = requests.Sheet == 0 ? 0 : (requests.Sheet - 1);

                string Mahs = requests.MaDv + "_" + DateTime.Now.ToString("yyMMddssmmHH");
                using (var stream = new MemoryStream())
                {
                    await requests.FormFile.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[sheet];
                        if (worksheet != null)
                        {
                            var rowcount = worksheet.Dimension.Rows;
                            requests.LineStop = requests.LineStop > rowcount ? rowcount : requests.LineStop;
                            Regex trimmer = new Regex(@"\s\s+"); // Xóa khoảng trắng thừa trong chuỗi
                            var list_add = new List<CSDLGia_ASP.Models.Manages.KeKhaiGia.KkGiaLuHanhCt>();
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

                                list_add.Add(new CSDLGia_ASP.Models.Manages.KeKhaiGia.KkGiaLuHanhCt
                                {
                                    Mahs = Mahs,
                                    Madv = requests.MaDv,
                                    Trangthai = "CXD",
                                    STTSapxep = line,
                                    STTHienthi = worksheet.Cells[row, 1].Value != null ?
                                                worksheet.Cells[row, 1].Value.ToString().Trim() : "",
                                    Tendvcu = worksheet.Cells[row, 2].Value != null ?
                                                worksheet.Cells[row, 2].Value.ToString().Trim() : "",
                                    Dodaitgian = worksheet.Cells[row, 3].Value != null ?
                                                worksheet.Cells[row, 3].Value.ToString().Trim() : "",
                                    Thoigian = Helpers.ExcelConvertToDate(worksheet.Cells[row, 4].Value != null ?
                                                worksheet.Cells[row, 4].Value.ToString().Trim() : ""),
                                    Giakk = Helpers.ConvertStrToDb(worksheet.Cells[row, 5].Value != null ?
                                                    worksheet.Cells[row, 5].Value.ToString().Trim() : ""),
                                    Qccl = worksheet.Cells[row, 6].Value != null ?
                                                worksheet.Cells[row, 6].Value.ToString().Trim() : "",
                                    Style = strStyle.ToString(),
                                });
                                line++;
                            }
                            _db.KkGiaLuHanhCt.AddRange(list_add);
                            _db.SaveChanges();
                        }
                    }
                }

                var model = new VMKkGia
                {
                    Manghe = requests.MaNghe,
                    Madv = requests.MaDv,
                    Ngaynhap = DateTime.Now,
                    Ngayhieuluc = DateTime.Now,
                    Mahs = Mahs,
                };

                var modellk = _db.KkGia.Where(t => t.Manghe == model.Manghe && t.Madv == model.Madv &&
                (t.Trangthai == "DD" || t.Trangthai == "CB" || t.Trangthai == "HCB")).OrderByDescending(t => t.Ngayhieuluc).FirstOrDefault();
                if (modellk != null)
                {
                    model.Socvlk = modellk.Socv;
                    model.Ngaycvlk = modellk.Ngaynhap;
                }

                var model_ct = _db.KkGiaLuHanhCt.Where(t => t.Mahs == model.Mahs);

                model.KkGiaLuHanhCt = model_ct.ToList();

                int max_sttsapxep = model_ct.Any() ? model_ct.Max(t => t.STTSapxep) : 0;
                ViewData["SapXep"] = max_sttsapxep;
                ViewData["Madv"] = model.Madv;
                ViewData["Tendn"] = _db.Company.FirstOrDefault(t => t.Madv == model.Madv).Tendn;
                ViewData["Manghe"] = model.Manghe;
                ViewData["Title"] = "Thêm mới hồ sơ kê khai giá dịch vụ lữ hành";
                ViewData["MenuLv1"] = "menu_kknygia";
                ViewData["MenuLv2"] = "menu_kkgluhanh";
                ViewData["MenuLv3"] = "menu_ttluhanh";
                return View("Views/Admin/Manages/KeKhaiGia/KkGiaLuHanh/DanhSach/Create.cshtml", model);
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }
    }
}
