using CSDLGia_ASP.Database;
using CSDLGia_ASP.Models.Manages.KeKhaiGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.KeKhaiGia.KkGiaEtanol
{
    public class KkGiaEtanolExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public KkGiaEtanolExcelController(CSDLGiaDBContext db)
        {
            _db = db;
        }
        public IActionResult Index(string Madv)
        {
            var model = new KkGiaXmTxdCt
            {
                Tendvcu = "2",
                Qccl = "3",
                Dvt = "4",
                Gialk = 5,
                Giakk = 6,
                LineStart = 4,
                LineStop = 111,
                Sheet = 1,
            };

            ViewData["Madv"] = Madv;
            ViewData["Title"] = "Nhận dữ liệu file Excel";
            ViewData["MenuLv1"] = "menu_kknygia";
            ViewData["MenuLv2"] = "menu_kkgxmtxd";
            ViewData["MenuLv3"] = "menu_giakk";
            return View("Views/Admin/Manages/KeKhaiGia/KkGiaXmTxd/Excel.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Import(KkGiaXmTxdCt request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {

                request.LineStart = request.LineStart == 0 ? 1 : request.LineStart;
                var list_add = new List<KkGiaXmTxdCt>();
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
                            list_add.Add(new KkGiaXmTxdCt
                            {
                                Mahs = request.Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                                Madv = request.Madv,
                                Ghichu = "",
                                Thuevat = "",
                                Trangthai = "CXD",
                                Created_at = DateTime.Now,
                                Updated_at = DateTime.Now,

                                Tendvcu = worksheet.Cells[row, Int16.Parse(request.Tendvcu)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Tendvcu)].Value.ToString().Trim() : "",

                                Qccl = worksheet.Cells[row, Int16.Parse(request.Qccl)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Qccl)].Value.ToString().Trim() : "",

                                Dvt = worksheet.Cells[row, Int16.Parse(request.Dvt)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Dvt)].Value.ToString().Trim() : "",

                                Gialk = worksheet.Cells[row, Int16.Parse(request.Gialk.ToString())].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Gialk.ToString())].Value) : 0,

                                Giakk = worksheet.Cells[row, Int16.Parse(request.Giakk.ToString())].Value != null ?
                                            Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Giakk.ToString())].Value) : 0,
                            });
                        }

                    }

                }
                /*return Ok(list_add);*/
                _db.KkGiaXmTxdCt.AddRange(list_add);
                _db.SaveChanges();

                return RedirectToAction("Create", "KeKhaiGiaXmTxd", new { Madv = request.Madv });
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }
    }
}
