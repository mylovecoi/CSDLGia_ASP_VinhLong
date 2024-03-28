using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaThueDN
{
    //2024.03.15 => Xem lại chức năng do thiết kế lại hồ sơ

    public class GiaThueDNExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        public GiaThueDNExcelController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index(string Madv)
        {
            var model_ct_cxd = _db.GiaThueMatDatMatNuocCt.Where(t => t.Trangthai == "CXD" && t.Madv == Madv);
            if (model_ct_cxd.Any())
            {
                _db.GiaThueMatDatMatNuocCt.RemoveRange(model_ct_cxd);
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
                MaDv = Madv,               
                LineStart = 4,
                LineStop = 10000,
                Sheet = 1,
            };
            ViewData["GiaThueDNNhom"] = _db.GiaThueMatDatMatNuocNhom;
            ViewData["MenuLv1"] = "menu_dg";
            ViewData["MenuLv2"] = "menu_dgtmdmn";
            ViewData["MenuLv3"] = "menu_dgtmdmn_tt";
            ViewData["Title"] = "Thông tin hồ sơ giá thuê mặt đất mặt nước";
            return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/Excels/Excel.cshtml", model);
        }
       

        [HttpPost]
        public async Task<IActionResult> Import(VMImportExcel requests)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                requests.LineStart = requests.LineStart == 0 ? 1 : requests.LineStart;
                int sheet = requests.Sheet == 0 ? 0 : (requests.Sheet - 1);

                string Mahs = requests.MaDv + "_" + DateTime.Now.ToString("yyMMddssmmHH");
                using (var stream = new MemoryStream())
                {
                    await requests.FormFile.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[sheet];
                        if (worksheet != null)
                        {
                            var rowcount = worksheet.Dimension.Rows;
                            requests.LineStop = requests.LineStop > rowcount ? rowcount : requests.LineStop;
                            Regex trimmer = new Regex(@"\s\s+"); // Xóa khoảng trắng thừa trong chuỗi
                            var list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMatDatMatNuocCt>();
                            int line = 1;
                            for (int row = requests.LineStart; row <= requests.LineStop; row++)
                            {
                                ExcelStyle style = worksheet.Cells[row, 2].Style;
                                // Kiểm tra xem font chữ có được đánh dấu là đậm không
                                bool isBold = style.Font.Bold;
                                // Kiểm tra xem font chữ có được đánh dấu là nghiêng không
                                bool isItalic = style.Font.Italic;
                                StringBuilder strStyle = new StringBuilder();
                                if (isBold) { strStyle.Append("Chữ in đậm,"); }
                                if (isItalic) { strStyle.Append("Chữ in nghiêng,"); }
                                string str_manhom = requests.MaNhom;
                                if (requests.MaNhom == "all")
                                {
                                    str_manhom = worksheet.Cells[row, 7].Value != null ?
                                                worksheet.Cells[row, 7].Value.ToString().Trim() : "";
                                }
                                list_add.Add(new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMatDatMatNuocCt
                                {
                                    Mahs = Mahs,
                                    Madv = requests.MaDv,
                                    Trangthai = "CXD",
                                    SapXep = line,
                                    HienThi = worksheet.Cells[row, 1].Value != null ?
                                                worksheet.Cells[row, 1].Value.ToString().Trim() : "",
                                    LoaiDat = worksheet.Cells[row, 2].Value != null ?
                                                worksheet.Cells[row, 2].Value.ToString().Trim() : "",
                                    TyLe1 = worksheet.Cells[row, 3].Value != null ?
                                                worksheet.Cells[row, 3].Value.ToString().Trim() : "",
                                    TyLe2 = worksheet.Cells[row, 4].Value != null ?
                                                worksheet.Cells[row, 4].Value.ToString().Trim() : "",
                                    TyLe3 = worksheet.Cells[row, 5].Value != null ?
                                                worksheet.Cells[row, 5].Value.ToString().Trim() : "",
                                    Dongia1 = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 6].Value != null ?
                                                    worksheet.Cells[row, 6].Value.ToString().Trim() : ""),
                                    Style = strStyle.ToString(),
                                
                                    MaNhom = str_manhom
                                });
                                line++;
                            }
                            _db.GiaThueMatDatMatNuocCt.AddRange(list_add);
                            _db.SaveChanges();
                        }
                    }
                }

                var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMatDatMatNuoc
                {
                    Madv = requests.MaDv,
                    Thoidiem = DateTime.Now,
                    Mahs = Mahs,
                };

                var modelct = _db.GiaThueMatDatMatNuocCt.Where(t => t.Mahs == Mahs);
                model.GiaThueMatDatMatNuocCt = modelct.ToList();
                ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                ViewData["Title"] = "Thêm mới giá thuê mặt đất mặt nước";
                ViewData["MenuLv1"] = "menu_dg";
                ViewData["MenuLv2"] = "menu_dgtmdmn";
                ViewData["MenuLv3"] = "menu_dgtmdmn_tt";
                ViewData["GiaThueDNNhom"] = _db.GiaThueMatDatMatNuocNhom;
                return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/Create.cshtml", model);
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }




    }
}
