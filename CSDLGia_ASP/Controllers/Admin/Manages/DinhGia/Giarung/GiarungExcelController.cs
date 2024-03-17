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
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaRung
                    {
                        Madv = Madv,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Thoidiem = DateTime.Now,
                        Sheet = 1,
                        LineStart = 4,
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
        public async Task<IActionResult> Import(CSDLGia_ASP.Models.Manages.DinhGia.GiaRung request, IFormFile Ipf1)
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
                        for (int row = request.LineStart; row <= request.LineStop; row++)
                        {
                            list_add.Add(new GiaRungCt
                            {
                                Mahs = request.Mahs,
                                Trangthai = "CXD",
                                Created_at = DateTime.Now,
                                Updated_at = DateTime.Now,

                                Phanloai = worksheet.Cells[row, 2].Value != null ? worksheet.Cells[row, 2].Value.ToString().Trim() : "",

                                Manhom = worksheet.Cells[row, 3].Value != null ? worksheet.Cells[row, 3].Value.ToString().Trim() : "",

                                Dvthue = worksheet.Cells[row, 4].Value != null ? worksheet.Cells[row, 4].Value.ToString().Trim() : "",

                                Noidung = worksheet.Cells[row, 5].Value != null ? worksheet.Cells[row, 5].Value.ToString().Trim() : "",

                                Dientich = worksheet.Cells[row, 6].Value != null ? Helpers.ConvertStrToDb(worksheet.Cells[row, 6].Value.ToString().Trim()) : 0,

                                Dientichsd = worksheet.Cells[row, 7].Value != null ? Helpers.ConvertStrToDb(worksheet.Cells[row, 7].Value.ToString().Trim()) : 0,

                                Dvt = worksheet.Cells[row, 8].Value != null ? worksheet.Cells[row, 8].Value.ToString().Trim() : "",

                                Dongia = worksheet.Cells[row, 9].Value != null ? Helpers.ConvertStrToDb(worksheet.Cells[row, 9].Value.ToString().Trim()) : 0,

                            });
                        }
                    }

                    if (Ipf1 != null && Ipf1.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf1.FileName);
                        string extension = Path.GetExtension(Ipf1.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf1.CopyToAsync(FileStream);
                        }
                        request.Ipf1 = filename;
                    }

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaRung
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Manhom = request.Manhom,
                        Madiaban = request.Madiaban,
                        Soqd = request.Soqd,
                        Ghichu = request.Ghichu,
                        Thoidiem = request.Thoidiem,
                        Noidung = request.Noidung,
                        Thongtin = request.Thongtin,
                        Ipf1 = request.Ipf1,
                        Trangthai = "CHT",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaRung.Add(model);
                }
                _db.GiaRungCt.AddRange(list_add);
                _db.SaveChanges();
                return RedirectToAction("Modify", "Giarung", new { Mahs = request.Mahs });
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

    }
}
