using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GTroGiaTroCuoc
{
    public class TroGiaTroCuocController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        public TroGiaTroCuocController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("DinhGiaTGTC")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.trogiatrocuoc.thongtin", "Index") || Helpers.GetSsAdmin(HttpContext.Session, "Level") == "T")
                {

                    var dsdonvi = (from db in _db.DsDiaBan
                                   join dv in _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI") on db.MaDiaBan equals dv.MaDiaBan
                                   select new VMDsDonVi
                                   {
                                       Id = dv.Id,
                                       TenDiaBan = db.TenDiaBan,
                                       MaDiaBan = dv.MaDiaBan,
                                       TenDv = dv.TenDv,
                                       MaDv = dv.MaDv
                                   }).ToList();
                    if (dsdonvi.Count > 0)
                    {
                        Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;
                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") != null)
                        {
                            Madv = Helpers.GetSsAdmin(HttpContext.Session, "Madv");
                        }

                        IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaTroGiaTroCuoc> model = _db.GiaTroGiaTroCuoc;

                        if (Madv != "all")
                        {
                            model = model.Where(t => t.Madv == Madv);
                        }

                        if (string.IsNullOrEmpty(Nam))
                        {
                            Nam = Helpers.ConvertYearToStr(DateTime.Now.Year);
                            model = model.Where(t => t.Thoidiem.Year == int.Parse(Nam));
                        }
                        else
                        {
                            if (Nam != "all")
                            {
                                model = model.Where(t => t.Thoidiem.Year == int.Parse(Nam));
                            }
                        }

                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                        {
                            ViewData["DsDonVi"] = dsdonvi;
                        }
                        else
                        {
                            ViewData["DsDonVi"] = dsdonvi.Where(t => t.MaDv == Madv);
                        }
                        var dsDonViTH = (from donvi in _db.DsDonVi
                                         join tk in _db.Users on donvi.MaDv equals tk.Madv
                                         join gr in _db.GroupPermissions.Where(x => x.ChucNang == "TONGHOP") on tk.Chucnang equals gr.KeyLink
                                         select new CSDLGia_ASP.Models.Systems.DsDonVi
                                         {
                                             MaDiaBan = donvi.MaDiaBan,
                                             MaDv = donvi.MaDv,
                                             TenDv = donvi.TenDv,
                                         });
                        ViewData["DsDonViTh"] = dsDonViTH;

                        ViewData["Madv"] = Madv;
                        ViewData["Nam"] = Nam;
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                        ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");

                        ViewData["Title"] = " Thông tin hồ sơ mức trợ giá trợ cước";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgtgtc";
                        ViewData["MenuLv3"] = "menu_dgtgtc_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaTroGiaTroCuoc/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Title"] = " Thông tin hồ sơ Mức trợ giá trợ cước";
                        ViewData["Messages"] = "Thông tin hồ sơ Mức trợ giá trợ cước.";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgtgtc";
                        ViewData["MenuLv3"] = "menu_dgtgtc_tt";
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
        [Route("DinhGiaTGTC/Create")]
        [HttpGet]
        public IActionResult Create(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.trogiatrocuoc.thongtin", "Create"))
                {
                    var model_ct_cxd = _db.GiaTroGiaTroCuocCt.Where(t => t.Trangthai == "CXD" && t.Madv == Madv);
                    if (model_ct_cxd.Any())
                    {
                        _db.GiaTroGiaTroCuocCt.RemoveRange(model_ct_cxd);
                        _db.SaveChanges();
                    }
                    var model_file_cxd = _db.ThongTinGiayTo.Where(t => t.Status == "CXD" && t.Madv == Madv);
                    if (model_file_cxd.Any())
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        foreach (var file in model_file_cxd)
                        {
                            string path_del = Path.Combine(wwwRootPath + "/UpLoad/File/ThongTinGiayTo/", file.FileName);
                            FileInfo fi = new FileInfo(path_del);
                            if (fi != null)
                            {
                                System.IO.File.Delete(path_del);
                                fi.Delete();
                            }
                        }
                        _db.ThongTinGiayTo.RemoveRange(model_file_cxd);
                        _db.SaveChanges();
                    }

                    string Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH");
                    var model = new VMDinhGiaTroGiaTroCuoc
                    {
                        Madv = Madv,
                        Thoidiem = DateTime.Now,
                        Mahs = Mahs,
                    };
                    var danhmuc = _db.GiaTroGiaTroCuocDm;
                    List<GiaTroGiaTroCuocCt> list_add = new List<GiaTroGiaTroCuocCt>();
                    foreach(var item in danhmuc)
                    {
                        list_add.Add(new GiaTroGiaTroCuocCt
                        {
                            Mahs = Mahs,
                            Madv = Madv,
                            Mota = item.Tenspdv,
                            Trangthai = "CXD",
                            Maspdv = item.Maspdv,
                            Dvt = item.Dvt
                        });
                    }
                    _db.GiaTroGiaTroCuocCt.AddRange(list_add);
                    _db.SaveChanges();
                    model.GiaTroGiaTroCuocCt = _db.GiaTroGiaTroCuocCt.Where(t => t.Mahs == Mahs).ToList();

                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDiaBan"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["GiaTroGiaTroCuocDm"] = _db.GiaTroGiaTroCuocDm.ToList();
                    ViewData["Title"] = " Thông tin hồ Mức trợ giá trợ cước";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtgtc";
                    ViewData["MenuLv3"] = "menu_dgtgtc_tt";

                    return View("Views/Admin/Manages/DinhGia/GiaTroGiaTroCuoc/Create.cshtml", model);
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

        [Route("DinhGiaTGTC/Store")]
        [HttpPost]
        public IActionResult Store(VMDinhGiaTroGiaTroCuoc request)
        {

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.trogiatrocuoc.thongtin", "Create"))
                {
                    var model = new GiaTroGiaTroCuoc
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Madiaban = request.Madiaban,
                        Soqd = request.Soqd,
                        Thoidiem = request.Thoidiem,
                        Ttqd = request.Ttqd,
                        Trangthai = "CHT",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };

                    _db.GiaTroGiaTroCuoc.Add(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaTroGiaTroCuocCt.Where(t => t.Mahs == request.Mahs);
                    _db.GiaTroGiaTroCuocCt.UpdateRange(modelct);
                    _db.SaveChanges();
                    var model_file = _db.ThongTinGiayTo.Where(t => t.Mahs == request.Mahs);
                    if (model_file.Any())
                    {
                        foreach (var file in model_file)
                        {
                            file.Status = "XD";
                        }
                        _db.ThongTinGiayTo.UpdateRange(model_file);
                    }

                    return RedirectToAction("Index", "TroGiaTroCuoc", new { Madv = request.Madv });
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

        [Route("DinhGiaTGTC/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.trogiatrocuoc.thongtin", "Edit"))
                {
                    var model = _db.GiaTroGiaTroCuoc.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new VMDinhGiaTroGiaTroCuoc
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        Madiaban = model.Madiaban,
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Ttqd = model.Ttqd,
                    };

                    var model_ct = _db.GiaTroGiaTroCuocCt.Where(t => t.Mahs == model_new.Mahs);

                    model_new.GiaTroGiaTroCuocCt = model_ct.ToList();
                    model_new.ThongTinGiayTo = _db.ThongTinGiayTo.Where(t => t.Mahs == Mahs).ToList();

                    ViewData["Madv"] = model.Madv;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["GiaTroGiaTroCuocDm"] = _db.GiaTroGiaTroCuocDm.ToList();
                    ViewData["Title"] = "Chỉnh sửa Mức trợ giá trợ cước";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtgtc";
                    ViewData["MenuLv3"] = "menu_dgtgtc_tt";

                    return View("Views/Admin/Manages/DinhGia/GiaTroGiaTroCuoc/Edit.cshtml", model_new);
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

        [Route("DinhGiaTGTC/Update")]
        [HttpPost]
        public IActionResult Update(VMDinhGiaTroGiaTroCuoc request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.trogiatrocuoc.thongtin", "Edit"))
                {
                    var model = _db.GiaTroGiaTroCuoc.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Madiaban = request.Madiaban;
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Ttqd = request.Ttqd;
                    model.Updated_at = DateTime.Now;
                    _db.GiaTroGiaTroCuoc.Update(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaTroGiaTroCuocCt.Where(t => t.Mahs == request.Mahs);

                    _db.GiaTroGiaTroCuocCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    var model_file = _db.ThongTinGiayTo.Where(t => t.Mahs == request.Mahs);
                    if (model_file.Any())
                    {
                        foreach (var file in model_file)
                        {
                            file.Status = "XD";
                        }
                        _db.ThongTinGiayTo.UpdateRange(model_file);
                    }

                    return RedirectToAction("Index", "TroGiaTroCuoc", new { Madv = request.Madv });
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

        [Route("DinhGiaTGTC/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.trogiatrocuoc.thongtin", "Delete"))
                {
                    var model = _db.GiaTroGiaTroCuoc.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaTroGiaTroCuoc.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.GiaTroGiaTroCuocCt.Where(t => t.Mahs == model.Mahs);
                    _db.GiaTroGiaTroCuocCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    var model_file = _db.ThongTinGiayTo.Where(t => t.Mahs == model.Mahs);
                    if (model_file.Any())
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        foreach (var file in model_file)
                        {
                            string path_del = Path.Combine(wwwRootPath + "/UpLoad/File/ThongTinGiayTo/", file.FileName);
                            FileInfo fi = new FileInfo(path_del);
                            if (fi != null)
                            {
                                System.IO.File.Delete(path_del);
                                fi.Delete();
                            }
                        }
                        _db.ThongTinGiayTo.RemoveRange(model_file);
                    }
                    return RedirectToAction("Index", "TroGiaTroCuoc", new { Madv = model.Madv });
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

        [Route("DinhGiaTGTC/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.trogiatrocuoc.thongtin", "Show"))
                {
                    var model = _db.GiaTroGiaTroCuoc.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new VMDinhGiaTroGiaTroCuoc
                    {
                        Madiaban = model.Madiaban,
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Thongtin = model.Thongtin,
                    };
                    var model_ct = _db.GiaTroGiaTroCuocCt.Where(t => t.Mahs == Mahs);

                    model_new.GiaTroGiaTroCuocCt = model_ct.ToList();

                    ViewData["GiaTroGiaTroCuocDm"] = _db.GiaTroGiaTroCuocDm.ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["Title"] = "Thông tin Mức giá trợ giá trợ cước";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtgtc";
                    ViewData["MenuLv3"] = "menu_dgtgtc_tt";

                    return View("Views/Admin/Manages/DinhGia/GiaTroGiaTroCuoc/Show.cshtml", model_new);
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


        [Route("DinhGiaTGTC/Search")]
        [HttpGet]
        public IActionResult Search(string Madv, DateTime? NgayTu, DateTime? NgayDen, string Mahs, double DonGiaTu, double DonGiaDen, string MoTa)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.trogiatrocuoc.thongtin", "Index"))
                {
                    DateTime nowDate = DateTime.Now;
                    DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
                    DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);

                    Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;
                    NgayTu = NgayTu.HasValue ? NgayTu : firstDayCurrentYear;
                    NgayDen = NgayDen.HasValue ? NgayDen : lastDayCurrentYear;
                    Mahs = string.IsNullOrEmpty(Mahs) ? "all" : Mahs;
                    DonGiaTu = DonGiaTu == 0 ? 0 : DonGiaTu;
                    DonGiaDen = DonGiaDen == 0 ? 0 : DonGiaDen;
                    MoTa = string.IsNullOrEmpty(MoTa) ? "" : MoTa;

                    var model = (from hosoct in _db.GiaTroGiaTroCuocCt
                                 join hoso in _db.GiaTroGiaTroCuoc on hosoct.Mahs equals hoso.Mahs
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaTroGiaTroCuocCt
                                 {
                                     Madv = hoso.Madv,
                                     Tendv = donvi.TenDv,
                                     SoQD = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                     Mota = hosoct.Mota,
                                     Dongia = hosoct.Dongia,
                                     Trangthai = hoso.Trangthai,
                                     Mahs = hoso.Mahs,
                                     Dvt = hosoct.Dvt
                                 });
                    model = model.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen && t.Trangthai == "HT");
                    if (Madv != "all") { model = model.Where(t => t.Madv == Madv); }
                    if (DonGiaTu > 0) { model = model.Where(t => t.Dongia >= DonGiaTu); }
                    if (DonGiaDen > 0) { model = model.Where(t => t.Dongia <= DonGiaDen); }
                    if (Mahs != "all") { model = model.Where(t => t.Mahs == Mahs); }
                    if (!string.IsNullOrEmpty(MoTa))
                    {
                        model = model.Where(t => t.Mota.ToLower().Contains(MoTa.ToLower()));
                    }

                    ViewData["Madv"] = Madv;
                    ViewData["NgayTu"] = NgayTu;
                    ViewData["NgayDen"] = NgayDen;
                    ViewData["Mahs"] = Mahs;
                    ViewData["DonGiaTu"] = Helpers.ConvertDbToStr(DonGiaTu);
                    ViewData["DonGiaDen"] = Helpers.ConvertDbToStr(DonGiaDen);
                    ViewData["MoTa"] = MoTa;
                    ViewData["DanhSachHoSo"] = _db.GiaTroGiaTroCuoc.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen && t.Trangthai == "HT");

                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");

                    ViewData["Title"] = "Tìm kiếm thông tin mức trợ giá trợ cước";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtgtc";
                    ViewData["MenuLv3"] = "menu_dgtgtc_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaTroGiaTroCuoc/TimKiem/Search.cshtml", model);
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
        [Route("DinhGiaTGTC/PrintSearch")]
        [HttpPost]
        public IActionResult Print(string Madv, DateTime? NgayTu, DateTime? NgayDen, string Mahs, double DonGiaTu, double DonGiaDen, string MoTa)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.trogiatrocuoc.thongtin", "Edit"))
                {

                    var model = (from hosoct in _db.GiaTroGiaTroCuocCt
                                 join hoso in _db.GiaTroGiaTroCuoc on hosoct.Mahs equals hoso.Mahs
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaTroGiaTroCuocCt
                                 {
                                     Madv = hoso.Madv,
                                     Tendv = donvi.TenDv,
                                     SoQD = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                     Mota = hosoct.Mota,
                                     Dongia = hosoct.Dongia,
                                     Trangthai = hoso.Trangthai,
                                     Mahs = hoso.Mahs,
                                     Dvt = hosoct.Dvt
                                 });
                    model = model.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen && t.Trangthai == "HT");
                    if (Madv != "all") { model = model.Where(t => t.Madv == Madv); }
                    if (DonGiaTu > 0) { model = model.Where(t => t.Dongia >= DonGiaTu); }
                    if (DonGiaDen > 0) { model = model.Where(t => t.Dongia <= DonGiaDen); }
                    if (Mahs != "all") { model = model.Where(t => t.Mahs == Mahs); }
                    if (!string.IsNullOrEmpty(MoTa))
                    {
                        model = model.Where(t => t.Mota.ToLower().Contains(MoTa.ToLower()));
                    }



                    ViewData["Title"] = "Kết quả tìm kiếm thông tin mức trợ giá trợ cước";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtgtc";
                    ViewData["MenuLv3"] = "menu_dgtgtc_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaTroGiaTroCuoc/TimKiem/Result.cshtml", model);
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

        [HttpPost("DinhGiaTGTC/GetListHoSo")]
        public JsonResult GetListHoSo(DateTime ngaytu, DateTime ngayden)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.GiaTroGiaTroCuoc.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && t.Trangthai == "HT");
                string result = "<select class='form-control' id='Mahs_Search' name='Mahs_Search'>";
                result += "<option value='all'>--Tất cả---</option>";

                if (model.Any())
                {
                    foreach (var item in model)
                    {
                        result += "<option value='" + @item.Mahs + "'>Số QĐ: " + @item.Soqd + " - Thời điểm: " + @Helpers.ConvertDateToStr(item.Thoidiem) + "</option>";
                    }
                }
                result += "</select>";
                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Phiên đăng nhập kết thúc, Bạn cần đăng nhập lại!!!" };
                return Json(data);
            }
        }

    }
}
