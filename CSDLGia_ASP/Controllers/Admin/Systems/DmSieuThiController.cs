using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class DmSieuThiController : Controller
    {

        private readonly CSDLGiaDBContext _db;

        public DmSieuThiController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var dssieuthi = _db.DmSieuThi.ToList();
            ViewData["DsSieuThi"] = dssieuthi;
             return View("~/Views/Admin/Systems/DmSieuThi/Index.cshtml", dssieuthi);
        }

        public IActionResult Store(string masieuthi, string tensieuthi)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmloaidat", "Create"))
                {

                    var model = new DmSieuThi
                    {
                        masieuthi = masieuthi,
                        tensieuthi = tensieuthi,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.DmSieuThi.Add(model);
                    _db.SaveChanges();
                    var dssieuthi = _db.DmSieuThi.ToList();
                    ViewData["Dssieuthi"] = dssieuthi;
                    return View("~/Views/Admin/Systems/DmSieuThi/Index.cshtml", dssieuthi);
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

        public IActionResult Update(int Id_edit, string masieuthi_edit, string tensieuthi_edit)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmloaidat", "Create"))
                {

                    var model = _db.DmSieuThi.FirstOrDefault(t => t.Id == Id_edit);

                    model.masieuthi = masieuthi_edit;
                    model.tensieuthi = tensieuthi_edit;

                    _db.DmSieuThi.Update(model);
                    _db.SaveChanges();
                    var dssieuthi = _db.DmSieuThi.ToList();
                    ViewData["Dssieuthi"] = dssieuthi;
                     return View("~/Views/Admin/Systems/DmSieuThi/Index.cshtml", dssieuthi);
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

        public IActionResult Delete(string masieuthi_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmloaidat", "Create"))
                {
                    var chiteucanxoa = _db.DmSieuThi.Where(x => x.masieuthi == masieuthi_delete).FirstOrDefault();
                    _db.DmSieuThi.Remove(chiteucanxoa);
                    _db.SaveChanges();
                    var dssieuthi = _db.DmSieuThi.ToList();
                    ViewData["Dssieuthi"] = dssieuthi;
                     return View("~/Views/Admin/Systems/DmSieuThi/Index.cshtml", dssieuthi);
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
