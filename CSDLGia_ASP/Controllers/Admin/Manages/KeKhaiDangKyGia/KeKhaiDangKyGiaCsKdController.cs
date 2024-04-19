using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.KeKhaiDangKyGia;
using CSDLGia_ASP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.KeKhaiDangKyGia
{
    public class KeKhaiDangKyGiaCsKdController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IDsDonviService _dsDonviService;

        public KeKhaiDangKyGiaCsKdController(CSDLGiaDBContext db, IDsDonviService dsDonviService)
        {
            _db = db;
            _dsDonviService = dsDonviService;
        }

        [HttpGet("KeKhaiDangKyGia/CoSoKinhDoanh")]

        public IActionResult Index(string MaNghe)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kekhaidangkygia.thongtin", "Index"))
                {
                    var model = from cskd in _db.KeKhaiDangKyGiaCSKD.Where(t => t.MaNghe == MaNghe)
                                join lvcc in _db.CompanyLvCc.Where(t => t.Manghe == MaNghe) on cskd.MaDv equals lvcc.Madv
                                join com in _db.Company on cskd.MaDv equals com.Madv
                                select new CSDLGia_ASP.Models.Manages.KeKhaiDangKyGia.KeKhaiDangKyGiaCSKD
                                {
                                    Id = cskd.Id,
                                    MaCsKd = cskd.MaCsKd,
                                    TenCsKd = cskd.TenCsKd,
                                    MaNghe = cskd.MaNghe,
                                    DiaChi = cskd.DiaChi,
                                    SoDt = cskd.SoDt,
                                    MaCqCq = lvcc.Macqcq,
                                    TenDv = com.Tendn,
                                    MaDv = cskd.MaDv
                                };

                    IQueryable<CSDLGia_ASP.Models.Systems.Company> model_dn = from com in _db.Company
                                                                              join lvcc in _db.CompanyLvCc.Where(t => t.Manghe == MaNghe) on com.Madv equals lvcc.Madv
                                                                              select new CSDLGia_ASP.Models.Systems.Company
                                                                              {
                                                                                  Madv = com.Madv,
                                                                                  Tendn = com.Tendn,
                                                                                  Macqcq = lvcc.Macqcq
                                                                              };
                    if (Helpers.GetSsAdmin(HttpContext.Session, "Level") == "DN")
                    {
                        model = model.Where(t => t.MaDv == Helpers.GetSsAdmin(HttpContext.Session, "Madv"));
                        model_dn = model_dn.Where(t => t.Madv == Helpers.GetSsAdmin(HttpContext.Session, "Madv"));
                    }
                    else
                    {
                        var model_donvicq = _dsDonviService.GetListDonvi(Helpers.GetSsAdmin(HttpContext.Session, "Madv"));
                        List<string> list_madvcq = model_donvicq.Select(t => t.MaDv).ToList();
                        model = model.Where(t => list_madvcq.Contains(t.MaCqCq));
                        model_dn = model_dn.Where(t => list_madvcq.Contains(t.Macqcq));
                    }
                    var danhmucnghe = _db.DmNgheKd.FirstOrDefault(t => t.Manghe == MaNghe);
                    ViewData["HoSo"] = danhmucnghe?.Tennghe ?? "";
                    ViewData["MaNghe"] = MaNghe;
                    ViewData["Company"] = model_dn;
                    ViewData["Title"] = "Thông tin cơ sở kinh doanh";
                    ViewData["MenuLv1"] = "menu_kekhaidangkygia";
                    ViewData["MenuLv2"] = "menu_kekhaidangkygia_" + MaNghe;
                    ViewData["MenuLv3"] = "menu_kekhaidangkygia_hoso_" + MaNghe;
                    return View("Views/Admin/Manages/KeKhaiDangKyGia/CoSoKinhDoanh/Index.cshtml", model);
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


        [HttpPost("KeKhaiDangKyGia/CoSoKinhDoanh/Store")]

        public IActionResult Store(KeKhaiDangKyGiaCSKD requests)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kekhaidangkygia.thongtin", "Create"))
                {
                    var model = new KeKhaiDangKyGiaCSKD
                    {
                        MaCsKd = requests.MaDv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        MaNghe = requests.MaNghe,
                        MaDv = requests.MaDv,
                        TenCsKd = requests.TenCsKd,
                        SoDt = requests.SoDt,
                        DiaChi = requests.DiaChi
                    };
                    _db.KeKhaiDangKyGiaCSKD.Add(model);
                    _db.SaveChanges();
                    var data = new { status = "success", message = "Thành công" };
                    return Json(data);
                }
                else
                {
                    var data = new { status = "error", message = "Bạn không có quyền truy cập chức năng này!!!" };
                    return Json(data);
                }
            }
            else
            {
                var data = new { status = "error", message = "Kết thúc phiên làm việc. Bạn cần đăng nhập lại!!!" };
                return Json(data);
            }
        }
    }
}
