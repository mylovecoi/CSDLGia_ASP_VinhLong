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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaThueTSanCong
{
    public class GiaThueTSanCongExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaThueTSanCongExcelController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetaisancong.thongtin", "Create"))
                {
                    var model = new GiaThueTaiSanCongCt
                    {
                        Mataisan = "1",
                        Dvthue = "2",
                        Hdthue = "3",
                        Ththue = "4",
                        Dvt = "5",
                        Dongiathue = 6,
                        Sotienthuenam = 7,
                        Sheet = 1,
                        LineStart = 2,
                        LineStop = 1000,
                    };
                    ViewData["Title"] = "Danh mục thuê tài sản công";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtsc";
                    ViewData["MenuLv3"] = "menu_dgtsc_tt";
                    ViewData["Madv"] = Madv;
                    return View("Views/Admin/Manages/DinhGia/GiaThueTSC/Excels/Excel.cshtml", model);

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


        [Route("GiaThueTSanCongExcel/Create")]
        [HttpGet]
        public IActionResult Create(string Madv, string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetaisancong.thongtin", "Create"))
                {

                    ViewData["Title"] = "Danh mục thuê tài sản công";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtsc";
                    ViewData["MenuLv3"] = "menu_dgtsc_tt";
                    ViewData["Madv"] = Madv;
                    ViewData["Mahs"] = Mahs;
                    ViewData["GiaThueTaiSanCongDm"] = _db.GiaThueTaiSanCongDm.ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["modelct"] = _db.GiaThueTaiSanCongCt.Where(t => t.Mahs == Mahs);

                    return View("Views/Admin/Manages/DinhGia/GiaThueTsc/Excels/Create.cshtml");
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
        public async Task<IActionResult> Import(GiaThueTaiSanCongCt request, string Madv, string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {

                request.LineStart = request.LineStart == 0 ? 1 : request.LineStart;
                var list_add = new List<GiaThueTaiSanCongCt>();
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
                            list_add.Add(new GiaThueTaiSanCongCt
                            {
                                Mahs = Mahs,
                                Trangthai = "CXD",
                                Created_at = DateTime.Now,
                                Updated_at = DateTime.Now,

                                Mataisan = worksheet.Cells[row, Int16.Parse(request.Mataisan)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Mataisan)].Value.ToString().Trim() : "",

                                Dvthue = worksheet.Cells[row, Int16.Parse(request.Dvthue)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Dvthue)].Value.ToString().Trim() : "",

                                Hdthue = worksheet.Cells[row, Int16.Parse(request.Hdthue)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Hdthue)].Value.ToString().Trim() : "",

                                Ththue = worksheet.Cells[row, Int16.Parse(request.Ththue)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Ththue)].Value.ToString().Trim() : "",

                                Dvt = worksheet.Cells[row, Int16.Parse(request.Dvt)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Dvt)].Value.ToString().Trim() : "",

                                Dongiathue = worksheet.Cells[row, Int16.Parse(request.Dongiathue.ToString())].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Dongiathue.ToString())].Value) : 0,

                                Sotienthuenam = worksheet.Cells[row, Int16.Parse(request.Sotienthuenam.ToString())].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Sotienthuenam.ToString())].Value) : 0,

                            });
                        }
                    }

                }
                _db.GiaThueTaiSanCongCt.AddRange(list_add);
                _db.SaveChanges();
                return RedirectToAction("Create", "GiaThueTSanCongExcel", new { Madv = Madv, Mahs = Mahs });
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

    }
}
