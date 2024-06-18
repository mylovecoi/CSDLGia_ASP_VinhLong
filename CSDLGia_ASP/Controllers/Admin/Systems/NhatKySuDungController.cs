using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.ViewModels.Manages.KeKhaiGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class NhatKySuDungController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public NhatKySuDungController(CSDLGiaDBContext db)
        {
            _db = db;
        }



        [Route("NhatKySuDung")]
        [HttpGet]
        public IActionResult Index(string Nam, string Madv, string Noidung)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.nhatky.nguoidung", "Index"))
                {
                    var model = _db.NhatKySuDung.ToList();

                    if (string.IsNullOrEmpty(Madv) || Madv == "All")
                    {
                        model = model.ToList();
                    }
                    else
                    {
                        model = model.Where(t => t.Madv == Madv).ToList();
                    }

                    if (string.IsNullOrEmpty(Nam))
                    {
                        Nam = Helpers.ConvertYearToStr(DateTime.Now.Year);
                    }

                    if (!string.IsNullOrEmpty(Noidung))
                    {
                        model = model.Where(t =>  t.Thoigian.Year == int.Parse(Nam) && t.Noidung.Contains(Noidung)).ToList();
                    }
                    else
                    {
                        model = model.Where(t => t.Thoigian.Year == int.Parse(Nam)).ToList();
                    }

                    ViewData["Dsdonvi"] = _db.DsDonVi.ToList();
                    ViewData["Nam"] = Nam;
                    ViewData["Madv"] = Madv;
                    ViewData["Noidung"] = Noidung;
                    ViewData["Title"] = "Tìm kiếm nhật ký sử dụng";
                    ViewData["MenuLv1"] = "menu_hethong";
                    ViewData["MenuLv2"] = "menu_qthethong";
                    ViewData["MenuLv3"] = "menu_nhatky";
                    return View("Views/Admin/Systems/Nhatky/Search.cshtml", model);

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

        [Route("NhatKySuDung/Printf")]
        [HttpGet]
        public IActionResult Printf(string Nam, string Madv, string Noidung)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.nhatky.nguoidung", "Index"))
                {

                    var model = _db.NhatKySuDung.ToList();

                    if (string.IsNullOrEmpty(Madv) || Madv == "All")
                    {
                        model = model.ToList();
                    }
                    else
                    {
                        model = model.Where(t => t.Madv == Madv).ToList();
                    }

                    if (string.IsNullOrEmpty(Nam))
                    {
                        Nam = Helpers.ConvertYearToStr(DateTime.Now.Year);
                    }

                    if (!string.IsNullOrEmpty(Noidung))
                    {
                        model = model.Where(t => t.Thoigian.Year == int.Parse(Nam) && t.Noidung.Contains(Noidung)).ToList();
                    }
                    else
                    {
                        model = model.Where(t => t.Thoigian.Year == int.Parse(Nam)).ToList();
                    }

                    ViewData["Dsdonvi"] = _db.DsDonVi.ToList();
                    ViewData["Nam"] = Nam;
                    ViewData["Madv"] = Madv;
                    ViewData["Noidung"] = Noidung;
                    ViewData["Title"] = "Tìm kiếm nhật ký sử dụng";
                    ViewData["MenuLv1"] = "menu_hethong";
                    ViewData["MenuLv2"] = "menu_qthethong";
                    ViewData["MenuLv3"] = "menu_nhatky";
                    return View("Views/Admin/Systems/Nhatky/Printf.cshtml", model);
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

        [Route("NhatKySuDung/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.nhatky.nguoidung", "Delete"))
                {
                    var model = _db.NhatKySuDung.FirstOrDefault(p => p.Id == id_delete);
                    _db.NhatKySuDung.Remove(model);
                    _db.SaveChanges();

                    ViewData["Nam"] = model.Thoigian.Year;
                    return RedirectToAction("Index", "NhatKySuDung", new { model.Thoigian.Year });
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
