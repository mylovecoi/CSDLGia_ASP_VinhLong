using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDatPl
{
    public class GiaDatPlHtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaDatPlHtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaDatCuThe/XetDuyet")]
        [HttpGet]
        public IActionResult Index(int Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthe.xetduyet", "Index"))
                {
                    //var dsdonvi = _db.DsDonVi;
                    //var dsdiaban = _db.DsDiaBan;

                    //if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") != null)
                    //{
                    //    Madv = Helpers.GetSsAdmin(HttpContext.Session, "Madv");

                    //}
                    //else
                    //{
                    //    if (string.IsNullOrEmpty(Madv))
                    //    {
                    //        Madv = dsdonvi.OrderBy(t => t.Id).Select(t => t.MaDv).First();
                    //    }
                    //}

                    //var getdonvi = (from dv in dsdonvi.Where(t => t.MaDv == Madv)
                    //                join db in dsdiaban on dv.MaDiaBan equals db.MaDiaBan
                    //                select new VMDsDonVi
                    //                {
                    //                    Id = dv.Id,
                    //                    MaDiaBan = dv.MaDiaBan,
                    //                    MaDv = dv.MaDv,
                    //                    TenDv = dv.TenDv,
                    //                    ChucNang = dv.ChucNang,
                    //                    Level = db.Level,
                    //                }).First();

                    //var model = _db.GiaDatPhanLoai.ToList();

                    //if (getdonvi.Level == "H")
                    //{
                    //    if (string.IsNullOrEmpty(Nam))
                    //    {
                    //        model = model.Where(t => t.Madv_h == Madv).ToList();
                    //    }
                    //    else
                    //    {
                    //        if (Nam != "all")
                    //        {
                    //            //model = model.Where(t => t.Thoidiem_h.Year == int.Parse(Nam) && t.Madv_h == Madv).ToList();
                    //            model = model.Where(t => t.Thoidiem.Year == int.Parse(Nam) && t.Madv_h == Madv).ToList();
                    //        }
                    //        else
                    //        {
                    //            model = model.Where(t => t.Madv_h == Madv).ToList();
                    //        }
                    //    }

                    //    var model_new = (from kk in model
                    //                     select new CSDLGia_ASP.ViewModels.Manages.DinhGia.VMDinhGiaDat
                    //                     {
                    //                         Id = kk.Id,
                    //                         Mahs = kk.Mahs,
                    //                         MadvCh = GetMadvChuyen(Madv, kk),
                    //                         Macqcq = Madv,
                    //                         Madv = kk.Madv_h,
                    //                         //Thoidiem = kk.Thoidiem_h,
                    //                         Thoidiem = kk.Thoidiem,
                    //                         Thongtin = kk.Thongtin,
                    //                         Trangthai = kk.Trangthai_h,
                    //                         Soqd = kk.Soqd,
                    //                         Level = getdonvi.Level,
                    //                     });

                    //    var model_join = (from kkj in model_new
                    //                      join dv in dsdonvi on kkj.MadvCh equals dv.MaDv
                    //                      select new CSDLGia_ASP.ViewModels.Manages.DinhGia.VMDinhGiaDat
                    //                      {
                    //                          Id = kkj.Id,
                    //                          Mahs = kkj.Mahs,
                    //                          MadvCh = kkj.MadvCh,
                    //                          TendvCh = dv.TenDv,
                    //                          Macqcq = kkj.Macqcq,
                    //                          Madv = kkj.Madv,
                    //                          Thoidiem = kkj.Thoidiem,
                    //                          Thongtin = kkj.Thongtin,
                    //                          Trangthai = kkj.Trangthai,
                    //                          Soqd = kkj.Soqd,
                    //                          Level = kkj.Level,
                    //                      });

                    //    if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                    //    {
                    //        ViewData["DsDonVi"] = dsdonvi;
                    //    }
                    //    else
                    //    {
                    //        ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.MaDv == Madv);
                    //    }



                    List<string> list_trangthai = new List<string> { "CD", "HT", "DD", "CB" };
                    var model = _db.GiaDatPhanLoai.Where(t => list_trangthai.Contains(t.Trangthai));
                    if (Nam != 0)
                    {
                        model = model.Where(t => t.Thoidiem.Year == Nam);
                    }
                    var model_join = from dg in model
                                     join donvi in _db.DsDonVi on dg.Madv equals donvi.MaDv
                                     select new VMDinhGiaDat
                                     {
                                         Id = dg.Id,
                                         Madv = dg.Madv,
                                         Ghichu = dg.Ghichu,
                                         Mahs = dg.Mahs,
                                         Thoidiem = dg.Thoidiem,
                                         Soqd = dg.Soqd,
                                         TenDv = donvi.TenDv,
                                         Trangthai = dg.Trangthai,
                                         Thongtin = dg.Thongtin,
                                     };

                    ViewData["Nam"] = Nam;
                    ViewData["Title"] = "Xét duyệt giá đất cụ thể";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgdct";
                    ViewData["MenuLv3"] = "menu_dgdct_ht";
                    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/HoanThanh/Index.cshtml", model_join);
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
        [HttpPost]
        public IActionResult Duyet(string mahs_duyet)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthe.xetduyet", "Approve"))
                {
                    var model = _db.GiaDatPhanLoai.FirstOrDefault(p => p.Mahs == mahs_duyet);

                    model.Updated_at = DateTime.Now;
                    model.Trangthai = "DD";

                    _db.GiaDatPhanLoai.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "GiaDatPlHt", new { Nam = model.Thoidiem.Year });
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

        [HttpPost]
        public IActionResult HuyDuyet(string mahs_huyduyet)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthe.xetduyet", "Approve"))
                {
                    var model = _db.GiaDatPhanLoai.FirstOrDefault(p => p.Mahs == mahs_huyduyet);

                    model.Updated_at = DateTime.Now;
                    model.Trangthai = "CD";
                    _db.GiaDatPhanLoai.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "GiaDatPlHt", new { Nam = model.Thoidiem.Year });
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
        [HttpPost]
        public IActionResult TraLai(int id_tralai, string Lydo)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthe.thongtin", "Approve"))
                {
                    var model = _db.GiaDatPhanLoai.FirstOrDefault(t => t.Id == id_tralai);                   

                    model.Trangthai = "BTL";
                    model.Lydo = Lydo;
                    model.Updated_at = DateTime.Now;                  

                    _db.GiaDatPhanLoai.Update(model);
                    _db.SaveChanges();

                    // Xử lý phần lịch sử hồ sơ 

                    var lichSuHoSo = new TrangThaiHoSo
                    {
                        MaHoSo = model.Mahs,
                        TenDangNhap = Helpers.GetSsAdmin(HttpContext.Session, "Name"),
                        ThoiGian = DateTime.Now,
                        TrangThai = "BTL",

                    };
                    _db.TrangThaiHoSo.Add(lichSuHoSo);

                    _db.GiaDatPhanLoai.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaDatPlHt", new { Nam = model.Thoidiem.Year });
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

        public IActionResult CongBo(string mahs_cb)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthe.xetduyet", "Approve"))
                {
                    var model = _db.GiaDatPhanLoai.FirstOrDefault(t => t.Mahs == mahs_cb);

                    model.Updated_at = DateTime.Now;
                    model.Trangthai = "CB";
                    _db.GiaDatPhanLoai.Update(model);                    

                    // Xử lý phần lịch sử hồ sơ 

                    var lichSuHoSo = new TrangThaiHoSo
                    {
                        MaHoSo = mahs_cb,
                        TenDangNhap = Helpers.GetSsAdmin(HttpContext.Session, "Name"),
                        ThoiGian = DateTime.Now,
                        TrangThai = "CB",
                    };
                    _db.TrangThaiHoSo.Add(lichSuHoSo);
                    _db.SaveChanges();
                    //model.Thoidiem_ad = DateTime.Now;
                    //model.Trangthai_ad = "CB";
                    //model.Congbo = "DACONGBO";
                    //if (model.Macqcq_h == model.Madv_ad)
                    //{
                    //    model.Thoidiem_h = DateTime.Now;
                    //    model.Trangthai_h = "CB";
                    //}
                    //if (model.Macqcq_t == model.Madv_ad)
                    //{
                    //    model.Thoidiem_t = DateTime.Now;
                    //    model.Trangthai_t = "CB";
                    //}

                    //_db.GiaDatPhanLoai.Update(model);
                    //_db.SaveChanges();
                    //ViewData["MenuLv1"] = "menu_giadat";
                    //ViewData["MenuLv2"] = "menu_dgdct";
                    //ViewData["MenuLv3"] = "menu_dgdct_ht";
                    return RedirectToAction("Index", "GiaDatPlHt", new { Nam = model.Thoidiem.Year });
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

        public IActionResult HuyCongBo(string mahs_hcb, string trangthai_hcb)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.datcuthe.xetduyet", "Approve"))
                {
                    var model = _db.GiaDatPhanLoai.FirstOrDefault(t => t.Mahs == mahs_hcb);

                    model.Trangthai = "DD";
                    model.Updated_at = DateTime.Now;

                    _db.GiaDatPhanLoai.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaDatPlHt", new { Nam = model.Thoidiem.Year });
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
