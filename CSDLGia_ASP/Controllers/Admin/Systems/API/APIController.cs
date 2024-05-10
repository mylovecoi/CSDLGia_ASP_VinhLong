using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems.API;
using CSDLGia_ASP.ViewModels.Systems.CSDLQuocGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Text.RegularExpressions;

namespace CSDLGia_ASP.Controllers.Admin.Systems.API
{
    public class APIController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _env;

        public APIController(CSDLGiaDBContext db, IWebHostEnvironment hostingEnv)
        {
            _db = db;
            _env = hostingEnv;
        }

        [Route("KetNoiAPI/ThietLapChung")]
        public IActionResult ThietLapChung(string Phanloai)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.api.chung", "Index"))
                {
                    if (string.IsNullOrEmpty(Phanloai))
                    {
                        Phanloai = "Header";
                    }

                    var model = _db.KetNoiAPI.Where(t => t.Phanloai == Phanloai).OrderBy(t => t.Stt);
                    var stt = model.Count() + 1;
                    /*var VMGetAPIThietLap = Helper.Helpers.getAPIThietLapMacDinh();
                    ViewBag.VMGetAPIThietLap = (from t in VMGetAPIThietLap
                                        group t by t.Phanloai into grp
                                        select new { 
                                            phanloai=grp.Key
                                        }).ToList();*/
                    ViewData["Stt"] = stt;
                    ViewData["Phanloai"] = Phanloai;
                    ViewData["Title"] = "Thiết lập chung kết nối API";
                    ViewData["MenuLv1"] = "menu_hethong";
                    ViewData["MenuLv2"] = "menu_qthethong";
                    ViewData["MenuLv3"] = "menu_api";
                    ViewData["MenuLv4"] = "menu_api_chung";
                    return View("Views/Admin/Systems/API/ThietLapChung.cshtml", model);
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

        [Route("KetNoiAPI/ThietLapChung/Store")]
        [HttpPost]
        public JsonResult ThietLapChungStore(string Phanloai, string Tentruong, string Mota, string Kieudl,
            string Dinhdang, string Dodai, bool Batbuoc, string Macdinh, int Stt)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.api.chung", "Create"))
                {
                    int check = _db.KetNoiAPI.Where(t => t.Stt == Stt && t.Phanloai == Phanloai).Count();
                    if (check == 0)
                    {
                        var request = new KetNoiAPI
                        {
                            Phanloai = Phanloai,
                            Tendong = Tentruong,
                            Mota = Mota,
                            Kieudulieu = Kieudl,
                            Dinhdang = Dinhdang,
                            Dodai = Dodai,
                            Batbuoc = Batbuoc,
                            Macdinh = Macdinh,
                            Stt = Stt,
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        };

                        _db.KetNoiAPI.Add(request);
                        _db.SaveChanges();

                        var data = new { status = "success", message = "Thêm mới nhóm hàng hóa thành công!" };
                        return Json(data);
                    }
                    else
                    {
                        var data = new { status = "error", message = "Số thứ tự đã tồn tại!!!" };
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

        [Route("KetNoiAPI/ThietLapChung/Edit")]
        [HttpPost]
        public JsonResult ThietLapChungEdit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.api.chung", "Edit"))
                {
                    var model = _db.KetNoiAPI.FirstOrDefault(p => p.Id == Id);
                    if (model != null)
                    {
                        string result = "<div class='modal-body' id='frm_edit'>";
                        result += "<div class='row'>";
                        result += "<div class='col-xl-6'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Phân loại*</label>";
                        result += "<select class='form-control' id='phanloai_edit' name='phanloai_edit'>";
                        foreach (var item in Helpers.GetClass())
                        {
                            result += "<option value='" + item.Value + "'  " + (model.Phanloai == item.Value ? "selected" : "") + ">" + item.Description + "</option>";
                        }
                        result += "</select>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-6'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Tên trường*</label>";
                        result += "<input type='text' class='form-control' id='tentruong_edit' name='tentruong_edit' value='" + model.Tendong + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Mô tả*</label>";
                        result += "<textarea class='form-control' id='mota_edit' name='mota_edit' rows='2'>" + model.Mota + "</textarea>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Kiểu dữ liệu*</label>";
                        result += "<select class='form-control' id='kieudl_edit' name='kieudl_edit'>";
                        foreach (var item in Helpers.GetTypeData())
                        {
                            result += "<option value='" + item.Value + "'  " + (model.Kieudulieu == item.Value ? "selected" : "") + ">" + item.Description + "</option>";
                        }
                        result += "</select>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Định dạng</label>";
                        result += "<input type='text' class='form-control' id='dinhdang_edit' name='dinhdang_edit' value='" + model.Dinhdang + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Độ dài*</label>";
                        result += "<input type='text' class='form-control' id='dodai_edit' name='dodai_edit' value='" + model.Dodai + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Bắt buộc*</label>";
                        result += "<select class='form-control' id='batbuoc_edit' name='batbuoc_edit'>";
                        result += "<option value='" + false + "' " + (model.Batbuoc == false ? "selected" : "") + ">Không</option>";
                        result += "<option value='" + true + "' " + (model.Batbuoc == true ? "selected" : "") + ">Có</option>";
                        result += "</select>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Giá trị mặc định</label>";
                        result += "<input type='text' class='form-control' id='macdinh_edit' name='macdinh_edit' value='" + model.Macdinh + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Stt</label>";
                        result += "<input type='number' class='form-control' id='stt_edit' name='stt_edit' value='" + model.Stt + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "</div>";
                        result += "<input hidden class='form-control' id='id_edit' name='id_edit' value='" + model.Id + "'/>";
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

        [Route("KetNoiAPI/ThietLapChung/Update")]
        [HttpPost]
        public IActionResult ThietLapChungUpdate(int Id, string Phanloai, string Tentruong, string Mota, string Kieudl,
            string Dinhdang, string Dodai, bool Batbuoc, string Macdinh, int Stt)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.api.chung", "Edit"))
                {
                    int check = _db.KetNoiAPI.Where(t => t.Stt == Stt && t.Phanloai == Phanloai && t.Id != Id).Count();
                    if (check == 0)
                    {
                        var model = _db.KetNoiAPI.FirstOrDefault(t => t.Id == Id);
                        model.Phanloai = Phanloai;
                        model.Tendong = Tentruong;
                        model.Mota = Mota;
                        model.Kieudulieu = Kieudl;
                        model.Dinhdang = Dinhdang;
                        model.Dodai = Dodai;
                        model.Batbuoc = Batbuoc;
                        model.Macdinh = Macdinh;
                        model.Stt = Stt;
                        model.Updated_at = DateTime.Now;

                        _db.KetNoiAPI.Update(model);
                        _db.SaveChanges();

                        var data = new { status = "success", message = "Cập nhật thành công!" };
                        return Json(data);
                    }
                    else
                    {
                        var data = new { status = "error", message = "Số thứ tự đã tồn tại!!!" };
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

        [Route("KetNoiAPI/ThietLapChung/Delete")]
        [HttpPost]
        public IActionResult ThietLapChungDelete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.api.chung", "Delete"))
                {
                    var model = _db.KetNoiAPI.FirstOrDefault(p => p.Id == id_delete);
                    _db.KetNoiAPI.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("ThietLapChung", "API", new { Phanloai = model.Phanloai });
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
        [Route("KetNoiAPI/ThietLapChung/Default")]
        [HttpPost]
        public IActionResult ThietLapChungDefault(string phanloai_default)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.api.chung", "Create"))
                {
                    var deleteModel = _db.KetNoiAPI.Where(x => x.Phanloai == phanloai_default);
                    foreach (var itemDl in deleteModel)
                    {
                        _db.KetNoiAPI.Remove(_db.KetNoiAPI.FirstOrDefault(x => x.Id == itemDl.Id));
                    }
                    _db.SaveChanges();
                    var newModel = Helper.Helpers.getAPIThietLapMacDinh().Where(x => x.Phanloai == phanloai_default && x.Tendong_goc == null);
                    var newModelChild = Helper.Helpers.getAPIThietLapMacDinh().Where(x => x.Phanloai == phanloai_default && (x.Tendong_goc != "" && x.Tendong_goc != null));

                    foreach (var itemM in newModel)
                    {
                        var model = new KetNoiAPI()
                        {
                            Phanloai = itemM.Phanloai,
                            Tendong = itemM.Tendong,
                            Tendong_goc = itemM.Tendong_goc,
                            Stt = itemM.Stt
                        };
                        _db.KetNoiAPI.Add(model);
                        _db.SaveChanges();
                        foreach (var itemMC in newModelChild.Where(x => x.Tendong_goc == itemM.Tendong))
                        {
                            var modelC = new KetNoiAPI()
                            {
                                Phanloai = itemMC.Phanloai,
                                Tendong = itemMC.Tendong,
                                Tendong_goc = itemMC.Tendong_goc,
                                Stt = itemMC.Stt,
                            };
                            _db.KetNoiAPI.Add(modelC);
                            _db.SaveChanges();
                        }

                    }
                    _db.SaveChanges();
                    var result = _db.KetNoiAPI.Where(x => x.Phanloai == phanloai_default);
                    ViewData["Phanloai"] = phanloai_default;

                    return View("Views/Admin/Systems/API/ThietLapChung.cshtml", result);
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
        [Route("KetNoiAPI/ThietLapChiTiet")]
        public IActionResult ThietLapChiTiet()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmchucnang", "Index"))
                {
                    ViewData["level0"] = _db.DmChucnang.Where(x => x.Capdo == "0");
                    ViewData["level1"] = _db.DmChucnang.Where(x => x.Capdo == "1");
                    ViewData["level2"] = _db.DmChucnang.Where(x => x.Capdo == "2");

                    ViewData["Title"] = "Danh mục chức năng hệ thống";
                    ViewData["MenuLv1"] = "menu_hethong";
                    ViewData["MenuLv2"] = "";
                    ViewData["MenuLv3"] = "menu_dmchucnang";
                    return View("Views/Admin/Systems/API/ThietLapChiTiet.cshtml");
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

        [Route("KetNoiAPI/ThietLapHoso")]
        public IActionResult ThietLapHoso(string Maso)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.api.", "Index"))
                {

                    var model = _db.KetNoiAPI_HoSo.Where(t => t.Maso == Maso).OrderBy(t => t.Stt);
                    var stt = model.Count() + 1;
                    /*var VMGetAPIThietLap = Helper.Helpers.getAPIThietLapMacDinh();
                    ViewBag.VMGetAPIThietLap = (from t in VMGetAPIThietLap
                                        group t by t.Phanloai into grp
                                        select new { 
                                            phanloai=grp.Key
                                        }).ToList();*/
                    ViewData["KetNoiAPI_HoSo_ChiTiet"] = _db.KetNoiAPI_HoSo_ChiTiet.Where(t => t.Maso == Maso).OrderBy(t => t.Stt);
                    ViewData["Stt"] = stt;
                    ViewData["Maso"] = Maso;
                    //ViewData["Menu"] = _db.DmChucnang.FirstOrDefault(x => x.Maso == Maso).Menu;
                    ViewData["Title"] = "Thiết lập chung kết nối API";
                    ViewData["MenuLv1"] = "menu_hethong";
                    ViewData["MenuLv2"] = "menu_qthethong";
                    ViewData["MenuLv3"] = "menu_api";
                    ViewData["MenuLv4"] = "menu_api_hoso";
                    return View("Views/Admin/Systems/API/ThietLapHoso.cshtml", model);
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

        [Route("KetNoiAPI/ThietLapHoso/Store")]
        [HttpPost]
        public JsonResult ThietLapHosoStore(string Tendong, string Tendong_goc, string Tentruong, string Kieudl,
            string Dinhdang, string Dodai, bool Batbuoc, string Macdinh, int Stt, string Maso)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.api.", "Create"))
                {
                    int check = _db.KetNoiAPI_HoSo.Where(t => t.Stt == Stt && t.Maso == Maso).Count();
                    if (check == 0)
                    {
                        var request = new KetNoiAPI_HoSo
                        {
                            Tendong = Tendong,
                            Tendong_Goc = Tendong_goc,
                            Tentruong = Tentruong,
                            Kieudulieu = Kieudl,
                            Dinhdang = Dinhdang,
                            Dodai = Dodai,
                            Batbuoc = Batbuoc,
                            Macdinh = Macdinh,
                            Stt = Stt,
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                            Maso = Maso,
                        };

                        _db.KetNoiAPI_HoSo.Add(request);
                        _db.SaveChanges();

                        var data = new { status = "success", message = "Thêm mới nhóm hàng hóa thành công!" };
                        return Json(data);
                    }
                    else
                    {
                        var data = new { status = "error", message = "Số thứ tự đã tồn tại!!!" };
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

        [Route("KetNoiAPI/ThietLapHoso/Edit")]
        [HttpPost]
        public JsonResult ThietLapHosoEdit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.api.", "Edit"))
                {
                    var model = _db.KetNoiAPI_HoSo.FirstOrDefault(p => p.Id == Id);
                    if (model != null)
                    {
                        string result = "<div class='modal-body' id='frm_edit'>";
                        result += "<div class='row'>";

                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Tên dòng(API</label>";
                        result += "<input type='text' class='form-control' id='tendong_edit' name='tendong_edit' value='" + model.Tendong + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Tên dòng gốc</label>";
                        result += "<input type='text' class='form-control' id='tendong_goc_edit' name='tendong_goc_edit' value='" + model.Tendong_Goc + "' placeholder=\"Cho các trường trong danh sách chi tiết (DS_HHDV, ...)\"/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Tên trường*</label>";
                        result += "<input type='text' class='form-control' id='tentruong_edit' name='tentruong_edit' value='" + model.Tentruong + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Kiểu dữ liệu*</label>";
                        result += "<select class='form-control' id='kieudl_edit' name='kieudl_edit'>";
                        foreach (var item in Helpers.GetTypeData())
                        {
                            result += "<option value='" + item.Value + "'  " + (model.Kieudulieu == item.Value ? "selected" : "") + ">" + item.Description + "</option>";
                        }
                        result += "</select>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Định dạng</label>";
                        result += "<input type='text' class='form-control' id='dinhdang_edit' name='dinhdang_edit' value='" + model.Dinhdang + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Độ dài*</label>";
                        result += "<input type='text' class='form-control' id='dodai_edit' name='dodai_edit' value='" + model.Dodai + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Bắt buộc*</label>";
                        result += "<select class='form-control' id='batbuoc_edit' name='batbuoc_edit'>";
                        result += "<option value='" + false + "' " + (model.Batbuoc == false ? "selected" : "") + ">Không</option>";
                        result += "<option value='" + true + "' " + (model.Batbuoc == true ? "selected" : "") + ">Có</option>";
                        result += "</select>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Giá trị mặc định</label>";
                        result += "<input type='text' class='form-control' id='macdinh_edit' name='macdinh_edit' value='" + model.Macdinh + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-4'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Stt</label>";
                        result += "<input type='number' class='form-control' id='stt_edit' name='stt_edit' value='" + model.Stt + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "</div>";
                        result += "<input hidden class='form-control' id='id_edit' name='id_edit' value='" + model.Id + "'/>";
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

        [Route("KetNoiAPI/ThietLapHoso/Update")]
        [HttpPost]
        public IActionResult ThietLapHosoUpdate(int Id, string Maso, string Tentruong, string Tendong, string Tendong_goc, string Kieudl,
            string Dinhdang, string Dodai, bool Batbuoc, string Macdinh, int Stt)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.api.", "Edit"))
                {
                    int check = _db.KetNoiAPI_HoSo.Where(t => t.Stt == Stt && t.Maso == Maso && t.Id != Id).Count();
                    if (check == 0)
                    {
                        var model = _db.KetNoiAPI_HoSo.FirstOrDefault(t => t.Id == Id);
                        model.Tendong = Tendong;
                        model.Tendong_Goc = Tendong_goc;
                        model.Tentruong = Tentruong;
                        model.Kieudulieu = Kieudl;
                        model.Dinhdang = Dinhdang;
                        model.Dodai = Dodai;
                        model.Batbuoc = Batbuoc;
                        model.Macdinh = Macdinh;
                        model.Stt = Stt;
                        model.Updated_at = DateTime.Now;

                        _db.KetNoiAPI_HoSo.Update(model);
                        _db.SaveChanges();

                        var data = new { status = "success", message = "Cập nhật thành công!" };
                        return Json(data);
                    }
                    else
                    {
                        var data = new { status = "error", message = "Số thứ tự đã tồn tại!!!" };
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

        [Route("KetNoiAPI/ThietLapHoso/Delete")]
        [HttpPost]
        public IActionResult ThietLapHosoDelete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.api.", "Delete"))
                {
                    var model = _db.KetNoiAPI_HoSo.FirstOrDefault(p => p.Id == id_delete);
                    _db.KetNoiAPI_HoSo.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("ThietLapHoso", "API", new { Maso = model.Maso });
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

        [Route("KetNoiAPI/ThietLapHoso/Default")]
        [HttpPost]
        public IActionResult ThietLapHosoDefault(string phanloai_default)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.api.chung", "Create"))
                {
                    var deleteModel = _db.KetNoiAPI.Where(x => x.Phanloai == phanloai_default);
                    foreach (var itemDl in deleteModel)
                    {
                        _db.KetNoiAPI.Remove(_db.KetNoiAPI.FirstOrDefault(x => x.Id == itemDl.Id));
                    }
                    _db.SaveChanges();
                    var newModel = Helper.Helpers.getAPIThietLapMacDinh().Where(x => x.Phanloai == phanloai_default && x.Tendong_goc == null);
                    var newModelChild = Helper.Helpers.getAPIThietLapMacDinh().Where(x => x.Phanloai == phanloai_default && (x.Tendong_goc != "" && x.Tendong_goc != null));

                    foreach (var itemM in newModel)
                    {
                        var model = new KetNoiAPI()
                        {
                            Phanloai = itemM.Phanloai,
                            Tendong = itemM.Tendong,
                            Tendong_goc = itemM.Tendong_goc,
                            Stt = itemM.Stt
                        };
                        _db.KetNoiAPI.Add(model);
                        _db.SaveChanges();
                        foreach (var itemMC in newModelChild.Where(x => x.Tendong_goc == itemM.Tendong))
                        {
                            var modelC = new KetNoiAPI()
                            {
                                Phanloai = itemMC.Phanloai,
                                Tendong = itemMC.Tendong,
                                Tendong_goc = itemMC.Tendong_goc,
                                Stt = itemMC.Stt,
                            };
                            _db.KetNoiAPI.Add(modelC);
                            _db.SaveChanges();
                        }

                    }
                    _db.SaveChanges();
                    var result = _db.KetNoiAPI.Where(x => x.Phanloai == phanloai_default);
                    ViewData["Phanloai"] = phanloai_default;

                    return View("Views/Admin/Systems/API/ThietLapChung.cshtml", result);
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

        [Route("KetNoiAPI/DanhSachKetNoi")]
        public IActionResult DanhSachKetNoi_20240410(string Maso)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.api.", "Index"))
                {
                    string Madv = "";
                    if (Helpers.GetSsAdmin(HttpContext.Session, "Madv") != null)
                    {
                        Madv = Helpers.GetSsAdmin(HttpContext.Session, "Madv");
                    }
                    else
                    {
                        Madv = _db.DsDonVi.FirstOrDefault().MaDv;
                    }
                    /*var dsdonvi = (from db in _db.DsDiaBan
                                   join dv in _db.DsDonVi.Where(t => t.MaDv==Madv) on db.MaDiaBan equals dv.MaDiaBan
                                   select new VMDsDonVi
                                   {
                                       Id = dv.Id,
                                       TenDiaBan = db.TenDiaBan,
                                       MaDiaBan = dv.MaDiaBan,
                                       TenDv = dv.TenDv,
                                       MaDv = dv.MaDv,
                                   });

                    var model = (from user in _db.Users
                                 join dsdv in dsdonvi on user.Madv equals dsdv.MaDv
                                 select new Users {
                                     LinkAPI = "localhost/api/getAPI?name=" + user.Username + 
                                     "&token=" + Helper.Helpers.StringtoMD5(user.Username + user.Madv) + "&maso=" + Maso,
                                     Name=user.Name
                                 }); */
                    var model = _db.Users.Where(x => x.Madv == Madv);
                    var serverName = HttpContext.Session.GetString("ServerName");
                    foreach (var md in model)
                    {
                        md.LinkAPI = serverName + "/api/getAPI?name=" + md.Username +
                                     "&token=" + Helper.Helpers.StringtoMD5(md.Username + md.Madv) + "&maso=" + Maso;
                    }
                    ViewData["Maso"] = Maso;
                    ViewData["Menu"] = _db.DmChucnang.FirstOrDefault(x => x.Maso == Maso).Menu;
                    ViewData["Title"] = "Thiết lập chung kết nối API";
                    ViewData["MenuLv1"] = "menu_hethong";
                    ViewData["MenuLv2"] = "menu_qthethong";
                    ViewData["MenuLv3"] = "menu_api";
                    ViewData["MenuLv4"] = "menu_api_hoso";
                    return View("Views/Admin/Systems/API/DanhSachKetNoi.cshtml", model);
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

        [Route("KetNoiAPI/XemHoso")]
        [HttpGet]
        public IActionResult XemHoso(string maSo, string maHS)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.api.chung", "Create"))
                {
                    var truongDuLieu = _db.KetNoiAPI_HoSo.Where(t => t.Maso == maSo);
                    var sKQ = "[";
                    switch (maSo)
                    {
                        case "giahhdvk":
                            {
                                var dSHoSo = _db.GiaHhDvkTh.Where(x => x.Mahs == maHS).ToList();
                                var dSChiTiet = _db.GiaHhDvkThCt.Where(x => x.Mahs == maHS).ToList().OrderBy(x => x.Mahhdv);

                                //string jsonHoSo = Newtonsoft.Json.JsonSerializer.Serialize(dSHoSo);
                                //JsonDocument docHoSo = JsonDocument.Parse(jsonHoSo);
                                string sHoSo = "{";
                                foreach (var item in truongDuLieu.Where(x => x.Tendong != "" && x.Tendong != null).OrderBy(x => x.Stt))
                                {
                                    var dSTruongChiTiet = truongDuLieu.Where(x => x.Tendong_Goc == item.Tendong);
                                    sHoSo += item.Tendong + "" + ":";
                                    if (!string.IsNullOrEmpty(item.Tentruong))
                                    {
                                        //    if (dSHoSo.RootElement.TryGetProperty(item.Tentruong, out JsonElement element))
                                        //    {
                                        //        Console.WriteLine($"{fieldName}: {element.GetString()}");
                                        //    }
                                        //    else
                                        //    {
                                        //        Console.WriteLine($"Field '{fieldName}' not found.");
                                        //    }
                                    }
                                    else
                                    {
                                        if (!dSTruongChiTiet.Any())
                                        {
                                            sHoSo += item.Macdinh + ",";
                                        }
                                        else { sHoSo += "["; }
                                    }

                                    if (dSTruongChiTiet.Any())
                                    {
                                        foreach (var cths in dSChiTiet)
                                        {
                                            sHoSo += "[";
                                            foreach (var ct in dSTruongChiTiet)
                                            {
                                                sHoSo += ct.Tendong + "" + ":";
                                                if (!string.IsNullOrEmpty(ct.Tentruong))
                                                {
                                                    switch (ct.Tentruong)
                                                    {
                                                        case "Mahhdv":
                                                            {
                                                                sHoSo += cths.Mahhdv + ",";
                                                                break;
                                                            }
                                                        case "Gia":
                                                            {
                                                                sHoSo += cths.Gia + ",";
                                                                break;
                                                            }
                                                        case "Gialk":
                                                            {
                                                                sHoSo += cths.Gialk + ",";
                                                                break;
                                                            }
                                                    }
                                                }
                                                else
                                                {
                                                    sHoSo += ct.Macdinh + ",";
                                                }
                                            }
                                            sHoSo += "]";
                                        }
                                        sHoSo += "]";
                                    }
                                }
                                sHoSo += "}]";
                                sKQ = sHoSo;
                                break;
                            }
                        case "giathuetn":
                            {
                                var dSHoSo = _db.GiaThueTaiNguyen.Where(x => x.Mahs == maHS).ToList();
                                break;
                            }
                    }
                    return Ok(Json(sKQ));

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

        [Route("KetNoiAPI/DanhSachCSDLQuocGia")]
        [HttpGet]
        public IActionResult DanhSachCSDLQuocGia()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.api.csdlqg", "Create"))
                {

                    //Tạo mảng lưu các danh sách trường kết nối để chọn trong danh sách

                    var model = _db.KetNoiAPI_DanhSach.ToList();
                    ViewData["Title"] = "Thiết lập chung kết nối API";
                    ViewData["MenuLv1"] = "menu_hethong";
                    ViewData["MenuLv2"] = "menu_qthethong";
                    ViewData["MenuLv3"] = "menu_qlketnoi";
                    ViewData["MenuLv4"] = "menu_dsketnoicsdlquocgia";
                    return View("Views/Admin/Systems/API/DanhSachKetNoi.cshtml", model);

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

        [HttpPost]
        public IActionResult LuuThietLapCSDLQG(KetNoiAPI_DanhSach request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.api.chung", "Create"))
                {
                    var model = _db.KetNoiAPI_DanhSach.FirstOrDefault(x => x.Maso == request.Maso);
                    if (model == null)
                    {
                        var ketNoi = new KetNoiAPI_DanhSach()
                        {
                            Maso = request.Maso,
                            LinkNhanGet = request.LinkNhanGet,
                            LinkNhanPost = request.LinkNhanPost,
                            LinkNhanPut = request.LinkNhanPut,
                            LinkTruyenGet = request.LinkTruyenGet,
                            LinkTruyenPost = request.LinkTruyenPost,
                            LinkTruyenPut = request.LinkTruyenPut,
                        };
                        _db.KetNoiAPI_DanhSach.Add(ketNoi);
                        _db.SaveChanges();
                    }
                    else
                    {
                        model.LinkNhanGet = request.LinkNhanGet;
                        model.LinkNhanPost = request.LinkNhanPost;
                        model.LinkNhanPut = request.LinkNhanPut;
                        model.LinkTruyenGet = request.LinkTruyenGet;
                        model.LinkTruyenPost = request.LinkTruyenPost;
                        model.LinkTruyenPut = request.LinkTruyenPut;
                        _db.KetNoiAPI_DanhSach.Update(model);
                        _db.SaveChanges();
                    }

                    return RedirectToAction("DanhSachCSDLQuocGia", "API");
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

        //
        [HttpPost]
        public async Task<IActionResult> TruyenDuLieuCSDLQG(VMHoSoTruyenCSDLQG request)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                string maBearer = "Bearer ";
                //Lấy mã kết nối

                HttpClient client_layma = new HttpClient();
                // Thêm các header cần thiết
                client_layma.DefaultRequestHeaders.Add("lgspaccesstoken", request.TokenLGSP);
                var httpRequest = new HttpRequestMessage(HttpMethod.Post, request.LinkAPIXacthuc);
                // Dữ liệu gửi đi
                var requestData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "client_credentials")
                });
                // Thêm Content-Type vào header của nội dung
                requestData.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

                httpRequest.Content = requestData;
                var response = await client_layma.SendAsync(httpRequest);
                // Đọc nội dung phản hồi
                string ketQuaLayMa = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    JObject jsonObject = JObject.Parse(ketQuaLayMa);
                    maBearer += (string)jsonObject["access_token"];
                }
                else
                {
                    return Ok("Không thể kết nối đến LGSP để lấy mã kết nối. Mã lỗi:" + response.StatusCode);
                }

                //Tạo hồ sơ truyền dữ liệu
                string jsonKetQua = "";
                switch (request.MaKetNoiAPI)
                {
                    case "giahhdvk":
                        {
                            var hoSo = _db.GiaHhDvkTh.FirstOrDefault(x => x.Mahs == request.Mahs);
                            var dmDVT = _db.DmDvt;
                            var hoSoChiTiet = from chitiet in _db.GiaHhDvkThCt.Where(x => x.Mahs == request.Mahs)
                                              join danhmuc in _db.GiaHhDvkDm.Where(x => x.Matt == hoSo.Matt) on chitiet.Mahhdv equals danhmuc.Mahhdv
                                              select new GiaHhDvkThCt
                                              {
                                                  Mahhdv = danhmuc.Mahhdv,
                                                  Tenhhdv = danhmuc.Tenhhdv,
                                                  Manhom = danhmuc.Manhom,
                                                  Dvt = danhmuc.Dvt,
                                                  Dacdiemkt = danhmuc.Dacdiemkt,
                                                  Nguontt = chitiet.Nguontt,
                                                  Loaigia = chitiet.Loaigia,
                                                  Gia = chitiet.Gia,
                                                  Gialk = chitiet.Gialk,
                                                  Ghichu = chitiet.Ghichu,
                                              };
                            var giaHHDVK_DSHH = new List<VMGiaHHDVK_DSHH>();
                            Dictionary<string, int> loaiGia = new Dictionary<string, int>();
                            loaiGia["Giá bán buôn"] = 10;
                            loaiGia["Giá bán lẻ"] = 5;

                            Dictionary<string, int> nguonThongTin = new Dictionary<string, int>();
                            loaiGia["Do trực tiếp điều tra, thu thập"] = 1;
                            loaiGia["Do cơ quan/đơn vị quản lý nhà nước có liên quan cung cấp/báo cáo theo quy định"] = 2;
                            loaiGia["Từ thống kê đăng ký giá, kê khai giá, thông báo giá của doanh nghiệp"] = 3;
                            loaiGia["Hợp đồng mua tin"] = 4;
                            loaiGia["Các nguồn thông tin khác"] = 5;

                            foreach (var item in hoSoChiTiet)
                            {
                                var dvt = dmDVT.FirstOrDefault(x => x.Dvt.ToLower() == item.Dvt.ToLower());
                                int lOAI_GIA;
                                int nGUON_THONG_TIN;
                                TryGetKey(loaiGia, item.Loaigia, out lOAI_GIA);
                                TryGetKey(nguonThongTin, item.Nguontt, out nGUON_THONG_TIN);
                                giaHHDVK_DSHH.Add(new VMGiaHHDVK_DSHH
                                {
                                    LOAI_GIA = lOAI_GIA == -1 ? 5 : lOAI_GIA,//Viết hàm lấy loại giá
                                    MA_HHDV = item.Mahhdv,
                                    DON_VI_TINH = dvt.Madvt,
                                    GIA_KY_TRUOC = item.Gialk,
                                    GIA_KY_NAY = item.Gia,
                                    NGUON_THONG_TIN = nGUON_THONG_TIN == -1 ? 2 : nGUON_THONG_TIN,//Viết hàm lấy nguồn thông tin
                                    GHI_CHU = item.Ghichu,
                                });
                            }
                            var giaHHDVK = new List<VMGiaHHDVK>();
                            giaHHDVK.Add(new VMGiaHHDVK
                            {
                                DIA_BAN = request.DIA_BAN,
                                NGUON_SO_LIEU = request.NGUON_SO_LIEU,
                                DONVI_TTSL = request.NGUON_SO_LIEU,
                                DINH_KY = 24,
                                THOI_GIAN_BC_1 = Int16.Parse(hoSo.Thang),
                                THOI_GIAN_BC_2 = Int16.Parse(hoSo.Thang),
                                THOI_GIAN_BC_NAM = Int16.Parse(hoSo.Nam),
                                FILE_DINH_KEM_WORD = hoSo.ipf_word_base64,
                                FILE_DINH_KEM_PDF = hoSo.ipf_pdf_base64,
                                NGUOI_TAO = request.NGUOI_TAO,
                                NGUOI_DUYET = request.NGUOI_DUYET,
                                DS_HHDV_TT = giaHHDVK_DSHH
                            });

                            jsonKetQua = @"{""data"":" + JsonConvert.SerializeObject(giaHHDVK) + @"}";
                            break;
                        }

                    case "giahhdvkdm":
                        {
                            var model_giahhdvkdm = _db.GiaHhDvkDm.Where(x => x.Matt == request.Mahs && x.Theodoi == "TD").OrderBy(x => x.Mahhdv);
                            var dmDVT = _db.DmDvt;
                            var giaHHDVKDM = new List<VMGiaHHDVKDM>();
                            foreach (var item in model_giahhdvkdm)
                            {
                                var dvt = dmDVT.FirstOrDefault(x => x.Dvt.ToLower() == item.Dvt.ToLower());
                                giaHHDVKDM.Add(new VMGiaHHDVKDM
                                {
                                    DIA_BAN = request.DIA_BAN,
                                    NHOM_HHDV = item.Manhom,
                                    MA_HHDV = item.Mahhdv,
                                    MA_HHDV_TINH_THANH = item.Mahhdv,
                                    TEN_HHDV_TINH_THANH = item.Tenhhdv,
                                    DAC_DIEM_KY_THUAT = item.Dacdiemkt,
                                    DON_VI_TINH = dvt.Madvt,
                                    NGUOI_TAO = request.NGUOI_TAO,
                                });
                            }
                            jsonKetQua = @"{""data"":" + JsonConvert.SerializeObject(giaHHDVKDM) + @"}";
                            break;

                        }
                        //START
                    case "giathuetainguyen":
                        {
                            var chiTiet = _db.GiaThueTaiNguyenCt.Where(x => x.Mahs == request.Mahs);
                            var hoSo = _db.GiaThueTaiNguyen.FirstOrDefault(x => x.Mahs == request.Mahs);

                            var giaTaiNguyenCT = new List<VMGiaThueTaiNguyen_DSCT>();
                            List<string> capFields = new List<string> { "Cap6", "Cap5", "Cap4", "Cap3", "Cap2", "Cap1" };
                            foreach (var item in chiTiet.OrderBy(x => x.SapXep))
                            {
                                string maTN = "";
                                int capDo = 1;
                                string maGoc = "";
                                string nhomTN = "";
                                foreach (var capField in capFields)
                                {
                                    string capProperty = (string)item.GetType().GetProperty(capField).GetValue(item);

                                    if (!string.IsNullOrEmpty(capProperty))
                                    {
                                        maTN = capProperty;
                                        capDo = int.Parse(capField.Substring(3)); // Lấy số từ chuỗi "CapX"
                                        maGoc = capDo == 1 ? maTN : maTN.Substring(0, (maTN.Length > 2 ? maTN.Length - 2 : 0));
                                        break;
                                    }
                                }

                                if (maTN == "")//do nhận dữ liệu có trường hợp để trống mã tài nguyên
                                {
                                    continue;
                                }
                                //Gán nhóm tài nguyên để cho trường hợp truyền từng Mục tài nguyên
                                nhomTN = Regex.Replace(maTN, @"\d", "");
                                //Check gửi từng nhóm
                                //if(nhomTN == "IV" && item.Gia > 0)                                
                                //if(nhomTN == "IV")                                
                                giaTaiNguyenCT.Add(new VMGiaThueTaiNguyen_DSCT
                                {
                                    MA_TAI_NGUYEN = maTN,
                                    GIA_TINH_THUE = item.Gia,
                                    THUE_SUAT = 0,
                                    GHI_CHU = "Giá tính thuế tài nguyên",
                                });
                            }

                            var giaTaiNguyen = new List<VMGiaThueTaiNguyen>();
                            giaTaiNguyen.Add(new VMGiaThueTaiNguyen
                            {
                                DIA_BAN = request.DIA_BAN,
                                DONVI_TTSL = request.NGUON_SO_LIEU,
                                NGUON_SO_LIEU = request.NGUON_SO_LIEU,
                                SO_VAN_BAN = hoSo.Soqd,
                                NGAY_THUC_HIEN = hoSo.Thoidiem.ToString("yyyyMMdd"),
                                NGAY_BD_HIEU_LUC = hoSo.Thoidiem.ToString("yyyyMMdd"),
                                NGAY_KT_HIEU_LUC = null,
                                NGUOI_TAO = request.NGUOI_TAO,
                                NGUOI_DUYET = request.NGUOI_DUYET,
                                DS_TAI_NGUYEN_CT = giaTaiNguyenCT
                            });
                            jsonKetQua = @"{""data"":" + JsonConvert.SerializeObject(giaTaiNguyen) + @"}";
                            break;
                        }
                    //END
                    case "giathuetainguyendm":
                        {

                            //Chỉ lấy các danh mục theo hồ sơ đã kê khai giá
                            var hoSoChiTiet = (from hosoct in _db.GiaThueTaiNguyenCt
                                               join hoso in _db.GiaThueTaiNguyen.Where(x => x.Manhom == request.Mahs) on hosoct.Mahs equals hoso.Mahs
                                               select new CSDLGia_ASP.Models.Manages.DinhGia.GiaThueTaiNguyenCt
                                               {
                                                   Cap1 = hosoct.Cap1,
                                                   Cap2 = hosoct.Cap2,
                                                   Cap3 = hosoct.Cap3,
                                                   Cap4 = hosoct.Cap4,
                                                   Cap5 = hosoct.Cap5,
                                                   Cap6 = hosoct.Cap6,
                                                   Ten = hosoct.Ten,
                                                   Dvt = hosoct.Dvt,
                                                   Gia = hosoct.Gia,
                                                   SapXep = hosoct.SapXep,
                                               });
                            if (!hoSoChiTiet.Any())
                            {
                                ViewData["Messages"] = "Danh mục chưa phát sinh hồ sơ giá thuế tài nguyên để so sánh và gửi danh mục lên cơ sở dữ liệu quốc giá";
                                return View("Views/Admin/Error/Error.cshtml");
                            }

                            var dmDVT = _db.DmDvt;
                            var giaTaiNguyenDM = new List<VMGiaThueTaiNguyenDM>();
                            List<string> capFields = new List<string> { "Cap6", "Cap5", "Cap4", "Cap3", "Cap2", "Cap1" };
                            //Lấy danh mục
                            List<string> listDM = new List<string>();
                            var model_giatndm = _db.GiaThueTaiNguyenDm.Where(x => x.Manhom == request.Mahs && x.Theodoi == "TD").OrderBy(x => x.Sapxep);
                            foreach (var item in model_giatndm)
                            {
                                foreach (var capField in capFields)
                                {
                                    string capProperty = (string)item.GetType().GetProperty(capField).GetValue(item);

                                    if (!string.IsNullOrEmpty(capProperty))
                                    {
                                        listDM.Add(capProperty);
                                        break;
                                    }
                                }
                            }

                            //Lấy danh mục có trong bảng kê khai và listDM để tạo danh mục xuất ra
                            List<string> listDM_hientai = new List<string>();

                            foreach (var item in hoSoChiTiet.OrderBy(x => x.SapXep))
                            {
                                var dvt = dmDVT.FirstOrDefault(x => x.Dvt.ToLower() == item.Dvt.ToLower());

                                string maTN = "";
                                int capDo = 1;
                                string maGoc = "";

                                foreach (var capField in capFields)
                                {
                                    string capProperty = (string)item.GetType().GetProperty(capField).GetValue(item);

                                    if (!string.IsNullOrEmpty(capProperty))
                                    {
                                        maTN = capProperty;
                                        capDo = int.Parse(capField.Substring(3)); // Lấy số từ chuỗi "CapX"
                                        if (capDo == 1)
                                        {
                                            maGoc = null;
                                        }
                                        else if (capDo == 2)
                                        {
                                            maGoc = Regex.Replace(maTN, @"\d", "");
                                        }
                                        else
                                        {
                                            maGoc = maTN.Substring(0, (maTN.Length > 2 ? maTN.Length - 2 : 0));
                                        }

                                        break;
                                    }
                                }
                                /*Xử lý nếu  nằm trong danh mục thì 
                                  - Nằm trong danh mục thì TAI_NGUYEN_BTC = maTN;
                                  - Ko trong danh mục thì lấy TAI_NGUYEN_BTC = mã tài nguyên trong danh mục gần nhất
                                 */
                                string maBTC = maTN;
                                if (!listDM.Contains(maTN))
                                {
                                    maBTC = maGoc;
                                    //Kiểm tra mã gốc xem có nằm trong danh mục không nếu ko thì lùi lại thêm 1 lần
                                    if (!listDM.Contains(maBTC))
                                    {
                                        maBTC = maBTC.Substring(0, maBTC.Length - 2);
                                    }
                                }
                                //gán lại mã cho mục II210103 do theo dm 05/VBHN-BTC tài nguyên này ở nhóm II2401
                                if (maTN == "II210103")
                                {
                                    maGoc = "II2401";
                                }

                                //Kiểm tra trong danh muc nếu chưa có thì thêm vào
                                if (!listDM_hientai.Contains(maTN))
                                {
                                    //Thử bỏ qua mã cấp 1
                                    //if (capDo > 1)
                                        giaTaiNguyenDM.Add(new VMGiaThueTaiNguyenDM
                                        {
                                            DIA_BAN = request.DIA_BAN,
                                            MA_TAI_NGUYEN = maTN,
                                            TEN_TAI_NGUYEN = item.Ten,
                                            CAP_TAI_NGUYEN = capDo,
                                            DON_VI_TINH = (dvt == null ? null : dvt.Madvt), // Kiểm tra null trước khi truy cập thuộc tính
                                            MA_TAI_NGUYEN_TINH_CHA = maGoc,
                                            TAI_NGUYEN_BTC = maBTC,
                                            NGUOI_TAO = request.NGUOI_TAO,
                                        });
                                    listDM_hientai.Add(maTN);
                                    //jsonKetQua = @"{""data"":" + JsonConvert.SerializeObject(giaTaiNguyenDM) + @"}";
                                    //break;
                                }
                            }
                            jsonKetQua = @"{""data"":" + JsonConvert.SerializeObject(giaTaiNguyenDM) + @"}";
                            break;
                        }

                    case "giaspdvcongichdm":
                        {
                            break;
                        }

                    case "giaspdvcongich":
                        {
                            var model = _db.GiaSpDvToiDa.FirstOrDefault(x => x.Mahs == request.Mahs);
                            var model_file = _db.ThongTinGiayTo.FirstOrDefault(t => t.Mahs == model.Mahs);
                            string fileBase64 = "";
                            if (model_file == null)
                            {
                                ViewData["Messages"] = "Hồ sơ chưa có file đính kèm để gửi dữ liệu lên cơ sở dữ liệu quốc giá";
                                return View("Views/Admin/Error/Error.cshtml");
                            }
                            else
                            {
                                //Kiểm tra lại đường dẫn của file
                                string path = _env.WebRootPath + "/UpLoad/File/ThongTinGiayTo/" + model_file.FileName;
                                if (!System.IO.File.Exists(path))
                                {
                                    ViewData["Messages"] = "Hồ sơ chưa có file đính kèm để gửi dữ liệu lên cơ sở dữ liệu quốc giá";
                                    return View("Views/Admin/Error/Error.cshtml");
                                }
                                else
                                {
                                    // Đọc tất cả dữ liệu từ tập tin
                                    byte[] fileBytes = System.IO.File.ReadAllBytes(path);

                                    // Chuyển đổi dữ liệu thành mã base64
                                    fileBase64 = Convert.ToBase64String(fileBytes);
                                }

                            }
                            var giaDichVuRacThai = new List<VMGiaDichVuRacThai>();

                            giaDichVuRacThai.Add(new VMGiaDichVuRacThai
                            {
                                DIA_BAN = request.DIA_BAN,
                                DONVI_TTSL = request.NGUON_SO_LIEU,
                                NGUON_SO_LIEU = request.NGUON_SO_LIEU,
                                SO_VAN_BAN = LaySoQD(model.Soqd),
                                NGAY_THUC_HIEN = model.Thoidiem.ToString("yyyyMMdd"),
                                NGAY_BD_HIEU_LUC = model.Thoidiem.ToString("yyyyMMdd"),
                                NGAY_KT_HIEU_LUC = null,
                                NGUOI_TAO = request.NGUOI_TAO,
                                NGUOI_DUYET = request.NGUOI_DUYET,
                                MA_BM = "82001",
                                FILE_SO_LIEU = fileBase64,
                                TEN_FILE = model_file.FileName,
                            });

                            jsonKetQua = @"{""data"":" + JsonConvert.SerializeObject(giaDichVuRacThai) + @"}";
                            break;
                        }
                }


                if (!request.THAO_TAC)
                {
                    //Xem dữ liệu
                    return Ok(jsonKetQua);
                }
                /* 25.04

                //Truyền dữ liệu
                // Khởi tạo HttpClient
                var client = new HttpClient();
                // Đặt các header cần thiết
                client.DefaultRequestHeaders.Add("lgspaccesstoken", request.TokenLGSP);
                client.DefaultRequestHeaders.Add("Authorization", maBearer);
                var httpTruyen = new HttpRequestMessage(HttpMethod.Post, request.LinkAPIKetNoi);
                // Dữ liệu gửi đi
                var requestTruyen = new StringContent(jsonKetQua, Encoding.UTF8, "application/json");
                // Thêm Content-Type vào header của nội dung
                requestTruyen.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpTruyen.Content = requestData;
                var responseTruyen = await client.SendAsync(httpTruyen);
                */
                var client = new HttpClient();
                var requestTruyen = new HttpRequestMessage(HttpMethod.Post, request.LinkAPIKetNoi);
                requestTruyen.Headers.Add("lgspaccesstoken", request.TokenLGSP);
                requestTruyen.Headers.Add("SENDER_CODE", "");
                requestTruyen.Headers.Add("TRAN_CODE", "");
                requestTruyen.Headers.Add("RECEIVER_CODE", "");
                requestTruyen.Headers.Add("Authorization", maBearer);
                requestTruyen.Content = new StringContent(jsonKetQua, null, "application/json");
                var responseTruyen = await client.SendAsync(requestTruyen);
                // Đọc phản hồi
                string responseBody = await responseTruyen.Content.ReadAsStringAsync();
                if (responseTruyen.IsSuccessStatusCode)
                {
                    ViewData["Messages"] = responseBody;
                    return View("Views/Admin/Error/Success.cshtml");
                }
                else
                {
                    return Ok("Không thể truyền dữ liệu lên CSDL quốc giá. Mã lỗi:" + responseTruyen.StatusCode);
                }

            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }
        //
        public static bool TryGetKey(Dictionary<string, int> dictionary, string value, out int ketQua)
        {
            // Lặp qua từng cặp key-value trong từ điển
            foreach (var pair in dictionary)
            {
                // Nếu value của cặp key-value đúng với giá trị tìm kiếm
                if (pair.Key == value)
                {
                    // Trả về value và đánh dấu là thành công
                    ketQua = pair.Value;
                    return true;
                }
            }

            // Không tìm thấy key tương ứng với giá trị
            ketQua = -1;
            return false;
        }

        public static string LaySoQD(string value)
        {
            string ketQua = "0";
            if (!string.IsNullOrEmpty(value))
            {
                string[] parts = value.Split("/");
                string soQD = parts[0];
                ketQua = new string(soQD.Where(char.IsDigit).ToArray());
            }
            return ketQua;
        }
    }

}
