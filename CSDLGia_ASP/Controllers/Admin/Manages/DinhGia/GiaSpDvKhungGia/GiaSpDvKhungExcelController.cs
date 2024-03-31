using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaSpDvKhungGia
{
    public class GiaSpDvKhungExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GiaSpDvKhungExcelController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvkhunggia.thongtin", "Create"))
                {
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvKhungGia
                    {
                        Madv = Madv,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Thoidiem = DateTime.Now,
                        Sheet = 1,
                        LineStart = 4,
                        LineStop = 3000,
                    };

                    ViewData["MenuLv1"] = "menu_spdvkhunggia";
                    ViewData["MenuLv2"] = "menu_spdvkhunggia_thongtin";
                    ViewData["Madv"] = Madv;
                    ViewData["Title"] = "Thông tin hồ sơ giá sản phẩm dịch vụ khung giá";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvKhungGia/Excels/Excel.cshtml", model);

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


        [Route("GiaSpDvKhungGiaExcel/Create")]
        [HttpGet]
        public IActionResult Create(string Madv, string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvkhunggia.thongtin", "Create"))
                {

                    ViewData["Title"] = "Thông tin hồ sơ giá sản phẩm dịch vụ khung giá";
                    ViewData["MenuLv1"] = "menu_spdvkhunggia";
                    ViewData["MenuLv2"] = "menu_spdvkhunggia_thongtin";
                    ViewData["Madv"] = Madv;
                    ViewData["Mahs"] = Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["modelct"] = _db.GiaSpDvKhungGiaCt.Where(t => t.Mahs == Mahs);

                    ViewData["GiaSpDvKhungGiaDm"] = _db.GiaSpDvKhungGiaDm.ToList();
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvKhungGia/Excels/Create.cshtml");
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
        public async Task<IActionResult> Import(CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvKhungGia request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {

                request.LineStart = request.LineStart == 0 ? 1 : request.LineStart;
                var list_add = new List<GiaSpDvKhungGiaCt>();
                int sheet = request.Sheet == 0 ? 0 : (request.Sheet - 1);
                using (var stream = new MemoryStream())
                { 
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvKhungGia
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Manhom = request.Manhom,
                        Madiaban = request.Madiaban,
                        Soqd = request.Soqd,
                        Ghichu = request.Ghichu,
                        Thoidiem = request.Thoidiem,
                        Ttqd = request.Ttqd,
                        Trangthai = "CHT",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaSpDvKhungGia.Add(model);

                    //Xử lý nhận excel
                    await request.FormFile.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[sheet];
                        var rowcount = worksheet.Dimension.Rows;
                        request.LineStop = request.LineStop > rowcount ? rowcount : request.LineStop;

                        int stt = 1;
                        for (int row = request.LineStart; row <= request.LineStop; row++)
                        {
                            list_add.Add(new GiaSpDvKhungGiaCt
                            {
                                Mahs = request.Mahs,
                                SapXep = stt++,
                                Created_at = DateTime.Now,
                                Updated_at = DateTime.Now,
                                HienThi = worksheet.Cells[row, 1].Value != null ? worksheet.Cells[row, 1].Value.ToString().Trim() : "",                                
                                Tenspdv = worksheet.Cells[row, 2].Value != null ? worksheet.Cells[row, 2].Value.ToString().Trim() : "",
                                Dvt = worksheet.Cells[row, 3].Value != null ?worksheet.Cells[row, 3].Value.ToString().Trim() : "",
                                Giatoithieu = worksheet.Cells[row, 4].Value != null ? Helpers.ConvertStrToDb(worksheet.Cells[row, 4].Value.ToString().Trim()) : 0,
                                Giatoida = worksheet.Cells[row, 5].Value != null ? Helpers.ConvertStrToDb(worksheet.Cells[row, 5].Value.ToString().Trim()) : 0,
                                Manhom = worksheet.Cells[row, 6].Value != null ? worksheet.Cells[row, 6].Value.ToString().Trim() : "",
                            });
                        }
                    }

                }
                _db.GiaSpDvKhungGiaCt.AddRange(list_add);
                _db.SaveChanges();
                return RedirectToAction("Edit", "GiaSpDvKhungGia", new { Mahs = request.Mahs });
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }
    }
}
