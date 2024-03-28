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
using CSDLGia_ASP.ViewModels.Manages.DinhGia;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.Giarung
{
    public class GiarungExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GiarungExcelController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }


        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.rung.thongtin", "Create"))
                {
                    var modelct_cxd = _db.GiaRungCt.Where(t => t.Madv == Madv && t.Trangthai == "CXD");
                    if (modelct_cxd.Any())
                    {
                        _db.GiaRungCt.RemoveRange(modelct_cxd);
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
                        Sheet = 1,
                        LineStart = 5,
                        LineStop = 3000,
                    };

                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgr";
                    ViewData["MenuLv3"] = "menu_dgr_tt";
                    ViewData["Madv"] = Madv;
                    ViewData["Title"] = "Thông tin hồ sơ giá rừng";
                    return View("Views/Admin/Manages/DinhGia/GiaRung/Excels/Excel.cshtml", model);

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
                            var list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaRungCt>();
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

                                list_add.Add(new CSDLGia_ASP.Models.Manages.DinhGia.GiaRungCt
                                {
                                    Mahs = Mahs,
                                    Madv = requests.MaDv,
                                    Trangthai = "CXD",
                                    STTSapXep = line,
                                    STTHienThi = worksheet.Cells[row, 1].Value != null ?
                                                worksheet.Cells[row, 1].Value.ToString().Trim() : "",
                                    MoTa = worksheet.Cells[row, 2].Value != null ?
                                                worksheet.Cells[row, 2].Value.ToString().Trim() : "",
                                    GiaRung1 = Helpers.ConvertStrToDb(worksheet.Cells[row, 3].Value != null ?
                                                    worksheet.Cells[row, 3].Value.ToString().Trim() : ""),
                                    GiaRung2 = Helpers.ConvertStrToDb(worksheet.Cells[row, 4].Value != null ?
                                                    worksheet.Cells[row, 4].Value.ToString().Trim() : ""),
                                    GiaRung3 = Helpers.ConvertStrToDb(worksheet.Cells[row, 5].Value != null ?
                                                    worksheet.Cells[row, 5].Value.ToString().Trim() : ""),
                                    GiaRung4 = Helpers.ConvertStrToDb(worksheet.Cells[row, 6].Value != null ?
                                                    worksheet.Cells[row, 6].Value.ToString().Trim() : ""),
                                    GiaRung5 = Helpers.ConvertStrToDb(worksheet.Cells[row, 7].Value != null ?
                                                    worksheet.Cells[row, 7].Value.ToString().Trim() : ""),
                                    GiaRung6 = Helpers.ConvertStrToDb(worksheet.Cells[row, 8].Value != null ?
                                                    worksheet.Cells[row, 8].Value.ToString().Trim() : ""),
                                    GiaChoThue1 = Helpers.ConvertStrToDb(worksheet.Cells[row, 9].Value != null ?
                                                    worksheet.Cells[row, 9].Value.ToString().Trim() : ""),
                                    GiaChoThue2 = Helpers.ConvertStrToDb(worksheet.Cells[row, 10].Value != null ?
                                                    worksheet.Cells[row, 10].Value.ToString().Trim() : ""),
                                    GiaBoiThuong1 = Helpers.ConvertStrToDb(worksheet.Cells[row, 11].Value != null ?
                                                    worksheet.Cells[row, 11].Value.ToString().Trim() : ""),
                                    GiaBoiThuong2 = Helpers.ConvertStrToDb(worksheet.Cells[row, 12].Value != null ?
                                                    worksheet.Cells[row, 12].Value.ToString().Trim() : ""),
                                    GiaBoiThuong3 = Helpers.ConvertStrToDb(worksheet.Cells[row, 13].Value != null ?
                                                    worksheet.Cells[row, 13].Value.ToString().Trim() : ""),
                                    GiaBoiThuong4 = Helpers.ConvertStrToDb(worksheet.Cells[row, 14].Value != null ?
                                                    worksheet.Cells[row, 14].Value.ToString().Trim() : ""),
                                    GiaBoiThuong5 = Helpers.ConvertStrToDb(worksheet.Cells[row, 15].Value != null ?
                                                    worksheet.Cells[row, 15].Value.ToString().Trim() : ""),
                                    GiaBoiThuong6 = Helpers.ConvertStrToDb(worksheet.Cells[row, 16].Value != null ?
                                                    worksheet.Cells[row, 16].Value.ToString().Trim() : ""),
                                    Manhom = worksheet.Cells[row, 17].Value != null ?
                                                worksheet.Cells[row, 17].Value.ToString().Trim() : "",
                                    Style = strStyle.ToString(),
                                });
                                line++;
                            }
                            _db.GiaRungCt.AddRange(list_add);
                            _db.SaveChanges();
                        }
                    }
                }

                var model = new VMDinhGiaRung
                {
                    Madv = requests.MaDv,
                    Thoidiem = DateTime.Now,
                    Mahs = Mahs,
                    Dvt = "Triệu đồng/ha"
                };
                var modelct = _db.GiaRungCt.Where(t => t.Mahs == Mahs);
                model.GiaRungCt = modelct.ToList();

                ViewData["NhomDm"] = _db.GiaRungDm;
                ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                ViewData["Dmdvt"] = _db.DmDvt.ToList();
                ViewData["Title"] = "Thêm mới giá rừng";
                ViewData["MenuLv1"] = "menu_dg";
                ViewData["MenuLv2"] = "menu_dgr";
                ViewData["MenuLv3"] = "menu_dgr_tt";

                return View("Views/Admin/Manages/DinhGia/GiaRung/Create.cshtml", model);
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

    }
}
