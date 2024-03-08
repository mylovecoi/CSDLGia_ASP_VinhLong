using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.Models.Systems.KetNoiGiaDichVu;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using OfficeOpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaHhDvk
{
    public class GiaHhDvkThController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GiaHhDvkThController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("GiaHhDvk/TongHop")]
        [HttpGet]
        public IActionResult Index(string Thang, string Nam, string maDV)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.th", "Index"))
                {
                    var dsDonVi = _db.DsDonVi;
                    if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") != null)
                    {
                        dsDonVi = (DbSet<DsDonVi>)dsDonVi.Where(t => t.MaDv == maDV);
                    }
                    Nam = Nam ?? Helpers.ConvertYearToStr(DateTime.Now.Year);
                    Thang = Thang ?? Helpers.ConvertYearToStr(DateTime.Now.Month);
                    maDV = maDV ?? dsDonVi.FirstOrDefault().MaDv;

                    var model = _db.GiaHhDvkTh.Where(t => t.Madv == maDV);
                    if (Nam != "all")
                    {
                        model = model.Where(t => t.Nam == Nam);
                    }
                    if (Thang != "all")
                    {
                        model = model.Where(t => t.Thang == Thang);
                    }
                    //Lấy danh sách hồ sơ đơn vị cấp dưới gửi lên

                    var dsdonvi = _db.DsDonVi;
                    var dsdiaban = _db.DsDiaBan;
                    var getdonvi = (from dv in dsdonvi.Where(t => t.MaDv == maDV)
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


                    switch (getdonvi.Level)
                    {
                        case "H":
                            {
                                var dsHoSo = _db.GiaHhDvk.Where(t => t.Madv_h == maDV && t.Thang == Thang);
                                ViewData["Hoso"] = dsHoSo;
                                break;
                            }
                        case "T":
                            {
                                var dsHoSo = _db.GiaHhDvk.Where(t => t.Madv_t == maDV && t.Thang == Thang);
                                ViewData["Hoso"] = dsHoSo;
                                break;
                            }
                        case "ADMIN":
                            {
                                var dsHoSo = _db.GiaHhDvk.Where(t => t.Madv_ad == maDV && t.Thang == Thang);
                                ViewData["Hoso"] = dsHoSo;
                                break;
                            }
                    }


                    ViewData["dsnhom"] = _db.GiaHhDvkNhom;
                    /*ViewData["model"] = model_join;*/
                    ViewData["DsDiaBan"] = _db.DsDiaBan;
                    ViewData["DsDonVi"] = dsDonVi;
                    ViewData["thang"] = Thang;
                    ViewData["nam"] = Nam;
                    ViewData["maDV"] = maDV;
                    ViewData["Title"] = "Tổng hợp giá hàng hóa dịch vụ khác";
                    ViewData["MenuLv1"] = "menu_hhdvk";
                    ViewData["MenuLv2"] = "menu_hhdvk_th";
                    return View("Views/Admin/Manages/DinhGia/GiaHhDvk/TongHop/Index.cshtml", model.ToList());
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

        [Route("GiaHhDvk/TongHop/Create")]
        [HttpGet]
        public IActionResult Create(string Matt, string Thang, string Nam, string maDV, string[] Hoso)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.th", "Create"))
                {

                    var modelcthskk = _db.GiaHhDvkCt.Where(t => Hoso.Contains(t.Mahs) && t.Gia != 0).ToList();
                    var modeldm = _db.GiaHhDvkDm.Where(x => x.Matt == Matt);

                    var model = new GiaHhDvkTh
                    {
                        Madv = maDV,
                        Mahs = DateTime.Now.ToString("yyMMddssmmHH"),
                        Matt = Matt,
                        Trangthai = "CHT",
                        Thang = Thang,
                        Nam = Nam,
                        Ttbc = "Tổng hợp số liệu tháng " + Thang + " năm " + Nam,
                        Mahstonghop = string.Join(",", Hoso)
                    };
                    //Lấy danh sách chi tiết hồ sơ
                    var dsHoSoChiTiet = _db.GiaHhDvkCt.Where(item => Hoso.Contains(item.Mahs)).ToList();
                    //Trường mảng mahs rỗng

                    //Trường mảng mahs có dữ liệu

                    //Lấy danh mục hàng hoá
                    var dmHangHoa = _db.GiaHhDvkDm.Where(x => x.Matt == Matt);
                    var chiTiet = new List<GiaHhDvkThCt>();
                    foreach (var item in dmHangHoa)
                    {
                        //Lấy ds chi tiết
                        var ct = dsHoSoChiTiet.Where(x => x.Mahhdv == item.Mahhdv);
                        var Gia = ct.Any() ? ct.Sum(x => x.Gialk) / ct.Count() : 0;
                        var Gialk = ct.Any() ? ct.Sum(x => x.Gialk) / ct.Count() : 0;
                        
                        chiTiet.Add(new GiaHhDvkThCt
                        {
                            Mahs = model.Mahs,
                            Mahhdv = item.Mahhdv,
                            Gialk = (double)Gialk,
                            Gia = (double)Gia,
                        });
                    }
                    _db.GiaHhDvkTh.Add(model);
                    _db.GiaHhDvkThCt.AddRange(chiTiet);
                    _db.SaveChanges();

                    ViewData["Title"] = "Tổng hợp giá hàng hóa dịch vụ khác thêm mới";
                    ViewData["MenuLv1"] = "menu_hhdvk";
                    ViewData["MenuLv2"] = "menu_hhdvk_th";
                    return View("Views/Admin/Manages/DinhGia/GiaHhDvk/TongHop/Create.cshtml", model);

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

        [Route("GiaHhDvk/TongHop/Store")]
        [HttpPost]
        public IActionResult Store(string Mahs, string Matt, string Thang, string Nam, string Sobc, DateTime Ngaybc, DateTime Ngaychotbc, string Ghichu)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.th", "Create"))
                {

                    var modelSave = _db.GiaHhDvkTh.FirstOrDefault(x => x.Mahs == Mahs);
                    modelSave.Sobc = Sobc;
                    modelSave.Ngaybc = Ngaybc;
                    modelSave.Ngaychotbc = Ngaychotbc;
                    modelSave.Ghichu = Ghichu;
                    _db.GiaHhDvkTh.Update(modelSave);
                    _db.SaveChanges();
                    var model = _db.GiaHhDvkTh.Where(x => x.Matt == Matt);
                    return RedirectToAction("Index", "GiaHhDvkTh", new { Thang = Thang, Nam = Nam, Matt = Matt });

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

        [Route("GiaHhDvk/TongHop/Edit")]
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.th", "Create"))
                {
                    var model = _db.GiaHhDvkTh.FirstOrDefault(x => x.Id == Id);
                    var modelCt = _db.GiaHhDvkCtTh.Where(x => x.Mahs == model.Mahs);
                    ViewBag.model = model;
                    ViewData["modelCt"] = modelCt;
                    ViewData["Matt"] = model.Matt;
                    ViewData["Title"] = "Tổng hợp giá hàng hóa dịch vụ khác chỉnh sửa";
                    ViewData["MenuLv1"] = "menu_hhdvk";
                    ViewData["MenuLv2"] = "menu_hhdvk_th";
                    return View("Views/Admin/Manages/DinhGia/GiaHhDvk/TongHop/Edit.cshtml");

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

        [Route("GiaHhDvk/TongHop/Update")]
        [HttpPost]
        public IActionResult Update(string Mahs, string Matt, string Thang, string Nam, string Sobc, DateTime Ngaybc, DateTime Ngaychotbc, string Ghichu)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.th", "Create"))
                {

                    var modelSave = _db.GiaHhDvkTh.FirstOrDefault(x => x.Mahs == Mahs);
                    modelSave.Sobc = Sobc;
                    modelSave.Ngaybc = Ngaybc;
                    modelSave.Ngaychotbc = Ngaychotbc;
                    modelSave.Ghichu = Ghichu;
                    _db.GiaHhDvkTh.Update(modelSave);
                    _db.SaveChanges();
                    var model = _db.GiaHhDvkTh.Where(x => x.Matt == Matt);
                    return RedirectToAction("Index", "GiaHhDvkTh", new { Thang = Thang, Nam = Nam, Matt = Matt });

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

        [Route("GiaHhDvk/TongHop/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.th", "Delete"))
                {
                    var model = _db.GiaHhDvkTh.FirstOrDefault(t => t.Id == id_delete);
                    var Thang = model.Thang;
                    var Nam = model.Nam;
                    var Matt = model.Matt;
                    _db.GiaHhDvkTh.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaHhDvkTh", new { Thang = Thang, Nam = Nam, Matt = Matt });
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

        [Route("GiaHhDvk/TongHop/CreateHs")]
        [HttpGet]
        public IActionResult CreateHs(string Mahs, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.tt", "Create"))
                {
                    var getRecordTh = _db.GiaHhDvkTh.FirstOrDefault(x => x.Mahs == Mahs);
                    var getMadb = _db.DsDonVi.FirstOrDefault(x => x.MaDv == Madv).MaDiaBan;
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvk();
                    model.Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH");
                    model.Madiaban = Madv;
                    model.Matt = getRecordTh.Matt;
                    model.Soqd = getRecordTh.Sobc;
                    model.Thoidiem = getRecordTh.Ngaybc;
                    model.Madiaban = getMadb;
                    model.Madv = Madv;
                    model.Trangthai = "CHT";
                    model.Thang = getRecordTh.Thang;
                    model.Nam = getRecordTh.Nam;
                    _db.GiaHhDvk.Add(model);

                    var getModelThCt = _db.GiaHhDvkCtTh.Where(x => x.Mahs == Mahs);
                    foreach (var item in getModelThCt)
                    {
                        var modelCt = new GiaHhDvkCt();
                        modelCt.Mahs = model.Mahs;
                        modelCt.Mahhdv = item.Mahhdv;
                        modelCt.Loaigia = "Giá bán lẻ";
                        modelCt.Nguontt = "Do cơ quan/đơn vị quản lý nhà nước có liên quan cung cấp/báo cáo theo quy định";
                        modelCt.Gia = item.Gia;
                        modelCt.Gialk = item.Gialk;
                    }
                    _db.SaveChanges();
                    return RedirectToAction("Index", "GiaHhDvkTh", new { Thang = getRecordTh.Thang, Nam = getRecordTh.Nam, Matt = getRecordTh.Matt });
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
