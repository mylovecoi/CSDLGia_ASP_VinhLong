﻿using Azure.Core;
using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems.API;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.KetNoiGiaQG
{
    public class KetNoiGiaRacThaiController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public KetNoiGiaRacThaiController(CSDLGiaDBContext db)
        {
            _db = db;
        }       

        [Route("KetNoiGiaRacThai/KhaiThac")]
        [HttpGet]
        public IActionResult KhaiThac()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {

                ViewData["Maso"] = "giaspdvcongich";
                var chk = _db.KetNoiAPI_DanhSach.FirstOrDefault(t => t.Maso == (string)ViewData["Maso"]);
                if (chk == null)
                {
                    var ketnoi = new KetNoiAPI_DanhSach
                    {
                        Maso = (string)ViewData["Maso"],
                    };
                    _db.KetNoiAPI_DanhSach.Add(ketnoi);
                    _db.SaveChanges();
                }
                var model = _db.KetNoiAPI_DanhSach.FirstOrDefault(t => t.Maso == (string)ViewData["Maso"]);
                ViewData["Title"] = "Khai thác giá thu gom rác thải";
                ViewData["MenuLv1"] = "menu_giaqg";
                ViewData["MenuLv2"] = "menu_giaqg_racthai";
                ViewData["MenuLv3"] = "menu_giaqg_racthai_khaithac";

                return View("Views/Admin/Manages/KetNoiGiaQG/KetNoiGiaRacThai/KhaiThac.cshtml", model);

            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

        [Route("KetNoiGiaRacThai/LayKhaiThac")]
        [HttpPost]
        public IActionResult LayKhaiThac(KetNoiAPI_DanhSach request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {               
                return RedirectToAction("KhaiThac", "KetNoiGiaRacThai");
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

        [Route("KetNoiGiaRacThai/DanhMuc")]
        [HttpGet]
        public IActionResult DanhMuc()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                ViewData["Maso"] = "giaspdvcongichdm";
                var model = _db.GiaSpDvToiDaNhom.ToList();
                var chk = _db.KetNoiAPI_DanhSach.FirstOrDefault(t => t.Maso == (string)ViewData["Maso"]);

                if (chk == null)
                {
                    var a_chucnang = Helpers.getDSChucNangCSDLQG();
                    var tenCN = a_chucnang.ContainsKey((string)ViewData["Maso"]) ? a_chucnang[(string)ViewData["Maso"]] : (string)ViewData["Maso"];

                    ViewData["Messages"] = "Hệ thống chưa thiết lập kết nối cho: "+ tenCN;
                    return View("Views/Admin/Error/Error.cshtml");
                }
                ViewData["KetNoiAPI_DanhSach"] = chk;
                ViewData["Title"] = "Giá thu gom rác thải";
                ViewData["MenuLv1"] = "menu_giaqg";
                ViewData["MenuLv2"] = "menu_giaqg_racthai";
                ViewData["MenuLv3"] = "menu_giaqg_racthai_danhmuc";

                return View("Views/Admin/Manages/KetNoiGiaQG/KetNoiGiaRacThai/DanhMuc.cshtml", model);
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

        [Route("KetNoiGiaRacThai/HoSo")]
        [HttpGet]
        public IActionResult HoSo()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                ViewData["Maso"] = "giaspdvcongich";
                var model = _db.GiaSpDvToiDa.ToList();
                ViewData["KetNoiAPI_DanhSach"] = _db.KetNoiAPI_DanhSach.FirstOrDefault(t => t.Maso == (string)ViewData["Maso"]);
                ViewData["Title"] = "Giá thu gom rác thải";
                ViewData["MenuLv1"] = "menu_giaqg";
                ViewData["MenuLv2"] = "menu_giaqg_racthai";
                ViewData["MenuLv3"] = "menu_giaqg_racthai_hoso";

                return View("Views/Admin/Manages/KetNoiGiaQG/KetNoiGiaRacThai/HoSo.cshtml", model);
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }
    }
}