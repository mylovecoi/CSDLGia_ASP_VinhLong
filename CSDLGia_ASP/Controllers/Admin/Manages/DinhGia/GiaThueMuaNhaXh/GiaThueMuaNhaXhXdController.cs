using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaThueMuaNhaXhXd
{
    public class GiaThueMuaNhaXhXdController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaThueMuaNhaXhXdController(CSDLGiaDBContext db)
        {
            _db = db;
        }
        
        [Route("GiaThueMuaNhaXhXd/XetDuyet")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanha.xetduyet", "Index"))
                {
                    var dsdonvi = _db.DsDonVi;
                    var dsdiaban = _db.DsDiaBan;

                    // Lấy mã đơn vị chuyển hồ sơ

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
                                    }).FirstOrDefault();

                    // Hết phần lấy mã đơn vị ( Nếu đăng nhập bằng SA thì mã đơn vị có Level = ADMIN )

                    var model = _db.GiaThueMuaNhaXh.ToList();

                    // Nếu đơn vị chuyển lên có level huyện thì lấy trong phần huyện

                    if (getdonvi.Level == "H")
                    {
                        if (string.IsNullOrEmpty(Nam))
                        {
                            model = model.Where(t => t.Madv_h == Madv).ToList();
                        }
                        else
                        {
                            if (Nam != "all")
                            {
                                model = model.Where(t => t.Thoidiem_h.Year == int.Parse(Nam) && t.Madv_h == Madv).ToList();
                            }
                            else
                            {
                                model = model.Where(t => t.Madv_h == Madv).ToList();
                            }
                        }

                        var model_new = (from kk in model
                                         select new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMuaNhaXh
                                         {
                                             Id = kk.Id,
                                             Mahs = kk.Mahs,
                                             Madiaban = kk.Madiaban,
                                             MadvCh = GetMadvChuyen(Madv, kk),
                                             Macqcq = Madv,
                                             Madv = kk.Madv_h,
                                             Thoidiem = kk.Thoidiem_h,
                                             Mota = kk.Mota,
                                             Trangthai = kk.Trangthai_h,
                                             Soqd = kk.Soqd,
                                             Level = getdonvi.Level,
                                             Ipf1 = kk.Ipf1,
                                             Ipf2 = kk.Ipf2,
                                             Ipf3 = kk.Ipf3,
                                             Ipf4 = kk.Ipf4,
                                             Ipf5 = kk.Ipf5,
                                         });

                        var model_join = (from kkj in model_new
                                          join dv in dsdonvi on kkj.MadvCh equals dv.MaDv
                                          select new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMuaNhaXh
                                          {
                                              Id = kkj.Id,
                                              Mahs = kkj.Mahs,
                                              Madiaban = kkj.Madiaban,
                                              MadvCh = kkj.MadvCh,
                                              TendvCh = dv.TenDv,
                                              Macqcq = kkj.Macqcq,
                                              Madv = kkj.Madv,
                                              Thoidiem = kkj.Thoidiem,
                                              Mota = kkj.Mota,
                                              Trangthai = kkj.Trangthai,
                                              Soqd = kkj.Soqd,
                                              Level = kkj.Level,
                                              Ipf1 = kkj.Ipf1,
                                              Ipf2 = kkj.Ipf2,
                                              Ipf3 = kkj.Ipf3,
                                              Ipf4 = kkj.Ipf4,
                                              Ipf5 = kkj.Ipf5,
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
                        ViewData["Title"] = "Thông tin xét duyệt giá thuê mua nhà xã hội";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgtmnxh";
                        ViewData["MenuLv3"] = "menu_dgtmnxh_xd";
                        return View("Views/Admin/Manages/DinhGia/GiaThueMuaNhaXh/XetDuyet/Index.cshtml", model_join);
                    }
                    else if (getdonvi.Level == "T")
                    {
                        if (string.IsNullOrEmpty(Nam))
                        {
                            model = model.Where(t => t.Madv_t == Madv).ToList();
                        }
                        else
                        {
                            if (Nam != "all")
                            {
                                model = model.Where(t => t.Thoidiem_t.Year == int.Parse(Nam) && t.Madv_t == Madv).ToList();
                            }
                            else
                            {
                                model = model.Where(t => t.Madv_t == Madv).ToList();
                            }
                        }

                        var model_new = (from kk in model
                                         select new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMuaNhaXh
                                         {
                                             Id = kk.Id,
                                             Mahs = kk.Mahs,
                                             Madiaban = kk.Madiaban,
                                             MadvCh = GetMadvChuyen(Madv, kk),
                                             Macqcq = Madv,
                                             Madv = kk.Madv_t,
                                             Thoidiem = kk.Thoidiem_t,
                                             Mota = kk.Mota,
                                             Trangthai = kk.Trangthai_t,
                                             Soqd = kk.Soqd,
                                             Level = getdonvi.Level,
                                             Ipf1 = kk.Ipf1,
                                             Ipf2 = kk.Ipf2,
                                             Ipf3 = kk.Ipf3,
                                             Ipf4 = kk.Ipf4,
                                             Ipf5 = kk.Ipf5,
                                         });

                        var model_join = (from kkj in model_new
                                          join dv in dsdonvi on kkj.MadvCh equals dv.MaDv
                                          select new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMuaNhaXh
                                          {
                                              Id = kkj.Id,
                                              Mahs = kkj.Mahs,
                                              Madiaban = kkj.Madiaban,
                                              MadvCh = kkj.MadvCh,
                                              TendvCh = dv.TenDv,
                                              Macqcq = kkj.Macqcq,
                                              Madv = kkj.Madv,
                                              Thoidiem = kkj.Thoidiem,
                                              Mota = kkj.Mota,
                                              Trangthai = kkj.Trangthai,
                                              Soqd = kkj.Soqd,
                                              Level = kkj.Level,
                                              Ipf1 = kkj.Ipf1,
                                              Ipf2 = kkj.Ipf2,
                                              Ipf3 = kkj.Ipf3,
                                              Ipf4 = kkj.Ipf4,
                                              Ipf5 = kkj.Ipf5,
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
                        ViewData["Title"] = "Thông tin xét duyệt giá thuê mua nhà xã hội";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgtmnxh";
                        ViewData["MenuLv3"] = "menu_dgtmnxh_xd";
                        return View("Views/Admin/Manages/DinhGia/GiaThueMuaNhaXh/XetDuyet/Index.cshtml", model_join);
                    }
                    else
                    {

                        if (string.IsNullOrEmpty(Nam))
                        {
                            model = model.Where(t => t.Madv_ad == Madv).ToList();
                        }
                        else
                        {
                            if (Nam != "all")
                            {
                                model = model.Where(t => t.Thoidiem_ad.Year == int.Parse(Nam) && t.Madv_ad == Madv).ToList();
                            }
                            else
                            {
                                model = model.Where(t => t.Madv_ad == Madv).ToList();
                            }
                        }


                        var model_new = (from kk in model
                                         select new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMuaNhaXh
                                         {
                                             Id = kk.Id,
                                             Mahs = kk.Mahs,
                                             Madiaban = kk.Madiaban,
                                             MadvCh = GetMadvChuyen(Madv, kk),
                                             Macqcq = Madv,
                                             Madv = kk.Madv_ad,
                                             Thoidiem = kk.Thoidiem_ad,
                                             Mota = kk.Mota,
                                             Trangthai = kk.Trangthai_ad,
                                             Soqd = kk.Soqd,
                                             Level = getdonvi.Level,
                                             Ipf1 = kk.Ipf1,
                                             Ipf2 = kk.Ipf2,
                                             Ipf3 = kk.Ipf3,
                                             Ipf4 = kk.Ipf4,
                                             Ipf5 = kk.Ipf5,
                                         });
  
                        var model_join = (from kkj in model_new
                                          join dv in dsdonvi on kkj.MadvCh equals dv.MaDv
                                          select new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMuaNhaXh
                                          {
                                              Id = kkj.Id,
                                              Mahs = kkj.Mahs,
                                              Madiaban = kkj.Madiaban,
                                              MadvCh = kkj.MadvCh,
                                              TendvCh = dv.TenDv,
                                              Macqcq = kkj.Macqcq,
                                              Madv = kkj.Madv,
                                              Thoidiem = kkj.Thoidiem,
                                              Mota = kkj.Mota,
                                              Trangthai = kkj.Trangthai,
                                              Soqd = kkj.Soqd,
                                              Level = kkj.Level,
                                              Ipf1 = kkj.Ipf1,
                                              Ipf2 = kkj.Ipf2,
                                              Ipf3 = kkj.Ipf3,
                                              Ipf4 = kkj.Ipf4,
                                              Ipf5 = kkj.Ipf5,
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
                        ViewData["Title"] = "Thông tin xét duyệt giá thuê mua nhà xã hội";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgtmnxh";
                        ViewData["MenuLv3"] = "menu_dgtmnxh_xd";
                        return View("Views/Admin/Manages/DinhGia/GiaThueMuaNhaXh/XetDuyet/Index.cshtml", model_join);
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanha.xetduyet", "Approve"))
                {
                    var model = _db.GiaThueMuaNhaXh.FirstOrDefault(t => t.Mahs == mahs);

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
                    _db.GiaThueMuaNhaXh.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "GiaThueMuaNhaXhXd", new { Madv = madv, Nam = model.Thoidiem.Year });
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanha.xetduyet", "Approve"))
                {
                    var model = _db.GiaThueMuaNhaXh.FirstOrDefault(t => t.Id == id_tralai);

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

                    _db.GiaThueMuaNhaXh.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaThueMuaNhaXhXd", new { Madv = madv_tralai, Nam = model.Thoidiem.Year });
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanha.xetduyet", "Approve"))
                {
                    var model = _db.GiaThueMuaNhaXh.FirstOrDefault(t => t.Mahs == mahs_cb);

                    model.Trangthai_ad = "CB";
                    model.Congbo = "DACONGBO";

                    _db.GiaThueMuaNhaXh.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "GiaThueMuaNhaXhXd");
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanha.xetduyet", "Approve"))
                {
                    var model = _db.GiaThueMuaNhaXh.FirstOrDefault(t => t.Mahs == mahs_hcb);

                    model.Trangthai_ad = "HCB";
                    model.Congbo = "CHUACONGBO";

                    _db.GiaThueMuaNhaXh.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaThueMuaNhaXhXd");
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

        private static string GetMadvChuyen(string macqcq, CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMuaNhaXh hoso)
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
