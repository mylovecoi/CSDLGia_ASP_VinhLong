using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaGiaoDichDBDS
{ 
    public class GiaVatLieuXayDungCongBoController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaVatLieuXayDungCongBoController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaVatLieuXayDung/CongBo")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.giavatlieuxaydung.xetduyet", "Index"))
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
                                    }).FirstOrDefault();

                    var model = _db.GiaVatLieuXayDung.ToList();



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
                                         select new CSDLGia_ASP.Models.Manages.DinhGia.GiaVatLieuXayDung
                                         {
                                             Id = kk.Id,
                                             Mahs = kk.Mahs,
                                             MadvCh = GetMadvChuyen(Madv, kk),
                                             Macqcq = Madv,
                                             Madv = kk.Madv_h,
                                             Thoidiem = kk.Thoidiem_h,
                                             Cqbh = kk.Cqbh,
                                             Trangthai = kk.Trangthai_h,
                                             Soqd = kk.Soqd,
                                             Level = getdonvi.Level,
                                             Ipf1 = kk.Ipf1,
                                         });

                        var model_join = (from kkj in model_new
                                          join dv in dsdonvi on kkj.MadvCh equals dv.MaDv
                                          select new CSDLGia_ASP.Models.Manages.DinhGia.GiaVatLieuXayDung
                                          {
                                              Id = kkj.Id,
                                              Mahs = kkj.Mahs,
                                              MadvCh = kkj.MadvCh,
                                              TendvCh = dv.TenDv,
                                              Macqcq = kkj.Macqcq,
                                              Madv = kkj.Madv,
                                              Thoidiem = kkj.Thoidiem,
                                              Cqbh = kkj.Cqbh,
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
                            ViewData["DsDonVi"] = dsdonvi.Where(t => t.MaDv == Madv);
                        }
                        ViewData["DsDiaBan"] = dsdiaban;
                        ViewData["Madv"] = Madv;
                        ViewData["Nam"] = Nam;
                        ViewData["Title"] = "Thông tin hồ sơ giá vật liệu xây dựng";
                        ViewData["MenuLv1"] = "menu_giakhac";
                        ViewData["MenuLv2"] = "menu_giakhac_giavatlieuxaydung";
                        ViewData["MenuLv3"] = "menu_giakhac_giavatlieuxaydung_xd";
                        ViewBag.bSession = true;
                        return View("Views/Admin/Systems/CongBo/GiaVatLieuXayDungCongBo.cshtml", model_join);
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
                                         select new CSDLGia_ASP.Models.Manages.DinhGia.GiaVatLieuXayDung
                                         {
                                             Id = kk.Id,
                                             Mahs = kk.Mahs,
                                             MadvCh = GetMadvChuyen(Madv, kk),
                                             Macqcq = Madv,
                                             Madv = kk.Madv_t,
                                             Thoidiem = kk.Thoidiem_t,
                                             Cqbh = kk.Cqbh,
                                             Trangthai = kk.Trangthai_t,
                                             Soqd = kk.Soqd,
                                             Level = getdonvi.Level,
                                             Ipf1 = kk.Ipf1,
                                         });

                        var model_join = (from kkj in model_new
                                          join dv in dsdonvi on kkj.MadvCh equals dv.MaDv
                                          select new CSDLGia_ASP.Models.Manages.DinhGia.GiaVatLieuXayDung
                                          {
                                              Id = kkj.Id,
                                              Mahs = kkj.Mahs,
                                              MadvCh = kkj.MadvCh,
                                              TendvCh = dv.TenDv,
                                              Macqcq = kkj.Macqcq,
                                              Madv = kkj.Madv,
                                              Thoidiem = kkj.Thoidiem,
                                              Cqbh = kkj.Cqbh,
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
                            ViewData["DsDonVi"] = dsdonvi.Where(t => t.MaDv == Madv);
                        }
                        ViewData["DsDiaBan"] = dsdiaban;
                        ViewData["Madv"] = Madv;
                        ViewData["Nam"] = Nam;
                        ViewData["Title"] = "Thông tin hồ sơ giá vật liệu xây dựng";
                        ViewData["MenuLv1"] = "menu_giakhac";
                        ViewData["MenuLv2"] = "menu_giakhac_giavatlieuxaydung";
                        ViewData["MenuLv3"] = "menu_giakhac_giavatlieuxaydung_xd";
                        ViewBag.bSession = true;
                        return View("Views/Admin/Systems/CongBo/GiaVatLieuXayDungCongBo.cshtml", model_join);
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
                                         select new CSDLGia_ASP.Models.Manages.DinhGia.GiaVatLieuXayDung
                                         {
                                             Id = kk.Id,
                                             Mahs = kk.Mahs,
                                             MadvCh = GetMadvChuyen(Madv, kk),
                                             Macqcq = Madv,
                                             Madv = kk.Madv_ad,
                                             Thoidiem = kk.Thoidiem_ad,
                                             Cqbh = kk.Cqbh,
                                             Trangthai = kk.Trangthai_ad,
                                             Soqd = kk.Soqd,
                                             Level = getdonvi.Level,
                                             Ipf1 = kk.Ipf1,
                                         });

                        var model_join = (from kkj in model_new
                                          join dv in dsdonvi on kkj.MadvCh equals dv.MaDv
                                          select new CSDLGia_ASP.Models.Manages.DinhGia.GiaVatLieuXayDung
                                          {
                                              Id = kkj.Id,
                                              Mahs = kkj.Mahs,
                                              MadvCh = kkj.MadvCh,
                                              TendvCh = dv.TenDv,
                                              Macqcq = kkj.Macqcq,
                                              Madv = kkj.Madv,
                                              Thoidiem = kkj.Thoidiem,
                                              Cqbh = kkj.Cqbh,
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
                            ViewData["DsDonVi"] = dsdonvi.Where(t => t.MaDv == Madv);
                        }
                        ViewData["DsDiaBan"] = dsdiaban;
                        ViewData["Madv"] = Madv;
                        ViewData["Nam"] = Nam;
                        ViewData["Title"] = "Thông tin hồ sơ giá vật liệu xây dựng";
                        ViewData["MenuLv1"] = "menu_giakhac";
                        ViewData["MenuLv2"] = "menu_giakhac_giavatlieuxaydung";
                        ViewData["MenuLv3"] = "menu_giakhac_giavatlieuxaydung_xd";
                        ViewBag.bSession = true;
                        return View("Views/Admin/Systems/CongBo/GiaVatLieuXayDungCongBo.cshtml", model_join);
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

        private static string GetMadvChuyen(string macqcq, CSDLGia_ASP.Models.Manages.DinhGia.GiaVatLieuXayDung hoso)
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

