using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class DmNgheKdController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public DmNgheKdController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DmNganhNghe/ChiTiet")]
        [HttpGet]
        public IActionResult Index(string Manganh)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmnganhnghekd", "Index"))
                {
                    var dmnghekd = _db.DmNgheKd.Where(t => t.Manganh == Manganh).ToList();
                    ViewData["Manganh"] = Manganh;
                    ViewData["Title"] = "Danh mục nghề kinh doanh";
                    ViewData["MenuLv1"] = "menu_hethong";
                    ViewData["MenuLv2"] = "menu_qtdanhmuc";
                    ViewData["MenuLv3"] = "menu_dmnganhnghekd";
                    return View("Views/Admin/Systems/DmNganhNgheKd/Show.cshtml", dmnghekd);
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

        [Route("DmNganhNghe/ChiTiet/Store")]
        [HttpPost]
        public JsonResult Store(string Manganh, string Manghe, string Tennghe, string Theodoi)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmnganhnghekd", "Create"))
                {
                    var check = _db.DmNgheKd.FirstOrDefault(t => t.Manghe == Manghe);
                    if (check == null)
                    {
                        var request = new DmNgheKd
                        {
                            Manganh = Manganh,
                            Manghe = Manghe,
                            Tennghe = Tennghe,
                            Theodoi = Theodoi,
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        };

                        _db.DmNgheKd.Add(request);
                        _db.SaveChanges();

                        var data = new { status = "success", message = "Thêm mới nghề kinh doanh thành công!" };
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

        [Route("DmNganhNghe/ChiTiet/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmnganhnghekd", "Edit"))
                {
                    var model = _db.DmNgheKd.FirstOrDefault(p => p.Id == Id);
                    if (model != null)
                    {
                        string result = "<div class='modal-body' id='frm_edit'>";
                        result += "<div class='row'>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Mã nghề<span class='require'>*</span></label>";
                        result += "<input type='text' class='form-control' id='manghe_edit' name='manghe_edit' value='" + model.Manghe + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Tên nghề<span class='require'>*</span></label>";
                        result += "<input type='text' class='form-control' id='tennghe_edit' name='tennghe_edit' value='" + model.Tennghe + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Theo dõi</label>";
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

        [Route("DmNganhNghe/ChiTiet/Update")]
        [HttpPost]
        public IActionResult Update(int Id, string Manghe, string Tennghe, string Theodoi)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmnganhnghekd", "Edit"))
                {
                    if (!string.IsNullOrEmpty(Manghe))
                    {
                        if (!string.IsNullOrEmpty(Tennghe))
                        {
                            int check = _db.DmNgheKd.Where(t => t.Manghe == Manghe && t.Id != Id).Count();
                            if (check == 0)
                            {
                                var model = _db.DmNgheKd.FirstOrDefault(t => t.Id == Id);
                                model.Manghe = Manghe;
                                model.Tennghe = Tennghe;
                                model.Theodoi = Theodoi;
                                model.Updated_at = DateTime.Now;

                                _db.DmNgheKd.Update(model);
                                _db.SaveChanges();

                                var data = new { status = "success", message = "Cập nhật thành công!" };
                                return Json(data);
                            }
                            else
                            {
                                var data = new { status = "error", message = "Mã nghề kinh doanh đã tồn tại!!!" };
                                return Json(data);
                            }
                        }
                        else
                        {
                            var data = new { status = "error", message = "Tên nghề kinh doanh không được bỏ trống" };
                            return Json(data);
                        }
                    }
                    else
                    {
                        var data = new { status = "error", message = "Mã nghề kinh doanh không được bỏ trống" };
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


        [Route("DmNganhNghe/ChiTiet/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmnganhnghekd", "Delete"))
                {
                    var model = _db.DmNgheKd.FirstOrDefault(t => t.Id == id_delete);
                    _db.DmNgheKd.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "DmNgheKd");
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
