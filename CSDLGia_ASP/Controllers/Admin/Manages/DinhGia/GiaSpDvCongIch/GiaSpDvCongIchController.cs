
using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Services;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaSpDvCongIch
{
    public class GiaSpDvCongIchController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IDsDonviService _dsDonviService;
        private readonly ITrangThaiHoSoService _trangThaiHoSoService;

        public GiaSpDvCongIchController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment, IDsDonviService dsDonviService, ITrangThaiHoSoService trangThaiHoSoService)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
            _dsDonviService = dsDonviService;
            _trangThaiHoSoService = trangThaiHoSoService;
        }

        [Route("GiaSpDvCongIch")]
        [HttpGet]
        public IActionResult Index(int Nam, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.thongtin", "Index"))
                {
                    Madv = string.IsNullOrEmpty(Madv) ? Helpers.GetSsAdmin(HttpContext.Session, "Madv") : Madv;
                    var model_donvi = _dsDonviService.GetListDonvi(Helpers.GetSsAdmin(HttpContext.Session, "Madv"));
                    List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();

                    IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCongIch> model = _db.GiaSpDvCongIch.Where(t => list_madv.Contains(t.Madv));

                    if (Madv != "all")
                    {
                        model = model.Where(t => t.Madv == Madv);
                    }
                    if (Nam != 0)
                    {
                        model = model.Where(t => t.Thoidiem.Year == Nam).ToList();
                    }


                    ViewData["Nam"] = Nam;
                    ViewData["Madv"] = Madv;
                    ViewData["DsDonvi"] = model_donvi;
                    ViewData["DanhMucNhom"] = _db.GiaSpDvCongIchNhom.ToList();
                    ViewData["Title"] = "Thông tin giá sản phẩm dịch vụ công ích";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdvci";
                    ViewData["MenuLv3"] = "menu_dgdvci_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCongIch/Index.cshtml", model);
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

        [Route("GiaSpDvCongIch/Create")]
        [HttpGet]
        public IActionResult Create(string Manhom, string MadvBc)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.thongtin", "Create"))
                {
                    // hồ sơ mà chỉ đến bước hoàn thành mà quay lại thì sẽ có trạng thái CXD --> cần xóa
                    var check = _db.GiaSpDvCongIchCt.Where(t => t.Trangthai == "CXD" && t.Madv == MadvBc);
                    if (check.Any())
                    {
                        _db.GiaSpDvCongIchCt.RemoveRange(check);
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

                    // Thông tin của bộ hồ sơ
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCongIch
                    {
                        Mahs = MadvBc + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Madv = MadvBc,
                        Manhom = Manhom,
                        Thoidiem = DateTime.Now
                    };
                    IEnumerable<GiaSpDvCongIchDm> danhmuc = _db.GiaSpDvCongIchDm; // lấy dữ liệu trong bảng GiaSpDvCongIchDm

                    // Khi bấm đồng ý trong moda thì add dữ liệu GiaSpDvCongIchDm -> bản GiaSpDvCongIchCt
                    if (Manhom != "all")
                    {
                        danhmuc = danhmuc.Where(t => t.Manhom == Manhom);
                    }

                    var chitiet = new List<GiaSpDvCongIchCt>();

                    foreach (var item in danhmuc)
                    {
                        chitiet.Add(new GiaSpDvCongIchCt()
                        {
                            Mahs = model.Mahs,
                            Maspdv = item.Maspdv,
                            Ten = item.Tenspdv,
                            Manhom = item.Manhom,
                            Dvt = item.Dvt,
                            HienThi = item.HienThi,
                            Trangthai = "CXD",
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        });
                    }
                    _db.GiaSpDvCongIchCt.AddRange(chitiet);
                    _db.SaveChanges();

                    model.GiaSpDvCongIchCt = chitiet.Where(t => t.Mahs == model.Mahs).ToList();


                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "T");
                    ViewData["GiaSpDvCongIchNhom"] = _db.GiaSpDvCongIchNhom;
                    ViewData["Title"] = "Bảng giá sản phẩm dịch vụ công ích";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdvci";
                    ViewData["MenuLv3"] = "menu_dgdvci_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCongIch/Create.cshtml", model);

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

        [Route("GiaSpDvCongIch/Store")]
        [HttpPost]
        public IActionResult Store(CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCongIch request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.thongtin", "Create"))
                {
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCongIch
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Manhom = request.Manhom,
                        Madiaban = request.Madiaban,
                        Soqd = request.Soqd,
                        Ghichu = request.Ghichu,
                        Thoidiem = request.Thoidiem,
                        Thongtin = request.Thongtin,
                        Trangthai = "CC",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };

                    var modelct = _db.GiaSpDvCongIchCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct.Any())
                    {
                        foreach (var item in modelct)
                        {
                            item.Trangthai = "XD";
                        }
                        _db.GiaSpDvCongIchCt.UpdateRange(modelct);
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

                    _db.GiaSpDvCongIch.Add(model);
                    _db.SaveChanges();

                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Thêm mới");

                    return RedirectToAction("Index", "GiaSpDvCongIch", new { request.Madv });
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

        [Route("GiaSpDvCongIch/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.thongtin", "Delete"))
                {
                    var model = _db.GiaSpDvCongIch.FirstOrDefault(t => t.Id == id_delete);
                    if (model != null)
                    {
                        var model_ct = _db.GiaSpDvCongIchCt.Where(t => t.Mahs == model.Mahs);
                        if (model_ct.Any()) { _db.GiaSpDvCongIchCt.RemoveRange(model_ct); }

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
                        _db.GiaSpDvCongIch.Remove(model);
                        _db.SaveChanges();
                    }
                    return RedirectToAction("Index", "GiaSpDvCongIch", new { model.Madv });
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

        [Route("GiaSpDvCongIch/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.thongtin", "Edit"))
                {
                    var model = _db.GiaSpDvCongIch.FirstOrDefault(t => t.Mahs == Mahs);

                    model.GiaSpDvCongIchCt = _db.GiaSpDvCongIchCt.Where(t => t.Mahs == model.Mahs).ToList();
                    model.ThongTinGiayTo = _db.ThongTinGiayTo.Where(t => t.Mahs == model.Mahs).ToList();

                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["GiaSpDvCongIchNhom"] = _db.GiaSpDvCongIchNhom.ToList();
                    ViewData["GiaSpDvCongIchDm"] = _db.GiaSpDvCongIchDm.ToList();
                    ViewData["Title"] = "Bảng giá tính sản phẩm dịch vụ công ích";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdvci";
                    ViewData["MenuLv3"] = "menu_dgdvci_tt";
                    ViewData["GroupMaNhom"] = _db.GiaSpDvCongIchNhom;
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCongIch/Modify.cshtml", model);

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

        [Route("GiaSpDvCongIch/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.thongtin", "Edit"))
                {
                    var model = _db.GiaSpDvCongIch.FirstOrDefault(t => t.Mahs == Mahs);

                    var model_ct = _db.GiaSpDvCongIchCt.Where(t => t.Mahs == model.Mahs);
                    model.GiaSpDvCongIchCt = model_ct.ToList();

                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["GiaSpDvCongIchNhom"] = _db.GiaSpDvCongIchNhom;
                    ViewData["Title"] = "Bảng giá tính sản phẩm dịch vụ công ích";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdvci";
                    ViewData["MenuLv3"] = "menu_dgdvci_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCongIch/Show.cshtml", model);

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

        [Route("DinhGiaSpDvCongIch/Update")]
        [HttpPost]
        public IActionResult Update(CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCongIch request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.datcuthe.thongtin", "Edit"))
                {
                    var model = _db.GiaSpDvCongIch.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Madiaban = request.Madiaban;
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Thongtin = request.Thongtin;
                    model.Ipf1 = request.Ipf1;
                    model.Ghichu = request.Ghichu;
                    model.Updated_at = DateTime.Now;
                    _db.GiaSpDvCongIch.Update(model);

                    var modelct = _db.GiaSpDvCongIchCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct.Any())
                    {
                        foreach (var item in modelct)
                        {
                            item.Mahs = model.Mahs;
                        }
                        _db.GiaSpDvCongIchCt.UpdateRange(modelct);
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

                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Cập nhật");

                    return RedirectToAction("Index", "GiaSpDvCongIch", new { request.Mahs });
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

        [Route("DinhGiaSpDvCongIch/Print")]
        [HttpGet]
        public IActionResult Print(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.thongtin", "Index"))
                {
                    var model = _db.GiaSpDvCongIch.FirstOrDefault(t => t.Mahs == Mahs);

                    var hoso_dg = new VMDinhGiaPrint
                    {
                        Id = model.Id,

                        Phanloaidv = model.Phanloai,
                        Mahs = model.Mahs,
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Ghichu = model.Ghichu,
                    };

                    var modeldv = _db.DsDonVi.FirstOrDefault(t => t.MaDv == model.Madv);

                    if (modeldv != null)
                    {
                        hoso_dg.Tendv = modeldv.TenDvHienThi;
                    }

                    var modeldb = _db.DsDiaBan.FirstOrDefault(t => t.MaDiaBan == modeldv.MaDiaBan);
                    if (modeldb != null)
                    {
                        hoso_dg.Tendb = modeldb.TenDiaBan;
                    }

                    var modelct = _db.GiaSpDvCongIchCt.Where(t => t.Mahs == model.Mahs);
                    if (modelct != null)
                    {
                        hoso_dg.GiaSpDvCongIchCt = modelct.ToList();
                    }

                    /*var model = GetThongTinKk(Mahs);*/


                    ViewData["Title"] = "In định giá sản phẩm dịch vụ công ích";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdvci";
                    ViewData["MenuLv3"] = "menu_dgdvci_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCongIch/Print.cshtml", hoso_dg);

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

        public IActionResult Complete(string mahs_chuyen, string trangthai_complete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.thongtin", "Approve"))
                {
                    var model = _db.GiaSpDvCongIch.FirstOrDefault(t => t.Mahs == mahs_chuyen);
                    model.Updated_at = DateTime.Now;
                    model.Trangthai = trangthai_complete;

                    _db.GiaSpDvCongIch.Update(model);
                    _db.SaveChanges();

                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), trangthai_complete);
                    return RedirectToAction("Index", "GiaSpDvCongIch", new { model.Madv });

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

        [Route("GiaSpDvCongIch/TimKiem")]
        [HttpGet]
        public IActionResult Search(string Madv, string Manhom, DateTime? NgayTu, DateTime? NgayDen, string Mahs, double DonGiaTu, double DonGiaDen, string Ten)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.timkiem", "Index"))
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



                    var model = (from hosoct in _db.GiaSpDvCongIchCt
                                 join hoso in _db.GiaSpDvCongIch on hosoct.Mahs equals hoso.Mahs
                                 join nhom in _db.GiaSpDvCongIchNhom on hosoct.Manhom equals nhom.Manhom
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCongIchCt
                                 {
                                     Madv = hoso.Madv,
                                     Tendv = donvi.TenDv,
                                     Tennhom = nhom.Tennhom,
                                     Soqd = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                     Ten = hosoct.Ten,
                                     Dvt = hosoct.Dvt,
                                     Mucgiatu = hosoct.Mucgiatu,
                                     Mucgiaden = hosoct.Mucgiaden,
                                     Mucgia3 = hosoct.Mucgia3,
                                     Mucgia4 = hosoct.Mucgia4,
                                     Manhom = hosoct.Manhom,
                                     Trangthai = hoso.Trangthai,
                                     Mahs = hoso.Mahs
                                 });
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model = model.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen && list_trangthai.Contains(t.Trangthai));
                    if (Madv != "all") { model = model.Where(t => t.Madv == Madv); }
                    if (Manhom != "all") { model = model.Where(t => t.Manhom == Manhom); }
                    if (DonGiaTu > 0) { model = model.Where(t => t.Mucgiatu >= DonGiaTu || t.Mucgiaden >= DonGiaTu || t.Mucgia3 >= DonGiaTu || t.Mucgia4 >= DonGiaTu); }
                    if (DonGiaDen > 0) { model = model.Where(t => t.Mucgia4 <= DonGiaDen || t.Mucgiaden <= DonGiaDen || t.Mucgia3 <= DonGiaDen || t.Mucgia4 <= DonGiaDen); }
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
                    ViewData["DonGiaTu"] = Helpers.ConvertDbToStr(DonGiaTu);
                    ViewData["DonGiaDen"] = Helpers.ConvertDbToStr(DonGiaDen);
                    ViewData["Ten"] = Ten;
                    ViewData["DanhSachHoSo"] = _db.GiaSpDvCongIch.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen && t.Trangthai == "HT");
                    ViewData["DanhMucNhom"] = _db.GiaSpDvCongIchNhom;

                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá sản phẩm dịch vụ công ích";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdvci";
                    ViewData["MenuLv3"] = "menu_dgdvci_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCongIch/TimKiem/Search.cshtml", model);

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

        [Route("GiaSpDvCongIch/TimKiem/PrintSearch")]
        [HttpGet]
        public IActionResult Print(string Madv, string Manhom, DateTime? NgayTu, DateTime? NgayDen, string Mahs, double DonGiaTu, double DonGiaDen, string Ten)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.timkiem", "Index"))
                {

                    var model = (from hosoct in _db.GiaSpDvCongIchCt
                                 join hoso in _db.GiaSpDvCongIch on hosoct.Mahs equals hoso.Mahs
                                 join nhom in _db.GiaSpDvCongIchNhom on hosoct.Manhom equals nhom.Manhom
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCongIchCt
                                 {
                                     Madv = hoso.Madv,
                                     Tendv = donvi.TenDv,
                                     Tennhom = nhom.Tennhom,
                                     Soqd = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                     Ten = hosoct.Ten,
                                     Dvt = hosoct.Dvt,
                                     Mucgiatu = hosoct.Mucgiatu,
                                     Mucgiaden = hosoct.Mucgiaden,
                                     Mucgia3 = hosoct.Mucgia3,
                                     Mucgia4 = hosoct.Mucgia4,
                                     Manhom = hosoct.Manhom,
                                     Trangthai = hoso.Trangthai,
                                     Mahs = hoso.Mahs
                                 });
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model = model.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen && list_trangthai.Contains(t.Trangthai));
                    if (Madv != "all") { model = model.Where(t => t.Madv == Madv); }
                    if (Manhom != "all") { model = model.Where(t => t.Manhom == Manhom); }
                    if (DonGiaTu > 0) { model = model.Where(t => t.Mucgiatu >= DonGiaTu || t.Mucgiaden >= DonGiaTu || t.Mucgia3 >= DonGiaTu || t.Mucgia4 >= DonGiaTu); }
                    if (DonGiaDen > 0) { model = model.Where(t => t.Mucgia4 <= DonGiaDen || t.Mucgiaden <= DonGiaDen || t.Mucgia3 <= DonGiaDen || t.Mucgia4 <= DonGiaDen); }
                    if (Mahs != "all") { model = model.Where(t => t.Mahs == Mahs); }
                    if (!string.IsNullOrEmpty(Ten))
                    {
                        model = model.Where(t => t.Ten.ToLower().Contains(Ten.ToLower()));
                    }

                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá sản phẩm dịch vụ công ích";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdvci";
                    ViewData["MenuLv3"] = "menu_dgdvci_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCongIch/TimKiem/Result.cshtml", model);
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


        [HttpPost("GiaSpDvCongIch/GetListHoSo")]
        public JsonResult GetListHoSo(DateTime ngaytu, DateTime ngayden)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                var model = _db.GiaSpDvCongIch.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && list_trangthai.Contains(t.Trangthai));
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
