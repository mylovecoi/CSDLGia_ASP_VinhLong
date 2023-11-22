
﻿using Microsoft.AspNetCore.Mvc;
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
        [Route("GiaThueDNDm")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.danhmuc", "Index"))
                {
                    var model = _db.GiaThueMatDatMatNuocDm.ToList();
                    ViewData["Title"] = "Danh mục mặt đất, mặt nước";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dg_giathuematdatmatnuoc";
                    ViewData["MenuLv3"] = "menu_dg_giathuematdatmatnuoc_dm";
                    return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/Danhmuc/Index.cshtml", model);
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
        [Route("GiaThueDNDm/Store")]
        [HttpPost]
        public JsonResult Store(string Maloaidat, string Loaidat)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.GiaThueDNDm", "Create"))
                {

                    var model = new GiaThueMatDatMatNuocDm
                    {
                        Maloaidat = Maloaidat,
                        Loaidat = Loaidat,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dg_giathuematdatmatnuoc";
                    ViewData["MenuLv3"] = "menu_dg_giathuematdatmatnuoc_dm";
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

        [Route("GiaThueDNDm/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.GiaThueDNDm", "Delete"))
                {
                    var model = _db.GiaThueMatDatMatNuocDm.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaThueMatDatMatNuocDm.Remove(model);
                    _db.SaveChanges();
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dg_giathuematdatmatnuoc";
                    ViewData["MenuLv3"] = "menu_dg_giathuematdatmatnuoc_dm";
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
        [Route("GiaThueDNDm/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.GiaThueDNDm", "Edit"))
                {
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dg_giathuematdatmatnuoc";
                    ViewData["MenuLv3"] = "menu_dg_giathuematdatmatnuoc_dm";
                    var model = _db.GiaThueMatDatMatNuocDm.FirstOrDefault(p => p.Id == Id);
                    if (model != null)
                    {
                        string result = "<div class='modal-body' id='frm_edit'>";
                        result += "<div class='row'>";

                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Mã loại mặt đất mặt nước<span class='require'>*</span></label>";
                        result += "<input type='text' class='form-control' id='maloaidat_edit' name='maloaidat_edit' value='" + model.Maloaidat + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Loại mặt đất mặt nước<span class='require'>*</span></label>";
                        result += "<input type='text' class='form-control' id='loaidat_edit' name='loaidat_edit' value='" + model.Loaidat + "'/>";
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
        [Route("GiaThueDNDm/Update")]
        [HttpPost]
        public IActionResult Update(int Id, string Maloaidat, string Loaidat)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.GiaThueDNDm", "Edit"))
                {
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dg_giathuematdatmatnuoc";
                    ViewData["MenuLv3"] = "menu_dg_giathuematdatmatnuoc_dm";
                    var model = _db.GiaThueMatDatMatNuocDm.FirstOrDefault(t => t.Id == Id);

                    model.Loaidat = Loaidat;
                    model.Maloaidat = Maloaidat;
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
