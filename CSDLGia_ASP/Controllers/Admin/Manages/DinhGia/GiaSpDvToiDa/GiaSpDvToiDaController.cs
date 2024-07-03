using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.Services;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaSpDvToiDa
{
    public class GiaSpDvToiDaController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IDsDonviService _dsDonviService;
        private readonly ITrangThaiHoSoService _trangThaiHoSoService;

        public GiaSpDvToiDaController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment, IDsDonviService dsDonviService, ITrangThaiHoSoService trangThaiHoSoService)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
            _dsDonviService = dsDonviService;
            _trangThaiHoSoService = trangThaiHoSoService;
        }

        [Route("GiaSpDvToiDa")]
        [HttpGet]
        public IActionResult Index(int Nam, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvtoida.thongtin", "Index"))
                {
                    Madv = string.IsNullOrEmpty(Madv) ? Helpers.GetSsAdmin(HttpContext.Session, "Madv") : Madv;
                    var model_donvi = _dsDonviService.GetListDonvi(Helpers.GetSsAdmin(HttpContext.Session, "Madv"));
                    List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();

                    IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvToiDa> model = _db.GiaSpDvToiDa.Where(t => list_madv.Contains(t.Madv));

                    if (Madv != "all")
                    {
                        model = model.Where(t => t.Madv == Madv);
                    }

                    if (Nam != 0)
                    {
                        model = model.Where(t => t.Thoidiem.Year == Nam);
                    }

                    ViewData["DsDonVi"] = model_donvi;
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvtoida.thongtin", "Create"))
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
                        Thoidiem = DateTime.Now,
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
                    _db.GiaSpDvToiDaCt.AddRange(chitiet);
                    _db.SaveChanges();

                    model.GiaSpDvToiDaCt = chitiet.Where(t => t.Mahs == model.Mahs).ToList();
                    var donVi = _db.DsDonVi.FirstOrDefault(x => x.MaDv == model.Madv);
                    string diaBanApDung = donVi?.DiaBanApDung ?? null;
                    if (!string.IsNullOrEmpty(diaBanApDung))
                    {
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(x => diaBanApDung.Contains(x.MaDiaBan));
                    }
                    else
                    {
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(x => x.Level == "H");
                    }
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
        public IActionResult Store(CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvToiDa request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvtoida.thongtin", "Create"))
                {
                    // 2024.03.15 Gộp Update và phần nhận cho Excel
                    if (_db.GiaSpDvToiDa.Where(x => x.Mahs == request.Mahs).Any())
                    {
                        //Xử lý hồ sơ
                        var modelExcel = _db.GiaSpDvToiDa.FirstOrDefault(t => t.Mahs == request.Mahs);
                        modelExcel.Madiaban = request.Madiaban;
                        modelExcel.Soqd = request.Soqd;
                        modelExcel.Thoidiem = request.Thoidiem;
                        modelExcel.Thongtin = request.Thongtin;
                        modelExcel.Ghichu = request.Ghichu;
                        modelExcel.Updated_at = DateTime.Now;
                        _db.GiaSpDvToiDa.Update(modelExcel);

                        // Xử lý phần lịch sử hồ sơ 
                        //Add Log
                        _trangThaiHoSoService.LogHoSo(modelExcel.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Cập nhật");
                        _db.SaveChanges();

                        return RedirectToAction("Index", "GiaSpDvToiDa", new { request.Madv });
                    }

                    //Phần cũ
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvToiDa
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Manhom = request.Manhom,
                        Madiaban = request.Madiaban,
                        Soqd = request.Soqd,
                        Ttqd = request.Ttqd,
                        Ghichu = request.Ghichu,
                        Thoidiem = request.Thoidiem,
                        Thongtin = request.Thongtin,
                        PhanLoaiHoSo = request.PhanLoaiHoSo,
                        Trangthai = "CC",
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
                    var model_file = _db.ThongTinGiayTo.Where(t => t.Mahs == request.Mahs);
                    if (model_file.Any())
                    {
                        foreach (var file in model_file) { file.Status = "XD"; }
                        _db.ThongTinGiayTo.UpdateRange(model_file);
                    }

                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Thêm mới");

                    // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                    LoggingHelper.LogAction(HttpContext, _db, "Store", "Thêm mới hồ sơ giá sản phẩm dịch vụ tối đa");

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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvtoida.thongtin", "Delete"))
                {
                    var model = _db.GiaSpDvToiDa.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaSpDvToiDa.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.GiaSpDvToiDaCt.Where(t => t.Mahs == model.Mahs);
                    _db.GiaSpDvToiDaCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                    LoggingHelper.LogAction(HttpContext, _db, "Delete", "Xóa hồ sơ giá sản phẩm dịch vụ tối đa");

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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvtoida.thongtin", "Edit"))
                {
                    var model = _db.GiaSpDvToiDa.FirstOrDefault(t => t.Mahs == Mahs);

                    var model_ct = _db.GiaSpDvToiDaCt.Where(t => t.Mahs == model.Mahs);
                    model.GiaSpDvToiDaCt = model_ct.ToList();
                    var model_file = _db.ThongTinGiayTo.Where(t => t.Mahs == model.Mahs);
                    model.ThongTinGiayTo = model_file.ToList();
                    var donVi = _db.DsDonVi.FirstOrDefault(x => x.MaDv == model.Madv);
                    string diaBanApDung = donVi?.DiaBanApDung ?? null;
                    if (!string.IsNullOrEmpty(diaBanApDung))
                    {
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(x => diaBanApDung.Contains(x.MaDiaBan));
                    }
                    else
                    {
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(x => x.Level == "H");
                    }
                    ViewData["Madv"] = model.Madv;
                    ViewData["Ipf1"] = model.Ipf1;
                    ViewData["Title"] = "Bảng giá tính sản phẩm dịch vụ tối đa";
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

        [Route("DinhGiaSpDvToiDa/Show")]
        [HttpGet]

        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvtoida.thongtin", "Index"))
                {
                    var model = _db.GiaSpDvToiDa.FirstOrDefault(t => t.Mahs == Mahs);
                    model.GiaSpDvToiDaCt = _db.GiaSpDvToiDaCt.Where(t => t.Mahs == model.Mahs).ToList();
                    ViewData["Title"] = "Bảng giá sản phẩm dịch vụ tối đa";
                    ViewData["Ipf1"] = model.Ipf1;
                    ViewData["MenuLv1"] = "menu_spdvtoida";
                    ViewData["MenuLv2"] = "menu_spdvtoida_thongtin";
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
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
        public IActionResult Update(CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvToiDa request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.datToiDa.thongtin", "Edit"))
                {
                    var model = _db.GiaSpDvToiDa.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Madiaban = request.Madiaban;
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Ttqd = request.Ttqd;
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
                    var model_file = _db.ThongTinGiayTo.Where(t => t.Mahs == request.Mahs);
                    if (model_file.Any())
                    {
                        foreach (var file in model_file) { file.Status = "XD"; }
                        _db.ThongTinGiayTo.UpdateRange(model_file);
                    }
                    _db.SaveChanges();
                    _db.GiaSpDvToiDaCt.UpdateRange(modelct);
                    _db.SaveChanges();
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Cập nhật");

                    // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                    LoggingHelper.LogAction(HttpContext, _db, "Update", " Update hồ sơ giá sản phẩm dịch vụ tối đa");


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

        public IActionResult Complete(string mahs_chuyen, string trangthai_complete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetn.thongtin", "Index"))
                {
                    var model = _db.GiaSpDvToiDa.FirstOrDefault(t => t.Mahs == mahs_chuyen);

                    model.Updated_at = DateTime.Now;
                    model.Trangthai = trangthai_complete;

                    _db.GiaSpDvToiDa.Update(model);
                    _db.SaveChanges();
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), trangthai_complete);



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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvtoida.timkiem", "Index"))
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
        public IActionResult Result(string madv, string Madiaban, string tenhanghoa, DateTime ngaynhap_tu, DateTime ngaynhap_den, double beginPrice, double endPrice)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvtoida.timkiem", "Index"))
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
                                     Tenspdv = giaspdvtoidact.Tenspdv,
                                     Dongia = giaspdvtoidact.Dongia,
                                     Ghichu = giaspdvtoida.Ttqd,
                                     Madiaban = giaspdvtoidact.Madiaban,
                                     Trangthai = giaspdvtoida.Trangthai,
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

                    //Địa bàn áp dụng
                    if (Madiaban != "all")
                    {
                        model = model.Where(t => t.Madiaban == Madiaban);
                    }

                    if (beginPrice > 0)
                    {
                        model = model.Where(t => t.Dongia >= beginPrice);
                    }
                    if (endPrice > 0)
                    {
                        model = model.Where(t => t.Dongia <= endPrice);
                    }
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá sản phẩm dịch vụ tối đa";
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["MenuLv1"] = "menu_spdvtoida";
                    ViewData["MenuLv2"] = "menu_spdvtoida_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvToiDa/TimKiem/Result.cshtml", model);

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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvtoida.thongtin", "Index"))
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

