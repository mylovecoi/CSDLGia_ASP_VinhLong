using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDvKhamChuaBenh
{
    public class DvKhamChuaBenhHtController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        public DvKhamChuaBenhHtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaDvKcbHt")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khamchuabenh.xetduyet", "Index"))
                {
                    List<string> list_trangthai = new List<string> { "CD", "HT", "DD", "CB" };
                    var model = _db.GiaDvKcb.Where(t => list_trangthai.Contains(t.Trangthai));
                    var model_join = from dg in model
                                     join donvi in _db.DsDonVi on dg.Madv equals donvi.MaDv
                                     select new VMDinhGiaDvKcb
                                     {
                                         Id = dg.Id,
                                         Madv = dg.Madv,
                                         Mahs = dg.Mahs,
                                         Thoidiem = dg.Thoidiem,
                                         Soqd = dg.Soqd,
                                         Thongtin = dg.Thongtin,
                                         Tendv = donvi.TenDv,
                                         Trangthai = dg.Trangthai
                                     };

                    ViewData["Nam"] = Nam;
                    ViewData["Title"] = "Hoàn thành định giá dịch vụ khám chữa bệnh";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgkcb";
                    ViewData["MenuLv3"] = "menu_dgkcb_ht";
                    return View("Views/Admin/Manages/DinhGia/GiaDvKhamChuabenh/HoanThanh/Index.cshtml", model_join);

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
        public IActionResult Duyet(string mahs_duyet)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khamchuabenh.xetduyet", "Approve"))
                {
                    var model = _db.GiaDvKcb.FirstOrDefault(p => p.Mahs == mahs_duyet);

                    model.Updated_at = DateTime.Now;
                    model.Trangthai = "DD";

                    _db.GiaDvKcb.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "DvKhamChuaBenhHt", new { Nam = model.Thoidiem.Year });
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
        public IActionResult HuyDuyet(string mahs_huyduyet)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khamchuabenh.xetduyet", "Approve"))
                {
                    var model = _db.GiaDvKcb.FirstOrDefault(p => p.Mahs == mahs_huyduyet);

                    model.Updated_at = DateTime.Now;
                    model.Trangthai = "CD";

                    _db.GiaDvKcb.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "DvKhamChuaBenhHt", new { Nam = model.Thoidiem.Year });
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

        public IActionResult TraLai(int id_tralai, string Lydo)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khamchuabenh.xetduyet", "Approve"))
                {
                    var model = _db.GiaDvKcb.FirstOrDefault(t => t.Id == id_tralai);
                    model.Lydo = Lydo;
                    model.Updated_at = DateTime.Now;
                    model.Trangthai = "BTL";
                  
                    _db.GiaDvKcb.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "DvKhamChuaBenhHt", new { Nam = model.Thoidiem.Year });
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

        public IActionResult CongBo(string mahs_cb)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khamchuabenh.xetduyet", "Public"))
                {
                    var model = _db.GiaDvKcb.FirstOrDefault(t => t.Mahs == mahs_cb);

                    model.Updated_at = DateTime.Now;
                    model.Trangthai = "CB";                  

                    _db.GiaDvKcb.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "DvKhamChuaBenhHt");
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

        public IActionResult HuyCongBo(string mahs_hcb)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khamchuabenh.xetduyet", "Public"))
                {
                    var model = _db.GiaDvKcb.FirstOrDefault(t => t.Mahs == mahs_hcb);

                    model.Updated_at = DateTime.Now;
                    model.Trangthai = "DD";                 

                    _db.GiaDvKcb.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "DvKhamChuaBenhHt");
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

        public IActionResult TongHop()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khamchuabenh.xetduyet", "Index"))
                {

                    return View("Views/Admin/Manages/DinhGia/GiaDvKhamChuaBenh/Tonghop.cshtml");

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
