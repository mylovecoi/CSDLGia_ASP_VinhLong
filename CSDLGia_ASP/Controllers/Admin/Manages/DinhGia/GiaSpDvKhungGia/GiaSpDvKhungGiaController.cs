
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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaSpDvKhungGia
{
    public class GiaSpDvKhungGiaController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IDsDonviService _dsDonviService;
        private readonly ITrangThaiHoSoService _trangThaiHoSoService;

        public GiaSpDvKhungGiaController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment, IDsDonviService dsDonviService, ITrangThaiHoSoService trangThaiHoSoService)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
            _dsDonviService = dsDonviService;
            _trangThaiHoSoService = trangThaiHoSoService;
        }

        [Route("GiaSpDvKhungGia")]
        [HttpGet]
        public IActionResult Index(int Nam, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvkhunggia.thongtin", "Index"))
                {
                    Madv = string.IsNullOrEmpty(Madv) ? Helpers.GetSsAdmin(HttpContext.Session, "Madv") : Madv;

                    var model_donvi = _dsDonviService.GetListDonvi(Helpers.GetSsAdmin(HttpContext.Session, "Madv"));
                    List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();

                    IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvKhungGia> model = _db.GiaSpDvKhungGia;
                    if (Madv != "all")
                    {
                        model = model.Where(t => t.Madv == Madv);
                    }

                    if (Nam != 0)
                    {
                        model = model.Where(t => t.Thoidiem.Year == Nam);
                    }

                    ViewData["DsDonVi"] = model_donvi;
                    ViewData["NhomTn"] = _db.GiaSpDvKhungGiaNhom.ToList();
                    ViewData["Nam"] = Nam;
                    ViewData["Madv"] = Madv;
                    ViewData["Title"] = "Thông tin giá sản phẩm dịch vụ khung giá";
                    ViewData["MenuLv1"] = "menu_spdvkhunggia";
                    ViewData["MenuLv2"] = "menu_spdvkhunggia_thongtin";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvKhungGia/Index.cshtml", model);
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

        [Route("GiaSpDvKhungGia/Create")]
        [HttpGet]
        public IActionResult Create(string Manhom, string MadvBc)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvkhunggia.thongtin", "Create"))
                {
                    //hồ sơ mà chỉ đến bước hoàn thành mà quay lại thì sẽ có trạng thái CXD-- > cần xóa
                    var check = _db.GiaSpDvKhungGiaCt.Where(t => t.Trangthai == "CXD");
                    if (check != null)
                    {
                        _db.GiaSpDvKhungGiaCt.RemoveRange(check);
                        _db.SaveChanges();
                    }

                    //Thông tin của bộ hồ sơ
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvKhungGia
                    {
                        Mahs = MadvBc + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Madv = MadvBc,
                        Thoidiem = DateTime.Now,
                        //Manhom = Manhom,
                    };

                    var danhmuc = _db.GiaSpDvKhungGiaDm.ToList(); // lấy dữ liệu trong bảng GiaSpDvKhungGiaDm


                    // Khi bấm đồng ý trong moda thì add dữ liệu GiaSpDvKhungGiaDm -> bản GiaSpDvKhungGiaCt
                    if (Manhom != "all")
                    {
                        danhmuc = danhmuc.Where(t => t.Manhom == Manhom).ToList();
                    }
                    else
                    {
                        danhmuc = danhmuc.ToList();
                    }


                    var chitiet = new List<GiaSpDvKhungGiaCt>();


                    foreach (var item in danhmuc)
                    {
                        chitiet.Add(new GiaSpDvKhungGiaCt()
                        {
                            Mahs = model.Mahs,
                            Tenspdv = item.Tenspdv,
                            Dvt = item.Dvt,
                            Maspdv = item.Maspdv,
                            SapXep = item.SapXep,
                            HienThi = item.HienThi,
                            Manhom = item.Manhom,
                            Giatoithieu = 0,
                            Giatoida = 0,
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        });
                    }
                    _db.GiaSpDvKhungGiaCt.AddRange(chitiet);
                    _db.SaveChanges();

                    model.GiaSpDvKhungGiaCt = chitiet.Where(t => t.Mahs == model.Mahs).ToList();
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
                    ViewData["Title"] = "Bảng giá sản phẩm dịch vụ khung giá";
                    ViewData["MenuLv1"] = "menu_spdvkhunggia";
                    ViewData["MenuLv2"] = "menu_spdvkhunggia_thongtin";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvKhungGia/Create.cshtml", model);

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

        [Route("GiaSpDvKhungGia/Store")]
        [HttpPost]
        public IActionResult Store(CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvKhungGia request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvkhunggia.thongtin", "Create"))
                {
                    // 2024.03.15 Gộp Update và phần nhận cho Excel
                    if (_db.GiaSpDvKhungGia.Where(x => x.Mahs == request.Mahs).Any())
                    {
                        //Xử lý hồ sơ
                        var modelExcel = _db.GiaSpDvKhungGia.FirstOrDefault(t => t.Mahs == request.Mahs);
                        modelExcel.Madiaban = request.Madiaban;
                        modelExcel.Soqd = request.Soqd;
                        modelExcel.Thoidiem = request.Thoidiem;
                        modelExcel.Thongtin = request.Thongtin;
                        modelExcel.Ttqd = request.Ttqd;
                        modelExcel.Ghichu = request.Ghichu;
                        modelExcel.Updated_at = DateTime.Now;
                        _db.GiaSpDvKhungGia.Update(modelExcel);

                        // Xử lý phần lịch sử hồ sơ 
                        //Add Log
                        _trangThaiHoSoService.LogHoSo(modelExcel.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Chỉnh sửa");


                        return RedirectToAction("Index", "GiaSpDvKhungGia", new { request.Madv });
                    }
                    //Phần cũ
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvKhungGia
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Manhom = request.Manhom,
                        Madiaban = request.Madiaban,
                        Soqd = request.Soqd,
                        Ghichu = request.Ghichu,
                        Thoidiem = request.Thoidiem,
                        Ttqd = request.Ttqd,
                        Trangthai = "CC",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaSpDvKhungGia.Add(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaSpDvKhungGiaCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Trangthai = "XD";
                        }
                    }
                    _db.GiaSpDvKhungGiaCt.UpdateRange(modelct);
                    _db.SaveChanges();
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Thêm mới");

                    // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                    LoggingHelper.LogAction(HttpContext, _db, "Store", "Thêm mới hồ sơ giá sản phẩm dịch vụ khung giá");

                    return RedirectToAction("Index", "GiaSpDvKhungGia", new { request.Madv });
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

        [Route("GiaSpDvKhungGia/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvkhunggia.thongtin", "Delete"))
                {
                    var model = _db.GiaSpDvKhungGia.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaSpDvKhungGia.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.GiaSpDvKhungGiaCt.Where(t => t.Mahs == model.Mahs);
                    _db.GiaSpDvKhungGiaCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                    LoggingHelper.LogAction(HttpContext, _db, "Delete", "Xóa hồ sơ giá sản phẩm dịch vụ khung giá");

                    return RedirectToAction("Index", "GiaSpDvKhungGia", new { model.Madv });
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

        [Route("GiaSpDvKhungGia/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvkhunggia.thongtin", "Edit"))
                {
                    var model = _db.GiaSpDvKhungGia.FirstOrDefault(t => t.Mahs == Mahs);

                    var model_ct = _db.GiaSpDvKhungGiaCt.Where(t => t.Mahs == model.Mahs);
                    model.GiaSpDvKhungGiaCt = model_ct.ToList();
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
                    ViewData["Title"] = "Bảng giá sản phẩm dịch vụ khung giá";
                    ViewData["MenuLv1"] = "menu_spdvkhunggia";
                    ViewData["MenuLv2"] = "menu_spdvkhunggia_thongtin";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvKhungGia/Create.cshtml", model);

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


        [Route("GiaSpDvKhungGia/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvkhunggia.thongtin", "Edit"))
                {
                    var model = _db.GiaSpDvKhungGia.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_ct = _db.GiaSpDvKhungGiaCt.Where(t => t.Mahs == Mahs);

                    model.GiaSpDvKhungGiaCt = model_ct.ToList();
                    ViewData["Title"] = "Thông tin chi tiết sản phẩm dịch vụ khung giá";
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvKhungGia/Show.cshtml", model);
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

        [Route("DinhGiaSpDvKhungGia/Update")]
        [HttpPost]
        public async Task<IActionResult> Update(CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvKhungGia request, IFormFile Ipf1)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvkhunggia.thongtin", "Edit"))
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

                    var model = _db.GiaSpDvKhungGia.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Madiaban = request.Madiaban;
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Thongtin = request.Thongtin;
                    model.Ipf1 = request.Ipf1;
                    model.Ttqd = request.Ttqd;
                    model.Updated_at = DateTime.Now;
                    _db.GiaSpDvKhungGia.Update(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaSpDvKhungGiaCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Mahs = model.Mahs;
                        }
                    }
                    _db.GiaSpDvKhungGiaCt.UpdateRange(modelct);
                    _db.SaveChanges();
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Cập nhật");

                    // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                    LoggingHelper.LogAction(HttpContext, _db, "Update", " Update hồ sơ giá sản phẩm dịch vụ khung giá");

                    return RedirectToAction("Index", "GiaSpDvKhungGia", new { request.Mahs });
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvkhunggia.thongtin", "Approve"))
                {
                    var model = _db.GiaSpDvKhungGia.FirstOrDefault(t => t.Mahs == mahs_chuyen);
                    model.Updated_at = DateTime.Now;
                    model.Trangthai = trangthai_complete;

                    _db.GiaSpDvKhungGia.Update(model);
                    _db.SaveChanges();
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), trangthai_complete);


                    return RedirectToAction("Index", "GiaSpDvKhungGia", new { model.Madv });

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

        [Route("DinhGiaSpDvKhungGia/Print")]
        [HttpGet]
        public IActionResult Print(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvkhunggia.thongtin", "Index"))
                {
                    var model = _db.GiaSpDvKhungGia.FirstOrDefault(t => t.Mahs == Mahs);

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

                    var modelct = _db.GiaSpDvKhungGiaCt.Where(t => t.Mahs == model.Mahs);
                    if (modelct != null)
                    {
                        hoso_dg.GiaSpDvKhungGiaCt = modelct.ToList();
                    }

                    /*var model = GetThongTinKk(Mahs);*/

                    ViewData["Title"] = "In định giá đât cụ thể";
                    ViewData["MenuLv1"] = "menu_spdvkhunggia";
                    ViewData["MenuLv2"] = "menu_spdvkhunggia_thongtin";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvKhungGia/Print.cshtml", hoso_dg);

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

        [Route("GiaSpDvKhungGia/TimKiem")]
        [HttpGet]
        public IActionResult Search()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvkhunggia.timkiem", "Index"))
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
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá sản phẩm dịch vụ khung giá";
                    ViewData["MenuLv1"] = "menu_spdvkhunggia";
                    ViewData["MenuLv2"] = "menu_spdvkhunggia_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvKhungGia/TimKiem/Index.cshtml");

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

        [Route("GiaSpDvKhungGia/TimKiem/KetQua")]
        [HttpPost]
        public IActionResult Result(string madv, string tenhanghoa, DateTime ngaynhap_tu, DateTime ngaynhap_den, double beginPrice, double endPrice)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvkhunggia.timkiem", "Index"))
                {
                    var model = (from giaspdvkhunggiact in _db.GiaSpDvKhungGiaCt
                                 join giaspdvkhunggia in _db.GiaSpDvKhungGia on giaspdvkhunggiact.Mahs equals giaspdvkhunggia.Mahs
                                 join donvi in _db.DsDonVi on giaspdvkhunggia.Madv equals donvi.MaDv
                                 select new GiaSpDvKhungGiaCt
                                 {
                                     Id = giaspdvkhunggiact.Id,
                                     Dvt = giaspdvkhunggiact.Dvt,
                                     Mahs = giaspdvkhunggiact.Mahs,
                                     Madv = giaspdvkhunggia.Madv,
                                     Thoidiem = giaspdvkhunggia.Thoidiem,
                                     Tendv = donvi.TenDv,
                                     Soqd = giaspdvkhunggia.Soqd,
                                     Giatoida = giaspdvkhunggiact.Giatoida,
                                     Giatoithieu = giaspdvkhunggiact.Giatoithieu,
                                     Ttqd = giaspdvkhunggia.Ttqd,
                                     Mota = giaspdvkhunggiact.Mota,
                                     Tenspdv = giaspdvkhunggiact.Tenspdv,
                                     Madiaban = giaspdvkhunggia.Madiaban,
                                     Trangthai = giaspdvkhunggia.Trangthai
                                 });
                    //List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    //model = model.Where(t => list_trangthai.Contains(t.Trangthai));
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
                        model = model.Where(t => t.Giatoithieu >= beginPrice || t.Giatoida >= beginPrice);
                    }
                    if (endPrice > 0)
                    {
                        model = model.Where(t => t.Giatoida <= endPrice || t.Giatoithieu <= endPrice);
                    }

                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá sản phẩm dịch vụ khung giá";
                    ViewData["MenuLv1"] = "menu_spdvkhunggia";
                    ViewData["MenuLv2"] = "menu_spdvkhunggia_tk";
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvKhungGia/TimKiem/Result.cshtml", model);
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
