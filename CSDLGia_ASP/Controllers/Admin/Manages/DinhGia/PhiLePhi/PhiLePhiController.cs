using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.PhiLePhi
{
    public class PhiLePhiController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PhiLePhiController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("PhiLePhi/DanhSach")]
        [HttpGet]
        public IActionResult Index(string Nam, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.lephi.thongtin", "Index"))
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

                        var model = _db.PhiLePhi.Where(t => t.Madv == Madv).ToList();

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
                        //Gán các thông tin ở bảng khác
                        foreach (var item in model)
                        {
                            if (item.Macqcq != null)
                            {
                                item.Tencqcq = dsdonvi.Where(x => x.MaDv == item.Macqcq).FirstOrDefault().TenDv;
                            }
                            else
                            {
                                item.Tencqcq = "";
                            }

                        }
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                        ViewData["Nam"] = Nam;
                        ViewData["Madv"] = Madv;
                        ViewData["Title"] = "Thông tin giá phí, lệ phí";
                        ViewData["MenuLv1"] = "menu_giakhac";
                        ViewData["MenuLv2"] = "menu_plp";
                        ViewData["MenuLv3"] = "menu_plp_tt";
                        return View("Views/Admin/Manages/DinhGia/PhiLePhi/DanhSach/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Title"] = "Thông tin giá phí, lệ phí";
                        ViewData["Messages"] = "Hệ thống chưa có định giá phí, lệ phí.";
                        ViewData["MenuLv1"] = "menu_giakhac";
                        ViewData["MenuLv2"] = "menu_plp";
                        ViewData["MenuLv3"] = "menu_plp_tt";
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


        [Route("PhiLePhi/Create")]
        [HttpGet]
        public IActionResult Create(string maDV)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.lephi.thongtin", "Create"))
                {

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.PhiLePhi
                    {
                        Mahs = maDV + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Madv = maDV,
                        Thoidiem = DateTime.Now,
                        PhanLoaiHoSo = "HOSOCHITIET",
                    };

                    ViewData["Mahs"] = model.Mahs;
                    ViewData["Title"] = "Giá phí, lệ phí";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_plp";
                    ViewData["MenuLv3"] = "menu_plp_tt";
                    return View("Views/Admin/Manages/DinhGia/PhiLePhi/DanhSach/Create.cshtml", model);

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

        public IActionResult NhanExcel(string maDV)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.lephi.thongtin", "Create"))
                {

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.PhiLePhi
                    {
                        Mahs = maDV + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Madv = maDV,
                        Thoidiem = DateTime.Now,
                        PhanLoaiHoSo = "HOSONHANEXCEL",
                    };


                    ViewData["Mahs"] = model.Mahs;
                    ViewData["Title"] = "Giá phí, lệ phí";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_plp";
                    ViewData["MenuLv3"] = "menu_plp_tt";
                    ViewData["codeExcel"] = model.CodeExcel;//Gán ra view để dùng chung
                    return View("Views/Admin/Manages/DinhGia/PhiLePhi/DanhSach/NhanExcel.cshtml", model);

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

        [Route("PhiLePhi/Store")]
        [HttpPost]
        public async Task<IActionResult> Store(CSDLGia_ASP.Models.Manages.DinhGia.PhiLePhi request, IFormFile Ipf1upload)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.lephi.thongtin", "Create"))
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

                    var mPhiLePhi = new CSDLGia_ASP.Models.Manages.DinhGia.PhiLePhi
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Thoidiem = request.Thoidiem,
                        Soqd = request.Soqd,
                        Ttqd = request.Ttqd,
                        Ghichu = request.Ghichu,
                        PhanLoaiHoSo = request.PhanLoaiHoSo,
                        CodeExcel = request.CodeExcel,
                        Trangthai = "CHT",
                        Congbo = "CHUACONGBO",
                        Ipf1 = request.Ipf1,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };

                    _db.PhiLePhi.Add(mPhiLePhi);

                    _db.SaveChanges();

                    return RedirectToAction("Index", "PhiLePhi", new { request.Madv });
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

        [Route("PhiLePhi/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.lephi.thongtin", "Edit"))
                {
                    var model = _db.PhiLePhi.FirstOrDefault(t => t.Mahs == Mahs);

                    var model_ct = _db.PhiLePhiCt.Where(t => t.Mahs == model.Mahs);

                    model.PhiLePhiCt = _db.PhiLePhiCt.Where(t => t.Mahs == model.Mahs).ToList();

                    /*ViewData["Madv"] = model.Madv;
                    ViewData["Ipf1"] = model.Ipf1;*/
                    ViewData["Title"] = "Bảng giá phí, lệ phí";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_plp";
                    ViewData["MenuLv3"] = "menu_plp_tt";
                    if (model.PhanLoaiHoSo == "HOSONHANEXCEL")
                    {
                        //model.CodeExcel = model.CodeExcel.Replace("[", "");
                        //model.CodeExcel = model.CodeExcel.Replace("]", "");
                        return View("Views/Admin/Manages/DinhGia/PhiLePhi/DanhSach/NhanExcel.cshtml", model);
                    }
                    return View("Views/Admin/Manages/DinhGia/PhiLePhi/DanhSach/Create.cshtml", model);

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

        [Route("PhiLePhi/Update")]
        [HttpPost]
        public async Task<IActionResult> Update(CSDLGia_ASP.Models.Manages.DinhGia.PhiLePhi request, IFormFile Ipf1upload)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.lephi.thongtin", "Edit"))
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

                    var model = _db.PhiLePhi.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Ttqd = request.Ttqd;
                    model.Thongtin = request.Thongtin;
                    model.Ghichu = request.Ghichu;
                    model.Ipf1 = request.Ipf1;
                    model.Updated_at = DateTime.Now;

                    _db.PhiLePhi.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "PhiLePhi", new { request.Madv });
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

        [Route("PhiLePhi/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.lephi.thongtin", "Delete"))
                {
                    var model = _db.PhiLePhi.FirstOrDefault(t => t.Id == id_delete);
                    _db.PhiLePhi.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.PhiLePhiCt.Where(t => t.Mahs == model.Mahs);
                    _db.PhiLePhiCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "PhiLePhi", new { model.Madv });
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

        [Route("PhiLePhi/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.lephi.thongtin", "Index"))
                {
                    var model = _db.PhiLePhi.FirstOrDefault(t => t.Mahs == Mahs);
                    model.PhiLePhiCt = _db.PhiLePhiCt.Where(t => t.Mahs == model.Mahs).ToList();

                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["Title"] = "Bảng giá phí, lệ phí";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_plp";
                    ViewData["MenuLv3"] = "menu_plp_tt";
                    if (model.PhanLoaiHoSo == "HOSONHANEXCEL")
                    {
                        return View("Views/Admin/Manages/DinhGia/PhiLePhi/DanhSach/ShowExcel.cshtml", model);
                    }
                    return View("Views/Admin/Manages/DinhGia/PhiLePhi/DanhSach/Show.cshtml", model);

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

        [Route("PhiLePhi/Complete")]
        [HttpPost]
        public IActionResult Complete(string mahs_chuyen, string macqcq_chuyen)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.lephi.thongtin", "Index"))
                {
                    var model = _db.PhiLePhi.FirstOrDefault(t => t.Mahs == mahs_chuyen);

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
                    _db.PhiLePhi.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "PhiLePhi", new { model.Madv });

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

        [Route("PhiLePhi/TimKiem")]
        [HttpGet]
        public IActionResult Search()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.lephi.thongtin", "Index"))
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
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá phí, lệ phí";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_plp";
                    ViewData["MenuLv3"] = "menu_plp_tt";
                    return View("Views/Admin/Manages/DinhGia/PhiLePhi/TimKiem/Index.cshtml");

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

        [Route("PhiLePhi/TimKiem/KetQua")]
        [HttpPost]
        public IActionResult Result(string madv, string manhom, DateTime ngaynhap_tu, DateTime ngaynhap_den, string gia_tu, string gia_den)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.lephi.thongtin", "Index"))
                {
                    var model = (from PhiLePhiCt in _db.PhiLePhiCt
                                 join PhiLePhi in _db.PhiLePhi on PhiLePhiCt.Mahs equals PhiLePhi.Mahs
                                 join donvi in _db.DsDonVi on PhiLePhi.Madv equals donvi.MaDv
                                 /*join nhomtn in _db.GiaXayDungMoiNhom on giathuetn.Manhom equals nhomtn.Manhom*/
                                 select new PhiLePhiCt
                                 {
                                     Id = PhiLePhiCt.Id,
                                     Mahs = PhiLePhiCt.Mahs,
                                     Madv = PhiLePhi.Madv,
                                     Tendv = donvi.TenDv,
                                     MaSo = PhiLePhiCt.MaSo,
                                     MaSoGoc = PhiLePhiCt.MaSoGoc,
                                     Dongia = PhiLePhiCt.Dongia,
                                     HienThi = PhiLePhiCt.HienThi,
                                     CapDo = PhiLePhiCt.CapDo,
                                     Thoidiem = PhiLePhi.Thoidiem,
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


                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá phí, lệ phí";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_plp";
                    ViewData["MenuLv3"] = "menu_plp_tt";
                    return View("Views/Admin/Manages/DinhGia/PhiLePhi/TimKiem/Result.cshtml", model);

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

        [Route("PhiLePhiCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.PhiLePhiCt.FirstOrDefault(t => t.Id == Id);
            // Trả về JSON cho JavaScript
            return Json(model);
        }

        [Route("PhiLePhiCt/Update")]
        [HttpPost]
        public JsonResult UpdateCt(CSDLGia_ASP.Models.Manages.DinhGia.PhiLePhiCt request)
        {

            if (request.CapDo <= 0 || request.CapDo > 4)
            {
                request.CapDo = 1;
            }
            //Kiểm tra mã trước khi nhập
            if (request.CapDo > 1)
            {
                //Kiểm tra mã gốc xem tồn tại ko
                var chk = _db.PhiLePhiCt.Where(x => x.Mahs == request.Mahs && x.MaSo == request.MaSoGoc);
                if (!chk.Any())
                {
                    return Json(new { status = "error", message = "Mã gốc không tồn tại. Bạn hãy kiểm tra lại !!!" });
                }
            }

            //Kiểm tra mã trước khi nhập
            if (request.CapDo == 1)
            {
                //Kiểm tra mã cấp 1 không nhập mã gốc
                var chk = _db.PhiLePhiCt.Where(x => x.Mahs == request.Mahs && x.MaSo == request.MaSoGoc);
                if (request.MaSoGoc != null && request.MaSoGoc != "")
                {
                    return Json(new { status = "error", message = "Mã số cấp 1 không nhập mã gốc. Bạn hãy kiểm tra lại !!!" });
                }
            }

            //Kiêm tra id = =-1 =>thêm mới
            if (request.Id == -1)
            {
                var model = new CSDLGia_ASP.Models.Manages.DinhGia.PhiLePhiCt
                {
                    Mahs = request.Mahs,
                    MaSo = request.MaSo,
                    MaSoGoc = request.MaSoGoc,
                    HienThi = request.HienThi,//HIện thị dữ liệu ra màn hình
                    STT = request.STT,//Số thứ tự theo mã gốc
                    CapDo = request.CapDo,//Bắt đầu từ 1
                    ChiTieu = request.ChiTieu,
                    Dvt = request.Dvt,
                    Dongia = request.Dongia,
                };

                _db.PhiLePhiCt.Add(model);
                _db.SaveChanges();
            }
            else
            {
                var model = _db.PhiLePhiCt.FirstOrDefault(t => t.Id == request.Id);

                model.MaSo = request.MaSo;
                model.MaSoGoc = request.MaSoGoc;
                model.HienThi = request.HienThi;//HIện thị dữ liệu ra màn hình
                model.STT = request.STT;//Số thứ tự theo mã gốc
                model.CapDo = request.CapDo;//Bắt đầu từ 1
                model.ChiTieu = request.ChiTieu;
                model.Dvt = request.Dvt;
                model.Dongia = request.Dongia;
                _db.PhiLePhiCt.Update(model);
                _db.SaveChanges();
            }



            string result = GetDataCt(request.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        [Route("PhiLePhiCt/Delete")]
        [HttpPost]
        public JsonResult DeleteCt(int Id)
        {
            var model = _db.PhiLePhiCt.FirstOrDefault(t => t.Id == Id);
            _db.PhiLePhiCt.Remove(model);
            _db.SaveChanges();
            var result = GetDataCt(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        public string GetDataCt(string Mahs)
        {
            var model = _db.PhiLePhiCt.Where(t => t.Mahs == Mahs).ToList();

            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover table-responsive' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th width='4%'>#</th>";
            result += "<th>Mã số</th>";
            result += "<th>Mã số gốc</th>";
            result += "<th>Hiển thị<br>báo cáo</th>";
            result += "<th>Cấp độ </th>";
            result += "<th>Nội dung </th>";
            result += "<th>Đơn vị<br/>tính</th>";
            result += "<th>Mức giá </th>";
            result += "<th width = '8%'>Thao tác </th>";


            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";

            foreach (var item in model.Where(x => x.CapDo == 1).OrderBy(x => x.STT))
            {
                result += "<tr>";
                result += "<td class='text-center'>" + record++ + "</td>";
                result += "<td class='active' style='font-weight:bold'>" + item.MaSo + "</td>";
                result += "<td>" + @item.MaSoGoc + "</td>";
                result += "<td>" + @item.HienThi + "</td>";
                result += "<td>" + @item.CapDo + "</td>";
                result += "<td>" + @item.ChiTieu + "</td>";
                result += "<td>" + @item.Dvt + "</td>";
                result += "<td>" + @item.Dongia + "</td>";
                result += "<td>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Nhập giá'";
                result += " data-target='#Edit_Modal' data-toggle='modal' onclick='SetEdit(" + item.Id + ")'>";
                result += "<i class='icon-lg la la-edit text-primary'></i>";
                result += "</button>";
                result += "<button type='button' data-target='#Delete_Modal' data-toggle='modal'";
                result += " onclick='GetDelete(" + item.Id + ")' class='btn btn-sm btn-clean btn-icon' title='Xóa'>";
                result += "<i class='icon-lg la la-trash text-danger'></i>";
                result += "</button>";
                result += "</td>";
                result += "</tr>";
                //Vòng 2
                foreach (var item1 in model.Where(x => x.MaSoGoc == item.MaSo).OrderBy(x => x.STT))
                {
                    result += "<tr>";
                    result += "<td class='text-center'>" + record++ + "</td>";
                    result += "<td class='active' style='font-weight:bold'>" + item1.MaSo + "</td>";
                    result += "<td>" + item1.MaSoGoc + "</td>";
                    result += "<td>" + item1.HienThi + "</td>";
                    result += "<td>" + item1.CapDo + "</td>";
                    result += "<td>" + item1.ChiTieu + "</td>";
                    result += "<td>" + item1.Dvt + "</td>";
                    result += "<td>" + item1.Dongia + "</td>";
                    result += "<td>";
                    result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Nhập giá'";
                    result += " data-target='#Edit_Modal' data-toggle='modal' onclick='SetEdit(" + item1.Id + ")'>";
                    result += "<i class='icon-lg la la-edit text-primary'></i>";
                    result += "</button>";
                    result += "<button type='button' data-target='#Delete_Modal' data-toggle='modal'";
                    result += " onclick='GetDelete(" + item1.Id + ")' class='btn btn-sm btn-clean btn-icon' title='Xóa'>";
                    result += "<i class='icon-lg la la-trash text-danger'></i>";
                    result += "</button>";
                    result += "</td>";
                    result += "</tr>";
                    //Vòng 3
                    foreach (var item2 in model.Where(x => x.MaSoGoc == item1.MaSo).OrderBy(x => x.STT))
                    {
                        result += "<tr>";
                        result += "<td class='text-center'>" + record++ + "</td>";
                        result += "<td class='active' style='font-weight:bold'>" + item2.MaSo + "</td>";
                        result += "<td>" + item2.MaSoGoc + "</td>";
                        result += "<td>" + item2.HienThi + "</td>";
                        result += "<td>" + item2.CapDo + "</td>";
                        result += "<td>" + item2.ChiTieu + "</td>";
                        result += "<td>" + item2.Dvt + "</td>";
                        result += "<td>" + item2.Dongia + "</td>";
                        result += "<td>";
                        result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Nhập giá'";
                        result += " data-target='#Edit_Modal' data-toggle='modal' onclick='SetEdit(" + item2.Id + ")'>";
                        result += "<i class='icon-lg la la-edit text-primary'></i>";
                        result += "</button>";
                        result += "<button type='button' data-target='#Delete_Modal' data-toggle='modal'";
                        result += " onclick='GetDelete(" + item2.Id + ")' class='btn btn-sm btn-clean btn-icon' title='Xóa'>";
                        result += "<i class='icon-lg la la-trash text-danger'></i>";
                        result += "</button>";
                        result += "</td>";
                        result += "</tr>";
                        //Vòng 4
                        foreach (var item3 in model.Where(x => x.MaSoGoc == item2.MaSo).OrderBy(x => x.STT))
                        {
                            result += "<tr>";
                            result += "<td class='text-center'>" + record++ + "</td>";
                            result += "<td class='active' style='font-weight:bold'>" + item3.MaSo + "</td>";
                            result += "<td>" + item3.MaSoGoc + "</td>";
                            result += "<td>" + item3.HienThi + "</td>";
                            result += "<td>" + item3.CapDo + "</td>";
                            result += "<td>" + item3.ChiTieu + "</td>";
                            result += "<td>" + item3.Dvt + "</td>";
                            result += "<td>" + item3.Dongia + "</td>";
                            result += "<td>";
                            result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Nhập giá'";
                            result += " data-target='#Edit_Modal' data-toggle='modal' onclick='SetEdit(" + item3.Id + ")'>";
                            result += "<i class='icon-lg la la-edit text-primary'></i>";
                            result += "</button>";
                            result += "<button type='button' data-target='#Delete_Modal' data-toggle='modal'";
                            result += " onclick='GetDelete(" + item3.Id + ")' class='btn btn-sm btn-clean btn-icon' title='Xóa'>";
                            result += "<i class='icon-lg la la-trash text-danger'></i>";
                            result += "</button>";
                            result += "</td>";
                            result += "</tr>";
                        }
                    }
                }
            }
            result += "</tbody>";
            result += "</table>";
            result += "</div>";

            return result;

        }
    }
}
