using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using CSDLGia_ASP.ViewModels.Manages.ChiSoGiaTd;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.ChiSoGiaTd
{
    public class ChiSoGiaTdXdController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public ChiSoGiaTdXdController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("ChiSoGiaTieuDung/Xetduyet")]
        [HttpGet]
        public IActionResult Index(string Madv, string Matt)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csg.chisogia.hoso.xetduyet", "Index"))
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

                    List<CSDLGia_ASP.Models.Manages.ChiSoGiaTd.ChiSoGiaTd> model = new List<CSDLGia_ASP.Models.Manages.ChiSoGiaTd.ChiSoGiaTd>();
                    if (getdonvi.Level == "H")
                    {

                        if (string.IsNullOrEmpty(Matt))
                        {
                            model = _db.ChiSoGiaTd.Where(x => x.Madv == Madv).ToList();
                            ViewData["matt"] = _db.ChiSoGiaTdDm.FirstOrDefault(x => x.Matt != "1").Matt;
                            ViewData["nam"] = _db.ChiSoGiaTdDm.FirstOrDefault(x => x.Matt != "1").Nam;
                        }
                        else
                        {
                            var getNam = _db.ChiSoGiaTdDm.FirstOrDefault(x => x.Matt == Matt).Nam;
                            model = _db.ChiSoGiaTd.Where(x => x.Nam == getNam && x.Madv == Madv).ToList();
                            ViewData["matt"] = Matt;
                            ViewData["nam"] = getNam;
                        }

                        var model_new = (from csg in model
                                         select new CSDLGia_ASP.Models.Manages.ChiSoGiaTd.ChiSoGiaTd
                                         {
                                             Id = csg.Id,
                                             Mahs = csg.Mahs,
                                             MadvCh = GetMadvChuyen(Madv, csg),
                                             Macqcq = Madv,
                                             Madv = csg.Madv_h,
                                             Thoidiem = csg.Thoidiem_h,
                                             Trangthai = csg.Trangthai_h,
                                             Level = getdonvi.Level,
                                         });

                        var model_join = (from csg in model_new
                                          join dv in dsdonvi on csg.MadvCh equals dv.MaDv
                                          select new CSDLGia_ASP.Models.Manages.ChiSoGiaTd.ChiSoGiaTd
                                          {
                                              Id = csg.Id,
                                              Mahs = csg.Mahs,
                                              MadvCh = csg.MadvCh,
                                              TendvCh = dv.TenDv,
                                              Macqcq = csg.Macqcq,
                                              Madv = csg.Madv,
                                              Thoidiem = csg.Thoidiem,
                                              Trangthai = csg.Trangthai,
                                              Level = csg.Level,
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
                        ViewData["listTt"] = _db.ChiSoGiaTdDm.Where(x => x.Matt != "1");
                        ViewData["Title"] = "Thông tin chỉ số giá tiêu dùng";
                        ViewData["MenuLv1"] = "menu_csg";
                        ViewData["MenuLv2"] = "menu_csgDm";
                        ViewData["MenuLv3"] = "menu_csgDm";
                        return View("Views/Admin/Manages/ChiSoGiaTd/XetDuyet/Index.cshtml", model_join);
                    }
                    else if (getdonvi.Level == "T")
                    {
                        if (string.IsNullOrEmpty(Matt))
                        {
                            model = _db.ChiSoGiaTd.Where(x => x.Madv == Madv).ToList();
                            ViewData["matt"] = _db.ChiSoGiaTdDm.FirstOrDefault(x => x.Matt != "1").Matt;
                            ViewData["nam"] = _db.ChiSoGiaTdDm.FirstOrDefault(x => x.Matt != "1").Nam;
                        }
                        else
                        {
                            var getNam = _db.ChiSoGiaTdDm.FirstOrDefault(x => x.Matt == Matt).Nam;
                            model = _db.ChiSoGiaTd.Where(x => x.Nam == getNam && x.Madv == Madv).ToList();
                            ViewData["matt"] = Matt;
                            ViewData["nam"] = getNam;
                        }

                        var model_new = (from csg in model
                                         select new CSDLGia_ASP.Models.Manages.ChiSoGiaTd.ChiSoGiaTd
                                         {
                                             Id = csg.Id,
                                             Mahs = csg.Mahs,
                                             MadvCh = GetMadvChuyen(Madv, csg),
                                             Macqcq = Madv,
                                             Madv = csg.Madv_t,
                                             Thoidiem = csg.Thoidiem_t,
                                             Trangthai = csg.Trangthai_t,
                                             Level = getdonvi.Level,
                                         });

                        var model_join = (from csg in model_new
                                          join dv in dsdonvi on csg.MadvCh equals dv.MaDv
                                          select new CSDLGia_ASP.Models.Manages.ChiSoGiaTd.ChiSoGiaTd
                                          {
                                              Id = csg.Id,
                                              Mahs = csg.Mahs,
                                              MadvCh = csg.MadvCh,
                                              TendvCh = dv.TenDv,
                                              Macqcq = csg.Macqcq,
                                              Madv = csg.Madv,
                                              Thoidiem = csg.Thoidiem,
                                              Trangthai = csg.Trangthai,
                                              Level = csg.Level,
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
                        ViewData["listTt"] = _db.ChiSoGiaTdDm.Where(x => x.Matt != "1");
                        ViewData["Title"] = "Thông tin chỉ số giá tiêu dùng";
                        ViewData["MenuLv1"] = "menu_csg";
                        ViewData["MenuLv2"] = "menu_csgDm";
                        ViewData["MenuLv3"] = "menu_csgDm";
                        return View("Views/Admin/Manages/ChiSoGiaTd/XetDuyet/Index.cshtml", model_join);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(Matt))
                        {
                            model = _db.ChiSoGiaTd.Where(x => x.Madv == Madv).ToList();
                            ViewData["matt"] = _db.ChiSoGiaTdDm.FirstOrDefault(x => x.Matt != "1").Matt;
                            ViewData["nam"] = _db.ChiSoGiaTdDm.FirstOrDefault(x => x.Matt != "1").Nam;
                        }
                        else
                        {
                            var getNam = _db.ChiSoGiaTdDm.FirstOrDefault(x => x.Matt == Matt).Nam;
                            model = _db.ChiSoGiaTd.Where(x => x.Nam == getNam && x.Madv == Madv).ToList();
                            ViewData["matt"] = Matt;
                            ViewData["nam"] = getNam;
                        }

                        var model_new = (from csg in model
                                         select new CSDLGia_ASP.Models.Manages.ChiSoGiaTd.ChiSoGiaTd
                                         {
                                             Id = csg.Id,
                                             Mahs = csg.Mahs,
                                             MadvCh = GetMadvChuyen(Madv, csg),
                                             Macqcq = Madv,
                                             Madv = csg.Madv_ad,
                                             Thoidiem = csg.Thoidiem_ad,
                                             Trangthai = csg.Trangthai_ad,
                                             Level = getdonvi.Level,
                                         });

                        var model_join = (from csg in model_new
                                          join dv in dsdonvi on csg.MadvCh equals dv.MaDv
                                          select new CSDLGia_ASP.Models.Manages.ChiSoGiaTd.ChiSoGiaTd
                                          {
                                              Id = csg.Id,
                                              Mahs = csg.Mahs,
                                              MadvCh = csg.MadvCh,
                                              TendvCh = dv.TenDv,
                                              Macqcq = csg.Macqcq,
                                              Madv = csg.Madv,
                                              Thoidiem = csg.Thoidiem,
                                              Trangthai = csg.Trangthai,
                                              Level = csg.Level,
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
                        ViewData["listTt"] = _db.ChiSoGiaTdDm.Where(x => x.Matt != "1");
                        ViewData["Title"] = "Thông tin chỉ số giá tiêu dùng";
                        ViewData["MenuLv1"] = "menu_csg";
                        ViewData["MenuLv2"] = "menu_csgDm";
                        ViewData["MenuLv3"] = "menu_csgDm";
                        return View("Views/Admin/Manages/ChiSoGiaTd/XetDuyet/Index.cshtml", model_join);
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
        private static string GetMadvChuyen(string macqcq, CSDLGia_ASP.Models.Manages.ChiSoGiaTd.ChiSoGiaTd hoso)
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
        public IActionResult ChuyenXd(string mahs, string madv, string macqcq)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csg.chisogia.hoso.xetduyet", "Approve"))
                {
                    var model = _db.ChiSoGiaTd.FirstOrDefault(t => t.Mahs == mahs);

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

                    _db.ChiSoGiaTd.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "ChiSoGiaTdXd", new { Madv = madv, Nam = model.Thoidiem.Year });
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

        public IActionResult TraLai(int id_tralai, string madv_tralai)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csg.chisogia.hoso.xetduyet", "Approve"))
                {
                    var model = _db.ChiSoGiaTd.FirstOrDefault(t => t.Id == id_tralai);

                    //Gán trạng thái của đơn vị chuyển hồ sơ
                    if (madv_tralai == model.Macqcq)
                    {
                        model.Macqcq = null;
                        model.Trangthai = "BTL";
                    }

                    if (madv_tralai == model.Macqcq_h)
                    {
                        model.Macqcq_h = null;
                        model.Trangthai_h = "BTL";
                    }

                    if (madv_tralai == model.Macqcq_t)
                    {
                        model.Macqcq_t = null;
                        model.Trangthai_t = "BTL";
                    }

                    if (madv_tralai == model.Macqcq_ad)
                    {
                        model.Macqcq_ad = null;
                        model.Trangthai_ad = "BTL";
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

                    _db.ChiSoGiaTd.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "ChiSoGiaTdXd", new { Madv = madv_tralai, Nam = model.Thoidiem.Year });
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
                if (Helpers.CheckPermission(HttpContext.Session, "csg.chisogia.hoso.xetduyet", "Approve"))
                {
                    var model = _db.ChiSoGiaTd.FirstOrDefault(t => t.Mahs == mahs_cb);

                    model.Thoidiem_ad = DateTime.Now;
                    model.Trangthai_ad = "CB";
                    model.Congbo = "DACONGBO";
                    if (model.Macqcq_h == model.Madv_ad)
                    {
                        model.Thoidiem_h = DateTime.Now;
                        model.Trangthai_h = "CB";
                    }
                    if (model.Macqcq_t == model.Madv_ad)
                    {
                        model.Thoidiem_t = DateTime.Now;
                        model.Trangthai_t = "CB";
                    }

                    _db.ChiSoGiaTd.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "ChiSoGiaTdXd");
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
                if (Helpers.CheckPermission(HttpContext.Session, "csg.chisogia.hoso.xetduyet", "Approve"))
                {
                    var model = _db.ChiSoGiaTd.FirstOrDefault(t => t.Mahs == mahs_hcb);

                    model.Thoidiem_ad = DateTime.Now;
                    model.Trangthai_ad = "HCB";
                    model.Congbo = "CHUACONGBO";
                    if (model.Macqcq_h == model.Madv_ad)
                    {
                        model.Thoidiem_h = DateTime.Now;
                        model.Trangthai_h = "HCB";
                    }
                    if (model.Macqcq_t == model.Madv_ad)
                    {
                        model.Thoidiem_t = DateTime.Now;
                        model.Trangthai_t = "HCB";
                    }

                    _db.ChiSoGiaTd.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "ChiSoGiaTd");
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
