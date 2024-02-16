using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaTrungThauDat
{
    public class GiaTrungThauDatBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaTrungThauDatBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("TrungThauQuyenSuDungDat/BaoCao")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.trungthaudat.baocao", "Index"))
                {
                    ViewData["Nam"] = DateTime.Now.Year;
                    ViewData["Title"] = "Báo cáo tổng hợp mức giá trúng thầu quyền sử dụng đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgd";
                    ViewData["MenuLv3"] = "menu_giadgd_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauDat/BaoCao/Index.cshtml");
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

        [Route("TrungThauQuyenSuDungDat/BaoCao/Tonghop")]
        [HttpPost]
        public IActionResult BcTH(DateTime tungay, DateTime denngay)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.trungthaudat.baocao", "Index"))
                {
                    var model = _db.GiaDvGdDt.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT");
                    ViewData["tungay"] = tungay;
                    ViewData["denngay"] = denngay;
                    ViewData["Title"] = "Báo cáo tổng hợp giá trúng thầu quyền sử dụng đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgd";
                    ViewData["MenuLv3"] = "menu_giadgd_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauDat/BaoCao/BcTH.cshtml", model);
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

        [Route("TrungThauQuyenSuDungDat/BaoCao/ChiTiet")]
        [HttpPost]
        public IActionResult BcCT(DateTime tungay, DateTime denngay)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.trungthaudat.baocao", "Index"))
                {
                    var model = _db.GiaDvGdDt.Where(t => t.Thoidiem >= tungay && t.Thoidiem <= denngay && t.Trangthai == "HT");

                    var modelct = (from a in _db.GiaDvGdDtCt.ToList()
                                   join b in _db.GiaDvGdDtDm on a.Maspdv equals b.Maspdv
                                   select new GiaDvGdDtCt
                                   {

                                       Tenspdv = b.Tenspdv,
                                       Mahs = a.Mahs,
                                       Namapdung1 = a.Namapdung1,
                                       Giamiennui1 = a.Giamiennui1,
                                       Giathanhthi1 = a.Giathanhthi1,
                                       Gianongthon1 = a.Gianongthon1,

                                       Namapdung2 = a.Namapdung2,
                                       Giamiennui2 = a.Giamiennui2,
                                       Giathanhthi2 = a.Giathanhthi2,
                                       Gianongthon2 = a.Gianongthon2,

                                       Namapdung3 = a.Namapdung3,
                                       Giamiennui3 = a.Giamiennui3,
                                       Giathanhthi3 = a.Giathanhthi3,
                                       Gianongthon3 = a.Gianongthon3,


                                   }); ;

                    ViewData["tungay"] = tungay;
                    ViewData["denngay"] = denngay;
                    ViewData["ct"] = modelct;
                    ViewData["Title"] = "Báo cáo chi tiết giá trúng thầu quyền sử dụng đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgd";
                    ViewData["MenuLv3"] = "menu_giadgd_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauDat/BaoCao/BcCT.cshtml", model);
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
