
using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaSpDvCongIch
{
    public class GiaSpDvCongIchController : Controller
    {

        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public GiaSpDvCongIchController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        //1.Sau khi chọn menu chuyển vào đây
        //2.Thêm mới
        //3.Kiểm tra Nam/DsDv của tài khoản đăng nhập xuất bản nghi ra nếu không có thì năm lấy năm hiện tại và đơn vị đầu tiên trong DsDonVi
        //4.Donvi lấy dữ liệu từ dsdiaban lấy tất cả bản ghi

        [Route("GiaSpDvCongIch")]
        [HttpGet]
        public IActionResult Index(string Nam, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.thongtin", "Index"))
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

                        var model = _db.GiaSpDvCongIch.Where(t => t.Madv == Madv).ToList();

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
                        ViewData["NhomTn"] = _db.GiaSpDvCongIchNhom.ToList();
                        ViewData["Nam"] = Nam;
                        ViewData["Madv"] = Madv;
                        ViewData["Title"] = "Thông tin giá sản phẩm dịch vụ công ích";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgdvci";
                        ViewData["MenuLv3"] = "menu_dgdvci_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaSpDvCongIch/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Title"] = "Thông tin giá sản phẩm dịch vụ công ích";
                        ViewData["Messages"] = "Hệ thống chưa có định giá sản phẩm dịch vụ công ích.";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgdvci";
                        ViewData["MenuLv3"] = "menu_dgdvci_tt";
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

        [Route("GiaSpDvCongIch/Create")]
        [HttpGet]
        public IActionResult Create(string Manhom, string MadvBc)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.thongtin", "Create"))
                {

                    // hồ sơ mà chỉ đến bước hoàn thành mà quay lại thì sẽ có trạng thái CXD --> cần xóa
                    var check = _db.GiaSpDvCongIchCt.Where(t => t.Trangthai == "CXD");
                    if (check != null)
                    {
                        _db.GiaSpDvCongIchCt.RemoveRange(check);
                        _db.SaveChanges();
                    }

                    // Thông tin của bộ hồ sơ
                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCongIch
                    {
                        Mahs = MadvBc + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Madv = MadvBc,
                        Manhom = Manhom,
                    };

                    var danhmuc = _db.GiaSpDvCongIchDm.ToList(); // lấy dữ liệu trong bảng GiaSpDvCongIchDm


                    // Khi bấm đồng ý trong moda thì add dữ liệu GiaSpDvCongIchDm -> bản GiaSpDvCongIchCt
                    if (Manhom != "all")
                    {
                        danhmuc = danhmuc.Where(t => t.Manhom == Manhom).ToList();
                    }
                    else
                    {
                        danhmuc = danhmuc.ToList();
                    }

                    var chitiet = new List<GiaSpDvCongIchCt>();


                    foreach (var item in danhmuc)
                    {
                        chitiet.Add(new GiaSpDvCongIchCt()
                        {
                            Mahs = model.Mahs,
                            Maso = item.Maso,
                            Ten = item.Ten,
                            Magoc = item.Magoc,
                            Capdo = item.Capdo,
                            HienThi = item.HienThi,
                            Dvt = item.Dvt,
                            Mucgiatu = item.Mucgiatu,
                            Mucgiaden = item.Mucgiaden,
                            Manhom = item.Manhom,


                            Phanloaidv = item.Phanloai,
                            Trangthai = "CXD",
                            Maspdv = item.Maspdv,
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        });
                    }
                    _db.GiaSpDvCongIchCt.AddRange(chitiet);
                    _db.SaveChanges();


                    // Xử lý phần Forech theo mã nhóm khi chọn
                    var groupmanhom1 = _db.GiaSpDvCongIchCt.Select(item => item.Manhom).Distinct().ToList();
                    var groupmanhom2 = _db.GiaSpDvCongIchCt.Where(item => item.Manhom == Manhom).Select(item => item.Manhom).Distinct().ToList();

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
                    ViewData["GiaSpDvCongIchNhom"] = _db.GiaSpDvCongIchNhom.ToList();
                    // End xử lý phần Forech theo mã nhóm khi chọn

                    // Xử lý phần điều kiện để hiện lên nút sửa 
                    model.GiaSpDvCongIchCt = chitiet.Where(t => t.Mahs == model.Mahs).ToList();
                    foreach (var item in model.GiaSpDvCongIchCt)
                    {
                        var List = model.GiaSpDvCongIchCt.Where(t => t.Magoc == item.Maso).ToList();
                        if (List.Count > 0)
                        {
                            item.NhapGia = false;
                        }
                        else
                        {
                            item.NhapGia = true;
                        }
                    }
                    // End Xử lý phần điều kiện để hiện lên nút sửa 

                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "T");
                    ViewData["Manhom"] = Manhom;
                    ViewData["Madv"] = MadvBc;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["GiaSpDvCongIchDm"] = _db.GiaSpDvCongIchDm.ToList();
                    ViewData["Title"] = "Bảng giá sản phẩm dịch vụ công ích";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdvci";
                    ViewData["MenuLv3"] = "menu_dgdvci_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCongIch/Create.cshtml", model);

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

        [Route("GiaSpDvCongIch/Store")]
        [HttpPost]
        public async Task<IActionResult> Store(CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCongIch request, IFormFile Ipf1)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.thongtin", "Create"))
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

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCongIch
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Manhom = request.Manhom,
                        Madiaban = request.Madiaban,
                        Soqd = request.Soqd,
                        Ghichu = request.Ghichu,
                        Thoidiem = request.Thoidiem,
                        Thongtin = request.Thongtin,
                        Ipf1 = request.Ipf1,
                        Trangthai = "CHT",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaSpDvCongIch.Add(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaSpDvCongIchCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Trangthai = "XD";
                        }
                    }
                    _db.GiaSpDvCongIchCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaSpDvCongIch", new { request.Madv });
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

        [Route("GiaSpDvCongIch/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.thongtin", "Delete"))
                {
                    var model = _db.GiaSpDvCongIch.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaSpDvCongIch.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.GiaSpDvCongIchCt.Where(t => t.Mahs == model.Mahs);
                    _db.GiaSpDvCongIchCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaSpDvCongIch", new { model.Madv });
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

        [Route("GiaSpDvCongIch/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.thongtin", "Edit"))
                {
                    var model = _db.GiaSpDvCongIch.FirstOrDefault(t => t.Mahs == Mahs);

                    var model_ct = _db.GiaSpDvCongIchCt.Where(t => t.Mahs == model.Mahs);

                    model.GiaSpDvCongIchCt = model_ct.ToList();

                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Madv"] = model.Madv;
                    ViewData["Ipf1"] = model.Ipf1;


                    model.GiaSpDvCongIchCt = model_ct.Where(t => t.Mahs == model.Mahs).ToList();
                    foreach (var item in model.GiaSpDvCongIchCt)
                    {
                        var List = model.GiaSpDvCongIchCt.Where(t => t.Magoc == item.Maso).ToList();
                        if (List.Count > 0)
                        {
                            item.NhapGia = false;
                        }
                        else
                        {
                            item.NhapGia = true;
                        }
                    }

                    ViewData["GiaSpDvCongIchNhom"] = _db.GiaSpDvCongIchNhom.ToList();
                    ViewData["GiaSpDvCongIchDm"] = _db.GiaSpDvCongIchDm.ToList();
                    ViewData["Title"] = "Bảng giá tính sản phẩm dịch vụ công ích";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdvci";
                    ViewData["MenuLv3"] = "menu_dgdvci_tt";
                    var groupmanhom = _db.GiaSpDvCongIchCt.Select(item => item.Manhom).Distinct().ToList();
                    ViewData["GroupMaNhom"] = groupmanhom;
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCongIch/Modify.cshtml", model);

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

        [Route("GiaSpDvCongIch/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.thongtin", "Edit"))
                {
                    var model = _db.GiaSpDvCongIch.FirstOrDefault(t => t.Mahs == Mahs);

                    var model_ct = _db.GiaSpDvCongIchCt.Where(t => t.Mahs == model.Mahs);

                    model.GiaSpDvCongIchCt = model_ct.ToList();

                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Madv"] = model.Madv;
                    ViewData["Ipf1"] = model.Ipf1;


                    model.GiaSpDvCongIchCt = model_ct.Where(t => t.Mahs == model.Mahs).ToList();
                    foreach (var item in model.GiaSpDvCongIchCt)
                    {
                        var List = model.GiaSpDvCongIchCt.Where(t => t.Magoc == item.Maso).ToList();
                        if (List.Count > 0)
                        {
                            item.NhapGia = false;
                        }
                        else
                        {
                            item.NhapGia = true;
                        }
                    }
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["GiaSpDvCongIchNhom"] = _db.GiaSpDvCongIchNhom.ToList();
                    ViewData["GiaSpDvCongIchDm"] = _db.GiaSpDvCongIchDm.ToList();
                    ViewData["Title"] = "Bảng giá tính sản phẩm dịch vụ công ích";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdvci";
                    ViewData["MenuLv3"] = "menu_dgdvci_tt";
                    var groupmanhom = _db.GiaSpDvCongIchCt.Select(item => item.Manhom).Distinct().ToList();
                    ViewData["GroupMaNhom"] = groupmanhom;
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCongIch/Show.cshtml", model);

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
        //[Route("DinhGiaSpDvCongIch/Show")]
        //[HttpGet]
        //public IActionResult Show(string Mahs)
        //{
        //    if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
        //    {
        //        if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.thongtin", "Edit"))
        //        {
        //            var model = _db.GiaSpDvCongIch.FirstOrDefault(t => t.Mahs == Mahs);
        //            var model_new = new VMDinhGiaSpDvCongIch
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

        //            var model_ct = _db.GiaSpDvCongIchCt.Where(t => t.Mahs == model_new.Mahs);

        //            model_new.GiaSpDvCongIchCt = model_ct.ToList();

        //            ViewData["Madv"] = model.Madv;
        //            ViewData["Mahs"] = model.Mahs;
        //            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
        //            ViewData["DsDonVi"] = _db.DsDonVi.ToList();
        //            ViewData["GiaSpDvCongIchDm"] = _db.GiaSpDvCongIchDm.ToList();
        //            ViewData["PhanLoaiDichVu"] = _db.GiaSpDvCongIchCt.ToList();
        //            ViewData["Title"] = "Thông tin chi tiết sản phẩm dịch vụ công ích";
        //            ViewData["MenuLv1"] = "menu_dg";
        //            ViewData["MenuLv2"] = "menu_dgdvci";
        //            ViewData["MenuLv3"] = "menu_dgdvci_tt";
        //            return View("Views/Admin/Manages/DinhGia/GiaSpDvCongIch/Show.cshtml", model_new);
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

        [Route("DinhGiaSpDvCongIch/Update")]
        [HttpPost]
        public async Task<IActionResult> Update(CSDLGia_ASP.Models.Manages.DinhGia.GiaSpDvCongIch request, IFormFile Ipf1)
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

                    var model = _db.GiaSpDvCongIch.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Madiaban = request.Madiaban;
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Thongtin = request.Thongtin;
                    model.Ipf1 = request.Ipf1;
                    model.Ghichu = request.Ghichu;
                    model.Updated_at = DateTime.Now;
                    _db.GiaSpDvCongIch.Update(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaSpDvCongIchCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Mahs = model.Mahs;
                        }
                    }
                    _db.GiaSpDvCongIchCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaSpDvCongIch", new { request.Mahs });
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

        [Route("DinhGiaSpDvCongIch/Print")]
        [HttpGet]
        public IActionResult Print(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.thongtin", "Index"))
                {
                    var model = _db.GiaSpDvCongIch.FirstOrDefault(t => t.Mahs == Mahs);

                    var hoso_dg = new VMDinhGiaPrint
                    {
                        Id = model.Id,

                        Phanloaidv = model.Phanloai,
                        Mahs = model.Mahs,
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Ghichu = model.Ghichu,
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

                    var modelct = _db.GiaSpDvCongIchCt.Where(t => t.Mahs == model.Mahs);
                    if (modelct != null)
                    {
                        hoso_dg.GiaSpDvCongIchCt = modelct.ToList();
                    }

                    /*var model = GetThongTinKk(Mahs);*/


                    ViewData["Title"] = "In định giá sản phẩm dịch vụ công ích";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdvci";
                    ViewData["MenuLv3"] = "menu_dgdvci_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCongIch/Print.cshtml", hoso_dg);

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
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.thongtin", "Index"))
                {
                    var model = _db.GiaSpDvCongIch.FirstOrDefault(t => t.Mahs == mahs_chuyen);

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
                    _db.GiaSpDvCongIch.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaSpDvCongIch", new { model.Madv });

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

        [Route("GiaSpDvCongIch/TimKiem")]
        [HttpGet]
        public IActionResult Search()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.timkiem", "Index"))
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
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá sản phẩm dịch vụ công ích";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdvci";
                    ViewData["MenuLv3"] = "menu_dgdvci_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCongIch/TimKiem/Index.cshtml");

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

        [Route("GiaSpDvCongIch/TimKiem/KetQua")]
        [HttpPost]
        public IActionResult Result(string madv, string tenhanghoa, DateTime ngaynhap_tu, DateTime ngaynhap_den, double beginPrice, double endPrice)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.dichvucongich.timkiem", "Index"))
                {

                    var model = (from GiaSpDvCongIchct in _db.GiaSpDvCongIchCt
                                 join GiaSpDvCongIch in _db.GiaSpDvCongIch on GiaSpDvCongIchct.Mahs equals GiaSpDvCongIch.Mahs
                                 join donvi in _db.DsDonVi on GiaSpDvCongIch.Madv equals donvi.MaDv
                                 select new GiaSpDvCongIchCt
                                 {
                                     Id = GiaSpDvCongIchct.Id,
                                     Dvt = GiaSpDvCongIchct.Dvt,
                                     Mahs = GiaSpDvCongIchct.Mahs,
                                     Madv = GiaSpDvCongIch.Madv,
                                     Tendv = donvi.TenDv,
                                     Soqd = GiaSpDvCongIch.Soqd,

                                     Maso = GiaSpDvCongIchct.Maso,
                                     Ten = GiaSpDvCongIchct.Ten,
                                     Magoc = GiaSpDvCongIchct.Magoc,
                                     Capdo = GiaSpDvCongIchct.Capdo,
                                     HienThi = GiaSpDvCongIchct.HienThi,
                                     Mucgiatu = GiaSpDvCongIchct.Mucgiatu,
                                     Mucgiaden = GiaSpDvCongIchct.Mucgiaden,


                                 });

                    ViewData["GiaSpDvCongIchDm"] = _db.GiaSpDvCongIchDm;
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

                    if (tenhanghoa != null)
                    {
                        model = model.Where(t => t.Mota == tenhanghoa);
                    }

                    if (beginPrice != 0)
                    {
                        model = model.Where(t => t.Mucgiatu >= beginPrice);
                    }
                    if (endPrice != 0)
                    {
                        model = model.Where(t => t.Mucgiaden <= endPrice);
                    }

                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá sản phẩm dịch vụ công ích";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgdvci";
                    ViewData["MenuLv3"] = "menu_dgdvci_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaSpDvCongIch/TimKiem/Result.cshtml", model);
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
