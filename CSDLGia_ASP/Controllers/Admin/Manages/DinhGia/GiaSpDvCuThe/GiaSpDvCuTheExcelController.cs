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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaSpDvCuThe
{
    public class GiaSpDvCuTheExcelController : Controller
    {

        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        public GiaSpDvCuTheExcelController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.thongtin", "Create"))
                {

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCuThe
                    {
                        Madv = Madv,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Thoidiem = DateTime.Now,
                        Sheet = 1,
                        LineStart = 4,
                        LineStop = 3000,
                    };
                    ViewData["MenuLv1"] = "menu_spdvcuthe";
                    ViewData["MenuLv2"] = "menu_spdvcuthe_thongtin";
                    var donVi = _db.DsDonVi.FirstOrDefault(x => x.MaDv == model.Madv);
                    string diaBanApDung = donVi?.DiaBanApDung ?? null;
                    if (!string.IsNullOrEmpty(diaBanApDung))
                    {
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(x => diaBanApDung.Contains(x.MaDiaBan));
                    }
                    else
                    {
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(x => x.Level == "H");
                    }
                    ViewData["Title"] = "Thông tin hồ sơ giá sản phẩm dịch vụ cụ thể";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCuThe/Excels/Excel.cshtml", model);

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
        public async Task<IActionResult> Import(CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCuThe request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {

                request.LineStart = request.LineStart == 0 ? 1 : request.LineStart;
                var list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCuTheCt>();
                int sheet = request.Sheet == 0 ? 0 : (request.Sheet - 1);
                using (var stream = new MemoryStream())
                {
                    await request.FormFile.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        //ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[request.Sheet - 1];
                       
                        var rowcount = worksheet.Dimension.Rows;
                        request.LineStop = request.LineStop > rowcount ? rowcount : request.LineStop;
                        int sTT = 1;
                        for (int row = request.LineStart; row <= request.LineStop; row++)
                        {
                            list_add.Add(new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCuTheCt
                            {
                                Mahs = request.Mahs,
                                Created_at = DateTime.Now,
                                Sapxep = sTT++ ,
                                Tt = worksheet.Cells[row, 1].Value != null ? worksheet.Cells[row, 1].Value.ToString().Trim() : "",
                                TenSpDv = worksheet.Cells[row, 2].Value != null ? worksheet.Cells[row, 2].Value.ToString().Trim() : "",
                                Dvt = worksheet.Cells[row, 3].Value != null ? worksheet.Cells[row, 3].Value.ToString().Trim() : "",

                                Mucgia1 = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 4].Value != null ?
                                                    worksheet.Cells[row, 4].Value.ToString().Trim() : ""),
                                Mucgia2 = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 5].Value != null ?
                                                    worksheet.Cells[row, 5].Value.ToString().Trim() : ""),
                                Mucgia3 = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 6].Value != null ?
                                                    worksheet.Cells[row, 6].Value.ToString().Trim() : ""),
                                Mucgia4 = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 7].Value != null ?
                                                    worksheet.Cells[row, 7].Value.ToString().Trim() : ""),
                                Manhom = worksheet.Cells[row, 8].Value != null ? worksheet.Cells[row, 8].Value.ToString().Trim() : "",
                            });
                        }
                    }
                }
                
                _db.GiaSpDvCuTheCt.AddRange(list_add);
                _db.SaveChanges();

                var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCuThe
                {
                    Madiaban = request.Madiaban,
                    Mahs = request.Mahs,
                    Madv = request.Madv,
                    Soqd = request.Soqd,
                    Thoidiem = request.Thoidiem,
                    PhanLoaiHoSo = request.PhanLoaiHoSo,
                    GhiChu = request.Noidung,
                    Trangthai = "CHT",
                    Congbo = "CHUACONGBO",
                    Created_at = DateTime.Now,
                    Updated_at = DateTime.Now,
                };

                model.GiaSpDvCuTheCt = _db.GiaSpDvCuTheCt.Where(t => t.Mahs == model.Mahs).ToList();
                var donVi = _db.DsDonVi.FirstOrDefault(x => x.MaDv == model.Madv);
                string diaBanApDung = donVi?.DiaBanApDung ?? null;
                if (!string.IsNullOrEmpty(diaBanApDung))
                {
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(x => diaBanApDung.Contains(x.MaDiaBan));
                }
                else
                {
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(x => x.Level == "H");
                }
                //ViewData["Manhom"] = Manhom;
                ViewData["Madv"] = request.Madv;
                ViewData["Mahs"] = model.Mahs;

                ViewData["Title"] = "Bảng giá sản phẩm dịch vụ cụ thể";
                ViewData["MenuLv1"] = "menu_spdvcuthe";
                ViewData["MenuLv2"] = "menu_spdvcuthe_thongtin";
                return View("Views/Admin/Manages/DinhGia/GiaSpDvCuThe/Create.cshtml", model);
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

    }
}
