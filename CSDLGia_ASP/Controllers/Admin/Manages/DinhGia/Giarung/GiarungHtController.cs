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
using CSDLGia_ASP.Models.Systems;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.Giarung
{
    public class GiarungHtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiarungHtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaRung/XetDuyet")]
        [HttpGet]

        public IActionResult Index(string Madv, int Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.rung.xetduyet", "Index"))
                {
                    var dsdonvi = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    var dsdiaban = _db.DsDiaBan.Where(t => t.Level != "H");

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


                    /*var getdonvi = (from dv in dsdonvi.Where(t => t.MaDv == Madv)
                                    join db in dsdiaban on dv.MaDiaBan equals db.MaDiaBan
                                    select new VMDsDonVi
                                    {
                                        Id = dv.Id,
                                        MaDiaBan = dv.MaDiaBan,
                                        MaDv = dv.MaDv,
                                        ChucNang = dv.ChucNang,
                                        Level = db.Level,
                                    }).First();*/

                    var getdonvi = (from dv in dsdonvi.Where(t => t.MaDv == Madv)
                                    join db in dsdiaban on dv.MaDiaBan equals db.MaDiaBan
                                    select new VMDsDonVi
                                    {
                                        Id = dv.Id,
                                        MaDiaBan = dv.MaDiaBan,
                                        MaDv = dv.MaDv,
                                        TenDv = dv.TenDv,
                                        ChucNang = dv.ChucNang,
                                        Level = db.Level,
                                    }).First();


                    if (getdonvi.Level == "ADMIN")
                    {
                        var model = _db.GiaRung.Where(t => t.Madv_ad == Madv).ToList();

                        if (Nam != 0)
                        {
                            model = model.Where(t => t.Thoidiem_ad.Year == Nam).ToList();
                        }
                        var model_join = from dg in model
                                         select new VMDinhGiaRung
                                         {
                                             Id = dg.Id,
                                             Macqcq = Madv,
                                             Madv = dg.Madv,
                                             Madv_t = dg.Madv_t,
                                             Madv_h = dg.Madv_h,
                                             Mahs = dg.Mahs,
                                             //Thoidiem = dg.Thoidiem_ad,
                                             Thoidiem = dg.Thoidiem,
                                             Soqd = dg.Soqd,
                                             Madiaban = getdonvi.MaDiaBan,
                                             Trangthai_ad = dg.Trangthai_ad,
                                             Trangthai_t = dg.Trangthai_t,
                                             Trangthai_h = dg.Trangthai_h,
                                             Thongtin = dg.Thongtin,
                                             Level = getdonvi.Level,
                                         };
                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                        {
                            ViewData["DsDonVi"] = dsdonvi;
                        }
                        else
                        {
                            ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.MaDv == Madv);
                        }

                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                        ViewData["Madv"] = Madv;
                        ViewData["Nam"] = Nam;
                        ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                        ViewData["Title"] = "Hoàn thành định giá rừng";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgr";
                        ViewData["MenuLv3"] = "menu_dgr_ht";
                        return View("Views/Admin/Manages/DinhGia/GiaRung/HoanThanh/Index.cshtml", model_join);
                    }
                    else if (getdonvi.Level == "T")
                    {
                        var model = _db.GiaRung.Where(t => t.Madv_t == Madv).ToList();
                        if (Nam != 0)
                        {
                            model = model.Where(t => t.Thoidiem_t.Year == Nam).ToList();
                        }
                        var model_join = from dg in model
                                         select new VMDinhGiaRung
                                         {
                                             Id = dg.Id,
                                             Madv = dg.Madv,
                                             Macqcq = Madv,
                                             Macqcq_t = dg.Macqcq_t,
                                             Macqcq_h = dg.Macqcq_h,
                                             Mahs = dg.Mahs,
                                             //Thoidiem = dg.Thoidiem_t,
                                             Thoidiem = dg.Thoidiem,
                                             Soqd = dg.Soqd,
                                             Madiaban = getdonvi.MaDiaBan,
                                             Trangthai_ad = dg.Trangthai_ad,
                                             Trangthai_t = dg.Trangthai_t,
                                             Trangthai_h = dg.Trangthai_h,
                                             Thongtin = dg.Thongtin,
                                             Level = getdonvi.Level,
                                         };
                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                        {
                            ViewData["DsDonVi"] = dsdonvi;
                        }
                        else
                        {
                            ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.MaDv == Madv);
                        }

                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                        ViewData["Madv"] = Madv;
                        ViewData["Nam"] = Nam;
                        ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                        ViewData["Title"] = "Hoàn thành định giá rừng";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgr";
                        ViewData["MenuLv3"] = "menu_dgr_ht";
                        return View("Views/Admin/Manages/DinhGia/GiaRung/HoanThanh/Index.cshtml", model_join);
                    }
                    else
                    {
                        var model = _db.GiaRung.Where(t => t.Madv_h == Madv).ToList();

                        if (Nam != 0)
                        {
                            model = model.Where(t => t.Thoidiem_h.Year == Nam).ToList();
                        }

                        var model_join = from dg in model
                                         select new VMDinhGiaRung
                                         {
                                             Id = dg.Id,
                                             Madv = dg.Madv,
                                             Macqcq = Madv,
                                             Macqcq_t = dg.Macqcq_t,
                                             Macqcq_h = dg.Macqcq_h,
                                             Mahs = dg.Mahs,
                                             //Thoidiem = dg.Thoidiem_h,
                                             Thoidiem = dg.Thoidiem,
                                             Soqd = dg.Soqd,
                                             Madiaban = getdonvi.MaDiaBan,
                                             Trangthai_ad = dg.Trangthai_ad,
                                             Trangthai_t = dg.Trangthai_t,
                                             Trangthai_h = dg.Trangthai_h,
                                             Thongtin = dg.Thongtin,
                                             Level = getdonvi.Level,
                                         };
                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                        {
                            ViewData["DsDonVi"] = dsdonvi;
                        }
                        else
                        {
                            ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.MaDv == Madv);
                        }

                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                        ViewData["Madv"] = Madv;
                        ViewData["Nam"] = Nam;
                        ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                        ViewData["Title"] = "Hoàn thành định giá rừng";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgr";
                        ViewData["MenuLv3"] = "menu_dgr_ht";
                        return View("Views/Admin/Manages/DinhGia/GiaRung/HoanThanh/Index.cshtml", model_join);
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

        public IActionResult HoanThanh(string mahs_complete, string Macqcq)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.rung.xetduyet", "Approve"))
                {
                    var model = _db.GiaRung.FirstOrDefault(p => p.Mahs == mahs_complete);

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
                    var chk_dvcq = dvcq_join.FirstOrDefault(t => t.MaDv == Macqcq);
                    model.Macqcq = Macqcq;
                    //model.Thoidiem = DateTime.Now;
                    model.Trangthai = "HT";
                    if (chk_dvcq != null && chk_dvcq.Level == "T")
                    {
                        model.Madv_t = Macqcq;
                        model.Thoidiem_t = DateTime.Now;
                        model.Trangthai_t = "CCB";
                    }
                    else if (chk_dvcq != null && chk_dvcq.Level == "ADMIN")
                    {
                        model.Madv_ad = Macqcq;
                        model.Thoidiem_ad = DateTime.Now;
                        model.Trangthai_ad = "CCB";
                    }
                    else
                    {
                        model.Madv_h = Macqcq;
                        model.Thoidiem_h = DateTime.Now;
                        model.Trangthai_h = "CCB";
                    }
                    _db.GiaRung.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Giarung", new { Madv = model.Madv, Nam = model.Thoidiem.Year });
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

        public IActionResult ChuyenHoanThanh(string mahs_complete, string Macqcq, string madv_hientai)
        {

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.rung.xetduyet", "Approve"))
                {
                    var model = _db.GiaRung.FirstOrDefault(p => p.Mahs == mahs_complete);

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
                    var dvchuyen = dvcq_join.FirstOrDefault(t => t.MaDv == madv_hientai);


                    model.Trangthai_ad = "CCB";
                    model.Thoidiem_ad = DateTime.Now;
                    model.Madv_ad = Macqcq;
                    if (dvchuyen != null && dvchuyen.Level == "T")
                    {
                        model.Macqcq_t = Macqcq;
                        model.Trangthai_t = "CCB";
                    }
                    if (dvchuyen != null && dvchuyen.Level == "H")
                    {
                        model.Macqcq_h = Macqcq;
                        model.Trangthai_h = "CCB";
                    }
                    _db.GiaRung.Update(model);
                    _db.SaveChanges();

                    //Xử lý phần lịch sử hồ sơ 
                    var lichSuHoSo = new TrangThaiHoSo
                    {
                        MaHoSo = mahs_complete,
                        TenDangNhap = Helpers.GetSsAdmin(HttpContext.Session, "Name"),
                        MaDonViNhan = Macqcq,
                        ThoiGian = DateTime.Now,
                        TrangThai = "CCB",

                    };
                    _db.TrangThaiHoSo.Add(lichSuHoSo);
                    _db.SaveChanges();

                    //Kết thúc Xử lý phần lịch sử hồ sơ 

                    return RedirectToAction("Index", "GiarungHt", new { Madv = madv_hientai, Nam = model.Thoidiem.Year });
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

        public IActionResult TraLai(int id_tralai, string madv_tralai, string Lydo)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.rung.xetduyet", "Approve"))
                {
                    var model = _db.GiaRung.FirstOrDefault(t => t.Id == id_tralai);

                    //Gán trạng thái của đơn vị chuyển hồ sơ
                    if (madv_tralai == model.Macqcq)
                    {

                        model.Macqcq = null;
                        model.Trangthai = "BTL";
                        model.Lydo = Lydo;
                    }
                    if (madv_tralai == model.Macqcq_h)
                    {
                        model.Macqcq_h = null;
                        model.Trangthai_h = "BTL";
                        model.Lydo = Lydo;
                    }
                    if (madv_tralai == model.Macqcq_t)
                    {
                        model.Macqcq_t = null;
                        model.Trangthai_t = "BTL";
                        model.Lydo = Lydo;
                    }
                    if (madv_tralai == model.Macqcq_ad)
                    {
                        model.Macqcq_ad = null;
                        model.Trangthai_ad = "BTL";
                        model.Lydo = Lydo;
                    }
                    //Gán trạng thái của đơn vị tiếp nhận hồ sơ

                    if (madv_tralai == model.Madv_h)
                    {
                        model.Macqcq_h = null;
                        model.Madv_h = null;
                        model.Thoidiem_h = DateTime.MinValue;
                        model.Trangthai_h = null;
                    }
                    if (madv_tralai == model.Madv_t)
                    {
                        model.Macqcq_t = null;
                        model.Madv_t = null;
                        model.Thoidiem_t = DateTime.MinValue;
                        model.Trangthai_t = null;
                    }
                    if (madv_tralai == model.Madv_ad)
                    {
                        model.Macqcq_ad = null;
                        model.Madv_ad = null;
                        model.Thoidiem_ad = DateTime.MinValue;
                        model.Trangthai_ad = null;
                    }

                    _db.GiaRung.Update(model);
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
                    _db.SaveChanges();

                    //Kết thúc Xử lý phần lịch sử hồ sơ 


                    return RedirectToAction("Index", "GiarungHt", new { Madv = madv_tralai, Nam = model.Thoidiem.Year });
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.tgtc.htg", "Approve"))
                {
                    var model = _db.GiaRung.FirstOrDefault(t => t.Mahs == mahs_cb);

                    model.Thoidiem_ad = DateTime.Now;
                    model.Trangthai_ad = "CB";
                    model.Congbo = "DACONGBO";
                    if (model.Macqcq_h == model.Madv_ad)
                    {
                        model.Thoidiem_h = DateTime.Now;
                        model.Trangthai_h = "CB";
                    }
                    if (model.Macqcq_t == model.Madv_ad)
                    {
                        model.Thoidiem_t = DateTime.Now;
                        model.Trangthai_t = "CB";
                    }

                    _db.GiaRung.Update(model);
                    _db.SaveChanges();

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

                    //Kết thúc Xử lý phần lịch sử hồ sơ 

                    return RedirectToAction("Index", "GiarungHt");
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

        public IActionResult HuyCongBo(string mahs_hcb)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.tgtc.htg", "Approve"))
                {
                    var model = _db.GiaRung.FirstOrDefault(t => t.Mahs == mahs_hcb);

                    model.Thoidiem_ad = DateTime.Now;
                    model.Trangthai_ad = "HCB";
                    model.Congbo = "CHUACONGBO";
                    if (model.Macqcq_h == model.Madv_ad)
                    {
                        model.Thoidiem_h = DateTime.Now;
                        model.Trangthai_h = "HCB";
                    }
                    if (model.Macqcq_t == model.Madv_ad)
                    {
                        model.Thoidiem_t = DateTime.Now;
                        model.Trangthai_t = "HCB";
                    }

                    _db.GiaRung.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiarungHt");
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

        [HttpPost("GiaRung/XetDuyet/TongHop")]
        public IActionResult TongHop(DateTime ngaytu, DateTime ngayden)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.tgtc.htg", "Index"))
                {
                    var model = _db.GiaRung.Where(t => t.Trangthai == "HT" && t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden);
                    List<string> list_donvi = model.Select(t => t.Madv).ToList();
                    var model_donvi = _db.DsDonVi.Where(t => list_donvi.Contains(t.MaDv));
                    List<string> list_hoso = model.Select(t => t.Mahs).ToList();
                    var model_hosoct = _db.GiaRungCt.Where(t => list_hoso.Contains(t.Mahs));

                    ViewData["DonVis"] = model_donvi;
                    ViewData["HoSoCt"] = model_hosoct;
                    ViewData["DanhMuc"] = _db.GiaRungDm;
                    ViewData["ThoiDiemKetXuat"] = "Từ ngày " + Helpers.ConvertDateToStr(ngaytu) + " đến ngày " + Helpers.ConvertDateToStr(ngayden);
                    ViewData["Title"] = "Tỏng hợp giá rừng";
                    return View("Views/Admin/Manages/DinhGia/GiaRung/HoanThanh/Tonghop.cshtml", model);

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
