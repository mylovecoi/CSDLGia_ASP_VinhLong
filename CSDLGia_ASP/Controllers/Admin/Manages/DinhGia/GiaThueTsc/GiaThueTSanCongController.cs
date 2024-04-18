using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Services;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaThueTsc
{
    public class GiaThueTSanCongController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IDsDonviService _dsDonviService;
        public GiaThueTSanCongController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment, IDsDonviService dsDonviService)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
            _dsDonviService = dsDonviService;
        }

        [Route("DinhGiaThueTsc")]
        [HttpGet]
        public IActionResult Index(string Madv, int Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetaisancong.thongtin", "Index"))
                {
                    Madv = string.IsNullOrEmpty(Madv) ? Helpers.GetSsAdmin(HttpContext.Session, "Madv") : Madv;
                    var model_donvi = _dsDonviService.GetListDonvi(Helpers.GetSsAdmin(HttpContext.Session, "Madv"));
                    List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();

                    IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaThueTaiSanCong> model = _db.GiaThueTaiSanCong.Where(t => list_madv.Contains(t.Madv));
                    if (Madv != "all")
                    {
                        model = model.Where(t => t.Madv == Madv);
                    }

                    if (Nam != 0)
                    {
                        model = model.Where(t => t.Thoidiem.Year == Nam);
                    }
                    ViewData["Madv"] = Madv;
                    ViewData["Nam"] = Nam;
                    ViewData["DsDonvi"] = model_donvi;
                    ViewData["Title"] = " Thông tin hồ sơ thuê tài sản công";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtsc";
                    ViewData["MenuLv3"] = "menu_dgtsc_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaThueTsc/Index.cshtml", model);
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
        [Route("DinhGiaThueTsc/Create")]
        [HttpGet]
        public IActionResult Create(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetaisancong.thongtin", "Create"))
                {
                    var model_ct_cxd = _db.GiaThueTaiSanCongCt.Where(t => t.Trangthai == "CXD" && t.Madv == Madv);
                    if (model_ct_cxd.Any())
                    {
                        _db.GiaThueTaiSanCongCt.RemoveRange(model_ct_cxd);
                        _db.SaveChanges();
                    }
                    var model_file_cxd = _db.ThongTinGiayTo.Where(t => t.Status == "CXD" && t.Madv == Madv);
                    if (model_file_cxd.Any())
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        foreach (var file in model_file_cxd)
                        {
                            string path_del = Path.Combine(wwwRootPath + "/UpLoad/File/ThongTinGiayTo/", file.FileName);
                            FileInfo fi = new FileInfo(path_del);
                            if (fi != null)
                            {
                                System.IO.File.Delete(path_del);
                                fi.Delete();
                            }
                        }
                        _db.ThongTinGiayTo.RemoveRange(model_file_cxd);
                        _db.SaveChanges();
                    }

                    string Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH");
                    var danhmuc = _db.GiaThueTaiSanCongDm;
                    if (danhmuc.Any())
                    {
                        List<GiaThueTaiSanCongCt> list_add = new List<GiaThueTaiSanCongCt>();
                        foreach (var dm in danhmuc)
                        {
                            list_add.Add(new GiaThueTaiSanCongCt
                            {
                                Mataisan = dm.Mataisan,
                                Tentaisan = dm.Tentaisan,
                                Madv = Madv,
                                Mahs = Mahs
                            });
                        }
                        _db.GiaThueTaiSanCongCt.AddRange(list_add);
                        _db.SaveChanges();
                    }


                    var model = new VMDinhGiaThueTsc
                    {
                        Madv = Madv,
                        Thoidiem = DateTime.Now,
                        Mahs = Mahs,
                        Thoigianpd_create = DateTime.Now,
                        Thoigiandg_create = DateTime.Now,
                        Thuetungay_create = DateTime.Now,
                        Thuedenngay_create = DateTime.Now,
                    };
                    model.GiaThueTaiSanCongCt = _db.GiaThueTaiSanCongCt.Where(t => t.Mahs == Mahs).ToList();

                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDiaBan"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["GiaThueTaiSanCongDm"] = _db.GiaThueTaiSanCongDm.ToList();

                    ViewData["Title"] = " Thông tin hồ sơ thuê tài sản công";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtsc";
                    ViewData["MenuLv3"] = "menu_dgtsc_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaThueTsc/Create.cshtml", model);
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

        [Route("DinhGiaThueTsc/Store")]
        [HttpPost]
        public IActionResult Store(VMDinhGiaThueTsc request)
        {

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetaisancong.thongtin", "Create"))
                {
                    var model = new GiaThueTaiSanCong
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Madiaban = request.Madiaban,
                        Soqd = request.Soqd,
                        Thoidiem = request.Thoidiem,
                        Thongtinhs = request.Thongtinhs,
                        Trangthai = "CC",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaThueTaiSanCong.Add(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaThueTaiSanCongCt.Where(t => t.Mahs == request.Mahs);
                    _db.GiaThueTaiSanCongCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaThueTSanCong", new { Madv = request.Madv });
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


        [Route("DinhGiaThueTsc/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetaisancong.thongtin", "Edit"))
                {
                    var model = _db.GiaThueTaiSanCong.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new VMDinhGiaThueTsc
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        Madiaban = model.Madiaban,
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Thongtinhs = model.Thongtinhs,
                    };

                    var model_ct = _db.GiaThueTaiSanCongCt.Where(t => t.Mahs == model_new.Mahs);

                    model_new.GiaThueTaiSanCongCt = model_ct.ToList();

                    ViewData["Madv"] = model.Madv;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["GiaThueTaiSanCongDm"] = _db.GiaThueTaiSanCongDm.ToList();
                    ViewData["Title"] = " Thông tin hồ sơ thuê tài sản công";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtsc";
                    ViewData["MenuLv3"] = "menu_dgtsc_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaThueTsc/Edit.cshtml", model_new);
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

        [Route("DinhGiaThueTsc/Update")]
        [HttpPost]
        public IActionResult Update(VMDinhGiaThueTsc request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetaisancong.thongtin", "Edit"))
                {
                    var model = _db.GiaThueTaiSanCong.FirstOrDefault(t => t.Mahs == request.Mahs);

                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Thongtinhs = request.Thongtinhs;
                    model.Updated_at = DateTime.Now;
                    _db.GiaThueTaiSanCong.Update(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaThueTaiSanCongCt.Where(t => t.Mahs == request.Mahs);

                    _db.GiaThueTaiSanCongCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaThueTSanCong", new { Madv = request.Madv });
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


        [Route("DinhGiaThueTsc/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetaisancong.thongtin", "Delete"))
                {
                    var model = _db.GiaThueTaiSanCong.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaThueTaiSanCong.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.GiaThueTaiSanCongCt.Where(t => t.Mahs == model.Mahs);
                    _db.GiaThueTaiSanCongCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaThueTSanCong", new { Madv = model.Madv });
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

        [Route("DinhGiaThueTsc/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetaisancong.thongtin", "Index"))
                {
                    var model = _db.GiaThueTaiSanCong.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new VMDinhGiaThueTsc
                    {
                        Madiaban = model.Madiaban,
                        Soqd = model.Soqd,
                        Madv = model.Madv,
                        Macqcq = model.Macqcq,
                        Thoidiem = model.Thoidiem,
                        Thongtin = model.Thongtin,
                    };

                    var model_ct = _db.GiaThueTaiSanCongCt.Where(t => t.Mahs == Mahs);

                    model_new.GiaThueTaiSanCongCt = model_ct.ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["GiaThueTaiSanCongDm"] = _db.GiaThueTaiSanCongDm.ToList();
                    ViewData["Title"] = " Thông tin hồ sơ thuê tài sản công";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtsc";
                    ViewData["MenuLv3"] = "menu_dgtsc_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaThueTsc/Show.cshtml", model_new);
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
        public IActionResult HoanThanh(string mahs_complete, string trangthai_complete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetaisancong.thongtin", "Approve"))
                {
                    var model = _db.GiaThueTaiSanCong.FirstOrDefault(p => p.Mahs == mahs_complete);
                    model.Trangthai = trangthai_complete;
                    model.Updated_at = DateTime.Now;
                  
                    _db.GiaThueTaiSanCong.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "GiaThueTSanCong", new { Madv = model.Madv, Nam = model.Thoidiem.Year });
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


        [Route("DinhGiaThueTsc/Search")]
        [HttpGet]
        public IActionResult Search(string Madv, DateTime? NgayTu, DateTime? NgayDen, string Mahs, double DonGiaTu, double DonGiaDen, string Tentaisan)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetaisancong.timkiem", "Index"))
                {
                    DateTime nowDate = DateTime.Now;
                    DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
                    DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);

                    Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;
                    NgayTu = NgayTu.HasValue ? NgayTu : firstDayCurrentYear;
                    NgayDen = NgayDen.HasValue ? NgayDen : lastDayCurrentYear;
                    Mahs = string.IsNullOrEmpty(Mahs) ? "all" : Mahs;
                    DonGiaTu = DonGiaTu == 0 ? 0 : DonGiaTu;
                    DonGiaDen = DonGiaDen == 0 ? 0 : DonGiaDen;
                    Tentaisan = string.IsNullOrEmpty(Tentaisan) ? "" : Tentaisan;



                    var model = (from hosoct in _db.GiaThueTaiSanCongCt
                                 join hoso in _db.GiaThueTaiSanCong on hosoct.Mahs equals hoso.Mahs
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueTaiSanCongCt
                                 {
                                     Madv = hoso.Madv,
                                     Tendv = donvi.TenDv,
                                     SoQD = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                     Mataisan = hosoct.Mataisan,
                                     Tentaisan = hosoct.Tentaisan,
                                     Dvthue = hosoct.Dvthue,
                                     Hdthue = hosoct.Hdthue,
                                     Ththue = hosoct.Ththue,
                                     Thuetungay = hosoct.Thuetungay,
                                     Dvt = hosoct.Dvt,
                                     Dongiathue = hosoct.Dongiathue,
                                     Sotienthuenam = hosoct.Sotienthuenam,
                                     Trangthai = hoso.Trangthai,
                                     Mahs = hoso.Mahs
                                 });
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model = model.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen && list_trangthai.Contains(t.Trangthai));
                    if (Madv != "all") { model = model.Where(t => t.Madv == Madv); }
                    if (DonGiaTu > 0) { model = model.Where(t => t.Dongiathue >= DonGiaTu || t.Sotienthuenam >= DonGiaTu); }
                    if (DonGiaDen > 0) { model = model.Where(t => t.Dongiathue <= DonGiaDen || t.Sotienthuenam <= DonGiaDen); }
                    if (Mahs != "all") { model = model.Where(t => t.Mahs == Mahs); }
                    if (!string.IsNullOrEmpty(Tentaisan))
                    {
                        model = model.Where(t => t.Tentaisan.ToLower().Contains(Tentaisan.ToLower()));
                    }

                    ViewData["Madv"] = Madv;
                    ViewData["NgayTu"] = NgayTu;
                    ViewData["NgayDen"] = NgayDen;
                    ViewData["Mahs"] = Mahs;
                    ViewData["DonGiaTu"] = Helpers.ConvertDbToStr(DonGiaTu);
                    ViewData["DonGiaDen"] = Helpers.ConvertDbToStr(DonGiaDen);
                    ViewData["Tentaisan"] = Tentaisan;
                    ViewData["DanhSachHoSo"] = _db.GiaThueTaiSanCong.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen && list_trangthai.Contains(t.Trangthai));

                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Title"] = " Thông tin hồ sơ thuê tài sản công";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtsc";
                    ViewData["MenuLv3"] = "menu_dgtsc_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaThueTsc/TimKiem/Search.cshtml", model);
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

        [Route("DinhGiaThueTsc/PrintSearch")]
        [HttpPost]
        public IActionResult Print(string Madv_Search, DateTime? NgayTu_Search, DateTime? NgayDen_Search, string Mahs_Search,
                                    double DonGiaTu_Search, double DonGiaDen_Search, string Tentaisan_Search)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuetaisancong.timkiem", "Edit"))
                {

                    var model = (from hosoct in _db.GiaThueTaiSanCongCt
                                 join hoso in _db.GiaThueTaiSanCong on hosoct.Mahs equals hoso.Mahs
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueTaiSanCongCt
                                 {
                                     Madv = hoso.Madv,
                                     Tendv = donvi.TenDv,
                                     SoQD = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                     Mataisan = hosoct.Mataisan,
                                     Tentaisan = hosoct.Tentaisan,
                                     Dvthue = hosoct.Dvthue,
                                     Hdthue = hosoct.Hdthue,
                                     Ththue = hosoct.Ththue,
                                     Thuetungay = hosoct.Thuetungay,
                                     Dvt = hosoct.Dvt,
                                     Dongiathue = hosoct.Dongiathue,
                                     Sotienthuenam = hosoct.Sotienthuenam,

                                     Trangthai = hoso.Trangthai,
                                     Mahs = hoso.Mahs
                                 });
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model = model.Where(t => t.Thoidiem >= NgayTu_Search && t.Thoidiem <= NgayDen_Search && list_trangthai.Contains(t.Trangthai));
                    if (Madv_Search != "all") { model = model.Where(t => t.Madv == Madv_Search); }
                    if (DonGiaTu_Search > 0) { model = model.Where(t => t.Dongiathue >= DonGiaTu_Search); }
                    if (DonGiaDen_Search > 0) { model = model.Where(t => t.Dongiathue <= DonGiaDen_Search); }
                    if (Mahs_Search != "all") { model = model.Where(t => t.Mahs == Mahs_Search); }
                    if (!string.IsNullOrEmpty(Tentaisan_Search))
                    {
                        model = model.Where(t => t.Tentaisan.ToLower().Contains(Tentaisan_Search.ToLower()));
                    }



                    ViewData["Title"] = "Kết quả tìm kiếm thông tin giá thuê tài sản công";

                    return View("Views/Admin/Manages/DinhGia/GiaThueTsc/TimKiem/Result.cshtml", model);
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

        [HttpPost("DinhGiaThueTsc/GetListHoSo")]
        public JsonResult GetListHoSo(DateTime ngaytu, DateTime ngayden)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                var model = _db.GiaThueTaiSanCong.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && list_trangthai.Contains(t.Trangthai));
                string result = "<select class='form-control' id='Mahs_Search' name='Mahs_Search'>";
                result += "<option value='all'>--Tất cả---</option>";

                if (model.Any())
                {
                    foreach (var item in model)
                    {
                        result += "<option value='" + @item.Mahs + "'>Số QĐ: " + @item.Soqd + " - Thời điểm: " + @Helpers.ConvertDateToStr(item.Thoidiem) + "</option>";
                    }
                }
                result += "</select>";
                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Phiên đăng nhập kết thúc, Bạn cần đăng nhập lại!!!" };
                return Json(data);
            }
        }
    }
}
