using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Services;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaSpDvCongIch
{
    public class GiaSpDvCongIchHtController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly ITrangThaiHoSoService _trangThaiHoSoService;

        public GiaSpDvCongIchHtController(CSDLGiaDBContext db, ITrangThaiHoSoService trangThaiHoSoService)
        {
            _db = db;
            _trangThaiHoSoService = trangThaiHoSoService;
        }

        [Route("HoanThanhDinhGiaSpDvCongIch")]
        [HttpGet]
        public IActionResult Index(int Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.xetduyet", "Index"))
                {
                    List<string> list_trangthai = new List<string> { "CD", "HT", "DD", "CB" };
                    var model = _db.GiaSpDvCongIch.Where(t => list_trangthai.Contains(t.Trangthai));
                    if (Nam != 0)
                    {
                        model = model.Where(t => t.Thoidiem.Year == Nam);
                    }

                    var model_join = from dg in model
                                     join donvi in _db.DsDonVi on dg.Madv equals donvi.MaDv
                                     select new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCongIch
                                     {
                                         Id = dg.Id,
                                         Madv = dg.Madv,
                                         Mahs = dg.Mahs,
                                         Thoidiem = dg.Thoidiem,
                                         Soqd = dg.Soqd,
                                         Thongtin = dg.Thongtin,
                                         TenDonVi = donvi.TenDv,
                                         Trangthai = dg.Trangthai,
                                         Ghichu = dg.Ghichu
                                     };

                    ViewData["Nam"] = Nam;
                    ViewData["Title"] = "Thông tin hồ sơ giá sản phẩm dịch vụ cụ thể";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdvci";
                    ViewData["MenuLv3"] = "menu_dgdvci_ht";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCongIch/HoanThanh/Index.cshtml", model_join);

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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.xetduyet", "Approve"))
                {
                    var model = _db.GiaSpDvCongIch.FirstOrDefault(p => p.Mahs == mahs_duyet);

                    model.Updated_at = DateTime.Now;
                    model.Trangthai = "DD";

                    _db.GiaSpDvCongIch.Update(model);
                    _db.SaveChanges();

                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Duyệt");

                    // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                    LoggingHelper.LogAction(HttpContext, _db, "XetDuyet", "Xét duyệt hồ sơ giá sản phẩm dịch vụ công ích");


                    return RedirectToAction("Index", "GiaSpDvCongIchHt", new { Nam = model.Thoidiem.Year });
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.xetduyet", "Approve"))
                {
                    var model = _db.GiaSpDvCongIch.FirstOrDefault(p => p.Mahs == mahs_huyduyet);

                    model.Updated_at = DateTime.Now;
                    model.Trangthai = "CD";

                    _db.GiaSpDvCongIch.Update(model);
                    _db.SaveChanges();

                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Hủy duyệt");

                    // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                    LoggingHelper.LogAction(HttpContext, _db, "HuyDuyet", "Hủy xét duyệt hồ sơ giá sản phẩm dịch vụ công ích");


                    return RedirectToAction("Index", "GiaSpDvCongIchHt", new { Nam = model.Thoidiem.Year });
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.xetduyet", "Approve"))
                {
                    var model = _db.GiaSpDvCongIch.FirstOrDefault(t => t.Mahs == mahs_cb);

                    model.Trangthai = "CB";
                    model.Updated_at = DateTime.Now;

                    _db.GiaSpDvCongIch.Update(model);
                    _db.SaveChanges();

                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Công bố");

                    // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                    LoggingHelper.LogAction(HttpContext, _db, "CongBo", "Công bố hồ sơ giá sản phẩm dịch vụ công ích");


                    return RedirectToAction("Index", "GiaSpDvCongIchHt");
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.xetduyet", "Approve"))
                {
                    var model = _db.GiaSpDvCongIch.FirstOrDefault(t => t.Mahs == mahs_hcb);

                    model.Trangthai = "DD";
                    model.Updated_at = DateTime.Now;

                    _db.GiaSpDvCongIch.Update(model);
                    _db.SaveChanges();

                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Hủy công bố");

                    // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                    LoggingHelper.LogAction(HttpContext, _db, "HuyCongBo", "Hủy công bố hồ sơ giá sản phẩm dịch vụ công ích");


                    return RedirectToAction("Index", "GiaSpDvCongIchHt");
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.xetduyet", "Approve"))
                {
                    var model = _db.GiaSpDvCongIch.FirstOrDefault(t => t.Id == id_tralai);
                    model.Trangthai = "BTL";
                    model.Lydo = Lydo;
                    model.MadvXuly = model.Madv;
                    model.Updated_at = DateTime.Now;

                    _db.GiaSpDvCongIch.Update(model);
                    _db.SaveChanges();

                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Trả lại");

                    // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                    LoggingHelper.LogAction(HttpContext, _db, "TraLai", "Trả lại hồ sơ giá sản phẩm dịch vụ công ích");


                    return RedirectToAction("Index", "GiaSpDvCongIchHt", new { Nam = model.Thoidiem.Year });
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
    

        [HttpPost("HoanThanhDinhGiaSpDvCongIch/TongHop")]
        public IActionResult TongHop(DateTime ngaytu, DateTime ngayden)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.xetduyet", "Index"))
                {
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    var model = _db.GiaSpDvCongIch.Where(t => list_trangthai.Contains(t.Trangthai) && t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden);
                    List<string> list_mahs = model.Select(t => t.Mahs).ToList();
                    List<string> list_madv = model.Select(t => t.Madv).ToList();
                    var model_ct = _db.GiaSpDvCongIchCt.Where(t => list_mahs.Contains(t.Mahs));
                    var model_dv = _db.DsDonVi.Where(t => list_madv.Contains(t.MaDv));

                    ViewData["DanhMucNhom"] = _db.GiaSpDvCongIchNhom;
                    ViewData["HoSoCt"] = model_ct;
                    ViewData["DonVis"] = model_dv;
                    ViewData["Titile"] = "Tổng hợp giá sản phẩm dịch vụ công ích";

                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCongIch/HoanThanh/Tonghop.cshtml", model);

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
