using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.ViewModels.Manages.KeKhaiDkg;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.KeKhaiDkg
{
    public class KkMhBogCongBoController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public KkMhBogCongBoController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("BinhOnGia/CongBo")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.bog.xetduyet", "Index"))
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

                    var model = _db.KkMhBog.Where(t => t.Trangthai != "CC" || t.Trangthai != "BTL").ToList();


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
                                model = model.Where(t => t.Ngaychuyen_h.Year == int.Parse(Nam)).ToList();
                            }
                            else
                            {
                                model = model.Where(t => t.Madv_h == Madv).ToList();
                            }
                        }

                        var model_join = (from kk in model
                                          join dn in _db.Company on kk.Madv equals dn.Madv
                                          join nghe in _db.DmNgheKd on kk.Manghe equals nghe.Manghe
                                          join db in _db.DsDiaBan on dn.Madiaban equals db.MaDiaBan
                                          select new VMKkMhBog
                                          {
                                              Id = kk.Id,
                                              Tendn = dn.Tendn,
                                              Macqcq = Madv,
                                              Madv = kk.Madv_h,
                                              Tennghe = nghe.Tennghe,
                                              Tendb = db.TenDiaBan,
                                              Phanloai = kk.Phanloai,
                                              Mahs = kk.Mahs,
                                              Thoidiem = kk.Thoidiem,
                                              Ngaychuyen = kk.Ngaychuyen_h,
                                              Nguoichuyen = kk.Nguoichuyen,
                                              Ngaycvlk = kk.Ngaycvlk,
                                              Ngaynhan = kk.Ngaynhan_h,
                                              Ngayhieuluc = kk.Ngayhieuluc,
                                              Socv = kk.Socv,
                                              Dtll = kk.Dtll,
                                              Trangthai = kk.Trangthai_h,
                                              Level = getdonvi.Level,
                                          });

                        /*return Ok(model_join);*/
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
                        ViewData["Title"] = "Thông tin hồ sơ kê khai bình ổn giá";
                        ViewData["MenuLv1"] = "menu_bog";
                        ViewData["MenuLv2"] = "menu_ttdntdxdbog";
                        ViewBag.bSession = true;
                        return View("Views/Admin/Systems/CongBo/GiaMatHangBOGCongBo.cshtml", model_join);
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
                                model = model.Where(t => t.Ngaychuyen_t.Year == int.Parse(Nam)).ToList();
                            }
                            else
                            {
                                model = model.Where(t => t.Madv_t == Madv).ToList();
                            }
                        }

                        var model_join = from kk in model
                                         join dn in _db.Company on kk.Madv equals dn.Madv
                                         join nghe in _db.DmNgheKd on kk.Manghe equals nghe.Manghe
                                         join db in _db.DsDiaBan on dn.Madiaban equals db.MaDiaBan
                                         select new VMKkMhBog
                                         {
                                             Id = kk.Id,
                                             Tendn = dn.Tendn,
                                             Macqcq = Madv,
                                             Madv = kk.Madv_t,
                                             Madiaban = kk.Madv,
                                             Manghe = kk.Manghe,
                                             Tennghe = nghe.Tennghe,
                                             Tendb = db.TenDiaBan,
                                             Phanloai = kk.Phanloai,
                                             Mahs = kk.Mahs,
                                             Thoidiem = kk.Thoidiem,
                                             Ngaychuyen = kk.Ngaychuyen_t,
                                             Nguoichuyen = kk.Nguoichuyen,
                                             Ngaycvlk = kk.Ngaycvlk,
                                             Ngaynhan = kk.Ngaynhan_t,
                                             Ngayhieuluc = kk.Ngayhieuluc,
                                             Socv = kk.Socv,
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
                        ViewData["Madv"] = Madv;
                        ViewData["Nam"] = Nam;
                        ViewData["Title"] = "Thông tin hồ sơ kê khai bình ổn giá";
                        ViewData["MenuLv1"] = "menu_bog";
                        ViewData["MenuLv2"] = "menu_ttdntdxdbog";
                        ViewBag.bSession = true;
                        return View("Views/Admin/Systems/CongBo/GiaMatHangBOGCongBo.cshtml", model_join);
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
                                model = model.Where(t => t.Ngaychuyen_ad.Year == int.Parse(Nam)).ToList();
                            }
                            else
                            {
                                model = model.Where(t => t.Madv_ad == Madv).ToList();
                            }
                        }

                        var model_join = from kk in model
                                         join dn in _db.Company on kk.Madv equals dn.Madv
                                         join nghe in _db.DmNgheKd on kk.Manghe equals nghe.Manghe
                                         join db in _db.DsDiaBan on dn.Madiaban equals db.MaDiaBan
                                         select new VMKkMhBog
                                         {
                                             Id = kk.Id,
                                             Tendn = dn.Tendn,
                                             Macqcq = Madv,
                                             Madv = kk.Madv_ad,
                                             Madiaban = kk.Madv,
                                             Manghe = kk.Manghe,
                                             Tennghe = nghe.Tennghe,
                                             Tendb = db.TenDiaBan,
                                             Phanloai = kk.Phanloai,
                                             Mahs = kk.Mahs,
                                             Thoidiem = kk.Thoidiem,
                                             Ngaychuyen = kk.Ngaychuyen_ad,
                                             Nguoichuyen = kk.Nguoichuyen,
                                             Ngaycvlk = kk.Ngaycvlk,
                                             Ngaynhan = kk.Ngaynhan_ad,
                                             Ngayhieuluc = kk.Ngayhieuluc,
                                             Socv = kk.Socv,
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
                        ViewData["Madv"] = Madv;
                        ViewData["Nam"] = Nam;
                        ViewData["Title"] = "Thông tin hồ sơ kê khai bình ổn giá";
                        ViewData["MenuLv1"] = "menu_bog";
                        ViewData["MenuLv2"] = "menu_ttdntdxdbog";
                        ViewBag.bSession = true;
                        return View("Views/Admin/Systems/CongBo/GiaMatHangBOGCongBo.cshtml", model_join);
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


    }
}
