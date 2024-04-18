using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.KeKhaiGia;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.ViewModels.Manages.KeKhaiGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.KeKhaiGia.KkGiaLuHanh
{
    public class ThongTinKkGiaLuHanhController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public ThongTinKkGiaLuHanhController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("KeKhaiGiaLuHanh")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam, string Trangthai)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgluhanh.giakk", "Index") ||
                    Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var Manghe = "LUHANH";
                    var dsdonvi = (from com in _db.Company
                                   join lvkd in _db.CompanyLvCc.Where(t => t.Manghe == Manghe) on com.Mahs equals lvkd.Mahs
                                   select new VMCompany
                                   {
                                       Id = com.Id,
                                       Manghe = lvkd.Manghe,
                                       Madv = com.Madv,
                                       Madiaban = com.Madiaban,
                                       Macqcq = lvkd.Macqcq,
                                       Mahs = com.Mahs,
                                       Tendn = com.Tendn,
                                       Trangthai = com.Trangthai
                                   }).ToList();
                    return Ok(dsdonvi);
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

                        var model = _db.KkGia.Where(t => t.Madv == Madv && t.Ngaynhap.Year == int.Parse(Nam) && t.Manghe == Manghe).ToList();

                        if (string.IsNullOrEmpty(Trangthai) || Trangthai == "All")
                        {
                            model = model.ToList();
                        }
                        else
                        {
                            model = model.Where(t => t.Trangthai == Trangthai).ToList();
                        }

                        var comct = _db.CompanyLvCc.Where(t => t.Manghe == Manghe && t.Madv == Madv).ToList();

                        if (comct.Count > 0)
                        {
                            if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                            {
                                ViewData["DsDonVi"] = dsdonvi;
                            }
                            else
                            {
                                ViewData["DsDonVi"] = dsdonvi.Where(t => t.Madv == Madv);
                            }
                            var check_tt = _db.KkGia.Where(t => t.Manghe == Manghe && t.Trangthai != "DD" && t.Madv == Madv).Count();
                            ViewData["check_tt"] = check_tt;
                            ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "ADMIN");
                            ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang == "NHAPLIEU");
                            ViewData["Madv"] = Madv;
                            ViewData["Trangthai"] = Trangthai;
                            ViewData["Tendn"] = _db.Company.FirstOrDefault(t => t.Madv == Madv).Tendn;
                            ViewData["Nam"] = Nam;
                            ViewData["Manghe"] = Manghe;
                            ViewData["Title"] = "Danh sách hồ sơ kê khai giá dịch vụ lữ hành";
                            ViewData["MenuLv1"] = "menu_kknygia";
                            ViewData["MenuLv2"] = "menu_kkgluhanh";
                            ViewData["MenuLv3"] = "menu_ttluhanh";
                            return View("Views/Admin/Manages/KeKhaiGia/KkGiaLuHanh/DanhSach/Index.cshtml", model);
                        }
                        else
                        {
                            ViewData["Title"] = "Danh sách hồ sơ kê khai giá dịch vụ lữ hành";
                            ViewData["Messages"] = "Kê khai giá dịch vụ lữ hành không thuộc quản lý của doanh nghiệp";
                            ViewData["MenuLv1"] = "menu_kknygia";
                            ViewData["MenuLv2"] = "menu_kkgluhanh";
                            ViewData["MenuLv3"] = "menu_ttluhanh";
                            return View("Views/Admin/Error/ThongBaoLoi.cshtml");
                        }
                    }
                    else
                    {
                        ViewData["Title"] = "Danh sách hồ sơ kê khai giá dịch vụ lữ hành";
                        ViewData["Messages"] = "Hệ thống chưa có doanh nghiệp kê khai giá dịch vụ lữ hành.";
                        ViewData["MenuLv1"] = "menu_kknygia";
                        ViewData["MenuLv2"] = "menu_kkgluhanh";
                        ViewData["MenuLv3"] = "menu_ttluhanh";
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

        [Route("KeKhaiGiaLuHanh/Create")]
        [HttpGet]
        public IActionResult Create(string Madv, string Manghe)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgluhanh.giakk", "Create") ||
                    Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var model = new VMKkGia
                    {
                        Manghe = Manghe,
                        Madv = Madv,
                        Ngaynhap = DateTime.Now,
                        Ngayhieuluc = DateTime.Now,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                    };

                    var modellk = _db.KkGia.Where(t => t.Manghe == "LUHANH" && t.Madv == Madv &&
                    (t.Trangthai == "DD" || t.Trangthai == "CB" || t.Trangthai == "HCB")).OrderByDescending(t => t.Ngayhieuluc).FirstOrDefault();
                    if (modellk != null)
                    {
                        model.Socvlk = modellk.Socv;
                        model.Ngaycvlk = modellk.Ngaynhap;
                    }

                    var model_ct = _db.KkGiaLuHanhCt.Where(t => t.Mahs == model.Mahs);

                    model.KkGiaLuHanhCt = model_ct.ToList();

                    int max_sttsapxep = model_ct.Any() ? model_ct.Max(t => t.STTSapxep) : 0;
                    ViewData["SapXep"] = max_sttsapxep;
                    ViewData["Madv"] = Madv;
                    ViewData["Tendn"] = _db.Company.FirstOrDefault(t => t.Madv == Madv).Tendn;
                    ViewData["Manghe"] = Manghe;
                    ViewData["Title"] = "Thêm mới hồ sơ kê khai giá dịch vụ lữ hành";
                    ViewData["MenuLv1"] = "menu_kknygia";
                    ViewData["MenuLv2"] = "menu_kkgluhanh";
                    ViewData["MenuLv3"] = "menu_ttluhanh";
                    return View("Views/Admin/Manages/KeKhaiGia/KkGiaLuHanh/DanhSach/Create.cshtml", model);
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

        [Route("KeKhaiGiaLuHanh/Store")]
        [HttpPost]
        public IActionResult Store(VMKkGia request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgluhanh.giakk", "Create") ||
                    Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
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

                    var modelct = _db.KkGiaLuHanhCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Trangthai = "XD";
                        }
                    }
                    _db.KkGiaLuHanhCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    var history = new NhatKySuDung
                    {
                        Madv = Helpers.GetSsAdmin(HttpContext.Session, "Madv"),
                        Diachitruycap = Request.HttpContext.Connection.RemoteIpAddress,
                        Nguoisudung = Helpers.GetSsAdmin(HttpContext.Session, "Name"),
                        Tendangnhap = Helpers.GetSsAdmin(HttpContext.Session, "Username"),
                        Thoigian = model.Created_at,
                        Chucnang = model.Manghe,
                        Hanhdong = "Create",
                        Noidung = "Thêm mới hồ sơ kê khai giá dịch vụ lữ hành",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.NhatKySuDung.Add(history);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "ThongTinKkGiaLuHanh", new { Madv = request.Madv, Nam = request.Ngaynhap.Year });
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

        [Route("KeKhaiGiaLuHanh/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgluhanh.giakk", "Edit") ||
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

                    var model_ct = _db.KkGiaLuHanhCt.Where(t => t.Mahs == model_new.Mahs && t.Madv == model_new.Madv);

                    model_new.KkGiaLuHanhCt = model_ct.ToList();
                    model_new.ThongTinGiayTo = _db.ThongTinGiayTo.Where(t => t.Mahs == model.Mahs).ToList();

                    ViewData["Madv"] = model.Madv;
                    ViewData["Title"] = "Chỉnh sửa hồ sơ kê khai giá dịch vụ lữ hành";
                    ViewData["MenuLv1"] = "menu_kknygia";
                    ViewData["MenuLv2"] = "menu_kkgluhanh";
                    ViewData["MenuLv3"] = "menu_ttluhanh";
                    return View("Views/Admin/Manages/KeKhaiGia/KkGiaLuHanh/DanhSach/Edit.cshtml", model_new);
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

        [Route("KeKhaiGiaLuHanh/Update")]
        [HttpPost]
        public IActionResult Update(VMKkGia request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgluhanh.giakk", "Edit") ||
                    Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
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

                    var modelct = _db.KkGiaLuHanhCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Trangthai = "XD";
                        }
                    }
                    _db.KkGiaLuHanhCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    var history = new NhatKySuDung
                    {
                        Madv = Helpers.GetSsAdmin(HttpContext.Session, "Madv"),
                        Diachitruycap = Request.HttpContext.Connection.RemoteIpAddress,
                        Nguoisudung = Helpers.GetSsAdmin(HttpContext.Session, "Name"),
                        Tendangnhap = Helpers.GetSsAdmin(HttpContext.Session, "Username"),
                        Thoigian = model.Updated_at,
                        Chucnang = model.Manghe,
                        Hanhdong = "Edit",
                        Noidung = "Cập nhật hồ sơ kê khai giá dịch vụ lữ hành",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.NhatKySuDung.Add(history);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "ThongTinKkGiaLuHanh", new { Madv = request.Madv, Nam = request.Ngaynhap.Year });
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

        [Route("KeKhaiGiaLuHanh/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgluhanh.giakk", "Delete") ||
                    Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var model = _db.KkGia.FirstOrDefault(t => t.Id == id_delete);
                    _db.KkGia.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.KkGiaLuHanhCt.Where(t => t.Mahs == model.Mahs && t.Madv == model.Madv);
                    _db.KkGiaLuHanhCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    var history = new NhatKySuDung
                    {
                        Madv = Helpers.GetSsAdmin(HttpContext.Session, "Madv"),
                        Diachitruycap = Request.HttpContext.Connection.RemoteIpAddress,
                        Nguoisudung = Helpers.GetSsAdmin(HttpContext.Session, "Name"),
                        Tendangnhap = Helpers.GetSsAdmin(HttpContext.Session, "Username"),
                        Thoigian = DateTime.Now,
                        Chucnang = model.Manghe,
                        Hanhdong = "Delete",
                        Noidung = "Xóa hồ sơ kê khai giá xi măng thép xây dựng",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.NhatKySuDung.Add(history);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "ThongTinKkGiaLuHanh", new { Madv = model.Madv, Nam = model.Ngaynhap.Year });
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

        [Route("KeKhaiGiaLuHanh/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgxmtxd.giakk", "Index") ||
                    Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                {
                    var model = GetThongTinKk(Mahs);

                    ViewData["Title"] = "Xem chi tiết hồ sơ kê khai giá dịch vụ lữ hành";
                    ViewData["MenuLv1"] = "menu_kknygia";
                    ViewData["MenuLv2"] = "menu_kkgluhanh";
                    ViewData["MenuLv3"] = "menu_ttluhanh";
                    return View("Views/Admin/Manages/KeKhaiGia/KkGiaLuHanh/DanhSach/Show.cshtml", model);
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

        [Route("KeKhaiGiaLuHanh/Search")]
        [HttpGet]
        public IActionResult Search(string Nam, string Tendvcu, string Qccl, double GiakkTu, double GiakkDen)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgluhanh.timkiem", "Index"))
                {
                    var model_join = from kkct in _db.KkGiaLuHanhCt
                                     join kk in _db.KkGia.Where(t => t.Manghe == "LUHANH" && t.Trangthai == "DD") on kkct.Mahs equals kk.Mahs
                                     join com in _db.Company on kk.Madv equals com.Madv
                                     select new VMKkGiaCt
                                     {
                                         Id = kkct.Id,
                                         Mahs = kkct.Mahs,
                                         Madv = kkct.Madv,
                                         Tendvcu = kkct.Tendvcu,
                                         Qccl = kkct.Qccl,
                                         Giakk = kkct.Giakk,
                                         STTHienthi = kkct.STTHienthi,
                                         Thoigian = kkct.Thoigian,
                                         Dodaitgian = kkct.Dodaitgian,
                                         Tendn = com.Tendn,
                                         Ngayhieuluc = kk.Ngayhieuluc,
                                     };
                    GiakkTu = GiakkTu == 0 ? 0 : GiakkTu;
                    GiakkDen = GiakkDen == 0 ? 0 : GiakkDen;

                    if (string.IsNullOrEmpty(Nam))
                    {
                        Nam = Helpers.ConvertYearToStr(DateTime.Now.Year);
                    }

                    if (!string.IsNullOrEmpty(Nam))
                    {
                        model_join = model_join.Where(t => t.Ngayhieuluc.Year == int.Parse(Nam));
                    }

                    if (!string.IsNullOrEmpty(Tendvcu))
                    {
                        model_join = model_join.Where(t => t.Tendvcu.ToLower().Contains(Tendvcu.ToLower()));

                    }

                    if (!string.IsNullOrEmpty(Qccl))
                    {
                        model_join = model_join.Where(t => t.Tendvcu.ToLower().Contains(Tendvcu.ToLower()));
                    }

                    if (GiakkTu > 0)
                    {
                        model_join = model_join.Where(t => t.Giakk >= GiakkTu);
                    }

                    if (GiakkDen > 0)
                    {
                        model_join = model_join.Where(t => t.Giakk <= GiakkDen);
                    }

                    ViewData["Nam"] = Nam;
                    ViewData["Tendvcu"] = Tendvcu;
                    ViewData["Qccl"] = Qccl;
                    ViewData["GiakkTu"] = GiakkTu;
                    ViewData["GiakkDen"] = GiakkDen;
                    ViewData["Title"] = "Tìm kiếm thông tin kê khai giá dịch vụ lữ hành";
                    ViewData["MenuLv1"] = "menu_kknygia";
                    ViewData["MenuLv2"] = "menu_kkgluhanh";
                    ViewData["MenuLv3"] = "menu_tkluhanh";
                    return View("Views/Admin/Manages/KeKhaiGia/KkGiaLuHanh/TimKiem/Index.cshtml", model_join);

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

        [Route("KeKhaiGiaLuHanh/Printf")]
        [HttpGet]
        public IActionResult Printf(string Nam, string Tendvcu, string Qccl, double GiakkTu, double GiakkDen)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgluhanh.timkiem", "Index"))
                {

                    var model_join = from kkct in _db.KkGiaLuHanhCt
                                     join kk in _db.KkGia.Where(t => t.Manghe == "LUHANH" && t.Trangthai == "DD") on kkct.Mahs equals kk.Mahs
                                     join com in _db.Company on kk.Madv equals com.Madv
                                     select new VMKkGiaCt
                                     {
                                         Id = kkct.Id,
                                         Mahs = kkct.Mahs,
                                         Madv = kkct.Madv,
                                         Tendvcu = kkct.Tendvcu,
                                         Qccl = kkct.Qccl,
                                         Giakk = kkct.Giakk,
                                         STTHienthi = kkct.STTHienthi,
                                         Thoigian = kkct.Thoigian,
                                         Dodaitgian = kkct.Dodaitgian,
                                         Tendn = com.Tendn,
                                         Ngayhieuluc = kk.Ngayhieuluc,
                                     };
                    GiakkTu = GiakkTu == 0 ? 0 : GiakkTu;
                    GiakkDen = GiakkDen == 0 ? 0 : GiakkDen;

                    if (!string.IsNullOrEmpty(Nam))
                    {
                        model_join = model_join.Where(t => t.Ngayhieuluc.Year == int.Parse(Nam));
                    }

                    if (!string.IsNullOrEmpty(Tendvcu))
                    {
                        model_join = model_join.Where(t => t.Tendvcu.ToLower().Contains(Tendvcu.ToLower()));

                    }

                    if (!string.IsNullOrEmpty(Qccl))
                    {
                        model_join = model_join.Where(t => t.Tendvcu.ToLower().Contains(Tendvcu.ToLower()));
                    }

                    if (GiakkTu > 0)
                    {
                        model_join = model_join.Where(t => t.Giakk >= GiakkTu);
                    }

                    if (GiakkDen > 0)
                    {
                        model_join = model_join.Where(t => t.Giakk <= GiakkDen);
                    }

                    ViewData["Nam"] = Nam;
                    ViewData["Tendvcu"] = Tendvcu;
                    ViewData["Qccl"] = Qccl;
                    ViewData["GiakkTu"] = GiakkTu;
                    ViewData["GiakkDen"] = GiakkDen;
                    ViewData["Title"] = "Tìm kiếm thông tin kê khai giá dịch vụ lữ hành";
                    ViewData["MenuLv1"] = "menu_kknygia";
                    ViewData["MenuLv2"] = "menu_kkgluhanh";
                    ViewData["MenuLv3"] = "menu_tkluhanh";
                    return View("Views/Admin/Manages/KeKhaiGia/KkGiaLuHanh/TimKiem/Printf.cshtml", model_join);

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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgluhanh.giakk", "Approve") ||
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

                    var history = new NhatKySuDung
                    {
                        Madv = Helpers.GetSsAdmin(HttpContext.Session, "Madv"),
                        Diachitruycap = Request.HttpContext.Connection.RemoteIpAddress,
                        Nguoisudung = Helpers.GetSsAdmin(HttpContext.Session, "Name"),
                        Tendangnhap = Helpers.GetSsAdmin(HttpContext.Session, "Username"),
                        Thoigian = model.Ngaychuyen,
                        Chucnang = model.Manghe,
                        Hanhdong = "Transfer",
                        Noidung = "Chuyển hồ sơ kê khai giá xi măng thép xây dựng",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.NhatKySuDung.Add(history);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "ThongTinKkGiaLuHanh", new { model.Madv, Nam = model.Ngaynhap.Year, Trangthai = "CD" });
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

        private VMKkGiaShow GetThongTinKk(string Mahs)
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

            hoso_kk = GetThongTinDn(hoso_kk, model.Madv);
            hoso_kk = GetThongTinDv(hoso_kk, model.Macqcq);
            hoso_kk = GetThongTinCt(hoso_kk, model.Mahs);

            return hoso_kk;
        }

        private VMKkGiaShow GetThongTinDn(VMKkGiaShow hoso, string Madv)
        {
            var modeldn = _db.Company.FirstOrDefault(t => t.Madv == Madv);
            if (modeldn != null)
            {
                hoso.Tendn = modeldn.Tendn;
                hoso.Diadanh = modeldn.Diadanh;
                hoso.Diachi = modeldn.Diachi;
            }
            return hoso;
        }

        private VMKkGiaShow GetThongTinDv(VMKkGiaShow hoso, string Macqcq)
        {
            var modeldv = _db.DsDonVi.FirstOrDefault(t => t.MaDv == Macqcq);
            if (modeldv != null)
            {
                hoso.Tendvhienthi = modeldv.TenDvHienThi;
                hoso.Chucvuky = modeldv.ChucVuKy;
                hoso.Nguoiky = modeldv.NguoiKy;
            }
            return hoso;
        }

        private VMKkGiaShow GetThongTinCt(VMKkGiaShow hoso, string Mahs)
        {
            var modelct = _db.KkGiaLuHanhCt.Where(t => t.Mahs == Mahs);
            if (modelct != null)
            {
                hoso.KkGiaLuHanhCt = modelct.ToList();
            }
            return hoso;
        }
    }
}
