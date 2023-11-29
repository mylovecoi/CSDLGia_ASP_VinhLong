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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaGiaoDichBDS
{
    public class GiaGiaoDichBDSController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaGiaoDichBDSController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaGiaoDichBDS")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.thongtin", "Index") || Helpers.GetSsAdmin(HttpContext.Session, "Level") == "T")
                {
                    List<CSDLGia_ASP.Models.Manages.DinhGia.GiaGiaoDichBDS> model = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaGiaoDichBDS>();
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
                                model = _db.GiaGiaoDichBDS.Where(t => t.Madv == Madv && t.Thoidiem.Year == int.Parse(Nam)).ToList();
                            }
                            else
                            {
                                model = _db.GiaGiaoDichBDS.Where(t => t.Madv == Madv && t.Thoidiem.Year == int.Parse(Nam)).ToList();
                            }
                            ViewData["DsDonVi"] = dsdonvi.Where(t => t.MaDv == Madv);
                        }

                        ViewData["Nam"] = Nam;
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                        ViewData["DsDiaBanAll"] = _db.DsDiaBan;
                        ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                        ViewData["Donvi"] = Madv;
                        ViewData["Title"] = " Thông tin hồ sơ giá thuê mặt dất mặt nước";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dggdbds";
                        ViewData["MenuLv3"] = "menu_dggdbds_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaGiaoDichBDS/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Title"] = "Thông tin hồ sơ giá thuê mặt đất mặt nước";
                        ViewData["Messages"] = "Thông tin hồ sơ giá thuê mặt dất mặt nước.";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dggdbds";
                        ViewData["MenuLv3"] = "menu_dggdbds_tt";
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

        [Route("GiaGiaoDichBDS/Create")]
        [HttpGet]
        public IActionResult Create(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.thongtin", "Create"))
                {

                    var model = new VMDinhGiaGiaoDichBDS
                    {
                        Madv = Madv,
                        Thoidiem = DateTime.Now,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                    };
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Title"] = "Thêm mới giá thuê mặt đất mặt nước";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dggdbds";
                    ViewData["MenuLv3"] = "menu_dggdbds_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichBDS/Create.cshtml", model);
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

        [Route("GiaGiaoDichBDS/Store")]
        [HttpPost]
        public IActionResult Store(VMDinhGiaGiaoDichBDS request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.thongtin", "Create"))
                {
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaGiaoDichBDS
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Madiaban = request.Madiaban,
                        Soqd = request.Soqd,
                        Thoidiem = request.Thoidiem,
                        Thongtin = request.Thongtin,
                        Ghichu = request.Ghichu,
                        Trangthai = "CHT",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaGiaoDichBDS.Add(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaGiaoDichBDSCt.Where(t => t.Mahs == request.Mahs);
                    _db.GiaGiaoDichBDSCt.UpdateRange(modelct);
                    _db.SaveChanges();
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dggdbds";
                    ViewData["MenuLv3"] = "menu_dggdbds_tt";
                    return RedirectToAction("Index", "GiaGiaoDichBDS", new { request.Madv });
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

        [Route("GiaGiaoDichBDS/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.thongtin", "Delete"))
                {
                    var model = _db.GiaGiaoDichBDS.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaGiaoDichBDS.Remove(model);
                    _db.SaveChanges();
                    var model_ct = _db.GiaGiaoDichBDSCt.Where(t => t.Mahs == model.Mahs);
                    _db.GiaGiaoDichBDSCt.RemoveRange(model_ct);
                    _db.SaveChanges();
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmdmn";
                    ViewData["MenuLv3"] = "menu_dgtmdmn_tt";
                    return RedirectToAction("Index", "GiaGiaoDichBDS", new { model.Madv });
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

        [Route("GiaGiaoDichBDS/Modify")]
        [HttpGet]
        public IActionResult Modify(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.thongtin", "Edit"))
                {
                    var model = _db.GiaGiaoDichBDS.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new VMDinhGiaGiaoDichBDS
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        Madiaban = model.Madiaban,
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Thongtin = model.Thongtin,
                        Ghichu = model.Ghichu
                    };

                    var model_ct = _db.GiaGiaoDichBDSCt.Where(t => t.Mahs == Mahs);

                    model_new.GiaGiaoDichBDSCt = model_ct.ToList();

                    ViewData["Madv"] = model.Madv;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Title"] = "Chỉnh sửa giá thuê mặt dất mặt nước";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dggdbds";
                    ViewData["MenuLv3"] = "menu_dggdbds_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichBDS/Modify.cshtml", model_new);
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

        [Route("GiaGiaoDichBDS/Update")]
        [HttpPost]
        public IActionResult Update(VMDinhGiaGiaoDichBDS request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.thongtin", "Edit"))
                {
                    var model = _db.GiaGiaoDichBDS.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Madiaban = request.Madiaban;
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Thongtin = request.Thongtin;
                    model.Ghichu = request.Ghichu;
                    model.Updated_at = DateTime.Now;
                    _db.GiaGiaoDichBDS.Update(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaGiaoDichBDSCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Mahs = model.Mahs;
                        }
                    }
                    _db.GiaGiaoDichBDSCt.UpdateRange(modelct);
                    _db.SaveChanges();
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dggdbds";
                    ViewData["MenuLv3"] = "menu_dggdbds_tt";
                    return RedirectToAction("Index", "GiaGiaoDichBDS", new { request.Mahs });
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

        [Route("GiaGiaoDichBDS/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.thongtin", "Edit"))
                {
                    var model = _db.GiaGiaoDichBDS.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new VMDinhGiaGiaoDichBDS
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        Madiaban = model.Madiaban,
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Thongtin = model.Thongtin,
                        Ghichu = model.Ghichu
                    };


                    var model_ct = _db.GiaGiaoDichBDSCt.Where(t => t.Mahs == model_new.Mahs);

                    model_new.GiaGiaoDichBDSCt = model_ct.ToList();

                    ViewData["Madv"] = model.Madv;
                    ViewData["Mahs"] = model.Mahs;

                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Title"] = "Chi tiết giá thuê mặt dất mặt nước";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dggdbds";
                    ViewData["MenuLv3"] = "menu_dggdbds_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichBDS/Show.cshtml", model_new);
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

        [Route("GiaGiaoDichBDS/Search")]
        [HttpGet]
        public IActionResult Search()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.timkiem", "Index"))
                {

                    var model_join = from dgct in _db.GiaGiaoDichBDSCt
                                     join dg in _db.GiaGiaoDichBDS.Where(t => t.Trangthai == "HT") on dgct.Mahs equals dg.Mahs
                                     select new VMDinhGiaGiaoDichBDSCt
                                     {
                                         Id = dg.Id,
                                         Mahs = dg.Mahs,
                                         Madv = dg.Madv,
                                         Vitri = dgct.Vitri,
                                         Dongia = dgct.Dongia,
                                         Macqcq = dg.Macqcq,
                                         Thoidiem = dg.Thoidiem,
                                         Diemdau = dgct.Diemdau,
                                         Diemcuoi = dgct.Diemcuoi,
                                         Dientich = dgct.Dientich,
                                     };

                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Title"] = "Tìm kiếm thông tin định giá thuê mặt đất mặt nước";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dggdbds";
                    ViewData["MenuLv3"] = "menu_dggdbds_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichBDS/TimKiem/Index.cshtml", model_join);

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

        [Route("GiaGiaoDichBDS/Result")]
        [HttpPost]
        public IActionResult Result(DateTime beginTime, DateTime endTime, double beginPrice, double endPrice, int vitri, string madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.timkiem", "Edit"))
                {
                    var model = from dgct in _db.GiaGiaoDichBDSCt
                                join dg in _db.GiaGiaoDichBDS on dgct.Mahs equals dg.Mahs
                                select new VMDinhGiaGiaoDichBDSCt
                                {
                                    Id = dg.Id,
                                    Mahs = dg.Mahs,
                                    Madv = dg.Madv,
                                    Vitri = dgct.Vitri,
                                    Dongia = dgct.Dongia,
                                    Macqcq = dg.Macqcq,
                                    Thoidiem = dg.Thoidiem,
                                    Diemdau = dgct.Diemdau,
                                    Diemcuoi = dgct.Diemcuoi,
                                    Dientich = dgct.Dientich,
                                };
                    if (madv != "All")
                    {
                        model = model.Where(t => t.Madv == madv);
                    }
                    if (vitri != 0)
                    {
                        model = model.Where(t => t.Vitri == vitri);
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
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dggdbds";
                    ViewData["MenuLv3"] = "menu_dggdbds_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichBDS/TimKiem/Result.cshtml", model);
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
