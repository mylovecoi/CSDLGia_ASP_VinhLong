using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
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

        public RegisterController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("DangKy/DanhSach_Cu")]
        [HttpGet]
        public IActionResult Index_Cu(string Madiaban)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.nguoidung.dsdangky", "Index"))
                {
                    var dsdiaban = _db.DsDiaBan.Where(t => t.Level != "ADMIN");

                    if (string.IsNullOrEmpty(Madiaban))
                    {
                        Madiaban = dsdiaban.OrderBy(t => t.Id).Select(t => t.MaDiaBan).First();
                    }
                    var users = _db.Users.Where(t => t.Level == "DN" && t.Status != "Kích hoạt").ToList();
                    var coms = _db.Company.Where(t => t.Madiaban == Madiaban).ToList();
                    var model_join = (from user in users
                                      join com in coms on user.Madv equals com.Madv
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
                                      });

                    ViewData["DsDiaBan"] = dsdiaban;
                    ViewData["Madiaban"] = Madiaban;
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


        [Route("DangKy/DanhSach")]
        [HttpGet]
        public IActionResult Index(string Madiaban)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.nguoidung.dsdangky", "Index"))
                {
                    var dsdiaban = _db.DsDiaBan.Where(t => t.Level != "ADMIN");

                    if (string.IsNullOrEmpty(Madiaban))
                    {
                        Madiaban = dsdiaban.OrderBy(t => t.Id).Select(t => t.MaDiaBan).First();
                    }

                    var users = _db.Users.Where(t => t.Level == "DN").ToList();
                    var coms = _db.Company.Where(t => t.Madiaban == Madiaban).ToList();
                    var model_join = (from user in users
                                      join com in coms on user.Madv equals com.Madv
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
                                      });

                    ViewData["DsDiaBan"] = dsdiaban;
                    ViewData["Madiaban"] = Madiaban;
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
                                     join lvkd in _db.CompanyLvCc on com.Mahs equals lvkd.Mahs
                                     join dmnghe in _db.DmNgheKd on lvkd.Manghe equals dmnghe.Manghe
                                     select new VMRegisters
                                     {
                                         Id = user.Id,
                                         Madv = user.Madv,
                                         Tendn = com.Tendn,
                                         Diachi = com.Diachi,
                                         Tel = com.Tel,
                                         Fax = com.Fax,
                                         Email = com.Email,
                                         Diadanh = com.Diadanh,
                                         Username = user.Username,
                                         Mahs = com.Mahs,
                                         Tennghe = dmnghe.Tennghe,
                                         Status = user.Status,
                                         Lydo = user.Lydo,
                                     }).ToList();

                    ViewData["Madiaban"] = Madiaban;
                    ViewData["Madv"] = Madv;
                    ViewData["Id"] = Id;
                    ViewData["Username"] = Username;
                    ViewData["Name"] = Name;
                    ViewData["Status"] = Status;
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

        [Route("DoanhNghiep/DangKy")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "ADMIN").ToList();
            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
            ViewData["DmNganhKd"] = _db.DmNganhKd.ToList();
            ViewData["DmNgheKd"] = _db.DmNgheKd.ToList();
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

                    var Manghe = _db.CompanyLvCc.Where(c => c.Madv == request.Madv).Select(c => c.Manghe).ToList();

                    var XmThepXd = Manghe.Any(m => m.Contains("XMTXD")) ? 1 : 0;
                    var SachGk = Manghe.Any(m => m.Contains("SACH")) ? 1 : 0;
                    var Etanol = Manghe.Any(m => m.Contains("ETANOL")) ? 1 : 0;
                    var ThucPhamCn = Manghe.Any(m => m.Contains("TPCNTE6T")) ? 1 : 0;
                    var VlXdCatSan = Manghe.Any(m => m.Contains("CATSAN")) ? 1 : 0;
                    var HocPhiDaoTaoLaiXe = Manghe.Any(m => m.Contains("HOCPHILX")) ? 1 : 0;
                    var Than = Manghe.Any(m => m.Contains("THAN")) ? 1 : 0;
                    var Giay = Manghe.Any(m => m.Contains("GIAY")) ? 1 : 0;
                    var ThucAnChanNuoi = Manghe.Any(m => m.Contains("TACN")) ? 1 : 0;
                    var VlXdDatSanlap = Manghe.Any(m => m.Contains("DATSANLAP")) ? 1 : 0;
                    var VanTaiKhachBangOtoCoDinh = Manghe.Any(m => m.Contains("VTXK")) ? 1 : 0;
                    var VanTaiKhachBangTaXi = Manghe.Any(m => m.Contains("VTXTX")) ? 1 : 0;
                    var CaHue = Manghe.Any(m => m.Contains("CAHUE")) ? 1 : 0;
                    var SieuThi = Manghe.Any(m => m.Contains("SIEUTHI")) ? 1 : 0;
                   
                    var company = new Company
                    {
                        XmThepXd = XmThepXd,
                        SachGk = SachGk,
                        Etanol = XmThepXd,
                        ThucPhamCn = XmThepXd,
                        VlXdCatSan = VlXdCatSan,
                        HocPhiDaoTaoLaiXe = HocPhiDaoTaoLaiXe,
                        Than = Than,
                        Giay = Giay,
                        ThucAnChanNuoi = ThucAnChanNuoi,
                        VlXdDatSanlap = VlXdDatSanlap,
                        VanTaiKhachBangOtoCoDinh = VanTaiKhachBangOtoCoDinh,
                        VanTaiKhachBangTaXi = VanTaiKhachBangTaXi,
                        CaHue = CaHue,
                        SieuThi = SieuThi,
                        Madv = request.Madv,
                        Madiaban = request.Madiaban,
                        Tendn = request.Tendn,
                        Diachi = request.Diachi,
                        Tel = request.Tel,
                        Fax = request.Fax,
                        Email = request.Email,
                        Diadanh = request.Diadanh,
                        Chucdanh = request.Chucdanh,
                        Nguoiky = request.Nguoiky,
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
