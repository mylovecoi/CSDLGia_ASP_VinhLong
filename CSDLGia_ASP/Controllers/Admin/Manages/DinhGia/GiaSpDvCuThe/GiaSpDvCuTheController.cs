
using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;
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

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaSpDvCuThe
{
    public class GiaSpDvCuTheController : Controller
    {

        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GiaSpDvCuTheController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        //1.Sau khi chọn menu chuyển vào đây
        //2.Thêm mới
        //3.Kiểm tra Nam/DsDv của tài khoản đăng nhập xuất bản nghi ra nếu không có thì năm lấy năm hiện tại và đơn vị đầu tiên trong DsDonVi
        //4.Donvi lấy dữ liệu từ dsdiaban lấy tất cả bản ghi

        [Route("GiaSpDvCuThe")]
        [HttpGet]
        public IActionResult Index(string Nam, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.thongtin", "Index"))
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

                        var model = _db.GiaSpDvCuThe.Where(t => t.Madv == Madv).ToList();

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
                        ViewData["NhomTn"] = _db.GiaSpDvCuTheNhom.ToList();
                        ViewData["Nam"] = Nam;
                        ViewData["Madv"] = Madv;
                        ViewData["Title"] = "Thông tin giá sản phẩm dịch vụ cụ thể";
                        ViewData["MenuLv1"] = "menu_spdvcuthe";
                        ViewData["MenuLv2"] = "menu_spdvcuthe_thongtin";
                        return View("Views/Admin/Manages/DinhGia/GiaSpDvCuThe/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Title"] = "Thông tin giá sản phẩm dịch vụ cụ thể";
                        ViewData["Messages"] = "Hệ thống chưa có định giá sản phẩm dịch vụ cụ thể.";
                        ViewData["MenuLv1"] = "menu_spdvcuthe";
                        ViewData["MenuLv2"] = "menu_spdvcuthe_thongtin";
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

        [Route("GiaSpDvCuThe/Create")]
        [HttpGet]
        public IActionResult Create(string Manhom, string MadvBc)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.thongtin", "Create"))
                {

                    // hồ sơ mà chỉ đến bước hoàn thành mà quay lại thì sẽ có trạng thái CXD --> cần xóa
                    var check = _db.GiaSpDvCuTheCt.Where(t => t.Trangthai == "CXD");
                    if (check != null)
                    {
                        _db.GiaSpDvCuTheCt.RemoveRange(check);
                        _db.SaveChanges();
                    }

                    // Thông tin của bộ hồ sơ
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCuThe
                    {
                        Mahs = MadvBc + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Madv = MadvBc,
                        Manhom = Manhom,
                        PhanLoaiHoSo = "HOSOCHITIET",
                    };

                    var danhmuc = _db.GiaSpDvCuTheDm.ToList(); // lấy dữ liệu trong bảng GiaSpDvCuTheDm


                    // Khi bấm đồng ý trong moda thì add dữ liệu GiaSpDvCuTheDm -> bản GiaSpDvCuTheCt

                    if (Manhom != "all")
                    {
                        danhmuc = danhmuc.Where(t => t.Manhom == Manhom).ToList();
                    }
                    else
                    {
                        danhmuc = danhmuc.ToList();
                    }


                   var chitiet = new List<GiaSpDvCuTheCt>();


                    foreach (var item in danhmuc)
                    {
                        chitiet.Add(new GiaSpDvCuTheCt()
                        {
                            Mahs = model.Mahs,
                            Dvt = item.Dvt,
                            Tt = item.Tt,                            
                            Manhom = item.Manhom,
                            Maspdv = item.Maspdv,
                            TenSpDv = item.Tenspdv,

                            Trangthai = "CXD",
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        });
                    }
                    _db.GiaSpDvCuTheCt.AddRange(chitiet);
                    _db.SaveChanges();


                    // Xử lý phần Forech theo mã nhóm khi chọn
                    var groupmanhom1 = _db.GiaSpDvCuTheNhom.Select(item => item.Manhom).ToList();
                    var groupmanhom2 = _db.GiaSpDvCuTheNhom.Where(item => item.Manhom == Manhom).Select(item => item.Manhom).ToList();
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
                    ViewData["GiaSpDvCuTheNhom"] = _db.GiaSpDvCuTheNhom.ToList();
                    ViewData["GiaSpDvCuTheDm"] = _db.GiaSpDvCuTheDm.ToList();
                    // End xử lý phần Forech theo mã nhóm khi chọn

                    model.GiaSpDvCuTheCt = chitiet.Where(t => t.Mahs == model.Mahs).ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "T");
                    ViewData["Manhom"] = Manhom;
                    ViewData["Madv"] = MadvBc;
                    ViewData["Mahs"] = model.Mahs;
                  
                    ViewData["Title"] = "Bảng giá sản phẩm dịch vụ cụ thể";
                    ViewData["MenuLv1"] = "menu_spdvcuthe";
                    ViewData["MenuLv2"] = "menu_spdvcuthe_thongtin";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCuThe/Create.cshtml", model);

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

        [Route("GiaSpDvCuThe/NhanExcel")]
        [HttpGet]
        public IActionResult NhanExcel(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.thongtin", "Create"))
                {

                    // hồ sơ mà chỉ đến bước hoàn thành mà quay lại thì sẽ có trạng thái CXD --> cần xóa
                    var check = _db.GiaSpDvCuTheCt.Where(t => t.Trangthai == "CXD");
                    if (check != null)
                    {
                        _db.GiaSpDvCuTheCt.RemoveRange(check);
                        _db.SaveChanges();
                    }

                    // Thông tin của bộ hồ sơ
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCuThe
                    {
                        Madv = Madv,
                        Thoidiem = DateTime.Now,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        PhanLoaiHoSo = "HOSONHANEXCEL",
                    };

                    
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "T");

                    ViewData["codeExcel"] = model.CodeExcel;//Gán ra view để dùng chung
                    ViewData["Title"] = "Bảng giá sản phẩm dịch vụ cụ thể";
                    ViewData["MenuLv1"] = "menu_spdvcuthe";
                    ViewData["MenuLv2"] = "menu_spdvcuthe_thongtin";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCuThe/NhanExcel.cshtml", model);

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

        [Route("GiaSpDvCuThe/Store")]
        [HttpPost]
        public async Task<IActionResult> Store(CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCuThe request, IFormFile Ipf1)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.thongtin", "Create"))
                {
                    if (Ipf1 != null && Ipf1.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf1.FileName);
                        string extension = Path.GetExtension(Ipf1.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf1.CopyToAsync(FileStream);
                        }
                        request.Ipf1 = filename;
                    }
                    // 2024.03.15 Gộp Update và phần nhận cho Excel
                    if (_db.GiaSpDvCuThe.Where(x => x.Mahs == request.Mahs).Any())
                    {
                        //Xử lý hồ sơ
                        var modelExcel = _db.GiaSpDvCuThe.FirstOrDefault(t => t.Mahs == request.Mahs);
                        modelExcel.Madiaban = request.Madiaban;
                        modelExcel.Soqd = request.Soqd;
                        modelExcel.Thoidiem = request.Thoidiem;
                        modelExcel.Thongtin = request.Thongtin;
                        modelExcel.GhiChu = request.GhiChu;
                        modelExcel.CodeExcel = request.CodeExcel;
                        modelExcel.Updated_at = DateTime.Now;
                        modelExcel.Ipf1 = request.Ipf1;
                        _db.GiaSpDvCuThe.Update(modelExcel);

                        // Xử lý phần lịch sử hồ sơ 
                        var lichSu = new TrangThaiHoSo
                        {
                            MaHoSo = request.Mahs,
                            TenDangNhap = Helpers.GetSsAdmin(HttpContext.Session, "Name"),
                            ThongTin = "Thay đổi thông tin hồ sơ",
                            ThoiGian = DateTime.Now,
                            TrangThai = "CHT",
                        };
                        _db.TrangThaiHoSo.Add(lichSu);
                        _db.SaveChanges();
                        // End Xử lý phần lịch sử hồ sơ 
                        return RedirectToAction("Index", "GiaSpDvCuThe", new { request.Madv });
                    }

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCuThe
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Manhom = request.Manhom,

                        Madiaban = request.Madiaban,
                        Soqd = request.Soqd,
                        GhiChu = request.GhiChu,
                        Thoidiem = request.Thoidiem,
                        Thongtin = request.Thongtin,
                        Ipf1 = request.Ipf1,
                        Trangthai = "CHT",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaSpDvCuThe.Add(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaSpDvCuTheCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Trangthai = "XD";
                        }
                    }
                    _db.GiaSpDvCuTheCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    // Xử lý phần lịch sử hồ sơ 

                    var lichSuHoSo = new TrangThaiHoSo
                    {
                        MaHoSo = request.Mahs,
                        TenDangNhap = Helpers.GetSsAdmin(HttpContext.Session, "Name"),
                        ThongTin = "Thay đổi thông tin hồ sơ",
                        ThoiGian = DateTime.Now,
                        TrangThai = "CHT",

                    };
                    _db.TrangThaiHoSo.Add(lichSuHoSo);
                    _db.SaveChanges();

                    //Kết thúc Xử lý phần lịch sử hồ sơ 

                    return RedirectToAction("Index", "GiaSpDvCuThe", new { request.Madv });
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

        [Route("GiaSpDvCuThe/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.thongtin", "Delete"))
                {
                    var model = _db.GiaSpDvCuThe.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaSpDvCuThe.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.GiaSpDvCuTheCt.Where(t => t.Mahs == model.Mahs);
                    _db.GiaSpDvCuTheCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaSpDvCuThe", new { model.Madv });
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

        [Route("GiaSpDvCuThe/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.thongtin", "Edit"))
                {
                    var model = _db.GiaSpDvCuThe.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_ct = _db.GiaSpDvCuTheCt.Where(t => t.Mahs == model.Mahs);
                    model.GiaSpDvCuTheCt = model_ct.ToList();
                    ViewData["Madv"] = model.Madv;
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();

                    // Xử lý phần Forech theo mã nhóm khi chọn
                    var groupmanhom = _db.GiaSpDvCuTheCt.Where(t => t.Mahs == model.Mahs).Select(item => item.Manhom).Distinct().ToList();
                    ViewData["GroupMaNhom"] = groupmanhom;
                    ViewData["GiaSpDvCuTheNhom"] = _db.GiaSpDvCuTheNhom.ToList();
                    ViewData["GiaSpDvCuTheDm"] = _db.GiaSpDvCuTheDm.ToList();
                    // End xử lý phần Forech theo mã nhóm khi chọn

                    ViewData["Madv"] = model.Madv;
                    ViewData["Ipf1"] = model.Ipf1;
                    ViewData["Title"] = "Bảng giá tính sản phẩm dịch vụ cụ thể";
                    ViewData["MenuLv1"] = "menu_spdvcuthe";
                    ViewData["MenuLv2"] = "menu_spdvcuthe_thongtin"; 
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCuThe/Modify.cshtml", model);

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

        //[Route("DinhGiaSpDvCuThe/Show")]
        //[HttpGet]
        //public IActionResult Show(string Mahs)
        //{
        //    if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
        //    {
        //        if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.thongtin", "Edit"))
        //        {
        //            var model = _db.GiaSpDvCuThe.FirstOrDefault(t => t.Mahs == Mahs);
        //            if (model.CodeExcel != "")
        //            {
        //                ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
        //                ViewData["DsDonVi"] = _db.DsDonVi.ToList();
        //                ViewData["Title"] = "Bảng giá tính sản phẩm dịch vụ cụ thể";                      
        //                return View("Views/Admin/Manages/DinhGia/GiaSpDvCuThe/ShowExcel.cshtml", model);
        //            }
        //            var model_new = new VMDinhGiaSpDvCuThe
        //            {
        //                Madv = model.Madv,
        //                Mahs = model.Mahs,
        //                Madiaban = model.Madiaban,
        //                Soqd = model.Soqd,
        //                Phanloaidv = model.Phanloai,
        //                Thoidiem = model.Thoidiem,
        //                Thongtin = model.Thongtin,
        //                Ghichu = model.Ghichu
        //            };

        //            var model_ct = _db.GiaSpDvCuTheCt.Where(t => t.Mahs == model_new.Mahs);

        //            model_new.GiaSpDvCuTheCt = model_ct.ToList();

        //            ViewData["Madv"] = model.Madv;
        //            ViewData["Mahs"] = model.Mahs;
        //            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
        //            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
        //            ViewData["GiaSpDvCuTheDm"] = _db.GiaSpDvCuTheDm.ToList();
        //            ViewData["PhanLoaiDichVu"] = _db.GiaSpDvCuTheCt.ToList();
        //            ViewData["Title"] = "Thông tin chi tiết sản phẩm dịch vụ cụ thể";
        //            ViewData["MenuLv1"] = "menu_spdvcuthe";
        //            ViewData["MenuLv2"] = "menu_spdvcuthe_thongtin";
        //            return View("Views/Admin/Manages/DinhGia/GiaSpDvCuThe/Show.cshtml", model_new);
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


        [Route("DinhGiaSpDvCuThe/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.thongtin", "Edit"))
                {
                    var model = _db.GiaSpDvCuThe.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_ct = _db.GiaSpDvCuTheCt.Where(t => t.Mahs == model.Mahs);
                    model.GiaSpDvCuTheCt = model_ct.ToList();
                    ViewData["Madv"] = model.Madv;
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();

                    // Xử lý phần Forech theo mã nhóm khi chọn
                    var groupmanhom = _db.GiaSpDvCuTheCt.Where(t => t.Mahs == model.Mahs).Select(item => item.Manhom).Distinct().ToList();
                    ViewData["GroupMaNhom"] = groupmanhom;
                    ViewData["GiaSpDvCuTheNhom"] = _db.GiaSpDvCuTheNhom.ToList();
                    ViewData["GiaSpDvCuTheDm"] = _db.GiaSpDvCuTheDm.ToList();
                    // End xử lý phần Forech theo mã nhóm khi chọn

                    model.GiaSpDvCuTheCt = model_ct.ToList();             
                   
                    ViewData["Ipf1"] = model.Ipf1;
                    ViewData["Title"] = "Bảng giá tính sản phẩm dịch vụ cụ thể";
                    ViewData["MenuLv1"] = "menu_spdvcuthe";
                    ViewData["MenuLv2"] = "menu_spdvcuthe_thongtin";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCuThe/Show.cshtml", model);

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

        [Route("DinhGiaSpDvCuThe/Update")]
        [HttpPost]
        public async Task<IActionResult> Update(CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCuThe request, IFormFile Ipf1)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.datcuthe.thongtin", "Edit"))
                {

                    if (Ipf1 != null && Ipf1.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf1.FileName);
                        string extension = Path.GetExtension(Ipf1.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf1.CopyToAsync(FileStream);
                        }
                        request.Ipf1 = filename;
                    }

                    var model = _db.GiaSpDvCuThe.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Madiaban = request.Madiaban;
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Thongtin = request.Thongtin;
                    model.Ipf1 = request.Ipf1;
                    model.GhiChu = request.GhiChu;
                    model.Updated_at = DateTime.Now;
                    _db.GiaSpDvCuThe.Update(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaSpDvCuTheCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Mahs = model.Mahs;
                        }
                    }
                    _db.GiaSpDvCuTheCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaSpDvCuThe", new { request.Mahs });
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

        [Route("DinhGiaSpDvCuThe/Print")]
        [HttpGet]
        public IActionResult Print(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.thongtin", "Index"))
                {
                    var model = _db.GiaSpDvCuThe.FirstOrDefault(t => t.Mahs == Mahs);

                    var hoso_dg = new VMDinhGiaPrint
                    {
                        Id = model.Id,

                        Phanloaidv = model.Phanloai,
                        Mahs = model.Mahs,
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Ghichu = model.GhiChu,
                    };

                    var modeldv = _db.DsDonVi.FirstOrDefault(t => t.MaDv == model.Madv);

                    if (modeldv != null)
                    {
                        hoso_dg.Tendv = modeldv.TenDvHienThi;
                    }

                    var modeldb = _db.DsDiaBan.FirstOrDefault(t => t.MaDiaBan == modeldv.MaDiaBan);
                    if (modeldb != null)
                    {
                        hoso_dg.Tendb = modeldb.TenDiaBan;
                    }

                    var modelct = _db.GiaSpDvCuTheCt.Where(t => t.Mahs == model.Mahs);
                    if (modelct != null)
                    {
                        hoso_dg.GiaSpDvCuTheCt = modelct.ToList();
                    }

                    /*var model = GetThongTinKk(Mahs);*/


                    ViewData["Title"] = "In định giá đât cụ thể";
                    ViewData["MenuLv1"] = "menu_spdvcuthe";
                    ViewData["MenuLv2"] = "menu_spdvcuthe_thongtin";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCuThe/Print.cshtml", hoso_dg);

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

        public IActionResult Complete(string mahs_chuyen, string macqcq_chuyen)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.thongtin", "Index"))
                {
                    var model = _db.GiaSpDvCuThe.FirstOrDefault(t => t.Mahs == mahs_chuyen);

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
                    _db.GiaSpDvCuThe.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaSpDvCuThe", new { model.Madv });

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

        [Route("GiaSpDvCuThe/TimKiem")]
        [HttpGet]
        public IActionResult Search()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.thongtin", "Index"))
                {

                    if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") != null)
                    {
                        ViewData["Madv"] = Helpers.GetSsAdmin(HttpContext.Session, "Madv");
                    }
                    else
                    {
                        ViewData["Madv"] = "";
                    }
               
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá sản phẩm dịch vụ cụ thể";
                    ViewData["MenuLv1"] = "menu_spdvcuthe";
                    ViewData["MenuLv2"] = "menu_spdvcuthe_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCuThe/TimKiem/Index.cshtml");

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

        [Route("GiaSpDvCuThe/TimKiem/KetQua")]
        [HttpPost]
        public IActionResult Result(string madv, string tenhanghoa, DateTime ngaynhap_tu, DateTime ngaynhap_den, double beginPrice, double endPrice)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.spdvcuthe.thongtin", "Index"))
                {

                    var model = (from giaspdvcuthect in _db.GiaSpDvCuTheCt
                                 join giaspdvcuthe in _db.GiaSpDvCuThe on giaspdvcuthect.Mahs equals giaspdvcuthe.Mahs
                                 join donvi in _db.DsDonVi on giaspdvcuthe.Madv equals donvi.MaDv
                                 select new GiaSpDvCuTheCt
                                 {
                                     Id = giaspdvcuthect.Id,
                                     Dvt = giaspdvcuthect.Dvt,
                                     Mahs = giaspdvcuthect.Mahs,
                                     Madv = giaspdvcuthe.Madv,
                                     Thoidiem = giaspdvcuthe.Thoidiem,
                                     Tendv = donvi.TenDv,
                                     Soqd = giaspdvcuthe.Soqd,
                                     TenSpDv = giaspdvcuthect.TenSpDv,
                                     Mucgia1 = giaspdvcuthect.Mucgia1,
                                     Mucgia2 = giaspdvcuthect.Mucgia2,                                     
                                     Maspdv = giaspdvcuthect.Maspdv,
                                 });

                    ViewData["GiaSpDvCuTheDm"] = _db.GiaSpDvCuTheDm;
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

                    

                

                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá sản phẩm dịch vụ cụ thể";
                    ViewData["MenuLv1"] = "menu_spdvcuthe";
                    ViewData["MenuLv2"] = "menu_spdvcuthe_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCuThe/TimKiem/Result.cshtml", model);
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
