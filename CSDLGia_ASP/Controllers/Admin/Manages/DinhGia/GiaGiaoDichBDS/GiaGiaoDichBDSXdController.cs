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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaGiaoDichBDS
{
    public class GiaGiaoDichBDSXdController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly ITrangThaiHoSoService _trangThaiHoSoService;

        public GiaGiaoDichBDSXdController(CSDLGiaDBContext db, ITrangThaiHoSoService trangThaiHoSoService)
        {
            _db = db;
            _trangThaiHoSoService = trangThaiHoSoService;
        }

        [Route("GiaGiaoDichBDS/XetDuyet")]
        [HttpGet]
        public IActionResult Index(int Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaodichbds.xetduyet", "Index"))
                {
                    List<string> list_trangthai = new List<string> { "CD", "HT", "DD", "CB" };
                    var model = _db.GiaGiaoDichBDS.Where(t => list_trangthai.Contains(t.Trangthai));
                    if (Nam != 0)
                    {
                        model = model.Where(t => t.Thoidiem.Year == Nam);
                    }
                    var model_join = from dg in model
                                     join donvi in _db.DsDonVi on dg.Madv equals donvi.MaDv
                                     select new CSDLGia_ASP.Models.Manages.DinhGia.GiaGiaoDichBDS
                                     {
                                         Id = dg.Id,
                                         Madv = dg.Madv,
                                         Mahs = dg.Mahs,
                                         Thoidiem = dg.Thoidiem,
                                         Soqd = dg.Soqd,
                                         Thongtin = dg.Thongtin,
                                         TenDonVi = donvi.TenDv,
                                         Trangthai = dg.Trangthai,
                                         Cqbh=dg.Cqbh,
                                     };

                    ViewData["Nam"] = Nam;
                    ViewData["Title"] = "Thông tin hồ sơ giá giao dịch bất động sản";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dg_giaodichbds";
                    ViewData["MenuLv3"] = "menu_dg_giaodichbds_xd";
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichBDS/XetDuyet/Index.cshtml", model_join);

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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaodichbds.xetduyet", "Approve"))
                {
                    var model = _db.GiaGiaoDichBDS.FirstOrDefault(p => p.Mahs == mahs_duyet);

                    model.Updated_at = DateTime.Now;
                    model.Trangthai = "DD";

                    _db.GiaGiaoDichBDS.Update(model);
                    _db.SaveChanges();

                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Duyệt");

                    return RedirectToAction("Index", "GiaGiaoDichBDSXd", new { Nam = model.Thoidiem.Year });
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaodichbds.xetduyet", "Approve"))
                {
                    var model = _db.GiaGiaoDichBDS.FirstOrDefault(p => p.Mahs == mahs_huyduyet);

                    model.Updated_at = DateTime.Now;
                    model.Trangthai = "CD";

                    _db.GiaGiaoDichBDS.Update(model);
                    _db.SaveChanges();

                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Hủy duyệt");

                    return RedirectToAction("Index", "GiaGiaoDichBDSXd", new { Nam = model.Thoidiem.Year });
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaodichbds.xetduyet", "Approve"))
                {
                    var model = _db.GiaGiaoDichBDS.FirstOrDefault(t => t.Id == id_tralai);
                    model.Lydo = Lydo;
                    model.Updated_at = DateTime.Now;
                    model.Trangthai = "BTL";                 

                    _db.GiaGiaoDichBDS.Update(model);
                    _db.SaveChanges();

                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Trả lại");

                    return RedirectToAction("Index", "GiaGiaoDichBDSXd", new {Nam = model.Thoidiem.Year });
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaodichbds.xetduyet", "Approve"))
                {
                    var model = _db.GiaGiaoDichBDS.FirstOrDefault(t => t.Mahs == mahs_cb);

                    model.Trangthai = "CB";
                    model.Updated_at = DateTime.Now;

                    _db.GiaGiaoDichBDS.Update(model);
                    _db.SaveChanges();

                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Công bố");

                    return RedirectToAction("Index", "GiaGiaoDichBDSXd");
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaodichbds.xetduyet", "Approve"))
                {
                    var model = _db.GiaGiaoDichBDS.FirstOrDefault(t => t.Mahs == mahs_hcb);

                    model.Trangthai = "DD";
                    model.Updated_at = DateTime.Now;

                    _db.GiaGiaoDichBDS.Update(model);
                    _db.SaveChanges();


                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Hủy công bố");

                    return RedirectToAction("Index", "GiaGiaoDichBDSXd");
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

        private static string GetMadvChuyen(string macqcq, CSDLGia_ASP.Models.Manages.DinhGia.GiaGiaoDichBDS hoso)
        {
            string madv = "";
            if (macqcq == hoso.Macqcq)
            {
                madv = hoso.Madv;
                goto ketthuc;
            }
            if (macqcq == hoso.Macqcq_h)
            {
                madv = hoso.Madv_h;
                goto ketthuc;
            }
            if (macqcq == hoso.Macqcq_t)
            {
                madv = hoso.Madv_t;
                goto ketthuc;
            }
            if (macqcq == hoso.Macqcq_ad)
            {
                madv = hoso.Madv_ad;
                goto ketthuc;
            }
        ketthuc:
            return madv;
        }

        public IActionResult TongHop(DateTime tungay, DateTime denngay)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaodichbds.xetduyet", "Index"))
                {
                    ViewData["Title"] = "Tổng hợp bảng giá giao dịch bất động sản";
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichBDS/DanhSach/TongHop.cshtml");
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
