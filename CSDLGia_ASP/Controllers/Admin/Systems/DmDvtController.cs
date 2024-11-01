using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class DmDvtController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        public DmDvtController(CSDLGiaDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var models = _db.DmDvt;
            ViewData["Title"] = "Thông tin danh mục đơn vị tính";
            
            ViewData["MenuLv1"] = "menu_qtdanhmuc";
            ViewData["MenuLv2"] = "menu_dmdonvitinh";
            return View("~/Views/Admin/Systems/DmDonViTinh/Index.cshtml", models);
        }

        [HttpPost]
        public IActionResult Store(string Dvt_create)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmdonvitinh", "Create"))
                {

                    var model = new DmDvt
                    {
                        Madvt = $"Dvt_{DateTime.Now.ToString("yyMMddHHmmssff")}",
                        Dvt = Dvt_create,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.DmDvt.Add(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "DmDvt");
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

        public IActionResult Update(int id_edit, string Dvt_edit)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmdonvitinh", "Edit"))
                {

                    var model = _db.DmDvt.FirstOrDefault(t => t.Id == id_edit);

                    model.Dvt = Dvt_edit;

                    _db.DmDvt.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "DmDvt");
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

        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmdonvitinh", "Delete"))
                {
                    var model = _db.DmDvt.FirstOrDefault(x => x.Id == id_delete);
                    _db.DmDvt.Remove(model);
                    _db.SaveChanges();
                    var dschitieu = _db.DmChiTieuKinhTeViMo.ToList();
                    ViewData["DsChiTieu"] = dschitieu;
                    return RedirectToAction("Index", "DmDvt");
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
