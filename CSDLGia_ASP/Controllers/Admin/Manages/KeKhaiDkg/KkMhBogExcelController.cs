using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Manages.KeKhaiDkg;
using CSDLGia_ASP.ViewModels.Manages.KeKhaiDkg;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.KkMhBog
{
    public class KkMhBogExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public KkMhBogExcelController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.bog.thongtin", "Create"))
                {
                    var model = new KkMhBogCt
                    {
                        Plhh = "1",
                        Tenhh = "2",
                        Dvt = "3",
                        Gialk = 4,
                        Giakk = 5,
                        Ghichu = "6",

                        LineStart = 2,
                        LineStop = 1000,
                        Sheet = 1,
                    };
                    ViewData["MenuLv1"] = "menu_bog";
                    ViewData["MenuLv2"] = "menu_thongtin";
                    ViewData["Madv"] = Madv;
                    ViewData["Title"] = "Thông tin hồ sơ giá hàng hóa, dịch vụ bình ổn giá";
                    return View("Views/Admin/Manages/KeKhaiDkg/KkMhBog/Excels/Excel.cshtml", model);

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


        [Route("KkMhBogExcel/Create")]
        [HttpGet]
        public IActionResult Create(string Madv, string Mahs, string Manghe, string Phanloai)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.bog.thongtin", "Create"))
                {

                    ViewData["Title"] = "Thông tin hồ sơ giá hàng hóa, dịch vụ bình ổn giá";
                    ViewData["MenuLv1"] = "menu_bog";
                    ViewData["MenuLv2"] = "menu_thongtin";
                    ViewData["Madv"] = Madv;
                    ViewData["Mahs"] = Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["modelct"] = _db.KkMhBogCt.Where(t => t.Mahs == Mahs);

                    return View("Views/Admin/Manages/KeKhaiDkg/KkMhBog/Excels/Create.cshtml");
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


        [Route("BinhOnGia/Create/Excel")]
        [HttpGet]
        public IActionResult Create(string Madv, string Manghe, string Phanloai)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.bog.thongtin", "Create") || Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var model = new VMKkMhBog
                    {
                        Manghe = Manghe,
                        Madv = Madv,
                        Phanloai = Phanloai,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                    };

                    ViewData["Madv"] = Madv;
                    ViewData["Tendn"] = _db.Company.FirstOrDefault(t => t.Madv == Madv).Tendn;
                    ViewData["Manghe"] = Manghe;
                    ViewData["Tennghe"] = _db.DmNgheKd.FirstOrDefault(t => t.Manghe == t.Manghe).Tennghe;
                    ViewData["Title"] = "Giá kê khai mặt hàng bình ổn giá";
                    ViewData["MenuLv1"] = "menu_bog";
                    ViewData["MenuLv2"] = "menu_thongtin";

                    return View("Views/Admin/Manages/KeKhaiDkg/KkMhBog/Excels/Excel.cshtml", model);
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


        [Route("KkMhBogExcel/Import")]
        [HttpPost]
        public async Task<IActionResult> Import(KkMhBogCt request, string Madv, string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {

                request.LineStart = request.LineStart == 0 ? 1 : request.LineStart;
                var list_add = new List<KkMhBogCt>();
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
                        Mahs = request.Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH");

                        for (int row = request.LineStart; row <= request.LineStop; row++)
                        {
                            list_add.Add(new KkMhBogCt
                            {
                                Mahs = Mahs,
                                Trangthai = "CXD",
                                Created_at = DateTime.Now,
                                Updated_at = DateTime.Now,

                                Plhh = worksheet.Cells[row, Int16.Parse(request.Plhh)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Plhh)].Value.ToString().Trim() : "",

                                Tenhh = worksheet.Cells[row, Int16.Parse(request.Tenhh)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Tenhh)].Value.ToString().Trim() : "",

                                Dvt = worksheet.Cells[row, Int16.Parse(request.Dvt)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Dvt)].Value.ToString().Trim() : "",

                                Gialk = worksheet.Cells[row, Int16.Parse(request.Gialk.ToString())].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Gialk.ToString())].Value) : 0,

                                Giakk = worksheet.Cells[row, Int16.Parse(request.Giakk.ToString())].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Giakk.ToString())].Value) : 0,

                                Ghichu = worksheet.Cells[row, Int16.Parse(request.Ghichu)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Ghichu)].Value.ToString().Trim() : "",

                            });
                        }
                    }

                }
                _db.KkMhBogCt.AddRange(list_add);
                _db.SaveChanges();
                return RedirectToAction("Create", "KkMhBogExcel", new { Madv = Madv, Mahs = Mahs });
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

    }
}
