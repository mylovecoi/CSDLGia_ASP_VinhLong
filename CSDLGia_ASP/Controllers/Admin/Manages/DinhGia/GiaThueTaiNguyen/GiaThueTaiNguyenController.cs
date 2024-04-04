using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaThueTaiNguyen
{
    public class GiaThueTaiNguyenController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GiaThueTaiNguyenController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("GiaThueTaiNguyen/DanhSach")]
        [HttpGet]
        public IActionResult Index(string Nam, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetn.thongtin", "Index"))
                {
                    var dsdonvi = (from db in _db.DsDiaBan
                                   join dv in _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI") on db.MaDiaBan equals dv.MaDiaBan
                                   select new VMDsDonVi
                                   {
                                       Id = dv.Id,
                                       TenDiaBan = db.TenDiaBan,
                                       MaDiaBan = dv.MaDiaBan,
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

                        IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaThueTaiNguyen> model = _db.GiaThueTaiNguyen;

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
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                        ViewData["DsCqcq"] = _db.DsDonVi.ToList();
                        ViewData["NhomTn"] = _db.GiaThueTaiNguyenNhom.ToList();
                        ViewData["Nam"] = Nam;
                        ViewData["Madv"] = Madv;
                        ViewData["Title"] = "Thông tin giá thuế tài nguyên";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgthuetn";
                        ViewData["MenuLv3"] = "menu_dgthuetn_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaThueTaiNguyen/DanhSach/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Title"] = "Thông tin giá thuế tài nguyên";
                        ViewData["Messages"] = "Hệ thống chưa có định giá thuế tài nguyên.";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgthuetn";
                        ViewData["MenuLv3"] = "menu_dgthuetn_tt";
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

        [Route("GiaThueTaiNguyen/Create")]
        [HttpGet]
        public IActionResult Create(string Manhom, string MadvBc)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetn.thongtin", "Create"))
                {
                    var check = _db.GiaThueTaiNguyenCt.Where(t => t.Trangthai == "CXD" && t.Madv == MadvBc);
                    if (check != null)
                    {
                        _db.GiaThueTaiNguyenCt.RemoveRange(check);
                        _db.SaveChanges();
                    }

                    var model_file_cxd = _db.ThongTinGiayTo.Where(t => t.Status == "CXD" && t.Madv == MadvBc);
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

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueTaiNguyen
                    {
                        Mahs = MadvBc + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Madv = MadvBc,
                        Manhom = Manhom,
                        Thoidiem = DateTime.Now,
                    };

                    var danhmuc = _db.GiaThueTaiNguyenDm.Where(t=>t.Theodoi == "TD");
                    if (Manhom != "all")
                    {
                        danhmuc = danhmuc.Where(t => t.Manhom == Manhom);
                    }                    

                    var chitiet = new List<GiaThueTaiNguyenCt>();

                    foreach (var item in danhmuc)
                    {
                        chitiet.Add(new GiaThueTaiNguyenCt()
                        {
                            Mahs = model.Mahs,
                            Cap1 = item.Cap1,
                            Cap2 = item.Cap2,
                            Cap3 = item.Cap3,
                            Cap4 = item.Cap4,
                            Cap5 = item.Cap5,
                            Ten = item.Ten,
                            Dvt = item.Dvt,
                            Level = item.Level,
                            Trangthai = "CXD",
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                            Madv = MadvBc,
                            Manhom = item.Manhom
                        });
                    }
                    _db.GiaThueTaiNguyenCt.AddRange(chitiet);
                    _db.SaveChanges();

                    model.GiaThueTaiNguyenCt = chitiet.Where(t => t.Mahs == model.Mahs).ToList();

                    ViewData["Manhom"] = Manhom;
                    ViewData["Madv"] = MadvBc;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["Title"] = "Bảng giá tính thuế tài nguyên";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgthuetn";
                    ViewData["MenuLv3"] = "menu_dgthuetn_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaThueTaiNguyen/DanhSach/Create.cshtml", model);

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
        

        [Route("GiaThueTaiNguyen/Store")]
        [HttpPost]
        public IActionResult Store(CSDLGia_ASP.Models.Manages.DinhGia.GiaThueTaiNguyen request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetn.thongtin", "Create"))
                {
               

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueTaiNguyen
                    {
                        Mahs = request.Mahs,
                        Manhom = request.Manhom,
                        Madv = request.Madv,
                        Thoidiem = request.Thoidiem,
                        Soqd = request.Soqd,
                        Soqdlk = request.Soqdlk,
                        Thoidiemlk = request.Thoidiemlk,
                        Cqbh = request.Cqbh,
                        Ghichu = request.Ghichu,
                        Trangthai = "CHT",
                        Congbo = "CHUACONGBO",
                        Ipf1 = request.Ipf1,
                        PhanLoaiHoSo = request.PhanLoaiHoSo,
                        CodeExcel = request.CodeExcel,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaThueTaiNguyen.Add(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaThueTaiNguyenCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct.Any())
                    {
                        foreach (var item in modelct)
                        {
                            item.Trangthai = "XD";
                        }
                        _db.GiaThueTaiNguyenCt.UpdateRange(modelct);
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

                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaThueTaiNguyen", new { request.Madv });
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

        [Route("GiaThueTaiNguyen/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetn.thongtin", "Edit"))
                {
                    var model = _db.GiaThueTaiNguyen.FirstOrDefault(t => t.Mahs == Mahs);

                    model.GiaThueTaiNguyenCt = _db.GiaThueTaiNguyenCt.Where(t => t.Mahs == model.Mahs).ToList();
                    model.ThongTinGiayTo = _db.ThongTinGiayTo.Where(t => t.Mahs == model.Mahs).ToList();
              

                    /*ViewData["Madv"] = model.Madv;
                    ViewData["Ipf1"] = model.Ipf1;*/
                    ViewData["Title"] = "Bảng giá tính thuế tài nguyên";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgthuetn";
                    ViewData["MenuLv3"] = "menu_dgthuetn_tt";                    
                    return View("Views/Admin/Manages/DinhGia/GiaThueTaiNguyen/DanhSach/Edit.cshtml", model);

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

        [Route("GiaThueTaiNguyen/Update")]
        [HttpPost]
        public IActionResult Update(CSDLGia_ASP.Models.Manages.DinhGia.GiaThueTaiNguyen request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetn.thongtin", "Edit"))
                {   
                    var model = _db.GiaThueTaiNguyen.FirstOrDefault(t => t.Mahs == request.Mahs);
                    if (model != null)
                    {
                        model.Soqd = request.Soqd;
                        model.Thoidiem = request.Thoidiem;
                        model.Soqdlk = request.Soqdlk;
                        model.Thoidiemlk = request.Thoidiemlk;
                        model.Cqbh = request.Cqbh;
                        model.Ghichu = request.Ghichu;
                        model.Ipf1 = request.Ipf1;
                        model.Updated_at = DateTime.Now;
                        _db.GiaThueTaiNguyen.Update(model);
                        var modelct = _db.GiaThueTaiNguyenCt.Where(t => t.Mahs == request.Mahs);
                        if (modelct.Any())
                        {
                            foreach (var ct in modelct)
                            {
                                ct.Trangthai = "XD";
                            }
                            _db.GiaThueTaiNguyenCt.UpdateRange(modelct);
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
                        _db.SaveChanges();
                    }

                    return RedirectToAction("Index", "GiaThueTaiNguyen", new { request.Madv });
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

        [Route("GiaThueTaiNguyen/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetn.thongtin", "Delete"))
                {
                    var model = _db.GiaThueTaiNguyen.FirstOrDefault(t => t.Id == id_delete);
                    if (model != null)
                    {
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
                        var model_ct = _db.GiaThueTaiNguyenCt.Where(t => t.Mahs == model.Mahs);
                        if (model_ct.Any()) { _db.GiaThueTaiNguyenCt.RemoveRange(model_ct); }
                        
                    }
                        _db.GiaThueTaiNguyen.Remove(model);
                        _db.SaveChanges();

                    return RedirectToAction("Index", "GiaThueTaiNguyen", new { model.Madv });
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

        [Route("GiaThueTaiNguyen/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetn.thongtin", "Index"))
                {
                    var model = _db.GiaThueTaiNguyen.FirstOrDefault(t => t.Mahs == Mahs);
                    model.GiaThueTaiNguyenCt = _db.GiaThueTaiNguyenCt.Where(t => t.Mahs == model.Mahs).ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["Title"] = "Bảng giá tính thuế tài nguyên";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgthuetn";
                    ViewData["MenuLv3"] = "menu_dgthuetn_tt";                   
                    return View("Views/Admin/Manages/DinhGia/GiaThueTaiNguyen/DanhSach/Show.cshtml", model);

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

        [Route("GiaThueTaiNguyen/Complete")]
        [HttpPost]
        public IActionResult Complete(string mahs_chuyen, string macqcq_chuyen)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetn.thongtin", "Index"))
                {
                    var model = _db.GiaThueTaiNguyen.FirstOrDefault(t => t.Mahs == mahs_chuyen);

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
                    _db.GiaThueTaiNguyen.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaThueTaiNguyen", new { model.Madv, Nam = model.Thoidiem.Year });

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

        [Route("GiaThueTaiNguyen/Search")]
        [HttpGet]
        public IActionResult Search(string Madv, string Manhom, DateTime? NgayTu, DateTime? NgayDen, string Mahs, double DonGiaTu, double DonGiaDen, string Ten)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetn.thongtin", "Index"))
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
                    Ten = string.IsNullOrEmpty(Ten) ? "" : Ten;



                    var model = (from hosoct in _db.GiaThueTaiNguyenCt
                                 join hoso in _db.GiaThueTaiNguyen on hosoct.Mahs equals hoso.Mahs
                                 join nhom in _db.GiaThueTaiNguyenNhom on hosoct.Manhom equals nhom.Manhom
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueTaiNguyenCt
                                 {
                                     Madv = hoso.Madv,
                                     Tendv = donvi.TenDv,
                                     Tennhom = nhom.Tennhom,
                                     SoQD = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                     Cap1 = hosoct.Cap1,
                                     Cap2 = hosoct.Cap2,
                                     Cap3 = hosoct.Cap3,
                                     Cap4 = hosoct.Cap4,
                                     Cap5 = hosoct.Cap5,
                                     Cap6 = hosoct.Cap6,
                                     Ten = hosoct.Ten,
                                     Dvt = hosoct.Dvt,                                    
                                     Gia = hosoct.Gia,
                                     Manhom = hosoct.Manhom,
                                     Trangthai = hoso.Trangthai,
                                     Mahs = hoso.Mahs
                                 });
                    model = model.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen && t.Trangthai == "HT");
                    if (Madv != "all") { model = model.Where(t => t.Madv == Madv); }
                    if (Manhom != "all") { model = model.Where(t => t.Manhom == Manhom); }
                    if (DonGiaTu > 0) { model = model.Where(t => t.Gia >= DonGiaTu); }
                    if (DonGiaDen > 0) { model = model.Where(t => t.Gia <= DonGiaDen); }
                    if (Mahs != "all") { model = model.Where(t => t.Mahs == Mahs); }
                    if (!string.IsNullOrEmpty(Ten))
                    {
                        model = model.Where(t => t.Ten.ToLower().Contains(Ten.ToLower()));
                    }


                    ViewData["Madv"] = Madv;
                    ViewData["Manhom"] = Manhom;
                    ViewData["NgayTu"] = NgayTu;
                    ViewData["NgayDen"] = NgayDen;
                    ViewData["Mahs"] = Mahs;
                    ViewData["DonGiaTu"] = DonGiaTu;
                    ViewData["DonGiaDen"] = DonGiaDen;
                    ViewData["Ten"] = Ten;
                    ViewData["DanhSachHoSo"] = _db.GiaThueTaiNguyen.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen && t.Trangthai == "HT");
                    ViewData["DanhMucNhom"] = _db.GiaThueTaiNguyenNhom;

                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá thuế tài nguyên";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgthuetn";
                    ViewData["MenuLv3"] = "menu_dgthuetn_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaThueTaiNguyen/TimKiem/Search.cshtml", model);

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

        [Route("GiaThueTaiNguyen/PrintSearch")]
        [HttpPost]
        public IActionResult Print(string Madv_Search, string Manhom_Search, DateTime? NgayTu_Search, DateTime? NgayDen_Search, 
                                    string Mahs_Search, double DonGiaTu_Search, double DonGiaDen_Search, string Ten_Search)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetn.thongtin", "Index"))
                {
                    var model = (from hosoct in _db.GiaThueTaiNguyenCt
                                 join hoso in _db.GiaThueTaiNguyen on hosoct.Mahs equals hoso.Mahs
                                 join nhom in _db.GiaThueTaiNguyenNhom on hosoct.Manhom equals nhom.Manhom
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueTaiNguyenCt
                                 {
                                     Madv = hoso.Madv,
                                     Tendv = donvi.TenDv,
                                     Tennhom = nhom.Tennhom,
                                     SoQD = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                     Cap1 = hosoct.Cap1,
                                     Cap2 = hosoct.Cap2,
                                     Cap3 = hosoct.Cap3,
                                     Cap4 = hosoct.Cap4,
                                     Cap5 = hosoct.Cap5,
                                     Cap6 = hosoct.Cap6,
                                     Ten = hosoct.Ten,
                                     Dvt = hosoct.Dvt,
                                     Gia = hosoct.Gia,
                                     Manhom = hosoct.Manhom,
                                     Trangthai = hoso.Trangthai,
                                     Mahs = hoso.Mahs
                                 });
                    model = model.Where(t => t.Thoidiem >= NgayTu_Search && t.Thoidiem <= NgayDen_Search && t.Trangthai == "HT");
                    if (Madv_Search != "all") { model = model.Where(t => t.Madv == Madv_Search); }
                    if (Manhom_Search != "all") { model = model.Where(t => t.Manhom == Manhom_Search); }
                    if (DonGiaTu_Search > 0) { model = model.Where(t => t.Gia >= DonGiaTu_Search); }
                    if (DonGiaDen_Search > 0) { model = model.Where(t => t.Gia <= DonGiaDen_Search); }
                    if (Mahs_Search != "all") { model = model.Where(t => t.Mahs == Mahs_Search); }
                    if (!string.IsNullOrEmpty(Ten_Search))
                    {
                        model = model.Where(t => t.Ten.ToLower().Contains(Ten_Search.ToLower()));
                    }


                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá thuế tài nguyên";
                  ;
                    return View("Views/Admin/Manages/DinhGia/GiaThueTaiNguyen/TimKiem/Result.cshtml", model);

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

        [Route("GiaThueTaiNguyenCt/Edit")]
        [HttpPost]
        public JsonResult EditCt(int Id)
        {
            var model = _db.GiaThueTaiNguyenCt.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Giá tính thuế tài nguyên (đồng)</label>";
                result += "<input type='text' id='gia_edit' name='gia_edit' value='" + model.Gia + "' class='form-control money text-right' style='font-weight: bold'/>";
                result += "</div>";
                result += "</div>";
                result += "</div>";

                result += "<input hidden type='text' id='id_edit' name='id_edit' value='" + model.Id + "'/>";
                result += "</div>";

                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Không tìm thấy thông tin cần chỉnh sửa!!!" };
                return Json(data);
            }
        }

        [Route("GiaThueTaiNguyenCt/Update")]
        [HttpPost]
        public JsonResult UpdateCt(int Id, double Gia)
        {
            var model = _db.GiaThueTaiNguyenCt.FirstOrDefault(t => t.Id == Id);
            model.Gia = Gia;
            model.Updated_at = DateTime.Now;
            _db.GiaThueTaiNguyenCt.Update(model);
            _db.SaveChanges();
            string result = GetDataCt(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        public string GetDataCt(string Mahs)
        {
            var model = _db.GiaThueTaiNguyenCt.Where(t => t.Mahs == Mahs).ToList();

            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover table-responsive' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th width='2%'>#</th>";
            result += "<th>Mã nhóm tài nguyên cấp 1</th>";
            result += "<th>Mã nhóm tài nguyên cấp 2</th>";
            result += "<th>Mã nhóm tài nguyên cấp 3</th>";
            result += "<th>Mã nhóm tài nguyên cấp 4</th>";
            result += "<th>Mã nhóm tài nguyên cấp 5</th>";
            result += "<th>Mã nhóm tài nguyên cấp 6</th>";
            result += "<th width='25%'>Tên nhóm, loại tài nguyên</th>";
            result += "<th>Đơn vị tính</th>";
            result += "<th>Giá tính thuế tài nguyên (đồng)</th>";
            result += "<th width='9%'>Thao tác</th>";
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";

            foreach (var item in model)
            {
                result += "<tr>";
                result += "<td class='text-center'>" + record++ + "</td>";
                result += "<td class='text-center'>" + item.Cap1 + "</td>";
                result += "<td class='text-center'>" + item.Cap2 + "</td>";
                result += "<td class='text-center'>" + item.Cap3 + "</td>";
                result += "<td class='text-center'>" + item.Cap4 + "</td>";
                result += "<td class='text-center'>" + item.Cap5 + "</td>";
                result += "<td class='text-center'>" + item.Cap6 + "</td>";
                result += "<td class='active' style='font-weight:bold'>" + item.Ten + "</td>";
                result += "<td class='text-center'>" + item.Dvt + "</td>";
                result += "<td style='text-align:right; font-weight:bold'>" + Helpers.ConvertDbToStr(item.Gia) + "</td>";
                result += "<td>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Nhập giá'";
                result += " data-target='#Edit_Modal' data-toggle='modal' onclick='SetEdit(`" + item.Id + "`)'>";
                result += "<i class='icon-lg la la-edit text-primary'></i>";
                result += "</button>";
                result += "</td>";
                result += "</tr>";
            }
            result += "</tbody>";
            result += "</table>";
            result += "</div>";

            return result;

        }

        [HttpPost("GiaThueTaiNguyen/GetListHoSo")]
        public JsonResult GetListHoSo(DateTime ngaytu, DateTime ngayden)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.GiaThueTaiNguyen.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && t.Trangthai == "HT");
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
