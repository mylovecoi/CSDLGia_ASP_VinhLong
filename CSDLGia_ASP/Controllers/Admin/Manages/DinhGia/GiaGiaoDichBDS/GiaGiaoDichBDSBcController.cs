using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CSDLGia_ASP.Database;
using System.Security.Cryptography;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.KeKhaiGia;
using CSDLGia_ASP.ViewModels.Systems;
using CSDLGia_ASP.ViewModels.Manages.KeKhaiGia;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using CSDLGia_ASP.Models.Manages.DinhGia;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaThueDN
{
    public class GiaGiaoDichBDSBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaGiaoDichBDSBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("BaoCaoDgGiaGiaoDichBDS")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.baocao", "Index"))
                {

                    ViewData["Nam"] = DateTime.Now.Year;
                    ViewData["Title"] = "Báo cáo tổng hợp giá giao dịch bất động sản";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dggdbds";
                    ViewData["MenuLv3"] = "menu_dggdbds_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/BaoCao/Index.cshtml");
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
        //GiaThueMatDatMatNuoc
        //GiaThueMatDatMatNuocCt
        [Route("BaoCaoDgGiaGiaoDichBDS/BcTH")]
        [HttpPost]
        public IActionResult BcTH(DateTime tungay, DateTime denngay)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgxmtxd.giakkbc", "Index"))
                {
                    var model = (from pl in _db.GiaThueMatDatMatNuoc.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT")
                                 join db in _db.DsDiaBan on pl.Madiaban equals db.MaDiaBan
                                 select new GiaThueMatDatMatNuoc
                                 {
                                     Id = pl.Id,
                                     Mahs = pl.Mahs,
                                     Tendiaban = db.TenDiaBan,
                                     Soqd = pl.Soqd,
                                     Thoidiem = pl.Thoidiem,
                                 });
                    ViewData["tungay"] = tungay;
                    ViewData["denngay"] = denngay;
                    ViewData["Title"] = "Báo cáo tổng hợp giá giao dịch bất động sản";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dggdbds";
                    ViewData["MenuLv3"] = "menu_dggdbds_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/BaoCao/BcTH.cshtml", model);
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

        [Route("BaoCaoDgGiaGiaoDichBDS/BcCT")]
        [HttpPost]
        public IActionResult BcCT(DateTime tungay, DateTime denngay)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgxmtxd.giakkbc", "Index"))
                {
                    var model = _db.GiaThueMatDatMatNuoc.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT");  
                    ViewData["tungay"] = tungay;
                    ViewData["denngay"] = denngay;
                    ViewData["ct"] = _db.GiaThueMatDatMatNuocCt;
                    ViewData["Title"] = "Báo cáo chi tiết giá giao dịch bất động sản";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dggdbds";
                    ViewData["MenuLv3"] = "menu_dggdbds_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/BaoCao/BcCT.cshtml", model);
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
