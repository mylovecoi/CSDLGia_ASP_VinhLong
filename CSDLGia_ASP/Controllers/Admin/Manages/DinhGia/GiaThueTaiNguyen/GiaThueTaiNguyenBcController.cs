using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaThueTaiNguyen
{
    public class GiaThueTaiNguyenBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaThueTaiNguyenBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaThueTaiNguyen/BaoCao")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetn.baocao", "Index"))
                {
                    var nhomtn = _db.GiaThueTaiNguyenNhom.Where(t => t.Theodoi == "TD").ToList();

                    ViewData["NhomTn"] = nhomtn;
                    ViewData["Nam"] = DateTime.Now.Year;
                    ViewData["Title"] = "Báo cáo tổng hợp thuế tài nguyên";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgthuetn";
                    ViewData["MenuLv3"] = "menu_dgthuetn_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaThueTaiNguyen/BaoCao/Index.cshtml");
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

        [Route("GiaThueTaiNguyen/Bc1")]
        [HttpPost]
        public IActionResult Bc1(string manhom, int namlk, int nambc, string tenthutruong, string chucvu)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetn.baocao", "Index"))
                {
                    var model = _db.GiaThueTaiNguyenDm.Where(t => t.Manhom == manhom).ToList();

                    var modellk = _db.GiaThueTaiNguyen.FirstOrDefault(t => t.Thoidiem.Year == namlk && t.Manhom == manhom);

                    var modelbc = _db.GiaThueTaiNguyen.FirstOrDefault(t => t.Thoidiem.Year == nambc && t.Manhom == manhom);

                    foreach (var tn in model)
                    {
                        if (tn.Level == "1")
                        {
                            if (modellk != null)
                            {
                                var modelctlk = _db.GiaThueTaiNguyenCt.FirstOrDefault(t => t.Mahs == modellk.Mahs
                                                                                    && t.Cap1 == tn.Cap1
                                                                                    && t.Level == "1");
                                tn.dongialk = (modelctlk != null) && (modellk != null) ? modelctlk.Gia : 0;
                            }
                            if (modelbc != null)
                            {
                                var modelctbc = _db.GiaThueTaiNguyenCt.FirstOrDefault(t => t.Mahs == modelbc.Mahs
                                                                                    && t.Cap1 == tn.Cap1
                                                                                    && t.Level == "1");
                                tn.dongiabc = (modelctbc != null) && (modelbc != null) ? modelctbc.Gia : 0;
                            }
                        }
                        else if (tn.Level == "2")
                        {
                            if (modellk != null)
                            {
                                var modelctlk = _db.GiaThueTaiNguyenCt.FirstOrDefault(t => t.Mahs == modellk.Mahs
                                                                                && t.Cap1 == tn.Cap1
                                                                                && t.Cap2 == tn.Cap2
                                                                                && t.Level == "2");
                                tn.dongialk = (modelctlk != null) && (modellk != null) ? modelctlk.Gia : 0;
                            }
                            if (modelbc != null)
                            {
                                var modelctbc = _db.GiaThueTaiNguyenCt.FirstOrDefault(t => t.Mahs == modelbc.Mahs
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
                                var modelctlk = _db.GiaThueTaiNguyenCt.FirstOrDefault(t => t.Mahs == modellk.Mahs
                                                                                && t.Cap1 == tn.Cap1
                                                                                && t.Cap2 == tn.Cap2
                                                                                && t.Cap3 == tn.Cap3
                                                                                && t.Level == "3");
                                tn.dongialk = (modelctlk != null) && (modellk != null) ? modelctlk.Gia : 0;
                            }
                            if (modelbc != null)
                            {
                                var modelctbc = _db.GiaThueTaiNguyenCt.FirstOrDefault(t => t.Mahs == modelbc.Mahs
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
                                var modelctlk = _db.GiaThueTaiNguyenCt.FirstOrDefault(t => t.Mahs == modellk.Mahs
                                                                                && t.Cap1 == tn.Cap1
                                                                                && t.Cap2 == tn.Cap2
                                                                                && t.Cap3 == tn.Cap3
                                                                                && t.Cap4 == tn.Cap4
                                                                                && t.Level == "4");
                                tn.dongialk = (modelctlk != null) && (modellk != null) ? modelctlk.Gia : 0;
                            }
                            if (modelbc != null)
                            {
                                var modelctbc = _db.GiaThueTaiNguyenCt.FirstOrDefault(t => t.Mahs == modelbc.Mahs
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
                                var modelctlk = _db.GiaThueTaiNguyenCt.FirstOrDefault(t => t.Mahs == modellk.Mahs
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
                                var modelctbc = _db.GiaThueTaiNguyenCt.FirstOrDefault(t => t.Mahs == modelbc.Mahs
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
                    ViewData["tenthutruong"] = tenthutruong;
                    ViewData["chucvu"] = chucvu;
                    ViewData["TenNhom"] = _db.GiaThueTaiNguyenNhom.FirstOrDefault(t => t.Manhom == manhom).Tennhom;
                    ViewData["Namlk"] = namlk;
                    ViewData["Nambc"] = nambc;
                    ViewData["Title"] = "Báo cáo tổng hợp thuế tài nguyên";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgthuetn";
                    ViewData["MenuLv3"] = "menu_dgthuetn_bc";
                    return View("Views/Admin/Manages/DinhGia/GiaThueTaiNguyen/BaoCao/Bc1.cshtml", model);
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
