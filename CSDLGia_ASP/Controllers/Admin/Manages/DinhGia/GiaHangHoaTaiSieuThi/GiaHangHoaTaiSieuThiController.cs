using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Linq;
using CSDLGia_ASP.ViewModels.Systems;
using CSDLGia_ASP.Models.Systems;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaHangHoaTaiSieuThi
{
    public class GiaHangHoaTaiSieuThiController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        private readonly IWebHostEnvironment _hostEnvironment;

        public GiaHangHoaTaiSieuThiController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("GiaHangHoaTaiSieuThi")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giasieuthi.thongtin", "Index"))
                {
                    var dsdonvi = (from db in _db.DsDiaBan.Where(t => t.Level != "H")
                                   join dv in _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI") on db.MaDiaBan equals dv.MaDiaBan
                                   select new VMDsDonVi
                                   {
                                       Id = dv.Id,
                                       TenDiaBan = db.TenDiaBan,
                                       TenDv = dv.TenDv,
                                       MaDiaBan = dv.MaDiaBan,
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

                        var model = _db.GiaHangHoaTaiSieuThi.Where(t => t.Madv == Madv).ToList();

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
                        ViewData["Nam"] = Nam;
                        ViewData["Madv"] = Madv;
                        ViewData["Title"] = " Thông tin giá hàng hóa tại siêu thị";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgsieuthi";
                        ViewData["MenuLv3"] = "menu_dgsieuthi_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaHangHoaTaiSieuThi/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Title"] = "Thông tin hồ sơ giá hàng hóa tại siêu thị";
                        ViewData["Messages"] = "Hệ thống chưa có định giá hàng hóa tại siêu thị.";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgsieuthi";
                        ViewData["MenuLv3"] = "menu_dgsieuthi_tt";
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

        [Route("GiaHangHoaTaiSieuThi/Create")]
        [HttpGet]
        public IActionResult Create(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giasieuthi.thongtin", "Create"))
                {
                    var check = _db.GiaHangHoaTaiSieuThiCt.Where(t => t.Trangthai == "CXD");
                    if (check != null)
                    {
                        _db.GiaHangHoaTaiSieuThiCt.RemoveRange(check);
                        _db.SaveChanges();
                    }

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaHangHoaTaiSieuThi
                    {
                        Madv = Madv,
                        Thoidiem = DateTime.Now,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Tunam = DateTime.Now.Year.ToString(),
                        Dennam = DateTime.Now.Year.ToString(),
                    };

                    var dm = _db.GiaHangHoaTaiSieuThiDmSieuThi.ToList();
                    var ct = new List<GiaHangHoaTaiSieuThiCt>();
                    foreach (var item in dm)
                    {
                        ct.Add(new GiaHangHoaTaiSieuThiCt()
                        {
                            Mahs = model.Mahs,
                            //Madoituong = item.Madoituong,
                            //Doituongsd = item.Doituongsd,
                            Trangthai = "CXD",
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        });
                    }

                    _db.GiaHangHoaTaiSieuThiCt.AddRange(ct);
                    _db.SaveChanges();

                    model.GiaHangHoaTaiSieuThiCt = ct.Where(t => t.Mahs == model.Mahs).ToList();

                    ViewData["Madv"] = Madv;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Danhmuc"] = _db.GiaHangHoaTaiSieuThiDmSieuThi.ToList();
                    ViewData["Title"] = "Thêm mới giá hàng hóa tại siêu thị";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgsieuthi";
                    ViewData["MenuLv3"] = "menu_dgsieuthi_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaHangHoaTaiSieuThi/Create.cshtml", model);
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

        [Route("GiaHangHoaTaiSieuThi/Store")]
        [HttpPost]
        public async Task<IActionResult> Store(CSDLGia_ASP.Models.Manages.DinhGia.GiaHangHoaTaiSieuThi request, IFormFile Ipf1upload, IFormFile Ipf2upload, IFormFile Ipf3upload, IFormFile Ipf4upload, IFormFile Ipf5upload)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giasieuthi.thongtin", "Create"))
                {
                    if (Ipf1upload != null && Ipf1upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf1upload.FileName);
                        string extension = Path.GetExtension(Ipf1upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/GiaHangHoaTaiSieuThi/", filename);
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
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/GiaHangHoaTaiSieuThi/", filename);
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
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/GiaHangHoaTaiSieuThi/", filename);
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
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/GiaHangHoaTaiSieuThi/", filename);
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
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/GiaHangHoaTaiSieuThi/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf5upload.CopyToAsync(FileStream);
                        }
                        request.Ipf5 = filename;
                    }

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaHangHoaTaiSieuThi
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Madiaban = request.Madiaban,
                        Soqd = request.Soqd,
                        Thoidiem = request.Thoidiem,
                        Mota = request.Mota,
                        Ghichu = request.Ghichu,
                        Tunam = request.Tunam,
                        Dennam = request.Dennam,
                        Ipf1 = request.Ipf1,
                        Ipf2 = request.Ipf2,
                        Ipf3 = request.Ipf3,
                        Ipf4 = request.Ipf4,
                        Ipf5 = request.Ipf5,
                        Trangthai = "CHT",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaHangHoaTaiSieuThi.Add(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaHangHoaTaiSieuThiCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Trangthai = "XD";
                        }
                    }
                    _db.GiaHangHoaTaiSieuThiCt.UpdateRange(modelct);
                    _db.SaveChanges();


                    return RedirectToAction("Index", "GiaHangHoaTaiSieuThi", new { request.Madv });
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

        [Route("GiaHangHoaTaiSieuThi/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giasieuthi.thongtin", "Edit"))
                {
                    var model = _db.GiaHangHoaTaiSieuThi.FirstOrDefault(t => t.Mahs == Mahs); 

                    var model_ct = _db.GiaHangHoaTaiSieuThiCt.Where(t => t.Mahs == model.Mahs);

                    model.GiaHangHoaTaiSieuThiCt = model_ct.ToList();

                    ViewData["Madv"] = model.Madv;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Title"] = "Chỉnh sửa hàng hóa tại siêu thị";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgsieuthi";
                    ViewData["MenuLv3"] = "menu_dgsieuthi_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaHangHoaTaiSieuThi/Edit.cshtml", model);
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

        [Route("GiaHangHoaTaiSieuThi/Update")]
        [HttpPost]
        public async Task<IActionResult> Update(CSDLGia_ASP.Models.Manages.DinhGia.GiaHangHoaTaiSieuThi request, IFormFile Ipf1upload, IFormFile Ipf2upload, IFormFile Ipf3upload, IFormFile Ipf4upload, IFormFile Ipf5upload)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giasieuthi.thongtin", "Edit"))
                {
                    if (Ipf1upload != null && Ipf1upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf1upload.FileName);
                        string extension = Path.GetExtension(Ipf1upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/GiaHangHoaTaiSieuThi/", filename);
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
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/GiaHangHoaTaiSieuThi/", filename);
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
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/GiaHangHoaTaiSieuThi/", filename);
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
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/GiaHangHoaTaiSieuThi/", filename);
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
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/GiaHangHoaTaiSieuThi/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf5upload.CopyToAsync(FileStream);
                        }
                        request.Ipf5 = filename;
                    }

                    var model = _db.GiaHangHoaTaiSieuThi.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Madiaban = request.Madiaban;
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Thongtin = request.Thongtin;
                    model.Ghichu = request.Ghichu;
                    model.Mota = request.Mota;
                    model.Ipf1 = request.Ipf1;
                    model.Ipf2 = request.Ipf2;
                    model.Ipf3 = request.Ipf3;
                    model.Ipf4 = request.Ipf4;
                    model.Ipf5 = request.Ipf5;
                    model.Updated_at = DateTime.Now;
                    _db.GiaHangHoaTaiSieuThi.Update(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaHangHoaTaiSieuThiCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Mahs = model.Mahs;
                        }
                    }
                    _db.GiaHangHoaTaiSieuThiCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaHangHoaTaiSieuThi", new { request.Madv });
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

        [Route("GiaHangHoaTaiSieuThi/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giasieuthi.thongtin", "Delete"))
                {
                    var model = _db.GiaHangHoaTaiSieuThi.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaHangHoaTaiSieuThi.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.GiaHangHoaTaiSieuThiCt.Where(t => t.Mahs == model.Mahs);
                    _db.GiaHangHoaTaiSieuThiCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaHangHoaTaiSieuThi", new { model.Madv });
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

        [Route("GiaHangHoaTaiSieuThi/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giasieuthi.thongtin", "Edit"))
                {
                    var model = _db.GiaHangHoaTaiSieuThi.FirstOrDefault(t => t.Mahs == Mahs);

                    var model_ct = _db.GiaHangHoaTaiSieuThiCt.Where(t => t.Mahs == model.Mahs);

                    model.GiaHangHoaTaiSieuThiCt = model_ct.ToList();

                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Title"] = "Xem chi tiết hàng hóa tại siêu thị";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgsieuthi";
                    ViewData["MenuLv3"] = "menu_dgsieuthi_tt";
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    return View("Views/Admin/Manages/DinhGia/GiaHangHoaTaiSieuThi/Show.cshtml", model);
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

        [Route("GiaHangHoaTaiSieuThi/Printf")]
        [HttpGet]
        public IActionResult Printf(string Nam, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giasieuthi.thongtin", "Index"))
                {
                    var dsdonvi = (from db in _db.DsDiaBan.Where(t => t.Level != "H")
                                     join dv in _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI") on db.MaDiaBan equals dv.MaDiaBan
                                     select new VMDsDonVi
                                     {
                                         Id = dv.Id,
                                         TenDiaBan = db.TenDiaBan,
                                         TenDv = dv.TenDv,
                                         MaDiaBan = dv.MaDiaBan,
                                         MaDv = dv.MaDv,
                                     }).ToList();

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

                    var model = _db.GiaHangHoaTaiSieuThi.Where(t => t.Madv == Madv).ToList();

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
                    ViewData["MenuLv2"] = "menu_dgsieuthi";
                    ViewData["MenuLv3"] = "menu_dgsieuthi_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaHangHoaTaiSieuThi/Printf.cshtml", model);

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

        public IActionResult Complete(string mahs_complete, string macqcq_chuyen)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.xetduyet", "Approve"))
                {
                    var model = _db.GiaHangHoaTaiSieuThi.FirstOrDefault(p => p.Mahs == mahs_complete);

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
                    _db.GiaHangHoaTaiSieuThi.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "GiaHangHoaTaiSieuThi", new { Madv = model.Madv, Nam = model.Thoidiem.Year });
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

        [Route("GiaHangHoaTaiSieuThi/Search")]
        [HttpGet]
        public IActionResult Search()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giasieuthi.timkiem", "Index"))
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
                    ViewData["Doituong"] = _db.GiaHangHoaTaiSieuThiDmSieuThi.ToList();
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ hàng hóa tại siêu thị";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgsieuthi";
                    ViewData["MenuLv3"] = "menu_dgsieuthi_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaHangHoaTaiSieuThi/TimKiem/Index.cshtml");

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

        [Route("GiaHangHoaTaiSieuThi/Result")]
        [HttpPost]
        public IActionResult Result(DateTime beginTime, DateTime endTime, double beginPrice, double endPrice, string Madoituong, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giasieuthi.timkiem", "Edit"))
                {
                    var model = from dgct in _db.GiaHangHoaTaiSieuThiCt
                                join dg in _db.GiaHangHoaTaiSieuThi on dgct.Mahs equals dg.Mahs
                                select new GiaHangHoaTaiSieuThiCt
                                {
                                    Id = dgct.Id,
                                    Mahs = dgct.Mahs,
                                    Madoituong = dgct.Madoituong,
                                    Doituongsd = dgct.Doituongsd,
                                    Namchuathue = dgct.Namchuathue,
                                    Namchuathue1 = dgct.Namchuathue1,
                                    Namchuathue2 = dgct.Namchuathue2,
                                    Namchuathue3 = dgct.Namchuathue3,
                                    Namchuathue4 = dgct.Namchuathue4,
                                    Giachuathue = dgct.Giachuathue,
                                    Giachuathue1 = dgct.Giachuathue1,
                                    Giachuathue2 = dgct.Giachuathue2,
                                    Giachuathue3 = dgct.Giachuathue3,
                                    Giachuathue4 = dgct.Giachuathue4,
                                    Madv = dg.Madv,
                                    Thoidiem = dg.Thoidiem,
                                };

                    if (Madv != "All")
                    {
                        model = model.Where(t => t.Madv == Madv);
                    }

                    if (Madoituong != "All")
                    {
                        model = model.Where(t => t.Madoituong == Madoituong);
                    }

                    if (beginTime.ToString("yyMMdd") != "010101")
                    {
                        model = model.Where(t => t.Thoidiem >= beginTime);
                    }

                    if (endTime.ToString("yyMMdd") != "010101")
                    {
                        model = model.Where(t => t.Thoidiem <= endTime);
                    }

                    model = model.Where(t => t.Giachuathue >= beginPrice);
                    if (endPrice > 0)
                    {
                        model = model.Where(t => t.Giachuathue <= endPrice);
                    }

                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Doituong"] = _db.GiaHangHoaTaiSieuThiDmSieuThi.ToList();
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ hàng hóa tại siêu thị";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgsieuthi";
                    ViewData["MenuLv3"] = "menu_dgsieuthi_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaHangHoaTaiSieuThi/TimKiem/Result.cshtml", model);
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
