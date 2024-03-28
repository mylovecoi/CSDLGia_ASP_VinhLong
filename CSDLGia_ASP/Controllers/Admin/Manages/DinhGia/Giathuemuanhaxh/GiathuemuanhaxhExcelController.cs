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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiathuemuanhaxhExcel
{
    public class GiathuemuanhaxhExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GiathuemuanhaxhExcelController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }


        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanhaxh.thongtin", "Create"))
                {
                    var model_ct_cxd = _db.GiaThueMuaNhaXhCt.Where(t => t.Trangthai == "CXD" && t.Madv == Madv);
                    if (model_ct_cxd.Any())
                    {
                        _db.GiaThueMuaNhaXhCt.RemoveRange(model_ct_cxd);
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

                    var model = new GiaThueMuaNhaXhCt
                    {
                        Maso = "1",
                        Dvthue = "2",
                        Dvt = "3",
                        Dongia = 4,
                        Dongiathue = 5,
                        Sheet = 1,
                        LineStart = 2,
                        LineStop = 10000,
                        Madv = Madv
                    };

                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmnxh";
                    ViewData["MenuLv3"] = "menu_dgtmnxh_tt";
                    ViewData["Title"] = "Thông tin hồ sơ giá thuê mua nhà xã hội";
                    return View("Views/Admin/Manages/DinhGia/GiaThueMuaNhaXh/Excels/Excel.cshtml", model);

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
        public async Task<IActionResult> Import(GiaThueMuaNhaXhCt request, string Madv, string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                request.LineStart = request.LineStart == 0 ? 1 : request.LineStart;
                var list_add = new List<GiaThueMuaNhaXhCt>();
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
                            list_add.Add(new GiaThueMuaNhaXhCt
                            {
                                Mahs = Mahs,
                                Trangthai = "CXD",
                                Created_at = DateTime.Now,
                                Updated_at = DateTime.Now,

                                Maso = worksheet.Cells[row, Int16.Parse(request.Maso)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Maso)].Value.ToString().Trim() : "",

                                Dvthue = worksheet.Cells[row, Int16.Parse(request.Dvthue)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Dvthue)].Value.ToString().Trim() : "",

                                Dvt = worksheet.Cells[row, Int16.Parse(request.Dvt)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Dvt)].Value.ToString().Trim() : "",

                                Dongia = worksheet.Cells[row, Int16.Parse(request.Dongia.ToString())].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Dongia.ToString())].Value) : 0,

                                Dongiathue = worksheet.Cells[row, Int16.Parse(request.Dongiathue.ToString())].Value != null ?
                                           Convert.ToInt32(worksheet.Cells[row, Int16.Parse(request.Dongiathue.ToString())].Value) : 0,
                            });
                        }
                    }

                }
                _db.GiaThueMuaNhaXhCt.AddRange(list_add);
                _db.SaveChanges();
                var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMuaNhaXh
                {
                    Madv = Madv,
                    Thoidiem = DateTime.Now,
                    Mahs = Mahs
                };
                model.GiaThueMuaNhaXhCt = _db.GiaThueMuaNhaXhCt.Where(t => t.Mahs == Mahs).ToList();


                ViewData["DmDvt"] = _db.DmDvt.ToList();
                ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                ViewData["Dmtmnxh"] = _db.GiaThueMuaNhaXhDm.ToList();
                ViewData["Title"] = "Thêm mới giá thuê mua nhà xã hội";
                ViewData["MenuLv1"] = "menu_dg";
                ViewData["MenuLv2"] = "menu_dgtmnxh";
                ViewData["MenuLv3"] = "menu_dgtmnxh_tt";
                return View("Views/Admin/Manages/DinhGia/GiaThueMuaNhaXh/Create.cshtml", model);
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

    }
}
