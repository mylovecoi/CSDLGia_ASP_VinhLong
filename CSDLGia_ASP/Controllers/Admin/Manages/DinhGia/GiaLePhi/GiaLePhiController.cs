using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaLePhi
{
    public class GiaLePhiController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GiaLePhiController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("DinhGiaLePhi")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.lephi.thongtin", "Index"))
                {

                    var dsdonvi = (from db in _db.DsDiaBan.Where(t => t.Level != "H")
                                   join dv in _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI") on db.MaDiaBan equals dv.MaDiaBan
                                   select new VMDsDonVi
                                   {
                                       Id = dv.Id,
                                       TenDiaBan = db.TenDiaBan,
                                       MaDv = dv.MaDv
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
                        var model = _db.GiaPhiLePhi.Where(t => t.Madv == Madv).ToList();

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

                        ViewData["Madv"] = Madv;
                        ViewData["Nam"] = Nam;
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                        ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                        ViewData["Title"] = " Thông tin hồ sơ giá lệ phí trước bạ";
                        ViewData["MenuLv1"] = "menu_giakhac";
                        ViewData["MenuLv2"] = "menu_dglp";
                        ViewData["MenuLv3"] = "menu_dglp_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaLePhi/DanhSach/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Messages"] = "Thông tin hồ sơ  giá lệ phí trước bạ.";
                        ViewData["Title"] = " Thông tin hồ sơ giá lệ phí trước bạ";
                        ViewData["MenuLv1"] = "menu_lp";
                        ViewData["MenuLv2"] = "menu_gialp_tt";
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
        [Route("DinhGiaLePhi/Create")]
        [HttpGet]
        public IActionResult Create(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.lephi.thongtin", "Create"))
                {
                    var modelcxd = _db.GiaPhiLePhiCt.Where(t => t.Ghichu == "CXD").ToList();
                    if (modelcxd != null)
                    {
                        _db.GiaPhiLePhiCt.RemoveRange(modelcxd);

                        _db.SaveChanges();
                    }
                    var model = new GiaPhiLePhi
                    {
                        Madv = Madv,
                        Thoidiem = DateTime.Now,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                    };
                    var dm = _db.GiaPhiLePhiDm.ToList();
                    var chitiet = new List<GiaPhiLePhiCt>();
                    foreach (var item in dm)
                    {
                        chitiet.Add(new GiaPhiLePhiCt()
                        {
                            Mahs = model.Mahs,
                            Phanloai = item.Phanloai,
                            Ptcp = item.Tennhom,
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        });
                    }
                    _db.GiaPhiLePhiCt.AddRange(chitiet);
                    _db.SaveChanges();

                    model.GiaPhiLePhiCt = _db.GiaPhiLePhiCt.Where(t => t.Mahs == model.Mahs).ToList();
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDiaBan"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Phanloai"] = _db.GiaPhiLePhiDm.ToList();

                    ViewData["Title"] = " Thông tin hồ sơ giá lệ phí trước bạ";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_dglp";
                    ViewData["MenuLv3"] = "menu_dglp_tt";

                    return View("Views/Admin/Manages/DinhGia/GiaLePhi/DanhSach/Create.cshtml", model);
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

        [Route("DinhGiaLePhi/Store")]
        [HttpPost]
        public async Task<IActionResult> Store(GiaPhiLePhi request, IFormFile Ipf1, IFormFile Ipf2
            , IFormFile Ipf3, IFormFile Ipf4, IFormFile Ipf5)
        {

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.lephi.thongtin", "Create"))
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
                    var model = new GiaPhiLePhi
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Madiaban = request.Madiaban,
                        Soqd = request.Soqd,
                        Thoidiem = request.Thoidiem,
                        Thongtin = request.Thongtin,
                        Trangthai = "CHT",
                        Congbo = "CHUACONGBO",
                        Mota = request.Mota,
                        Ipf1 = request.Ipf1,
                        Ipf2 = request.Ipf2,
                        Ipf3 = request.Ipf3,
                        Ipf4 = request.Ipf4,
                        Ipf5 = request.Ipf5,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaPhiLePhi.Add(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaPhiLePhiCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Ghichu = "XD";
                        }
                    }
                    _db.GiaPhiLePhiCt.UpdateRange(modelct);
                    _db.SaveChanges();



                    return RedirectToAction("Index", "GiaLePhi", new { Madv = request.Madv });
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


        [Route("DinhGiaLePhi/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.lephi.thongtin", "Edit"))
                {
                    var model = _db.GiaPhiLePhi.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new GiaPhiLePhi
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        Mota = model.Mota,
                        Ghichu = model.Ghichu,
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Thongtin = model.Thongtin,
                    };

                    var model_ct = _db.GiaPhiLePhiCt.Where(t => t.Mahs == model_new.Mahs);
                    model_new.GiaPhiLePhiCt = model_ct.ToList();



                    ViewData["Madv"] = model.Madv;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Phanloai"] = _db.GiaPhiLePhiDm.ToList();

                    ViewData["Title"] = " Thông tin hồ sơ giá lệ phí trước bạ";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_dglp";
                    ViewData["MenuLv3"] = "menu_dglp_tt";

                    return View("Views/Admin/Manages/DinhGia/GiaLePhi/DanhSach/Edit.cshtml", model_new);
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

        [Route("DinhGiaLePhi/Update")]
        [HttpPost]
        public async Task<IActionResult> Update(GiaPhiLePhi request, IFormFile Ipf1, IFormFile Ipf2
            , IFormFile Ipf3, IFormFile Ipf4, IFormFile Ipf5)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.lephi.thongtin", "Edit"))
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
                    var model = _db.GiaPhiLePhi.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Soqd = request.Soqd;
                    model.Mota = request.Mota;
                    model.Ghichu = request.Ghichu;
                    model.Thoidiem = request.Thoidiem;
                    model.Thongtin = request.Thongtin;
                    model.Updated_at = DateTime.Now;
                    model.Ipf1 = request.Ipf1;
                    model.Ipf2 = request.Ipf2;
                    model.Ipf3 = request.Ipf3;
                    model.Ipf4 = request.Ipf4;
                    model.Ipf5 = request.Ipf5;
                    _db.GiaPhiLePhi.Update(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaPhiLePhiCt.Where(t => t.Mahs == request.Mahs);

                    _db.GiaPhiLePhiCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaLePhi", new { Madv = request.Madv });
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


        [Route("DinhGiaLePhi/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.lephi.thongtin", "Delete"))
                {
                    var model = _db.GiaPhiLePhi.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaPhiLePhi.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.GiaPhiLePhiCt.Where(t => t.Mahs == model.Mahs);
                    _db.GiaPhiLePhiCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaLePhi", new { Madv = model.Madv });
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

        [Route("DinhGiaLePhi/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.lephi.thongtin", "Show"))
                {
                    var model = _db.GiaPhiLePhi.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new GiaPhiLePhi
                    {
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Thongtin = model.Thongtin,
                        Mota = model.Mota,
                        Ghichu = model.Ghichu,
                    };
                    var model_ct = _db.GiaPhiLePhiCt.Where(t => t.Mahs == Mahs);

                    model_new.GiaPhiLePhiCt = model_ct.ToList();


                    ViewData["Title"] = " Thông tin hồ sơ giá lệ phí trước bạ";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_dglp";
                    ViewData["MenuLv3"] = "menu_dglp_tt";
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();

                    return View("Views/Admin/Manages/DinhGia/GiaLePhi/DanhSach/Show.cshtml", model_new);
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


        [Route("DinhGiaLePhi/Search")]
        [HttpGet]
        public IActionResult Search()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.lephi.timkiem", "Index"))
                {

                    ViewData["GiaPhiLePhiDm"] = _db.GiaPhiLePhiDm.ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");

                    ViewData["Title"] = " Thông tin hồ sơ lệ phí trước bạ";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_dglp";
                    ViewData["MenuLv3"] = "menu_dglp_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaLePhi/TimKiem/Index.cshtml");
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
        [Route("DinhGiaLePhi/Result")]
        [HttpPost]
        public IActionResult Result(double beginPrice, double endPrice, DateTime beginTime, DateTime endTime, string tsp, string dv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.lephi.timkiem", "Edit"))
                {
                    var model_join = from dgct in _db.GiaPhiLePhiCt
                                     join dg in _db.GiaPhiLePhi on dgct.Mahs equals dg.Mahs
                                     select new GiaPhiLePhi
                                     {
                                         Id = dg.Id,
                                         Mahs = dg.Mahs,
                                         Madv = dg.Madv,
                                         Madiaban = dg.Madiaban,
                                         Macqcq = dg.Macqcq,
                                         Thoidiem = dg.Thoidiem,
                                         Mota = dg.Mota,
                                         Phanloai = dgct.Phanloai,
                                         Tennhom = dgct.Ptcp,
                                         Phantram = dgct.Phantram,
                                         Mucthutu = dgct.Mucthutu,
                                         Mucthuden = dgct.Mucthuden,
                                     };
                    if (tsp != "All")
                    {
                        model_join = model_join.Where(t => t.Manhom == tsp);
                    }
                    if (dv != "All")
                    {
                        model_join = model_join.Where(t => t.Madv == dv);
                    }
                    if (beginTime.ToString("yyMMdd") != "010101")
                    {
                        model_join = model_join.Where(t => t.Thoidiem >= beginTime);
                    }
                    if (endTime.ToString("yyMMdd") != "010101")
                    {
                        model_join = model_join.Where(t => t.Thoidiem <= endTime);
                    }
                    if (beginPrice != 0)
                    {
                        model_join = model_join.Where(t => t.Mucthutu >= beginPrice);
                    }
                    if (endPrice != 0)
                    {
                        model_join = model_join.Where(t => t.Mucthuden <= endPrice);
                    }

                    ViewData["GiaPhiLePhiDm"] = _db.GiaPhiLePhiDm.ToList();
                    ViewData["dsdonvi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");


                    ViewData["Title"] = " Thông tin hồ sơ lệ phí trước bạ";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_dglp";
                    ViewData["MenuLv3"] = "menu_dglp_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaLePhi/TimKiem/Result.cshtml", model_join);
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
