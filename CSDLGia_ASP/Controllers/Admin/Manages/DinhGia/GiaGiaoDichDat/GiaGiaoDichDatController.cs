using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaGiaoDichDat
{
    public class GiaGiaoDichDatController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GiaGiaoDichDatController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("GiaGiaoDichDat/DanhSach")]
        [HttpGet]
        public IActionResult Index(string Nam, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.thongtin", "Index"))
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

                        var model = _db.GiaGiaoDichDat.Where(t => t.Madv == Madv).ToList();

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

                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                        {
                            ViewData["DsDonVi"] = dsdonvi;
                        }
                        else
                        {
                            ViewData["DsDonVi"] = dsdonvi.Where(t => t.MaDv == Madv);
                        }
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                        ViewData["NhomTn"] = _db.GiaGiaoDichDatNhom.ToList();
                        ViewData["Nam"] = Nam;
                        ViewData["Madv"] = Madv;
                        ViewData["Title"] = "Thông tin giá giao dịch đất thực tế trên thị trường";
                        ViewData["MenuLv1"] = "menu_giadat";
                        ViewData["MenuLv2"] = "menu_dg_giaodichdattrenthitruong";
                        ViewData["MenuLv3"] = "menu_dg_giaodichdattrenthitruong_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaGiaoDichDat/DanhSach/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Title"] = "Thông tin giá giao dịch đất thực tế trên thị trường";
                        ViewData["Messages"] = "Hệ thống chưa có định giá giao dịch đất thực tế trên thị trường.";
                        ViewData["MenuLv1"] = "menu_giadat";
                        ViewData["MenuLv2"] = "menu_dg_giaodichdattrenthitruong";
                        ViewData["MenuLv3"] = "menu_dg_giaodichdattrenthitruong_tt";
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

        [Route("GiaGiaoDichDat/Create")]
        [HttpGet]
        public IActionResult Create(string Manhom, string MadvBc)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.thongtin", "Create"))
                {
                    var check = _db.GiaGiaoDichDatCt.Where(t => t.Madv == MadvBc && t.Trangthai == "CXD");
                    if (check.Any())
                    {
                        _db.GiaGiaoDichDatCt.RemoveRange(check);
                    }
                    // xóa thông tin giấy tờ chưa lưu lại
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
                    }
                    _db.SaveChanges();
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaGiaoDichDat
                    {
                        Mahs = MadvBc + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Madv = MadvBc,
                        Manhom = Manhom,
                    };

                    var danhmuc = _db.GiaGiaoDichDatDm.ToList();
                    if (Manhom != "all")
                    {
                        danhmuc = danhmuc.Where(t => t.Manhom == Manhom).ToList();
                    }                   

                    var chitiet = new List<GiaGiaoDichDatCt>();

                    foreach (var item in danhmuc)
                    {
                        chitiet.Add(new GiaGiaoDichDatCt()
                        {
                            Mahs = model.Mahs,
                            Ten = item.Ten,
                            Trangthai = "CXD",
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                            Madv = model.Madv,
                            Manhom= item.Manhom,
                        });
                    }
                    _db.GiaGiaoDichDatCt.AddRange(chitiet);
                    _db.SaveChanges();

                    model.GiaGiaoDichDatCt = _db.GiaGiaoDichDatCt.Where(t => t.Mahs == model.Mahs).ToList();
                    ViewData["DanhMucNhom"]= _db.GiaGiaoDichDatNhom;
                    ViewData["Manhom"] = Manhom;
                    ViewData["Madv"] = MadvBc;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["Title"] = "Bảng giá giao dịch đất thực tế trên thị trường";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dg_giaodichdattrenthitruong";
                    ViewData["MenuLv3"] = "menu_dg_giaodichdattrenthitruong_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichDat/DanhSach/Create.cshtml", model);

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

        [Route("GiaGiaoDichDat/Store")]
        [HttpPost]
        public async Task<IActionResult> Store(CSDLGia_ASP.Models.Manages.DinhGia.GiaGiaoDichDat request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.thongtin", "Create"))
                {            
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaGiaoDichDat
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
                    _db.GiaGiaoDichDat.Add(model);
                    // Lưu lại hồ sơ chi tiết chưa xác định
                    var modelct = _db.GiaGiaoDichDatCt.Where(t => t.Mahs == request.Mahs && t.Trangthai == "CXD");
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Trangthai = "XD";
                        }
                    }
                    var giayto = _db.ThongTinGiayTo.Where(t => t.Mahs == request.Mahs && t.Status == "CXD");
                    if (giayto.Any())
                    {
                        foreach (var item in giayto)
                        {
                            item.Status = "XD";
                        }
                    }
                    await _db.SaveChangesAsync();

                    return RedirectToAction("Index", "GiaGiaoDichDat", new { request.Madv });
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

        [Route("GiaGiaoDichDat/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.thongtin", "Edit"))
                {

                    var model = _db.GiaGiaoDichDat.FirstOrDefault(t => t.Mahs == Mahs);
                    // xóa hồ sơ chi tiết chưa xác định
                    var check = _db.GiaGiaoDichDatCt.Where(t => t.Madv == model.Madv && t.Trangthai == "CXD");
                    if (check.Any())
                    {
                        _db.GiaGiaoDichDatCt.RemoveRange(check);
                    }

                    // xóa thông tin giấy tờ chưa lưu lại
                    var model_file_cxd = _db.ThongTinGiayTo.Where(t => t.Status == "CXD" && t.Madv == model.Madv);
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
                    //

                    var model_ct = _db.GiaGiaoDichDatCt.Where(t => t.Mahs == model.Mahs);

                    model.GiaGiaoDichDatCt = model_ct.ToList();
                    model.ThongTinGiayTo = _db.ThongTinGiayTo.Where(x => x.Mahs == model.Mahs).ToList();

                    ViewData["Title"] = "Bảng giá giao dịch đất thực tế trên thị trường";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dg_giaodichdattrenthitruong";
                    ViewData["MenuLv3"] = "menu_dg_giaodichdattrenthitruong_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichDat/DanhSach/Edit.cshtml", model);

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

        [Route("GiaGiaoDichDat/Update")]
        [HttpPost]
        public async Task<IActionResult> Update(CSDLGia_ASP.Models.Manages.DinhGia.GiaGiaoDichDat request )
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.thongtin", "Edit"))
                {                    
                    var model = _db.GiaGiaoDichDat.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Soqdlk = request.Soqdlk;
                    model.Thoidiemlk = request.Thoidiemlk;
                    model.Cqbh = request.Cqbh;
                    model.Ghichu = request.Ghichu;
                    model.Ipf1 = request.Ipf1;
                    model.Updated_at = DateTime.Now;

                    _db.GiaGiaoDichDat.Update(model);
                    // Lưu lại hồ sơ chi tiết chưa xác định
                    var modelct = _db.GiaGiaoDichDatCt.Where(t => t.Mahs == request.Mahs && t.Trangthai == "CXD");
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Trangthai = "XD";
                        }
                    }
                    var giayto = _db.ThongTinGiayTo.Where(t => t.Mahs == request.Mahs && t.Status == "CXD");
                    if (giayto.Any())
                    {
                        foreach (var item in giayto)
                        {
                            item.Status = "XD";
                        }
                    }
                    await _db.SaveChangesAsync();

                    return RedirectToAction("Index", "GiaGiaoDichDat", new { request.Madv });
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

        [Route("GiaGiaoDichDat/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.thongtin", "Delete"))
                {
                    var model = _db.GiaGiaoDichDat.FirstOrDefault(t => t.Id == id_delete);

                    var model_ct = _db.GiaGiaoDichDatCt.Where(t => t.Mahs == model.Mahs);
                    if (model_ct.Any())
                    {
                        _db.GiaGiaoDichDatCt.RemoveRange(model_ct);
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
                    _db.GiaGiaoDichDat.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaGiaoDichDat", new { model.Madv });
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

        [Route("GiaGiaoDichDat/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.thongtin", "Index"))
                {
                    var model = _db.GiaGiaoDichDat.FirstOrDefault(t => t.Mahs == Mahs);
                    model.GiaGiaoDichDatCt = _db.GiaGiaoDichDatCt.Where(t => t.Mahs == model.Mahs).ToList();

                    ViewData["Title"] = "Bảng giá giao dịch đất thực tế trên thị trường";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dg_giaodichdattrenthitruong";
                    ViewData["MenuLv3"] = "menu_dg_giaodichdattrenthitruong_tt";
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichDat/DanhSach/Show.cshtml", model);

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

        [Route("GiaGiaoDichDat/Complete")]
        [HttpPost]
        public IActionResult Complete(string mahs_chuyen, string macqcq_chuyen)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.thongtin", "Index"))
                {
                    var model = _db.GiaGiaoDichDat.FirstOrDefault(t => t.Mahs == mahs_chuyen);

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
                    _db.GiaGiaoDichDat.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaGiaoDichDat", new { model.Madv });

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

        [Route("GiaGiaoDichDat/TimKiem")]
        [HttpGet]
        public IActionResult Search(DateTime ngaynhap_tu, DateTime ngaynhap_den, double gia_tu, double gia_den, string madv = "all", string manhom = "all")
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.thongtin", "Index"))
                {
                    ngaynhap_den = ngaynhap_den == DateTime.MinValue ? new DateTime(DateTime.Now.Year, 12, 31) : ngaynhap_den;
                    ngaynhap_tu = ngaynhap_tu == DateTime.MinValue ? new DateTime(DateTime.Now.Year, 01, 01) : ngaynhap_tu;

                    var model = (from giathuetnct in _db.GiaGiaoDichDatCt
                                 join giathuetn in _db.GiaGiaoDichDat on giathuetnct.Mahs equals giathuetn.Mahs
                                 join donvi in _db.DsDonVi on giathuetn.Madv equals donvi.MaDv
                                 /*join nhomtn in _db.GiaGiaoDichDatNhom on giathuetn.Manhom equals nhomtn.Manhom*/
                                 select new GiaGiaoDichDatCt
                                 {
                                     Id = giathuetnct.Id,
                                     Gia = giathuetnct.Gia,
                                     Mahs = giathuetnct.Mahs,
                                     Madv = giathuetn.Madv,
                                     Manhom = giathuetn.Manhom,
                                     Thoidiem = giathuetn.Thoidiem,
                                     Tendv = donvi.TenDv,
                                     Ten= giathuetnct.Ten,
                                     
                                     /*Tennhom = nhomtn.Tennhom,*/
                                 });
                    model = model.Where(x => x.Thoidiem >= ngaynhap_tu && x.Thoidiem <= ngaynhap_den);

                    if (madv != "all")
                    {
                        model = model.Where(t => t.Madv == madv);
                    }

                    if (manhom != "all")
                    {
                        model = model.Where(t => t.Manhom == manhom);
                    }
                    model = model.Where(t => t.Gia >= gia_tu);
                    if (gia_den > 0)
                    {
                        model = model.Where(t => t.Gia <= gia_den);
                    }
                    ViewData["ngaynhap_tu"] = ngaynhap_tu;
                    ViewData["ngaynhap_den"] = ngaynhap_den;
                    ViewData["gia_tu"] = gia_tu;
                    ViewData["gia_den"] = gia_den;
                    ViewData["madv"] = madv;
                    ViewData["manhom"] = manhom;


                    ViewData["DsDiaBan"] = _db.DsDiaBan;
                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["NhomTn"] = _db.GiaGiaoDichDatNhom.Where(t => t.Theodoi == "TD").ToList();
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá giao dịch đất thực tế trên thị trường";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dg_giaodichdattrenthitruong";
                    ViewData["MenuLv3"] = "menu_dg_giaodichdattrenthitruong_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichDat/TimKiem/Index.cshtml",model);

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

        [Route("GiaGiaoDichDat/TimKiem/KetQua")]
        [HttpPost]
        public IActionResult Result(string madv, string manhom, DateTime ngaynhap_tu, DateTime ngaynhap_den, double gia_tu, double gia_den)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.thongtin", "Index"))
                {
                    var model = (from giathuetnct in _db.GiaGiaoDichDatCt
                                 join giathuetn in _db.GiaGiaoDichDat on giathuetnct.Mahs equals giathuetn.Mahs
                                 join donvi in _db.DsDonVi on giathuetn.Madv equals donvi.MaDv
                                 /*join nhomtn in _db.GiaGiaoDichDatNhom on giathuetn.Manhom equals nhomtn.Manhom*/
                                 select new GiaGiaoDichDatCt
                                 {
                                     Id = giathuetnct.Id,
                                     Gia = giathuetnct.Gia,
                                     Mahs = giathuetnct.Mahs,
                                     Madv = giathuetn.Madv,
                                     Manhom = giathuetn.Manhom,
                                     Thoidiem = giathuetn.Thoidiem,
                                     Tendv = donvi.TenDv,
                                     /*Tennhom = nhomtn.Tennhom,*/
                                 });

                    if (madv != "all")
                    {
                        model = model.Where(t => t.Madv == madv);
                    }

                    if (manhom != "all")
                    {
                        model = model.Where(t => t.Manhom == manhom);
                    }

                    if (ngaynhap_tu.ToString("yyMMdd") != "010101")
                    {
                        model = model.Where(t => t.Thoidiem >= ngaynhap_tu);
                    }

                    if (ngaynhap_den.ToString("yyMMdd") != "010101")
                    {
                        model = model.Where(t => t.Thoidiem <= ngaynhap_den);
                    }

                    model = model.Where(t => t.Gia >= gia_tu);
                    if (gia_den > 0)
                    {
                        model = model.Where(t => t.Gia <= gia_den);
                    }

                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá giao dịch đất thực tế trên thị trường";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dg_giaodichdattrenthitruong";
                    ViewData["MenuLv3"] = "menu_dg_giaodichdattrenthitruong_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichDat/TimKiem/Result.cshtml", model);

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

        [Route("GiaGiaoDichDatCt/Edit")]
        [HttpPost]
        public JsonResult EditCt(int Id)
        {
            var model = _db.GiaGiaoDichDatCt.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Giá giao dịch đất thực tế trên thị trường (đồng)</label>";
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

        [Route("GiaGiaoDichDatCt/Update")]
        [HttpPost]
        public JsonResult UpdateCt(int Id, double Gia)
        {
            var model = _db.GiaGiaoDichDatCt.FirstOrDefault(t => t.Id == Id);
            model.Gia = Gia;
            model.Updated_at = DateTime.Now;
            _db.GiaGiaoDichDatCt.Update(model);
            _db.SaveChanges();
            string result = GetDataCt(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        public string GetDataCt(string Mahs)
        {
            var model = _db.GiaGiaoDichDatCt.Where(t => t.Mahs == Mahs).ToList();

            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover table-responsive' id='datatable_4'>";

            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th width='2%'>#</th>";
            result += "<th width='25%'>Phân loại nhà cho thuê</th>";
            result += "<th>Giá cho thuê (đồng)</th>";
            result += "<th width='9%'>Thao tác</th>";
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";

            foreach (var item in model)
            {
                result += "<tr>";
                result += "<td class='text-center'>" + record++ + "</td>";
                result += "<td class='active' style='font-weight:bold'>" + item.Ten + "</td>";
                result += "<td style='text-align:right; font-weight:bold'>" + item.Gia + "</td>";
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
    }
}
