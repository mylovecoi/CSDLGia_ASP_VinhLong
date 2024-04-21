using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaTrungThauDat
{
    public class GiaTrungThauDatExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IDsDiaBanService _IDsDiaBan;

        public GiaTrungThauDatExcelController(CSDLGiaDBContext db, IDsDiaBanService IDsDiaBan)
        {
            _db = db;
            _IDsDiaBan = IDsDiaBan;
        }


        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.trungthaudat.thongtin", "Create"))
                {
                    var model = new CSDLGia_ASP.ViewModels.VMImportExcel
                    {
                        LineStart = 3,
                        LineStop = 1000,
                        Sheet = 1,
                        MaDv = Madv,
                    };
                    ViewData["DsDiaBanHuyen"] = _db.DsDiaBan;
                    ViewData["DsDiaBanXa"] = _db.DsDiaBan.Where(x => x.Level == "X");
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dg_xaydungmoi";
                    ViewData["MenuLv3"] = "menu_dg_xaydungmoi_tt";
                    ViewData["Madv"] = Madv;
                    ViewData["Title"] = "Thông tin hồ sơ giá trúng thầu đất";
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauDat/Excels/Excel.cshtml", model);

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
            var list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaDauGiaDatCt>();
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
                    for (int row = requests.LineStart; row <= requests.LineStop; row++)
                    {
                        string MaDiaBan = requests.Maxp == "all" ? (worksheet.Cells[row, 11].Value != null ? worksheet.Cells[row, 11].Value.ToString().Trim() : "") : requests.Maxp;
                        MaDiaBan = (string.IsNullOrEmpty(MaDiaBan) || MaDiaBan == "all") ? requests.MadiabanBc : MaDiaBan;
                        list_add.Add(new CSDLGia_ASP.Models.Manages.DinhGia.GiaDauGiaDatCt
                        {
                            MaDiaBan= MaDiaBan,                           
                            Mahs = Mahs,
                            MaDv = requests.MaDv,
                            TrangThai = "CXD",

                            Mota = worksheet.Cells[row, 2].Value != null ?
                                   worksheet.Cells[row, 2].Value.ToString().Trim() : "",

                            Solo = worksheet.Cells[row, 3].Value != null ?
                                   worksheet.Cells[row, 3].Value.ToString().Trim() : "",

                            Sothua = worksheet.Cells[row, 4].Value != null ?
                                     worksheet.Cells[row, 4].Value.ToString().Trim() : "",

                            Tobanbo = worksheet.Cells[row, 5].Value != null ?
                                      worksheet.Cells[row, 5].Value.ToString().Trim() : "",

                            Dientich = Helpers.ConvertStrToDb(worksheet.Cells[row, 6].Value != null ?
                                                              worksheet.Cells[row, 6].Value.ToString().Trim() : ""),

                            Dvt = worksheet.Cells[row, 7].Value != null ?
                                  worksheet.Cells[row, 7].Value.ToString().Trim() : "",

                            Giakhoidiem = Helpers.ConvertStrToDb(worksheet.Cells[row, 8].Value != null ?
                                                                 worksheet.Cells[row, 8].Value.ToString().Trim() : ""),

                            Giadaugia = Helpers.ConvertStrToDb(worksheet.Cells[row, 9].Value != null ?
                                                               worksheet.Cells[row, 9].Value.ToString().Trim() : ""),

                            Giasddat = Helpers.ConvertStrToDb(worksheet.Cells[row, 10].Value != null ?
                                                              worksheet.Cells[row, 10].Value.ToString().Trim() : ""),
                        });
                    }
                }
            }
            _db.GiaDauGiaDatCt.AddRange(list_add);
            _db.SaveChanges();
            var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaDauGiaDat
            {
                Madiaban=requests.MadiabanBc,
                Maxp= requests.Maxp,
                Madv = requests.MaDv,
                Thoidiem = DateTime.Now,
                Mahs = Mahs,
            };
            var modelct = _db.GiaDauGiaDatCt.Where(t => t.Mahs == Mahs);
            model.GiaDauGiaDatCt = modelct.ToList();


            var diaban = _db.DsDiaBan.FirstOrDefault(x => x.MaDiaBan == requests.MadiabanBc);

            var DsXaPhuong = _IDsDiaBan.GetListDsDiaBan(requests.MadiabanBc);

            ViewData["TenDiaBan"] = _db.DsDiaBan.FirstOrDefault(x => x.MaDiaBan == requests.MadiabanBc).TenDiaBan;
            ViewData["DsXaPhuong"] = DsXaPhuong;

            ViewData["DmDvt"] = _db.DmDvt.ToList();
            ViewData["TenDonVi"] = _db.DsDonVi.First(x => x.MaDv == requests.MaDv).TenDv;            
            ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");           
            ViewData["Title"] = " Thông tin hồ sơ giá trúng thầu quyền sd đất";
            ViewData["MenuLv1"] = "menu_giadat";
            ViewData["MenuLv2"] = "menu_dgd";
            ViewData["MenuLv3"] = "menu_giadgd_tt";
            return View("Views/Admin/Manages/DinhGia/GiaTrungThauDat/DanhSach/Create.cshtml", model);

        }

    }
}
