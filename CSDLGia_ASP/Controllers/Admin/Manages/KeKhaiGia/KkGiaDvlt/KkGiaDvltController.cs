using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.KeKhaiGia;
using CSDLGia_ASP.ViewModels.Manages.KeKhaiGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CSDLGia_ASP.Controllers.Admin.Manages.KeKhaiGia.KkGiaDvlt
{
    public class KkGiaDvltController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public KkGiaDvltController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("KeKhaiGiaDvlt")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam, string Macskd, string Trangthai)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, " csdlmucgiahhdv.kknygia.kkgdvlt.giakk", "Index") ||
                    Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var Manghe = "DVLT";
                    var dsdonvi = (from com in _db.Company
                                   join lvkd in _db.CompanyLvCc.Where(t => t.Manghe == Manghe) on com.Mahs equals lvkd.Mahs
                                   select new VMCompany
                                   {
                                       Id = com.Id,
                                       Manghe = lvkd.Manghe,
                                       Madv = com.Madv,
                                       Madiaban = com.Madiaban,
                                       Mahs = com.Mahs,
                                       Tendn = com.Tendn,
                                       Trangthai = com.Trangthai
                                   }).ToList();

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

                        if (string.IsNullOrEmpty(Trangthai))
                        {
                            Trangthai = "CC";
                        }

                        if (string.IsNullOrEmpty(Nam))
                        {
                            Nam = Helpers.ConvertYearToStr(DateTime.Now.Year);
                        }

                        var model_cskd = _db.KkGiaDvLtCskd.Where(t => t.Madv == Madv);

                        if (string.IsNullOrEmpty(Macskd))
                        {
                            Macskd = model_cskd.Select(t => t.Macskd).First();
                        }

                        var comct = _db.CompanyLvCc.Where(t => t.Manghe == Manghe && t.Madv == Madv).ToList();

                        if (comct.Count > 0)
                        {
                            var cskd = _db.KkGiaDvLtCskd.Where(t => t.Madv == Madv).ToList();
                            if (cskd.Count > 0)
                            {
                                var model = _db.KkGia.Where(t => t.Madv == Madv && t.Ngaynhap.Year == int.Parse(Nam) && t.Manghe == Manghe && t.Macskd == Macskd && t.Trangthai == Trangthai).ToList();

                                if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                                {
                                    ViewData["DsDonVi"] = dsdonvi;
                                }
                                else
                                {
                                    ViewData["DsDonVi"] = dsdonvi.Where(t => t.Madv == Madv);
                                }
                                var check_tt = _db.KkGia.Where(t => t.Manghe == Manghe && t.Trangthai != "DD").Count();
                                ViewData["check_tt"] = check_tt;
                                ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "ADMIN");
                                ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang == "NHAPLIEU");
                                ViewData["Cskd"] = _db.KkGiaDvLtCskd.Where(t => t.Madv == Madv);
                                ViewData["Madv"] = Madv;
                                ViewData["Macskd"] = Macskd;
                                ViewData["Tendn"] = _db.Company.FirstOrDefault(t => t.Madv == Madv).Tendn;
                                ViewData["Nam"] = Nam;
                                ViewData["Manghe"] = Manghe;
                                ViewData["Title"] = "Danh sách hồ sơ kê khai dịch vụ lưu trú";
                                ViewData["MenuLv1"] = "menu_kknygia";
                                ViewData["MenuLv2"] = "menu_kkgdvlt";
                                ViewData["MenuLv3"] = "menu_giakkdvlt";
                                return View("Views/Admin/Manages/KeKhaiGia/KkGiaDvlt/Index.cshtml", model);
                            }
                            else
                            {
                                ViewData["Title"] = "Danh sách hồ sơ kê khai dịch vụ lưu trú";
                                ViewData["Messages"] = "Hệ thống chưa có doanh nghiệp kê khai dịch vụ lưu trú thuộc doanh nghiệp này";
                                ViewData["MenuLv1"] = "menu_kknygia";
                                ViewData["MenuLv2"] = "menu_kkgdvlt";
                                ViewData["MenuLv3"] = "menu_giakkdvlt";
                                return View("Views/Admin/Error/ThongBaoLoi.cshtml");
                            }
                        }
                        else
                        {
                            ViewData["Title"] = "Danh sách hồ sơ kê khai dịch vụ lưu trú";
                            ViewData["Messages"] = "Kê khai dịch vụ lưu trú không thuộc quản lý của doanh nghiệp";
                            ViewData["MenuLv1"] = "menu_kknygia";
                            ViewData["MenuLv2"] = "menu_kkgdvlt";
                            ViewData["MenuLv3"] = "menu_giakkdvlt";
                            return View("Views/Admin/Error/ThongBaoLoi.cshtml");
                        }
                    }
                    else
                    {
                        ViewData["Title"] = "Danh sách hồ sơ kê khai dịch vụ lưu trú";
                        ViewData["Messages"] = "Hệ thống chưa có doanh nghiệp kê khai dịch vụ lưu trú.";
                        ViewData["MenuLv1"] = "menu_kknygia";
                        ViewData["MenuLv2"] = "menu_kkgdvlt";
                        ViewData["MenuLv3"] = "menu_giakkdvlt";
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

        [Route("KeKhaiGiaDvlt/Create")]
        [HttpGet]
        public IActionResult Create(string Madv, string Macskd, string Manghe)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgdvlt.giakk", "Create") ||
                    Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var model = new VMKkGia
                    {
                        Manghe = Manghe,
                        Madv = Madv,
                        Macskd = Macskd,
                        Ngaynhap = DateTime.Now,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                    };

                    ViewData["Madv"] = Madv;
                    ViewData["Macskd"] = Macskd;
                    ViewData["Tendn"] = _db.Company.FirstOrDefault(t => t.Madv == Madv).Tendn;
                    ViewData["Tencskd"] = _db.KkGiaDvLtCskd.FirstOrDefault(t => t.Macskd == Macskd).Tencskd;
                    ViewData["Manghe"] = Manghe;
                    ViewData["Title"] = "Thêm mới Kê khai giá dịch vụ lưu trú";
                    ViewData["MenuLv1"] = "menu_kknygia";
                    ViewData["MenuLv2"] = "menu_kkgdvlt";
                    ViewData["MenuLv3"] = "menu_giakkdvlt";

                    return View("Views/Admin/Manages/KeKhaiGia/KkGiaDvlt/Create.cshtml", model);
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

        [Route("KeKhaiGiaDvlt/Store")]
        [HttpPost]
        public IActionResult Store(VMKkGia request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgdvlt.giakk", "Create") ||
                    Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var model = new KkGia
                    {
                        Mahs = request.Mahs,
                        Manghe = request.Manghe,
                        Madv = request.Madv,
                        Macskd = request.Macskd,
                        Ngaynhap = request.Ngaynhap,
                        Ngayhieuluc = request.Ngayhieuluc,
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
                    _db.KkGia.Add(model);
                    _db.SaveChanges();

                    var modelct = _db.KkGiaDvLtCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Trangthai = "XD";
                        }
                    }
                    _db.KkGiaDvLtCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "KkGiaDvLt", new { Madv = request.Madv, Macskd = request.Macskd });
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

        [Route("KeKhaiGiaDvlt/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgdvlt.giakk", "Edit") ||
                    Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var model = _db.KkGia.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new VMKkGia
                    {
                        Madv = model.Madv,
                        Manghe = model.Manghe,
                        Mahs = model.Mahs,
                        Macskd = model.Macskd,
                        Ngaynhap = model.Ngaynhap,
                        Ngayhieuluc = model.Ngayhieuluc,
                        Ngaycvlk = model.Ngaycvlk,
                        Socv = model.Socv,
                        Socvlk = model.Socvlk,
                        Ptnguyennhan = model.Ptnguyennhan,
                        Chinhsachkm = model.Chinhsachkm
                    };

                    var model_ct = _db.KkGiaDvLtCt.Where(t => t.Mahs == model_new.Mahs && t.Madv == model_new.Madv && t.Macskd == model_new.Macskd);

                    model_new.KkGiaDvLtCt = model_ct.ToList();

                    ViewData["Madv"] = model.Madv;
                    ViewData["Macskd"] = model.Macskd;
                    ViewData["Tendn"] = _db.Company.FirstOrDefault(t => t.Madv == model.Madv).Tendn;
                    ViewData["Tencskd"] = _db.KkGiaDvLtCskd.FirstOrDefault(t => t.Macskd == model.Macskd).Tencskd;
                    ViewData["Manghe"] = model.Manghe;
                    ViewData["Title"] = "Chỉnh sửa Kê khai giá dịch vụ lưu trú";
                    ViewData["MenuLv1"] = "menu_kknygia";
                    ViewData["MenuLv2"] = "menu_kkgdvlt";
                    ViewData["MenuLv3"] = "menu_giakkdvlt";

                    return View("Views/Admin/Manages/KeKhaiGia/KkGiaDvlt/Edit.cshtml", model_new);
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

        [Route("KeKhaiGiaDvlt/Update")]
        [HttpPost]
        public IActionResult Update(VMKkGia request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgdvlt.giakk", "Edit") ||
                    Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var model = _db.KkGia.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Ngaynhap = request.Ngaynhap;
                    model.Ngayhieuluc = request.Ngayhieuluc;
                    model.Socv = request.Socv;
                    model.Socvlk = request.Socvlk;
                    model.Ngaycvlk = request.Ngaycvlk;
                    model.Ptnguyennhan = request.Ptnguyennhan;
                    model.Chinhsachkm = request.Chinhsachkm;
                    model.Updated_at = DateTime.Now;
                    _db.KkGia.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "KkGiaDvLt", new { Madv = request.Madv, Macskd = request.Macskd });
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

        [Route("KeKhaiGiaDvlt/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgdvlt.giakk", "Delete") ||
                    Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var model = _db.KkGia.FirstOrDefault(t => t.Id == id_delete);
                    _db.KkGia.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.KkGiaDvLtCt.Where(t => t.Mahs == model.Mahs && t.Madv == model.Madv);
                    _db.KkGiaDvLtCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "KkGiaDvLt", new { Madv = model.Madv, Macskd = model.Macskd });
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

        [Route("KeKhaiGiaDvlt/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgdvlt.giakk", "Index") ||
                    Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var model = _db.KkGia.FirstOrDefault(t => t.Mahs == Mahs);
                    var hoso_kk = new VMKkGiaShow
                    {
                        Id = model.Id,
                        Mahs = model.Mahs,
                        Madv = model.Madv,
                        Macskd = model.Macskd,
                        Socv = model.Socv,
                        Ngaynhap = model.Ngaynhap,
                        Ngayhieuluc = model.Ngayhieuluc,
                        Ttnguoinop = model.Ttnguoinop,
                        Dtll = model.Dtll,
                        Sohsnhan = model.Sohsnhan,
                        Ngaychuyen = model.Ngaychuyen,
                        Ngaynhan = model.Ngaynhan,
                        Ptnguyennhan = model.Ptnguyennhan,
                        Chinhsachkm = model.Chinhsachkm,
                    };

                    var modeldn = _db.Company.FirstOrDefault(t => t.Madv == model.Madv);
                    if (modeldn != null)
                    {
                        hoso_kk.Tendn = modeldn.Tendn;
                        hoso_kk.Diadanh = modeldn.Diadanh;
                        hoso_kk.Diachi = modeldn.Diachi;
                    }

                    var modelcskd = _db.KkGiaDvLtCskd.FirstOrDefault(t => t.Macskd == model.Macskd);
                    if (modelcskd != null)
                    {
                        hoso_kk.Tencskd = modelcskd.Tencskd;
                        hoso_kk.Diachikd = modelcskd.Diachikd;
                        hoso_kk.Loaihang = modelcskd.Loaihang;
                    }

                    var modeldv = _db.DsDonVi.FirstOrDefault(t => t.MaDv == model.Macqcq);
                    if (modeldv != null)
                    {
                        hoso_kk.Tendvhienthi = modeldv.TenDvHienThi;
                    }

                    var modelct = _db.KkGiaDvLtCt.Where(t => t.Mahs == model.Mahs);
                    if (modelct != null)
                    {
                        hoso_kk.KkGiaDvLtCt = modelct.ToList();
                    }

                    ViewData["Title"] = "Kê khai giá dịch vụ lưu trú";
                    ViewData["MenuLv1"] = "menu_kknygia";
                    ViewData["MenuLv2"] = "menu_kkgdvlt";
                    ViewData["MenuLv3"] = "menu_giakkdvlt";
                    return View("Views/Admin/Manages/KeKhaiGia/KkGiaDvLt/Show.cshtml", hoso_kk);

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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgdvlt.giakk", "Approve") ||
                    Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var model = _db.KkGia.FirstOrDefault(p => p.Mahs == mahs_chuyen);

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

                    model.Ttnguoinop = Ttnguoinop;
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
                    _db.KkGia.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "KkGiaDvlt", new { Madv = model.Madv, Macskd = model.Macskd });
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

        [Route("KeKhaiGiaDvlt/TimKiem")]
        [HttpGet]
        public IActionResult Search(string Nam, string Tenks, string Tenhh)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgdvlt.giakk", "Index"))
                {
                    ViewData["Nam"] = Nam;
                    ViewData["Tenks"] = Tenks;
                    ViewData["Tenhh"] = Tenhh;
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ kê khai giá dịch vụ lưu trú";
                    ViewData["MenuLv1"] = "menu_kknygia";
                    ViewData["MenuLv2"] = "menu_kkgdvlt";
                    ViewData["MenuLv3"] = "menu_giakktkdvlt";
                    return View("Views/Admin/Manages/KeKhaiGia/KkGiaDvlt/TimKiem/Index.cshtml");
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

        [Route("KeKhaiGiaDvlt/TimKiem/KetQua")]
        [HttpPost]
        public IActionResult Result(string Nam, string Tenks, string Tenhh)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgdvlt.giakk", "Index"))
                {
                    var model = (from kkct in _db.KkGiaDvLtCt
                                 join kk in _db.KkGia.Where(t => t.Manghe == "DVLT" && t.Trangthai == "DD") on kkct.Mahs equals kk.Mahs
                                 join com in _db.Company on kkct.Madv equals com.Madv
                                 join cskd in _db.KkGiaDvLtCskd on kkct.Macskd equals cskd.Macskd
                                 select new VMKkGiaCt
                                 {
                                     Id = kkct.Id,
                                     Mahs = kkct.Mahs,
                                     Madv = kkct.Madv,
                                     Tendvcu = kkct.Tendvcu,
                                     Qccl = kkct.Qccl,
                                     Dvt = kkct.Dvt,
                                     Gialk = kkct.Gialk,
                                     Giakk = kkct.Giakk,
                                     Ghichu = kkct.Ghichu,
                                     Tendn = com.Tendn,
                                     Tenks = cskd.Tencskd,
                                     Ngayhieuluc = kk.Ngayhieuluc,
                                 });

                    if (Nam != "all")
                    {
                        model = model.Where(t => t.Ngayhieuluc.Year == int.Parse(Nam));
                    }


                    if (!string.IsNullOrEmpty(Tenks))
                    {
                        model = model.Where(t => t.Tenks.Contains(Tenks));
                    }


                    if (!string.IsNullOrEmpty(Tenhh))
                    {
                        model = model.Where(t => t.Tendvcu.Contains(Tenhh));
                    }

                    ViewData["Nam"] = Nam;
                    ViewData["Tenks"] = Tenks;
                    ViewData["Tenhh"] = Tenhh;
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ kê khai giá dịch vụ lưu trú";
                    ViewData["MenuLv1"] = "menu_kknygia";
                    ViewData["MenuLv2"] = "menu_kkgdvlt";
                    ViewData["MenuLv3"] = "menu_giakktkdvlt";
                    return View("Views/Admin/Manages/KeKhaiGia/KkGiaDvlt/TimKiem/Result.cshtml", model);
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

        [Route("KeKhaiGiaDvlt/TimKiem/KetQua/InTrang")]
        [HttpGet]
        public IActionResult Printf(string Nam, string Tenks, string Tenhh)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgdvlt.giakk", "Index"))
                {
                    var model = (from kkct in _db.KkGiaDvLtCt
                                 join kk in _db.KkGia.Where(t => t.Manghe == "DVLT" && t.Trangthai == "DD") on kkct.Mahs equals kk.Mahs
                                 join com in _db.Company on kkct.Madv equals com.Madv
                                 join cskd in _db.KkGiaDvLtCskd on kkct.Macskd equals cskd.Macskd
                                 select new VMKkGiaCt
                                 {
                                     Id = kkct.Id,
                                     Mahs = kkct.Mahs,
                                     Madv = kkct.Madv,
                                     Tendvcu = kkct.Tendvcu,
                                     Qccl = kkct.Qccl,
                                     Dvt = kkct.Dvt,
                                     Gialk = kkct.Gialk,
                                     Giakk = kkct.Giakk,
                                     Ghichu = kkct.Ghichu,
                                     Tendn = com.Tendn,
                                     Tenks = cskd.Tencskd,
                                     Ngayhieuluc = kk.Ngayhieuluc,
                                 });

                    if (Nam != "all")
                    {
                        model = model.Where(t => t.Ngayhieuluc.Year == int.Parse(Nam));
                    }


                    if (!string.IsNullOrEmpty(Tenks))
                    {
                        model = model.Where(t => t.Tenks.Contains(Tenks));
                    }


                    if (!string.IsNullOrEmpty(Tenhh))
                    {
                        model = model.Where(t => t.Tendvcu.Contains(Tenhh));
                    }

                    ViewData["Nam"] = Nam;
                    ViewData["Tenks"] = Tenks;
                    ViewData["Tenhh"] = Tenhh;
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ kê khai giá dịch vụ lưu trú";
                    ViewData["MenuLv1"] = "menu_kknygia";
                    ViewData["MenuLv2"] = "menu_kkgdvlt";
                    ViewData["MenuLv3"] = "menu_giakktkdvlt";
                    return View("Views/Admin/Manages/KeKhaiGia/KkGiaDvlt/TimKiem/Printf.cshtml", model);
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
