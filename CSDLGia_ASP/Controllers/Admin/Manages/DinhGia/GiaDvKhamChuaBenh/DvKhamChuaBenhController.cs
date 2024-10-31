﻿using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.Services;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDvKhamChuaBenh
{
    public class DvKhamChuaBenhController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IDsDonviService _dsDonviService;
        private readonly ITrangThaiHoSoService _trangThaiHoSoService;
        public DvKhamChuaBenhController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment, IDsDonviService dsDonviService, ITrangThaiHoSoService trangThaiHoSoService)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
            _dsDonviService = dsDonviService;
            _trangThaiHoSoService = trangThaiHoSoService;
        }

        [Route("DinhGiaDvKcb")]
        [HttpGet]
        public IActionResult Index(string Madv, int Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khamchuabenh.thongtin", "Index"))
                {
                    Madv = string.IsNullOrEmpty(Madv) ? Helpers.GetSsAdmin(HttpContext.Session, "Madv") : Madv;
                    var model_donvi = _dsDonviService.GetListDonvi(Helpers.GetSsAdmin(HttpContext.Session, "Madv"));
                    List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();
                    IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaDvKcb> model = _db.GiaDvKcb;

                    if (Madv != "all")
                    {
                        model = model.Where(t => t.Madv == Madv);
                    }

                    if (Nam != 0)
                    {
                        model = model.Where(t => t.Thoidiem.Year == Nam);
                    }
                    ViewData["DanhMucNhom"] = _db.GiaDvKcbNhom;
                    ViewData["Nam"] = Nam;
                    ViewData["Madv"] = Madv;
                    ViewData["DsDonvi"] = model_donvi;
                    ViewData["Title"] = " Thông tin hồ sơ giá dịch vụ khám chữa bệnh";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgkcb";
                    ViewData["MenuLv3"] = "menu_dgkcb_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaDvKhamChuaBenh/Index.cshtml", model);

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


        [Route("DinhGiaDvKcb/Create")]
        [HttpGet]
        public IActionResult Create(string Madv, string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khamchuabenh.thongtin", "Create"))
                {
                    var model_ct_cxd = _db.GiaDvKcbCt.Where(t => t.Trangthai == "CXD" && t.Madv == Madv);
                    if (model_ct_cxd.Any())
                    {
                        _db.GiaDvKcbCt.RemoveRange(model_ct_cxd);
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
                    /*var modelcxd = _db.GiaDvKcbCt.Where(t => t.Trangthai == "CXD" && t.Madv == Madv).ToList();
                    if (modelcxd != null)
                    {
                        _db.GiaDvKcbCt.RemoveRange(modelcxd);
                        _db.SaveChanges();
                    }*/

                    var model = new VMDinhGiaDvKcb
                    {
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Madv = Madv,
                        Manhom = Manhom,
                        Thoidiem = DateTime.Now,
                        PhanLoaiHoSo = "HOSOCHITIET",
                    };

                    var danhmuc = _db.GiaDvKcbDm.ToList();
                    if (Manhom != "all")
                    {
                        danhmuc = danhmuc.Where(t => t.Manhom == Manhom).ToList();
                    }
                    else
                    {
                        danhmuc = danhmuc.ToList();
                    }

                    var chitiet = new List<GiaDvKcbCt>();

                    foreach (var item in danhmuc)
                    {
                        chitiet.Add(new GiaDvKcbCt()
                        {
                            Mahs = model.Mahs,
                            Madv = model.Madv,
                            Maspdv = item.Maspdv,
                            Tenspdv = item.Tenspdv,
                            Madichvu = item.Madichvu,
                            Dvt = item.Dvt,
                            Phanloai = item.Phanloai,
                            Manhom = item.Manhom,
                            Trangthai = "CXD",
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        });
                    }
                    _db.GiaDvKcbCt.AddRange(chitiet);
                    _db.SaveChanges();
                    model.GiaDvKcbCt = _db.GiaDvKcbCt.Where(t => t.Mahs == model.Mahs).ToList();


                    var groupmanhom1 = _db.GiaDvKcbCt.Where(t => t.Mahs == model.Mahs).Select(item => item.Manhom).Distinct().ToList();
                    var groupmanhom2 = _db.GiaDvKcbCt.Where(item => item.Manhom == Manhom && item.Mahs == model.Mahs).Select(item => item.Manhom).Distinct().ToList();

                    List<string> groupmanhom;

                    if (Manhom != "all")
                    {
                        groupmanhom = groupmanhom2;
                    }
                    else
                    {
                        groupmanhom = groupmanhom1;
                    }

                    ViewData["GroupMaNhom"] = groupmanhom;
                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["GiaDvKcbNhom"] = _db.GiaDvKcbNhom.ToList();
                    ViewData["Title"] = "Thêm mới giá dịch vụ khám chữa bệnh";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgkcb";
                    ViewData["MenuLv3"] = "menu_dgkcb_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaDvKhamChuaBenh/Create.cshtml", model);
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

        [Route("DinhGiaDvKcb/NhanExcel")]
        [HttpGet]
        public IActionResult NhanExcel(string Madv, string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khamchuabenh.thongtin", "Create"))
                {

                    var modelcxd = _db.GiaDvKcbCt.Where(t => t.Ghichu == "CXD").ToList();
                    if (modelcxd != null)
                    {

                        _db.GiaDvKcbCt.RemoveRange(modelcxd);

                        _db.SaveChanges();
                    }
                    var model = new VMDinhGiaDvKcb
                    {
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Madv = Madv,
                        Manhom = Manhom,
                        Thoidiem = DateTime.Now,
                        PhanLoaiHoSo = "HOSOCHITIET",
                    };


                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["codeExcel"] = model.CodeExcel;//Gán ra view để dùng chung
                    ViewData["Title"] = "Thêm mới giá dịch vụ khám chữa bệnh";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgkcb";
                    ViewData["MenuLv3"] = "menu_dgkcb_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaDvKhamChuaBenh/NhanExcel.cshtml", model);
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

        [Route("DinhGiaDvKcb/Store")]
        [HttpPost]
        public IActionResult Store(VMDinhGiaDvKcb request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khamchuabenh.thongtin", "Create"))
                {
                    // 2024.03.15 Gộp Update và phần nhận cho Excel (chưa update file đính kèm)
                    if (_db.GiaDvKcb.Where(x => x.Mahs == request.Mahs).Any())
                    {
                        //Xử lý hồ sơ
                        var modelExcel = _db.GiaDvKcb.FirstOrDefault(t => t.Mahs == request.Mahs);
                        modelExcel.Madiaban = request.Madiaban;
                        modelExcel.Soqd = request.Soqd;
                        modelExcel.Thoidiem = request.Thoidiem;
                        modelExcel.Thongtin = request.Thongtin;
                        modelExcel.Ghichu = request.Ghichu;
                        modelExcel.CodeExcel = request.CodeExcel;
                        modelExcel.Updated_at = DateTime.Now;
                        _db.GiaDvKcb.Update(modelExcel);

                        //Add Log
                        _trangThaiHoSoService.LogHoSo(modelExcel.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Thêm mới");

                        return RedirectToAction("Index", "DvKhamChuaBenh", new { Madv = request.Madv });
                    }
                    //Phần cũ
                    var model = new GiaDvKcb
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Manhom = request.Manhom,
                        Soqd = request.Soqd,
                        Thoidiem = request.Thoidiem,
                        Mota = request.Mota,
                        PhanLoaiHoSo = request.PhanLoaiHoSo,
                        CodeExcel = request.CodeExcel,
                        Trangthai = "CC",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };

                    _db.GiaDvKcb.Add(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaDvKcbCt.Where(t => t.Mahs == request.Mahs).ToList();
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Ghichu = "XD";
                        }
                    }
                    var model_file = _db.ThongTinGiayTo.Where(t => t.Mahs == request.Mahs);
                    if (model_file.Any())
                    {
                        foreach (var file in model_file)
                        {
                            file.Status = "XD";
                        }
                        _db.ThongTinGiayTo.UpdateRange(model_file);
                    }
                    _db.GiaDvKcbCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Thêm mới");

                    // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                    LoggingHelper.LogAction(HttpContext, _db, "Store", "Thêm mới hồ sơ giá khám chữa bệnh");

                    return RedirectToAction("Index", "DvKhamChuaBenh", new { Madv = request.Madv });
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

        [Route("DinhGiaDvKcb/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khamchuabenh.thongtin", "Edit"))
                {
                    var model = _db.GiaDvKcb.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new VMDinhGiaDvKcb
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        Manhom = model.Manhom,
                        Ipf1 = model.Ipf1,
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Mota = model.Mota
                    };

                    var model_ct = _db.GiaDvKcbCt.Where(t => t.Mahs == model_new.Mahs);

                    model_new.GiaDvKcbCt = model_ct.ToList();
                    // Xử lý phần Forech theo mã nhóm khi chọn
                    var groupmanhom = _db.GiaDvKcbCt.Where(t => t.Mahs == model.Mahs).Select(item => item.Manhom).Distinct().ToList();
                    ViewData["GroupMaNhom"] = groupmanhom;
                    // End xử lý phần Forech theo mã nhóm khi chọn

                    //ViewData["Madv"] = model.Madv;
                    //ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["GiaDvKcbNhom"] = _db.GiaDvKcbNhom.ToList();
                    ViewData["Title"] = "Chỉnh sửa giá dịch vụ khám chữa bệnh";
                    ViewData["Ipf1"] = model.Ipf1;
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgkcb";
                    ViewData["MenuLv3"] = "menu_dgkcb_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaDvKhamChuaBenh/Edit.cshtml", model_new);
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

        [Route("DinhGiaDvKcb/Update")]
        [HttpPost]
        public IActionResult Update(VMDinhGiaDvGdDt request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khamchuabenh.thongtin", "Edit"))
                {
                    var model = _db.GiaDvKcb.FirstOrDefault(t => t.Mahs == request.Mahs);

                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Mota = request.Mota;
                    model.Updated_at = DateTime.Now;
                    _db.GiaDvKcb.Update(model);
                    _db.SaveChanges();

                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Cập nhật");

                    // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                    LoggingHelper.LogAction(HttpContext, _db, "Update", " Update hồ sơ giá khám chữa bệnh");

                    return RedirectToAction("Index", "DvKhamChuaBenh", new { Madv = request.Madv });
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

        [Route("DinhGiaDvKcb/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khamchuabenh.thongtin", "Delete"))
                {
                    var model = _db.GiaDvKcb.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaDvKcb.Remove(model);
                    _db.SaveChanges();

                    var model_file = _db.ThongTinGiayTo.Where(t => t.Mahs == model.Mahs);
                    if (model_file.Any())
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        foreach (var file in model_file)
                        {
                            string path_del = Path.Combine(wwwRootPath + "/UpLoad/File/ThongTinGiayTo/", file.FileName);
                            FileInfo fi = new FileInfo(path_del);
                            if (fi != null)
                            {
                                System.IO.File.Delete(path_del);
                                fi.Delete();
                            }
                        }
                        _db.ThongTinGiayTo.RemoveRange(model_file);
                    }

                    var model_ct = _db.GiaDvKcbCt.Where(t => t.Mahs == model.Mahs);
                    _db.GiaDvKcbCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    // Lưu vết từng tài khoản đăng nhập theo thời gian truy cập vào hệ thống 
                    LoggingHelper.LogAction(HttpContext, _db, "Delete", "Xóa hồ sơ giá khám chữa bệnh");


                    return RedirectToAction("Index", "DvKhamChuaBenh", new { Madv = model.Madv });
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


        [Route("DinhGiaDvKcb/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                
                    var model = _db.GiaDvKcb.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new VMDinhGiaDvKcb
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        Manhom = model.Manhom,
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Mota = model.Mota

                    };

                    var model_ct = _db.GiaDvKcbCt.Where(t => t.Mahs == model_new.Mahs);

                    model_new.GiaDvKcbCt = model_ct.ToList();

                    var groupmanhom = _db.GiaDvKcbCt.Where(t => t.Mahs == model.Mahs).Select(item => item.Manhom).Distinct().ToList();
                    ViewData["GroupMaNhom"] = groupmanhom;
                    ViewData["GiaDvKcbNhom"] = _db.GiaDvKcbNhom.ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["Title"] = "Chỉnh sửa giá dịch vụ khám chữa bệnh";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgkcb";
                    ViewData["MenuLv3"] = "menu_dgkcb_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaDvKhamChuaBenh/Show.cshtml", model_new);
              
            }
            else
            {
                return View("Views/Admin/Error/SessionOut.cshtml");
            }
        }

        public IActionResult HoanThanh(string mahs_complete, string trangthai_complete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khamchuabenh.thongtin", "Approve"))
                {
                    var model = _db.GiaDvKcb.FirstOrDefault(p => p.Mahs == mahs_complete);

                    model.Updated_at = DateTime.Now;
                    model.Trangthai = trangthai_complete;

                    _db.GiaDvKcb.Update(model);
                    _db.SaveChanges();

                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), trangthai_complete);
                    return RedirectToAction("Index", "DvKhamChuaBenh", new { Madv = model.Madv, Nam = model.Thoidiem.Year });
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

        [Route("DinhGiaDvKcb/Search")]
        [HttpGet]
        public IActionResult Search(string Madv, string Manhom, DateTime? NgayTu, DateTime? NgayDen, string Mahs, double DonGiaTu, double DonGiaDen, string Tenspdv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khamchuabenh.timkiem", "Index"))
                {
                    DateTime nowDate = DateTime.Now;
                    DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
                    DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);

                    Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;
                    Manhom = string.IsNullOrEmpty(Manhom) ? "all" : Manhom;
                    NgayTu = NgayTu.HasValue ? NgayTu : firstDayCurrentYear;
                    NgayDen = NgayDen.HasValue ? NgayDen : lastDayCurrentYear;
                    Mahs = string.IsNullOrEmpty(Mahs) ? "all" : Mahs;
                    DonGiaTu = DonGiaTu == 0 ? 0 : DonGiaTu;
                    DonGiaDen = DonGiaDen == 0 ? 0 : DonGiaDen;
                    Tenspdv = string.IsNullOrEmpty(Tenspdv) ? "" : Tenspdv;

                    var model = (from hosoct in _db.GiaDvKcbCt
                                 join hoso in _db.GiaDvKcb on hosoct.Mahs equals hoso.Mahs
                                 join nhom in _db.GiaDvKcbNhom on hosoct.Manhom equals nhom.Manhom
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaDvKcbCt
                                 {
                                     Madv = hoso.Madv,
                                     Tendv = donvi.TenDv,
                                     Tennhom = nhom.Tennhom,
                                     SoQD = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                     Madichvu = hosoct.Madichvu,
                                     Tenspdv = hosoct.Tenspdv,
                                     Giadv = hosoct.Giadv,
                                     Manhom = hosoct.Manhom,
                                     Trangthai = hoso.Trangthai,
                                     Mahs = hoso.Mahs
                                 });
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model = model.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen && list_trangthai.Contains(t.Trangthai));
                    if (Madv != "all") { model = model.Where(t => t.Madv == Madv); }
                    if (Manhom != "all") { model = model.Where(t => t.Manhom == Manhom); }
                    if (DonGiaTu > 0) { model = model.Where(t => t.Giadv >= DonGiaTu); }
                    if (DonGiaDen > 0) { model = model.Where(t => t.Giadv <= DonGiaDen); }
                    if (Mahs != "all") { model = model.Where(t => t.Mahs == Mahs); }
                    if (!string.IsNullOrEmpty(Tenspdv))
                    {
                        model = model.Where(t => t.Tenspdv.ToLower().Contains(Tenspdv.ToLower()));
                    }


                    ViewData["Madv"] = Madv;
                    ViewData["Manhom"] = Manhom;
                    ViewData["NgayTu"] = NgayTu;
                    ViewData["NgayDen"] = NgayDen;
                    ViewData["Mahs"] = Mahs;
                    ViewData["DonGiaTu"] = Helpers.ConvertDbToStr(DonGiaTu);
                    ViewData["DonGiaDen"] = Helpers.ConvertDbToStr(DonGiaDen);
                    ViewData["Tenspdv"] = Tenspdv;
                    ViewData["DanhSachHoSo"] = _db.GiaDvKcb.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen && list_trangthai.Contains(t.Trangthai));
                    ViewData["DanhMucNhom"] = _db.GiaDvKcbNhom;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");

                    ViewData["Title"] = "Tìm kiếm thông tin định giá khám chữa bệnh";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgkcb";
                    ViewData["MenuLv3"] = "menu_dgkcb_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaDvKhamChuaBenh/TimKiem/Search.cshtml", model);
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

        [Route("DinhGiaDvKcb/PrintSearch")]
        [HttpGet]
        public IActionResult Print(string Madv, string Manhom, DateTime? NgayTu, DateTime? NgayDen,
                                    string Mahs, double DonGiaTu, double DonGiaDen, string Tenspdv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.khamchuabenh.timkiem", "Index"))
                {
                    var model = (from hosoct in _db.GiaDvKcbCt
                                 join hoso in _db.GiaDvKcb on hosoct.Mahs equals hoso.Mahs
                                 join nhom in _db.GiaDvKcbNhom on hosoct.Manhom equals nhom.Manhom
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaDvKcbCt
                                 {
                                     Madv = hoso.Madv,
                                     Tendv = donvi.TenDv,
                                     Tennhom = nhom.Tennhom,
                                     SoQD = hoso.Soqd,
                                     Thoidiem = hoso.Thoidiem,
                                     Madichvu = hosoct.Madichvu,
                                     Tenspdv = hosoct.Tenspdv,
                                     Giadv = hosoct.Giadv,
                                     Manhom = hosoct.Manhom,
                                     Trangthai = hoso.Trangthai,
                                     Mahs = hoso.Mahs
                                 });

                 
                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                 
                    model = model.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen && list_trangthai.Contains(t.Trangthai));
                 
                    if (Madv != "all") { model = model.Where(t => t.Madv == Madv); }
                    if (Manhom != "all") { model = model.Where(t => t.Manhom == Manhom); }
                    if (DonGiaTu > 0) { model = model.Where(t => t.Giadv >= DonGiaTu); }
                    if (DonGiaDen > 0) { model = model.Where(t => t.Giadv <= DonGiaDen); }
                    if (Mahs != "all") { model = model.Where(t => t.Mahs == Mahs); }
                    if (!string.IsNullOrEmpty(Tenspdv))
                    {
                        model = model.Where(t => t.Tenspdv.ToLower().Contains(Tenspdv.ToLower()));
                    }

                    ViewData["Title"] = "Kết quả tìm kiếm thông tin định giá khám chữa bệnh";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgkcb";
                    ViewData["MenuLv3"] = "menu_dgkcb_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaDvKhamChuaBenh/TimKiem/Result.cshtml", model);
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

        [Route("DinhGiaDvKcb/EditCt")]
        [HttpPost]
        public JsonResult EditCt(int Id)
        {
            var model = _db.GiaDvKcbCt.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Đơn giá</b></label>";
                result += "<input type='text' id='Giadv_edit' name='Giadv_edit'  value='" + model.Giadv + "' class='form-control money-decimal-mask' style='font-weight: bold' />";
                result += "</div>";
                result += "</div>";
                result += "</div>";
                result += "<input hidden id='id_edit' name='id_edit' value='" + Id + "'/>";
                result += "</div>";
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

        [Route("DinhGiaDvKcb/UpdateCt")]
        [HttpPost]
        public JsonResult UpdateCt(int Id, double Giadv)
        {
            var model = _db.GiaDvKcbCt.FirstOrDefault(t => t.Id == Id);
            model.Giadv = Giadv;
            model.Updated_at = DateTime.Now;
            _db.GiaDvKcbCt.Update(model);
            _db.SaveChanges();
            string result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

     

        public string GetData(string Mahs)
        {
            //var Model = _db.GiaDvKcbCt.Where(t => t.Mahs == Mahs).ToList();

            var model = _db.GiaDvKcbCt.Where(t => t.Mahs == Mahs).ToList();

            var modeldanhmuc = _db.GiaDvKcbDm.ToList();

            var modeldanhmucnhom = _db.GiaDvKcbNhom.ToList();
            int record = 1;

            var groupmanhom1 = _db.GiaDvKcbCt.Where(item => item.Mahs == Mahs).Select(item => item.Manhom).Distinct().ToList();

            string result = "<div class='card-body' id='frm_data'>";

            foreach (var manhom in groupmanhom1)

            {
                foreach (var dm in modeldanhmucnhom)
                {
                    if (manhom == dm.Manhom)
                    {
                        result += "<p style='text-align:center'>" + dm.Tennhom + "</p>";
                    }
                }

                result += "<table class='table table-striped table-bordered table-hover class-nosort'>";
                result += "<thead>";
                result += "<tr style='text-align:center'>";
                result += "<th>STT</th>";
                result += "<th>STT TT37</th>";
                result += "<td>Mã dịch vụ</td>";
                result += "<th>Tên dịch vụ</th>";
                result += "<th>Đơn giá</th>";
                result += "<th>Ghi chú</th>";
                result += "<th width='8%'>Thao tác</th>";
                result += "</tr>";
                result += "</thead>";

                result += "<tbody>";
                foreach (var item in model.Where(t => t.Manhom == manhom))
                {
                    result += "<tr>";
                    result += "<td style='text-align:center'>" + (record++) + "</td>";
                    result += "<td style='text-align:center'></td>";
                    result += "<td style='text-align:center'>" + item.Madichvu + "</td>";
                    result += "<td style='text-align:left'>" + item.Tenspdv + "</td>";
                    result += "<td style='text-align:right'>" + Helpers.ConvertDbToStr(item.Giadv) + "</td>";
                    result += "<td style='text-align:center'>" + item.Ghichu + "</td>";
                    result += "<td><button type='button' class='btn btn-sm btn-clean btn-icon' title='Chỉnh sửa' data-toggle='modal'";
                    result += "data-target = '#Edit_Modal' onclick = 'SetEdit(`" + item.Id + "`)'>";
                    result += "<i class='icon-lg la la-edit text-warning'></i>";
                    result += "</button></td>";
                    result += "</tr>";
                }

                result += "</ tbody >";

                result += "</ table >";

            }
            result += "</div>";

            return result;
        }

        [HttpPost("DinhGiaDvKcb/GetListHoSo")]
        public JsonResult GetListHoSo(DateTime ngaytu, DateTime ngayden)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                var model = _db.GiaDvKcb.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && list_trangthai.Contains(t.Trangthai));
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
