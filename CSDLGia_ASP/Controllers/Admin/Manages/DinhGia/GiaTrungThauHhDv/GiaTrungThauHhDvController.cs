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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaTrungThauHhDv
{
    public class GiaTrungThauHhDvController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IDsDonviService _dsDonviService;
        private readonly ITrangThaiHoSoService _trangThaiHoSoService;

        public GiaTrungThauHhDvController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment, IDsDonviService dsDonviService, ITrangThaiHoSoService trangThaiHoSoService)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
            _dsDonviService = dsDonviService;
            _trangThaiHoSoService = trangThaiHoSoService;
        }

        [Route("GiaTrungThauHhDv")]
        [HttpGet]
        public IActionResult Index(string Madv, int Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.muataisan.thongtin", "Index"))
                {

                    Madv = string.IsNullOrEmpty(Madv) ? Helpers.GetSsAdmin(HttpContext.Session, "Madv") : Madv;

                    var model_donvi = _dsDonviService.GetListDonvi(Helpers.GetSsAdmin(HttpContext.Session, "Madv"));
                    List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();

                    IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaMuaTaiSan> model = _db.GiaMuaTaiSan;

                    if (Madv != "all")
                    {
                        model = model.Where(t => t.Madv == Madv);
                    }

                    if (Nam != 0)
                    {
                        model = model.Where(t => t.Thoidiem.Year == Nam);
                    }
                 
                    ViewData["DsDonVi"] = model_donvi;
                    ViewData["Madv"] = Madv;
                    ViewData["Nam"] = Nam;
                    ViewData["Title"] = " Thông tin hồ sơ giá trúng thầu hàng hóa dịch vụ";
                    ViewData["MenuLv1"] = "menu_mts";
                    ViewData["MenuLv2"] = "menu_giamts_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauHhDv/DanhSach/Index.cshtml", model);
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


        [Route("GiaTrungThauHhDv/Create")]
        [HttpGet]
        public IActionResult Create(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.muataisan.thongtin", "Create"))
                {
                    var model = new GiaMuaTaiSan
                    {
                        Madv = Madv,
                        Ngayqd = DateTime.Now,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                    };

                    //ViewData["Madb"] = _db.GiaMuaTaiSan.Where(t => t.Madv == Madv).OrderBy(t => t.Id).Select(t => t.Madiaban).First();
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level == "H");
                    //ViewData["DmNhomHh"] = _db.DmNhomHh.ToList();

                    ViewData["Title"] = " Thông tin hồ sơ giá trúng thầu hàng hóa dịch vụ";
                    ViewData["MenuLv1"] = "menu_mts";
                    ViewData["MenuLv2"] = "menu_giamts_tt";

                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauHhDv/DanhSach/Create.cshtml", model);
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


        [Route("GiaTrungThauHhDv/Store")]
        [HttpPost]
        public IActionResult Store(GiaMuaTaiSan request)
        {

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.muataisan.thongtin", "Create"))
                {
                    var model = _db.GiaMuaTaiSan.FirstOrDefault(t => t.Mahs == request.Mahs);
                    if (model != null)
                    {
                        model.Madiaban = request.Madiaban;
                        model.Soqd = request.Soqd;
                        model.Ngayqd = request.Ngayqd;
                        model.Thoidiem = request.Ngayqd;
                        model.Manhom = request.Manhom;
                        model.Tennhathau = request.Tennhathau;
                        model.Thongtinqd = request.Thongtinqd;
                        model.Ghichu = request.Ghichu;
                        model.Ipf1 = request.Ipf1;
                        model.Updated_at = DateTime.Now;
                        _db.GiaMuaTaiSan.Update(model);
                        //Add Log
                        _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Cập nhật");
                    }
                    else
                    {
                        var giaMuaTaiSan = new GiaMuaTaiSan
                        {
                            Mahs = request.Mahs,
                            Madv = request.Madv,
                            Madiaban = request.Madiaban,
                            Soqd = request.Soqd,
                            Manhom = request.Manhom,
                            Ngayqd = request.Ngayqd,
                            Thoidiem = request.Ngayqd,
                            Tennhathau = request.Tennhathau,
                            Thongtinqd = request.Thongtinqd,
                            Ghichu = request.Ghichu,
                            Trangthai = "CC",
                            Congbo = "CHUACONGBO",
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        };
                        _db.GiaMuaTaiSan.Add(giaMuaTaiSan);
                        //Add Log
                        _trangThaiHoSoService.LogHoSo(request.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Thêm mới");
                    }
                    _db.SaveChanges();


                    return RedirectToAction("Index", "GiaTrungThauHhDv", new { Madv = request.Madv });
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


        [Route("GiaTrungThauHhDv/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.muataisan.thongtin", "Edit"))
                {
                    var model = _db.GiaMuaTaiSan.FirstOrDefault(t => t.Mahs == Mahs);
                    model.GiaMuaTaiSanCt = _db.GiaMuaTaiSanCt.Where(t => t.Mahs == Mahs).ToList();
                    var model_file = _db.ThongTinGiayTo.Where(t => t.Mahs == model.Mahs);
                    model.ThongTinGiayTo = model_file.ToList();

                    ViewData["DmNhomHh"] = _db.DmNhomHh.ToList();
                    ViewData["Madv"] = model.Madv;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level == "T");

                    ViewData["Title"] = " Thông tin hồ sơ giá trúng thầu hàng hóa dịch vụ";
                    ViewData["MenuLv1"] = "menu_mts";
                    ViewData["MenuLv2"] = "menu_giamts_tt";

                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauHhDv/DanhSach/Create.cshtml", model);
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

        [Route("GiaTrungThauHhDv/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.muataisan.thongtin", "Delete"))
                {
                    var model = _db.GiaMuaTaiSan.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaMuaTaiSan.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaTrungThauHhDv", new { Madv = model.Madv });
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

        [Route("GiaTrungThauHhDv/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.muataisan.thongtin", "Show"))
                {
                    var model = _db.GiaMuaTaiSan.FirstOrDefault(t => t.Mahs == Mahs);
                    model.GiaMuaTaiSanCt = _db.GiaMuaTaiSanCt.Where(t => t.Mahs == Mahs).ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level == "T");
                    ViewData["Title"] = " Thông tin hồ sơ giá trúng thầu hàng hóa dịch vụ";
                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauHhDv/DanhSach/Show.cshtml", model);
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
        public IActionResult HoanThanh(string mahs_complete, string trangthai_complete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.muataisan.thongtin", "Approve"))
                {
                    var model = _db.GiaMuaTaiSan.FirstOrDefault(p => p.Mahs == mahs_complete);
                    model.Updated_at = DateTime.Now;
                    model.Trangthai = trangthai_complete;

                    _db.GiaMuaTaiSan.Update(model);
                    _db.SaveChanges();
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), trangthai_complete);
                    return RedirectToAction("Index", "GiaTrungThauHhDv", new { Nam = model.Thoidiem.Year });
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

        [Route("GiaTrungThauHhDv/Search")]
        [HttpGet]
        public IActionResult Search()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.muataisan.timkiem", "Index"))
                {

                    ViewData["DsDiaBan"] = _db.DsDiaBan;
                    ViewData["DsDonVi"] = _db.DsDonVi;

                    ViewData["Title"] = " Thông tin hồ sơ giá trúng thầu mua hàng hóa dịch vụ";
                    ViewData["MenuLv1"] = "menu_mts";
                    ViewData["MenuLv2"] = "menu_giamts_tk";

                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauHhDv/TimKiem/Index.cshtml");
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

        [Route("GiaTrungThauHhDv/Result")]
        [HttpPost]
        public IActionResult Result(string madv, string tenhanghoa, DateTime ngaynhap_tu, DateTime ngaynhap_den,
            double beginPrice, double endPrice, double KhoiLuongTu, double KhoiLuongDen)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.muataisan.timkiem", "Index"))
                {

                    var model = (from chitiet in _db.GiaMuaTaiSanCt
                                 join hoso in _db.GiaMuaTaiSan on chitiet.Mahs equals hoso.Mahs
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new GiaMuaTaiSanCt
                                 {
                                     Id = chitiet.Id,
                                     Dvt = chitiet.Dvt,
                                     Mahs = chitiet.Mahs,
                                     Madv = hoso.Madv,
                                     Thoidiem = hoso.Thoidiem,
                                     Soqd = hoso.Soqd,
                                     DonGia = chitiet.DonGia,
                                     KhoiLuong = chitiet.KhoiLuong,
                                     Thongtinqd = hoso.Thongtinqd,
                                     Mota = chitiet.Mota,
                                     Madiaban = hoso.Madiaban,
                                     Tennhathau = hoso.Tennhathau,
                                     TrangThai = hoso.Trangthai
                                 });

                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model = model.Where(t => list_trangthai.Contains(t.TrangThai));

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
                    //Tìm theo đơn giá
                    if (beginPrice > 0)
                    {
                        model = model.Where(t => t.DonGia >= beginPrice);
                    }
                    if (endPrice > 0)
                    {
                        model = model.Where(t => t.DonGia <= endPrice);
                    }
                    //Tìm theo khối lượng
                    if (KhoiLuongTu > 0)
                    {
                        model = model.Where(t => t.KhoiLuong >= KhoiLuongTu);
                    }
                    if (KhoiLuongDen > 0)
                    {
                        model = model.Where(t => t.KhoiLuong <= KhoiLuongDen);
                    }
                    //Tên sản phẩm dịch vụ
                    if (!string.IsNullOrEmpty(tenhanghoa))
                    {
                        model = model.Where(t => t.Mota.ToLower().Contains(tenhanghoa.ToLower()));
                    }
                    ViewData["DsDonVi"] = _db.DsDonVi;
                    ViewData["DsDiaBan"] = _db.DsDiaBan;

                    ViewData["Title"] = " Thông tin hồ sơ giá trúng thầu mua hàng hóa dịch vụ";
                    ViewData["MenuLv1"] = "menu_mts";
                    ViewData["MenuLv2"] = "menu_giamts_tk";

                    return View("Views/Admin/Manages/DinhGia/GiaTrungThauHhDv/TimKiem/Result.cshtml", model);
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


        [Route("GiaTrungThauHhDvCt/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            var model = _db.GiaMuaTaiSanCt.FirstOrDefault(t => t.Id == Id);
            model.ThanhTien = model.KhoiLuong * model.DonGia;
            // Trả về JSON cho JavaScript
            return Json(model);
        }

        [Route("GiaTrungThauHhDvCt/Update")]
        [HttpPost]
        public JsonResult UpdateCt(GiaMuaTaiSanCt requets)
        {
            //Kiêm tra id = =-1 =>thêm mới
            if (requets.Id == -1)
            {
                var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaMuaTaiSanCt
                {
                    Mahs = requets.Mahs,
                    Mota = requets.Mota,
                    KhoiLuong = requets.KhoiLuong,
                    DonGia = requets.DonGia,
                    Dvt = requets.Dvt,
                    SapXep = requets.SapXep,
                    HienThi = requets.HienThi,
                };

                _db.GiaMuaTaiSanCt.Add(model);
                _db.SaveChanges();
            }
            else
            {
                var model = _db.GiaMuaTaiSanCt.FirstOrDefault(t => t.Id == requets.Id);
                model.DonGia = requets.DonGia;
                model.Mota = requets.Mota;
                model.KhoiLuong = requets.KhoiLuong;
                model.Dvt = requets.Dvt;
                model.DonGia = requets.DonGia;
                model.SapXep = requets.SapXep;
                model.HienThi = requets.HienThi;
                model.Updated_at = DateTime.Now;
                _db.GiaMuaTaiSanCt.Update(model);
                _db.SaveChanges();
            }


            string result = GetDataCt(requets.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        public string GetDataCt(string Mahs)
        {
            var model = _db.GiaMuaTaiSanCt.Where(t => t.Mahs == Mahs).ToList();

            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover table-responsive' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th width='2%'>#</th>";
            result += "<th>Tên công tác</th>";
            result += "<th>Đơn vị</th>";
            result += "<th>Khối lượng<br>mời thầu</th>";
            result += "<th>Đơn giá</th>";
            result += "<th>Thành tiền</th>";
            result += "<th width='10%'>Thao tác</th>";
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";

            foreach (var item in model)
            {
                result += "<tr>";
                result += "<td class='text-center'>" + record++ + "</td>";
                result += "<td class='text-center'>" + item.Mota + "</td>";
                result += "<td class='text-center'>" + item.Dvt + "</td>";
                result += "<td class='text-center'>" + Helpers.ConvertDbToStr(item.KhoiLuong) + "</td>";
                result += "<td class='text-center'>" + Helpers.ConvertDbToStr(item.DonGia) + "</td>";
                result += "<td style='text-align:right; font-weight:bold'>" + Helpers.ConvertDbToStr(item.KhoiLuong * item.DonGia) + "</td>";
                result += "<td>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' title='Nhập giá'";
                result += " data-target='#Edit_Modal' data-toggle='modal' onclick='SetEdit(`" + item.Id + "`)'>";
                result += "<i class='icon-lg la la-edit text-primary'></i>";
                result += "</button>";
                result += "<button type='button' data-target='#Delete_Modal' data-toggle='modal'";
                result += " onclick='GetDelete(" + item.Id + ")' class='btn btn-sm btn-clean btn-icon' title='Xóa'>";
                result += "<i class='icon-lg la la-trash text-danger'></i>";
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
