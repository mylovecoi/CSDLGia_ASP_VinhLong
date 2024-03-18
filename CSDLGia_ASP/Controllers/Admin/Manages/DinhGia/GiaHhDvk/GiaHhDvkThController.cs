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
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
                    ViewData["maKetNoiAPI"] = "giahhdvk";
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
                    //Đẩy dữ liệu qua view
                    var modelct_join = (from ct in chiTiet
                                        join dm in _db.GiaHhDvkDm on ct.Mahhdv equals dm.Mahhdv
                                        select new GiaHhDvkThCt
                                        {
                                            Id = ct.Id,
                                            Manhom = ct.Manhom,
                                            Mahhdv = ct.Mahhdv,
                                            Mahs = ct.Mahs,
                                            Gia = ct.Gia,
                                            Gialk = ct.Gialk,
                                            Loaigia = ct.Loaigia,
                                            Nguontt = ct.Nguontt,
                                            Ghichu = ct.Ghichu,
                                            Created_at = ct.Created_at,
                                            Updated_at = ct.Updated_at,
                                            Tenhhdv = dm.Tenhhdv,
                                            Dacdiemkt = dm.Dacdiemkt,
                                            Dvt = dm.Dvt,
                                        }).ToList();

                    model.GiaHhDvkThCt = modelct_join.Where(t => t.Mahs == model.Mahs).ToList();
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
        public IActionResult Edit(string maHS)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.th", "Create"))
                {
                    var model = _db.GiaHhDvkTh.FirstOrDefault(x => x.Mahs == maHS);
                    var modelCt = _db.GiaHhDvkThCt.Where(x => x.Mahs == model.Mahs);

                    var modelct_join = (from ct in modelCt
                                        join dm in _db.GiaHhDvkDm on ct.Mahhdv equals dm.Mahhdv
                                        select new GiaHhDvkThCt
                                        {
                                            Id = ct.Id,
                                            Manhom = ct.Manhom,
                                            Mahhdv = ct.Mahhdv,
                                            Mahs = ct.Mahs,
                                            Gia = ct.Gia,
                                            Gialk = ct.Gialk,
                                            Loaigia = ct.Loaigia,
                                            Nguontt = ct.Nguontt,
                                            Ghichu = ct.Ghichu,                                            
                                            Tenhhdv = dm.Tenhhdv,
                                            Dacdiemkt = dm.Dacdiemkt,
                                            Dvt = dm.Dvt,
                                        }).ToList();

                    model.GiaHhDvkThCt = modelct_join.ToList();

                    ViewData["Nhomhhdvk"] = _db.GiaHhDvkNhom.ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "ADMIN");

                    ViewData["Matt"] = model.Matt;
                    ViewData["Title"] = "Tổng hợp giá hàng hóa dịch vụ khác chỉnh sửa";
                    ViewData["MenuLv1"] = "menu_hhdvk";
                    ViewData["MenuLv2"] = "menu_hhdvk_th";
                    return View("Views/Admin/Manages/DinhGia/GiaHhDvk/TongHop/Edit.cshtml",model);

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
        public async Task<IActionResult> Update(CSDLGia_ASP.Models.Manages.DinhGia.GiaHhDvkTh request, IFormFile ipf_excel, IFormFile ipf_pdf, IFormFile ipf_word)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.hhdvk.tt", "Edit"))
                {
                    if (ipf_excel != null && ipf_excel.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(ipf_excel.FileName);
                        string extension = Path.GetExtension(ipf_excel.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await ipf_excel.CopyToAsync(FileStream);
                        }
                        // Đọc dữ liệu từ IFormFile
                       
                        using (var memoryStream = new MemoryStream())
                        {
                            ipf_excel.CopyTo(memoryStream);
                            // Chuyển đổi dữ liệu sang chuỗi base64
                            request.ipf_excel_base64 = Convert.ToBase64String(memoryStream.ToArray());                            
                        }

                        request.ipf_excel = filename;
                    }

                    if (ipf_word != null && ipf_word.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(ipf_word.FileName);
                        string extension = Path.GetExtension(ipf_word.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await ipf_word.CopyToAsync(FileStream);
                        }
                        // Đọc dữ liệu từ IFormFile
                        
                        using (var memoryStream = new MemoryStream())
                        {
                            ipf_word.CopyTo(memoryStream);
                            // Chuyển đổi dữ liệu sang chuỗi base64
                            request.ipf_word_base64 = Convert.ToBase64String(memoryStream.ToArray());
                        }
                        request.ipf_word = filename;
                    }

                    if (ipf_pdf != null && ipf_pdf.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(ipf_pdf.FileName);
                        string extension = Path.GetExtension(ipf_pdf.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await ipf_pdf.CopyToAsync(FileStream);
                        }
                        // Đọc dữ liệu từ IFormFile
                        
                        using (var memoryStream = new MemoryStream())
                        {
                            ipf_pdf.CopyTo(memoryStream);
                            // Chuyển đổi dữ liệu sang chuỗi base64
                            request.ipf_pdf_base64 = Convert.ToBase64String(memoryStream.ToArray());
                        }
                        request.ipf_pdf = filename;
                    }

                    var model = _db.GiaHhDvkTh.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Sobc = request.Sobc;
                    model.Ngaybc = request.Ngaybc;                   
                    model.Ttbc = request.Ttbc;
                    model.Ghichu = request.Ghichu;
                    model.ipf_pdf = request.ipf_pdf;
                    model.ipf_word = request.ipf_word;
                    model.ipf_excel = request.ipf_excel;
                    model.ipf_excel_base64 = request.ipf_excel_base64;
                    model.ipf_word_base64 = request.ipf_word_base64;
                    model.ipf_pdf_base64 = request.ipf_pdf_base64;
                    model.Updated_at = DateTime.Now;

                    _db.GiaHhDvkTh.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaHhDvkTh", new { request.Madv });
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

        [Route("GiaHhDvkThCt/Edit")]
        [HttpPost]
        public JsonResult EditCt(int Id)
        {
            var model = (from ct in _db.GiaHhDvkThCt.Where(t => t.Id == Id)
                         join dm in _db.GiaHhDvkDm on ct.Mahhdv equals dm.Mahhdv
                         select new GiaHhDvkThCt
                         {
                             Id = ct.Id,
                             Mahhdv = ct.Mahhdv,
                             Mahs = ct.Mahs,
                             Gia = ct.Gia,
                             Gialk = ct.Gialk,
                             Loaigia = ct.Loaigia,
                             Nguontt = ct.Nguontt,
                             Ghichu = ct.Ghichu,
                             Tenhhdv = dm.Tenhhdv,
                             Dacdiemkt = dm.Dacdiemkt,
                             Dvt = dm.Dvt,
                         }).First();

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";

                result += "<div class='row text-left'>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Tên hàng hóa, dịch vụ</label>";
                result += "<select class='form-control' disabled='disabled'>";
                result += "<option>" + model.Tenhhdv + "</option>";
                result += "</select>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Loại giá</label>";
                result += "<select id='loaigia_edit' name='loaigia_edit' class='form-control'>";
                result += "<option value='Giá bán buôn' " + ((string)model.Loaigia == "Giá bán buôn" ? "selected" : "") + ">Giá bán buôn</option>";
                result += "<option value='Giá bán lẻ' " + ((string)model.Loaigia == "Giá bán lẻ" ? "selected" : "") + ">Giá bán lẻ</option>";
                result += "<option value='Giá kê khai' " + ((string)model.Loaigia == "Giá kê khai" ? "selected" : "") + ">Giá kê khai</option>";
                result += "<option value='Giá đăng ký' " + ((string)model.Loaigia == "Giá đăng ký" ? "selected" : "") + ">Giá đăng ký</option>";
                result += "</select>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Giá kỳ trước</label>";
                result += "<input type='text' id='gialk_edit' name='gialk_edit' value='" + model.Gialk + "' class='form-control money text-right' style='font-weight: bold'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-6'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Giá kỳ trước</label>";
                result += "<input type='text' id='gia_edit' name='gia_edit' value='" + model.Gia + "' class='form-control money text-right' style='font-weight: bold'/>";
                result += "</div>";
                result += "</div>";

                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label>Nguồn thông tin</label>";
                result += "<select id='nguontt_edit' name='nguontt_edit' class='form-control'>";
                result += "<option value='Do trực tiếp điều tra, thu thập' " + ((string)model.Nguontt == "Do trực tiếp điều tra, thu thập" ? "selected" : "") + ">Do trực tiếp điều tra, thu thập</option>";
                result += "<option value='Hợp đồng mua tin' " + ((string)model.Nguontt == "Hợp đồng mua tin" ? "selected" : "") + ">Hợp đồng mua tin</option>";
                result += "<option value='Do cơ quan/đơn vị quản lý nhà nước có liên quan cung cấp/báo cáo theo quy định' " + ((string)model.Nguontt == "Do cơ quan/đơn vị quản lý nhà nước có liên quan cung cấp/báo cáo theo quy định" ? "selected" : "") + ">Do cơ quan/đơn vị quản lý nhà nước có liên quan cung cấp/báo cáo theo quy định</option>";
                result += "<option value='Từ thống kê đăng ký giá, kê khai giá, thông báo giá của doanh nghiệp' " + ((string)model.Nguontt == "Từ thống kê đăng ký giá, kê khai giá, thông báo giá của doanh nghiệp" ? "selected" : "") + ">Từ thống kê đăng ký giá, kê khai giá, thông báo giá của doanh nghiệp</option>";
                result += "<option value='Các nguồn thông tin khác' " + ((string)model.Nguontt == "Các nguồn thông tin khác" ? "selected" : "") + ">Các nguồn thông tin khác</option>";
                result += "</select>";
                result += "</div>";
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

        [Route("GiaHhDvkThCt/Update")]
        [HttpPost]
        public JsonResult UpdateCt(int Id, double Gia, double Gialk, string Nguontt, string Loaigia)
        {
            var model = _db.GiaHhDvkThCt.FirstOrDefault(t => t.Id == Id);
            model.Gia = Gia;
            model.Gialk = Gialk;
            model.Loaigia = Loaigia;
            model.Nguontt = Nguontt;
            model.Updated_at = DateTime.Now;
            _db.GiaHhDvkThCt.Update(model);
            _db.SaveChanges();
            string result = GetDataCt(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }

        public string GetDataCt(string Mahs)
        {
            var model = (from ct in _db.GiaHhDvkThCt.Where(t => t.Mahs == Mahs)
                         join dm in _db.GiaHhDvkDm on ct.Mahhdv equals dm.Mahhdv
                         select new GiaHhDvkThCt
                         {
                             Id = ct.Id,
                             Mahhdv = ct.Mahhdv,
                             Mahs = ct.Mahs,
                             Gia = ct.Gia,
                             Gialk = ct.Gialk,
                             Loaigia = ct.Loaigia,
                             Nguontt = ct.Nguontt,
                             Ghichu = ct.Ghichu,
                             Created_at = ct.Created_at,
                             Updated_at = ct.Updated_at,
                             Tenhhdv = dm.Tenhhdv,
                             Dacdiemkt = dm.Dacdiemkt,
                             Dvt = dm.Dvt,
                         });

            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover table-responsive' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th width='2%'>STT</th>";
            result += "<th>Mã hàng hóa dịch vụ</th>";
            result += "<th>Tên hàng hóa dịch vụ</th>";
            result += "<th>Đặc điểm kỹ thuật</th>";
            result += "<th>Đơn vị tính</th>";
            result += "<th>Giá kỳ trước</th>";
            result += "<th>Giá kỳ này</th>";
            result += "<th width='9%'>Thao tác</th>";
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";

            foreach (var item in model)
            {
                result += "<tr>";
                result += "<td class='text-center'>" + record++ + "</td>";
                result += "<td class='text-center'>" + item.Mahhdv + "</td>";
                result += "<td class='text-left active'>" + item.Tenhhdv + "</td>";
                result += "<td class='text-left'>" + item.Dacdiemkt + "</td>";
                result += "<td class='text-center'>" + item.Dvt + "</td>";
                result += "<td style='text-align:right; font-weight:bold'>" + Helpers.ConvertDbToStr(item.Gialk) + "</td>";
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

            return result;

        }
    }
}
