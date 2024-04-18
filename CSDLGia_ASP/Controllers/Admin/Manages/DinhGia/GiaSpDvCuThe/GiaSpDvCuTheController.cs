
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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaSpDvCuThe
{
    public class GiaSpDvCuTheController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IDsDonviService _dsDonviService;
        private readonly ITrangThaiHoSoService _trangThaiHoSoService;

        public GiaSpDvCuTheController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment, IDsDonviService dsDonviService, ITrangThaiHoSoService trangThaiHoSoService)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
            _dsDonviService = dsDonviService;
            _trangThaiHoSoService = trangThaiHoSoService;
        }

        [Route("GiaSpDvCuThe")]
        [HttpGet]
        public IActionResult Index(int Nam, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.thongtin", "Index"))
                {
                    Madv = string.IsNullOrEmpty(Madv) ? Helpers.GetSsAdmin(HttpContext.Session, "Madv") : Madv;
                    var model_donvi = _dsDonviService.GetListDonvi(Helpers.GetSsAdmin(HttpContext.Session, "Madv"));
                    List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();

                    IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCuThe> model = _db.GiaSpDvCuThe;
                    if (Madv != "all")
                    {
                        model = model.Where(t => t.Madv == Madv);
                    }

                    if (Nam != 0)
                    {
                        model = model.Where(t => t.Thoidiem.Year == Nam);
                    }

                    ViewData["DsDonvi"] = model_donvi;
                    ViewData["NhomTn"] = _db.GiaSpDvCuTheNhom.ToList();
                    ViewData["Nam"] = Nam;
                    ViewData["Madv"] = Madv;
                    ViewData["Title"] = "Thông tin giá sản phẩm dịch vụ cụ thể";
                    ViewData["MenuLv1"] = "menu_spdvcuthe";
                    ViewData["MenuLv2"] = "menu_spdvcuthe_thongtin";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCuThe/Index.cshtml", model);


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

        [Route("GiaSpDvCuThe/Create")]
        [HttpGet]
        public IActionResult Create(string Manhom, string MadvBc, string PhanLoaiHoSo)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.thongtin", "Create"))
                {
                    //Xoa dữ liệu thừa
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
                        _db.SaveChanges();
                    }

                    // hồ sơ mà chỉ đến bước hoàn thành mà quay lại thì sẽ có trạng thái CXD --> cần xóa
                    var check = _db.GiaSpDvCuTheCt.Where(t => t.Trangthai == "CXD");
                    if (check != null)
                    {
                        _db.GiaSpDvCuTheCt.RemoveRange(check);
                        _db.SaveChanges();
                    }

                    // Thông tin của bộ hồ sơ
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCuThe
                    {
                        Mahs = MadvBc + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Madv = MadvBc,
                        Manhom = Manhom,
                        Thoidiem = DateTime.Now,
                        PhanLoaiHoSo = PhanLoaiHoSo,
                    };

                    var danhmuc = _db.GiaSpDvCuTheDm.ToList(); // lấy dữ liệu trong bảng GiaSpDvCuTheDm


                    // Khi bấm đồng ý trong moda thì add dữ liệu GiaSpDvCuTheDm -> bản GiaSpDvCuTheCt

                    if (Manhom != "all")
                    {
                        danhmuc = danhmuc.Where(t => t.Manhom == Manhom).ToList();
                    }
                    else
                    {
                        danhmuc = danhmuc.ToList();
                    }


                    var chitiet = new List<GiaSpDvCuTheCt>();


                    foreach (var item in danhmuc)
                    {
                        chitiet.Add(new GiaSpDvCuTheCt()
                        {
                            Mahs = model.Mahs,
                            Dvt = item.Dvt,
                            Tt = item.Tt,
                            Manhom = item.Manhom,
                            Maspdv = item.Maspdv,
                            TenSpDv = item.Tenspdv,
                            Sapxep = item.Sapxep,
                            Style = item.Style,
                            Trangthai = "CXD",
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        });
                    }
                    _db.GiaSpDvCuTheCt.AddRange(chitiet);
                    _db.SaveChanges();


                    //// Xử lý phần Forech theo mã nhóm khi chọn
                    //var groupmanhom1 = _db.GiaSpDvCuTheNhom.Select(item => item.Manhom).ToList();
                    //var groupmanhom2 = _db.GiaSpDvCuTheNhom.Where(item => item.Manhom == Manhom).Select(item => item.Manhom).ToList();
                    //List<string> groupmanhom;

                    //if (Manhom != "all")
                    //{
                    //    groupmanhom = groupmanhom2;
                    //}
                    //else
                    //{
                    //    groupmanhom = groupmanhom1;
                    //}
                    //ViewData["GroupMaNhom"] = groupmanhom;
                    //ViewData["GiaSpDvCuTheNhom"] = _db.GiaSpDvCuTheNhom.ToList();
                    //ViewData["GiaSpDvCuTheDm"] = _db.GiaSpDvCuTheDm.ToList();
                    //// End xử lý phần Forech theo mã nhóm khi chọn

                    model.GiaSpDvCuTheCt = chitiet.Where(t => t.Mahs == model.Mahs).ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "T");
                    ViewData["Manhom"] = Manhom;
                    ViewData["Madv"] = MadvBc;
                    ViewData["Mahs"] = model.Mahs;

                    ViewData["Title"] = "Bảng giá sản phẩm dịch vụ cụ thể";
                    ViewData["MenuLv1"] = "menu_spdvcuthe";
                    ViewData["MenuLv2"] = "menu_spdvcuthe_thongtin";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCuThe/Create.cshtml", model);

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

        [Route("GiaSpDvCuThe/Store")]
        [HttpPost]
        public IActionResult Store(CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCuThe request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.thongtin", "Create"))
                {                   
                    // 2024.03.15 Gộp Update và phần nhận cho Excel
                    if (_db.GiaSpDvCuThe.Where(x => x.Mahs == request.Mahs).Any())
                    {
                        //Xử lý hồ sơ
                        var modelExcel = _db.GiaSpDvCuThe.FirstOrDefault(t => t.Mahs == request.Mahs);
                        modelExcel.Madiaban = request.Madiaban;
                        modelExcel.Soqd = request.Soqd;
                        modelExcel.Thoidiem = request.Thoidiem;
                        modelExcel.Thongtin = request.Thongtin;
                        modelExcel.GhiChu = request.GhiChu;
                        modelExcel.PhanLoaiHoSo = request.PhanLoaiHoSo;
                        modelExcel.Updated_at = DateTime.Now;
                        _db.GiaSpDvCuThe.Update(modelExcel);

                        // Xử lý phần lịch sử hồ sơ 
                        _trangThaiHoSoService.LogHoSo(modelExcel.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Cập nhật");
                        // End Xử lý phần lịch sử hồ sơ 
                        return RedirectToAction("Index", "GiaSpDvCuThe", new { request.Madv });
                    }

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCuThe
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Manhom = request.Manhom,
                        PhanLoaiHoSo = request.PhanLoaiHoSo,
                        Madiaban = request.Madiaban,
                        Soqd = request.Soqd,
                        GhiChu = request.GhiChu,
                        Thoidiem = request.Thoidiem,
                        Thongtin = request.Thongtin,
                        Trangthai = "CC",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaSpDvCuThe.Add(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaSpDvCuTheCt.Where(t => t.Mahs == request.Mahs);
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
                    _db.GiaSpDvCuTheCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    // Xử lý phần lịch sử hồ sơ 
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Thêm mới");

                    //Kết thúc Xử lý phần lịch sử hồ sơ 

                    return RedirectToAction("Index", "GiaSpDvCuThe", new { request.Madv });
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

        [Route("GiaSpDvCuThe/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.thongtin", "Delete"))
                {
                    var model = _db.GiaSpDvCuThe.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaSpDvCuThe.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.GiaSpDvCuTheCt.Where(t => t.Mahs == model.Mahs);
                    _db.GiaSpDvCuTheCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaSpDvCuThe", new { model.Madv });
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

        [Route("GiaSpDvCuThe/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.thongtin", "Edit"))
                {
                    var model = _db.GiaSpDvCuThe.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_ct = _db.GiaSpDvCuTheCt.Where(t => t.Mahs == model.Mahs);
                    model.GiaSpDvCuTheCt = model_ct.ToList();
                    var model_file = _db.ThongTinGiayTo.Where(t => t.Mahs == model.Mahs);
                    model.ThongTinGiayTo = model_file.ToList();
                    ViewData["Madv"] = model.Madv;
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();

                    ViewData["Madv"] = model.Madv;
                    ViewData["Ipf1"] = model.Ipf1;
                    ViewData["Title"] = "Bảng giá tính sản phẩm dịch vụ cụ thể";
                    ViewData["MenuLv1"] = "menu_spdvcuthe";
                    ViewData["MenuLv2"] = "menu_spdvcuthe_thongtin";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCuThe/Create.cshtml", model);

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

        //[Route("DinhGiaSpDvCuThe/Show")]
        //[HttpGet]
        //public IActionResult Show(string Mahs)
        //{
        //    if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
        //    {
        //        if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.thongtin", "Edit"))
        //        {
        //            var model = _db.GiaSpDvCuThe.FirstOrDefault(t => t.Mahs == Mahs);
        //            if (model.CodeExcel != "")
        //            {
        //                ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
        //                ViewData["DsDonVi"] = _db.DsDonVi.ToList();
        //                ViewData["Title"] = "Bảng giá tính sản phẩm dịch vụ cụ thể";                      
        //                return View("Views/Admin/Manages/DinhGia/GiaSpDvCuThe/ShowExcel.cshtml", model);
        //            }
        //            var model_new = new VMDinhGiaSpDvCuThe
        //            {
        //                Madv = model.Madv,
        //                Mahs = model.Mahs,
        //                Madiaban = model.Madiaban,
        //                Soqd = model.Soqd,
        //                Phanloaidv = model.Phanloai,
        //                Thoidiem = model.Thoidiem,
        //                Thongtin = model.Thongtin,
        //                Ghichu = model.Ghichu
        //            };

        //            var model_ct = _db.GiaSpDvCuTheCt.Where(t => t.Mahs == model_new.Mahs);

        //            model_new.GiaSpDvCuTheCt = model_ct.ToList();

        //            ViewData["Madv"] = model.Madv;
        //            ViewData["Mahs"] = model.Mahs;
        //            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
        //            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
        //            ViewData["GiaSpDvCuTheDm"] = _db.GiaSpDvCuTheDm.ToList();
        //            ViewData["PhanLoaiDichVu"] = _db.GiaSpDvCuTheCt.ToList();
        //            ViewData["Title"] = "Thông tin chi tiết sản phẩm dịch vụ cụ thể";
        //            ViewData["MenuLv1"] = "menu_spdvcuthe";
        //            ViewData["MenuLv2"] = "menu_spdvcuthe_thongtin";
        //            return View("Views/Admin/Manages/DinhGia/GiaSpDvCuThe/Show.cshtml", model_new);
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


        [Route("DinhGiaSpDvCuThe/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.timkiem", "Edit"))
                {
                    var model = _db.GiaSpDvCuThe.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_ct = _db.GiaSpDvCuTheCt.Where(t => t.Mahs == model.Mahs);
                    model.GiaSpDvCuTheCt = model_ct.ToList();
                    ViewData["Madv"] = model.Madv;
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();


                    model.GiaSpDvCuTheCt = model_ct.ToList();

                    ViewData["Ipf1"] = model.Ipf1;
                    ViewData["Title"] = "Bảng giá tính sản phẩm dịch vụ cụ thể";
                    ViewData["MenuLv1"] = "menu_spdvcuthe";
                    ViewData["MenuLv2"] = "menu_spdvcuthe_thongtin";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCuThe/Show.cshtml", model);

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

        [Route("DinhGiaSpDvCuThe/Print")]
        [HttpGet]
        public IActionResult Print(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.timkiem", "Index"))
                {
                    var model = _db.GiaSpDvCuThe.FirstOrDefault(t => t.Mahs == Mahs);

                    var hoso_dg = new VMDinhGiaPrint
                    {
                        Id = model.Id,

                        Phanloaidv = model.Phanloai,
                        Mahs = model.Mahs,
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Ghichu = model.GhiChu,
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

                    var modelct = _db.GiaSpDvCuTheCt.Where(t => t.Mahs == model.Mahs);
                    if (modelct != null)
                    {
                        hoso_dg.GiaSpDvCuTheCt = modelct.ToList();
                    }

                    /*var model = GetThongTinKk(Mahs);*/


                    ViewData["Title"] = "In định giá đât cụ thể";
                    ViewData["MenuLv1"] = "menu_spdvcuthe";
                    ViewData["MenuLv2"] = "menu_spdvcuthe_thongtin";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCuThe/Print.cshtml", hoso_dg);

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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.thongtin", "Approve"))
                {
                    var model = _db.GiaSpDvCuThe.FirstOrDefault(t => t.Mahs == mahs_chuyen);

                    model.Updated_at = DateTime.Now;
                    model.Trangthai = trangthai_complete;

                    _db.GiaSpDvCuThe.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaSpDvCuThe", new { model.Madv });

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

        [Route("GiaSpDvCuThe/TimKiem")]
        [HttpGet]
        public IActionResult Search()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.timkiem", "Index"))
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
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá sản phẩm dịch vụ cụ thể";
                    ViewData["MenuLv1"] = "menu_spdvcuthe";
                    ViewData["MenuLv2"] = "menu_spdvcuthe_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCuThe/TimKiem/Index.cshtml");

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

        [Route("GiaSpDvCuThe/TimKiem/KetQua")]
        [HttpPost]
        public IActionResult Result(string madv, string tenhanghoa, DateTime? ngaynhap_tu, DateTime? ngaynhap_den,
            double beginPrice, double endPrice, string MaDiaBan, string PhanLoaiHoSo)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.timkiem", "Index"))
                {

                    var model = (from giaspdvcuthect in _db.GiaSpDvCuTheCt
                                 join giaspdvcuthe in _db.GiaSpDvCuThe on giaspdvcuthect.Mahs equals giaspdvcuthe.Mahs
                                 join donvi in _db.DsDonVi on giaspdvcuthe.Madv equals donvi.MaDv
                                 select new GiaSpDvCuTheCt
                                 {
                                     Id = giaspdvcuthect.Id,
                                     Dvt = giaspdvcuthect.Dvt,
                                     Mahs = giaspdvcuthect.Mahs,
                                     Madv = giaspdvcuthe.Madv,
                                     Thoidiem = giaspdvcuthe.Thoidiem,
                                     Tendv = donvi.TenDv,
                                     Soqd = giaspdvcuthe.Soqd,
                                     TenSpDv = giaspdvcuthect.TenSpDv,
                                     Mucgia1 = giaspdvcuthect.Mucgia1,
                                     Mucgia2 = giaspdvcuthect.Mucgia2,
                                     Mucgia3 = giaspdvcuthect.Mucgia3,
                                     Mucgia4 = giaspdvcuthect.Mucgia4,
                                     Maspdv = giaspdvcuthect.Maspdv,
                                     MaDiaBan = giaspdvcuthe.Madiaban,
                                     PhanLoaiHoSo = giaspdvcuthe.PhanLoaiHoSo,
                                     Ttqd = giaspdvcuthe.Ttqd,
                                     GhiChu = giaspdvcuthe.GhiChu,
                                     Trangthai = giaspdvcuthe.Trangthai
                                 });

                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model = model.Where(t => list_trangthai.Contains(t.Trangthai));
                    //Đơn vị nhập liệu
                    if (madv != "all")
                    {
                        model = model.Where(t => t.Madv == madv);
                    }
                    //Tên sản phẩm dịch vụ
                    if (!string.IsNullOrEmpty(tenhanghoa))
                    {
                        model = model.Where(t => t.TenSpDv.ToLower().Contains(tenhanghoa.ToLower()));
                    }
                    //Phân loại hồ sơ
                    if (PhanLoaiHoSo != "all")
                    {
                        model = model.Where(t => t.PhanLoaiHoSo == PhanLoaiHoSo);
                    }
                    //Địa bàn áp dụng
                    if (MaDiaBan != "all")
                    {
                        model = model.Where(t => t.MaDiaBan == MaDiaBan);
                    }
                    //Chưa làm mức giá và thời gian

                    //
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá sản phẩm dịch vụ cụ thể";
                    ViewData["MenuLv1"] = "menu_spdvcuthe";
                    ViewData["MenuLv2"] = "menu_spdvcuthe_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCuThe/TimKiem/Result.cshtml", model);
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
