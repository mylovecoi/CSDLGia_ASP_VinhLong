
using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.Services;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaHhDvCn
{
    public class GiaHhDvCnController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IDsDonviService _dsDonviService;
        private readonly ITrangThaiHoSoService _trangThaiHoSoService;

        public GiaHhDvCnController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment, IDsDonviService dsDonviService, ITrangThaiHoSoService trangThaiHoSoService)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
            _dsDonviService = dsDonviService;
            _trangThaiHoSoService = trangThaiHoSoService;
        }

        [Route("GiaHhDvCn")]
        [HttpGet]
        public IActionResult Index(int Nam, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.hhdvcn.thongtin", "Index"))
                {
                    Madv = string.IsNullOrEmpty(Madv) ? Helpers.GetSsAdmin(HttpContext.Session, "Madv") : Madv;
                    var model_donvi = _dsDonviService.GetListDonvi(Helpers.GetSsAdmin(HttpContext.Session, "Madv"));
                    List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();

                    IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvCn> model = _db.GiaHhDvCn.Where(t => list_madv.Contains(t.Madv));

                    if (Madv != "all")
                    {
                        model = model.Where(t => t.Madv == Madv);
                    }

                    if (Nam != 0)
                    {
                        model = model.Where(t => t.Thoidiem.Year == Nam);
                    }
                    ViewData["Nam"] = Nam;
                    ViewData["Madv"] = Madv;
                    ViewData["DsDonvi"] = model_donvi;
                    ViewData["NhomTn"] = _db.GiaHhDvCnNhom.ToList();                   
                    ViewData["Title"] = "Thông tin giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_hhdvcn";
                    ViewData["MenuLv3"] = "menu_hhdvcn_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaHhDvCn/Index.cshtml", model);

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

        [Route("GiaHhDvCn/Create")]
        [HttpGet]

        public IActionResult Create(string Manhom, string MadvBc)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.hhdvcn.thongtin", "Create"))
                {

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvCn
                    {
                        Mahs = MadvBc + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Madv = MadvBc,
                        Thoidiem = DateTime.Now,
                    };

                    var danhmuc = _db.GiaHhDvCnDm.ToList(); // lấy dữ liệu trong bảng GiaSpDvToiDaDm


                    // Khi bấm đồng ý trong moda thì add dữ liệu GiaSpDvToiDaDm -> bản GiaSpDvToiDaCt
                    if (Manhom != "all")
                    {
                        danhmuc = danhmuc.Where(t => t.Manhom == Manhom).ToList();
                    }
                    else
                    {
                        danhmuc = danhmuc.ToList();
                    }


                    var chitiet = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvCnCt>();


                    foreach (var item in danhmuc)
                    {
                        chitiet.Add(new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvCnCt()
                        {
                            Mahs = model.Mahs,
                            Sapxep = item.Sapxep,
                            Tenspdv = item.Tenspdv,
                            Maspdv = item.Maspdv,
                            Dvt = item.Dvt,
                            HienThi = item.HienThi,
                            Style = item.Style,
                            Trangthai = "CXD",
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        });
                    }
                    _db.GiaHhDvCnCt.AddRange(chitiet);
                    _db.SaveChanges();

                    model.GiaHhDvCnCt = chitiet.ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "T");
                    ViewData["Manhom"] = Manhom;
                    ViewData["Madv"] = MadvBc; // gửi sang create
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["Title"] = "Bảng giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành ";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_hhdvcn";
                    ViewData["MenuLv3"] = "menu_hhdvcn_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaHhDvCn/Create.cshtml", model);

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

        [Route("GiaHhDvCn/Store")]
        [HttpPost]
        public IActionResult Store(CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvCn request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.hhdvcn.thongtin", "Create"))
                {
                    //Update
                    if (_db.GiaHhDvCn.Where(x => x.Mahs == request.Mahs).Any())
                    {
                        //Xử lý hồ sơ
                        var modelExcel = _db.GiaHhDvCn.FirstOrDefault(t => t.Mahs == request.Mahs);
                        modelExcel.Madiaban = request.Madiaban;
                        modelExcel.Soqd = request.Soqd;
                        modelExcel.Ttqd = request.Ttqd;
                        modelExcel.Thoidiem = request.Thoidiem;
                        modelExcel.Thongtin = request.Thongtin;
                        modelExcel.Ghichu = request.Ghichu;
                        modelExcel.Trangthai = "CC";
                        modelExcel.Congbo = "CHUACONGBO";
                        modelExcel.Updated_at = DateTime.Now;
                        _db.GiaHhDvCn.Update(modelExcel);
                        //Add Log
                        _trangThaiHoSoService.LogHoSo(modelExcel.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Cập nhật");

                        return RedirectToAction("Index", "GiaHhDvCn", new { request.Madv });
                    }


                    // thêm dữ liệu vào
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvCn
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Soqd = request.Soqd,
                        Ttqd = request.Ttqd,
                        Thoidiem = request.Thoidiem,
                        Thongtin = request.Thongtin,
                        Trangthai = "CC",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaHhDvCn.Add(model);
                    _db.SaveChanges();
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_hhdvcn";
                    ViewData["MenuLv3"] = "menu_hhdvcn_tt";
                    //update lại Trangthai trong bảng chi tiết
                    var modelct = _db.GiaHhDvCnCt.Where(t => t.Mahs == request.Mahs);

                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Trangthai = "XD";
                        }
                    }
                    _db.GiaHhDvCnCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    // Xóa Trangthai chưa xác định
                    var model_ct2 = _db.GiaHhDvCnCt.Where(t => t.Trangthai == "CXD");
                    _db.GiaHhDvCnCt.RemoveRange(model_ct2);
                    _db.SaveChanges();

                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Thêm mới");

                    return RedirectToAction("Index", "GiaHhDvCn", new { request.Madv });
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

        [Route("GiaHhDvCn/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.spdvcuthe.thongtin", "Delete"))
                {
                    var model = _db.GiaHhDvCn.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaHhDvCn.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.GiaHhDvCnCt.Where(t => t.Mahs == model.Mahs);
                    _db.GiaHhDvCnCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaHhDvCn", new { model.Madv });

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

        // Sau khi nhận mã hồ sơ
        // Gửi model( phải có cả thông tin từ bảng ct ) nên phải tạo 1 cái notmap trong GiaHhDvCn ( ban đầu cái này rỗng ) nên phải add dữ liệu từ bảng GiaHhDvCn vào

        [Route("GiaHhDvCn/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.hhdvcn.thongtin", "Edit"))
                {
                    var model = _db.GiaHhDvCn.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_ct = _db.GiaHhDvCnCt.Where(t => t.Mahs == model.Mahs);
                    model.GiaHhDvCnCt = model_ct.ToList();
                    var model_file = _db.ThongTinGiayTo.Where(t => t.Mahs == model.Mahs);
                    model.ThongTinGiayTo = model_file.ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Title"] = "Bảng giá tính giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_hhdvcn";
                    ViewData["MenuLv3"] = "menu_hhdvcn_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaHhDvCn/Create.cshtml", model);

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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.hhdvcn.thongtin", "Index"))
                {
                    // Lấy bản ghi có Mahs =  mahs_chuyen
                    var model = _db.GiaHhDvCn.FirstOrDefault(t => t.Mahs == mahs_chuyen);
                    model.Trangthai = trangthai_complete;
                    model.Updated_at = DateTime.Now;
                    _db.GiaHhDvCn.Update(model);
                    _db.SaveChanges();
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), trangthai_complete);
                    return RedirectToAction("Index", "GiaHhDvCn", new { Madv = model.Madv });

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


        [Route("GiaHhDvCn/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.hhdvcn.thongtin", "Index"))
                {
                    var model = _db.GiaHhDvCn.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_ct = _db.GiaHhDvCnCt.Where(t => t.Mahs == model.Mahs);
                    model.GiaHhDvCnCt = model_ct.ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["Title"] = "Thông tin hồ sơ giá giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành";
                    return View("Views/Admin/Manages/DinhGia/GiaHhDvCn/Show.cshtml", model);

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

        [Route("GiaHhDvCn/TimKiem")]
        [HttpGet]
        public IActionResult Search()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.hhdvcn.timkiem", "Index"))
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
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_hhdvcn";
                    ViewData["MenuLv3"] = "menu_hhdvcn_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaHhDvCn/TimKiem/Index.cshtml");

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

        [Route("GiaHhDvCn/TimKiem/KetQua")]
        [HttpPost]
        public IActionResult Result(string madv, string tenhanghoa, DateTime ngaynhap_tu, DateTime ngaynhap_den, double beginPrice, double endPrice)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.hhdvcn.timkiem", "Index"))
                {

                    var model = (from chitiet in _db.GiaHhDvCnCt
                                 join hoso in _db.GiaHhDvCn on chitiet.Mahs equals hoso.Mahs
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvCnCt
                                 {
                                     Id = chitiet.Id,
                                     Mahs = hoso.Mahs,
                                     Dongia = chitiet.Dongia,
                                     Tenspdv = chitiet.Tenspdv,
                                     Madv = hoso.Madv,
                                     Thoidiem = hoso.Thoidiem,
                                     Tendv = donvi.TenDv,
                                     Ttqd = hoso.Ttqd,
                                     Dvt = chitiet.Dvt,
                                     Trangthai = hoso.Trangthai
                                 });
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model = model.Where(t => list_trangthai.Contains(t.Trangthai));
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

                    //Tên sản phẩm dịch vụ
                    if (!string.IsNullOrEmpty(tenhanghoa))
                    {
                        model = model.Where(t => t.Tenspdv.ToLower().Contains(tenhanghoa.ToLower()));
                    }

                    if (beginPrice > 0)
                    {
                        model = model.Where(t => t.Dongia >= beginPrice);
                    }
                    if (endPrice > 0)
                    {
                        model = model.Where(t => t.Dongia <= endPrice);
                    }

                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_hhdvcn";
                    ViewData["MenuLv3"] = "menu_hhdvcn_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaHhDvCn/TimKiem/Result.cshtml", model);
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
