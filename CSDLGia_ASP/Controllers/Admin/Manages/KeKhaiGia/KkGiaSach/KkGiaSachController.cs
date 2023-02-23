using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.KeKhaiGia;
using CSDLGia_ASP.ViewModels.Manages.KeKhaiGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.KeKhaiGia.KkGiaSach
{
    public class KkGiaSachController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public KkGiaSachController(CSDLGiaDBContext db) => _db = db;


        [Route("KeKhaiGiaSach")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam, string Trangthai)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgsach.giakk", "Index") || Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var Manghe = "SACH";
                    var dsdonvi = (from com in _db.Company
                                   join lvkd in _db.CompanyLvCc.Where(t => t.Manghe == "SACH") on com.Mahs equals lvkd.Mahs
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

                        if (string.IsNullOrEmpty(Nam))
                        {
                            Nam = Helpers.ConvertYearToStr(DateTime.Now.Year);
                        }

                        var comct = _db.CompanyLvCc.Where(t => t.Manghe == Manghe && t.Madv == Madv).ToList();

                        if (comct.Count > 0)
                        {
                            var model = _db.KkGia.Where(t => t.Madv == Madv && t.Ngaynhap.Year == int.Parse(Nam) && t.Manghe == Manghe && t.Trangthai == Trangthai).ToList();
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
                            ViewData["Madv"] = Madv;
                            ViewData["Nam"] = Nam;
                            ViewData["Manghe"] = Manghe;
                            ViewData["Title"] = "Danh sách hồ sơ kê khai giá sách giáo khoa";
                            ViewData["MenuLv1"] = "menu_kknygia";
                            ViewData["MenuLv2"] = "menu_kkgsach";
                            ViewData["MenuLv3"] = "menu_giakksach";
                            return View("Views/Admin/Manages/KeKhaiGia/KkGiaSach/Index.cshtml", model);
                        }
                        else
                        {
                            ViewData["Title"] = "Danh sách hồ sơ kê khai giá sách giáo khoa";
                            ViewData["Messages"] = "Kê khai giá sách giáo khoa không thuộc quản lý của doanh nghiệp";
                            ViewData["MenuLv1"] = "menu_kknygia";
                            ViewData["MenuLv2"] = "menu_kkgsach";
                            ViewData["MenuLv3"] = "menu_giakksach";
                            return View("Views/Admin/Error/ThongBaoLoi.cshtml");
                        }
                    }
                    else
                    {
                        ViewData["Title"] = "Danh sách hồ sơ kê khai giá sách giáo khoa";
                        ViewData["Messages"] = "Hệ thống chưa có doanh nghiệp kê khai giá sách giáo khoa.";
                        ViewData["MenuLv1"] = "menu_kknygia";
                        ViewData["MenuLv2"] = "menu_kkgsach";
                        ViewData["MenuLv3"] = "menu_giakksach";
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

        [Route("KeKhaiGiaSach/Create")]
        [HttpGet]
        public IActionResult Create(string Madv, string Manghe)
        {

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgsach.giakk", "Create") || Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {

                    var model = new VMKkGia
                    {
                        Manghe = Manghe,
                        Madv = Madv,
                        Ngaynhap = DateTime.Now,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                    };

                    ViewData["Madv"] = Madv;
                    ViewData["Manghe"] = Manghe;
                    ViewData["Title"] = "Thêm mới Kê khai giá Sách giáo khoa";
                    ViewData["MenuLv1"] = "menu_kknygia";
                    ViewData["MenuLv2"] = "menu_kkgsach";
                    ViewData["MenuLv3"] = "menu_giakksach";
                    return View("Views/Admin/Manages/KeKhaiGia/KkGiaSach/Create.cshtml", model);
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

        [Route("KeKhaiGiaSach/Store")]
        [HttpPost]
        public IActionResult Store(VMKkGia request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgsach.giakk", "Create") || Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var model = new KkGia
                    {
                        Mahs = request.Mahs,
                        Manghe = request.Manghe,
                        Madv = request.Madv,
                        Ngaynhap = request.Ngaynhap,
                        Ngayhieuluc = request.Ngayhieuluc,
                        Socv = request.Socv,
                        Socvlk = request.Socvlk,
                        Ngaycvlk = request.Ngaycvlk,
                        Ytcauthanhgia = request.Ytcauthanhgia,
                        Thydggadgia = request.Thydggadgia,
                        Trangthai = "CC",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.KkGia.Add(model);
                    _db.SaveChanges();

                    var modelct = _db.KkGiaSachCt.Where(t => t.Madv == request.Madv);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Trangthai = "XD";
                        }
                    }
                    _db.KkGiaSachCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "KkGiaSach", new { request.Madv });
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

        [Route("KeKhaiGiaSach/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgsach.giakk", "Edit") ||
                    Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var model = _db.KkGia.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new VMKkGia
                    {
                        Madv = model.Madv,
                        Manghe = model.Manghe,
                        Mahs = model.Mahs,
                        Ngaynhap = model.Ngaynhap,
                        Ngayhieuluc = model.Ngayhieuluc,
                        Ngaycvlk = model.Ngaycvlk,
                        Socv = model.Socv,
                        Socvlk = model.Socvlk,
                        Ytcauthanhgia = model.Ytcauthanhgia,
                        Thydggadgia = model.Thydggadgia
                    };

                    var model_ct = _db.KkGiaSachCt.Where(t => t.Mahs == model_new.Mahs && t.Madv == model_new.Madv);

                    model_new.KkGiaSachCt = model_ct.ToList();

                    ViewData["Madv"] = model.Madv;
                    ViewData["Title"] = "Chỉnh sửa Kê khai giá sách giáo khoa";
                    ViewData["MenuLv1"] = "menu_kknygia";
                    ViewData["MenuLv2"] = "menu_kkgsach";
                    ViewData["MenuLv3"] = "menu_giakksach";

                    return View("Views/Admin/Manages/KeKhaiGia/KkGiaSach/Edit.cshtml", model_new);
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


        [Route("KeKhaiGiaSach/Update")]
        [HttpPost]
        public IActionResult Update(VMKkGia request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgsach.giakk", "Edit") || Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var model = _db.KkGia.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Ngaynhap = request.Ngaynhap;
                    model.Ngayhieuluc = request.Ngayhieuluc;
                    model.Socv = request.Socv;
                    model.Socvlk = request.Socvlk;
                    model.Ngaycvlk = request.Ngaycvlk;
                    model.Ytcauthanhgia = request.Ytcauthanhgia;
                    model.Thydggadgia = request.Thydggadgia;
                    model.Updated_at = DateTime.Now;
                    _db.KkGia.Update(model);
                    _db.SaveChanges();

                    var modelct = _db.KkGiaSachCt.Where(t => t.Madv == request.Madv);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Trangthai = "XD";
                        }
                    }
                    _db.KkGiaSachCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "KkGiaSach", new { request.Madv });
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

        [Route("KeKhaiGiaSach/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgsach.giakk", "Delete") || Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var model = _db.KkGia.FirstOrDefault(t => t.Id == id_delete);
                    _db.KkGia.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.KkGiaSachCt.Where(t => t.Mahs == model.Mahs && t.Madv == model.Madv);
                    _db.KkGiaSachCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "KkGiaSach", new { model.Madv });
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

        [Route("KeKhaiGiaSach/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgsach.giakk", "Index") || Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var model = _db.KkGia.FirstOrDefault(t => t.Mahs == Mahs);
                    var hoso_kk = new VMKkGiaShow
                    {
                        Id = model.Id,
                        Mahs = model.Mahs,
                        Socv = model.Socv,
                        Ngaynhap = model.Ngaynhap,
                        Ngayhieuluc = model.Ngayhieuluc,
                        Ttnguoinop = model.Ttnguoinop,
                        Dtll = model.Dtll,
                        Sohsnhan = model.Sohsnhan,
                        Ngaychuyen = model.Ngaychuyen,
                        Ngaynhan = model.Ngaynhan,
                        Ytcauthanhgia = model.Ytcauthanhgia,
                        Thydggadgia = model.Thydggadgia
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

                    var modelct = _db.KkGiaSachCt.Where(t => t.Mahs == model.Mahs);
                    if (modelct != null)
                    {
                        hoso_kk.KkGiaSachCt = modelct.ToList();
                    }

                    ViewData["Title"] = "Kê khai giá Sách";
                    ViewData["MenuLv1"] = "menu_kknygia";
                    ViewData["MenuLv2"] = "menu_kkgsach";
                    ViewData["MenuLv3"] = "menu_giakksach";
                    return View("Views/Admin/Manages/KeKhaiGia/KkGiaSach/Show.cshtml", hoso_kk);

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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgsach.giakk", "Approve") || Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
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
                    return RedirectToAction("Index", "KkGiaSach", new { model.Madv, Nam = model.Ngaynhap.Year });
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

        [Route("KeKhaiGiaSach/Search")]
        [HttpGet]
        public IActionResult Search(string Nam, string Mota)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgsach.giakk", "Index"))
                {

                    var model_join = from kkct in _db.KkGiaSachCt
                                     join kk in _db.KkGia.Where(t => t.Manghe == "SACH" && t.Trangthai == "DD") on kkct.Mahs equals kk.Mahs
                                     join com in _db.Company on kk.Madv equals com.Madv
                                     select new VMKkGiaCt
                                     {
                                         Id = kkct.Id,
                                         Mahs = kkct.Mahs,
                                         Madv = kkct.Madv,
                                         Tendvcu = kkct.Tendvcu,
                                         Qccl = kkct.Qccl,
                                         Dvt = kkct.Dvt,
                                         Giakk = kkct.Giakk,
                                         Ghichu = kkct.Ghichu,
                                         Tendn = com.Tendn,
                                         Ngayhieuluc = kk.Ngayhieuluc,
                                     };


                    if (string.IsNullOrEmpty(Nam))
                    {
                        Nam = Helpers.ConvertYearToStr(DateTime.Now.Year);
                    }

                    if (!string.IsNullOrEmpty(Mota))
                    {
                        model_join = model_join.Where(t => t.Ngayhieuluc.Year == int.Parse(Nam) && t.Tendvcu.Contains(Mota));
                    }
                    else
                    {
                        model_join = model_join.Where(t => t.Ngayhieuluc.Year == int.Parse(Nam));
                    }

                    ViewData["Nam"] = Nam;
                    ViewData["Mota"] = Mota;
                    ViewData["Title"] = "Tìm kiếm thông tin kê khai giá sách";
                    ViewData["MenuLv1"] = "menu_kknygia";
                    ViewData["MenuLv2"] = "menu_kkgsach";
                    ViewData["MenuLv3"] = "menu_giakktksach";
                    return View("Views/Admin/Manages/KeKhaiGia/KkGiaSach/TimKiem/Index.cshtml", model_join);

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

        [Route("KeKhaiGiaSach/Printf")]
        [HttpGet]
        public IActionResult Printf(string Nam, string Mota)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgsach.giakk", "Index"))
                {

                    var model_join = from kkct in _db.KkGiaSachCt
                                     join kk in _db.KkGia.Where(t => t.Manghe == "SACH" && t.Trangthai == "DD") on kkct.Mahs equals kk.Mahs
                                     join com in _db.Company on kk.Madv equals com.Madv
                                     select new VMKkGiaCt
                                     {
                                         Id = kkct.Id,
                                         Mahs = kkct.Mahs,
                                         Madv = kkct.Madv,
                                         Tendvcu = kkct.Tendvcu,
                                         Qccl = kkct.Qccl,
                                         Dvt = kkct.Dvt,
                                         Giakk = kkct.Giakk,
                                         Ghichu = kkct.Ghichu,
                                         Tendn = com.Tendn,
                                         Ngayhieuluc = kk.Ngayhieuluc,
                                     };


                    if (string.IsNullOrEmpty(Nam))
                    {
                        Nam = Helpers.ConvertYearToStr(DateTime.Now.Year);
                    }

                    if (!string.IsNullOrEmpty(Mota))
                    {
                        model_join = model_join.Where(t => t.Ngayhieuluc.Year == int.Parse(Nam) && t.Tendvcu.Contains(Mota));
                    }
                    else
                    {
                        model_join = model_join.Where(t => t.Ngayhieuluc.Year == int.Parse(Nam));
                    }

                    ViewData["Nam"] = Nam;
                    ViewData["Mota"] = Mota;
                    ViewData["Title"] = "Tìm kiếm thông tin kê khai giá sách";
                    ViewData["MenuLv1"] = "menu_kknygia";
                    ViewData["MenuLv2"] = "menu_kkgsach";
                    ViewData["MenuLv3"] = "menu_giakktksach";
                    return View("Views/Admin/Manages/KeKhaiGia/KkGiaSach/TimKiem/Printf.cshtml", model_join);

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
