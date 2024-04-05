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
using Microsoft.Extensions.Hosting;
using CSDLGia_ASP.Models.Systems;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaThueDN
{
    public class GiaThueDNController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GiaThueDNController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("GiaThueMatDatMatNuoc")]
        [HttpGet]
        public IActionResult Index(string Madv, int Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.thongtin", "Index"))
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

                        IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMatDatMatNuoc> model = _db.GiaThueMatDatMatNuoc;

                        if (Madv != "all")
                        {
                            model = model.Where(t => t.Madv == Madv);
                        }


                        if (Nam != 0)
                        {
                            model = model.Where(t => t.Thoidiem.Year == Nam).ToList();
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
                        ViewData["Nam"] = Nam;
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                        ViewData["DsDiaBanAll"] = _db.DsDiaBan;
                        ViewData["DsCqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                        ViewData["Madv"] = Madv;
                        ViewData["NhomTn"] = _db.GiaThueMatDatMatNuocNhom.ToList();
                        ViewData["Title"] = " Thông tin hồ sơ giá thuê mặt đất mặt nước";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgtmdmn";
                        ViewData["MenuLv3"] = "menu_dgtmdmn_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Title"] = "Thông tin hồ sơ giá thuê mặt đất mặt nước";
                        ViewData["Messages"] = "Thông tin hồ sơ giá thuê mặt đất mặt nước.";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgtmdmn";
                        ViewData["MenuLv3"] = "menu_dgtmdmn_tt";
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

        [Route("GiaThueMatDatMatNuoc/Create")]
        [HttpGet]
        public IActionResult Create(string Madv, string maNhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.thongtin", "Create"))
                {
                    var model_ct_cxd = _db.GiaThueMatDatMatNuocCt.Where(t => t.Trangthai == "CXD" && t.Madv == Madv);
                    if (model_ct_cxd.Any())
                    {
                        _db.GiaThueMatDatMatNuocCt.RemoveRange(model_ct_cxd);
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


                    var model = new GiaThueMatDatMatNuoc
                    {
                        Madv = Madv,
                        Thoidiem = DateTime.Now,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        PhanLoaiHoSo = "HOSOCHITIET",
                    };
                    IEnumerable<GiaThueMatDatMatNuocDm> danhmuc = _db.GiaThueMatDatMatNuocDm;
                    if (maNhom != "all")
                    {
                        danhmuc = danhmuc.Where(t => t.Manhom == maNhom);
                    }

                    var chitiet = new List<GiaThueMatDatMatNuocCt>();


                    foreach (var item in danhmuc)
                    {
                        chitiet.Add(new GiaThueMatDatMatNuocCt()
                        {
                            Mahs = model.Mahs,
                            LoaiDat = item.Loaidat,
                            SapXep = item.SapXep,
                            HienThi = item.HienThi,
                            MaNhom = item.Manhom,
                            Style = item.Style,
                            NhapGia = item.NhapGia,
                            Trangthai = "CXD",
                            Madv = Madv,
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        });
                    }

                  
                  

                    _db.GiaThueMatDatMatNuocCt.AddRange(chitiet);
                    _db.SaveChanges();

                    model.GiaThueMatDatMatNuocCt = chitiet.Where(t => t.Mahs == model.Mahs).ToList();

                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Title"] = "Thêm mới giá thuê mặt đất mặt nước";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmdmn";
                    ViewData["MenuLv3"] = "menu_dgtmdmn_tt";
                    ViewData["GiaThueDNNhom"] = _db.GiaThueMatDatMatNuocNhom;
                    return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/Create.cshtml", model);
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

        [Route("GiaThueMatDatMatNuoc/NhanExcel")]
        [HttpGet]
        public IActionResult NhanExcel(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.thongtin", "Create"))
                {

                    var model = new VMDinhGiaThueDN
                    {
                        Madv = Madv,
                        Thoidiem = DateTime.Now,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        PhanLoaiHoSo = "HOSONHANEXCEL",
                    };
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Title"] = "Thêm mới giá thuê mặt đất mặt nước";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmdmn";
                    ViewData["MenuLv3"] = "menu_dgtmdmn_tt";

                    ViewData["codeExcel"] = model.CodeExcel;//Gán ra view để dùng chung
                    return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/NhanExcel.cshtml", model);
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

        [Route("GiaThueMatDatMatNuoc/Store")]
        [HttpPost]
        public IActionResult Store(GiaThueMatDatMatNuoc request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.thongtin", "Create"))
                {
                    var model = new GiaThueMatDatMatNuoc
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Madiaban = request.Madiaban,
                        Soqd = request.Soqd,
                        Thoidiem = request.Thoidiem,
                        Thongtin = request.Thongtin,
                        Ghichu = request.Ghichu,
                        PhanLoaiHoSo = request.PhanLoaiHoSo,
                        CodeExcel = request.CodeExcel,
                        Trangthai = "CHT",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };

                    // Xử lý phần lịch sử hồ sơ 

                    //var lichSuHoSo = new TrangThaiHoSo
                    //{
                    //    MaHoSo = request.Mahs,
                    //    TenDangNhap = Helpers.GetSsAdmin(HttpContext.Session, "Name"),
                    //    ThongTin = "Thay đổi thông tin hồ sơ",
                    //    ThoiGian = DateTime.Now,
                    //    TrangThai = "CHT",

                    //};
                    //_db.TrangThaiHoSo.Add(lichSuHoSo);
                    //_db.SaveChanges();

                    //Kết thúc Xử lý phần lịch sử hồ sơ 

                    var modelct = _db.GiaThueMatDatMatNuocCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct.Any())
                    {
                        foreach (var ct in modelct)
                        {
                            ct.Trangthai = "XD";
                        }
                        _db.GiaThueMatDatMatNuocCt.UpdateRange(modelct);
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
                    _db.GiaThueMatDatMatNuoc.Add(model);
                    _db.SaveChanges();
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmdmn";
                    ViewData["MenuLv3"] = "menu_dgtmdmn_tt";
                    return RedirectToAction("Index", "GiaThueDN", new { request.Madv });
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

        [Route("GiaThueMatDatMatNuoc/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.thongtin", "Delete"))
                {
                    var model = _db.GiaThueMatDatMatNuoc.FirstOrDefault(t => t.Id == id_delete);
                    if (model != null)
                    {
                        var model_ct = _db.GiaThueMatDatMatNuocCt.Where(t => t.Mahs == model.Mahs);
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
                        if (model_ct.Any()) { _db.GiaThueMatDatMatNuocCt.RemoveRange(model_ct); }

                        _db.GiaThueMatDatMatNuoc.Remove(model);
                        _db.SaveChanges();
                    }
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmdmn";
                    ViewData["MenuLv3"] = "menu_dgtmdmn_tt";
                    return RedirectToAction("Index", "GiaThueDN", new { model.Madv });
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

        [Route("GiaThueMatDatMatNuoc/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.thongtin", "Edit"))
                {
                    var model = _db.GiaThueMatDatMatNuoc.FirstOrDefault(t => t.Mahs == Mahs);
                    model.GiaThueMatDatMatNuocCt = _db.GiaThueMatDatMatNuocCt.Where(t => t.Mahs == Mahs).ToList();
                    model.ThongTinGiayTo = _db.ThongTinGiayTo.Where(t => t.Mahs == Mahs).ToList();
                    ViewData["GiaThueDNNhom"] = _db.GiaThueMatDatMatNuocNhom;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Title"] = "Chỉnh sửa giá thuê mặt dất mặt nước";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmdmn";
                    ViewData["MenuLv3"] = "menu_dgtmdmn_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/Edit.cshtml", model);
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

        [Route("GiaThueMatDatMatNuoc/Update")]
        [HttpPost]
        public IActionResult Update(GiaThueMatDatMatNuoc request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.thongtin", "Edit"))
                {
                    var model = _db.GiaThueMatDatMatNuoc.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Madiaban = request.Madiaban;
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Thongtin = request.Thongtin;
                    model.Ghichu = request.Ghichu;
                    model.Updated_at = DateTime.Now;


                    var modelct = _db.GiaThueMatDatMatNuocCt.Where(t => t.Mahs == request.Mahs);
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
                    _db.GiaThueMatDatMatNuoc.Update(model);
                    _db.GiaThueMatDatMatNuocCt.UpdateRange(modelct);
                    _db.SaveChanges();


                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmdmn";
                    ViewData["MenuLv3"] = "menu_dgtmdmn_tt";
                    return RedirectToAction("Index", "GiaThueDN", new { request.Mahs });
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

        [Route("GiaThueMatDatMatNuoc/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.thongtin", "Edit"))
                {
                    var model = _db.GiaThueMatDatMatNuoc.FirstOrDefault(t => t.Mahs == Mahs);
                    var modelct = _db.GiaThueMatDatMatNuocCt.Where(t => t.Mahs == Mahs);
                    model.GiaThueMatDatMatNuocCt = modelct.ToList();
                    ViewData["DsNhom"] = _db.GiaThueMatDatMatNuocNhom;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["Title"] = "Chi tiết giá thuê mặt dất mặt nước";
                    return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/Show.cshtml", model);
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

        [Route("GiaThueMatDatMatNuoc/Search")]
        [HttpGet]
        public IActionResult Search(string Madv, string Manhom, DateTime? NgayTu, DateTime? NgayDen, string Mahs, double DonGiaTu, double DonGiaDen, string LoaiDat)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.timkiem", "Index"))
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
                    LoaiDat = string.IsNullOrEmpty(LoaiDat) ? "" : LoaiDat;

                    var model = (from hosoct in _db.GiaThueMatDatMatNuocCt
                                 join hoso in _db.GiaThueMatDatMatNuoc on hosoct.Mahs equals hoso.Mahs
                                 join nhom in _db.GiaThueMatDatMatNuocNhom on hosoct.MaNhom equals nhom.Manhom
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMatDatMatNuocCt
                                 {
                                     Madv = hoso.Madv,
                                     Tendv = donvi.TenDv,
                                     Tennhom = nhom.Tennhom,
                                     SoQD = hoso.Soqd,
                                     ThoiDiem = hoso.Thoidiem,
                                     LoaiDat = hosoct.LoaiDat,
                                     TyLe1 = hosoct.TyLe1,
                                     TyLe2 = hosoct.TyLe2,
                                     TyLe3 = hosoct.TyLe3,
                                     Dongia1 = hosoct.Dongia1,
                                     MaNhom = hosoct.MaNhom,
                                     Trangthai = hoso.Trangthai,
                                     Mahs = hoso.Mahs
                                 });

                    model = model.Where(t => t.ThoiDiem >= NgayTu && t.ThoiDiem <= NgayDen && t.Trangthai == "HT" && t.Dongia1 >= DonGiaTu);
                    if (Madv != "all") { model = model.Where(t => t.Madv == Madv); }
                    if (Manhom != "all") { model = model.Where(t => t.MaNhom == Manhom); }
                    if (DonGiaDen > 0) { model = model.Where(t => t.Dongia1 <= DonGiaDen); }
                    if (!string.IsNullOrEmpty(LoaiDat))
                    {
                        model = model.Where(t => t.LoaiDat.ToLower().Contains(LoaiDat.ToLower()));
                    }

                    ViewData["Madv"] = Madv;
                    ViewData["Manhom"] = Manhom;
                    ViewData["NgayTu"] = NgayTu;
                    ViewData["NgayDen"] = NgayDen;
                    ViewData["Mahs"] = Mahs;
                    ViewData["DonGiaTu"] = DonGiaTu;
                    ViewData["DonGiaDen"] = DonGiaDen;
                    ViewData["LoaiDat"] = LoaiDat;
                    ViewData["DanhSachHoSo"] = _db.GiaThueMatDatMatNuoc.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen && t.Trangthai == "HT");
                    ViewData["DanhMucNhom"] = _db.GiaThueMatDatMatNuocNhom;

                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Title"] = "Tìm kiếm thông tin định giá thuê mặt đất mặt nước";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmdmn";
                    ViewData["MenuLv3"] = "menu_dgtmdmn_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/TimKiem/Search.cshtml", model);

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

        [HttpPost("GiaThueMatDatMatNuoc/Print")]
        public IActionResult Print(string Madv_Search, string Manhom_Search, DateTime? NgayTu_Search, DateTime? NgayDen_Search, string Mahs_Search, double DonGiaTu_Search, double DonGiaDen_Search, string LoaiDat_Search)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.timkiem", "Edit"))
                {
                    var model = (from hosoct in _db.GiaThueMatDatMatNuocCt
                                 join hoso in _db.GiaThueMatDatMatNuoc on hosoct.Mahs equals hoso.Mahs
                                 join nhom in _db.GiaThueMatDatMatNuocNhom on hosoct.MaNhom equals nhom.Manhom
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMatDatMatNuocCt
                                 {
                                     Madv = hoso.Madv,
                                     Tendv = donvi.TenDv,
                                     Tennhom = nhom.Tennhom,
                                     SoQD = hoso.Soqd,
                                     ThoiDiem = hoso.Thoidiem,
                                     LoaiDat = hosoct.LoaiDat,
                                     TyLe1 = hosoct.TyLe1,
                                     TyLe2 = hosoct.TyLe2,
                                     TyLe3 = hosoct.TyLe3,
                                     Dongia1 = hosoct.Dongia1,
                                     MaNhom = hosoct.MaNhom,
                                     Trangthai = hoso.Trangthai,
                                     Mahs = hoso.Mahs
                                 });
                    model = model.Where(t => t.ThoiDiem >= NgayTu_Search && t.ThoiDiem <= NgayDen_Search && t.Trangthai == "HT" && t.Dongia1 >= DonGiaTu_Search);
                    if (Madv_Search != "all") { model = model.Where(t => t.Madv == Madv_Search); }
                    if (Manhom_Search != "all") { model = model.Where(t => t.MaNhom == Manhom_Search); }
                    if (DonGiaDen_Search > 0) { model = model.Where(t => t.Dongia1 <= DonGiaDen_Search); }
                    if (!string.IsNullOrEmpty(LoaiDat_Search))
                    {
                        model = model.Where(t => t.LoaiDat.ToLower().Contains(LoaiDat_Search.ToLower()));
                    }

                    ViewData["Title"] = " Tìm kiếm thông tin định giá thuê mặt đất mặt nước";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmdmn";
                    ViewData["MenuLv3"] = "menu_dgtmdmn_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/TimKiem/Result.cshtml", model);
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

        [HttpPost("GiaThueMatDatMatNuoc/GetHoSoSearch")]
        public JsonResult GetListHoSo(DateTime ngaytu, DateTime ngayden)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.GiaThueMatDatMatNuoc.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && t.Trangthai == "HT");
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
