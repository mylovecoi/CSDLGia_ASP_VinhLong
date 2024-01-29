using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.KeKhaiDkg;
using CSDLGia_ASP.ViewModels.Manages.KeKhaiDkg;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.KeKhaiDkg
{
    public class KkMhBogController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public KkMhBogController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("BinhOnGia/DanhSach")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam, string Manghe)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")) ||Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var dsdonvi = _db.Company.ToList();

                    if (dsdonvi.Count > 0)
                    {
                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") != null)
                        {
                            Madv = Helpers.GetSsAdmin(HttpContext.Session, "Madv");
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(Madv))
                            {
                                Madv = dsdonvi.OrderBy(t => t.Id).Select(t => t.Madv).First();
                            }
                        }

                        var model = _db.KkMhBog.Where(t => t.Madv == Madv).ToList();

                        if (string.IsNullOrEmpty(Nam))
                        {
                            model = model.ToList();
                        }
                        else
                        {
                            if (Nam != "all")
                            {
                                model = model.Where(t => t.Ngayhieuluc.Year == int.Parse(Nam)).ToList();
                            }
                            else
                            {
                                model = model.ToList();
                            }
                        }

                        if (string.IsNullOrEmpty(Manghe))
                        {
                            model = model.ToList();
                        }
                        else
                        {
                            if (Manghe != "all")
                            {
                                model = model.Where(t => t.Manghe == Manghe).ToList();
                            }
                            else
                            {
                                model = model.ToList();
                            }

                        }

                        var model_join = (from kkbog in model
                                          join nghe in _db.DmNgheKd on kkbog.Manghe equals nghe.Manghe
                                          select new VMKkMhBog
                                          {
                                              Id = kkbog.Id,
                                              Tennghe = nghe.Tennghe,
                                              Socv = kkbog.Socv,
                                              Phanloai = kkbog.Phanloai,
                                              Ngayhieuluc = kkbog.Ngayhieuluc,
                                              Ngaychuyen = kkbog.Ngaychuyen,
                                              Trangthai = kkbog.Trangthai,
                                              Madiaban = kkbog.Madiaban,
                                              Mahs = kkbog.Mahs,
                                              Madv = kkbog.Madv,
                                              Lydo = kkbog.Lydo,
                                          }).ToList();

                        var dmnghekd = (from comct in _db.CompanyLvCc.Where(t => t.Madv == Madv)
                                        join nghe in _db.DmNgheKd.Where(t => t.Manganh == "BOG") on comct.Manghe equals nghe.Manghe
                                        select new VMCompanyLvCc
                                        {
                                            Id = comct.Id,
                                            Manghe = nghe.Manghe,
                                            Tennghe = nghe.Tennghe,
                                            Phanloai = nghe.Phanloai,
                                            Madv = comct.Madv
                                        });

                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                        {
                            ViewData["DsDonVi"] = dsdonvi;
                        }
                        else
                        {
                            ViewData["DsDonVi"] = dsdonvi.Where(t => t.Madv == Madv);
                        }
                        /*var check_tt = _db.KkMhBog.Where(t => t.Manghe == Manghe && t.Trangthai != "DD").Count();
                        ViewData["check_tt"] = check_tt;*/
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "ADMIN");
                        ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang == "NHAPLIEU");
                        ViewData["DmNgheKd"] = dmnghekd;
                        ViewData["Madv"] = Madv;
                        ViewData["Nam"] = Nam;
                        ViewData["Manghe"] = Manghe;
                        ViewData["Title"] = "Danh sách hồ sơ giá kê khai mặt hàng bình ổn giá";
                        ViewData["MenuLv1"] = "menu_bog";
                        ViewData["MenuLv2"] = "menu_thongtin";
                        return View("Views/Admin/Manages/KeKhaiDkg/KkMhBog/Index.cshtml", model_join);
                    }
                    else
                    {
                        ViewData["Title"] = "Danh sách hồ sơ giá kê khai mặt hàng bình ổn giá";
                        ViewData["Messages"] = "Hệ thống chưa có doanh nghiệp kê khai mặt hàng bình ổn giá.";
                        ViewData["MenuLv1"] = "menu_bog";
                        ViewData["MenuLv2"] = "menu_thongtin";
                        return View("Views/Admin/Error/ThongBaoLoi.cshtml");
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

        [Route("BinhOnGia/Create")]
        [HttpGet]
        public IActionResult Create(string Madv, string Manghe, string Phanloai)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.bog.thongtin", "Create") || Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var model = new VMKkMhBog
                    {
                        Manghe = Manghe,
                        Madv = Madv,
                        Phanloai = Phanloai,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                    };

                    ViewData["Madv"] = Madv;
                    ViewData["Tendn"] = _db.Company.FirstOrDefault(t => t.Madv == Madv).Tendn;
                    ViewData["Manghe"] = Manghe;
                    ViewData["Tennghe"] = _db.DmNgheKd.FirstOrDefault(t => t.Manghe == t.Manghe).Tennghe;
                    ViewData["Title"] = "Giá kê khai mặt hàng BOG";
                    ViewData["MenuLv1"] = "menu_bog";
                    ViewData["MenuLv2"] = "menu_thongtin";

                    return View("Views/Admin/Manages/KeKhaiDkg/KkMhBog/Create.cshtml", model);
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

        [Route("BinhOnGia/Store")]
        [HttpPost]
        public IActionResult Store(VMKkMhBog request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.bog.thongtin", "Create") || Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var model = new KkMhBog
                    {
                        Mahs = request.Mahs,
                        Manghe = request.Manghe,
                        Madv = request.Madv,
                        Thoidiem = request.Thoidiem,
                        Ngayhieuluc = request.Ngayhieuluc,
                        Phanloai = request.Phanloai,
                        Socv = request.Socv,
                        Socvlk = request.Socvlk,
                        Ngaycvlk = request.Ngaycvlk,
                        Ptnguyennhan = request.Ptnguyennhan,
                        Chinhsachkm = request.Chinhsachkm,
                        Trangthai = "CC",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.KkMhBog.Add(model);
                    _db.SaveChanges();

                    var modelct = _db.KkMhBogCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Trangthai = "XD";
                        }
                    }
                    _db.KkMhBogCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "KkMhBog", new { request.Madv });
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

        [Route("BinhOnGia/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.bog.thongtin", "Edit") || Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var model = _db.KkMhBog.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new VMKkMhBog
                    {
                        Madv = model.Madv,
                        Manghe = model.Manghe,
                        Mahs = model.Mahs,
                        Thoidiem = model.Thoidiem,
                        Ngayhieuluc = model.Ngayhieuluc,
                        Ngaycvlk = model.Ngaycvlk,
                        Socv = model.Socv,
                        Socvlk = model.Socvlk,
                        Ptnguyennhan = model.Ptnguyennhan,
                        Chinhsachkm = model.Chinhsachkm
                    };

                    var model_ct = _db.KkMhBogCt.Where(t => t.Mahs == model_new.Mahs && t.Madv == model_new.Madv);

                    model_new.KkMhBogCt = model_ct.ToList();

                    ViewData["Madv"] = model.Madv;
                    ViewData["Tendn"] = _db.Company.FirstOrDefault(t => t.Madv == model.Madv).Tendn;
                    ViewData["Tennghe"] = _db.DmNgheKd.FirstOrDefault(t => t.Manghe == t.Manghe).Tennghe;
                    ViewData["Title"] = "Chỉnh sửa hồ sơ giá kê khai mặt hàng BOG";
                    ViewData["MenuLv1"] = "menu_bog";
                    ViewData["MenuLv2"] = "menu_thongtin";

                    return View("Views/Admin/Manages/KeKhaiDkg/KkMhBog/Edit.cshtml", model_new);
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

        [Route("BinhOnGia/Update")]
        [HttpPost]
        public IActionResult Update(VMKkMhBog request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.bog.thongtin", "Edit") || Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var model = _db.KkMhBog.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Thoidiem = request.Thoidiem;
                    model.Ngayhieuluc = request.Ngayhieuluc;
                    model.Socv = request.Socv;
                    model.Socvlk = request.Socvlk;
                    model.Ngaycvlk = request.Ngaycvlk;
                    model.Ptnguyennhan = request.Ptnguyennhan;
                    model.Chinhsachkm = request.Chinhsachkm;
                    model.Updated_at = DateTime.Now;
                    _db.KkMhBog.Update(model);
                    _db.SaveChanges();

                    var modelct = _db.KkMhBogCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Trangthai = "XD";
                        }
                    }
                    _db.KkMhBogCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "KkMhBog", new { request.Madv });
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

        [Route("BinhOnGia/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.bog.thongtin", "Delete") || Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var model = _db.KkMhBog.FirstOrDefault(t => t.Id == id_delete);
                    _db.KkMhBog.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.KkMhBogCt.Where(t => t.Mahs == model.Mahs && t.Madv == model.Madv);
                    _db.KkMhBogCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "KkMhBog", new { model.Madv });
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

        [Route("BinhOnGia/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.bog.thongtin", "Index") || Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var model = _db.KkMhBog.FirstOrDefault(t => t.Mahs == Mahs);
                    var hoso_kk = new VMKkMhBogShow
                    {
                        Id = model.Id,
                        Mahs = model.Mahs,
                        Socv = model.Socv,
                        Thoidiem = model.Thoidiem,
                        Ngayhieuluc = model.Ngayhieuluc,
                        Dtll = model.Dtll,
                        Sohsnhan = model.Sohsnhan,
                        Ngaychuyen = model.Ngaychuyen,
                        Ngaynhan = model.Ngaynhan,
                        Ptnguyennhan = model.Ptnguyennhan,
                        Chinhsachkm = model.Chinhsachkm
                    };

                    var modeldn = _db.Company.FirstOrDefault(t => t.Madv == model.Madv);
                    if (modeldn != null)
                    {
                        hoso_kk.Tendn = modeldn.Tendn;
                        hoso_kk.Diadanh = modeldn.Diadanh;
                        hoso_kk.Diachi = modeldn.Diachi;
                    }

                    var modeldv = _db.DsDonVi.FirstOrDefault(t => t.MaDv == model.Macqcq);
                    if (modeldv != null)
                    {
                        hoso_kk.Tendvhienthi = modeldv.TenDvHienThi;
                    }

                    var modelct = _db.KkMhBogCt.Where(t => t.Mahs == model.Mahs);
                    if (modelct != null)
                    {
                        hoso_kk.KkMhBogCt = modelct.ToList();
                    }

                    ViewData["Title"] = "Giá kê khai mặt hàng bình ổn giá";
                    ViewData["MenuLv1"] = "menu_bog";
                    ViewData["MenuLv2"] = "menu_thongtin";
                    return View("Views/Admin/Manages/KeKhaiDkg/KkMhBog/Show.cshtml", hoso_kk);

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

        public IActionResult Chuyen(string mahs_chuyen, string Ttnguoinop, string Dtll, string Macqcq)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.bog.thongtin", "Approve") || Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var model = _db.KkMhBog.FirstOrDefault(p => p.Mahs == mahs_chuyen);

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

                    model.Nguoichuyen = Ttnguoinop;
                    model.Dtll = Dtll;
                    model.Macqcq = Macqcq;
                    model.Ngaychuyen = DateTime.Now;
                    model.Trangthai = "CD";
                    if (chk_dvcq != null && chk_dvcq.Level == "T")
                    {
                        model.Madv_t = Macqcq;
                        model.Ngaychuyen_t = DateTime.Now;
                        model.Trangthai_t = "CD";
                    }
                    else if (chk_dvcq != null && chk_dvcq.Level == "ADMIN")
                    {
                        model.Madv_ad = Macqcq;
                        model.Ngaychuyen_ad = DateTime.Now;
                        model.Trangthai_ad = "CD";
                    }
                    else
                    {
                        model.Madv_h = Macqcq;
                        model.Ngaychuyen_h = DateTime.Now;
                        model.Trangthai_h = "CD";
                    }
                    _db.KkMhBog.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "KkMhBog", new { model.Madv, Nam = model.Thoidiem.Year });
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

        [Route("BinhOnGia/TimKiem")]
        [HttpGet]
        public IActionResult Search()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.bog.timkiem", "Index"))
                {
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "ADMIN");
                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang == "NHAPLIEU");
                    ViewData["DsDn"] = _db.Company;
                    ViewData["DmNghe"] = _db.DmNgheKd.Where(t => t.Manganh == "BOG");
                    ViewData["Title"] = "Tìm kiếm hồ sơ giá kê khai mặt hàng bình ổn giá";
                    ViewData["MenuLv1"] = "menu_bog";
                    ViewData["MenuLv2"] = "menu_timkiem";
                    return View("Views/Admin/Manages/KeKhaiDkg/KkMhBog/TimKiem/Index.cshtml");

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

        [Route("BinhOnGia/TimKiem/KetQua")]
        [HttpPost]
        public IActionResult Result(string madv, string manghe, string tenhh, DateTime ngayapdung_tu, DateTime ngayapdung_den, double giakk_tu, double giakk_den)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.bog.timkiem", "Index"))
                {
                    var model = (from kkct in _db.KkMhBogCt
                                 join kk in _db.KkMhBog.Where(t => t.Trangthai == "DD") on kkct.Mahs equals kk.Mahs
                                 join com in _db.Company on kkct.Madv equals com.Madv
                                 select new VMKkMhBogCt
                                 {
                                     Id = kkct.Id,
                                     Mahs = kkct.Mahs,
                                     Madv = kkct.Madv,
                                     Manghe = kk.Manghe,
                                     Tenhh = kkct.Tenhh,
                                     Quycach = kkct.Plhh,
                                     Dvt = kkct.Dvt,
                                     Gialk = kkct.Gialk,
                                     Giakk = kkct.Giakk,
                                     Ghichu = kkct.Ghichu,
                                     Tendn = com.Tendn,
                                     Ngayhieuluc = kk.Ngayhieuluc,
                                 });

                    if (madv != "all")
                    {
                        model = model.Where(t => t.Madv == madv);
                    }


                    if (manghe != "all")
                    {
                        model = model.Where(t => t.Manghe == manghe);
                    }


                    if (!string.IsNullOrEmpty(tenhh))
                    {
                        model = model.Where(t => t.Tenhh.Contains(tenhh));
                    }

                    if (ngayapdung_tu.ToString("yyMMdd") != "010101")
                    {
                        model = model.Where(t => t.Ngayhieuluc >= ngayapdung_tu);
                    }

                    if (ngayapdung_den.ToString("yyMMdd") != "010101")
                    {
                        model = model.Where(t => t.Ngayhieuluc <= ngayapdung_den);
                    }

                    model = model.Where(t => t.Giakk >= giakk_tu);
                    if (giakk_den > 0)
                    {
                        model = model.Where(t => t.Giakk <= giakk_den);
                    }

                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "ADMIN");
                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang == "NHAPLIEU");
                    ViewData["DsDn"] = _db.Company;
                    ViewData["DmNghe"] = _db.DmNgheKd.Where(t => t.Manganh == "BOG");
                    ViewData["Title"] = "Tìm kiếm hồ sơ giá kê khai mặt hàng bình ổn giá";
                    ViewData["MenuLv1"] = "menu_bog";
                    ViewData["MenuLv2"] = "menu_timkiem";
                    return View("Views/Admin/Manages/KeKhaiDkg/KkMhBog/TimKiem/Result.cshtml", model);

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

        /*[Route("BinhOnGia/TimKiem/Printf")]
        [HttpGet]
        public IActionResult Printf(string madv, string manghe, string tenhh, DateTime ngayapdung_tu, DateTime ngayapdung_den, double giakk_tu, double giakk_den)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.bog.timkiem", "Index"))
                {
                    var model = (from kkct in _db.KkMhBogCt
                                 join kk in _db.KkMhBog.Where(t => t.Trangthai == "DD") on kkct.Mahs equals kk.Mahs
                                 join com in _db.Company on kkct.Madv equals com.Madv
                                 select new VMKkMhBogCt
                                 {
                                     Id = kkct.Id,
                                     Mahs = kkct.Mahs,
                                     Madv = kkct.Madv,
                                     Manghe = kk.Manghe,
                                     Tenhh = kkct.Tenhh,
                                     Quycach = kkct.Plhh,
                                     Dvt = kkct.Dvt,
                                     Gialk = kkct.Gialk,
                                     Giakk = kkct.Giakk,
                                     Ghichu = kkct.Ghichu,
                                     Tendn = com.Tendn,
                                     Ngayhieuluc = kk.Ngayhieuluc,
                                 });

                    if (madv != "all")
                    {
                        model = model.Where(t => t.Madv == madv);
                    }


                    if (manghe != "all")
                    {
                        model = model.Where(t => t.Manghe == manghe);
                    }


                    if (!string.IsNullOrEmpty(tenhh))
                    {
                        model = model.Where(t => t.Tenhh.Contains(tenhh));
                    }

                    if (ngayapdung_tu.ToString("yyMMdd") != "010101")
                    {
                        model = model.Where(t => t.Ngayhieuluc >= ngayapdung_tu);
                    }

                    if (ngayapdung_den.ToString("yyMMdd") != "010101")
                    {
                        model = model.Where(t => t.Ngayhieuluc <= ngayapdung_den);
                    }

                    model = model.Where(t => t.Giakk >= giakk_tu);
                    if (giakk_den > 0)
                    {
                        model = model.Where(t => t.Giakk <= giakk_den);
                    }

                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "ADMIN");
                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang == "NHAPLIEU");
                    ViewData["DsDn"] = _db.Company;
                    ViewData["DmNghe"] = _db.DmNgheKd.Where(t => t.Manganh == "BOG");
                    ViewData["Title"] = "Tìm kiếm hồ sơ giá kê khai mặt hàng bình ổn giá";
                    ViewData["MenuLv1"] = "menu_bog";
                    ViewData["MenuLv2"] = "menu_timkiem";
                    return View("Views/Admin/Manages/KeKhaiDkg/KkMhBog/TimKiem/Printf.cshtml", model);

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
        }*/
    }
}
