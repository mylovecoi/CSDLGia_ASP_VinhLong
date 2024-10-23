using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;
using System;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDatCuThe
{
    public class GiaDatCuTheVlDmPPDGDatCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaDatCuTheVlDmPPDGDatCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaDatCuTheVlDmPPDGDatCt")]
        [HttpGet]
        public IActionResult Index(string Mapp)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthevinhlong.danhmuc", "Index"))
                {
                    var model = _db.GiaDatCuTheVlDmPPDGDatCt.Where(x => x.Mapp == Mapp);

                    if (model.Any())
                    {
                        ViewData["STT"] = model.Max(x => x.SapXep) + 1;
                    }
                    else
                    {
                        ViewData["STT"] = 1;
                    }

                    ViewData["Title"] = "Danh mục mặt đất, mặt nước";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmdmn";
                    ViewData["MenuLv3"] = "menu_dgtmdmn_dm";
                    ViewData["Tenpp"] = _db.GiaDatCuTheVlDmPPDGDat.FirstOrDefault(t => t.Mapp == Mapp)?.Tenpp ?? "";

                    ViewData["Mapp"] = Mapp;
                    return View("Views/Admin/Manages/DinhGia/GiaDatCuTheVl/Danhmuc/ChiTiet/Index.cshtml", model);
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

        [Route("GiaDatCuTheVlDmPPDGDatCt/Store")]
        [HttpPost]
        public JsonResult Store(string Loaidat, string MaNhom, string HienThi, double SapXep, string[] Style, bool Nhapgia)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthevinhlong.danhmuc", "Create"))
                {
                    string str_style = Style.Count() > 0 ? string.Join(",", Style.ToArray()) : "";
                    var model = new Models.Manages.DinhGia.GiaThueMatDatMatNuocDm
                    {
                        Manhom = MaNhom,
                        HienThi = HienThi,
                        SapXep = SapXep,
                        Loaidat = Loaidat,
                        Style = str_style,
                        NhapGia = Nhapgia,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaThueMatDatMatNuocDm.Add(model);
                    _db.SaveChanges();
                    var data = new { status = "success", message = "Thêm mới loại đất thành công!" };
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

        [Route("GiaDatCuTheVlDmPPDGDatCt/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthevinhlong.danhmuc", "Delete"))
                {
                    var model = _db.GiaThueMatDatMatNuocDm.FirstOrDefault(t => t.Id == id_delete);
                    string Manhom = model.Manhom;
                    _db.GiaThueMatDatMatNuocDm.Remove(model);
                    _db.SaveChanges();
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmdmn";
                    ViewData["MenuLv3"] = "menu_dgtmdmn_dm";
                    return RedirectToAction("Index", "GiaThueDNDm", new { Manhom = Manhom });
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

        [Route("GiaDatCuTheVlDmPPDGDatCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthevinhlong.danhmuc", "Edit"))
                {

                    var model = _db.GiaThueMatDatMatNuocDm.FirstOrDefault(p => p.Id == Id);
                    if (model != null)
                    {
                        List<string> list_style = !string.IsNullOrEmpty(model.Style) ? new List<string>(model.Style.Split(',')) : new List<string>();
                        string result = "<div class='row' id='edit_thongtin'>";

                        result += "<div class='col-xl-2'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Sắp xếp</label>";
                        result += "<input type='text' id='sapxep_edit' name='sapxep_edit' class='form-control' value='" + model.SapXep + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-2'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>STT báo cáo</label>";
                        result += "<input type='text' id='hienthi_edit' name='hienthi_edit' class='form-control' value='" + model.HienThi + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-8'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label style='font-weight:bold;color:blue'>Kiểu in hiển thị: </label>";
                        result += "<select class='form-control select2multi' multiple='multiple' id='style_edit' name='style_edit' style='width:100%'>";
                        result += "<option value='Chữ in đậm'" + (list_style.Contains("Chữ in đậm") ? "selected" : "") + ">Chữ in đậm</option >";
                        result += "<option value='Chữ in nghiêng'" + (list_style.Contains("Chữ in nghiêng") ? "selected" : "") + ">Chữ in nghiêng</option >";
                        result += "</select>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Loại mặt đất mặt nước<span class='require'>*</span></label>";
                        result += "<input type='text' class='form-control' id='loaidat_edit' name='loaidat_edit' value='" + model.Loaidat + "'/>";
                        result += "</div>";
                        result += "</div>";


                        result += "<div class='col-xl-6'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label style='font-weight:bold;color:blue'>Nhập giá </label>";
                        result += "<select class='form-control' id='nhapgia_edit' name='nhapgia_edit' style='width:100%'>";
                        result += "<option value='false'" + (!model.NhapGia ? " selected" : "") + " >Không nhập giá</option >";
                        result += "<option value='true' " + (model.NhapGia ? " selected" : "") + ">Nhập giá</option >";
                        result += "</select>";
                        result += "</div>";
                        result += "</div>";

                        result += "<input hidden class='form-control' id='id_edit' name='id_edit' value='" + model.Id + "'/>";
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

        // Cập nhật thông tin mới
        [Route("GiaDatCuTheVlDmPPDGDatCt/Update")]
        [HttpPost]
        public IActionResult Update(int Id, string Loaidat, string HienThi, double SapXep, string[] Style, bool Nhapgia)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthevinhlong.danhmuc", "Edit"))
                {
                    var model = _db.GiaThueMatDatMatNuocDm.FirstOrDefault(t => t.Id == Id);
                    string str_style = Style.Count() > 0 ? string.Join(",", Style.ToArray()) : "";
                    model.Loaidat = Loaidat;
                    model.HienThi = HienThi;
                    model.NhapGia = Nhapgia;
                    model.SapXep = SapXep;
                    model.Style = str_style;
                    model.Updated_at = DateTime.Now;
                    _db.GiaThueMatDatMatNuocDm.Update(model);
                    _db.SaveChanges();

                    var data = new { status = "success", message = "Cập nhật thành công!" };
                    return Json(data);
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
        public IActionResult RemoveRange(string manhom_remove)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthevinhlong.danhmuc", "Delete"))
                {
                    var model = _db.GiaThueMatDatMatNuocDm.Where(t => t.Manhom == manhom_remove);

                    _db.GiaThueMatDatMatNuocDm.RemoveRange(model);
                    _db.SaveChanges();
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmdmn";
                    ViewData["MenuLv3"] = "menu_dgtmdmn_dm";
                    return RedirectToAction("Index", "GiaThueDNDm", new { Manhom = manhom_remove });

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

        [HttpGet]
        public IActionResult NhanExcel(string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthevinhlong.danhmuc", "Create"))
                {

                    var model = new CSDLGia_ASP.ViewModels.VMImportExcel
                    {
                        LineStart = 3,
                        LineStop = 1000,
                        Sheet = 1,
                        MaNhom = Manhom,
                        TenNhom = _db.GiaPhiLePhiNhom.FirstOrDefault(t => t.Manhom == Manhom)?.Tennhom ?? ""
                    };

                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmdmn";
                    ViewData["MenuLv3"] = "menu_dgtmdmn_dm";
                    ViewData["Title"] = "Danh mục mặt đất, mặt nước";
                    return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/Danhmuc/ChiTiet/Excel.cshtml", model);

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
        public async Task<IActionResult> ImportExcel(CSDLGia_ASP.ViewModels.VMImportExcel requests)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                requests.LineStart = requests.LineStart == 0 ? 1 : requests.LineStart;
                int sheet = requests.Sheet == 0 ? 0 : (requests.Sheet - 1);

                using (var stream = new MemoryStream())
                {
                    await requests.FormFile.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[sheet];
                        if (worksheet != null)
                        {
                            int rowcount = worksheet.Dimension.Rows;
                            requests.LineStop = requests.LineStop > rowcount ? rowcount : requests.LineStop;
                            Regex trimmer = new Regex(@"\s\s+"); // Xóa khoảng trắng thừa trong chuỗi
                            var list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMatDatMatNuocDm>();
                            int line = 1;
                            for (int row = requests.LineStart; row <= requests.LineStop; row++)
                            {
                                ExcelStyle style = worksheet.Cells[row, 2].Style;
                                // Kiểm tra xem font chữ có được đánh dấu là đậm không
                                bool isBold = style.Font.Bold;
                                // Kiểm tra xem font chữ có được đánh dấu là nghiêng không
                                bool isItalic = style.Font.Italic;
                                StringBuilder strStyle = new StringBuilder();
                                if (isBold) { strStyle.Append("Chữ in đậm,"); }
                                if (isItalic) { strStyle.Append("Chữ in nghiêng,"); }

                                list_add.Add(new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMatDatMatNuocDm
                                {
                                    SapXep = line,
                                    HienThi = worksheet.Cells[row, 1].Value != null ?
                                                 worksheet.Cells[row, 1].Value.ToString().Trim() : "",
                                    Loaidat = worksheet.Cells[row, 2].Value != null ?
                                                 worksheet.Cells[row, 2].Value.ToString().Trim() : "",
                                    Style = strStyle.ToString(),
                                    Manhom = requests.MaNhom
                                });
                                line++;
                            }
                            _db.GiaThueMatDatMatNuocDm.AddRange(list_add);
                            _db.SaveChanges();
                        }
                    }
                }

                return RedirectToAction("Index", "GiaThueDNDm", new { Manhom = requests.MaNhom });
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }
    }
}
