using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaSpDvToiDa
{
    public class GiaSpDvToiDaExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaSpDvToiDaExcelController(CSDLGiaDBContext db)
        {
            _db = db;
        }


        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvtoida.thongtin", "Create"))
                {
                    var model = new CSDLGia_ASP.ViewModels.VMImportExcel
                    {
                        LineStart = 4,
                        LineStop = 1000,
                        Sheet = 1,
                        MaDv = Madv,
                    };
                    ViewData["MenuLv1"] = "menu_spdvtoida";
                    ViewData["MenuLv2"] = "menu_spdvtoida_thongtin";
                    ViewData["Madv"] = Madv;
                    ViewData["Title"] = "Thông tin hồ sơ giá sản phẩm dịch vụ tối đa";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvToiDa/Excels/Excel.cshtml", model);

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
        public async Task<IActionResult> Import(CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvToiDa request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                string Mahs = request.Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH");
                request.LineStart = request.LineStart == 0 ? 1 : request.LineStart;
                var list_add = new List<GiaSpDvToiDaCt>();
                int sheet = request.Sheet == 0 ? 0 : (request.Sheet - 1);
                using (var stream = new MemoryStream())
                {
                    await request.FormFile.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[sheet];
                        var rowcount = worksheet.Dimension.Rows;
                        request.LineStop = request.LineStop > rowcount ? rowcount : request.LineStop;
                        int sapXep = 1;
                        for (int row = request.LineStart; row <= request.LineStop; row++)
                        {
                            list_add.Add(new GiaSpDvToiDaCt
                            {
                                Mahs = Mahs,
                                Sapxep = sapXep++,
                                Trangthai = "CXD",
                                Created_at = DateTime.Now,
                                Updated_at = DateTime.Now,
                              
                                HienThi = worksheet.Cells[row, 1].Value != null ?
                                            worksheet.Cells[row, 1].Value.ToString().Trim() : "",
                                Tenspdv = worksheet.Cells[row, 2].Value != null ?
                                            worksheet.Cells[row, 2].Value.ToString().Trim() : "",
                                Dvt = worksheet.Cells[row, 3].Value != null ?
                                            worksheet.Cells[row, 3].Value.ToString().Trim() : "",
                                Dongia = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 4].Value != null ?
                                                    worksheet.Cells[row, 4].Value.ToString().Trim() : ""),
                                Manhom = worksheet.Cells[row, 5].Value != null ?
                                            worksheet.Cells[row, 5].Value.ToString().Trim() : "",
                                GiaToiDaTheoCuLy1 = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 6].Value != null ?
                                                    worksheet.Cells[row, 6].Value.ToString().Trim() : ""),
                                GiaToiDaTheoCuLy2 = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 7].Value != null ?
                                                    worksheet.Cells[row, 7].Value.ToString().Trim() : ""),
                                GiaToiDaTheoCuLy3 = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 8].Value != null ?
                                                    worksheet.Cells[row, 8].Value.ToString().Trim() : ""),
                                GiaToiDaTheoCuLy4 = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 9].Value != null ?
                                                    worksheet.Cells[row, 9].Value.ToString().Trim() : ""),

                            });
                        }
                    }

                }
                _db.GiaSpDvToiDaCt.AddRange(list_add);
                _db.SaveChanges();
                var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvToiDa
                {
                    Madv = request.Madv,
                    Thoidiem = DateTime.Now,
                    Mahs = Mahs,
                };
                var modelct = _db.GiaSpDvToiDaCt.Where(t => t.Mahs == Mahs);
                model.GiaSpDvToiDaCt = modelct.ToList();
                ViewData["Mahs"] = model.Mahs;
                ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "T");
                ViewData["Title"] = "Bảng giá sản phẩm dịch vụ tối đa";
                ViewData["MenuLv1"] = "menu_spdvtoida";
                ViewData["MenuLv2"] = "menu_spdvtoida_thongtin";
                return View("Views/Admin/Manages/DinhGia/GiaSpDvToiDa/Create.cshtml", model);
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

    }
}
