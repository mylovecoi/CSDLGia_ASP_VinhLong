using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CSDLGia_ASP.Database;
using System.Security.Cryptography;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSDLGia_ASP.ViewModels.Systems;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using CSDLGia_ASP.Models.Manages.KeKhaiDkg;
using CSDLGia_ASP.ViewModels.Manages.KeKhaiDkg;
using Microsoft.Extensions.Hosting;
using CSDLGia_ASP.Models.Systems;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaHhHaiQuanXnk
{
    public class GiaHhHaiQuanXnkController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GiaHhHaiQuanXnkController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("GiaHhHaiQuanXnk")]
        [HttpGet]
        public IActionResult Index(string Nam, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.hqxnk.thongtin", "Index"))
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

                        var model = _db.GiaHhHaiQuanXnk.Where(t => t.Madv == Madv).ToList();

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
                        ViewData["Nhom"] = _db.GiaHhHaiQuanXnkDm;
                        ViewData["Nam"] = Nam;
                        ViewData["Madv"] = Madv;
                        ViewData["DmDvt"] = _db.DmDvt;
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                        ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                        ViewData["Title"] = "Thông tin giá hàng hoá hải quan trong xuất nhập khẩu";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dghqxnk";
                        ViewData["MenuLv3"] = "menu_dghqxnk_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaHhHaiQuanXnk/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Title"] = "Thông tin giá hàng hoá hải quan trong xuất nhập khẩu";
                        ViewData["Messages"] = "Hệ thống chưa có định giá hàng hoá hải quan trong xuất nhập khẩu.";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dghqxnk";
                        ViewData["MenuLv3"] = "menu_dghqxnk_tt";
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
        [Route("GiaHhHaiQuanXnk/Create")]
        [HttpGet]
        public IActionResult Create(string Manhom, string MadvBc)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.hqxnk.thongtin", "Create"))
                {
                    var check = _db.GiaHhHaiQuanXnkCt.Where(t => t.Trangthai == "CXD");
                    if (check != null)
                    {
                        _db.GiaHhHaiQuanXnkCt.RemoveRange(check);
                        _db.SaveChanges();
                    }

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhHaiQuanXnk
                    {
                        Mahs = MadvBc + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Madv = MadvBc,
                        Manhom = Manhom,
                    };

                    /*var chitiet = new GiaHhHaiQuanXnkCt {
                        Mahs = model.Mahs,
                        Trangthai = "CXD",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaHhHaiQuanXnkCt.AddRange(chitiet);
                    
                     */
                    _db.SaveChanges();
                    model.GiaHhHaiQuanXnkCt = _db.GiaHhHaiQuanXnkCt.Where(t => t.Mahs == model.Mahs).ToList();

                    ViewData["Madv"] = MadvBc;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["Nhom"] = Manhom;
                    ViewData["DmDvt"] = _db.DmDvt;
                    ViewData["Thue"] = _db.GiaHhHaiQuanXnkThue;
                    
                    ViewData["Title"] = "Thông tin giá hàng hoá hải quan trong xuất nhập khẩu";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dghqxnk";
                    ViewData["MenuLv3"] = "menu_dghqxnk_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaHhHaiQuanXnk/Create.cshtml", model);

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
        [Route("GiaHhHaiQuanXnk/Store")]
        [HttpPost]
        public async Task<IActionResult> Store(CSDLGia_ASP.Models.Manages.DinhGia.GiaHhHaiQuanXnk request, IFormFile Ipf1upload)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.hqxnk.thongtin", "Create"))
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

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhHaiQuanXnk
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
                    _db.GiaHhHaiQuanXnk.Add(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaHhHaiQuanXnkCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Trangthai = "XD";
                        }
                    }
                    _db.GiaHhHaiQuanXnkCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaHhHaiQuanXnk", new { request.Madv });
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
        [Route("GiaHhHaiQuanXnk/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.hqxnk.thongtin", "Edit"))
                {
                    var model = _db.GiaHhHaiQuanXnk.FirstOrDefault(t => t.Mahs == Mahs);

                    //var model_ct = _db.GiaHhHaiQuanXnkCt.Where(t => t.Mahs == model.Mahs);
                    var model_ct = from ct in _db.GiaHhHaiQuanXnkCt.Where(x=>x.Mahs==Mahs)
                                join thue in _db.GiaHhHaiQuanXnkThue
                                on ct.MaThue equals thue.MaThue
                                select new GiaHhHaiQuanXnkCt
                                {
                                    Id=ct.Id,
                                    TenHh = ct.TenHh,
                                    Dvt = ct.Dvt,
                                    GiaTruocThue = ct.GiaTruocThue,
                                    GiaSauThue = ct.GiaSauThue,
                                    PhanTramThue = ct.PhanTramThue,
                                    MaThue = ct.MaThue,
                                    TenThue = thue.TenThue,
                                };

                    model.GiaHhHaiQuanXnkCt = model_ct.ToList();

                    /*ViewData["Madv"] = model.Madv;
                    ViewData["Ipf1"] = model.Ipf1;*/
                    ViewData["Title"] = "Bảng giá hàng hoá hải quan trong xuất nhập khẩu";
                    ViewData["Thue"] = _db.GiaHhHaiQuanXnkThue;
                    ViewData["DmDvt"] = _db.DmDvt;
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dghqxnk";
                    ViewData["MenuLv3"] = "menu_dghqxnk_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaHhHaiQuanXnk/Edit.cshtml", model);

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
        [Route("GiaHhHaiQuanXnk/Update")]
        [HttpPost]
        public async Task<IActionResult> Update(CSDLGia_ASP.Models.Manages.DinhGia.GiaHhHaiQuanXnk request, IFormFile Ipf1upload)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.hqxnk.thongtin", "Edit"))
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

                    var model = _db.GiaHhHaiQuanXnk.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Soqdlk = request.Soqdlk;
                    model.Thoidiemlk = request.Thoidiemlk;
                    model.Cqbh = request.Cqbh;
                    model.Ghichu = request.Ghichu;
                    model.Ipf1 = request.Ipf1;
                    model.Updated_at = DateTime.Now;

                    _db.GiaHhHaiQuanXnk.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaHhHaiQuanXnk", new { request.Madv });
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
        [Route("GiaHhHaiQuanXnk/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.hqxnk.thongtin", "Delete"))
                {
                    var model = _db.GiaHhHaiQuanXnk.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaHhHaiQuanXnk.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.GiaHhHaiQuanXnkCt.Where(t => t.Mahs == model.Mahs);
                    _db.GiaHhHaiQuanXnkCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaHhHaiQuanXnk", new { model.Madv });
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
        [Route("GiaHhHaiQuanXnk/Search")]
        [HttpGet]
        public IActionResult Search()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.hqxnk.thongtin", "Index"))
                {

                    if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") != null)
                    {
                        ViewData["Madv"] = Helpers.GetSsAdmin(HttpContext.Session, "Madv");
                    }
                    else
                    {
                        ViewData["Madv"] = "";
                    }
                    ViewData["DsDiaBan"] = _db.DsDiaBan;
                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Nhom"] = _db.GiaHhHaiQuanXnkDm.ToList();
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá hàng hoá hải quan trong xuất nhập khẩu";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dghqxnk";
                    ViewData["MenuLv3"] = "menu_dghqxnk_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaHhHaiQuanXnk/TimKiem/Index.cshtml");

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

        [Route("GiaHhHaiQuanXnk/Result")]
        [HttpPost]
        public IActionResult Result(string madv, string manhom, DateTime ngaynhap_tu,
            DateTime ngaynhap_den, string tinhtrang, double gia_tu, double gia_den)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.hqxnk.thongtin", "Index"))
                {
                    var model = (from giact in _db.GiaHhHaiQuanXnkCt
                                 join gia in _db.GiaHhHaiQuanXnk on giact.Mahs equals gia.Mahs
                                 join donvi in _db.DsDonVi on gia.Madv equals donvi.MaDv
                                 /*join nhomtn in _db.GiaHhHaiQuanXnkNhom on gia.Manhom equals nhomtn.Manhom*/
                                 select new GiaHhHaiQuanXnkCt
                                 {
                                     Id = giact.Id,
                                     Dvt = giact.Dvt,
                                     GiaTruocThue = giact.GiaTruocThue,
                                     GiaSauThue = giact.GiaSauThue,
                                     Mahs = giact.Mahs,
                                     Madv = gia.Madv,
                                     Manhom = gia.Manhom,
                                     Thoidiem = gia.Thoidiem,
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
                    if (tinhtrang == "truoc")
                    {
                        model = model.Where(t => t.GiaTruocThue >= gia_tu);
                        if (gia_den > 0)
                        {
                            model = model.Where(t => t.GiaTruocThue <= gia_den);
                        }
                    }
                    if (tinhtrang == "sau")
                    {
                        model = model.Where(t => t.GiaSauThue >= gia_tu);
                        if (gia_den > 0)
                        {
                            model = model.Where(t => t.GiaSauThue <= gia_den);
                        }
                    }


                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá thuế tài nguyên";
                    ViewData["Tinhtrang"] = tinhtrang;
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dghqxnk";
                    ViewData["MenuLv3"] = "menu_dghqxnk_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaHhHaiQuanXnk/TimKiem/Result.cshtml", model);

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
    }

}
