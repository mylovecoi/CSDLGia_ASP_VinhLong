using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Services;
using CSDLGia_ASP.ViewModels.Manages.DashBoard;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace CSDLGia_ASP.Controllers
{
    public class HomeController : Controller
    {

        private readonly CSDLGiaDBContext _db;
        private readonly IDsDiaBanService _dsDiaBanService;

        public HomeController(CSDLGiaDBContext db, IDsDiaBanService dsDiaBanService)
        {
            _db = db;
            _dsDiaBanService = dsDiaBanService;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.bSession = false;
            var serverName = Request.Host.Host;

            HttpContext.Session.SetString("ServerName", serverName);

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                ViewBag.bSession = true;
            }

            //Lấy data default cho bảng giá đất            
            string Mahs = _db.GiaDatDiaBan.Where(t => t.Trangthai == "CB" && t.Thoidiem <= DateTime.Now).OrderByDescending(t=>t.Thoidiem)
                                                                                .FirstOrDefault()?.Mahs;
            var datact = new List<GiaDatDiaBanCt>();
            if(Mahs != null)
            {
                datact = (from dat in _db.GiaDatDiaBanCt.Where(t => t.Mahs == Mahs)
                          join dm in _db.DmLoaiDat on dat.Maloaidat equals dm.Maloaidat
                          select new CSDLGia_ASP.Models.Manages.DinhGia.GiaDatDiaBanCt
                          {
                              Id = dat.Id,
                              HienThi = dat.HienThi,
                              Maloaidat = dat.Maloaidat,
                              Mota = dat.Mota,
                              Diemdau = dat.Diemdau,
                              Diemcuoi = dat.Diemcuoi,
                              Loaiduong = dat.Loaiduong,
                              Hesok = dat.Hesok,
                              Giavt1 = dat.Giavt1,
                              Giavt2 = dat.Giavt2,
                              Giavt3 = dat.Giavt3,
                              Giavt4 = dat.Giavt4,
                              Giavt5 = dat.Giavt5,
                              Loaidat = dm.Loaidat,
                              Sapxep = dat.Sapxep
                          }).ToList();
            }           
            ViewData["ThongTinHoSo"] = datact;           
            //

            var model = _db.Supports;
            ViewData["Title"] = "Trang chủ";
            ViewData["MenuLv1"] = "menu_home";
            return View("Views/Admin/Home/Index.cshtml", model);
        }

        [Route("ChangePassword")]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                ViewData["Title"] = "Thay đổi mật khẩu";
                return View("Views/Admin/Home/ChangePassword.cshtml");
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

        [Route("ChangePassword")]
        [HttpPost]
        public IActionResult ChangePassword(string current_password, string new_password, string verify_password)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (!string.IsNullOrEmpty(current_password) && !string.IsNullOrEmpty(new_password) && !string.IsNullOrEmpty(verify_password))
                {
                    string md5_password = "";
                    using (MD5 md5Hash = MD5.Create())
                    {
                        string change = Helpers.GetMd5Hash(md5Hash, current_password);
                        md5_password = change;
                    }
                    if (md5_password == Helpers.GetSsAdmin(HttpContext.Session, "Password"))
                    {
                        if (new_password == verify_password)
                        {
                            Regex regex = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,20}$");

                            if (regex.IsMatch(verify_password))
                            {
                                string new_md5_password = "";
                                using (MD5 md5Hash = MD5.Create())
                                {
                                    string new_change = Helpers.GetMd5Hash(md5Hash, verify_password);
                                    new_md5_password = new_change;
                                }
                                var model = _db.Users.FirstOrDefault(u => u.Username == Helpers.GetSsAdmin(HttpContext.Session, "Username"));
                                model.Password = new_md5_password;
                                _db.SaveChanges();

                                return RedirectToAction("Login", "Login");
                            }
                            else
                            {
                                string error = "Mật khẩu từ phải có ít nhất 4 ký tự và không quá 20 ký tự, bao gồm ít nhất 1 chữ cái viết hoa, 1 chữ cái viết thường và một chữ số ";
                                ViewData["Title"] = "Thay đổi mật khẩu";
                                ViewData["current_password"] = current_password;
                                ViewData["new_password"] = new_password;
                                ViewData["verify_password"] = verify_password;
                                ModelState.AddModelError("error", error);
                                return View("Views/Admin/Home/ChangePassword.cshtml");
                            }
                        }
                        else
                        {
                            ViewData["Title"] = "Thay đổi mật khẩu";
                            ModelState.AddModelError("error", "Mật khẩu mới và mật khẩu xác thực không trùng nhau");
                            ViewData["current_password"] = current_password;
                            ViewData["new_password"] = new_password;
                            ViewData["verify_password"] = verify_password;
                            return View("Views/Admin/Home/ChangePassword.cshtml");
                        }
                    }
                    else
                    {
                        ViewData["Title"] = "Thay đổi mật khẩu";
                        ModelState.AddModelError("error", "Mật khẩu hiện tại không đúng");
                        ViewData["current_password"] = current_password;
                        ViewData["new_password"] = new_password;
                        ViewData["verify_password"] = verify_password;
                        return View("Views/Admin/Home/ChangePassword.cshtml");
                    }
                }
                else
                {
                    ViewData["Title"] = "Thay đổi mật khẩu";
                    ModelState.AddModelError("error", "Thông tin không được bỏ trống");
                    ViewData["current_password"] = current_password;
                    ViewData["new_password"] = new_password;
                    ViewData["verify_password"] = verify_password;
                    return View("Views/Admin/Home/ChangePassword.cshtml");
                }
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

        [Route("MobileApp")]
        [HttpGet]
        public IActionResult MobileApp()
        {
            ViewBag.bSession = false;
            ViewData["Title"] = "Ứng dụng điện thoại";
            return View("Views/Admin/Systems/CongBo/MobileApp.cshtml");
        }
    }
}
