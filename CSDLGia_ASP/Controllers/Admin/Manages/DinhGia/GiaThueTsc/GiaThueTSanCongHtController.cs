using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaThueTsc
{
    public class GiaThueTSanCongHtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaThueTSanCongHtController(CSDLGiaDBContext db)
        {
            _db = db;
        }
        [Route("GiaThueTsc/XetDuyet")]
        [HttpGet]
        public IActionResult Index(int Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetaisancong.xetduyet", "Index"))
                {
                    List<string> list_trangthai = new List<string> { "CD", "HT", "DD", "CB" };
                    var model = _db.GiaThueTaiSanCong.Where(t => list_trangthai.Contains(t.Trangthai));
                    if (Nam != 0)
                    {
                        model = model.Where(t => t.Thoidiem.Year == Nam);
                    }

                    var model_join = from dg in model
                                     join donvi in _db.DsDonVi on dg.Madv equals donvi.MaDv
                                     select new VMDinhGiaThueTsc
                                     {
                                         Id = dg.Id,
                                         Madv = dg.Madv,
                                         Mahs = dg.Mahs,
                                         Thoidiem = dg.Thoidiem,
                                         Soqd = dg.Soqd,
                                         Thongtin = dg.Thongtin,
                                         TenDonvi = donvi.TenDv,
                                         Trangthai = dg.Trangthai
                                     };

                    ViewData["Nam"] = Nam;
                    ViewData["Title"] = "Hoàn thành định giá thuê tài sản công";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtsc";
                    ViewData["MenuLv3"] = "menu_dgtsc_ht";
                    return View("Views/Admin/Manages/DinhGia/GiaThueTsc/HoanThanh/Index.cshtml", model_join);

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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetaisancong.xetduyet", "Approve"))
                {
                    var model = _db.GiaThueTaiSanCong.FirstOrDefault(p => p.Mahs == mahs_duyet);

                    model.Updated_at = DateTime.Now;
                    model.Trangthai = "DD";

                    _db.GiaThueTaiSanCong.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "GiaThueTSanCongHt", new { Nam = model.Thoidiem.Year });
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetaisancong.xetduyet", "Approve"))
                {
                    var model = _db.GiaThueTaiSanCong.FirstOrDefault(p => p.Mahs == mahs_huyduyet);

                    model.Updated_at = DateTime.Now;
                    model.Trangthai = "CD";

                    _db.GiaThueTaiSanCong.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "GiaThueTSanCongHt", new { Nam = model.Thoidiem.Year });
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
        public IActionResult TraLai(int id_tralai, string Lydo)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetaisancong.xetduyet", "Approve"))
                {
                    var model = _db.GiaThueTaiSanCong.FirstOrDefault(t => t.Id == id_tralai);
                    model.Trangthai = "BTL";
                    model.Lydo = Lydo;
                    model.Updated_at = DateTime.Now;

                    _db.GiaThueTaiSanCong.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaThueTSanCongHt", new { Nam = model.Thoidiem.Year });
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
        public IActionResult CongBo(string mahs_cb)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetaisancong.xetduyet", "Public"))
                {
                    var model = _db.GiaThueTaiSanCong.FirstOrDefault(t => t.Mahs == mahs_cb);

                    model.Updated_at = DateTime.Now;
                    model.Trangthai = "CB";               
                    _db.GiaThueTaiSanCong.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "GiaThueTSanCongHt");
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
        public IActionResult HuyCongBo(string mahs_hcb)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetaisancong.xetduyet", "Public"))
                {
                    var model = _db.GiaThueTaiSanCong.FirstOrDefault(t => t.Mahs == mahs_hcb);

                    model.Updated_at = DateTime.Now;
                    model.Trangthai = "DD";                   
                    _db.GiaThueTaiSanCong.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaThueTSanCongHt");
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
