using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CSDLGia_ASP.Database;
using System.Security.Cryptography;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using CSDLGia_ASP.Models.Systems;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaThueDN
{
    public class GiaThueDNController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GiaThueDNController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("GiaThueMatDatMatNuoc")]
        [HttpGet]
        public IActionResult Index(string Madv, int Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.thongtin", "Index"))
                {
                    List<GiaThueMatDatMatNuoc> model = new List<GiaThueMatDatMatNuoc>();
                    Nam = Nam == 0 ? DateTime.Now.Year : Nam;
                    var dsdonvi = (from db in _db.DsDiaBan.Where(t => t.Level != "H")
                                   join dv in _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI") on db.MaDiaBan equals dv.MaDiaBan
                                   select new VMDsDonVi
                                   {
                                       Id = dv.Id,
                                       TenDiaBan = db.TenDiaBan,
                                       TenDv = dv.TenDv,
                                       MaDv = dv.MaDv,
                                   }).ToList();
                    if (dsdonvi.Count > 0)
                    {
                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") != null)
                        {
                            Madv = Helpers.GetSsAdmin(HttpContext.Session, "Madv");
                            model = _db.GiaThueMatDatMatNuoc.Where(t => t.Madv == Madv && t.Thoidiem.Year == Nam).ToList();
                            ViewData["DsDonVi"] = dsdonvi;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(Madv))
                            {
                                Madv = dsdonvi.OrderBy(t => t.Id).Select(t => t.MaDv).First();
                                model = _db.GiaThueMatDatMatNuoc.Where(t => t.Madv == Madv && t.Thoidiem.Year == Nam).ToList();
                            }
                            else
                            {
                                model = _db.GiaThueMatDatMatNuoc.Where(t => t.Madv == Madv && t.Thoidiem.Year == Nam).ToList();
                            }
                            ViewData["DsDonVi"] = dsdonvi.Where(t => t.MaDv == Madv);
                        }

                        ViewData["Nam"] = Nam;
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                        ViewData["DsDiaBanAll"] = _db.DsDiaBan;
                        ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                        ViewData["Madv"] = Madv;
                        ViewData["NhomTn"] = _db.GiaThueMatDatMatNuocNhom.ToList();
                        ViewData["Title"] = " Thông tin hồ sơ giá thuê mặt đất mặt nước";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgtmdmn";
                        ViewData["MenuLv3"] = "menu_dgtmdmn_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Title"] = "Thông tin hồ sơ giá thuê mặt đất mặt nước";
                        ViewData["Messages"] = "Thông tin hồ sơ giá thuê mặt đất mặt nước.";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgtmdmn";
                        ViewData["MenuLv3"] = "menu_dgtmdmn_tt";
                        return View("Views/Admin/Error/ThongBaoLoi.cshtml");
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

        [Route("GiaThueMatDatMatNuoc/Create")]
        [HttpGet]
        public IActionResult Create(string Madv, string maNhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.thongtin", "Create"))
                {
                    var model_ct_cxd = _db.GiaThueMatDatMatNuocCt.Where(t => t.Trangthai == "CXD" && t.Madv == Madv);
                    if (model_ct_cxd.Any())
                    {
                        _db.GiaThueMatDatMatNuocCt.RemoveRange(model_ct_cxd);
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


                    var model = new GiaThueMatDatMatNuoc
                    {
                        Madv = Madv,
                        Thoidiem = DateTime.Now,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        PhanLoaiHoSo = "HOSOCHITIET",
                    };
                    IEnumerable<GiaThueMatDatMatNuocDm> danhmuc = _db.GiaThueMatDatMatNuocDm;
                    if(maNhom != "all")
                    {
                        danhmuc = danhmuc.Where(t => t.Manhom == maNhom);
                    }    

                    var chitiet = new List<GiaThueMatDatMatNuocCt>();


                    foreach (var item in danhmuc)
                    {
                        chitiet.Add(new GiaThueMatDatMatNuocCt()
                        {
                            Mahs = model.Mahs,
                            LoaiDat = item.Loaidat,
                            SapXep = item.SapXep,
                            HienThi = item.HienThi,
                            MaNhom = item.Manhom,
                            Style = item.Style,
                            Trangthai = "CXD",
                            Madv = Madv,                          
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        });
                    }
                    _db.GiaThueMatDatMatNuocCt.AddRange(chitiet);
                    _db.SaveChanges();

                    model.GiaThueMatDatMatNuocCt = chitiet.Where(t => t.Mahs == model.Mahs).ToList();

                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Title"] = "Thêm mới giá thuê mặt đất mặt nước";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmdmn";
                    ViewData["MenuLv3"] = "menu_dgtmdmn_tt";
                    ViewData["GiaThueDNNhom"] = _db.GiaThueMatDatMatNuocNhom;
                    return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/Create.cshtml", model);
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

        [Route("GiaThueMatDatMatNuoc/NhanExcel")]
        [HttpGet]
        public IActionResult NhanExcel(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.thongtin", "Create"))
                {

                    var model = new VMDinhGiaThueDN
                    {
                        Madv = Madv,
                        Thoidiem = DateTime.Now,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        PhanLoaiHoSo = "HOSONHANEXCEL",
                    };
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Title"] = "Thêm mới giá thuê mặt đất mặt nước";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmdmn";
                    ViewData["MenuLv3"] = "menu_dgtmdmn_tt";

                    ViewData["codeExcel"] = model.CodeExcel;//Gán ra view để dùng chung
                    return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/NhanExcel.cshtml", model);
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

        [Route("GiaThueMatDatMatNuoc/Store")]
        [HttpPost]
        public IActionResult Store(GiaThueMatDatMatNuoc request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.thongtin", "Create"))
                {    
                    var model = new GiaThueMatDatMatNuoc
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Madiaban = request.Madiaban,
                        Soqd = request.Soqd,
                        Thoidiem = request.Thoidiem,
                        Thongtin = request.Thongtin,
                        Ghichu = request.Ghichu,
                        PhanLoaiHoSo = request.PhanLoaiHoSo,
                        CodeExcel = request.CodeExcel,
                        Trangthai = "CHT",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaThueMatDatMatNuoc.Add(model);
                    _db.SaveChanges();

                    // Xử lý phần lịch sử hồ sơ 

                    //var lichSuHoSo = new TrangThaiHoSo
                    //{
                    //    MaHoSo = request.Mahs,
                    //    TenDangNhap = Helpers.GetSsAdmin(HttpContext.Session, "Name"),
                    //    ThongTin = "Thay đổi thông tin hồ sơ",
                    //    ThoiGian = DateTime.Now,
                    //    TrangThai = "CHT",

                    //};
                    //_db.TrangThaiHoSo.Add(lichSuHoSo);
                    //_db.SaveChanges();

                    //Kết thúc Xử lý phần lịch sử hồ sơ 

                    var modelct = _db.GiaThueMatDatMatNuocCt.Where(t => t.Mahs == request.Mahs);
                    foreach(var ct in modelct)
                    {
                        ct.Trangthai = "XD";
                    }
                    var model_file = _db.ThongTinGiayTo.Where(t => t.Mahs == request.Mahs);
                    foreach(var file in model_file)
                    {
                        file.Status = "XD";
                    }
                    _db.GiaThueMatDatMatNuocCt.UpdateRange(modelct);
                    _db.SaveChanges();
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmdmn";
                    ViewData["MenuLv3"] = "menu_dgtmdmn_tt";
                    return RedirectToAction("Index", "GiaThueDN", new { request.Madv });
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

        [Route("GiaThueMatDatMatNuoc/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.thongtin", "Delete"))
                {
                    var model = _db.GiaThueMatDatMatNuoc.FirstOrDefault(t => t.Id == id_delete);
                    if (model != null)
                    {
                        var model_ct = _db.GiaThueMatDatMatNuocCt.Where(t => t.Mahs == model.Mahs);
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
                        if (model_ct.Any()) { _db.GiaThueMatDatMatNuocCt.RemoveRange(model_ct); }

                        _db.GiaThueMatDatMatNuoc.Remove(model);
                        _db.SaveChanges();
                    }
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmdmn";
                    ViewData["MenuLv3"] = "menu_dgtmdmn_tt";
                    return RedirectToAction("Index", "GiaThueDN", new { model.Madv });
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

        [Route("GiaThueMatDatMatNuoc/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.thongtin", "Edit"))
                {
                    var model = _db.GiaThueMatDatMatNuoc.FirstOrDefault(t => t.Mahs == Mahs);
                    model.GiaThueMatDatMatNuocCt = _db.GiaThueMatDatMatNuocCt.Where(t => t.Mahs == Mahs).ToList();
                    model.ThongTinGiayTo = _db.ThongTinGiayTo.Where(t => t.Mahs == Mahs).ToList();
                    ViewData["GiaThueDNNhom"] = _db.GiaThueMatDatMatNuocNhom;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Title"] = "Chỉnh sửa giá thuê mặt dất mặt nước";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmdmn";
                    ViewData["MenuLv3"] = "menu_dgtmdmn_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/Edit.cshtml", model);
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

        [Route("GiaThueMatDatMatNuoc/Update")]
        [HttpPost]
        public IActionResult Update(GiaThueMatDatMatNuoc request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.thongtin", "Edit"))
                {
                    var model = _db.GiaThueMatDatMatNuoc.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Madiaban = request.Madiaban;
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Thongtin = request.Thongtin;
                    model.Ghichu = request.Ghichu;
                    model.Updated_at = DateTime.Now;
                   

                    var modelct = _db.GiaThueMatDatMatNuocCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Mahs = model.Mahs;
                        }
                    }
                    var model_file = _db.ThongTinGiayTo.Where(t => t.Mahs == request.Mahs);
                    foreach(var file in model_file) { file.Status = "XD"; }
                    _db.ThongTinGiayTo.UpdateRange(model_file);
                    _db.GiaThueMatDatMatNuoc.Update(model);
                    _db.GiaThueMatDatMatNuocCt.UpdateRange(modelct);
                    _db.SaveChanges();


                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmdmn";
                    ViewData["MenuLv3"] = "menu_dgtmdmn_tt";
                    return RedirectToAction("Index", "GiaThueDN", new { request.Mahs });
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

        [Route("GiaThueMatDatMatNuoc/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.thongtin", "Edit"))
                {
                    var model = _db.GiaThueMatDatMatNuoc.FirstOrDefault(t => t.Mahs == Mahs);
                    var modelct = _db.GiaThueMatDatMatNuocCt.Where(t => t.Mahs == Mahs);
                    model.GiaThueMatDatMatNuocCt = modelct.ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["Title"] = "Chi tiết giá thuê mặt dất mặt nước";
                    return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/Show.cshtml", model);
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

        [Route("GiaThueMatDatMatNuoc/Search")]
        [HttpGet]
        public IActionResult Search()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.timkiem", "Index"))
                {

                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Title"] = "Tìm kiếm thông tin định giá thuê mặt đất mặt nước";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmdmn";
                    ViewData["MenuLv3"] = "menu_dgtmdmn_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/TimKiem/Index.cshtml");

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

        [Route("GiaThueMatDatMatNuoc/Result")]
        [HttpPost]
        public IActionResult Result(DateTime beginTime, DateTime endTime, double beginPrice, double endPrice, string madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.thuedatnuoc.timkiem", "Edit"))
                {
                    var model = from dgct in _db.GiaThueMatDatMatNuocCt
                                join dg in _db.GiaThueMatDatMatNuoc on dgct.Mahs equals dg.Mahs
                                select new VMDinhGiaThueDNCt
                                {
                                    Id = dg.Id,
                                    Mahs = dg.Mahs,
                                    Madv = dg.Madv,
                                    Vitri = dgct.Vitri,
                                    Dongia1 = dgct.Dongia1,
                                    Macqcq = dg.Macqcq,
                                    Thoidiem = dg.Thoidiem,
                                    Diemdau = dgct.Diemdau,
                                    Diemcuoi = dgct.Diemcuoi,
                                    Dientich = dgct.Dientich,
                                    PhanLoaiDatNuoc = dgct.PhanLoaiDatNuoc,
                                };
                    if (madv != "All")
                    {
                        model = model.Where(t => t.Madv == madv);
                    }

                    if (beginTime.ToString("yyMMdd") != "010101")
                    {
                        model = model.Where(t => t.Thoidiem >= beginTime);
                    }

                    if (endTime.ToString("yyMMdd") != "010101")
                    {
                        model = model.Where(t => t.Thoidiem <= endTime);
                    }
                    model = model.Where(t => t.Dongia1 >= beginPrice);
                    if (endPrice > 0)
                    {
                        model = model.Where(t => t.Dongia1 <= endPrice);
                    }
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["Title"] = " Tìm kiếm thông tin định giá thuê mặt đất mặt nước";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgtmdmn";
                    ViewData["MenuLv3"] = "menu_dgtmdmn_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaThueMatDatMatNuoc/TimKiem/Result.cshtml", model);
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
