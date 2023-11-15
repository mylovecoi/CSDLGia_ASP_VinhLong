using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class DmChiTieuKinhTeViMoController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public DmChiTieuKinhTeViMoController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            
            var dschitieu = _db.DmChiTieuKinhTeViMo.ToList();
            ViewData["DsChiTieu"] = dschitieu;
            return View("~/Views/Admin/Systems/DmChiTieuKinhTeViMo/Index.cshtml", dschitieu);
        }

        public IActionResult Store(string machitieu, string tenchitieu)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmloaidat", "Create"))
                {

                    var model = new DmChiTieuKinhTeViMo
                    {
                        machitieu = machitieu,
                        tenchitieu = tenchitieu,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.DmChiTieuKinhTeViMo.Add(model);
                    _db.SaveChanges();
                    var dschitieu = _db.DmChiTieuKinhTeViMo.ToList();
                    ViewData["DsChiTieu"] = dschitieu;
                    return View("~/Views/Admin/Systems/DmChiTieuKinhTeViMo/Index.cshtml", dschitieu);
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

        public IActionResult Update(int Id_edit, string machitieu_edit, string tenchitieu_edit)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmloaidat", "Create"))
                {

                    var model = _db.DmChiTieuKinhTeViMo.FirstOrDefault(t => t.Id == Id_edit);

                    model.machitieu = machitieu_edit;
                    model.tenchitieu = tenchitieu_edit;
                 
                    _db.DmChiTieuKinhTeViMo.Update(model);
                    _db.SaveChanges();
                    var dschitieu = _db.DmChiTieuKinhTeViMo.ToList();
                    ViewData["DsChiTieu"] = dschitieu;
                    return View("~/Views/Admin/Systems/DmChiTieuKinhTeViMo/Index.cshtml", dschitieu);
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

        public IActionResult Delete(string machitieu_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmloaidat", "Create"))
                {
                    var chiteucanxoa = _db.DmChiTieuKinhTeViMo.Where(x => x.machitieu == machitieu_delete).FirstOrDefault();
                    _db.DmChiTieuKinhTeViMo.Remove(chiteucanxoa);
                    _db.SaveChanges();
                    var dschitieu = _db.DmChiTieuKinhTeViMo.ToList();
                    ViewData["DsChiTieu"] = dschitieu;
                    return View("~/Views/Admin/Systems/DmChiTieuKinhTeViMo/Index.cshtml", dschitieu);
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
