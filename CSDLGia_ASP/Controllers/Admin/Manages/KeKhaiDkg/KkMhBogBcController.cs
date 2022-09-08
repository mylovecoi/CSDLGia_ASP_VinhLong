using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CSDLGia_ASP.Database;
using System.Security.Cryptography;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.KeKhaiDkg;
using CSDLGia_ASP.ViewModels.Systems;
using CSDLGia_ASP.ViewModels.Manages.KeKhaiDkg;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CSDLGia_ASP.Controllers.Admin.Manages.KeKhaiDkg
{
    public class KkMhBogBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public KkMhBogBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("BaoCaoKkMhBog")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.bog.baocao", "Index"))
                {
                    var dmnghe = _db.DmNgheKd.Where(t => t.Manganh == "BOG").ToList();

                    ViewData["Manghe"] = dmnghe;
                    ViewData["Title"] = "Báo cáo tổng hợp kê khai mặt hàng bình ổn giá";
                    ViewData["MenuLv1"] = "menu_bog";
                    ViewData["MenuLv2"] = "menu_baocao";
                    return View("Views/Admin/Manages/KeKhaiDkg/KkMhBog/BaoCao/Index.cshtml");
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

        [Route("BaoCaoKkMhBog/Bc1")]
        [HttpPost]
        public IActionResult BcTongHop(string manghe, string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.bog.baocao", "Index"))
                {
                    var model = _db.KkMhBog.Where(t => t.Trangthai == "DD");

                    if (manghe != "all")
                    {
                        model = model.Where(t => t.Manghe == manghe);
                    }

                    var model_join = (from kk in model
                                      join com in _db.Company on kk.Madv equals com.Madv
                                      select new VMKkMhBog
                                      {
                                          Id = kk.Id,
                                          Mahs = kk.Mahs,
                                          Thoidiem = kk.Thoidiem,
                                          Ngayhieuluc = kk.Ngayhieuluc,
                                          Manghe = kk.Manghe,
                                          Socv = kk.Socv,
                                          Socvlk = kk.Socvlk,
                                          Ngaycvlk = kk.Ngaycvlk,
                                          Chinhsachkm = kk.Chinhsachkm,
                                          Ptnguyennhan = kk.Ptnguyennhan,
                                          Dtll = kk.Dtll,
                                          Sohsnhan = kk.Sohsnhan,
                                          Ngaychuyen = kk.Ngaychuyen,
                                          Ngaynhan = kk.Ngaynhan,
                                          Trangthai = kk.Trangthai,
                                          Madv = com.Madv,
                                          Tendn = com.Tendn,
                                      });

                    if (phanloai == "ngaychuyen")
                    {
                        if (time == "ngay")
                        {
                            model_join = model_join.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                        }
                        else if (time == "thang")
                        {
                            model_join = model_join.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                        }
                        else
                        {
                            if (quy == "1")
                            {
                                tungay = new DateTime(int.Parse(nam), 1, 1);
                                denngay = new DateTime(int.Parse(nam), 3, 31);
                            }
                            else if (quy == "2")
                            {
                                tungay = new DateTime(int.Parse(nam), 4, 1);
                                denngay = new DateTime(int.Parse(nam), 6, 30);
                            }
                            else if (quy == "3")
                            {
                                tungay = new DateTime(int.Parse(nam), 7, 1);
                                denngay = new DateTime(int.Parse(nam), 9, 30);
                            }
                            else
                            {
                                tungay = new DateTime(int.Parse(nam), 10, 1);
                                denngay = new DateTime(int.Parse(nam), 12, 31);
                            }

                            model_join = model_join.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                        }
                    }
                    else
                    {
                        if (time == "ngay")
                        {
                            model_join = model_join.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                        }
                        else if (time == "thang")
                        {
                            model_join = model_join.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                        }
                        else
                        {
                            if (quy == "1")
                            {
                                tungay = new DateTime(int.Parse(nam), 1, 1);
                                denngay = new DateTime(int.Parse(nam), 3, 31);
                            }
                            else if (quy == "2")
                            {
                                tungay = new DateTime(int.Parse(nam), 4, 1);
                                denngay = new DateTime(int.Parse(nam), 6, 30);
                            }
                            else if (quy == "3")
                            {
                                tungay = new DateTime(int.Parse(nam), 7, 1);
                                denngay = new DateTime(int.Parse(nam), 9, 30);
                            }
                            else
                            {
                                tungay = new DateTime(int.Parse(nam), 10, 1);
                                denngay = new DateTime(int.Parse(nam), 12, 31);
                            }

                            model_join = model_join.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                        }
                    }

                    ViewData["time"] = time;
                    ViewData["tungay"] = tungay;
                    ViewData["denngay"] = denngay;
                    ViewData["thang"] = thang;
                    ViewData["quy"] = quy;
                    ViewData["nam"] = nam;
                    ViewData["Title"] = "Báo cáo tổng hợp kê khai mặt hàng bình ổn giá";
                    ViewData["MenuLv1"] = "menu_bog";
                    ViewData["MenuLv2"] = "menu_baocao";
                    return View("Views/Admin/Manages/KeKhaiDkg/KkMhBog/BaoCao/Bc1.cshtml", model_join);
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

        [Route("BaoCaoKkMhBog/Bc2")]
        [HttpPost]
        public IActionResult BcChiTiet(string manghe, string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.bog.baocao", "Index"))
                {
                    var model = _db.KkMhBog.Where(t => t.Trangthai == "DD");

                    if (manghe != "all")
                    {
                        model = model.Where(t => t.Manghe == manghe);
                    }

                    var model_join = (from kk in model
                                 join com in _db.Company on kk.Madv equals com.Madv
                                 select new VMKkMhBog
                                 {
                                     Id = kk.Id,
                                     Mahs = kk.Mahs,
                                     Thoidiem = kk.Thoidiem,
                                     Ngayhieuluc = kk.Ngayhieuluc,
                                     Manghe = kk.Manghe,
                                     Socv = kk.Socv,
                                     Socvlk = kk.Socvlk,
                                     Ngaycvlk = kk.Ngaycvlk,
                                     Ptnguyennhan = kk.Ptnguyennhan,
                                     Chinhsachkm = kk.Chinhsachkm,
                                     Dtll = kk.Dtll,
                                     Sohsnhan = kk.Sohsnhan,
                                     Ngaychuyen = kk.Ngaychuyen,
                                     Ngaynhan = kk.Ngaynhan,
                                     Trangthai = kk.Trangthai,
                                     Madv = com.Madv,
                                     Tendn = com.Tendn,
                                 });

                    if (phanloai == "ngaychuyen")
                    {
                        if (time == "ngay")
                        {
                            model_join = model_join.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                        }
                        else if (time == "thang")
                        {
                            model_join = model_join.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                        }
                        else
                        {
                            if (quy == "1")
                            {
                                tungay = new DateTime(int.Parse(nam), 1, 1);
                                denngay = new DateTime(int.Parse(nam), 3, 31);
                            }
                            else if (quy == "2")
                            {
                                tungay = new DateTime(int.Parse(nam), 4, 1);
                                denngay = new DateTime(int.Parse(nam), 6, 30);
                            }
                            else if (quy == "3")
                            {
                                tungay = new DateTime(int.Parse(nam), 7, 1);
                                denngay = new DateTime(int.Parse(nam), 9, 30);
                            }
                            else
                            {
                                tungay = new DateTime(int.Parse(nam), 10, 1);
                                denngay = new DateTime(int.Parse(nam), 12, 31);
                            }

                            model_join = model_join.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                        }
                    }
                    else
                    {
                        if (time == "ngay")
                        {
                            model_join = model_join.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                        }
                        else if (time == "thang")
                        {
                            model_join = model_join.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                        }
                        else
                        {
                            if (quy == "1")
                            {
                                tungay = new DateTime(int.Parse(nam), 1, 1);
                                denngay = new DateTime(int.Parse(nam), 3, 31);
                            }
                            else if (quy == "2")
                            {
                                tungay = new DateTime(int.Parse(nam), 4, 1);
                                denngay = new DateTime(int.Parse(nam), 6, 30);
                            }
                            else if (quy == "3")
                            {
                                tungay = new DateTime(int.Parse(nam), 7, 1);
                                denngay = new DateTime(int.Parse(nam), 9, 30);
                            }
                            else
                            {
                                tungay = new DateTime(int.Parse(nam), 10, 1);
                                denngay = new DateTime(int.Parse(nam), 12, 31);
                            }

                            model_join = model_join.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                        }
                    }

                    ViewData["time"] = time;
                    ViewData["tungay"] = tungay;
                    ViewData["denngay"] = denngay;
                    ViewData["thang"] = thang;
                    ViewData["quy"] = quy;
                    ViewData["nam"] = nam;
                    ViewData["ct"] = _db.KkMhBogCt.ToList();
                    ViewData["Title"] = "Báo cáo tổng hợp kê khai mặt hàng bình ổn giá";
                    ViewData["MenuLv1"] = "menu_bog";
                    ViewData["MenuLv2"] = "menu_baocao";
                    return View("Views/Admin/Manages/KeKhaiDkg/KkMhBog/BaoCao/Bc2.cshtml", model_join);
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
