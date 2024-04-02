using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDatPl
{
    public class GiaDatPlExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaDatPlExcelController(CSDLGiaDBContext db)
        {
            _db = db;
        }


        public IActionResult Index(string MadvExcel, string PhanloaiExcel)
        {
            var model = new CSDLGia_ASP.ViewModels.VMImportExcel
            {
                LineStart = 2,
                LineStop = 1000,
                Sheet = 1,
                MaDv = MadvExcel,
                Phanloai = PhanloaiExcel,
            };
            ViewData["MenuLv1"] = "menu_giadat";
            ViewData["MenuLv2"] = "menu_dgdct";
            ViewData["MenuLv3"] = "menu_dgdct_tt";
            ViewData["Title"] = "Thông tin hồ sơ giá các loại đất";
            return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excels/Excel.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Import(CSDLGia_ASP.ViewModels.VMImportExcel requests)
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
                            var list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaDatPhanLoaiCt>();
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

                                list_add.Add(new GiaDatPhanLoaiCt
                                {

                                    Mahs = Mahs,
                                    Trangthai = "CXD",
                                    Created_at = DateTime.Now,
                                    Updated_at = DateTime.Now,
                                    Khuvuc = worksheet.Cells[row, 1].Value != null ?
                                             worksheet.Cells[row, 1].Value.ToString().Trim() : "",

                                    Maloaidat = worksheet.Cells[row, 2].Value != null ?
                                             worksheet.Cells[row, 2].Value.ToString().Trim() : "",

                                    Vitri = worksheet.Cells[row, 3].Value != null ?
                                            Helpers.ConvertStrToInt(worksheet.Cells[row, 3].Value.ToString()) : 0,

                                    Banggiadat = worksheet.Cells[row, 4].Value != null ?
                                            Helpers.ConvertStrToDb(worksheet.Cells[row, 4].Value.ToString()) : 0,

                                    Diagioitu = worksheet.Cells[row, 5].Value != null ?
                                             worksheet.Cells[row, 5].Value.ToString().Trim() : "",

                                    Diagioiden = worksheet.Cells[row, 6].Value != null ?
                                             worksheet.Cells[row, 6].Value.ToString().Trim() : "",                                    

                                    Giacuthe = worksheet.Cells[row, 7].Value != null ?
                                            Helpers.ConvertStrToDb(worksheet.Cells[row, 7].Value.ToString()) : 0,

                                    Hesodc = worksheet.Cells[row, 8].Value != null ?
                                            Helpers.ConvertStrToDb(worksheet.Cells[row, 8].Value.ToString()) : 0,
                                    
                                });
                                line++;
                            }
                            _db.GiaDatPhanLoaiCt.AddRange(list_add);
                            _db.SaveChanges();
                        }
                    }
                }

                var model = new CSDLGia_ASP.ViewModels.Manages.DinhGia.VMDinhGiaDat
                {
                    Madv = requests.MaDv,
                    Thoidiem = DateTime.Now,
                    Mahs = Mahs,
                    Phanloai = requests.Phanloai,
                };

                var modelct = _db.GiaDatPhanLoaiCt.Where(t => t.Mahs == Mahs);
                model.GiaDatPhanLoaiCt = modelct.ToList();

                ViewData["Mahs"] = model.Mahs;
                ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                ViewData["Dmloaidat"] = _db.DmLoaiDat.ToList();                
                ViewData["Title"] = "Thông tin hồ sơ giá các loại đất";
                ViewData["MenuLv1"] = "menu_giadat";
                ViewData["MenuLv2"] = "menu_dgdct";
                ViewData["MenuLv3"] = "menu_dgdct_tt";
                return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Create.cshtml", model);
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }
    }
}
