
using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
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

        public GiaHhDvCnController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("GiaHhDvCn")]
        [HttpGet]
        public IActionResult Index(string Nam, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.hhdvcn.thongtin", "Index"))
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

                        var model = _db.GiaHhDvCn.Where(t => t.Madv == Madv).ToList();

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
                        ViewData["NhomTn"] = _db.GiaHhDvCnDm.ToList();
                        ViewData["Nam"] = Nam;
                        ViewData["Madv"] = Madv;
                        ViewData["Title"] = "Thông tin giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành";
                        ViewData["MenuLv1"] = "menu_giakhac";
                        ViewData["MenuLv2"] = "menu_hhdvcn";
                        ViewData["MenuLv3"] = "menu_hhdvcn_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaHhDvCn/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Title"] = "Thông tin giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành";
                        ViewData["Messages"] = "Hệ thống chưa có định giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành.";
                        ViewData["MenuLv1"] = "menu_giakhac";
                        ViewData["MenuLv2"] = "menu_hhdvcn";
                        ViewData["MenuLv3"] = "menu_hhdvcn_tt";
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

        [Route("GiaHhDvCn/Create")]
        [HttpGet]

        public IActionResult Create(string MadvBc)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.hhdvcn.thongtin", "Create"))
                {

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvCn
                    {
                        Mahs = MadvBc + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Madv = MadvBc,
                    };

                    //model.GiaHhDvCnCt = _db.GiaHhDvCnCt.Where(t=>t.Mahs==model.Mahs).ToList();

                    ViewData["Spdv"] = _db.GiaHhDvCnDm.ToList();
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
        public async Task<IActionResult> Store(CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvCn request, IFormFile Ipf1upload, IFormFile Ipf2upload, IFormFile Ipf3upload, IFormFile Ipf4upload, IFormFile Ipf5upload)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.hhdvcn.thongtin", "Create"))
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

                    if (Ipf2upload != null && Ipf2upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf2upload.FileName);
                        string extension = Path.GetExtension(Ipf2upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf2upload.CopyToAsync(FileStream);
                        }
                        request.Ipf2 = filename;
                    }

                    if (Ipf3upload != null && Ipf3upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf3upload.FileName);
                        string extension = Path.GetExtension(Ipf3upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf3upload.CopyToAsync(FileStream);
                        }
                        request.Ipf3 = filename;
                    }

                    if (Ipf4upload != null && Ipf4upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf4upload.FileName);
                        string extension = Path.GetExtension(Ipf4upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf4upload.CopyToAsync(FileStream);
                        }
                        request.Ipf4 = filename;
                    }

                    if (Ipf5upload != null && Ipf5upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf5upload.FileName);
                        string extension = Path.GetExtension(Ipf5upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf5upload.CopyToAsync(FileStream);
                        }
                        request.Ipf5 = filename;
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
                        Trangthai = "CHT",
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

                    var model_ct = from ct in _db.GiaHhDvCnCt.Where(t => t.Mahs == Mahs)
                                   join dm in _db.GiaHhDvCnDm on ct.Maspdv equals dm.Maspdv
                                   select new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvCnCt
                                   {
                                       Id = ct.Id,
                                       Tenspdv = dm.Tenspdv,
                                       Dongia = ct.Dongia,
                                       Maspdv = ct.Maspdv,
                                       Trangthai = ct.Trangthai,
                                       Mota = ct.Mota,
                                       Mahs = ct.Mahs,
                                   };

                    model.GiaHhDvCnCt = model_ct.ToList();

                    ViewData["Spdv"] = _db.GiaHhDvCnDm.ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Madv"] = model.Madv;
                    ViewData["Ipf1"] = model.Ipf1;
                    ViewData["Ipf2"] = model.Ipf2;
                    ViewData["Ipf3"] = model.Ipf3;
                    ViewData["Ipf4"] = model.Ipf4;
                    ViewData["Ipf5"] = model.Ipf5;
                    ViewData["Title"] = "Bảng giá tính giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_hhdvcn";
                    ViewData["MenuLv3"] = "menu_hhdvcn_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaHhDvCn/Modify.cshtml", model);

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


        [Route("DinhGiaHhDvCn/Update")]
        [HttpPost]
        public async Task<IActionResult> Update(CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvCn request, IFormFile Ipf1upload, IFormFile Ipf2upload, IFormFile Ipf3upload, IFormFile Ipf4upload, IFormFile Ipf5upload)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.hhdvcn.thongtin", "Edit"))
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

                    if (Ipf2upload != null && Ipf2upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf2upload.FileName);
                        string extension = Path.GetExtension(Ipf2upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf2upload.CopyToAsync(FileStream);
                        }
                        request.Ipf2 = filename;
                    }
                    if (Ipf3upload != null && Ipf3upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf3upload.FileName);
                        string extension = Path.GetExtension(Ipf3upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf3upload.CopyToAsync(FileStream);
                        }
                        request.Ipf3 = filename;
                    }

                    if (Ipf4upload != null && Ipf4upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf4upload.FileName);
                        string extension = Path.GetExtension(Ipf4upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf4upload.CopyToAsync(FileStream);
                        }
                        request.Ipf4 = filename;
                    }

                    if (Ipf5upload != null && Ipf5upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf5upload.FileName);
                        string extension = Path.GetExtension(Ipf5upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf5upload.CopyToAsync(FileStream);
                        }
                        request.Ipf5 = filename;
                    }


                    var model = _db.GiaHhDvCn.FirstOrDefault(t => t.Mahs == request.Mahs);

                    model.Soqd = request.Soqd;
                    model.Ttqd = request.Ttqd;
                    model.Ipf1 = request.Ipf1;
                    model.Ipf2 = request.Ipf2;
                    model.Ipf3 = request.Ipf3;
                    model.Ipf4 = request.Ipf4;
                    model.Ipf5 = request.Ipf5;
                    model.Updated_at = DateTime.Now;

                    _db.GiaHhDvCn.Update(model);
                    _db.SaveChanges();
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_hhdvcn";
                    ViewData["MenuLv3"] = "menu_hhdvcn_tt";
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


        public IActionResult Complete(string mahs_chuyen, string macqcq_chuyen)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.hhdvcn.thongtin", "Index"))
                {
                    // Lấy bản ghi có Mahs =  mahs_chuyen
                    var model = _db.GiaHhDvCn.FirstOrDefault(t => t.Mahs == mahs_chuyen);

                    // Lấy Level của đơn vị chuyển lên
                    var dvcq_join = from dv in _db.DsDonVi
                                    join db in _db.DsDiaBan on dv.MaDiaBan equals db.MaDiaBan
                                    select new VMDsDonVi
                                    {
                                        Id = dv.Id,
                                        MaDiaBan = dv.MaDiaBan,
                                        MaDv = dv.MaDv,
                                        TenDv = dv.TenDv,
                                        Level = db.Level,
                                    };

                    //Check đơn vị chủ quản xem có bằng với đơn vị chủ quản vào không
                    var chk_dvcq = dvcq_join.FirstOrDefault(t => t.MaDv == macqcq_chuyen);
                    model.Macqcq = macqcq_chuyen;
                    model.Trangthai = "HT";

                    if (chk_dvcq != null && chk_dvcq.Level == "T")
                    {
                        model.Madv_t = macqcq_chuyen;
                        model.Thoidiem_t = DateTime.Now;
                        model.Trangthai_t = "CCB";
                    }
                    else if (chk_dvcq != null && chk_dvcq.Level == "ADMIN")
                    {
                        model.Madv_ad = macqcq_chuyen;
                        model.Thoidiem_ad = DateTime.Now;
                        model.Trangthai_ad = "CCB";
                    }
                    else
                    {
                        model.Madv_h = macqcq_chuyen;
                        model.Thoidiem_h = DateTime.Now;
                        model.Trangthai_h = "CCB";
                    }
                    _db.GiaHhDvCn.Update(model);
                    _db.SaveChanges();

                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_hhdvcn";
                    ViewData["MenuLv3"] = "menu_hhdvcn_tt";
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.hhdvcn.thongtin", "Edit"))
                {
                    var model = _db.GiaHhDvCn.FirstOrDefault(t => t.Mahs == Mahs);

                    var model_ct = from ct in _db.GiaHhDvCnCt.Where(t => t.Mahs == Mahs)
                                   join dm in _db.GiaHhDvCnDm on ct.Maspdv equals dm.Maspdv
                                   select new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvCnCt
                                   {
                                       Id = ct.Id,
                                       Tenspdv = dm.Tenspdv,
                                       Dongia = ct.Dongia,
                                       Maspdv = ct.Maspdv,
                                       Trangthai = ct.Trangthai,
                                       Mota = ct.Mota,
                                       Mahs = ct.Mahs,
                                   };

                    model.GiaHhDvCnCt = model_ct.ToList();

                    ViewData["Spdv"] = _db.GiaHhDvCnDm.ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Madv"] = model.Madv;
                    ViewData["Ipf1"] = model.Ipf1;
                    ViewData["Ipf2"] = model.Ipf2;
                    ViewData["Ipf3"] = model.Ipf3;
                    ViewData["Ipf4"] = model.Ipf4;
                    ViewData["Ipf5"] = model.Ipf5;
                    ViewData["Title"] = "Bảng giá tính giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_hhdvcn";
                    ViewData["MenuLv3"] = "menu_hhdvcn_tt";
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.hhdvcn.thongtin", "Index"))
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

        //[Route("GiaHhDvCn/TimKiem/KetQua")]
        //[HttpPost]
        //public IActionResult Result(string madv, string tenhanghoa, DateTime ngaynhap_tu, DateTime ngaynhap_den, double gia_tu, double gia_den)
        //{
        //    if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
        //    {
        //        if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.hhdvcn.thongtin", "Index"))
        //        {

        //            var model = (from giahhdvcnct in _db.GiaHhDvCnCt
        //                         join giahhdvcn in _db.GiaHhDvCn on giahhdvcnct.Mahs equals giahhdvcn.Mahs
        //                         join donvi in _db.DsDonVi on giahhdvcn.Madv equals donvi.MaDv
        //                         select new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvCnCt
        //                         {
        //                             Id = giahhdvcnct.Id,
        //                             Mahs = giahhdvcnct.Mahs,
        //                             Dongia = giahhdvcnct.Dongia,
        //                             Mota = giahhdvcnct.Mota,
        //                             Maspdv = giahhdvcnct.Maspdv
        //                         });


        //            if (madv != "all")
        //            {
        //                model = model.Where(t => t.Madv == madv);
        //            }

        //            if (ngaynhap_tu.ToString("yyMMdd") != "010101")
        //            {
        //                model = model.Where(t => t.Thoidiem >= ngaynhap_tu);
        //            }



        //            if (ngaynhap_den.ToString("yyMMdd") != "010101")
        //            {
        //                model = model.Where(t => t.Thoidiem <= ngaynhap_den);
        //            }

        //            if (tenhanghoa != null)
        //            {
        //                model = model.Where(t => t.Mota == tenhanghoa);
        //            }

        //            model = model.Where(t => t.Mucgia >= gia_tu);
        //            if (gia_den > 0)
        //            {
        //                model = model.Where(t => t.Mucgia <= gia_den);
        //            }

        //            ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá giá hàng hóa, dịch vụ khác theo quy định của pháp luật chuyên ngành";
        //            ViewData["MenuLv1"] = "menu_giakhac";
        //            ViewData["MenuLv2"] = "menu_hhdvcn";
        //            ViewData["MenuLv3"] = "menu_hhdvcn_tk";
        //            return View("Views/Admin/Manages/DinhGia/GiaHhDvCn/TimKiem/Result.cshtml", model);
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

        //    [Route("DinhGiaHhDvCn/Print")]
        //    [HttpGet]
        //    public IActionResult Print(string Mahs)
        //    {
        //        if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
        //        {
        //            if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.spdvcuthe.thongtin", "Index"))
        //            {
        //                var model = _db.GiaHhDvCn.FirstOrDefault(t => t.Mahs == Mahs);

        //                var hoso_dg = new VMDinhGiaPrint
        //                {
        //                    Id = model.Id,

        //                    Phanloaidv = model.Phanloai,
        //                    Mahs = model.Mahs,
        //                    Soqd = model.Soqd,
        //                    Thoidiem = model.Thoidiem,
        //                    Ghichu = model.Ghichu,
        //                };

        //                var modeldv = _db.DsDonVi.FirstOrDefault(t => t.MaDv == model.Macqcq);

        //                if (modeldv != null)
        //                {
        //                    hoso_dg.Tendv = modeldv.TenDvHienThi;
        //                }

        //                var modeldb = _db.DsDiaBan.FirstOrDefault(t => t.MaDiaBan == modeldv.MaDiaBan);
        //                if (modeldb != null)
        //                {
        //                    hoso_dg.Tendb = modeldb.TenDiaBan;
        //                }

        //                var modelct = _db.GiaHhDvCnCt.Where(t => t.Mahs == model.Mahs);
        //                if (modelct != null)
        //                {
        //                    hoso_dg.GiaHhDvCnCt = modelct.ToList();
        //                }

        //                var model = GetThongTinKk(Mahs);


        //                ViewData["Title"] = "In định giá đât cụ thể";
        //               ViewData["MenuLv1"] = "menu_giakhac";
        //                ViewData["MenuLv2"] = "menu_spdvcuthe";
        //                ViewData["MenuLv3"] = "menu_sandvcuthe_thongtin";
        //                return View("Views/Admin/Manages/DinhGia/GiaHhDvCn/Print.cshtml", hoso_dg);

        //            }
        //            else
        //            {
        //                ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
        //                return View("Views/Admin/Error/Page.cshtml");
        //            }
        //        }
        //        else
        //        {
        //            return View("Views/Admin/Error/SessionOut.cshtml");
        //        }
        //    }
        //}
    }
}
