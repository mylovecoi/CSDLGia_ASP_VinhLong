using CSDLGia_ASP.Models.Manages.KeKhaiGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
using CSDLGia_ASP.Database;
using CSDLGia_ASP.Models.Manages.DinhGia;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDatPl
{
    public class GiaDatPlExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaDatPlExcelController(CSDLGiaDBContext db)
        {
            _db = db;
        }


        public IActionResult Index(string Madv)
        {

            ViewData["Madv"] = Madv;
            ViewData["Title"] = "Thông tin hồ sơ giá các loại đất";
            return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Import(GiaDatPhanLoaiExcel request )
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {

                request.LineStart = request.LineStart == 0 ? 1 : request.LineStart;
                var list_add = new List<GiaDatPhanLoaiExcel>();
                int sheet = request.Sheet == 0 ? 0 : (request.Sheet - 1);
                using (var stream = new MemoryStream())
                {
                    await request.FormFile.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[sheet];
                        var rowcount = worksheet.Dimension.Rows;
                        request.LineStop = request.LineStop > rowcount ? rowcount : request.LineStop;

                        for (int row = request.LineStart; row <= request.LineStop; row++)
                        {
                            list_add.Add(new GiaDatPhanLoaiExcel
                            {
                                Mahs = request.Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                                Madv = request.Madv,
                                Created_at = DateTime.Now,
                                Updated_at = DateTime.Now,

                                Soqd = worksheet.Cells[row, Int16.Parse(request.Soqd)].Value != null ?
                                            worksheet.Cells[row, Int16.Parse(request.Soqd)].Value.ToString().Trim() : "",

                            });
                        }

                    }

                }
                /*return Ok(list_add);*/
                _db.GiaDatPhanLoaiExcel.AddRange(list_add);
                _db.SaveChanges();

                return RedirectToAction("Index", "GiaDatPl" );
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }


        //public IActionResult Index(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/Index.cshtml");
        //}
        //public IActionResult Index1(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/1.cshtml");
        //}
        //public IActionResult Index2(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/2.cshtml");
        //}
        //public IActionResult Index3(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/3.cshtml");
        //}
        //public IActionResult Index4(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/4.cshtml");
        //}
        //public IActionResult Index5(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/5.cshtml");
        //}
        //public IActionResult Index6(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/6.cshtml");
        //}
        //public IActionResult Index7(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/7.cshtml");
        //}
        //public IActionResult Index8(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/8.cshtml");
        //}


        //public IActionResult Index9(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/9.cshtml");
        //}
        //public IActionResult Index10(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/10.cshtml");
        //}
        //public IActionResult Index11(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/11.cshtml");
        //}
        //public IActionResult Index12(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/12.cshtml");
        //}
        //public IActionResult Index13(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/13.cshtml");
        //}
        //public IActionResult Index14(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/14.cshtml");
        //}
        //public IActionResult Index15(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/15.cshtml");
        //}
        //public IActionResult Index16(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/16.cshtml");
        //}
        //public IActionResult Index17(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/17.cshtml");
        //}
        //public IActionResult Index18(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/18.cshtml");
        //}
        //public IActionResult Index20(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/20.cshtml");
        //}
        //public IActionResult Index21(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/21.cshtml");
        //}


        //public IActionResult Index22(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/22.cshtml");
        //}
        //public IActionResult Index23(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/23.cshtml");
        //}
        //public IActionResult Index24(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/24.cshtml");
        //}
        //public IActionResult Index25(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/25.cshtml");
        //}
        //public IActionResult Index26(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/26.cshtml");
        //}
        //public IActionResult Index27(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/27.cshtml");
        //}
        //public IActionResult Index28(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/28.cshtml");
        //}
        //public IActionResult Index29(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/29.cshtml");
        //}

        //public IActionResult Index30(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/30.cshtml");
        //}
        //public IActionResult Index31(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/31.cshtml");
        //}
        //public IActionResult Index32(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/32.cshtml");
        //}
        //public IActionResult Index33(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/33.cshtml");
        //}
        //public IActionResult Index34(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/34.cshtml");
        //}
        //public IActionResult Index35(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/35.cshtml");
        //}
        //public IActionResult Index36(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/36.cshtml");
        //}


        //public IActionResult Index37(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/37.cshtml");
        //}
        //public IActionResult Index38(string Madv)
        //{

        //    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Excel/38.cshtml");
        //}


    }
}
