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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.Giathuemuanhaxh
{
    public class GiathuemuanhaxhHtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiathuemuanhaxhHtController(CSDLGiaDBContext db)
        {
            _db = db;
        }
        
        [Route("HoanThanhDinhGiaThueMuaNhaXh")]
        [HttpGet]
        public IActionResult Index(string Donvi, string Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanha.xetduyet", "Index"))
                {
                    var dsdonvi = _db.DsDonVi;
                    var dsdiaban = _db.DsDiaBan;

                    if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") != null)
                    {
                        Donvi = Helpers.GetSsAdmin(HttpContext.Session, "Madv");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(Donvi))
                        {
                            Donvi = dsdonvi.OrderBy(t => t.Id).Select(t => t.MaDv).First();
                        }
                    }

                    if (string.IsNullOrEmpty(Nam))
                    {
                        Nam = Helpers.ConvertYearToStr(DateTime.Now.Year);
                    }
                    var getdonvi = (from dv in dsdonvi.Where(t => t.MaDv == Donvi)
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
                    
                    if (getdonvi.Level == "ADMIN")
                    {
                        var model = _db.GiaThueMuaNhaXh.Where(t => t.Madv_ad == Donvi && t.Thoidiem_ad.Year == int.Parse(Nam)
                                                         && (t.Trangthai != "HT" || t.Trangthai != "BTL")).ToList();

                        var model_join = from dg in model
                                         join dgct in _db.GiaThueMuaNhaXhCt on dg.Mahs equals dgct.Mahs
                                         select new VMDinhGiaThueMuaNhaXh
                                         {
                                             Id = dg.Id,
                                             //Macqcq = Donvi,
                                             Mahs = dg.Mahs,
                                             Thoidiem = dg.Thoidiem,
                                             Soqd = dg.Soqd,
                                             TenDv = getdonvi.TenDv,
                                             TenDiaBan = getdonvi.TenDiaBan,
                                             Trangthai = dg.Trangthai,
                                             Thongtin = dg.Thongtin,
                                             Trangthai_ad = dg.Trangthai_ad,
                                             Level = getdonvi.Level,
                                         };
                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                        {
                            ViewData["DsDonVi"] = dsdonvi;
                        }
                        else
                        {
                            ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.MaDv == Donvi);
                        }
                        //ViewData["DsDiaBan"] = dsdiaban;
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                        ViewData["Madv"] = Donvi;
                        ViewData["Nam"] = Nam;
                        ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                        ViewData["Title"] = "Hoàn thành định giá thuê mua nhà ở";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgtmnxh";
                        ViewData["MenuLv3"] = "menu_dgtmnxh_ht";
                        return View("Views/Admin/Manages/DinhGia/GiaThueMuaNhaXh/HoanThanh/Index.cshtml", model_join);
                    }
                    else
                    {
                        var model = _db.GiaThueMuaNhaXh.Where(t => t.Madv_t == Donvi && t.Thoidiem_t.Year == int.Parse(Nam)
                                                        && (t.Trangthai != "HT" || t.Trangthai != "BTL")).ToList();

                        var model_join = from dg in model
                                         join dgct in _db.GiaThueMuaNhaXhCt on dg.Mahs equals dgct.Mahs
                                         join dv in _db.DsDonVi on dg.Macqcq equals dv.MaDv
                                         select new VMDinhGiaThueMuaNhaXh
                                         {
                                             Id = dg.Id,
                                             Macqcq = Donvi,
                                             Mahs = dg.Mahs,
                                             Thoidiem = dg.Thoidiem,
                                             Soqd = dg.Soqd,
                                             TenDv = getdonvi.TenDv,
                                             TenDiaBan = getdonvi.TenDiaBan,
                                             Trangthai = dg.Trangthai,
                                             Thongtin = dg.Thongtin,
                                             Trangthai_ad = dg.Trangthai_ad,
                                             Level = getdonvi.Level,
                                         };
                        /*if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                        {
                            ViewData["DsDonVi"] = dsdonvi;
                        }
                        else
                        {
                            ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.MaDv == Donvi);
                        }*/
                        //ViewData["DsDiaBan"] = dsdiaban;
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                        ViewData["Madv"] = Donvi;
                        ViewData["Nam"] = Nam;
                        ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                        ViewData["Title"] = "Hoàn thành định giá thuê mua nhà ở";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgtmnxh";
                        ViewData["MenuLv3"] = "menu_dgtmnxh_ht";
                        return View("Views/Admin/Manages/DinhGia/GiaThueMuaNhaXh/HoanThanh/Index.cshtml", model_join);
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

        public IActionResult Complete(string mahs_complete,string Macqcq)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanha.thongtin", "Approve"))
                {
                    var model = _db.GiaThueMuaNhaXh.FirstOrDefault(p => p.Mahs == mahs_complete);

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
                        //model.Macqcq_t = chk_dvcq.MaDiaBan;
                        //model.Madv_t= chk_dvcq.MaDiaBan; 
                        //model.Trangthai_ad = "CCB";
                    }
                    else if (chk_dvcq != null && chk_dvcq.Level == "ADMIN")
                    {
                        model.Madv_ad = Macqcq;
                        model.Thoidiem_ad = DateTime.Now;
                        model.Trangthai_ad = "CCB";
                        //model.Macqcq_ad = chk_dvcq.MaDiaBan;
                        //model.Madv_ad= chk_dvcq.MaDiaBan;
                    }
                    else
                    {
                        model.Madv_h = Macqcq;
                        model.Thoidiem_h = DateTime.Now;
                        model.Trangthai_h = "CCB";
                        //model.Trangthai_ad = "CCB";
                        //model.Macqcq_h = chk_dvcq.MaDiaBan;
                        //model.Madv_h= chk_dvcq.MaDiaBan;
                    }
                    _db.GiaThueMuaNhaXh.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Giathuemuanhaxh", new { model.Mahs, Nam = model.Thoidiem.Year });
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanha.thongtin", "Approve"))
                {
                    var model = _db.GiaThueMuaNhaXh.FirstOrDefault(t => t.Id == id_tralai);

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
                        model.Thoidiem_h= DateTime.MinValue;
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

                    return RedirectToAction("Index", "GiathuemuanhaxhHt", new { Madv = madv_tralai, Nam = model.Thoidiem.Year });
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanha.xetduyet", "Approve"))
                {
                    var model = _db.GiaThueMuaNhaXh.FirstOrDefault(t => t.Mahs == mahs_cb);

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

                    _db.GiaThueMuaNhaXh.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiathuemuanhaxhHt");
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanha.xetduyet", "Approve"))
                {
                    var model = _db.GiaThueMuaNhaXh.FirstOrDefault(t => t.Mahs == mahs_hcb);

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

                    _db.GiaThueMuaNhaXh.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiathuemuanhaxhHt");
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
