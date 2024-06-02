using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.Services;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaoDucDaoTao
{
    public class GiaoDucDaoTaoController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IDsDonviService _dsDonviService;
        private readonly ITrangThaiHoSoService _trangThaiHoSoService;


        public GiaoDucDaoTaoController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment, IDsDonviService dsDonviService, ITrangThaiHoSoService trangThaiHoSoService)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
            _dsDonviService = dsDonviService;
            _trangThaiHoSoService = trangThaiHoSoService;
        }

        [Route("DinhGiaGdDt")]
        [HttpGet]
        public IActionResult Index(string Madv, int Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaoducdaotao.thongtin", "Index"))
                {
                    Madv = string.IsNullOrEmpty(Madv) ? Helpers.GetSsAdmin(HttpContext.Session, "Madv") : Madv;
                    var model_donvi = _dsDonviService.GetListDonvi(Helpers.GetSsAdmin(HttpContext.Session, "Madv"));
                    List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();

                    IEnumerable<CSDLGia_ASP.Models.Manages.DinhGia.GiaDvGdDt> model = _db.GiaDvGdDt;

                    if (Madv != "all")
                    {
                        model = model.Where(t => t.Madv == Madv);
                    }

                    if (Nam != 0)
                    {
                        model = model.Where(t => t.Thoidiem.Year == Nam);
                    }

                    ViewData["Madv"] = Madv;
                    ViewData["Nam"] = Nam;
                    ViewData["DsDonvi"] = model_donvi;
                    ViewData["Title"] = " Thông tin hồ sơ giá dịch vụ giáo dục đào tạo";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dggddt";
                    ViewData["MenuLv3"] = "menu_dggddt_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaoDucDaoTao/Index.cshtml", model);

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


        [Route("DinhGiaGdDt/Create")]
        [HttpGet]
        public IActionResult Create(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaoducdaotao.thongtin", "Create"))
                {

                    var modelcxd = _db.GiaDvGdDtCt.Where(t => t.Gc == "CXD" && t.Madv == Madv).ToList();
                    if (modelcxd != null)
                    {
                        _db.GiaDvGdDtCt.RemoveRange(modelcxd);

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

                    var model = new VMDinhGiaDvGdDt
                    {
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Madv = Madv,
                        Thoidiem = DateTime.Now,
                    };
                    var dm = _db.GiaDvGdDtDm.ToList();
                    var chitiet = new List<GiaDvGdDtCt>();
                    foreach (var item in dm)
                    {
                        chitiet.Add(new GiaDvGdDtCt()
                        {
                            Mahs = model.Mahs,
                            MaNhom = item.MaNhom,
                            Madv = model.Madv,
                            Mota = item.Tenspdv,
                            Trangthai = "CXD",
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        });
                    }
                    _db.GiaDvGdDtCt.AddRange(chitiet);
                    _db.SaveChanges();

                    model.GiaDvGdDtCt = _db.GiaDvGdDtCt.Where(t => t.Mahs == model.Mahs).ToList();

                    ViewData["NhomDM"] = _db.GiaDvGdDtNhom;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["GiaDvGdDtDm"] = _db.GiaDvGdDtDm.ToList();
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["Title"] = "Thêm mới giá dịch vụ giáo dục đào tạo";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dggddt";
                    ViewData["MenuLv3"] = "menu_dggddt_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaoDucDaoTao/Create.cshtml", model);
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


        [Route("DinhGiaGdDt/NhanExcel")]
        [HttpGet]
        public IActionResult NhanExcel(string Madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaoducdaotao.thongtin", "Create"))
                {
                    var modelcxd = _db.GiaDvGdDtCt.Where(t => t.Gc == "CXD" && t.Madv == Madv).ToList();
                    if (modelcxd != null)
                    {
                        _db.GiaDvGdDtCt.RemoveRange(modelcxd);

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

                    var model = new CSDLGia_ASP.ViewModels.VMImportExcel
                    {
                        MaDv = Madv,
                        Sheet = 1,
                        LineStart = 2,
                        LineStop = 6
                    };
                    ViewData["Title"] = "Thêm mới giá dịch vụ giáo dục đào tạo";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dggddt";
                    ViewData["MenuLv3"] = "menu_dggddt_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaoDucDaoTao/Excels/Excel.cshtml", model);
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
        public async Task<IActionResult> ImportExcel(CSDLGia_ASP.ViewModels.VMImportExcel requests)
        {
            requests.LineStart = requests.LineStart == 0 ? 1 : requests.LineStart;
            int sheet = requests.Sheet == 0 ? 0 : (requests.Sheet - 1);
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
                    var list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaDvGdDtCt>();
                    for (int row = requests.LineStart; row <= requests.LineStop; row++)
                    {
                        list_add.Add(new CSDLGia_ASP.Models.Manages.DinhGia.GiaDvGdDtCt
                        {
                            Mahs = Mahs,
                            Madv = requests.MaDv,
                            Trangthai = "CXD",
                            Mota = worksheet.Cells[row, 1].Value != null ?
                                        worksheet.Cells[row, 1].Value.ToString().Trim() : "",
                            Giathanhthi1 = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 2].Value != null ?
                                        worksheet.Cells[row, 2].Value.ToString().Trim() : ""),
                            Gianongthon1 = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 3].Value != null ?
                                        worksheet.Cells[row, 3].Value.ToString().Trim() : ""),
                            Giamiennui1 = Helper.Helpers.ConvertStrToDb(worksheet.Cells[row, 4].Value != null ?
                                        worksheet.Cells[row, 4].Value.ToString().Trim() : ""),
                            MaNhom = worksheet.Cells[row, 5].Value != null ?
                                        worksheet.Cells[row, 5].Value.ToString().Trim() : "",
                        });
                    }
                    _db.GiaDvGdDtCt.AddRange(list_add);
                    _db.SaveChanges();
                }
            }
            var model = new CSDLGia_ASP.ViewModels.Manages.DinhGia.VMDinhGiaDvGdDt
            {
                Madv = requests.MaDv,
                Thoidiem = DateTime.Now,
                Mahs = Mahs,
                Tunam = DateTime.Now.Year.ToString(),
                Dennam = DateTime.Now.Year.ToString(),
            };
            var modelct = _db.GiaDvGdDtCt.Where(t => t.Mahs == Mahs);
            model.GiaDvGdDtCt = modelct.ToList();

            ViewData["NhomDM"] = _db.GiaDvGdDtNhom;
            ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
            ViewData["GiaDvGdDtDm"] = _db.GiaDvGdDtDm.ToList();
            ViewData["Mahs"] = model.Mahs;
            ViewData["Title"] = "Thêm mới giá dịch vụ giáo dục đào tạo";
            ViewData["MenuLv1"] = "menu_dg";
            ViewData["MenuLv2"] = "menu_dggddt";
            ViewData["MenuLv3"] = "menu_dggddt_tt";
            return View("Views/Admin/Manages/DinhGia/GiaoDucDaoTao/Create.cshtml", model);
        }

        [Route("DinhGiaGdDt/Store")]
        [HttpPost]
        public IActionResult Store(VMDinhGiaDvGdDt request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaoducdaotao.thongtin", "Create"))
                {

                    var model = new GiaDvGdDt
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Madiaban = request.Madiaban,
                        Soqd = request.Soqd,
                        Thoidiem = request.Thoidiem,
                        Tunam = request.Tunam,
                        Dennam = request.Dennam,
                        Mota = request.Mota,
                        PhanLoaiHoSo = request.PhanLoaiHoSo,
                        CodeExcel = request.CodeExcel,
                        Trangthai = "CC",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.GiaDvGdDt.Add(model);

                    var modelct = _db.GiaDvGdDtCt.Where(t => t.Mahs == request.Mahs);
                    foreach (var item in modelct)
                    {
                        item.Trangthai = "XD";
                    }
                    var modelFile = _db.ThongTinGiayTo.Where(t => t.Mahs == request.Mahs);
                    if (modelFile.Any())
                    {
                        foreach (var file in modelFile) { file.Status = "XD"; }
                    }
                    _db.GiaDvGdDtCt.UpdateRange(modelct);
                    _db.SaveChanges();
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Thêm mới");

                    return RedirectToAction("Index", "GiaoDucDaoTao", new { Madv = request.Madv });
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

        [Route("DinhGiaGdDt/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaoducdaotao.thongtin", "Delete"))
                {
                    var model = _db.GiaDvGdDt.FirstOrDefault(t => t.Id == id_delete);
                    var model_ct = _db.GiaDvGdDtCt.Where(t => t.Mahs == model.Mahs);
                    _db.GiaDvGdDtCt.RemoveRange(model_ct);
                    _db.GiaDvGdDt.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "GiaoDucDaoTao", new { Madv = model.Madv });
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


        [Route("DinhGiaGdDt/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaoducdaotao.thongtin", "Edit"))
                {
                    var model = _db.GiaDvGdDt.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new VMDinhGiaDvGdDt
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        Madiaban = model.Madiaban,
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Tunam = model.Tunam,
                        Dennam = model.Dennam,
                        Mota = model.Mota
                    };

                    var model_ct = _db.GiaDvGdDtCt.Where(t => t.Mahs == model_new.Mahs);
                    model_new.GiaDvGdDtCt = model_ct.ToList();
                    model_new.ThongTinGiayTo = _db.ThongTinGiayTo.Where(t => t.Mahs == Mahs).ToList();

                    ViewData["NhomDM"] = _db.GiaDvGdDtNhom;
                    ViewData["Madv"] = model.Madv;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["GiaDvGdDtDm"] = _db.GiaDvGdDtDm.ToList();
                    ViewData["Title"] = "Chỉnh sửa giá dịch vụ giáo dục đào tạo";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dggddt";
                    ViewData["MenuLv3"] = "menu_dggddt_tt";

                    return View("Views/Admin/Manages/DinhGia/GiaoDucDaoTao/Edit.cshtml", model_new);
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

        [Route("DinhGiaGdDt/Update")]
        [HttpPost]
        public IActionResult Update(VMDinhGiaDvGdDt request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaoducdaotao.thongtin", "Edit"))
                {
                    var model = _db.GiaDvGdDt.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Madiaban = request.Madiaban;
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Thongtin = request.Thongtin;
                    model.Mota = request.Mota;
                    model.Ghichu = request.Ghichu;
                    model.Updated_at = DateTime.Now;
                    _db.GiaDvGdDt.Update(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaDvGdDtCt.Where(t => t.Mahs == request.Mahs);
                    if (modelct.Any())
                    {
                        foreach (var item in modelct)
                        {
                            item.Trangthai = "XD";
                        }
                    }
                    var modelFile = _db.ThongTinGiayTo.Where(t => t.Mahs == request.Mahs);
                    if (modelFile.Any())
                    {
                        foreach (var file in modelFile) { file.Status = "XD"; }
                    }
                    _db.GiaDvGdDtCt.UpdateRange(modelct);
                    _db.SaveChanges();
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), "Cập nhật");
                    return RedirectToAction("Index", "GiaoDucDaoTao", new { Madv = request.Madv });
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

        [Route("DinhGiaGdDt/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaoducdaotao.thongtin", "Show"))
                {
                //    var model = _db.GiaDvGdDt.FirstOrDefault(t => t.Mahs == Mahs);
                //    ViewData["GiaDvGdDtCt"] = _db.GiaDvGdDtCt.Where(t => t.Mahs == Mahs).ToList();
                //    ViewData["DsNhom"] = _db.GiaDvGdDtNhom;
                    var model = _db.GiaDvGdDt.FirstOrDefault(t => t.Mahs == Mahs);
                    var modelct = _db.GiaDvGdDtCt.Where(t => t.Mahs == Mahs);
                    model.GiaDvGdDtCt = modelct.ToList();
                    ViewData["DsNhom"] = _db.GiaDvGdDtNhom;



                    ViewData["Madv"] = model.Madv;
                    ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDiaBan"] = _db.DsDiaBan.ToList();
                    ViewData["DsDonVi"] = _db.DsDonVi.ToList();
                    ViewData["Title"] = "Xem giá dịch vụ giáo dục đào tạo";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dggddt";
                    ViewData["MenuLv3"] = "menu_dggddt_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaoDucDaoTao/Show.cshtml", model);
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
        public IActionResult HoanThanh(string mahs_complete, string trangthai_complete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaoducdaotao.thongtin", "Approve"))
                {
                    var model = _db.GiaDvGdDt.FirstOrDefault(p => p.Mahs == mahs_complete);
                    model.Updated_at = DateTime.Now;
                    model.Trangthai = trangthai_complete;

                    _db.GiaDvGdDt.Update(model);
                    _db.SaveChanges();
                    //Add Log
                    _trangThaiHoSoService.LogHoSo(model.Mahs, Helpers.GetSsAdmin(HttpContext.Session, "Name"), trangthai_complete);
                    return RedirectToAction("Index", "GiaoDucDaoTao", new { Madv = model.Madv, Nam = model.Thoidiem.Year });
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

        [HttpGet("DinhGiaGdDt/Search")]
        public IActionResult Search(string Madv, string MaNhom, DateTime? NgayTu, DateTime? NgayDen, string Mahs, double DonGiaTu, double DonGiaDen, string Mota)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaoducdaotao.timkiem", "Index"))
                {
                    DateTime nowDate = DateTime.Now;
                    DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
                    DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);

                    Madv = string.IsNullOrEmpty(Madv) ? "all" : Madv;
                    MaNhom = string.IsNullOrEmpty(MaNhom) ? "all" : MaNhom;
                    NgayTu = NgayTu.HasValue ? NgayTu : firstDayCurrentYear;
                    NgayDen = NgayDen.HasValue ? NgayDen : lastDayCurrentYear;
                    Mahs = string.IsNullOrEmpty(Mahs) ? "all" : Mahs;
                    DonGiaTu = DonGiaTu == 0 ? 0 : DonGiaTu;
                    DonGiaDen = DonGiaDen == 0 ? 0 : DonGiaDen;
                    Mota = string.IsNullOrEmpty(Mota) ? "" : Mota;

                    var model = (from hosoct in _db.GiaDvGdDtCt
                                 join hoso in _db.GiaDvGdDt on hosoct.Mahs equals hoso.Mahs
                                 join nhom in _db.GiaDvGdDtNhom on hosoct.MaNhom equals nhom.MaNhom
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaDvGdDtCt
                                 {
                                     Madv = hoso.Madv,
                                     Tendv = donvi.TenDv,
                                     Tennhom = nhom.TenNhom,
                                     SoQD = hoso.Soqd,
                                     ThoiDiem = hoso.Thoidiem,
                                     Mota = hosoct.Mota,
                                     Giathanhthi1 = hosoct.Giathanhthi1,
                                     Gianongthon1 = hosoct.Gianongthon1,
                                     Giamiennui1 = hosoct.Giamiennui1,
                                     MaNhom = hosoct.MaNhom,
                                     Trangthai = hoso.Trangthai,
                                     Mahs = hoso.Mahs
                                 });

                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model = model.Where(t => t.ThoiDiem >= NgayTu && t.ThoiDiem <= NgayDen && t.Giathanhthi1 >= DonGiaTu || t.Gianongthon1 >= DonGiaTu || t.Giamiennui1 >= DonGiaTu
                                            && list_trangthai.Contains(t.Trangthai));
                    if (Madv != "all") { model = model.Where(t => t.Madv == Madv); }
                    if (MaNhom != "all") { model = model.Where(t => t.MaNhom == MaNhom); }
                    if (DonGiaDen > 0) { model = model.Where(t => t.Giathanhthi1 <= DonGiaDen || t.Gianongthon1 <= DonGiaDen || t.Giamiennui1 <= DonGiaDen); }
                    if (!string.IsNullOrEmpty(Mota))
                    {
                        model = model.Where(t => t.Mota.ToLower().Contains(Mota.ToLower()));
                    }

                    ViewData["Madv"] = Madv;
                    ViewData["MaNhom"] = MaNhom;
                    ViewData["NgayTu"] = NgayTu;
                    ViewData["NgayDen"] = NgayDen;
                    ViewData["Mahs"] = Mahs;
                    ViewData["DonGiaTu"] = Helpers.ConvertDbToStr(DonGiaTu);
                    ViewData["DonGiaDen"] = Helpers.ConvertDbToStr(DonGiaDen);
                    ViewData["Mota"] = Mota;
                    ViewData["DanhMucNhom"] = _db.GiaDvGdDtNhom;
                    ViewData["DanhSachHoSo"] = _db.GiaDvGdDt.Where(t => t.Thoidiem >= NgayTu && t.Thoidiem <= NgayDen && t.Trangthai == "HT");

                    ViewData["DsDiaBan"] = _db.DsDiaBan;
                    ViewData["Cqcq"] = _db.DsDonVi;


                    ViewData["Title"] = "Tìm kiếm thông tin định giá giáo dục đào tạo";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dggddt";
                    ViewData["MenuLv3"] = "menu_dggddt_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaoDucDaoTao/TimKiem/Search.cshtml", model);
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


        [Route("DinhGiaGdDt/PrintSearch")]
        [HttpGet]
        public IActionResult Print(string Madv, string MaNhom, DateTime? NgayTu, DateTime? NgayDen, string Mahs, double DonGiaTu, double DonGiaDen, string Mota)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaoducdaotao.timkiem", "Index"))
                {

                    var model = (from hosoct in _db.GiaDvGdDtCt
                                 join hoso in _db.GiaDvGdDt on hosoct.Mahs equals hoso.Mahs
                                 join nhom in _db.GiaDvGdDtNhom on hosoct.MaNhom equals nhom.MaNhom
                                 join donvi in _db.DsDonVi on hoso.Madv equals donvi.MaDv
                                 select new CSDLGia_ASP.Models.Manages.DinhGia.GiaDvGdDtCt
                                 {
                                     Madv = hoso.Madv,
                                     Tendv = donvi.TenDv,
                                     Tennhom = nhom.TenNhom,
                                     SoQD = hoso.Soqd,
                                     ThoiDiem = hoso.Thoidiem,
                                     Mota = hosoct.Mota,
                                     Giathanhthi1 = hosoct.Giathanhthi1,
                                     Gianongthon1 = hosoct.Gianongthon1,
                                     Giamiennui1 = hosoct.Giamiennui1,
                                     MaNhom = hosoct.MaNhom,
                                     Trangthai = hoso.Trangthai,
                                     Mahs = hoso.Mahs
                                 });

                    List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                    model = model.Where(t => t.ThoiDiem >= NgayTu && t.ThoiDiem <= NgayDen && t.Giathanhthi1 >= DonGiaTu || t.Gianongthon1 >= DonGiaTu || t.Giamiennui1 >= DonGiaTu
                                            && list_trangthai.Contains(t.Trangthai));
                    if (Madv != "all") { model = model.Where(t => t.Madv == Madv); }
                    if (MaNhom != "all") { model = model.Where(t => t.MaNhom == MaNhom); }
                    if (DonGiaDen > 0) { model = model.Where(t => t.Giathanhthi1 <= DonGiaDen || t.Gianongthon1 <= DonGiaDen || t.Giamiennui1 <= DonGiaDen); }
                    if (!string.IsNullOrEmpty(Mota))
                    {
                        model = model.Where(t => t.Mota.ToLower().Contains(Mota.ToLower()));
                    }

                    return View("Views/Admin/Manages/DinhGia/GiaoDucDaoTao/TimKiem/Result.cshtml", model);
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

        [HttpPost("DinhGiaGdDt/GetListHoSo")]
        public JsonResult GetListHoSo(DateTime ngaytu, DateTime ngayden)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                List<string> list_trangthai = new List<string> { "HT", "DD", "CB" };
                var model = _db.GiaDvGdDt.Where(t => t.Thoidiem >= ngaytu && t.Thoidiem <= ngayden && list_trangthai.Contains(t.Trangthai));
                string result = "<select class='form-control' id='Mahs_Search' name='Mahs_Search'>";
                result += "<option value='all'>--Tất cả---</option>";

                if (model.Any())
                {
                    foreach (var item in model)
                    {
                        result += "<option value='" + @item.Mahs + "'>Số QĐ: " + @item.Soqd + " - Thời điểm: " + @Helpers.ConvertDateToStr(item.Thoidiem) + "</option>";
                    }
                }
                result += "</select>";
                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Phiên đăng nhập kết thúc, Bạn cần đăng nhập lại!!!" };
                return Json(data);
            }
        }
    }
}
