using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Linq;
using CSDLGia_ASP.ViewModels.Systems;
using CSDLGia_ASP.Models.Systems;
using OfficeOpenXml;
using System.Text.RegularExpressions;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaNuocSinhHoat
{
    public class GiaNuocShController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        private readonly IWebHostEnvironment _hostEnvironment;

        public GiaNuocShController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("GiaNuocSinhHoat")]
        [HttpGet]
        public IActionResult Index(string Madv, int Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.thongtin", "Index"))
                {
                    var dsdonvi = (from db in _db.DsDiaBan.Where(t => t.Level != "H")
                                   join dv in _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI") on db.MaDiaBan equals dv.MaDiaBan
                                   select new VMDsDonVi
                                   {
                                       Id = dv.Id,
                                       TenDiaBan = db.TenDiaBan,
                                       TenDv = dv.TenDv,
                                       MaDiaBan = dv.MaDiaBan,
                                       MaDv = dv.MaDv,
                                   });

                    Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;

                    if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") != null)
                    {
                        Madv = Helpers.GetSsAdmin(HttpContext.Session, "Madv");
                    }

                    IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaNuocSh> model = _db.GiaNuocSh;
                    if (Nam != 0)
                    {
                        model = model.Where(t => t.Thoidiem.Year == Nam);
                    }
                    if (Madv != "all")
                    {
                        model = _db.GiaNuocSh.Where(t => t.Madv == Madv);
                    }

                    ViewData["DsDonVi"] = dsdonvi;

                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Nam"] = Nam;
                    ViewData["Madv"] = Madv;
                    ViewData["TenDv"] = Madv != "all" ? dsdonvi.First(t => t.MaDv == Madv).TenDv : "all";
                    ViewData["Title"] = " Thông tin giá nước sạch sinh hoạt";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgnsh";
                    ViewData["MenuLv3"] = "menu_dgnsh_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaNuocSh/Index.cshtml", model);
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


        [HttpGet("GiaNuocSh/Create")]
        public IActionResult Create(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.thongtin", "Create"))
                {
                    var check = _db.GiaNuocShCt.Where(t => t.Trangthai == "CXD" && t.Madv == Madv);
                    if (check.Any())
                    {
                        _db.GiaNuocShCt.RemoveRange(check);
                        _db.SaveChanges();
                    }

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaNuocSh
                    {
                        Madv = Madv,
                        Thoidiem = DateTime.Now,
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Tunam = DateTime.Now.Year.ToString(),
                        Dennam = DateTime.Now.Year.ToString(),
                        PhanLoaiHoSo = "HOSOCHITIET",
                    };

                    var dm = _db.GiaNuocShDmKhung;
                    var ct = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaNuocShCt>();
                    foreach (var item in dm)
                    {
                        ct.Add(new CSDLGia_ASP.Models.Manages.DinhGia.GiaNuocShCt()
                        {
                            Mahs = model.Mahs,
                            Madv = Madv,
                            STTSapxep = item.STTSapxep,
                            STTHienthi = item.STTHienthi,
                            Doituongsd = item.Doituongsd,
                            Style = item.Style,
                            Trangthai = "CXD",
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        }) ;
                    }

                    _db.GiaNuocShCt.AddRange(ct);
                    _db.SaveChanges();

                    model.GiaNuocShCt = ct.Where(t => t.Mahs == model.Mahs).ToList();

                    ViewData["Madv"] = Madv;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Danhmuc"] = _db.GiaNuocShDmVung.ToList();
                    ViewData["Title"] = "Thêm mới giá nước sạch sinh hoạt";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgnsh";
                    ViewData["MenuLv3"] = "menu_dgnsh_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaNuocSh/Create.cshtml", model);
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

        [HttpGet("GiaNuocSh/NhanExcel")]
        public IActionResult NhanExcel(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.thongtin", "Create"))
                {
                    var check = _db.GiaNuocShCt.Where(t => t.Trangthai == "CXD" && t.Madv == Madv);
                    if (check.Any())
                    {
                        _db.GiaNuocShCt.RemoveRange(check);
                        _db.SaveChanges();
                    } 
                    var model = new CSDLGia_ASP.ViewModels.VMImportExcel {
                        MaDv = Madv,
                        LineStart = 2,
                        LineStop = 100,
                        Sheet = 1
                    };

                    ViewData["Madv"] = Madv;                 
                    ViewData["Title"] = "Thêm mới giá nước sạch sinh hoạt";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgnsh";
                    ViewData["MenuLv3"] = "menu_dgnsh_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaNuocSh/Excels/Excel.cshtml", model);
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

        [HttpPost]
        public async Task<IActionResult> ImportExcel (CSDLGia_ASP.ViewModels.VMImportExcel requests)
        {
            requests.LineStart = requests.LineStart == 0 ? 1 : requests.LineStart;
            int sheet = requests.Sheet == 0 ? 0 : (requests.Sheet - 1);
            var list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaNuocShCt>();
            string Mahs = requests.MaDv + "_" + DateTime.Now.ToString("yyMMddssmmHH");
            using (var stream = new MemoryStream())
            {
                await requests.FormFile.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[sheet];
                    var rowcount = worksheet.Dimension.Rows;
                    requests.LineStop = requests.LineStop > rowcount ? rowcount : requests.LineStop;
                    Regex trimmer = new Regex(@"\s\s+"); // Xóa khoảng trắng thừa trong chuỗi
                    int line = 1;
                    for (int row = requests.LineStart; row <= requests.LineStop; row++)
                    {
                        list_add.Add(new CSDLGia_ASP.Models.Manages.DinhGia.GiaNuocShCt
                        {
                            Mahs = Mahs,
                            Madv = requests.MaDv,
                            Trangthai = "CXD",
                            STTSapxep = line,
                            STTHienthi = worksheet.Cells[row, 1].Value != null ?
                                        worksheet.Cells[row, 1].Value.ToString().Trim() : "",
                            Doituongsd = worksheet.Cells[row, 2].Value != null ?
                                        worksheet.Cells[row, 2].Value.ToString().Trim() : "",
                            TyTrongTieuThu = worksheet.Cells[row, 3].Value != null ?
                                        worksheet.Cells[row, 3].Value.ToString().Trim() : "",
                            SanLuong = worksheet.Cells[row, 4].Value != null ?
                                        worksheet.Cells[row, 4].Value.ToString().Trim() : "",
                            DonGia1 = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 5].Value != null ?
                                        worksheet.Cells[row, 5].Value.ToString().Trim() : ""),
                            DonGia2 = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 6].Value != null ?
                                        worksheet.Cells[row, 6].Value.ToString().Trim() : "")                           
                        });
                        line = line + 1;
                    }
                }
            }
            _db.GiaNuocShCt.AddRange(list_add);
            _db.SaveChanges();
            var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaNuocSh
            {
                Madv = requests.MaDv,
                Thoidiem = DateTime.Now,
                Mahs = Mahs,
                Tunam = DateTime.Now.Year.ToString(),
                Dennam = DateTime.Now.Year.ToString(),                
            };
            var modelct = _db.GiaNuocShCt.Where(t => t.Mahs == Mahs);
            model.GiaNuocShCt = modelct.ToList();
            ViewData["Madv"] = requests.MaDv;
            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["Danhmuc"] = _db.GiaNuocShDmVung.ToList();
           
            ViewData["Title"] = "Thêm mới giá nước sạch sinh hoạt";
            ViewData["MenuLv1"] = "menu_dg";
            ViewData["MenuLv2"] = "menu_dgnsh";
            ViewData["MenuLv3"] = "menu_dgnsh_tt";
            return View("Views/Admin/Manages/DinhGia/GiaNuocSh/Create.cshtml", model);
        }

        [Route("GiaNuocSh/Store")]
        [HttpPost]
        public async Task<IActionResult> Store(CSDLGia_ASP.Models.Manages.DinhGia.GiaNuocSh request, IFormFile Ipf1upload, IFormFile Ipf2upload, IFormFile Ipf3upload, IFormFile Ipf4upload, IFormFile Ipf5upload)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.thongtin", "Create"))
                {
                    if (Ipf1upload != null && Ipf1upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf1upload.FileName);
                        string extension = Path.GetExtension(Ipf1upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/GiaNuocSh/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf1upload.CopyToAsync(FileStream);
                        }
                        request.Ipf1 = filename;
                    }

                    if (Ipf2upload != null && Ipf2upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf2upload.FileName);
                        string extension = Path.GetExtension(Ipf2upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/GiaNuocSh/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf2upload.CopyToAsync(FileStream);
                        }
                        request.Ipf2 = filename;
                    }

                    if (Ipf3upload != null && Ipf3upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf3upload.FileName);
                        string extension = Path.GetExtension(Ipf3upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/GiaNuocSh/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf3upload.CopyToAsync(FileStream);
                        }
                        request.Ipf3 = filename;
                    }

                    if (Ipf4upload != null && Ipf4upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf4upload.FileName);
                        string extension = Path.GetExtension(Ipf4upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/GiaNuocSh/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf4upload.CopyToAsync(FileStream);
                        }
                        request.Ipf4 = filename;
                    }

                    if (Ipf5upload != null && Ipf5upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf5upload.FileName);
                        string extension = Path.GetExtension(Ipf5upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/GiaNuocSh/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf5upload.CopyToAsync(FileStream);
                        }
                        request.Ipf5 = filename;
                    }                  

                    var model = new CSDLGia_ASP.Models.Manages.DinhGia.GiaNuocSh
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Madiaban = request.Madiaban,
                        Soqd = request.Soqd,
                        Thoidiem = request.Thoidiem,
                        Mota = request.Mota,
                        Ghichu = request.Ghichu,
                        Tunam = request.Tunam,
                        Dennam = request.Dennam,
                        Ipf1 = request.Ipf1,
                        Ipf2 = request.Ipf2,
                        Ipf3 = request.Ipf3,
                        Ipf4 = request.Ipf4,
                        Ipf5 = request.Ipf5,
                        PhanLoaiHoSo = request.PhanLoaiHoSo,
                        CodeExcel = request.CodeExcel,
                        Trangthai = "CHT",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaNuocSh.Add(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaNuocShCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Trangthai = "XD";
                        }
                    }
                    _db.GiaNuocShCt.UpdateRange(modelct);
                    _db.SaveChanges();


                    return RedirectToAction("Index", "GiaNuocSh", new { request.Madv });
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

        [Route("GiaNuocSh/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.thongtin", "Edit"))
                {
                    var model = _db.GiaNuocSh.FirstOrDefault(t => t.Mahs == Mahs);

                    var model_ct = _db.GiaNuocShCt.Where(t => t.Mahs == model.Mahs);

                    model.GiaNuocShCt = model_ct.ToList();

                    ViewData["Madv"] = model.Madv;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["Title"] = "Chỉnh sửa giá nước sinh hoạt";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgnsh";
                    ViewData["MenuLv3"] = "menu_dgnsh_tt";                 
                    return View("Views/Admin/Manages/DinhGia/GiaNuocSh/Edit.cshtml", model);
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

        [Route("GiaNuocSh/Update")]
        [HttpPost]
        public async Task<IActionResult> Update(CSDLGia_ASP.Models.Manages.DinhGia.GiaNuocSh request, IFormFile Ipf1upload, IFormFile Ipf2upload, IFormFile Ipf3upload, IFormFile Ipf4upload, IFormFile Ipf5upload)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.thongtin", "Edit"))
                {
                    if (Ipf1upload != null && Ipf1upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf1upload.FileName);
                        string extension = Path.GetExtension(Ipf1upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/GiaNuocSh/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf1upload.CopyToAsync(FileStream);
                        }
                        request.Ipf1 = filename;
                    }

                    if (Ipf2upload != null && Ipf2upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf2upload.FileName);
                        string extension = Path.GetExtension(Ipf2upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/GiaNuocSh/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf2upload.CopyToAsync(FileStream);
                        }
                        request.Ipf2 = filename;
                    }

                    if (Ipf3upload != null && Ipf3upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf3upload.FileName);
                        string extension = Path.GetExtension(Ipf3upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/GiaNuocSh/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf3upload.CopyToAsync(FileStream);
                        }
                        request.Ipf3 = filename;
                    }

                    if (Ipf4upload != null && Ipf4upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf4upload.FileName);
                        string extension = Path.GetExtension(Ipf4upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/GiaNuocSh/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf4upload.CopyToAsync(FileStream);
                        }
                        request.Ipf4 = filename;
                    }

                    if (Ipf5upload != null && Ipf5upload.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf5upload.FileName);
                        string extension = Path.GetExtension(Ipf5upload.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/GiaNuocSh/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf5upload.CopyToAsync(FileStream);
                        }
                        request.Ipf5 = filename;
                    }

                    var model = _db.GiaNuocSh.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Madiaban = request.Madiaban;
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Thongtin = request.Thongtin;
                    model.Ghichu = request.Ghichu;
                    model.Mota = request.Mota;
                    model.Ipf1 = request.Ipf1;
                    model.Ipf2 = request.Ipf2;
                    model.Ipf3 = request.Ipf3;
                    model.Ipf4 = request.Ipf4;
                    model.Ipf5 = request.Ipf5;
                    model.Updated_at = DateTime.Now;
                    _db.GiaNuocSh.Update(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaNuocShCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Mahs = model.Mahs;
                        }
                    }
                    _db.GiaNuocShCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaNuocSh", new { request.Madv });
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

        [Route("GiaNuocSh/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.thongtin", "Delete"))
                {
                    var model = _db.GiaNuocSh.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaNuocSh.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.GiaNuocShCt.Where(t => t.Mahs == model.Mahs);
                    _db.GiaNuocShCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaNuocSh", new { model.Madv });
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

        [Route("GiaNuocSh/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.thongtin", "Edit"))
                {
                    var model = _db.GiaNuocSh.FirstOrDefault(t => t.Mahs == Mahs);
                    //if (model.CodeExcel != "")
                    //{
                    //    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    //    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    //    ViewData["Title"] = "Xem chi tiết giá nước sinh hoạt";
                    //    ViewData["MenuLv1"] = "menu_dg";
                    //    ViewData["MenuLv2"] = "menu_dgtmdmn";
                    //    ViewData["MenuLv3"] = "menu_dgtmdmn_tt";
                    //    return View("Views/Admin/Manages/DinhGia/GiaNuocSh/ShowExcel.cshtml", model);
                    //}
                    model.GiaNuocShCt = _db.GiaNuocShCt.Where(t => t.Mahs == model.Mahs).ToList();

                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["Title"] = "Xem chi tiết giá nước sinh hoạt";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgnsh";
                    ViewData["MenuLv3"] = "menu_dgnsh_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaNuocSh/Show.cshtml", model);
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

        [Route("GiaNuocSh/Printf")]
        [HttpGet]
        public IActionResult Printf(string Nam, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.thongtin", "Index"))
                {
                    var dsdonvi = (from db in _db.DsDiaBan.Where(t => t.Level != "H")
                                   join dv in _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI") on db.MaDiaBan equals dv.MaDiaBan
                                   select new VMDsDonVi
                                   {
                                       Id = dv.Id,
                                       TenDiaBan = db.TenDiaBan,
                                       TenDv = dv.TenDv,
                                       MaDiaBan = dv.MaDiaBan,
                                       MaDv = dv.MaDv,
                                   }).ToList();

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

                    var model = _db.GiaNuocSh.Where(t => t.Madv == Madv).ToList();

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

                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgnsh";
                    ViewData["MenuLv3"] = "menu_dgnsh_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaNuocSh/Printf.cshtml", model);

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

        public IActionResult Complete(string mahs_complete, string macqcq_chuyen)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.xetduyet", "Approve"))
                {
                    var model = _db.GiaNuocSh.FirstOrDefault(p => p.Mahs == mahs_complete);

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
                    _db.GiaNuocSh.Update(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Gianuocsh", new { Madv = model.Madv, Nam = model.Thoidiem.Year });
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

        [Route("GiaNuocSh/Search")]
        [HttpGet]
        public IActionResult Search()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.thongtin", "Index"))
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
                    ViewData["Doituong"] = _db.GiaNuocShDmVung.ToList();
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá nước sinh hoạt";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgnsh";
                    ViewData["MenuLv3"] = "menu_dgnsh_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaNuocSh/TimKiem/Index.cshtml");

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

        [Route("GiaNuocSh/Result")]
        [HttpPost]
        public IActionResult Result(DateTime beginTime, DateTime endTime, double beginPrice, double endPrice, string Madoituong, string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.nuocsh.thongtin", "Edit"))
                {
                    var model = from dgct in _db.GiaNuocShCt
                                join dg in _db.GiaNuocSh on dgct.Mahs equals dg.Mahs
                                join donvi in _db.DsDonVi on dg.Madv equals donvi.MaDv
                                select new GiaNuocShCt
                                {                
                                    Doituongsd = dgct.Doituongsd,
                                    TyTrongTieuThu = dgct.TyTrongTieuThu,
                                    SanLuong = dgct.SanLuong,
                                    DonGia1 = dgct.DonGia1,
                                    DonGia2 = dgct.DonGia2,
                                    Namchuathue = donvi.TenDv,

                                    Thoidiem = dg.Thoidiem,                                    
                                };


                    if (Madv != "All")
                    {
                        model = model.Where(t => t.Madv == Madv);
                    }

                    if (Madoituong != "All")
                    {
                        model = model.Where(t => t.Madoituong == Madoituong);
                    }

                    if (beginTime.ToString("yyMMdd") != "010101")
                    {
                        model = model.Where(t => t.Thoidiem >= beginTime);
                    }

                    if (endTime.ToString("yyMMdd") != "010101")
                    {
                        model = model.Where(t => t.Thoidiem <= endTime);
                    }
                    model = model.Where(t => t.DonGia1 >= beginPrice);
                    if(beginPrice < endPrice)
                    {
                        model = model.Where(t => t.DonGia1 <= endPrice);
                    }
                   

                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Doituong"] = _db.GiaNuocShDmVung.ToList();
                    ViewData["Title"] = "Tìm kiếm thông tin hồ sơ giá nước sinh hoạt";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgnsh";
                    ViewData["MenuLv3"] = "menu_dgnsh_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaNuocSh/TimKiem/Result.cshtml", model);
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
