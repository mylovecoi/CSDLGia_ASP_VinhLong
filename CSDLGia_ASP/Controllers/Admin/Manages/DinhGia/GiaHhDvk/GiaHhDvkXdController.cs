using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaHhDvk
{
    public class GiaHhDvkXdController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaHhDvkXdController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaHhDvk/XetDuyet")]
        [HttpGet]
        public IActionResult Index(string Nam, string Thang, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.xd", "Index"))
                {
                    var dsdonvi = (from donvi in _db.DsDonVi
                                   join tk in _db.Users on donvi.MaDv equals tk.Madv
                                   join gr in _db.GroupPermissions.Where(x => x.ChucNang == "TONGHOP") on tk.Chucnang equals gr.KeyLink
                                   select new CSDLGia_ASP.Models.Systems.DsDonVi
                                   {
                                       MaDiaBan = donvi.MaDiaBan,
                                       MaDv = donvi.MaDv,
                                       TenDv = donvi.TenDv,
                                   });

                    var dsdiaban = _db.DsDiaBan;
                    Madv = string.IsNullOrEmpty(Madv) ? dsdonvi.Select(t => t.MaDv).First() : Madv;                    

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
                    List<string> list_trangthai = new List<string> { "CD", "HT", "DD", "CB", "CHT", "HCB" };
                    IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvk> model = _db.GiaHhDvk.Where(t => list_trangthai.Contains(t.Trangthai) && t.Macqcq == Madv);


                    if (string.IsNullOrEmpty(Nam))
                    {
                        Nam = Helpers.ConvertYearToStr(DateTime.Now.Year);
                        model = model.Where(t => t.Nam == Nam);
                    }
                    else
                    {
                        if (Nam != "all")
                        {
                            model = model.Where(t => t.Nam == Nam);
                        }
                    }

                    if (string.IsNullOrEmpty(Thang))
                    {
                        Thang = Helpers.ConvertYearToStr(DateTime.Now.Month);
                        model = model.Where(t => t.Thang == Thang);
                    }
                    else
                    {
                        if (Thang != "all")
                        {
                            model = model.Where(t => t.Thang == Thang);
                        }

                    }

                    ViewData["DsDonVi"] = dsdonvi;
                    ViewData["DsDonViChuyen"] = _db.DsDonVi;
                    ViewData["DsDiaBan"] = dsdiaban;
                    ViewData["Madv"] = Madv;
                    ViewData["Nam"] = Nam;
                    ViewData["Thang"] = Thang;
                    ViewData["Title"] = "Thông tin hồ sơ giá hàng hóa dịch vụ khác";
                    ViewData["MenuLv1"] = "menu_hhdvk";
                    ViewData["MenuLv2"] = "menu_hhdvk_xd";
                    return View("Views/Admin/Manages/DinhGia/GiaHhDvk/XetDuyet/Index.cshtml", model);
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

        public IActionResult Index_20240418(string Nam, string Thang, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.xd", "Index"))
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

                    var model = _db.GiaHhDvk.ToList();

                    /*return Ok(getdonvi.Level);*/

                    if (getdonvi.Level == "H")
                    {
                        if (string.IsNullOrEmpty(Nam))
                        {
                            Nam = Helpers.ConvertYearToStr(DateTime.Now.Year);
                            model = model.Where(t => t.Madv_h == Madv && t.Nam == Nam).ToList();
                        }
                        else
                        {
                            if (Nam != "all")
                            {
                                model = model.Where(t => t.Nam == Nam && t.Madv_h == Madv).ToList();
                            }
                            else
                            {
                                model = model.Where(t => t.Madv_h == Madv).ToList();
                            }
                        }

                        if (string.IsNullOrEmpty(Thang))
                        {
                            Thang = Helpers.ConvertYearToStr(DateTime.Now.Month);
                            model = model.Where(t => t.Madv_h == Madv && t.Thang == Thang).ToList();
                        }
                        else
                        {
                            if (Thang != "all")
                            {
                                model = model.Where(t => t.Thang == Thang && t.Madv_h == Madv).ToList();
                            }
                            else
                            {
                                model = model.Where(t => t.Madv_h == Madv).ToList();
                            }
                        }

                        var model_new = (from kk in model
                                         join nhom in _db.GiaHhDvkNhom on kk.Matt equals nhom.Matt
                                         select new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvk
                                         {
                                             Id = kk.Id,
                                             Madiaban = kk.Madiaban,
                                             Matt = kk.Matt,
                                             Tentt = nhom.Tentt,
                                             Mahs = kk.Mahs,
                                             MadvCh = GetMadvChuyen(Madv, kk),
                                             Macqcq = Madv,
                                             Madv = kk.Madv_h,
                                             Thang = kk.Thang,
                                             Nam = kk.Nam,
                                             Thoidiem = kk.Thoidiem_h,
                                             Trangthai = kk.Trangthai_h,
                                             Soqd = kk.Soqd,
                                             Level = getdonvi.Level,
                                             Ipf1 = kk.Ipf1,
                                         });

                        var model_join = (from kkj in model_new
                                          join dv in dsdonvi on kkj.MadvCh equals dv.MaDv
                                          select new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvk
                                          {
                                              Id = kkj.Id,
                                              Madiaban = kkj.Madiaban,
                                              Matt = kkj.Matt,
                                              Tentt = kkj.Tentt,
                                              Mahs = kkj.Mahs,
                                              MadvCh = kkj.MadvCh,
                                              TendvCh = dv.TenDv,
                                              Macqcq = kkj.Macqcq,
                                              Madv = kkj.Madv,
                                              Thang = kkj.Thang,
                                              Nam = kkj.Nam,
                                              Thoidiem = kkj.Thoidiem,
                                              Trangthai = kkj.Trangthai,
                                              Soqd = kkj.Soqd,
                                              Level = kkj.Level,
                                              Ipf1 = kkj.Ipf1,
                                          });

                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                        {
                            ViewData["DsDonVi"] = dsdonvi;
                        }
                        else
                        {
                            ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.MaDv == Madv);
                        }
                        ViewData["DsDiaBan"] = dsdiaban;
                        ViewData["Madv"] = Madv;
                        ViewData["Nam"] = Nam;
                        ViewData["Thang"] = Thang;
                        ViewData["Title"] = "Thông tin hồ sơ giá hàng hóa dịch vụ khác";
                        ViewData["MenuLv1"] = "menu_hhdvk";
                        ViewData["MenuLv2"] = "menu_hhdvk_xd";
                        return View("Views/Admin/Manages/DinhGia/GiaHhDvk/XetDuyet/Index.cshtml", model_join);
                    }
                    else if (getdonvi.Level == "T")
                    {
                        if (string.IsNullOrEmpty(Nam))
                        {
                            Nam = Helpers.ConvertYearToStr(DateTime.Now.Year);
                            model = model.Where(t => t.Madv_t == Madv && t.Nam == Nam).ToList();
                        }
                        else
                        {
                            if (Nam != "all")
                            {
                                model = model.Where(t => t.Nam == Nam && t.Madv_t == Madv).ToList();
                            }
                            else
                            {
                                model = model.Where(t => t.Madv_t == Madv).ToList();
                            }
                        }

                        if (string.IsNullOrEmpty(Thang))
                        {
                            Thang = Helpers.ConvertYearToStr(DateTime.Now.Month);
                            model = model.Where(t => t.Madv_t == Madv && t.Thang == Thang).ToList();
                        }
                        else
                        {
                            if (Thang != "all")
                            {
                                model = model.Where(t => t.Thang == Thang && t.Madv_t == Madv).ToList();
                            }
                            else
                            {
                                model = model.Where(t => t.Madv_t == Madv).ToList();
                            }
                        }

                        var model_new = (from kk in model
                                         join nhom in _db.GiaHhDvkNhom on kk.Matt equals nhom.Matt
                                         select new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvk
                                         {
                                             Id = kk.Id,
                                             Madiaban = kk.Madiaban,
                                             Matt = kk.Matt,
                                             Tentt = nhom.Tentt,
                                             Mahs = kk.Mahs,
                                             MadvCh = GetMadvChuyen(Madv, kk),
                                             Macqcq = Madv,
                                             Madv = kk.Madv_t,
                                             Thang = kk.Thang,
                                             Nam = kk.Nam,
                                             Thoidiem = kk.Thoidiem_t,
                                             Trangthai = kk.Trangthai_t,
                                             Soqd = kk.Soqd,
                                             Level = getdonvi.Level,
                                             Ipf1 = kk.Ipf1,
                                         });

                        var model_join = (from kkj in model_new
                                          join dv in dsdonvi on kkj.MadvCh equals dv.MaDv
                                          select new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvk
                                          {
                                              Id = kkj.Id,
                                              Madiaban = kkj.Madiaban,
                                              Matt = kkj.Matt,
                                              Tentt = kkj.Tentt,
                                              Mahs = kkj.Mahs,
                                              MadvCh = kkj.MadvCh,
                                              TendvCh = dv.TenDv,
                                              Macqcq = kkj.Macqcq,
                                              Madv = kkj.Madv,
                                              Thang = kkj.Thang,
                                              Nam = kkj.Nam,
                                              Thoidiem = kkj.Thoidiem,
                                              Trangthai = kkj.Trangthai,
                                              Soqd = kkj.Soqd,
                                              Level = kkj.Level,
                                              Ipf1 = kkj.Ipf1,
                                          });

                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                        {
                            ViewData["DsDonVi"] = dsdonvi;
                        }
                        else
                        {
                            ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.MaDv == Madv);
                        }
                        ViewData["DsDiaBan"] = dsdiaban;
                        ViewData["Madv"] = Madv;
                        ViewData["Nam"] = Nam;
                        ViewData["Nam"] = Nam;
                        ViewData["Thang"] = Thang;
                        ViewData["Title"] = "Thông tin hồ sơ giá hàng hóa dịch vụ khác";
                        ViewData["MenuLv1"] = "menu_hhdvk";
                        ViewData["MenuLv2"] = "menu_hhdvk_xd";
                        return View("Views/Admin/Manages/DinhGia/GiaHhDvk/XetDuyet/Index.cshtml", model_join);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(Nam))
                        {
                            Nam = Helpers.ConvertYearToStr(DateTime.Now.Year);
                            model = model.Where(t => t.Madv_ad == Madv && t.Nam == Nam).ToList();
                        }
                        else
                        {
                            if (Nam != "all")
                            {
                                model = model.Where(t => t.Nam == Nam && t.Madv_ad == Madv).ToList();
                            }
                            else
                            {
                                model = model.Where(t => t.Madv_ad == Madv).ToList();
                            }
                        }

                        if (string.IsNullOrEmpty(Thang))
                        {
                            Thang = Helpers.ConvertYearToStr(DateTime.Now.Month);
                            model = model.Where(t => t.Madv_ad == Madv && t.Thang == Thang).ToList();
                        }
                        else
                        {
                            if (Thang != "all")
                            {
                                model = model.Where(t => t.Thang == Thang && t.Madv_ad == Madv).ToList();
                            }
                            else
                            {
                                model = model.Where(t => t.Madv_ad == Madv).ToList();
                            }
                        }

                        var model_new = (from kk in model
                                         join nhom in _db.GiaHhDvkNhom on kk.Matt equals nhom.Matt
                                         select new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvk
                                         {
                                             Id = kk.Id,
                                             Madiaban = kk.Madiaban,
                                             Matt = kk.Matt,
                                             Tentt = nhom.Tentt,
                                             Mahs = kk.Mahs,
                                             MadvCh = GetMadvChuyen(Madv, kk),
                                             Macqcq = Madv,
                                             Madv = kk.Madv_ad,
                                             Thang = kk.Thang,
                                             Nam = kk.Nam,
                                             Thoidiem = kk.Thoidiem_ad,
                                             Trangthai = kk.Trangthai_ad,
                                             Soqd = kk.Soqd,
                                             Level = getdonvi.Level,
                                             Ipf1 = kk.Ipf1,
                                         });

                        var model_join = (from kkj in model_new
                                          join dv in dsdonvi on kkj.MadvCh equals dv.MaDv
                                          select new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvk
                                          {
                                              Id = kkj.Id,
                                              Madiaban = kkj.Madiaban,
                                              Matt = kkj.Matt,
                                              Tentt = kkj.Tentt,
                                              Mahs = kkj.Mahs,
                                              MadvCh = kkj.MadvCh,
                                              TendvCh = dv.TenDv,
                                              Macqcq = kkj.Macqcq,
                                              Madv = kkj.Madv,
                                              Thang = kkj.Thang,
                                              Nam = kkj.Nam,
                                              Thoidiem = kkj.Thoidiem,
                                              Trangthai = kkj.Trangthai,
                                              Soqd = kkj.Soqd,
                                              Level = kkj.Level,
                                              Ipf1 = kkj.Ipf1,
                                          });

                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                        {
                            ViewData["DsDonVi"] = dsdonvi;
                        }
                        else
                        {
                            ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.MaDv == Madv);
                        }
                        ViewData["DsDiaBan"] = dsdiaban;
                        ViewData["Madv"] = Madv;
                        ViewData["Nam"] = Nam;
                        ViewData["Nam"] = Nam;
                        ViewData["Thang"] = Thang;
                        ViewData["Title"] = "Thông tin hồ sơ giá hàng hóa dịch vụ khác";
                        ViewData["MenuLv1"] = "menu_hhdvk";
                        ViewData["MenuLv2"] = "menu_hhdvk_xd";
                        return View("Views/Admin/Manages/DinhGia/GiaHhDvk/XetDuyet/Index.cshtml", model_join);
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

        public IActionResult ChuyenXd(string mahs, string madv, string macqcq)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.xd", "Approve"))
                {
                    var model = _db.GiaHhDvk.FirstOrDefault(t => t.Mahs == mahs);

                    if (madv == model.Madv)
                    {
                        model.Macqcq = macqcq;
                        model.Trangthai = "HT";
                    }

                    if (madv == model.Madv_h)
                    {
                        model.Macqcq_h = macqcq;
                        model.Trangthai_h = "HT";
                    }

                    if (madv == model.Madv_t)
                    {
                        model.Macqcq_t = macqcq;
                        model.Trangthai_t = "HT";
                    }

                    if (madv == model.Madv_ad)
                    {
                        model.Macqcq_ad = macqcq;
                        model.Trangthai_ad = "HT";
                    }

                    var dvcq_join = from dvcq in _db.DsDonVi
                                    join db in _db.DsDiaBan on dvcq.MaDiaBan equals db.MaDiaBan
                                    select new VMDsDonVi
                                    {
                                        Id = dvcq.Id,
                                        MaDiaBan = dvcq.MaDiaBan,
                                        MaDv = dvcq.MaDv,
                                        TenDv = dvcq.TenDv,
                                        Level = db.Level,
                                    };
                    var chk_dvcq = dvcq_join.FirstOrDefault(t => t.MaDv == macqcq);

                    if (chk_dvcq != null && chk_dvcq.Level == "T")
                    {
                        model.Madv_t = macqcq;
                        model.Thoidiem_t = DateTime.Now;
                        model.Trangthai_t = "CHT";
                    }
                    if (chk_dvcq != null && chk_dvcq.Level == "ADMIN")
                    {
                        model.Madv_ad = macqcq;
                        model.Thoidiem_ad = DateTime.Now;
                        model.Trangthai_ad = "CHT";
                    }
                    if (chk_dvcq != null && chk_dvcq.Level == "H")
                    {
                        model.Madv_h = macqcq;
                        model.Thoidiem_h = DateTime.Now;
                        model.Trangthai_h = "CHT";
                    }

                    _db.GiaHhDvk.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaHhDvkXd", new { Nam = model.Nam, Thang = model.Thang, Madv = madv });
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.xd", "Approve"))
                {
                    var model = _db.GiaHhDvk.FirstOrDefault(t => t.Id == id_tralai);

                    //Gán trạng thái của đơn vị chuyển hồ sơ
                    if (madv_tralai == model.Macqcq)
                    {
                        model.Macqcq = null;
                        model.Trangthai = "HHT";
                        model.Lydo = Lydo;
                    }

                    if (madv_tralai == model.Macqcq_h)
                    {
                        model.Macqcq_h = null;
                        model.Trangthai_h = "HHT";
                        model.Lydo = Lydo;
                    }

                    if (madv_tralai == model.Macqcq_t)
                    {
                        model.Macqcq_t = null;
                        model.Trangthai_t = "HHT";
                        model.Lydo = Lydo;
                    }

                    if (madv_tralai == model.Macqcq_ad)
                    {
                        model.Macqcq_ad = null;
                        model.Trangthai_ad = "HHT";
                        model.Lydo = Lydo;
                    }


                    //Gán trạng thái của đơn vị tiếp nhận hồ sơ


                    if (madv_tralai == model.Madv_h)
                    {
                        model.Macqcq_h = null;
                        model.Madv_h = null;
                        model.Thoidiem_h = DateTime.MinValue;
                        model.Trangthai_h = null;
                    }

                    if (madv_tralai == model.Madv_t)
                    {
                        model.Macqcq_t = null;
                        model.Madv_t = null;
                        model.Thoidiem_t = DateTime.MinValue;
                        model.Trangthai_t = null;
                    }

                    if (madv_tralai == model.Madv_ad)
                    {
                        model.Macqcq_ad = null;
                        model.Madv_ad = null;
                        model.Thoidiem_ad = DateTime.MinValue;
                        model.Trangthai_ad = null;
                    }

                    _db.GiaHhDvk.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaHhDvkXd", new { Nam = model.Nam, Thang = model.Thang, Madv = madv_tralai });
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.xd", "Approve"))
                {
                    var model = _db.GiaHhDvk.FirstOrDefault(t => t.Mahs == mahs_cb);

                    model.Trangthai = "CB";
                    model.Congbo = "DACONGBO";

                    _db.GiaHhDvk.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaHhDvkXd");
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.xd", "Approve"))
                {
                    var model = _db.GiaHhDvk.FirstOrDefault(t => t.Mahs == mahs_hcb);

                    model.Trangthai = "HCB";
                    model.Congbo = "CHUACONGBO";

                    _db.GiaHhDvk.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaHhDvkXd");
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

        private static string GetMadvChuyen(string macqcq, CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvk hoso)
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


    }
}
