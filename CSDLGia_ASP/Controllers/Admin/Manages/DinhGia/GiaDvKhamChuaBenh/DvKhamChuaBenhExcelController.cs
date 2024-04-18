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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.DvKhamChuaBenh
{
    public class DvKhamChuaBenhExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public DvKhamChuaBenhExcelController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }


        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khamchuabenh.thongtin", "Create"))
                {
                    var model_ct_cxd = _db.GiaDvKcbCt.Where(t => t.Trangthai == "CXD" && t.Madv == Madv);
                    if (model_ct_cxd.Any())
                    {
                        _db.GiaDvKcbCt.RemoveRange(model_ct_cxd);
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

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaDvKcb
                    {
                        Madv = Madv,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Thoidiem = DateTime.Now,
                        Sheet = 1,
                        LineStart = 4,
                        LineStop = 3000,
                    };
                    ViewData["Title"] = " Thông tin hồ sơ giá dịch vụ khám chữa bệnh";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgkcb";
                    ViewData["MenuLv3"] = "menu_dgkcb_tt";
                    ViewData["Madv"] = Madv;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level == "H");
                    return View("Views/Admin/Manages/DinhGia/GiaDvKhamChuaBenh/Excels/Excel.cshtml", model);
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

        [Route("DvKhamChuaBenhExcel/Create")]
        [HttpGet]
        public IActionResult Create(string Madv, string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khamchuabenh.thongtin", "Create"))
                {
                    ViewData["Title"] = " Thông tin hồ sơ giá dịch vụ khám chữa bệnh";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgkcb";
                    ViewData["MenuLv3"] = "menu_dgkcb_tt";
                    ViewData["Madv"] = Madv;
                    ViewData["Mahs"] = Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["modelct"] = _db.GiaDvKcbCt.Where(t => t.Mahs == Mahs);
 
                    return View("Views/Admin/Manages/DinhGia/GiaDvKhamChuaBenh/Excels/Create.cshtml");
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
        public async Task<IActionResult> Import(GiaDvKcb request, IFormFile Ipf1upload)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {

                request.LineStart = request.LineStart == 0 ? 1 : request.LineStart;
                var list_add = new List<GiaDvKcbCt>();
                int sheet = request.Sheet == 0 ? 0 : (request.Sheet - 1);
                using (var stream = new MemoryStream())
                {
                    await request.FormFile.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        /*ExcelWorksheet worksheet = package.Workbook.Worksheets[sheet];*/
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[request.Sheet - 1];
                        var rowcount = worksheet.Dimension.Rows;
                        request.LineStop = request.LineStop > rowcount ? rowcount : request.LineStop;
                        /*Mahs = request.Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH");*/
                        int stt = 1;
                        for (int row = request.LineStart; row <= request.LineStop; row++)
                        {
                            list_add.Add(new GiaDvKcbCt
                            {
                                Mahs = request.Mahs,
                                Madv = request.Madv,
                                Trangthai = "CXD",
                                Sapxep = stt++,
                                Created_at = DateTime.Now,
                                Updated_at = DateTime.Now,
                                Maspdv = request.Mahs + DateTime.Now.ToString("yyMMddssmmHH"),

                                Hienthi = worksheet.Cells[row, 1].Value != null ? worksheet.Cells[row, 1].Value.ToString().Trim() : "",

                                HienthiTT37 = worksheet.Cells[row, 2].Value != null ? worksheet.Cells[row, 2].Value.ToString().Trim() : "",

                                Madichvu = worksheet.Cells[row, 3].Value != null ? worksheet.Cells[row, 3].Value.ToString().Trim() : "",

                                Tenspdv = worksheet.Cells[row, 4].Value != null ? worksheet.Cells[row, 4].Value.ToString().Trim() : "",

                                Giadv = worksheet.Cells[row, 5].Value != null ? Helpers.ConvertStrToDb(worksheet.Cells[row, 5].Value.ToString().Trim()) : 0,

                                Ghichu = worksheet.Cells[row, 6].Value != null ? worksheet.Cells[row, 6].Value.ToString().Trim() : "",

                                Manhom = worksheet.Cells[row, 7].Value != null ? worksheet.Cells[row, 7].Value.ToString().Trim() : "",
                            });
                        }
                    }

                }
                if (Ipf1upload != null && Ipf1upload.Length > 0)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string filename = Path.GetFileNameWithoutExtension(Ipf1upload.FileName);
                    string extension = Path.GetExtension(Ipf1upload.FileName);
                    filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/GiaDvKcb", filename);
                    using (var FileStream = new FileStream(path, FileMode.Create))
                    {
                        await Ipf1upload.CopyToAsync(FileStream);
                    }
                    request.Ipf1 = filename;
                }

                var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaDvKcb
                {
                    Mahs = request.Mahs,
                    Madv = request.Madv,
                    Soqd = request.Soqd,
                    Mota = request.Mota,
                    Madiaban = request.Madiaban,
                    Thoidiem = request.Thoidiem,
                    Ipf1 = request.Ipf1,
                    Trangthai = "CC",
                    Congbo = "CHUACONGBO",
                    Created_at = DateTime.Now,
                    Updated_at = DateTime.Now,
                };

                _db.GiaDvKcb.Add(model);
                _db.GiaDvKcbCt.AddRange(list_add);
                _db.SaveChanges();
                return RedirectToAction("Edit", "DvKhamChuaBenh", new { Mahs = request.Mahs });
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

    }
}
