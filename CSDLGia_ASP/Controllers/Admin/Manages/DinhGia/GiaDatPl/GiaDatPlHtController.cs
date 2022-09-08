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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDatPl
{
    public class GiaDatPlHtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaDatPlHtController(CSDLGiaDBContext db)
        {
            _db = db;
        }
        
        [Route("HoanThanhDinhGiaDatCuThe")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.datcuthe.xetduyet", "Index"))
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

                    if (getdonvi.Level == "H")
                    {
                        var model = _db.GiaDatPhanLoai.Where(t => t.Madv_h == Madv && t.Thoidiem_h.Year == int.Parse(Nam)
                                                    && (t.Trangthai != "HT" || t.Trangthai != "BTL")).ToList();

                        var model_join = from dg in model
                                          join dn in _db.Company on dg.Madv equals dn.Madv
                                          join dnct in _db.CompanyLvCc.Where(t => t.Manghe == "DCT") on dn.Mahs equals dnct.Mahs
                                          select new VMDinhGiaDat
                                          {
                                              Id = dg.Id,
                                              Macqcq = Madv,
                                              Madiaban=dg.Madiaban,
                                              Mahs = dg.Mahs,
                                              Thoidiem = dg.Thoidiem,
                                              Soqd = dg.Soqd,
                                              TenDv=getdonvi.TenDv,
                                              TenDiaBan=getdonvi.TenDiaBan,
                                              Trangthai = dg.Trangthai,
                                              Thongtin=dg.Thongtin,
                                              Trangthai_ad = dg.Trangthai_ad,
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
                        ViewData["Madv"] = Madv;
                        ViewData["Nam"] = Nam;
                        ViewData["Title"] = "Hoàn thành định giá đất cụ thể";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgdct";
                        ViewData["MenuLv3"] = "menu_dgdct_ht";
                        return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/HoanThanh/Index.cshtml", model_join);
                    }
                    else if (getdonvi.Level == "T")
                    {
                        var model = _db.GiaDatPhanLoai.Where(t => t.Madv_t == Madv && t.Thoidiem_t.Year == int.Parse(Nam)
                                                        && (t.Trangthai != "HT" || t.Trangthai != "BTL")).ToList();

                        var model_join = from dg in model
                                          join dn in _db.Company on dg.Madv equals dn.Madv
                                          join dnct in _db.CompanyLvCc.Where(t => t.Manghe == "DCT") on dn.Mahs equals dnct.Mahs
                                          select new VMDinhGiaDat
                                          {
                                              Id = dg.Id,
                                              Macqcq = Madv,
                                              Mahs = dg.Mahs,
                                              Madiaban = dg.Madiaban,
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
                            ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.MaDv == Madv);
                        }
                        ViewData["DsDiaBan"] = dsdiaban;
                        ViewData["Madv"] = Madv;
                        ViewData["Nam"] = Nam;
                        ViewData["Title"] = "Hoàn thành định giá đất cụ thể";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgdct";
                        ViewData["MenuLv3"] = "menu_dgdct_ht";
                        return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/HoanThanh/Index.cshtml", model_join);
                    }
                    else
                    {
                        var model = _db.GiaDatPhanLoai.Where(t => t.Madv_ad == Madv && t.Thoidiem_ad.Year == int.Parse(Nam)
                                                         && (t.Trangthai != "HT" || t.Trangthai != "BTL")).ToList();

                        var model_join = from dg in model
                                          join dn in _db.Company on dg.Madv equals dn.Madv
                                          join dnct in _db.CompanyLvCc.Where(t => t.Manghe == "DCT") on dn.Mahs equals dnct.Mahs
                                          select new VMDinhGiaDat
                                          {
                                              Id = dg.Id,
                                              Macqcq = Madv,
                                              Madiaban = dg.Madiaban,
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
                            ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.MaDv == Madv);
                        }
                        ViewData["DsDiaBan"] = dsdiaban;
                        ViewData["Madv"] = Madv;
                        ViewData["Nam"] = Nam;
                        ViewData["Title"] = "Hoàn thành định giá đất cụ thể";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgdct";
                        ViewData["MenuLv3"] = "menu_dgdct_ht";
                        return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/HoanThanh/Index.cshtml", model_join);
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.datcuthe.thongtin", "Approve"))
                {
                    var model = _db.GiaDatPhanLoai.FirstOrDefault(p => p.Mahs == mahs_complete);

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
                    _db.GiaDatPhanLoai.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "GiaDatPl", new { model.Mahs, Nam = model.Thoidiem.Year });
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.datcuthe.thongtin", "Approve"))
                {
                    var model = _db.GiaDatPhanLoai.FirstOrDefault(t => t.Id == id_tralai);

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

                    _db.GiaDatPhanLoai.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaDatPlHt", new { Madv = madv_tralai, Nam = model.Thoidiem.Year });
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.datcuthe.xetduyet", "Approve"))
                {
                    var model = _db.GiaDatPhanLoai.FirstOrDefault(t => t.Mahs == mahs_cb);

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

                    _db.GiaDatPhanLoai.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaDatPlHt");
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.datcuthe.xetduyet", "Approve"))
                {
                    var model = _db.GiaDatPhanLoai.FirstOrDefault(t => t.Mahs == mahs_hcb);

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

                    _db.GiaDatPhanLoai.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaDatPlHt");
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
