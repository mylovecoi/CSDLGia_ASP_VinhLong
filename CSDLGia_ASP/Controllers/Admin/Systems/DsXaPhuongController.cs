using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class DsXaPhuongController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public DsXaPhuongController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DsXaPhuong")]
        [HttpGet]
        public IActionResult Index(string MaDiaBan)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.dsdiaban", "Index"))
                {
                    var dsdiaban = _db.DsDiaBan.Where(t => t.Level == "H").ToList();
                    if (string.IsNullOrEmpty(MaDiaBan))
                    {
                        MaDiaBan = dsdiaban.OrderBy(t => t.Id).Select(t => t.MaDiaBan).First();
                    }
                    var dsxaphuong = _db.DsXaPhuong.Where(t => t.Madiaban == MaDiaBan).ToList();

                    ViewData["DsDiaBan"] = dsdiaban;
                    ViewData["MaDiaBan"] = MaDiaBan;
                    ViewData["Title"] = "Danh sách xã phường";
                    ViewData["MenuLv1"] = "menu_hethong";
                    ViewData["MenuLv2"] = "menu_qthethong";
                    ViewData["MenuLv3"] = "menu_dsxaphuong";
                    return View("Views/Admin/Systems/DsXaPhuong/Index.cshtml", dsxaphuong);
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

        [Route("DsXaPhuong/Store")]
        [HttpPost]
        public JsonResult Store(string TenXp, string MaDiaBan)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.dsdiaban", "Create"))
                {
                    if (!string.IsNullOrEmpty(TenXp))
                    {
                        var request = new DsXaPhuong
                        {
                            Madiaban = MaDiaBan,
                            Maxp = DateTime.Now.ToString("yyMMddssmmHH"),
                            Tenxp = TenXp,
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        };
                        _db.DsXaPhuong.Add(request);
                        _db.SaveChanges();

                        var data = new { status = "success", message = "Cập nhật thành công!" };
                        return Json(data);
                    }
                    else
                    {
                        var data = new { status = "error", message = "Tên xã, phường không được bỏ trống" };
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

        [Route("DsXaPhuong/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.dsdiaban", "Create"))
                {
                    var model = _db.DsXaPhuong.FirstOrDefault(p => p.Id == Id);
                    if (model != null)
                    {
                        string result = "<div class='row' id='edit_thongtin'>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Tên xã, phường<span class='required'>*</span>: </label>";
                        result += "<input type='text' id='tenxp_edit' name='tenxp_edit' class='form-control' value='" + model.Tenxp + "'/>";
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

        [Route("DsXaPhuong/Update")]
        [HttpPost]
        public JsonResult Update(int Id, string TenXp)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.dsdiaban", "Create"))
                {
                    if (!string.IsNullOrEmpty(TenXp))
                    {
                        var model = _db.DsXaPhuong.FirstOrDefault(t => t.Id == Id);
                        model.Tenxp = TenXp;
                        model.Updated_at = DateTime.Now;
                        _db.DsXaPhuong.Update(model);
                        _db.SaveChanges();

                        var data = new { status = "success", message = "Cập nhật thành công!" };
                        return Json(data);
                    }
                    else
                    {
                        var data = new { status = "error", message = "Tên xã, phường không được bỏ trống" };
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

        [Route("DsXaPhuong/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.dsdiaban", "Create"))
                {
                    var model = _db.DsXaPhuong.FirstOrDefault(p => p.Id == id_delete);
                    _db.DsXaPhuong.Remove(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "DsXaPhuong", new { MaDiaBan = model.Madiaban });
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
