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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.TroGiaTroCuoc
{
    public class TroGiaTroCuocExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;


        public TroGiaTroCuocExcelController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }


        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.trogiatrocuoc.thongtin", "Create"))
                {
                    var model_ct_cxd = _db.GiaTroGiaTroCuocCt.Where(t => t.Trangthai == "CXD" && t.Madv == Madv);
                    if (model_ct_cxd.Any())
                    {
                        _db.GiaTroGiaTroCuocCt.RemoveRange(model_ct_cxd);
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
                    var model = new GiaTroGiaTroCuocCt
                    {
                        Tenspdv = "1",
                        Dongia = 2,
                        LineStart = 2,
                        LineStop = 1000,
                        Sheet = 1,
                        Madv = Madv
                    };
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtgtc";
                    ViewData["MenuLv3"] = "menu_dgtgtc_tt";
                    ViewData["Title"] = "Thông tin hồ sơ giá sản phẩm dịch vụ được trợ giá trợ cước";
                    return View("Views/Admin/Manages/DinhGia/GiaTroGiaTroCuoc/Excels/Excel.cshtml", model);

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
        public async Task<IActionResult> Import(GiaTroGiaTroCuocCt request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {

                request.LineStart = request.LineStart == 0 ? 1 : request.LineStart;

                string Mahs = request.Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH");
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
                        var list_add = new List<GiaTroGiaTroCuocCt>();
                        for (int row = request.LineStart; row <= request.LineStop; row++)
                        {
                            list_add.Add(new GiaTroGiaTroCuocCt
                            {
                                Mahs = Mahs,
                                Trangthai = "CXD",
                                Created_at = DateTime.Now,
                                Updated_at = DateTime.Now,
                                Mota = worksheet.Cells[row, 1].Value != null ?
                                                worksheet.Cells[row, 1].Value.ToString().Trim() : "",
                                Dongia = Helpers.ConvertStrToDb(worksheet.Cells[row, 2].Value != null ?
                                                    worksheet.Cells[row, 2].Value.ToString().Trim() : ""),
                                

                            });
                        }
                        _db.GiaTroGiaTroCuocCt.AddRange(list_add);
                        _db.SaveChanges();

                    }

                }

                var model = new VMDinhGiaTroGiaTroCuoc
                {
                    Madv = request.Madv,
                    Mahs = request.Mahs,
                    Thoidiem = DateTime.Now
                };
                model.GiaTroGiaTroCuocCt = _db.GiaTroGiaTroCuocCt.Where(t => t.Mahs == Mahs).ToList();
                ViewData["DsDiaBan"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                ViewData["Title"] = " Thông tin hồ Mức trợ giá trợ cước";
                ViewData["MenuLv1"] = "menu_dg";
                ViewData["MenuLv2"] = "menu_dgtgtc";
                ViewData["MenuLv3"] = "menu_dgtgtc_tt";

                return View("Views/Admin/Manages/DinhGia/GiaTroGiaTroCuoc/Create.cshtml", model);
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

    }
}
