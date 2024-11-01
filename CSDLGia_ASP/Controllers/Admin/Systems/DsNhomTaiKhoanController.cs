using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.ViewModels.Systems;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class DsNhomTaiKhoanController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public DsNhomTaiKhoanController(CSDLGiaDBContext db)
        {
            _db = db;
        }
        [Route("DsNhomTaiKhoan")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.nguoidung.dsnhomtaikhoan", "Index"))
                {
                    var model = _db.GroupPermissions;
                    ViewData["Title"] = "Danh sách nhóm tài khoản";
                    
                    ViewData["MenuLv1"] = "menu_qtnguoidung";
                    ViewData["MenuLv2"] = "menu_dsnhomtaikhoan";
                    return View("Views/Admin/Systems/DsNhomTaiKhoan/Index.cshtml", model);
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

        [Route("DsNhomTaiKhoan/Create")]
        [HttpGet]
        public IActionResult Create(string ChucNang_create)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.nguoidung.dsnhomtaikhoan", "Create"))
                {
                    var model_del = _db.Permissions.Where(t => t.Status == "Disable");
                    _db.Permissions.RemoveRange(model_del);
                    _db.SaveChanges();
                    string KeyLink = "Per_" + DateTime.Now.ToString("yymmssfff");
                    var model = new VMGroupPermissions { KeyLink = KeyLink };

                    var data_rolelist = _db.RoleList.Where(t => t.TrangThai == "Active").ToList();

                    var per = new List<Permissions>();
                    foreach (var item in data_rolelist)
                    {
                        per.Add(new Permissions
                        {
                            Username = KeyLink,
                            Roles = item.Role,
                            Name = item.Name,
                            Index = true,
                            Create = false,
                            Edit = false,
                            Delete = false,
                            Approve = false,
                            Public = false,
                            Status = "Disable",
                            Magoc = item.MaGoc,
                            Level = item.Level,
                            Sttsx = item.STTSapXep,
                            Phanloai = item.PhanLoai,
                        });
                    }
                    _db.Permissions.AddRange(per);
                    _db.SaveChanges();
                    model.Permissions = per.Where(t => t.Username == model.KeyLink).ToList();

                    ViewData["ChucNang"] = ChucNang_create;
                    ViewData["RoleList"] = data_rolelist;
                    ViewData["Title"] = "Danh sách nhóm tài khoản";
                    
                    ViewData["MenuLv1"] = "menu_qtnguoidung";
                    ViewData["MenuLv2"] = "menu_dsnhomtaikhoan";
                    return View("Views/Admin/Systems/DsNhomTaiKhoan/Create.cshtml", model);
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

        [Route("DsNhomTaiKhoan/Store")]
        [HttpPost]
        public IActionResult Store(VMGroupPermissions requests)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.nguoidung.dsnhomtaikhoan", "Create"))
                {
                    this.GroupPermission(requests);
                    this.Permission(requests.KeyLink);
                    return RedirectToAction("Index", "DsNhomTaiKhoan");
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

        [Route("DsNhomTaiKhoan/Edit")]
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.nguoidung.dsnhomtaikhoan", "Edit"))
                {
                    var model_group = _db.GroupPermissions.FirstOrDefault(t => t.Id == Id);
                    var model_per = _db.Permissions.Where(t => t.Username == model_group.KeyLink);
                    var model = new VMGroupPermissions
                    {
                        KeyLink = model_group.KeyLink,
                        TenNhomQ = model_group.TenNhomQ,
                        ChucNang = model_group.ChucNang,
                        Id = model_group.Id,
                        Permissions = model_per.ToList()
                    };

                    var data_rolelist = _db.RoleList.Where(t => t.TrangThai == "Active").ToList();

                    ViewData["RoleList"] = data_rolelist;
                    ViewData["Title"] = "Thông tin quyền truy cập";
                    
                    ViewData["MenuLv1"] = "menu_qtnguoidung";
                    ViewData["MenuLv2"] = "menu_dsnhomtaikhoan";
                    return View("Views/Admin/Systems/DsNhomTaiKhoan/Edit.cshtml", model);
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

        [Route("DsNhomTaiKhoan/Update")]
        [HttpPost]
        public IActionResult Update(VMGroupPermissions requests)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.nguoidung.dsnhomtaikhoan", "Edit"))
                {
                    this.GroupPermission(requests);
                    this.Permission(requests.KeyLink);
                    ViewData["Title"] = "Thông tin quyền truy cập";
                    
                    ViewData["MenuLv1"] = "menu_qtnguoidung";
                    ViewData["MenuLv2"] = "menu_dsnhomtaikhoan";
                    return RedirectToAction("Index", "DsNhomTaiKhoan");
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

        [Route("DsNhomTaiKhoan/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.nguoidung.dsnhomtaikhoan", "Delete"))
                {
                    var model_del = _db.GroupPermissions.FirstOrDefault(t => t.Id == id_delete);
                    var model_per_del = _db.Permissions.Where(t => t.Username == model_del.KeyLink);
                    _db.Permissions.RemoveRange(model_per_del);
                    _db.GroupPermissions.Remove(model_del);
                    _db.SaveChanges();
                    ViewData["Title"] = "Thông tin quyền truy cập";
                    
                    ViewData["MenuLv1"] = "menu_qtnguoidung";
                    ViewData["MenuLv2"] = "menu_dsnhomtaikhoan";
                    return RedirectToAction("Index", "DsNhomTaiKhoan");
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

        public void GroupPermission(VMGroupPermissions requests)
        {
            if (requests.Id == 0)
            {
                var model = new GroupPermissions
                {
                    KeyLink = requests.KeyLink,
                    TenNhomQ = requests.TenNhomQ,
                    ChucNang = requests.ChucNang
                };
                _db.GroupPermissions.Add(model);
                _db.SaveChanges();
            }
            else
            {
                var model = _db.GroupPermissions.FirstOrDefault(t => t.Id == requests.Id);
                model.ChucNang = requests.ChucNang;
                model.TenNhomQ = requests.TenNhomQ;
                _db.GroupPermissions.Update(model);
                _db.SaveChanges();
            }
        }

        public void Permission(string Username)
        {
            var model = _db.Permissions.Where(t => t.Username == Username);
            foreach (var item in model)
            {
                item.Status = "Enable";
            }
            _db.Permissions.UpdateRange(model);
            _db.SaveChanges();
        }
    }
}
