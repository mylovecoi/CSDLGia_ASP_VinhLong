using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
//using OfficeOpenXml;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaSpDvCongIch
{
    public class GiaSpDvCongIchDmController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _env;

        public GiaSpDvCongIchDmController(CSDLGiaDBContext db, IWebHostEnvironment hostingEnv)
        {
            _db = db;
            _env = hostingEnv;
        }

        [Route("GiaSpDvCongIchDmCt")]
        [HttpGet]
        public IActionResult Index(string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.danhmuc", "Index"))
                {
                    var model = _db.GiaSpDvCongIchDm.Where(t => t.Manhom == Manhom).ToList();

                    ViewData["Manhom"] = Manhom;
                    ViewData["Tennhom"] = _db.GiaSpDvCongIchNhom.FirstOrDefault(t => t.Manhom == Manhom).Tennhom;
                    ViewData["Title"] = "Danh mục chi tiết nhóm sản phẩm dịch vụ công ích";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdvci";
                    ViewData["MenuLv3"] = "menu_dgdvci_dm";
                    ViewData["Donvitinh"] = _db.DmDvt.ToList();
                    ViewData["PhanLoaiDichVu"] = _db.GiaSpDvCongIchDm.ToList();
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCongIch/DanhMuc/ChiTiet/Index.cshtml", model);
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

        [Route("GiaSpDvCongIchDmCt/Store")]
        [HttpPost]
        public JsonResult Store(string Manhom, string Tenspdv, string Mota, string Dvt, double Mucgiatu, double Mucgiaden, string Hientrang, string Maso, string Ten, string HienThi, string Magoc, string Capdo)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.danhmuc", "Create"))
                {
                    var request = new GiaSpDvCongIchDm
                    {
                        Manhom = Manhom,
                        Maso = Maso,
                        Ten = Ten,
                        HienThi = HienThi,
                        Magoc = Magoc,
                        Capdo = Capdo,
                        Dvt = Dvt,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaSpDvCongIchDm.Add(request);
                    _db.SaveChanges();

                    var checkdvt = _db.DmDvt.FirstOrDefault(t => t.Dvt == Dvt); // kiểm tra đơn vị tính nếu không có giá trị thì thêm vào
                    if (checkdvt == null)
                    {
                        var dvt = new DmDvt
                        {
                            Dvt = Dvt,
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        };
                        _db.DmDvt.Add(dvt);
                        _db.SaveChanges();

                    }

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

        [Route("GiaSpDvCongIchDmCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.danhmuc", "Edit"))
                {
                    var model = _db.GiaSpDvCongIchDm.FirstOrDefault(p => p.Id == Id);
                    var phanloai = _db.GiaSpDvCongIchDm;
                    var list = (from t in phanloai
                                group t by t.Phanloai into grp
                                select new
                                {
                                    pl = grp.Key
                                });
                    if (model != null)
                    {
                        string result = "<div class='row' id='edit_thongtin'>";

                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Mã số</label>";
                        result += "<input type='text' id='maso_edit' name='maso_edit' class='form-control' value='" + model.Maso + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Tên</label>";
                        result += "<input type='text' id='ten_edit' name='ten_edit' class='form-control' value='" + model.Ten + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Mã gốc</label>";
                        result += "<input type='text' id='magoc_edit' name='magoc_edit' class='form-control' value='" + model.Magoc + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Cấp độ</label>";
                        result += "<select id='capdo_edit' name='capdo_edit' class='form-control select2me select2-offscreen' tabindex='-1' title=''>";
                        result += "<option value ='" + 1 + "'>" + 1 + "</ option >";
                        result += "<option value ='" + 2 + "'>" + 2 + "</ option >";
                        result += "<option value ='" + 3 + "'>" + 3 + "</ option >";
                        result += "<option value ='" + 4 + "'>" + 4 + "</ option >";
                        result += "</select>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Hiển thị</label>";
                        result += "<input type='text' id='hienthi_edit' name='hienthi_edit' class='form-control' value='" + model.HienThi + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Đơn vị tính</label>";
                        result += "<input type='text' id='dvt_edit' name='dvt_edit' class='form-control' value='" + model.Dvt + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<input hidden type='text' id='id_edit' name='id_edit' value='" + model.Id + "'/>";
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

        [Route("GiaSpDvCongIchDmCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Dvt, string Magoc, string Ten, string Capdo, string HienThi)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.danhmuc", "Edit"))
                {
                    var model = _db.GiaSpDvCongIchDm.FirstOrDefault(t => t.Id == Id);
                    model.Magoc = Magoc;
                    model.Ten = Ten;
                    model.Magoc = Magoc;
                    model.Capdo = Capdo;
                    model.HienThi = HienThi;
                    model.Dvt = Dvt;

                    model.Updated_at = DateTime.Now;
                    _db.GiaSpDvCongIchDm.Update(model);
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

        [Route("GiaSpDvCongIchDmCt/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.danhmuc", "Delete"))
                {
                    var model = _db.GiaSpDvCongIchDm.FirstOrDefault(p => p.Id == id_delete);
                    _db.GiaSpDvCongIchDm.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaSpDvCongIchDm", new { Manhom = model.Manhom });
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

        [Route("GiaSpDvCongIchDmCt/Lock")]
        [HttpPost]
        public IActionResult Lock(string Manhom, string Theodoi)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.danhmuc", "Edit"))
                {
                    var model = _db.GiaSpDvCongIchDm.Where(t => t.Manhom == Manhom).ToList();
                    model.ForEach(t => { t.Hientrang = Theodoi; });
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

        [Route("GiaSpDvCongIchDmCt/Excel")]
        [HttpPost]
        public async Task<JsonResult> Excel(string Manhom, string Mota, string Ten, double Mucgiatu, double Mucgiaden, string Phanloai,
            string Hientrang, string Dvt, int Sheet, int LineStart, int LineStop, IFormFile FormFile)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe", "Edit"))
                {
                    LineStart = LineStart == 0 ? 1 : LineStart;
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
                            //var n = 1;
                            for (int row = LineStart; row <= LineStop; row++)
                            {
                                var list_add = new GiaSpDvCongIchDm
                                {
                                    Manhom = Manhom,
                                    Created_at = DateTime.Now,
                                    Updated_at = DateTime.Now,
                                    Maspdv = DateTime.Now.ToString("yyMMddfffssmmHH"),

                                    Mota = worksheet.Cells[row, Int16.Parse(Mota)].Value.ToString() != null ?
                                                worksheet.Cells[row, Int16.Parse(Mota)].Value.ToString().Trim() : "",

                                    Tenspdv = worksheet.Cells[row, Int16.Parse(Ten)].Value != null ?
                                                worksheet.Cells[row, Int16.Parse(Ten)].Value.ToString().Trim() : "",

                                    Mucgiatu = Convert.ToDouble(worksheet.Cells[row, Int16.Parse(Mucgiatu.ToString())].Value) != 0 ?
                                                Convert.ToDouble(worksheet.Cells[row, Int16.Parse(Mucgiatu.ToString())].Value) : 0,

                                    Mucgiaden = Convert.ToDouble(worksheet.Cells[row, Int16.Parse(Mucgiaden.ToString())].Value) != 0 ?
                                                Convert.ToDouble(worksheet.Cells[row, Int16.Parse(Mucgiaden.ToString())].Value) : 0,
                                    Phanloai = worksheet.Cells[row, Int16.Parse(Phanloai)].Value != null ?
                                                worksheet.Cells[row, Int16.Parse(Phanloai)].Value.ToString().Trim() : "",
                                    Hientrang = worksheet.Cells[row, Int16.Parse(Hientrang)].Value != null ?
                                                worksheet.Cells[row, Int16.Parse(Hientrang)].Value.ToString().Trim() : "",
                                    Dvt = worksheet.Cells[row, Int16.Parse(Dvt)].Value != null ?
                                                worksheet.Cells[row, Int16.Parse(Dvt)].Value.ToString().Trim() : "",


                                };
                                _db.GiaSpDvCongIchDm.Add(list_add);
                            }

                        }
                    }
                    //_db.GiaDvKcbDm.AddRange(list_add);
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
    }
}
