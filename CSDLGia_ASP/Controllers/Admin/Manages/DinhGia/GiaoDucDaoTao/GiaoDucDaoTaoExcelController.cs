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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaoDucDaoTao
{
    public class GiaoDucDaoTaoExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaoDucDaoTaoExcelController(CSDLGiaDBContext db)
        {
            _db = db;
        }


        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaoducdaotao.thongtin", "Create"))
                {
                    var model = new GiaDvGdDtCt
                    {
                        Maspdv = "1",
                        Namapdung1 = "2",
                        Giathanhthi1 = 3,
                        Gianongthon1 = 4,
                        Giamiennui1 = 5,
                        Namapdung2 = "6",
                        Giathanhthi2 = 7,
                        Gianongthon2 = 8,
                        Giamiennui2 = 9,
                        Namapdung3 = "10",
                        Giathanhthi3 = 11,
                        Gianongthon3 = 12,
                        Giamiennui3 = 13,

                        Sheet = 1,
                        LineStart = 2,
                        LineStop = 1000,

                    };
                    ViewData["Title"] = " Thông tin hồ sơ giá dịch vụ giáo dục đào tạo";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dggddt";
                    ViewData["MenuLv3"] = "menu_dggddt_tt";
                    ViewData["Madv"] = Madv;
                    return View("Views/Admin/Manages/DinhGia/GiaoDucDaoTao/Excels/Excel.cshtml", model);

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


        [Route("GiaoDucDaoTaoExcel/Create")]
        [HttpGet]
        public IActionResult Create(string Madv, string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaoducdaotao.thongtin", "Create"))
                {

                    ViewData["Title"] = " Thông tin hồ sơ giá dịch vụ giáo dục đào tạo";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dggddt";
                    ViewData["MenuLv3"] = "menu_dggddt_tt";
                    ViewData["Madv"] = Madv;
                    ViewData["Mahs"] = Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["modelct"] = _db.GiaDvGdDtCt.Where(t => t.Mahs == Mahs);
                    return View("Views/Admin/Manages/DinhGia/GiaXayDungMoi/Excels/Create.cshtml");
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
        public async Task<IActionResult> Import(GiaDvGdDtCt request, string Madv, string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {

                request.LineStart = request.LineStart == 0 ? 1 : request.LineStart;
                var list_add = new List<GiaDvGdDtCt>();
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
                            list_add.Add(new GiaDvGdDtCt
                            {
                                Mahs = Mahs,
                                Trangthai = "CXD",
                                Created_at = DateTime.Now,
                                Updated_at = DateTime.Now,

                                Maspdv = worksheet.Cells[row, Int16.Parse(request.Maspdv)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Maspdv)].Value.ToString().Trim() : "",

                                Namapdung1 = worksheet.Cells[row, Int16.Parse(request.Namapdung1)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Namapdung1)].Value.ToString().Trim() : "",

                                Giathanhthi1 = worksheet.Cells[row, Int16.Parse(request.Giathanhthi1.ToString())].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Giathanhthi1.ToString())].Value) : 0,

                                Gianongthon1 = worksheet.Cells[row, Int16.Parse(request.Gianongthon1.ToString())].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Gianongthon1.ToString())].Value) : 0,

                                Giamiennui1 = worksheet.Cells[row, Int16.Parse(request.Giamiennui1.ToString())].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Giamiennui1.ToString())].Value) : 0,

                                Namapdung2 = worksheet.Cells[row, Int16.Parse(request.Namapdung2)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Namapdung1)].Value.ToString().Trim() : "",

                                Giathanhthi2 = worksheet.Cells[row, Int16.Parse(request.Giathanhthi2.ToString())].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Giathanhthi1.ToString())].Value) : 0,

                                Gianongthon2 = worksheet.Cells[row, Int16.Parse(request.Gianongthon2.ToString())].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Gianongthon1.ToString())].Value) : 0,

                                Giamiennui2 = worksheet.Cells[row, Int16.Parse(request.Giamiennui2.ToString())].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Giamiennui1.ToString())].Value) : 0,

                                Namapdung3 = worksheet.Cells[row, Int16.Parse(request.Namapdung3)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Namapdung1)].Value.ToString().Trim() : "",

                                Giathanhthi3 = worksheet.Cells[row, Int16.Parse(request.Giathanhthi3.ToString())].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Giathanhthi1.ToString())].Value) : 0,

                                Gianongthon3 = worksheet.Cells[row, Int16.Parse(request.Gianongthon3.ToString())].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Gianongthon1.ToString())].Value) : 0,

                                Giamiennui3 = worksheet.Cells[row, Int16.Parse(request.Giamiennui3.ToString())].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Giamiennui1.ToString())].Value) : 0,



                            });
                        }
                    }

                }
                _db.GiaDvGdDtCt.AddRange(list_add);
                _db.SaveChanges();
                return RedirectToAction("Create", "GiaXayDungMoiExcel", new { Madv = Madv, Mahs = Mahs });
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

    }
}
