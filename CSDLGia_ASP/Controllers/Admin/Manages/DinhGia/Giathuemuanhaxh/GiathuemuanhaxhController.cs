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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.Giathuemuanhaxh
{
    public class GiathuemuanhaxhController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiathuemuanhaxhController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DinhGiaThueMuaNhaXaHoi")]
        [HttpGet]
        public IActionResult Index(string Donvi, string Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanha.thongtin", "Index"))
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
                                       TenDv = dv.TenDv
                                   }).ToList();
                    if (dsdonvi.Count > 0)
                    {
                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") != null)
                        {
                            Donvi = Helpers.GetSsAdmin(HttpContext.Session, "Madv");
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(Donvi))
                            {
                                //Madv = dsdonvi.OrderBy(t => t.Id).Select(t => t.MaDv).First();
                                model = _db.GiaThueMuaNhaXh.Where(t => t.Thoidiem.Year == int.Parse(Nam)).ToList();
                            }
                            else
                            {
                                model = _db.GiaThueMuaNhaXh.Where(t => t.Macqcq == Donvi && t.Thoidiem.Year == int.Parse(Nam)).ToList();
                            }
                        }
                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                        {
                            ViewData["DsDonVi"] = dsdonvi;
                        }
                        else
                        {
                            ViewData["DsDonVi"] = dsdonvi.Where(t => t.MaDiaBan == Donvi);
                        }
                        ViewData["Nam"] = Nam;
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                        ViewData["DsDiaBanAll"] = _db.DsDiaBan.ToList();
                        ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                        ViewData["Donvi"] = Donvi;
                        //ViewData["Loairung"]=_db.GiaThueMuaNhaXhDm.ToList();
                        ViewData["Title"] = " Thông tin hồ sơ giá rừng";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgtmnxh";
                        ViewData["MenuLv3"] = "menu_dgtmnxh_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaThueMuaNhaXh/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Title"] = "Thông tin hồ sơ giá thuê mua nhà xã hội";
                        ViewData["Messages"] = "Thông tin hồ sơ giá thuê mua nhà xã hội.";
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
        public IActionResult Create()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanha.thongtin", "Create"))
                {
                    var dsdonvi = (from com in _db.Company
                                   join lvkd in _db.CompanyLvCc.Where(t => t.Manghe == "RUNG") on com.Mahs equals lvkd.Mahs
                                   select new VMCompany
                                   {
                                       Id = com.Id,
                                       Manghe = lvkd.Manghe,
                                       Madv = com.Madv,
                                       Madiaban = com.Madiaban,
                                       Mahs = com.Mahs,
                                       Tendn = com.Tendn,
                                       Trangthai = com.Trangthai
                                   }).FirstOrDefault();

                    var model = new VMDinhGiaThueMuaNhaXh
                    {
                        Madv = dsdonvi.Madv,
                        Thoidiem=DateTime.Now, 
                        Mahs= dsdonvi.Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                    };
                    ViewData["Mahs"] = dsdonvi.Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH");
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Tennha"]=_db.GiaThueMuaNhaXhDm.ToList();
                    ViewData["Title"] = "Thêm mới giá thuê mua nhà xã hội";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmnxh";
                    ViewData["MenuLv3"] = "menu_dgtmnxh_tm";

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
        public IActionResult Store(VMDinhGiaThueMuaNhaXh request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanha.thongtin", "Create"))
                {
                    var model = new GiaThueMuaNhaXh
                    {
                        Mahs=request.Mahs,
                        Madv = request.Madv,
                        Madiaban=request.Madiaban,
                        Soqd = request.Soqd,
                        Thoidiem = request.Thoidiem,
                        Thongtin = request.Thongtin,
                        Ghichu=request.Ghichu,
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanha.thongtin", "Delete"))
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanha.thongtin", "Edit"))
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
                        Ghichu=model.Ghichu
                    };

                    var model_ct = _db.GiaThueMuaNhaXhCt.Where(t => t.Mahs == model_new.Mahs);

                    model_new.GiaThueMuaNhaXhCt = model_ct.ToList();

                    ViewData["Madv"] = model.Madv;
                    ViewData["Mahs"] = model.Mahs;
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanha.thongtin", "Edit"))
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
        
        [Route("DinhGiaThueMuaNhaXaHoi/Search")]
        [HttpGet]
        public IActionResult Search()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanha.thongtin", "Index"))
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
        public IActionResult Result(DateTime beginTime,DateTime endTime,double beginPrice,double endPrice,string tn,string dv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuemuanha.thongtin", "Edit"))
                {
                    var model_join = from dgct in _db.GiaThueMuaNhaXhCt
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
                                         Maso=dgct.Maso,
                                         Tennha=dgdm.Tennha,
                                         Dvt =dgct.Dvt,
                                         Phanloai=dgct.Phanloai,
                                     };
                    model_join.Where(t => t.Maso == tn
                                    && t.Macqcq == dv
                                    && t.Thoidiem >= beginTime
                                    && t.Thoidiem <= endTime
                                    && t.Dongia >= beginPrice
                                    && t.Dongia <= endPrice);
                    /*VMDinhGiaThueMuaNhaXhSearch vm = new VMDinhGiaThueMuaNhaXhSearch();
                    foreach(var item in model) {
                        vm.GiaThueMuaNhaXhCt = _db.GiaThueMuaNhaXhCt.Where(t => t.Mahs == item.Mahs);
                    }*/
                    ViewData["Tennha"] = _db.GiaThueMuaNhaXhDm.ToList();
                    return View("Views/Admin/Manages/DinhGia/GiaThueMuaNhaXh/TimKiem/Result.cshtml",model_join);
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
