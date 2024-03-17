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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaThueDN
{
    //2024.03.15 => Xem lại chức năng do thiết kế lại hồ sơ

    public class PhiLePhiExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public PhiLePhiExcelController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        public IActionResult Index(string Madv)
        {
            var model = new GiaThueMatDatMatNuoc
            {
                Madv = Madv,
                Thoidiem = DateTime.Now,
                Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                LineStart = 2,
                LineStop = 10000,
                Sheet = 1,
            };
            ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
            ViewData["MenuLv1"] = "menu_dg";
            ViewData["MenuLv2"] = "menu_dgtmdmn";
            ViewData["MenuLv3"] = "menu_dgtmdmn_tt";
            ViewData["Title"] = "Thông tin hồ sơ giá thuê mặt đất mặt nước";
            return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/Excels/Excel.cshtml", model);
        }


        [Route("GiaDatDNExcel/Create")]
        [HttpGet]
        public IActionResult Create(string Madv, string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.thongtin", "Create"))
                {
                    ViewData["Title"] = "Thông tin hồ sơ giá thuê mặt đất mặt nước";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmdmn";
                    ViewData["MenuLv3"] = "menu_dgtmdmn_tt";
                    ViewData["Madv"] = Madv;
                    ViewData["Mahs"] = Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["modelct"] = _db.GiaThueMatDatMatNuocCt.Where(t => t.Mahs == Mahs);

                    return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/Excels/Create.cshtml");
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
