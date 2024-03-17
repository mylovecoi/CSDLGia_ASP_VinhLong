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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaTrungThauHhDv
{
    public class GiaTrungThauHhDvExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GiaTrungThauHhDvExcelController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }


        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.xaydungmoi.thongtin", "Create"))
                {
                    var model = new GiaMuaTaiSan
                    {
                        Madv = Madv,
                        Ngayqd = DateTime.Now,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        LineStart = 4,
                        LineStop = 1000,
                        Sheet = 1,
                    };
                    ViewData["Title"] = " Thông tin hồ sơ giá trúng thầu hàng hóa dịch vụ";
                    ViewData["MenuLv1"] = "menu_mts";
                    ViewData["MenuLv2"] = "menu_giamts_tt";
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level == "H");
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauHhDv/Excels/Excel.cshtml", model);

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
        public async Task<IActionResult> Import(GiaMuaTaiSan request, IFormFile Ipf1)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {

                request.LineStart = request.LineStart == 0 ? 1 : request.LineStart;
                var list_add = new List<GiaMuaTaiSanCt>();
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
                            list_add.Add(new GiaMuaTaiSanCt
                            {
                                Mahs = request.Mahs,
                                HienThi = worksheet.Cells[row, 1].Value != null ? worksheet.Cells[row, 1].Value.ToString().Trim() : "",
                                Mota = worksheet.Cells[row, 2].Value != null ? worksheet.Cells[row, 2].Value.ToString().Trim() : "",
                                Dvt = worksheet.Cells[row, 3].Value != null ? worksheet.Cells[row, 3].Value.ToString().Trim() : "",                                
                                KhoiLuong = worksheet.Cells[row, 4].Value != null ? Helpers.ConvertStrToDb(worksheet.Cells[row, 4].Value.ToString().Trim()) : 0,
                                DonGia = worksheet.Cells[row, 5].Value != null ? Helpers.ConvertStrToDb(worksheet.Cells[row, 5].Value.ToString().Trim()) : 0,                                
                                SapXep = stt++,
                            });
                        }
                    }
                    //Xử lý hồ sơ
                    if (Ipf1 != null && Ipf1.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf1.FileName);
                        string extension = Path.GetExtension(Ipf1.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf1.CopyToAsync(FileStream);
                        }
                        request.Ipf1 = filename;
                    }
                    var model = new GiaMuaTaiSan
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Madiaban = request.Madiaban,
                        Soqd = request.Soqd,
                        Manhom = request.Manhom,
                        Ngayqd = request.Ngayqd,
                        Thoidiem = request.Ngayqd,
                        Tennhathau = request.Tennhathau,
                        Thongtinqd = request.Thongtinqd,
                        Ghichu = request.Ghichu,
                        Ipf1 = request.Ipf1,
                        Trangthai = "CHT",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaMuaTaiSan.Update(model);

                }
                _db.GiaMuaTaiSanCt.AddRange(list_add);
                _db.SaveChanges();
                return RedirectToAction("Edit", "GiaTrungThauHhDv", new { Mahs = request.Mahs });
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

    }
}
