using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.VbQlNn
{
    public class VbQlNnController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IDsDonviService _dsDonviService;

        public VbQlNnController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment, IDsDonviService dsDonviService)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
            _dsDonviService = dsDonviService;
        }

        [Route("VanBanQlNnVeGia")]
        [HttpGet]
        public IActionResult Index(string Phanloai ="all", string Loaivb = "all")
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "vbqlnnvegiaplp.vbqlnn.ds", "Index"))
                {

                    var MaDonVi = Helpers.GetSsAdmin(HttpContext.Session, "Madv");
                    var model_donvi = _dsDonviService.GetListDonvi(MaDonVi);
                    List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();
                    var model = _db.VbQlNn.Where(x=>x.Mahs != null);
                    if (Phanloai != "all")
                    {
                        model = model.Where(x=>x.Phanloai == Phanloai);
                    }
                    if (Loaivb != "all")
                    {
                        model = model.Where(t => t.Loaivb == Loaivb);
                    }
                    ViewData["DsDonVi"] = model_donvi;
                    ViewData["Madv"] = MaDonVi;
                    ViewData["Phanloai"] = Phanloai;
                    ViewData["Loaivb"] = Loaivb;
                    ViewData["Title"] = "Văn bản quản lý nhà nước về giá";
                    ViewData["MenuLv1"] = "menu_vbql";
                    ViewData["MenuLv2"] = "menu_vbqlds";
                    return View("Views/Admin/Manages/VbQlNn/Index.cshtml", model);
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

        [Route("VanBanQlNnVeGia/Create")]
        [HttpGet]
        public IActionResult Create(string madv)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "vbqlnnvegiaplp.vbqlnn.ds", "Create"))
                {                    
                    // xóa giấy tờ đính kèm chưa xác định                    
                    var model_file_cxd = _db.ThongTinGiayTo.Where(x => x.Madv == madv && x.Status == "CXD");
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
                    }
                    _db.SaveChanges();
                    var model = new CSDLGia_ASP.Models.Manages.VbQlNn.VbQlNn()
                    {
                        Madv = madv,
                        Mahs = $"{madv}_VBQL_{DateTime.Now.ToString("yyMMddHHmmssff")}",
                        Thoidiem=DateTime.Now,
                        Ngaybanhanh=DateTime.Now,
                        Ngayapdung=DateTime.Now,
                    };
                    ViewData["Dsdonvi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Title"] = "Thêm mới văn bản quản lý nhà nước về giá";
                    ViewData["MenuLv1"] = "menu_vbql";
                    ViewData["MenuLv2"] = "menu_vbqlds";
                    return View("Views/Admin/Manages/VbQlNn/Create.cshtml",model);
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

        [Route("VanBanQlNnVeGia/Store")]
        [HttpPost]
        public  IActionResult Store(CSDLGia_ASP.Models.Manages.VbQlNn.VbQlNn request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "vbqlnnvegiaplp.vbqlnn.ds", "Create"))
                {                   

                    var model = new CSDLGia_ASP.Models.Manages.VbQlNn.VbQlNn
                    {
                        Mahs = request.Mahs,
                        Madv = request.Madv,
                        Phanloai = request.Phanloai,
                        Loaivb = request.Loaivb,
                        Dvbanhanh = request.Dvbanhanh,
                        Kyhieuvb = request.Kyhieuvb,
                        Thoidiem = request.Thoidiem,
                        Ngayapdung = request.Ngayapdung,
                        Ngaybanhanh = request.Ngaybanhanh,
                        Tieude = request.Tieude,
                        Ghichu = request.Ghichu,                        
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };
                    _db.VbQlNn.Add(model);
                    // Lưu lại thông tin giấy tờ chưa xác định
                    var giayto = _db.ThongTinGiayTo.Where(t => t.Mahs == model.Mahs && t.Status =="CXD").ToList();
                    if (giayto.Any())
                    {
                        giayto.ForEach(x => x.Status = "XD");
                    }
                    _db.SaveChanges();

                    return RedirectToAction("Index", "VbQlNn");
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

        [Route("VanBanQlNnVeGia/Edit")]
        [HttpGet]
        public IActionResult Edit(string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "vbqlnnvegiaplp.vbqlnn.ds", "Edit"))
                {
                    var model = _db.VbQlNn.FirstOrDefault(t => t.Mahs == Mahs);
                    model.ThongTinGiayTo = _db.ThongTinGiayTo.Where(x=>x.Mahs==model.Mahs).ToList();
                    ViewData["Dsdonvi"] = _db.DsDonVi.Where(t => t.ChucNang != "QUANTRI");
                    ViewData["Title"] = "Chỉnh sửa văn bản quản lý nhà nước về giá";
                    ViewData["MenuLv1"] = "menu_vbql";
                    ViewData["MenuLv2"] = "menu_vbqlds";
                    return View("Views/Admin/Manages/VbQlNn/Edit.cshtml", model);
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

        [Route("VanBanQlNnVeGia/Update")]
        [HttpPost]
        public  IActionResult Update(CSDLGia_ASP.Models.Manages.VbQlNn.VbQlNn request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "vbqlnnvegiaplp.vbqlnn.ds", "Edit"))
                {
                    

                    var model = _db.VbQlNn.FirstOrDefault(t => t.Mahs == request.Mahs);
                    model.Phanloai = request.Phanloai;
                    model.Loaivb = request.Loaivb;
                    model.Dvbanhanh = request.Dvbanhanh;
                    model.Kyhieuvb = request.Kyhieuvb;
                    model.Thoidiem = request.Thoidiem;
                    model.Ngayapdung = request.Ngayapdung;
                    model.Ngaybanhanh = request.Ngaybanhanh;
                    model.Tieude = request.Tieude;
                    model.Ghichu = request.Ghichu;                   
                    model.Updated_at = DateTime.Now;

                    _db.VbQlNn.Update(model);
                    // Lưu lại thông tin giấy tờ chưa xác định
                    var giayto = _db.ThongTinGiayTo.Where(t => t.Mahs == model.Mahs && t.Status == "CXD").ToList();
                    if (giayto.Any())
                    {
                        giayto.ForEach(x => x.Status = "XD");
                    }
                    _db.SaveChanges();

                    return RedirectToAction("Index", "VbQlNn");
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

        [Route("VanBanQlNnVeGia/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "vbqlnnvegiaplp.vbqlnn.ds", "Delete"))
                {
                    var model = _db.VbQlNn.FirstOrDefault(t => t.Id == id_delete);
                    _db.VbQlNn.Remove(model);
                    // xóa thông tin giấy tờ
                    var model_file_cxd = _db.ThongTinGiayTo.Where(x => x.Mahs == model.Mahs);
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
                    }
                    _db.SaveChanges();

                    return RedirectToAction("Index", "VbQlNn");
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

        [Route("VanBanQlNnVeGia/Show")]
        [HttpPost]
        public JsonResult Show(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.VbQlNn.FirstOrDefault(t => t.Id == Id);
                string result = "<div class='modal-body' id='frm_show'>";
                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label style='font-weight:bold'>Nội dung: </label>";
                result += "<span style='color:blue'>" + model.Tieude + "</span>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form -group fv-plugins-icon-container'>";
                result += "<label style='font-weight:bold'>File đính kèm</label>";

                if (model.Ipf1 != null && model.Ipf1.Length > 0)
                {
                    result += "<p>";
                    result += "-";
                    result += "<a href='/UpLoad/File/VbQlNn/" + model.Ipf1 + "' target='_blank' class='btn btn-link'";
                    result += " onclick='window.open(`/UpLoad/File/VbQlNn/" + model.Ipf1 + "`, `mywin`, `left=20,top=20,width=500,height=500,toolbar=1,resizable=0`); return false;'>";
                    result += model.Ipf1 + "</a>";
                    result += "</p>";
                }
                if (model.Ipf2 != null && model.Ipf2.Length > 0)
                {
                    result += "<p>";
                    result += "-";
                    result += "<a href='/UpLoad/File/VbQlNn/" + model.Ipf2 + "' target='_blank' class='btn btn-link'";
                    result += " onclick='window.open(`/UpLoad/File/VbQlNn/" + model.Ipf2 + "`, `mywin`, `left=20,top=20,width=500,height=500,toolbar=1,resizable=0`); return false;'>";
                    result += model.Ipf2 + "</a>";
                    result += "</p>";
                }
                if (model.Ipf3 != null && model.Ipf3.Length > 0)
                {
                    result += "<p>";
                    result += "-";
                    result += "<a href='/UpLoad/File/VbQlNn/" + model.Ipf3 + "' target='_blank' class='btn btn-link'";
                    result += " onclick='window.open(`/UpLoad/File/VbQlNn/" + model.Ipf3 + "`, `mywin`, `left=20,top=20,width=500,height=500,toolbar=1,resizable=0`); return false;'>";
                    result += model.Ipf3 + "</a>";
                    result += "</p>";
                }
                if (model.Ipf4 != null && model.Ipf4.Length > 0)
                {
                    result += "<p>";
                    result += "-";
                    result += "<a href='/UpLoad/File/VbQlNn/" + model.Ipf4 + "' target='_blank' class='btn btn-link'";
                    result += " onclick='window.open(`/UpLoad/File/VbQlNn/" + model.Ipf4 + "`, `mywin`, `left=20,top=20,width=500,height=500,toolbar=1,resizable=0`); return false;'>";
                    result += model.Ipf4 + "</a>";
                    result += "</p>";
                }
                if (model.Ipf5 != null && model.Ipf5.Length > 0)
                {
                    result += "<p>";
                    result += "-";
                    result += "<a href='/UpLoad/File/VbQlNn/" + model.Ipf5 + "' target='_blank' class='btn btn-link'";
                    result += " onclick='window.open(`/UpLoad/File/VbQlNn/" + model.Ipf5 + "`, `mywin`, `left=20,top=20,width=500,height=500,toolbar=1,resizable=0`); return false;'>";
                    result += model.Ipf5 + "</a>";
                    result += "</p>";
                }

                result += "</div>";
                result += "</div>";

                result += "</div>";
                result += "</div>";

                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }
    }
}
