using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using CSDLGia_ASP.ViewModels;
using Microsoft.AspNetCore.Hosting;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.KeKhaiDangKyGia
{
    public class KeKhaiDangKyGiaExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public KeKhaiDangKyGiaExcelController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet("KeKhaiDangKyGia/NhanExcel")]
        public IActionResult Index(string MaCsKd, string MaNghe)
        {
            if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kekhaidangkygia.thongtin", "Create"))
            {
                var cskd = _db.KeKhaiDangKyGiaCSKD.FirstOrDefault(t => t.MaCsKd == MaCsKd);
                
                var nghekd = _db.DmNgheKd.FirstOrDefault(t => t.Manghe == MaNghe);
                var model = new VMImportExcel
                {
                    MaCsKd = MaCsKd,
                    MaNghe = MaNghe,
                    TenCsKd = cskd?.TenCsKd ?? "",
                    TenNghe = nghekd?.Tennghe ?? "",
                    TenDn = string.IsNullOrEmpty(cskd?.MaCsKd ?? "") ? "" : _db.Company.FirstOrDefault(t => t.Madv == cskd.MaDv)?.Tendn ?? "",
                    LineStart = 3,
                    LineStop = 1000,
                    Sheet = 1,
                };

                ViewData["Title"] = "Thông tin hồ sơ kê khai đăng ký giá";
                ViewData["MenuLv1"] = "menu_kekhaidangkygia";
                ViewData["MenuLv2"] = "menu_kekhaidangkygia_thongtin_" + MaNghe;
                return View("Views/Admin/Manages/KeKhaiDangKyGia/ImportExcel/Index.cshtml", model);
            }
            else
            {
                ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
                return View("Views/Admin/Error/Page.cshtml");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Import(VMImportExcel requests)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                requests.LineStart = requests.LineStart == 0 ? 1 : requests.LineStart;
                int sheet = requests.Sheet == 0 ? 0 : (requests.Sheet - 1);

                string Mahoso = requests.MaCsKd + "_" + requests.MaNghe + "_" + DateTime.Now.ToString("yyMMddssmmHH");
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
                            var list_add = new List<CSDLGia_ASP.Models.Manages.KeKhaiDangKyGia.KeKhaiDangKyGiaCt>();
                            for (int row = requests.LineStart; row <= requests.LineStop; row++)
                            {
                                //    ExcelStyle style = worksheet.Cells[row, 2].Style;
                                //    // Kiểm tra xem font chữ có được đánh dấu là đậm không
                                //    bool isBold = style.Font.Bold;
                                //    // Kiểm tra xem font chữ có được đánh dấu là nghiêng không
                                //    bool isItalic = style.Font.Italic;
                                //    StringBuilder strStyle = new StringBuilder();
                                //    if (isBold) { strStyle.Append("Chữ in đậm,"); }
                                //    if (isItalic) { strStyle.Append("Chữ in nghiêng,"); }

                                //    string MaNhom = requests.MaNhom == "all" ? worksheet.Cells[row, 17].Value != null ?
                                //                    worksheet.Cells[row, 17].Value.ToString().Trim() : "" : requests.MaNhom;

                                list_add.Add(new CSDLGia_ASP.Models.Manages.KeKhaiDangKyGia.KeKhaiDangKyGiaCt
                                {
                                    Mahs = Mahoso,
                                    MaCsKd = requests.MaCsKd,
                                    TrangThai = "CXD",

                                    TenDvCungUng = worksheet.Cells[row, 1].Value != null ?
                                                worksheet.Cells[row, 1].Value.ToString().Trim() : "",
                                    QuyCachChatLuong = worksheet.Cells[row, 2].Value != null ?
                                                worksheet.Cells[row, 2].Value.ToString().Trim() : "",
                                    ThoiGianThucHien = worksheet.Cells[row, 3].Value != null ?
                                                worksheet.Cells[row, 3].Value.ToString().Trim() : "",

                                    MucGiaKeKhaiLk = Helpers.ConvertStrToDb(worksheet.Cells[row, 4].Value != null ?
                                                    worksheet.Cells[row, 4].Value.ToString().Trim() : ""),
                                    MucGiaKeKhai = Helpers.ConvertStrToDb(worksheet.Cells[row, 5].Value != null ?
                                                    worksheet.Cells[row, 5].Value.ToString().Trim() : ""),
                                    HinhThucKinhDoanh = worksheet.Cells[row, 6].Value != null ?
                                                worksheet.Cells[row, 6].Value.ToString().Trim() : "",

                                });

                            }
                            _db.KeKhaiDangKyGiaCt.AddRange(list_add);
                            _db.SaveChanges();
                        }
                    }

                    var model = new CSDLGia_ASP.Models.Manages.KeKhaiDangKyGia.KeKhaiDangKyGia
                    {
                        NgayQD = DateTime.Now,
                        NgayQdLk = DateTime.Now,
                        Mahs = Mahoso,
                        DonViTinh = "đồng/",
                        MaNghe = requests.MaNghe,
                        MaCsKd = requests.MaCsKd,
                        TrangThai = "CC"                        
                    };
                    var modelct = _db.KeKhaiDangKyGiaCt.Where(t => t.Mahs == Mahoso);
                    model.KeKhaiDangKyGiaCt = modelct.ToList();

                    var nghekd = _db.DmNgheKd.FirstOrDefault(t => t.Manghe == requests.MaNghe);
                    ViewData["HoSo"] = (nghekd?.Phanloai ?? "") + " " + (nghekd?.Tennghe ?? "");

                    ViewData["Title"] = "Thông tin hồ sơ kê khai đăng ký giá";
                    ViewData["MenuLv1"] = "menu_kekhaidangkygia";
                    ViewData["MenuLv2"] = "menu_kekhaidangkygia_thongtin_" + requests.MaNghe;
                    return View("Views/Admin/Manages/KeKhaiDangKyGia/ThongTinHoSo/Create.cshtml", model);
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }
    }
}