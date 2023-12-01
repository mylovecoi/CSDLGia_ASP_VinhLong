using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.Giathuemuanhaxh
{
    public class GiathuemuanhaxhController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        private readonly IWebHostEnvironment _hostEnvironment;

        public GiathuemuanhaxhController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("DinhGiaThueMuaNhaXaHoi")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanhaxh.thongtin", "Index"))
                {
                    List<GiaThueMuaNhaXh> model = new List<GiaThueMuaNhaXh>();
                    if (string.IsNullOrEmpty(Nam))
                    {
                        Nam = Helpers.ConvertYearToStr(DateTime.Now.Year);
                    }
                    var dsdonvi = (from db in _db.DsDiaBan.Where(t => t.Level != "H")
                                   join dv in _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI") on db.MaDiaBan equals dv.MaDiaBan
                                   select new VMDsDonVi
                                   {
                                       Id = dv.Id,
                                       TenDiaBan = db.TenDiaBan,
                                       TenDv = dv.TenDv,
                                       MaDv = dv.MaDv,
                                   }).ToList();
                    if (dsdonvi.Count > 0)
                    {
                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") != null)
                        {
                            Madv = Helpers.GetSsAdmin(HttpContext.Session, "Madv");
                            ViewData["DsDonVi"] = dsdonvi;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(Madv))
                            {
                                Madv = dsdonvi.OrderBy(t => t.Id).Select(t => t.MaDv).First();
                                model = _db.GiaThueMuaNhaXh.Where(t => t.Madv == Madv && t.Thoidiem.Year == int.Parse(Nam)).ToList();
                            }
                            else
                            {
                                model = _db.GiaThueMuaNhaXh.Where(t => t.Madv == Madv && t.Thoidiem.Year == int.Parse(Nam)).ToList();
                            }
                            ViewData["DsDonVi"] = dsdonvi.Where(t => t.MaDv == Madv);
                        }
                        ViewData["Nam"] = Nam;
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                        ViewData["DsDiaBanAll"] = _db.DsDiaBan;
                        ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                        ViewData["Madv"] = Madv;
                        ViewData["Title"] = " Thông tin hồ sơ giá thuê mua nhà ở xã hội";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgtmnxh";
                        ViewData["MenuLv3"] = "menu_dgtmnxh_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaThueMuaNhaXh/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Title"] = "Thông tin hồ sơ giá thuê mua nhà ở xã hội";
                        ViewData["Messages"] = "Thông tin hồ sơ giá thuê mua nhà ở xã hội.";
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
        [Route("DinhGiaThueMuaNhaXaHoi/Create")]
        [HttpGet]
        public IActionResult Create(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanhaxh.thongtin", "Create"))
                {

                    var model = new VMDinhGiaThueMuaNhaXh
                    {
                        Madv = Madv,
                        Thoidiem = DateTime.Now,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                    };

                    ViewData["Mahs"] = model.Mahs;
                    ViewData["Madv"] = model.Madv;
                    ViewData["Donvitinh"] = _db.DmDvt.ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Tennha"] = _db.GiaThueMuaNhaXhDm.ToList();
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

        [Route("DinhGiaThueMuaNhaXaHoi/Store")]
        [HttpPost]
        public async Task<IActionResult> Store(CSDLGia_ASP.Models.Manages.DinhGia.GiaThueMuaNhaXh request, IFormFile Ipf1, IFormFile Ipf2, IFormFile Ipf3, IFormFile Ipf4, IFormFile Ipf5)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanhaxh.thongtin", "Create"))
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

                    if (Ipf2 != null && Ipf2.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf2.FileName);
                        string extension = Path.GetExtension(Ipf2.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf2.CopyToAsync(FileStream);
                        }
                        request.Ipf2 = filename;
                    }

                    if (Ipf3 != null && Ipf3.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf3.FileName);
                        string extension = Path.GetExtension(Ipf3.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf3.CopyToAsync(FileStream);
                        }
                        request.Ipf3 = filename;
                    }

                    if (Ipf4 != null && Ipf4.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf4.FileName);
                        string extension = Path.GetExtension(Ipf4.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf4.CopyToAsync(FileStream);
                        }
                        request.Ipf4 = filename;
                    }

                    if (Ipf5 != null && Ipf5.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf5.FileName);
                        string extension = Path.GetExtension(Ipf5.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf5.CopyToAsync(FileStream);
                        }
                        request.Ipf5 = filename;
                    }
                    var model = new GiaThueMuaNhaXh
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Madiaban = request.Madiaban,
                        Soqd = request.Soqd,
                        Thoidiem = request.Thoidiem,
                        Thongtin = request.Thongtin,
                        Ghichu = request.Ghichu,
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
                    _db.GiaThueMuaNhaXh.Add(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaThueMuaNhaXhCt.Where(t => t.Mahs == request.Mahs);
                    _db.GiaThueMuaNhaXhCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "Giathuemuanhaxh", new { request.Mahs });
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

        [Route("DinhGiaThueMuaNhaXaHoi/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanhaxh.thongtin", "Delete"))
                {
                    var model = _db.GiaThueMuaNhaXh.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaThueMuaNhaXh.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.GiaThueMuaNhaXhCt.Where(t => t.Mahs == model.Mahs);
                    _db.GiaThueMuaNhaXhCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "Giathuemuanhaxh", new { model.Madv });
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

        [Route("DinhGiaThueMuaNhaXaHoi/Modify")]
        [HttpGet]
        public IActionResult Modify(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanhaxh.thongtin", "Edit"))
                {
                    var model = _db.GiaThueMuaNhaXh.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new VMDinhGiaThueMuaNhaXh
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        Madiaban = model.Madiaban,
                        Soqd = model.Soqd,
                        Ipf1 = model.Ipf1,
                        Ipf2 = model.Ipf2,
                        Ipf3 = model.Ipf3,
                        Ipf4 = model.Ipf4,
                        Ipf5 = model.Ipf5,
                        Thoidiem = model.Thoidiem,
                        Thongtin = model.Thongtin,
                        Ghichu = model.Ghichu
                    };

                    var model_ct = _db.GiaThueMuaNhaXhCt.Where(t => t.Mahs == model_new.Mahs);

                    model_new.GiaThueMuaNhaXhCt = model_ct.ToList();

                    ViewData["Madv"] = model.Madv;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["Ipf1"] = model.Ipf1;
                    ViewData["Ipf2"] = model.Ipf2;
                    ViewData["Ipf3"] = model.Ipf3;
                    ViewData["Ipf4"] = model.Ipf4;
                    ViewData["Ipf5"] = model.Ipf5;
                    ViewData["Donvitinh"] = _db.DmDvt.ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Tennha"] = _db.GiaThueMuaNhaXhDm.ToList();
                    ViewData["Title"] = "Chỉnh sửa giá thuê mua nhà xã hội";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmnxh";
                    ViewData["MenuLv3"] = "menu_dgtmnxh_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaThueMuaNhaXh/Modify.cshtml", model_new);
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

        [Route("DinhGiaThueMuaNhaXaHoi/Update")]
        [HttpPost]
        public IActionResult Update(VMDinhGiaThueMuaNhaXh request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanhaxh.thongtin", "Edit"))
                {
                    var model = _db.GiaThueMuaNhaXh.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Madiaban = request.Madiaban;
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Thongtin = request.Thongtin;
                    model.Ghichu = request.Ghichu;
                    model.Updated_at = DateTime.Now;
                    _db.GiaThueMuaNhaXh.Update(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaThueMuaNhaXhCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Mahs = model.Mahs;
                        }
                    }
                    _db.GiaThueMuaNhaXhCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "Giathuemuanhaxh", new { request.Mahs });
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

        [Route("DinhGiaThueMuaNhaXaHoi/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanhaxh.thongtin", "Edit"))
                {
                    var model = _db.GiaThueMuaNhaXh.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new VMDinhGiaThueMuaNhaXh
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        Madiaban = model.Madiaban,
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Thongtin = model.Thongtin,
                        Ghichu = model.Ghichu
                    };

                    var model_ct = _db.GiaThueMuaNhaXhCt.Where(t => t.Mahs == model_new.Mahs);

                    model_new.GiaThueMuaNhaXhCt = model_ct.ToList();

                    ViewData["Madv"] = model.Madv;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Tennha"] = _db.GiaThueMuaNhaXhDm.ToList();
                    ViewData["Title"] = "Chi tiết giá thuê mua nhà xã hội";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmnxh";
                    ViewData["MenuLv3"] = "menu_dgtmnxh_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaThueMuaNhaXh/Show.cshtml", model_new);
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

        [Route("DinhGiaThueMuaNhaXaHoi/Printf")]
        [HttpGet]
        public IActionResult Printf(string Nam, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanhaxh.thongtin", "Index"))
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

        [Route("DinhGiaThueMuaNhaXaHoi/Search")]
        [HttpGet]
        public IActionResult Search()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanhaxh.timkiem", "Index"))
                {

                    /*var model_join = from dgct in _db.GiaThueMuaNhaXhCt
                                     join dg in _db.GiaThueMuaNhaXh.Where(t =>  t.Trangthai == "HT") on dgct.Mahs equals dg.Mahs
                                     select new VMDinhGiaThueMuaNhaXaHoiCt
                                     {
                                         Id = dg.Id,
                                         Mahs = dg.Mahs,
                                         Madv = dg.Madv,
                                         Dongia = dgct.Dongia,
                                         Macqcq=dg.Macqcq,
                                         Thoidiem=dg.Thoidiem,
                                         Dientich=dgct.Dientich,
                                     };*/

                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Title"] = "Tìm kiếm thông tin định giá thuê mua nhà xã hội";
                    ViewData["Tennha"] = _db.GiaThueMuaNhaXhDm.ToList();
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmnxh";
                    ViewData["MenuLv3"] = "menu_dgtmnxh_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaThueMuaNhaXh/TimKiem/Index.cshtml");

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
        [Route("DinhGiaThueMuaNhaXaHoi/Result")]
        [HttpPost]
        public IActionResult Result(DateTime beginTime, DateTime endTime, double beginPrice, double endPrice, string tn, string madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanhaxh.timkiem", "Edit"))
                {
                    var model = from dgct in _db.GiaThueMuaNhaXhCt
                                join dg in _db.GiaThueMuaNhaXh on dgct.Mahs equals dg.Mahs
                                join dgdm in _db.GiaThueMuaNhaXhDm on dgct.Maso equals dgdm.Maso
                                select new VMDinhGiaThueMuaNhaXhSearch
                                {
                                    Id = dg.Id,
                                    Mahs = dg.Mahs,
                                    Madv = dg.Madv,
                                    Dongia = dg.Dongia,
                                    Macqcq = dg.Macqcq,
                                    Thoidiem = dg.Thoidiem,
                                    Maso = dgct.Maso,
                                    Tennha = dgdm.Tennha,
                                    Dvt = dgct.Dvt,
                                    Phanloai = dgct.Phanloai,
                                };
                    /*model_join.Where(t => t.Maso == tn
                                    && t.Macqcq == dv
                                    && t.Thoidiem >= beginTime
                                    && t.Thoidiem <= endTime
                                    && t.Dongia >= beginPrice
                                    && t.Dongia <= endPrice);*/
                    if (madv != "All")
                    {
                        model = model.Where(t => t.Madv == madv);
                    }
                    if (tn != "All")
                    {
                        model = model.Where(t => t.Maso == tn);
                    }
                    if (beginTime.ToString("yyMMdd") != "010101")
                    {
                        model = model.Where(t => t.Thoidiem >= beginTime);
                    }

                    if (endTime.ToString("yyMMdd") != "010101")
                    {
                        model = model.Where(t => t.Thoidiem <= endTime);
                    }
                    model = model.Where(t => t.Dongia >= beginPrice);
                    if (endPrice > 0)
                    {
                        model = model.Where(t => t.Dongia <= endPrice);
                    }
                    ViewData["Tennha"] = _db.GiaThueMuaNhaXhDm.ToList();
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
    }
}
