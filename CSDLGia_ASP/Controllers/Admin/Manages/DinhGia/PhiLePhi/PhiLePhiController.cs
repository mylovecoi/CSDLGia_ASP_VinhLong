using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.PhiLePhi
{
    public class PhiLePhiController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PhiLePhiController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("PhiLePhi/DanhSach")]
        [HttpGet]
        public IActionResult Index(string Nam, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.lephi.thongtin", "Index"))
                {
                    var dsdonvi = (from db in _db.DsDiaBan.Where(t => t.Level != "H")
                                   join dv in _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI") on db.MaDiaBan equals dv.MaDiaBan
                                   select new VMDsDonVi
                                   {
                                       Id = dv.Id,
                                       TenDiaBan = db.TenDiaBan,
                                       MaDiaBan = dv.MaDiaBan,
                                       TenDv = dv.TenDv,
                                       MaDv = dv.MaDv,
                                   }).ToList();
                    if (dsdonvi.Count > 0)
                    {
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

                        var model = _db.PhiLePhi.Where(t => t.Madv == Madv).ToList();

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

                        if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") == null)
                        {
                            ViewData["DsDonVi"] = dsdonvi;
                        }
                        else
                        {
                            ViewData["DsDonVi"] = dsdonvi.Where(t => t.MaDv == Madv);
                        }
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                        ViewData["Nam"] = Nam;
                        ViewData["Madv"] = Madv;
                        ViewData["Title"] = "Thông tin giá phí, lệ phí";
                        ViewData["MenuLv1"] = "menu_giakhac";
                        ViewData["MenuLv2"] = "menu_plp";
                        ViewData["MenuLv3"] = "menu_plp_tt";
                        return View("Views/Admin/Manages/DinhGia/PhiLePhi/DanhSach/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Title"] = "Thông tin giá phí, lệ phí";
                        ViewData["Messages"] = "Hệ thống chưa có định giá phí, lệ phí.";
                        ViewData["MenuLv1"] = "menu_giakhac";
                        ViewData["MenuLv2"] = "menu_plp";
                        ViewData["MenuLv3"] = "menu_plp_tt";
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


        [Route("PhiLePhi/Create")]
        [HttpGet]
        public IActionResult Create(string maDV)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.lephi.thongtin", "Create"))
                {

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.PhiLePhi
                    {
                        Mahs = maDV + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Madv = maDV,
                        Thoidiem = DateTime.Now,
                    };

                    ViewData["Mahs"] = model.Mahs;
                    ViewData["Title"] = "Giá phí, lệ phí";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_plp";
                    ViewData["MenuLv3"] = "menu_plp_tt";
                    return View("Views/Admin/Manages/DinhGia/PhiLePhi/DanhSach/Create.cshtml", model);

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

        [Route("PhiLePhi/Store")]
        [HttpPost]
        public async Task<IActionResult> Store(CSDLGia_ASP.Models.Manages.DinhGia.PhiLePhi request, IFormFile Ipf1upload)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.lephi.thongtin", "Create"))
                {
                    if (Ipf1upload != null && Ipf1upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf1upload.FileName);
                        string extension = Path.GetExtension(Ipf1upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf1upload.CopyToAsync(FileStream);
                        }
                        request.Ipf1 = filename;
                    }

                    var model = _db.PhiLePhi.FirstOrDefault(t => t.Mahs == request.Mahs);
                    if(model != null)
                    {
                        model.Soqd = request.Soqd;
                        model.Thoidiem = request.Thoidiem;
                        model.Ttqd = request.Ttqd;
                        model.Thongtin = request.Thongtin;
                        model.Ghichu = request.Ghichu;
                        model.Ipf1 = request.Ipf1;
                        model.Updated_at = DateTime.Now;                        
                    }
                    else
                    {
                        var mPhiLePhi = new CSDLGia_ASP.Models.Manages.DinhGia.PhiLePhi
                        {
                            Mahs = request.Mahs,
                            Madv = request.Madv,
                            Thoidiem = request.Thoidiem,
                            Soqd = request.Soqd,
                            Ttqd = request.Ttqd,
                            Ghichu = request.Ghichu,
                            Trangthai = "CHT",
                            Congbo = "CHUACONGBO",
                            Ipf1 = request.Ipf1,
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        };

                        _db.PhiLePhi.Add(mPhiLePhi);
                    }
                    
                    _db.SaveChanges();

                    return RedirectToAction("Index", "PhiLePhi", new { request.Madv });
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

        [Route("PhiLePhi/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.lephi.thongtin", "Edit"))
                {
                    var model = _db.PhiLePhi.FirstOrDefault(t => t.Mahs == Mahs);

                    var model_ct = _db.PhiLePhiCt.Where(t => t.Mahs == model.Mahs);

                    model.PhiLePhiCt = model_ct.ToList();

                    /*ViewData["Madv"] = model.Madv;
                    ViewData["Ipf1"] = model.Ipf1;*/
                    ViewData["Title"] = "Bảng giá xây dựng mới";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_plp";
                    ViewData["MenuLv3"] = "menu_plp_tt";
                    return View("Views/Admin/Manages/DinhGia/PhiLePhi/DanhSach/Create.cshtml", model);

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

        [Route("PhiLePhi/Update")]
        [HttpPost]
        public async Task<IActionResult> Update(CSDLGia_ASP.Models.Manages.DinhGia.PhiLePhi request, IFormFile Ipf1upload)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.lephi.thongtin", "Edit"))
                {
                    if (Ipf1upload != null && Ipf1upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf1upload.FileName);
                        string extension = Path.GetExtension(Ipf1upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf1upload.CopyToAsync(FileStream);
                        }
                        request.Ipf1 = filename;
                    }

                    var model = _db.PhiLePhi.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Ttqd = request.Ttqd;
                    model.Thongtin = request.Thongtin;
                    model.Ghichu = request.Ghichu;
                    model.Ipf1 = request.Ipf1;
                    model.Updated_at = DateTime.Now;

                    _db.PhiLePhi.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "PhiLePhi", new { request.Madv });
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

        [Route("PhiLePhi/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.lephi.thongtin", "Delete"))
                {
                    var model = _db.PhiLePhi.FirstOrDefault(t => t.Id == id_delete);
                    _db.PhiLePhi.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.PhiLePhiCt.Where(t => t.Mahs == model.Mahs);
                    _db.PhiLePhiCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "PhiLePhi", new { model.Madv });
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

        [Route("PhiLePhi/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.lephi.thongtin", "Index"))
                {
                    var model = _db.PhiLePhi.FirstOrDefault(t => t.Mahs == Mahs);
                    model.PhiLePhiCt = _db.PhiLePhiCt.Where(t => t.Mahs == model.Mahs).ToList();

                    ViewData["Title"] = "Bảng giá phí, lệ phí";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_plp";
                    ViewData["MenuLv3"] = "menu_plp_tt";
                    return View("Views/Admin/Manages/DinhGia/PhiLePhi/DanhSach/Show.cshtml", model);

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

        [Route("PhiLePhi/Complete")]
        [HttpPost]
        public IActionResult Complete(string mahs_chuyen, string macqcq_chuyen)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.lephi.thongtin", "Index"))
                {
                    var model = _db.PhiLePhi.FirstOrDefault(t => t.Mahs == mahs_chuyen);

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
                        model.Trangthai_t = "HT";
                    }
                    else if (chk_dvcq != null && chk_dvcq.Level == "ADMIN")
                    {
                        model.Madv_ad = macqcq_chuyen;
                        model.Thoidiem_ad = DateTime.Now;
                        model.Trangthai_ad = "HT";
                    }
                    else
                    {
                        model.Madv_h = macqcq_chuyen;
                        model.Thoidiem_h = DateTime.Now;
                        model.Trangthai_h = "HT";
                    }
                    _db.PhiLePhi.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "PhiLePhi", new { model.Madv });

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

        [Route("PhiLePhi/TimKiem")]
        [HttpGet]
        public IActionResult Search()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.lephi.thongtin", "Index"))
                {

                    if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") != null)
                    {
                        ViewData["Madv"] = Helpers.GetSsAdmin(HttpContext.Session, "Madv");
                    }
                    else
                    {
                        ViewData["Madv"] = "";
                    }
                    ViewData["DsDiaBan"] = _db.DsDiaBan;
                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["NhomTn"] = _db.GiaXayDungMoiNhom.Where(t => t.Theodoi == "TD").ToList();
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá phí, lệ phí";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_plp";
                    ViewData["MenuLv3"] = "menu_plp_tt";
                    return View("Views/Admin/Manages/DinhGia/PhiLePhi/TimKiem/Index.cshtml");

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

        [Route("PhiLePhi/TimKiem/KetQua")]
        [HttpPost]
        public IActionResult Result(string madv, string manhom, DateTime ngaynhap_tu, DateTime ngaynhap_den, string gia_tu, string gia_den)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.lephi.thongtin", "Index"))
                {
                    var model = (from PhiLePhiCt in _db.PhiLePhiCt
                                 join PhiLePhi in _db.PhiLePhi on PhiLePhiCt.Mahs equals PhiLePhi.Mahs
                                 join donvi in _db.DsDonVi on PhiLePhi.Madv equals donvi.MaDv
                                 /*join nhomtn in _db.GiaXayDungMoiNhom on giathuetn.Manhom equals nhomtn.Manhom*/
                                 select new PhiLePhiCt
                                 {
                                     Id = PhiLePhiCt.Id,
                                     Mahs = PhiLePhiCt.Mahs,
                                     Madv = PhiLePhi.Madv,
                                     Tendv = donvi.TenDv,
                                     MaSo = PhiLePhiCt.MaSo,
                                     MaSoGoc = PhiLePhiCt.MaSoGoc,
                                     Dongia = PhiLePhiCt.Dongia,
                                     HienThi = PhiLePhiCt.HienThi,
                                     CapDo = PhiLePhiCt.CapDo,
                                     Thoidiem = PhiLePhi.Thoidiem,
                                 });

                    if (madv != "all")
                    {
                        model = model.Where(t => t.Madv == madv);
                    }


                    if (ngaynhap_tu.ToString("yyMMdd") != "010101")
                    {
                        model = model.Where(t => t.Thoidiem >= ngaynhap_tu);
                    }

                    if (ngaynhap_den.ToString("yyMMdd") != "010101")
                    {
                        model = model.Where(t => t.Thoidiem <= ngaynhap_den);
                    }


                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá phí, lệ phí";
                    ViewData["MenuLv1"] = "menu_giakhac";
                    ViewData["MenuLv2"] = "menu_plp";
                    ViewData["MenuLv3"] = "menu_plp_tt";
                    return View("Views/Admin/Manages/DinhGia/PhiLePhi/TimKiem/Result.cshtml", model);

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


        [Route("PhiLePhiCt/Update")]
        [HttpPost]
        public JsonResult UpdateCt(CSDLGia_ASP.Models.Manages.DinhGia.PhiLePhiCt request)
        {
            //Kiêm tra id = =-1 =>thêm mới
            if (request.Id == -1)
            {
                var model = new CSDLGia_ASP.Models.Manages.DinhGia.PhiLePhiCt
                {
                    Mahs = request.Mahs,
                    MaSo = request.Mahs,
                    MaSoGoc = request.Mahs,
                    HienThi = request.HienThi,//HIện thị dữ liệu ra màn hình
                    STT = request.STT,//Số thứ tự theo mã gốc
                    CapDo = request.CapDo,//Bắt đầu từ 1
                    ChiTieu = request.ChiTieu,
                    Dongia = request.Dongia,
                };

                _db.PhiLePhiCt.Add(model);
                _db.SaveChanges();
            }
            else
            {
                var model = _db.PhiLePhiCt.FirstOrDefault(t => t.Id == request.Id);
                model.Dongia = request.Dongia;
                model.MaSo = request.Mahs;
                model.MaSoGoc = request.Mahs;
                model.HienThi = request.HienThi;//HIện thị dữ liệu ra màn hình
                model.STT = request.STT;//Số thứ tự theo mã gốc
                model.CapDo = request.CapDo;//Bắt đầu từ 1
                model.ChiTieu = request.ChiTieu;
                model.Dongia = request.Dongia;
                _db.PhiLePhiCt.Update(model);
                _db.SaveChanges();
            }



            string result = GetDataCt(request.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        public string GetDataCt(string Mahs)
        {
            var model = _db.PhiLePhiCt.Where(t => t.Mahs == Mahs).ToList();

            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover table-responsive' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th width='4%'>#</th>";
            result += "<th width='25%'>Mã số</th>";
          
            result += "<th>Đơn giá</th>";
            result += "<th width='8%'>Thao tác</th>";
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";

            foreach (var item in model)
            {
                result += "<tr>";
                result += "<td class='text-center'>" + record++ + "</td>";
                result += "<td class='active' style='font-weight:bold'>" + item.MaSo + "</td>";
                result += "<td>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Nhập giá'";
                result += " data-target='#Edit_Modal' data-toggle='modal' onclick='SetEdit('" + item.Id + "','"+ item.MaSo + "')'>";
                result += "<i class='icon-lg la la-edit text-primary'></i>";
                result += "</button>";
                result += "</td>";
                result += "</tr>";
            }
            result += "</tbody>";
            result += "</table>";
            result += "</div>";

            return result;

        }
    }
}
