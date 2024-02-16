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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaTrungThauDat
{
    public class GiaTrungThauDatController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GiaTrungThauDatController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("GiaTrungThauDat")]
        [HttpGet]
        public IActionResult Index(string Madb, string Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.trungthaudat.thongtin", "Index"))
                {

                    var dsdonvi = (from db in _db.DsDiaBan.Where(t => t.Level == "H")
                                   join dv in _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI") on db.MaDiaBan equals dv.MaDiaBan
                                   select new VMDsDonVi
                                   {
                                       Id = dv.Id,
                                       TenDiaBan = db.TenDiaBan,
                                       MaDiaBan = db.MaDiaBan,
                                       MaDv = dv.MaDv
                                   }).ToList();
                    if (dsdonvi.Count > 0)
                    {
                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madb") != null)
                        {
                            Madb = Helpers.GetSsAdmin(HttpContext.Session, "Madb");

                        }
                        else
                        {
                            if (string.IsNullOrEmpty(Madb))
                            {
                                Madb = dsdonvi.OrderBy(t => t.Id).Select(t => t.MaDiaBan).First();

                            }
                        }


                        var model = _db.GiaDauGiaDat.Where(t => t.Madiaban == Madb).ToList();

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

                        ViewData["Madb"] = Madb;
                        ViewData["Nam"] = Nam;
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level == "H");
                        ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                        ViewData["ADMIN"] = _db.DsDiaBan.Where(t => t.Level == "ADMIN");
                        ViewData["Title"] = " Thông tin hồ sơ giá trúng thầu quyền sd đất";
                        ViewData["MenuLv1"] = "menu_giadat";
                        ViewData["MenuLv2"] = "menu_dgd";
                        ViewData["MenuLv3"] = "menu_giadgd_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaTrungThauDat/DanhSach/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Messages"] = "Thông tin hồ sơ giá trúng thầu quyền sd đất.";
                        ViewData["Title"] = " Thông tin hồ sơ giá trúng thầu quyền sd đất";
                        ViewData["MenuLv1"] = "menu_giadat";
                        ViewData["MenuLv2"] = "menu_dgd";
                        ViewData["MenuLv3"] = "menu_giadgd_tt";
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


        [Route("GiaTrungThauDat/Create")]
        [HttpGet]
        public IActionResult Create(string Madiaban)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.trungthaudat.thongtin", "Create"))
                {
                    var model = new GiaDauGiaDat
                    {

                        Thoidiem = DateTime.Now,
                        Mahs = Madiaban + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                    };

                    var dsdv = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI" && t.MaDiaBan == Madiaban).OrderBy(t => t.Id).Select(t => t.MaDv).First();
                    ViewData["DmDvt"] = _db.DmDvt.ToList();
                    ViewData["Madv"] = dsdv;
                    ViewData["Madb"] = Madiaban;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsXaPhuong"] = _db.DsXaPhuong.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["DsDiaBan"] = _db.DsDiaBan;
                    ViewData["Title"] = " Thông tin hồ sơ giá trúng thầu quyền sd đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgd";
                    ViewData["MenuLv3"] = "menu_giadgd_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauDat/DanhSach/Create.cshtml", model);
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


        [Route("GiaTrungThauDat/Store")]
        [HttpPost]
        public async Task<IActionResult> Store(GiaDauGiaDat request, IFormFile Ipf1)
        {

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.trungthaudat.thongtin", "Create"))
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
                    var model = new GiaDauGiaDat
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Madiaban = request.Madiaban,
                        Maxp = request.Maxp,
                        Thoidiem = request.Thoidiem,
                        Tenduan = request.Tenduan,
                        Soqdpagia = request.Soqdpagia,
                        Soqddaugia = request.Soqddaugia,
                        Soqdgiakhoidiem = request.Soqdgiakhoidiem,
                        Soqdkqdaugia = request.Soqdkqdaugia,
                        Thongtin = request.Thongtin,
                        Phanloai = request.Phanloai,
                        Trangthai = "CHT",
                        Congbo = "CHUACONGBO",
                        Ipf1 = request.Ipf1,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaDauGiaDat.Add(model);
                    _db.SaveChanges();


                    return RedirectToAction("Index", "GiaTrungThauDat", new { Madb = request.Madiaban });
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


        [Route("GiaTrungThauDat/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.trungthaudat.thongtin", "Edit"))
                {
                    var model = _db.GiaDauGiaDat.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new GiaDauGiaDat
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        Madiaban = model.Madiaban,
                        Thoidiem = model.Thoidiem,
                        Thongtin = model.Thongtin,
                        Tenduan = model.Tenduan,
                        Soqddaugia = model.Soqddaugia,
                        Soqdgiakhoidiem = model.Soqdgiakhoidiem,
                        Soqdkqdaugia = model.Soqdkqdaugia,
                        Soqdpagia = model.Soqdpagia,
                        Phanloai = model.Phanloai
                    };
                    var model_ct = _db.GiaDauGiaDatCt.Where(t => t.Mahs == model_new.Mahs);

                    model_new.GiaDauGiaDatCt = model_ct.ToList();
                    ViewData["DmDvt"] = _db.DmDvt.ToList();
                    ViewData["DsXaPhuong"] = _db.DsXaPhuong.ToList();
                    ViewData["Maxp"] = model.Maxp;
                    ViewData["Madb"] = model.Madiaban;
                    ViewData["Madv"] = model.Madv;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI").ToList();
                    ViewData["Title"] = " Thông tin hồ sơ giá Trúng thầu quyền sd đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgd";
                    ViewData["MenuLv3"] = "menu_giadgd_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauDat/DanhSach/Edit.cshtml", model_new);
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

        [Route("GiaTrungThauDat/Update")]
        [HttpPost]
        public async Task<IActionResult> Update(GiaDauGiaDat request, IFormFile Ipf1)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.trungthaudat.thongtin", "Edit"))
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
                    var model = _db.GiaDauGiaDat.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Tenduan = request.Tenduan;
                    model.Thoidiem = request.Thoidiem;
                    model.Thongtin = request.Thongtin;
                    model.Maxp = request.Maxp;
                    model.Soqdpagia = request.Soqdpagia;
                    model.Soqddaugia = request.Soqddaugia;
                    model.Soqdkqdaugia = request.Soqdkqdaugia;
                    model.Soqdgiakhoidiem = request.Soqdgiakhoidiem;
                    model.Phanloai = request.Phanloai;
                    model.Ipf1 = request.Ipf1;
                    model.Updated_at = DateTime.Now;
                    _db.GiaDauGiaDat.Update(model);
                    _db.SaveChanges();


                    return RedirectToAction("Index", "GiaTrungThauDat", new { Madb = request.Madiaban });
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


        [Route("GiaTrungThauDat/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.trungthaudat.thongtin", "Delete"))
                {
                    var model = _db.GiaDauGiaDat.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaDauGiaDat.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.GiaDauGiaDatCt.Where(t => t.Mahs == model.Mahs);
                    _db.GiaDauGiaDatCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaTrungThauDat", new { Madb = model.Madiaban });
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

        [Route("GiaTrungThauDat/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.trungthaudat.thongtin", "Show"))
                {
                    var model = _db.GiaDauGiaDat.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new GiaDauGiaDat
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        Madiaban = model.Madiaban,
                        Thoidiem = model.Thoidiem,
                        Thongtin = model.Thongtin,
                        Tenduan = model.Tenduan,
                        Soqddaugia = model.Soqddaugia,
                        Soqdgiakhoidiem = model.Soqdgiakhoidiem,
                        Soqdkqdaugia = model.Soqdkqdaugia,
                        Soqdpagia = model.Soqdpagia,
                        Phanloai = model.Phanloai
                    };
                    var model_ct = _db.GiaDauGiaDatCt.Where(t => t.Mahs == Mahs);

                    model_new.GiaDauGiaDatCt = model_ct.ToList();
                    ViewData["DsXaPhuong"] = _db.DsXaPhuong.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI").ToList();
                    ViewData["Title"] = " Thông tin hồ sơ giá trúng thầu quyền sử dụng đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgd";
                    ViewData["MenuLv3"] = "menu_giadgd_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauDat/DanhSach/Show.cshtml", model_new);
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

        [Route("GiaTrungThauDat/Search")]
        [HttpGet]
        public IActionResult Search()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.daugiadat.timkiem", "Index"))
                {

                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level == "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Title"] = " Thông tin hồ sơ giá trúng thầu quyền sd đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgd";
                    ViewData["MenuLv3"] = "menu_giadgd_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauDat/TimKiem/Index.cshtml");
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

        [Route("GiaTrungThauDat/Result")]
        [HttpPost]
        public IActionResult Result(DateTime beginTime, DateTime endTime, string ten, string dv,
            double Giakhoidiem_den, double Giakhoidiem_tu, double Giadaugia_tu, double Giadaugia_den)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.daugiadat.timkiem", "Index"))
                {

                    var model_join = from dg in _db.GiaDauGiaDat
                                     join dgct in _db.GiaDauGiaDatCt on dg.Mahs equals dgct.Mahs
                                     select new GiaDauGiaDat
                                     {
                                         Id = dg.Id,
                                         Mahs = dg.Mahs,
                                         Madv = dg.Madv,
                                         Tenduan = dg.Tenduan,
                                         Giakhoidiem = dgct.Giakhoidiem,
                                         Giadaugia = dgct.Giadaugia,
                                         Thoidiem = dg.Thoidiem,

                                     };
                    if (ten != null)
                    {
                        model_join = model_join.Where(t => t.Tenduan.Contains(ten));
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
                    if (Giakhoidiem_tu != 0)
                    {
                        model_join = model_join.Where(t => t.Giakhoidiem >= Giakhoidiem_tu);
                    }
                    if (Giakhoidiem_den != 0)
                    {
                        model_join = model_join.Where(t => t.Giakhoidiem <= Giakhoidiem_den);
                    }
                    if (Giadaugia_tu != 0)
                    {
                        model_join = model_join.Where(t => t.Giadaugia >= Giadaugia_tu);
                    }
                    if (Giadaugia_den != 0)
                    {
                        model_join = model_join.Where(t => t.Giadaugia <= Giadaugia_den);
                    }

                    ViewData["dsdonvi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");

                    ViewData["Title"] = " Thông tin hồ sơ giá trúng thầu quyền sửu dụng đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgd";
                    ViewData["MenuLv3"] = "menu_giadgd_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauDat/TimKiem/Result.cshtml", model_join);
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
