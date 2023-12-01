using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.ViewModels.Manages.ChiSoGiaTd;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.ChiSoGiaTd
{
    public class ChiSoGiaTdHhCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public ChiSoGiaTdHhCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DanhmuchanghoaChitiet")]
        [HttpGet]
        public IActionResult Index(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csg.chisogia.hanghoa", "Index") ||
                    Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    /*var model = (from cpiHhCt in _db.ChiSoGiaTdHhCt.Where(x=>x.Mahs==Mahs)
                                 join cpiDmCt in _db.ChiSoGiaTdDmCt.Where(x=>x.Masonhomhanghoa=="4")
                                 on cpiHhCt.Masogoc equals cpiDmCt.Masohanghoa
                                 select new VMChiSoGiaTdHh
                                 {
                                     Id = cpiHhCt.Id,
                                     Masogoc = cpiHhCt.Masogoc,
                                     Masohanghoa=cpiHhCt.Masohanghoa,
                                     Tenhanghoa = cpiHhCt.Tenhanghoa,
                                     Tennhom = cpiDmCt.Tenhanghoa,
                                     Giagoc = cpiHhCt.Giagoc,
                                     Giakychon = cpiHhCt.Giakychon,
                                     Dvt = cpiHhCt.Dvt,
                                     Mahs= cpiHhCt.Mahs,
                                 });*/
                    var model = _db.ChiSoGiaTdHhCt.Where(x => x.Mahs == Mahs);
                    var getInfo = _db.ChiSoGiaTd.FirstOrDefault(x => x.Mahs == Mahs);
                    ViewData["model"] = model;
                    ViewData["danhsach"] = _db.ChiSoGiaTd;
                    ViewData["nhomModel"] = _db.ChiSoGiaTdDmCt.Where(x => x.Mahs == Mahs);
                    ViewData["dvtinh"] = _db.DmDvt.ToList();
                    ViewData["mahs"] = Mahs;
                    ViewData["thang"] = getInfo.Thang;
                    ViewData["nam"] = getInfo.Nam;
                    ViewData["Title"] = " Thông tin chi tiết danh mục";
                    ViewData["MenuLv1"] = "menu_csg";
                    ViewData["MenuLv2"] = "menu_csgHs";
                    ViewData["MenuLv3"] = "menu_csgHs_ds";
                    return View("Views/Admin/Manages/ChiSoGiaTd/Hanghoact/Index.cshtml");

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
        [Route("DanhmuchanghoaChitiet/Import")]
        [HttpPost]
        public async Task<JsonResult> Import(string Mahanghoa, double Giakychon,
             string Mahs, int Sheet, int LineStart, int LineStop, IFormFile FormFile)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csg.chisogia.hanghoa", "Edit"))
                {
                    LineStart = LineStart == 0 ? 1 : LineStart;
                    var list_add = _db.ChiSoGiaTdHhCt.Where(x => x.Mahs == Mahs);
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
                            for (int row = LineStart; row <= LineStop; row++)
                            {
                                foreach (var list in list_add)
                                {
                                    var checkMasohanghoa = worksheet.Cells[row, Int16.Parse(Mahanghoa)].Value.ToString() != null ?
                                                worksheet.Cells[row, Int16.Parse(Mahanghoa)].Value.ToString().Trim() : "";
                                    if (list.Masohanghoa == checkMasohanghoa)
                                    {
                                        list.Created_at = DateTime.Now;
                                        list.Updated_at = DateTime.Now;
                                        list.Giakychon = Convert.ToDouble(worksheet.Cells[row, Int16.Parse(Giakychon.ToString())].Value) != 0 ?
                                                Convert.ToDouble(worksheet.Cells[row, Int16.Parse(Giakychon.ToString())].Value) : 0;
                                        _db.ChiSoGiaTdHhCt.UpdateRange(list);
                                    }
                                }
                            }

                        }
                    }
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
        [Route("DanhmuchanghoaChitiet/Edit")]
        [HttpPost]
        public IActionResult Edit(int id_edit, double giakychon_edit)
        {
            var update = _db.ChiSoGiaTdHhCt.FirstOrDefault(x => x.Id == id_edit);
            update.Giakychon = giakychon_edit;
            _db.ChiSoGiaTdHhCt.Update(update);
            _db.SaveChanges();

            var model = (from cpiHhCt in _db.ChiSoGiaTdHhCt.Where(x => x.Mahs == update.Mahs)
                         join cpiDmCt in _db.ChiSoGiaTdDmCt.Where(x => x.Masonhomhanghoa == "4")
                         on cpiHhCt.Masogoc equals cpiDmCt.Masohanghoa
                         select new VMChiSoGiaTdHh
                         {
                             Id = cpiHhCt.Id,
                             Masogoc = cpiHhCt.Masogoc,
                             Masohanghoa = cpiHhCt.Masohanghoa,
                             Tenhanghoa = cpiHhCt.Tenhanghoa,
                             Tennhom = cpiDmCt.Tenhanghoa,
                             Giagoc = cpiHhCt.Giagoc,
                             Giakychon = cpiHhCt.Giakychon,
                             Dvt = cpiHhCt.Dvt,
                             Mahs = cpiHhCt.Mahs,
                         });
            var getInfo = _db.ChiSoGiaTd.FirstOrDefault(x => x.Mahs == update.Mahs);
            ViewData["model"] = model;
            ViewData["danhsach"] = _db.ChiSoGiaTd;
            ViewData["nhom"] = _db.ChiSoGiaTdDm;
            ViewData["dvtinh"] = _db.DmDvt.ToList();
            ViewData["mahs"] = update.Mahs;
            ViewData["thang"] = getInfo.Thang;
            ViewData["nam"] = getInfo.Nam;
            ViewData["Title"] = " Thông tin chi tiết danh mục";
            ViewData["MenuLv1"] = "menu_csg";
            ViewData["MenuLv2"] = "menu_csgTc";
            ViewData["MenuLv3"] = "menu_csgTc";
            return View("Views/Admin/Manages/ChiSoGiaTd/Hanghoact/Index.cshtml");
        }
    }
}
