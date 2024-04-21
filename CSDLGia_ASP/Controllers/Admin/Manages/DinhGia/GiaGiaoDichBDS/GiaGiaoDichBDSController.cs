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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaGiaoDichBDS
{
    public class GiaGiaoDichBDSController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IDsDonviService _dsDonviService;
        private readonly ITrangThaiHoSoService _trangThaiHoSoService;

        public GiaGiaoDichBDSController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment, IDsDonviService dsDonviService, ITrangThaiHoSoService trangThaiHoSoService)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
            _dsDonviService = dsDonviService;
            _trangThaiHoSoService = trangThaiHoSoService;
        }

        [Route("GiaGiaoDichBDS/DanhSach")]
        [HttpGet]
        public IActionResult Index(string Madv, int Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaodichbds.thongtin", "Index"))
                {
                    Madv = string.IsNullOrEmpty(Madv) ? Helpers.GetSsAdmin(HttpContext.Session, "Madv") : Madv;
                    var model_donvi = _dsDonviService.GetListDonvi(Helpers.GetSsAdmin(HttpContext.Session, "Madv"));
                    List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();

                    IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaGiaoDichBDS> model = _db.GiaGiaoDichBDS.Where(t => list_madv.Contains(t.Madv));
                    if (Madv != "all")
                    {
                        model = model.Where(t => t.Madv == Madv);
                    }

                    if (Nam != 0)
                    {
                        model = model.Where(t => t.Thoidiem.Year == Nam);
                    }
                    ViewData["Nam"] = Nam;
                    ViewData["Madv"] = Madv;
                    ViewData["DsDonvi"] = model_donvi;
                    ViewData["DanhMucNhom"] = _db.GiaGiaoDichBDSNhom.ToList();
                    ViewData["Title"] = "Thông tin giá giao dịch bất động sản";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dg_giaodichbds";
                    ViewData["MenuLv3"] = "menu_dg_giaodichbds_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichBDS/DanhSach/Index.cshtml", model);
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

        [Route("GiaGiaoDichBDS/Create")]
        [HttpGet]
        public IActionResult Create(string maNhom, string MadvBc)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaodichbds.thongtin", "Create"))
                {

                    var Mahs = MadvBc + "_" + DateTime.Now.ToString("yyMMddssmmHH");
                    this.RemoveData_ChuaXacDinh(MadvBc);

                    var danhmuc = _db.GiaGiaoDichBDSDm.ToList();
                    if (maNhom != "all")
                    {
                        danhmuc = danhmuc.Where(t => t.Manhom == maNhom).ToList();
                    }
                    var chitiet = new List<GiaGiaoDichBDSCt>();
                    foreach (var item in danhmuc)
                    {
                        chitiet.Add(new GiaGiaoDichBDSCt()
                        {
                            Mahs = Mahs,
                            Ten = item.Ten,
                            Trangthai = "CXD",
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                            Madv = MadvBc,
                            Manhom = item.Manhom,
                            Dvt = item.Dvt,
                        });
                    }
                    _db.GiaGiaoDichBDSCt.AddRange(chitiet);
                    _db.SaveChanges();

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaGiaoDichBDS
                    {
                        Mahs = Mahs,
                        Madv = MadvBc,
                        Manhom = maNhom,
                    };
                    model.GiaGiaoDichBDSCt = _db.GiaGiaoDichBDSCt.Where(t => t.Mahs == Mahs).ToList();

                    ViewData["DanhMucNhom"] = _db.GiaGiaoDichBDSNhom;
                    ViewData["Title"] = "Bảng giá giao dịch bất động sản";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dg_giaodichbds";
                    ViewData["MenuLv3"] = "menu_dg_giaodichbds_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichBDS/DanhSach/Create.cshtml", model);

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

        [Route("GiaGiaoDichBDS/Store")]
        [HttpPost]
        public async Task<IActionResult> Store(CSDLGia_ASP.Models.Manages.DinhGia.GiaGiaoDichBDS request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaodichbds.thongtin", "Create"))
                {
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaGiaoDichBDS
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
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaGiaoDichBDS.Add(model);
                    this.SaveData_ChuaXacDinh(model.Mahs);
                    await _db.SaveChangesAsync();

                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Thêm mới");

                    return RedirectToAction("Index", "GiaGiaoDichBDS", new { request.Madv });
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

        [Route("GiaGiaoDichBDS/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaodichbds.thongtin", "Edit"))
                {
                    var model = _db.GiaGiaoDichBDS.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_ct = _db.GiaGiaoDichBDSCt.Where(t => t.Mahs == model.Mahs);

                    model.GiaGiaoDichBDSCt = model_ct.ToList();
                    var giayto = _db.ThongTinGiayTo.Where(x => x.Mahs == Mahs).ToList();
                    model.ThongTinGiayTo = giayto;
                    ViewData["Title"] = "Bảng giá giao dịch bất động sản";
                    ViewData["DanhMucNhom"] = _db.GiaGiaoDichBDSNhom;
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dg_giaodichbds";
                    ViewData["MenuLv3"] = "menu_dg_giaodichbds_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichBDS/DanhSach/Edit.cshtml", model);

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

        [Route("GiaGiaoDichBDS/Update")]
        [HttpPost]
        public async Task<IActionResult> Update(CSDLGia_ASP.Models.Manages.DinhGia.GiaGiaoDichBDS request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaodichbds.thongtin", "Edit"))
                {
                    var model = _db.GiaGiaoDichBDS.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Soqdlk = request.Soqdlk;
                    model.Thoidiemlk = request.Thoidiemlk;
                    model.Cqbh = request.Cqbh;
                    model.Ghichu = request.Ghichu;
                    model.Ipf1 = request.Ipf1;
                    model.Updated_at = DateTime.Now;

                    _db.GiaGiaoDichBDS.Update(model);
                    this.SaveData_ChuaXacDinh(model.Mahs);
                    await _db.SaveChangesAsync();

                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Cập nhật");

                    return RedirectToAction("Index", "GiaGiaoDichBDS", new { request.Madv });
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

        [Route("GiaGiaoDichBDS/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaodichbds.thongtin", "Delete"))
                {
                    var model = _db.GiaGiaoDichBDS.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaGiaoDichBDS.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.GiaGiaoDichBDSCt.Where(t => t.Mahs == model.Mahs);
                    _db.GiaGiaoDichBDSCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaGiaoDichBDS", new { model.Madv });
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

        [Route("GiaGiaoDichBDS/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaodichbds.thongtin", "Index"))
                {
                    var model = _db.GiaGiaoDichBDS.FirstOrDefault(t => t.Mahs == Mahs);
                    var modelct = _db.GiaGiaoDichBDSCt.Where(t => t.Mahs == Mahs);

                    var viewModel = new CSDLGia_ASP.Models.Manages.DinhGia.GiaGiaoDichBDS
                    {
                        // Thông tin hồ sơ
                        Soqd = model.Soqd,
                        Mahs = model.Mahs,
                        Madv = model.Madv,
                        Madiaban = model.Madiaban,
                        Thoidiem = model.Thoidiem,
                        Macqcq = model.Macqcq,
                        //// Thông tin hồ sơ chi tiết
                        //Ten = modelct.Ten,
                        //Dvt = modelct.Dvt,
                        //Gia = modelct.Gia,
                    };
                    viewModel.GiaGiaoDichBDSCt = modelct.ToList();
                    var donvi = _db.DsDonVi.First(x => x.MaDv == model.Madv);
                    ViewData["DanhMucNhom"] = _db.GiaGiaoDichBDSNhom;
                    ViewData["TenDiaBan"] = _db.DsDiaBan.First(x => x.MaDiaBan == donvi.MaDiaBan).TenDiaBan;
                    ViewData["TenDonVi"] = donvi.TenDv;
                    ViewData["Title"] = "Bảng giá giao dịch bất động sản";
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichBDS/DanhSach/Show.cshtml", viewModel);

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

        [Route("GiaGiaoDichBDS/Complete")]
        [HttpPost]
        public IActionResult Complete(string mahs_chuyen, string trangthai_complete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaodichbds.thongtin", "Approve"))
                {
                    var model = _db.GiaGiaoDichBDS.FirstOrDefault(t => t.Mahs == mahs_chuyen);
                    model.Trangthai = trangthai_complete;
                    model.Updated_at = DateTime.Now;
                   
                    _db.GiaGiaoDichBDS.Update(model);
                    _db.SaveChanges();

                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), trangthai_complete);

                    return RedirectToAction("Index", "GiaGiaoDichBDS", new { model.Madv });

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

        [Route("GiaGiaoDichBDS/Search")]
        [HttpGet]
        public IActionResult Search(DateTime ngaynhap_tu, DateTime ngaynhap_den, double gia_tu, double gia_den,string Soqd="all", string madv = "all", string manhom = "all")
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaodichbds.timkiem", "Index"))
                {
                    ngaynhap_tu = ngaynhap_tu == DateTime.MinValue ? new DateTime(DateTime.Now.Year, 01, 01) : ngaynhap_tu;
                    ngaynhap_den = ngaynhap_den == DateTime.MinValue ? new DateTime(DateTime.Now.Year, 12, 31) : ngaynhap_den;
                    var model = (from giathuetnct in _db.GiaGiaoDichBDSCt
                                 join giathuetn in _db.GiaGiaoDichBDS on giathuetnct.Mahs equals giathuetn.Mahs
                                 join donvi in _db.DsDonVi on giathuetn.Madv equals donvi.MaDv
                                 select new GiaGiaoDichBDSCt
                                 {
                                     Madv = giathuetn.Madv,
                                     Thoidiem = giathuetn.Thoidiem,
                                     Ten = giathuetnct.Ten,
                                     Dvt = giathuetnct.Dvt,
                                     Gia = giathuetnct.Gia,
                                     Manhom = giathuetnct.Manhom,
                                     Mahs = giathuetn.Mahs,
                                     Trangthai = giathuetn.Trangthai,

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
                    if (Soqd !="all")
                    {
                        model = model.Where(x => x.Mahs == Soqd);
                    }
                    ViewData["ListQuyetDinh"] = _db.GiaGiaoDichBDS;
                    ViewData["QuyetDinh"]     = Soqd;



                    ViewData["ngaynhap_tu"] = ngaynhap_tu;
                    ViewData["ngaynhap_den"] = ngaynhap_den;
                    ViewData["gia_tu"] = Helpers.ConvertDbToStr(gia_tu);
                    ViewData["gia_den"] = Helpers.ConvertDbToStr(gia_den);
                    ViewData["madv"] = madv;
                    ViewData["manhom"] = manhom;
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["NhomTn"] = _db.GiaGiaoDichBDSNhom.Where(t => t.Theodoi == "TD").ToList();
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá giao dịch bất động sản";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dg_giaodichbds";
                    ViewData["MenuLv3"] = "menu_dg_giaodichbds_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichBDS/TimKiem/Index.cshtml", model);
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

        [Route("GiaGiaoDichBDS/PrintSearch")]
        [HttpPost]
        public IActionResult PrintSearch(DateTime ngaynhap_tu, DateTime ngaynhap_den, double gia_tu, double gia_den, string Soqd="all", string madv = "all", string manhom = "all")
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaodichbds.timkiem", "Index"))
                {
                    ngaynhap_tu = ngaynhap_tu == DateTime.MinValue ? new DateTime(DateTime.Now.Year, 01, 01) : ngaynhap_tu;
                    ngaynhap_den = ngaynhap_den == DateTime.MinValue ? new DateTime(DateTime.Now.Year, 12, 31) : ngaynhap_den;
                    var model = (from giathuetnct in _db.GiaGiaoDichBDSCt
                                 join giathuetn in _db.GiaGiaoDichBDS on giathuetnct.Mahs equals giathuetn.Mahs
                                 join donvi in _db.DsDonVi on giathuetn.Madv equals donvi.MaDv
                                 select new GiaGiaoDichBDSCt
                                 {
                                     Madv = giathuetn.Madv,
                                     Thoidiem = giathuetn.Thoidiem,
                                     Ten = giathuetnct.Ten,
                                     Dvt = giathuetnct.Dvt,
                                     Gia = giathuetnct.Gia,
                                     Manhom = giathuetnct.Manhom,
                                     Mahs = giathuetn.Mahs,
                                     Trangthai = giathuetn.Trangthai,

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
                    if (Soqd!="all")
                    {
                        model = model.Where(x=>x.Mahs == Soqd);
                    }

                    ViewData["ngaynhap_tu"] = ngaynhap_tu;
                    ViewData["ngaynhap_den"] = ngaynhap_den;
                    ViewData["gia_tu"] = gia_tu;
                    ViewData["gia_den"] = gia_den;
                    ViewData["madv"] = madv;
                    ViewData["manhom"] = manhom;
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["NhomTn"] = _db.GiaGiaoDichBDSNhom.Where(t => t.Theodoi == "TD").ToList();


                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá giao dịch bất động sản";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dg_giaodichbds";
                    ViewData["MenuLv3"] = "menu_dg_giaodichbds_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaGiaoDichBDS/TimKiem/PrintSearch.cshtml", model);
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
        [Route("GiaGiaoDichBDSCt/Edit")]
        [HttpPost]
        public JsonResult EditCt(int Id)
        {
            var model = _db.GiaGiaoDichBDSCt.FirstOrDefault(p => p.Id == Id);
            var danhmucdonvitinh = _db.DmDvt;
            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";
                result += "</div>";

                //result += "<div class='col-xl-12'>";
                //result += "<div class='form-group fv-plugins-icon-container'>";
                //result += "<label>Đơn vị tính</label>";
                //result += "<input type='text' id='Dvt_edit' name='Dvt_edit' value='" + model.Dvt + "' class='form-control' style='font-weight: bold'/>";
                //result += "</div>";
                //result += "</div>";


                result += "<div class='form-group' style='width:100%'>";
                result += "<div class='col-xl-12'>";
                result += "<label>Đơn vị tính</label>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<select id='Dvt_edit' name='Dvt_edit' class='form-control select2basic' style='width:100%'>";
                foreach (var item in danhmucdonvitinh)
                {
                    result += "<option value ='" + item.Dvt + "'" + ((string)model.Dvt == item.Dvt ? "selected" : "") + " >" + item.Dvt + "</ option >";
                }
                result += "</select>";

                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Giá giao dịch bất động sản (đồng)</label>";
                result += "<input type='text' id='gia_edit' name='gia_edit' value='" + Helpers.ConvertDbToStr(model.Gia) + "' class='form-control money-decimal-mask' style='font-weight: bold'/>";
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
        [Route("GiaGiaoDichBDSCt/Update")]
        [HttpPost]
        public JsonResult UpdateCt(int Id, double Gia, string Dvt)
        {
            var model = _db.GiaGiaoDichBDSCt.FirstOrDefault(t => t.Id == Id);
            model.Gia = Gia;
            model.Dvt = Dvt;
            model.Updated_at = DateTime.Now;
            _db.GiaGiaoDichBDSCt.Update(model);
            _db.SaveChanges();
            string result = GetDataCt(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }
        public string GetDataCt(string Mahs)
        {
            var model = _db.GiaGiaoDichBDSCt.Where(t => t.Mahs == Mahs).ToList();
            var DanhMucNhom = _db.GiaGiaoDichBDSNhom;
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
                    result += "<th width='9%'>Thao tác</th>";
                    result += "</tr>";
                    result += "</thead>";
                    result += "<tbody>";
                    foreach (var item in data)
                    {
                        result += "<tr>";
                        result += "<td class='text-center'>" + record++ + "</td>";
                        result += "<td class='active' style='font-weight:bold'>" + item.Ten + "</td>";
                        result += "<td >" + item.Dvt + "</td>";
                        result += "<td style='text-align:right; font-weight:bold'>" + Helpers.ConvertDbToStr(item.Gia) + "</td>";
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
                    result += "</div>";
                }
            }
            return result;

        }
        private void RemoveData_ChuaXacDinh(string maDonVi)
        {
            // 
            var check = _db.GiaGiaoDichBDSCt.Where(t => t.Madv == maDonVi && t.Trangthai == "CXD");
            if (check.Any())
            {
                _db.GiaGiaoDichBDSCt.RemoveRange(check);
            }
            // xóa thông tin giấy tờ chưa lưu lại
            var model_file_cxd = _db.ThongTinGiayTo.Where(t => t.Status == "CXD" && t.Madv == maDonVi);
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
        public void SaveData_ChuaXacDinh(string MaHoSo)
        {
            // 
            var models = _db.GiaGiaoDichBDSCt.Where(t => t.Mahs == MaHoSo && t.Trangthai == "CXD").ToList();
            if (models.Any())
            {
                models.ForEach(x => x.Trangthai = "XD");
            }
            // xóa thông tin giấy tờ chưa lưu lại
            var model_file_cxd = _db.ThongTinGiayTo.Where(t => t.Status == "CXD" && t.Mahs == MaHoSo).ToList();
            if (model_file_cxd.Any())
            {
                model_file_cxd.ForEach(x => x.Status = "XD");
            }
            _db.SaveChanges();
        }
    }
}
