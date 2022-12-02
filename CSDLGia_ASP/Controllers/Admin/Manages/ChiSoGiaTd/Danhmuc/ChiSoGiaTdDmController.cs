using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CSDLGia_ASP.Database;
using System.Security.Cryptography;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.ChiSoGiaTd;
using CSDLGia_ASP.ViewModels.Systems;
//using CSDLGia_ASP.ViewModels.Manages.ChiSoGiaTd;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CSDLGia_ASP.ViewModels.Manages.ChiSoGiaTd;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;

namespace CSDLGia_ASP.Controllers.Admin.Manages.ChiSoGiaTd
{
    public class ChiSoGiaTdDmController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public ChiSoGiaTdDmController(CSDLGiaDBContext db)
        {
            _db = db;
        }
        [Route("Danhsachnhomhanghoa")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.chisogia.danhmuc", "Index") ||
                    Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var model = _db.ChiSoGiaTdDm.Where(x=>x.Matt=="1");
                    ViewData["dvtinh"] = _db.DmDvt.ToList();
                    ViewData["Title"] = " Thông tin chi tiết hồ sơ";
                    ViewData["listTt"] = _db.ChiSoGiaTdDm;
                    ViewData["MenuLv1"] = "menu_csg";
                    ViewData["MenuLv2"] = "menu_csgDm";
                    ViewData["MenuLv3"] = "menu_csgDm_2019";
                    return View("Views/Admin/Manages/ChiSoGiaTd/Danhmuc/Index.cshtml", model);

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
        [Route("Danhsachnhomhanghoa/Group")]
        [HttpGet]
        public IActionResult Group(string Matt)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.chisogia.danhmuc", "Index") ||
                    Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    List<ChiSoGiaTdDm> model = new List<ChiSoGiaTdDm>();
                    if (string.IsNullOrEmpty(Matt))
                    {
                        model= _db.ChiSoGiaTdDm.Where(x => x.Matt != "1").ToList();
                    }
                    else
                    {
                        model = _db.ChiSoGiaTdDm.Where(x=>x.Matt==Matt).ToList();
                    }
                    //var model = _db.ChiSoGiaTdDm.Where(x => x.Matt != "").ToList();
                    ViewData["dvtinh"] = _db.DmDvt.ToList();
                    ViewData["Title"] = " Thông tin chi tiết hồ sơ";
                    ViewData["matt"] = Matt;
                    ViewData["listTt"] = _db.ChiSoGiaTdDm.Where(x=>x.Matt!="1");
                    ViewData["MenuLv1"] = "menu_csg";
                    ViewData["MenuLv2"] = "menu_csgDm";
                    ViewData["MenuLv3"] = "menu_csgDm_group";
                    return View("Views/Admin/Manages/ChiSoGiaTd/Danhmuc/Group.cshtml", model);

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
        [Route("Danhsachnhomhanghoa/Import")]
        [HttpPost]
        public async Task<JsonResult> Import(string Mahanghoa, string Tennhomhang, string Masonhom, string Magoc,
             string Baocao,string Nam,double Nt,double Tt, int Sheet, int LineStart, int LineStop, IFormFile FormFile)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.chisogia.danhmuc", "Create"))
                {
                    LineStart = LineStart == 0 ? 1 : LineStart;
                    var list_add = new List<ChiSoGiaTdDm>();
                    int sheet = Sheet == 0 ? 0 : (Sheet - 1);
                    using (var stream = new MemoryStream())
                    {
                        await FormFile.CopyToAsync(stream);
                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[sheet];
                            var rowcount = worksheet.Dimension.Rows;
                            LineStop = LineStop > rowcount ? rowcount : LineStop;
                            var time = DateTime.Now.ToString("yyMMddssmmHH");
                            for (int row = LineStart; row <= LineStop; row++)
                            {
                                list_add.Add(new ChiSoGiaTdDm
                                {
                                    Created_at = DateTime.Now,
                                    Updated_at = DateTime.Now,
                                    Matt = time,
                                    Nam = Nam,
                                    Baocao = Baocao,
                                    Masohanghoa = worksheet.Cells[row, Int16.Parse(Mahanghoa)].Value.ToString() != null ?
                                                worksheet.Cells[row, Int16.Parse(Mahanghoa)].Value.ToString().Trim() : "",
                                    Tenhanghoa = worksheet.Cells[row, Int16.Parse(Tennhomhang)].Value.ToString() != null ?
                                                worksheet.Cells[row, Int16.Parse(Tennhomhang)].Value.ToString().Trim() : "",

                                    Masonhomhanghoa = worksheet.Cells[row, Int16.Parse(Masonhom)].Value != null ?
                                                worksheet.Cells[row, Int16.Parse(Masonhom)].Value.ToString().Trim() : "",

                                    Masogoc = worksheet.Cells[row, Int16.Parse(Magoc)].Value != null ?
                                                worksheet.Cells[row, Int16.Parse(Magoc)].Value.ToString().Trim() : "",

                                    QuyensoNt = Convert.ToDouble(worksheet.Cells[row, Int16.Parse(Nt.ToString())].Value) != 0 ?
                                                System.Math.Round(Convert.ToDouble(worksheet.Cells[row, Int16.Parse(Nt.ToString())].Value), 5) : 0,
                                    QuyensoTt = Convert.ToDouble(worksheet.Cells[row, Int16.Parse(Tt.ToString())].Value) != 0 ?
                                                System.Math.Round(Convert.ToDouble(worksheet.Cells[row, Int16.Parse(Tt.ToString())].Value), 5) : 0,

                                    /* Gia = Convert.ToDouble(worksheet.Cells[row, Int16.Parse(Gia.ToString())].Value) != 0 ?
                                                 System.Math.Round(Convert.ToDouble(worksheet.Cells[row, Int16.Parse(Gia.ToString())].Value), 5) : 0,*/
                                }); ; ; ;
                            }

                        }
                    }
                    _db.ChiSoGiaTdDm.AddRange(list_add);
                    _db.SaveChanges();

                    var data = new { status = "success" };
                    return Json(data);
                }
                else
                {
                    var data = new { status = "error", message = "Bạn không có quyền thực hiện chức năng này!!!" };
                    return Json(data);
                }
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }
        [Route("Danhsachnhomhanghoa/Export")]
        [HttpGet]
        public IActionResult Export()
        {
            var firstDm = _db.ChiSoGiaTdDm.FirstOrDefault();
            var model = _db.ChiSoGiaTdDm.Where(x => x.Matt == firstDm.Matt);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Danh sách nhóm hàng hóa");
            workSheet.Cells.Style.Font.Name = "Times New Roman";
            workSheet.Cells.Style.Font.Size = 12;
           
            workSheet.Cells[1, 1].Value = "Tên nhóm hàng";
            workSheet.Cells[1, 2].Value = "Mã số nhóm hàng";
            workSheet.Cells[1, 3].Value = "Mã số gốc";
            workSheet.Cells[1, 4].Value = "Mã số hàng hoá";
            workSheet.Cells[1, 5].Value = "Quyền số nông thôn";
            workSheet.Cells[1, 6].Value = "Quyền số thành thị";
            workSheet.Cells[1, 1, 1, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[1, 1, 1, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[1, 1, 1, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
            workSheet.Cells[1, 1, 1, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            int record_index = 2;
            foreach (var item in model)
            {
                workSheet.Cells[record_index, 1].Value = item.Tenhanghoa;
                workSheet.Cells[record_index, 2].Value = item.Masonhomhanghoa;
                workSheet.Cells[record_index, 3].Value = item.Masogoc;
                workSheet.Cells[record_index, 4].Value = item.Masohanghoa;
                workSheet.Cells[record_index, 5].Value = item.QuyensoNt;
                workSheet.Cells[record_index, 6].Value = item.QuyensoTt;
                workSheet.Cells[record_index, 1, record_index, 6].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[record_index, 1, record_index, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[record_index, 1, record_index, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                workSheet.Cells[record_index, 1, record_index, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                record_index++;
            }
            workSheet.Row(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(2).Style.Font.Bold = true;
            workSheet.Cells.AutoFitColumns();
            workSheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            string excelName = "mauNhomhangs" + ".xlsx";
            using (var stream = new MemoryStream())
            {
                excel.SaveAs(stream);
                var content = stream.ToArray();
                return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }

        }
    }
}
