using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaGiaoDichDat
{
    public class GiaGiaoDichDatController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IDsDonviService _dsDonviService;
        private readonly ITrangThaiHoSoService _trangThaiHoSoService;

        public GiaGiaoDichDatController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment, IDsDonviService dsDonviService,ITrangThaiHoSoService trangThaiHoSoService)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
            _dsDonviService = dsDonviService;
            _trangThaiHoSoService = trangThaiHoSoService;
        }

        [Route("GiaGiaoDichDat/DanhSach")]
        [HttpGet]
        public IActionResult Index(string Madv, int Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.thongtin", "Index"))
                {
                    Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

                    var model_donvi = _dsDonviService.GetListDonvi(Helpers.GetSsAdmin(HttpContext.Session, "Madv"));
                    List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();

                    var model = _db.GiaGiaoDichDat.Where(t => list_madv.Contains(t.Madv));

                    if (Madv != "all")
                    {
                        model = model.Where(t => t.Madv == Madv);
                    }

                    if (Nam != 0)
                    {
                        model = model.Where(t => t.Thoidiem.Year == Nam);
                    }
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["NhomTn"] = _db.GiaGiaoDichDatNhom.ToList();
                    ViewData["DsDonvi"] = model_donvi;
                    ViewData["Nam"] = Nam;
                    ViewData["Madv"] = Madv;
                    ViewData["Title"] = "Thông tin giá giao dịch đất thực tế trên thị trường";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dg_giaodichdattrenthitruong";
                    ViewData["MenuLv3"] = "menu_dg_giaodichdattrenthitruong_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichDat/DanhSach/Index.cshtml", model);
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

        [Route("GiaGiaoDichDat/Create")]
        [HttpGet]
        public IActionResult Create(string Manhom, string MadvBc)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.thongtin", "Create"))
                {
                    this.RemoveData_Ct_CXD(MadvBc);
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaGiaoDichDat
                    {
                        Mahs = MadvBc + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Madv = MadvBc,
                        Manhom = Manhom,
                        Thoidiem=DateTime.Now,
                        Thoidiemlk = DateTime.Now,
                    };

                    var danhmuc = _db.GiaGiaoDichDatDm.ToList();
                    if (Manhom != "all")
                    {
                        danhmuc = danhmuc.Where(t => t.Manhom == Manhom).ToList();
                    }

                    var chitiet = new List<GiaGiaoDichDatCt>();

                    foreach (var item in danhmuc)
                    {
                        chitiet.Add(new GiaGiaoDichDatCt()
                        {
                            Mahs = model.Mahs,
                            Ten = item.Ten,
                            Trangthai = "CXD",
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                            Madv = model.Madv,
                            Manhom = item.Manhom,
                            Dvt = item.Dvt,
                        });
                    }
                    _db.GiaGiaoDichDatCt.AddRange(chitiet);
                    _db.SaveChanges();

                    model.GiaGiaoDichDatCt = _db.GiaGiaoDichDatCt.Where(t => t.Mahs == model.Mahs).ToList();
                    ViewData["DanhMucNhom"] = _db.GiaGiaoDichDatNhom;
                    ViewData["Manhom"] = Manhom;
                    ViewData["Madv"] = MadvBc;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["Title"] = "Bảng giá giao dịch đất thực tế trên thị trường";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dg_giaodichdattrenthitruong";
                    ViewData["MenuLv3"] = "menu_dg_giaodichdattrenthitruong_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichDat/DanhSach/Create.cshtml", model);

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

        [Route("GiaGiaoDichDat/Store")]
        [HttpPost]
        public IActionResult Store(CSDLGia_ASP.Models.Manages.DinhGia.GiaGiaoDichDat request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.thongtin", "Create"))
                {
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaGiaoDichDat
                    {
                        Mahs = request.Mahs,
                        Manhom = request.Manhom,
                        Madv = request.Madv,
                        Thoidiem = request.Thoidiem,
                        Soqd = request.Soqd,
                        Soqdlk = request.Soqdlk,
                        Thoidiemlk = request.Thoidiemlk,
                        Cqbh = request.Cqbh,
                        Ghichu = request.Ghichu,
                        Trangthai = "CC",
                        Congbo = "CHUACONGBO",
                        Ipf1 = request.Ipf1,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaGiaoDichDat.Add(model);
                    this.SaveData_Ct_CXD(model.Mahs);
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Thêm mới");
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaGiaoDichDat", new { request.Madv });
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

        [Route("GiaGiaoDichDat/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.thongtin", "Edit"))
                {

                    var model = _db.GiaGiaoDichDat.FirstOrDefault(t => t.Mahs == Mahs);

                    var model_ct = _db.GiaGiaoDichDatCt.Where(t => t.Mahs == model.Mahs);
                    model.GiaGiaoDichDatCt = model_ct.ToList();
                    model.ThongTinGiayTo = _db.ThongTinGiayTo.Where(x => x.Mahs == model.Mahs).ToList();
                    ViewData["DanhMucNhom"] = _db.GiaGiaoDichDatNhom;
                    ViewData["Title"] = "Bảng giá giao dịch đất thực tế trên thị trường";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dg_giaodichdattrenthitruong";
                    ViewData["MenuLv3"] = "menu_dg_giaodichdattrenthitruong_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichDat/DanhSach/Edit.cshtml", model);

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

        [Route("GiaGiaoDichDat/Update")]
        [HttpPost]
        public IActionResult Update(CSDLGia_ASP.Models.Manages.DinhGia.GiaGiaoDichDat request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.thongtin", "Edit"))
                {
                    var model = _db.GiaGiaoDichDat.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Soqdlk = request.Soqdlk;
                    model.Thoidiemlk = request.Thoidiemlk;
                    model.Cqbh = request.Cqbh;
                    model.Ghichu = request.Ghichu;
                    model.Ipf1 = request.Ipf1;
                    model.Updated_at = DateTime.Now;
                    this.SaveData_Ct_CXD(model.Mahs);
                    _db.SaveChanges();
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Cập nhật");
                    return RedirectToAction("Index", "GiaGiaoDichDat", new { request.Madv });
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

        [Route("GiaGiaoDichDat/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.thongtin", "Delete"))
                {
                    var model = _db.GiaGiaoDichDat.FirstOrDefault(t => t.Id == id_delete);

                    var model_ct = _db.GiaGiaoDichDatCt.Where(t => t.Mahs == model.Mahs);
                    if (model_ct.Any())
                    {
                        _db.GiaGiaoDichDatCt.RemoveRange(model_ct);
                    }
                    // xóa thông tin giấy tờ chưa lưu lại
                    var model_file_cxd = _db.ThongTinGiayTo.Where(t => t.Mahs == model.Mahs);
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
                    }
                    _db.GiaGiaoDichDat.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaGiaoDichDat", new { model.Madv });
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

        [Route("GiaGiaoDichDat/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.thongtin", "Index"))
                {
                    var model = _db.GiaGiaoDichDat.FirstOrDefault(t => t.Mahs == Mahs);
                    model.GiaGiaoDichDatCt = _db.GiaGiaoDichDatCt.Where(t => t.Mahs == model.Mahs).ToList();
                    var donvi = _db.DsDonVi.First(x => x.MaDv == model.Madv);
                    ViewData["Title"] = "Bảng giá giao dịch đất thực tế trên thị trường";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dg_giaodichdattrenthitruong";
                    ViewData["MenuLv3"] = "menu_dg_giaodichdattrenthitruong_tt";
                    ViewData["TenDiaBan"] = _db.DsDiaBan.First(x => x.MaDiaBan == donvi.MaDiaBan).TenDiaBan;
                    ViewData["TenDonVi"] = donvi.TenDv;
                    ViewData["DanhMucNhom"] = _db.GiaGiaoDichDatNhom;
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichDat/DanhSach/Show.cshtml", model);

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

        [Route("GiaGiaoDichDat/Chuyen")]
        [HttpPost]
        public IActionResult Chuyen(string mahs_complete, string trangthai_complete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.thongtin", "Index"))
                {
                    var model = _db.GiaGiaoDichDat.FirstOrDefault(p => p.Mahs == mahs_complete);
                    model.Trangthai = trangthai_complete;
                    model.Updated_at = DateTime.Now;

                    _db.GiaGiaoDichDat.Update(model);
                    _db.SaveChanges();
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), trangthai_complete);
                    return RedirectToAction("Index", "GiaGiaoDichDat", new { Madv = model.Madv, Nam = model.Thoidiem.Year });    
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

        [Route("GiaGiaoDichDat/Search")]
        [HttpGet]
        public IActionResult Search(DateTime ngaynhap_tu, DateTime ngaynhap_den, double gia_tu, double gia_den, string madv = "all", string manhom = "all")
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.timkiem", "Index"))
                {
                    ngaynhap_den = ngaynhap_den == DateTime.MinValue ? new DateTime(DateTime.Now.Year, 12, 31) : ngaynhap_den;
                    ngaynhap_tu = ngaynhap_tu == DateTime.MinValue ? new DateTime(DateTime.Now.Year, 01, 01) : ngaynhap_tu;

                    var model = (from giathuetnct in _db.GiaGiaoDichDatCt
                                 join giathuetn in _db.GiaGiaoDichDat on giathuetnct.Mahs equals giathuetn.Mahs
                                 join donvi in _db.DsDonVi on giathuetn.Madv equals donvi.MaDv
                                 join nhomtn in _db.GiaGiaoDichDatNhom on giathuetnct.Manhom equals nhomtn.Manhom
                                 select new GiaGiaoDichDatCt
                                 {
                                     Id = giathuetnct.Id,
                                     Gia = giathuetnct.Gia,
                                     Mahs = giathuetnct.Mahs,
                                     Madv = giathuetn.Madv,
                                     Manhom = giathuetnct.Manhom,
                                     Thoidiem = giathuetn.Thoidiem,
                                     Tendv = donvi.TenDv,
                                     Ten = giathuetnct.Ten,
                                     Trangthai = giathuetn.Trangthai,
                                     Dvt = giathuetnct.Dvt,
                                     Tennhom = nhomtn.Tennhom,
                                 });
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model = model.Where(x => x.Thoidiem >= ngaynhap_tu && x.Thoidiem <= ngaynhap_den && list_trangthai.Contains(x.Trangthai));

                    if (madv != "all")
                    {
                        model = model.Where(t => t.Madv == madv);
                    }

                    if (manhom != "all")
                    {
                        model = model.Where(t => t.Manhom == manhom);
                    }
                    model = model.Where(t => t.Gia >= gia_tu);
                    if (gia_den > 0)
                    {
                        model = model.Where(t => t.Gia <= gia_den);
                    }
                    ViewData["ngaynhap_tu"] = ngaynhap_tu;
                    ViewData["ngaynhap_den"] = ngaynhap_den;
                    ViewData["gia_tu"] = gia_tu;
                    ViewData["gia_den"] = gia_den;
                    ViewData["madv"] = madv;
                    ViewData["manhom"] = manhom;


                    ViewData["DsDiaBan"] = _db.DsDiaBan;
                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["NhomTn"] = _db.GiaGiaoDichDatNhom.Where(t => t.Theodoi == "TD").ToList();
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá giao dịch đất thực tế trên thị trường";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dg_giaodichdattrenthitruong";
                    ViewData["MenuLv3"] = "menu_dg_giaodichdattrenthitruong_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichDat/TimKiem/Index.cshtml", model);

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

        [Route("GiaGiaoDichDat/PrintSearch")]
        [HttpPost]
        public IActionResult PrintSearch(DateTime ngaynhap_tu, DateTime ngaynhap_den, double gia_tu, double gia_den, string madv = "all", string manhom = "all")
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giagiaodichdattrenthitruong.timkiem", "Index"))
                {
                    ngaynhap_den = ngaynhap_den == DateTime.MinValue ? new DateTime(DateTime.Now.Year, 12, 31) : ngaynhap_den;
                    ngaynhap_tu = ngaynhap_tu == DateTime.MinValue ? new DateTime(DateTime.Now.Year, 01, 01) : ngaynhap_tu;

                    var model = (from giathuetnct in _db.GiaGiaoDichDatCt
                                 join giathuetn in _db.GiaGiaoDichDat on giathuetnct.Mahs equals giathuetn.Mahs
                                 join donvi in _db.DsDonVi on giathuetn.Madv equals donvi.MaDv
                                 join nhomtn in _db.GiaGiaoDichDatNhom on giathuetnct.Manhom equals nhomtn.Manhom
                                 select new GiaGiaoDichDatCt
                                 {
                                     Id = giathuetnct.Id,
                                     Gia = giathuetnct.Gia,
                                     Mahs = giathuetnct.Mahs,
                                     Madv = giathuetn.Madv,
                                     Manhom = giathuetnct.Manhom,
                                     Thoidiem = giathuetn.Thoidiem,
                                     Tendv = donvi.TenDv,
                                     Ten = giathuetnct.Ten,
                                     Trangthai = giathuetn.Trangthai,
                                     Dvt = giathuetnct.Dvt,
                                     Tennhom = nhomtn.Tennhom,
                                 });
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model = model.Where(x => x.Thoidiem >= ngaynhap_tu && x.Thoidiem <= ngaynhap_den && list_trangthai.Contains(x.Trangthai));

                    if (madv != "all")
                    {
                        model = model.Where(t => t.Madv == madv);
                    }

                    if (manhom != "all")
                    {
                        model = model.Where(t => t.Manhom == manhom);
                    }
                    model = model.Where(t => t.Gia >= gia_tu);
                    if (gia_den > 0)
                    {
                        model = model.Where(t => t.Gia <= gia_den);
                    }
                    ViewData["ngaynhap_tu"] = ngaynhap_tu;
                    ViewData["ngaynhap_den"] = ngaynhap_den;
                    ViewData["gia_tu"] = gia_tu;
                    ViewData["gia_den"] = gia_den;
                    ViewData["madv"] = madv;
                    ViewData["manhom"] = manhom;


                    ViewData["DsDiaBan"] = _db.DsDiaBan;
                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["NhomTn"] = _db.GiaGiaoDichDatNhom.Where(t => t.Theodoi == "TD").ToList();
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá giao dịch đất thực tế trên thị trường";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dg_giaodichdattrenthitruong";
                    ViewData["MenuLv3"] = "menu_dg_giaodichdattrenthitruong_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichDat/TimKiem/PrintSearch.cshtml", model);

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

        [Route("GiaGiaoDichDatCt/Edit")]
        [HttpPost]
        public JsonResult EditCt(int Id)
        {
            var model = _db.GiaGiaoDichDatCt.FirstOrDefault(p => p.Id == Id);
            var danhmucdonvitinh = _db.DmDvt;
            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Giá giao dịch đất thực tế trên thị trường (đồng):</label>";
                result += "<input type='text' id='gia_edit' name='gia_edit' value='" + Helpers.ConvertDbToStr(model.Gia) + "' class='form-control money-decimal-mask'/>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Đơn vị tính</label>";
                result += "<select id='donvitinh_edit' name='donvitinh_edit' class='form-control select2basic' style='width:100%'>";
                foreach (var item in danhmucdonvitinh)
                {
                    result += "<option value ='" + item.Dvt + "'" + ((string)model.Dvt == item.Dvt ? "selected" : "") + " >" + item.Dvt + "</ option >";
                }
                result += "</select>";

                result += "</div>";
                result += "</div>";

                result += "<input hidden type='text' id='id_edit' name='id_edit' value='" + model.Id + "'/>";
                result += "</div>";

                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Không tìm thấy thông tin cần chỉnh sửa!!!" };
                return Json(data);
            }
        }

        [Route("GiaGiaoDichDatCt/Update")]
        [HttpPost]
        public JsonResult UpdateCt(int Id, double Gia, string Dvt)
        {
            var model = _db.GiaGiaoDichDatCt.FirstOrDefault(t => t.Id == Id);
            model.Gia = Gia;
            model.Dvt = Dvt;
            model.Updated_at = DateTime.Now;
            _db.GiaGiaoDichDatCt.Update(model);
            _db.SaveChanges();
            string result = GetDataCt(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        public string GetDataCt(string Mahs)
        {
            var model = _db.GiaGiaoDichDatCt.Where(t => t.Mahs == Mahs).ToList();
            var DanhMucNhom = _db.GiaGiaoDichDatNhom;
            int record = 1;
            string result = "";
            foreach (var nhom in DanhMucNhom)
            {
                var data = model.Where(x => x.Manhom == nhom.Manhom);
                if (data.Any())
                {
                    result += "<p style='text-align:center; font-size:16px; text-transform:uppercase; font-weight:bold'>" + @nhom.Tennhom + "</p>";
                    result += "<table class='table table-striped table-bordered table-hover table-responsive class-nosort'>";
                    result += "<thead>";
                    result += "<tr style='text-align:center'>";
                    result += "<th width='2%'>#</th>";
                    result += "<th width='25%'>Phân loại nhà cho thuê</th>";
                    result += "<th>Đơn vị tính</th>";
                    result += "<th>Giá cho thuê (đồng)</th>";
                    result += "<th width='5%'>Thao tác</th>";
                    result += "</tr>";
                    result += "</thead>";
                    result += "<tbody>";
                    foreach (var item in data)
                    {
                        result += "<tr>";
                        result += "<td class='text-center'>" + record++ + "</td>";
                        result += "<td class='active' style='font-weight:bold'>" + item.Ten + "</td>";
                        result += "<td style='text-align:right; font-weight:bold'>" + item.Dvt + "</td>";
                        result += "<td style='text-align:right; font-weight:bold'>" + item.Gia + "</td>";
                        result += "<td>";
                        result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Nhập giá'";
                        result += " data-target='#Edit_Modal' data-toggle='modal' onclick='SetEdit(`" + item.Id + "`)'>";
                        result += "<i class='icon-lg la la-edit text-primary'></i>";
                        result += "</button>";
                        result += "</td>";
                        result += "</tr>";
                    }
                    result += "</tbody>";
                    result += "</table>";
                }
            }
            return result;

        }

        private void RemoveData_Ct_CXD(string maDv)
        {
            var check = _db.GiaGiaoDichDatCt.Where(t => t.Madv == maDv && t.Trangthai == "CXD");
            if (check.Any())
            {
                _db.GiaGiaoDichDatCt.RemoveRange(check);
            }
            // xóa thông tin giấy tờ chưa lưu lại
            var model_file_cxd = _db.ThongTinGiayTo.Where(t => t.Status == "CXD" && t.Madv == maDv);
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
            }
            _db.SaveChanges();
        }
        private void SaveData_Ct_CXD(string maHs)
        {
            // Lưu lại hồ sơ chi tiết chưa xác định
            var modelct = _db.GiaGiaoDichDatCt.Where(t => t.Mahs == maHs && t.Trangthai == "CXD").ToList();
            if (modelct.Any())
            {
                modelct.ForEach(x => x.Trangthai = "XD");
            }
            var giayto = _db.ThongTinGiayTo.Where(t => t.Mahs == maHs && t.Status == "CXD").ToList();
            if (giayto.Any())
            {
                giayto.ForEach(x => x.Status = "XD");
            }
            _db.SaveChanges();
        }
    }
}
