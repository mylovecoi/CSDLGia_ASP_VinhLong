using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems.API;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Systems.API
{
    public class APIController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public APIController(CSDLGiaDBContext db)
        {
            _db = db;
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
        public IActionResult DanhSachKetNoi(string Maso)
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

        [HttpPost]
        public IActionResult XemHoso(string maSo, string maHS)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.hethong.api.chung", "Create"))
                {
                    

                    return View("Views/Admin/Systems/API/XemHoSo.cshtml");
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
    }
}
