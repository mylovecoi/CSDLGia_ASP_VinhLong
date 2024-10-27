using CSDLGia_ASP.Database;
using CSDLGia_ASP.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDatCuTheVl
{
    public class GiaDatCuTheVlExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        public GiaDatCuTheVlExcelController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("GiaDatCuTheVl/Create/Excel")]
        public IActionResult Index(string Madv)
        {
            var model_ct_cxd = _db.GiaDatCuTheVlCt.Where(t => t.Trangthai == "CXD" && t.Madv == Madv);
            if (model_ct_cxd.Any())
            {
                _db.GiaDatCuTheVlCt.RemoveRange(model_ct_cxd);
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
            ViewData["DanhMucPp"] = _db.GiaDatCuTheVlDmPPDGDat;
            ViewData["Title"] = " Thông tin hồ sơ giá đất cụ thể";
            ViewData["MenuLv1"] = "menu_giadat";
            ViewData["MenuLv2"] = "menu_dgdctvl";
            ViewData["MenuLv3"] = "menu_dgdctvl_tt";
            return View("Views/Admin/Manages/DinhGia/GiaDatCuTheVl/Excels/Excel.cshtml", model);
        }

        [Route("GiaDatCuTheVl/Create/Excel/Import")]
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
                            var list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaDatCuTheVlCt>();
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
                                string str_mapp = requests.Mapp;
                                if (requests.Mapp == "all")
                                {
                                    str_mapp = worksheet.Cells[row, 14].Value != null ?
                                                worksheet.Cells[row, 14].Value.ToString().Trim() : "";
                                }

                                list_add.Add(new CSDLGia_ASP.Models.Manages.DinhGia.GiaDatCuTheVlCt
                                {
                                    Mahs = Mahs,
                                    Madv = requests.MaDv,
                                    Trangthai = "CXD",
                                    STTSapXep = line,
                                    STTHienThi = worksheet.Cells[row, 1].Value != null ?
                                                worksheet.Cells[row, 1].Value.ToString().Trim() : "",
                                    Noidungcv = worksheet.Cells[row, 2].Value != null ?
                                                worksheet.Cells[row, 2].Value.ToString().Trim() : "",
                                    ChiPhiNhanCong = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 3].Value != null ?
                                                    worksheet.Cells[row, 3].Value.ToString().Trim() : ""),
                                    ChiPhiDungCu = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 4].Value != null ?
                                                    worksheet.Cells[row, 4].Value.ToString().Trim() : ""),
                                    ChiPhiNangLuong = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 5].Value != null ?
                                                    worksheet.Cells[row, 5].Value.ToString().Trim() : ""),
                                    ChiPhiKhauHao = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 6].Value != null ?
                                                    worksheet.Cells[row, 6].Value.ToString().Trim() : ""),
                                    ChiPhiVatLieu = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 7].Value != null ?
                                                    worksheet.Cells[row, 7].Value.ToString().Trim() : ""),
                                    ChiPhiTrucTiepKkh = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 8].Value != null ?
                                                    worksheet.Cells[row, 8].Value.ToString().Trim() : ""),
                                    ChiPhiTrucTiepCkh = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 9].Value != null ?
                                                    worksheet.Cells[row, 9].Value.ToString().Trim() : ""),
                                    ChiPhiQlChungKkh = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 10].Value != null ?
                                                    worksheet.Cells[row, 10].Value.ToString().Trim() : ""),
                                    ChiPhiQlChungCkh = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 11].Value != null ?
                                                    worksheet.Cells[row, 11].Value.ToString().Trim() : ""),
                                    DonGiaKkh = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 12].Value != null ?
                                                    worksheet.Cells[row, 12].Value.ToString().Trim() : ""),
                                    DonGiaCkh = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 13].Value != null ?
                                                    worksheet.Cells[row, 13].Value.ToString().Trim() : ""),
                                    Style = strStyle.ToString(),
                                    Mapp = str_mapp,
                                    NhapGia = true,
                                });
                                line++;
                            }
                            _db.GiaDatCuTheVlCt.AddRange(list_add);
                            _db.SaveChanges();
                        }
                    }
                }

                var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaDatCuTheVl
                {
                    Madv = requests.MaDv,
                    Thoidiem = DateTime.Now,
                    Mahs = Mahs,
                };

                var modelct = _db.GiaDatCuTheVlCt.Where(t => t.Mahs == Mahs);
                model.GiaDatCuTheVlCt = modelct.ToList();
                ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                ViewData["DanhMucPp"] = _db.GiaDatCuTheVlDmPPDGDat;
                ViewData["Title"] = " Thông tin hồ sơ giá đất cụ thể";
                ViewData["MenuLv1"] = "menu_giadat";
                ViewData["MenuLv2"] = "menu_dgdctvl";
                ViewData["MenuLv3"] = "menu_dgdctvl_tt";
                
                return View("Views/Admin/Manages/DinhGia/GiaDatCuTheVl/DanhSach/Create.cshtml", model);
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }
    }
}
