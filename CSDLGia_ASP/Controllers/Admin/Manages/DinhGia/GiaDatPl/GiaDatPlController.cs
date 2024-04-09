using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;
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

        public GiaDatPlController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("GiaDatCuThe")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam ="all")
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthe.thongtin", "Index"))
                {
                    
                    var dsdonvi = (from db in _db.DsDiaBan
                                   join dv in _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI") on db.MaDiaBan equals dv.MaDiaBan
                                   select new VMDsDonVi
                                   {
                                       Id = dv.Id,
                                       TenDiaBan = db.TenDiaBan,
                                       MaDiaBan = db.MaDiaBan,
                                       TenDv = dv.TenDv,
                                       MaDv = dv.MaDv,

                                   }).ToList();

                    if (dsdonvi.Count > 0)
                    {
                        Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;
                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") != null)
                        {
                            Madv = Helpers.GetSsAdmin(HttpContext.Session, "Madv");
                        }

                        IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaDatPhanLoai> model = _db.GiaDatPhanLoai;

                        if (Madv != "all")
                        {
                            model = model.Where(t => t.Madv == Madv);
                        }
                        if (Nam !="all")
                        {
                            model = model.Where(x=>x.Thoidiem.Year ==Convert.ToInt32(Nam));
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
                        ViewData["DsCqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                        ViewData["Title"] = " Thông tin hồ sơ giá các loại đất";
                        ViewData["MenuLv1"] = "menu_giadat";
                        ViewData["MenuLv2"] = "menu_dgdct";
                        ViewData["MenuLv3"] = "menu_dgdct_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Messages"] = "Thông tin hồ sơ giá dịch vụ khám chữa bệnh.";
                        ViewData["Title"] = " Thông tin hồ sơ giá dịch vụ khám chữa bệnh";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgkcb";
                        ViewData["MenuLv3"] = "menu_dgkcb_tt";
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

        [Route("GiaDatCuThe/Create")]
        [HttpGet]
        public IActionResult Create(string Madv, string Phanloai)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthe.thongtin", "Create"))
                {

                    this.RemoveData_Ct_CXD(Madv);
                    var donvi = _db.DsDonVi.FirstOrDefault(x => x.MaDv == Madv);
                    var MaDiaBan = _db.DsDiaBan.FirstOrDefault(x=>x.MaDiaBan == donvi.MaDiaBan).MaDiaBan;
                    var model = new VMDinhGiaDat
                    {
                        Madv = Madv,
                        Phanloai = Phanloai,
                        Thoidiem = DateTime.Now,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Madiaban = MaDiaBan,
                        ThongTinGiayTo = new List<CSDLGia_ASP.Models.Manages.DinhGia.ThongTinGiayTo>(),
                    };
                    ViewData["TenDiaBan"]= _db.DsDiaBan.FirstOrDefault(x=>x.MaDiaBan == MaDiaBan).TenDiaBan;
                    ViewData["Madv"] = Madv;
                    ViewData["Mahs"] = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH");
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Dmloaidat"] = _db.DmLoaiDat.ToList();
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
                        Trangthai = "CHT",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaDatPhanLoai.Add(model);
                    this.SaveData_Ct_CXD(model.Mahs);
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
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Thongtin = model.Thongtin,
                        Ghichu = model.Ghichu,
                        ThongTinGiayTo = _db.ThongTinGiayTo.Where(x => x.Mahs == model.Mahs).ToList(),
                    };
                    var model_ct = _db.GiaDatPhanLoaiCt.Where(t => t.Mahs == model_new.Mahs);
                    model_new.GiaDatPhanLoaiCt = model_ct.ToList();
                    ViewData["TenDiaBan"] = _db.DsDiaBan.FirstOrDefault(x => x.MaDiaBan == model.Madiaban).TenDiaBan;
                    ViewData["Madv"] = model.Madv;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
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
                        Phanloai= model.Phanloai,

                    };

                    var model_ct = _db.GiaDatPhanLoaiCt.Where(t => t.Mahs == model_new.Mahs);

                    model_new.GiaDatPhanLoaiCt = model_ct.ToList();

                    ViewData["Madv"] = model.Madv;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
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
                    model.Madiaban = request.Madiaban;
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Thongtin = request.Thongtin;
                    model.Ghichu = request.Ghichu;
                    model.Updated_at = DateTime.Now;
                    _db.GiaDatPhanLoai.Update(model);
                    this.SaveData_Ct_CXD(model.Mahs);
                    _db.SaveChanges();
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
        public IActionResult Search(DateTime beginTime, DateTime endTime, double beginPrice, double endPrice,string phanloaihoso, string mld="All", string madv ="All")
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthe.timkiem", "Index"))
                {
                    phanloaihoso = string.IsNullOrEmpty(phanloaihoso) ? "Giá đất bồi thường tái định cư" : phanloaihoso;
                    beginTime = beginTime == DateTime.MinValue ? new DateTime(DateTime.Now.Year,01,01) : beginTime;
                    endTime = endTime == DateTime.MinValue ? new DateTime(DateTime.Now.Year,12,31) : endTime;
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
                                     PhanLoai= gia.Phanloai,
                                     Khuvuc = giact.Khuvuc,
                                 });
                    model = model.Where(x=>x.Thoidiem >= beginTime && x.Thoidiem <= endTime && x.PhanLoai== phanloaihoso);
                    if (madv != "All")
                    {
                        model = model.Where(t => t.Madv == madv);
                    }
                    model = model.Where(t => t.Giacuthe >= beginPrice);
                    if (endPrice > 0)
                    {
                        model = model.Where(t => t.Giacuthe <= endPrice);
                    }
                    ViewBag.beginTime = beginTime;
                    ViewBag.endTime = endTime;
                    ViewBag.beginPrice = beginPrice;
                    ViewBag.endPrice = endPrice;
                    ViewData["phanloaihoso"] = phanloaihoso;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
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
        public IActionResult PrintSearch(DateTime beginTime, DateTime endTime, double beginPrice, double endPrice,string phanloaihoso, string mld = "All", string madv = "All")
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
                                 });
                    model = model.Where(x => x.Thoidiem >= beginTime && x.Thoidiem <= endTime && x.PhanLoai==phanloaihoso);
                    if (madv != "All")
                    {
                        model = model.Where(t => t.Madv == madv);
                    }
                    if (mld != "All")
                    {
                        model = model.Where(t => t.Maloaidat == mld);
                    }
                    model = model.Where(t => t.Giacuthe >= beginPrice);
                    if (endPrice > 0)
                    {
                        model = model.Where(t => t.Giacuthe <= endPrice);
                    }
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

        public IActionResult Complete(string mahs_chuyen, string macqcq_chuyen)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthe.thongtin", "Index"))
                {
                    var model = _db.GiaDatPhanLoai.FirstOrDefault(t => t.Mahs == mahs_chuyen);

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
                    var chk_dvcq = dvcq_join.FirstOrDefault(t => t.MaDv == macqcq_chuyen);
                    model.Macqcq = macqcq_chuyen;
                    model.Trangthai = "HT";
                    if (chk_dvcq != null && chk_dvcq.Level == "T")
                    {
                        model.Madv_t = macqcq_chuyen;
                        model.Thoidiem_t = DateTime.Now;
                        model.Trangthai_t = "CHT";
                    }
                    else if (chk_dvcq != null && chk_dvcq.Level == "ADMIN")
                    {
                        model.Madv_ad = macqcq_chuyen;
                        model.Thoidiem_ad = DateTime.Now;
                        model.Trangthai_ad = "CHT";
                    }
                    else
                    {
                        model.Madv_h = macqcq_chuyen;
                        model.Thoidiem_h = DateTime.Now;
                        model.Trangthai_h = "CHT";
                    }
                    _db.GiaDatPhanLoai.Update(model);
                    _db.SaveChanges();

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

        private void RemoveData_Ct_CXD( string Madv)
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
            var modelct = _db.GiaDatPhanLoaiCt.Where(t => t.Mahs == Mahs && t.Trangthai=="CXD").ToList();
            if (modelct.Any())
            {
                modelct.ForEach(x=>x.Trangthai ="XD");
            }
            // Lưu lại dữ liệu thông tin giấy tờ
            var modelFile = _db.ThongTinGiayTo.Where(t => t.Mahs == Mahs && t.Status=="CXD").ToList();
            if (modelFile.Any())
            {
                modelFile.ForEach(x=>x.Status="XD");
            }
            _db.SaveChanges();
        }

    }
}
