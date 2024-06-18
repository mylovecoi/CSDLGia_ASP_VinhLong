using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class HeThongChungController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public HeThongChungController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var model = _db.tblHeThong.First();

            ViewData["Title"] = "Cấu hình hệ thống";
            ViewData["MenuLv1"] = "menu_hethong";
            ViewData["MenuLv2"] = "menu_qthethong";
            ViewData["MenuLv3"] = "menu_hethongchung";
            return View("Views/Admin/Systems/HeThongChung/Index.cshtml", model);
        }

        public IActionResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.hethongchung", "Edit"))
                {
                    var model = _db.tblHeThong.FirstOrDefault(t => t.Id == Id);                    
                    ViewData["Title"] = "Cấu hình hệ thống";
                    ViewData["MenuLv1"] = "menu_hethong";
                    ViewData["MenuLv2"] = "menu_qthethong";
                    ViewData["MenuLv3"] = "menu_hethongchung";
                    return View("Views/Admin/Systems/HeThongChung/Edit.cshtml", model);
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

        [Route("HeThongChung/Update")]
        [HttpPost]
        public IActionResult Update(tblHeThong request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.hethongchung", "Edit"))
                {
                    var check = _db.tblHeThong.FirstOrDefault(u => u.Id == request.Id);

                    //Xử lý file hướng dẫn sử dụng
                    if (request.FileImportHDSD != null && request.FileImportHDSD.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            request.FileImportHDSD.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            // Chuyển đổi dữ liệu tệp sang base64
                            request.FileHDSDBase64 = Convert.ToBase64String(fileBytes);
                            request.FileHDSD = request.FileImportHDSD.FileName;
                        }
                    }
                    else
                    {
                        //Giữ thông tin file cũ
                        if(check != null)
                        {
                            request.FileHDSDBase64 = check.FileHDSDBase64;
                            request.FileHDSD = check.FileHDSD;
                        }
                    }
                    //Xử lý file đăng ký tài khoản
                    if (request.FileImportDangKy != null && request.FileImportDangKy.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            request.FileImportDangKy.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            // Chuyển đổi dữ liệu tệp sang base64
                            request.FileDangKyBase64 = Convert.ToBase64String(fileBytes);
                            request.FileDangKy = request.FileImportDangKy.FileName;
                        }
                    }
                    else
                    {
                        //Giữ thông tin file cũ
                        if (check != null)
                        {
                            request.FileDangKyBase64 = check.FileDangKyBase64;
                            request.FileDangKy = check.FileDangKy;
                        }
                    }

                    if (check == null)
                    {
                        var model = new tblHeThong
                        {
                            TenDonVi = request.TenDonVi,
                            DienThoai = request.DienThoai,
                            DiaChi = request.DiaChi,
                            LinkAPIXacthuc = request.LinkAPIXacthuc,
                            TokenLGSP = request.TokenLGSP,
                            MaDiaBanHanhChinh = request.MaDiaBanHanhChinh,
                            MaDonViThuThap = request.MaDonViThuThap,
                            FileHDSDBase64 = request.FileHDSDBase64,
                            FileHDSD = request.FileHDSD,
                            FileDangKyBase64 = request.FileDangKyBase64,
                            FileDangKy = request.FileDangKy,
                            TimeOut = request.TimeOut,
                        };
                        _db.tblHeThong.Add(model);
                        _db.SaveChanges();

                    }
                    else
                    {
                        check.TenDonVi = request.TenDonVi;
                        check.DienThoai = request.DienThoai;
                        check.DiaChi = request.DiaChi;
                        check.LinkAPIXacthuc = request.LinkAPIXacthuc;
                        check.TokenLGSP = request.TokenLGSP;
                        check.MaDiaBanHanhChinh = request.MaDiaBanHanhChinh;
                        check.MaDonViThuThap = request.MaDonViThuThap;
                        check.FileHDSDBase64 = request.FileHDSDBase64;
                        check.FileHDSD = request.FileHDSD;
                        check.FileDangKyBase64 = request.FileDangKyBase64;
                        check.FileDangKy = request.FileDangKy;
                        check.TimeOut = request.TimeOut;

                        _db.tblHeThong.Update(check);
                        _db.SaveChanges();
                    }

                    return RedirectToAction("Index", "HeThongChung");
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
