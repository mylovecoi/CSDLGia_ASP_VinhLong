using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.Models.Systems.API;
using CSDLGia_ASP.Services;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficeOpenXml.Drawing.Chart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Systems.Auth
{
    public class LoginController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IDsDonviService _dsDonviService;
        public LoginController(CSDLGiaDBContext db, IDsDonviService dsDonviService)
        {
            _db = db;
            _dsDonviService = dsDonviService;
        }

        [Route("DangNhap")]
        [HttpGet]
        public IActionResult Login()
        {
            /*var hethong = _db.tblHeThong;*/
            ViewData["Title"] = "Đăng nhập";
            return View("Views/Admin/Systems/Auth/Login.cshtml");
            /*return View("Views/Admin/Systems/Auth/Login.cshtml", hethong);*/

        }

        [Route("SignIn")]
        [HttpPost]
        public IActionResult SignIn(string username, string password)
        {
            if (username != null && password != null)
            {
                var model = _db.Users.FirstOrDefault(u => u.Username == username);
                //Lấy thông tin trong bảng hệ thống để gán
                var heThong = _db.tblHeThong.FirstOrDefault();
                if (heThong != null)
                {
                    HttpContext.Session.SetString("LinkAPIXacthuc", heThong.LinkAPIXacthuc);
                    HttpContext.Session.SetString("TokenLGSP", heThong.TokenLGSP);
                    HttpContext.Session.SetString("MaDiaBanHanhChinh", heThong.MaDiaBanHanhChinh);
                    HttpContext.Session.SetString("MaDonViThuThap", heThong.MaDonViThuThap);
                }
                //Lấy các thông tin về kết nối API
                //var dsKetNoi = _db.KetNoiAPI_DanhSach.Where(x=>x.Maso=="KOGKFKJ").ToList();
                var dsKetNoi = _db.KetNoiAPI_DanhSach.ToList();

                // Chuyển đổi danh sách các đối tượng thành chuỗi JSON với key là trường khóa
                Dictionary<string, KetNoiAPI_DanhSach> dictionary = dsKetNoi.ToDictionary(item => item.Maso, item => item);
                string json = JsonConvert.SerializeObject(dictionary);

                // Lưu chuỗi JSON vào session
                HttpContext.Session.SetString("LinkAPIKetNoi", json);

                //HttpContext.Session.SetString("LinkAPIKetNoi", JsonConvert.SerializeObject(dsKetNoi ?? null));
                //

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

                                if (model.Level != "DN")
                                {
                                    var model_donvi = _dsDonviService.GetListDonvi(model.Madv);
                                    List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();

                                    var data_nghe = _db.DmNgheKd.Where(t => t.Theodoi == "TD").ToList(); // Lấy toàn bộ dữ liệu ra bằng ToList()

                                    // Lọc dữ liệu sử dụng LINQ to Objects thay vì LINQ to Entities
                                    data_nghe = data_nghe.Where(x => list_madv.Any(v => x.Madv.Split(',').Contains(v))).ToList();
                                   
                                    HttpContext.Session.SetString("KeKhaiDangKyGia", JsonConvert.SerializeObject(data_nghe));
                                }
                                else
                                {
                                    var donvi_nghe = _db.CompanyLvCc.Where(t => t.Madv == model.Madv);
                                    List<string> list_manghe = donvi_nghe.Select(t => t.Manghe).ToList();
                                    var data_nghe = _db.DmNgheKd.Where(t => t.Theodoi == "TD" &&list_manghe.Contains(t.Manghe));
                                    HttpContext.Session.SetString("KeKhaiDangKyGia", JsonConvert.SerializeObject(data_nghe));
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
        [HttpGet("TestList")]
        public IActionResult TestList()
        {
            string Madv = Helpers.GetSsAdmin(HttpContext.Session, "Madv");
            var model_donvi = _dsDonviService.GetListDonvi(Helpers.GetSsAdmin(HttpContext.Session, "Madv"));
            List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();

            var model = _db.DmNgheKd.ToList(); // Lấy toàn bộ dữ liệu ra bằng ToList()

            // Lọc dữ liệu sử dụng LINQ to Objects thay vì LINQ to Entities
            model = model.Where(x => list_madv.Any(v => x.Madv.Split(',').Contains(v))).ToList();
            return Ok(model);
        }
    }
}
