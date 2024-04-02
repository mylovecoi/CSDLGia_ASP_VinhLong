using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaGiaoDichDBDS
{
    public class GiaVatLieuXayDungController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GiaVatLieuXayDungController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("GiaVatLieuXayDung/DanhSach")]
        [HttpGet]
        public IActionResult Index(string Nam, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.giavatlieuxaydung.thongtin", "Index"))
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

                        IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaVatLieuXayDung> model = _db.GiaVatLieuXayDung;

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
                        ViewData["Nam"] = Nam;
                        ViewData["Madv"] = Madv;
                        ViewData["Title"] = "Thông tin vật liệu xây dựng";
                        ViewData["MenuLv1"] = "menu_giakhac";
                        ViewData["MenuLv2"] = "menu_giakhac_giavatlieuxaydung";
                        ViewData["MenuLv3"] = "menu_giakhac_giavatlieuxaydung_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaVatLieuXayDung/DanhSach/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Title"] = "Danh mục nhóm liệu xây dựng";
                        ViewData["Messages"] = "Hệ thống chưa có định giá vật liệu xây dựng.";
                        ViewData["MenuLv1"] = "menu_giakhac";
                        ViewData["MenuLv2"] = "menu_giakhac_giavatlieuxaydung";
                        ViewData["MenuLv3"] = "menu_giakhac_giavatlieuxaydung_tt";
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

        [Route("GiaVatLieuXayDung/Create")]
        [HttpGet]
        public IActionResult Create(string MadvBc)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.giavatlieuxaydung.thongtin", "Create"))
                {
                    // Xóa những cái chưa xác định

                    var check = _db.GiaVatLieuXayDungCt.Where(t => t.Trangthai == "CXD" && t.Madv == MadvBc);
                    if (check.Any())
                    {
                        _db.GiaVatLieuXayDungCt.RemoveRange(check);
                        _db.SaveChanges();
                    }

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaVatLieuXayDung
                    {
                        Mahs = MadvBc + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Madv = MadvBc,
                    };

                    // lấy danh mục theo mã nhóm truyền sang
                    var danhmuc = _db.GiaVatLieuXayDungDm;          
                    var chitiet = new List<GiaVatLieuXayDungCt>();
                    foreach (var item in danhmuc)
                    {
                        chitiet.Add(new GiaVatLieuXayDungCt()
                        {
                            Mahs = model.Mahs,                           
                            Mavlxd = item.Mavlxd,
                            Tenvlxd = item.Tenvlxd,
                            Dvt = item.Dvt,
                            Tieuchuan = item.Tieuchuan,
                            Trangthai = "CXD",
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        });
                    }

                    _db.GiaVatLieuXayDungCt.AddRange(chitiet);
                    _db.SaveChanges();

                    model.GiaVatLieuXayDungCt = chitiet.Where(t => t.Mahs == model.Mahs).ToList();
                  
                    ViewData["Title"] = "Bảng giá vật liệu xây dựng";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_giakhac_giavatlieuxaydung";
                    ViewData["MenuLv3"] = "menu_giakhac_giavatlieuxaydung_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaVatLieuXayDung/DanhSach/Create.cshtml", model);

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

        [Route("GiaVatLieuXayDung/Store")]
        [HttpPost]
        public async Task<IActionResult> Store(CSDLGia_ASP.Models.Manages.DinhGia.GiaVatLieuXayDung request, IFormFile Ipf1upload)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.giavatlieuxaydung.thongtin", "Create"))
                {
                    if (Ipf1upload != null && Ipf1upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf1upload.FileName);
                        string extension = Path.GetExtension(Ipf1upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf1upload.CopyToAsync(FileStream);
                        }
                        request.Ipf1 = filename;
                    }

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaVatLieuXayDung
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
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaVatLieuXayDung.Add(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaVatLieuXayDungCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Trangthai = "XD";
                        }
                    }

                    var trangthaihoso = new TrangThaiHoSo
                    {
                        MaHoSo = model.Mahs,
                        TrangThai = "Thêm mới",
                        TenDangNhap = Helpers.GetSsAdmin(HttpContext.Session, "Name"),
                        ThoiGian = DateTime.Now
                    };
                    _db.TrangThaiHoSo.Add(trangthaihoso);

                    _db.GiaVatLieuXayDungCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaVatLieuXayDung", new { request.Madv });
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

        [Route("GiaVatLieuXayDung/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.giavatlieuxaydung.thongtin", "Edit"))
                {
                    var model = _db.GiaVatLieuXayDung.FirstOrDefault(t => t.Mahs == Mahs);

                    var model_ct = _db.GiaVatLieuXayDungCt.Where(t => t.Mahs == model.Mahs);

                    model.GiaVatLieuXayDungCt = model_ct.ToList();

                    ViewData["Title"] = "Bảng giá vật liệu xây dựng";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_giakhac_giavatlieuxaydung";
                    ViewData["MenuLv3"] = "menu_giakhac_giavatlieuxaydung_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaVatLieuXayDung/DanhSach/Edit.cshtml", model);

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

        [Route("GiaVatLieuXayDung/Update")]
        [HttpPost]
        public async Task<IActionResult> Update(CSDLGia_ASP.Models.Manages.DinhGia.GiaVatLieuXayDung request, IFormFile Ipf1upload)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.giavatlieuxaydung.thongtin", "Edit"))
                {
                    if (Ipf1upload != null && Ipf1upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf1upload.FileName);
                        string extension = Path.GetExtension(Ipf1upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf1upload.CopyToAsync(FileStream);
                        }
                        request.Ipf1 = filename;
                    }

                    var model = _db.GiaVatLieuXayDung.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Soqdlk = request.Soqdlk;
                    model.Thoidiemlk = request.Thoidiemlk;
                    model.Cqbh = request.Cqbh;
                    model.Ghichu = request.Ghichu;
                    model.Ipf1 = request.Ipf1;
                    model.Updated_at = DateTime.Now;


                    var trangthaihoso = new TrangThaiHoSo
                    {
                        MaHoSo = model.Mahs,
                        TrangThai = "Cập nhật",
                        TenDangNhap = Helpers.GetSsAdmin(HttpContext.Session, "Name"),
                        ThoiGian = DateTime.Now
                    };
                    _db.TrangThaiHoSo.Add(trangthaihoso);

                    _db.GiaVatLieuXayDung.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaVatLieuXayDung", new { request.Madv });
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

        [Route("GiaVatLieuXayDung/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.giavatlieuxaydung.thongtin", "Delete"))
                {
                    var model = _db.GiaVatLieuXayDung.FirstOrDefault(t => t.Id == id_delete);   
                    var model_ct = _db.GiaVatLieuXayDungCt.Where(t => t.Mahs == model.Mahs);
                    _db.GiaVatLieuXayDungCt.RemoveRange(model_ct);
                    _db.GiaVatLieuXayDung.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaVatLieuXayDung", new { model.Madv });
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

        [Route("GiaVatLieuXayDung/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.giavatlieuxaydung.thongtin", "Index"))
                {
                    var model = _db.GiaVatLieuXayDung.FirstOrDefault(t => t.Mahs == Mahs);
                    model.GiaVatLieuXayDungCt = _db.GiaVatLieuXayDungCt.Where(t => t.Mahs == model.Mahs).ToList();

                    ViewData["Title"] = "Bảng giá vật liệu xây dựng";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_giakhac_giavatlieuxaydung";
                    ViewData["MenuLv3"] = "menu_giakhac_giavatlieuxaydung_tt";
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    return View("Views/Admin/Manages/DinhGia/GiaVatLieuXayDung/DanhSach/Show.cshtml", model);

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

        [Route("GiaVatLieuXayDung/Complete")]
        [HttpPost]
        public IActionResult Complete(string mahs_chuyen, string macqcq_chuyen)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.giavatlieuxaydung.thongtin", "Index"))
                {
                    var model = _db.GiaVatLieuXayDung.FirstOrDefault(t => t.Mahs == mahs_chuyen);

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
                        model.Trangthai_t = "HT";
                    }
                    else if (chk_dvcq != null && chk_dvcq.Level == "ADMIN")
                    {
                        model.Madv_ad = macqcq_chuyen;
                        model.Thoidiem_ad = DateTime.Now;
                        model.Trangthai_ad = "HT";
                    }
                    else
                    {
                        model.Madv_h = macqcq_chuyen;
                        model.Thoidiem_h = DateTime.Now;
                        model.Trangthai_h = "HT";
                    }


                    var trangthaihoso = new TrangThaiHoSo
                    {
                        MaHoSo = model.Mahs,
                        TrangThai = "Hoàn thành",
                        TenDangNhap = Helpers.GetSsAdmin(HttpContext.Session, "Name"),
                        ThoiGian = DateTime.Now
                    };
                    _db.TrangThaiHoSo.Add(trangthaihoso);
                    _db.GiaVatLieuXayDung.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaVatLieuXayDung", new { model.Madv });

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

        [Route("GiaVatLieuXayDung/Search")]
        [HttpGet]
        public IActionResult Search(string Madv, DateTime? NgayTu, DateTime? NgayDen, string Mahs, double DonGiaTu, double DonGiaDen, string Tenvlxd, string Tieuchuan)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.giavatlieuxaydung.timkiem", "Index"))
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
                    Tenvlxd = string.IsNullOrEmpty(Tenvlxd) ? "" : Tenvlxd;
                    Tieuchuan = string.IsNullOrEmpty(Tieuchuan) ? "" : Tieuchuan;

                    var model = (from hosoct in _db.GiaVatLieuXayDungCt
                                 join hoso in _db.GiaVatLieuXayDung on hosoct.Mahs equals hoso.Mahs
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaVatLieuXayDungCt
                                 {
                                     Madv = hoso.Madv,
                                     Tendv = donvi.TenDv,
                                     Soqd = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                     Trangthai = hoso.Trangthai,
                                     Mahs = hosoct.Mahs,
                                     Mavlxd = hosoct.Mavlxd,
                                     Tenvlxd = hosoct.Tenvlxd,
                                     Tieuchuan = hosoct.Tieuchuan,
                                     Dvt = hosoct.Dvt,
                                     Gia = hosoct.Gia,
                                 });

                    model = model.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen /*&& t.Trangthai == "HT"*/ && t.Gia >= DonGiaTu);
                    if (Madv != "all") { model = model.Where(t => t.Madv == Madv); }
                    if (DonGiaDen > 0) { model = model.Where(t => t.Gia <= DonGiaDen); }
                    if (!string.IsNullOrEmpty(Tenvlxd))
                    {
                        model = model.Where(t => t.Tenvlxd.ToLower().Contains(Tenvlxd.ToLower()));
                    }
                    if (!string.IsNullOrEmpty(Tieuchuan))
                    {
                        model = model.Where(t => t.Tieuchuan.ToLower().Contains(Tieuchuan.ToLower()));
                    }

                    ViewData["Madv"] = Madv;
                    ViewData["NgayTu"] = NgayTu;
                    ViewData["NgayDen"] = NgayDen;
                    ViewData["Mahs"] = Mahs;
                    ViewData["DonGiaTu"] = DonGiaTu;
                    ViewData["DonGiaDen"] = DonGiaDen;
                    ViewData["Tenvlxd"] = Tenvlxd;
                    ViewData["Tieuchuan"] = Tieuchuan;
                    ViewData["DanhSachHoSo"] = _db.GiaVatLieuXayDung.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen /*&& t.Trangthai == "HT"*/);

                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá vật liệu xây dựng";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_giakhac_giavatlieuxaydung";
                    ViewData["MenuLv3"] = "menu_giakhac_giavatlieuxaydung_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaVatLieuXayDung/TimKiem/Search.cshtml", model);

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

        [Route("GiaVatLieuXayDung/Print")]
        [HttpPost]
        public IActionResult Print(string Madv_Search, DateTime? NgayTu_Search, DateTime? NgayDen_Search, double DonGiaTu_Search, double DonGiaDen_Search,
                                   string Tenvlxd_Search, string Tieuchuan_Search)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.giavatlieuxaydung.timkiem", "Edit"))
                {

                    var model = (from hosoct in _db.GiaVatLieuXayDungCt
                                 join hoso in _db.GiaVatLieuXayDung on hosoct.Mahs equals hoso.Mahs
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaVatLieuXayDungCt
                                 {
                                     Madv = hoso.Madv,
                                     Tendv = donvi.TenDv,
                                     Soqd = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                     Gia = hosoct.Gia,
                                     Mavlxd = hosoct.Mavlxd,
                                     Tenvlxd = hosoct.Tenvlxd,
                                     Tieuchuan = hosoct.Tieuchuan,
                                     Dvt = hosoct.Dvt,
                                     Trangthai = hoso.Trangthai,
                                     Mahs = hoso.Mahs
                                 });
                    model = model.Where(t => t.Thoidiem >= NgayTu_Search && t.Thoidiem <= NgayDen_Search /*&& t.Trangthai == "HT"*/ && t.Gia >= DonGiaTu_Search);
                    if (Madv_Search != "all") { model = model.Where(t => t.Madv == Madv_Search); }
                    if (DonGiaDen_Search > 0) { model = model.Where(t => t.Gia <= DonGiaDen_Search); }
                    if (!string.IsNullOrEmpty(Tenvlxd_Search))
                    {
                        model = model.Where(t => t.Tenvlxd.ToLower().Contains(Tenvlxd_Search.ToLower()));
                    }
                    if (!string.IsNullOrEmpty(Tieuchuan_Search))
                    {
                        model = model.Where(t => t.Tieuchuan.ToLower().Contains(Tieuchuan_Search.ToLower()));
                    }

                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá vật liệu xây dựng";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_giakhac_giavatlieuxaydung";
                    ViewData["MenuLv3"] = "menu_giakhac_giavatlieuxaydung_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaVatLieuXayDung/TimKiem/Result.cshtml", model);
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

        [HttpPost("GiaVatLieuXayDung/GetListHoSo")]
        public JsonResult GetListHoSo(DateTime ngaytu, DateTime ngayden)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.GiaVatLieuXayDung.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && t.Trangthai == "HT");
                string result = "<select class='form-control' id='mahs_Search' name='mahs_Search'>";
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


        [HttpPost("GiaVatLieuXayDungCt/Edit")]
        public JsonResult EditCt(int Id)
        {
            var model = _db.GiaVatLieuXayDungCt.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                List<string> list_style = !string.IsNullOrEmpty(model.Style) ? new List<string>(model.Style.Split(',')) : new List<string>();
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Tên vật liệu xây dựng</label>";
                result += "<label class='form-control'>" + model.Tenvlxd + "</label>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label >Kiểu in hiển thị: </label>";
                result += "<select class='form-control select2multi' multiple='multiple' id='style_edit' name='style_edit' style='width:100%'>";
                result += "<option value='Chữ in hoa'" + (list_style.Contains("Chữ in hoa") ? "selected" : "") + ">Chữ in hoa</option >";
                result += "<option value='Chữ in đậm'" + (list_style.Contains("Chữ in đậm") ? "selected" : "") + ">Chữ in đậm</option >";
                result += "<option value='Chữ in nghiêng'" + (list_style.Contains("Chữ in nghiêng") ? "selected" : "") + ">Chữ in nghiêng</option >";
                result += "</select>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Giá vật liệu xây dựng (đồng)</label>";
                result += "<input type='text' id='gia_edit' name='gia_edit' value='" + model.Gia + "' class='form-control money text-right' style='font-weight: bold'/>";
                result += "</div>";
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

        [Route("GiaVatLieuXayDungCt/Update")]
        [HttpPost]
        public JsonResult UpdateCt(int Id, double Gia, string[] Style)
        {
            string str_style = Style.Count() > 0 ? string.Join(",", Style.ToArray()) : "";
            var model = _db.GiaVatLieuXayDungCt.FirstOrDefault(t => t.Id == Id);
            model.Gia = Gia;
            model.Style = str_style;
            model.Updated_at = DateTime.Now;
            _db.GiaVatLieuXayDungCt.Update(model);
            _db.SaveChanges();
            string result = GetDataCt(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        public string GetDataCt(string Mahs)
        {
            var model = _db.GiaVatLieuXayDungCt.Where(t => t.Mahs == Mahs);

            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover table-responsive' id='datatable_4'>";

            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th width='2%'>#</th>";
            result += "<th width='2%'>STT</th>";
            result += "<th>Tên vật liệu xây dựng</th>";
            result += "<th width='2%'>Đơn vị tính</th>";
            result += "<th width='15%'>Tiêu chuẩn</th>";
            result += "<th width='5%'>Đơn giá</th>";
            result += "<th width='5%'>Thao tác</th>";
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";

            foreach (var item in model)
            {
                string HtmlStyle = Helpers.ConvertStrToStyle(item.Style);
                result += "<tr>";
                result += "<td style='text-align:center;" + HtmlStyle + "'>" + item.STTSapXep + "</td>";
                result += "<td style='text-align:center;" + HtmlStyle + "'>" + item.STTHienThi + "</td>";
                result += "<td style='text-align:left;" + HtmlStyle + "'>" + item.Tenvlxd + "</td>";
                result += "<td style='text-align:center;" + HtmlStyle + "'>" + item.Dvt + "</td>";
                result += "<td style='text-align:left;" + HtmlStyle + "'>" + item.Tieuchuan + "</td>";
                result += "<td style='text-align:right;" + HtmlStyle + "'>" + Helpers.ConvertDbToStr(item.Gia) + "</td>";        
                result += "<td>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Chỉnh sửa'";
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
    }
}
