using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CSDLGia_ASP.Controllers.Admin.Manages.KeKhaiDangKyGia
{
    public class KeKhaiDangKyGiaController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly ITrangThaiHoSoService _trangThaiHoSoService;

        public KeKhaiDangKyGiaController(CSDLGiaDBContext db, ITrangThaiHoSoService trangThaiHoSoService)
        {
            _db = db;
            _trangThaiHoSoService = trangThaiHoSoService;
        }

        [HttpGet("KeKhaiDangKyGia")]

        public IActionResult Index(string MaCsKd, string MaNghe, int Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kekhaidangkygia.thongtin", "Index"))
                {
                    var model = from hoso in _db.KeKhaiDangKyGia.Where(t => t.MaCsKd == MaCsKd && t.MaNghe == MaNghe)
                                join cskd in _db.KeKhaiDangKyGiaCSKD on hoso.MaCsKd equals cskd.MaCsKd
                                join dn in _db.Company on cskd.MaDv equals dn.Madv
                                select new CSDLGia_ASP.Models.Manages.KeKhaiDangKyGia.KeKhaiDangKyGia
                                {
                                    Id = hoso.Id,
                                    Mahs = hoso.Mahs,
                                    TenCsKd = cskd.TenCsKd,
                                    TenDv = dn.Tendn,
                                    SoQD = hoso.SoQD,
                                    NgayQD = hoso.NgayQD,
                                    NgayChuyen = hoso.NgayChuyen,
                                    ThongTinNguoiChuyen = hoso.ThongTinNguoiChuyen,
                                    LyDo = hoso.LyDo,
                                    TrangThai = hoso.TrangThai,
                                    NgayDuyet = hoso.NgayDuyet,
                                };
                    if (Nam != 0)
                    {
                        model = model.Where(t => t.NgayQD.Year == Nam);
                    }
                    var nghekd = _db.DmNgheKd.FirstOrDefault(t => t.Manghe == MaNghe);
                    ViewData["HoSo"] = (nghekd?.Phanloai ?? "") + " " + (nghekd?.Tennghe ?? "");
                    ViewData["Nam"] = Nam;
                    ViewData["MaNghe"] = MaNghe;
                    ViewData["MaCsKd"] = MaCsKd;
                    ViewData["Title"] = "Thông tin hồ sơ kê khai đăng ký giá";
                    ViewData["MenuLv1"] = "menu_kekhaidangkygia";
                    ViewData["MenuLv2"] = "menu_kekhaidangkygia_thongtin_" + MaNghe;
                    return View("Views/Admin/Manages/KeKhaiDangKyGia/ThongTinHoSo/Index.cshtml", model);
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

        [HttpGet("KeKhaiDangKyGia/Create")]
        public IActionResult Create(string MaCsKd, string MaNghe)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kekhaidangkygia.thongtin", "Create"))
                {
                    var data_remove = _db.KeKhaiDangKyGiaCt.Where(t => t.MaCsKd == MaCsKd && t.TrangThai == "CXD");
                    if (data_remove.Any())
                    {
                        _db.KeKhaiDangKyGiaCt.RemoveRange(data_remove);
                        _db.SaveChanges();
                    }
                    var model = new CSDLGia_ASP.Models.Manages.KeKhaiDangKyGia.KeKhaiDangKyGia
                    {
                        NgayQD = DateTime.Now,
                        NgayQdLk = DateTime.Now,
                        Mahs = MaCsKd + "_" + MaNghe + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        MaNghe = MaNghe,
                        MaCsKd = MaCsKd,
                        TrangThai = "CC"
                    };

                    var nghekd = _db.DmNgheKd.FirstOrDefault(t => t.Manghe == MaNghe);
                    ViewData["HoSo"] = (nghekd?.Phanloai ?? "") + " " + (nghekd?.Tennghe ?? "");

                    ViewData["Title"] = "Thông tin hồ sơ kê khai đăng ký giá";
                    ViewData["MenuLv1"] = "menu_kekhaidangkygia";
                    ViewData["MenuLv2"] = "menu_kekhaidangkygia_thongtin_" + MaNghe;
                    return View("Views/Admin/Manages/KeKhaiDangKyGia/ThongTinHoSo/Create.cshtml", model);
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
        public IActionResult Store(CSDLGia_ASP.Models.Manages.KeKhaiDangKyGia.KeKhaiDangKyGia request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kekhaidangkygia.thongtin", "Create"))
                {
                    var data_ct = _db.KeKhaiDangKyGiaCt.Where(t => t.Mahs == request.Mahs);
                    if (data_ct.Any())
                    {
                        foreach (var item in data_ct) { item.TrangThai = "XD"; }
                        _db.KeKhaiDangKyGiaCt.UpdateRange(data_ct);
                    }
                    _db.KeKhaiDangKyGia.Add(request);
                    _db.SaveChanges();
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(request.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Thêm mới");

                    return RedirectToAction("Index", "KeKhaiDangKyGia", new { MaCsKd = request.MaCsKd, MaNghe = request.MaNghe });
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

        [HttpGet("KeKhaiDangKyGia/Edit")]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kekhaidangkygia.thongtin", "Edit"))
                {
                    var model = _db.KeKhaiDangKyGia.FirstOrDefault(t => t.Mahs == Mahs);
                    model.KeKhaiDangKyGiaCt = _db.KeKhaiDangKyGiaCt.Where(t => t.Mahs == model.Mahs).ToList();

                    var nghekd = _db.DmNgheKd.FirstOrDefault(t => t.Manghe == model.MaNghe);
                    ViewData["HoSo"] = (nghekd?.Phanloai ?? "") + " " + (nghekd?.Tennghe ?? "");
                    ViewData["Title"] = "Thông tin hồ sơ kê khai đăng ký giá";
                    ViewData["MenuLv1"] = "menu_kekhaidangkygia";
                    ViewData["MenuLv2"] = "menu_kekhaidangkygia_thongtin_" + model.MaNghe;
                    return View("Views/Admin/Manages/KeKhaiDangKyGia/ThongTinHoSo/Edit.cshtml", model);
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
        public IActionResult Update(CSDLGia_ASP.Models.Manages.KeKhaiDangKyGia.KeKhaiDangKyGia request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kekhaidangkygia.thongtin", "Edit"))
                {
                    var data_ct = _db.KeKhaiDangKyGiaCt.Where(t => t.Mahs == request.Mahs);
                    if (data_ct.Any())
                    {
                        foreach (var item in data_ct) { item.TrangThai = "XD"; }
                        _db.KeKhaiDangKyGiaCt.UpdateRange(data_ct);
                    }
                    _db.KeKhaiDangKyGia.Update(request);
                    _db.SaveChanges();
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(request.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Cập nhật");
                    return RedirectToAction("Index", "KeKhaiDangKyGia", new { MaCsKd = request.MaCsKd, MaNghe = request.MaNghe });
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
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kekhaidangkygia.thongtin", "Delete"))
                {
                    var model = _db.KeKhaiDangKyGia.FirstOrDefault(t => t.Id == id_delete);
                    var data_ct = _db.KeKhaiDangKyGiaCt.Where(t => t.Mahs == model.Mahs);
                    if (data_ct.Any())
                    {

                        _db.KeKhaiDangKyGiaCt.RemoveRange(data_ct);
                    }
                    string MaCsKd = model.MaCsKd;
                    string MaNghe = model.MaNghe;
                    _db.KeKhaiDangKyGia.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "KeKhaiDangKyGia", new { MaCsKd = MaCsKd, MaNghe = MaNghe });
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
        public IActionResult Chuyen(string mahs_chuyen, string thongtinnguoichuyen_chuyen, string sodienthoaichuyen_chuyen)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kekhaidangkygia.thongtin", "Approve"))
                {
                    var model = _db.KeKhaiDangKyGia.FirstOrDefault(t => t.Mahs == mahs_chuyen);
                    var model_dv = _db.KeKhaiDangKyGiaCSKD.FirstOrDefault(t => t.MaCsKd == model.MaCsKd);
                    var model_cqcq = _db.CompanyLvCc.FirstOrDefault(t => t.Manghe == model.MaNghe && t.Madv == model_dv.MaDv);
                    var donvi = _db.DsDonVi.FirstOrDefault(t => t.MaDv == model_cqcq.Macqcq);
                    model.TrangThai = "CD";
                    model.ThongTinNguoiChuyen = thongtinnguoichuyen_chuyen;
                    model.SoDtNguoiChuyen = sodienthoaichuyen_chuyen;
                    model.NgayChuyen = DateTime.Now;
                    model.MaCqCq = model_cqcq.Macqcq;
                    _db.KeKhaiDangKyGia.Update(model);
                    _db.SaveChanges();
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Chuyển hồ sơ " + (donvi?.TenDv ?? ""));
                    return RedirectToAction("Index", "KeKhaiDangKyGia", new { MaCsKd = model.MaCsKd, MaNghe = model.MaNghe });
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
