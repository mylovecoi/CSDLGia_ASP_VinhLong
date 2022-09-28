using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CSDLGia_ASP.Database;
using System.Security.Cryptography;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaTrungThauHhDv
{
    public class GiaTrungThauHhDvController : Controller
    {

        private readonly CSDLGiaDBContext _db;

        public GiaTrungThauHhDvController(CSDLGiaDBContext db)
        {
            _db = db;
        }
        [Route("GiaTrungThauHhDv")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.mts.giamts.ttg", "Index"))
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


                        var model = _db.GiaMuaTaiSan.Where(t => t.Madv == Madv).ToList();
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

                        ViewData["Title"] = " Thông tin hồ sơ giá trúng thầu hàng hóa dịch vụ";
                        ViewData["MenuLv1"] = "menu_mts";
                        ViewData["MenuLv2"] = "menu_giamts_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaTrungThauHhDv/DanhSach/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Messages"] = "Thông tin hồ sơ giá trúng thầu hàng hóa dịch vụ.";
                        ViewData["Title"] = " Thông tin hồ sơ giá trúng thầu hàng hóa dịch vụ";
                        ViewData["MenuLv1"] = "menu_mts";
                        ViewData["MenuLv2"] = "menu_giamts_tt";
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


        [Route("GiaTrungThauHhDv/Create")]
        [HttpGet]
        public IActionResult Create(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.mts.giamts.ttg", "Create"))
                {

                    var model = new GiaMuaTaiSan
                    {
                        Madv = Madv,
                        Ngayqd = DateTime.Now,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                    };

                    //ViewData["Madb"] = _db.GiaMuaTaiSan.Where(t => t.Madv == Madv).OrderBy(t => t.Id).Select(t => t.Madiaban).First();
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level == "T");
                    ViewData["DmNhomHh"] = _db.DmNhomHh.ToList();

                    ViewData["Title"] = " Thông tin hồ sơ giá trúng thầu hàng hóa dịch vụ";
                    ViewData["MenuLv1"] = "menu_mts";
                    ViewData["MenuLv2"] = "menu_giamts_tt";

                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauHhDv/DanhSach/Create.cshtml", model);
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


        [Route("GiaTrungThauHhDv/Store")]
        [HttpPost]
        public IActionResult Store(GiaMuaTaiSan request)
        {

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.mts.giamts.ttg", "Create"))
                {
                    var model = new GiaMuaTaiSan
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Madiaban = request.Madiaban,
                        Soqd = request.Soqd,
                        Manhom = request.Manhom,
                        Ngayqd = request.Ngayqd,
                        Tennhathau = request.Tennhathau,
                        Thongtinqd = request.Thongtinqd,
                        Ghichu = request.Ghichu,
                        Trangthai = "CHT",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaMuaTaiSan.Add(model);
                    _db.SaveChanges();


                    return RedirectToAction("Index", "GiaTrungThauHhDv", new { Madv = request.Madv });
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


        [Route("GiaTrungThauHhDv/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.mts.giamts.ttg", "Edit"))
                {
                    var model = _db.GiaMuaTaiSan.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new GiaMuaTaiSan
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        Madiaban = model.Madiaban,
                        Soqd = model.Soqd,
                        Manhom = model.Manhom,
                        Ngayqd = model.Ngayqd,
                        Tennhathau = model.Tennhathau,
                        Ghichu = model.Ghichu,
                        Thongtinqd = model.Thongtinqd,
                    };

                    ViewData["DmNhomHh"] = _db.DmNhomHh.ToList();
                    ViewData["Madv"] = model.Madv;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level == "T");

                    ViewData["Title"] = " Thông tin hồ sơ giá trúng thầu hàng hóa dịch vụ";
                    ViewData["MenuLv1"] = "menu_mts";
                    ViewData["MenuLv2"] = "menu_giamts_tt";

                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauHhDv/DanhSach/Edit.cshtml", model_new);
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

        [Route("GiaTrungThauHhDv/Update")]
        [HttpPost]
        public IActionResult Update(GiaMuaTaiSan request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.mts.giamts.ttg", "Edit"))
                {
                    var model = _db.GiaMuaTaiSan.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Madiaban = request.Madiaban;
                    model.Soqd = request.Soqd;
                    model.Ngayqd = request.Ngayqd;
                    model.Manhom = request.Manhom;
                    model.Tennhathau = request.Tennhathau;
                    model.Thongtinqd = request.Thongtinqd;
                    model.Ghichu = request.Ghichu;
                    model.Updated_at = DateTime.Now;
                    _db.GiaMuaTaiSan.Update(model);
                    _db.SaveChanges();


                    return RedirectToAction("Index", "GiaTrungThauHhDv", new { Madv = request.Madv });
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


        [Route("GiaTrungThauHhDv/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.mts.giamts.ttg", "Delete"))
                {
                    var model = _db.GiaMuaTaiSan.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaMuaTaiSan.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaTrungThauHhDv", new { Madv = model.Madv });
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

        [Route("GiaTrungThauHhDv/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.mts.giamts.ttg", "Show"))
                {
                    var model = _db.GiaMuaTaiSan.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new GiaMuaTaiSan
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        Madiaban = model.Madiaban,
                        Soqd = model.Soqd,
                        Manhom = model.Manhom,
                        Ngayqd = model.Ngayqd,
                        Tennhathau = model.Tennhathau,
                        Ghichu = model.Ghichu,
                        Thongtinqd = model.Thongtinqd,
                    };

                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level == "T");

                    ViewData["Title"] = " Thông tin hồ sơ giá trúng thầu hàng hóa dịch vụ";
                    ViewData["MenuLv1"] = "menu_mts";
                    ViewData["MenuLv2"] = "menu_giamts_tt";

                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauHhDv/DanhSach/Show.cshtml", model_new);
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


        [Route("GiaTrungThauHhDv/Search")]
        [HttpGet]
        public IActionResult Search()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.mts.giamts.ttg", "Index"))
                {

                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");

                    ViewData["Title"] = " Thông tin hồ sơ giá trúng thầu mua hàng hóa dịch vụ";
                    ViewData["MenuLv1"] = "menu_mts";
                    ViewData["MenuLv2"] = "menu_giamts_tk";

                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauHhDv/TimKiem/Index.cshtml");
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
        [Route("GiaTrungThauHhDv/Result")]
        [HttpPost]
        public IActionResult Result(DateTime beginTime, DateTime endTime, string ten, string dv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.mts.giamts.ttg", "Edit"))
                {

                    var model = from dg in _db.GiaMuaTaiSan
                                select new GiaMuaTaiSan
                                {
                                    Id = dg.Id,
                                    Mahs = dg.Mahs,
                                    Madv = dg.Madv,
                                    Madiaban = dg.Madiaban,
                                    Macqcq = dg.Macqcq,
                                    Thoidiem = dg.Thoidiem,
                                    Tennhathau = dg.Tennhathau,
                                    Thongtinqd = dg.Thongtinqd,

                                };

                    if (dv != "All")
                    {
                        model = model.Where(t => t.Madv == dv);
                    }
                    if (beginTime.ToString("yyMMdd") != "010101")
                    {
                        model = model.Where(t => t.Thoidiem >= beginTime);

                    }
                    if (endTime.ToString("yyMMdd") != "010101")
                    {
                        model = model.Where(t => t.Thoidiem <= endTime);
                    }


                    ViewData["dsdonvi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");

                    ViewData["Title"] = " Thông tin hồ sơ giá trúng thầu mua hàng hóa dịch vụ";
                    ViewData["MenuLv1"] = "menu_mts";
                    ViewData["MenuLv2"] = "menu_giamts_tk";

                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauHhDv/TimKiem/Result.cshtml", model);
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
