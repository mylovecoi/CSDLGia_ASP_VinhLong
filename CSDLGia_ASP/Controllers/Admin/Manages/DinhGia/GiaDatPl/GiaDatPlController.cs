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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDatPl
{
    public class GiaDatPlController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaDatPlController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DinhGiaDatCuThe")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.datcuthe.thongtin", "Index"))
                {
                    var dsdonvi = (from db in _db.DsDiaBan.Where(t => t.Level != "H")
                                   join dv in _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI") on db.MaDiaBan equals dv.MaDiaBan
                                   select new VMDsDonVi
                                   {
                                       Id = dv.Id,
                                       TenDiaBan = db.TenDiaBan,
                                       MaDiaBan = dv.MaDiaBan,
                                       TenDv = dv.TenDv,
                                       MaDv = dv.MaDv,
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

                        var model = _db.GiaDatPhanLoai.Where(t => t.Madv == Madv).ToList();

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
                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                        {
                            ViewData["DsDonVi"] = dsdonvi;
                        }
                        else
                        {
                            ViewData["DsDonVi"] = dsdonvi.Where(t => t.MaDv == Madv);
                        }
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                        ViewData["Nam"] = Nam;
                        ViewData["DsDiaBanAll"] = _db.DsDiaBan;
                        ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                        ViewData["Madv"] = Madv;
                        ViewData["Title"] = " Thông tin hồ sơ giá đất cụ thể";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgdct";
                        ViewData["MenuLv3"] = "menu_dgdct_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Title"] = "Thông tin hồ sơ giá đất cụ thể";
                        ViewData["Messages"] = "Thông tin hồ sơ giá đất cụ thể.";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgdct";
                        ViewData["MenuLv3"] = "menu_dgdct_tt";
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

        [Route("DinhGiaDatCuThe/Create")]
        [HttpGet]
        public IActionResult Create(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.datcuthe.thongtin", "Create"))
                {
                    var model = new VMDinhGiaDat
                    {
                        Madv = Madv,
                        Thoidiem=DateTime.Now, 
                        Mahs= Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                    };
                    ViewData["Madv"] = Madv;
                    ViewData["Mahs"] = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH");
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Maloaidat"] = _db.DmLoaiDat.ToList();
                    //ViewData["Maloaidat"] = _db.GiaDatPhanLoaiDm.ToList();
                    ViewData["Title"] = "Thêm mới giá đát cụ thể";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdct";
                    ViewData["MenuLv3"] = "menu_dgdct_tm";

                    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Create.cshtml", model);
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

        [Route("DinhGiaDatCuThe/Store")]
        [HttpPost]
        public IActionResult Store(VMDinhGiaDat request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.datcuthe.thongtin", "Create"))
                {
                    var model = new GiaDatPhanLoai
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
                    _db.GiaDatPhanLoai.Add(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaDatPhanLoaiCt.Where(t => t.Mahs == request.Mahs);
                    _db.GiaDatPhanLoaiCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaDatPl", new { request.Mahs });
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

        [Route("DinhGiaDatCuThe/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.datcuthe.thongtin", "Delete"))
                {
                    var model = _db.GiaDatPhanLoai.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaDatPhanLoai.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.GiaDatPhanLoaiCt.Where(t => t.Mahs == model.Mahs);
                    _db.GiaDatPhanLoaiCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaDatPl", new { model.Madv });
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

        [Route("DinhGiaDatCuThe/Modify")]
        [HttpGet]
        public IActionResult Modify(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.datcuthe.thongtin", "Edit"))
                {
                    var model = _db.GiaDatPhanLoai.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new VMDinhGiaDat
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        Madiaban = model.Madiaban,
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Thongtin = model.Thongtin,
                        Ghichu=model.Ghichu
                    };

                    var model_ct = _db.GiaDatPhanLoaiCt.Where(t => t.Mahs == model_new.Mahs);

                    model_new.GiaDatPhanLoaiCt = model_ct.ToList();

                    ViewData["Madv"] = model.Madv;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Maloaidat"] = _db.DmLoaiDat.ToList();
                    ViewData["Title"] = "Chỉnh sửa giá đất cụ thể";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdct";
                    ViewData["MenuLv3"] = "menu_dgdct_tt";

                    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Modify.cshtml", model_new);
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

        [Route("DinhGiaDatCuThe/Update")]
        [HttpPost]
        public IActionResult Update(VMDinhGiaDat request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.datcuthe.thongtin", "Edit"))
                {
                    var model = _db.GiaDatPhanLoai.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Madiaban = request.Madiaban;
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Thongtin = request.Thongtin;
                    model.Ghichu = request.Ghichu;
                    model.Updated_at = DateTime.Now;
                    _db.GiaDatPhanLoai.Update(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaDatPhanLoaiCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Mahs = model.Mahs;
                        }
                    }
                    _db.GiaDatPhanLoaiCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaDatPl", new { request.Mahs });
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

        [Route("DinhGiaDatCuThe/Print")]
        [HttpGet]
        public IActionResult Print(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.datcuthe.thongtin", "Index"))
                {
                    var model = _db.GiaDatPhanLoai.FirstOrDefault(t => t.Mahs == Mahs);
                    var hoso_dg = new VMDinhGiaPrint
                    {
                        Id = model.Id,
                        Mahs = model.Mahs,
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Vitri=model.Vitri,
                        Ghichu=model.Ghichu,
                    };

                    var modeldv = _db.DsDonVi.FirstOrDefault(t => t.MaDv == model.Macqcq);
                    if (modeldv != null)
                    {
                        hoso_dg.Tendv = modeldv.TenDvHienThi;
                    }
                    var modeldb = _db.DsDiaBan.FirstOrDefault(t => t.MaDiaBan == modeldv.MaDiaBan);
                    if (modeldb != null)
                    {
                        hoso_dg.Tendb = modeldb.TenDiaBan;
                    }
                    var modelct = _db.GiaDatPhanLoaiCt.Where(t => t.Mahs == model.Mahs);
                    if (modelct != null)
                    {
                        hoso_dg.GiaDatPhanLoaiCt = modelct.ToList();
                    }

                    /*var model = GetThongTinKk(Mahs);*/

                    ViewData["Title"] = "In định giá đât cụ thể";
                    ViewData["Maloaidat"] = _db.GiaDatPhanLoaiDm.ToList();
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdct";
                    ViewData["MenuLv3"] = "menu_dgdct_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/Print.cshtml", hoso_dg);

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

        [Route("DinhGiaDatCuThe/Search")]
        [HttpGet]
        public IActionResult Search()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.datcuthe.thongtin", "Index"))
                {

                    var model_join = from dgct in _db.GiaDatPhanLoaiCt
                                     join dg in _db.GiaDatPhanLoai.Where(t =>  t.Trangthai == "HT") on dgct.Mahs equals dg.Mahs
                                     join dgdm in _db.GiaDatPhanLoaiDm on dgct.Maloaidat equals dgdm.Maloaidat
                                     select new VMDinhGiaDatCt
                                     {
                                         Id = dg.Id,
                                         Mahs = dg.Mahs,
                                         Madv = dg.Madv,
                                         Giatri = dgdm.Giatri,
                                         Tenvitri = dgdm.Tenvitri,
                                         Dientich = dgdm.Dientich,
                                         Thoidiem = dg.Thoidiem,
                                         Maloaidat = dgct.Maloaidat,
                                         Macqcq = dg.Macqcq,
                                         Loaidat = dgdm.Loaidat
                                     };

                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Maloaidat"] = _db.DmLoaiDat.ToList();
                    ViewData["Title"] = "Tìm kiếm thông tin định giá đất cụ thể";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdct";
                    ViewData["MenuLv3"] = "menu_dgdct_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/TimKiem/Index.cshtml", model_join);

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

        [Route("DinhGiaDatCuThe/Result")]
        [HttpPost]
        public IActionResult Result(DateTime beginTime,DateTime endTime,double beginPrice,double endPrice,string mld,string madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.datcuthe.thongtin", "Edit"))
                {
                    var model = (from giact in _db.GiaDatPhanLoaiCt
                                 join gia in _db.GiaDatPhanLoai on giact.Mahs equals gia.Mahs
                                 join dm in _db.DmLoaiDat on giact.Maloaidat equals dm.Maloaidat
                                 join donvi in _db.DsDonVi on gia.Madv equals donvi.MaDv
                                 select new GiaDatPhanLoaiCt
                                 {
                                     Id = giact.Id,
                                     Madv = gia.Madv,
                                     Tendv = donvi.TenDv,
                                     Mahs = giact.Mahs,
                                     Thoidiem = gia.Thoidiem,
                                     Giacuthe=giact.Giacuthe,
                                     Vitri=giact.Vitri,
                                     Loaidat=dm.Loaidat,
                                     Dientich=gia.Dientich,
                                     Maloaidat=giact.Maloaidat
                                 });

                    if (madv != "All")
                    {
                        model = model.Where(t => t.Madv == madv);
                    }
                    if (mld != "All")
                    {
                        model = model.Where(t => t.Maloaidat == mld);
                    }
                    if (beginTime.ToString("yyMMdd") != "010101")
                    {
                        model = model.Where(t => t.Thoidiem >= beginTime);
                    }

                    if (endTime.ToString("yyMMdd") != "010101")
                    {
                        model = model.Where(t => t.Thoidiem <= endTime);
                    }
                    model = model.Where(t => t.Giacuthe >= beginPrice);
                    if (endPrice > 0)
                    {
                        model = model.Where(t => t.Giacuthe <= endPrice);
                    }
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá tài sản trong tố tụng hình sự";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdct";
                    ViewData["MenuLv3"] = "menu_dgdct_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaDatPhanLoai/TimKiem/Result.cshtml", model);
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

        public IActionResult Complete(string mahs_chuyen, string macqcq_chuyen)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.datcuthe.thongtin", "Index"))
                {
                    var model = _db.GiaDatPhanLoai.FirstOrDefault(t => t.Mahs == mahs_chuyen);

                    var dvcq_join = from dvcq in _db.DsDonVi
                                    join db in _db.DsDiaBan on dvcq.MaDiaBan equals db.MaDiaBan
                                    select new VMDsDonVi
                                    {
                                        Id = dvcq.Id,
                                        MaDiaBan = dvcq.MaDiaBan,
                                        MaDv = dvcq.MaDv,
                                        TenDv = dvcq.TenDv,
                                        Level = db.Level,
                                    };
                    var chk_dvcq = dvcq_join.FirstOrDefault(t => t.MaDv == macqcq_chuyen);
                    model.Macqcq = macqcq_chuyen;
                    model.Trangthai = "HT";
                    if (chk_dvcq != null && chk_dvcq.Level == "T")
                    {
                        model.Madv_t = macqcq_chuyen;
                        model.Thoidiem_t = DateTime.Now;
                        model.Trangthai_t = "CHT";
                    }
                    else if (chk_dvcq != null && chk_dvcq.Level == "ADMIN")
                    {
                        model.Madv_ad = macqcq_chuyen;
                        model.Thoidiem_ad = DateTime.Now;
                        model.Trangthai_ad = "CHT";
                    }
                    else
                    {
                        model.Madv_h = macqcq_chuyen;
                        model.Thoidiem_h = DateTime.Now;
                        model.Trangthai_h = "CHT";
                    }
                    _db.GiaDatPhanLoai.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaDatPl", new { model.Madv });

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
