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

namespace CSDLGia_ASP.Controllers.Admin.Manages.KeKhaiGia.KkGiaVtXtx
{
    public class KkGiaVtXtxBcController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public KkGiaVtXtxBcController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("BaoCaoKkGiaVtXtx")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgvtxtx.giakk", "Index"))
                {
                    ViewData["Title"] = "Báo cáo tổng hợp kê khai giá cước vận tải hành khách bằng xe taxi";
                    ViewData["MenuLv1"] = "menu_kknygia";
                    ViewData["MenuLv2"] = "menu_kkgvtxtx";
                    ViewData["MenuLv3"] = "menu_giakkvtxtxbc";
                    return View("Views/Admin/Manages/KeKhaiGia/KkGiaVtXtx/BaoCao/Index.cshtml");
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

        [Route("BaoCaoKkGiaVtXtx/Bc1")]
        [HttpPost]
        public IActionResult BcTongHop(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgvtxtx.giakk", "Index"))
                {
                    var model = (from kk in _db.KkGia.Where(t => t.Manghe == "VTXTX" && t.Trangthai == "DD")
                                 join com in _db.Company on kk.Madv equals com.Madv
                                 select new VMKkGia
                                 {
                                     Id = kk.Id,
                                     Mahs = kk.Mahs,
                                     Ngaynhap = kk.Ngaynhap,
                                     Ngayhieuluc = kk.Ngayhieuluc,
                                     Manghe = kk.Manghe,
                                     Socv = kk.Socv,
                                     Socvlk = kk.Socvlk,
                                     Ngaycvlk = kk.Ngaycvlk,
                                     Ytcauthanhgia = kk.Ytcauthanhgia,
                                     Thydggadgia = kk.Thydggadgia,
                                     Ttnguoinop = kk.Ttnguoinop,
                                     Dtll = kk.Dtll,
                                     Sohsnhan = kk.Sohsnhan,
                                     Ngaychuyen = kk.Ngaychuyen,
                                     Ngaynhan = kk.Ngaynhan,
                                     Trangthai = kk.Trangthai,
                                     Madv = com.Madv,
                                     Tendn = com.Tendn,
                                 });

                    if(phanloai == "ngaychuyen")
                    {
                        if(time == "ngay")
                        {
                            model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                        }else if(time == "thang")
                        {
                            model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                        }
                        else
                        {
                            if(quy == "1")
                            {
                                tungay = new DateTime(int.Parse(nam), 1, 1);
                                denngay = new DateTime(int.Parse(nam), 3, 31);
                            }else if(quy == "2")
                            {
                                tungay = new DateTime(int.Parse(nam), 4, 1);
                                denngay = new DateTime(int.Parse(nam), 6, 30);
                            }else if(quy == "3")
                            {
                                tungay = new DateTime(int.Parse(nam), 7, 1);
                                denngay = new DateTime(int.Parse(nam), 9, 30);
                            }
                            else
                            {
                                tungay = new DateTime(int.Parse(nam), 10, 1);
                                denngay = new DateTime(int.Parse(nam), 12, 31);
                            }

                            model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                        }
                    }
                    else
                    {
                        if (time == "ngay")
                        {
                            model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                        }
                        else if (time == "thang")
                        {
                            model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
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

                            model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                        }
                    }

                    ViewData["time"] = time;
                    ViewData["tungay"] = tungay;
                    ViewData["denngay"] = denngay;
                    ViewData["thang"] = thang;
                    ViewData["quy"] = quy;
                    ViewData["nam"] = nam;
                    ViewData["Title"] = "Báo cáo tổng hợp kê khai giá Cước vận tải hành khách bằng xe taxi";
                    ViewData["MenuLv1"] = "menu_kknygia";
                    ViewData["MenuLv2"] = "menu_kkgvtxtx";
                    ViewData["MenuLv3"] = "menu_giakkvtxtxbc";
                    return View("Views/Admin/Manages/KeKhaiGia/KkGiaVtXtx/BaoCao/Bc1.cshtml", model);
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

        [Route("BaoCaoKkGiaVtXtx/Bc2")]
        [HttpPost]
        public IActionResult BcChiTiet(string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kknygia.kkgvtxtx.giakk", "Index"))
                {
                    var model = (from kk in _db.KkGia.Where(t => t.Manghe == "VTXTX" && t.Trangthai == "DD")
                                 join com in _db.Company on kk.Madv equals com.Madv
                                 select new VMKkGia
                                 {
                                     Id = kk.Id,
                                     Mahs = kk.Mahs,
                                     Ngaynhap = kk.Ngaynhap,
                                     Ngayhieuluc = kk.Ngayhieuluc,
                                     Manghe = kk.Manghe,
                                     Socv = kk.Socv,
                                     Socvlk = kk.Socvlk,
                                     Ngaycvlk = kk.Ngaycvlk,
                                     Ytcauthanhgia = kk.Ytcauthanhgia,
                                     Thydggadgia = kk.Thydggadgia,
                                     Ttnguoinop = kk.Ttnguoinop,
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
                            model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                        }
                        else if (time == "thang")
                        {
                            model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
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

                            model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                        }
                    }
                    else
                    {
                        if (time == "ngay")
                        {
                            model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                        }
                        else if (time == "thang")
                        {
                            model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
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

                            model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                        }
                    }

                    ViewData["time"] = time;
                    ViewData["tungay"] = tungay;
                    ViewData["denngay"] = denngay;
                    ViewData["thang"] = thang;
                    ViewData["quy"] = quy;
                    ViewData["nam"] = nam;
                    ViewData["ct"] = _db.KkGiaVtXtxCt.ToList();
                    ViewData["Title"] = "Báo cáo tổng hợp kê khai giá cước vận tải hành khách bằng xe taxi";
                    ViewData["MenuLv1"] = "menu_kknygia";
                    ViewData["MenuLv2"] = "menu_kkgvtxtx";
                    ViewData["MenuLv3"] = "menu_giakkvtxtxbc";
                    return View("Views/Admin/Manages/KeKhaiGia/KkGiaVtXtx/BaoCao/Bc2.cshtml", model);
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
