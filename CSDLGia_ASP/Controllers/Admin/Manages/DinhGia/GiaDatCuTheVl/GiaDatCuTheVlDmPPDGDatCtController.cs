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

                    ViewData["Tenpp"] = _db.GiaDatCuTheVlDmPPDGDat.FirstOrDefault(t => t.Mapp == Mapp)?.Tenpp ?? "";
                    ViewData["Mapp"] = Mapp;
                    ViewData["Title"] = "Danh mục phương pháp định giá đất cụ thể chi tiết";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgdctvl";
                    ViewData["MenuLv3"] = "menu_dgdctvl_dm";

                    return View("Views/Admin/Manages/DinhGia/GiaDatCuTheVl/DanhMuc/PhuongPhapCt/Index.cshtml", model);
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
        public JsonResult Store(string Noidungcv, string Mapp, string HienThi, double SapXep, string[] Style, bool Nhapgia)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthevinhlong.danhmuc", "Create"))
                {
                    string str_style = Style.Count() > 0 ? string.Join(",", Style.ToArray()) : "";
                    var model = new Models.Manages.DinhGia.GiaDatCuTheVlDmPPDGDatCt
                    {
                        Mapp = Mapp,
                        HienThi = HienThi,
                        SapXep = SapXep,
                        Noidungcv = Noidungcv,
                        Style = str_style,
                        NhapGia = Nhapgia,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaDatCuTheVlDmPPDGDatCt.Add(model);
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

        [Route("GiaDatCuTheVlDmPPDGDatCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthevinhlong.danhmuc", "Edit"))
                {

                    var model = _db.GiaDatCuTheVlDmPPDGDatCt.FirstOrDefault(p => p.Id == Id);
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
                        result += "<label>Nội dung công việc</label>";
                        result += "<input type='text' class='form-control' id='noidungcv_edit' name='noidungcv_edit' value='" + model.Noidungcv + "'/>";
                        result += "</div>";
                        result += "</div>";


                        result += "<div class='col-xl-6'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label style='font-weight:bold;color:blue'>Nhập giá</label>";
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

        [Route("GiaDatCuTheVlDmPPDGDatCt/Update")]
        [HttpPost]
        public IActionResult Update(int Id, string Noidungcv, string HienThi, double SapXep, string[] Style, bool Nhapgia)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthevinhlong.danhmuc", "Edit"))
                {
                    var model = _db.GiaDatCuTheVlDmPPDGDatCt.FirstOrDefault(t => t.Id == Id);
                    string str_style = Style.Count() > 0 ? string.Join(",", Style.ToArray()) : "";
                    model.Noidungcv = Noidungcv;
                    model.HienThi = HienThi;
                    model.NhapGia = Nhapgia;
                    model.SapXep = SapXep;
                    model.Style = str_style;
                    model.Updated_at = DateTime.Now;
                    _db.GiaDatCuTheVlDmPPDGDatCt.Update(model);
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

        [Route("GiaDatCuTheVlDmPPDGDatCt/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthevinhlong.danhmuc", "Delete"))
                {
                    var model = _db.GiaDatCuTheVlDmPPDGDatCt.FirstOrDefault(t => t.Id == id_delete);
                    string Mapp = model.Mapp;
                    _db.GiaDatCuTheVlDmPPDGDatCt.Remove(model);
                    _db.SaveChanges();

                    ViewData["Title"] = "Danh mục phương pháp định giá đất cụ thể chi tiết";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgdctvl";
                    ViewData["MenuLv3"] = "menu_dgdctvl_dm";
                    return RedirectToAction("Index", "GiaDatCuTheVlDmPPDGDatCt", new { Mapp = Mapp });
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
        public IActionResult RemoveRange(string mapp_remove)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthevinhlong.danhmuc", "Delete"))
                {
                    var model = _db.GiaDatCuTheVlDmPPDGDatCt.Where(t => t.Mapp == mapp_remove);

                    _db.GiaDatCuTheVlDmPPDGDatCt.RemoveRange(model);
                    _db.SaveChanges();

                    ViewData["Title"] = "Danh mục phương pháp định giá đất cụ thể chi tiết";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgdctvl";
                    ViewData["MenuLv3"] = "menu_dgdctvl_dm";
                    return RedirectToAction("Index", "GiaDatCuTheVlDmPPDGDatCt", new { Mapp = mapp_remove });

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
    }
}
