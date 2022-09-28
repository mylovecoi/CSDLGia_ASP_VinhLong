using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CSDLGia_ASP.Database;
using System.Security.Cryptography;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GTroGiaTroCuoc
{
    public class TroGiaTroCuocHtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public TroGiaTroCuocHtController(CSDLGiaDBContext db)
        {
            _db = db;
        }
        [Route("GiaTGTCHt")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.tgtc.htg", "Index"))
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
                        var model = _db.GiaTroGiaTroCuoc.Where(t => t.Madv_ad == Madv ).ToList();
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
                                         select new VMDinhGiaTroGiaTroCuoc
                                         {
                                             Id = dg.Id,
                                             Macqcq = Madv,
                                             Madv = dg.Madv,
                                             Madv_t = dg.Madv_t,
                                             Madv_h = dg.Madv_h,
                                             Mahs = dg.Mahs,
                                             Thoidiem = dg.Thoidiem_ad,
                                             Soqd = dg.Soqd,
                                             Madiaban = getdonvi.MaDiaBan,
                                             Trangthai_ad = dg.Trangthai_ad,
                                             Trangthai_t = dg.Trangthai_t,
                                             Trangthai_h = dg.Trangthai_h,
                                             Thongtin = dg.Thongtin,
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

                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                        ViewData["Madv"] = Madv;
                        ViewData["Nam"] = Nam;
                        ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                        ViewData["Title"] = "Hoàn thành định giá Mức trợ giá trợ cước";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgtgtc";
                        ViewData["MenuLv3"] = "menu_dgtgtc_ht";
                        return View("Views/Admin/Manages/DinhGia/GiaTroGiaTroCuoc/HoanThanh/Index.cshtml", model_join);
                    }
                    else if (getdonvi.Level == "T")
                    {
                        var model = _db.GiaTroGiaTroCuoc.Where(t => t.Madv_t == Madv ).ToList();
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
                                         select new VMDinhGiaTroGiaTroCuoc
                                         {
                                             Id = dg.Id,
                                             Madv = dg.Madv,
                                             Macqcq = Madv,
                                             Macqcq_t = dg.Macqcq_t,
                                             Macqcq_h = dg.Macqcq_h,
                                             Mahs = dg.Mahs,
                                             Thoidiem = dg.Thoidiem_t,
                                             Soqd = dg.Soqd,
                                             Madiaban = getdonvi.MaDiaBan,
                                             Trangthai_ad = dg.Trangthai_ad,
                                             Trangthai_t = dg.Trangthai_t,
                                             Trangthai_h = dg.Trangthai_h,
                                             Thongtin = dg.Thongtin,
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

                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                        ViewData["Madv"] = Madv;
                        ViewData["Nam"] = Nam;
                        ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                        ViewData["Title"] = "Hoàn thành định giá Mức trợ giá trợ cước";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgtgtc";
                        ViewData["MenuLv3"] = "menu_dgtgtc_ht";
                        return View("Views/Admin/Manages/DinhGia/GiaTroGiaTroCuoc/HoanThanh/Index.cshtml", model_join);
                    }
                    else
                    {
                        var model = _db.GiaTroGiaTroCuoc.Where(t => t.Madv_h == Madv ).ToList();
                        if (string.IsNullOrEmpty(Nam))
                        {
                            model = model.ToList();
                        }
                        else
                        {
                            if (Nam != "all")
                            {
                                model = model.Where(t => t.Thoidiem_h.Year == int.Parse(Nam)).ToList();
                            }
                            else
                            {
                                model = model.ToList();
                            }
                        }
                        var model_join = from dg in model
                                         select new VMDinhGiaTroGiaTroCuoc
                                         {
                                             Id = dg.Id,
                                             Madv = dg.Madv,
                                             Macqcq = Madv,
                                             Macqcq_t = dg.Macqcq_t,
                                             Macqcq_h = dg.Macqcq_h,
                                             Mahs = dg.Mahs,
                                             Thoidiem = dg.Thoidiem_h,
                                             Soqd = dg.Soqd,
                                             Madiaban = getdonvi.MaDiaBan,
                                             Trangthai_ad = dg.Trangthai_ad,
                                             Trangthai_t = dg.Trangthai_t,
                                             Trangthai_h = dg.Trangthai_h,
                                             Thongtin = dg.Thongtin,
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

                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                        ViewData["Madv"] = Madv;
                        ViewData["Nam"] = Nam;
                        ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                        ViewData["Title"] = "Hoàn thành định giá Mức trợ giá trợ cước";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgtgtc";
                        ViewData["MenuLv3"] = "menu_dgtgtc_ht";
                        return View("Views/Admin/Manages/DinhGia/GiaTroGiaTroCuoc/HoanThanh/Index.cshtml", model_join);
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.tgtc.ttg", "Approve"))
                {
                    var model = _db.GiaTroGiaTroCuoc.FirstOrDefault(p => p.Mahs == mahs_complete);

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
                    model.Thoidiem = DateTime.Now;
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
                    _db.GiaTroGiaTroCuoc.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "TroGiaTroCuoc", new { Madv = model.Madv, Nam = model.Thoidiem.Year });
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.tgtc.ttg", "Approve"))
                {
                    var model = _db.GiaTroGiaTroCuoc.FirstOrDefault(p => p.Mahs == mahs_complete);
                   
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
                    _db.GiaTroGiaTroCuoc.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "TroGiaTroCuocHt", new {Madv =  madv_hientai, Nam = model.Thoidiem.Year });
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.tgtc.ttg", "Approve"))
                {
                    var model = _db.GiaTroGiaTroCuoc.FirstOrDefault(t => t.Id == id_tralai);

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

                    _db.GiaTroGiaTroCuoc.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "TroGiaTroCuocHt", new { Madv = madv_tralai, Nam = model.Thoidiem.Year });
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

        public IActionResult CongBo(string mahs_cb )
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.tgtc.htg", "Approve"))
                {
                    var model = _db.GiaTroGiaTroCuoc.FirstOrDefault(t => t.Mahs == mahs_cb);

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

                    _db.GiaTroGiaTroCuoc.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "TroGiaTroCuocHt");
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

        public IActionResult HuyCongBo(string mahs_hcb )
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.tgtc.htg", "Approve"))
                {
                    var model = _db.GiaTroGiaTroCuoc.FirstOrDefault(t => t.Mahs == mahs_hcb);

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

                    _db.GiaTroGiaTroCuoc.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "TroGiaTroCuocHt");
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
