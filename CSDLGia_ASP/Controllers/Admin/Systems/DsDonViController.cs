using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class DsDonViController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public DsDonViController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DsDonVi")]
        [HttpGet]
        public IActionResult Index(string MaDiaBan)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.dsdiaban", "Index"))
                {
                    var dsdiaban = _db.DsDiaBan;
                    if (string.IsNullOrEmpty(MaDiaBan))
                    {
                        MaDiaBan = dsdiaban.OrderBy(t => t.Id).Select(t => t.MaDiaBan).First();
                    }
                    var dsdonvi = _db.DsDonVi.Where(t => t.MaDiaBan == MaDiaBan).ToList();

                    var dsDiaBanApDung = _db.Districts.ToList();
                    foreach (var item in dsdonvi)
                    {
                        foreach(var db in dsDiaBanApDung)
                        {
                            if (!string.IsNullOrEmpty(item.DiaBanApDung))
                                if (item.DiaBanApDung.Contains(db.Mahuyen))
                                    item.TenDiaBanApDung += db.Tenhuyen + ";";
                        }
                    }
                    ViewData["DsDiaBan"] = dsdiaban;
                    ViewData["MaDiaBan"] = MaDiaBan;
                    ViewData["TenDiaBan"] = dsdiaban.Where(t => t.MaDiaBan == MaDiaBan).ToList();
                    ViewData["Title"] = "Danh sách đơn vị";
                    ViewData["MenuLv1"] = "menu_hethong";
                    ViewData["MenuLv2"] = "menu_qthethong";
                    ViewData["MenuLv3"] = "menu_dsdonvi";
                    return View("Views/Admin/Systems/DsDonVi/Index.cshtml", dsdonvi);
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

        [Route("DsDonVi/Create")]
        [HttpGet]
        public IActionResult Create(string MaDiaBan)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.dsdiaban", "Create"))
                {
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.MaDiaBan == MaDiaBan).ToList();
                    ViewData["MaDiaBan"] = MaDiaBan;
                    ViewData["Title"] = "Thêm mới Danh sách đơn vị";
                    ViewData["MenuLv1"] = "menu_hethong";
                    ViewData["MenuLv2"] = "menu_qthethong";
                    ViewData["MenuLv3"] = "menu_dsdonvi";
                    ViewData["DsDiaBanApDung"] = _db.Districts.ToList();
                    return View("Views/Admin/Systems/DsDonVi/Create.cshtml");
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

        [Route("DsDonVi/Store")]
        [HttpPost]
        public IActionResult Store(DsDonVi request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.dsdiaban", "Create"))
                {
                    if (request.TenDv != null && request.TenDvHienThi != null)
                    {
                        var model = new DsDonVi
                        {
                            MaDiaBan = request.MaDiaBan,
                            MaQhNs = request.MaQhNs,
                            MaDv = DateTime.Now.ToString("yyMMddssmmHH"),
                            TenDv = request.TenDv,
                            DiaChi = request.DiaChi,
                            TtLienHe = request.TtLienHe,
                            EmailQl = request.EmailQl,
                            EmailQt = request.EmailQt,
                            SoNgayLv = request.SoNgayLv,
                            TenDvHienThi = request.TenDvHienThi,
                            TenDvCqHienThi = request.TenDvCqHienThi,
                            ChucVuKy = request.ChucVuKy,
                            ChucVuKyThay = request.ChucVuKyThay,
                            NguoiKy = request.NguoiKy,
                            DiaDanh = request.DiaDanh,
                            ChucNang = request.ChucNang,
                            NhapLieu = request.NhapLieu,
                            XetDuyet = request.XetDuyet,
                            CongBo = request.CongBo,
                            QuanTri = request.QuanTri,
                            DiaBanApDung = request.DiaBanApDung,
                            Created_At = DateTime.Now,
                            Updated_At = DateTime.Now,
                        };
                        _db.DsDonVi.Add(model);
                        _db.SaveChanges();
                        return RedirectToAction("Index", "DsDonVi", new { MaDiaBan = request.MaDiaBan });
                    }
                    else
                    {
                        ModelState.AddModelError("TenDv", "Thông tin không được bỏ trống");
                        ModelState.AddModelError("TenDvHienThi", "Thông tin không được bỏ trống");
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.MaDiaBan == request.MaDiaBan).ToList();
                        ViewData["MaDiaBan"] = request.MaDiaBan;
                        ViewData["Title"] = "Thêm mới Danh sách đơn vị";
                        ViewData["MenuLv1"] = "menu_hethong";
                        ViewData["MenuLv2"] = "menu_qthethong";
                        ViewData["MenuLv3"] = "menu_dsdonvi";

                        return View("Views/Admin/Systems/DsDonVi/Create.cshtml", request);
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

        [Route("DsDonVi/Edit")]
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.dsdiaban", "Update"))
                {
                    var model = _db.DsDonVi.FirstOrDefault(t => t.Id == Id);

                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.MaDiaBan == model.MaDiaBan).ToList();
                    ViewData["DsDiaBanApDung"] = _db.Districts.ToList();

                    ViewData["MaDiaBan"] = model.MaDiaBan;
                    ViewData["Title"] = "Chỉnh sửa Danh sách đơn vị";
                    ViewData["MenuLv1"] = "menu_hethong";
                    ViewData["MenuLv2"] = "menu_qthethong";
                    ViewData["MenuLv3"] = "menu_dsdonvi";

                    return View("Views/Admin/Systems/DsDonVi/Edit.cshtml", model);
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

        [Route("DsDonVi/Update")]
        [HttpPost]
        public IActionResult Update(DsDonVi request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.dsdiaban", "Update"))
                {
                    var model = _db.DsDonVi.FirstOrDefault(t => t.Id == request.Id);
                    model.MaQhNs = request.MaQhNs;
                    model.TenDv = request.TenDv;
                    model.DiaChi = request.DiaChi;
                    model.TtLienHe = request.TtLienHe;
                    model.EmailQl = request.EmailQl;
                    model.EmailQt = request.EmailQt;
                    model.SoNgayLv = request.SoNgayLv;
                    model.TenDvHienThi = request.TenDvHienThi;
                    model.TenDvCqHienThi = request.TenDvCqHienThi;
                    model.ChucVuKy = request.ChucVuKy;
                    model.ChucVuKyThay = request.ChucVuKyThay;
                    model.NguoiKy = request.NguoiKy;
                    model.DiaDanh = request.DiaDanh;
                    model.ChucNang = request.ChucNang;
                    model.NhapLieu = request.NhapLieu;
                    model.XetDuyet = request.XetDuyet;
                    model.CongBo = request.CongBo;
                    model.QuanTri = request.QuanTri;
                    model.DiaBanApDung = request.DiaBanApDung;
                    model.Updated_At = DateTime.Now;
                    _db.DsDonVi.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "DsDonVi", new { MaDiaBan = request.MaDiaBan });
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

        [Route("DsDonVi/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.dsdiaban", "Delete"))
                {
                    var model = _db.DsDonVi.FirstOrDefault(t => t.Id == id_delete);
                    _db.DsDonVi.Remove(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "DsDonVi", new { MaDiaBan = model.MaDiaBan });
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