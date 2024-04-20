using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.Services;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class RegisterController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IDsDonviService _dsDonviService;

        public RegisterController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment, IDsDonviService dsDonviService)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
            _dsDonviService = dsDonviService;
        }

        [Route("DangKy/DanhSach")]
        [HttpGet]
        public IActionResult Index(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.nguoidung.dsdangky", "Index"))
                {

                    var users = _db.Users.Where(t => t.Level == "DN").ToList();
                    var company = _db.Company.ToList();
                    var model_join = (from user in users
                                      join com in company on user.Madv equals com.Madv
                                      select new VMUsers
                                      {
                                          Id = user.Id,
                                          Name = user.Name,
                                          Username = user.Username,
                                          Madv = user.Madv,
                                          Status = user.Status,
                                          Lydo = user.Lydo,
                                          Created_at = user.Created_at,
                                          Updated_at = user.Updated_at,
                                          Macqcq = com.Macqcq,
                                      });

                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang == "NHAPLIEU");
                    ViewData["Title"] = "Xét duyệt tài khoản đăng ký";
                    ViewData["MenuLv1"] = "menu_hethong";
                    ViewData["MenuLv2"] = "menu_qtnguoidung";
                    ViewData["MenuLv3"] = "menu_dsdangky";
                    return View("Views/Admin/Systems/Register/Index.cshtml", model_join);
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

        [Route("DoanhNghiep/DangKy")]
        [HttpGet]
        public IActionResult Create()
        {
            var modelct_cxd = _db.CompanyLvCc.Where(t => t.Trangthai == "CXD");
            if (modelct_cxd.Any())
            {
                _db.CompanyLvCc.RemoveRange(modelct_cxd);
                _db.SaveChanges();
            }

            ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "ADMIN").ToList();
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            ViewData["DmNganhKd"] = _db.DmNganhKd.ToList();
            ViewData["DmNgheKd"] = _db.DmNgheKd.Where(t => t.Theodoi == "TD").ToList();
            ViewData["Title"] = "Đăng ký";
            return View("Views/Admin/Systems/Register/Register.cshtml");
        }

        [Route("DoanhNghiep/DangKy/Store")]
        [HttpPost]
        public async Task<IActionResult> Store(VMRegisters request, IFormFile Giayphepkdupload)
        {
            var check_company = _db.Company.FirstOrDefault(c => c.Madv == request.Madv);
            if (check_company == null)
            {
                var check_user = _db.Users.FirstOrDefault(u => u.Username == request.Username);
                if (check_user == null)
                {
                    string md5_password = "";
                    using (MD5 md5Hash = MD5.Create())
                    {
                        string change = Helpers.GetMd5Hash(md5Hash, request.Password);
                        md5_password = change;
                    }

                    var user = new Users
                    {
                        Madv = request.Madv,
                        Name = request.Tendn,
                        Username = request.Username,
                        Password = md5_password,
                        Status = "Chờ xét duyệt",
                        Level = "DN",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.Users.Add(user);
                    _db.SaveChanges();


                    if (Giayphepkdupload != null && Giayphepkdupload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Giayphepkdupload.FileName);
                        string extension = Path.GetExtension(Giayphepkdupload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Giayphepkdupload.CopyToAsync(FileStream);
                        }
                        request.Giayphepkd = filename;
                    }

                    //var Manghe = _db.CompanyLvCc.Where(c => c.Madv == request.Madv).Select(c => c.Manghe).ToList();
                    //var Manghanh = _db.CompanyLvCc.Where(c => c.Madv == request.Madv).Select(c => c.Manganh).ToList();
                    //var LuHanh = Manghe.Any(m => m.Contains("LUHANH")) ? 1 : 0;

                    var company = new Company
                    {
                        Madv = request.Madv,
                        Madiaban = request.Madiaban,
                        Macqcq = request.Macqcq,
                        Tendn = request.Tendn,
                        Diachi = request.Diachi,
                        Tel = request.Tel,
                        Fax = request.Fax,
                        Email = request.Email,
                        Website = request.Website,
                        Diadanh = request.Diadanh,
                        Chucdanh = request.Chucdanh,
                        Nguoiky = request.Nguoiky,
                        Tailieu = request.Tailieu,
                        Giayphepkd = request.Giayphepkd,
                        Trangthai = "Chưa kích hoạt",
                        Level = "DN",
                        Mahs = DateTime.Now.ToString("yyMMddssmmHH") + "_" + request.Madv,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.Company.Add(company);
                    _db.SaveChanges();

                    var model = _db.CompanyLvCc.Where(t => t.Madv == request.Madv);
                    if (model != null)
                    {
                        foreach (var item in model)
                        {
                            item.Trangthai = "XD";
                            item.Mahs = company.Mahs;
                        }
                    }
                    _db.CompanyLvCc.UpdateRange(model);
                    _db.SaveChanges();


                    ViewData["Title"] = "Đăng ký thành công";
                    return View("Views/Admin/Error/PageSuccess.cshtml");
                }
                else
                {
                    var model_join = (from lvkd in _db.CompanyLvCc.Where(t => t.Madv == request.Madv)
                                      join dmnghe in _db.DmNgheKd on lvkd.Manghe equals dmnghe.Manghe
                                      select new VMCompanyLvCc
                                      {
                                          Id = lvkd.Id,
                                          Madv = lvkd.Madv,
                                          Mahs = lvkd.Mahs,
                                          Manghe = lvkd.Manghe,
                                          Macqcq = lvkd.Macqcq,
                                          Tennghe = dmnghe.Tennghe,
                                          Phanloai = dmnghe.Phanloai,
                                      }).ToList();
                    request.VMCompanyLvCc = model_join.ToList();

                    ModelState.AddModelError("Username", "Tài khoản truy cập này đã tồn tại");
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "ADMIN").ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["DmNganhKd"] = _db.DmNganhKd.ToList();
                    ViewData["DmNgheKd"] = _db.DmNgheKd.ToList();
                    ViewData["Title"] = "Đăng ký";
                    return View("Views/Admin/Systems/Register/Register.cshtml", request);
                }
            }
            else
            {
                ModelState.AddModelError("Madv", "Mã số thuế hoặc mã số đăng ký KD này đã tồn tại");
                ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "ADMIN").ToList();
                ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                ViewData["DmNganhKd"] = _db.DmNganhKd.ToList();
                ViewData["DmNgheKd"] = _db.DmNgheKd.ToList();
                ViewData["Title"] = "Đăng ký";
                return View("Views/Admin/Systems/Register/Register.cshtml", request);
            }
        }

        [Route("DangKy/DanhSach/ChiTiet")]
        [HttpGet]
        public IActionResult Show(int Id, string Madiaban, string Madv, string Name, string Username, string Status)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.nguoidung.dsdangky", "Index"))
                {

                    var user_join = (from user in _db.Users.Where(t => t.Id == Id)
                                     join com in _db.Company on user.Madv equals com.Madv
                                     select new VMRegisters
                                     {
                                         Id = user.Id,
                                         Madv = user.Madv,
                                         Username = user.Username,
                                         Status = user.Status,
                                         Lydo = user.Lydo,
                                         Mahs = com.Mahs,
                                         Tendn = com.Tendn,
                                         Diachi = com.Diachi,
                                         Diadanh = com.Diadanh,
                                         Giayphepkd = com.Giayphepkd,
                                         Tel = com.Tel,
                                         Fax = com.Fax,
                                         Email = com.Email,
                                     }).ToList();

                    var model = _db.Users.FirstOrDefault(t => t.Id == Id);
                    var model_com = _db.Company.FirstOrDefault(t => t.Madv == model.Madv);
                    var model_lvkd = _db.CompanyLvCc.Where(t => t.Mahs == model_com.Mahs);

                    ViewData["Madiaban"] = Madiaban;
                    ViewData["Madv"] = Madv;
                    ViewData["Id"] = Id;
                    ViewData["Username"] = Username;
                    ViewData["Name"] = Name;
                    ViewData["Status"] = Status;
                    ViewData["Lvkd"] = model_lvkd;
                    ViewData["DmNgheKd"] = _db.DmNgheKd;
                    ViewData["Title"] = "Chi tiết doanh nghiệp đăng ký";
                    ViewData["MenuLv1"] = "menu_hethong";
                    ViewData["MenuLv2"] = "menu_qtnguoidung";
                    ViewData["MenuLv3"] = "menu_dsdangky";
                    return View("Views/Admin/Systems/Register/Show.cshtml", user_join);
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

        [HttpPost]
        public IActionResult Duyet(int id_duyet, string madiaban_duyet)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.nguoidung.dsdangky", "Approve"))
                {
                    var model = _db.Users.FirstOrDefault(t => t.Id == id_duyet);
                    model.Status = "Kích hoạt";
                    model.Updated_at = DateTime.Now;
                    _db.Users.Update(model);
                    _db.SaveChanges();

                    var com = _db.Company.FirstOrDefault(t => t.Madv == model.Madv);
                    com.Trangthai = "Kích hoạt";
                    com.Updated_at = DateTime.Now;
                    _db.Company.Update(com);
                    _db.SaveChanges();


                    return RedirectToAction("Index", "Register", new { Madiaban = madiaban_duyet });
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

        [HttpPost]
        public IActionResult Tralai(int id_tralai, string lydo_tralai, string madiaban_tralai)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.nguoidung.dsdangky", "Approve"))
                {
                    var model = _db.Users.FirstOrDefault(t => t.Id == id_tralai);
                    model.Status = "Bị trả lại";
                    model.Updated_at = DateTime.Now;
                    model.Lydo = lydo_tralai;
                    _db.Users.Update(model);
                    _db.SaveChanges();


                    return RedirectToAction("Index", "Register", new { Madiaban = madiaban_tralai });
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
