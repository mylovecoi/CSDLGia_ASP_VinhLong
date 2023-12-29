using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.ViewModels.Manages.KeKhaiGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.KeKhaiGia.KkGiaThan
{
    public class KkGiaThanXdController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public KkGiaThanXdController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("XetDuyetGiaThan")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgthan.giakkxd", "Index"))
                {
                    var dsdonvi = _db.DsDonVi;
                    var dsdiaban = _db.DsDiaBan;

                    if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") != null)
                    {
                        Madv = Helpers.GetSsAdmin(HttpContext.Session, "Madv");

                    }
                    else
                    {
                        if (string.IsNullOrEmpty(Madv))
                        {
                            Madv = dsdonvi.OrderBy(t => t.Id).Select(t => t.MaDv).First();
                        }
                    }

                    if (string.IsNullOrEmpty(Nam))
                    {
                        Nam = Helpers.ConvertYearToStr(DateTime.Now.Year);
                    }

                    /*return Ok(Madv);*/

                    var getdonvi = (from dv in dsdonvi.Where(t => t.MaDv == Madv)
                                    join db in dsdiaban on dv.MaDiaBan equals db.MaDiaBan
                                    select new VMDsDonVi
                                    {
                                        Id = dv.Id,
                                        MaDiaBan = dv.MaDiaBan,
                                        MaDv = dv.MaDv,
                                        TenDv = dv.TenDv,
                                        ChucNang = dv.ChucNang,
                                        Level = db.Level,
                                    }).First();

                    /*return Ok(getdonvi.Level);*/

                    if (getdonvi.Level == "H")
                    {
                        var model = _db.KkGia.Where(t => t.Madv_h == Madv && t.Ngaychuyen_h.Year == int.Parse(Nam) && t.Manghe == "THAN"
                                                    && (t.Trangthai != "CC" || t.Trangthai != "BTL")).ToList();

                        var model_join = from kk in model
                                         join dn in _db.Company on kk.Madv equals dn.Madv
                                         join dnct in _db.CompanyLvCc.Where(t => t.Manghe == "THAN") on dn.Mahs equals dnct.Mahs
                                         select new VMKkGia
                                         {
                                             Id = kk.Id,
                                             Tendn = dn.Tendn,
                                             Macqcq = Madv,
                                             Madv = kk.Madv_h,
                                             Mahs = kk.Mahs,
                                             Ngaynhap = kk.Ngaynhap,
                                             Ngaychuyen = kk.Ngaychuyen_h,
                                             Ngaycvlk = kk.Ngaycvlk,
                                             Ngaynhan = kk.Ngaynhan_h,
                                             Ngayhieuluc = kk.Ngayhieuluc,
                                             Socv = kk.Socv,
                                             Ttnguoinop = kk.Ttnguoinop,
                                             Dtll = kk.Dtll,
                                             Trangthai = kk.Trangthai_h,
                                             Level = getdonvi.Level,
                                         };
                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                        {
                            ViewData["DsDonVi"] = dsdonvi;
                        }
                        else
                        {
                            ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.MaDv == Madv);
                        }
                        ViewData["DsDiaBan"] = dsdiaban;
                        ViewData["DsDonViTH"] = dsdonvi;
                        ViewData["Madv"] = Madv;
                        ViewData["Nam"] = Nam;
                        ViewData["Title"] = "Xét duyệt hồ sơ kê khai giá than";
                        ViewData["MenuLv1"] = "menu_kknygia";
                        ViewData["MenuLv2"] = "menu_kkgthan";
                        ViewData["MenuLv3"] = "menu_giakkthanxd";
                        return View("Views/Admin/Manages/KeKhaiGia/KkGiaThan/XetDuyet/Index.cshtml", model_join);
                    }
                    else if (getdonvi.Level == "T")
                    {
                        var model = _db.KkGia.Where(t => t.Madv_t == Madv && t.Ngaychuyen_t.Year == int.Parse(Nam) && t.Manghe == "THAN"
                                                        && (t.Trangthai != "CC" || t.Trangthai != "BTL")).ToList();

                        var model_join = from kk in model
                                         join dn in _db.Company on kk.Madv equals dn.Madv
                                         join dnct in _db.CompanyLvCc.Where(t => t.Manghe == "THAN") on dn.Mahs equals dnct.Mahs
                                         select new VMKkGia
                                         {
                                             Id = kk.Id,
                                             Tendn = dn.Tendn,
                                             Macqcq = Madv,
                                             Madv = kk.Madv_t,
                                             Mahs = kk.Mahs,
                                             Ngaynhap = kk.Ngaynhap,
                                             Ngaychuyen = kk.Ngaychuyen_t,
                                             Ngaycvlk = kk.Ngaycvlk,
                                             Ngaynhan = kk.Ngaynhan_t,
                                             Ngayhieuluc = kk.Ngayhieuluc,
                                             Socv = kk.Socv,
                                             Ttnguoinop = kk.Ttnguoinop,
                                             Dtll = kk.Dtll,
                                             Trangthai = kk.Trangthai_t,
                                             Level = getdonvi.Level,
                                         };
                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                        {
                            ViewData["DsDonVi"] = dsdonvi;
                        }
                        else
                        {
                            ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.MaDv == Madv);
                        }
                        ViewData["DsDiaBan"] = dsdiaban;
                        ViewData["DsDonViTH"] = dsdonvi;
                        ViewData["Madv"] = Madv;
                        ViewData["Nam"] = Nam;
                        ViewData["Title"] = "Xét duyệt hồ sơ kê khai giá than";
                        ViewData["MenuLv1"] = "menu_kknygia";
                        ViewData["MenuLv2"] = "menu_kkgthan";
                        ViewData["MenuLv3"] = "menu_giakkthanxd";
                        return View("Views/Admin/Manages/KeKhaiGia/KkGiaThan/XetDuyet/Index.cshtml", model_join);
                    }
                    else
                    {
                        var model = _db.KkGia.Where(t => t.Madv_ad == Madv && t.Ngaychuyen_ad.Year == int.Parse(Nam) && t.Manghe == "THAN"
                                                         && (t.Trangthai != "CC" || t.Trangthai != "BTL")).ToList();

                        var model_join = from kk in model
                                         join dn in _db.Company on kk.Madv equals dn.Madv
                                         join dnct in _db.CompanyLvCc.Where(t => t.Manghe == "THAN") on dn.Mahs equals dnct.Mahs
                                         select new VMKkGia
                                         {
                                             Id = kk.Id,
                                             Tendn = dn.Tendn,
                                             Macqcq = Madv,
                                             Madv = kk.Madv_ad,
                                             Mahs = kk.Mahs,
                                             Ngaynhap = kk.Ngaynhap,
                                             Ngaychuyen = kk.Ngaychuyen_ad,
                                             Ngaycvlk = kk.Ngaycvlk,
                                             Ngaynhan = kk.Ngaynhan_ad,
                                             Ngayhieuluc = kk.Ngayhieuluc,
                                             Socv = kk.Socv,
                                             Ttnguoinop = kk.Ttnguoinop,
                                             Dtll = kk.Dtll,
                                             Trangthai = kk.Trangthai_ad,
                                             Trangthai_ad = kk.Trangthai_ad,
                                             Level = getdonvi.Level,
                                         };
                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                        {
                            ViewData["DsDonVi"] = dsdonvi;
                        }
                        else
                        {
                            ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.MaDv == Madv);
                        }
                        ViewData["DsDiaBan"] = dsdiaban;
                        ViewData["DsDonViTH"] = dsdonvi;
                        ViewData["Madv"] = Madv;
                        ViewData["Nam"] = Nam;
                        ViewData["Title"] = "Xét duyệt hồ sơ kê khai giá than";
                        ViewData["MenuLv1"] = "menu_kknygia";
                        ViewData["MenuLv2"] = "menu_kkgthan";
                        ViewData["MenuLv3"] = "menu_giakkthanxd";
                        return View("Views/Admin/Manages/KeKhaiGia/KkGiaThan/XetDuyet/Index.cshtml", model_join);
                    }
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

        public IActionResult TraLai(int id_tralai, string madv_tralai, string Lydo)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgthan.giakkxd", "Approve"))
                {
                    var model = _db.KkGia.FirstOrDefault(t => t.Id == id_tralai);

                    //Gán trạng thái của đơn vị chuyển hồ sơ
                    if (madv_tralai == model.Macqcq)
                    {
                        model.Macqcq = null;
                        model.Trangthai = "BTL";
                        model.Lydo = Lydo;
                    }

                    if (madv_tralai == model.Macqcq_h)
                    {
                        model.Macqcq_h = null;
                        model.Trangthai_h = "BTL";
                        model.Lydo_h = Lydo;
                    }

                    if (madv_tralai == model.Macqcq_t)
                    {
                        model.Macqcq_t = null;
                        model.Trangthai_t = "BTL";
                        model.Lydo_t = Lydo;
                    }

                    if (madv_tralai == model.Macqcq_ad)
                    {
                        model.Macqcq_ad = null;
                        model.Trangthai_ad = "BTL";
                        model.Lydo_ad = Lydo;
                    }


                    //Gán trạng thái của đơn vị tiếp nhận hồ sơ


                    if (madv_tralai == model.Madv_h)
                    {
                        model.Macqcq_h = null;
                        model.Madv_h = null;
                        model.Ngaychuyen_h = DateTime.MinValue;
                        model.Ngaynhan_h = DateTime.MinValue;
                        model.Lydo_h = null;
                        model.Trangthai_h = null;
                    }

                    if (madv_tralai == model.Madv_t)
                    {
                        model.Macqcq_t = null;
                        model.Madv_t = null;
                        model.Ngaychuyen_t = DateTime.MinValue;
                        model.Ngaynhan_t = DateTime.MinValue;
                        model.Lydo_t = null;
                        model.Trangthai_t = null;
                    }

                    if (madv_tralai == model.Madv_ad)
                    {
                        model.Macqcq_ad = null;
                        model.Madv_ad = null;
                        model.Ngaychuyen_ad = DateTime.MinValue;
                        model.Ngaynhan_ad = DateTime.MinValue;
                        model.Lydo_ad = null;
                        model.Trangthai_ad = null;
                    }

                    _db.KkGia.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "KkGiaThanXd", new { Madv = madv_tralai, Nam = model.Ngaynhap.Year });
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

        public IActionResult XetDuyet(int id_nhanhs, string Sohsnhan, DateTime Ngaynhan)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgthan.giakkxd", "Approve"))
                {
                    var model = _db.KkGia.FirstOrDefault(t => t.Id == id_nhanhs);
                    model.Trangthai = "DD";
                    model.Ngaynhan = Ngaynhan;
                    model.Sohsnhan = Sohsnhan;

                    if (model.Macqcq == model.Madv)
                    {
                        model.Trangthai = "DD";
                        model.Lydo = null;
                        model.Ngaynhan = Ngaynhan;
                    }

                    if (model.Macqcq == model.Madv_h)
                    {
                        model.Trangthai_h = "DD";
                        model.Lydo_h = null;
                        model.Ngaynhan_h = Ngaynhan;
                    }

                    if (model.Macqcq == model.Madv_t)
                    {
                        model.Trangthai_t = "DD";
                        model.Lydo_t = null;
                        model.Ngaynhan_t = Ngaynhan;
                    }

                    if (model.Macqcq == model.Madv_ad)
                    {
                        model.Trangthai_ad = "DD";
                        model.Lydo_ad = null;
                        model.Ngaynhan_ad = Ngaynhan;
                    }
                    _db.KkGia.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "KkGiaThanXd", new { Madv = model.Macqcq, Nam = model.Ngaynhap.Year });
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

        public IActionResult ChuyenXd(string mahs, string madv, string macqcq)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgthan.giakkxd", "Approve"))
                {
                    var model = _db.KkGia.FirstOrDefault(t => t.Mahs == mahs);

                    if (madv == model.Madv)
                    {
                        model.Macqcq = macqcq;
                        model.Trangthai = "CCB";
                        model.Lydo = null;
                    }

                    if (madv == model.Madv_h)
                    {
                        model.Macqcq_h = macqcq;
                        model.Trangthai_h = "CCB";
                        model.Lydo_h = null;
                    }

                    if (madv == model.Madv_t)
                    {
                        model.Macqcq_t = macqcq;
                        model.Trangthai_t = "CCB";
                        model.Lydo_t = null;
                    }

                    if (madv == model.Madv_ad)
                    {
                        model.Macqcq_ad = macqcq;
                        model.Trangthai_ad = "CCB";
                        model.Lydo_ad = null;
                    }

                    model.Ngaynhan_ad = DateTime.Now;
                    model.Ngaychuyen_ad = DateTime.Now;
                    model.Trangthai_ad = "CCB";
                    model.Madv_ad = macqcq;

                    if (model.Macqcq_h == model.Madv_ad)
                    {
                        model.Ngaynhan_h = DateTime.Now;
                        model.Trangthai_h = "CCB";
                    }

                    if (model.Macqcq_t == model.Madv_ad)
                    {
                        model.Ngaynhan_t = DateTime.Now;
                        model.Trangthai_t = "CCB";
                    }

                    _db.KkGia.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "KkGiaThanXd", new { Madv = model.Macqcq, Nam = model.Ngaynhap.Year });
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

        public IActionResult CongBo(string mahs_cb, string trangthai_cb)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgthan.giakkxd", "Approve"))
                {
                    var model = _db.KkGia.FirstOrDefault(t => t.Mahs == mahs_cb);

                    model.Ngaynhan_ad = DateTime.Now;
                    model.Ngaychuyen_ad = DateTime.Now;
                    model.Trangthai_ad = "CB";
                    model.Congbo = "DACONGBO";
                    if (model.Macqcq_h == model.Madv_ad)
                    {
                        model.Ngaynhan_h = DateTime.Now;
                        model.Trangthai_h = "CB";
                    }
                    if (model.Macqcq_t == model.Madv_ad)
                    {
                        model.Ngaynhan_t = DateTime.Now;
                        model.Trangthai_t = "CB";
                    }

                    _db.KkGia.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "KkGiaThanXd");
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

        public IActionResult HuyCongBo(string mahs_hcb, string trangthai_hcb)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgthan.giakkxd", "Approve"))
                {
                    var model = _db.KkGia.FirstOrDefault(t => t.Mahs == mahs_hcb);

                    model.Ngaynhan_ad = DateTime.Now;
                    model.Ngaychuyen_ad = DateTime.Now;
                    model.Trangthai_ad = "HCB";
                    model.Congbo = "CHUACONGBO";
                    if (model.Macqcq_h == model.Madv_ad)
                    {
                        model.Ngaynhan_h = DateTime.Now;
                        model.Trangthai_h = "HCB";
                    }
                    if (model.Macqcq_t == model.Madv_ad)
                    {
                        model.Ngaynhan_t = DateTime.Now;
                        model.Trangthai_t = "HCB";
                    }

                    _db.KkGia.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "KkGiaThanXd");
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
