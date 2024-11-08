﻿using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.Services;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaKhungGiaDat
{
    public class GiaKhungGiaDatController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IDsDonviService _dsDonviService;
        private readonly ITrangThaiHoSoService _trangThaiHoSoService;

        public GiaKhungGiaDatController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment, IDsDonviService dsDonviService, ITrangThaiHoSoService trangThaiHoSoService)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
            _dsDonviService = dsDonviService;
            _trangThaiHoSoService = trangThaiHoSoService;
        }

        [Route("GiaKhungGiaDat/DanhSach")]
        [HttpGet]
        public IActionResult Index(int Nam, string Madv = "all")
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.khunggd.thongtin", "Index"))
                {
                    Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

                    var model_donvi = _dsDonviService.GetListDonvi(Helpers.GetSsAdmin(HttpContext.Session, "Madv"));
                    List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();

                    var model = _db.GiaKhungGiaDat.Where(t => list_madv.Contains(t.Madv));
                    if (Nam != 0)
                    {
                        model = model.Where(t => t.Thoidiem.Year == Nam);
                    }

                    if (Madv != "all")
                    {
                        model = model.Where(t => t.Madv == Madv);
                    }
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["DsDonVi"] = model_donvi;
                    ViewData["Nam"] = Nam;
                    ViewData["Madv"] = Madv;
                    ViewData["Title"] = "Thông tin giá khung giá đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgkhunggd";
                    ViewData["MenuLv3"] = "menu_dgkhunggd_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaKhungGiaDat/DanhSach/Index.cshtml", model);

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

        [Route("GiaKhungGiaDat/Create")]
        [HttpGet]
        public IActionResult Create(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.khunggd.thongtin", "Create"))
                {
                    this.RemoveData_Ct_CXD(Madv);
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaKhungGiaDat
                    {
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Madv = Madv,
                        PhanLoaiHoSo = "HOSOCHITIET",

                    };

                    ViewData["Madv"] = Madv;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["Title"] = "Bảng giá khung giá đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgkhunggd";
                    ViewData["MenuLv3"] = "menu_dgkhunggd_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaKhungGiaDat/DanhSach/Create.cshtml", model);

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

        [Route("GiaKhungGiaDat/NhanExcel")]
        [HttpGet]
        public IActionResult NhanExcel(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.khunggd.thongtin", "Create"))
                {
                    var check = _db.GiaKhungGiaDatCt.Where(t => t.Trangthai == "CXD");
                    if (check != null)
                    {
                        _db.GiaKhungGiaDatCt.RemoveRange(check);
                        _db.SaveChanges();
                    }

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaKhungGiaDat
                    {
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Madv = Madv,
                        PhanLoaiHoSo = "HOSONHANEXCEL",

                    };

                    ViewData["codeExcel"] = model.CodeExcel;//Gán ra view để dùng chung
                    ViewData["Title"] = "Bảng giá khung giá đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgkhunggd";
                    ViewData["MenuLv3"] = "menu_dgkhunggd_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaKhungGiaDat/DanhSach/NhanExcel.cshtml", model);

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

        [Route("GiaKhungGiaDat/Store")]
        [HttpPost]
        public IActionResult Store(CSDLGia_ASP.Models.Manages.DinhGia.GiaKhungGiaDat request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.khunggd.thongtin", "Create"))
                {

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaKhungGiaDat
                    {
                        Mahs = request.Mahs,
                        Madiaban = request.Madiaban,
                        Maxp = request.Maxp,
                        Madv = request.Madv,
                        Macqcq = request.Madv,
                        Thoidiem = request.Thoidiem,
                        Tieude = request.Tieude,
                        Dvbanhanh = request.Dvbanhanh,
                        Kyhieuvb = request.Kyhieuvb,
                        Ngayapdung = request.Ngayapdung,
                        Ghichu = request.Ghichu,
                        PhanLoaiHoSo = request.PhanLoaiHoSo,
                        CodeExcel = request.CodeExcel,
                        Trangthai = "CC",
                        Congbo = "CHUACONGBO",
                        Ipf1 = request.Ipf1,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaKhungGiaDat.Add(model);
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Thêm mới");
                    _db.SaveChanges();
                    return RedirectToAction("Index", "GiaKhungGiaDat", new { request.Madv });
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

        [Route("GiaKhungGiaDat/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.khunggd.thongtin", "Edit"))
                {

                    var model = _db.GiaKhungGiaDat.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_ct = _db.GiaKhungGiaDatCt.Where(t => t.Mahs == model.Mahs);
                    model.GiaKhungGiaDatCt = model_ct.ToList();
                    var model_hosogiayto = _db.ThongTinGiayTo.Where(x => x.Mahs == Mahs);
                    model.ThongTinGiayTo = model_hosogiayto.ToList();
                    ViewData["Title"] = "Bảng giá khung giá đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgkhunggd";
                    ViewData["MenuLv3"] = "menu_dgkhunggd_tt";

                    return View("Views/Admin/Manages/DinhGia/GiaKhungGiaDat/DanhSach/Edit.cshtml", model);

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

        [Route("GiaKhungGiaDat/Update")]
        [HttpPost]
        public IActionResult Update(CSDLGia_ASP.Models.Manages.DinhGia.GiaKhungGiaDat request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.khunggd.thongtin", "Edit"))
                {
                    var model = _db.GiaKhungGiaDat.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Madiaban = request.Madiaban;
                    model.Maxp = request.Maxp;
                    model.Thoidiem = request.Thoidiem;
                    model.Ngayapdung = request.Ngayapdung;
                    model.Tieude = request.Tieude;
                    model.Dvbanhanh = request.Dvbanhanh;
                    model.Kyhieuvb = request.Kyhieuvb;
                    model.Ghichu = request.Ghichu;
                    model.Ipf1 = request.Ipf1;
                    model.Updated_at = DateTime.Now;
                    _db.GiaKhungGiaDat.Update(model);
                    this.SaveData_Ct_CXD(model.Mahs);
                    _db.SaveChanges();
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Cập nhật");
                    return RedirectToAction("Index", "GiaKhungGiaDat", new { Madv = request.Madv, Nam = request.Thoidiem.Year });
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

        [Route("GiaKhungGiaDat/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.khunggd.thongtin", "Delete"))
                {
                    var model = _db.GiaKhungGiaDat.FirstOrDefault(t => t.Id == id_delete);
                    var model_ct = _db.GiaKhungGiaDatCt.Where(t => t.Mahs == model.Mahs);
                    if (model_ct.Any())
                    {
                        _db.GiaKhungGiaDatCt.RemoveRange(model_ct);
                    }

                    // xóa thông tin giấy tờ chưa lưu lại
                    var model_file_cxd = _db.ThongTinGiayTo.Where(t => t.Mahs == model.Mahs);
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
                    }
                    _db.GiaKhungGiaDat.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaKhungGiaDat", new { Madv = model.Madv, Nam = model.Thoidiem.Year });
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

        [Route("GiaKhungGiaDat/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.khunggd.thongtin", "Index"))
                {
                    var model = _db.GiaKhungGiaDat.FirstOrDefault(t => t.Mahs == Mahs);
                    model.GiaKhungGiaDatCt = _db.GiaKhungGiaDatCt.Where(t => t.Mahs == model.Mahs).ToList();
                    var DonVi = _db.DsDonVi.First(x => x.MaDv == model.Madv);

                    ViewData["Title"] = "Bảng giá khung giá đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgkhunggd";
                    ViewData["MenuLv3"] = "menu_dgkhunggd_tt";
                    ViewData["TenDonVi"] = DonVi.TenDv;
                    ViewData["TenDiaBan"] = _db.DsDiaBan.First(x => x.MaDiaBan == DonVi.MaDiaBan).TenDiaBan;
                    return View("Views/Admin/Manages/DinhGia/GiaKhungGiaDat/DanhSach/Show.cshtml", model);
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

        [Route("GiaKhungGiaDat/Complete")]
        [HttpPost]
        public IActionResult Complete(string mahs_complete, string trangthai_complete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.khunggd.thongtin", "Index"))
                {
                    var model = _db.GiaKhungGiaDat.FirstOrDefault(t => t.Mahs == mahs_complete);

                    model.Trangthai = trangthai_complete;
                    model.Updated_at = DateTime.Now;
                    _db.GiaKhungGiaDat.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "GiaKhungGiaDat", new { Madv = model.Madv, Nam = model.Thoidiem.Year });
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

        [Route("GiaKhungGiaDat/Search")]
        [HttpGet]
        public IActionResult Search(string madv, DateTime ngaynhap_tu, DateTime ngaynhap_den, string Vungkt, string SoQuyetDinh = "all")
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.khunggd.timkiem", "Index"))
                {
                    DateTime nowDate = DateTime.Now;
                    DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
                    DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);
                    madv = string.IsNullOrEmpty(madv) ? "all" : madv;
                    ngaynhap_tu = ngaynhap_tu == DateTime.MinValue ? firstDayCurrentYear : ngaynhap_tu;
                    ngaynhap_den = ngaynhap_den == DateTime.MinValue ? lastDayCurrentYear : ngaynhap_den;

                    var model = (from giakgdct in _db.GiaKhungGiaDatCt
                                 join giakgd in _db.GiaKhungGiaDat on giakgdct.Mahs equals giakgd.Mahs
                                 join donvi in _db.DsDonVi on giakgd.Madv equals donvi.MaDv
                                 select new GiaKhungGiaDatCt
                                 {
                                     Id = giakgdct.Id,
                                     Vungkt = giakgdct.Vungkt,
                                     Mahs = giakgdct.Mahs,
                                     Madv = giakgd.Madv,
                                     Thoidiem = giakgd.Thoidiem,
                                     Tendv = donvi.TenDv,
                                     SoQD = giakgd.Kyhieuvb,
                                     Trangthai = giakgd.Trangthai,
                                     Giattdb = giakgdct.Giattdb,
                                     Giatddb = giakgdct.Giatddb,
                                     Giatdmn = giakgdct.Giatdmn,
                                     Giatdtd = giakgdct.Giatdtd,
                                     Giattmn = giakgdct.Giattmn,
                                     Giatttd = giakgdct.Giatttd

                                 });
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model = model.Where(x => x.Thoidiem >= ngaynhap_tu && x.Thoidiem <= ngaynhap_den && list_trangthai.Contains(x.Trangthai));


                    if (SoQuyetDinh != "all")
                    {
                        model = model.Where(x => x.SoQD == SoQuyetDinh);

                    }
                    if (madv != "all")
                    {
                        model = model.Where(t => t.Madv == madv);

                    }
                    if (!string.IsNullOrEmpty(Vungkt))
                    {
                        model = model.Where(x => x.Vungkt.ToLower().Contains(Vungkt.ToLower()));
                    }
                    ViewData["GiaKhungGiaDat"] = _db.GiaKhungGiaDat;
                    ViewData["MaDonVi"] = madv;
                    ViewData["VungKt"] = Vungkt;
                    ViewData["SoQuyetDinh"] = SoQuyetDinh;
                    ViewData["NgayNhapTu"] = ngaynhap_tu;
                    ViewData["NgayNhapDen"] = ngaynhap_den;
                    ViewData["DsDiaBan"] = _db.DsDiaBan;
                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá khung giá đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgkhunggd";
                    ViewData["MenuLv3"] = "menu_dgkhunggd_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaKhungGiaDat/TimKiem/Index.cshtml", model);

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
        [Route("GiaKhungGiaDat/Result")]
        [HttpPost]
        public IActionResult Result(string madv, string Vungkt, DateTime ngaynhap_tu, DateTime ngaynhap_den, string SoQuyetDinh = "all")
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.khunggd.timkiem", "Index"))
                {
                    DateTime nowDate = DateTime.Now;
                    DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
                    DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);
                    madv = string.IsNullOrEmpty(madv) ? "all" : madv;
                    ngaynhap_tu = ngaynhap_tu == DateTime.MinValue ? firstDayCurrentYear : ngaynhap_tu;
                    ngaynhap_den = ngaynhap_den == DateTime.MinValue ? lastDayCurrentYear : ngaynhap_den;
                    var model = (from giakgdct in _db.GiaKhungGiaDatCt
                                 join giakgd in _db.GiaKhungGiaDat on giakgdct.Mahs equals giakgd.Mahs
                                 join donvi in _db.DsDonVi on giakgd.Madv equals donvi.MaDv
                                 select new GiaKhungGiaDatCt
                                 {
                                     Id = giakgdct.Id,
                                     Vungkt = giakgdct.Vungkt,
                                     Mahs = giakgdct.Mahs,
                                     Madv = giakgd.Madv,
                                     Thoidiem = giakgd.Thoidiem,
                                     Tendv = donvi.TenDv,
                                     SoQD = giakgd.Kyhieuvb,
                                     Trangthai = giakgd.Trangthai,
                                     Giattdb = giakgdct.Giattdb,
                                     Giatddb = giakgdct.Giatddb,
                                     Giatdmn = giakgdct.Giatdmn,
                                     Giatdtd = giakgdct.Giatdtd,
                                     Giattmn = giakgdct.Giattmn,
                                     Giatttd = giakgdct.Giatttd

                                 });
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model = model.Where(x => x.Thoidiem >= ngaynhap_tu && x.Thoidiem <= ngaynhap_den && list_trangthai.Contains(x.Trangthai));

                    if (SoQuyetDinh != "all")
                    {
                        model = model.Where(x => x.SoQD == SoQuyetDinh);

                    }
                    if (madv != "all")
                    {
                        model = model.Where(t => t.Madv == madv);

                    }
                    if (!string.IsNullOrEmpty(Vungkt))
                    {
                        model = model.Where(x => x.Vungkt.Contains(Vungkt));
                    }
                    return View("Views/Admin/Manages/DinhGia/GiaKhungGiaDat/TimKiem/Result.cshtml", model);

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
        private void RemoveData_Ct_CXD(string maDv)
        {
            // xóa giá khung giá đất chi tiết chưa xác định
            var check = _db.GiaKhungGiaDatCt.Where(t => t.Madv == maDv && t.Trangthai == "CXD");
            if (check.Any())
            {
                _db.GiaKhungGiaDatCt.RemoveRange(check);
            }
            // xóa thông tin giấy tờ chưa lưu lại
            var model_file_cxd = _db.ThongTinGiayTo.Where(t => t.Status == "CXD" && t.Madv == maDv);
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
            }
            _db.SaveChanges();
        }
        private void SaveData_Ct_CXD(string maHs)
        {
            // Lưu lại hồ sơ chi tiết chưa xác định
            var modelct = _db.GiaKhungGiaDatCt.Where(t => t.Mahs == maHs && t.Trangthai == "CXD");
            if (modelct.Any())
            {
                foreach (var item in modelct)
                {
                    item.Trangthai = "XD";
                }
            }
            // Lưu lại giấy tờ chưa xác định
            var hosogiayto = _db.ThongTinGiayTo.Where(t => t.Mahs == maHs && t.Status == "CXD");
            if (hosogiayto.Any())
            {
                foreach (var item in hosogiayto)
                {
                    item.Status = "XD";
                }
            }
            _db.SaveChanges();
        }

    }
}
