using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.ThamDinhGia
{
    public class ThamDinhGiaXdController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ThamDinhGiaXdController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("ThamDinhGia/XetDuyet")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.xd", "Index"))
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

                    var model = _db.ThamDinhGia.ToList();

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
                                         select new CSDLGia_ASP.Models.Manages.ThamDinhGia.ThamDinhGia
                                         {
                                             Id = kk.Id,
                                             Mahs = kk.Mahs,
                                             MadvCh = GetMadvChuyen(Madv, kk),
                                             Macqcq = Madv,
                                             Madiaban = kk.Madiaban,
                                             Madv = kk.Madv_h,
                                             Thoidiem = kk.Thoidiem_h,
                                             Trangthai = kk.Trangthai_h,
                                             Diadiem = kk.Diadiem,
                                             Dvyeucau = kk.Dvyeucau,
                                             Sotbkl = kk.Sotbkl,
                                             Hosotdgia = kk.Hosotdgia,
                                             Songaykq = kk.Songaykq,
                                             Tttstd = kk.Tttstd,
                                             Level = getdonvi.Level,
                                         });

                        var model_join = (from kkj in model_new
                                          join dv in dsdonvi on kkj.MadvCh equals dv.MaDv                                         
                                          select new CSDLGia_ASP.Models.Manages.ThamDinhGia.ThamDinhGia
                                          {
                                              Id = kkj.Id,
                                              Mahs = kkj.Mahs,
                                              MadvCh = kkj.MadvCh,
                                              TendvCh = dv.TenDv,
                                              Macqcq = kkj.Macqcq,
                                              Madiaban = kkj.Madiaban,
                                              Madv = kkj.Madv,
                                              Thoidiem = kkj.Thoidiem,
                                              Trangthai = kkj.Trangthai,
                                              Diadiem = kkj.Diadiem,
                                              Dvyeucau = kkj.Dvyeucau,
                                              Sotbkl = kkj.Sotbkl,
                                              Hosotdgia = kkj.Hosotdgia,
                                              Songaykq = kkj.Songaykq,
                                              Tttstd = kkj.Tttstd,
                                              Level = kkj.Level,
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
                        ViewData["Title"] = "Thông tin hồ sơ thẩm định giá";
                        ViewData["MenuLv1"] = "menu_tdg";
                        ViewData["MenuLv2"] = "menu_tdg_xd";
                        ViewData["maKetNoiAPI"] = "thamdinhgia";
                        return View("Views/Admin/Manages/ThamDinhGia/XetDuyet/Index.cshtml", model_join);
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
                                         select new CSDLGia_ASP.Models.Manages.ThamDinhGia.ThamDinhGia
                                         {
                                             Id = kk.Id,
                                             Mahs = kk.Mahs,
                                             MadvCh = GetMadvChuyen(Madv, kk),
                                             Macqcq = Madv,
                                             Madiaban = kk.Madiaban,
                                             Madv = kk.Madv_t,
                                             Thoidiem = kk.Thoidiem_t,
                                             Trangthai = kk.Trangthai_t,
                                             Diadiem = kk.Diadiem,
                                             Dvyeucau = kk.Dvyeucau,
                                             Sotbkl = kk.Sotbkl,
                                             Hosotdgia = kk.Hosotdgia,
                                             Songaykq = kk.Songaykq,
                                             Tttstd = kk.Tttstd,
                                             Level = getdonvi.Level,
                                         });

                        var model_join = (from kkj in model_new
                                          join dv in dsdonvi on kkj.MadvCh equals dv.MaDv
                                          select new CSDLGia_ASP.Models.Manages.ThamDinhGia.ThamDinhGia
                                          {
                                              Id = kkj.Id,
                                              Mahs = kkj.Mahs,
                                              MadvCh = kkj.MadvCh,
                                              TendvCh = dv.TenDv,
                                              Macqcq = kkj.Macqcq,
                                              Madiaban = kkj.Madiaban,                                             
                                              Madv = kkj.Madv,
                                              Thoidiem = kkj.Thoidiem,
                                              Trangthai = kkj.Trangthai,
                                              Diadiem = kkj.Diadiem,
                                              Dvyeucau = kkj.Dvyeucau,
                                              Sotbkl = kkj.Sotbkl,
                                              Hosotdgia = kkj.Hosotdgia,
                                              Songaykq = kkj.Songaykq,
                                              Tttstd = kkj.Tttstd,
                                              Level = kkj.Level,
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
                        ViewData["Title"] = "Thông tin hồ sơ thẩm định giá";
                        ViewData["MenuLv1"] = "menu_tdg";
                        ViewData["MenuLv2"] = "menu_tdg_xd";
                        ViewData["maKetNoiAPI"] = "thamdinhgia";
                        return View("Views/Admin/Manages/ThamDinhGia/XetDuyet/Index.cshtml", model_join);
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
                                         select new CSDLGia_ASP.Models.Manages.ThamDinhGia.ThamDinhGia
                                         {
                                             Id = kk.Id,
                                             Mahs = kk.Mahs,
                                             MadvCh = GetMadvChuyen(Madv, kk),
                                             Macqcq = Madv,
                                             Madiaban = kk.Madiaban,
                                             Madv = kk.Madv_ad,
                                             Thoidiem = kk.Thoidiem_ad,
                                             Trangthai = kk.Trangthai,
                                             Diadiem = kk.Diadiem,
                                             Dvyeucau = kk.Dvyeucau,
                                             Sotbkl = kk.Sotbkl,
                                             Hosotdgia = kk.Hosotdgia,
                                             Songaykq = kk.Songaykq,
                                             Tttstd = kk.Tttstd,
                                             Level = getdonvi.Level,
                                         });

                        /*return Ok(model_new);*/

                        var model_join = (from kkj in model_new
                                          join dv in dsdonvi on kkj.MadvCh equals dv.MaDv                                          
                                          select new CSDLGia_ASP.Models.Manages.ThamDinhGia.ThamDinhGia
                                          {
                                              Id = kkj.Id,
                                              Mahs = kkj.Mahs,
                                              MadvCh = kkj.MadvCh,
                                              TendvCh = dv.TenDv,
                                              Macqcq = kkj.Macqcq,
                                              Madiaban = kkj.Madiaban,                                             
                                              Madv = kkj.Madv,
                                              Thoidiem = kkj.Thoidiem,
                                              Trangthai = kkj.Trangthai,
                                              Diadiem = kkj.Diadiem,
                                              Dvyeucau = kkj.Dvyeucau,
                                              Sotbkl = kkj.Sotbkl,
                                              Hosotdgia = kkj.Hosotdgia,
                                              Songaykq = kkj.Songaykq,
                                              Tttstd = kkj.Tttstd,
                                              Level = kkj.Level,
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
                        ViewData["Title"] = "Thông tin hồ sơ thẩm định giá";
                        ViewData["MenuLv1"] = "menu_tdg";
                        ViewData["MenuLv2"] = "menu_tdg_xd";
                        ViewData["maKetNoiAPI"] = "thamdinhgia";
                        return View("Views/Admin/Manages/ThamDinhGia/XetDuyet/Index.cshtml", model_join);
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

        public IActionResult TraLai(int id_tralai, string madv_tralai)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.xd", "Approve"))
                {
                    var model = _db.ThamDinhGia.FirstOrDefault(t => t.Id == id_tralai);

                    //Gán trạng thái của đơn vị chuyển hồ sơ
                    if (madv_tralai == model.Macqcq)
                    {
                        model.Macqcq = null;
                        model.Trangthai = "HHT";
                    }

                    if (madv_tralai == model.Macqcq_h)
                    {
                        model.Macqcq_h = null;
                        model.Trangthai_h = "HHT";
                    }

                    if (madv_tralai == model.Macqcq_t)
                    {
                        model.Macqcq_t = null;
                        model.Trangthai_t = "HHT";
                    }

                    if (madv_tralai == model.Macqcq_ad)
                    {
                        model.Macqcq_ad = null;
                        model.Trangthai_ad = "HHT";
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

                    _db.ThamDinhGia.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "ThamDinhGiaXd", new { Madv = madv_tralai, Nam = model.Thoidiem.Year });
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.xd", "Approve"))
                {
                    var model = _db.ThamDinhGia.FirstOrDefault(t => t.Mahs == mahs_cb);

                    model.Trangthai_ad = "CB";
                    model.Congbo = "DACONGBO";

                    _db.ThamDinhGia.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "ThamDinhGiaXd");
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.xd", "Approve"))
                {
                    var model = _db.ThamDinhGia.FirstOrDefault(t => t.Mahs == mahs_hcb);

                    model.Trangthai_ad = "HCB";
                    model.Congbo = "CHUACONGBO";

                    _db.ThamDinhGia.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "ThamDinhGiaXd");
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

        private static string GetMadvChuyen(string macqcq, CSDLGia_ASP.Models.Manages.ThamDinhGia.ThamDinhGia hoso)
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
