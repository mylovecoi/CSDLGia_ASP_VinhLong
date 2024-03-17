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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaSpDvCuThe
{
    public class GiaSpDvCuTheExcelController : Controller
    {

        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        public GiaSpDvCuTheExcelController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.thongtin", "Create"))
                {
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCuThe
                    {
                        Madv = Madv,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Thoidiem = DateTime.Now,
                        Sheet = 1,
                        LineStart = 2,
                        LineStop = 3000,
                    };
                    ViewData["MenuLv1"] = "menu_spdvcuthe";
                    ViewData["MenuLv2"] = "menu_spdvcuthe_thongtin";
                
                    ViewData["Title"] = "Thông tin hồ sơ giá sản phẩm dịch vụ cụ thể";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCuThe/Excels/Excel.cshtml", model);

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


        [Route("GiaSpDvCuTheExcel/Create")]
        [HttpGet]
        public IActionResult Create(string Madv, string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.thongtin", "Create"))
                {

                    ViewData["Title"] = "Thông tin hồ sơ giá sản phẩm dịch vụ cụ thể";
                    ViewData["MenuLv1"] = "menu_spdvcuthe";
                    ViewData["MenuLv2"] = "menu_spdvcuthe_thongtin";
                    ViewData["Madv"] = Madv;
                    ViewData["Mahs"] = Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["modelct"] = _db.GiaSpDvCuTheCt.Where(t => t.Mahs == Mahs);
                
                    ViewData["GiaSpDvCuTheDm"] = _db.GiaSpDvCuTheDm.ToList();

                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCuThe/Excels/Create.cshtml");
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
        public async Task<IActionResult> Import(CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCuThe request, IFormFile Ipf1upload)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {

                request.LineStart = request.LineStart == 0 ? 1 : request.LineStart;
                var list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCuTheCt>();
                int sheet = request.Sheet == 0 ? 0 : (request.Sheet - 1);
                using (var stream = new MemoryStream())
                {
                    await request.FormFile.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        //ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[request.Sheet - 1];
                        var rowcount = worksheet.Dimension.Rows;
                        request.LineStop = request.LineStop > rowcount ? rowcount : request.LineStop;
                      
                        for (int row = request.LineStart; row <= request.LineStop; row++)
                        {
                            list_add.Add(new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCuTheCt
                            {

                                Mahs = request.Mahs,
                                Created_at = DateTime.Now,
                                Manhom = worksheet.Cells[row, 1].Value != null ? worksheet.Cells[row, 1].Value.ToString().Trim() : "",
                                Tt = worksheet.Cells[row, 2].Value != null ? worksheet.Cells[row, 2].Value.ToString().Trim() : "",
                                TenSpDv = worksheet.Cells[row, 3].Value != null ? worksheet.Cells[row, 3].Value.ToString().Trim() : "",
                                Mucgia1 = worksheet.Cells[row, 4].Value != null ? worksheet.Cells[row, 4].Value.ToString().Trim() : "",
                                Mucgia2 = worksheet.Cells[row, 5].Value != null ? worksheet.Cells[row, 5].Value.ToString().Trim() : "",
              
                            });
                        }
                    }

                }
                _db.GiaSpDvCuTheCt.AddRange(list_add);

                if (Ipf1upload != null && Ipf1upload.Length > 0)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string filename = Path.GetFileNameWithoutExtension(Ipf1upload.FileName);
                    string extension = Path.GetExtension(Ipf1upload.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/GiaSpDvCuThe", filename);
                    using (var FileStream = new FileStream(path, FileMode.Create))
                    {
                        await Ipf1upload.CopyToAsync(FileStream);
                    }
                    request.Ipf1 = filename;
                }

                var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCuThe
                {
                    Mahs = request.Mahs,
                    Madv = request.Madv,
                    Soqd = request.Soqd,
                    Thoidiem = request.Thoidiem,
                    Ipf1 = request.Ipf1,

                    Trangthai = "CHT",
                    Congbo = "CHUACONGBO",
                    Created_at = DateTime.Now,
                    Updated_at = DateTime.Now,
                };

                _db.GiaSpDvCuThe.Add(model);
                _db.SaveChanges();
                return RedirectToAction("Edit", "GiaSpDvCuThe", new { Mahs = request.Mahs });
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

    }
}
