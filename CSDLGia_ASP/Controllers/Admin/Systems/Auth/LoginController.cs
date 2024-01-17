using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml.Drawing.Chart;
using System.Linq;
using System.Security.Cryptography;

namespace CSDLGia_ASP.Controllers.Admin.Systems.Auth
{
    public class LoginController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        public LoginController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DangNhap")]
        [HttpGet]
        public IActionResult Login()
        {
            var hethong = _db.tblHeThong;
            ViewData["Title"] = "Đăng nhập";
            return View("Views/Admin/Systems/Auth/Login.cshtml", hethong);
        }

        [Route("SignIn")]
        [HttpPost]
        public IActionResult SignIn(string username, string password)
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
                            ModelState.AddModelError("username", "Tài khoản chưa được kích hoạt. Liên hệ với quản trị hệ thống !!!");
                            ViewData["username"] = username;
                            ViewData["password"] = password;
                            return View("Views/Admin/Systems/Auth/Login.cshtml");
                        } 
                        else 
                        {
                            if (model.Status == "Vô hiệu")
                            {
                                ModelState.AddModelError("username", "Tài khoản bị khóa. Liên hệ với quản trị hệ thống !!!");
                                ViewData["username"] = username;
                                ViewData["password"] = password;
                                return View("Views/Admin/Systems/Auth/Login.cshtml");
                            }
                            else
                            {
                                HttpContext.Session.SetString("SsAdmin", JsonConvert.SerializeObject(model));
                                if (model.Chucnang == "K")
                                {
                                    var permissions = _db.Permissions.Where(p => p.Username == username);
                                    HttpContext.Session.SetString("Permission", JsonConvert.SerializeObject(permissions));
                                }
                                else
                                {
                                    var permissions = _db.Permissions.Where(p => p.Username == model.Chucnang);
                                    HttpContext.Session.SetString("Permission", JsonConvert.SerializeObject(permissions));
                                }
                                return RedirectToAction("Index", "Home");
                            }
                        }
                        
                            /*if (model.Status == "Vô hiệu")
                            {
                                ModelState.AddModelError("username", "Tài khoản bị khóa. Liên hệ với quản trị hệ thống !!!");
                                ViewData["username"] = username;
                                ViewData["password"] = password;
                                return View("Views/Admin/Systems/Auth/Login.cshtml");
                            }
                            else
                            {
                                HttpContext.Session.SetString("SsAdmin", JsonConvert.SerializeObject(model));
                                if (model.Chucnang == "K")
                                {
                                    var permissions = _db.Permissions.Where(p => p.Username == username);
                                    HttpContext.Session.SetString("Permission", JsonConvert.SerializeObject(permissions));
                                }
                                else
                                {
                                    var permissions = _db.Permissions.Where(p => p.Username == model.Chucnang);
                                    HttpContext.Session.SetString("Permission", JsonConvert.SerializeObject(permissions));
                                }
                                return RedirectToAction("Index", "Home");
                            }*/
                           
                       
                    }
                    else
                    {
                        ModelState.AddModelError("password", "Mật khẩu truy cập không đúng !!!");
                        ViewData["username"] = username;
                        ViewData["Title"] = "Đăng nhập";
                        return View("Views/Admin/Systems/Auth/Login.cshtml");
                    }
                }
                else
                {
                    ModelState.AddModelError("username", "Tài khoản truy cập không tồn tại !!!");
                    ViewData["Title"] = "Đăng nhập";
                    return View("Views/Admin/Systems/Auth/Login.cshtml");
                }
            }
            else
            {
                ModelState.AddModelError("username", "Tài khoản truy cập không được để trống !!!");
                ModelState.AddModelError("password", "Mật khẩu truy cập không được để trống !!!");
                ViewData["Title"] = "Đăng nhập";
                return View("Views/Admin/Systems/Auth/Login.cshtml");
            }
        }

        [Route("DangXuat")]
        [HttpPost]
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("Permission");
            HttpContext.Session.Remove("SsAdmin");
            return RedirectToAction("Login", "Login");
        }
    }
}
