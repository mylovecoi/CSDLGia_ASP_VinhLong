using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.KeKhaiGia;
using CSDLGia_ASP.Models.Manages.ThamDinhGia;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.KeKhaiGia.ThamDinhGia
{
    public class ThamDinhGiaExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public ThamDinhGiaExcelController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [HttpGet("ThamDinhGia/NhanExcel")]
        public IActionResult Index(string Madv)
        {
            var model = new CSDLGia_ASP.ViewModels.VMImportExcel
            {
                LineStart = 4,
                LineStop = 1000,
                Sheet = 1,
                MaDv = Madv,
            };
            ViewData["MenuLv1"] = "menu_tdg";
            ViewData["MenuLv2"] = "menu_tdg_tk";
            ViewData["Title"] = "Thông tin hồ sơ thẩm định giá";
            ViewData["DmNhomHh"] = _db.DmNhomHh.Where(t => t.Phanloai == "THAMDINHGIA" && t.Theodoi == "TD").ToList();
            return View("Views/Admin/Manages/ThamDinhGia/Excels/Excel.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> ImportExcel(CSDLGia_ASP.ViewModels.VMImportExcel request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                //xử lý hồ sơ
                var model = new CSDLGia_ASP.Models.Manages.ThamDinhGia.ThamDinhGia
                {
                    Mahs = request.MaDv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                    Madv = request.MaDv,
                    Phanloai = request.Phanloai,
                    Tttstd = request.MaNhom,
                    Thoidiem = DateTime.Now,
                    Ngayqdpheduyet = DateTime.Now,
                    Thoihan = DateTime.Now,
                };
                request.LineStart = request.LineStart == 0 ? 1 : request.LineStart;
                var list_add = new List<ThamDinhGiaCt>();
                int sheet = request.Sheet == 0 ? 0 : (request.Sheet - 1);
                using (var stream = new MemoryStream())
                {

                    //Chi tiết hồ sơ
                    await request.FormFile.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[sheet];
                        var rowcount = worksheet.Dimension.Rows;
                        request.LineStop = request.LineStop > rowcount ? rowcount : request.LineStop;

                        for (int row = request.LineStart; row <= request.LineStop; row++)
                        {
                            list_add.Add(new ThamDinhGiaCt
                            {
                                Mahs = model.Mahs,
                                Manhom = model.Tttstd,
                                Mats = worksheet.Cells[row, 2].Value != null ? worksheet.Cells[row, 2].Value.ToString().Trim() : "",
                                Tents = worksheet.Cells[row, 3].Value != null ? worksheet.Cells[row, 3].Value.ToString().Trim() : "",
                                Thongsokt = worksheet.Cells[row, 4].Value != null ? worksheet.Cells[row, 4].Value.ToString().Trim() : "",
                                Nguongoc = worksheet.Cells[row, 5].Value != null ? worksheet.Cells[row, 5].Value.ToString().Trim() : "",
                                Dvt = worksheet.Cells[row, 6].Value != null ? worksheet.Cells[row, 6].Value.ToString().Trim() : "",
                                Sl = worksheet.Cells[row, 7].Value != null ? Helpers.ConvertStrToDb(worksheet.Cells[row, 7].Value.ToString().Trim()) : 0,
                                Nguyengiathamdinh = worksheet.Cells[row, 8].Value != null ? Helpers.ConvertStrToDb(worksheet.Cells[row, 8].Value.ToString().Trim()) : 0,
                                Giatritstd = worksheet.Cells[row, 9].Value != null ? Helpers.ConvertStrToDb(worksheet.Cells[row, 9].Value.ToString().Trim()) : 0,

                            });
                        }
                    }

                }
                /*return Ok(list_add);*/
                _db.ThamDinhGiaCt.AddRange(list_add);
                _db.SaveChanges();


                ViewData["Madv"] = model.Madv;
                model.ThamDinhGiaCt = list_add;
                ViewData["TdgDonvi"] = _db.ThamDinhGiaDv.ToList();
                ViewData["TdgDmHh"] = _db.ThamDinhGiaDmHh.Where(t => t.Manhom == model.Tttstd).ToList();
                ViewData["DmNhomHh"] = _db.DmNhomHh.Where(t => t.Phanloai == "THAMDINHGIA" && t.Theodoi == "TD").ToList();
                ViewData["Dvt"] = _db.DmDvt.ToList();
                ViewData["Title"] = "Hồ sơ thẩm định giá";
                ViewData["MenuLv1"] = "menu_tdg";
                ViewData["MenuLv2"] = "menu_tdg_tt";
                return View("Views/Admin/Manages/ThamDinhGia/DanhSach/Create.cshtml", model);

            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }
    }
}
