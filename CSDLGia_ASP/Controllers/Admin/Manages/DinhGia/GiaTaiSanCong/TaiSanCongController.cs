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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaTaiSanCong
{
    public class TaiSanCongController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public TaiSanCongController(CSDLGiaDBContext db)
        {
            _db = db;
        }
        [Route("GiaTaiSanCong")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.tsc.giatsc.ttg", "Index"))
                {

                    var dsdonvi = (from db in _db.DsDiaBan
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


                        var model = _db.GiaTaiSanCong.Where(t => t.Madv == Madv).ToList();
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
                        ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                        ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");

                        ViewData["Title"] = " Thông tin hồ sơ giá tài sản công";
                        ViewData["MenuLv1"] = "menu_tsc";
                        ViewData["MenuLv2"] = "menu_giatsc_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaTaiSanCong/DanhSach/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Messages"] = "Thông tin hồ sơ giá tài sản công.";
                        ViewData["Title"] = " Thông tin hồ sơ giá tài sản công";
                        ViewData["MenuLv1"] = "menu_tsc";
                        ViewData["MenuLv2"] = "menu_giatsc_tt";
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


        [Route("GiaTaiSanCong/Create")]
        [HttpGet]
        public IActionResult Create(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.tsc.giatsc.ttg", "Create"))
                {

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaTaiSanCong
                    {
                        Madv = Madv,
                        Thoidiem = DateTime.Now,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                    };

                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "ADMIN");

                    ViewData["Title"] = " Thông tin hồ sơ giá tài sản công";
                    ViewData["MenuLv1"] = "menu_tsc";
                    ViewData["MenuLv2"] = "menu_giatsc_tt";

                    return View("Views/Admin/Manages/DinhGia/GiaTaiSanCong/DanhSach/Create.cshtml", model);
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


        [Route("GiaTaiSanCong/Store")]
        [HttpPost]
        public IActionResult Store(CSDLGia_ASP.Models.Manages.DinhGia.GiaTaiSanCong request)
        {

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.tsc.giatsc.ttg", "Create"))
                {
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaTaiSanCong
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Madiaban = request.Madiaban,
                        Soqd = request.Soqd,
                        Thoidiem = request.Thoidiem,
                        Thongtin = request.Thongtin,
                        Trangthai = "CHT",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaTaiSanCong.Add(model);
                    _db.SaveChanges();


                    return RedirectToAction("Index", "TaiSanCong", new { Madv = request.Madv });
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


        [Route("GiaTaiSanCong/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.tsc.giatsc.ttg", "Edit"))
                {
                    var model = _db.GiaTaiSanCong.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new CSDLGia_ASP.Models.Manages.DinhGia.GiaTaiSanCong
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        Madiaban = model.Madiaban,
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Thongtin = model.Thongtin,
                    };

                    var model_ct = _db.GiaTaiSanCongCt.Where(t => t.Mahs == model_new.Mahs);

                    model_new.GiaTaiSanCongCt = model_ct.ToList();

                    ViewData["Madv"] = model.Madv;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Title"] = " Thông tin hồ sơ giá tài sản công";
                    ViewData["MenuLv1"] = "menu_tsc";
                    ViewData["MenuLv2"] = "menu_giatsc_tt";


                    return View("Views/Admin/Manages/DinhGia/GiaTaiSanCong/DanhSach/Edit.cshtml", model_new);
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

        [Route("GiaTaiSanCong/Update")]
        [HttpPost]
        public IActionResult Update(CSDLGia_ASP.Models.Manages.DinhGia.GiaTaiSanCong request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.tsc.giatsc.ttg", "Edit"))
                {
                    var model = _db.GiaTaiSanCong.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Madiaban = request.Madiaban;
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Thongtin = request.Thongtin;
                    model.Updated_at = DateTime.Now;
                    _db.GiaTaiSanCong.Update(model);
                    _db.SaveChanges();


                    return RedirectToAction("Index", "TaiSanCong", new { Madv = request.Madv });
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


        [Route("GiaTaiSanCong/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.tsc.giatsc.ttg", "Delete"))
                {
                    var model = _db.GiaTaiSanCong.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaTaiSanCong.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.GiaTaiSanCongCt.Where(t => t.Mahs == model.Mahs);
                    _db.GiaTaiSanCongCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "TaiSanCong", new { Madv = model.Madv });
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

        [Route("GiaTaiSanCong/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.tsc.giatsc.ttg", "Show"))
                {
                    var model = _db.GiaTaiSanCong.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new CSDLGia_ASP.Models.Manages.DinhGia.GiaTaiSanCong
                    {
                        Madiaban = model.Madiaban,
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Thongtin = model.Thongtin,
                    };
                    var model_ct = _db.GiaTaiSanCongCt.Where(t => t.Mahs == Mahs);

                    model_new.GiaTaiSanCongCt = model_ct.ToList();

                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Title"] = " Thông tin hồ sơ giá tài sản công";
                    ViewData["MenuLv1"] = "menu_tsc";
                    ViewData["MenuLv2"] = "menu_giatsc_tt";


                    return View("Views/Admin/Manages/DinhGia/GiaTaiSanCong/DanhSach/Show.cshtml", model_new);
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


        [Route("GiaTaiSanCong/Search")]
        [HttpGet]
        public IActionResult Search()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.tsc.giatsc.ttg", "Index"))
                {

                    ViewData["DsDiaBan"] = _db.DsDiaBan;
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");

                    ViewData["Title"] = " Thông tin hồ sơ giá tài sản công";
                    ViewData["MenuLv1"] = "menu_tsc";
                    ViewData["MenuLv2"] = "menu_giatsc_tk";

                    return View("Views/Admin/Manages/DinhGia/GiaTaiSanCong/TimKiem/Index.cshtml");
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
        [Route("GiaTaiSanCong/Result")]
        [HttpPost]
        public IActionResult Result(double beginPrice, double endPrice, DateTime beginTime, DateTime endTime, string ten, string dv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.tsc.giatsc.ttg", "Edit"))
                {

                    var model_join = from dg in _db.GiaTaiSanCong
                                     join dgct in _db.GiaTaiSanCongCt on dg.Mahs equals dgct.Mahs
                                     select new CSDLGia_ASP.Models.Manages.DinhGia.GiaTaiSanCong
                                     {
                                         Id = dg.Id,
                                         Mahs = dg.Mahs,
                                         Madv = dg.Madv,
                                         Madiaban = dg.Madiaban,
                                         Macqcq = dg.Macqcq,
                                         Thoidiem = dg.Thoidiem,
                                         Tentaisan = dgct.Tentaisan,
                                         Dacdiem = dgct.Dacdiem,
                                         Giathue = dgct.Giathue,
                                         Giaconlai = dgct.Giaconlai,
                                         Giapheduyet = dgct.Giapheduyet,
                                         Giaban = dgct.Giaban,
                                     };
                    if (ten != null)
                    {
                        model_join = model_join.Where(t => t.Tentaisan.Contains(ten));
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
                        model_join = model_join.Where(t => t.Giathue >= beginPrice);
                    }
                    if (endPrice != 0)
                    {
                        model_join = model_join.Where(t => t.Giathue <= endPrice);
                    }

                    ViewData["dsdonvi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");

                    ViewData["Title"] = " Thông tin hồ sơ giá tài sản công";
                    ViewData["MenuLv1"] = "menu_tsc";
                    ViewData["MenuLv2"] = "menu_giatsc_tk";

                    return View("Views/Admin/Manages/DinhGia/GiaTaiSanCong/TimKiem/Result.cshtml", model_join);
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
