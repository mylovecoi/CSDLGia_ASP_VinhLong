using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaThueTSanCong
{
    public class GiaThueTSanCongExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaThueTSanCongExcelController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetaisancong.thongtin", "Create"))
                {
                    var model = new VMImportExcel
                    {
                        Sheet = 1,
                        LineStart = 2,
                        LineStop = 1000,
                        MaDv = Madv,
                    };
                    ViewData["Title"] = "Danh mục thuê tài sản công";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtsc";
                    ViewData["MenuLv3"] = "menu_dgtsc_tt";
                    ViewData["Madv"] = Madv;
                    return View("Views/Admin/Manages/DinhGia/GiaThueTSC/Excels/Excel.cshtml", model);

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


        [Route("GiaThueTSanCongExcel/Create")]
        [HttpGet]
        public IActionResult Create(string Madv, string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetaisancong.thongtin", "Create"))
                {

                    ViewData["Title"] = "Danh mục thuê tài sản công";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtsc";
                    ViewData["MenuLv3"] = "menu_dgtsc_tt";
                    ViewData["Madv"] = Madv;
                    ViewData["Mahs"] = Mahs;
                    ViewData["GiaThueTaiSanCongDm"] = _db.GiaThueTaiSanCongDm.ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["modelct"] = _db.GiaThueTaiSanCongCt.Where(t => t.Mahs == Mahs);

                    return View("Views/Admin/Manages/DinhGia/GiaThueTsc/Excels/Create.cshtml");
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
        public async Task<IActionResult> Import(VMImportExcel request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                string Mahs = request.MaDv + "_" + DateTime.Now.ToString("yyMMddssmmHH");
                request.LineStart = request.LineStart == 0 ? 1 : request.LineStart;
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
                        var list_add = new List<GiaThueTaiSanCongCt>();
                        for (int row = request.LineStart; row <= request.LineStop; row++)
                        {
                            list_add.Add(new GiaThueTaiSanCongCt
                            {
                                Mahs = Mahs,
                                Madv = request.MaDv,
                                Trangthai = "CXD",
                                Created_at = DateTime.Now,
                                Updated_at = DateTime.Now,

                                Mataisan = worksheet.Cells[row, 1].Value != null ?
                                            worksheet.Cells[row, 1].Value.ToString().Trim() : "",

                                Tentaisan = worksheet.Cells[row, 2].Value != null ?
                                            worksheet.Cells[row, 2].Value.ToString().Trim() : "",

                                Dvthue = worksheet.Cells[row, 3].Value != null ?
                                            worksheet.Cells[row, 3].Value.ToString().Trim() : "",

                                Hdthue = worksheet.Cells[row, 4].Value != null ?
                                            worksheet.Cells[row, 4].Value.ToString().Trim() : "",

                                Ththue = worksheet.Cells[row, 5].Value != null ?
                                            worksheet.Cells[row, 5].Value.ToString().Trim() : "",
                                //Thuetungay = Helpers.ExcelConvertToDate(worksheet.Cells[row, 6].Value != null ?
                                //            worksheet.Cells[row, 6].Value.ToString().Trim() : ""),

                                Dvt = worksheet.Cells[row, 7].Value != null ?
                                            worksheet.Cells[row, 7].Value.ToString().Trim() : "",

                                Dongiathue = worksheet.Cells[row, 8].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, 8].Value) : 0,

                                Sotienthuenam = worksheet.Cells[row, 9].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, 9].Value) : 0,

                            });
                        }
                        _db.GiaThueTaiSanCongCt.AddRange(list_add);
                        _db.SaveChanges();

                    }

                }

                var model = new VMDinhGiaThueTsc
                {
                    Madv = request.MaDv,
                    Thoidiem = DateTime.Now,
                    Mahs = Mahs,
                    Thoigianpd_create = DateTime.Now,
                    Thoigiandg_create = DateTime.Now,
                    Thuetungay_create = DateTime.Now,
                    Thuedenngay_create = DateTime.Now,
                };
                model.GiaThueTaiSanCongCt = _db.GiaThueTaiSanCongCt.Where(t => t.Mahs == Mahs).ToList();

                ViewData["Mahs"] = model.Mahs;
                ViewData["DsDiaBan"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                ViewData["GiaThueTaiSanCongDm"] = _db.GiaThueTaiSanCongDm.ToList();

                ViewData["Title"] = " Thông tin hồ sơ thuê tài sản công";
                ViewData["MenuLv1"] = "menu_dg";
                ViewData["MenuLv2"] = "menu_dgtsc";
                ViewData["MenuLv3"] = "menu_dgtsc_tt";
                return View("Views/Admin/Manages/DinhGia/GiaThueTsc/Create.cshtml", model);
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

    }
}
