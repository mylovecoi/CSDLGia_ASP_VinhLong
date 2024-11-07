using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels;
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanha.thongtin", "Create"))
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

                    var model = new VMImportExcel
                    {

                        Sheet = 1,
                        LineStart = 2,
                        LineStop = 10000,
                        MaDv = Madv,

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
        public async Task<IActionResult> Import(VMImportExcel request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                request.LineStart = request.LineStart == 0 ? 1 : request.LineStart;
                var list_add = new List<GiaThueMuaNhaXhCt>();
                int sheet = request.Sheet == 0 ? 0 : (request.Sheet - 1);
                string Mahs = request.MaDv + "_" + DateTime.Now.ToString("yyMMddssmmHH");
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
                            list_add.Add(new GiaThueMuaNhaXhCt
                            {
                                Mahs = Mahs,
                                Madv = request.MaDv,
                                Trangthai = "CXD",
                                Created_at = DateTime.Now,
                                Updated_at = DateTime.Now,

                                Tennha = worksheet.Cells[row, 1].Value != null ?
                                            worksheet.Cells[row, 1].Value.ToString().Trim() : "",

                                Dvthue = worksheet.Cells[row, 2].Value != null ?
                                            worksheet.Cells[row, 2].Value.ToString().Trim() : "",

                                Dvt = worksheet.Cells[row, 3].Value != null ?
                                            worksheet.Cells[row, 3].Value.ToString().Trim() : "",

                                Dongia = Helpers.ConvertStrToDb(worksheet.Cells[row, 4].Value != null ?
                                                    worksheet.Cells[row, 4].Value.ToString().Trim() : ""),

                                Dongiathue = Helpers.ConvertStrToDb(worksheet.Cells[row, 5].Value != null ?
                                                    worksheet.Cells[row, 5].Value.ToString().Trim() : ""),

                                Phanloai = worksheet.Cells[row, 6].Value != null ?
                                            worksheet.Cells[row, 6].Value.ToString().Trim() : "",
                            });
                        }
                    }

                }
                _db.GiaThueMuaNhaXhCt.AddRange(list_add);
                _db.SaveChanges();
                var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMuaNhaXh
                {
                    Madv = request.MaDv,
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
