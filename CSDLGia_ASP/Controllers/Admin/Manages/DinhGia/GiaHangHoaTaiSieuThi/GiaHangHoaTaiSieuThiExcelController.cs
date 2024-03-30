using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaHangHoaTaiSieuThi
{
    public class GiaHangHoaTaiSieuThiExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaHangHoaTaiSieuThiExcelController(CSDLGiaDBContext db)
        {
            _db = db;
        }


        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giasieuthi.thongtin", "Create"))
                {
                    var model = new CSDLGia_ASP.ViewModels.VMImportExcel
                    {
                        LineStart = 2,
                        LineStop = 1000,
                        Sheet = 1,
                        MaDv = Madv,
                    };

                    var dsdonvi = (from db in _db.DsDiaBan.Where(t => t.Level != "ADMIN")
                                   join dv in _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI") on db.MaDiaBan equals dv.MaDiaBan
                                   select new VMDsDonVi
                                   {
                                       Id = dv.Id,
                                       TenDiaBan = db.TenDiaBan,
                                       MaDiaBan = dv.MaDiaBan,
                                       TenDv = dv.TenDv,
                                       MaDv = dv.MaDv,
                                   }).ToList();

                    if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                    {
                        ViewData["DsDonVi"] = dsdonvi;
                    }
                    else
                    {
                        ViewData["DsDonVi"] = dsdonvi.Where(t => t.MaDv == Madv);
                    }
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgsieuthi";
                    ViewData["MenuLv3"] = "menu_dgsieuthi_tt";
                    ViewData["Madv"] = Madv;
                    ViewData["Dstt"] = _db.GiaHangHoaTaiSieuThiDm;
                    ViewData["Title"] = "Thông tin hồ sơ giá hàng hóa tại siêu thị";
                    return View("Views/Admin/Manages/DinhGia/GiaHangHoaTaiSieuThi/Excels/Excel.cshtml", model);

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
                            var list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaHangHoaTaiSieuThiCt>();
                            for (int row = requests.LineStart; row <= requests.LineStop; row++)
                            {
                                list_add.Add(new CSDLGia_ASP.Models.Manages.DinhGia.GiaHangHoaTaiSieuThiCt
                                {
                                    Matt = requests.Matt,
                                    Mahs = Mahs,
                                    Madv = requests.MaDv,
                                    Trangthai = "CXD",
                                    Mahanghoa = worksheet.Cells[row, 2].Value != null ?
                                                worksheet.Cells[row, 2].Value.ToString().Trim() : "",
                                    Tenhanghoa = worksheet.Cells[row, 3].Value != null ?
                                                worksheet.Cells[row, 3].Value.ToString().Trim() : "",
                                    Giatu = Helpers.ConvertStrToDb(worksheet.Cells[row, 4].Value != null ?
                                                    worksheet.Cells[row, 4].Value.ToString().Trim() : ""),
                                    Giaden = Helpers.ConvertStrToDb(worksheet.Cells[row, 5].Value != null ?
                                                    worksheet.Cells[row, 5].Value.ToString().Trim() : ""),
                                });
                            }
                            _db.GiaHangHoaTaiSieuThiCt.AddRange(list_add);
                            _db.SaveChanges();
                        }
                    }
                }

                var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaHangHoaTaiSieuThi
                {
                    Madv = requests.MaDv,
                    Thoidiem = DateTime.Now,
                    Mahs = Mahs,
                    Thang = requests.Thang.ToString(),
                    Nam = requests.Nam.ToString(),
                };
                var modelct = _db.GiaHangHoaTaiSieuThiCt.Where(t => t.Mahs == Mahs);
                model.GiaHangHoaTaiSieuThiCt = modelct.ToList();

                ViewData["Madv"] = requests.MaDv;
                ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                ViewData["Title"] = "Thêm mới giá hàng hóa tại siêu thị";
                ViewData["MenuLv1"] = "menu_dg";
                ViewData["MenuLv2"] = "menu_dgsieuthi";
                ViewData["MenuLv3"] = "menu_dgsieuthi_tt";
                return View("Views/Admin/Manages/DinhGia/GiaHangHoaTaiSieuThi/Create.cshtml", model);
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

    }
}
