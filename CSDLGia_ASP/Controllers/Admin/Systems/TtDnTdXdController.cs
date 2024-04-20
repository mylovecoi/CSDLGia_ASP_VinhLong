using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class TtDnTdXdController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public TtDnTdXdController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DoanhNghiep/XetDuyet")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.bog.xdtttddn", "Approve"))
                {
                    //if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") != null)
                    //{
                    //    var Madv = Helpers.GetSsAdmin(HttpContext.Session, "Madv");
                    //    var GetMadiaban = _db.DsDonVi.FirstOrDefault(t => t.MaDv == Madv);
                    //    Madiaban = GetMadiaban.MaDiaBan;
                    //}
                    //else
                    //{
                    //    if (string.IsNullOrEmpty(Madiaban))
                    //    {
                    //        Madiaban = _db.DsDiaBan.OrderBy(t => t.Id).Where(t => t.Level != "ADMIN").Select(t => t.MaDiaBan).First();
                    //    }
                    //}

                    var model = _db.TtDnTd.Where(t => t.Trangthai == "CD");

                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "ADMIN");
                    ViewData["Title"] = "Thông tin doanh nghiệp xét duyệt";
                    ViewData["MenuLv1"] = "menu_kekhaidangkygia";
                    ViewData["MenuLv2"] = "menu_kekhaidangkygia_xetduyetthongtindonvi";
                    return View("Views/Admin/Systems/TtDnTd/XetDuyet/Index.cshtml", model);
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

        [Route("DoanhNghiep/ChiTiet")]
        [HttpGet]
        public IActionResult Show(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.bog.xdtttddn", "Approve"))
                {
                    var model = _db.Company.FirstOrDefault(t => t.Madv == Madv);

                    var comct_join = (from comct in _db.CompanyLvCc
                                      join nghe in _db.DmNgheKd on comct.Manghe equals nghe.Manghe
                                      select new VMCompanyLvCc
                                      {
                                          Id = comct.Id,
                                          Mahs = comct.Mahs,
                                          Madv = comct.Madv,
                                          Manghe = comct.Manghe,
                                          Tennghe = nghe.Tennghe,
                                          Manganh = comct.Manganh,
                                          Macqcq = comct.Macqcq,
                                          Trangthai = comct.Trangthai,
                                      }).Where(t => t.Madv == Madv && t.Trangthai == "XD").ToList();

                    var model_ttdntd = _db.TtDnTd.FirstOrDefault(t => t.Madv == Madv);

                    var dnct_join = (from dnct in _db.TtDnTdCt
                                     join nghe in _db.DmNgheKd on dnct.Manghe equals nghe.Manghe
                                     select new VMTtDnTdCt
                                     {
                                         Id = dnct.Id,
                                         Mahs = dnct.Mahs,
                                         Madv = dnct.Madv,
                                         Manghe = dnct.Manghe,
                                         Tennghe = nghe.Tennghe,
                                         Manganh = dnct.Manganh,
                                         Macqcq = dnct.Macqcq,
                                         Trangthai = dnct.Trangthai,
                                     }).Where(t => t.Madv == Madv).ToList();

                    ViewData["CompanyLvCc"] = comct_join;
                    ViewData["TtDnTd"] = model_ttdntd;
                    ViewData["TtDnTdCt"] = dnct_join;
                    ViewData["Madv"] = Madv;
                    ViewData["Title"] = "Thông tin doanh nghiệp chi tiết";
                    ViewData["MenuLv1"] = "menu_kekhaidangkygia";
                    ViewData["MenuLv2"] = "menu_kekhaidangkygia_xetduyetthongtindonvi";
                    return View("Views/Admin/Systems/TtDnTd/XetDuyet/Show.cshtml", model);
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

        public IActionResult Approve(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.bog.xdtttddn", "Approve"))
                {
                    var model = _db.TtDnTd.FirstOrDefault(t => t.Madv == Madv);
                    var model_ct = _db.TtDnTdCt.Where(t => t.Madv == model.Madv && t.Mahs == model.Mahs);
                    var companylvcc = _db.CompanyLvCc.Where(t => t.Madv == model.Madv && t.Mahs == model.Mahs);

                    var company_update = _db.Company.FirstOrDefault(t => t.Madv == model.Madv);
                    company_update.Madv = model.Madv;
                    company_update.Madiaban = model.Madiaban;
                    company_update.Tendn = model.Tendn;
                    company_update.Diachi = model.Diachi;
                    company_update.Tel = model.Tel;
                    company_update.Fax = model.Fax;
                    company_update.Email = model.Email;
                    company_update.Diadanh = model.Diadanh;
                    company_update.Chucdanh = model.Chucdanh;
                    company_update.Nguoiky = model.Nguoiky;
                    company_update.Noidknopthue = model.Noidknopthue;
                    company_update.Ghichu = model.Ghichu;
                    company_update.Mahs = model.Mahs;
                    company_update.Updated_at = model.Updated_at;

                    foreach (var item in model_ct)
                    {
                        var model_ct_update = new CompanyLvCc
                        {
                            Mahs = item.Mahs,
                            Madv = item.Madv,
                            Manghe = item.Manghe,
                            Manganh = item.Manganh,
                            Macqcq = item.Macqcq,
                            Trangthai = "XD",
                            Created_at = item.Created_at,
                            Updated_at = DateTime.Now,
                        };
                        _db.CompanyLvCc.UpdateRange(model_ct_update);
                        _db.CompanyLvCc.RemoveRange(companylvcc);
                    }

                    _db.Company.Update(company_update);
                    _db.TtDnTd.Remove(model);
                    _db.TtDnTdCt.RemoveRange(model_ct);
                    _db.SaveChanges();


                    return RedirectToAction("Index", "TtDnTdXd", new { Madv = Madv });
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

        public IActionResult Return(string madv_tralai, string lydo_tralai)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.bog.xdtttddn", "Approve"))
                {
                    var model = _db.TtDnTd.FirstOrDefault(t => t.Madv == madv_tralai);
                    model.Trangthai = "BTL";
                    model.Lydo = lydo_tralai;

                    _db.TtDnTd.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "TtDnTdXd", new { Madv = madv_tralai });
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
