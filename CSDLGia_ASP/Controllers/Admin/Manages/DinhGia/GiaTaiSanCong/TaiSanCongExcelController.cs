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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.TaiSanCong
{
    public class TaiSanCongExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public TaiSanCongExcelController(CSDLGiaDBContext db)
        {
            _db = db;
        }


        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.taisancong.thongtin", "Create"))
                {
                    var model = new GiaTaiSanCongCt
                    { 
                        Tentaisan = "1",
                        Dacdiem = "2",
                        Giathue = 3,
                        Giaconlai = 4,
                        Giapheduyet = 5,
                        Giaban = 6,
                        LineStart = 2,
                        LineStop = 1000,
                        Sheet = 1,
                    };
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_tsc";
                    ViewData["MenuLv3"] = "menu_giatsc_tt";
                    ViewData["Madv"] = Madv;
                    ViewData["Title"] = "Thông tin hồ sơ giá tài sản công";
                    return View("Views/Admin/Manages/DinhGia/GiaTaiSanCong/Excels/Excel.cshtml", model);

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


        [Route("TaiSanCongExcel/Create")]
        [HttpGet]
        public IActionResult Create(string Madv, string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.taisancong.thongtin", "Create"))
                {

                    ViewData["Title"] = "Thông tin hồ sơ giá tài sản công";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_tsc";
                    ViewData["MenuLv3"] = "menu_giatsc_tt";
                    ViewData["Madv"] = Madv;
                    ViewData["Mahs"] = Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["modelct"] = _db.GiaTaiSanCongCt.Where(t => t.Mahs == Mahs);

                    return View("Views/Admin/Manages/DinhGia/GiaTaiSanCong/Excels/Create.cshtml");
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
        public async Task<IActionResult> Import(GiaTaiSanCongCt request, string Madv, string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {

                request.LineStart = request.LineStart == 0 ? 1 : request.LineStart;
                var list_add = new List<GiaTaiSanCongCt>();
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
                            list_add.Add(new GiaTaiSanCongCt
                            {
                                Mahs = Mahs,
                                Trangthai = "CXD",
                                Created_at = DateTime.Now,
                                Updated_at = DateTime.Now,
                                Tentaisan = worksheet.Cells[row, Int16.Parse(request.Tentaisan)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Tentaisan)].Value.ToString().Trim() : "",

                                Dacdiem = worksheet.Cells[row, Int16.Parse(request.Dacdiem)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Dacdiem)].Value.ToString().Trim() : "",


                                Giathue = worksheet.Cells[row, Int16.Parse(request.Giathue.ToString())].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Giathue.ToString())].Value) : 0,


                                Giapheduyet = worksheet.Cells[row, Int16.Parse(request.Giapheduyet.ToString())].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Giapheduyet.ToString())].Value) : 0,


                                Giaban = worksheet.Cells[row, Int16.Parse(request.Giaban.ToString())].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Giaban.ToString())].Value) : 0,


                            });
                        }
                    }

                }
                _db.GiaTaiSanCongCt.AddRange(list_add);
                _db.SaveChanges();
                return RedirectToAction("Create", "TaiSanCongExcel", new { Madv = Madv, Mahs = Mahs });
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

    }
}
