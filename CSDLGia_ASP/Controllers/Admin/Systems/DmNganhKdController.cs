using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class DmNganhKdController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public DmNganhKdController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DmNganhNghe")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmnganhnghekd", "Index"))
                {
                    var dmnganhkd = _db.DmNganhKd.ToList();

                    ViewData["Title"] = "Danh mục ngành kinh doanh";
                    
                    ViewData["MenuLv1"] = "menu_qtdanhmuc";
                    ViewData["MenuLv2"] = "menu_dmnganhnghekd";
                    return View("Views/Admin/Systems/DmNganhNgheKd/Index.cshtml", dmnganhkd);
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

        [Route("DmNganhNghe/Store")]
        [HttpPost]
        public JsonResult Store(string Manganh, string Tennganh, string Theodoi)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmnganhnghekd", "Create"))
                {
                    var check = _db.DmNganhKd.FirstOrDefault(t => t.Manganh == Manganh);
                    if (check == null)
                    {
                        var request = new DmNganhKd
                        {
                            Manganh = Manganh,
                            Tennganh = Tennganh,
                            Theodoi = Theodoi,
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        };

                        _db.DmNganhKd.Add(request);
                        _db.SaveChanges();

                        var data = new { status = "success", message = "Thêm mới ngành kinh doanh thành công!" };
                        return Json(data);
                    }
                    else
                    {
                        var data = new { status = "error", message = "Mã ngành này đã tồn tại!" };
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

        [Route("DmNganhNghe/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmnganhnghekd", "Edit"))
                {
                    var model = _db.DmNganhKd.FirstOrDefault(p => p.Id == Id);
                    if (model != null)
                    {
                        string result = "<div class='modal-body' id='frm_edit'>";
                        result += "<div class='row'>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Mã ngành kinh doanh<span class='require'>*</span></label>";
                        result += "<input type='text' class='form-control' id='manganh_edit' name='manganh_edit' value='" + model.Manganh + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Tên ngành kinh doanh<span class='require'>*</span></label>";
                        result += "<input type='text' class='form-control' id='tennganh_edit' name='tennganh_edit' value='" + model.Tennganh + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Theo dõi<span class='require'>*</span></label>";
                        result += "<select class='form-control' id='theodoi_edit' name='theodoi_edit'>";
                        if (model.Theodoi == "TD")
                        {
                            result += "<option value='TD' selected>Theo dõi</option>";
                            result += "<option value='DTD'>Dừng theo dõi</option>";
                        }
                        else
                        {
                            result += "<option value='TD'>Theo dõi</option>";
                            result += "<option value='DTD' selected>Dừng theo dõi</option>";
                        }
                        result += "</select>";
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

        [Route("DmNganhNghe/Update")]
        [HttpPost]
        public IActionResult Update(int Id, string Manganh, string Tennganh, string Theodoi)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmnganhnghekd", "Edit"))
                {
                    if (!string.IsNullOrEmpty(Manganh))
                    {
                        if (!string.IsNullOrEmpty(Tennganh))
                        {
                            int check = _db.DmNganhKd.Where(t => t.Manganh == Manganh && t.Id != Id).Count();
                            if (check == 0)
                            {
                                var model = _db.DmNganhKd.FirstOrDefault(t => t.Id == Id);
                                model.Manganh = Manganh;
                                model.Tennganh = Tennganh;
                                model.Theodoi = Theodoi;
                                model.Updated_at = DateTime.Now;

                                _db.DmNganhKd.Update(model);
                                _db.SaveChanges();

                                var data = new { status = "success", message = "Cập nhật thành công!" };
                                return Json(data);
                            }
                            else
                            {
                                var data = new { status = "error", message = "Mã ngành kinh doanh đã tồn tại!!!" };
                                return Json(data);
                            }
                        }
                        else
                        {
                            var data = new { status = "error", message = "Tên ngành kinh doanh không được bỏ trống" };
                            return Json(data);
                        }
                    }
                    else
                    {
                        var data = new { status = "error", message = "Mã ngành kinh doanh không được bỏ trống" };
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

        [Route("DmNganhNghe/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmnganhnghekd", "Delete"))
                {
                    var model = _db.DmNganhKd.FirstOrDefault(t => t.Id == id_delete);
                    _db.DmNganhKd.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "DmNganhKd");
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
