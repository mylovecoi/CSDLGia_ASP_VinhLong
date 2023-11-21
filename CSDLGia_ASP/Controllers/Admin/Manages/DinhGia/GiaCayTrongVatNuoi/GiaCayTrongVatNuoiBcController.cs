using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CSDLGia_ASP.Database;
using System.Security.Cryptography;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSDLGia_ASP.ViewModels.Systems;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using CSDLGia_ASP.Models.Manages.KeKhaiDkg;
using CSDLGia_ASP.ViewModels.Manages.KeKhaiDkg;
using Microsoft.Extensions.Hosting;
using CSDLGia_ASP.Models.Systems;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaCayTrongVatNuoi
{
    public class GiaCayTrongVatNuoiBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaCayTrongVatNuoiBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaCayTrongVatNuoi/BaoCao")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.caytrongvatnuoi.baocao", "Index"))
                {
                    var nhomtn = _db.GiaCayTrongVatNuoiNhom.Where(t => t.Theodoi == "TD").ToList();

                    ViewData["NhomTn"] = nhomtn;
                    ViewData["Nam"] = DateTime.Now.Year;
                    ViewData["Title"] = "Báo cáo tổng hợp cây trồng vật nuôi";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dg_caytrongvatnuoi";
                    ViewData["MenuLv3"] = "menu_dg_caytrongvatnuoi_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaCayTrongVatNuoi/BaoCao/Index.cshtml");
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

        [Route("GiaCayTrongVatNuoi/Bc1")]
        [HttpPost]
        public IActionResult Bc1(string manhom, int namlk, int nambc)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.caytrongvatnuoi.baocao", "Index"))
                {
                    var model = _db.GiaCayTrongVatNuoiDm.Where(t => t.Manhom == manhom).ToList();

                    var modellk = _db.GiaCayTrongVatNuoi.FirstOrDefault(t => t.Thoidiem.Year == namlk && t.Manhom == manhom);

                    var modelbc = _db.GiaCayTrongVatNuoi.FirstOrDefault(t => t.Thoidiem.Year == nambc && t.Manhom == manhom);

                    foreach (var tn in model)
                    {
                        if (tn.Level == "1")
                        {
                            if (modellk != null)
                            {
                                var modelctlk = _db.GiaCayTrongVatNuoiCt.FirstOrDefault(t => t.Mahs == modellk.Mahs
                                                                                    && t.Cap1 == tn.Cap1
                                                                                    && t.Level == "1");
                                tn.dongialk = (modelctlk != null) && (modellk != null) ? modelctlk.Gia : 0;
                            }
                            if (modelbc != null)
                            {
                                var modelctbc = _db.GiaCayTrongVatNuoiCt.FirstOrDefault(t => t.Mahs == modelbc.Mahs
                                                                                    && t.Cap1 == tn.Cap1
                                                                                    && t.Level == "1");
                                tn.dongiabc = (modelctbc != null) && (modelbc != null) ? modelctbc.Gia : 0;
                            }
                        }
                        else if (tn.Level == "2")
                        {
                            if (modellk != null)
                            {
                                var modelctlk = _db.GiaCayTrongVatNuoiCt.FirstOrDefault(t => t.Mahs == modellk.Mahs
                                                                                && t.Cap1 == tn.Cap1
                                                                                && t.Cap2 == tn.Cap2
                                                                                && t.Level == "2");
                                tn.dongialk = (modelctlk != null) && (modellk != null) ? modelctlk.Gia : 0;
                            }
                            if (modelbc != null)
                            {
                                var modelctbc = _db.GiaCayTrongVatNuoiCt.FirstOrDefault(t => t.Mahs == modelbc.Mahs
                                                                                && t.Cap1 == tn.Cap1
                                                                                && t.Cap2 == tn.Cap2
                                                                                && t.Level == "2");
                                tn.dongiabc = (modelctbc != null) && (modelbc != null) ? modelctbc.Gia : 0;
                            }
                        }
                        else if (tn.Level == "3")
                        {
                            if (modellk != null)
                            {
                                var modelctlk = _db.GiaCayTrongVatNuoiCt.FirstOrDefault(t => t.Mahs == modellk.Mahs
                                                                                && t.Cap1 == tn.Cap1
                                                                                && t.Cap2 == tn.Cap2
                                                                                && t.Cap3 == tn.Cap3
                                                                                && t.Level == "3");
                                tn.dongialk = (modelctlk != null) && (modellk != null) ? modelctlk.Gia : 0;
                            }
                            if (modelbc != null)
                            {
                                var modelctbc = _db.GiaCayTrongVatNuoiCt.FirstOrDefault(t => t.Mahs == modelbc.Mahs
                                                                                && t.Cap1 == tn.Cap1
                                                                                && t.Cap2 == tn.Cap2
                                                                                && t.Cap3 == tn.Cap3
                                                                                && t.Level == "3");
                                tn.dongiabc = (modelctbc != null) && (modelbc != null) ? modelctbc.Gia : 0;
                            }
                        }
                        else if (tn.Level == "4")
                        {
                            if (modellk != null)
                            {
                                var modelctlk = _db.GiaCayTrongVatNuoiCt.FirstOrDefault(t => t.Mahs == modellk.Mahs
                                                                                && t.Cap1 == tn.Cap1
                                                                                && t.Cap2 == tn.Cap2
                                                                                && t.Cap3 == tn.Cap3
                                                                                && t.Cap4 == tn.Cap4
                                                                                && t.Level == "4");
                                tn.dongialk = (modelctlk != null) && (modellk != null) ? modelctlk.Gia : 0;
                            }
                            if (modelbc != null)
                            {
                                var modelctbc = _db.GiaCayTrongVatNuoiCt.FirstOrDefault(t => t.Mahs == modelbc.Mahs
                                                                                && t.Cap1 == tn.Cap1
                                                                                && t.Cap2 == tn.Cap2
                                                                                && t.Cap3 == tn.Cap3
                                                                                && t.Cap4 == tn.Cap4
                                                                                && t.Level == "4");
                                tn.dongiabc = (modelctbc != null) && (modelbc != null) ? modelctbc.Gia : 0;
                            }
                        }
                        else if (tn.Level == "5")
                        {
                            if (modellk != null)
                            {
                                var modelctlk = _db.GiaCayTrongVatNuoiCt.FirstOrDefault(t => t.Mahs == modellk.Mahs
                                                                                && t.Cap1 == tn.Cap1
                                                                                && t.Cap2 == tn.Cap2
                                                                                && t.Cap3 == tn.Cap3
                                                                                && t.Cap4 == tn.Cap4
                                                                                && t.Cap5 == tn.Cap5
                                                                                && t.Level == "5");
                                tn.dongialk = (modelctlk != null) && (modellk != null) ? modelctlk.Gia : 0;
                            }
                            if (modelbc != null)
                            {
                                var modelctbc = _db.GiaCayTrongVatNuoiCt.FirstOrDefault(t => t.Mahs == modelbc.Mahs
                                                                                && t.Cap1 == tn.Cap1
                                                                                && t.Cap2 == tn.Cap2
                                                                                && t.Cap3 == tn.Cap3
                                                                                && t.Cap4 == tn.Cap4
                                                                                && t.Cap5 == tn.Cap5
                                                                                && t.Level == "5");
                                tn.dongiabc = (modelctbc != null) && (modelbc != null) ? modelctbc.Gia : 0;
                            }
                        }

                    }

                    ViewData["TenNhom"] = _db.GiaCayTrongVatNuoiNhom.FirstOrDefault(t => t.Manhom == manhom).Tennhom;
                    ViewData["Namlk"] = namlk;
                    ViewData["Nambc"] = nambc;
                    ViewData["Title"] = "Báo cáo tổng hợp cây trồng vật nuôi";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dg_caytrongvatnuoi";
                    ViewData["MenuLv3"] = "menu_dg_caytrongvatnuoi_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaCayTrongVatNuoi/BaoCao/Bc1.cshtml", model);
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
