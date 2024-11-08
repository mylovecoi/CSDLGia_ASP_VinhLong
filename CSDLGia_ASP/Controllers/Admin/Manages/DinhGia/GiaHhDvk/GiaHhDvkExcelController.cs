﻿using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.Services;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaHhDvk
{
    public class GiaHhDvkExcelController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IDsDonviService _dsDonviService;

        public GiaHhDvkExcelController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment, IDsDonviService dsDonviService)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
            _dsDonviService = dsDonviService;
        }

        [HttpGet("GiaHangHoaDichVuKhac/NhanExcel")]
        public IActionResult Index(string Madv, string MadiabanBc)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.tt", "Create"))
                {
                    Madv = string.IsNullOrEmpty(Madv) ? Helpers.GetSsAdmin(HttpContext.Session, "Madv") : Madv;

                    var model_donvi = _dsDonviService.GetListDonvi(Helpers.GetSsAdmin(HttpContext.Session, "Madv"));
                    List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();

                    var model = new CSDLGia_ASP.ViewModels.VMImportExcel
                    {
                        MaDv = Madv,
                        LineStart = 4,
                        LineStop = 1000,
                        Sheet = 1,
                        MadiabanBc = MadiabanBc
                    };
                    
                    var donVi = _db.DsDonVi.FirstOrDefault(x => x.MaDv == model.MaDv);
                    string diaBanApDung = donVi?.DiaBanApDung ?? null;
                    if (!string.IsNullOrEmpty(diaBanApDung))
                    {
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(x => diaBanApDung.Contains(x.MaDiaBan));
                    }
                    else
                    {
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(x => x.Level == "H");
                    }

                    ViewData["DsDonVi"] = model_donvi;                    
                    ViewData["DanhMucThongTu"] = _db.GiaHhDvkNhom;
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dg_xaydungmoi";
                    ViewData["MenuLv3"] = "menu_dg_xaydungmoi_tt";
                    ViewData["Madv"] = Madv;
                  
                    ViewData["Title"] = "Thông tin hồ sơ giá hàng hóa dịch vụ khác";
                    return View("Views/Admin/Manages/DinhGia/GiaHhDvk/Excels/Excel.cshtml", model);

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
        public async Task<IActionResult> Import(CSDLGia_ASP.ViewModels.VMImportExcel request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                string Mahs = request.MaDv + "_" + DateTime.Now.ToString("yyMMddssmmHH");
                var data_dm = _db.GiaHhDvkDm.Where(t => t.Matt == request.Matt).Select(t =>
                    new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvkCt
                    {
                        Mahs = Mahs,
                        Madv = request.MaDv,
                        Mahhdv = t.Mahhdv,
                        Trangthai = "CXD"
                    });

                request.LineStart = request.LineStart == 0 ? 1 : request.LineStart;
                int sheet = request.Sheet == 0 ? 0 : (request.Sheet - 1);
                using (var stream = new MemoryStream())
                {
                    await request.FormFile.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[sheet];
                        var rowcount = worksheet.Dimension.Rows;
                        request.LineStop = request.LineStop > rowcount ? rowcount : request.LineStop;
                        var list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvkCt>();
                        foreach (var item in data_dm)
                        {
                            double gialk = 0;
                            double gia = 0;
                            string ghichu = "";
                            string loaigia = "";
                            string nguontt = "";
                            for (int row = request.LineStart; row <= request.LineStop; row++)
                            {
                                string mahhdvInLine = worksheet.Cells[row, 2].Value != null ?
                                                worksheet.Cells[row, 2].Value.ToString().Trim() : "";
                                if (item.Mahhdv == mahhdvInLine)
                                {
                                    loaigia = worksheet.Cells[row, 6].Value != null ?
                                                worksheet.Cells[row, 6].Value.ToString().Trim() : "";
                                    gialk = Helpers.ConvertStrToDb(worksheet.Cells[row, 7].Value != null ?
                                                worksheet.Cells[row, 7].Value.ToString().Trim() : "");
                                    gia = Helpers.ConvertStrToDb(worksheet.Cells[row, 8].Value != null ?
                                                worksheet.Cells[row, 8].Value.ToString().Trim() : "");
                                    nguontt = worksheet.Cells[row, 11].Value != null ?
                                                worksheet.Cells[row, 11].Value.ToString().Trim() : "";
                                    ghichu = worksheet.Cells[row, 12].Value != null ?
                                                worksheet.Cells[row, 12].Value.ToString().Trim() : "";
                                    break;
                                }
                            }

                            list_add.Add(new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvkCt
                            {
                                Mahs = item.Mahs,
                                Madv = item.Madv,
                                Mahhdv = item.Mahhdv,
                                Trangthai = item.Trangthai,
                                Loaigia = loaigia,
                                Nguontt = nguontt,
                                Gialk = gialk,
                                Gia = gia,
                                Ghichu = ghichu
                            });
                        }
                        _db.GiaHhDvkCt.AddRange(list_add);
                        _db.SaveChanges();
                        var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvk
                        {
                            Mahs = Mahs,
                            Madv = request.MaDv,
                            Madiaban = request.MadiabanBc,
                            Thoidiem = DateTime.Now,
                            Created_at = DateTime.UtcNow,
                            Thang = request.Thang.ToString(),
                            Nam = request.Nam.ToString()
                        };
                        var model_ct = (from ct in _db.GiaHhDvkCt.Where(t => t.Mahs == Mahs)
                                        join dm in _db.GiaHhDvkDm.Where(t => t.Matt == request.Matt && t.Theodoi == "TD") on ct.Mahhdv equals dm.Mahhdv
                                        select new GiaHhDvkCt
                                        {
                                            Id = ct.Id,
                                            Manhom = ct.Manhom,
                                            Mahhdv = ct.Mahhdv,
                                            Mahs = ct.Mahs,
                                            Gia = ct.Gia,
                                            Gialk = ct.Gialk,
                                            Loaigia = ct.Loaigia,
                                            Nguontt = ct.Nguontt,
                                            Ghichu = ct.Ghichu,
                                            Created_at = ct.Created_at,
                                            Updated_at = ct.Updated_at,
                                            Tenhhdv = dm.Tenhhdv,
                                            Dacdiemkt = dm.Dacdiemkt,
                                            Dvt = dm.Dvt,
                                        }).ToList();

                        model.GiaHhDvkCt = model_ct;

                        ViewData["MadvBc"] = request.MaDv;
                        ViewData["MattBc"] = request.Matt;
                        ViewData["MadiabanBc"] = request.MadiabanBc;
                        ViewData["ThangBc"] = request.Thang.ToString();
                        ViewData["NamBc"] = request.Nam.ToString();
                        ViewData["Mahs"] = model.Mahs;
                        ViewData["Nhomhhdvk"] = _db.GiaHhDvkNhom.ToList();
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "ADMIN");
                        ViewData["DmDvt"] = _db.DmDvt.ToList();
                        ViewData["Title"] = "Thông tin giá hàng hóa dịch vụ thêm mới";
                        ViewData["MenuLv1"] = "menu_hhdvk";
                        ViewData["MenuLv2"] = "menu_hhdvk_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaHhDvk/DanhSach/Create.cshtml", model);
                    }
                }      
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }
    }
}
