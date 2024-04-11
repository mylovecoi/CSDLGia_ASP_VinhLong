using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaTrungThauDat
{
    public class GiaTrungThauDatController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GiaTrungThauDatController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("GiaTrungThauDat")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam = "all", string MaDiaBan = "all")
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.trungthaudat.thongtin", "Index"))
                {
                    var dsdonvi = (from db in _db.DsDiaBan
                                   join dv in _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI") on db.MaDiaBan equals dv.MaDiaBan
                                   select new VMDsDonVi
                                   {
                                       Id = dv.Id,
                                       TenDiaBan = db.TenDiaBan,
                                       MaDiaBan = db.MaDiaBan,
                                       MaDv = dv.MaDv,
                                       TenDv = dv.TenDv,
                                   }).ToList();
                    if (dsdonvi.Count > 0)
                    {
                        Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;
                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") != null)
                        {
                            Madv = Helpers.GetSsAdmin(HttpContext.Session, "Madv");
                        }

                        var model = from dat in _db.GiaDauGiaDat
                                    join diaban in _db.DsDiaBan on dat.Madiaban equals diaban.MaDiaBan
                                    select new GiaDauGiaDat
                                    {
                                        Id = dat.Id,
                                        Tenduan = dat.Tenduan,
                                        Thoidiem = dat.Thoidiem,
                                        Mahs = dat.Mahs,
                                        Trangthai = dat.Trangthai,
                                        Madiaban = dat.Madiaban,
                                        TenDiaBan = diaban.TenDiaBan,
                                        Macqcq = dat.Macqcq,
                                        Madv = dat.Madv,
                                    };


                        if (Madv != "all")
                        {
                            model = model.Where(t => t.Madv == Madv);
                        }
                        if (Nam != "all")
                        {
                            model = model.Where(x => x.Thoidiem.Year == Convert.ToInt32(Nam));
                        }
                        if (MaDiaBan != "all")
                        {
                            model = model.Where(x => x.Madiaban == MaDiaBan);
                        }
                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                        {
                            ViewData["DsDonVi"] = dsdonvi;
                        }
                        else
                        {
                            ViewData["DsDonVi"] = dsdonvi.Where(t => t.MaDv == Madv);
                        }
                        var dsDonViTH = (from donvi in _db.DsDonVi
                                         join tk in _db.Users on donvi.MaDv equals tk.Madv
                                         join gr in _db.GroupPermissions.Where(x => x.ChucNang == "TONGHOP") on tk.Chucnang equals gr.KeyLink
                                         select new CSDLGia_ASP.Models.Systems.DsDonVi
                                         {
                                             MaDiaBan = donvi.MaDiaBan,
                                             MaDv = donvi.MaDv,
                                             TenDv = donvi.TenDv,
                                         });
                        ViewData["DsDiaBan"] = _db.DsDiaBan;
                        ViewData["DsXaPhuong"] = _db.DsXaPhuong;
                        ViewData["DsDonViTh"] = dsDonViTH;
                        ViewData["Madv"] = Madv;
                        ViewData["Nam"] = Nam;
                        ViewData["MaDiaBan"] = MaDiaBan;
                        //ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level == "H");
                        ViewData["DsCqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                        ViewData["ADMIN"] = _db.DsDiaBan.Where(t => t.Level == "ADMIN");
                        ViewData["Title"] = " Thông tin hồ sơ giá trúng thầu quyền sử dụng đất";
                        ViewData["MenuLv1"] = "menu_giadat";
                        ViewData["MenuLv2"] = "menu_dgd";
                        ViewData["MenuLv3"] = "menu_giadgd_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaTrungThauDat/DanhSach/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Messages"] = "Thông tin hồ sơ giá trúng thầu quyền sd đất.";
                        ViewData["Title"] = " Thông tin hồ sơ giá trúng thầu quyền sd đất";
                        ViewData["MenuLv1"] = "menu_giadat";
                        ViewData["MenuLv2"] = "menu_dgd";
                        ViewData["MenuLv3"] = "menu_giadgd_tt";
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


        [Route("GiaTrungThauDat/Create")]
        [HttpGet]
        public IActionResult Create(string Madv, string MaDiaBan, string MaXaPhuong)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.trungthaudat.thongtin", "Create"))
                {
                    this.RemoveData_Ct_CXD(Madv);
                    var model = new GiaDauGiaDat
                    {
                        Madv = Madv,
                        Thoidiem = DateTime.Now,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Madiaban = MaDiaBan,
                        Maxp = MaXaPhuong,
                    };

                    //var dsdv = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI" && t.MaDv == Madv).OrderBy(t => t.Id).Select(t => t.MaDv).First();
                    //ViewData["Huyens"] = _db.Districts;
                    //ViewData["Xas"] = new List<Towns>();
                    var XaPhuong = _db.DsXaPhuong.FirstOrDefault(x => x.Maxp == MaXaPhuong);
                    var DsXaPhuong = _db.DsXaPhuong.Where(x => x.Madiaban == MaDiaBan);
                    ViewData["TenDiaBan"] = _db.DsDiaBan.FirstOrDefault(x => x.MaDiaBan == MaDiaBan).TenDiaBan;
                    ViewData["TenXaPhuong"] = XaPhuong != null ? XaPhuong.Tenxp : "Tất cả";
                    ViewData["DmDvt"] = _db.DmDvt.ToList();
                    ViewData["TenDonVi"] = _db.DsDonVi.First(x => x.MaDv == Madv).TenDv;
                    ViewData["Xas"] = _db.DsXaPhuong.Where(x => x.Madiaban == MaDiaBan);
                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["DsDiaBan"] = _db.DsDiaBan;
                    ViewData["DsXaPhuong"] = DsXaPhuong.Any() ? DsXaPhuong : _db.DsXaPhuong;
                    ViewData["Title"] = " Thông tin hồ sơ giá trúng thầu quyền sử dụng đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgd";
                    ViewData["MenuLv3"] = "menu_giadgd_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauDat/DanhSach/Create.cshtml", model);
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


        [Route("GiaTrungThauDat/Store")]
        [HttpPost]
        public IActionResult Store(GiaDauGiaDat request)
        {

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.trungthaudat.thongtin", "Create"))
                {
                    var model = new GiaDauGiaDat
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Maxp = request.Maxp,
                        Thoidiem = request.Thoidiem,
                        Tenduan = request.Tenduan,
                        Soqdpagia = request.Soqdpagia,
                        Soqddaugia = request.Soqddaugia,
                        Soqdgiakhoidiem = request.Soqdgiakhoidiem,
                        Soqdkqdaugia = request.Soqdkqdaugia,
                        Thongtin = request.Thongtin,
                        Phanloai = request.Phanloai,
                        Trangthai = "CHT",
                        Congbo = "CHUACONGBO",
                        Ipf1 = request.Ipf1,
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                        Madiaban = request.Madiaban,
                        //MaHuyen=request.MaHuyen,
                    };
                    _db.GiaDauGiaDat.Add(model);
                    this.SaveData_Ct_CXD(model.Mahs);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "GiaTrungThauDat", new { Madb = request.Madiaban });
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


        [Route("GiaTrungThauDat/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.trungthaudat.thongtin", "Edit"))
                {
                    var model = _db.GiaDauGiaDat.FirstOrDefault(t => t.Mahs == Mahs);

                    var model_new = new GiaDauGiaDat
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        //Madiaban = model.Madiaban,
                        Thoidiem = model.Thoidiem,
                        Thongtin = model.Thongtin,
                        Tenduan = model.Tenduan,
                        Soqddaugia = model.Soqddaugia,
                        Soqdgiakhoidiem = model.Soqdgiakhoidiem,
                        Soqdkqdaugia = model.Soqdkqdaugia,
                        Soqdpagia = model.Soqdpagia,
                        Phanloai = model.Phanloai,
                        Madiaban = model.Madiaban,
                        Maxp = model.Maxp,
                        //MaHuyen = model.MaHuyen,
                        //Maxp=model.Maxp,                        
                    };
                    var model_ct = _db.GiaDauGiaDatCt.Where(t => t.Mahs == model_new.Mahs);
                    model_new.GiaDauGiaDatCt = model_ct.ToList();
                    var giayto = _db.ThongTinGiayTo.Where(x => x.Mahs == model.Mahs);
                    model_new.ThongTinGiayTo = giayto.ToList();
                    var XaPhuong = _db.DsXaPhuong.FirstOrDefault(x => x.Maxp == model.Maxp);
                    var DsXaPhuong = _db.DsXaPhuong.Where(x => x.Madiaban == model.Madiaban);
                    ViewData["TenDiaBan"] = _db.DsDiaBan.FirstOrDefault(x => x.MaDiaBan == model.Madiaban).TenDiaBan;
                    ViewData["TenXaPhuong"] = XaPhuong != null ? XaPhuong.Tenxp : "Tất cả";
                    ViewData["DsDiaBan"] = _db.DsDiaBan;
                    ViewData["DsXaPhuong"] = DsXaPhuong.Any() ? DsXaPhuong : _db.DsXaPhuong;

                    ViewData["DmDvt"] = _db.DmDvt.ToList();
                    ViewData["Xas"] = _db.DsXaPhuong.Where(x => x.Madiaban == model.Madiaban);
                    ViewData["Maxp"] = model.Maxp;
                    ViewData["Madb"] = model.Madiaban;
                    ViewData["Madv"] = model.Madv;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI").ToList();
                    ViewData["Title"] = " Thông tin hồ sơ giá Trúng thầu quyền sử dụng đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgd";
                    ViewData["MenuLv3"] = "menu_giadgd_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauDat/DanhSach/Edit.cshtml", model_new);
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

        [Route("GiaTrungThauDat/Update")]
        [HttpPost]
        public IActionResult Update(GiaDauGiaDat request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.trungthaudat.thongtin", "Edit"))
                {
                    var model = _db.GiaDauGiaDat.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Tenduan = request.Tenduan;
                    model.Thoidiem = request.Thoidiem;
                    model.Thongtin = request.Thongtin;
                    model.Maxp = request.Maxp;
                    model.Soqdpagia = request.Soqdpagia;
                    model.Soqddaugia = request.Soqddaugia;
                    model.Soqdkqdaugia = request.Soqdkqdaugia;
                    model.Soqdgiakhoidiem = request.Soqdgiakhoidiem;
                    model.Phanloai = request.Phanloai;
                    model.Ipf1 = request.Ipf1;
                    model.Updated_at = DateTime.Now;
                    //model.MaHuyen = request.MaHuyen;                    
                    _db.GiaDauGiaDat.Update(model);
                    this.SaveData_Ct_CXD(model.Mahs);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "GiaTrungThauDat", new { Madb = request.Madiaban });
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
        [Route("GiaTrungThauDat/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.trungthaudat.thongtin", "Delete"))
                {
                    var model = _db.GiaDauGiaDat.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaDauGiaDat.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.GiaDauGiaDatCt.Where(t => t.Mahs == model.Mahs);
                    if (model_ct.Any())
                    {
                        _db.GiaDauGiaDatCt.RemoveRange(model_ct);
                    }
                    // xóa thông tin giấy tờ 
                    var model_file_remove = _db.ThongTinGiayTo.Where(t => t.Mahs == model.Mahs);
                    if (model_file_remove.Any())
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        foreach (var file in model_file_remove)
                        {
                            string path_del = Path.Combine(wwwRootPath + "/UpLoad/File/ThongTinGiayTo/", file.FileName);
                            FileInfo fi = new FileInfo(path_del);
                            if (fi != null)
                            {
                                System.IO.File.Delete(path_del);
                                fi.Delete();
                            }
                        }
                        _db.ThongTinGiayTo.RemoveRange(model_file_remove);
                    }
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaTrungThauDat", new { Madb = model.Madiaban });
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

        [Route("GiaTrungThauDat/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.trungthaudat.thongtin", "Show"))
                {
                    var model = _db.GiaDauGiaDat.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new GiaDauGiaDat
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        Madiaban = model.Madiaban,
                        Thoidiem = model.Thoidiem,
                        Thongtin = model.Thongtin,
                        Tenduan = model.Tenduan,
                        Soqddaugia = model.Soqddaugia,
                        Soqdgiakhoidiem = model.Soqdgiakhoidiem,
                        Soqdkqdaugia = model.Soqdkqdaugia,
                        Soqdpagia = model.Soqdpagia,
                        Phanloai = model.Phanloai,
                        Maxp = model.Maxp,

                    };
                    var model_ct = _db.GiaDauGiaDatCt.Where(t => t.Mahs == Mahs);
                    model_new.GiaDauGiaDatCt = model_ct.ToList();
                    var donvi = _db.DsDonVi.First(t => t.MaDv == model.Madv);
                    ViewData["DsDiaBan"]=_db.DsDiaBan;                    
                    ViewData["DsXaPhuong"] = _db.DsXaPhuong;
                    ViewData["TenDiaBan"] = _db.DsDiaBan.First(x => x.MaDiaBan == model.Madiaban).TenDiaBan;
                    //ViewData["TenXa"] = _db.Towns.First(x => x.Mahuyen == model.MaHuyen && x.Maxa == model.Maxp).Tenxa;
                    ViewData["TenDonVi"] = donvi.TenDv;
                    //ViewData["XaPhuong"] = _db.DsXaPhuong.FirstOrDefault(x => x.Maxp == model.Maxp).Tenxp;
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauDat/DanhSach/Show.cshtml", model_new);
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
        [Route("GiaTrungThauDat/Search")]
        [HttpGet]
        public IActionResult Search(DateTime beginTime, DateTime endTime,
            double Giakhoidiem_den, double Giakhoidiem_tu, double Giadaugia_tu, double Giadaugia_den, string ten, string PhanLoai = "all", string madv = "all", string MaDiaBan = "all", string MaXa = "all")
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.daugiadat.timkiem", "Index"))
                {
                    beginTime = beginTime == DateTime.MinValue ? new DateTime(DateTime.Now.Year, 01, 01) : beginTime;
                    endTime = endTime == DateTime.MinValue ? new DateTime(DateTime.Now.Year, 12, 31) : endTime;
                    var model_join = from dgct in _db.GiaDauGiaDatCt
                                     join dg in _db.GiaDauGiaDat on dgct.Mahs equals dg.Mahs
                                     select new GiaDauGiaDatCt
                                     {
                                         Id = dgct.Id,
                                         Mahs = dgct.Mahs,
                                         Mota = dgct.Mota,
                                         Solo = dgct.Solo,
                                         Sothua = dgct.Sothua,
                                         Tobanbo = dgct.Tobanbo,
                                         Dientich = dgct.Dientich,
                                         Dvt = dgct.Dvt,
                                         Giakhoidiem = dgct.Giakhoidiem,
                                         Giadaugia = dgct.Giadaugia,
                                         ThoiDiem = dg.Thoidiem,
                                         SoQuyetDinh = dg.Soqddaugia,
                                         PhanLoai = dg.Phanloai,
                                         TrangThai = dg.Trangthai,
                                         TenDuAn = dg.Tenduan,
                                         MaDv = dg.Madv,
                                         MaDiaBan = dg.Madiaban,
                                         Maxp = dgct.Maxp,
                                     };
                    model_join = model_join.Where(x => x.ThoiDiem >= beginTime && x.ThoiDiem <= endTime && x.TrangThai == "HT");
                    if (!string.IsNullOrEmpty(ten))
                    {
                        model_join = model_join.Where(t => t.TenDuAn.Contains(ten));
                    }
                    if (madv != "all")
                    {
                        model_join = model_join.Where(t => t.MaDv == madv);
                    }
                    if (MaDiaBan != "all")
                    {
                        model_join = model_join.Where(t => t.MaDiaBan == MaDiaBan);
                        if (MaXa != "all")
                        {
                            model_join = model_join.Where(x => x.Maxp == MaXa);
                        }
                    }
                    if (Giakhoidiem_tu != 0)
                    {
                        model_join = model_join.Where(t => t.Giakhoidiem >= Giakhoidiem_tu);
                    }
                    if (Giakhoidiem_den != 0)
                    {
                        model_join = model_join.Where(t => t.Giakhoidiem <= Giakhoidiem_den);
                    }
                    if (Giadaugia_tu != 0)
                    {
                        model_join = model_join.Where(t => t.Giadaugia >= Giadaugia_tu);
                    }
                    if (Giadaugia_den != 0)
                    {
                        model_join = model_join.Where(t => t.Giadaugia <= Giadaugia_den);
                    }
                    if (PhanLoai != "all")
                    {
                        model_join = model_join.Where(t => t.PhanLoai == PhanLoai);
                    }
                    var DsXaPhuong = _db.DsXaPhuong.Where(x => x.Madiaban == MaDiaBan);
                    ViewData["DsDiaBan"] = _db.DsDiaBan;
                   ViewData["DsXaPhuong"] = DsXaPhuong.Any()? DsXaPhuong : _db.DsXaPhuong;
                    ViewData["MaDiaBan"] = MaDiaBan;
                    ViewData["MaXa"] = MaXa;




                    ViewData["Phanloai"] = PhanLoai;
                    ViewData["beginTime"] = beginTime;
                    ViewData["endTime"] = endTime;
                    ViewData["Giakhoidiem_den"] = Giakhoidiem_den;
                    ViewData["Giakhoidiem_tu"] = Giakhoidiem_tu;
                    ViewData["Giadaugia_tu"] = Giadaugia_tu;
                    ViewData["Giadaugia_den"] = Giadaugia_den;
                    ViewData["ten"] = ten;
                    ViewData["maDv"] = madv;
                    ViewData["MaDiaBan"] = MaDiaBan;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level == "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Title"] = " Thông tin hồ sơ giá trúng thầu quyền sd đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgd";
                    ViewData["MenuLv3"] = "menu_giadgd_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauDat/TimKiem/Index.cshtml", model_join);
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
        [Route("GiaTrungThauDat/Search")]
        [HttpPost]
        public IActionResult PrintSearch(DateTime beginTime, DateTime endTime,
            double Giakhoidiem_den, double Giakhoidiem_tu, double Giadaugia_tu, double Giadaugia_den, string ten, string PhanLoai = "all", string madv = "all", string MaDiaBan = "all",string MaXa = "all")
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.daugiadat.timkiem", "Index"))
                {
                    beginTime = beginTime == DateTime.MinValue ? new DateTime(DateTime.Now.Year, 01, 01) : beginTime;
                    endTime = endTime == DateTime.MinValue ? new DateTime(DateTime.Now.Year, 12, 31) : endTime;
                    var model_join = from dgct in _db.GiaDauGiaDatCt
                                     join dg in _db.GiaDauGiaDat on dgct.Mahs equals dg.Mahs
                                     select new GiaDauGiaDatCt
                                     {
                                         Id = dgct.Id,
                                         Mahs = dgct.Mahs,
                                         Mota = dgct.Mota,
                                         Solo = dgct.Solo,
                                         Sothua = dgct.Sothua,
                                         Tobanbo = dgct.Tobanbo,
                                         Dientich = dgct.Dientich,
                                         Dvt = dgct.Dvt,
                                         Giakhoidiem = dgct.Giakhoidiem,
                                         Giadaugia = dgct.Giadaugia,
                                         ThoiDiem = dg.Thoidiem,
                                         SoQuyetDinh = dg.Soqddaugia,
                                         PhanLoai = dg.Phanloai,
                                         TrangThai = dg.Trangthai,
                                         TenDuAn = dg.Tenduan,
                                         MaDv = dg.Madv,
                                         MaDiaBan = dg.Madiaban,
                                         Maxp = dgct.Maxp,
                                     };
                    model_join = model_join.Where(x => x.ThoiDiem >= beginTime && x.ThoiDiem <= endTime && x.TrangThai == "HT");
                    if (!string.IsNullOrEmpty(ten))
                    {
                        model_join = model_join.Where(t => t.TenDuAn.Contains(ten));
                    }
                    if (madv != "all")
                    {
                        model_join = model_join.Where(t => t.MaDv == madv);
                    }
                    if (MaDiaBan != "all")
                    {
                        model_join = model_join.Where(t => t.MaDiaBan == MaDiaBan);
                        if (MaXa != "all")
                        {
                            model_join = model_join.Where(x=>x.Maxp == MaXa);
                        }
                    }
                    if (Giakhoidiem_tu != 0)
                    {
                        model_join = model_join.Where(t => t.Giakhoidiem >= Giakhoidiem_tu);
                    }
                    if (Giakhoidiem_den != 0)
                    {
                        model_join = model_join.Where(t => t.Giakhoidiem <= Giakhoidiem_den);
                    }
                    if (Giadaugia_tu != 0)
                    {
                        model_join = model_join.Where(t => t.Giadaugia >= Giadaugia_tu);
                    }
                    if (Giadaugia_den != 0)
                    {
                        model_join = model_join.Where(t => t.Giadaugia <= Giadaugia_den);
                    }
                    if (PhanLoai != "all")
                    {
                        model_join = model_join.Where(t => t.PhanLoai == PhanLoai);
                    }
                    ViewData["Phanloai"] = PhanLoai;
                    ViewData["beginTime"] = beginTime;
                    ViewData["endTime"] = endTime;
                    ViewData["Giakhoidiem_den"] = Giakhoidiem_den;
                    ViewData["Giakhoidiem_tu"] = Giakhoidiem_tu;
                    ViewData["Giadaugia_tu"] = Giadaugia_tu;
                    ViewData["Giadaugia_den"] = Giadaugia_den;
                    ViewData["ten"] = ten;
                    ViewData["maDv"] = madv;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level == "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Title"] = " Thông tin hồ sơ giá trúng thầu quyền sd đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_dgd";
                    ViewData["MenuLv3"] = "menu_giadgd_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauDat/TimKiem/PrintSearch.cshtml", model_join);
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

        private void RemoveData_Ct_CXD(string maDv)
        {
            // xóa giá giá trúng thầu đất chi tiết chưa xác định
            var check = _db.GiaDauGiaDatCt.Where(t => t.MaDv == maDv && t.TrangThai == "CXD");
            if (check.Any())
            {
                _db.GiaDauGiaDatCt.RemoveRange(check);
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
            // Lưu lại giá trúng thầu đất chi tiết chưa xác định
            var modelCt_cxd = _db.GiaDauGiaDatCt.Where(x => x.Mahs == maHs && x.TrangThai == "CXD").ToList();
            if (modelCt_cxd.Any())
            {
                modelCt_cxd.ForEach(x => x.TrangThai = "XD");
            }
            // Lưu lại giấy tờ chưa xác định
            var giayto_cxd = _db.ThongTinGiayTo.Where(x => x.Mahs == maHs && x.Status == "CXD").ToList();
            if (giayto_cxd.Any())
            {
                giayto_cxd.ForEach(x => x.Status = "XD");
            }
            _db.SaveChanges();
        }

        //[Route("GiaTrungThauDat/Search")]
        //[HttpGet]
        //public IActionResult Search(DateTime beginTime, DateTime endTime,
        //    double Giakhoidiem_den, double Giakhoidiem_tu, double Giadaugia_tu, double Giadaugia_den, string ten, string maDv = "all")
        //{
        //    if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
        //    {
        //        if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.daugiadat.timkiem", "Index"))
        //        {
        //            beginTime = beginTime == DateTime.MinValue ? new DateTime(DateTime.Now.Year, 01, 01) : beginTime;
        //            endTime = endTime == DateTime.MinValue ? new DateTime(DateTime.Now.Year, 12, 31) : endTime;
        //            var model_join = from dgct in _db.GiaDauGiaDatCt
        //                             join dg in _db.GiaDauGiaDat on dgct.Mahs equals dg.Mahs
        //                             select new GiaDauGiaDat
        //                             {
        //                                 Id = dgct.Id,
        //                                 Mahs = dgct.Mahs,
        //                                 Madv = dgct.MaDv,
        //                                 Tenduan = dg.Tenduan,
        //                                 Giakhoidiem = dgct.Giakhoidiem,
        //                                 Giadaugia = dgct.Giadaugia,
        //                                 Thoidiem = dg.Thoidiem,
        //                                 Trangthai = dg.Trangthai,

        //                             };
        //            model_join = model_join.Where(x => x.Thoidiem >= beginTime && x.Thoidiem <= endTime && x.Trangthai == "HT");
        //            if (!string.IsNullOrEmpty(ten))
        //            {
        //                model_join = model_join.Where(t => t.Tenduan.Contains(ten));
        //            }
        //            if (maDv != "all")
        //            {
        //                model_join = model_join.Where(t => t.Madv == maDv);
        //            }
        //            if (Giakhoidiem_tu != 0)
        //            {
        //                model_join = model_join.Where(t => t.Giakhoidiem >= Giakhoidiem_tu);
        //            }
        //            if (Giakhoidiem_den != 0)
        //            {
        //                model_join = model_join.Where(t => t.Giakhoidiem <= Giakhoidiem_den);
        //            }
        //            if (Giadaugia_tu != 0)
        //            {
        //                model_join = model_join.Where(t => t.Giadaugia >= Giadaugia_tu);
        //            }
        //            if (Giadaugia_den != 0)
        //            {
        //                model_join = model_join.Where(t => t.Giadaugia <= Giadaugia_den);
        //            }

        //            ViewData["beginTime"] = beginTime;
        //            ViewData["endTime"] = endTime;
        //            ViewData["Giakhoidiem_den"] = Giakhoidiem_den;
        //            ViewData["Giakhoidiem_tu"] = Giakhoidiem_tu;
        //            ViewData["Giadaugia_tu"] = Giadaugia_tu;
        //            ViewData["Giadaugia_den"] = Giadaugia_den;
        //            ViewData["ten"] = ten;
        //            ViewData["maDv"] = maDv;

        //            ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level == "H");
        //            ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
        //            ViewData["Title"] = " Thông tin hồ sơ giá trúng thầu quyền sd đất";
        //            ViewData["MenuLv1"] = "menu_giadat";
        //            ViewData["MenuLv2"] = "menu_dgd";
        //            ViewData["MenuLv3"] = "menu_giadgd_tk";
        //            return View("Views/Admin/Manages/DinhGia/GiaTrungThauDat/TimKiem/Index.cshtml", model_join);
        //        }
        //        else
        //        {
        //            ViewData["Messages"] = "Bạn không có quyền truy cập vào chức năng này!";
        //            return View("Views/Admin/Error/Page.cshtml");
        //        }
        //    }
        //    else
        //    {
        //        return View("Views/Admin/Error/SessionOut.cshtml");
        //    }
        //}
    }
}
