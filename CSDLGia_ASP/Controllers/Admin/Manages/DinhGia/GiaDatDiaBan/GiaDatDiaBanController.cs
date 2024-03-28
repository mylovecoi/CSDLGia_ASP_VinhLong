using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDatDiaBan
{
    public class GiaDatDiaBanController : Controller
    {

        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GiaDatDiaBanController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("GiaDatDiaBan")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giadatdb.thongtin", "Index") || Helpers.GetSsAdmin(HttpContext.Session, "Level") == "T")
                {
                    //Check Madv lần đầu tiên nếu không có thì lấy Madv đầu tiên trong bảng DsDonVi
                    var dsdonvi = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    var dsdiaban = _db.DsDiaBan.Where(t => t.Level != "H");

                    if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") != null)
                    {
                        Madv = Helpers.GetSsAdmin(HttpContext.Session, "Madv");

                    }
                    else
                    {
                        if (string.IsNullOrEmpty(Madv))
                        {
                            Madv = dsdonvi.OrderBy(t => t.Id).Select(t => t.MaDv).First();
                        }
                    }

                    var model = _db.GiaDatDiaBan.Where(t => t.Madv == Madv).ToList();


                    //Check nam
                    if (string.IsNullOrEmpty(Nam))
                    {
                        model = model.ToList();
                    }
                    else
                    {
                        if (Nam != "all")
                        {
                            model = model.Where(t => t.Thoidiem.Year == int.Parse(Nam)).ToList();
                        }
                        else
                        {
                            model = model.ToList();
                        }
                    }

                    // Lấy tên đơn vị từ Madv
                    var getdonvi = (from dv in dsdonvi.Where(t => t.MaDv == Madv)
                                    join db in dsdiaban on dv.MaDiaBan equals db.MaDiaBan
                                    select new VMDsDonVi
                                    {
                                        Id = dv.Id,
                                        MaDiaBan = dv.MaDiaBan,
                                        MaDv = dv.MaDv,
                                        TenDv = dv.TenDv,
                                        ChucNang = dv.ChucNang,
                                        Level = db.Level,
                                    }).First();

                    if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                    {
                        ViewData["DsDonVi"] = dsdonvi;
                    }
                    else
                    {
                        ViewData["DsDonVi"] = dsdonvi.Where(t => t.MaDv == Madv);
                    }
                    ViewData["Dsdonvi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["DsDiaBanHuyen"] = _db.DsDiaBan.Where(t => t.Level == "H");
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["DsDv"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Soqd"] = _db.GiaDatDiaBanTt.ToList();
                    ViewData["Nam"] = Nam;
                    ViewData["Madv"] = Madv;
                    ViewData["Title"] = " Thông tin hồ sơ";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_giadatdiaban";
                    ViewData["MenuLv3"] = "menu_giadatdiaban_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaDatDiaBan/Index.cshtml", model);


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

        [Route("GiaDatDiaBan/Create")]
        [HttpGet]
        public IActionResult Create(string soqd, string madiaban, string madv)
        {

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giadatdb.thongtin", "Create"))
                {
                    //xóa giá đất địa bàn chi tiết chưa xác định
                    var giadatdiabanchitiet = _db.GiaDatDiaBanCt.Where(x => x.MaDv == madv && x.Trangthai == "CXD");
                    if (giadatdiabanchitiet.Any())
                    {
                        _db.GiaDatDiaBanCt.RemoveRange(giadatdiabanchitiet);
                    }
                    // xóa giấy tờ đính kèm chưa xác định                    
                    var model_file_cxd = _db.ThongTinGiayTo.Where(x => x.Madv == madv && x.Status == "CXD");
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
                    var model = _db.GiaDatDiaBan.Where(t => t.Soqd == soqd && t.Madiaban == madiaban).FirstOrDefault();

                    if (model == null)
                    {
                        var ndqd = _db.GiaDatDiaBanTt.Where(t => t.Soqd == soqd).FirstOrDefault();
                        var m_qd = new CSDLGia_ASP.Models.Manages.DinhGia.GiaDatDiaBan
                        {
                            SoQDTT = ndqd.Soqd,
                            NoiDungQDTT = ndqd.Mota,
                            Soqd = soqd,
                            Madiaban = madiaban,
                            Madv = madv,
                            Mahs = madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                            Thoidiem = DateTime.Now,

                        };
                        ViewData["Khuvuc"] = _db.GiaDatDiaBanCt.ToList();
                        ViewData["DsDonVi"] = _db.DsDonVi;

                        ViewData["MaDv"] = madv;
                        ViewData["Dsloaidat"] = _db.DmLoaiDat.ToList();
                        ViewData["DsXaPhuong"] = _db.DsXaPhuong.ToList();
                        ViewData["DsDiaBanHuyen"] = _db.DsDiaBan.Where(t => t.Level == "H");
                        ViewData["Title"] = "Thông tin hồ sơ giá đất theo địa bàn";
                        ViewData["MenuLv1"] = "menu_giadat";
                        ViewData["MenuLv2"] = "menu_giadatdiaban";
                        ViewData["MenuLv3"] = "menu_giadatdiaban_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaDatDiaBan/Create.cshtml", m_qd);

                    }
                    else if (model != null && model.Trangthai != "HT")

                    {
                        ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                        var model_ct = _db.GiaDatDiaBanCt.Where(t => t.Mahs == model.Mahs).ToList();
                        model.GiaDatDiaBanCt = model_ct;
                        ViewData["Title"] = "Thông tin hồ sơ giá đất theo địa bàn";
                        ViewData["MenuLv1"] = "menu_giadat";
                        ViewData["MenuLv2"] = "menu_giadatdiaban";
                        ViewData["MenuLv3"] = "menu_giadatdiaban_tt";
                        ViewData["Dsloaidat"] = _db.DmLoaiDat.ToList();
                        ViewData["DsXaPhuong"] = _db.DsXaPhuong.ToList();
                        ViewData["DsDiaBanHuyen"] = _db.DsDiaBan.Where(t => t.Level == "H");
                        return View("Views/Admin/Manages/DinhGia/GiaDatDiaBan/Edit.cshtml", model);
                    }
                    else
                    {
                        ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                        var model_ct = _db.GiaDatDiaBanCt.Where(t => t.Mahs == model.Mahs).ToList();
                        model.GiaDatDiaBanCt = model_ct;
                        ViewData["Title"] = "Thông tin hồ sơ giá đất";
                        ViewData["MenuLv1"] = "menu_giadat";
                        ViewData["MenuLv2"] = "menu_giadatdiaban";
                        ViewData["MenuLv3"] = "menu_giadatdiaban_tt";
                        ViewData["Dsloaidat"] = _db.DmLoaiDat.ToList();
                        ViewData["DsXaPhuong"] = _db.DsXaPhuong.ToList();
                        ViewData["DsDiaBanHuyen"] = _db.DsDiaBan.Where(t => t.Level == "H");
                        return View("Views/Admin/Manages/DinhGia/GiaDatDiaBan/Show.cshtml", model);
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


        [Route("GiaDatDiaBan/Store")]
        [HttpPost]
        public async Task<IActionResult> Store(CSDLGia_ASP.Models.Manages.DinhGia.GiaDatDiaBan request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giadatdb.thongtin", "Create"))
                {
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaDatDiaBan
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Soqd = request.SoQDTT,
                        Madiaban = request.Madiaban,
                        Thoidiem = request.Thoidiem,
                        Ipf1 = request.Ipf1,

                        Trangthai = "CHT",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaDatDiaBan.Add(model);
                    // Lưu lại giá đất địa bàn chưa xác định
                    var modelct = _db.GiaDatDiaBanCt.Where(t => t.Mahs == request.Mahs && t.Trangthai =="CXD");
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Trangthai = "XD";
                            item.Khuvuc = request.Khuvuc;
                        }
                    }
                    // Lưu lại thông tin giấy tờ chưa xác định
                    var giayto = _db.ThongTinGiayTo.Where(t => t.Mahs == request.Mahs).ToList();
                    if (giayto.Any())
                    {
                        giayto.ForEach(x => x.Status = "XD");
                    }                   

                    //var model_ct2 = _db.GiaDatDiaBanCt.Where(t => t.Trangthai == "CXD");
                    //_db.GiaDatDiaBanCt.UpdateRange(modelct);
                  await  _db.SaveChangesAsync();

                    return RedirectToAction("Index", "GiaDatDiaBan", new { request.Madv });
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


        [Route("GiaDatDiaBan/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giadatdb.thongtin", "Delete"))
                {
                    var model = _db.GiaDatDiaBan.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaDatDiaBan.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.GiaDatDiaBanCt.Where(t => t.Mahs == model.Mahs);
                    if (model_ct.Any())
                    {
                        _db.GiaDatDiaBanCt.RemoveRange(model_ct);
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
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaDatDiaBan", new { model.Madiaban });
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

        [Route("GiaDatDiaBan/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giadatdb.thongtin", "Edit"))
                {
                    var model = _db.GiaDatDiaBan.FirstOrDefault(t => t.Mahs == Mahs);
                    //xóa giá đất địa bàn chi tiết chưa xác định
                    var giadatdiabanchitiet = _db.GiaDatDiaBanCt.Where(x => x.MaDv == model.Madv && x.Trangthai == "CXD");
                    if (giadatdiabanchitiet.Any())
                    {
                        _db.GiaDatDiaBanCt.RemoveRange(giadatdiabanchitiet);
                    }
                    // xóa giấy tờ đính kèm chưa xác định                    
                    var model_file_cxd = _db.ThongTinGiayTo.Where(x => x.Madv == model.Madv && x.Status == "CXD");
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
                    var model_ct = _db.GiaDatDiaBanCt.Where(t => t.Mahs == model.Mahs);
                    model.GiaDatDiaBanCt = model_ct.ToList();
                    var thongtingiayto = _db.ThongTinGiayTo.Where(x=>x.Mahs == Mahs);
                    model.ThongTinGiayTo = thongtingiayto.ToList();
                    ViewData["Khuvuc"] = _db.GiaDatDiaBanCt.ToList();
                    ViewData["DsXaPhuong"] = _db.DsXaPhuong.ToList();
                    ViewData["Dsloaidat"] = _db.DmLoaiDat.ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DsDiaBanHuyen"] = _db.DsDiaBan.Where(t => t.Level == "H");
                    ViewData["Madv"] = model.Madv;
                    ViewData["Title"] = "Bảng giá đất theo địa bàn";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_giadatdiaban";
                    ViewData["MenuLv3"] = "menu_giadatdiaban_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaDatDiaBan/Edit.cshtml", model);

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


        [Route("GiaDatDiaBan/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giadatdb.thongtin", "Edit"))
                {
                    var model = _db.GiaDatDiaBan.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_ct = _db.GiaDatDiaBanCt.Where(t => t.Mahs == model.Mahs);
                    model.GiaDatDiaBanCt = model_ct.ToList();

                    ViewData["DsXaPhuong"] = _db.DsXaPhuong.ToList();
                    ViewData["Dsloaidat"] = _db.DmLoaiDat.ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["DsDiaBanHuyen"] = _db.DsDiaBan.Where(t => t.Level == "H");
                    ViewData["Madv"] = model.Madv;
                    ViewData["Title"] = "Bảng giá các loại đất";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_giadatdiaban";
                    ViewData["MenuLv3"] = "menu_giadatdiaban_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaDatDiaBan/Show.cshtml", model);

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

        [Route("GiaDatDiaBan/Update")]
        [HttpPost]
        public IActionResult Update(CSDLGia_ASP.Models.Manages.DinhGia.GiaDatDiaBan request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giadatdb.thongtin", "Edit"))
                {

                    var model = _db.GiaDatDiaBan.FirstOrDefault(t => t.Mahs == request.Mahs);

                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Madv = request.Madv;
                    model.Updated_at = DateTime.Now;


                    _db.GiaDatDiaBan.Update(model);
                    // Lưu lại giá đất địa bàn chưa xác định
                    var modelct = _db.GiaDatDiaBanCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Trangthai = "XD";
                            item.Khuvuc = request.Khuvuc;
                        }
                    }
                    // Lưu lại thông tin giấy tờ chưa xác định
                    var giayto = _db.ThongTinGiayTo.Where(t => t.Mahs == request.Mahs).ToList();
                    if (giayto.Any())
                    {
                        giayto.ForEach(x => x.Status = "XD");
                    }
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaDatDiaBan", new { request.Madv });
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


        //Tìm ra cơ quan chủ quản và lưu vào
        //Trạng thái thái từ CHT thành HT
        //Madv_ad --> mã đơn vị chuyển hồ sơ lên
        [Route("GiaDatDiaBan/Complete")]
        [HttpPost]
        public IActionResult Complete(string mahs_chuyen, string macqcq_chuyen)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giadatdb.thongtin", "Index"))
                {
                    var model = _db.GiaDatDiaBan.FirstOrDefault(t => t.Mahs == mahs_chuyen);

                    var dvcq_join = from dvcq in _db.DsDonVi
                                    join db in _db.DsDiaBan on dvcq.MaDiaBan equals db.MaDiaBan
                                    select new VMDsDonVi
                                    {
                                        Id = dvcq.Id,
                                        MaDiaBan = dvcq.MaDiaBan,
                                        MaDv = dvcq.MaDv,
                                        TenDv = dvcq.TenDv,
                                        Level = db.Level,
                                    };
                    var chk_dvcq = dvcq_join.FirstOrDefault(t => t.MaDv == macqcq_chuyen);
                    model.Macqcq = macqcq_chuyen;
                    model.Trangthai = "HT";
                    if (chk_dvcq != null && chk_dvcq.Level == "T")
                    {
                        model.Madv_t = macqcq_chuyen;
                        model.Thoidiem_t = DateTime.Now;
                        model.Trangthai_t = "CHT";
                    }
                    else if (chk_dvcq != null && chk_dvcq.Level == "ADMIN")
                    {
                        model.Madv_ad = macqcq_chuyen;
                        model.Thoidiem_ad = DateTime.Now;
                        model.Trangthai_ad = "CHT";
                    }
                    else
                    {
                        model.Madv_h = macqcq_chuyen;
                        model.Thoidiem_h = DateTime.Now;
                        model.Trangthai_h = "CHT";
                    }
                    _db.GiaDatDiaBan.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaDatDiaBan", new { model.Madv });

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

        [Route("GiaDatDiaBan/Search")]
        [HttpGet]
        public IActionResult Search(DateTime beginTime, DateTime endTime, string soQuyetDinh, string moTa, string loaiDuong, double heSoK, string madv = "all")        
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giadatdb.timkiem", "Index"))
                {
                    beginTime = beginTime == DateTime.MinValue ? new DateTime(DateTime.Now.Year, 01, 01) : beginTime;
                    endTime = endTime == DateTime.MinValue ? new DateTime(DateTime.Now.Year, 12, 31) : endTime;

                    var model = from dgct in _db.GiaDatDiaBanCt
                                join dg in _db.GiaDatDiaBan on dgct.Mahs equals dg.Mahs
                                select new VMDinhGiaDatDiaBanCt
                                {
                                    Id = dg.Id,
                                    Mahs = dg.Mahs,
                                    Diemdau = dgct.Diemdau,
                                    Diemcuoi = dgct.Diemcuoi,
                                    Soqd = dg.Soqd,
                                    Thoidiem = dg.Thoidiem,
                                    Mota = dgct.Mota,
                                    Loaiduong = dgct.Loaiduong,
                                    Hesok = dgct.Hesok,
                                    MaDv = dgct.MaDv,

                                };
                    model = model.Where(x => x.Thoidiem >= beginTime && x.Thoidiem <= endTime && x.Hesok == heSoK);
                    if (madv != "all")
                    {
                        model = model.Where(x => x.MaDv == madv);
                    }
                    if (!string.IsNullOrEmpty(soQuyetDinh))
                    {
                        model = model.Where(x=>x.Soqd.Contains(soQuyetDinh));
                    }
                    if (!string.IsNullOrEmpty(moTa))
                    {
                        model = model.Where(x => x.Mota.Contains(moTa));
                    }
                    if (!string.IsNullOrEmpty(loaiDuong))
                    {
                        model = model.Where(x => x.Loaiduong.Contains(loaiDuong));
                    }
                    
                    ViewData["beginTime"] = beginTime;
                    ViewData["endTime"] = endTime;
                    ViewData["soQuyetDinh"] = soQuyetDinh;
                    ViewData["moTa"]         = moTa;
                    ViewData["loaiDuong"]    = loaiDuong;
                    ViewData["heSoK"]         = heSoK;
                    ViewData["madv"] = madv;


                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["DsDiaBanHuyen"] = _db.DsDiaBan.Where(t => t.Level == "H");
                    ViewData["Title"] = "Tìm kiếm thông tin định giá đất địa bàn";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_giadatdiaban";
                    ViewData["MenuLv3"] = "menu_giadatdiaban_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaDatDiaBan/TimKiem/Index.cshtml", model);

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

        //[Route("GiaDatDiaBan/Result")]
        //[HttpPost]
        //public IActionResult Result(DateTime beginTime, DateTime endTime, double beginPrice, double endPrice, string madv)
        //{
        //    if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
        //    {
        //        if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giadatdb.timkiem", "Edit"))
        //        {
        //            var model = from dgct in _db.GiaThueMatDatMatNuocCt
        //                        join dg in _db.GiaThueMatDatMatNuoc on dgct.Mahs equals dg.Mahs
        //                        select new VMDinhGiaDatDiaBanCt
        //                        {
        //                            Id = dg.Id,
        //                            Mahs = dg.Mahs,
        //                            Madv = dg.Madv,
        //                            Vitri = dgct.Vitri,
        //                            Dongia = dgct.Dongia1,
        //                            Macqcq = dg.Macqcq,
        //                            Thoidiem = dg.Thoidiem,
        //                            Diemdau = dgct.Diemdau,
        //                            Diemcuoi = dgct.Diemcuoi,
        //                            Dientich = dgct.Dientich,
        //                            PhanLoaiDatNuoc = dgct.PhanLoaiDatNuoc,
        //                        };
        //            if (madv != "All")
        //            {
        //                model = model.Where(t => t.Madv == madv);
        //            }

        //            if (beginTime.ToString("yyMMdd") != "010101")
        //            {
        //                model = model.Where(t => t.Thoidiem >= beginTime);
        //            }

        //            if (endTime.ToString("yyMMdd") != "010101")
        //            {
        //                model = model.Where(t => t.Thoidiem <= endTime);
        //            }
        //            model = model.Where(t => t.Dongia >= beginPrice);
        //            if (endPrice > 0)
        //            {
        //                model = model.Where(t => t.Dongia <= endPrice);
        //            }
        //            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
        //            ViewData["Title"] = " Tìm kiếm thông tin định giá đất địa bàn";
        //            ViewData["MenuLv1"] = "menu_giadat";
        //            ViewData["MenuLv2"] = "menu_giadatdiaban";
        //            ViewData["MenuLv3"] = "menu_giadatdiaban_tk";
        //            return View("Views/Admin/Manages/DinhGia/GiaDatDiaBan/TimKiem/Result.cshtml", model);
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
