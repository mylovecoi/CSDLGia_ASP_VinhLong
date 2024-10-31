using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
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
        private readonly IWebHostEnvironment _hostEnvironment;
        public TaiSanCongExcelController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }


        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.taisancong.thongtin", "Create"))
                {
                    var model_ct_cxd = _db.GiaTaiSanCongCt.Where(t => t.Trangthai == "CXD" && t.Madv == Madv);
                    if (model_ct_cxd.Any())
                    {
                        _db.GiaTaiSanCongCt.RemoveRange(model_ct_cxd);
                        _db.SaveChanges();
                    }
                    var model_file_cxd = _db.ThongTinGiayTo.Where(t => t.Status == "CXD" && t.Madv == Madv);
                    if (model_file_cxd.Any())
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        foreach (var file in model_file_cxd)
                        {
                            string path_del = Path.Combine(wwwRootPath + "/UpLoad/File/ThongTinGiayTo/", file.FileName);
                            FileInfo fi = new FileInfo(path_del);
                            if (fi != null)
                            {
                                System.IO.File.Delete(path_del);
                                fi.Delete();
                            }
                        }
                        _db.ThongTinGiayTo.RemoveRange(model_file_cxd);
                        _db.SaveChanges();
                    }

                    var model = new GiaTaiSanCongCt
                    {
                        Mataisan = "1",
                        Tentaisan = "2",
                        Dacdiem = "3",
                        Giathue = 4,
                        Giaconlai = 5,
                        Giapheduyet = 6,
                        Giaban = 7,
                        LineStart = 2,
                        LineStop = 1000,
                        Sheet = 1,
                        Madv = Madv
                    };
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_tsc";
                    ViewData["MenuLv3"] = "menu_giatsc_tt";
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
                                Mataisan = worksheet.Cells[row, Int16.Parse(request.Mataisan)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Mataisan)].Value.ToString().Trim() : "",
                                Tentaisan = worksheet.Cells[row, Int16.Parse(request.Tentaisan)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Tentaisan)].Value.ToString().Trim() : "",

                                Dacdiem = worksheet.Cells[row, Int16.Parse(request.Dacdiem)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Dacdiem)].Value.ToString().Trim() : "",


                                Giathue = Helpers.ConvertStrToDb(worksheet.Cells[row, 5].Value != null ?
                                                    worksheet.Cells[row, 5].Value.ToString().Trim() : ""),
                                Giapheduyet = Helpers.ConvertStrToDb(worksheet.Cells[row, 6].Value != null ?
                                                    worksheet.Cells[row, 6].Value.ToString().Trim() : ""),
                                Giaban = Helpers.ConvertStrToDb(worksheet.Cells[row, 7].Value != null ?
                                                    worksheet.Cells[row, 7].Value.ToString().Trim() : ""),


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





//namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.TaiSanCong
//{
//    public class TaiSanCongExcelController : Controller
//    {
//        private readonly CSDLGiaDBContext _db;
//        private readonly IWebHostEnvironment _hostEnvironment;


//        public TaiSanCongExcelController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
//        {
//            _db = db;
//            _hostEnvironment = hostEnvironment;
//        }


//        public IActionResult Index(string Madv)
//        {
//            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
//            {
//                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.trogiatrocuoc.thongtin", "Create"))
//                {
//                    var model_ct_cxd = _db.GiaTaiSanCongCt.Where(t => t.Trangthai == "CXD" && t.Madv == Madv);
//                    if (model_ct_cxd.Any())
//                    {
//                        _db.GiaTaiSanCongCt.RemoveRange(model_ct_cxd);
//                        _db.SaveChanges();
//                    }
//                    var model_file_cxd = _db.ThongTinGiayTo.Where(t => t.Status == "CXD" && t.Madv == Madv);
//                    if (model_file_cxd.Any())
//                    {
//                        string wwwRootPath = _hostEnvironment.WebRootPath;
//                        foreach (var file in model_file_cxd)
//                        {
//                            string path_del = Path.Combine(wwwRootPath + "/UpLoad/File/ThongTinGiayTo/", file.FileName);
//                            FileInfo fi = new FileInfo(path_del);
//                            if (fi != null)
//                            {
//                                System.IO.File.Delete(path_del);
//                                fi.Delete();
//                            }
//                        }
//                        _db.ThongTinGiayTo.RemoveRange(model_file_cxd);
//                        _db.SaveChanges();
//                    }
//                    var model = new GiaTaiSanCongCt
//                    {
//                        LineStart = 3,
//                        LineStop = 1000,
//                        Sheet = 1,
//                        Madv = Madv
//                    };
//                    ViewData["MenuLv1"] = "menu_dg";
//                    ViewData["MenuLv2"] = "menu_tsc";
//                    ViewData["MenuLv3"] = "menu_giatsc_tt";
//                    ViewData["Title"] = "Thông tin hồ sơ giá tài sản công";
//                    return View("Views/Admin/Manages/DinhGia/GiaTaiSanCong/Excels/Excel.cshtml", model);

//                }
//                else
//                {
//                    ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
//                    return View("Views/Admin/Error/Page.cshtml");
//                }
//            }
//            else
//            {
//                return View("Views/Admin/Error/SessionOut.cshtml");
//            }
//        }

//        [HttpPost]
//        public async Task<IActionResult> Import(GiaTaiSanCongCt request)
//        {
//            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
//            {

//                request.LineStart = request.LineStart == 0 ? 1 : request.LineStart;

//                string Mahs = request.Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH");
//                int sheet = request.Sheet == 0 ? 0 : (request.Sheet - 1);
//                using (var stream = new MemoryStream())
//                {
//                    await request.FormFile.CopyToAsync(stream);
//                    using (var package = new ExcelPackage(stream))
//                    {
//                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
//                        ExcelWorksheet worksheet = package.Workbook.Worksheets[sheet];
//                        var rowcount = worksheet.Dimension.Rows;
//                        request.LineStop = request.LineStop > rowcount ? rowcount : request.LineStop;
//                        var list_add = new List<GiaTaiSanCongCt>();
//                        for (int row = request.LineStart; row <= request.LineStop; row++)
//                        {
//                            list_add.Add(new GiaTaiSanCongCt
//                            {
//                                Mahs = Mahs,
//                                Trangthai = "CXD",
//                                Created_at = DateTime.Now,
//                                Updated_at = DateTime.Now,
//                                Mataisan = worksheet.Cells[row, Int16.Parse(request.Mataisan)].Value != null ?
//                                            worksheet.Cells[row, Int16.Parse(request.Mataisan)].Value.ToString().Trim() : "",
//                                Tentaisan = worksheet.Cells[row, Int16.Parse(request.Tentaisan)].Value != null ?
//                                            worksheet.Cells[row, Int16.Parse(request.Tentaisan)].Value.ToString().Trim() : "",

//                                Dacdiem = worksheet.Cells[row, Int16.Parse(request.Dacdiem)].Value != null ?
//                                            worksheet.Cells[row, Int16.Parse(request.Dacdiem)].Value.ToString().Trim() : "",


//                                Giathue = Helpers.ConvertStrToDb(worksheet.Cells[row, 5].Value != null ?
//                                                    worksheet.Cells[row, 5].Value.ToString().Trim() : ""),
//                                Giapheduyet = Helpers.ConvertStrToDb(worksheet.Cells[row, 6].Value != null ?
//                                                    worksheet.Cells[row, 6].Value.ToString().Trim() : ""),
//                                Giaban = Helpers.ConvertStrToDb(worksheet.Cells[row, 7].Value != null ?
//                                                    worksheet.Cells[row, 7].Value.ToString().Trim() : ""),
//                                Madv = request.Madv

//                            });
//                        }
//                        _db.GiaTaiSanCongCt.AddRange(list_add);
//                        _db.SaveChanges();
//                    }
//                }

//                var model = new VMDinhGiaTaiSanCong
//                {
//                    Madv = request.Madv,
//                    Mahs = Mahs,
//                    Thoidiem = DateTime.Now
//                };
//                model.GiaTaiSanCongCt = _db.GiaTaiSanCongCt.Where(t => t.Mahs == Mahs).ToList();
//                ViewData["DsDiaBan"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
//                ViewData["Title"] = " Thông tin hồ sơ giá tài sản công";
//                ViewData["MenuLv1"] = "menu_dg";
//                ViewData["MenuLv2"] = "menu_tsc";
//                ViewData["MenuLv3"] = "menu_giatsc_tt";

//                return View("Views/Admin/Manages/DinhGia/GiaTaiSanCong/DanhSach/Create.cshtml", model);
//            }
//            else
//            {
//                return View("Views/Admin/Error/SessionOut.cshtml");
//            }
//        }

//    }
//}
