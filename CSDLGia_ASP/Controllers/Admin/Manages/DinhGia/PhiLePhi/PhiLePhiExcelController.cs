using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.PhiLePhi
{
    public class PhiLePhiExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public PhiLePhiExcelController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [HttpGet("PhiLePhi/NhanExcel")]
        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.philephi.thongtin", "Create"))
                {
                    var model = new CSDLGia_ASP.ViewModels.VMImportExcel
                    {
                        LineStart = 2,
                        LineStop = 1000,
                        Sheet = 1,
                        MaDv = Madv,
                    };
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dg_xaydungmoi";
                    ViewData["MenuLv3"] = "menu_dg_xaydungmoi_tt";
                    ViewData["Title"] = "Thông tin hồ sơ phí lệ phí";
                    return View("Views/Admin/Manages/DinhGia/PhiLePhi/Excels/Excel.cshtml", model);

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
                var list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.PhiLePhiCt>();
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
                            list_add.Add(new CSDLGia_ASP.Models.Manages.DinhGia.PhiLePhiCt
                            {
                                Mahs = Mahs,
                                Madv = requests.MaDv,
                                

                            });
                            line = line + 1;
                        }
                    }
                }
                _db.PhiLePhiCt.AddRange(list_add);
                _db.SaveChanges();
                var model = new CSDLGia_ASP.Models.Manages.DinhGia.PhiLePhi
                {
                    Madv = requests.MaDv,
                    Thoidiem = DateTime.Now,
                    Mahs = Mahs,
                };
                var modelct = _db.PhiLePhiCt.Where(t => t.Mahs == Mahs);
                model.PhiLePhiCt = modelct.ToList();
                ViewData["Mahs"] = model.Mahs;
                ViewData["DsDiaBan"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                ViewData["Phanloai"] = _db.GiaPhiLePhiDm.ToList();
                ViewData["Title"] = " Thông tin hồ sơ phí lệ phí";
                ViewData["MenuLv1"] = "menu_giakhac";
                ViewData["MenuLv2"] = "menu_dglp";
                ViewData["MenuLv3"] = "menu_dglp_tt";
                return View("Views/Admin/Manages/DinhGia/PhiLePhi/DanhSach/Create.cshtml", model);
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }
    }
}
