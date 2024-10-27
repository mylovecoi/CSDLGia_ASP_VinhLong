using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.Services;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDatCuThe
{
    public class GiaDatCuTheVlController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IDsDonviService _dsDonviService;
        private readonly ITrangThaiHoSoService _trangThaiHoSoService;

        public GiaDatCuTheVlController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment, IDsDonviService dsDonviService, ITrangThaiHoSoService trangThaiHoSoService)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
            _dsDonviService = dsDonviService;
            _trangThaiHoSoService = trangThaiHoSoService;
        }

        [Route("GiaDatCuTheVl")]
        [HttpGet]
        public IActionResult Index(string Madv, int Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthevinhlong.thongtin", "Index"))
                {
                    Madv = string.IsNullOrEmpty(Madv) ? Helpers.GetSsAdmin(HttpContext.Session, "Madv") : Madv;

                    var model_donvi = _dsDonviService.GetListDonvi(Helpers.GetSsAdmin(HttpContext.Session, "Madv"));
                    List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();

                    IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaDatCuTheVl> model = _db.GiaDatCuTheVl.Where(t => list_madv.Contains(t.Madv));

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
                    ViewData["Danhmucpp"] = _db.GiaDatCuTheVlDmPPDGDat.ToList();
                    ViewData["DsDonvi"] = model_donvi;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(x => x.Level != "X");
                    ViewData["Title"] = " Thông tin hồ sơ giá đất cụ thể";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgdctvl";
                    ViewData["MenuLv3"] = "menu_dgdctvl_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaDatCuTheVl/DanhSach/Index.cshtml", model);

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

        [Route("GiaDatCuTheVl/Create")]
        [HttpGet]
        public IActionResult Create(string Madv, string Mapp)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthevinhlong.thongtin", "Create"))
                {
                    var model_ct_cxd = _db.GiaDatCuTheVlCt.Where(t => t.Trangthai == "CXD" && t.Madv == Madv);
                    if (model_ct_cxd.Any())
                    {
                        _db.GiaDatCuTheVlCt.RemoveRange(model_ct_cxd);
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


                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaDatCuTheVl
                    {
                        Madv = Madv,
                        Thoidiem = DateTime.Now,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                    };

                    IEnumerable<GiaDatCuTheVlDmPPDGDatCt> danhmuc = _db.GiaDatCuTheVlDmPPDGDatCt;
                    if (Mapp != "all")
                    {
                        danhmuc = danhmuc.Where(t => t.Mapp == Mapp);
                    }

                    var chitiet = new List<GiaDatCuTheVlCt>();


                    foreach (var item in danhmuc)
                    {
                        chitiet.Add(new GiaDatCuTheVlCt()
                        {
                            Mahs = model.Mahs,
                            STTSapXep = item.SapXep,
                            STTHienThi = item.HienThi,
                            Mapp = item.Mapp,
                            Style = item.Style,
                            NhapGia = item.NhapGia,
                            Noidungcv = item.Noidungcv,
                            Trangthai = "CXD",
                            Madv = Madv,
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        });
                    }


                    _db.GiaDatCuTheVlCt.AddRange(chitiet);
                    _db.SaveChanges();
                    model.GiaDatCuTheVlCt = chitiet.Where(t => t.Mahs == model.Mahs).ToList();

                    var donVi = _db.DsDonVi.FirstOrDefault(x => x.MaDv == model.Madv);
                    string diaBanApDung = donVi?.DiaBanApDung ?? "";
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Dmloaidat"] = _db.DmLoaiDat;
                    ViewData["DanhMucPp"] = _db.GiaDatCuTheVlDmPPDGDat;
                    ViewData["Title"] = " Thêm mới hồ sơ giá đất cụ thể";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgdctvl";
                    ViewData["MenuLv3"] = "menu_dgdctvl_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaDatCuTheVl/DanhSach/Create.cshtml", model);
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

        [Route("GiaDatCuTheVl/Store")]
        [HttpPost]
        public IActionResult Store(CSDLGia_ASP.Models.Manages.DinhGia.GiaDatCuTheVl request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthevinhlong.thongtin", "Create"))
                {
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaDatCuTheVl
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
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

                    var modelct = _db.GiaDatCuTheVlCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct.Any())
                    {
                        foreach (var ct in modelct)
                        {
                            ct.Trangthai = "XD";
                        }
                        _db.GiaDatCuTheVlCt.UpdateRange(modelct);
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

                    _db.GiaDatCuTheVl.Add(model);
                    _db.SaveChanges();

                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Thêm mới");

                    // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                    LoggingHelper.LogAction(HttpContext, _db, "Store", "Thêm mới hồ sơ giá đất cụ thể");

                    return RedirectToAction("Index", "GiaDatCuTheVl", new { request.Madv });
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

        [Route("GiaDatCuTheVl/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthevinhlong.thongtin", "Edit"))
                {
                    var model = _db.GiaDatCuTheVl.FirstOrDefault(t => t.Mahs == Mahs);
                    model.GiaDatCuTheVlCt = _db.GiaDatCuTheVlCt.Where(t => t.Mahs == Mahs).ToList();
                    model.ThongTinGiayTo = _db.ThongTinGiayTo.Where(t => t.Mahs == Mahs).ToList();
                    ViewData["DanhMucPp"] = _db.GiaDatCuTheVlDmPPDGDat;
                    var donVi = _db.DsDonVi.FirstOrDefault(x => x.MaDv == model.Madv);
                    string diaBanApDung = donVi?.DiaBanApDung ?? "";
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    //ViewData["DsDiaBanApDung"] = _db.Districts.Where(x => diaBanApDung.Contains(x.Mahuyen));
                    ViewData["Title"] = " Chỉnh sửa hồ sơ giá đất cụ thể";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgdctvl";
                    ViewData["MenuLv3"] = "menu_dgdctvl_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaDatCuTheVl/DanhSach/Edit.cshtml", model);
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

        [Route("GiaDatCuTheVl/Update")]
        [HttpPost]
        public IActionResult Update(CSDLGia_ASP.Models.Manages.DinhGia.GiaDatCuTheVl request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthevinhlong.thongtin", "Edit"))
                {
                    var model = _db.GiaDatCuTheVl.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Madiaban = request.Madiaban;
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Thongtin = request.Thongtin;
                    model.Ghichu = request.Ghichu;
                    model.Updated_at = DateTime.Now;


                    var modelct = _db.GiaDatCuTheVlCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Mahs = model.Mahs;
                        }
                    }
                    var model_file = _db.ThongTinGiayTo.Where(t => t.Mahs == request.Mahs);
                    foreach (var file in model_file) { file.Status = "XD"; }
                    _db.ThongTinGiayTo.UpdateRange(model_file);
                    _db.GiaDatCuTheVl.Update(model);
                    _db.GiaDatCuTheVlCt.UpdateRange(modelct);
                    _db.SaveChanges();


                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Cập nhật");

                    // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                    LoggingHelper.LogAction(HttpContext, _db, "Update", "Cập nhật hồ sơ giá đất cụ thể");

                    return RedirectToAction("Index", "GiaDatCuTheVl", new { request.Madv });
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

        [Route("GiaDatCuTheVl/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthevinhlong.thongtin", "Delete"))
                {
                    var model = _db.GiaDatCuTheVl.FirstOrDefault(t => t.Id == id_delete);
                    if (model != null)
                    {
                        var model_ct = _db.GiaDatCuTheVlCt.Where(t => t.Mahs == model.Mahs);
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
                        if (model_ct.Any()) { _db.GiaDatCuTheVlCt.RemoveRange(model_ct); }

                        _db.GiaDatCuTheVl.Remove(model);
                        _db.SaveChanges();
                    }

                    // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                    LoggingHelper.LogAction(HttpContext, _db, "Delete", "Xóa hồ sơ giá đất cụ thể");

                    return RedirectToAction("Index", "GiaDatCuTheVl", new { model.Madv });
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

        [Route("GiaDatCuTheVl/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthevinhlong.thongtin", "Edit"))
                {
                    var model = _db.GiaDatCuTheVl.FirstOrDefault(t => t.Mahs == Mahs);
                    var modelct = _db.GiaDatCuTheVlCt.Where(t => t.Mahs == Mahs);
                    model.GiaDatCuTheVlCt = modelct.ToList();
                    ViewData["DanhMucPp"] = _db.GiaDatCuTheVlDmPPDGDat;
                    ViewData["Title"] = "Chi tiết giá đất cụ thể";
                    return View("Views/Admin/Manages/DinhGia/GiaDatCuTheVl/DanhSach/Show.cshtml", model);
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

        public IActionResult Chuyen(string mahs_complete, string trangthai_complete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthevinhlong.thongtin", "Approve"))
                {
                    var model = _db.GiaDatCuTheVl.FirstOrDefault(p => p.Mahs == mahs_complete);
                    model.Trangthai = trangthai_complete;
                    model.Updated_at = DateTime.Now;

                    _db.GiaDatCuTheVl.Update(model);
                    _db.SaveChanges();

                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), trangthai_complete);

                    // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                    LoggingHelper.LogAction(HttpContext, _db, "Chuyen", "Chuyển hồ sơ giá đất cụ thể ");


                    return RedirectToAction("Index", "GiaDatCuTheVl", new { Madv = model.Madv, Nam = model.Thoidiem.Year });
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

        [Route("GiaDatCuTheVl/Search")]
        [HttpGet]
        public IActionResult Search(string Madv, string Mapp, DateTime? NgayTu, DateTime? NgayDen, string Mahs, double DonGiaTu, double DonGiaDen, string Noidungcv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthevinhlong.timkiem", "Index"))
                {
                    DateTime nowDate = DateTime.Now;
                    DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
                    DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);

                    Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;
                    Mapp = string.IsNullOrEmpty(Mapp) ? "all" : Mapp;
                    NgayTu = NgayTu.HasValue ? NgayTu : firstDayCurrentYear;
                    NgayDen = NgayDen.HasValue ? NgayDen : lastDayCurrentYear;
                    Mahs = string.IsNullOrEmpty(Mahs) ? "all" : Mahs;
                    DonGiaTu = DonGiaTu == 0 ? 0 : DonGiaTu;
                    DonGiaDen = DonGiaDen == 0 ? 0 : DonGiaDen;
                    Noidungcv = string.IsNullOrEmpty(Noidungcv) ? "" : Noidungcv;

                    var model = (from hosoct in _db.GiaDatCuTheVlCt
                                 join hoso in _db.GiaDatCuTheVl on hosoct.Mahs equals hoso.Mahs
                                 join dm in _db.GiaDatCuTheVlDmPPDGDat on hosoct.Mapp equals dm.Mapp
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaDatCuTheVlCt
                                 {
                                     Madv = hoso.Madv,
                                     Tendv = donvi.TenDv,
                                     Tenpp = dm.Tenpp,
                                     Soqd = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                     Noidungcv = hosoct.Noidungcv,
                                     ChiPhiNhanCong = hosoct.ChiPhiNhanCong,
                                     ChiPhiDungCu = hosoct.ChiPhiDungCu,
                                     ChiPhiNangLuong = hosoct.ChiPhiNangLuong,
                                     ChiPhiKhauHao = hosoct.ChiPhiKhauHao,
                                     ChiPhiVatLieu = hosoct.ChiPhiVatLieu,
                                     ChiPhiTrucTiepKkh = hosoct.ChiPhiTrucTiepKkh,
                                     ChiPhiTrucTiepCkh = hosoct.ChiPhiTrucTiepCkh,
                                     ChiPhiQlChungKkh = hosoct.ChiPhiQlChungKkh,
                                     ChiPhiQlChungCkh = hosoct.ChiPhiQlChungCkh,
                                     DonGiaKkh = hosoct.DonGiaKkh,
                                     DonGiaCkh = hosoct.DonGiaCkh,
                                     STTHienThi = hosoct.STTHienThi,
                                     Mapp = hosoct.Mapp,
                                     Trangthai = hoso.Trangthai,
                                     Mahs = hoso.Mahs
                                 });
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model = model.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen && list_trangthai.Contains(t.Trangthai));
                    if (Madv != "all") { model = model.Where(t => t.Madv == Madv); }
                    if (Mapp != "all") { model = model.Where(t => t.Mapp == Mapp); }
                    if (!string.IsNullOrEmpty(Noidungcv))
                    {
                        model = model.Where(t => t.Noidungcv.ToLower().Contains(Noidungcv.ToLower()));
                    }

                    ViewData["Madv"] = Madv;
                    ViewData["Mapp"] = Mapp;
                    ViewData["NgayTu"] = NgayTu;
                    ViewData["NgayDen"] = NgayDen;
                    ViewData["Mahs"] = Mahs;
                    ViewData["DonGiaTu"] = Helpers.ConvertDbToStr(DonGiaTu);
                    ViewData["DonGiaDen"] = Helpers.ConvertDbToStr(DonGiaDen);
                    ViewData["Noidungcv"] = Noidungcv;
                    ViewData["DanhSachHoSo"] = _db.GiaDatCuTheVl.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen && list_trangthai.Contains(t.Trangthai));
                    ViewData["DanhMucPp"] = _db.GiaDatCuTheVlDmPPDGDat;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");

                    ViewData["Title"] = "Tìm kiếm thông tin giá đất cụ thể";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgdctvl";
                    ViewData["MenuLv3"] = "menu_dgdctvl_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaDatCuTheVl/TimKiem/Search.cshtml", model);

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

        [Route("GiaDatCuTheVl/Print")]
        [HttpGet]
        public IActionResult Print(string Madv, string Mapp, DateTime? NgayTu, DateTime? NgayDen, string Mahs, double DonGiaTu, double DonGiaDen, string Noidungcv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthevinhlong.timkiem", "Index"))
                {
                    var model = (from hosoct in _db.GiaDatCuTheVlCt
                                 join hoso in _db.GiaDatCuTheVl on hosoct.Mahs equals hoso.Mahs
                                 join dm in _db.GiaDatCuTheVlDmPPDGDat on hosoct.Mapp equals dm.Mapp
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaDatCuTheVlCt
                                 {
                                     Madv = hoso.Madv,
                                     Tendv = donvi.TenDv,
                                     Tenpp = dm.Tenpp,
                                     Soqd = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                     Noidungcv = hosoct.Noidungcv,
                                     ChiPhiNhanCong = hosoct.ChiPhiNhanCong,
                                     ChiPhiDungCu = hosoct.ChiPhiDungCu,
                                     ChiPhiNangLuong = hosoct.ChiPhiNangLuong,
                                     ChiPhiKhauHao = hosoct.ChiPhiKhauHao,
                                     ChiPhiVatLieu = hosoct.ChiPhiVatLieu,
                                     ChiPhiTrucTiepKkh = hosoct.ChiPhiTrucTiepKkh,
                                     ChiPhiTrucTiepCkh = hosoct.ChiPhiTrucTiepCkh,
                                     ChiPhiQlChungKkh = hosoct.ChiPhiQlChungKkh,
                                     ChiPhiQlChungCkh = hosoct.ChiPhiQlChungCkh,
                                     DonGiaKkh = hosoct.DonGiaKkh,
                                     DonGiaCkh = hosoct.DonGiaCkh,
                                     STTHienThi = hosoct.STTHienThi,
                                     Mapp = hosoct.Mapp,
                                     Trangthai = hoso.Trangthai,
                                     Mahs = hoso.Mahs
                                 });
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model = model.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen && list_trangthai.Contains(t.Trangthai));
                    if (Madv != "all") { model = model.Where(t => t.Madv == Madv); }
                    if (Mapp != "all") { model = model.Where(t => t.Mapp == Mapp); }
                    if (!string.IsNullOrEmpty(Noidungcv))
                    {
                        model = model.Where(t => t.Noidungcv.ToLower().Contains(Noidungcv.ToLower()));
                    }

                    ViewData["Title"] = "Tìm kiếm thông tin giá đất cụ thể";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgdctvl";
                    ViewData["MenuLv3"] = "menu_dgdctvl_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaDatCuTheVl/TimKiem/Result.cshtml", model);
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

        [HttpPost("GiaDatCuTheVl/GetHoSoSearch")]
        public JsonResult GetListHoSo(DateTime ngaytu, DateTime ngayden)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                var model = _db.GiaThueMatDatMatNuoc.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && list_trangthai.Contains(t.Trangthai));
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
