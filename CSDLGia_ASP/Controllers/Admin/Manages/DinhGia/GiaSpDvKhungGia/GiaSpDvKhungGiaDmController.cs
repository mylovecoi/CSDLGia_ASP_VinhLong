using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
//using OfficeOpenXml;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaSpDvKhungGia
{
    public class GiaSpDvKhungGiaDmController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _env;

        public GiaSpDvKhungGiaDmController(CSDLGiaDBContext db, IWebHostEnvironment hostingEnv)
        {
            _db = db;
            _env = hostingEnv;
        }

        [Route("GiaSpDvKhungGiaDmCt")]
        [HttpGet]
        public IActionResult Index(string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvkhunggia.danhmuc", "Index"))
                {
                    var model = _db.GiaSpDvKhungGiaDm.Where(t => t.Manhom == Manhom);

                    ViewData["Manhom"] = Manhom;
                    ViewData["Tennhom"] = _db.GiaSpDvKhungGiaNhom.FirstOrDefault(t => t.Manhom == Manhom).Tennhom;
                    ViewData["Title"] = "Thông tin chi tiết nhóm sản phẩm dịch vụ khung giá";
                    ViewData["MenuLv1"] = "menu_spdvkhunggia";
                    ViewData["MenuLv2"] = "menu_spdvkhunggia_dm";
                    ViewData["Donvitinh"] = _db.DmDvt.ToList();
                    ViewData["PhanLoaiDichVu"] = _db.GiaSpDvKhungGiaDm.ToList();
                    var chkSTT = _db.GiaSpDvKhungGiaDm.Where(t => t.Manhom == Manhom);
                    if (chkSTT.Any())
                    {
                        ViewData["STT"] = chkSTT.Max(x=>x.SapXep) + 1;
                    }
                    else
                    {
                        ViewData["STT"] = 1;
                    }
                    
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvKhungGia/DanhMuc/ChiTiet/Index.cshtml", model.ToList());
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

        [Route("GiaSpDvKhungGiaDmCt/Store")]
        [HttpPost]
        public JsonResult Store(string Manhom, string Tenspdv, string Dvt, string Hientrang, string HienThi, double SapXep)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvkhunggia.danhmuc", "Create"))
                {
                    var request = new GiaSpDvKhungGiaDm
                    {
                        Manhom = Manhom,
                        Dvt = Dvt,
                        Tenspdv = Tenspdv,
                        HienTrang = Hientrang,
                        HienThi = HienThi,
                        SapXep = SapXep,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaSpDvKhungGiaDm.Add(request);
                    _db.SaveChanges();

                    var data = new { status = "success", message = "Thêm mới thành công!" };
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

        [Route("GiaSpDvKhungGiaDmCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvkhunggia.danhmuc", "Edit"))
                {
                    var model = _db.GiaSpDvKhungGiaDm.FirstOrDefault(p => p.Id == Id);
                    if (model != null)
                    {
                        string result = "<div class='row' id='edit_thongtin'>";

                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Đối tượng cụ thể</label>";
                        result += "<input type='text' id='tenspdv_edit' name='tenspdv_edit' class='form-control' value='" + model.Tenspdv + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-12'>";
                        result += "<label class='form-control-label'>Đơn vị tính</label>";
                        result += "<select id='dvt_edit' name='dvt_edit' class='form-control select2me select2-offscreen' tabindex='-1' title=''>";

                        var dvt = _db.DmDvt.ToList();
                        result += "<option value =''>--Chọn đơn vị tính--</option>";
                        foreach (var item in dvt)
                        {
                            if(model.Dvt == item.Dvt)
                            {
                                result += "<option value ='" + @item.Dvt + "' selected >" + @item.Dvt + "</ option >";
                            }
                            else
                            {
                                result += "<option value ='" + @item.Dvt + "' >" + @item.Dvt + "</ option >";
                            }                           
                        }
                        result += "</select>";
                        result += "</div>";

                       
                        result += "<div class='col -xl-6'>";
                        result += "<div class='form -group fv-plugins-icon-container'>";
                        result += "<label>STT báo cáo</label>";
                        result += "<input type='text' id='hienthi_edit' name='hienthi_edit' class='form-control' value='" + model.HienThi + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col -xl-6'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Sắp xếp</label>";
                        result += "<input type='text' id='sapxep_edit' name='sapxep_edit' class='form-control' value='" + model.SapXep + "'/>";
                        result += "</div>";
                        result += "</div>";
                       

                        result += "<input hidden type='text' id='id_edit' name='id_edit' value='" + model.Id + "'/>";
                        result += "</div>";


                        var data = new { status = "success", message = result };
                        return Json(data);
                    }
                    else
                    {
                        var data = new { status = "error", message = "Không tìm thấy thông tin cần chỉnh sửa!!!" };
                        return Json(data);
                    }
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

        [Route("GiaSpDvKhungGiaDmCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Tenspdv, string Dvt, string Hientrang, string HienThi, double SapXep)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvkhunggia.danhmuc", "Edit"))
                {
                    var model = _db.GiaSpDvKhungGiaDm.FirstOrDefault(t => t.Id == Id);
                    model.Tenspdv = Tenspdv;                   
                    model.Dvt = Dvt;
                    model.HienThi = HienThi;
                    model.SapXep = SapXep;
                    model.Updated_at = DateTime.Now;
                    _db.GiaSpDvKhungGiaDm.Update(model);
                    _db.SaveChanges();

                    var data = new { status = "success", message = "Cập nhật thành công!" };
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

        [Route("GiaSpDvKhungGiaDmCt/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvkhunggia.danhmuc", "Delete"))
                {
                    var model = _db.GiaSpDvKhungGiaDm.FirstOrDefault(p => p.Id == id_delete);
                    _db.GiaSpDvKhungGiaDm.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaSpDvKhungGiaDm", new { Manhom = model.Manhom });
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

        [Route("GiaSpDvKhungGiaDmCt/Lock")]
        [HttpPost]
        public IActionResult Lock(string Manhom, string Theodoi)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvkhunggia.danhmuc", "Edit"))
                {
                    var model = _db.GiaSpDvKhungGiaDm.Where(t => t.Manhom == Manhom).ToList();
                    model.ForEach(t => { t.HienTrang = Theodoi; });
                    _db.SaveChanges();

                    var data = new { status = "success", message = "Khóa/mở khóa danh mục thành công!" };
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

        //[Route("GiaSpDvKhungGiaDmCt/Excel")]
        //[HttpPost]
        //public async Task<JsonResult> Excel(string Manhom, string Level, string Cap1, string Cap2, string Cap3,
        //    string Cap4, string Cap5, string Dvt, string Ten, int Sheet, int LineStart, int LineStop, IFormFile FormFile)
        //{
        //    if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
        //    {
        //        if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvkhunggia.danhmuc", "Edit"))
        //        {
        //            LineStart = LineStart == 0 ? 1 : LineStart;
        //            var list_add = new List<GiaSpDvKhungGiaDm>();
        //            int sheet = Sheet == 0 ? 0 : (Sheet - 1);
        //            using (var stream = new MemoryStream())
        //            {
        //                await FormFile.CopyToAsync(stream);
        //                using (var package = new ExcelPackage(stream))
        //                {
        //                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        //                    ExcelWorksheet worksheet = package.Workbook.Worksheets[sheet];
        //                    var rowcount = worksheet.Dimension.Rows;
        //                    LineStop = LineStop > rowcount ? rowcount : LineStop;

        //                    for (int row = LineStart; row <= LineStop; row++)
        //                    {
        //                        list_add.Add(new GiaSpDvKhungGiaDm
        //                        {
        //                            Manhom = Manhom,
        //                            Theodoi = "TD",
        //                            Created_at = DateTime.Now,
        //                            Updated_at = DateTime.Now,

        //                            Level = worksheet.Cells[row, Int16.Parse(Level)].Value.ToString() != null ?
        //                                        worksheet.Cells[row, Int16.Parse(Level)].Value.ToString().Trim() : "",

        //                            Cap1 = worksheet.Cells[row, Int16.Parse(Cap1)].Value != null ?
        //                                        worksheet.Cells[row, Int16.Parse(Cap1)].Value.ToString().Trim() : "",

        //                            Cap2 = worksheet.Cells[row, Int16.Parse(Cap2)].Value != null ?
        //                                        worksheet.Cells[row, Int16.Parse(Cap2)].Value.ToString().Trim() : "",

        //                            Cap3 = worksheet.Cells[row, Int16.Parse(Cap3)].Value != null ?
        //                                        worksheet.Cells[row, Int16.Parse(Cap3)].Value.ToString().Trim() : "",

        //                            Cap4 = worksheet.Cells[row, Int16.Parse(Cap4)].Value != null ?
        //                                        worksheet.Cells[row, Int16.Parse(Cap4)].Value.ToString().Trim() : "",

        //                            Cap5 = worksheet.Cells[row, Int16.Parse(Cap5)].Value != null ?
        //                                        worksheet.Cells[row, Int16.Parse(Cap5)].Value.ToString().Trim() : "",

        //                            Ten = worksheet.Cells[row, Int16.Parse(Ten)].Value != null ?
        //                                        worksheet.Cells[row, Int16.Parse(Ten)].Value.ToString().Trim() : "",

        //                            Dvt = worksheet.Cells[row, Int16.Parse(Dvt)].Value != null ?
        //                                        worksheet.Cells[row, Int16.Parse(Dvt)].Value.ToString().Trim() : "",
        //                        });
        //                    }

        //                }
        //            }
        //            _db.GiaSpDvKhungGiaDm.AddRange(list_add);
        //            _db.SaveChanges();

        //            var data = new { status = "success" };
        //            return Json(data);
        //        }
        //        else
        //        {
        //            var data = new { status = "error", message = "Bạn không có quyền thực hiện chức năng này!!!" };
        //            return Json(data);
        //        }
        //    }
        //    else
        //    {
        //        var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
        //        return Json(data);
        //    }
        //}
    }
}
