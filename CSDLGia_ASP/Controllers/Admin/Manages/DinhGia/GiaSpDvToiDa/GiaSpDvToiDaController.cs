﻿
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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaSpDvToiDa
{
    public class GiaSpDvToiDaController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GiaSpDvToiDaController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }


        [Route("GiaSpDvToiDa")]
        [HttpGet]
        public IActionResult Index(string Nam, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.spdvtoida.thongtin", "Index"))
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

                        var model = _db.GiaSpDvToiDa.Where(t => t.Madv == Madv).ToList();

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
                        ViewData["NhomTn"] = _db.GiaSpDvToiDaNhom.ToList();
                        ViewData["Nam"] = Nam;
                        ViewData["Madv"] = Madv;
                        ViewData["Title"] = "Thông tin giá sản phẩm dịch vụ tối đa";
                        ViewData["MenuLv1"] = "menu_spdvtoida";
                        ViewData["MenuLv2"] = "menu_spdvtoida_thongtin";
                        return View("Views/Admin/Manages/DinhGia/GiaSpDvToiDa/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Title"] = "Thông tin giá sản phẩm dịch vụ tối đa";
                        ViewData["Messages"] = "Hệ thống chưa có định giá sản phẩm dịch vụ tối đa.";
                        ViewData["MenuLv1"] = "menu_spdvtoida";
                        ViewData["MenuLv2"] = "menu_spdvtoida_thongtin";
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

        [Route("GiaSpDvToiDa/Create")]
        [HttpGet]
        public IActionResult Create(string Manhom, string MadvBc)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.spdvtoida.thongtin", "Create"))
                {

                    // hồ sơ mà chỉ đến bước hoàn thành mà quay lại thì sẽ có trạng thái CXD --> cần xóa
                    var check = _db.GiaSpDvToiDaCt.Where(t => t.Trangthai == "CXD");
                    if (check != null)
                    {
                        _db.GiaSpDvToiDaCt.RemoveRange(check);
                        _db.SaveChanges();
                    }

                    // Thông tin của bộ hồ sơ

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvToiDa
                    {
                        Mahs = MadvBc + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Madv = MadvBc,
                        Manhom = Manhom,
                    };

                    var danhmuc = _db.GiaSpDvToiDaDm.ToList(); // lấy dữ liệu trong bảng GiaSpDvToiDaDm


                    // Khi bấm đồng ý trong moda thì add dữ liệu GiaSpDvToiDaDm -> bản GiaSpDvToiDaCt
                    if (Manhom != "all")
                    {
                        danhmuc = danhmuc.Where(t => t.Manhom == Manhom).ToList();
                    }
                    else
                    {
                        danhmuc = danhmuc.ToList();
                    }


                    var chitiet = new List<GiaSpDvToiDaCt>();


                    foreach (var item in danhmuc)
                    {
                        chitiet.Add(new GiaSpDvToiDaCt()
                        {
                            Mahs = model.Mahs,
                            Phanloaidv = item.Tenspdv,
                            Maspdv = item.Maspdv,
                            Mota=item.Mota,
                            Tendv = item.Tenspdv,
                            Dvt = item.Dvt,
                            Dongia = item.Gia,
                            Trangthai = "CXD",
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        });
                    }
                    _db.GiaSpDvToiDaCt.AddRange(chitiet);
                    _db.SaveChanges();

                    model.GiaSpDvToiDaCt = chitiet.Where(t => t.Mahs == model.Mahs).ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "T");
                    ViewData["Manhom"] = Manhom;
                    ViewData["Madv"] = MadvBc;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["Title"] = "Bảng giá sản phẩm dịch vụ tối đa";
                    ViewData["MenuLv1"] = "menu_spdvtoida";
                    ViewData["MenuLv2"] = "menu_spdvtoida_thongtin";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvToiDa/Create.cshtml", model);

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



        [Route("GiaSpDvToiDa/Store")]
        [HttpPost]
        public async Task<IActionResult> Store(CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvToiDa request, IFormFile Ipf1)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.spdvtoida.thongtin", "Create"))
                {
                    if (Ipf1 != null && Ipf1.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf1.FileName);
                        string extension = Path.GetExtension(Ipf1.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf1.CopyToAsync(FileStream);
                        }
                        request.Ipf1 = filename;
                    }

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvToiDa
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Manhom = request.Manhom,
                        Madiaban = request.Madiaban,
                        Soqd = request.Soqd,
                        Ttqd = request.Ttqd,
                        Ghichu = request.Ghichu,
                        Ipf1 = request.Ipf1,
                        Thoidiem = request.Thoidiem,
                        Thongtin = request.Thongtin,
                        Trangthai = "CHT",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaSpDvToiDa.Add(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaSpDvToiDaCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Trangthai = "XD";
                        }
                    }
                    _db.GiaSpDvToiDaCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaSpDvToiDa", new { request.Madv });
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



        [Route("GiaSpDvToiDa/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.spdvtoida.thongtin", "Delete"))
                {
                    var model = _db.GiaSpDvToiDa.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaSpDvToiDa.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.GiaSpDvToiDaCt.Where(t => t.Mahs == model.Mahs);
                    _db.GiaSpDvToiDaCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaSpDvToiDa", new { model.Madv });
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


        [Route("GiaSpDvToiDa/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.spdvtoida.thongtin", "Edit"))
                {
                    var model = _db.GiaSpDvToiDa.FirstOrDefault(t => t.Mahs == Mahs);

                    var model_ct = _db.GiaSpDvToiDaCt.Where(t => t.Mahs == model.Mahs);

                    model.GiaSpDvToiDaCt = model_ct.ToList();

                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Madv"] = model.Madv;
                    ViewData["Ipf1"] = model.Ipf1;
                    ViewData["Title"] = "Bảng giá tính sản phẩm dịch vụ tối đa";
                    ViewData["MenuLv1"] = "menu_spdvtoida";
                    ViewData["MenuLv2"] = "menu_spdvtoida_thongtin";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvToiDa/Modify.cshtml", model);

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

        [Route("DinhGiaSpDvToiDa/Show")]
        [HttpGet]
      
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.spdvtoida.thongtin", "Index"))
                {
                    var model = _db.GiaSpDvToiDa.FirstOrDefault(t => t.Mahs == Mahs);
                    model.GiaSpDvToiDaCt = _db.GiaSpDvToiDaCt.Where(t => t.Mahs == model.Mahs).ToList();

                    ViewData["Title"] = "Bảng giá sản phẩm dịch vụ tối đa";
                    ViewData["Ipf1"] = model.Ipf1;
                    ViewData["MenuLv1"] = "menu_spdvtoida";
                    ViewData["MenuLv2"] = "menu_spdvtoida_thongtin";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvToiDa/Show.cshtml", model);

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


        [Route("DinhGiaSpDvToiDa/Update")]
        [HttpPost]
        public async Task<IActionResult> Update(CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvToiDa request, IFormFile Ipf1)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.datToiDa.thongtin", "Edit"))
                {
                    if (Ipf1 != null && Ipf1.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf1.FileName);
                        string extension = Path.GetExtension(Ipf1.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf1.CopyToAsync(FileStream);
                        }
                        request.Ipf1 = filename;
                    }
                    var model = _db.GiaSpDvToiDa.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Madiaban = request.Madiaban;
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Ttqd = request.Ttqd;
                    model.Ipf1 = request.Ipf1;
                    model.Ghichu = request.Ghichu;
                    model.Updated_at = DateTime.Now;
                    _db.GiaSpDvToiDa.Update(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaSpDvToiDaCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Mahs = model.Mahs;
                        }
                    }
                    _db.GiaSpDvToiDaCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaSpDvToiDa", new { request.Mahs });
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


        public IActionResult Complete(string mahs_chuyen, string macqcq_chuyen)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetn.thongtin", "Index"))
                {
                    var model = _db.GiaSpDvToiDa.FirstOrDefault(t => t.Mahs == mahs_chuyen);

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
                    _db.GiaSpDvToiDa.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaSpDvToiDa", new { model.Madv });

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


        [Route("GiaSpDvToiDa/TimKiem")]
        [HttpGet]
        public IActionResult Search()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.spdvtoida.thongtin", "Index"))
                {

                    if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") != null)
                    {
                        ViewData["Madv"] = Helpers.GetSsAdmin(HttpContext.Session, "Madv");
                    }
                    else
                    {
                        ViewData["Madv"] = "";
                    }
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá sản phẩm dịch vụ tối đa";
                    ViewData["MenuLv1"] = "menu_spdvtoida";
                    ViewData["MenuLv2"] = "menu_spdvtoida_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvToiDa/TimKiem/Index.cshtml");

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

        [Route("GiaSpDvToiDa/TimKiem/KetQua")]
        [HttpPost]
        public IActionResult Result(string madv, string manhom,string tenhanghoa, DateTime ngaynhap_tu, DateTime ngaynhap_den, double beginPrice, double endPrice)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.spdvtoida.thongtin", "Index"))
                {
                    var model = (from giaspdvtoidact in _db.GiaSpDvToiDaCt
                                 join giaspdvtoida in _db.GiaSpDvToiDa on giaspdvtoidact.Mahs equals giaspdvtoida.Mahs
                                 join donvi in _db.DsDonVi on giaspdvtoida.Madv equals donvi.MaDv
                                 select new GiaSpDvToiDaCt
                                 {
                                     Id = giaspdvtoidact.Id,
                                     Dvt = giaspdvtoidact.Dvt,
                                     Mahs = giaspdvtoidact.Mahs,
                                     Madv = giaspdvtoida.Madv,
                                     Thoidiem = giaspdvtoida.Thoidiem,
                                     Tendv = donvi.TenDv,
                                     Ttqd = giaspdvtoida.Ttqd,
                                     Mota = giaspdvtoidact.Mota,
                                     Dongia = giaspdvtoidact.Dongia,
                                 });


                    if (madv != "all")
                    {
                        model = model.Where(t => t.Madv == madv);
                    }

                    if (ngaynhap_tu.ToString("yyMMdd") != "010101")
                    {
                        model = model.Where(t => t.Thoidiem >= ngaynhap_tu);
                    }



                    if (ngaynhap_den.ToString("yyMMdd") != "010101")
                    {
                        model = model.Where(t => t.Thoidiem <= ngaynhap_den);
                    }

                    if (tenhanghoa != null)
                    {
                        model = model.Where(t => t.Mota == tenhanghoa);
                    }
                    if (beginPrice != 0)
                    {
                        model = model.Where(t => t.Dongia >= beginPrice);
                    }
                    if (endPrice != 0)
                    {
                        model = model.Where(t => t.Dongia <= endPrice);
                    }
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá sản phẩm dịch vụ tối đa";
                    ViewData["MenuLv1"] = "menu_spdvtoida";
                    ViewData["MenuLv2"] = "menu_spdvtoida_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvToiDa/TimKiem/Result.cshtml",model);

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
        [Route("GiaSpDvToiDa/Print")]
        [HttpGet]
        public IActionResult Print(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.spdvtoida.thongtin", "Index"))
                {
                    var model = _db.GiaSpDvToiDa.FirstOrDefault(t => t.Mahs == Mahs);

                    var hoso_dg = new VMDinhGiaPrint
                    {
                        Id = model.Id,

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

                    var modelct = _db.GiaSpDvToiDaCt.Where(t => t.Mahs == model.Mahs);
                    if (modelct != null)
                    {
                        hoso_dg.GiaSpDvToiDaCt = modelct.ToList();
                    }

                    /*var model = GetThongTinKk(Mahs);*/


                    ViewData["Title"] = "In định giá đât cụ thể";
                    ViewData["MenuLv1"] = "menu_spdvtoida";
                    ViewData["MenuLv2"] = "menu_spdvtoida_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvToiDa/Print.cshtml", hoso_dg);

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
