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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDatPl
{
    public class GiaDatPlExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaDatPlExcelController(CSDLGiaDBContext db)
        {
            _db = db;
        }


        public IActionResult Index(string Madv)
        {
            var model = new GiaDatPhanLoaiCt
            {
                Khuvuc = "1",
                Loaidat = "2",
                Vitri = 3,
                Banggiadat = 4,
                Giacuthe = 5,
                Hesodc = 6,

                LineStart = 2,
                LineStop = 1000,
                Sheet = 1,
            };
            ViewData["MenuLv1"] = "menu_giadat";
            ViewData["MenuLv2"] = "menu_dgdct";
            ViewData["MenuLv3"] = "menu_dgdct_tt";
            ViewData["Madv"] = Madv;
            ViewData["Title"] = "Thông tin hồ sơ giá các loại đất";
            return View("Views/Admin/Manages/DinhGia/GiaDatPl/Excel/Excel.cshtml", model);
        }


        [Route("GiaDatPlExcel/Create")]
        [HttpGet]
        public IActionResult Create(string Madv, string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.datcuthe.thongtin", "Create"))
                {

                    ViewData["Title"] = "Thông tin hồ sơ giá các loại đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgdct";
                    ViewData["MenuLv3"] = "menu_dgdct_tt";
                    ViewData["Madv"] = Madv;
                    ViewData["Mahs"] = Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["modelct"] = _db.GiaDatPhanLoaiCt.Where(t => t.Mahs == Mahs);

                    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/Create.cshtml");
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
        public async Task<IActionResult> Import(GiaDatPhanLoaiCt request, string Madv, string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {

                request.LineStart = request.LineStart == 0 ? 1 : request.LineStart;
                var list_add = new List<GiaDatPhanLoaiCt>();
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
                            list_add.Add(new GiaDatPhanLoaiCt
                            {
                                Mahs = Mahs,
                                Trangthai = "CXD",
                                Created_at = DateTime.Now,
                                Updated_at = DateTime.Now,
                                Khuvuc = worksheet.Cells[row, Int16.Parse(request.Khuvuc)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Khuvuc)].Value.ToString().Trim() : ""

                            });
                        }

                    }

                }

                _db.GiaDatPhanLoaiCt.AddRange(list_add);
                _db.SaveChanges();

                return RedirectToAction("Create", "GiaDatPlExcel", new { Madv = Madv, Mahs = Mahs });
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }


       

    }
}
