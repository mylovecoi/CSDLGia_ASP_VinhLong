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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaKhungGiaDat
{
    public class GiaKhungGiaDatController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GiaKhungGiaDatController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("GiaKhungGiaDat/DanhSach")]
        [HttpGet]
        public IActionResult Index(string Nam, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.khunggd.thongtin", "Index"))
                {
                    var dsdonvi = (from db in _db.DsDiaBan.Where(t => t.Level != "H")
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
                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") != null)
                        {
                            Madv = Helpers.GetSsAdmin(HttpContext.Session, "Madv");
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(Madv))
                            {
                                Madv = dsdonvi.OrderBy(t => t.Id).Select(t => t.MaDv).First();
                            }
                        }

                        var model = _db.GiaKhungGiaDat.Where(t => t.Madv == Madv).ToList();

                        if (string.IsNullOrEmpty(Nam))
                        {
                            model = model.ToList();
                        }
                        else
                        {
                            if (Nam != "all")
                            {
                                model = model.Where(t => t.Thoidiem.Year == int.Parse(Nam)).ToList();
                            }
                            else
                            {
                                model = model.ToList();
                            }
                        }

                        var model_join = (from kgd in model
                                          join dv in dsdonvi on kgd.Macqcq equals dv.MaDv
                                          select new CSDLGia_ASP.Models.Manages.DinhGia.GiaKhungGiaDat
                                          {
                                              Id = kgd.Id,
                                              Mahs = kgd.Mahs,
                                              Madv = kgd.Madv,
                                              Kyhieuvb = kgd.Kyhieuvb,
                                              Tieude = kgd.Tieude,
                                              Trangthai = kgd.Trangthai,
                                              Madiaban = kgd.Madiaban,
                                              Thoidiem = kgd.Thoidiem,
                                              Macqcq = kgd.Macqcq,
                                              Tencqcq = dv.TenDv,
                                          });

                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                        {
                            ViewData["DsDonVi"] = dsdonvi;
                        }
                        else
                        {
                            ViewData["DsDonVi"] = dsdonvi.Where(t => t.MaDv == Madv);
                        }
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                        ViewData["Nam"] = Nam;
                        ViewData["Madv"] = Madv;
                        ViewData["Title"] = "Thông tin giá khung giá đất";
                        ViewData["MenuLv1"] = "menu_giadat";
                        ViewData["MenuLv2"] = "menu_dgkhunggd";
                        ViewData["MenuLv3"] = "menu_dgkhunggd_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaKhungGiaDat/DanhSach/Index.cshtml", model_join);
                    }
                    else
                    {
                        ViewData["Title"] = "Thông tin giá khung giá đất";
                        ViewData["Messages"] = "Hệ thống chưa có định giá khung giá đất.";
                        ViewData["MenuLv1"] = "menu_giadat";
                        ViewData["MenuLv2"] = "menu_dgkhunggd";
                        ViewData["MenuLv3"] = "menu_dgkhunggd_tt";
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

        [Route("GiaKhungGiaDat/Create")]
        [HttpGet]
        public IActionResult Create(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.khunggd.thongtin", "Create"))
                {
                    // xóa giá khung giá đất chi tiết chưa xác định
                    var check = _db.GiaKhungGiaDatCt.Where(t => t.Madv == Madv && t.Trangthai == "CXD");
                    if (check != null)
                    {
                        _db.GiaKhungGiaDatCt.RemoveRange(check);
                    }
                    // xóa thông tin giấy tờ chưa xác định
                    var giayto_remove = _db.ThongTinGiayTo.Where(x => x.Madv == Madv && x.Status == "CXD");
                    if (giayto_remove.Any())
                    {
                        _db.ThongTinGiayTo.RemoveRange(giayto_remove);
                    }
                    _db.SaveChanges();
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
        public async Task<IActionResult> Store(CSDLGia_ASP.Models.Manages.DinhGia.GiaKhungGiaDat request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.khunggd.thongtin", "Create"))
                {
                    //if (Ipf1upload != null && Ipf1upload.Length > 0)
                    //{
                    //    string wwwRootPath = _hostEnvironment.WebRootPath;
                    //    string filename = Path.GetFileNameWithoutExtension(Ipf1upload.FileName);
                    //    string extension = Path.GetExtension(Ipf1upload.FileName);
                    //    filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    //    string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                    //    using (var FileStream = new FileStream(path, FileMode.Create))
                    //    {
                    //        await Ipf1upload.CopyToAsync(FileStream);
                    //    }
                    //    request.Ipf1 = filename;
                    //}
                    //// 2024.03.15 Gộp Update và phần nhận cho Excel
                    //if (_db.GiaKhungGiaDat.Where(x => x.Mahs == request.Mahs).Any())
                    //{
                    //    //Xử lý hồ sơ
                    //    var modelExcel = _db.GiaKhungGiaDat.FirstOrDefault(t => t.Mahs == request.Mahs);
                    //    modelExcel.Madiaban = request.Madiaban;
                    //    modelExcel.Tieude = request.Tieude;
                    //    modelExcel.Dvbanhanh = request.Dvbanhanh;
                    //    modelExcel.Kyhieuvb = request.Kyhieuvb;
                    //    modelExcel.Ngayapdung = request.Ngayapdung;
                    //    modelExcel.Ghichu = request.Ghichu;
                    //    modelExcel.CodeExcel = request.CodeExcel;
                    //    modelExcel.Updated_at = DateTime.Now;
                    //    modelExcel.Ipf1 = request.Ipf1;
                    //    _db.GiaKhungGiaDat.Update(modelExcel);

                    //    // Xử lý phần lịch sử hồ sơ 
                    //    var lichSu = new TrangThaiHoSo
                    //    {
                    //        MaHoSo = request.Mahs,
                    //        TenDangNhap = Helpers.GetSsAdmin(HttpContext.Session, "Name"),
                    //        ThongTin = "Thay đổi thông tin hồ sơ",
                    //        ThoiGian = DateTime.Now,
                    //        TrangThai = "CHT",
                    //    };
                    //    _db.TrangThaiHoSo.Add(lichSu);
                    //    _db.SaveChanges();

                    //    return RedirectToAction("Index", "GiaKhungGiaDat", new { request.Madv });
                    //}
                    //Phần cũ
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
                        Trangthai = "CHT",
                        Congbo = "CHUACONGBO",
                        Ipf1 = request.Ipf1,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaKhungGiaDat.Add(model);
                    // Lưu lại hồ sơ chi tiết chưa xác định
                    var modelct = _db.GiaKhungGiaDatCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Trangthai = "XD";
                        }
                    }
                    // Lưu lại giấy tờ chưa xác định
                    var hosogiayto = _db.ThongTinGiayTo.Where(t => t.Mahs == request.Mahs && t.Status == "CXD");
                    if (hosogiayto.Any())
                    {
                        foreach (var item in hosogiayto)
                        {
                            item.Status = "XD";
                        }
                    }
                    await _db.SaveChangesAsync();
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
        public async Task<IActionResult> Update(CSDLGia_ASP.Models.Manages.DinhGia.GiaKhungGiaDat request, IFormFile Ipf1upload)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.khunggd.thongtin", "Edit"))
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
                    _db.SaveChanges();

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
                    _db.GiaKhungGiaDat.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.GiaKhungGiaDatCt.Where(t => t.Mahs == model.Mahs);
                    _db.GiaKhungGiaDatCt.RemoveRange(model_ct);
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
                    if (model.CodeExcel != "")
                    {
                        ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                        ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                        ViewData["Title"] = "Bảng giá khung giá đất";
                        return View("Views/Admin/Manages/DinhGia/GiaKhungGiaDat/DanhSach/ShowExcel.cshtml", model);
                    }
                    model.GiaKhungGiaDatCt = _db.GiaKhungGiaDatCt.Where(t => t.Mahs == model.Mahs).ToList();

                    ViewData["Title"] = "Bảng giá khung giá đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgkhunggd";
                    ViewData["MenuLv3"] = "menu_dgkhunggd_tt";
                    ViewData["Dmloaidat"] = _db.DmLoaiDat.ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
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
        public IActionResult Complete(string mahs_chuyen, string macqcq_chuyen)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.khunggd.thongtin", "Index"))
                {
                    var model = _db.GiaKhungGiaDat.FirstOrDefault(t => t.Mahs == mahs_chuyen);

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
        public IActionResult Search(string madv, string Vungkt, DateTime ngaynhap_tu, DateTime ngaynhap_den, string SoQuyetDinh = "all")
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
                    //var model = from giakgd in _db.GiaKhungGiaDat 
                    //             join donvi in _db.DsDonVi on giakgd.Madv equals donvi.MaDv
                    //             select new CSDLGia_ASP.Models.Manages.DinhGia.GiaKhungGiaDat
                    //             {
                    //                 Id = giakgd.Id,                                     
                    //                 Mahs = giakgd.Mahs,
                    //                 Madv = giakgd.Madv,
                    //                 Thoidiem = giakgd.Thoidiem,
                    //                 TenDonVi = donvi.TenDv,
                    //                 Kyhieuvb = giakgd.Kyhieuvb,
                    //             };
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
                                     SoQD= giakgd.Kyhieuvb,                                     

                                 });
                    model = model.Where(x => x.Thoidiem >= ngaynhap_tu && x.Thoidiem <= ngaynhap_den);


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
                        model = model.Where(x=>x.Vungkt.Contains(Vungkt));
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
                    return View("Views/Admin/Manages/DinhGia/GiaKhungGiaDat/TimKiem/Index.cshtml",model);

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

        //[Route("GiaKhungGiaDat/Search/Result")]
        //[HttpPost]
        //public IActionResult Result(string madv, string vungkt, DateTime ngaynhap_tu, DateTime ngaynhap_den, double gia_tu, double gia_den)
        //{
        //    if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
        //    {
        //        if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.khunggd.timkiem", "Index"))
        //        {
        //            var model = (from giakgdct in _db.GiaKhungGiaDatCt
        //                         join giakgd in _db.GiaKhungGiaDat on giakgdct.Mahs equals giakgd.Mahs
        //                         join donvi in _db.DsDonVi on giakgd.Madv equals donvi.MaDv
        //                         select new GiaKhungGiaDatCt
        //                         {
        //                             Id = giakgdct.Id,
        //                             Vungkt = giakgdct.Vungkt,
        //                             Mahs = giakgdct.Mahs,
        //                             Madv = giakgd.Madv,
        //                             Thoidiem = giakgd.Thoidiem,
        //                             Tendv = donvi.TenDv,
        //                         });

        //            if (madv != "all")
        //            {
        //                model = model.Where(t => t.Madv == madv);
        //            }

        //            if (!string.IsNullOrEmpty(vungkt))
        //            {
        //                model = model.Where(t => t.Vungkt.Contains(vungkt));
        //            }
        //            else
        //            {
        //                model = model.OrderBy(t => t.Vungkt);
        //            }

        //            if (ngaynhap_tu.ToString("yyMMdd") != "010101")
        //            {
        //                model = model.Where(t => t.Thoidiem >= ngaynhap_tu);
        //            }

        //            if (ngaynhap_den.ToString("yyMMdd") != "010101")
        //            {
        //                model = model.Where(t => t.Thoidiem <= ngaynhap_den);
        //            }

        //            ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá khung giá đất";
        //            ViewData["MenuLv1"] = "menu_giadat";
        //            ViewData["MenuLv2"] = "menu_dgkhunggd";
        //            ViewData["MenuLv3"] = "menu_dgkhunggd_tk";
        //            return View("Views/Admin/Manages/DinhGia/GiaKhungGiaDat/TimKiem/Result.cshtml", model);

        //        }
        //        else
        //        {
        //            ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
        //            return View("Views/Admin/Error/Page.cshtml");
        //        }
        //    }
        //    else
        //    {
        //        return View("Views/Admin/Error/SessionOut.cshtml");
        //    }
        //}
    }
}
