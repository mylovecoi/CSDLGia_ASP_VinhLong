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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaGiaoDichDBDS
{
    public class GiaGiaoDichBDSExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaGiaoDichBDSExcelController(CSDLGiaDBContext db)
        {
            _db = db;
        }


        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaodichbds.thongtin", "Create"))
                {
                    var model = new GiaGiaoDichBDSCt
                    {
                        Ten = "1",
                        Dvt = "2",
                        Gia = 3,
                        Sheet = 1,
                        LineStart = 2,
                        LineStop = 1000,
                       
                    };
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dg_giaodichbds";
                    ViewData["MenuLv3"] = "menu_dg_giaodichbds_tt";
                    ViewData["Madv"] = Madv;
                    ViewData["Title"] = "Thông tin hồ sơ giao dịch bất động sản";
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichBDS/Excels/Excel.cshtml", model);

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

        [Route("GiaGiaoDichBDSExcel/Create")]
        [HttpGet]
        public IActionResult Create(string Madv, string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaodichbds.thongtin", "Create"))
                {

                    ViewData["Title"] = "Thông tin hồ sơ giá giao dịch bất động sản";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dg_giaodichbds";
                    ViewData["MenuLv3"] = "menu_dg_giaodichbds_tt";
                    ViewData["Madv"] = Madv;
                    ViewData["Mahs"] = Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["modelct"] = _db.GiaGiaoDichBDSCt.Where(t => t.Mahs == Mahs);

                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichBDS/Excels/Create.cshtml");
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
        public async Task<IActionResult> Import(GiaGiaoDichBDSCt request, string Madv, string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {

                request.LineStart = request.LineStart == 0 ? 1 : request.LineStart;
                var list_add = new List<GiaGiaoDichBDSCt>();
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
                            list_add.Add(new GiaGiaoDichBDSCt
                            {
                                Mahs = Mahs,
                                Trangthai = "CXD",
                                Created_at = DateTime.Now,
                                Updated_at = DateTime.Now,
                                Ten = worksheet.Cells[row, Int16.Parse(request.Ten)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Ten)].Value.ToString().Trim() : "",

                                Dvt = worksheet.Cells[row, Int16.Parse(request.Dvt)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Dvt)].Value.ToString().Trim() : "",

                                Gia = worksheet.Cells[row, Int16.Parse(request.Gia.ToString())].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Gia.ToString())].Value) : 0,
                             
                            });
                        }
                    }

                }
                _db.GiaGiaoDichBDSCt.AddRange(list_add);
                _db.SaveChanges();
                return RedirectToAction("Create", "GiaGiaoDichBDSExcel", new { Madv = Madv, Mahs = Mahs });
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }



    }
}
