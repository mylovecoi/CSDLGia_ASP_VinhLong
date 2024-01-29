using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class DsTaiKhoanController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public DsTaiKhoanController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DsTaiKhoan")]
        [HttpGet]
        public IActionResult Index(string MaDv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.nguoidung.dstaikhoan", "Index"))
                {
                    var dsdiaban = _db.DsDiaBan.ToList();
                    var dsdonvi = _db.DsDonVi.ToList();
                    if (string.IsNullOrEmpty(MaDv))
                    {
                        MaDv = dsdonvi.OrderBy(t => t.Id).Select(t => t.MaDv).First();
                    }
                    var model = _db.Users.Where(t => t.Madv == MaDv).ToList();
                    var model_join = (from user in model
                                      join nhom in _db.GroupPermissions on user.Chucnang equals nhom.KeyLink
                                      select new VMUsers
                                      {
                                          Id = user.Id,
                                          Madv = user.Madv,
                                          Name = user.Name,
                                          Username = user.Username,
                                          Chucnang = user.Chucnang,
                                          TenChucnang = nhom.ChucNang,
                                          Status = user.Status
                                      });

                    ViewData["DsDonVi"] = dsdonvi;
                    ViewData["DsDiaBan"] = dsdiaban;
                    ViewData["MaDv"] = MaDv;
                    ViewData["Title"] = "Danh sách tài khoản đơn vị";
                    ViewData["MenuLv1"] = "menu_hethong";
                    ViewData["MenuLv2"] = "menu_qtnguoidung";
                    ViewData["MenuLv3"] = "menu_dstaikhoan";
                    return View("Views/Admin/Systems/DsTaiKhoan/Index.cshtml", model_join);
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

        [Route("DsTaiKhoan/Create")]
        [HttpGet]
        public IActionResult Create(string MaDv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.nguoidung.dstaikhoan", "Create"))
                {
                    ViewData["ChucNang"] = _db.GroupPermissions.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.MaDv == MaDv).ToList();
                    ViewData["MaDv"] = MaDv;
                    ViewData["Title"] = "Thêm mới thông tin tài khoản";
                    ViewData["MenuLv1"] = "menu_hethong";
                    ViewData["MenuLv2"] = "menu_qtnguoidung";
                    ViewData["MenuLv3"] = "menu_dstaikhoan";

                    return View("Views/Admin/Systems/DsTaiKhoan/Create.cshtml");
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

        [Route("DsTaiKhoan/Store")]
        [HttpPost]
        public IActionResult Store(Users request, string nhaplieu, string tonghop, string quantri)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.nguoidung.dstaikhoan", "Create"))
                {
                    var check = _db.Users.FirstOrDefault(u => u.Username == request.Username);
                    if (check == null)
                    {
                        string md5_password = "";
                        using (MD5 md5Hash = MD5.Create())
                        {
                            string change = Helpers.GetMd5Hash(md5Hash, request.Password);
                            md5_password = change;
                        }

                        /*string chucnang = "";
                        chucnang += !string.IsNullOrEmpty(nhaplieu) ? "NHAPLIEU;" : "";
                        chucnang += !string.IsNullOrEmpty(tonghop) ? "TONGHOP;" : "";
                        chucnang += !string.IsNullOrEmpty(quantri) ? "QUANTRI;" : "";*/

                        var model = new Users
                        {
                            Madv = request.Madv,
                            Name = request.Name,
                            Status = request.Status,
                            Chucnang = request.Chucnang,
                            Username = request.Username,
                            Password = md5_password,
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        };
                        _db.Users.Add(model);
                        _db.SaveChanges();
                        return RedirectToAction("Index", "DsTaiKhoan", new { MaDv = request.Madv });
                    }
                    else
                    {
                        ModelState.AddModelError("Username", "Tài khoản truy cập này đã tồn tại");

                        /*ViewData["nhaplieu"] = nhaplieu;
                        ViewData["tonghop"] = tonghop;
                        ViewData["quantri"] = quantri;*/
                        ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.MaDv == request.Madv).ToList();
                        ViewData["MaDv"] = request.Madv;
                        ViewData["Title"] = "Thêm mới thông tin tài khoản";
                        ViewData["MenuLv1"] = "menu_hethong";
                        ViewData["MenuLv2"] = "menu_qtnguoidung";
                        ViewData["MenuLv3"] = "menu_dstaikhoan";
                        return View("Views/Admin/Systems/DsTaiKhoan/Create.cshtml");
                    }
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

        [Route("DsTaiKhoan/Edit")]
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.nguoidung.dstaikhoan", "Edit"))
                {
                    var model = _db.Users.FirstOrDefault(t => t.Id == Id);

                    ViewData["ChucNang"] = _db.GroupPermissions.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.MaDv == model.Madv).ToList();
                    ViewData["MaDv"] = model.Madv;
                    /*ViewData["nhaplieu"] = (model.Chucnang == "NHAPLIEU;" || model.Chucnang == "NHAPLIEU;TONGHOP;") ? "NHAPLIEU;" : "";
                    ViewData["tonghop"] = (model.Chucnang == "TONGHOP;" || model.Chucnang == "NHAPLIEU;TONGHOP;") ? "TONGHOP;" : "";
                    ViewData["quantri"] = (model.Chucnang == "QUANTRI;") ? "QUANTRI;" : "";*/
                    ViewData["Title"] = "Chỉnh sửa thông tin tài khoản";
                    ViewData["MenuLv1"] = "menu_hethong";
                    ViewData["MenuLv2"] = "menu_qtnguoidung";
                    ViewData["MenuLv3"] = "menu_dstaikhoan";

                    return View("Views/Admin/Systems/DsTaiKhoan/Edit.cshtml", model);
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

        [Route("DsTaiKhoan/Update")]
        [HttpPost]
        public IActionResult Update(Users request, string nhaplieu, string tonghop, string quantri, string NewPassword)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.nguoidung.dstaikhoan", "Edit"))
                {
                    var check = _db.Users.FirstOrDefault(u => u.Username == request.Username && u.Id != request.Id);
                    if (check == null)
                    {
                        var model = _db.Users.FirstOrDefault(t => t.Id == request.Id);

                        if (!string.IsNullOrEmpty(NewPassword))
                        {
                            string md5_password = "";
                            using (MD5 md5Hash = MD5.Create())
                            {
                                string change = Helpers.GetMd5Hash(md5Hash, NewPassword);
                                md5_password = change;
                            }
                            model.Password = md5_password;
                        }

                        /*string chucnang = "";
                        chucnang += !string.IsNullOrEmpty(nhaplieu) ? "NHAPLIEU;" : "";
                        chucnang += !string.IsNullOrEmpty(tonghop) ? "TONGHOP;" : "";
                        chucnang += !string.IsNullOrEmpty(quantri) ? "QUANTRI;" : "";*/

                        model.Name = request.Name;
                        model.Status = request.Status;
                        model.Chucnang = request.Chucnang;
                        model.Username = request.Username;
                        model.Updated_at = DateTime.Now;

                        _db.Users.Update(model);
                        _db.SaveChanges();
                        return RedirectToAction("Index", "DsTaiKhoan", new { MaDv = request.Madv });
                    }
                    else
                    {
                        ModelState.AddModelError("Username", "Tài khoản truy cập này đã tồn tại");

                        /*ViewData["nhaplieu"] = nhaplieu;
                        ViewData["tonghop"] = tonghop;
                        ViewData["quantri"] = quantri;*/
                        ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.MaDv == request.Madv).ToList();
                        ViewData["MaDv"] = request.Madv;
                        ViewData["Title"] = "Chỉnh sửa thông tin tài khoản";
                        ViewData["MenuLv1"] = "menu_hethong";
                        ViewData["MenuLv2"] = "menu_qtnguoidung";
                        ViewData["MenuLv3"] = "menu_dstaikhoan";
                        return View("Views/Admin/Systems/DsTaiKhoan/Edit.cshtml");
                    }
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

        [Route("DsTaiKhoan/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.nguoidung.dstaikhoan", "Delete"))
                {
                    var model = _db.Users.FirstOrDefault(t => t.Id == id_delete);
                    _db.Users.Remove(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "DsTaiKhoan", new { MaDv = model.Madv });
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

        [Route("DsTaiKhoan/Permission")]
        [HttpGet]
        public IActionResult Permissions(string Username, string Chucnang)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.nguoidung.dstaikhoan", "Index"))
                {
                    var model = _db.Permissions.Where(t => t.Username == Chucnang);
                    ViewData["Username"] = Username;
                    ViewData["Chucnang"] = Chucnang;
                    ViewData["Title"] = "Thông tin quyền truy cập";
                    ViewData["MenuLv1"] = "menu_hethong";
                    ViewData["MenuLv2"] = "menu_qtnguoidung";
                    ViewData["MenuLv3"] = "menu_dstaikhoan";
                    return View("Views/Admin/Systems/DsTaiKhoan/PermissionCustom.cshtml", model);
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
