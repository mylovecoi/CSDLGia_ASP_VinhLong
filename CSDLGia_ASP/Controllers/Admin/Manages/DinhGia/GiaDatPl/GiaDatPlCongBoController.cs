using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDatPlCongBo
{
    public class GiaDatPlCongBoController : Controller
    {
      

        private readonly CSDLGiaDBContext _db;

        public GiaDatPlCongBoController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaDatPlCongBo/CongBo")]
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

                    var model = _db.GiaDatPhanLoai.ToList();

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
                                         select new CSDLGia_ASP.ViewModels.Manages.DinhGia.VMDinhGiaDat
                                         {
                                             Id = kk.Id,
                                             Mahs = kk.Mahs,
                                             MadvCh = GetMadvChuyen(Madv, kk),
                                             Macqcq = Madv,
                                             Madv = kk.Madv_h,
                                             Thoidiem = kk.Thoidiem_h,
                                             Thongtin = kk.Thongtin,
                                             Trangthai = kk.Trangthai_h,
                                             Soqd = kk.Soqd,
                                             Level = getdonvi.Level,
                                             Ipf1 = kk.Ipf1,
                                         });

                        var model_join = (from kkj in model_new
                                          join dv in dsdonvi on kkj.MadvCh equals dv.MaDv
                                          select new CSDLGia_ASP.ViewModels.Manages.DinhGia.VMDinhGiaDat
                                          {
                                              Id = kkj.Id,
                                              Mahs = kkj.Mahs,
                                              MadvCh = kkj.MadvCh,
                                              TendvCh = dv.TenDv,
                                              Macqcq = kkj.Macqcq,
                                              Madv = kkj.Madv,
                                              Thoidiem = kkj.Thoidiem,
                                              Thongtin = kkj.Thongtin,
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
                        ViewData["Title"] = "Xét duyệt giá đất cụ thể";
                        ViewData["MenuLv1"] = "menu_giadat";
                        ViewData["MenuLv2"] = "menu_dgdct";
                        ViewData["MenuLv3"] = "menu_dgdct_ht";
                        ViewBag.bSession = true;
                        return View("Views/Admin/Systems/CongBo/GiaDatPlCongBo.cshtml", model_join);
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
                                         select new CSDLGia_ASP.ViewModels.Manages.DinhGia.VMDinhGiaDat
                                         {
                                             Id = kk.Id,
                                             Mahs = kk.Mahs,
                                             MadvCh = GetMadvChuyen(Madv, kk),
                                             Macqcq = Madv,
                                             Madv = kk.Madv_t,
                                             Thoidiem = kk.Thoidiem_t,
                                             Thongtin = kk.Thongtin,
                                             Trangthai = kk.Trangthai_t,
                                             Soqd = kk.Soqd,
                                             Level = getdonvi.Level,
                                             Ipf1 = kk.Ipf1,
                                         });

                        var model_join = (from kkj in model_new
                                          join dv in dsdonvi on kkj.MadvCh equals dv.MaDv
                                          select new CSDLGia_ASP.ViewModels.Manages.DinhGia.VMDinhGiaDat
                                          {
                                              Id = kkj.Id,
                                              Mahs = kkj.Mahs,
                                              MadvCh = kkj.MadvCh,
                                              TendvCh = dv.TenDv,
                                              Macqcq = kkj.Macqcq,
                                              Madv = kkj.Madv,
                                              Thoidiem = kkj.Thoidiem,
                                              Thongtin = kkj.Thongtin,
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
                        ViewData["Title"] = "Xét duyệt giá đất cụ thể";
                        ViewData["MenuLv1"] = "menu_giadat";
                        ViewData["MenuLv2"] = "menu_dgdct";
                        ViewData["MenuLv3"] = "menu_dgdct_ht";
                        ViewBag.bSession = true;
                        return View("Views/Admin/Systems/CongBo/GiaDatPlCongBo.cshtml", model_join);
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
                                         select new CSDLGia_ASP.ViewModels.Manages.DinhGia.VMDinhGiaDat
                                         {
                                             Id = kk.Id,
                                             Mahs = kk.Mahs,
                                             MadvCh = GetMadvChuyen(Madv, kk),
                                             Macqcq = Madv,
                                             Madv = kk.Trangthai,
                                             Thoidiem = kk.Thoidiem,
                                             Thongtin = kk.Thongtin,
                                             Trangthai = kk.Trangthai,
                                             Soqd = kk.Soqd,
                                             Level = getdonvi.Level,
                                             Ipf1 = kk.Ipf1,
                                         });

                        var model_join = (from kkj in model_new
                                          join dv in dsdonvi on kkj.MadvCh equals dv.MaDv
                                          select new CSDLGia_ASP.ViewModels.Manages.DinhGia.VMDinhGiaDat
                                          {
                                              Id = kkj.Id,
                                              Mahs = kkj.Mahs,
                                              MadvCh = kkj.MadvCh,
                                              TendvCh = dv.TenDv,
                                              Macqcq = kkj.Macqcq,
                                              Madv = kkj.Madv,
                                              Thoidiem = kkj.Thoidiem,
                                              Thongtin = kkj.Thongtin,
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
                        ViewData["Title"] = "Xét duyệt giá đất cụ thể";
                        ViewData["MenuLv1"] = "menu_giadat";
                        ViewData["MenuLv2"] = "menu_dgdct";
                        ViewData["MenuLv3"] = "menu_dgdct_ht";
                        ViewBag.bSession = true;
                        return View("Views/Admin/Systems/CongBo/GiaDatPlCongBo.cshtml", model_join);
                      
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

        private static string GetMadvChuyen(string macqcq, CSDLGia_ASP.Models.Manages.DinhGia.GiaDatPhanLoai hoso)
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

