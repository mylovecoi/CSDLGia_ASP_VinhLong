using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.Services;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class TtDnTdXdController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IDsDonviService _dsDonviService;

        public TtDnTdXdController(CSDLGiaDBContext db, IDsDonviService dsDonviService)
        {
            _db = db;
            _dsDonviService = dsDonviService;
        }

        [Route("DoanhNghiep/XetDuyet")]
        [HttpGet]
        public IActionResult Index(string Manghe, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kekhaidangkygia.xetduyetthongtindonvi", "Index"))
                {
                    Madv = string.IsNullOrEmpty(Madv) ? Helpers.GetSsAdmin(HttpContext.Session, "Madv") : Madv;
                    var model_donvi = _dsDonviService.GetListDonvi(Helpers.GetSsAdmin(HttpContext.Session, "Madv"));
                    List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();
                    var model = _db.TtDnTd.Where(t => t.Trangthai == "CD" && list_madv.Contains(t.Macqcq));
                    if (Madv != "all")
                    {
                        model = model.Where(t => t.Madv == Madv);
                    }
                    //var model = (from ttdntd in _db.TtDnTd.Where(t => t.Trangthai == "CD" && t.Macqcq == Madv)
                    //             join ttdntdct in _db.TtDnTdCt on ttdntd.Madv equals ttdntdct.Madv
                    //             select new TtDnTd
                    //             {
                    //                 Id = ttdntd.Id,
                    //                 Madv = ttdntd.Madv,
                    //                 Mahs = ttdntd.Mahs,
                    //                 Macqcq = ttdntd.Macqcq,
                    //                 Madiaban = ttdntd.Madiaban,
                    //                 Tendn = ttdntd.Tendn,
                    //                 Diachi = ttdntd.Diachi,
                    //                 Tel = ttdntd.Tel,
                    //                 Fax = ttdntd.Fax,
                    //                 Email = ttdntd.Email,
                    //                 Website = ttdntd.Website,
                    //                 Diadanh = ttdntd.Diadanh,
                    //                 Chucdanh = ttdntd.Chucdanh,
                    //                 Nguoiky = ttdntd.Nguoiky,
                    //                 Noidknopthue = ttdntd.Noidknopthue,
                    //                 Tailieu = ttdntd.Tailieu,
                    //                 Giayphepkd = ttdntd.Giayphepkd,
                    //                 Ghichu = ttdntd.Ghichu,
                    //                 Trangthai = ttdntd.Trangthai,
                    //                 Level = ttdntd.Level,
                    //                 Lydo = ttdntd.Lydo,
                    //                 Ngaychuyen = ttdntd.Ngaychuyen,
                    //                 Manghe = ttdntdct.Manghe,
                    //             }).ToList();

                    //model = model.Where(t=> list_madv.Contains(t.Macqcq)).ToList();
                    //if (Madv != "all" || !string.IsNullOrEmpty(Madv))
                    //{
                    //    model = model.Where(t => t.Macqcq == Madv).ToList();
                    //}

                    //if (string.IsNullOrEmpty(Manghe))
                    //{
                    //    Manghe = _db.DmNgheKd.FirstOrDefault(t => t.Theodoi == "TD")?.Manghe ?? "";
                    //}

                    //model = model.Where(t => t.Manghe == Manghe).ToList();

                    //var model = _db.TtDnTd.Where(t => t.Trangthai == "CD" && t.Macqcq == Madv);


                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "ADMIN");
                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang == "NHAPLIEU");
                    ViewData["Madv"] = Madv;
                    ViewData["DmNgheKd"] = _db.DmNgheKd.Where(t => t.Theodoi == "TD");
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
                    ViewData["DsDonVi"] = _db.DsDonVi;
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
