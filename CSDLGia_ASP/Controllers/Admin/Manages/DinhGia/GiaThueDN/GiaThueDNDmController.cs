using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CSDLGia_ASP.Database;
using System.Security.Cryptography;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSDLGia_ASP.Models.Systems;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
//using OfficeOpenXml;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaSpDvCuThe
{
    public class GiaSpDvCuTheDmController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _env;

        public GiaSpDvCuTheDmController(CSDLGiaDBContext db, IWebHostEnvironment hostingEnv)
        {
            _db = db;
            _env = hostingEnv;
        }

        [Route("GiaSpDvCuTheDmCt")]
        [HttpGet]
        public IActionResult Index(string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.spdvcuthe.danhmuc", "Index"))
                {
                    var model = _db.GiaSpDvCuTheDm.Where(t => t.Manhom == Manhom).ToList();

                    ViewData["Manhom"] = Manhom;
                    ViewData["Tennhom"] = _db.GiaSpDvCuTheNhom.FirstOrDefault(t => t.Manhom == Manhom).Tennhom;
                    ViewData["Title"] = "Thông tin chi tiết sản phẩm dịch vụ cụ thể";
                    ViewData["MenuLv1"] = "menu_spdvcuthe";
                    ViewData["MenuLv2"] = "menu_spdvcuthe_dm";
                    ViewData["Donvitinh"] = _db.DmDvt.ToList();
                    ViewData["PhanLoaiDichVu"] = _db.GiaSpDvCuTheDm.ToList();
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCuThe/DanhMuc/ChiTiet/Index.cshtml", model);
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

        [Route("GiaSpDvCuTheDmCt/Store")]
        [HttpPost]
        public JsonResult Store(string Manhom, string Tenspdv, string Mota, string Dvt, double Gia, string Hientrang, string Phanloai)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.spdvcuthe.danhmuc", "Create"))
                {
                    var request = new GiaSpDvCuTheDm
                    {
                        Manhom = Manhom,
                        Maspdv= DateTime.Now.ToString("yyMMddfffssmmHH"),
                        Dvt = Dvt,
                        Tenspdv = Tenspdv,
                        Mota = Mota,
                        Gia = Gia,
                        Phanloai = Phanloai,
                        Hientrang = Hientrang,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaSpDvCuTheDm.Add(request);
                    _db.SaveChanges();

                    var checkdvt = _db.DmDvt.FirstOrDefault(t => t.Dvt == Dvt); // kiểm tra đơn vị tính nếu không có giá trị thì thêm vào
                    if (checkdvt==null)
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

        [Route("GiaSpDvCuTheDmCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.spdvcuthe.danhmuc", "Edit"))
                {
                    var model = _db.GiaSpDvCuTheDm.FirstOrDefault(p => p.Id == Id);
                    var phanloai = _db.GiaSpDvCuTheDm;
                    var list = (from t in phanloai
                                group t by t.Phanloai into grp
                                select new
                                {
                                    pl=grp.Key
                                });
                    if (model != null)
                    {
                        string result = "<div class='row' id='edit_thongtin'>";

                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Tên Sản Phẩm</label>";
                        result += "<input type='text' id='tennhom_edit' name='tennhom_edit' class='form-control' value='" + model.Tenspdv + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Mô tả</label>";
                        result += "<input type='text' id='mota_edit' name='mota_edit' class='form-control' value='" + model.Mota + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Giá</label>";
                        result += "<input type='text' id='gia_edit' name='gia_edit' class='form-control' value='" + model.Gia + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-4'>";
                        result += "<label class='form-control-label'>Đơn vị tính</label>";
                        result += "<select id='dvt_edit' name='dvt_edit' class='form-control select2me select2-offscreen' tabindex='-1' title=''>";

                        var dvt = _db.DmDvt.ToList();
                        foreach (var item in dvt)
                        {
                            result += "<option value ='" + @item.Dvt + "'>" + @item.Dvt + "</ option >";
                        }
                        result += "</select>";
                        result += "</div>";
                        result += "<div class='col-xl-1' style='padding-left: 0px;'>";
                        result += "<label class='control-label'>Thêm</label>";
                        result += "<button type='button' class='btn btn-default' data-target='#Dvt_Modal_edit' data-toggle='modal'><i class='la la-plus'></i>";
                        result += "</button>";
                        result += "</div>";

                        result += "<div class='col-xl-7'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Trạng thái</label>";
                        result += "<select id='hientrang_edit' name='hientrang_edit' class='form-control'>";
                        result += "<option value='TD' " + ((string)model.Hientrang == "TD" ? "selected" : "") + ">Theo dõi</option>";
                        result += "<option value='KTD' " + ((string)model.Hientrang == "KTD" ? "selected" : "") + ">Không theo dõi</option>";
                        result += "</select>";
                        result += "</div>";
                        result += "</div>";


                        result += "<div class='col-xl-11'>";
                        result += "<label class='form-control-label'>Phân loại sản phẩm, dịch vụ</label>";
                        result += "<select id='plspdv_edit' name='plspdv_edit' class='form-control select2me select2-offscreen' tabindex='-1' title=''>";

                        var plspdvcuthe = _db.GiaSpDvCuTheDm.ToList();
                        foreach (var item in list)
                        {
                            result += "<option value ='" + item.pl + "'"+(item.pl==model.Phanloai?"selected":"")+">" + item.pl + "</ option >";
                        }
                        result += "</select>";
                        result += "</div>";
                        result += "<div class='col-xl-1' style='padding-left: 0px;'>";
                        result += "<label class='control-label'>Thêm</label>";
                        result += "<button type='button' class='btn btn-default' data-target='#Plspdv_Modal_edit' data-toggle='modal'><i class='la la-plus'></i>";
                        result += "</button>";
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

        [Route("GiaSpDvCuTheDmCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Tenspdv, string Mota, string Dvt, double Gia, string Hientrang, string Phanloai)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.spdvcuthe.danhmuc", "Edit"))
                {
                    var model = _db.GiaSpDvCuTheDm.FirstOrDefault(t => t.Id == Id);
                    model.Tenspdv = Tenspdv;
                    model.Mota = Mota;
                    model.Dvt = Dvt;
                    model.Gia = Gia;
                    model.Phanloai = Phanloai;
                    model.Hientrang = Hientrang;
                    model.Updated_at = DateTime.Now;
                    _db.GiaSpDvCuTheDm.Update(model);
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

        [Route("GiaSpDvCuTheDmCt/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.spdvcuthe.danhmuc", "Delete"))
                {
                    var model = _db.GiaSpDvCuTheDm.FirstOrDefault(p => p.Id == id_delete);
                    _db.GiaSpDvCuTheDm.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaSpDvCuTheDm", new { Manhom = model.Manhom });
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

        [Route("GiaSpDvCuTheDmCt/Lock")]
        [HttpPost]
        public IActionResult Lock(string Manhom, string Theodoi)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.spdvcuthe.danhmuc", "Edit"))
                {
                    var model = _db.GiaSpDvCuTheDm.Where(t => t.Manhom == Manhom).ToList();
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

        [Route("GiaSpDvCuTheDmCt/Excel")]
        [HttpPost]
        public async Task<JsonResult> Excel(string Manhom, string Mota, string Ten,double Gia, string Phanloai,
            string Hientrang, string Dvt, int Sheet, int LineStart, int LineStop, IFormFile FormFile)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.spdvcuthe", "Edit"))
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
                                var list_add = new GiaSpDvCuTheDm
                                {
                                    Manhom = Manhom,
                                    Created_at = DateTime.Now,
                                    Updated_at = DateTime.Now,
                                    Maspdv = DateTime.Now.ToString("yyMMddfffssmmHH"),

                                    Mota = worksheet.Cells[row, Int16.Parse(Mota)].Value.ToString() != null ?
                                                worksheet.Cells[row, Int16.Parse(Mota)].Value.ToString().Trim() : "",

                                    Tenspdv = worksheet.Cells[row, Int16.Parse(Ten)].Value != null ?
                                                worksheet.Cells[row, Int16.Parse(Ten)].Value.ToString().Trim() : "",
                                    Gia=Convert.ToDouble(worksheet.Cells[row, Int16.Parse(Gia.ToString())].Value) != 0 ?
                                                Convert.ToDouble(worksheet.Cells[row, Int16.Parse(Gia.ToString())].Value) : 0,
                                    Phanloai = worksheet.Cells[row, Int16.Parse(Phanloai)].Value != null ?
                                                worksheet.Cells[row, Int16.Parse(Phanloai)].Value.ToString().Trim() : "",
                                    Hientrang= worksheet.Cells[row, Int16.Parse(Hientrang)].Value != null ?
                                                worksheet.Cells[row, Int16.Parse(Hientrang)].Value.ToString().Trim() : "",
                                    Dvt = worksheet.Cells[row, Int16.Parse(Dvt)].Value != null ?
                                                worksheet.Cells[row, Int16.Parse(Dvt)].Value.ToString().Trim() : "",


                                };
                                _db.GiaSpDvCuTheDm.Add(list_add);
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
