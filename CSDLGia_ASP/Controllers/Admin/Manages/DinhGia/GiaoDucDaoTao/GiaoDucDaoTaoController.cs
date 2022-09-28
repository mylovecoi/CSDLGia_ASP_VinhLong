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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaoDucDaoTao
{
    public class GiaoDucDaoTaoController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaoDucDaoTaoController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DinhGiaGdDt")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.gddt.ttg", "Index"))
                {
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

                        }
                        else
                        {
                            if (string.IsNullOrEmpty(Madv))
                            {
                                Madv = dsdonvi.OrderBy(t => t.Id).Select(t => t.MaDv).First();
                            }
                        }


                        var model = _db.GiaDvGdDt.Where(t => t.Madv == Madv ).ToList();


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
                        ViewData["Title"] = " Thông tin hồ sơ giá dịch vụ giáo dục đào tạo";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dggddt";
                        ViewData["MenuLv3"] = "menu_dggddt_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaoDucDaoTao/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Messages"] = "Thông tin hồ sơ giá dịch vụ giáo dục đào tạo.";
                        ViewData["Title"] = " Thông tin hồ sơ giá dịch vụ giáo dục đào tạo";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dggddt";
                        ViewData["MenuLv3"] = "menu_dggddt_tt";
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


        [Route("DinhGiaGdDt/Create")]
        [HttpGet]
        public IActionResult Create(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.gddt.ttg", "Create"))
                {

                    var modelcxd = _db.GiaDvGdDtCt.Where(t => t.Gc == "CXD").ToList();
                    if (modelcxd != null)
                    {
                        _db.GiaDvGdDtCt.RemoveRange(modelcxd);

                        _db.SaveChanges();
                    }

                    var model = new VMDinhGiaDvGdDt
                    {
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Madv = Madv,
                        Thoidiem = DateTime.Now
                    };
                    var dm = _db.GiaDvGdDtDm.ToList();
                    var chitiet = new List<GiaDvGdDtCt>();
                    foreach (var item in dm)
                    {
                        chitiet.Add(new GiaDvGdDtCt()
                        {
                            Mahs = model.Mahs,
                            Maspdv = item.Maspdv,                          
                            Gc = "CXD",                           
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        });
                    }
                    _db.GiaDvGdDtCt.AddRange(chitiet);
                    _db.SaveChanges();

                    model.GiaDvGdDtCt = _db.GiaDvGdDtCt.Where(t => t.Mahs == model.Mahs).ToList();

                    
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["GiaDvGdDtDm"] = _db.GiaDvGdDtDm.ToList();
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["Title"] = "Thêm mới giá dịch vụ giáo dục đào tạo";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dggddt";
                    ViewData["MenuLv3"] = "menu_dggddt_tt";

                    return View("Views/Admin/Manages/DinhGia/GiaoDucDaoTao/Create.cshtml", model);
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


        [Route("DinhGiaGdDt/Store")]
        [HttpPost]
        public IActionResult Store(VMDinhGiaDvGdDt request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.gddt.ttg", "Create"))
                {
                    var model = new GiaDvGdDt
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Madiaban = request.Madiaban,
                        Soqd = request.Soqd,
                        Thoidiem = request.Thoidiem,
                        Tunam = request.Tunam,
                        Dennam = request.Dennam,
                        Mota = request.Mota,
                        Trangthai = "CHT",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaDvGdDt.Add(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaDvGdDtCt.Where(t => t.Mahs == request.Mahs);
                    _db.GiaDvGdDtCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaoDucDaoTao", new { Madv = request.Madv});
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

        [Route("DinhGiaGdDt/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.gddt.ttg", "Delete"))
                {
                    var model = _db.GiaDvGdDt.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaDvGdDt.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.GiaDvGdDtCt.Where(t => t.Mahs == model.Mahs);
                    _db.GiaDvGdDtCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaoDucDaoTao", new { Madv = model.Madv });
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


        [Route("DinhGiaGdDt/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.gddt.ttg", "Edit"))
                {
                    var model = _db.GiaDvGdDt.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new VMDinhGiaDvGdDt
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        Madiaban = model.Madiaban,
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Tunam = model.Tunam,
                        Dennam = model.Dennam,
                        Mota = model.Mota
                    };

                    var model_ct = _db.GiaDvGdDtCt.Where(t => t.Mahs == model_new.Mahs);

                    model_new.GiaDvGdDtCt = model_ct.ToList();

                    ViewData["Madv"] = model.Madv;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["GiaDvGdDtDm"] = _db.GiaDvGdDtDm.ToList();
                    ViewData["Title"] = "Chỉnh sửa giá dịch vụ giáo dục đào tạo";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dggddt";
                    ViewData["MenuLv3"] = "menu_dggddt_tt";

                    return View("Views/Admin/Manages/DinhGia/GiaoDucDaoTao/Edit.cshtml", model_new);
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

        [Route("DinhGiaGdDt/Update")]
        [HttpPost]
        public IActionResult Update(VMDinhGiaDvGdDt request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.gddt.ttg", "Edit"))
                {
                    var model = _db.GiaDvGdDt.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Madiaban = request.Madiaban;
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Thongtin = request.Thongtin;
                    model.Mota = request.Mota;
                    model.Ghichu = request.Ghichu;
                    model.Updated_at = DateTime.Now;
                    _db.GiaDvGdDt.Update(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaDvGdDtCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Mahs = model.Mahs;
                        }
                    }
                    _db.GiaDvGdDtCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaoDucDaoTao", new {Madv = request.Madv });
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

        [Route("DinhGiaGdDt/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.gddt.ttg", "Show"))
                {
                    var model = _db.GiaDvGdDt.FirstOrDefault(t => t.Mahs == Mahs);
                    ViewData["GiaDvGdDtCt"] = _db.GiaDvGdDtCt.Where(t => t.Mahs == Mahs).ToList();
                    ViewData["Madv"] = model.Madv;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Title"] = "Chỉnh sửa giá dịch vụ giáo dục đào tạo";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dggddt";
                    ViewData["MenuLv3"] = "menu_dggddt_ht";

                    return View("Views/Admin/Manages/DinhGia/GiaoDucDaoTao/Show.cshtml", model);
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
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.gddt.ttg", "Index"))
                {
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                 
                    ViewData["Tendv"] = _db.GiaDvGdDtDm.ToList();

                    ViewData["Title"] = "Tìm kiếm thông tin định giá giáo dục đào tạo";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dggddt";
                    ViewData["MenuLv3"] = "menu_dggddt_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaoDucDaoTao/TimKiem/Index.cshtml");
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
        [Route("DinhGiaGdDt/Result")]
        [HttpPost]
        public IActionResult Result(DateTime beginTime, DateTime endTime, string tsp, string dv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.gddt.ttg", "Edit"))
                {
                    var model_join = from dgct in _db.GiaDvGdDtCt
                                     join dg in _db.GiaDvGdDt on dgct.Mahs equals dg.Mahs
                                     join dgdm in _db.GiaDvGdDtDm on dgct.Maspdv equals dgdm.Maspdv
                                     select new VMDinhGiaDvGdDt
                                     {
                                         Id = dg.Id,
                                         Mahs = dg.Mahs,
                                         Madv = dg.Madv,
                                         Macqcq = dg.Macqcq,
                                         Thoidiem = dg.Thoidiem,
                                         Maspdv = dgct.Maspdv,

                                         Namapdung1 = dgct.Namapdung1,
                                         Giathanhthi1 = dgct.Giathanhthi1,
                                         Gianongthon1 = dgct.Gianongthon1,
                                         Giamiennui1 = dgct.Giamiennui1,

                                         Namapdung2 = dgct.Namapdung2,
                                         Giathanhthi2 = dgct.Giathanhthi2,
                                         Gianongthon2 = dgct.Gianongthon2,
                                         Giamiennui2 = dgct.Giamiennui2,

                                         Namapdung3 = dgct.Namapdung3,
                                         Giathanhthi3 = dgct.Giathanhthi3,
                                         Gianongthon3 = dgct.Gianongthon3,
                                         Giamiennui3 = dgct.Giamiennui3,
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


                    ViewData["Tendv"] = _db.GiaDvGdDtDm.ToList();
                    ViewData["dsdonvi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");

                    ViewData["Title"] = "Kết quả tìm kiếm thông tin định giá giáo dục đào tạo";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dggddt";
                    ViewData["MenuLv3"] = "menu_dggddt_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaoDucDaoTao/TimKiem/Result.cshtml", model_join);
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
