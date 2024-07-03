using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.Services;
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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDatPl
{
    public class GiaDatPlController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IDsDiaBanService _IDsDiaBan;
        private readonly IDsDonviService _dsDonviService;
        private readonly ITrangThaiHoSoService _trangThaiHoSoService;

        public GiaDatPlController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment, IDsDiaBanService IDsDiaBan, IDsDonviService dsDonviService, ITrangThaiHoSoService trangThaiHoSoService)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
            _IDsDiaBan = IDsDiaBan;
            _dsDonviService = dsDonviService;
            _trangThaiHoSoService = trangThaiHoSoService;
        }

        [Route("GiaDatCuThe")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam = "all")
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthe.thongtin", "Index"))
                {
                    Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

                    var model_donvi = _dsDonviService.GetListDonvi(Helpers.GetSsAdmin(HttpContext.Session, "Madv"));
                    List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();

                    var model = _db.GiaDatPhanLoai.Where(t => list_madv.Contains(t.Madv));
                    if (Madv != "all")
                    {
                        model = model.Where(t => t.Madv == Madv);
                    }
                    if (Nam != "all")
                    {
                        model = model.Where(x => x.Thoidiem.Year == Convert.ToInt32(Nam));
                    }
                        ViewData["DsDonVi"] = model_donvi;
                        ViewData["Madv"] = Madv;
                        ViewData["Nam"] = Nam;
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(x => x.Level != "X");                       
                        ViewData["Title"] = " Thông tin hồ sơ giá các loại đất";
                        ViewData["MenuLv1"] = "menu_giadat";
                        ViewData["MenuLv2"] = "menu_dgdct";
                        ViewData["MenuLv3"] = "menu_dgdct_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Index.cshtml", model);                   
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

        [Route("GiaDatCuThe/Create")]
        [HttpGet]
        public IActionResult Create(string Madv, string Phanloai, string MaDiaBan)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthe.thongtin", "Create"))
                {

                    this.RemoveData_Ct_CXD(Madv);
                    var model = new VMDinhGiaDat
                    {

                        Madv = Madv,
                        Phanloai = Phanloai,
                        Madiaban = MaDiaBan,
                        Thoidiem = DateTime.Now,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        ThongTinGiayTo = new List<CSDLGia_ASP.Models.Manages.DinhGia.ThongTinGiayTo>(),
                    };
                    var DsXaPhuong = _IDsDiaBan.GetListDsDiaBan(MaDiaBan);
                    ViewData["DsXaPhuong"] = DsXaPhuong;
                    ViewData["TenDiaBan"] = _db.DsDiaBan.FirstOrDefault(x => x.MaDiaBan == MaDiaBan).TenDiaBan;
                    ViewData["Madv"] = Madv;
                    ViewData["Mahs"] = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH");
                    ViewData["DsDonVi"] = _db.DsDonVi;
                    ViewData["Dmloaidat"] = _db.DmLoaiDat;
                    ViewData["Title"] = "Thông tin hồ sơ giá các loại đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgdct";
                    ViewData["MenuLv3"] = "menu_dgdct_tt";

                    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Create.cshtml", model);
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

        [Route("GiaDatCuThe/Store")]
        [HttpPost]
        public IActionResult Store(VMDinhGiaDat request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthe.thongtin", "Create"))
                {
                    var model = new GiaDatPhanLoai
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Phanloai = request.Phanloai,
                        Madiaban = request.Madiaban,
                        Soqd = request.Soqd,
                        Thoidiem = request.Thoidiem,
                        Thongtin = request.Thongtin,
                        Ghichu = request.Ghichu,
                        Trangthai = "CC",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaDatPhanLoai.Add(model);
                    this.SaveData_Ct_CXD(model.Mahs);
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Thêm mới");

                    // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                    LoggingHelper.LogAction(HttpContext, _db, "Store", "Thêm mới hồ sơ giá đất cụ thể");

                    _db.SaveChanges();
                    ViewData["Title"] = "Thông tin hồ sơ giá các loại đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgdct";
                    ViewData["MenuLv3"] = "menu_dgdct_tt";

                    return RedirectToAction("Index", "GiaDatPl", new { request.Madv });
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

        [Route("GiaDatCuThe/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthe.thongtin", "Delete"))
                {
                    var model = _db.GiaDatPhanLoai.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaDatPhanLoai.Remove(model);

                    var model_ct = _db.GiaDatPhanLoaiCt.Where(t => t.Mahs == model.Mahs);
                    if (model_ct.Any())
                    {
                        _db.GiaDatPhanLoaiCt.RemoveRange(model_ct);
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
                    _db.SaveChanges();
                    ViewData["Title"] = "Thông tin hồ sơ giá các loại đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgdct";
                    ViewData["MenuLv3"] = "menu_dgdct_tt";

                    // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                    LoggingHelper.LogAction(HttpContext, _db, "Delete", "Xóa hồ sơ giá đất cụ thể");

                    return RedirectToAction("Index", "GiaDatPl", new { model.Madv });
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

        [Route("GiaDatCuThe/Modify")]
        [HttpGet]
        public IActionResult Modify(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthe.thongtin", "Edit"))
                {
                    var model = _db.GiaDatPhanLoai.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new VMDinhGiaDat
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        Phanloai = model.Phanloai,
                        Madiaban = model.Madiaban,
                        Maxp = model.Maxp,
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Thongtin = model.Thongtin,
                        Ghichu = model.Ghichu,
                        ThongTinGiayTo = _db.ThongTinGiayTo.Where(x => x.Mahs == model.Mahs).ToList(),
                    };
                    var model_ct = _db.GiaDatPhanLoaiCt.Where(t => t.Mahs == model_new.Mahs);
                    model_new.GiaDatPhanLoaiCt = model_ct.ToList();
                    var DsXaPhuong = _IDsDiaBan.GetListDsDiaBan(model.Madiaban);                    
                    ViewData["DsXaPhuong"] = DsXaPhuong;
                    ViewData["TenDiaBan"] = _db.DsDiaBan.FirstOrDefault(x => x.MaDiaBan == model.Madiaban).TenDiaBan;
                    ViewData["Madv"] = model.Madv;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["Dmloaidat"] = _db.DmLoaiDat.ToList();
                    ViewData["Title"] = "Chỉnh sửa giá đất cụ thể";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgdct";
                    ViewData["MenuLv3"] = "menu_dgdct_tt";

                    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Modify.cshtml", model_new);
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
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthe.thongtin", "Edit"))
                {
                    var model = _db.GiaDatPhanLoai.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new VMDinhGiaDat
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        Madiaban = model.Madiaban,
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Thongtin = model.Thongtin,
                        Ghichu = model.Ghichu,
                        Phanloai = model.Phanloai,
                        Macqcq = model.Macqcq,
                    };

                    //var model_ct = _db.GiaDatPhanLoaiCt.Where(t => t.Mahs == model_new.Mahs);
                    var model_ct = from gd in _db.GiaDatPhanLoaiCt.Where(x=>x.Mahs== model_new.Mahs)
                                   join diaban in _db.DsDiaBan on gd.MaDiaBan equals diaban.MaDiaBan
                                   select new GiaDatPhanLoaiCt()
                                   {
                                       Mahs = gd.Mahs,
                                       Maloaidat = gd.Maloaidat,
                                       Khuvuc = gd.Khuvuc,
                                       Vitri = gd.Vitri,
                                       Banggiadat = gd.Banggiadat,
                                       Diagioitu = gd.Diagioitu,
                                       Diagioiden = gd.Diagioiden,
                                       Giacuthe = gd.Giacuthe,
                                       Hesodc = gd.Hesodc,
                                       Sapxep = gd.Sapxep,
                                       Trangthai = gd.Trangthai,
                                       Madv = gd.Madv,
                                       MaDiaBan = gd.MaDiaBan,  
                                       TenDiaBan=diaban.TenDiaBan,
                                   };

                    model_new.GiaDatPhanLoaiCt = model_ct.ToList();
                    ViewData["TenDiaBan"] = _db.DsDiaBan.FirstOrDefault(x => x.MaDiaBan == model.Madiaban).TenDiaBan;
                    ViewData["TenTinh"] = _db.DsDiaBan.FirstOrDefault(x => string.IsNullOrEmpty(x.MaDiaBanCq)).TenDiaBan;
                    ViewData["DsDiaBanHuyen"] = _db.DsDiaBan.Where(x => x.Level == "H");
                    ViewData["DsDiaBanXa"] = _db.DsDiaBan.Where(x => x.Level == "X");
                    ViewData["Madv"] = model.Madv;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["Dmloaidat"] = _db.DmLoaiDat.ToList();
                    ViewData["Title"] = "Chỉnh sửa giá đất cụ thể";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgdct";
                    ViewData["MenuLv3"] = "menu_dgdct_tt";

                    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Show.cshtml", model_new);
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

        [Route("GiaDatCuThe/Update")]
        [HttpPost]
        public IActionResult Update(VMDinhGiaDat request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthe.thongtin", "Edit"))
                {
                    var model = _db.GiaDatPhanLoai.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Thongtin = request.Thongtin;
                    model.Ghichu = request.Ghichu;
                    model.Updated_at = DateTime.Now;
                    _db.GiaDatPhanLoai.Update(model);
                    this.SaveData_Ct_CXD(model.Mahs);
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Cập nhật");
                    _db.SaveChanges();
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgdct";
                    ViewData["MenuLv3"] = "menu_dgdct_tt";

                    // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                    LoggingHelper.LogAction(HttpContext, _db, "Update", " Update hồ sơ giá đất cụ thể");

                    return RedirectToAction("Index", "GiaDatPl", new { request.Madv });

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

        [Route("GiaDatCuThe/Print")]
        [HttpGet]
        public IActionResult Print(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthe.timkiem", "Index"))
                {
                    var model = _db.GiaDatPhanLoai.FirstOrDefault(t => t.Mahs == Mahs);
                    var hoso_dg = new VMDinhGiaPrint
                    {
                        Id = model.Id,
                        Mahs = model.Mahs,
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Vitri = model.Vitri,
                        Ghichu = model.Ghichu,
                    };

                    var modeldv = _db.DsDonVi.FirstOrDefault(t => t.MaDv == model.Macqcq);
                    if (modeldv != null)
                    {
                        hoso_dg.Tendv = modeldv.TenDvHienThi;
                    }
                    var modeldb = _db.DsDiaBan.FirstOrDefault(t => t.MaDiaBan == modeldv.MaDiaBan);
                    if (modeldb != null)
                    {
                        hoso_dg.Tendb = modeldb.TenDiaBan;
                    }
                    var modelct = _db.GiaDatPhanLoaiCt.Where(t => t.Mahs == model.Mahs);
                    if (modelct != null)
                    {
                        hoso_dg.GiaDatPhanLoaiCt = modelct.ToList();
                    }

                    /*var model = GetThongTinKk(Mahs);*/

                    ViewData["Title"] = "In định giá đât cụ thể";
                    ViewData["Maloaidat"] = _db.GiaDatPhanLoaiDm.ToList();
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgdct";
                    ViewData["MenuLv3"] = "menu_dgdct_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Print.cshtml", hoso_dg);

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

        [Route("GiaDatCuThe/Search")]
        [HttpGet]
        public IActionResult Search(DateTime beginTime, DateTime endTime, double beginPrice, double endPrice, string phanloaihoso, string MaDiaBan = "all")
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthe.timkiem", "Index"))
                {
                    phanloaihoso = string.IsNullOrEmpty(phanloaihoso) ? "Giá đất bồi thường tái định cư" : phanloaihoso;
                    beginTime = beginTime == DateTime.MinValue ? new DateTime(DateTime.Now.Year, 01, 01) : beginTime;
                    endTime = endTime == DateTime.MinValue ? new DateTime(DateTime.Now.Year, 12, 31) : endTime;
                    var model = (from giact in _db.GiaDatPhanLoaiCt
                                 join gia in _db.GiaDatPhanLoai on giact.Mahs equals gia.Mahs
                                 join donvi in _db.DsDonVi on gia.Madv equals donvi.MaDv
                                 select new GiaDatPhanLoaiCt
                                 {
                                     Id = giact.Id,
                                     Madv = gia.Madv,
                                     Tendv = donvi.TenDv,
                                     Mahs = giact.Mahs,
                                     Thoidiem = gia.Thoidiem,
                                     Giacuthe = giact.Giacuthe,
                                     PhanLoai = gia.Phanloai,
                                     Khuvuc = giact.Khuvuc,
                                     MaDiaBan = giact.MaDiaBan,                                     
                                     Trangthai=gia.Trangthai,
                                     Soqd=gia.Soqd,                                     
                                 });
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model = model.Where(x => x.Thoidiem >= beginTime && x.Thoidiem <= endTime && x.PhanLoai == phanloaihoso && list_trangthai.Contains(x.Trangthai));                    
                    model = model.Where(t => t.Giacuthe >= beginPrice);
                    if (endPrice > 0)
                    {
                        model = model.Where(t => t.Giacuthe <= endPrice);
                    }
                    //var DsXaPhuong = new List<DsDiaBan>();
                    if (MaDiaBan != "all")
                    {
                        var diaban_search = _dsDonviService.GetListDonvi(MaDiaBan);
                        List<string> list_diaban_search = diaban_search.Select(t=>t.MaDiaBan).ToList();
                        model = model.Where(t => list_diaban_search.Contains(t.MaDiaBan));
                        
                    }                                       
                    ViewBag.beginTime = beginTime;
                    ViewBag.endTime = endTime;
                    ViewBag.beginPrice = beginPrice;
                    ViewBag.endPrice = endPrice;
                    ViewData["MaDiaBan"] = MaDiaBan;                    
                    ViewData["phanloaihoso"] = phanloaihoso;
                    ViewData["DsDiaBan"] = _db.DsDiaBan;                   
                    ViewData["DsCqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Maloaidat"] = _db.DmLoaiDat.ToList();
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá các loại đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgdct";
                    ViewData["MenuLv3"] = "menu_dgdct_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/TimKiem/Result.cshtml", model);
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
        [Route("GiaDatCuThe/PrintSearch")]
        [HttpPost]
        public IActionResult PrintSearch(DateTime beginTime, DateTime endTime, double beginPrice, double endPrice, string phanloaihoso, string MaDiaBan = "all")
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthe.timkiem", "Index"))
                {
                    phanloaihoso = string.IsNullOrEmpty(phanloaihoso) ? "Giá đất bồi thường tái định cư" : phanloaihoso;
                    beginTime = beginTime == DateTime.MinValue ? new DateTime(DateTime.Now.Year, 01, 01) : beginTime;
                    endTime = endTime == DateTime.MinValue ? new DateTime(DateTime.Now.Year, 12, 31) : endTime;
                    var model = (from giact in _db.GiaDatPhanLoaiCt
                                 join gia in _db.GiaDatPhanLoai on giact.Mahs equals gia.Mahs
                                 join donvi in _db.DsDonVi on gia.Madv equals donvi.MaDv
                                 join diaban in _db.DsDiaBan on gia.Madiaban equals diaban.MaDiaBan
                                 select new GiaDatPhanLoaiCt
                                 {
                                     Id = giact.Id,
                                     Madv = gia.Madv,
                                     Tendv = donvi.TenDv,
                                     Mahs = giact.Mahs,
                                     Thoidiem = gia.Thoidiem,
                                     Giacuthe = giact.Giacuthe,
                                     PhanLoai = gia.Phanloai,
                                     Khuvuc = giact.Khuvuc,                                     
                                     MaDiaBan = giact.MaDiaBan,                                                                         
                                     Trangthai=gia.Trangthai,
                                     Soqd=gia.Soqd,
                                     TenDiaBan=diaban.TenDiaBan,
                                 });
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model = model.Where(x => x.Thoidiem >= beginTime && x.Thoidiem <= endTime && x.PhanLoai == phanloaihoso && list_trangthai.Contains(x.Trangthai));                                      
                    if (MaDiaBan != "all")
                    {
                        var diaban_search = _dsDonviService.GetListDonvi(MaDiaBan);
                        List<string> list_diaban_search = diaban_search.Select(t => t.MaDiaBan).ToList();
                        model = model.Where(t => list_diaban_search.Contains(t.MaDiaBan));
                    }
                    model = model.Where(t => t.Giacuthe >= beginPrice);
                    if (endPrice > 0)
                    {
                        model = model.Where(t => t.Giacuthe <= endPrice);
                    }
                    ViewData["TenTinh"] = _db.DsDiaBan.FirstOrDefault(x => string.IsNullOrEmpty(x.MaDiaBanCq)).TenDiaBan;
                    ViewData["DsDiaBanHuyen"] = _db.DsDiaBan.Where(x => x.Level == "H");
                    ViewData["DsDiaBanXa"] = _db.DsDiaBan.Where(x => x.Level == "X");
                    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/TimKiem/PrintSearch.cshtml", model);
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

        public IActionResult Complete(string mahs_complete, string trangthai_complete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthe.thongtin", "Index"))
                {
                    
                    var model = _db.GiaDatPhanLoai.FirstOrDefault(p => p.Mahs == mahs_complete);
                    model.Trangthai = trangthai_complete;
                    model.Updated_at = DateTime.Now;

                    _db.GiaDatPhanLoai.Update(model);
                    _db.SaveChanges();
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), trangthai_complete);



                    return RedirectToAction("Index", "GiaDatPl", new { model.Madv });


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

        private void RemoveData_Ct_CXD(string Madv)
        {
            // xóa giá đất chi tiết chưa lưu lại
            var modelct = _db.GiaDatPhanLoaiCt.Where(x => x.Madv == Madv && x.Trangthai == "CXD");
            if (modelct.Any())
            {
                _db.GiaDatPhanLoaiCt.RemoveRange(modelct);
            }
            // xóa thông tin giấy tờ chưa lưu lại
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
            }
            _db.SaveChanges();
        }
        private void SaveData_Ct_CXD(string Mahs)
        {
            // Lưu lại dữ liệu giá đất chi tiết
            var modelct = _db.GiaDatPhanLoaiCt.Where(t => t.Mahs == Mahs && t.Trangthai == "CXD").ToList();
            if (modelct.Any())
            {
                modelct.ForEach(x => x.Trangthai = "XD");
            }
            // Lưu lại dữ liệu thông tin giấy tờ
            var modelFile = _db.ThongTinGiayTo.Where(t => t.Mahs == Mahs && t.Status == "CXD").ToList();
            if (modelFile.Any())
            {
                modelFile.ForEach(x => x.Status = "XD");
            }
            _db.SaveChanges();
        }

    }
}
