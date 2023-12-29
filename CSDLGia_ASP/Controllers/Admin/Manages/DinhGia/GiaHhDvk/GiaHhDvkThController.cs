using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaHhDvk
{
    public class GiaHhDvkThController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GiaHhDvkThController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("GiaHhDvk/TongHop")]
        [HttpGet]
        public IActionResult Index(string Thang, string Nam, string Matt)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.th", "Index"))
                {
                    var dsnhom = _db.GiaHhDvkNhom.ToList();
                    if (dsnhom.Count > 0)
                    {
                        var model = _db.GiaHhDvkTh.ToList();
                        var hoso = _db.GiaHhDvk.ToList();
                        if (string.IsNullOrEmpty(Matt))
                        {
                            Matt = dsnhom.OrderBy(t => t.Id).Select(t => t.Matt).First();
                            model = _db.GiaHhDvkTh.Where(t => t.Matt == Matt).ToList();
                            hoso = _db.GiaHhDvk.Where(t => t.Matt == Matt).ToList();
                        }
                        else
                        {
                            model = _db.GiaHhDvkTh.Where(t => t.Matt == Matt).ToList();
                            hoso = _db.GiaHhDvk.Where(t => t.Matt == Matt).ToList();
                        }

                        if (string.IsNullOrEmpty(Nam))
                        {
                            Nam = Helpers.ConvertYearToStr(DateTime.Now.Year);
                            model = model.Where(t => t.Nam == Nam).ToList();
                            hoso = hoso.Where(t => t.Nam == Nam).ToList();
                        }
                        else
                        {
                            if (Nam != "all")
                            {
                                model = model.Where(t => t.Nam == Nam).ToList();
                                hoso = hoso.Where(t => t.Nam == Nam).ToList();
                            }
                            else
                            {
                                model = model.ToList();
                                hoso = hoso.ToList();
                            }
                        }

                        if (string.IsNullOrEmpty(Thang))
                        {
                            Thang = Helpers.ConvertYearToStr(DateTime.Now.Month);
                            model = model.Where(t => t.Thang == Thang).ToList();
                            hoso = hoso.Where(t => t.Thang == Thang).ToList();
                        }
                        else
                        {
                            if (Thang != "all")
                            {
                                model = model.Where(t => t.Thang == Thang).ToList();
                                hoso = hoso.Where(t => t.Thang == Thang).ToList();
                            }
                            else
                            {
                                model = model.ToList();
                                hoso = hoso.ToList();
                            }
                        }

                        var model_join = (from th in model
                                          join nhom in dsnhom on th.Matt equals nhom.Matt
                                          select new GiaHhDvkTh
                                          {
                                              Id = th.Id,
                                              Mahs = th.Mahs,
                                              Matt = th.Matt,
                                              Tentt = nhom.Tentt,
                                              Ngaybc = th.Ngaybc,
                                              Ngaychotbc = th.Ngaychotbc,
                                              Sobc = th.Sobc,
                                              Ttbc = th.Ttbc,
                                              Thang = th.Thang,
                                              Nam = th.Nam,
                                              Trangthai = th.Trangthai,
                                          }).ToList();

                        ViewData["Thang"] = Thang;
                        ViewData["Nam"] = Nam;
                        ViewData["Matt"] = Matt;
                        ViewData["Dsnhom"] = dsnhom;
                        ViewData["Hoso"] = hoso;
                        ViewData["model"] = model_join;
                        ViewData["DsDiaBan"] = _db.DsDiaBan;
                        ViewData["Title"] = "Tổng hợp giá hàng hóa dịch vụ khác";
                        ViewData["MenuLv1"] = "menu_hhdvk";
                        ViewData["MenuLv2"] = "menu_hhdvk_th";
                        return View("Views/Admin/Manages/DinhGia/GiaHhDvk/TongHop/Index.cshtml", model_join);
                    }
                    else
                    {
                        ViewData["Title"] = "Tổng hợp giá hàng hóa dịch vụ khác";
                        ViewData["Messages"] = "Hệ thống chưa có danh mục nhóm hàng hóa dịch vụ.";
                        ViewData["MenuLv1"] = "menu_hhdvk";
                        ViewData["MenuLv2"] = "menu_hhdvk_th";
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

        [Route("GiaHhDvk/TongHop/Create")]
        [HttpGet]
        public IActionResult Create(string Matt, string Thang, string Nam, string[] Hoso)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.th", "Create"))
                {
                    //var model = _db.GiaHhDvk.Where(t => Hoso.Contains(t.Mahs)).ToList();
                    var hhCt = _db.GiaHhDvkCt.Where(t => Hoso.Contains(t.Mahs) && t.Gia != 0).ToList();
                    var modelHh = _db.GiaHhDvkDm.Where(x => x.Matt == Matt);
                    var model = new GiaHhDvkTh
                    {
                        Mahs = DateTime.Now.ToString("yyMMddssmmHH"),
                        Matt = Matt,
                        Trangthai = "CHT",
                        Thang = Thang,
                        Nam = Nam,
                        Ttbc = "Tổng hợp số liệu tháng " + Thang + " năm " + Nam,
                        Mahstonghop = string.Join(",", Hoso)
                    };
                    _db.GiaHhDvkTh.Add(model);
                    foreach (var hh in modelHh)
                    {
                        //double ttgia = modelCt.Where(x => x.Mahhdv == dm.mah).Select(x => x.Gia).Average();
                        var modelCt = new GiaHhDvkCtTh();
                        modelCt.Mahs = model.Mahs;
                        modelCt.Mahhdv = hh.Mahhdv;
                        modelCt.Tenhhdv = hh.Tenhhdv;
                        modelCt.Dacdiemkt = hh.Dacdiemkt;
                        modelCt.Xuatxu = hh.Xuatxu;
                        modelCt.Dvt = hh.Dvt;
                        modelCt.Loaigia = "Giá bán lẻ";
                        modelCt.Nguontt = "Do cơ quan/đơn vị quản lý nhà nước có liên quan cung cấp/báo cáo theo quy định";
                        modelCt.Created_at = DateTime.Now;
                        modelCt.Updated_at = DateTime.Now;
                        modelCt.Gia = hhCt.Where(x => x.Mahhdv == hh.Mahhdv).Average(x => x.Gia);
                        modelCt.Gialk = hhCt.Where(x => x.Mahhdv == hh.Mahhdv).Average(x => x.Gialk);

                        _db.GiaHhDvkCtTh.Add(modelCt);
                    }
                    _db.SaveChanges();
                    ViewBag.model = model;
                    ViewData["modelCt"] = _db.GiaHhDvkCtTh.Where(x => x.Mahs == model.Mahs);
                    ViewData["modelNhom"] = _db.GiaHhDvkNhom.FirstOrDefault(x => x.Matt == Matt);
                    ViewData["modelHh"] = modelHh;
                    //ViewBag.Mahs = Hoso.ToArray();
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["Thang"] = model.Thang;
                    ViewData["Nam"] = model.Nam;
                    ViewData["Matt"] = model.Matt;
                    ViewData["Title"] = "Tổng hợp giá hàng hóa dịch vụ khác thêm mới";
                    ViewData["MenuLv1"] = "menu_hhdvk";
                    ViewData["MenuLv2"] = "menu_hhdvk_th";
                    return View("Views/Admin/Manages/DinhGia/GiaHhDvk/TongHop/Create.cshtml", model);

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
        [Route("GiaHhDvk/TongHop/Store")]
        [HttpPost]
        public IActionResult Store(string Mahs, string Matt, string Thang, string Nam, string Sobc, DateTime Ngaybc, DateTime Ngaychotbc, string Ghichu)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.th", "Create"))
                {

                    var modelSave = _db.GiaHhDvkTh.FirstOrDefault(x => x.Mahs == Mahs);
                    modelSave.Sobc = Sobc;
                    modelSave.Ngaybc = Ngaybc;
                    modelSave.Ngaychotbc = Ngaychotbc;
                    modelSave.Ghichu = Ghichu;
                    _db.GiaHhDvkTh.Update(modelSave);
                    _db.SaveChanges();
                    var model = _db.GiaHhDvkTh.Where(x => x.Matt == Matt);
                    return RedirectToAction("Index", "GiaHhDvkTh", new { Thang = Thang, Nam = Nam, Matt = Matt });

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
        [Route("GiaHhDvk/TongHop/Edit")]
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.th", "Create"))
                {
                    var model = _db.GiaHhDvkTh.FirstOrDefault(x => x.Id == Id);
                    var modelCt = _db.GiaHhDvkCtTh.Where(x => x.Mahs == model.Mahs);
                    ViewBag.model = model;
                    ViewData["modelCt"] = modelCt;
                    ViewData["Matt"] = model.Matt;
                    ViewData["Title"] = "Tổng hợp giá hàng hóa dịch vụ khác chỉnh sửa";
                    ViewData["MenuLv1"] = "menu_hhdvk";
                    ViewData["MenuLv2"] = "menu_hhdvk_th";
                    return View("Views/Admin/Manages/DinhGia/GiaHhDvk/TongHop/Edit.cshtml");

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
        [Route("GiaHhDvk/TongHop/Update")]
        [HttpPost]
        public IActionResult Update(string Mahs, string Matt, string Thang, string Nam, string Sobc, DateTime Ngaybc, DateTime Ngaychotbc, string Ghichu)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.th", "Create"))
                {

                    var modelSave = _db.GiaHhDvkTh.FirstOrDefault(x => x.Mahs == Mahs);
                    modelSave.Sobc = Sobc;
                    modelSave.Ngaybc = Ngaybc;
                    modelSave.Ngaychotbc = Ngaychotbc;
                    modelSave.Ghichu = Ghichu;
                    _db.GiaHhDvkTh.Update(modelSave);
                    _db.SaveChanges();
                    var model = _db.GiaHhDvkTh.Where(x => x.Matt == Matt);
                    return RedirectToAction("Index", "GiaHhDvkTh", new { Thang = Thang, Nam = Nam, Matt = Matt });

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
        [Route("GiaHhDvk/TongHop/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.th", "Delete"))
                {
                    var model = _db.GiaHhDvkTh.FirstOrDefault(t => t.Id == id_delete);
                    var Thang = model.Thang;
                    var Nam = model.Nam;
                    var Matt = model.Matt;
                    _db.GiaHhDvkTh.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaHhDvkTh", new { Thang = Thang, Nam = Nam, Matt = Matt });
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
        [Route("GiaHhDvk/TongHop/CreateHs")]
        [HttpGet]
        public IActionResult CreateHs(string Mahs, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.tt", "Create"))
                {
                    var getRecordTh = _db.GiaHhDvkTh.FirstOrDefault(x => x.Mahs == Mahs);
                    var getMadb = _db.DsDonVi.FirstOrDefault(x => x.MaDv == Madv).MaDiaBan;
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvk();
                    model.Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH");
                    model.Madiaban = Madv;
                    model.Matt = getRecordTh.Matt;
                    model.Soqd = getRecordTh.Sobc;
                    model.Thoidiem = getRecordTh.Ngaybc;
                    model.Madiaban = getMadb;
                    model.Madv = Madv;
                    model.Trangthai = "CHT";
                    model.Thang = getRecordTh.Thang;
                    model.Nam = getRecordTh.Nam;
                    _db.GiaHhDvk.Add(model);

                    var getModelThCt = _db.GiaHhDvkCtTh.Where(x => x.Mahs == Mahs);
                    foreach (var item in getModelThCt)
                    {
                        var modelCt = new GiaHhDvkCt();
                        modelCt.Mahs = model.Mahs;
                        modelCt.Mahhdv = item.Mahhdv;
                        modelCt.Loaigia = "Giá bán lẻ";
                        modelCt.Nguontt = "Do cơ quan/đơn vị quản lý nhà nước có liên quan cung cấp/báo cáo theo quy định";
                        modelCt.Gia = item.Gia;
                        modelCt.Gialk = item.Gialk;
                    }
                    _db.SaveChanges();
                    return RedirectToAction("Index", "GiaHhDvkTh", new { Thang = getRecordTh.Thang, Nam = getRecordTh.Nam, Matt = getRecordTh.Matt });
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
