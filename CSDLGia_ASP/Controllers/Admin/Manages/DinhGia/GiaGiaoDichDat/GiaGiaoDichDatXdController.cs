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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaGiaoDichDat
{
    public class GiaGiaoDichDatXdController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaGiaoDichDatXdController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaGiaoDichDat/XetDuyet")]
        [HttpGet]
        public IActionResult Index(int Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.xetduyet", "Index"))
                {

                    List<string> list_trangthai = new List<string> { "CD", "HT", "DD", "CB" };
                    var model = _db.GiaGiaoDichDat.Where(t => list_trangthai.Contains(t.Trangthai));
                    if (Nam != 0)
                    {
                        model = model.Where(t => t.Thoidiem.Year == Nam);
                    }
                    var model_join = from dg in model
                                     join donvi in _db.DsDonVi on dg.Madv equals donvi.MaDv
                                     select new CSDLGia_ASP.Models.Manages.DinhGia.GiaGiaoDichDat
                                     {
                                         Id = dg.Id,
                                         Madv = dg.Madv,
                                         Ghichu = dg.Ghichu,
                                         Mahs = dg.Mahs,
                                         Thoidiem = dg.Thoidiem,
                                         Soqd = dg.Soqd,
                                         TenDonVi = donvi.TenDv,
                                         Trangthai = dg.Trangthai
                                     };
                        ViewData["Nam"] = Nam;
                        ViewData["Title"] = "Thông tin hồ sơ giá giao dịch đất thực tế trên thị trường";
                        ViewData["MenuLv1"] = "menu_giadat";
                        ViewData["MenuLv2"] = "menu_dg_giaodichdattrenthitruong";
                        ViewData["MenuLv3"] = "menu_dg_giaodichdattrenthitruong_xd";
                        return View("Views/Admin/Manages/DinhGia/GiaGiaoDichDat/XetDuyet/Index.cshtml", model_join);
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.xetduyet", "Approve"))
                {

                    var model = _db.GiaGiaoDichDat.FirstOrDefault(p => p.Mahs == mahs_duyet);

                    model.Updated_at = DateTime.Now;
                    model.Trangthai = "DD";                  

                    _db.GiaGiaoDichDat.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaGiaoDichDatXd", new { Nam = model.Thoidiem.Year });
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.xetduyet", "Approve"))
                {
                    var model = _db.GiaGiaoDichDat.FirstOrDefault(p => p.Mahs == mahs_huyduyet);

                    model.Updated_at = DateTime.Now;
                    model.Trangthai = "CD";

                    _db.GiaGiaoDichDat.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "GiaGiaoDichDatXd", new { Nam = model.Thoidiem.Year });
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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.xetduyet", "Approve"))
                {
                    var model = _db.GiaGiaoDichDat.FirstOrDefault(t => t.Id == id_tralai);

                    model.Trangthai = "BTL";
                    model.Lydo = Lydo;
                    model.Updated_at = DateTime.Now;
                    _db.GiaGiaoDichDat.Update(model);
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

                    return RedirectToAction("Index", "GiaGiaoDichDatXd", new { Nam = model.Thoidiem.Year });
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
        public IActionResult CongBo(string mahs_cb)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.xetduyet", "Public"))
                {
                    var model = _db.GiaGiaoDichDat.FirstOrDefault(t => t.Mahs == mahs_cb);

                    model.Updated_at = DateTime.Now;
                    model.Trangthai = "CB";


                    _db.GiaGiaoDichDat.Update(model);
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
                    return RedirectToAction("Index", "GiaGiaoDichDatXd", new { Nam = model.Thoidiem.Year });
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
        public IActionResult HuyCongBo(string mahs_hcb)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.xetduyet", "Public"))
                {
                    var model = _db.GiaGiaoDichDat.FirstOrDefault(t => t.Mahs == mahs_hcb);

                    model.Trangthai = "DD";
                    model.Updated_at = DateTime.Now;

                    _db.GiaGiaoDichDat.Update(model);
                    _db.SaveChanges();                    
                    return RedirectToAction("Index", "GiaGiaoDichDatXd");
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
