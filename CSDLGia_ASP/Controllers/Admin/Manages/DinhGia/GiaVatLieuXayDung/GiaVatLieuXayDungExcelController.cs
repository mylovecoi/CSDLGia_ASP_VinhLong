using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaVatLieuXayDung
{
    public class GiaVatLieuXayDungExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaVatLieuXayDungExcelController(CSDLGiaDBContext db)
        {
            _db = db;
        }


        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giavatlieuxaydung.thongtin", "Create"))
                {
                    var model = new CSDLGia_ASP.ViewModels.VMImportExcel
                    {                        
                        Sheet = 1,
                        LineStart = 3,
                        LineStop = 1000,
                        MaDv = Madv
                       
                    };
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_giakhac_giavatlieuxaydung";
                    ViewData["MenuLv3"] = "menu_giakhac_giavatlieuxaydung_tt";
                    ViewData["Madv"] = Madv;
                    ViewData["Title"] = "Thông tin hồ sơ giá vật liệu xây dựng";
                    return View("Views/Admin/Manages/DinhGia/GiaVatLieuXayDung/Excels/Excel.cshtml", model);

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
        public async Task<IActionResult> Import(CSDLGia_ASP.ViewModels.VMImportExcel requests)
        {
            requests.LineStart = requests.LineStart == 0 ? 1 : requests.LineStart;
            int sheet = requests.Sheet == 0 ? 0 : (requests.Sheet - 1);
            var list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaVatLieuXayDungCt>();
            string Mahs = requests.MaDv + "_" + DateTime.Now.ToString("yyMMddssmmHH");
            using (var stream = new MemoryStream())
            {
                await requests.FormFile.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[sheet];
                    var rowcount = worksheet.Dimension.Rows;
                    requests.LineStop = requests.LineStop > rowcount ? rowcount : requests.LineStop;
                    Regex trimmer = new Regex(@"\s\s+"); // Xóa khoảng trắng thừa trong chuỗi
                    int line = 1;
                    for (int row = requests.LineStart; row <= requests.LineStop; row++)
                    {
                        list_add.Add(new CSDLGia_ASP.Models.Manages.DinhGia.GiaVatLieuXayDungCt
                        {
                            Mahs = Mahs,
                            Madv = requests.MaDv,
                            Trangthai = "CXD",
                            STTSapXep = line,
                            STTHienThi = worksheet.Cells[row, 1].Value != null ?
                                        worksheet.Cells[row, 1].Value.ToString().Trim() : "",
                            Ten = worksheet.Cells[row, 2].Value != null ?
                                        worksheet.Cells[row, 2].Value.ToString().Trim() : "",
                            Dvt = worksheet.Cells[row, 3].Value != null ?
                                        worksheet.Cells[row, 3].Value.ToString().Trim() : "",
                            TieuChuan = worksheet.Cells[row, 4].Value != null ?
                                        worksheet.Cells[row, 4].Value.ToString().Trim() : "",
                            Gia = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 5].Value != null ?
                                        worksheet.Cells[row, 5].Value.ToString().Trim() : ""),
                            GhiChu = worksheet.Cells[row, 6].Value != null ?
                                        worksheet.Cells[row, 6].Value.ToString().Trim() : "",
                        });
                        line = line + 1;
                    }
                }
            }
            _db.GiaVatLieuXayDungCt.AddRange(list_add);
            _db.SaveChanges();
            var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaVatLieuXayDung
            {
                Madv = requests.MaDv,
                Thoidiem = DateTime.Now,
                Mahs = Mahs,               
            };
            var modelct = _db.GiaVatLieuXayDungCt.Where(t => t.Mahs == Mahs);
            model.GiaVatLieuXayDungCt = modelct.ToList();

            ViewData["Title"] = "Bảng giá vật liệu xây dựng";
            ViewData["MenuLv1"] = "menu_giakhac";
            ViewData["MenuLv2"] = "menu_giakhac_giavatlieuxaydung";
            ViewData["MenuLv3"] = "menu_giakhac_giavatlieuxaydung_tt";
            return View("Views/Admin/Manages/DinhGia/GiaVatLieuXayDung/DanhSach/Create.cshtml", model);

        }



    }
}
