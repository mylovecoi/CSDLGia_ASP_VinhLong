using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaThueDN
{
    //2024.03.15 => Xem lại chức năng do thiết kế lại hồ sơ

    public class GiaThueDNExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaThueDNExcelController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        public IActionResult Index(string Madv)
        {
            var model = new VMImportExcel
            {
                MaDv = Madv,               
                LineStart = 4,
                LineStop = 10000,
                Sheet = 1,
            };
            ViewData["GiaThueDNNhom"] = _db.GiaThueMatDatMatNuocNhom;
            ViewData["MenuLv1"] = "menu_dg";
            ViewData["MenuLv2"] = "menu_dgtmdmn";
            ViewData["MenuLv3"] = "menu_dgtmdmn_tt";
            ViewData["Title"] = "Thông tin hồ sơ giá thuê mặt đất mặt nước";
            return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/Excels/Excel.cshtml", model);
        }
       

        [HttpPost]
        public async Task<IActionResult> Import(GiaThueMatDatMatNuoc request, IFormFile Ipf1)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {

                request.LineStart = request.LineStart == 0 ? 1 : request.LineStart;
                var list_add = new List<GiaThueMatDatMatNuocCt>();
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

                        int stt = 1;
                        for (int row = request.LineStart; row <= request.LineStop; row++)
                        {
                            list_add.Add(new GiaThueMatDatMatNuocCt
                            {
                                Mahs = request.Mahs,
                                SapXep = stt++,
                                Created_at = DateTime.Now,
                                Updated_at = DateTime.Now,
                                HienThi = worksheet.Cells[row, 1].Value != null ? worksheet.Cells[row, 1].Value.ToString().Trim() : "",
                                MaNhom = worksheet.Cells[row, 2].Value != null ? worksheet.Cells[row, 2].Value.ToString().Trim() : "",
                                LoaiDat = worksheet.Cells[row, 3].Value != null ? worksheet.Cells[row, 3].Value.ToString().Trim() : "",
                                Dongia1 = worksheet.Cells[row, 4].Value != null ? Helpers.ConvertStrToDb(worksheet.Cells[row, 4].Value.ToString().Trim()) : 0,
                                Dongia2 = worksheet.Cells[row, 5].Value != null ? Helpers.ConvertStrToDb(worksheet.Cells[row, 5].Value.ToString().Trim()) : 0,
                                Dongia3 = worksheet.Cells[row, 6].Value != null ? Helpers.ConvertStrToDb(worksheet.Cells[row, 6].Value.ToString().Trim()) : 0,
                            });
                        }
                    }
                }
                _db.GiaThueMatDatMatNuocCt.AddRange(list_add);

                var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMatDatMatNuoc
                {
                    Mahs = request.Mahs,
                    Madv = request.Madv,
                    Madiaban = request.Madiaban,
                    Soqd = request.Soqd,
                    Ghichu = request.Ghichu,
                    Thoidiem = request.Thoidiem,
                    Ipf1 = request.Ipf1,
                    Trangthai = "CHT",
                    Congbo = "CHUACONGBO",
                    Created_at = DateTime.Now,
                    Updated_at = DateTime.Now,
                };
                _db.GiaThueMatDatMatNuoc.Add(model);

                _db.SaveChanges();

                return RedirectToAction("Modify", "GiaThueDN", new { Mahs = request.Mahs });
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }




    }
}
