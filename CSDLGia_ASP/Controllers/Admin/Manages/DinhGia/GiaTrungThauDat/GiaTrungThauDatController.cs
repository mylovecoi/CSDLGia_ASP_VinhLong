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
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaTrungThauDat
{
    public class GiaTrungThauDatController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IDsDiaBanService _IDsDiaBan;
        private readonly IDsDonviService _dsDonviService;
        private readonly ITrangThaiHoSoService _trangThaiHoSoService;

        public GiaTrungThauDatController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment, IDsDiaBanService IDsDiaBan, IDsDonviService dsDonviService, ITrangThaiHoSoService trangThaiHoSoService)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
            _IDsDiaBan = IDsDiaBan;
            _dsDonviService = dsDonviService;
            _trangThaiHoSoService = trangThaiHoSoService;
        }

        [Route("GiaTrungThauDat")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam = "all", string MaDiaBan = "all")
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.trungthaudat.thongtin", "Index"))
                {
                    Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;
                    var model_donvi = _dsDonviService.GetListDonvi(Helpers.GetSsAdmin(HttpContext.Session, "Madv"));
                    List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();

                    var model = from dat in _db.GiaDauGiaDat
                                join diaban in _db.DsDiaBan on dat.Madiaban equals diaban.MaDiaBan
                                join donvi in _db.DsDonVi on dat.Madv equals donvi.MaDv
                                select new GiaDauGiaDat
                                {
                                    Id = dat.Id,
                                    Tenduan = dat.Tenduan,
                                    Thoidiem = dat.Thoidiem,
                                    Mahs = dat.Mahs,
                                    Trangthai = dat.Trangthai,
                                    Madiaban = dat.Madiaban,
                                    TenDiaBan = diaban.TenDiaBan,
                                    Macqcq = dat.Macqcq,
                                    Madv = dat.Madv,
                                    TenDonVi = donvi.TenDv,
                                    Soqddaugia = dat.Soqddaugia,
                                };

                    if (Madv != "all")
                    {
                        model = model.Where(t => t.Madv == Madv);
                    }
                    if (Nam != "all")
                    {
                        model = model.Where(x => x.Thoidiem.Year == Convert.ToInt32(Nam));
                    }
                    if (MaDiaBan != "all")
                    {
                        model = model.Where(x => x.Madiaban == MaDiaBan);
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
                    ViewData["DsDonvi"] = model_donvi;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(x => x.Level != "X");
                    //ViewData["DsDonViTh"] = dsDonViTH;
                    ViewData["Madv"] = Madv;
                    ViewData["Nam"] = Nam;
                    ViewData["MaDiaBan"] = MaDiaBan;
                    //ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level == "H");
                    //ViewData["DsCqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    //ViewData["ADMIN"] = _db.DsDiaBan.Where(t => t.Level == "ADMIN");
                    ViewData["Title"] = " Thông tin hồ sơ giá trúng thầu quyền sử dụng đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgd";
                    ViewData["MenuLv3"] = "menu_giadgd_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauDat/DanhSach/Index.cshtml", model);
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


        [Route("GiaTrungThauDat/Create")]
        [HttpGet]
        public IActionResult Create(string Madv, string MaDiaBan)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.trungthaudat.thongtin", "Create"))
                {
                    this.RemoveData_Ct_CXD(Madv);
                    var model = new GiaDauGiaDat
                    {
                        Madv = Madv,
                        Thoidiem = DateTime.Now,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Madiaban = MaDiaBan,
                    };

                    //var dsdv = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI" && t.MaDv == Madv).OrderBy(t => t.Id).Select(t => t.MaDv).First();
                    //ViewData["Huyens"] = _db.Districts;
                    //ViewData["Xas"] = new List<Towns>();
                    var diaban = _db.DsDiaBan.FirstOrDefault(x => x.MaDiaBan == MaDiaBan);

                    var DsXaPhuong = _IDsDiaBan.GetListDsDiaBan(MaDiaBan);

                    ViewData["TenDiaBan"] = _db.DsDiaBan.FirstOrDefault(x => x.MaDiaBan == MaDiaBan).TenDiaBan;
                    ViewData["DsXaPhuong"] = DsXaPhuong;
                    ViewData["DmDvt"] = _db.DmDvt.ToList();
                    ViewData["TenDonVi"] = _db.DsDonVi.First(x => x.MaDv == Madv).TenDv;
                    ViewData["Xas"] = _db.DsXaPhuong.Where(x => x.Madiaban == MaDiaBan);
                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Title"] = " Thông tin hồ sơ giá trúng thầu quyền sử dụng đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgd";
                    ViewData["MenuLv3"] = "menu_giadgd_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauDat/DanhSach/Create.cshtml", model);
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


        [Route("GiaTrungThauDat/Store")]
        [HttpPost]
        public IActionResult Store(GiaDauGiaDat request)
        {

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.trungthaudat.thongtin", "Create"))
                {
                    var model = new GiaDauGiaDat
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Thoidiem = request.Thoidiem,
                        Tenduan = request.Tenduan,
                        Soqdpagia = request.Soqdpagia,
                        Soqddaugia = request.Soqddaugia,
                        Soqdgiakhoidiem = request.Soqdgiakhoidiem,
                        Soqdkqdaugia = request.Soqdkqdaugia,
                        Thongtin = request.Thongtin,
                        Phanloai = request.Phanloai,
                        Trangthai = "CC",
                        Congbo = "CHUACONGBO",
                        Ipf1 = request.Ipf1,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                        Madiaban = request.Madiaban,
                        //MaHuyen=request.MaHuyen,
                    };
                    _db.GiaDauGiaDat.Add(model);
                    this.SaveData_Ct_CXD(model.Mahs);
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Thêm mới");
                    _db.SaveChanges();
                    return RedirectToAction("Index", "GiaTrungThauDat", new { Madb = request.Madiaban });
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


        [Route("GiaTrungThauDat/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.trungthaudat.thongtin", "Edit"))
                {
                    var model = _db.GiaDauGiaDat.FirstOrDefault(t => t.Mahs == Mahs);

                    var model_new = new GiaDauGiaDat
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        Thoidiem = model.Thoidiem,
                        Thongtin = model.Thongtin,
                        Tenduan = model.Tenduan,
                        Soqddaugia = model.Soqddaugia,
                        Soqdgiakhoidiem = model.Soqdgiakhoidiem,
                        Soqdkqdaugia = model.Soqdkqdaugia,
                        Soqdpagia = model.Soqdpagia,
                        Phanloai = model.Phanloai,
                        Madiaban = model.Madiaban,
                    };
                    var model_ct = _db.GiaDauGiaDatCt.Where(t => t.Mahs == model_new.Mahs);
                    model_new.GiaDauGiaDatCt = model_ct.ToList();
                    var giayto = _db.ThongTinGiayTo.Where(x => x.Mahs == model.Mahs);
                    model_new.ThongTinGiayTo = giayto.ToList();

                    var diaban = _db.DsDiaBan.FirstOrDefault(x => x.MaDiaBan == model.Madiaban);

                    var DsXaPhuong = _IDsDiaBan.GetListDsDiaBan(model.Madiaban);

                    ViewData["TenDiaBan"] = _db.DsDiaBan.FirstOrDefault(x => x.MaDiaBan == model.Madiaban).TenDiaBan;
                    ViewData["DsXaPhuong"] = DsXaPhuong;

                    ViewData["DmDvt"] = _db.DmDvt.ToList();
                    ViewData["Xas"] = _db.DsXaPhuong.Where(x => x.Madiaban == model.Madiaban);
                    ViewData["Maxp"] = model.Maxp;
                    ViewData["Madb"] = model.Madiaban;
                    ViewData["Madv"] = model.Madv;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI").ToList();
                    ViewData["Title"] = " Thông tin hồ sơ giá Trúng thầu quyền sử dụng đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgd";
                    ViewData["MenuLv3"] = "menu_giadgd_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauDat/DanhSach/Edit.cshtml", model_new);
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

        [Route("GiaTrungThauDat/Update")]
        [HttpPost]
        public IActionResult Update(GiaDauGiaDat request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.trungthaudat.thongtin", "Edit"))
                {
                    var model = _db.GiaDauGiaDat.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Tenduan = request.Tenduan;
                    model.Thoidiem = request.Thoidiem;
                    model.Thongtin = request.Thongtin;
                    model.Soqdpagia = request.Soqdpagia;
                    model.Soqddaugia = request.Soqddaugia;
                    model.Soqdkqdaugia = request.Soqdkqdaugia;
                    model.Soqdgiakhoidiem = request.Soqdgiakhoidiem;
                    model.Phanloai = request.Phanloai;
                    model.Ipf1 = request.Ipf1;
                    model.Updated_at = DateTime.Now;
                    //model.MaHuyen = request.MaHuyen;                    
                    _db.GiaDauGiaDat.Update(model);
                    this.SaveData_Ct_CXD(model.Mahs);
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Cập nhật");
                    _db.SaveChanges();
                    return RedirectToAction("Index", "GiaTrungThauDat", new { Madb = request.Madiaban });
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
        [Route("GiaTrungThauDat/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.trungthaudat.thongtin", "Delete"))
                {
                    var model = _db.GiaDauGiaDat.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaDauGiaDat.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.GiaDauGiaDatCt.Where(t => t.Mahs == model.Mahs);
                    if (model_ct.Any())
                    {
                        _db.GiaDauGiaDatCt.RemoveRange(model_ct);
                    }
                    // xóa thông tin giấy tờ 
                    var model_file_remove = _db.ThongTinGiayTo.Where(t => t.Mahs == model.Mahs);
                    if (model_file_remove.Any())
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        foreach (var file in model_file_remove)
                        {
                            string path_del = Path.Combine(wwwRootPath + "/UpLoad/File/ThongTinGiayTo/", file.FileName);
                            FileInfo fi = new FileInfo(path_del);
                            if (fi != null)
                            {
                                System.IO.File.Delete(path_del);
                                fi.Delete();
                            }
                        }
                        _db.ThongTinGiayTo.RemoveRange(model_file_remove);
                    }
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaTrungThauDat", new { Madb = model.Madiaban });
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

        [Route("GiaTrungThauDat/Chuyen")]
        [HttpPost]
        public IActionResult Chuyen(string mahs_complete, string trangthai_complete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.trungthaudat.thongtin", "Index"))
                {
                    var model = _db.GiaDauGiaDat.FirstOrDefault(p => p.Mahs == mahs_complete);
                    model.Trangthai = trangthai_complete;
                    model.Updated_at = DateTime.Now;

                    _db.GiaDauGiaDat.Update(model);
                    _db.SaveChanges();
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), trangthai_complete);
                    return RedirectToAction("Index", "GiaTrungThauDat", new { Madv = model.Madv, Nam = model.Thoidiem.Year });
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

        [Route("GiaTrungThauDat/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.trungthaudat.thongtin", "Index"))
                {
                    var model = _db.GiaDauGiaDat.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new GiaDauGiaDat
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        Madiaban = model.Madiaban,
                        Thoidiem = model.Thoidiem,
                        Thongtin = model.Thongtin,
                        Tenduan = model.Tenduan,
                        Soqddaugia = model.Soqddaugia,
                        Soqdgiakhoidiem = model.Soqdgiakhoidiem,
                        Soqdkqdaugia = model.Soqdkqdaugia,
                        Soqdpagia = model.Soqdpagia,
                        Phanloai = model.Phanloai,
                        Maxp = model.Maxp,

                    };
                    var model_ct = from gdCt in _db.GiaDauGiaDatCt.Where(t => t.Mahs == Mahs)
                                   join diaban in _db.DsDiaBan on gdCt.MaDiaBan equals diaban.MaDiaBan
                                   select new GiaDauGiaDatCt()
                                   {
                                       Loaidat = gdCt.Loaidat,
                                       Khuvuc = gdCt.Khuvuc,
                                       Mota = gdCt.Mota,
                                       Dientich = gdCt.Dientich,
                                       Giakhoidiem = gdCt.Giakhoidiem,
                                       Giadaugia = gdCt.Giadaugia,
                                       Giasddat = gdCt.Giasddat,
                                       Mahs = gdCt.Mahs,
                                       Solo = gdCt.Solo,
                                       Sothua = gdCt.Sothua,
                                       Tobanbo = gdCt.Tobanbo,
                                       Dvt = gdCt.Dvt,
                                       Sotobanbo = gdCt.Sotobanbo,
                                       TrangThai = gdCt.TrangThai,
                                       MaDv = gdCt.MaDv,
                                       MaDiaBan = gdCt.MaDiaBan,
                                       MaDiaBanCapHuyen = _db.DsDiaBan.FirstOrDefault(x => x.MaDiaBan == gdCt.MaDiaBan).MaDiaBanCq,
                                       TenDiaBan = diaban.TenDiaBan,
                                   };
                    model_new.GiaDauGiaDatCt = model_ct.ToList();
                    var donvi = _db.DsDonVi.FirstOrDefault(t => t.MaDv == model.Macqcq);
                    ViewData["TenDiaBan"] = _db.DsDiaBan.FirstOrDefault(x => x.MaDiaBan == model.Madiaban).TenDiaBan;
                    ViewData["TenTinh"] = _db.DsDiaBan.FirstOrDefault(x => string.IsNullOrEmpty(x.MaDiaBanCq)).TenDiaBan;
                    ViewData["DsDiaBanHuyen"] = _db.DsDiaBan.Where(x => x.Level == "H");
                    ViewData["DsDiaBanXa"] = _db.DsDiaBan.Where(x => x.Level == "X");
                    ViewData["TenDonVi"] = donvi != null ? donvi.TenDv : "";
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauDat/DanhSach/Show.cshtml", model_new);
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
        [Route("GiaTrungThauDat/Search")]
        [HttpGet]
        public IActionResult Search(DateTime beginTime, DateTime endTime,
            double Giakhoidiem_den, double Giakhoidiem_tu, double Giadaugia_tu, double Giadaugia_den, string Soqd = "all", string PhanLoai = "all", string MaDiaBan = "all")
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.trungthaudat.timkiem", "Index"))
                {
                    beginTime = beginTime == DateTime.MinValue ? new DateTime(DateTime.Now.Year, 01, 01) : beginTime;
                    endTime = endTime == DateTime.MinValue ? new DateTime(DateTime.Now.Year, 12, 31) : endTime;
                    var model_join = from dgct in _db.GiaDauGiaDatCt
                                     join dg in _db.GiaDauGiaDat on dgct.Mahs equals dg.Mahs
                                     select new GiaDauGiaDatCt
                                     {
                                         Id = dgct.Id,
                                         Mahs = dgct.Mahs,
                                         Mota = dgct.Mota,
                                         Solo = dgct.Solo,
                                         Sothua = dgct.Sothua,
                                         Tobanbo = dgct.Tobanbo,
                                         Dientich = dgct.Dientich,
                                         Dvt = dgct.Dvt,
                                         Giakhoidiem = dgct.Giakhoidiem,
                                         Giadaugia = dgct.Giadaugia,
                                         ThoiDiem = dg.Thoidiem,
                                         SoQuyetDinh = dg.Soqddaugia,
                                         PhanLoai = dg.Phanloai,
                                         TrangThai = dg.Trangthai,
                                         TenDuAn = dg.Tenduan,
                                         MaDv = dg.Madv,
                                         MaDiaBan = dgct.MaDiaBan,


                                     };
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model_join = model_join.Where(x => x.ThoiDiem >= beginTime && x.ThoiDiem <= endTime && list_trangthai.Contains(x.TrangThai));
                    if (Soqd != "all")
                    {
                        model_join = model_join.Where(t => t.Mahs == Soqd);
                    }
                    if (Giakhoidiem_tu != 0)
                    {
                        model_join = model_join.Where(t => t.Giakhoidiem >= Giakhoidiem_tu);
                    }
                    if (Giakhoidiem_den != 0)
                    {
                        model_join = model_join.Where(t => t.Giakhoidiem <= Giakhoidiem_den);
                    }
                    if (Giadaugia_tu != 0)
                    {
                        model_join = model_join.Where(t => t.Giadaugia >= Giadaugia_tu);
                    }
                    if (Giadaugia_den != 0)
                    {
                        model_join = model_join.Where(t => t.Giadaugia <= Giadaugia_den);
                    }
                    if (PhanLoai != "all")
                    {
                        model_join = model_join.Where(t => t.PhanLoai == PhanLoai);
                    }

                    if (MaDiaBan != "all")
                    {

                        var diaban_search = _dsDonviService.GetListDonvi(MaDiaBan);
                        List<string> list_diaban_search = diaban_search.Select(t => t.MaDiaBan).ToList();
                        model_join = model_join.Where(t => list_diaban_search.Contains(t.MaDiaBan));
                    }

                    ViewData["ListQuyetDinh"] = _db.GiaDauGiaDat;
                    ViewData["QuyetDinh"] = Soqd;

                    ViewData["DsDiaBan"] = _db.DsDiaBan;
                    ViewData["DsDiaBanXaPhuong"] = _db.DsDiaBan;
                    ViewData["MaDiaBan"] = MaDiaBan;

                    ViewData["Phanloai"] = PhanLoai;
                    ViewData["beginTime"] = beginTime;
                    ViewData["endTime"] = endTime;
                    ViewData["Giakhoidiem_den"] = Giakhoidiem_den;
                    ViewData["Giakhoidiem_tu"] = Giakhoidiem_tu;
                    ViewData["Giadaugia_tu"] = Giadaugia_tu;
                    ViewData["Giadaugia_den"] = Giadaugia_den;
                    ViewData["MaDiaBan"] = MaDiaBan;

                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Title"] = " Thông tin hồ sơ giá trúng thầu quyền sd đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgd";
                    ViewData["MenuLv3"] = "menu_giadgd_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauDat/TimKiem/Index.cshtml", model_join);
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
        [Route("GiaTrungThauDat/PrintSearch")]
        [HttpPost]
        public IActionResult PrintSearch(DateTime beginTime, DateTime endTime,
            double Giakhoidiem_den, double Giakhoidiem_tu, double Giadaugia_tu, double Giadaugia_den, string Soqd = "all", string PhanLoai = "all", string MaDiaBan = "all")
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.trungthaudat.timkiem", "Index"))
                {
                    beginTime = beginTime == DateTime.MinValue ? new DateTime(DateTime.Now.Year, 01, 01) : beginTime;
                    endTime = endTime == DateTime.MinValue ? new DateTime(DateTime.Now.Year, 12, 31) : endTime;
                    var model_join = from dgct in _db.GiaDauGiaDatCt
                                     join dg in _db.GiaDauGiaDat on dgct.Mahs equals dg.Mahs
                                     join diaban in _db.DsDiaBan on dg.Madiaban equals diaban.MaDiaBan
                                     select new GiaDauGiaDatCt
                                     {
                                         Id = dgct.Id,
                                         Mahs = dgct.Mahs,
                                         Mota = dgct.Mota,
                                         Solo = dgct.Solo,
                                         Sothua = dgct.Sothua,
                                         Tobanbo = dgct.Tobanbo,
                                         Dientich = dgct.Dientich,
                                         Dvt = dgct.Dvt,
                                         Giakhoidiem = dgct.Giakhoidiem,
                                         Giadaugia = dgct.Giadaugia,
                                         ThoiDiem = dg.Thoidiem,
                                         SoQuyetDinh = dg.Soqddaugia,
                                         PhanLoai = dg.Phanloai,
                                         TrangThai = dg.Trangthai,
                                         TenDuAn = dg.Tenduan,
                                         MaDv = dg.Madv,
                                         MaDiaBan = dgct.MaDiaBan,
                                         Maxp = dgct.MaDiaBan,
                                         MaDiaBanCapHuyen = _db.DsDiaBan.FirstOrDefault(x => x.MaDiaBan == dgct.MaDiaBan).MaDiaBanCq,
                                         TenDiaBan = diaban.TenDiaBan,
                                     };
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model_join = model_join.Where(x => x.ThoiDiem >= beginTime && x.ThoiDiem <= endTime && list_trangthai.Contains(x.TrangThai));
                    if (Soqd != "all")
                    {
                        model_join = model_join.Where(t => t.Mahs == Soqd);
                    }
                    if (MaDiaBan != "all")
                    {
                        var diaban_search = _dsDonviService.GetListDonvi(MaDiaBan);
                        List<string> list_diaban_search = diaban_search.Select(t => t.MaDiaBan).ToList();
                        model_join = model_join.Where(t => list_diaban_search.Contains(t.MaDiaBan));
                    }
                    if (Giakhoidiem_tu != 0)
                    {
                        model_join = model_join.Where(t => t.Giakhoidiem >= Giakhoidiem_tu);
                    }
                    if (Giakhoidiem_den != 0)
                    {
                        model_join = model_join.Where(t => t.Giakhoidiem <= Giakhoidiem_den);
                    }
                    if (Giadaugia_tu != 0)
                    {
                        model_join = model_join.Where(t => t.Giadaugia >= Giadaugia_tu);
                    }
                    if (Giadaugia_den != 0)
                    {
                        model_join = model_join.Where(t => t.Giadaugia <= Giadaugia_den);
                    }
                    if (PhanLoai != "all")
                    {
                        model_join = model_join.Where(t => t.PhanLoai == PhanLoai);
                    }

                    ViewData["TenTinh"] = _db.DsDiaBan.FirstOrDefault(x => string.IsNullOrEmpty(x.MaDiaBanCq)).TenDiaBan;
                    ViewData["DsDiaBanHuyen"] = _db.DsDiaBan.Where(x => x.Level == "H");
                    ViewData["DsDiaBanXa"] = _db.DsDiaBan.Where(x => x.Level == "X");
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauDat/TimKiem/PrintSearch.cshtml", model_join);
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
            // xóa giá giá trúng thầu đất chi tiết chưa xác định
            var check = _db.GiaDauGiaDatCt.Where(t => t.MaDv == maDv && t.TrangThai == "CXD");
            if (check.Any())
            {
                _db.GiaDauGiaDatCt.RemoveRange(check);
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
            // Lưu lại giá trúng thầu đất chi tiết chưa xác định
            var modelCt_cxd = _db.GiaDauGiaDatCt.Where(x => x.Mahs == maHs && x.TrangThai == "CXD").ToList();
            if (modelCt_cxd.Any())
            {
                modelCt_cxd.ForEach(x => x.TrangThai = "XD");
            }
            // Lưu lại giấy tờ chưa xác định
            var giayto_cxd = _db.ThongTinGiayTo.Where(x => x.Mahs == maHs && x.Status == "CXD").ToList();
            if (giayto_cxd.Any())
            {
                giayto_cxd.ForEach(x => x.Status = "XD");
            }
            _db.SaveChanges();
        }
    }
}
