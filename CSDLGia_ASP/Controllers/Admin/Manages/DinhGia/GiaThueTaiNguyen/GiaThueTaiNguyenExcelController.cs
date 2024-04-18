using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaThueTaiNguyen
{
    public class GiaThueTaiNguyenExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GiaThueTaiNguyenExcelController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetn.thongtin", "Create"))
                {
                    var check = _db.GiaThueTaiNguyenCt.Where(t => t.Trangthai == "CXD" && t.Madv == Madv);
                    if (check != null)
                    {
                        _db.GiaThueTaiNguyenCt.RemoveRange(check);
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

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueTaiNguyen
                    {
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Madv = Madv,
                        LineStart = 4,
                        LineStop = 1000,
                        Sheet = 1,
                    };
                    ViewData["NhomTn"] = _db.GiaThueTaiNguyenNhom.ToList();
                    ViewData["Title"] = "Thông tin giá thuế tài nguyên";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgthuetn";
                    ViewData["MenuLv3"] = "menu_dgthuetn_tt";                   
                    return View("Views/Admin/Manages/DinhGia/GiaThueTaiNguyen/Excels/Excel.cshtml", model);

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


        [Route("GiaThueTaiNguyenExcel/Create")]
        [HttpGet]
        public IActionResult Create(string Madv, string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.xaydungmoi.thongtin", "Create"))
                {
                    ViewData["Title"] = "Thông tin giá thuế tài nguyên";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgthuetn";
                    ViewData["MenuLv3"] = "menu_dgthuetn_tt";
                    ViewData["Madv"] = Madv;
                    ViewData["Mahs"] = Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["modelct"] = _db.GiaThueTaiNguyenCt.Where(t => t.Mahs == Mahs);
     
                    return View("Views/Admin/Manages/DinhGia/GiaThueTaiNguyen/Excels/Create.cshtml");
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
        public async Task<IActionResult> Import(CSDLGia_ASP.Models.Manages.DinhGia.GiaThueTaiNguyen request, IFormFile Ipf1)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {

                if (Ipf1 != null && Ipf1.Length > 0)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string filename = Path.GetFileNameWithoutExtension(Ipf1.FileName);
                    string extension = Path.GetExtension(Ipf1.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/GiaThueTn/", filename);
                    using (var FileStream = new FileStream(path, FileMode.Create))
                    {
                        await Ipf1.CopyToAsync(FileStream);
                    }
                    request.Ipf1 = filename;
                }

                request.LineStart = request.LineStart == 0 ? 1 : request.LineStart;
                var list_add = new List<GiaThueTaiNguyenCt>();
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
                            list_add.Add(new GiaThueTaiNguyenCt
                            {
                                Mahs = request.Mahs,
                                Cap1= worksheet.Cells[row, 2].Value != null ?worksheet.Cells[row, 2].Value.ToString().Trim() : "",
                                Cap2 = worksheet.Cells[row, 3].Value != null ? worksheet.Cells[row, 3].Value.ToString().Trim() : "",
                                Cap3 = worksheet.Cells[row, 4].Value != null ? worksheet.Cells[row, 4].Value.ToString().Trim() : "",
                                Cap4 = worksheet.Cells[row, 5].Value != null ? worksheet.Cells[row, 5].Value.ToString().Trim() : "",
                                Cap5 = worksheet.Cells[row, 6].Value != null ? worksheet.Cells[row, 6].Value.ToString().Trim() : "",
                                Cap6 = worksheet.Cells[row, 7].Value != null ? worksheet.Cells[row, 7].Value.ToString().Trim() : "",
                                Ten = worksheet.Cells[row, 8].Value != null ? worksheet.Cells[row, 8].Value.ToString().Trim() : "",
                                Dvt = worksheet.Cells[row, 9].Value != null ? worksheet.Cells[row, 9].Value.ToString().Trim() : "",
                                Gia = worksheet.Cells[row, 10].Value != null ? Helpers.ConvertStrToDb(worksheet.Cells[row, 10].Value.ToString().Trim()) : 0,
                                Manhom = (request.Manhom == "all" 
                                        ? worksheet.Cells[row, 11].Value != null ? worksheet.Cells[row, 11].Value.ToString().Trim() : ""
                                        : request.Manhom),
                                Madv = request.Madv,
                                Trangthai = "CXD",
                                SapXep = stt++,        
                            }); 
                        }
                    }

                }
                _db.GiaThueTaiNguyenCt.AddRange(list_add);
                //Thông tin hồ sơ
                var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueTaiNguyen
                {
                    Mahs = request.Mahs,
                    Manhom = request.Manhom,
                    Madv = request.Madv,
                    Thoidiem = request.Thoidiem,
                    Soqd = request.Soqd,
                    Soqdlk = request.Soqdlk,
                    Thoidiemlk = request.Thoidiemlk,
                    Cqbh = request.Cqbh,
                    Ghichu = request.Ghichu,
                    Trangthai = "CC",
                    Congbo = "CHUACONGBO",
                    Ipf1 = request.Ipf1,
                    PhanLoaiHoSo = request.PhanLoaiHoSo,
                    CodeExcel = request.CodeExcel,
                    Created_at = DateTime.Now,
                    Updated_at = DateTime.Now,
                };
                _db.GiaThueTaiNguyen.Add(model);
                _db.SaveChanges();
                return RedirectToAction("Edit", "GiaThueTaiNguyen", new { Mahs =  request.Mahs });
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

    }
}
