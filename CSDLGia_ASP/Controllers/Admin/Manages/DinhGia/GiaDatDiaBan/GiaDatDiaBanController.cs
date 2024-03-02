using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDatDiaBan
{
    public class GiaDatDiaBanController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaDatDiaBanController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaDatDiaBan")]
        [HttpGet]
        public IActionResult Index(string Nam, string madiaban)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giadatdb.thongtin", "Index") || Helpers.GetSsAdmin(HttpContext.Session, "Level") == "T")
                {


                    // Check madiaban
                    if (Helpers.GetSsAdmin(HttpContext.Session, "madiaban") != null)
                    {
                        madiaban = Helpers.GetSsAdmin(HttpContext.Session, "madiaban");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(madiaban))
                        {
                            madiaban = _db.DsDiaBan.OrderBy(t => t.Id).Select(t => t.MaDiaBan).First();
                        }
                    }

                    var model = _db.GiaDatDiaBan.Where(t => t.Madiaban == madiaban).ToList();
                    //Check nam
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

                    ViewData["Dsdonvi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["DsDiaBan1"] = _db.DsDiaBan.Where(t => t.Level == "H");
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["DsDv"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Soqd"] = _db.GiaDatDiaBanTt.ToList();
                    ViewData["Nam"] = Nam;
                    ViewData["madiaban"] = madiaban;
                    ViewData["Title"] = " Thông tin hồ sơ";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_giadatdiaban";
                    ViewData["MenuLv3"] = "menu_giadatdiaban_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaDatDiaBan/Index.cshtml", model);


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


        [Route("GiaDatDiaBan/Create")]
        [HttpGet]
        public IActionResult Create(string soqd, string madiaban, string madv)
        {

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giadatdb.thongtin", "Create"))
                {
                    var model = _db.GiaDatDiaBan.Where(t => t.Soqd == soqd && t.Madiaban == madiaban).FirstOrDefault();

                    if (model == null)
                    {
                        var ndqd = _db.GiaDatDiaBanTt.Where(t => t.Soqd == soqd).FirstOrDefault();
                        var m_qd = new CSDLGia_ASP.Models.Manages.DinhGia.GiaDatDiaBan
                        {
                            Noidung = ndqd.Mota,
                            Soqd = soqd,
                            Madiaban = madiaban,
                            Madv = madv,
                            Mahs = madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                            Thoidiem = DateTime.Now,

                        };
                        ViewData["Khuvuc"] = _db.GiaDatDiaBanCt.ToList();
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H").ToList();
                        ViewData["MaDiaBan"] = madiaban;
                        ViewData["Dsloaidat"] = _db.DmLoaiDat.ToList();
                        ViewData["DsXaPhuong"] = _db.DsXaPhuong.ToList();
                        ViewData["Title"] = "Thông tin hồ sơ giá đất";
                        ViewData["MenuLv1"] = "menu_giadat";
                        ViewData["MenuLv2"] = "menu_giadatdiaban";
                        ViewData["MenuLv3"] = "menu_giadatdiaban_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaDatDiaBan/Create.cshtml", m_qd);

                    }
                    else if (model != null && model.Trangthai != "HT")

                    {
                        ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                        var model_ct = _db.GiaDatDiaBanCt.Where(t => t.Mahs == model.Mahs).ToList();
                        model.GiaDatDiaBanCt = model_ct;
                        ViewData["Title"] = "Thông tin hồ sơ giá đất";
                        ViewData["MenuLv1"] = "menu_giadat";
                        ViewData["MenuLv2"] = "menu_giadatdiaban";
                        ViewData["MenuLv3"] = "menu_giadatdiaban_tt";
                        ViewData["Dsloaidat"] = _db.DmLoaiDat.ToList();
                        ViewData["DsXaPhuong"] = _db.DsXaPhuong.ToList();

                        return View("Views/Admin/Manages/DinhGia/GiaDatDiaBan/Edit.cshtml", model);
                    }
                    else
                    {
                        ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                        var model_ct = _db.GiaDatDiaBanCt.Where(t => t.Mahs == model.Mahs).ToList();
                        model.GiaDatDiaBanCt = model_ct;
                        ViewData["Title"] = "Thông tin hồ sơ giá đất";
                        ViewData["MenuLv1"] = "menu_giadat";
                        ViewData["MenuLv2"] = "menu_giadatdiaban";
                        ViewData["MenuLv3"] = "menu_giadatdiaban_tt";
                        ViewData["Dsloaidat"] = _db.DmLoaiDat.ToList();
                        ViewData["DsXaPhuong"] = _db.DsXaPhuong.ToList();
                        return View("Views/Admin/Manages/DinhGia/GiaDatDiaBan/Show.cshtml", model);
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


        [Route("GiaDatDiaBan/Store")]
        [HttpPost]
        public IActionResult Store(CSDLGia_ASP.Models.Manages.DinhGia.GiaDatDiaBan request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giadatdb.thongtin", "Create"))
                {

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaDatDiaBan
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Soqd = request.Soqd,
                        Madiaban = request.Madiaban,
                        Thoidiem = request.Thoidiem,
                        Trangthai = "CHT",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };

                    var modelct = _db.GiaDatDiaBanCt.Where(t => t.Mahs == request.Mahs);

                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Trangthai = "XD";
                        }
                    }

                    _db.GiaDatDiaBan.Add(model);
                    _db.SaveChanges();

                    var model_ct2 = _db.GiaDatDiaBanCt.Where(t => t.Trangthai == "CXD");
                    _db.GiaDatDiaBanCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaDatDiaBan", new { request.Madv });
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


        [Route("GiaDatDiaBan/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giadatdb.thongtin", "Delete"))
                {
                    var model = _db.GiaDatDiaBan.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaDatDiaBan.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.GiaDatDiaBanCt.Where(t => t.Mahs == model.Mahs);
                    _db.GiaDatDiaBanCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaDatDiaBan", new { model.Madiaban });
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

        [Route("GiaDatDiaBan/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giadatdb.thongtin", "Edit"))
                {
                    var model = _db.GiaDatDiaBan.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_ct = _db.GiaDatDiaBanCt.Where(t => t.Mahs == model.Mahs);
                    model.GiaDatDiaBanCt = model_ct.ToList();

                    ViewData["DsXaPhuong"] = _db.DsXaPhuong.ToList();
                    ViewData["Dsloaidat"] = _db.DmLoaiDat.ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Madv"] = model.Madv;
                    ViewData["Title"] = "Bảng giá đất địa bàn";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_giadatdiaban";
                    ViewData["MenuLv3"] = "menu_giadatdiaban_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaDatDiaBan/Edit.cshtml", model);

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


        [Route("GiaDatDiaBan/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giadatdb.thongtin", "Edit"))
                {
                    var model = _db.GiaDatDiaBan.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_ct = _db.GiaDatDiaBanCt.Where(t => t.Mahs == model.Mahs);
                    model.GiaDatDiaBanCt = model_ct.ToList();

                    ViewData["DsXaPhuong"] = _db.DsXaPhuong.ToList();
                    ViewData["Dsloaidat"] = _db.DmLoaiDat.ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();  
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["Madv"] = model.Madv;
                    ViewData["Title"] = "Bảng giá các loại đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_giadatdiaban";
                    ViewData["MenuLv3"] = "menu_giadatdiaban_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaDatDiaBan/Show.cshtml", model);

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

        [Route("GiaDatDiaBan/Update")]
        [HttpPost]
        public IActionResult Update(CSDLGia_ASP.Models.Manages.DinhGia.GiaDatDiaBan request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giadatdb.thongtin", "Edit"))
                {

                    var model = _db.GiaDatDiaBan.FirstOrDefault(t => t.Mahs == request.Mahs);

                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Madv = request.Madv;
                    model.Updated_at = DateTime.Now;


                    _db.GiaDatDiaBan.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaDatDiaBan", new { request.Madv });
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


        //Tìm ra cơ quan chủ quản và lưu vào
        //Trạng thái thái từ CHT thành HT
        //Madv_ad --> mã đơn vị chuyển hồ sơ lên
        [Route("GiaDatDiaBan/Complete")]
        [HttpPost]
        public IActionResult Complete(string mahs_chuyen, string macqcq_chuyen)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giadatdb.thongtin", "Index"))
                {
                    var model = _db.GiaDatDiaBan.FirstOrDefault(t => t.Mahs == mahs_chuyen);

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
                    _db.GiaDatDiaBan.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaDatDiaBan", new { model.Madv });

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
        public IActionResult Search()
        {
            return View("Views/Admin/Manages/DinhGia/GiaDatDiaBan/TimKiem/Index.cshtml");
        }


    }
}
