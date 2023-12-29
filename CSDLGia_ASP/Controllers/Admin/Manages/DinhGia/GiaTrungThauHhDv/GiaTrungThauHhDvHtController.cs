using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaTrungThauHhDv
{
    public class GiaTrungThauHhDvHtController : Controller
    {

        private readonly CSDLGiaDBContext _db;

        public GiaTrungThauHhDvHtController(CSDLGiaDBContext db)
        {
            _db = db;
        }
        [Route("GiaTrungThauHhDvHt")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.muataisan.xetduyet", "Index"))
                {
                    var dsdonvi = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    var dsdiaban = _db.DsDiaBan.Where(t => t.Level != "H");

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
                                        ChucNang = dv.ChucNang,
                                        Level = db.Level,
                                    }).First();

                    if (getdonvi.Level == "ADMIN")
                    {
                        var model = _db.GiaMuaTaiSan.Where(t => t.Madv_ad == Madv).ToList();
                        if (string.IsNullOrEmpty(Nam))
                        {
                            model = model.ToList();
                        }
                        else
                        {
                            if (Nam != "all")
                            {
                                model = model.Where(t => t.Thoidiem_ad.Year == int.Parse(Nam)).ToList();
                            }
                            else
                            {
                                model = model.ToList();
                            }
                        }
                        var model_join = from dg in model
                                         select new GiaMuaTaiSan
                                         {
                                             Id = dg.Id,
                                             Macqcq = Madv,
                                             Madv = dg.Madv,
                                             Madv_t = dg.Madv_t,
                                             Madv_h = dg.Madv_h,
                                             Mahs = dg.Mahs,
                                             Thoidiem = dg.Thoidiem_ad,
                                             Soqd = dg.Soqd,
                                             Madiaban = dg.Madiaban,
                                             Trangthai_ad = dg.Trangthai_ad,
                                             Trangthai_t = dg.Trangthai_t,
                                             Trangthai_h = dg.Trangthai_h,
                                             Thongtinqd = dg.Thongtinqd,
                                             Level = getdonvi.Level,
                                             Tennhathau = dg.Tennhathau,
                                         };
                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                        {
                            ViewData["DsDonVi"] = dsdonvi;
                        }
                        else
                        {
                            ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.MaDv == Madv);
                        }

                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H"); ;
                        ViewData["Madv"] = Madv;
                        ViewData["Nam"] = Nam;
                        ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                        ViewData["Title"] = "Hoàn thành định giá trúng thầu mua hàng hóa dịch vụ";
                        ViewData["MenuLv1"] = "menu_mts";
                        ViewData["MenuLv2"] = "menu_giamts_ht";
                        return View("Views/Admin/Manages/DinhGia/GiaTrungThauHhDv/HoanThanh/Index.cshtml", model_join);
                    }
                    else
                    {
                        var model = _db.GiaMuaTaiSan.Where(t => t.Madv_t == Madv).ToList();
                        if (string.IsNullOrEmpty(Nam))
                        {
                            model = model.ToList();
                        }
                        else
                        {
                            if (Nam != "all")
                            {
                                model = model.Where(t => t.Thoidiem_t.Year == int.Parse(Nam)).ToList();
                            }
                            else
                            {
                                model = model.ToList();
                            }
                        }
                        var model_join = from dg in model
                                         select new GiaMuaTaiSan
                                         {
                                             Id = dg.Id,
                                             Madv = dg.Madv,
                                             Macqcq = Madv,
                                             Macqcq_h = dg.Macqcq_h,
                                             Macqcq_t = dg.Macqcq_t,
                                             Mahs = dg.Mahs,
                                             Thoidiem = dg.Thoidiem_t,
                                             Soqd = dg.Soqd,
                                             Madiaban = dg.Madiaban,
                                             Trangthai_ad = dg.Trangthai_ad,
                                             Trangthai_t = dg.Trangthai_t,
                                             Trangthai_h = dg.Trangthai_h,
                                             Thongtin = dg.Thongtin,
                                             Level = getdonvi.Level,
                                             Tennhathau = dg.Tennhathau,
                                         };
                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                        {
                            ViewData["DsDonVi"] = dsdonvi;
                        }
                        else
                        {
                            ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.MaDv == Madv);
                        }

                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H"); ;
                        ViewData["Madv"] = Madv;
                        ViewData["Nam"] = Nam;
                        ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                        ViewData["Title"] = "Hoàn thành định giá trúng thầu mua hàng hóa dịch vụ";
                        ViewData["MenuLv1"] = "menu_mts";
                        ViewData["MenuLv2"] = "menu_giamts_ht";
                        return View("Views/Admin/Manages/DinhGia/GiaTrungThauHhDv/HoanThanh/Index.cshtml", model_join);
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

        public IActionResult HoanThanh(string mahs_complete, string Macqcq)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.muataisan.xetduyet", "Approve"))
                {
                    var model = _db.GiaMuaTaiSan.FirstOrDefault(p => p.Mahs == mahs_complete);

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
                    var chk_dvcq = dvcq_join.FirstOrDefault(t => t.MaDv == Macqcq);
                    model.Macqcq = Macqcq;
                    model.Trangthai = "HT";
                    if (chk_dvcq != null && chk_dvcq.Level == "T")
                    {
                        model.Madv_t = Macqcq;
                        model.Thoidiem_t = DateTime.Now;
                        model.Trangthai_t = "CCB";
                    }
                    else if (chk_dvcq != null && chk_dvcq.Level == "ADMIN")
                    {
                        model.Madv_ad = Macqcq;
                        model.Thoidiem_ad = DateTime.Now;
                        model.Trangthai_ad = "CCB";
                    }
                    else
                    {
                        model.Madv_h = Macqcq;
                        model.Thoidiem_h = DateTime.Now;
                        model.Trangthai_h = "CCB";
                    }
                    _db.GiaMuaTaiSan.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "GiaTrungThauHhDv", new { Madv = model.Madv, Nam = model.Thoidiem.Year });
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

        public IActionResult ChuyenHoanThanh(string mahs_complete, string Macqcq, string madv_hientai)
        {

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.muataisan.xetduyet", "Approve"))
                {
                    var model = _db.GiaMuaTaiSan.FirstOrDefault(p => p.Mahs == mahs_complete);
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
                    var dvchuyen = dvcq_join.FirstOrDefault(t => t.MaDv == madv_hientai);


                    model.Trangthai_ad = "CCB";
                    model.Thoidiem_ad = DateTime.Now;
                    model.Madv_ad = Macqcq;
                    if (dvchuyen != null && dvchuyen.Level == "T")
                    {
                        model.Macqcq_t = Macqcq;
                        model.Trangthai_t = "CCB";
                    }
                    if (dvchuyen != null && dvchuyen.Level == "H")
                    {
                        model.Macqcq_h = Macqcq;
                        model.Trangthai_h = "CCB";
                    }
                    _db.GiaMuaTaiSan.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "GiaTrungThauHhDvHt", new { Madv = madv_hientai });
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.muataisan.xetduyet", "Approve"))
                {
                    var model = _db.GiaMuaTaiSan.FirstOrDefault(t => t.Id == id_tralai);

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

                    _db.GiaMuaTaiSan.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaTrungThauHhDvHt", new { Madv = madv_tralai, Nam = model.Thoidiem.Year });
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.muataisan.xetduyet", "Approve"))
                {
                    var model = _db.GiaMuaTaiSan.FirstOrDefault(t => t.Mahs == mahs_cb);

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

                    _db.GiaMuaTaiSan.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "GiaTrungThauHhDvHt");
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.muataisan.xetduyet", "Approve"))
                {
                    var model = _db.GiaMuaTaiSan.FirstOrDefault(t => t.Mahs == mahs_hcb);

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

                    _db.GiaMuaTaiSan.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaTrungThauHhDvHt");
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


