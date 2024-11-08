﻿using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Services;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GTroGiaTroCuoc
{
    public class TroGiaTroCuocController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IDsDonviService _dsDonviService;
        private readonly ITrangThaiHoSoService _trangThaiHoSoService;

        public TroGiaTroCuocController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment, IDsDonviService dsDonviService, ITrangThaiHoSoService trangThaiHoSoService)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
            _dsDonviService = dsDonviService;
            _trangThaiHoSoService = trangThaiHoSoService;
        }

        [Route("DinhGiaTGTC")]
        [HttpGet]
        public IActionResult Index(string Madv, int Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.trogiatrocuoc.thongtin", "Index"))
                {
                    Madv = string.IsNullOrEmpty(Madv) ? Helpers.GetSsAdmin(HttpContext.Session, "Madv") : Madv;
                    var model_donvi = _dsDonviService.GetListDonvi(Helpers.GetSsAdmin(HttpContext.Session, "Madv"));
                    List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();

                    IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaTroGiaTroCuoc> model = _db.GiaTroGiaTroCuoc.Where(t=>list_madv.Contains(t.Madv));
                    if (Madv != "all")
                    {
                        model = model.Where(t => t.Madv == Madv);
                    }
                    if (Nam != 0)
                    {
                        model = model.Where(t => t.Thoidiem.Year == Nam);
                    }

                    ViewData["Nam"] = Nam;
                    ViewData["Madv"] = Madv;
                    ViewData["DsDonvi"] = model_donvi;

                    ViewData["Title"] = " Thông tin hồ sơ mức trợ giá trợ cước";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtgtc";
                    ViewData["MenuLv3"] = "menu_dgtgtc_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaTroGiaTroCuoc/Index.cshtml", model);


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
                    foreach (var item in danhmuc)
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
                        Trangthai = "CC",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };

                    _db.GiaTroGiaTroCuoc.Add(model);
                    _db.SaveChanges();


                    var modelct = _db.GiaTroGiaTroCuocCt.Where(t => t.Mahs == request.Mahs).ToList();
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Trangthai = "XD";
                        }
                    }
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

                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Thêm mới");

                    // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                    LoggingHelper.LogAction(HttpContext, _db, "Store", "Thêm mới hồ sơ trợ giá trợ cước");


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


                    var modelct = _db.GiaTroGiaTroCuocCt.Where(t => t.Mahs == request.Mahs).ToList();
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Trangthai = "XD";
                        }
                    }
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

                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Cập nhật");

                    // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                    LoggingHelper.LogAction(HttpContext, _db, "Update", " Update hồ sơ giá trợ giá trợ cước");

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

                    // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                    LoggingHelper.LogAction(HttpContext, _db, "Delete", "Xóa hồ sơ giá trợ giá trợ cước");

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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.trogiatrocuoc.thongtin", "Index"))
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

        [HttpPost]
        public IActionResult HoanThanh(string mahs_complete, string trangthai_complete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.trogiatrocuoc.thongtin", "Approve"))
                {
                    var model = _db.GiaTroGiaTroCuoc.FirstOrDefault(p => p.Mahs == mahs_complete);

                    model.Updated_at = DateTime.Now;
                    model.Trangthai = trangthai_complete;

                    _db.GiaTroGiaTroCuoc.Update(model);
                    _db.SaveChanges();

                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), trangthai_complete);



                    return RedirectToAction("Index", "TroGiaTroCuoc", new { Madv = model.Madv, Nam = model.Thoidiem.Year });
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.trogiatrocuoc.timkiem", "Index"))
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
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model = model.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen && list_trangthai.Contains(t.Trangthai));
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
                    ViewData["DanhSachHoSo"] = _db.GiaTroGiaTroCuoc.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen && list_trangthai.Contains(t.Trangthai));

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
        [HttpGet]
        public IActionResult Print(string Madv, DateTime? NgayTu, DateTime? NgayDen, string Mahs, double DonGiaTu, double DonGiaDen, string MoTa)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.trogiatrocuoc.timkiem", "Index"))
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
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model = model.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen && list_trangthai.Contains(t.Trangthai));
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


        [HttpGet("DinhGiaTGTC/ExportToExcel")]
        public IActionResult ExportToExcel(string Madv, DateTime? NgayTu, DateTime? NgayDen, string Mahs, double DonGiaTu, double DonGiaDen, string MoTa)
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
            List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
            model = model.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen && list_trangthai.Contains(t.Trangthai));
            if (Madv != "all") { model = model.Where(t => t.Madv == Madv); }
            if (DonGiaTu > 0) { model = model.Where(t => t.Dongia >= DonGiaTu); }
            if (DonGiaDen > 0) { model = model.Where(t => t.Dongia <= DonGiaDen); }
            if (Mahs != "all") { model = model.Where(t => t.Mahs == Mahs); }
            if (!string.IsNullOrEmpty(MoTa))
            {
                model = model.Where(t => t.Mota.ToLower().Contains(MoTa.ToLower()));
            }


            // Start exporting to excel
            var stream = new MemoryStream();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var xlPackage = new ExcelPackage(stream))
            {
                // Define a worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("Sheet1");

                // Styling
                var customStyle = xlPackage.Workbook.Styles.CreateNamedStyle("CustomStyle");

                customStyle.Style.Font.UnderLine = true;
                customStyle.Style.Font.Color.SetColor(Color.Red);

                // 1st row
                var record_id = 1;
                var startRow = 3;
                var row = startRow;

                worksheet.Cells["A1"].Value = "Thông tin tìm kiếm ";
                using (var r = worksheet.Cells[1, 1, 1, 7])
                {
                    r.Merge = true;
                    r.Style.Font.Color.SetColor(Color.Green);
                    r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(Color.Lavender);
                }

                worksheet.Cells["A2"].Value = "STT";
                worksheet.Cells["B2"].Value = "Đơn vị";
                worksheet.Cells["C2"].Value = "Số QĐ";
                worksheet.Cells["D2"].Value = "Thời điểm";
                worksheet.Cells["E2"].Value = "Mô tả";
                worksheet.Cells["F2"].Value = "Đơn vị tính";
                worksheet.Cells["G2"].Value = "Đơn giá";

                worksheet.Cells[2, 1, 2, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[2, 1, 2, 7].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

                row = 3;
                foreach (var item in model)
                {
                    worksheet.Cells[row, 1].Value = record_id++;
                    worksheet.Cells[row, 2].Value = item.Tendv;
                    worksheet.Cells[row, 3].Value = item.SoQD;
                    worksheet.Cells[row, 4].Value = Helpers.ConvertDateToStr(item.Thoidiem);
                    worksheet.Cells[row, 5].Value = item.Mota;
                    worksheet.Cells[row, 6].Value = item.Dvt;
                    worksheet.Cells[row, 7].Value = item.Dongia;
                  
                    row++;
                }

                // Define border styles
                var borderStyle = ExcelBorderStyle.Thin;
                var borderColor = Color.Black;

                // Apply border to header cells
                using (var headerRange = worksheet.Cells["A2:G2"])
                {
                    headerRange.Style.Border.Top.Style = borderStyle;
                    headerRange.Style.Border.Bottom.Style = borderStyle;
                    headerRange.Style.Border.Left.Style = borderStyle;
                    headerRange.Style.Border.Right.Style = borderStyle;
                    headerRange.Style.Border.Top.Color.SetColor(borderColor);
                    headerRange.Style.Border.Bottom.Color.SetColor(borderColor);
                    headerRange.Style.Border.Left.Color.SetColor(borderColor);
                    headerRange.Style.Border.Right.Color.SetColor(borderColor);
                }

                // Apply border to data cells
                for (int i = startRow; i < row; i++)
                {
                    var dataRange = worksheet.Cells[i, 1, i, 7];
                    dataRange.Style.Border.Top.Style = borderStyle;
                    dataRange.Style.Border.Bottom.Style = borderStyle;
                    dataRange.Style.Border.Left.Style = borderStyle;
                    dataRange.Style.Border.Right.Style = borderStyle;
                    dataRange.Style.Border.Top.Color.SetColor(borderColor);
                    dataRange.Style.Border.Bottom.Color.SetColor(borderColor);
                    dataRange.Style.Border.Left.Color.SetColor(borderColor);
                    dataRange.Style.Border.Right.Color.SetColor(borderColor);
                }

                xlPackage.Workbook.Properties.Title = "Thông tin tìm kiếm ";
               


                var helpers = new Helpers(worksheet);

                // Căn chỉnh, độ rộng ô,  màu sắc
                helpers.CanChinhExCel(1, ExcelHorizontalAlignment.Center, 10, Color.Black);
                helpers.CanChinhExCel(2, ExcelHorizontalAlignment.Center, 20, Color.Black);
                helpers.CanChinhExCel(3, ExcelHorizontalAlignment.Center, 20, Color.Black);
                helpers.CanChinhExCel(4, ExcelHorizontalAlignment.Center, 20, Color.Black);
                helpers.CanChinhExCel(5, ExcelHorizontalAlignment.Left, 70, Color.Black);
                helpers.CanChinhExCel(6, ExcelHorizontalAlignment.Center, 20, Color.Black);
                helpers.CanChinhExCel(7, ExcelHorizontalAlignment.Center, 20, Color.Black);
                helpers.CanChinhExCel(8, ExcelHorizontalAlignment.Right, 50, Color.Black);
                helpers.CanChinhExCel(7, ExcelHorizontalAlignment.Right, 20, Color.Black);

                xlPackage.Save();
            }

            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Thông tin tìm kiếm .xlsx");
        }


        [HttpPost("DinhGiaTGTC/GetListHoSo")]
        public JsonResult GetListHoSo(DateTime ngaytu, DateTime ngayden)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                var model = _db.GiaTroGiaTroCuoc.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && list_trangthai.Contains(t.Trangthai));
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
