using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public GiaoDucDaoTaoController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DinhGiaGdDt")]
        [HttpGet]
        public IActionResult Index(string Madv, int Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaoducdaotao.thongtin", "Index"))
                {
                    var dsdonvi = (from db in _db.DsDiaBan.Where(t => t.Level != "H")
                                   join dv in _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI") on db.MaDiaBan equals dv.MaDiaBan
                                   select new VMDsDonVi
                                   {
                                       Id = dv.Id,
                                       TenDiaBan = db.TenDiaBan,
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


                        var model = _db.GiaDvGdDt.Where(t => t.Madv == Madv);


                        if (Nam != 0)
                        {
                            model = model.Where(t => t.Thoidiem.Year == Nam);
                        }

                        ViewData["Madv"] = Madv;
                        ViewData["Nam"] = Nam;
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                        ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                        ViewData["Title"] = " Thông tin hồ sơ giá dịch vụ giáo dục đào tạo";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dggddt";
                        ViewData["MenuLv3"] = "menu_dggddt_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaoDucDaoTao/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Messages"] = "Thông tin hồ sơ giá dịch vụ giáo dục đào tạo.";
                        ViewData["Title"] = " Thông tin hồ sơ giá dịch vụ giáo dục đào tạo";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dggddt";
                        ViewData["MenuLv3"] = "menu_dggddt_tt";
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
            var list_add = new List<CSDLGia_ASP.Models.Manages.DinhGia.GiaDvGdDtCt>();
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
                        });
                     
                    }
                }
            }
            _db.GiaDvGdDtCt.AddRange(list_add);
            _db.SaveChanges();
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
                        Trangthai = "CHT",
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
                    _db.GiaDvGdDtCt.UpdateRange(modelct);
                    _db.SaveChanges();

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
                    _db.GiaDvGdDtCt.UpdateRange(modelct);
                    _db.SaveChanges();

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
                    var model = _db.GiaDvGdDt.FirstOrDefault(t => t.Mahs == Mahs);                   
                    ViewData["GiaDvGdDtCt"] = _db.GiaDvGdDtCt.Where(t => t.Mahs == Mahs).ToList();

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

        public IActionResult Search()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaoducdaotao.thongtin", "Index"))
                {
                    ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");

                    ViewData["Tendv"] = _db.GiaDvGdDtDm.ToList();

                    ViewData["Title"] = "Tìm kiếm thông tin định giá giáo dục đào tạo";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dggddt";
                    ViewData["MenuLv3"] = "menu_dggddt_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaoDucDaoTao/TimKiem/Index.cshtml");
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
        [Route("DinhGiaGdDt/Result")]
        [HttpPost]
        public IActionResult Result(DateTime beginTime, DateTime endTime, string dv, string text_search)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dinhgia.giaoducdaotao.thongtin", "Edit"))
                {
                    var model_join = from dgct in _db.GiaDvGdDtCt
                                     join dg in _db.GiaDvGdDt on dgct.Mahs equals dg.Mahs    
                                     join dsdv in _db.DsDonVi on dg.Madv equals dsdv.MaDv
                                     select new VMDinhGiaDvGdDt
                                     {
                                         Mahs = dg.Mahs,
                                         Madv = dg.Madv,
                                         Macqcq = dg.Macqcq,
                                         Thoidiem = dg.Thoidiem,
                                         Maspdv = dgct.Maspdv,
                                         Mota = dgct.Mota,

                                         Giathanhthi1 = dgct.Giathanhthi1,
                                         Gianongthon1 = dgct.Gianongthon1,
                                         Giamiennui1 = dgct.Giamiennui1,
                                         TenDv = dsdv.TenDv,

                                         
                                     };
                    text_search = string.IsNullOrEmpty(text_search) ? "" : text_search;
                    if (text_search != "")
                    {
                        model_join = model_join.Where(t => t.Mota.Contains(text_search));
                    }
                    if (dv != "All")
                    {
                        model_join = model_join.Where(t => t.Madv == dv);
                    }
                    if (beginTime.ToString("yyMMdd") != "010101")
                    {
                        model_join = model_join.Where(t => t.Thoidiem >= beginTime);
                    }
                    if (endTime.ToString("yyMMdd") != "010101")
                    {
                        model_join = model_join.Where(t => t.Thoidiem <= endTime);
                    }



                    ViewData["Tendv"] = _db.GiaDvGdDtDm.ToList();
                    ViewData["dsdonvi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");

                    ViewData["Title"] = "Kết quả tìm kiếm thông tin định giá giáo dục đào tạo";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dggddt";
                    ViewData["MenuLv3"] = "menu_dggddt_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaoDucDaoTao/TimKiem/Result.cshtml", model_join);
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
