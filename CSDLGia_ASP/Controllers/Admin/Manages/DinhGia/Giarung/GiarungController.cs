using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CSDLGia_ASP.Database;
using System.Security.Cryptography;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CSDLGia_ASP.ViewModels.Manages.KeKhaiGia;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.Services;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.Giarung
{
    public class GiarungController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IDsDonviService _dsDonviService;

        public GiarungController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment, IDsDonviService dsDonviService)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
            _dsDonviService = dsDonviService;
        }

        [Route("GiaRung")]
        [HttpGet]
        public IActionResult Index(string Madv, int Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.rung.thongtin", "Index"))
                {
                    Madv = string.IsNullOrEmpty(Madv) ? Helpers.GetSsAdmin(HttpContext.Session, "Madv") : Madv;
                    var model_donvi = _dsDonviService.GetListDonvi(Helpers.GetSsAdmin(HttpContext.Session, "Madv"));
                    List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();

                    IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaRung> model = _db.GiaRung.Where(t=>list_madv.Contains(t.Madv));
                    if (Madv != "all")
                    {
                        model = model.Where(t => t.Madv == Madv);
                    }

                    if (Nam != 0)
                    {
                        model = model.Where(t => t.Thoidiem.Year == Nam);
                    }

                    ViewData["Nam"] = Nam;
                    ViewData["Donvi"] = Madv;
                    ViewData["DsDonvi"] = model_donvi;
                    ViewData["Loairung"] = _db.GiaRungDm.ToList();
                    ViewData["Title"] = " Quản lý thông tin hồ sơ giá rừng";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgr";
                    ViewData["MenuLv3"] = "menu_dgr_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaRung/Index.cshtml", model);


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

        [Route("GiaRung/Create")]
        [HttpGet]
        public IActionResult Create(string Madv_create, string Manhom_create)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.rung.thongtin", "Create"))
                {
                    var modelct_cxd = _db.GiaRungCt.Where(t => t.Madv == Madv_create && t.Trangthai == "CXD");
                    if (modelct_cxd.Any())
                    {
                        _db.GiaRungCt.RemoveRange(modelct_cxd);
                        _db.SaveChanges();
                    }
                    var model_file_cxd = _db.ThongTinGiayTo.Where(t => t.Status == "CXD" && t.Madv == Madv_create);
                    if (model_file_cxd.Any())
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        foreach (var file in model_file_cxd)
                        {
                            if (!string.IsNullOrEmpty(file.FileName))
                            {
                                string path_del = Path.Combine(wwwRootPath + "/UpLoad/File/ThongTinGiayTo/", file.FileName);
                                FileInfo fi = new FileInfo(path_del);
                                if (fi != null)
                                {
                                    System.IO.File.Delete(path_del);
                                    fi.Delete();
                                }
                            }
                        }
                        _db.ThongTinGiayTo.RemoveRange(model_file_cxd);
                        _db.SaveChanges();
                    }
                    string Mahs = Madv_create + "_" + DateTime.Now.ToString("yyMMddssmmHH");
                    var model = new VMDinhGiaRung
                    {
                        Madv = Madv_create,
                        Thoidiem = DateTime.Now,
                        Mahs = Mahs,
                        Dvt = "Triệu đồng/ha"
                    };
                    IQueryable<GiaRungDmCt> data_dm = _db.GiaRungDmCt;
                    if (Manhom_create != "all")
                    {
                        data_dm = data_dm.Where(t => t.Manhom == Manhom_create);
                    }
                    var chitiet = new List<GiaRungCt>();
                    foreach (var item in data_dm)
                    {
                        chitiet.Add(new GiaRungCt
                        {
                            Mahs = Mahs,
                            Madv = Madv_create,
                            Trangthai = "CXD",
                            Style = item.Style,
                            STTHienThi = item.STTHienThi,
                            STTSapXep = item.STTSapXep,
                            MoTa = item.MoTa,
                            Manhom = item.Manhom
                        });
                    }
                    _db.GiaRungCt.AddRange(chitiet);
                    _db.SaveChanges();

                    model.GiaRungCt = chitiet.Where(t => t.Mahs == model.Mahs).ToList();

                    ViewData["NhomDm"] = _db.GiaRungDm;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Dmdvt"] = _db.DmDvt.ToList();
                    ViewData["Title"] = "Thêm mới giá rừng";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgr";
                    ViewData["MenuLv3"] = "menu_dgr_tt";

                    return View("Views/Admin/Manages/DinhGia/GiaRung/Create.cshtml", model);
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

        [Route("GiaRung/NhanExcel")]
        [HttpGet]
        public IActionResult NhanExcel(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.rung.thongtin", "Create"))
                {

                    var model = new VMDinhGiaRung
                    {
                        Madv = Madv,
                        Thoidiem = DateTime.Now,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        PhanLoaiHoSo = "HOSONHANEXCEL",

                    };
                    ViewData["codeExcel"] = model.CodeExcel;//Gán ra view để dùng chung
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Title"] = "Thêm mới giá rừng";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgr";
                    ViewData["MenuLv3"] = "menu_dgr_tt";

                    return View("Views/Admin/Manages/DinhGia/GiaRung/NhanExcel.cshtml", model);
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

        [Route("GiaRung/Store")]
        [HttpPost]
        public IActionResult Store(CSDLGia_ASP.Models.Manages.DinhGia.GiaRung request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.rung.thongtin", "Create"))
                {   
                    var model = new GiaRung
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Madiaban = request.Madiaban,
                        Soqd = request.Soqd,
                        Thoidiem = request.Thoidiem,
                        Dvt = request.Dvt,
                        Thongtin = request.Thongtin,
                        Ghichu = request.Ghichu,
                        PhanLoaiHoSo = request.PhanLoaiHoSo,
                        CodeExcel = request.CodeExcel,
                        Trangthai = "CC",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaRung.Add(model);
                    _db.SaveChanges();

                    // Xử lý phần lịch sử hồ sơ 

                    var lichSuHoSo = new TrangThaiHoSo
                    {
                        MaHoSo = request.Mahs,
                        TenDangNhap = Helpers.GetSsAdmin(HttpContext.Session, "Name"),
                        ThongTin = "Thay đổi thông tin hồ sơ",
                        ThoiGian = DateTime.Now,
                        TrangThai = "CHT",

                    };
                    _db.TrangThaiHoSo.Add(lichSuHoSo);
                    _db.SaveChanges();

                    //Kết thúc Xử lý phần lịch sử hồ sơ 

                    var modelct = _db.GiaRungCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct.Any())
                    {
                        foreach (var ct in modelct) { ct.Trangthai = "XD"; }
                        _db.GiaRungCt.UpdateRange(modelct);
                        _db.SaveChanges();
                    }

                    var model_file = _db.ThongTinGiayTo.Where(t => t.Mahs == request.Mahs);
                    if (model_file.Any())
                    {
                        foreach (var file in model_file) { file.Status = "XD"; }
                        _db.ThongTinGiayTo.UpdateRange(model_file);
                        _db.SaveChanges();
                    }

                    return RedirectToAction("Index", "Giarung", new { Madv = request.Madv, Nam = request.Thoidiem.Year });
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

        [Route("GiaRung/Modify")]
        [HttpGet]
        public IActionResult Modify(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.rung.thongtin", "Edit"))
                {
                    var model = _db.GiaRung.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new VMDinhGiaRung
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        Madiaban = model.Madiaban,
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Thongtin = model.Thongtin,
                        Noidung = model.Noidung,
                        Ghichu = model.Ghichu,
                        Dvt = model.Dvt
                    };

                    var model_ct = _db.GiaRungCt.Where(t => t.Mahs == model_new.Mahs);
                    var model_file = _db.ThongTinGiayTo.Where(t => t.Mahs == model_new.Mahs);

                    model_new.GiaRungCt = model_ct.ToList();
                    model_new.ThongTinGiayTo = model_file.ToList();
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Loairung"] = _db.GiaRungDm.ToList();
                    ViewData["Dmdvt"] = _db.DmDvt.ToList();
                    ViewData["NhomDm"] = _db.GiaRungDm;
                    ViewData["Title"] = "Quản lý thông tin hồ sơ giá rừng";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgr";
                    ViewData["MenuLv3"] = "menu_dgr_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaRung/Modify.cshtml", model_new);
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

        [Route("GiaRung/Update")]
        [HttpPost]
        public IActionResult Update(VMDinhGiaRung request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.rung.thongtin", "Edit"))
                {
                    var model = _db.GiaRung.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Madiaban = request.Madiaban;
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Thongtin = request.Thongtin;
                    model.Ghichu = request.Ghichu;
                    model.Dvt = request.Dvt;
                    model.Updated_at = DateTime.Now;
                    _db.GiaRung.Update(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaRungCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct.Any())
                    {
                        foreach (var item in modelct)
                        {
                            item.Trangthai = "XD";
                        }
                        _db.GiaRungCt.UpdateRange(modelct);
                    }
                    var model_file = _db.ThongTinGiayTo.Where(t => t.Mahs == request.Mahs);
                    if (model_file.Any())
                    {
                        foreach (var file in model_file) { file.Status = "XD"; }
                        _db.ThongTinGiayTo.UpdateRange(model_file);
                    }
                    _db.SaveChanges();

                    return RedirectToAction("Index", "Giarung", new { Madv = request.Madv, Nam = request.Thoidiem.Year });
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

        [Route("GiaRung/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.rung.thongtin", "Delete"))
                {
                    var model = _db.GiaRung.FirstOrDefault(t => t.Id == id_delete);
                    if (model != null)
                    {
                        var model_ct = _db.GiaRungCt.Where(t => t.Mahs == model.Mahs);

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
                        if (model_ct.Any())
                        {
                            _db.GiaRungCt.RemoveRange(model_ct);
                        }
                        _db.GiaRung.Remove(model);
                        _db.SaveChanges();
                    }

                    return RedirectToAction("Index", "Giarung", new { model.Madv });
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

        [Route("GiaRung/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.rung.thongtin", "Edit"))
                {
                    var model = _db.GiaRung.FirstOrDefault(t => t.Mahs == Mahs);
                    var modelct = _db.GiaRungCt.Where(t => t.Mahs == Mahs);
                    model.GiaRungCt = modelct.ToList();
                    ViewData["NhomDm"] = _db.GiaRungDm;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["Title"] = "Chi tiết giá rừng";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_nhomhhdv";
                    ViewData["MenuLv3"] = "menu_dgr";
                    ViewData["MenuLv4"] = "menu_dgr_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaRung/Show.cshtml", model);
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.rung.thongtin", "Approve"))
                {
                    var model = _db.GiaRung.FirstOrDefault(p => p.Mahs == mahs_complete);

                    model.Updated_at = DateTime.Now;
                    model.Trangthai = trangthai_complete;

                    _db.GiaRung.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Giarung", new { Madv = model.Madv, Nam = model.Thoidiem.Year });
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


        [Route("GiaRung/Search")]
        [HttpGet]
        public IActionResult Search(string Madv, string Manhom, DateTime? NgayTu, DateTime? NgayDen, string Mahs, double DonGiaTu, double DonGiaDen, string MoTa)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.rung.timkiem", "Index"))
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
                    MoTa = string.IsNullOrEmpty(MoTa) ? "" : MoTa;

                    var model = (from hosoct in _db.GiaRungCt
                                 join hoso in _db.GiaRung on hosoct.Mahs equals hoso.Mahs
                                 join nhom in _db.GiaRungDm on hosoct.Manhom equals nhom.Manhom
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaRungCt
                                 {
                                     Madv = hoso.Madv,
                                     Tendv = donvi.TenDv,
                                     Tennhom = nhom.Tennhom,
                                     SoQD = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                     MoTa = hosoct.MoTa,
                                     GiaRung1 = hosoct.GiaRung1,
                                     GiaRung2 = hosoct.GiaRung2,
                                     GiaRung3 = hosoct.GiaRung3,
                                     GiaRung4 = hosoct.GiaRung4,
                                     GiaRung5 = hosoct.GiaRung5,
                                     GiaRung6 = hosoct.GiaRung6,
                                     GiaChoThue1 = hosoct.GiaChoThue1,
                                     GiaChoThue2 = hosoct.GiaChoThue2,
                                     GiaBoiThuong1 = hosoct.GiaBoiThuong1,
                                     GiaBoiThuong2 = hosoct.GiaBoiThuong2,
                                     GiaBoiThuong3 = hosoct.GiaBoiThuong3,
                                     GiaBoiThuong4 = hosoct.GiaBoiThuong4,
                                     GiaBoiThuong5 = hosoct.GiaBoiThuong5,
                                     GiaBoiThuong6 = hosoct.GiaBoiThuong6,
                                     Manhom = hosoct.Manhom,
                                     Trangthai = hoso.Trangthai,
                                     Mahs = hoso.Mahs
                                 });

                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model = model.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen && list_trangthai.Contains(t.Trangthai));
                    if (Madv != "all") { model = model.Where(t => t.Madv == Madv); }
                    if (Manhom != "all") { model = model.Where(t => t.Manhom == Manhom); }

                    if (DonGiaTu > 0)
                    {
                        model = model.Where(t => t.GiaRung1 >= DonGiaTu || t.GiaRung3 >= DonGiaTu || t.GiaRung5 >= DonGiaTu || t.GiaChoThue1 >= DonGiaTu
                                            || t.GiaBoiThuong1 >= DonGiaTu || t.GiaBoiThuong3 >= DonGiaTu || t.GiaBoiThuong5 >= DonGiaTu);
                    }
                    if (DonGiaDen > 0)
                    {
                        model = model.Where(t => t.GiaRung2 <= DonGiaDen || t.GiaRung4 <= DonGiaDen || t.GiaRung6 <= DonGiaDen || t.GiaChoThue2 <= DonGiaDen
                                            || t.GiaBoiThuong2 <= DonGiaDen || t.GiaBoiThuong4 <= DonGiaDen || t.GiaBoiThuong6 <= DonGiaDen);
                    }
                    if (Mahs != "all") { model = model.Where(t => t.Mahs == Mahs); }
                    if (!string.IsNullOrEmpty(MoTa))
                    {
                        model = model.Where(t => t.MoTa.ToLower().Contains(MoTa.ToLower()));
                    }
                    ViewData["Madv"] = Madv;
                    ViewData["Manhom"] = Manhom;
                    ViewData["NgayTu"] = NgayTu;
                    ViewData["NgayDen"] = NgayDen;
                    ViewData["Mahs"] = Mahs;
                    ViewData["DonGiaTu"] = Helpers.ConvertDbToStr(DonGiaTu);
                    ViewData["DonGiaDen"] = Helpers.ConvertDbToStr(DonGiaDen);
                    ViewData["MoTa"] = MoTa;
                    ViewData["DanhSachHoSo"] = _db.GiaRung.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen && list_trangthai.Contains(t.Trangthai));
                    ViewData["DanhMucNhom"] = _db.GiaRungDm;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Title"] = "Tìm kiếm thông tin định giá rừng";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgr";
                    ViewData["MenuLv3"] = "menu_dgr_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaRung/TimKiem/Search.cshtml", model);
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

        [Route("GiaRung/PrintSearch")]
        [HttpPost]
        public IActionResult Print(string Madv_Search, string Manhom_Search, DateTime? NgayTu_Search, DateTime? NgayDen_Search,
                                    string Mahs_Search, double DonGiaTu_Search, double DonGiaDen_Search, string MoTa_Search)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.rung.timkiem", "Index"))
                {

                    var model = (from hosoct in _db.GiaRungCt
                                 join hoso in _db.GiaRung on hosoct.Mahs equals hoso.Mahs
                                 join nhom in _db.GiaRungDm on hosoct.Manhom equals nhom.Manhom
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaRungCt
                                 {
                                     Madv = hoso.Madv,
                                     Tendv = donvi.TenDv,
                                     Tennhom = nhom.Tennhom,
                                     SoQD = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                     MoTa = hosoct.MoTa,
                                     GiaRung1 = hosoct.GiaRung1,
                                     GiaRung2 = hosoct.GiaRung2,
                                     GiaRung3 = hosoct.GiaRung3,
                                     GiaRung4 = hosoct.GiaRung4,
                                     GiaRung5 = hosoct.GiaRung5,
                                     GiaRung6 = hosoct.GiaRung6,
                                     GiaChoThue1 = hosoct.GiaChoThue1,
                                     GiaChoThue2 = hosoct.GiaChoThue2,
                                     GiaBoiThuong1 = hosoct.GiaBoiThuong1,
                                     GiaBoiThuong2 = hosoct.GiaBoiThuong2,
                                     GiaBoiThuong3 = hosoct.GiaBoiThuong3,
                                     GiaBoiThuong4 = hosoct.GiaBoiThuong4,
                                     GiaBoiThuong5 = hosoct.GiaBoiThuong5,
                                     GiaBoiThuong6 = hosoct.GiaBoiThuong6,
                                     Manhom = hosoct.Manhom,
                                     Trangthai = hoso.Trangthai,
                                     Mahs = hoso.Mahs
                                 });
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model = model.Where(t => t.Thoidiem >= NgayTu_Search && t.Thoidiem <= NgayDen_Search && list_trangthai.Contains(t.Trangthai));
                    if (Madv_Search != "all") { model = model.Where(t => t.Madv == Madv_Search); }
                    if (Manhom_Search != "all") { model = model.Where(t => t.Manhom == Manhom_Search); }
                    if (Mahs_Search != "all") { model = model.Where(t => t.Mahs == Mahs_Search); }
                    if (DonGiaTu_Search > 0)
                    {
                        model = model.Where(t => t.GiaRung1 >= DonGiaTu_Search || t.GiaRung3 >= DonGiaTu_Search || t.GiaRung5 >= DonGiaTu_Search 
                                            || t.GiaChoThue1 >= DonGiaTu_Search || t.GiaBoiThuong1 >= DonGiaTu_Search || t.GiaBoiThuong3 >= DonGiaTu_Search 
                                            || t.GiaBoiThuong5 >= DonGiaTu_Search);
                    }
                    if (DonGiaDen_Search > 0)
                    {
                        model = model.Where(t => t.GiaRung2 <= DonGiaDen_Search || t.GiaRung4 <= DonGiaDen_Search || t.GiaRung6 <= DonGiaDen_Search 
                                            || t.GiaChoThue2 <= DonGiaDen_Search || t.GiaBoiThuong2 <= DonGiaDen_Search || t.GiaBoiThuong4 <= DonGiaDen_Search 
                                            || t.GiaBoiThuong6 <= DonGiaDen_Search);
                    }

                    if (!string.IsNullOrEmpty(MoTa_Search))
                    {
                        model = model.Where(t => t.MoTa.ToLower().Contains(MoTa_Search.ToLower()));
                    }

                    ViewData["Title"] = " Tìm kiếm thông tin định giá rừng";
                    return View("Views/Admin/Manages/DinhGia/GiaRung/TimKiem/Result.cshtml", model);
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

        [HttpPost("GiaRung/GetListHoSo")]
        public JsonResult GetListHoSo(DateTime ngaytu, DateTime ngayden)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.GiaRung.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && t.Trangthai == "HT");
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
