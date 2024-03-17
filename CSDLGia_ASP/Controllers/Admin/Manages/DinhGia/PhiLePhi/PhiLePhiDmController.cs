using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.PhiLePhi
{
    public class PhiLePhiDmController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _env;

        public PhiLePhiDmController(CSDLGiaDBContext db, IWebHostEnvironment hostingEnv)
        {
            _db = db;
            _env = hostingEnv;
        }

        [Route("PhiLePhiDmCt")]
        [HttpGet]
        public IActionResult Index(string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.lephi.danhmuc", "Index"))
                {

                    var model = _db.PhiLePhiDm.Where(t => t.Manhom == Manhom);

                    ViewData["Manhom"] = Manhom;
                    ViewData["Tennhom"] = _db.PhiLePhiNhom.FirstOrDefault(t => t.Manhom == Manhom).Tennhom;
                    ViewData["Title"] = "Thông tin chi tiết nhóm phí, lệ phí";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_plp";
                    ViewData["MenuLv3"] = "menu_plp_dm";
                    ViewData["Donvitinh"] = _db.DmDvt.ToList();
                    ViewData["PhanLoaiDichVu"] = _db.PhiLePhiDm.ToList();
                    var chkSTT = _db.PhiLePhiDm.Where(t => t.Manhom == Manhom);
                    if (chkSTT.Any())
                    {
                        ViewData["STT"] = chkSTT.Max(x => x.SapXep) + 1;
                    }
                    else
                    {
                        ViewData["STT"] = 1;
                    }

                    return View("Views/Admin/Manages/DinhGia/PhiLePhi/DanhMuc/ChiTiet/Index.cshtml", model.ToList());
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

        [Route("PhiLePhiDmCt/Store")]
        [HttpPost]
        public JsonResult Store(string Manhom, string Tenspdv, string Dvt, string Hientrang, string HienThi, double SapXep)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.lephi.danhmuc", "Create"))
                {
                    var request = new PhiLePhiDm
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
                    _db.PhiLePhiDm.Add(request);
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

        [Route("PhiLePhiDmCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.lephi.danhmuc", "Edit"))
                {
                    var model = _db.PhiLePhiDm.FirstOrDefault(p => p.Id == Id);
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
                            if (model.Dvt == item.Dvt)
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

        [Route("PhiLePhiDmCt/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string Tenspdv, string Dvt, string Hientrang, string HienThi, double SapXep)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.lephi.danhmuc", "Edit"))
                {
                    var model = _db.PhiLePhiDm.FirstOrDefault(t => t.Id == Id);
                    model.Tenspdv = Tenspdv;
                    model.Dvt = Dvt;
                    model.HienThi = HienThi;
                    model.SapXep = SapXep;
                    model.Updated_at = DateTime.Now;
                    _db.PhiLePhiDm.Update(model);
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

        [Route("PhiLePhiDmCt/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.lephi.danhmuc", "Delete"))
                {
                    var model = _db.PhiLePhiDm.FirstOrDefault(p => p.Id == id_delete);
                    _db.PhiLePhiDm.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "PhiLePhiDm", new { Manhom = model.Manhom });
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

        [Route("PhiLePhiDmCt/Lock")]
        [HttpPost]
        public IActionResult Lock(string Manhom, string Theodoi)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.lephi.danhmuc", "Edit"))
                {
                    var model = _db.PhiLePhiDm.Where(t => t.Manhom == Manhom).ToList();
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
    }
}
