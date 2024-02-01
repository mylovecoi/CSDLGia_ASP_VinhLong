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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaHangHoaTaiSieuThi
{
    public class GiaHangHoaTaiSieuThiExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaHangHoaTaiSieuThiExcelController(CSDLGiaDBContext db)
        {
            _db = db;
        }


        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.thongtin", "Create"))
                {
                    var model = new GiaNuocShCt
                    {
                        Doituongsd = "1",
                        Namchuathue = "2",
                        Giachuathue = 3,
                        Namchuathue1 = "4",
                        Giachuathue1 = 5,
                        Namchuathue2 = "6",
                        Giachuathue2 = 7,
                        Namchuathue3 = "8",
                        Giachuathue3 = 9,
                        Namchuathue4 = "10",
                        Giachuathue4 = 11,

                        Sheet = 1,
                        LineStart = 3,
                        LineStop = 1000,  
                    };
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgnsh";
                    ViewData["MenuLv3"] = "menu_dgnsh_tt";
                    ViewData["Madv"] = Madv;
                    ViewData["Title"] = "Thông tin hồ sơ giá nước sạch sinh hoạt";
                    return View("Views/Admin/Manages/DinhGia/GiaNuocSh/Excels/Excel.cshtml", model);

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


        [Route("GiaNuocShExcel/Create")]
        [HttpGet]
        public IActionResult Create(string Madv, string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.thongtin", "Create"))
                {

                    ViewData["Title"] = "Thông tin hồ sơ giá nước sạch sinh hoạt";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgnsh";
                    ViewData["MenuLv3"] = "menu_dgnsh_tt";
                    ViewData["Madv"] = Madv;
                    ViewData["Mahs"] = Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["modelct"] = _db.GiaNuocShCt.Where(t => t.Mahs == Mahs);
    
                    return View("Views/Admin/Manages/DinhGia/GiaNuocSh/Excels/Create.cshtml");
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
        public async Task<IActionResult> Import(GiaNuocShCt request, string Madv, string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {

                request.LineStart = request.LineStart == 0 ? 1 : request.LineStart;
                var list_add = new List<GiaNuocShCt>();
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
                            list_add.Add(new GiaNuocShCt
                            {
                                Mahs = Mahs,
                                Trangthai = "CXD",
                                Created_at = DateTime.Now,
                                Updated_at = DateTime.Now,
                                Doituongsd = worksheet.Cells[row, Int16.Parse(request.Doituongsd)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Doituongsd)].Value.ToString().Trim() : "",

                                Namchuathue = worksheet.Cells[row, Int16.Parse(request.Namchuathue)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Namchuathue)].Value.ToString().Trim() : "",

                                Giachuathue = worksheet.Cells[row, Int16.Parse(request.Giachuathue.ToString())].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Giachuathue.ToString())].Value) : 0,

                                Namchuathue1 = worksheet.Cells[row, Int16.Parse(request.Namchuathue1)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Namchuathue1)].Value.ToString().Trim() : "",

                                Giachuathue1 = worksheet.Cells[row, Int16.Parse(request.Giachuathue1.ToString())].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Giachuathue1.ToString())].Value) : 0,

                                Namchuathue2 = worksheet.Cells[row, Int16.Parse(request.Namchuathue2)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Namchuathue2)].Value.ToString().Trim() : "",

                                Giachuathue2 = worksheet.Cells[row, Int16.Parse(request.Giachuathue2.ToString())].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Giachuathue.ToString())].Value) : 0,

                                Namchuathue3 = worksheet.Cells[row, Int16.Parse(request.Namchuathue3)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Namchuathue)].Value.ToString().Trim() : "",

                                Giachuathue3 = worksheet.Cells[row, Int16.Parse(request.Giachuathue3.ToString())].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Giachuathue.ToString())].Value) : 0,

                                Namchuathue4 = worksheet.Cells[row, Int16.Parse(request.Namchuathue4)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Namchuathue4)].Value.ToString().Trim() : "",

                                Giachuathue4 = worksheet.Cells[row, Int16.Parse(request.Giachuathue4.ToString())].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Giachuathue4.ToString())].Value) : 0,
                            });
                        }
                    }

                }
                _db.GiaNuocShCt.AddRange(list_add);
                _db.SaveChanges();
                return RedirectToAction("Create", "GiaNuocShExcel", new { Madv = Madv, Mahs = Mahs });
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

    }
}
