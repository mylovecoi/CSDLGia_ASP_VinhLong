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

    public class GiaThueDNExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaThueDNExcelController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        public IActionResult Index(string Madv)
        {
            var model = new GiaThueMatDatMatNuoc
            {
                Vitri = "1",
                Diemdau = "2",
                Diemcuoi = "3",
                Mota = "4",
                Dientich = 5,
                Dongia = 6,
                LineStart = 2,
                LineStop = 10000,
                Sheet = 1,
            };
            ViewData["MenuLv1"] = "menu_dg";
            ViewData["MenuLv2"] = "menu_dgtmdmn";
            ViewData["MenuLv3"] = "menu_dgtmdmn_tt";
            ViewData["Madv"] = Madv;
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
                       
                        for (int row = request.LineStart; row <= request.LineStop; row++)
                        {
                            list_add.Add(new GiaThueMatDatMatNuocCt
                            {
                                Mahs = request.Mahs,
                                Trangthai = "CXD",
                                Created_at = DateTime.Now,
                                Updated_at = DateTime.Now,

                                Vitri = worksheet.Cells[row, Int16.Parse(request.Vitri)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Vitri)].Value.ToString().Trim() : "",

                                Diemdau = worksheet.Cells[row, Int16.Parse(request.Diemdau)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Diemdau)].Value.ToString().Trim() : "",

                                Diemcuoi = worksheet.Cells[row, Int16.Parse(request.Diemcuoi)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Diemcuoi)].Value.ToString().Trim() : "",

                                Mota = worksheet.Cells[row, Int16.Parse(request.Mota)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Mota)].Value.ToString().Trim() : "",

                                Dientich = worksheet.Cells[row, Int16.Parse(request.Dientich.ToString())].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Dientich.ToString())].Value) : 0,

                                Dongia1 = worksheet.Cells[row, Int16.Parse(request.Dongia.ToString())].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Dongia.ToString())].Value) : 0,

                            });
                        }
                    }
                }
                _db.GiaThueMatDatMatNuocCt.AddRange(list_add);
                _db.SaveChanges();

                return RedirectToAction("Edit", "GiaThueDN", new { Mahs = request.Mahs });
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }




    }
}
