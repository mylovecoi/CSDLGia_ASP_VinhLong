using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.Services;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.PhiLePhi
{
    public class PhiLePhiController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IDsDonviService _dsDonviService;
        private readonly ITrangThaiHoSoService _trangThaiHoSoService;

        public PhiLePhiController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment, IDsDonviService dsDonviService, ITrangThaiHoSoService trangThaiHoSoService)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
            _dsDonviService = dsDonviService;
            _trangThaiHoSoService = trangThaiHoSoService;
        }

        [Route("PhiLePhi/DanhSach")]
        [HttpGet]
        public IActionResult Index(int Nam, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.philephi.thongtin", "Index"))
                {
                    Madv = string.IsNullOrEmpty(Madv) ? Helpers.GetSsAdmin(HttpContext.Session, "Madv") : Madv;
                    var model_donvi = _dsDonviService.GetListDonvi(Helpers.GetSsAdmin(HttpContext.Session, "Madv"));

                    List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();
                    IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.PhiLePhi> model = _db.PhiLePhi.Where(t => list_madv.Contains(t.Madv));

                    if (Madv != "all")
                    {
                        model = model.Where(t => t.Madv == Madv);
                    }

                    if (Nam != 0)
                    {
                        model = model.Where(t => t.Thoidiem.Year == Nam);
                    }

                    ViewData["DsDonvi"] = model_donvi;
                    ViewData["Nam"] = Nam;
                    ViewData["Madv"] = Madv;
                    ViewData["DanhMucNhom"] = _db.PhiLePhiNhom;
                    ViewData["Title"] = "Thông tin giá phí, lệ phí";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_plp";
                    ViewData["MenuLv3"] = "menu_plp_tt";
                    return View("Views/Admin/Manages/DinhGia/PhiLePhi/DanhSach/Index.cshtml", model);

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


        [HttpGet("PhiLePhi/Create")]
        public IActionResult Create(string madv_create, string manhom_create)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.philephi.thongtin", "Create"))
                {
                    var model_ct_cxd = _db.PhiLePhiCt.Where(t => t.TrangThai == "CXD" && t.Madv == madv_create);
                    if (model_ct_cxd.Any())
                    {
                        _db.PhiLePhiCt.RemoveRange(model_ct_cxd);
                        _db.SaveChanges();
                    }
                    var model_file_cxd = _db.ThongTinGiayTo.Where(t => t.Status == "CXD" && t.Madv == madv_create);
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

                    string Mahs = madv_create + "_" + DateTime.Now.ToString("yyMMddssmmHH");
                    var danhmuc = _db.PhiLePhiDm.Where(t => t.Manhom == manhom_create);
                    List<PhiLePhiCt> list_add = new List<PhiLePhiCt>();
                    if (danhmuc.Any())
                    {
                        foreach (var item in danhmuc)
                        {
                            list_add.Add(new PhiLePhiCt
                            {
                                Madv = madv_create,
                                Mahs = Mahs,
                                TrangThai = "CXD",
                                SapXep = item.SapXep,
                                HienThi = item.HienThi,
                                Tenspdv = item.Tenspdv,
                                Dvt = item.Dvt,
                                Manhom = item.Manhom,
                            });
                        }
                        _db.PhiLePhiCt.AddRange(list_add);
                        _db.SaveChanges();
                    }

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.PhiLePhi
                    {
                        Mahs = Mahs,
                        Madv = madv_create,
                        Thoidiem = DateTime.Now,
                    };
                    model.PhiLePhiCt = _db.PhiLePhiCt.Where(t => t.Mahs == Mahs).ToList();


                    _db.SaveChanges();

                    ViewData["Title"] = "Giá phí, lệ phí";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_plp";
                    ViewData["MenuLv3"] = "menu_plp_tt";
                    return View("Views/Admin/Manages/DinhGia/PhiLePhi/DanhSach/Create.cshtml", model);

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

        [Route("PhiLePhi/Store")]
        [HttpPost]
        public IActionResult Store(CSDLGia_ASP.Models.Manages.DinhGia.PhiLePhi request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.philephi.thongtin", "Create"))
                {

                    var modelct = _db.PhiLePhiCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct.Any())
                    {
                        foreach (var ct in modelct)
                        {
                            ct.TrangThai = "XD";
                        }
                        _db.PhiLePhiCt.UpdateRange(modelct);
                    }
                    var model_file = _db.ThongTinGiayTo.Where(t => t.Mahs == request.Mahs);
                    if (model_file.Any())
                    {
                        foreach (var file in model_file)
                        {
                            file.Status = "XD";
                        }
                        _db.ThongTinGiayTo.UpdateRange(model_file);
                    }


                    var mPhiLePhi = new CSDLGia_ASP.Models.Manages.DinhGia.PhiLePhi
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Thoidiem = request.Thoidiem,
                        Soqd = request.Soqd,
                        Ttqd = request.Ttqd,
                        Ghichu = request.Ghichu,
                        PhanLoaiHoSo = request.PhanLoaiHoSo,
                        CodeExcel = request.CodeExcel,
                        Trangthai = "CC",
                        Congbo = "CHUACONGBO",
                        Ipf1 = request.Ipf1,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };

                    _db.PhiLePhi.Add(mPhiLePhi);      
                    _db.SaveChanges();

                    //Add Log
                    _trangThaiHoSoService.LogHoSo(mPhiLePhi.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Thêm mới");


                    return RedirectToAction("Index", "PhiLePhi", new { request.Madv });
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

        [Route("PhiLePhi/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.philephi.thongtin", "Edit"))
                {
                    var model = _db.PhiLePhi.FirstOrDefault(t => t.Mahs == Mahs);

                    var model_ct = _db.PhiLePhiCt.Where(t => t.Mahs == model.Mahs);

                    model.PhiLePhiCt = _db.PhiLePhiCt.Where(t => t.Mahs == model.Mahs).ToList();
                    model.ThongTinGiayTo = _db.ThongTinGiayTo.Where(t => t.Mahs == Mahs).ToList();


                    ViewData["Title"] = "Bảng giá phí, lệ phí";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_plp";
                    ViewData["MenuLv3"] = "menu_plp_tt";
                    return View("Views/Admin/Manages/DinhGia/PhiLePhi/DanhSach/Edit.cshtml", model);

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

        [Route("PhiLePhi/Update")]
        [HttpPost]
        public IActionResult Update(CSDLGia_ASP.Models.Manages.DinhGia.PhiLePhi request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.philephi.thongtin", "Edit"))
                {
                    var modelct = _db.PhiLePhiCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct.Any())
                    {
                        foreach (var ct in modelct)
                        {
                            ct.TrangThai = "XD";
                        }
                        _db.PhiLePhiCt.UpdateRange(modelct);
                    }
                    var model_file = _db.ThongTinGiayTo.Where(t => t.Mahs == request.Mahs);
                    if (model_file.Any())
                    {
                        foreach (var file in model_file)
                        {
                            file.Status = "XD";
                        }
                        _db.ThongTinGiayTo.UpdateRange(model_file);
                    }

                    var model = _db.PhiLePhi.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Ttqd = request.Ttqd;
                    model.Thongtin = request.Thongtin;
                    model.Ghichu = request.Ghichu;
                    model.Updated_at = DateTime.Now;



                    _db.PhiLePhi.Update(model);
                    _db.SaveChanges();

                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Cập nhật");


                    return RedirectToAction("Index", "PhiLePhi", new { request.Madv });
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

        [Route("PhiLePhi/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.philephi.thongtin", "Delete"))
                {
                    var model = _db.PhiLePhi.FirstOrDefault(t => t.Id == id_delete);

                    if (model != null)
                    {
                        var model_ct = _db.PhiLePhiCt.Where(t => t.Mahs == model.Mahs);
                        if (model_ct.Any()) { _db.PhiLePhiCt.RemoveRange(model_ct); }

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
                        _db.PhiLePhi.Remove(model);
                        _db.SaveChanges();
                    }


                    return RedirectToAction("Index", "PhiLePhi", new { model.Madv });
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

        [Route("PhiLePhi/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.philephi.thongtin", "Index"))
                {
                    var model = _db.PhiLePhi.FirstOrDefault(t => t.Mahs == Mahs);
                    model.PhiLePhiCt = _db.PhiLePhiCt.Where(t => t.Mahs == model.Mahs).ToList();

                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["Title"] = "Bảng giá phí, lệ phí";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_plp";
                    ViewData["MenuLv3"] = "menu_plp_tt";
                    if (model.PhanLoaiHoSo == "HOSONHANEXCEL")
                    {
                        return View("Views/Admin/Manages/DinhGia/PhiLePhi/DanhSach/ShowExcel.cshtml", model);
                    }
                    return View("Views/Admin/Manages/DinhGia/PhiLePhi/DanhSach/Show.cshtml", model);

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

        [Route("PhiLePhi/Complete")]
        [HttpPost]
        public IActionResult Complete(string mahs_chuyen, string trangthai_complete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.philephi.thongtin", "Approve"))
                {
                    var model = _db.PhiLePhi.FirstOrDefault(t => t.Mahs == mahs_chuyen);

                    model.Trangthai = trangthai_complete;
                    model.Updated_at = DateTime.Now;


                    _db.PhiLePhi.Update(model);
                    _db.SaveChanges();

                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), trangthai_complete);


                    return RedirectToAction("Index", "PhiLePhi", new { model.Madv });

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

        [Route("PhiLePhi/Search")]
        [HttpGet]
        public IActionResult Search(string Madv, string Manhom, DateTime? NgayTu, DateTime? NgayDen, string Mahs, double DonGiaTu, double DonGiaDen, string Tenspdv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.philephi.timkiem", "Index"))
                {

                    DateTime nowDate = DateTime.Now;
                    DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
                    DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);

                    Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;
                    Manhom = string.IsNullOrEmpty(Manhom) ? "all" : Manhom;
                    NgayTu = NgayTu.HasValue ? NgayTu : firstDayCurrentYear;
                    NgayDen = NgayDen.HasValue ? NgayDen : lastDayCurrentYear;
                    Mahs = string.IsNullOrEmpty(Mahs) ? "all" : Mahs;
                    DonGiaTu = DonGiaTu == 0 ? 0 : DonGiaTu;
                    DonGiaDen = DonGiaDen == 0 ? 0 : DonGiaDen;
                    Tenspdv = string.IsNullOrEmpty(Tenspdv) ? "" : Tenspdv;

                    var model = (from hosoct in _db.PhiLePhiCt
                                 join hoso in _db.PhiLePhi on hosoct.Mahs equals hoso.Mahs
                                 join nhom in _db.PhiLePhiNhom on hosoct.Manhom equals nhom.Manhom
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.PhiLePhiCt
                                 {
                                     Madv = hoso.Madv,
                                     Tendv = donvi.TenDv,
                                     Tennhom = nhom.Tennhom,
                                     SoQD = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                     Tenspdv = hosoct.Tenspdv,
                                     Dvt = hosoct.Dvt,
                                     Dongia = hosoct.Dongia,
                                     GhiChu = hosoct.GhiChu,
                                     Manhom = hosoct.Manhom,
                                     TrangThai = hoso.Trangthai,
                                     Mahs = hoso.Mahs
                                 });
                    List<string> list_TrangThai = new List<string> { "HT", "DD", "CB" };
                    model = model.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen && list_TrangThai.Contains(t.TrangThai));
                    if (Madv != "all") { model = model.Where(t => t.Madv == Madv); }
                    if (Manhom != "all") { model = model.Where(t => t.Manhom == Manhom); }
                    if (DonGiaTu > 0) { model = model.Where(t => t.Dongia >= DonGiaTu); }
                    if (DonGiaDen > 0) { model = model.Where(t => t.Dongia <= DonGiaDen); }
                    if (Mahs != "all") { model = model.Where(t => t.Mahs == Mahs); }
                    if (!string.IsNullOrEmpty(Tenspdv))
                    {
                        model = model.Where(t => t.Tenspdv.ToLower().Contains(Tenspdv.ToLower()));
                    }

                    ViewData["Madv"] = Madv;
                    ViewData["Manhom"] = Manhom;
                    ViewData["NgayTu"] = NgayTu;
                    ViewData["NgayDen"] = NgayDen;
                    ViewData["Mahs"] = Mahs;
                    ViewData["DonGiaTu"] = DonGiaTu;
                    ViewData["DonGiaDen"] = DonGiaDen;
                    ViewData["Tenspdv"] = Tenspdv;
                    ViewData["DanhSachHoSo"] = _db.PhiLePhi.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen && list_TrangThai.Contains(t.Trangthai));
                    ViewData["DanhMucNhom"] = _db.PhiLePhiNhom;
                    ViewData["DsDiaBan"] = _db.DsDiaBan;
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá phí, lệ phí";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_plp";
                    ViewData["MenuLv3"] = "menu_plp_tk";
                    return View("Views/Admin/Manages/DinhGia/PhiLePhi/TimKiem/Search.cshtml", model);

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

        [Route("PhiLePhi/PrintSearch")]
        [HttpPost]
        public IActionResult Print(string Madv_Search, string Manhom_Search, DateTime? NgayTu_Search, DateTime? NgayDen_Search, string Mahs_Search,
                                    double DonGiaTu_Search, double DonGiaDen_Search, string Tenspdv_Search)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.philephi.timkiem", "Index"))
                {
                    var model = (from hosoct in _db.PhiLePhiCt
                                 join hoso in _db.PhiLePhi on hosoct.Mahs equals hoso.Mahs
                                 join nhom in _db.PhiLePhiNhom on hosoct.Manhom equals nhom.Manhom
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.PhiLePhiCt
                                 {
                                     Madv = hoso.Madv,
                                     Tendv = donvi.TenDv,
                                     Tennhom = nhom.Tennhom,
                                     SoQD = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                     Tenspdv = hosoct.Tenspdv,
                                     Dvt = hosoct.Dvt,
                                     Dongia = hosoct.Dongia,
                                     GhiChu = hosoct.GhiChu,
                                     Manhom = hosoct.Manhom,
                                     TrangThai = hoso.Trangthai,    
                                     Mahs = hoso.Mahs
                                 });
                    List<string> list_TrangThai = new List<string> { "HT", "DD", "CB" };
                    model = model.Where(t => t.Thoidiem >= NgayTu_Search && t.Thoidiem <= NgayDen_Search && list_TrangThai.Contains(t.TrangThai));
                    if (Madv_Search != "all") { model = model.Where(t => t.Madv == Madv_Search); }
                    if (Manhom_Search != "all") { model = model.Where(t => t.Manhom == Manhom_Search); }
                    if (DonGiaTu_Search > 0) { model = model.Where(t => t.Dongia >= DonGiaTu_Search); }
                    if (DonGiaDen_Search > 0) { model = model.Where(t => t.Dongia <= DonGiaDen_Search); }
                    if (Mahs_Search != "all") { model = model.Where(t => t.Mahs == Mahs_Search); }
                    if (!string.IsNullOrEmpty(Tenspdv_Search))
                    {
                        model = model.Where(t => t.Tenspdv.ToLower().Contains(Tenspdv_Search.ToLower()));
                    }

                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá phí, lệ phí";
                    return View("Views/Admin/Manages/DinhGia/PhiLePhi/TimKiem/Result.cshtml", model);

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

        [HttpPost("PhiLePhi/GetListHoSo")]
        public JsonResult GetListHoSo(DateTime ngaytu, DateTime ngayden)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                List<string> list_TrangThai = new List<string> { "HT", "DD", "CB" };
                var model = _db.PhiLePhi.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && list_TrangThai.Contains(t.Trangthai));
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




        [HttpPost("PhiLePhiCt/Edit")]
        public JsonResult Edit(int Id)
        {
            var model = _db.PhiLePhiCt.FirstOrDefault(t => t.Id == Id);

            string result = "<div class='row' id='edit_thongtin'>";
            result += "<div class='col-xl-12'>";
            result += "<div class='form-group fv-plugins-icon-container'>";
            result += "<label>Nội dung</label>";
            result += "<label type='text'class='form-control'>" + model.Tenspdv + "</label>";
            result += "</div>";
            result += "</div>";

            result += "<div class='col-xl-6'>";
            result += "<div class='form-group fv-plugins-icon-container'>";
            result += "<label><b>Đơn vị tính</b></label>";
            result += "<label type='text' class='form-control' style='font-weight: bold'>" + model.Dvt + " </label>";
            result += "</div>";
            result += "</div>";
            result += "<div class='col-xl-6'>";
            result += "<div class='form-group fv-plugins-icon-container'>";
            result += "<label><b>Đơn giá</b></label>";
            result += "<input type='text' id='dongia_edit' name='dongia_edit' class='form-control money-decimal-mask' style='font-weight: bold' value='" + Helpers.ConvertDbToStr(model.Dongia) + "'/>";
            result += "</div>";
            result += "</div>";
            result += "<div class='col-xl-12'>";
            result += "<div class='form-group fv-plugins-icon-container'>";
            result += "<label>Ghi chú</label>";
            result += "<input type='text' id='ghichu_edit' name='ghichu_edit' type='text' class='form-control' value='" + model.GhiChu + "'>";
            result += "</div>";
            result += "</div>";
            result += "<input hidden id='id_edit' name='id_edit' value='" + model.Id + "' />";
            result += "</div>";

            var data = new { status = "success", message = result };
            return Json(data);
        }

        [HttpPost("PhiLePhiCt/Update")]
        public JsonResult UpdateCt(int Id, double Dongia, string Mahs, string GhiChu)
        {
            var model = _db.PhiLePhiCt.FirstOrDefault(t => t.Id == Id);
            model.Dongia = Dongia;
            model.GhiChu = GhiChu;
            _db.PhiLePhiCt.Update(model);
            _db.SaveChanges();

            string result = GetDataCt(Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        public string GetDataCt(string Mahs)
        {
            var model = _db.PhiLePhiCt.Where(t => t.Mahs == Mahs);

            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover table-responsive' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += " <th width='2%'>#</th>";
            result += " <th width='2%'>STT</th>";
            result += " <th>Nội dung</th>";
            result += " <th width='10%'>Đơn vị tính</th>";
            result += " <th width='10%'>Đơn giá</th>";
            result += " <th width='10%'>Ghi chú</th>";
            result += " <th width='8%'>Thao tác</th>";
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";

            foreach (var item in model.OrderBy(t => t.SapXep))
            {
                string HtmlStyle = Helpers.ConvertStrToStyle(item.Style);
                result += "<tr>";
                result += "<td style='text-align:center; " + @HtmlStyle + "'> " + item.SapXep + "</td>";
                result += "<td style='text-align:center; " + @HtmlStyle + "'> " + item.HienThi + "</td>";
                result += "<td style='text-align:left;" + @HtmlStyle + "'>" + item.Tenspdv + "</td>";
                result += "<td style='text-align:center;" + @HtmlStyle + "'>" + item.Dvt + "</td>";
                result += "<td style='text-align:right; " + @HtmlStyle + "'>" + Helpers.ConvertDbToStr(item.Dongia) + "</td>";
                result += "<td style='text-align:center;" + @HtmlStyle + "'>" + item.GhiChu + "</td>";
                result += "<td>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Nhập giá'";
                result += " data-target='#Edit_Modal' data-toggle='modal' onclick='SetEdit(" + item.Id + ")'>";
                result += "<i class='icon-lg la la-edit text-primary'></i>";
                result += "</td>";
                result += "</tr>";

            }
            result += "</tbody>";
            result += "</table>";
            result += "</div>";

            return result;

        }
    }
}
