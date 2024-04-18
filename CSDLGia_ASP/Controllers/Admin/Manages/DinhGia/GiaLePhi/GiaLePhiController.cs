using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.Services;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaLePhi
{
    public class GiaLePhiController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IDsDonviService _dsDonviService;
        private readonly ITrangThaiHoSoService _trangThaiHoSoService;

        public GiaLePhiController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment, IDsDonviService dsDonviService, ITrangThaiHoSoService trangThaiHoSoService)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
            _dsDonviService = dsDonviService;
            _trangThaiHoSoService = trangThaiHoSoService;
        }

        [Route("DinhGiaLePhi")]
        [HttpGet]
        public IActionResult Index(string Madv, int Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.lephi.thongtin", "Index"))
                {
                    Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;
                    var model_donvi = _dsDonviService.GetListDonvi(Helpers.GetSsAdmin(HttpContext.Session, "Madv"));
                    List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();

                    IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaPhiLePhi> model = _db.GiaPhiLePhi.Where(t => list_madv.Contains(t.Madv));

                    if (Madv != "all")
                    {
                        model = model.Where(t => t.Madv == Madv);
                    }

                    if (Nam != 0)
                    {
                        model = model.Where(t => t.Thoidiem.Year == Nam).ToList();
                    }

                    ViewData["DsDonVi"] = model_donvi;
                    ViewData["NhomDm"] = _db.GiaPhiLePhiNhom;
                    ViewData["Madv"] = Madv;
                    ViewData["Nam"] = Nam;
                    ViewData["Title"] = " Thông tin hồ sơ giá lệ phí trước bạ";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_dglp";
                    ViewData["MenuLv3"] = "menu_dglp_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaLePhi/DanhSach/Index.cshtml", model);


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

        [HttpPost("DinhGiaLePhi/Create")]
        public IActionResult Create(string Madv_create, string Manhom_create)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.lephi.thongtin", "Create"))
                {
                    var modelcxd = _db.GiaPhiLePhiCt.Where(t => t.Trangthai == "CXD" && t.Madv == Madv_create);
                    if (modelcxd.Any())
                    {
                        _db.GiaPhiLePhiCt.RemoveRange(modelcxd);
                        _db.SaveChanges();
                    }
                    var model_file_cxd = _db.ThongTinGiayTo.Where(t => t.Status == "CXD" && t.Madv == Madv_create);
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

                    var model = new GiaPhiLePhi
                    {
                        Madv = Madv_create,
                        Thoidiem = DateTime.Now,
                        Mahs = Madv_create + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Manhom = Manhom_create
                    };
                    IQueryable<GiaPhiLePhiDm> danhmuc = _db.GiaPhiLePhiDm;
                    if (Manhom_create != "all")
                    {
                        danhmuc = danhmuc.Where(t => t.Manhom == Manhom_create);
                    }
                    var chitiet = new List<GiaPhiLePhiCt>();
                    foreach (var item in danhmuc)
                    {
                        chitiet.Add(new GiaPhiLePhiCt()
                        {
                            Madv = model.Madv,
                            Mahs = model.Mahs,
                            STTSapxep = item.Stt,
                            STTHienthi = item.SttHienthi,
                            Style = item.Style,
                            Phanloai = item.Manhom,
                            Ptcp = item.HienThi,
                            NhanHieu = item.NhanHieu,
                            NuocSxLr = item.NuocSxLr,
                            TheTich = item.TheTich,
                            SoNguoiTaiTrong = item.SoNguoiTaiTrong,
                            Trangthai = "CXD",
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        });
                    }
                    _db.GiaPhiLePhiCt.AddRange(chitiet);
                    _db.SaveChanges();
                    model.GiaPhiLePhiCt = _db.GiaPhiLePhiCt.Where(t => t.Mahs == model.Mahs).ToList();

                    ViewData["NhomDm"] = _db.GiaPhiLePhiNhom;
                    ViewData["Title"] = " Thông tin hồ sơ giá lệ phí trước bạ";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_dglp";
                    ViewData["MenuLv3"] = "menu_dglp_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaLePhi/DanhSach/Create.cshtml", model);
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

        [Route("DinhGiaLePhi/Store")]
        [HttpPost]
        public IActionResult Store(GiaPhiLePhi request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.lephi.thongtin", "Create"))
                {
                    var model = new GiaPhiLePhi
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Madiaban = request.Madiaban,
                        Manhom = request.Manhom,
                        Soqd = request.Soqd,
                        Thoidiem = request.Thoidiem,
                        Thongtin = request.Thongtin,
                        Trangthai = "CC",
                        Congbo = "CHUACONGBO",
                        Mota = request.Mota,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };

                    var modelct = _db.GiaPhiLePhiCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Trangthai = "XD";
                        }
                    }
                    var modelFile = _db.ThongTinGiayTo.Where(t => t.Mahs == request.Mahs);
                    if (modelFile.Any())
                    {
                        foreach (var file in modelFile) { file.Status = "XD"; }
                    }

                    _db.GiaPhiLePhi.Add(model);
                    _db.ThongTinGiayTo.UpdateRange(modelFile);
                    _db.GiaPhiLePhiCt.UpdateRange(modelct);
                    _db.SaveChanges();
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Thêm mới");

                    return RedirectToAction("Index", "GiaLePhi", new { Madv = request.Madv });
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

        [Route("DinhGiaLePhi/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.lephi.thongtin", "Edit"))
                {
                    var model = _db.GiaPhiLePhi.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new GiaPhiLePhi
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        Mota = model.Mota,
                        Ghichu = model.Ghichu,
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Thongtin = model.Thongtin,
                    };

                    var model_ct = _db.GiaPhiLePhiCt.Where(t => t.Mahs == model_new.Mahs);
                    model_new.GiaPhiLePhiCt = model_ct.ToList();
                    var model_file = _db.ThongTinGiayTo.Where(t => t.Mahs == model_new.Mahs);
                    model_new.ThongTinGiayTo = model_file.ToList();
       
                    ViewData["NhomDm"] = _db.GiaPhiLePhiNhom;
                    ViewData["Title"] = " Thông tin hồ sơ giá lệ phí trước bạ";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_dglp";
                    ViewData["MenuLv3"] = "menu_dglp_tt";

                    return View("Views/Admin/Manages/DinhGia/GiaLePhi/DanhSach/Edit.cshtml", model_new);
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

        [Route("DinhGiaLePhi/Update")]
        [HttpPost]
        public IActionResult Update(GiaPhiLePhi request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.lephi.thongtin", "Edit"))
                {
                    var model = _db.GiaPhiLePhi.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Soqd = request.Soqd;
                    model.Mota = request.Mota;
                    model.Ghichu = request.Ghichu;
                    model.Thoidiem = request.Thoidiem;
                    model.Thongtin = request.Thongtin;
                    model.Updated_at = DateTime.Now;

                    var modelct = _db.GiaPhiLePhiCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct.Any()) { foreach (var ct in modelct) { ct.Trangthai = "XD"; } }
                    var modelfile = _db.ThongTinGiayTo.Where(t => t.Mahs == request.Mahs);
                    if (modelfile.Any()) { foreach (var file in modelfile) { file.Status = "Enable"; } }

                    _db.GiaPhiLePhi.Update(model);
                    _db.ThongTinGiayTo.UpdateRange(modelfile);
                    _db.GiaPhiLePhiCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Cập nhật");

                    return RedirectToAction("Index", "GiaLePhi", new { Madv = request.Madv });
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

        [Route("DinhGiaLePhi/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.lephi.thongtin", "Delete"))
                {
                    var model = _db.GiaPhiLePhi.FirstOrDefault(t => t.Id == id_delete);

                    var model_ct = _db.GiaPhiLePhiCt.Where(t => t.Mahs == model.Mahs);
                    var modelfile = _db.ThongTinGiayTo.Where(t => t.Mahs == model.Mahs);

                    if (modelfile.Any())
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        foreach (var file in modelfile)
                        {
                            string path_del = Path.Combine(wwwRootPath + "/UpLoad/File/ThongTinGiayTo/", file.FileName);
                            FileInfo fi = new FileInfo(path_del);
                            if (fi != null)
                            {
                                System.IO.File.Delete(path_del);
                                fi.Delete();
                            }
                        }
                    }

                    _db.GiaPhiLePhiCt.RemoveRange(model_ct);
                    _db.ThongTinGiayTo.RemoveRange(modelfile);
                    _db.GiaPhiLePhi.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaLePhi", new { Madv = model.Madv });
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

        [Route("DinhGiaLePhi/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.lephi.thongtin", "Show"))
                {
                    var model = _db.GiaPhiLePhi.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new GiaPhiLePhi
                    {
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Thongtin = model.Thongtin,
                        Mota = model.Mota,
                        Ghichu = model.Ghichu,
                    };
                    var model_ct = _db.GiaPhiLePhiCt.Where(t => t.Mahs == Mahs);

                    model_new.GiaPhiLePhiCt = model_ct.ToList();

                    ViewData["DsNhom"] = _db.GiaPhiLePhiNhom;
                    ViewData["Title"] = " Thông tin hồ sơ giá lệ phí trước bạ";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_dglp";
                    ViewData["MenuLv3"] = "menu_dglp_tt";
        

                    return View("Views/Admin/Manages/DinhGia/GiaLePhi/DanhSach/Show.cshtml", model_new);
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
        public IActionResult Chuyen(string mahs_complete, string trangthai_complete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.lephi.thongtin", "Approve"))
                {
                    var model = _db.GiaPhiLePhi.FirstOrDefault(p => p.Mahs == mahs_complete);

                    model.Trangthai = trangthai_complete;
                    model.Updated_at = DateTime.Now;

                    _db.GiaPhiLePhi.Update(model);
                    _db.SaveChanges();

                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), trangthai_complete);
                    return RedirectToAction("Index", "GiaLePhi", new { Madv = model.Madv, Nam = model.Thoidiem.Year });
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

        [Route("DinhGiaLePhi/Search")]
        [HttpGet]
        public IActionResult Search(string Madv, string tsp, DateTime? beginTime, DateTime? endTime, string mahs,
                                    double beginPrice, double endPrice, string Ptcp)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.lephi.timkiem", "Index"))
                {
                    DateTime nowDate = DateTime.Now;
                    DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
                    DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);


                    Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;
                    tsp = string.IsNullOrEmpty(tsp) ? "all" : tsp;
                    beginTime = beginTime.HasValue ? beginTime : firstDayCurrentYear;
                    endTime = endTime.HasValue ? endTime : lastDayCurrentYear;
                    mahs = string.IsNullOrEmpty(mahs) ? "all" : mahs;
                    beginPrice = beginPrice == 0 ? 0 : beginPrice;
                    endPrice = endPrice == 0 ? 0 : endPrice;
                   
                    Ptcp = string.IsNullOrEmpty(Ptcp) ? "" : Ptcp;


                    var model = from dgct in _db.GiaPhiLePhiCt
                                join dg in _db.GiaPhiLePhi on dgct.Mahs equals dg.Mahs
                                join donvi in _db.DsDonVi on dg.Madv equals donvi.MaDv
                                select new CSDLGia_ASP.Models.Manages.DinhGia.GiaPhiLePhiCt
                                {
                                    TenDv = donvi.TenDv,
                                    Mahs = dgct.Mahs,
                                    Madv = dg.Madv,
                                    ThoiDiem = dg.Thoidiem,
                                    MoTa = dg.Mota,
                                    SoQD = dg.Soqd,
                                    Ptcp = dgct.Ptcp,
                                    Dvt = dgct.Dvt,
                                    Phantram = dgct.Phantram,
                                    Giatu = dgct.Giatu,
                                    Phanloai = dgct.Phanloai,
                                    Mucthutu = dgct.Mucthutu,
                                    Trangthai = dg.Trangthai,
                                    NuocSxLr = dgct.NuocSxLr,
                                    NhanHieu = dgct.NhanHieu,
                                    TheTich = dgct.TheTich,
                                    SoNguoiTaiTrong = dgct.SoNguoiTaiTrong,
                                };
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };

                    model = model.Where(t => t.ThoiDiem >= beginTime && t.ThoiDiem <= endTime && t.Mucthutu >= beginPrice && list_trangthai.Contains(t.Trangthai));
                    if (Madv != "all")
                    {
                        model = model.Where(t => t.Madv == Madv);
                    }
                    if (tsp != "all")
                    {
                        model = model.Where(t => t.Phanloai == tsp);
                    }
                    if (mahs != "all")
                    {
                        model = model.Where(t => t.Mahs == mahs);
                    }
                    if (beginPrice > 0)
                    {
                        model = model.Where(t => t.Mucthutu >= beginPrice);
                    }
                    if (endPrice > 0)
                    {
                        model = model.Where(t => t.Mucthutu <= endPrice);
                    }
                    if (!string.IsNullOrEmpty(Ptcp))
                    {
                        model = model.Where(t => t.Ptcp.ToLower().Contains(Ptcp.ToLower()));
                    }
                    ViewData["GiaPhiLePhiNhom"] = _db.GiaPhiLePhiNhom;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["DanhSachHoSo"] = _db.GiaPhiLePhi.Where(t => t.Thoidiem >= beginTime && t.Thoidiem <= endTime && list_trangthai.Contains(t.Trangthai));
                    ViewData["DanhMucNhom"] = _db.GiaPhiLePhiNhom;
                    ViewData["Madv"] = Madv;
                    ViewData["tsp"] = tsp;
                    ViewData["beginTime"] = beginTime;
                    ViewData["endTime"] = endTime;
                    ViewData["mahs"] = mahs;
                    ViewData["beginPrice"] = Helpers.ConvertDbToStr(beginPrice);
                    ViewData["endPrice"] = Helpers.ConvertDbToStr(endPrice);
                    ViewData["Ptcp"] = Ptcp;                 
                    ViewData["Title"] = " Thông tin hồ sơ lệ phí trước bạ";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_dglp";
                    ViewData["MenuLv3"] = "menu_dglp_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaLePhi/TimKiem/Search.cshtml", model);
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

        [HttpPost("DinhGiaLePhi/PrintSearch")]
        public IActionResult Print(string Madv_Search, string tsp_Search, DateTime beginTime_Search, DateTime endTime_Search,
                                    double beginPrice_Search, double endPrice_Search, string mahs_Search, string Ptcp_Search)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.cacloaigiakhac.lephi.timkiem", "Edit"))
                {

                    var model_join = from dgct in _db.GiaPhiLePhiCt
                                     join dg in _db.GiaPhiLePhi on dgct.Mahs equals dg.Mahs
                                     join donvi in _db.DsDonVi on dg.Madv equals donvi.MaDv
                                     select new CSDLGia_ASP.Models.Manages.DinhGia.GiaPhiLePhiCt
                                     {
                                         TenDv = donvi.TenDv,
                                         Mahs = dgct.Mahs,
                                         Madv = dg.Madv,
                                         ThoiDiem = dg.Thoidiem,
                                         MoTa = dg.Mota,
                                         SoQD = dg.Soqd,
                                         Ptcp = dgct.Ptcp,
                                         Dvt = dgct.Dvt,
                                         Phantram = dgct.Phantram,
                                         Giatu = dgct.Giatu,
                                         Phanloai = dgct.Phanloai,
                                         Mucthutu = dgct.Mucthutu,
                                         Trangthai = dg.Trangthai,
                                         NuocSxLr = dgct.NuocSxLr,
                                         NhanHieu = dgct.NhanHieu,
                                         TheTich = dgct.TheTich,
                                         SoNguoiTaiTrong = dgct.SoNguoiTaiTrong,
                                     };
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model_join = model_join.Where(t => t.ThoiDiem >= beginTime_Search && t.ThoiDiem <= endTime_Search && list_trangthai.Contains(t.Trangthai));
                    if (tsp_Search != "all")
                    {
                        model_join = model_join.Where(t => t.Phanloai == tsp_Search);
                    }
                    if (Madv_Search != "all")
                    {
                        model_join = model_join.Where(t => t.Madv == Madv_Search);
                    }
                    if (beginPrice_Search > 0)
                    {
                        model_join = model_join.Where(t => t.Mucthutu >= beginPrice_Search);
                    }
                    if (endPrice_Search > 0)
                    {
                        model_join = model_join.Where(t => t.Mucthuden <= endPrice_Search);
                    }                   
                    if (mahs_Search != "all")
                    {
                        model_join = model_join.Where(t => t.Mahs == mahs_Search);
                    }
                    if (!string.IsNullOrEmpty(Ptcp_Search))
                    {
                        model_join = model_join.Where(t => t.Ptcp.ToLower().Contains(Ptcp_Search.ToLower()));
                    }


                    ViewData["Title"] = " Thông tin hồ sơ lệ phí trước bạ";
                    return View("Views/Admin/Manages/DinhGia/GiaLePhi/TimKiem/Result.cshtml", model_join);
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

        [HttpPost("GiaLePhi/GetListHoSo")]
        public JsonResult GetListHoSo(DateTime ngaytu, DateTime ngayden)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                var model = _db.GiaPhiLePhi.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && list_trangthai.Contains(t.Trangthai));
                string result = "<select class='form-control' id='mahs_Search' name='mahs_Search'>";
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
