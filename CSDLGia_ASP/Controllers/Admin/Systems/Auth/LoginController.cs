using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Migrations;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml.Drawing.Chart;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Xml.Linq;

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

                if(model.Level != "DN")
                {
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
                    var model1 = _db.Users.FirstOrDefault(u => u.Username == username);

                    var model2 = _db.Users
                                .Where(u => u.Username == username)
                                .Join(
                                    _db.Company,
                                    user => user.Madv,
                                    company => company.Madv,
                                    (user, company) => new Users
                                    {
                                        Id = user.Id,
                                        Username = user.Username,
                                        Password = user.Password,
                                        Name = user.Name,
                                        Phone = user.Phone,
                                        Email = user.Email,
                                        Status = user.Status,
                                        Maxa = user.Maxa,
                                        Mahuyen = user.Mahuyen,
                                        Town = user.Town,
                                        District = user.District,
                                        Level = user.Level,
                                        Sadmin = user.Sadmin,
                                        Permission = user.Permission,
                                        Emailxt = user.Emailxt,
                                        Question = user.Question,
                                        Answer = user.Answer,
                                        Ttnguoitao = user.Ttnguoitao,
                                        Lydo = user.Lydo,
                                        Madv = user.Madv,
                                        Created_at = user.Created_at,
                                        Updated_at = user.Updated_at,
                                        Manhomtk = user.Manhomtk,
                                        Chucnang = user.Chucnang,
                                        Solandn = user.Solandn,
                                        LinkAPI = user.LinkAPI,
                                        Manghanh = user.Manghanh,
                                        Manghe = user.Manghe,
                                        Vtxk = company.Vtxk,
                                        Vtxb = company.Vtxb,
                                        Vtxtx = company.Vtxtx,
                                        Vtch = company.Vtch,
                                        Xangdau = company.Xangdau,
                                        Dien = company.Dien,
                                        Khidau = company.Khidau,
                                        Phan = company.Phan,
                                        Thuocbvtv = company.Thuocbvtv,
                                        Vacxingsgc = company.Vacxingsgc,
                                        Muoi = company.Muoi,
                                        Suate6t = company.Suate6t,
                                        Duong = company.Duong,
                                        Thocgao = company.Thocgao,
                                        Thuocpcb = company.Thuocpcb,

                                        XmThepXd = company.XmThepXd,
                                        SachGk = company.SachGk,
                                        Etanol = company.Etanol,
                                        ThucPhamCn = company.ThucPhamCn,
                                        VlXdCatSan = company.VlXdCatSan,
                                        HocPhiDaoTaoLaiXe = company.HocPhiDaoTaoLaiXe,
                                        Than = company.Than,
                                        Giay = company.Giay,
                                        ThucAnChanNuoi = company.ThucAnChanNuoi,
                                        VlXdDatSanlap = company.VlXdDatSanlap,
                                        VlXdDaXayDung = company.VlXdDaXayDung,
                                        VanTaiKhachBangOtoCoDinh = company.VanTaiKhachBangOtoCoDinh,
                                        VanTaiKhachBangXeBuyt = company.VanTaiKhachBangXeBuyt,
                                        CaHue = company.CaHue,
                                        SieuThi = company.SieuThi,

                                        BOG = company.BOG,
                                        KKNYGIA = company.KKNYGIA,
                                    }
                                ).FirstOrDefault();

                    if (model1 != null && !string.IsNullOrEmpty(model1.Madv))
                    {
                        model = model2;
                    }
                    else
                    {
                        model = model1;
                    }

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
