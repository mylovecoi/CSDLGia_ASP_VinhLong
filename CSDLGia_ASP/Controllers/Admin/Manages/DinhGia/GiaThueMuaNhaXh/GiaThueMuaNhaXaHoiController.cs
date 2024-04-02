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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaThueMuaNhaXh
{
    public class GiaThueMuaNhaXaHoiController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        private readonly IWebHostEnvironment _hostEnvironment;

        public GiaThueMuaNhaXaHoiController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("GiaThueMuaNhaXh")]
        [HttpGet]
        public IActionResult Index(string Madv, int Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanha.thongtin", "Index"))
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

                        IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMuaNhaXh> model = _db.GiaThueMuaNhaXh;

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
                        ViewData["Madv"] = Madv;
                        ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                        ViewData["Title"] = " Thông tin hồ sơ giá thuê mua nhà xã hội";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgtmnxh";
                        ViewData["MenuLv3"] = "menu_dgtmnxh_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaThueMuaNhaXh/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Title"] = "Thông tin hồ sơ giá đất cụ thể";
                        ViewData["Messages"] = "Thông tin hồ sơ giá đất cụ thể.";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgtmnxh";
                        ViewData["MenuLv3"] = "menu_dgtmnxh_tt";
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

        [Route("GiaThueMuaNhaXh/Create")]
        [HttpGet]
        public IActionResult Create(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanha.thongtin", "Create"))
                {
                    var model_ct_cxd = _db.GiaThueMuaNhaXhCt.Where(t => t.Trangthai == "CXD" && t.Madv == Madv);
                    if (model_ct_cxd.Any())
                    {
                        _db.GiaThueMuaNhaXhCt.RemoveRange(model_ct_cxd);
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
                    string Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH");
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMuaNhaXh
                    {
                        Madv = Madv,
                        Thoidiem = DateTime.Now,
                        Mahs = Mahs,
                    };
                    var danhmuc = _db.GiaThueMuaNhaXhDm.Where(t=>t.Hientrang != "Đã bán");

                    if (danhmuc.Any())
                    {
                        List<CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMuaNhaXhCt> list_add = new List<GiaThueMuaNhaXhCt>();
                        foreach(var item in danhmuc)
                        {
                            list_add.Add(new GiaThueMuaNhaXhCt
                            {
                                Tennha = item.Tennha,
                                Phanloai = item.Phanloai,
                                Mahs = Mahs,
                                Trangthai = "CXD"
                            });
                        }
                        _db.GiaThueMuaNhaXhCt.AddRange(list_add);
                        _db.SaveChanges();
                    }
                    model.GiaThueMuaNhaXhCt = _db.GiaThueMuaNhaXhCt.Where(t => t.Mahs == Mahs).ToList();

          
                    ViewData["DmDvt"] = _db.DmDvt.ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Dmtmnxh"] = _db.GiaThueMuaNhaXhDm.ToList();
                    ViewData["Title"] = "Thêm mới giá thuê mua nhà xã hội";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmnxh";
                    ViewData["MenuLv3"] = "menu_dgtmnxh_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaThueMuaNhaXh/Create.cshtml", model);
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

        [Route("GiaThueMuaNhaXh/Store")]
        [HttpPost]
        public IActionResult Store(CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMuaNhaXh request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanha.thongtin", "Create"))
                {                   

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMuaNhaXh
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Madiaban = request.Madiaban,
                        Soqd = request.Soqd,
                        Thoidiem = request.Thoidiem,
                        Mota = request.Mota,
                        Ghichu = request.Ghichu,                      
                        Trangthai = "CHT",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };

                    var modelct = _db.GiaThueMuaNhaXhCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct.Any())
                    {
                        foreach (var ct in modelct)
                        {
                            ct.Trangthai = "XD";
                        }
                        _db.GiaThueMuaNhaXhCt.UpdateRange(modelct);
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
                    _db.GiaThueMuaNhaXh.Add(model);
                    _db.SaveChanges();


                    // Xử lý phần lịch sử hồ sơ 

                    var lichSuHoSo = new TrangThaiHoSo
                    {
                        MaHoSo = request.Mahs,
                        TenDangNhap = Helpers.GetSsAdmin(HttpContext.Session, "Name"),
                        ThoiGian = DateTime.Now,
                        TrangThai = "CHT",

                    };
                    _db.TrangThaiHoSo.Add(lichSuHoSo);
                    _db.SaveChanges();

                    //Kết thúc Xử lý phần lịch sử hồ sơ 



                    return RedirectToAction("Index", "GiaThueMuaNhaXaHoi", new { Nam = request.Thoidiem.Year, Madv = request.Madv });
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

        [Route("GiaThueMuaNhaXh/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanha.thongtin", "Edit"))
                {
                    var model = _db.GiaThueMuaNhaXh.FirstOrDefault(t => t.Mahs == Mahs);
                    model.GiaThueMuaNhaXhCt = _db.GiaThueMuaNhaXhCt.Where(t => t.Mahs == model.Mahs).ToList();
                    model.ThongTinGiayTo = _db.ThongTinGiayTo.Where(t => t.Mahs == model.Mahs).ToList();

                    ViewData["Madv"] = model.Madv;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DmDvt"] = _db.DmDvt.ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Dmtmnxh"] = _db.GiaThueMuaNhaXhDm.ToList();
                    ViewData["Title"] = "Chỉnh sửa giá thuê mua nhà xã hội";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmnxh";
                    ViewData["MenuLv3"] = "menu_dgtmnxh_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaThueMuaNhaXh/Edit.cshtml", model);
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

        [Route("GiaThueMuaNhaXh/Update")]
        [HttpPost]
        public IActionResult Update(CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMuaNhaXh request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanha.thongtin", "Edit"))
                {                    

                    var model = _db.GiaThueMuaNhaXh.FirstOrDefault(t => t.Mahs == request.Mahs);
                    if (model != null)
                    {
                        model.Madiaban = request.Madiaban;
                        model.Soqd = request.Soqd;
                        model.Thoidiem = request.Thoidiem;
                        model.Mota = request.Mota;
                        model.Ghichu = request.Ghichu;
                        model.Ipf1 = request.Ipf1;                    
                        model.Updated_at = DateTime.Now;

                        var modelct = _db.GiaThueMuaNhaXhCt.Where(t => t.Mahs == request.Mahs);
                        if (modelct.Any())
                        {
                            foreach (var ct in modelct)
                            {
                                ct.Trangthai = "XD";
                            }
                            _db.GiaThueMuaNhaXhCt.UpdateRange(modelct);
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
                        _db.GiaThueMuaNhaXh.Update(model);
                        _db.SaveChanges();
                    }

                    return RedirectToAction("Index", "GiaThueMuaNhaXaHoi", new { Nam = request.Thoidiem.Year, Madv = request.Madv });
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

        [Route("GiaThueMuaNhaXh/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanha.thongtin", "Delete"))
                {
                    var model = _db.GiaThueMuaNhaXh.FirstOrDefault(t => t.Id == id_delete);                  

                    var model_ct = _db.GiaThueMuaNhaXhCt.Where(t => t.Mahs == model.Mahs);
                    if (model_ct.Any()) { _db.GiaThueMuaNhaXhCt.RemoveRange(model_ct); }
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
                    _db.GiaThueMuaNhaXh.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaThueMuaNhaXaHoi", new { Madv = model.Madv, Nam = model.Thoidiem.Year });
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

        [Route("GiaThueMuaNhaXh/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanha.thongtin", "Edit"))
                {
                    var model = _db.GiaThueMuaNhaXh.FirstOrDefault(t => t.Mahs == Mahs);
                    var modelct = _db.GiaThueMuaNhaXhCt.Where(t => t.Mahs == model.Mahs);
                    model.GiaThueMuaNhaXhCt = modelct.ToList();                   

                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["GiaThueMuaNhaXhDm"] = _db.GiaThueMuaNhaXhDm.ToList();
                    ViewData["Title"] = "Chi tiết giá thuê mua nhà xã hội";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmnxh";
                    ViewData["MenuLv3"] = "menu_dgtmnxh_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaThueMuaNhaXh/Show.cshtml", model);
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

        [Route("GiaThueMuaNhaXh/Printf")]
        [HttpGet]
        public IActionResult Printf(string Nam, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgxmtxd.giakk", "Index"))
                {

                    // Lấy bản ghi có năm và mã đơn vị trùng với năm và mã đơn vị truyền vào ( Năm và Mã đơn vị tuyền từ Index sang )


                    var model = _db.GiaThueMuaNhaXh.Where(t => t.Trangthai == "HT").ToList();


                    if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") != null)
                    {
                        Madv = Helpers.GetSsAdmin(HttpContext.Session, "Madv");

                    }
                    model = model.Where(t => t.Madv == Madv).ToList();

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

                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmnxh";
                    ViewData["MenuLv3"] = "menu_dgtmnxh_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaThueMuaNhaXh/Print.cshtml", model);

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

        [Route("GiaThueMuaNhaXh/Complete")]
        [HttpPost]
        public IActionResult Complete(string mahs_complete, string macqcq_chuyen)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanha.xetduyet", "Approve"))
                {
                    var model = _db.GiaThueMuaNhaXh.FirstOrDefault(p => p.Mahs == mahs_complete);

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

                    //if (chk_dvcq != null && chk_dvcq.Level == "T")
                    //{
                    //    model.Madv_t = macqcq_chuyen;
                    //    model.Thoidiem_t = DateTime.Now;
                    //    model.Trangthai_t = "CHT";
                    //}
                    //else if (chk_dvcq != null && chk_dvcq.Level == "ADMIN")
                    //{
                    //    model.Madv_ad = macqcq_chuyen;
                    //    model.Thoidiem_ad = DateTime.Now;
                    //    model.Trangthai_ad = "CHT";
                    //}
                    //else
                    //{
                    //    model.Madv_h = macqcq_chuyen;
                    //    model.Thoidiem_h = DateTime.Now;
                    //    model.Trangthai_h = "CHT";
                    //}
                    _db.GiaThueMuaNhaXh.Update(model);
                    _db.SaveChanges();

                    //Xử lý phần lịch sử hồ sơ 
                    var lichSuHoSo = new TrangThaiHoSo
                    {
                        MaHoSo = mahs_complete,
                        TenDangNhap = Helpers.GetSsAdmin(HttpContext.Session, "Name"),
                        ThoiGian = DateTime.Now,
                        TrangThai = "CCB",

                    };
                    _db.TrangThaiHoSo.Add(lichSuHoSo);
                    _db.SaveChanges();

                    //Kết thúc Xử lý phần lịch sử hồ sơ 
                    return RedirectToAction("Index", "GiaThueMuaNhaXh", new { Madv = model.Madv, Nam = model.Thoidiem.Year });
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

        [Route("GiaThueMuaNhaXh/Search")]
        [HttpGet]
        public IActionResult Search(string Madv, string PhanLoai, DateTime? NgayTu, DateTime? NgayDen, string Mahs, double DonGiaTu, double DonGiaDen)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanha.thongtin", "Index"))
                {
                    DateTime nowDate = DateTime.Now;
                    DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
                    DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);

                    Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;
                    Mahs = string.IsNullOrEmpty(Mahs) ? "all" : Mahs;
                    NgayTu = NgayTu.HasValue ? NgayTu : firstDayCurrentYear;
                    NgayDen = NgayDen.HasValue ? NgayDen : lastDayCurrentYear;
                    DonGiaTu = DonGiaTu == 0 ? 0 : DonGiaTu;
                    DonGiaDen = DonGiaDen == 0 ? 0 : DonGiaDen;
                    PhanLoai = string.IsNullOrEmpty(PhanLoai) ? "all" : PhanLoai;


                    var model = (from hosoct in _db.GiaThueMuaNhaXhCt
                                 join hoso in _db.GiaThueMuaNhaXh on hosoct.Mahs equals hoso.Mahs                                
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMuaNhaXhCt
                                 {
                                     Madv = hoso.Madv,
                                     Tendv = donvi.TenDv,
                                     Tennha = hosoct.Tennha,
                                     Phanloai = hosoct.Phanloai,
                                     SoQD = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                     Dvt = hosoct.Dvt,  
                                     Dongia = hosoct.Dongia,
                                     Dongiathue = hosoct.Dongiathue,                                    
                                     Trangthai = hoso.Trangthai,
                                     Mahs = hoso.Mahs,
                                     Maso = hosoct.Maso,
                                     Dvthue = hosoct.Dvthue
                                 });

                    model = model.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen && t.Trangthai == "HT" && t.Dongia >= DonGiaTu);
                    if (Madv != "all") { model = model.Where(t => t.Madv == Madv); }
                    if (PhanLoai != "all") { model = model.Where(t => t.Phanloai == PhanLoai); }
                    if (DonGiaDen > 0) { model = model.Where(t => t.Dongia <= DonGiaDen); }   
                    if(Mahs != "all") { model = model.Where(t => t.Mahs == Mahs); }

                    ViewData["Madv"] = Madv;
                    ViewData["PhanLoai"] = PhanLoai;
                    ViewData["NgayTu"] = NgayTu;
                    ViewData["NgayDen"] = NgayDen;
                    ViewData["Mahs"] = Mahs;
                    ViewData["DonGiaTu"] = DonGiaTu;
                    ViewData["DonGiaDen"] = DonGiaDen;
                    ViewData["DanhSachHoSo"] = _db.GiaThueMuaNhaXh.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen && t.Trangthai == "HT");


                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Title"] = "Tìm kiếm thông tin giá thuê mua nhà xã hội";
                    
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmnxh";
                    ViewData["MenuLv3"] = "menu_dgtmnxh_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaThueMuaNhaXh/TimKiem/Search.cshtml", model);

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

        [Route("GiaThueMuaNhaXh/PrintSearch")]
        [HttpPost]
        public IActionResult Print(string Madv_Search, string PhanLoai_Search, DateTime? NgayTu_Search, DateTime? NgayDen_Search, string Mahs_Search, double DonGiaTu_Search, double DonGiaDen_Search)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanha.thongtin", "Edit"))
                {

                    var model = (from hosoct in _db.GiaThueMuaNhaXhCt
                                 join hoso in _db.GiaThueMuaNhaXh on hosoct.Mahs equals hoso.Mahs
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMuaNhaXhCt
                                 {
                                     Madv = hoso.Madv,
                                     Tendv = donvi.TenDv,
                                     Tennha = hosoct.Tennha,
                                     Phanloai = hosoct.Phanloai,
                                     SoQD = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                     Dvt = hosoct.Dvt,
                                     Dongia = hosoct.Dongia,
                                     Dongiathue = hosoct.Dongiathue,
                                     Trangthai = hoso.Trangthai,
                                     Mahs = hoso.Mahs,
                                     Maso = hosoct.Maso,
                                     Dvthue = hosoct.Dvthue,
                                 });


                    model = model.Where(t => t.Thoidiem >= NgayTu_Search && t.Thoidiem <= NgayDen_Search && t.Trangthai == "HT" && t.Dongia >= DonGiaTu_Search);
                    if (Madv_Search != "all") { model = model.Where(t => t.Madv == Madv_Search); }
                    if (PhanLoai_Search != "all") { model = model.Where(t => t.Phanloai == PhanLoai_Search); }
                    if (DonGiaDen_Search > 0) { model = model.Where(t => t.Dongia <= DonGiaDen_Search); }
                    if (Mahs_Search != "all") { model = model.Where(t => t.Mahs == Mahs_Search); }

                    ViewData["Dmtmnxh"] = _db.GiaThueMuaNhaXhDm;
                    ViewData["Title"] = "Tìm kiếm thông tin giá thuê mua nhà xã hội";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmnxh";
                    ViewData["MenuLv3"] = "menu_dgtmnxh_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaThueMuaNhaXh/TimKiem/Result.cshtml", model);
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


        [HttpPost("GiaThueMuaNhaXh/GetListHoSo")]
        public JsonResult GetListHoSo(DateTime ngaytu, DateTime ngayden)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.GiaThueMuaNhaXh.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && t.Trangthai == "HT");
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
