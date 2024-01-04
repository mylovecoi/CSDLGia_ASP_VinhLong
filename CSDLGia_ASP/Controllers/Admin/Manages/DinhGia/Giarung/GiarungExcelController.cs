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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.Giarung
{
    public class GiarungExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiarungExcelController(CSDLGiaDBContext db)
        {
            _db = db;
        }


        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.rung.thongtin", "Create"))
                {
                    var model = new GiaRungCt
                    {
                        Phanloai = "1",
                        Manhom = "2",
                        Dvthue = "3",
                        Noidung = "4",
                        Dientich = 5,
                        Dientichsd = 6,
                        Dvt = "7",
                        Giatri = 8 ,
                        LineStart = 2,
                        LineStop = 10000,
                        Sheet = 1,
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


        [Route("GiaRungExcel/Create")]
        [HttpGet]
        public IActionResult Create(string Madv, string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.rung.thongtin", "Create"))
                {
                    ViewData["Title"] = "Thông tin hồ sơ giá rừng";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgr";
                    ViewData["MenuLv3"] = "menu_dgr_tt";
                    ViewData["Madv"] = Madv;
                    ViewData["Mahs"] = Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["modelct"] = _db.GiaRungCt.Where(t => t.Mahs == Mahs);
                    return View("Views/Admin/Manages/DinhGia/GiaRung/Excels/Create.cshtml");
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
        public async Task<IActionResult> Import(GiaRungCt request, string Madv, string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {

                request.LineStart = request.LineStart == 0 ? 1 : request.LineStart;
                var list_add = new List<GiaRungCt>();
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
                            list_add.Add(new GiaRungCt
                            {
                                Mahs = Mahs,
                                Trangthai = "CXD",
                                Created_at = DateTime.Now,
                                Updated_at = DateTime.Now,

                                Phanloai = worksheet.Cells[row, Int16.Parse(request.Phanloai)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Phanloai)].Value.ToString().Trim() : "",

                                Manhom = worksheet.Cells[row, Int16.Parse(request.Manhom)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Manhom)].Value.ToString().Trim() : "",

                                Dvthue = worksheet.Cells[row, Int16.Parse(request.Dvthue)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Dvthue)].Value.ToString().Trim() : "",

                                Noidung = worksheet.Cells[row, Int16.Parse(request.Noidung)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Noidung)].Value.ToString().Trim() : "",

                                Dientich = worksheet.Cells[row, Int16.Parse(request.Dientich.ToString())].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Dientich.ToString())].Value) : 0,

                                Dientichsd = worksheet.Cells[row, Int16.Parse(request.Dientichsd.ToString())].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Dientichsd.ToString())].Value) : 0,

                                Dvt = worksheet.Cells[row, Int16.Parse(request.Dvt)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Dvt)].Value.ToString().Trim() : "",

                                Giatri = worksheet.Cells[row, Int16.Parse(request.Giatri.ToString())].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Giatri.ToString())].Value) : 0,

                            });
                        }
                    }

                }
                _db.GiaRungCt.AddRange(list_add);
                _db.SaveChanges();
                return RedirectToAction("Create", "GiarungExcel", new { Madv = Madv, Mahs = Mahs });
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

    }
}
