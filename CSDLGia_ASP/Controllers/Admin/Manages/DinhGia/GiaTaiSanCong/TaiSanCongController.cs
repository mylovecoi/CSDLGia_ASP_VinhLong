using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Services;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaTaiSanCong
{
    public class TaiSanCongController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IDsDonviService _dsDonviService;
        private readonly ITrangThaiHoSoService _trangThaiHoSoService;


        public TaiSanCongController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment, IDsDonviService dsDonviService, ITrangThaiHoSoService trangThaiHoSoService)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
            _dsDonviService = dsDonviService;
            _trangThaiHoSoService = trangThaiHoSoService;
        }
        [Route("GiaTaiSanCong")]
        [HttpGet]
        public IActionResult Index(string Madv, int Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.taisancong.thongtin", "Index"))
                {
                    Madv = string.IsNullOrEmpty(Madv) ? Helpers.GetSsAdmin(HttpContext.Session, "Madv") : Madv;
                    var model_donvi = _dsDonviService.GetListDonvi(Helpers.GetSsAdmin(HttpContext.Session, "Madv"));
                    List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();

                    IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaTaiSanCong> model = _db.GiaTaiSanCong.Where(t => list_madv.Contains(t.Madv));
                    if (Madv != "all")
                    {
                        model = model.Where(t => t.Madv == Madv);
                    }

                    if (Nam != 0)
                    {
                        model = model.Where(t => t.Thoidiem.Year == Nam);
                    }
                    ViewData["Madv"] = Madv;
                    ViewData["Nam"] = Nam;
                    ViewData["DsDonvi"] = model_donvi;
                    ViewData["Title"] = " Thông tin hồ sơ giá tài sản công";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_tsc";
                    ViewData["MenuLv3"] = "menu_giatsc_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaTaiSanCong/DanhSach/Index.cshtml", model);


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


        [Route("GiaTaiSanCong/Create")]
        [HttpGet]
        public IActionResult Create(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.taisancong.thongtin", "Create"))
                {
                    var model_ct_cxd = _db.GiaTaiSanCongCt.Where(t => t.Trangthai == "CXD" && t.Madv == Madv);
                    if (model_ct_cxd.Any())
                    {
                        _db.GiaTaiSanCongCt.RemoveRange(model_ct_cxd);
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
                    var danhmuc = _db.GiaTaiSanCongDm;
                    if (danhmuc.Any())
                    {
                        List<CSDLGia_ASP.Models.Manages.DinhGia.GiaTaiSanCongCt> list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaTaiSanCongCt>();
                        foreach (var dm in danhmuc)
                        {
                            list_add.Add(new Models.Manages.DinhGia.GiaTaiSanCongCt()
                            {
                                Mataisan = dm.Mataisan,
                                Tentaisan = dm.Tentaisan,
                                Madv = Madv,
                                Mahs = Mahs,
                                Trangthai = "CXD"
                            });
                        }
                        _db.GiaTaiSanCongCt.AddRange(list_add);
                        _db.SaveChanges();
                    }


                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaTaiSanCong
                    {
                        Madv = Madv,
                        Thoidiem = DateTime.Now,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                    };
                    model.GiaTaiSanCongCt = _db.GiaTaiSanCongCt.Where(t => t.Mahs == Mahs).ToList();


                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "ADMIN");

                    ViewData["Title"] = " Thông tin hồ sơ giá tài sản công";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_tsc";
                    ViewData["MenuLv3"] = "menu_giatsc_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaTaiSanCong/DanhSach/Create.cshtml", model);
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


        [Route("GiaTaiSanCong/Store")]
        [HttpPost]
        public IActionResult Store(CSDLGia_ASP.Models.Manages.DinhGia.GiaTaiSanCong request)
        {

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.taisancong.thongtin", "Create"))
                {
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaTaiSanCong
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Madiaban = request.Madiaban,
                        Soqd = request.Soqd,
                        Thoidiem = request.Thoidiem,
                        Thongtin = request.Thongtin,
                        Trangthai = "CC",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };

                    var modelct = _db.GiaThueMatDatMatNuocCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct.Any())
                    {
                        foreach (var ct in modelct)
                        {
                            ct.Trangthai = "XD";
                        }
                        _db.GiaThueMatDatMatNuocCt.UpdateRange(modelct);
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

                    _db.GiaTaiSanCong.Add(model);
                    _db.SaveChanges();
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Thêm mới");

                    return RedirectToAction("Index", "TaiSanCong", new { Madv = request.Madv });
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


        [Route("GiaTaiSanCong/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.taisancong.thongtin", "Edit"))
                {
                    var model = _db.GiaTaiSanCong.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new CSDLGia_ASP.Models.Manages.DinhGia.GiaTaiSanCong
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        Madiaban = model.Madiaban,
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Thongtin = model.Thongtin,
                    };

                    model_new.GiaTaiSanCongCt = _db.GiaTaiSanCongCt.Where(t => t.Mahs == model_new.Mahs).ToList();
                    model_new.ThongTinGiayTo = _db.ThongTinGiayTo.Where(t => t.Mahs == model_new.Mahs).ToList();

                    ViewData["Madv"] = model.Madv;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Title"] = " Thông tin hồ sơ giá tài sản công";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_tsc";
                    ViewData["MenuLv3"] = "menu_giatsc_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaTaiSanCong/DanhSach/Edit.cshtml", model_new);
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

        [Route("GiaTaiSanCong/Update")]
        [HttpPost]
        public IActionResult Update(CSDLGia_ASP.Models.Manages.DinhGia.GiaTaiSanCong request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.taisancong.thongtin", "Edit"))
                {
                    var model = _db.GiaTaiSanCong.FirstOrDefault(t => t.Mahs == request.Mahs);
                    if (model != null)
                    {
                        var modelct = _db.GiaTaiSanCongCt.Where(t => t.Mahs == request.Mahs);
                        if (modelct.Any())
                        {
                            foreach (var ct in modelct)
                            {
                                ct.Trangthai = "XD";
                            }
                            _db.GiaTaiSanCongCt.UpdateRange(modelct);
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
                        model.Madiaban = request.Madiaban;
                        model.Soqd = request.Soqd;
                        model.Thoidiem = request.Thoidiem;
                        model.Thongtin = request.Thongtin;
                        model.Updated_at = DateTime.Now;
                        _db.GiaTaiSanCong.Update(model);
                        _db.SaveChanges();
                        //Add Log
                        _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Cập nhật");
                    }


                    return RedirectToAction("Index", "TaiSanCong", new { Madv = request.Madv });
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


        [Route("GiaTaiSanCong/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.taisancong.thongtin", "Delete"))
                {
                    var model = _db.GiaTaiSanCong.FirstOrDefault(t => t.Id == id_delete);
                    if (model != null)
                    {
                        var model_ct = _db.GiaTaiSanCongCt.Where(t => t.Mahs == model.Mahs);
                        if (model_ct.Any()) { _db.GiaTaiSanCongCt.RemoveRange(model_ct); }
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
                        _db.GiaTaiSanCong.Remove(model);
                        _db.SaveChanges();
                    }

                    return RedirectToAction("Index", "TaiSanCong", new { Madv = model.Madv });
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

        [Route("GiaTaiSanCong/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.taisancong.thongtin", "Index"))
                {

                    var model = _db.GiaTaiSanCong.FirstOrDefault(t => t.Mahs == Mahs);

                    model.GiaTaiSanCongCt = _db.GiaTaiSanCongCt.Where(t => t.Mahs == model.Mahs).ToList();

                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["Title"] = " Thông tin hồ sơ giá tài sản công";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_tsc";
                    ViewData["MenuLv3"] = "menu_giatsc_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaTaiSanCong/DanhSach/Show.cshtml", model);
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

        public IActionResult HoanThanh(string mahs_complete, string trangthai_complete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.taisancong.thongtin", "Approve"))
                {
                    var model = _db.GiaTaiSanCong.FirstOrDefault(p => p.Mahs == mahs_complete);
                    model.Trangthai = trangthai_complete;
                    model.Updated_at = DateTime.Now;

                    _db.GiaTaiSanCong.Update(model);
                    _db.SaveChanges();
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), trangthai_complete);
                    return RedirectToAction("Index", "TaiSanCong", new { Madv = model.Madv, Nam = model.Thoidiem.Year });
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

        [Route("GiaTaiSanCong/Search")]
        [HttpGet]
        public IActionResult Search(string Madv, DateTime? NgayTu, DateTime? NgayDen, string Mahs, double DonGiaTu, double DonGiaDen, string Tentaisan)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.taisancong.timkiem", "Index"))
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
                    Tentaisan = string.IsNullOrEmpty(Tentaisan) ? "" : Tentaisan;

                    var model = (from hosoct in _db.GiaTaiSanCongCt
                                 join hoso in _db.GiaTaiSanCong on hosoct.Mahs equals hoso.Mahs
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaTaiSanCongCt
                                 {
                                     Madv = hoso.Madv,
                                     Tendv = donvi.TenDv,
                                     SoQD = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                     Mataisan = hosoct.Mataisan,
                                     Tentaisan = hosoct.Tentaisan,
                                     Giathue = hosoct.Giathue,
                                     Giaconlai = hosoct.Giaconlai,
                                     Giapheduyet = hosoct.Giapheduyet,
                                     Giaban = hosoct.Giaban,

                                     Trangthai = hoso.Trangthai,
                                     Mahs = hoso.Mahs
                                 });

                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model = model.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen && list_trangthai.Contains(t.Trangthai));
                    if (Madv != "all") { model = model.Where(t => t.Madv == Madv); }
                    if (DonGiaTu > 0)
                    {
                        model = model.Where(t => t.Giathue >= DonGiaTu || t.Giaconlai >= DonGiaTu || t.Giapheduyet >= DonGiaTu || t.Giathue >= DonGiaTu);
                    }
                    if (DonGiaDen > 0)
                    {
                        model = model.Where(t => t.Giathue <= DonGiaDen || t.Giaconlai <= DonGiaDen || t.Giapheduyet <= DonGiaDen || t.Giathue <= DonGiaDen);
                    }
                    if (Mahs != "all") { model = model.Where(t => t.Mahs == Mahs); }
                    if (!string.IsNullOrEmpty(Tentaisan))
                    {
                        model = model.Where(t => t.Tentaisan.ToLower().Contains(Tentaisan.ToLower()));
                    }

                    ViewData["Madv"] = Madv;
                    ViewData["NgayTu"] = NgayTu;
                    ViewData["NgayDen"] = NgayDen;
                    ViewData["Mahs"] = Mahs;
                    ViewData["DonGiaTu"] = Helpers.ConvertDbToStr(DonGiaTu);
                    ViewData["DonGiaDen"] = Helpers.ConvertDbToStr(DonGiaDen);
                    ViewData["Tentaisan"] = Tentaisan;
                    ViewData["DanhSachHoSo"] = _db.GiaTaiSanCong.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen && list_trangthai.Contains(t.Trangthai));
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Cqcq"] = _db.DsDonVi;
                    ViewData["Title"] = " Thông tin hồ sơ giá tài sản công";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_tsc";
                    ViewData["MenuLv3"] = "menu_giatsc_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaTaiSanCong/TimKiem/Search.cshtml", model);
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

        [Route("GiaTaiSanCong/PrintSearch")]
        [HttpPost]
        public IActionResult Print(string Madv_Search, DateTime? NgayTu_Search, DateTime? NgayDen_Search, string Mahs_Search,
                                    double DonGiaTu_Search, double DonGiaDen_Search, string Tentaisan_Search)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.taisancong.timkiem", "Edit"))
                {

                    var model = (from hosoct in _db.GiaTaiSanCongCt
                                 join hoso in _db.GiaTaiSanCong on hosoct.Mahs equals hoso.Mahs
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaTaiSanCongCt
                                 {
                                     Madv = hoso.Madv,
                                     Tendv = donvi.TenDv,
                                     SoQD = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                     Mataisan = hosoct.Mataisan,
                                     Tentaisan = hosoct.Tentaisan,
                                     Giathue = hosoct.Giathue,
                                     Giaconlai = hosoct.Giaconlai,
                                     Giapheduyet = hosoct.Giapheduyet,
                                     Giaban = hosoct.Giaban,

                                     Trangthai = hoso.Trangthai,
                                     Mahs = hoso.Mahs
                                 });
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model = model.Where(t => t.Thoidiem >= NgayTu_Search && t.Thoidiem <= NgayDen_Search && list_trangthai.Contains(t.Trangthai));
                    if (Madv_Search != "all") { model = model.Where(t => t.Madv == Madv_Search); }
                    if (DonGiaTu_Search > 0) { model = model.Where(t => t.Giathue >= DonGiaTu_Search); }
                    if (DonGiaDen_Search > 0) { model = model.Where(t => t.Giathue <= DonGiaDen_Search); }
                    if (Mahs_Search != "all") { model = model.Where(t => t.Mahs == Mahs_Search); }
                    if (!string.IsNullOrEmpty(Tentaisan_Search))
                    {
                        model = model.Where(t => t.Tentaisan.ToLower().Contains(Tentaisan_Search.ToLower()));
                    }

                    ViewData["Title"] = " Thông tin hồ sơ giá tài sản công";
                    return View("Views/Admin/Manages/DinhGia/GiaTaiSanCong/TimKiem/Result.cshtml", model);
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

        [HttpPost("GiaTaiSanCong/GetListHoSo")]
        public JsonResult GetListHoSo(DateTime ngaytu, DateTime ngayden)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.GiaTaiSanCong.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && t.Trangthai == "HT");
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
