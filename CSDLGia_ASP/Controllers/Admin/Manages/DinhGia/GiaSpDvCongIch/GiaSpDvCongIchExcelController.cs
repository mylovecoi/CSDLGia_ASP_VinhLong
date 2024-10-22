using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
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
    public class GiaSpDvCongIchExcelController : Controller
    {

        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        public GiaSpDvCongIchExcelController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.thongtin", "Create"))
                {
                    var check = _db.GiaSpDvCongIchCt.Where(t => t.Trangthai == "CXD" && t.Madv == Madv);
                    if (check.Any())
                    {
                        _db.GiaSpDvCongIchCt.RemoveRange(check);
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
                    var model = new VMImportExcel
                    {
                        MaDv = Madv,
                        Sheet = 1,
                        LineStart = 2,
                        LineStop = 3000,
                    };
                    ViewData["GiaSpDvCongIchNhom"] = _db.GiaSpDvCongIchNhom;
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdvci";
                    ViewData["MenuLv3"] = "menu_dgdvci_tt";
                    ViewData["Title"] = "Thông tin hồ sơ giá sản phẩm dịch vụ công ích";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCongIch/Excels/Excel.cshtml", model);

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
                        var rowcount = worksheet.Dimension.Rows;
                        requests.LineStop = requests.LineStop > rowcount ? rowcount : requests.LineStop;
                        Regex trimmer = new Regex(@"\s\s+"); // Xóa khoảng trắng thừa trong chuỗi
                        var list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCongIchCt>();
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

                            int line = 1;
                            list_add.Add(new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCongIchCt
                            {
                                Mahs = Mahs,
                                Madv = requests.MaDv,
                                Trangthai = "CXD",
                                Sapxep = line,
                                HienThi = worksheet.Cells[row, 1].Value != null ?
                                            worksheet.Cells[row, 1].Value.ToString().Trim() : "",
                                Ten = worksheet.Cells[row, 2].Value != null ?
                                            worksheet.Cells[row, 2].Value.ToString().Trim() : "",
                                Dvt = worksheet.Cells[row, 3].Value != null ?
                                            worksheet.Cells[row, 3].Value.ToString().Trim() : "",
                                Mucgiatu = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 4].Value != null ?
                                            worksheet.Cells[row, 4].Value.ToString().Trim() : ""),
                                Mucgiaden = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 5].Value != null ?
                                            worksheet.Cells[row, 5].Value.ToString().Trim() : ""),
                                Mucgia3 = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 6].Value != null ?
                                            worksheet.Cells[row, 6].Value.ToString().Trim() : ""),
                                Mucgia4 = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 7].Value != null ?
                                            worksheet.Cells[row, 7].Value.ToString().Trim() : ""),
                                Style = strStyle.ToString(),
                                Manhom = worksheet.Cells[row, 8].Value != null ?
                                            worksheet.Cells[row, 8].Value.ToString().Trim() : "",
                                LoaiDoThi = worksheet.Cells[row, 9].Value != null ?
                                            worksheet.Cells[row, 9].Value.ToString().Trim() : "",
                            });
                            line = line + 1;
                        }
                        _db.GiaSpDvCongIchCt.AddRange(list_add);
                        _db.SaveChanges();
                    }
                }
                var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCongIch
                {
                    Madv = requests.MaDv,
                    Thoidiem = DateTime.Now,
                    Mahs = Mahs,
                    Manhom = requests.MaNhom
                };
                var modelct = _db.GiaSpDvCongIchCt.Where(t => t.Mahs == Mahs);
                model.GiaSpDvCongIchCt = modelct.ToList();

                ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "T");
                ViewData["GiaSpDvCongIchNhom"] = _db.GiaSpDvCongIchNhom;
                ViewData["Title"] = "Bảng giá sản phẩm dịch vụ công ích";
                ViewData["MenuLv1"] = "menu_dg";
                ViewData["MenuLv2"] = "menu_dgdvci";
                ViewData["MenuLv3"] = "menu_dgdvci_tt";
                return View("Views/Admin/Manages/DinhGia/GiaSpDvCongIch/Create.cshtml", model);

            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

    }
}
