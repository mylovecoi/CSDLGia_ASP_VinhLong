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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaSpDichVuCongIch
{
    public class GiaDichVuCongIchController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaDichVuCongIchController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DinhGiaDvCi")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.dvci.ttg", "Index"))
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



                        var model = _db.GiaSpDvCi.Where(t => t.Madv == Madv ).ToList();

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

                        ViewData["Title"] = " Thông tin hồ sơ sản phẩm dịch vụ công ích";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgdvci";
                        ViewData["MenuLv3"] = "menu_dgdvci_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaSpDichVuCongIch/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Title"] = " Thông tin hồ sơ sản phẩm dịch vụ công ích";
                        ViewData["Messages"] = "Thông tin hồ sơ sản phẩm dịch vụ công ích.";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgdvci";
                        ViewData["MenuLv3"] = "menu_dgdvci_tt";
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
        [Route("DinhGiaDvCi/Create")]
        [HttpGet]
        public IActionResult Create(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.dvci.ttg", "Create"))
                {

                    var model = new VMDinhGiaDvCi
                    {
                        Madv = Madv,
                        Thoidiem = DateTime.Now,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                    };

                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDiaBan"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["GiaSpDvCiDm"] = _db.GiaSpDvCiDm.ToList();

                    ViewData["Title"] = " Thông tin hồ sơ sản phẩm dịch vụ công ích";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdvci";
                    ViewData["MenuLv3"] = "menu_dgdvci_tt";

                    return View("Views/Admin/Manages/DinhGia/GiaSpDichVuCongIch/Create.cshtml", model);
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

        [Route("DinhGiaDvCi/Store")]
        [HttpPost]
        public IActionResult Store(VMDinhGiaDvCi request)
        {

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.dvci.ttg", "Create"))
                {
                    var model = new GiaSpDvCi
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
                    _db.GiaSpDvCi.Add(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaSpDvCiCt.Where(t => t.Mahs == request.Mahs);
                    _db.GiaSpDvCiCt.UpdateRange(modelct);
                    _db.SaveChanges();

                 

                    return RedirectToAction("Index", "GiaDichVuCongIch", new { Madv = request.Madv });
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


        [Route("DinhGiaDvCi/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.dvci.ttg", "Edit"))
                {
                    var model = _db.GiaSpDvCi.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new VMDinhGiaDvCi
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        Madiaban = model.Madiaban,
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Thongtin = model.Thongtin,
                    };

                    var model_ct = _db.GiaSpDvCiCt.Where(t => t.Mahs == model_new.Mahs);

                    model_new.GiaSpDvCiCt = model_ct.ToList();

                    ViewData["Madv"] = model.Madv;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["GiaSpDvCiDm"] = _db.GiaSpDvCiDm.ToList();
                    ViewData["Title"] = " Thông tin hồ sơ sản phẩm dịch vụ công ích";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdvci";
                    ViewData["MenuLv3"] = "menu_dgdvci_tt";

                    return View("Views/Admin/Manages/DinhGia/GiaSpDichVuCongIch/Edit.cshtml", model_new);
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

        [Route("DinhGiaDvCi/Update")]
        [HttpPost]
        public IActionResult Update(VMDinhGiaDvCi request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.dvci.ttg", "Edit"))
                {
                    var model = _db.GiaSpDvCi.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Madiaban = request.Madiaban;
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Thongtin = request.Thongtin;
                    model.Updated_at = DateTime.Now;
                    _db.GiaSpDvCi.Update(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaSpDvCiCt.Where(t => t.Mahs == request.Mahs);

                    _db.GiaSpDvCiCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaDichVuCongIch", new { Madv = request.Madv });
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


        [Route("DinhGiaDvCi/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.dvci.ttg", "Delete"))
                {
                    var model = _db.GiaSpDvCi.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaSpDvCi.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.GiaSpDvCiCt.Where(t => t.Mahs == model.Mahs);
                    _db.GiaSpDvCiCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaDichVuCongIch", new { Madv = model.Madv });
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

        [Route("DinhGiaDvCi/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.dvci.ttg", "Show"))
                {
                    var model = _db.GiaSpDvCi.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new VMDinhGiaDvCi
                    {
                        Madiaban = model.Madiaban,
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Thongtin = model.Thongtin,
                    };
                    var model_ct = _db.GiaSpDvCiCt.Where(t => t.Mahs == Mahs);

                    model_new.GiaSpDvCiCt = model_ct.ToList();

                    ViewData["GiaSpDvCiDm"] = _db.GiaSpDvCiDm.ToList();

                    ViewData["Title"] = " Thông tin hồ sơ sản phẩm dịch vụ công ích";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdvci";
                    ViewData["MenuLv3"] = "menu_dgdvci_tt";

                    return View("Views/Admin/Manages/DinhGia/GiaSpDichVuCongIch/Show.cshtml", model_new);
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


        [Route("DinhGiaDvCi/Search")]
        [HttpGet]
        public IActionResult Search()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.tgtc.ttg", "Index"))
                {

                    ViewData["GiaSpDvCiDm"] = _db.GiaSpDvCiDm.ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");

                    ViewData["Title"] = " Thông tin hồ sơ sản phẩm dịch vụ công ích";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdvci";
                    ViewData["MenuLv3"] = "menu_dgdvci_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDichVuCongIch/TimKiem/Index.cshtml");
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
        [Route("DinhGiaDvCi/Result")]
        [HttpPost]
        public IActionResult Result(double beginPrice, double endPrice, DateTime beginTime, DateTime endTime, string tsp, string dv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.tgtc.ttg", "Edit"))
                {
                    var model_join = from dgct in _db.GiaTroGiaTroCuocCt
                                     join dg in _db.GiaTroGiaTroCuoc on dgct.Mahs equals dg.Mahs
                                     join dgdm in _db.GiaTroGiaTroCuocDm on dgct.Maspdv equals dgdm.Maspdv
                                     select new VMDinhGiaTroGiaTroCuoc
                                     {
                                         Id = dg.Id,
                                         Mahs = dg.Mahs,
                                         Madv = dg.Madv,
                                         Madiaban = dg.Madiaban,
                                         Macqcq = dg.Macqcq,
                                         Thoidiem = dg.Thoidiem,
                                         Maspdv = dgct.Maspdv,
                                         Phanloai = dgdm.Phanloai,
                                         Dongia = dgct.Dongia,
                                     };
                    if (tsp != "All")
                    {
                        model_join = model_join.Where(t => t.Maspdv == tsp);
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
                        model_join = model_join.Where(t => t.Dongia >= beginPrice);
                    }
                    if (endPrice != 0)
                    {
                        model_join = model_join.Where(t => t.Dongia <= endPrice);
                    }

                    ViewData["GiaSpDvCiDm"] = _db.GiaSpDvCiDm.ToList();
                    ViewData["dsdonvi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");

                    
                    ViewData["Title"] = "Kết quả tìm kiếm thông tin sản phẩm dịch vụ công ích";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtgtc";
                    ViewData["MenuLv3"] = "menu_dgtgtc_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDichVuCongIch/TimKiem/Result.cshtml", model_join);
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
