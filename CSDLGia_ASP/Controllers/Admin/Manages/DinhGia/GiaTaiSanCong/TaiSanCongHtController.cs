using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Services;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaTaiSanCong
{
    public class TaiSanCongHtController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly ITrangThaiHoSoService _trangThaiHoSoService;
        public TaiSanCongHtController(CSDLGiaDBContext db, ITrangThaiHoSoService trangThaiHoSoService)
        {
            _db = db;
            _trangThaiHoSoService = trangThaiHoSoService;
        }
        [Route("GiaTaiSanCongHt")]
        [HttpGet]
        public IActionResult Index(int Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.taisancong.xetduyet", "Index"))
                {
                    List<string> list_trangthai = new List<string> { "CD", "HT", "DD", "CB" };
                    var model = _db.GiaTaiSanCong.Where(t => list_trangthai.Contains(t.Trangthai));
                    if (Nam != 0)
                    {
                        model = model.Where(t => t.Thoidiem.Year == Nam);
                    }

                    var model_join = from dg in model
                                     join donvi in _db.DsDonVi on dg.Madv equals donvi.MaDv
                                     select new CSDLGia_ASP.Models.Manages.DinhGia.GiaTaiSanCong
                                     {
                                         Id = dg.Id,
                                         Madv = dg.Madv,
                                         Mahs = dg.Mahs,
                                         Thoidiem = dg.Thoidiem,
                                         Soqd = dg.Soqd,
                                         Thongtin = dg.Thongtin,
                                         TenDonVi = donvi.TenDv,
                                         Trangthai = dg.Trangthai
                                     };

                    ViewData["Nam"] = Nam;
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Title"] = "Hoàn thành định giá tài sản công";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_tsc";
                    ViewData["MenuLv3"] = "menu_giatsc_ht";
                    return View("Views/Admin/Manages/DinhGia/GiaTaiSanCong/HoanThanh/Index.cshtml", model_join);
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.taisancong.xetduyet", "Approve"))
                {
                    var model = _db.GiaTaiSanCong.FirstOrDefault(p => p.Mahs == mahs_duyet);

                    model.Updated_at = DateTime.Now;
                    model.Trangthai = "DD";

                    _db.GiaTaiSanCong.Update(model);
                    _db.SaveChanges();
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Duyệt");

                    // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                    LoggingHelper.LogAction(HttpContext, _db, "XetDuyet", "Xét duyệt hồ sơ giá tài sản công");


                    return RedirectToAction("Index", "TaiSanCongHt", new { Nam = model.Thoidiem.Year });
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.taisancong.xetduyet", "Approve"))
                {
                    var model = _db.GiaTaiSanCong.FirstOrDefault(p => p.Mahs == mahs_huyduyet);

                    model.Updated_at = DateTime.Now;
                    model.Trangthai = "CD";

                    _db.GiaTaiSanCong.Update(model);
                    _db.SaveChanges();
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Hủy duyệt");

                    // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                    LoggingHelper.LogAction(HttpContext, _db, "HuyDuyet", "Hủy xét duyệt hồ sơ giá tài sản công");


                    return RedirectToAction("Index", "TaiSanCongHt", new { Nam = model.Thoidiem.Year });
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.taisancong.xetduyet", "Approve"))
                {
                    var model = _db.GiaTaiSanCong.FirstOrDefault(t => t.Id == id_tralai);
                    model.Trangthai = "BTL";
                    model.Lydo = Lydo;
                    model.Updated_at = DateTime.Now;

                    _db.GiaTaiSanCong.Update(model);
                    _db.SaveChanges();
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Trả lại");
                    // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                    LoggingHelper.LogAction(HttpContext, _db, "TraLai", "Trả lại hồ sơ giá tài sản công");

                    return RedirectToAction("Index", "TaiSanCongHt", new { Nam = model.Thoidiem.Year });
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.taisancong.xetduyet", "Public"))
                {
                    var model = _db.GiaTaiSanCong.FirstOrDefault(t => t.Mahs == mahs_cb);

                    model.Trangthai = "CB";
                    model.Updated_at = DateTime.Now;

                    _db.GiaTaiSanCong.Update(model);
                    _db.SaveChanges();
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Công bố");
                    // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                    LoggingHelper.LogAction(HttpContext, _db, "CongBo", "Công bố hồ sơ giá tài sản công");


                    return RedirectToAction("Index", "TaiSanCongHt");
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.taisancong.xetduyet", "Public"))
                {
                    var model = _db.GiaTaiSanCong.FirstOrDefault(t => t.Mahs == mahs_hcb);

                    model.Updated_at = DateTime.Now;
                    model.Trangthai = "DD";

                    _db.GiaTaiSanCong.Update(model);
                    _db.SaveChanges();
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Hủy công bố");
                    // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                    LoggingHelper.LogAction(HttpContext, _db, "HuyCongBo", "Hủy công bố hồ sơ giá tài sản công");

                    return RedirectToAction("Index", "TaiSanCongHt");
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.taisancong.xetduyet", "Index"))
                {

                    return View("Views/Admin/Manages/DinhGia/GiaTaiSanCong/HoanThanh/Tonghop.cshtml");

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
