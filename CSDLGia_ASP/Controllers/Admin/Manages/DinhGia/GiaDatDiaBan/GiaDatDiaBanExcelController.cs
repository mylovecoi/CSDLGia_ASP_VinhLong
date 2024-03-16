using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Migrations;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDatDiaBan
{
    public class GiaDatDiaBanExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaDatDiaBanExcelController(CSDLGiaDBContext db)
        {
            _db = db;
        }


        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giadatdb.thongtin", "Create"))
                {
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaDatDiaBan
                    {
                        Madv = Madv,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Thoidiem = DateTime.Now,
                        Sheet = 1,
                        LineStart = 4,
                        LineStop = 3000,
                    };

                    ViewData["Title"] = "Bảng giá các loại đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_giadatdiaban";
                    ViewData["MenuLv3"] = "menu_giadatdiaban_tt";
                    ViewData["Madv"] = Madv;
                    return View("Views/Admin/Manages/DinhGia/GiaDatDiaBan/Excels/Excel.cshtml", model);

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


        [Route("GiaDatDiaBanExcel/Create")]
        [HttpGet]
        public IActionResult Create(string Madv, string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giadatdb.thongtin", "Create"))
                {

                    ViewData["Title"] = "Thông tin hồ sơ giá xây dựng mới";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dg_xaydungmoi";
                    ViewData["MenuLv3"] = "menu_dg_xaydungmoi_tt";
                    ViewData["Madv"] = Madv;
                    ViewData["Mahs"] = Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["modelct"] = _db.GiaXayDungMoiCt.Where(t => t.Mahs == Mahs);

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
        public async Task<IActionResult> Import(CSDLGia_ASP.Models.Manages.DinhGia.GiaDatDiaBan request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {

                request.LineStart = request.LineStart == 0 ? 1 : request.LineStart;
                var list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaDatDiaBanCt>();
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
                        int stt = 1;
                        for (int row = request.LineStart; row <= request.LineStop; row++)
                        {
                            list_add.Add(new CSDLGia_ASP.Models.Manages.DinhGia.GiaDatDiaBanCt
                            {
                                Mahs = request.Mahs,
                                Mahs = request.Mahs,
                                Trangthai = "CXD",
                                Created_at = DateTime.Now,
                                Updated_at = DateTime.Now,
                                Sapxep = worksheet.Cells[row, 1].Value != null ?  Helpers.ConvertStrToDb(worksheet.Cells[row, 1].Value.ToString().Trim()) : 1,
                                Mota = worksheet.Cells[row, 2].Value != null ?  worksheet.Cells[row, 2].Value.ToString().Trim() : "",
                                Diemdau = worksheet.Cells[row, 3].Value != null ?  worksheet.Cells[row, 3].Value.ToString().Trim() : "",
                                Diemcuoi = worksheet.Cells[row, 4].Value != null ?  worksheet.Cells[row, 4].Value.ToString().Trim() : "",
                                Hesok = worksheet.Cells[row, 5].Value != null ? Helpers.ConvertStrToDb(worksheet.Cells[row,5].Value.ToString().Trim()) : 0,
                                Giavt1 = worksheet.Cells[row, 6].Value != null ? Helpers.ConvertStrToDb(worksheet.Cells[row, 6].Value.ToString().Trim()) : 0,
                                Giavt2 = worksheet.Cells[row, 7].Value != null ? Helpers.ConvertStrToDb(worksheet.Cells[row, 7].Value.ToString().Trim()) : 0,
                                Giavt3 = worksheet.Cells[row, 8].Value != null ? Helpers.ConvertStrToDb(worksheet.Cells[row, 8].Value.ToString().Trim()) : 0,
                                Giavt4 = worksheet.Cells[row, 9].Value != null ? Helpers.ConvertStrToDb(worksheet.Cells[row, 9].Value.ToString().Trim()) : 0,
                                Giavt5 = worksheet.Cells[row, 10].Value != null ? Helpers.ConvertStrToDb(worksheet.Cells[row, 10].Value.ToString().Trim()) : 0,                                
                            });
                        }
                    }

                }

                _db.GiaDatDiaBanCt.AddRange(list_add);
                _db.SaveChanges();
                return RedirectToAction("Create", "GiaDatDiaBan", new { Madv = request.Madv, Mahs = request.Mahs });
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

    }
}
