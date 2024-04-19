
using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaThueTaiNguyen
{
    public class TtGiaDatDiaBanController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public TtGiaDatDiaBanController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("TtGiaDatDiaBan")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giadatdb.thongtu", "Index"))
                {
                    var model = _db.GiaDatDiaBanTt.ToList();

                    ViewData["Title"] = "Thông tư giá đất theo địa bàn";
                    ViewData["MenuLv1"] = "menu_giadat";
                    ViewData["MenuLv2"] = "menu_giadatdiaban";
                    ViewData["MenuLv3"] = "menu_giadatdiaban_t4";
                    return View("Views/Admin/Manages/DinhGia/GiaDatDiaBan/TtGiaDatDiaBan/Index.cshtml", model);
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

        [Route("TtGiaDatDiaBan/Store")]
        [HttpPost]
        public async Task<JsonResult> Store(IFormFile Ipf1upload, string Soqd, DateTime Ngayqd_apdung, DateTime Ngayqd_banhanh, string Mota)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giadatdb.thongtu", "Create"))
                {
                    string filename = "";
                    if (Ipf1upload != null)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string extension = Path.GetExtension(Ipf1upload.FileName);
                        if (Helpers.CheckFileType(extension))
                        {
                            if (Helpers.CheckFileSize(Ipf1upload.Length))
                            {
                                filename = DateTime.Now.ToString("yymmssfff") + extension;
                                string path = Path.Combine(wwwRootPath + "/UpLoad/File/DinhGia/", filename);
                                using (var FileStream = new FileStream(path, FileMode.Create))
                                {
                                    await Ipf1upload.CopyToAsync(FileStream);
                                    FileStream.Close();
                                }

                                var model = new GiaDatDiaBanTt
                                {
                                    Soqd = Soqd,
                                    Mota = Mota,
                                    Ipf1 = filename,
                                    Ngayqd_banhanh = Ngayqd_banhanh,
                                    Ngayqd_apdung = Ngayqd_apdung,
                                    Created_at = DateTime.Now,
                                    Updated_at = DateTime.Now,
                                };
                                _db.GiaDatDiaBanTt.Add(model);
                                _db.SaveChanges();

                                var data = new { status = "success", message = "Thêm mới thành công" };
                                return Json(data);
                            }
                            else
                            {
                                var data = new { status = "error", message = "File tải lên dung lượng không vượt quá 50MB!" };
                                return Json(data);
                            }
                        }
                        else
                        {
                            var data = new { status = "error", message = "File tải lên không đúng định dạng (.jpg, jpeg, png, doc, docx, xls, xlsx, pdf)!" };
                            return Json(data);
                        }
                    }
                    else
                    {
                        var model = new GiaDatDiaBanTt
                        {
                            Soqd = Soqd,
                            Mota = Mota,
                            Ipf1 = filename,
                            Ngayqd_banhanh = Ngayqd_banhanh,
                            Ngayqd_apdung = Ngayqd_apdung,
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        };
                        _db.GiaDatDiaBanTt.Add(model);
                        _db.SaveChanges();
                        var data = new { status = "success" };
                        return Json(data);
                    }
                }
                else
                {
                    var data = new { status = "error", message = "Bạn không có quyền thực hiện chức năng này!!!" };
                    return Json(data);
                }
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }

        [Route("TtGiaDatDiaBan/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giadatdb.thongtu", "Edit"))
                {
                    var model = _db.GiaDatDiaBanTt.FirstOrDefault(p => p.Id == Id);
                    if (model != null)
                    {
                        string result = "<div class='row' id='edit_thongtin'>";


                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group'>";
                        result += "<label>Số quyết định<span class='text-danger'>*</span></label>";
                        result += "<input type='text' id='soqd_edit' name='soqd_edit' class='form-control' value='" + model.Soqd + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-6'>";
                        result += "<div class='form-group'>";
                        result += "<label>Ngày ban hành</label>";
                        result += "<input type='date' id='ngayqd_banhanh_edit' name='ngayqd_banhanh_edit' class='form-control' value='" + model.Ngayqd_banhanh.ToString("yyyy-MM-dd") + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-6'>";
                        result += "<div class='form-group'>";
                        result += "<label>Ngày áp dụng</label>";
                        result += "<input type='date' id='ngayqd_apdung_edit' name='ngayqd_apdung_edit' class='form-control' value='" + model.Ngayqd_apdung.ToString("yyyy-MM-dd") + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group'>";
                        result += "<label>Mô tả<span class='text-danger'>*</span></label>";
                        result += "<input type='text' id='mota_edit' name='mota_edit' class='form-control' value='" + model.Mota + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-6'>";
                        result += "<div class='form-group'>";
                        result += "<label class='control-label'>File đính kèm</label>";
                        result += "<input  type='file' id='ipf1_edit' name='ipf1_edit'/>";
                        result += "</div>";
                        result += "</div>";

                        if (!string.IsNullOrEmpty(model.Ipf1))
                        {
                            result += "<br>";
                            result += "<a href='/UpLoad/File/DinhGia/" + model.Ipf1 + "' target='_blank' class='btn btn-link'";
                            result += " onclick='window.open(`/UpLoad/File/DinhGia/" + model.Ipf1 + "`, `mywin`, `left=20,top=20,width=500,height=500,toolbar=1,resizable=0`); return false;'>";
                            result += "<i class='icon-lg la la-eye text-success''></i>File đính kèm hiện tại</a>";
                            result += "<br>";
                        }

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
                else
                {
                    var data = new { status = "error", message = "Bạn không có quyền thực hiện chức năng này!!!" };
                    return Json(data);
                }
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }

        [Route("TtGiaDatDiaBan/Update")]
        [HttpPost]
        public async Task<JsonResult> Update(int Id, IFormFile Ipf1upload, string Soqd, DateTime Ngayqd_apdung, DateTime Ngayqd_banhanh, string Mota)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giadatdb.thongtu", "Create"))
                {
                    string filename = "";
                    if (Ipf1upload != null)
                    {
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string extension = Path.GetExtension(Ipf1upload.FileName);
                        if (Helpers.CheckFileType(extension))
                        {
                            if (Helpers.CheckFileSize(Ipf1upload.Length))
                            {
                                filename = DateTime.Now.ToString("yymmssfff") + extension;
                                string path = Path.Combine(wwwRootPath + "/UpLoad/File/DinhGia/", filename);
                                using (var FileStream = new FileStream(path, FileMode.Create))
                                {
                                    await Ipf1upload.CopyToAsync(FileStream);
                                    FileStream.Close();
                                }
                                var model = _db.GiaDatDiaBanTt.FirstOrDefault(t => t.Id == Id);

                                model.Soqd = Soqd;
                                model.Mota = Mota;
                                model.Ipf1 = filename;
                                model.Ngayqd_banhanh = Ngayqd_banhanh;
                                model.Ngayqd_apdung = Ngayqd_apdung;
                                model.Created_at = DateTime.Now;
                                model.Updated_at = DateTime.Now;

                                _db.GiaDatDiaBanTt.Update(model);
                                _db.SaveChanges();

                                var data = new { status = "success", message = "Thêm mới thành công" };
                                return Json(data);
                            }
                            else
                            {
                                var data = new { status = "error", message = "File tải lên dung lượng không vượt quá 50MB!" };
                                return Json(data);
                            }
                        }
                        else
                        {
                            var data = new { status = "error", message = "File tải lên không đúng định dạng (.jpg, jpeg, png, doc, docx, xls, xlsx, pdf)!" };
                            return Json(data);
                        }
                    }
                    else
                    {
                        var model = _db.GiaDatDiaBanTt.FirstOrDefault(t => t.Id == Id);
                        model.Soqd = Soqd;
                        model.Mota = Mota;
                        model.Ipf1 = filename;
                        model.Ngayqd_banhanh = Ngayqd_banhanh;
                        model.Ngayqd_apdung = Ngayqd_apdung;
                        model.Created_at = DateTime.Now;
                        model.Updated_at = DateTime.Now;
                        _db.GiaDatDiaBanTt.Update(model);
                        _db.SaveChanges();

                        var data = new { status = "success", message = "Cập nhật thành công" };
                        return Json(data);
                    }
                }
                else
                {
                    var data = new { status = "error", message = "Bạn không có quyền thực hiện chức năng này!!!" };
                    return Json(data);
                }
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }

        [Route("TtGiaDatDiaBan/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.giadat.giadatdb.thongtu", "Delete"))
                {
                    var model = _db.GiaDatDiaBanTt.FirstOrDefault(p => p.Id == id_delete);
                    _db.GiaDatDiaBanTt.Remove(model);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "TtGiaDatDiaBan");
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

