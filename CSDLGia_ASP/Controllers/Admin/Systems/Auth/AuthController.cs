using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;

namespace CSDLGia_ASP.Controllers.Admin.Systems.Auth
{
    public class AuthController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IDsDonviService _dsDonviService;
        private readonly EmailService _emailService;
        public AuthController(CSDLGiaDBContext db, IDsDonviService dsDonviService, EmailService emailService)
        {
            _db = db;
            _dsDonviService = dsDonviService;
            _emailService = emailService;
        }

        [HttpGet("LogIn")]
        public IActionResult Login()
        {
            ViewData["Title"] = "Đăng nhập";
            return View("Views/Admin/Systems/Auth/Auth.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(string username, string password)
        {
            if (username != null && password != null)
            {
                var model = _db.Users.FirstOrDefault(u => u.Username == username);

                if (model != null)
                {
                    string md5_password = "";
                    using (MD5 md5Hash = MD5.Create())
                    {
                        string change = Helpers.GetMd5Hash(md5Hash, password);
                        md5_password = change;
                    }
                    if (md5_password == model.Password)
                    {
                        if (model.Status == "Chờ xét duyệt")
                        {
                            ModelState.AddModelError("error", "Tài khoản chưa được kích hoạt. Liên hệ với quản trị hệ thống !!!");
                            return View("Views/Admin/Systems/Auth/Auth.cshtml");
                        }
                        else
                        {
                            if (model.Status == "Vô hiệu")
                            {
                                ModelState.AddModelError("error", "Tài khoản bị khóa. Liên hệ với quản trị hệ thống !!!");
                                return View("Views/Admin/Systems/Auth/Auth.cshtml");
                            }
                            else
                            {
                                var danhsachykien = _db.YKienGopY.Count();
                                HttpContext.Session.SetString("DanhSachYKienDongGop", danhsachykien.ToString());

                                HttpContext.Session.SetString("SsAdmin", JsonConvert.SerializeObject(model));

                                var permissions = _db.Permissions.Where(t => t.Index);
                                if (model.Chucnang == "K")
                                {
                                    permissions = permissions.Where(p => p.Username == username);
                                }
                                else
                                {
                                    permissions = permissions.Where(p => p.Username == model.Chucnang);
                                }

                                //var data_nghe = _db.DmNgheKd.Where(t => t.Theodoi == "TD");
                                List<DmNgheKd> data_nghe = _db.DmNgheKd.Where(t => t.Theodoi == "TD").ToList();
                                if (model.Level != "DN")
                                {
                                    var model_donvi = _dsDonviService.GetListDonvi(model.Madv);
                                    List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();

                                    //data_nghe = _db.DmNgheKd.Where(t => t.Theodoi == "TD").ToList(); // Lấy toàn bộ dữ liệu ra bằng ToList()

                                    // Lọc dữ liệu sử dụng LINQ to Objects thay vì LINQ to Entities
                                    data_nghe = data_nghe.Where(x => list_madv.Any(v => x.Madv.Split(',').Contains(v))).ToList();
                                }
                                else
                                {
                                    var donvi_nghe = _db.CompanyLvCc.Where(t => t.Madv == model.Madv);
                                    List<string> list_manghe = donvi_nghe.Select(t => t.Manghe).ToList();
                                    data_nghe = data_nghe.Where(t => t.Theodoi == "TD" && list_manghe.Contains(t.Manghe)).ToList();
                                }
                                HttpContext.Session.SetString("Permission", JsonConvert.SerializeObject(permissions));
                                HttpContext.Session.SetString("KeKhaiDangKyGia", JsonConvert.SerializeObject(data_nghe));
                                //HttpContext.Session.SetString("TimeStart", Helper.Helpers.ConvertDateTimeToStr(DateTime.Now));

                                // Add Cookie
                                var claims = new List<Claim>
                                {
                                    new Claim(ClaimTypes.Name, model.Username),
                                };


                                // Tạo ClaimsIdentity và ClaimsPrincipal
                                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                                // Thiết lập thời gian hết hạn cho cookie
                                var authProperties = new AuthenticationProperties
                                {
                                    IsPersistent = true, // Để cookie tồn tại ngay cả khi đóng trình duyệt
                                };

                                // Lưu thông tin vào Cookie
                                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                                // End Add Cookie

                                // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                                LoggingHelper.LogAction(HttpContext, _db, "DANGNHAP", "Đăng nhập");

                                return RedirectToAction("Index", "Home");
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("error", "Tài khoản/ Mật khẩu truy cập không đúng !!!");
                        ViewData["Title"] = "Đăng nhập chương trình";
                        return View("Views/Admin/Systems/Auth/Auth.cshtml");
                    }
                }
                else
                {
                    ModelState.AddModelError("error", "Tài khoản/ Mật khẩu truy cập không đúng !!!");
                    ViewData["Title"] = "Đăng nhập chương trình";
                    return View("Views/Admin/Systems/Auth/Auth.cshtml");
                }
            }
            else
            {
                ModelState.AddModelError("error", "Tài khoản/Mật khẩu truy cập không được để trống !!!");
                ViewData["Title"] = "Đăng nhập chương trình";
                return View("Views/Admin/Systems/Auth/Auth.cshtml");
            }
        }

        [HttpPost("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
            LoggingHelper.LogAction(HttpContext, _db, "DANGXUAT", "Đăng xuất");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Permission");
            HttpContext.Session.Remove("SsAdmin");
            HttpContext.Session.Remove("KeKhaiDangKyGia");
            //HttpContext.Session.Remove("TimeStart");
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> SSO(string emailSSO)
        {
            var model = _db.Users.FirstOrDefault(t => t.Email == emailSSO);
            if (model != null)
            {
                if (model.Status == "Chờ xét duyệt")
                {
                    ModelState.AddModelError("error", "Tài khoản chưa được kích hoạt. Liên hệ với quản trị hệ thống !!!");
                    return View("Views/Admin/Systems/Auth/Auth.cshtml");
                }
                else
                {
                    if (model.Status == "Vô hiệu")
                    {
                        ModelState.AddModelError("error", "Tài khoản bị khóa. Liên hệ với quản trị hệ thống !!!");
                        return View("Views/Admin/Systems/Auth/Auth.cshtml");
                    }
                    else
                    {
                        var danhsachykien = _db.YKienGopY.Count();
                        HttpContext.Session.SetString("DanhSachYKienDongGop", danhsachykien.ToString());

                        HttpContext.Session.SetString("SsAdmin", JsonConvert.SerializeObject(model));

                        var permissions = _db.Permissions.Where(t => t.Index);
                        if (model.Chucnang == "K")
                        {
                            permissions = permissions.Where(p => p.Username == model.Username);
                        }
                        else
                        {
                            permissions = permissions.Where(p => p.Username == model.Chucnang);
                        }

                        //var data_nghe = _db.DmNgheKd.Where(t => t.Theodoi == "TD");
                        List<DmNgheKd> data_nghe = _db.DmNgheKd.Where(t => t.Theodoi == "TD").ToList();
                        if (model.Level != "DN")
                        {
                            var model_donvi = _dsDonviService.GetListDonvi(model.Madv);
                            List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();

                            //data_nghe = _db.DmNgheKd.Where(t => t.Theodoi == "TD").ToList(); // Lấy toàn bộ dữ liệu ra bằng ToList()

                            // Lọc dữ liệu sử dụng LINQ to Objects thay vì LINQ to Entities
                            data_nghe = data_nghe.Where(x => list_madv.Any(v => x.Madv.Split(',').Contains(v))).ToList();
                        }
                        else
                        {
                            var donvi_nghe = _db.CompanyLvCc.Where(t => t.Madv == model.Madv);
                            List<string> list_manghe = donvi_nghe.Select(t => t.Manghe).ToList();
                            data_nghe = data_nghe.Where(t => t.Theodoi == "TD" && list_manghe.Contains(t.Manghe)).ToList();
                        }
                        HttpContext.Session.SetString("Permission", JsonConvert.SerializeObject(permissions));
                        HttpContext.Session.SetString("KeKhaiDangKyGia", JsonConvert.SerializeObject(data_nghe));

                        // Add Cookie
                        var claims = new List<Claim>
                                {
                                    new Claim(ClaimTypes.Name, model.Username),
                                };

                        // Tạo ClaimsIdentity và ClaimsPrincipal
                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                        // Thiết lập thời gian hết hạn cho cookie
                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = true, // Để cookie tồn tại ngay cả khi đóng trình duyệt
                        };

                        // Lưu thông tin vào Cookie
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                        // End Add Cookie

                        // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                        LoggingHelper.LogAction(HttpContext, _db, "DANGNHAP", "Đăng nhập");

                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            else
            {
                ViewData["Messages"] = "Có lỗi xẩy ra! Bạn cần kiểm tra lại";
                return View("Views/Admin/Page/Error.cshtml");
            }
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string username, string email)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(email))
            {
                var model = _db.Users.FirstOrDefault(t => t.Email == email && t.Username == username);
                if (model != null)
                {
                    string md5_password = "";
                    using (MD5 md5Hash = MD5.Create())
                    {
                        string change = Helpers.GetMd5Hash(md5Hash, "Life@2012!");
                        md5_password = change;
                    }
                    model.Password = md5_password;
                    _db.Users.Update(model);
                    _db.SaveChanges();

                    await _emailService.SendEmailAsync($"{email}", "Lấy lại mật khẩu truy cập", $"Mật khẩu của tài khoản: {username} được đổi thành Life@2012! .");

                    ViewData["Messages"] = $"Mật khẩu của tài khoản: {username} đã được gửi về hòm thư: {email}. Vui lòng đăng nhập email để lấy mật khẩu!!!";
                    return View("Views/Admin/Page/Success.cshtml");
                }
                else
                {
                    ViewData["Messages"] = "Có lỗi xẩy ra! Bạn cần kiểm tra lại";
                    return View("Views/Admin/Page/Error.cshtml");
                }
            }
            else
            {

                ViewData["Messages"] = "Có lỗi xẩy ra! Bạn cần kiểm tra lại";
                return View("Views/Admin/Page/Error.cshtml");
            }
        }
    }
}
