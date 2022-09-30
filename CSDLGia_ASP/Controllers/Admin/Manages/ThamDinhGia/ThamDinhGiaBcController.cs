using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CSDLGia_ASP.Database;
using System.Security.Cryptography;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.ThamDinhGia;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using CSDLGia_ASP.Models.Systems;

namespace CSDLGia_ASP.Controllers.Admin.Manages.ThamDinhGia
{
    public class ThamDinhGiaBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public ThamDinhGiaBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("ThamDinhGia/BaoCao")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.bc", "Index"))
                {
                    ViewData["Dsdiaban"] = _db.DsDiaBan;
                    ViewData["Dsdonvi"] = _db.DsDonVi.Where(t => t.ChucNang == "TONGHOP");
                    ViewData["Title"] = "Báo cáo tổng hợp tài sản thẩm định giá";
                    ViewData["MenuLv1"] = "menu_tdg";
                    ViewData["MenuLv2"] = "menu_tdg_bc";
                    return View("Views/Admin/Manages/ThamDinhGia/BaoCao/Index.cshtml");
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

        [Route("ThamDinhGia/BaoCao/Bc1")]
        [HttpPost]
        public IActionResult Bc1(string madv, DateTime tungay, DateTime denngay)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.bc", "Index"))
                {
                    var model = (from tdgct in _db.ThamDinhGiaCt
                                 join tdg in _db.ThamDinhGia on tdgct.Mahs equals tdg.Mahs
                                 select new ThamDinhGiaCt
                                 {
                                     Id = tdgct.Id,
                                     Mahs = tdgct.Mahs,
                                     Madv = tdg.Madv,
                                     Tents = tdgct.Tents,
                                     Dacdiempl = tdgct.Dacdiempl,
                                     Thongsokt = tdgct.Thongsokt,
                                     Diadiem = tdg.Diadiem,
                                     Thoidiem = tdg.Thoidiem,
                                     Ppthamdinh = tdg.Ppthamdinh,
                                     Mucdich = tdg.Mucdich,
                                     Dvyeucau = tdg.Dvyeucau,
                                     Giatritstd = tdgct.Giatritstd,
                                     Thoihan = tdg.Thoihan,
                                     Ghichu = tdgct.Ghichu,
                                 });

                    /*return Ok(madv);*/

                    if (madv != "all")
                    {
                        model = model.Where(t => t.Madv == madv);
                    }

                    /*return Ok(model);*/

                    if (tungay.ToString("yyMMdd") != "010101")
                    {
                        model = model.Where(t => t.Thoidiem >= tungay);
                    }

                    if (denngay.ToString("yyMMdd") != "010101")
                    {
                        model = model.Where(t => t.Thoidiem <= denngay);
                    }

                    ViewData["Madv"] = madv;
                    ViewData["Tungay"] = tungay;
                    ViewData["Denngay"] = denngay;
                    ViewData["Title"] = "Báo cáo tổng hợp tài sản thẩm định giá";
                    ViewData["MenuLv1"] = "menu_tdg";
                    ViewData["MenuLv2"] = "menu_tdg_bc";
                    return View("Views/Admin/Manages/ThamDinhGia/BaoCao/Bc1.cshtml", model);
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
