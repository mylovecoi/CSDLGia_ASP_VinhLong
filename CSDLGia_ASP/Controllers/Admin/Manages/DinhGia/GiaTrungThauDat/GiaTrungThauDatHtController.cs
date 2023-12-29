using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaTrungThauDat
{
    public class GiaTrungThauDatHtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaTrungThauDatHtController(CSDLGiaDBContext db)
        {
            _db = db;
        }
        [Route("GiaTrungThauDatHt")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.trungthaudat.xetduyet", "Index"))
                {
                    var dsdonvi = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    var dsdiaban = _db.DsDiaBan;

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

                    var getdonvi = (from dv in dsdonvi.Where(t => t.MaDv == Madv)
                                    join db in dsdiaban on dv.MaDiaBan equals db.MaDiaBan
                                    select new VMDsDonVi
                                    {
                                        Id = dv.Id,
                                        MaDiaBan = dv.MaDiaBan,
                                        MaDv = dv.MaDv,
                                        ChucNang = dv.ChucNang,
                                        Level = db.Level,
                                    }).First();


                    var model = _db.GiaDauGiaDat.Where(t => t.Madv_ad == Madv).ToList();
                    if (string.IsNullOrEmpty(Nam))
                    {
                        model = model.ToList();
                    }
                    else
                    {
                        if (Nam != "all")
                        {
                            model = model.Where(t => t.Thoidiem_ad.Year == int.Parse(Nam)).ToList();
                        }
                        else
                        {
                            model = model.ToList();
                        }
                    }
                    var model_join = from dg in model
                                     select new GiaDauGiaDat
                                     {
                                         Id = dg.Id,
                                         Macqcq = Madv,
                                         Madv = dg.Madv,
                                         Madv_t = dg.Madv_t,
                                         Madv_h = dg.Madv_h,
                                         Mahs = dg.Mahs,
                                         Tenduan = dg.Tenduan,
                                         Thoidiem = dg.Thoidiem_ad,
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

                    ViewData["DsDiaBan"] = _db.DsDiaBan;
                    ViewData["Madv"] = Madv;
                    ViewData["Nam"] = Nam;
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Title"] = "Hoàn thành định giá trung thầu quyền sd đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgd";
                    ViewData["MenuLv3"] = "menu_giadgd_ht";
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauDat/HoanThanh/Index.cshtml", model_join);
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.trungthaudat.xetduyet", "Approve"))
                {
                    var model = _db.GiaDauGiaDat.FirstOrDefault(p => p.Mahs == mahs_complete);

                    model.Macqcq = Macqcq;
                    model.Trangthai = "HT";
                    model.Madv_ad = Macqcq;
                    model.Thoidiem_ad = DateTime.Now;
                    model.Trangthai_ad = "CCB";

                    _db.GiaDauGiaDat.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "GiaTrungThauDat", new { Madb = model.Madiaban, Nam = model.Thoidiem.Year });
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

        public IActionResult TraLai(int id_tralai, string madv_tralai)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.trungthaudat.xetduyet", "Approve"))
                {
                    var model = _db.GiaDauGiaDat.FirstOrDefault(t => t.Id == id_tralai);

                    //Gán trạng thái của đơn vị chuyển hồ sơ

                    model.Macqcq = null;
                    model.Trangthai = "BTL";

                    //Gán trạng thái của đơn vị tiếp nhận hồ sơ


                    model.Macqcq_ad = null;
                    model.Madv_ad = null;
                    model.Thoidiem_ad = DateTime.MinValue;
                    model.Trangthai_ad = null;


                    _db.GiaDauGiaDat.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaTrungThauDatHt", new { Madv = madv_tralai, Nam = model.Thoidiem.Year });
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.trungthaudat.xetduyet", "Approve"))
                {
                    var model = _db.GiaDauGiaDat.FirstOrDefault(t => t.Mahs == mahs_cb);

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

                    _db.GiaDauGiaDat.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "GiaTrungThauDatHt");
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.trungthaudat.xetduyet", "Approve"))
                {
                    var model = _db.GiaDauGiaDat.FirstOrDefault(t => t.Mahs == mahs_hcb);

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

                    _db.GiaDauGiaDat.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaTrungThauDatHt");
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
