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
/*using Grpc.Core;*/

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.Gianuocsh
{
    public class GianuocshController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        private readonly IWebHostEnvironment _hostEnvironment;

        public GianuocshController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("DinhGiaNuocSh")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.thongtin", "Index"))
                {
                    List<GiaNuocSh> model = new List<GiaNuocSh>();
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
                                //model = _db.GiaDatPhanLoai.Where(t => t.Thoidiem.Year == int.Parse(Nam)).ToList();
                                model = _db.GiaNuocSh.Where(t => t.Madv == Madv && t.Thoidiem.Year == int.Parse(Nam)).ToList();
                            }
                            else
                            {
                                model = _db.GiaNuocSh.Where(t => t.Madv == Madv && t.Thoidiem.Year == int.Parse(Nam)).ToList();
                            }
                            ViewData["DsDonVi"] = dsdonvi.Where(t => t.MaDv == Madv);
                        }
                        ViewData["Nam"] = Nam;
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                        ViewData["DsDiaBanAll"] = _db.DsDiaBan.ToList();
                        ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                        ViewData["Donvi"] = Madv;
                        ViewData["Title"] = " Thông tin hồ sơ giá nước sạch sinh hoạt";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgnsh";
                        ViewData["MenuLv3"] = "menu_dgnsh_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaNuocSh/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Title"] = "Thông tin hồ sơ giá nước sạch sinh hoạt";
                        ViewData["Messages"] = "Thông tin hồ sơ giá rừng.";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgnsh";
                        ViewData["MenuLv3"] = "menu_dgnsh_tt";
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
        [Route("DinhGiaNuocSh/Create")]
        [HttpGet]
        public IActionResult Create(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.thongtin", "Create"))
                {

                    var model = new VMDinhGiaNuocSh()
                    {
                        Madv = Madv,
                        Thoidiem = DateTime.Now,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),

                    };
                    var dm = _db.GiaNuocShDmVung.ToList();
                    foreach (var item in dm)
                    {
                        var ct = new GiaNuocShCt
                        {
                            Madoituong = item.Madoituong,
                            Doituongsd = item.Doituongsd,
                            Mahs = model.Mahs,
                        };


                        _db.GiaNuocShCt.Add(ct);
                        _db.SaveChanges();
                    }
                    ViewData["Madv"] = Madv;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Danhmuc"] = _db.GiaNuocShDmVung.ToList();
                    ViewData["Chitiet"] = _db.GiaNuocShCt.Where(t => t.Mahs == model.Mahs);
                    ViewData["Title"] = "Thêm mới giá nước sạch sinh hoạt";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgnsh";
                    ViewData["MenuLv3"] = "menu_dgnsh_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaNuocSh/Create.cshtml", model);
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

        [Route("DinhGiaNuocSh/Store")]
        [HttpPost]
        public async Task<IActionResult> Store(CSDLGia_ASP.Models.Manages.DinhGia.GiaNuocSh request, IFormFile Ipf1, IFormFile Ipf2, IFormFile Ipf3, IFormFile Ipf4, IFormFile Ipf5)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.thongtin", "Create"))
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
                    var model = new GiaNuocSh
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
                    _db.GiaNuocSh.Add(model);
                    _db.SaveChanges();

                    //var modelct = _db.GiaNuocShCt.Where(t => t.Mahs == request.Mahs);


                    return RedirectToAction("Index", "Gianuocsh", new { request.Mahs });
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

        [Route("DinhGiaNuocSh/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.thongtin", "Delete"))
                {
                    var model = _db.GiaNuocSh.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaNuocSh.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.GiaNuocShCt.Where(t => t.Mahs == model.Mahs);
                    _db.GiaNuocShCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "Gianuocsh", new { model.Madv });
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

        [Route("DinhGiaNuocSh/Modify")]
        [HttpGet]
        public IActionResult Modify(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.thongtin", "Edit"))
                {
                    var model = _db.GiaNuocSh.FirstOrDefault(t => t.Mahs == Mahs); // Lấy ra bản ghi có Mahs bằng Mahs chuyền sang
                    var model_new = new VMDinhGiaNuocSh
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        Madiaban = model.Madiaban,
                        Soqd = model.Soqd,
                        Tunam = model.Tunam,
                        Dennam = model.Dennam,
                        Mota = model.Mota,
                        Ipf1 = model.Ipf1,
                        Ipf2 = model.Ipf2,
                        Ipf3 = model.Ipf3,
                        Ipf4 = model.Ipf4,
                        Ipf5 = model.Ipf5,
                        Thoidiem = model.Thoidiem,
                        Thongtin = model.Thongtin,
                        Ghichu = model.Ghichu,
                    };

                    var model_ct = _db.GiaNuocShCt.Where(t => t.Mahs == model_new.Mahs);

                    ViewData["Madv"] = model.Madv;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["Ipf1"] = model.Ipf1;
                    ViewData["Ipf2"] = model.Ipf2;
                    ViewData["Ipf3"] = model.Ipf3;
                    ViewData["Ipf4"] = model.Ipf4;
                    ViewData["Ipf5"] = model.Ipf5;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Chitiet"] = _db.GiaNuocShCt.Where(t => t.Mahs == Mahs);
                    ViewData["Title"] = "Chỉnh sửa giá nước sinh hoạt";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgnsh";
                    ViewData["MenuLv3"] = "menu_dgnsh_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaNuocSh/Modify.cshtml", model_new);
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

        [Route("DinhGiaNuocSh/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.thongtin", "Edit"))
                {
                    var model = _db.GiaNuocSh.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new VMDinhGiaNuocSh
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        Madiaban = model.Madiaban,
                        Tunam = model.Tunam,
                        Dennam = model.Dennam,
                        Soqd = model.Soqd,
                        Mota = model.Mota,
                        Ipf1 = model.Ipf1,
                        Ipf2 = model.Ipf2,
                        Ipf3 = model.Ipf3,
                        Ipf4 = model.Ipf4,
                        Ipf5 = model.Ipf5,
                        Thoidiem = model.Thoidiem,
                        Thongtin = model.Thongtin,
                        Ghichu = model.Ghichu,
                    };

                    var model_ct = _db.GiaNuocShCt.Where(t => t.Mahs == model_new.Mahs);

                    ViewData["Madv"] = model.Madv;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["Ipf1"] = model.Ipf1;
                    ViewData["Ipf2"] = model.Ipf2;
                    ViewData["Ipf3"] = model.Ipf3;
                    ViewData["Ipf4"] = model.Ipf4;
                    ViewData["Ipf5"] = model.Ipf5;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Chitiet"] = _db.GiaNuocShCt.Where(t => t.Mahs == Mahs);
                    ViewData["Title"] = "Chỉnh sửa giá nước sinh hoạt";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgnsh";
                    ViewData["MenuLv3"] = "menu_dgnsh_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaNuocSh/Show.cshtml", model_new);
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

        [Route("DinhGiaNuocSh/Update")]
        [HttpPost]
        public IActionResult Update(VMDinhGiaNuocSh request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.thongtin", "Edit"))
                {
                    var model = _db.GiaNuocSh.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Madiaban = request.Madiaban;
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Thongtin = request.Thongtin;
                    model.Ghichu = request.Ghichu;
                    model.Updated_at = DateTime.Now;
                    _db.GiaNuocSh.Update(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaNuocShCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Mahs = model.Mahs;
                        }
                    }
                    _db.GiaNuocShCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "Gianuocsh", new { request.Mahs });
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

        [Route("DinhGiaNuocSh/Printf")]
        [HttpGet]
        public IActionResult Printf(string Nam, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgxmtxd.giakk", "Index"))
                {

                    // Lấy bản ghi có năm và mã đơn vị trùng với năm và mã đơn vị truyền vào ( Năm và Mã đơn vị tuyền từ Index sang )


                    var model = _db.GiaNuocSh.Where(t => t.Trangthai == "HT").ToList();


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
                    ViewData["MenuLv2"] = "menu_dgnsh";
                    ViewData["MenuLv3"] = "menu_dgnsh_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaNuocSh/Print.cshtml", model);

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

        [Route("DinhGiaNuocSh/Search")]
        [HttpGet]
        public IActionResult Search()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.thongtin", "Index"))
                {

                    var model_join = from dgct in _db.GiaNuocShCt
                                     join dg in _db.GiaNuocSh.Where(t => t.Trangthai == "HT") on dgct.Mahs equals dg.Mahs
                                     select new VMDinhGiaNuocShSearch
                                     {
                                         Id = dg.Id,
                                         Mahs = dg.Mahs,
                                         Madv = dg.Macqcq,
                                         Thanhtien = dgct.Thanhtien,
                                         Macqcq = dg.Macqcq,
                                         Thoidiem = dg.Thoidiem,
                                     };

                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Title"] = "Tìm kiếm thông tin định giá nước sạch sinh hoạt";
                    ViewData["Doituong"] = _db.GiaNuocShDmVung;
                    ViewData["DsDiaBanAll"] = _db.DsDiaBan;
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgnsh";
                    ViewData["MenuLv3"] = "menu_dgnsh_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaNuocSh/TimKiem/Index.cshtml", model_join);

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

        [Route("DinhGiaNuocSh/Result")]
        [HttpPost]
        public IActionResult Result(DateTime beginTime, DateTime endTime, double beginPrice, double endPrice, string dt, string madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.thongtin", "Edit"))
                {
                    var model = from dgct in _db.GiaNuocShCt
                                join dg in _db.GiaNuocSh on dgct.Mahs equals dg.Mahs
                                select new VMDinhGiaNuocShSearch
                                {
                                    Id = dg.Id,
                                    Mahs = dg.Mahs,
                                    Madv = dg.Madv,
                                    Thanhtien = dgct.Thanhtien,
                                    Macqcq = dg.Macqcq,
                                    Thoidiem = dg.Thoidiem,
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
                                };
                    /*model_join.Where(t => t.Madoituong == dt
                                    && t.Macqcq == dv
                                    && t.Thoidiem >= beginTime
                                    && t.Thoidiem <= endTime
                                    && t.Giatri >= beginPrice
                                    && t.Giatri <= endPrice);*/
                    if (madv != "All")
                    {
                        model = model.Where(t => t.Madv == madv);
                    }
                    if (dt != "All")
                    {
                        model = model.Where(t => t.Madoituong == dt);
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
                    ViewData["Doituong"] = _db.GiaNuocShDmVung;
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    return View("Views/Admin/Manages/DinhGia/GiaNuocSh/TimKiem/Result.cshtml", model);
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
