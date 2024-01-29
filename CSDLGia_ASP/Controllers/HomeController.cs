using CSDLGia_ASP.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;
using CSDLGia_ASP.Models.Systems;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using CSDLGia_ASP.Helper;

namespace CSDLGia_ASP.Controllers
{
    public class HomeController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, CSDLGiaDBContext db)
        {
            _logger = logger;
            _db = db;
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                return RedirectToAction("Index", "CongBo");
            }
            else
            {
                var model = _db.Supports.ToList();
                ViewData["Title"] = "Trang chủ";
                ViewData["MenuLv1"] = "menu_home";
                return View("Views/Admin/Home/Index.cshtml", model);
            }
            /*if (string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                ViewData["Title"] = "Login";
                return RedirectToAction("Login", "Login");
            }
            else
            {
                ViewData["Title"] = "Trang chủ";
                ViewData["MenuLv1"] = "menu_home";
                return View("Views/Admin/Home/Index.cshtml");
            }*/
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

        /*public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/
    }
}
