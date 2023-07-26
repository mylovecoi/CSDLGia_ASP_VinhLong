using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using CSDLGia_ASP.Database;
using System.Security.Cryptography;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.ViewModels.Systems;
using CSDLGia_ASP.ViewModels.Manages.DinhGia;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDvKhamChuaBenh
{
    public class DvKhamChuaBenhController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        public DvKhamChuaBenhController(CSDLGiaDBContext db,IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("DinhGiaDvKcb")]
        [HttpGet]
        public IActionResult Index(string Madv, string Nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.gddt.ttg", "Index"))
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



                        var model = _db.GiaDvKcb.Where(t => t.Madv == Madv ).ToList();

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

                        ViewData["Madv"] = Madv;
                        ViewData["Nam"] = Nam;
                        ViewData["GiaDvKcbNhom"] = _db.GiaDvKcbNhom.ToList();
                        ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                        ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                        ViewData["Title"] = " Thông tin hồ sơ giá dịch vụ khám chữa bệnh";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgkcb";
                        ViewData["MenuLv3"] = "menu_dgkcb_tt";
                        return View("Views/Admin/Manages/DinhGia/GiaDvKhamChuaBenh/Index.cshtml", model);
                    }
                    else
                    {
                        ViewData["Messages"] = "Thông tin hồ sơ giá dịch vụ khám chữa bệnh.";
                        ViewData["Title"] = " Thông tin hồ sơ giá dịch vụ khám chữa bệnh";
                        ViewData["MenuLv1"] = "menu_dg";
                        ViewData["MenuLv2"] = "menu_dgkcb";
                        ViewData["MenuLv3"] = "menu_dgkcb_tt";
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


        [Route("DinhGiaDvKcb/Create")]
        [HttpGet]
        public IActionResult Create(string Madv, string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.kcb.ttg", "Create"))
                {

                    var modelcxd = _db.GiaDvKcbCt.Where(t => t.Ghichu == "CXD").ToList();
                    if (modelcxd != null)
                    {

                        _db.GiaDvKcbCt.RemoveRange(modelcxd);

                        _db.SaveChanges();
                    }
                    var model = new VMDinhGiaDvKcb
                    {
                        Mahs = Madv + "_" + DateTime.Now.ToString("yyMMddssmmHH"),
                        Madv = Madv,
                        Manhom = Manhom,
                        Thoidiem = DateTime.Now,
                    };
                    var nhom = _db.GiaDvKcbDm.Where(t => t.Manhom == Manhom);
                    var chitiet = new List<GiaDvKcbCt>();
                    foreach (var item in nhom)
                    {
                        chitiet.Add(new GiaDvKcbCt()
                        {
                            Mahs = model.Mahs,
                            Maspdv = item.Maspdv,
                            Tenspdv = item.Tenspdv,
                            Madichvu=item.Madichvu,
                            Dvt=item.Dvt,
                            Phanloai=item.Phanloai,
                            Ghichu = "CXD",
                            Giadv = 0,
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        });
                    }
                    _db.GiaDvKcbCt.AddRange(chitiet);
                    _db.SaveChanges();
                    model.GiaDvKcbCt = _db.GiaDvKcbCt.Where(t => t.Mahs == model.Mahs).ToList();


                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["GiaDvKcbNhom"] = _db.GiaDvKcbNhom.ToList();
                    ViewData["Title"] = "Thêm mới giá dịch vụ khám chữa bệnh";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgkcb";
                    ViewData["MenuLv3"] = "menu_dgkcb_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaDvKhamChuaBenh/Create.cshtml", model);
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


        [Route("DinhGiaDvKcb/Store")]
        [HttpPost]
        public async Task<IActionResult> Store(VMDinhGiaDvKcb request, IFormFile Ipf1, IFormFile Ipf2, IFormFile Ipf3, IFormFile Ipf4, IFormFile Ipf5)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.kcb.ttg", "Create"))
                {
                    var model = new GiaDvKcb
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Manhom = request.Manhom,
                        Soqd = request.Soqd,
                        Thoidiem = request.Thoidiem,
                        Mota = request.Mota,
                        Trangthai = "CHT",
                        Congbo = "CHUACONGBO",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
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
                        model.Ipf1 = filename;
                    }
                    if (Ipf2 != null && Ipf2.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf2.FileName);
                        string extension = Path.GetExtension(Ipf2.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf2.CopyToAsync(FileStream);
                        }
                        model.Ipf2 = filename;
                    }
                    if (Ipf3 != null && Ipf3.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf3.FileName);
                        string extension = Path.GetExtension(Ipf3.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf3.CopyToAsync(FileStream);
                        }
                        model.Ipf3 = filename;
                    }
                    if (Ipf4 != null && Ipf4.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf4.FileName);
                        string extension = Path.GetExtension(Ipf4.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf4.CopyToAsync(FileStream);
                        }
                        model.Ipf4 = filename;
                    }
                    if (Ipf5 != null && Ipf5.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf5.FileName);
                        string extension = Path.GetExtension(Ipf5.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf5.CopyToAsync(FileStream);
                        }
                        model.Ipf5 = filename;
                    }
                    _db.GiaDvKcb.Add(model);
                    _db.SaveChanges();

                    var modelct = _db.GiaDvKcbCt.Where(t => t.Mahs == request.Mahs).ToList();
                    if (modelct != null)
                    {
                        foreach (var item in modelct)
                        {
                            item.Ghichu = "XD";
                        }
                    }
                    _db.GiaDvKcbCt.UpdateRange(modelct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "DvKhamChuaBenh", new {Madv = request.Madv });
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


        [Route("DinhGiaDvKcb/EditCt")]
        [HttpPost]
        public JsonResult EditCt(int Id)
        {
            var model = _db.GiaDvKcbCt.FirstOrDefault(p => p.Id == Id);

            if (model != null)
            {
                string result = "<div class='modal-body' id='edit_thongtin'>";
                result += "<div class='row'>"; ;
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Tên dịch vụ</b></label>";
                result += "<input type='text' id='Tenspdv_edit' name='Tenspdv_edit' value='" + model.Tenspdv + "' class='form-control  />";
                result += "</div></div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label><b>Đơn giá</b></label>";
                result += "<input type='text' id='Giadv_edit' name='Giadv_edit'  value='" + model.Giadv + "' class='form-control money text-right' style='font-weight: bold' />";
                result += "</div></div></div>";
                result += "<input  id='id_edit' name='id_edit' value='" + Id + "'/>";
                result += "</div></div>";

                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Không tìm thấy thông tin cần chỉnh sửa!!!" };
                return Json(data);
            }
        }

        [Route("DinhGiaDvKcb/UpdateCt")]
        [HttpPost]
        public JsonResult UpdateCt(int Id, Double Giadv)
        {
            var model = _db.GiaDvKcbCt.FirstOrDefault(t => t.Id == Id);
            model.Giadv = Giadv;
            model.Updated_at = DateTime.Now;
            _db.GiaDvKcbCt.Update(model);
            _db.SaveChanges();
            string result = GetData(model.Mahs);
            var data = new { status = "success", message = result };
            return Json(data);
        }


        [Route("DinhGiaDvKcb/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.kcb.ttg", "Edit"))
                {
                    var model = _db.GiaDvKcb.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new VMDinhGiaDvKcb
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        Manhom = model.Manhom,
                        Ipf1 = model.Ipf1,
                        Ipf2 = model.Ipf2,
                        Ipf3 = model.Ipf3,
                        Ipf4 = model.Ipf4,
                        Ipf5 = model.Ipf5,
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Mota = model.Mota
                    };

                    var model_ct = _db.GiaDvKcbCt.Where(t => t.Mahs == model_new.Mahs);

                    model_new.GiaDvKcbCt = model_ct.ToList();

                    //ViewData["Madv"] = model.Madv;
                    //ViewData["Mahs"] = model.Mahs;
                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["GiaDvKcbNhom"] = _db.GiaDvKcbNhom.ToList();
                    ViewData["Title"] = "Chỉnh sửa giá dịch vụ giáo dục đào tạo";
                    ViewData["Ipf1"] = model.Ipf1;
                    ViewData["Ipf2"] = model.Ipf2;
                    ViewData["Ipf3"] = model.Ipf3;
                    ViewData["Ipf4"] = model.Ipf4;
                    ViewData["Ipf5"] = model.Ipf5;
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgkcb";
                    ViewData["MenuLv3"] = "menu_dgkcb_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaDvKhamChuaBenh/Edit.cshtml", model_new);
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

        [Route("DinhGiaDvKcb/Update")]
        [HttpPost]
        public async Task<IActionResult> Update(VMDinhGiaDvGdDt request, IFormFile Ipf1, IFormFile Ipf2, IFormFile Ipf3, IFormFile Ipf4, IFormFile Ipf5)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.kcb.ttg", "Edit"))
                {
                    var model = _db.GiaDvKcb.FirstOrDefault(t => t.Mahs == request.Mahs);
                    
                    model.Soqd = request.Soqd;
                    model.Thoidiem = request.Thoidiem;
                    model.Mota = request.Mota;
                    model.Updated_at = DateTime.Now;
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
                        model.Ipf1 = filename;
                    }
                    if (Ipf2 != null && Ipf2.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf2.FileName);
                        string extension = Path.GetExtension(Ipf2.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf2.CopyToAsync(FileStream);
                        }
                        model.Ipf2 = filename;
                    }
                    if (Ipf3 != null && Ipf3.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf3.FileName);
                        string extension = Path.GetExtension(Ipf3.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf3.CopyToAsync(FileStream);
                        }
                        model.Ipf3 = filename;
                    }
                    if (Ipf4 != null && Ipf4.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf4.FileName);
                        string extension = Path.GetExtension(Ipf4.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf4.CopyToAsync(FileStream);
                        }
                        model.Ipf4 = filename;
                    }
                    if (Ipf5 != null && Ipf5.Length > 0)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string filename = Path.GetFileNameWithoutExtension(Ipf5.FileName);
                        string extension = Path.GetExtension(Ipf5.FileName);
                        filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath + "/Upload/File/DinhGia/", filename);
                        using (var FileStream = new FileStream(path, FileMode.Create))
                        {
                            await Ipf5.CopyToAsync(FileStream);
                        }
                        model.Ipf5 = filename;
                    }
                    _db.GiaDvKcb.Update(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "DvKhamChuaBenh", new {Madv = request.Madv });
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


        [Route("DinhGiaDvKcb/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.kcb.ttg", "Delete"))
                {
                    var model = _db.GiaDvKcb.FirstOrDefault(t => t.Id == id_delete);
                    _db.GiaDvKcb.Remove(model);
                    _db.SaveChanges();

                    var model_ct = _db.GiaDvKcbCt.Where(t => t.Mahs == model.Mahs);
                    _db.GiaDvKcbCt.RemoveRange(model_ct);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "DvKhamChuaBenh", new {Madv = model.Madv });
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
        public string GetData(string Mahs)
        {
            var Model = _db.GiaDvKcbCt.Where(t => t.Mahs == Mahs).ToList();

            int record = 1;
            string result = "<div class='card-body' id='frm_data'>";

            result += "<table class='table table-striped table-bordered table-hover' id='datatable_4'>";
            result += "<thead>";
            result += "<tr style = 'text-align:center' >";
            result += "<th> STT </ th >";
            result += "<td>Mã dịch vụ</td>";
            result += "<th> Tên sản phẩm <br />dịch vụ</th>";
            result += "<th>Đơn vị <br/> tính</th>";
            result += "<th>Đơn giá</th>";
            result += "<th width='8%'>Thao tác</th>";
            result += "</tr>";
            result += "</thead>";

            result += "<tbody>";
            if (Model != null)
            {
                foreach (var item in Model)
                {
                    result += "<tr>";
                    result += "<td style='text-align:center'>" + (record++) + "</td>";
                    result += "<td style='text-align:center'>" + item.Madichvu + "</td>";
                    result += "<td style='text-align:center'>" + item.Tenspdv + "</td>";
                    result += "<td style='text-align:center'>" + item.Dvt + "</td>";
                    result += "<td style='text-align:center'>" + item.Giadv + "</td>";

                    result += "<td><button type='button' class='btn btn-sm btn-clean btn-icon' title='kê khai' data-toggle='modal'";
                    result += "data-target = '#Edit_Modal' onclick = 'SetEdit(`" + item.Id + "`)'>";
                    result += "<i class='icon-lg la la-edit text-warning'></i>";
                    result += "</button></td>";
                    result += "</tr>";
                }
            }

            result += "</ tbody >";
            result += "</ table >";
            result += "</div>";

            return result;
        }

        [Route("DinhGiaDvKcb/Show")]
        [HttpGet]
        public IActionResult Show(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.kcb.ttg", "Show"))
                {
                    var model = _db.GiaDvKcb.FirstOrDefault(t => t.Mahs == Mahs);
                    var model_new = new VMDinhGiaDvKcb
                    {
                        Madv = model.Madv,
                        Mahs = model.Mahs,
                        Manhom = model.Manhom,
                        Soqd = model.Soqd,
                        Thoidiem = model.Thoidiem,
                        Ipf1 = model.Ipf1,
                        Ipf2 = model.Ipf2,
                        Ipf3 = model.Ipf3,
                        Ipf4 = model.Ipf4,
                        Ipf5 = model.Ipf5,
                        Mota = model.Mota

                    };

                    var model_ct = _db.GiaDvKcbCt.Where(t => t.Mahs == model_new.Mahs);

                    model_new.GiaDvKcbCt = model_ct.ToList();

                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["GiaDvKcbNhom"] = _db.GiaDvKcbNhom.ToList();
                    ViewData["Title"] = "Chỉnh sửa giá dịch vụ khám chữa bệnh";
                    ViewData["Ipf2"] = model.Ipf2;
                    ViewData["Ipf3"] = model.Ipf3;
                    ViewData["Ipf4"] = model.Ipf4;
                    ViewData["Ipf5"] = model.Ipf5;
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgkcb";
                    ViewData["MenuLv3"] = "menu_dgkcb_tt";
                    return View("Views/Admin/Manages/DinhGia/GiaDvKhamChuaBenh/Show.cshtml", model_new);
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

        [Route("DinhGiaDvKcb/Search")]
        [HttpGet]
        public IActionResult Search()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.kcb.ttg", "Index"))
                {
                    ViewData["DsDiaBan"] = ViewData["DsDiaBan"] = _db.DsDiaBan.Where(t => t.Level != "H");
                    ViewData["Cqcq"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                   
                    ViewData["Tenspdv"] = _db.GiaDvKcbDm.ToList();

                    ViewData["Title"] = "Tìm kiếm thông tin định giá khám chữa bệnh";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgkcb";
                    ViewData["MenuLv3"] = "menu_dgkcb_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaDvKhamChuaBenh/TimKiem/Index.cshtml");
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
        [Route("DinhGiaDvKcb/Result")]
        [HttpPost]
        public IActionResult Result(double beginPrice, double endPrice, DateTime beginTime, DateTime endTime, string tsp, string dv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.dg.kcb.ttg", "Edit"))
                {
                    var model_join = from dgct in _db.GiaDvKcbCt.Where(t => t.Ghichu == "XD")
                                     join dg in _db.GiaDvKcb on dgct.Mahs equals dg.Mahs
                                     //join dgdm in _db.GiaDvGdDtDm on dgct.Maspdv equals dgdm.Maspdv
                                     select new VMDinhGiaDvKcb
                                     {
                                         Id = dg.Id,
                                         Mahs = dg.Mahs,
                                         Madv = dg.Madv,
                                         Macqcq = dg.Macqcq,
                                         Thoidiem = dg.Thoidiem,
                                         Maspdv = dgct.Maspdv,
                                         Tenspdv = dgct.Tenspdv,
                                         Giadv = dgct.Giadv,
                                     };

                    if (tsp != "All")
                    {
                        model_join = model_join.Where(t => t.Maspdv == tsp);
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
                    if (beginPrice != 0)
                    {
                        model_join = model_join.Where(t => t.Giadv >= beginPrice);
                    }
                    if (endPrice != 0)
                    {
                        model_join = model_join.Where(t => t.Giadv <= endPrice);
                    }


                   
                    ViewData["dsdonvi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");

                    ViewData["Title"] = "Kết quả tìm kiếm thông tin định giá khám chữa bệnh";
                    ViewData["MenuLv1"] = "menu_dg";
                    ViewData["MenuLv2"] = "menu_dgkcb";
                    ViewData["MenuLv3"] = "menu_dgkcb_tk";
                    return View("Views/Admin/Manages/DinhGia/GiaDvKhamChuaBenh/TimKiem/Result.cshtml", model_join);
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
