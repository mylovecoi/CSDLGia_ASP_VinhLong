
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CSDLGia_ASP.Database;
using System.Security.Cryptography;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class GiaThueDNDmController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaThueDNDmController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        // Lấy dữ liệu từ bảng GiaThueDNDm đổ ra index
        [Route("GiaThueDNDMCT")]
        [HttpGet]
        public IActionResult Index(string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.danhmuc", "Index"))
                {
                    var model = _db.GiaThueMatDatMatNuocDm.Where(x => x.Manhom == Manhom).ToList();
                    ViewData["Title"] = "Danh mục mặt đất, mặt nước";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmdmn";
                    ViewData["MenuLv3"] = "menu_dgtmdmn_dm";
                    ViewData["Manhom"] = Manhom;
                    var chkSTT = _db.GiaThueMatDatMatNuocDm.Where(t => t.Manhom == Manhom);
                    if (chkSTT.Any())
                    {
                        ViewData["STT"] = chkSTT.Max(x => x.SapXep) + 1;
                    }
                    else
                    {
                        ViewData["STT"] = 1;
                    }
                    return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/Danhmuc/ChiTiet/Index.cshtml", model);
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

        // thêm dữ liệu vào bảng GiaThueDNDm
        [Route("GiaThueDNDMCT/Store")]
        [HttpPost]
        public JsonResult Store(string Maloaidat, string Loaidat, string maNhom, string hienThi, double sapXep)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.danhmuc", "Create"))
                {

                    var model = new Models.Manages.DinhGia.GiaThueMatDatMatNuocDm
                    {
                        Manhom = maNhom,
                        HienThi = hienThi,
                        SapXep = sapXep,
                        Maloaidat = Maloaidat,
                        Loaidat = Loaidat,
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

        // Xóa bản ghi được chọn trong bảng GiaThueDNDm

        [Route("GiaThueDNDMCT/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.danhmuc", "Delete"))
                {
                    var model = _db.GiaThueMatDatMatNuocDm.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaThueMatDatMatNuocDm.Remove(model);
                    _db.SaveChanges();
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmdmn";
                    ViewData["MenuLv3"] = "menu_dgtmdmn_dm";
                    return RedirectToAction("Index", "GiaThueDNDm");
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

        // Lấy thông tin bản ghi cần sửa, tạo 1 frm mới sau đó đẩy vào edit trong modal
        [Route("GiaThueDNDMCT/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.danhmuc", "Edit"))
                {

                    var model = _db.GiaThueMatDatMatNuocDm.FirstOrDefault(p => p.Id == Id);
                    if (model != null)
                    {
                        string result = "<div class='modal-body' id='frm_edit'>";
                        result += "<div class='row'>";

                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Loại mặt đất mặt nước<span class='require'>*</span></label>";
                        result += "<input type='text' class='form-control' id='loaidat_edit' name='loaidat_edit' value='" + model.Loaidat + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='row'>";
                        result += "<div class='col-xl-6'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>STT báo cáo</label>";
                        result += "<input type='text' id='hienthi_edit' name='hienthi_edit' class='form-control' value='" + model.HienThi + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-6'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += " <label>Sắp xếp</label>";
                        result += "<input type='text' id='sapxep_edit' name='sapxep_edit' class='form-control' value='" + model.SapXep + "'/>";
                        result += "</div>";
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
        [Route("GiaThueDNDMCT/Update")]
        [HttpPost]
        public IActionResult Update(int Id, string Maloaidat, string Loaidat, string hienThi, double sapXep)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.danhmuc", "Edit"))
                {
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmdmn";
                    ViewData["MenuLv3"] = "menu_dgtmdmn_dm";
                    var model = _db.GiaThueMatDatMatNuocDm.FirstOrDefault(t => t.Id == Id);

                    model.Loaidat = Loaidat;
                    model.HienThi = hienThi;
                    model.SapXep = sapXep;
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
    }
}
