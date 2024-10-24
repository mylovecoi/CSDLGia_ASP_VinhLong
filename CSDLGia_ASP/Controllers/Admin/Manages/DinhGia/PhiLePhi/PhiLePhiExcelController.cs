using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System;
using System.Linq;
using OfficeOpenXml.Style;
using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.PhiLePhi
{
    public class PhiLePhiExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;


        public PhiLePhiExcelController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet("PhiLePhi/NhanExcel")]
        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.philephi.thongtin", "Create"))
                {
                    var model_ct_cxd = _db.PhiLePhiCt.Where(t => t.TrangThai == "CXD" && t.Madv == Madv);
                    if (model_ct_cxd.Any())
                    {
                        _db.PhiLePhiCt.RemoveRange(model_ct_cxd);
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
                        LineStart = 4,
                        LineStop = 1000,
                        Sheet = 1,
                        MaDv = Madv,
                    };
                    ViewData["DanhMucNhom"] = _db.PhiLePhiNhom;
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dg_xaydungmoi";
                    ViewData["MenuLv3"] = "menu_dg_xaydungmoi_tt";
                    ViewData["Title"] = "Thông tin hồ sơ phí lệ phí";
                    return View("Views/Admin/Manages/DinhGia/PhiLePhi/Excels/Excel.cshtml", model);

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
        public async Task<IActionResult> Import(CSDLGia_ASP.ViewModels.VMImportExcel requests)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                requests.LineStart = requests.LineStart == 0 ? 1 : requests.LineStart;
                int sheet = requests.Sheet == 0 ? 0 : (requests.Sheet - 1);
                var list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.PhiLePhiCt>();
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

                            list_add.Add(new CSDLGia_ASP.Models.Manages.DinhGia.PhiLePhiCt
                            {
                                Mahs = Mahs,
                                Madv = requests.MaDv,
                                TrangThai = "CXD",
                                Manhom = requests.MaNhom,
                                SapXep = line,
                                HienThi = worksheet.Cells[row, 1].Value != null ?
                                                     worksheet.Cells[row, 1].Value.ToString().Trim() : "",
                                Tenspdv = worksheet.Cells[row, 2].Value != null ?
                                                     worksheet.Cells[row, 2].Value.ToString().Trim() : "",
                                Dvt = worksheet.Cells[row, 3].Value != null ?
                                                     worksheet.Cells[row, 3].Value.ToString().Trim() : "",
                                Dongia = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 4].Value != null ?
                                                worksheet.Cells[row, 4].Value.ToString().Trim() : ""),
                                Dongia2 = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 5].Value != null ?
                                                worksheet.Cells[row, 5].Value.ToString().Trim() : ""),
                                GhiChu = worksheet.Cells[row, 6].Value != null ?
                                                     worksheet.Cells[row, 6].Value.ToString().Trim() : "",
                               
                                Style = strStyle.ToString(),
                            });
                            line = line + 1;
                        }
                    }
                }
                _db.PhiLePhiCt.AddRange(list_add);
                _db.SaveChanges();
                var model = new CSDLGia_ASP.Models.Manages.DinhGia.PhiLePhi
                {
                    Madv = requests.MaDv,
                    Thoidiem = DateTime.Now,
                    Mahs = Mahs,
                };
                var modelct = _db.PhiLePhiCt.Where(t => t.Mahs == Mahs);
                model.PhiLePhiCt = modelct.ToList();
             
                ViewData["DsDiaBan"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                ViewData["Title"] = " Thông tin hồ sơ phí lệ phí";
                ViewData["MenuLv1"] = "menu_giakhac";
                ViewData["MenuLv2"] = "menu_dglp";
                ViewData["MenuLv3"] = "menu_dglp_tt";
                return View("Views/Admin/Manages/DinhGia/PhiLePhi/DanhSach/Create.cshtml", model);
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }
    }
}
