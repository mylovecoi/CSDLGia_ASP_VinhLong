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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaHhDvCn
{
    public class GiaHhDvCnExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaHhDvCnExcelController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.hhdvcn.thongtin", "Create"))
                {
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvCnCt
                    {
                        Maspdv = "1",
                        Dongia = "2",

                        LineStart = 2,
                        LineStop = 1000,
                        Sheet = 1,
                    };
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_hhdvcn";
                    ViewData["MenuLv3"] = "menu_hhdvcn_tt";
                    ViewData["Madv"] = Madv;
                    ViewData["Title"] = "Thông tin hồ sơ giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành";
                    return View("Views/Admin/Manages/DinhGia/GiaHhDvCn/Excels/Excel.cshtml", model);

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


        [Route("GiaHhDvCnExcel/Create")]
        [HttpGet]
        public IActionResult Create(string Madv, string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.hhdvcn.thongtin", "Create"))
                {

                    ViewData["Title"] = "Thông tin hồ sơ giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_hhdvcn";
                    ViewData["MenuLv3"] = "menu_hhdvcn_tt";
                    ViewData["Madv"] = Madv;
                    ViewData["Mahs"] = Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DanhMucSanPhamDichVu"] = _db.GiaHhDvCnDm.ToList();
                    ViewData["modelct"] = _db.GiaHhDvCnCt.Where(t => t.Mahs == Mahs);

                    return View("Views/Admin/Manages/DinhGia/GiaHhDvCn/Excels/Create.cshtml");
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
        public async Task<IActionResult> Import(CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvCnCt request, string Madv, string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {

                request.LineStart = request.LineStart == 0 ? 1 : request.LineStart;
                var list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvCnCt>();
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
                            list_add.Add(new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvCnCt
                            {
                                Mahs = Mahs,
                                Trangthai = "CXD",
                                Created_at = DateTime.Now,
                                Updated_at = DateTime.Now,

                                Maspdv = worksheet.Cells[row, Int16.Parse(request.Maspdv)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Maspdv)].Value.ToString().Trim() : "",

                                Dongia = worksheet.Cells[row, Int16.Parse(request.Dongia)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Dongia)].Value.ToString().Trim() : "",
                            });
                        }
                    }

                }
                _db.GiaHhDvCnCt.AddRange(list_add);
                _db.SaveChanges();
                return RedirectToAction("Create", "GiaHhDvCnExcel", new { Madv = Madv, Mahs = Mahs });
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

    }
}
