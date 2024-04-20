using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class DanhMucKkDkgController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public DanhMucKkDkgController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("DanhMucKkDkg")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmkkdkg", "Index"))
                {
                    var dmkkdkg = _db.DmNgheKd.ToList();

                    ViewData["DsDonVi"] = _db.DsDonVi.Where(t => t.ChucNang == "NHAPLIEU");
                    ViewData["Title"] = "Danh mục kê khai đăng ký giá";
                    ViewData["MenuLv1"] = "menu_hethong";
                    ViewData["MenuLv2"] = "menu_qtdanhmuc";
                    ViewData["MenuLv3"] = "menu_dmkkdkg";
                    return View("Views/Admin/Systems/DanhMucKkDkg/Index.cshtml", dmkkdkg);
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

        [Route("DanhMucKkDkg/Store")]
        [HttpPost]
        public JsonResult Store(string Manghe, string[] Madv, string Tennghe, string Phanloai, string Theodoi, string Baocao)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmkkdkg", "Create"))
                {
                    string str_mdv = Madv.Count() > 0 ? string.Join(",", Madv.ToArray()) : "";
                    var check = _db.DmNgheKd.FirstOrDefault(t => t.Manghe == Manghe);
                    if (check == null)
                    {
                        var request = new DmNgheKd
                        {
                            Manghe = Manghe,
                            Madv = str_mdv,
                            Tennghe = Tennghe,
                            Phanloai = Phanloai,
                            Report = Baocao,
                            Theodoi = Theodoi,
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        };

                        _db.DmNgheKd.Add(request);
                        _db.SaveChanges();

                        var data = new { status = "success", message = "Thêm mới danh mục kê khai đăng ký giá thành công!" };
                        return Json(data);
                    }
                    else
                    {
                        var data = new { status = "error", message = "Mã kê khai đăng ký giá này đã tồn tại!" };
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

        [Route("DanhMucKkDkg/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmkkdkg", "Edit"))
                {
                    var model = _db.DmNgheKd.FirstOrDefault(p => p.Id == Id);
                    var dsdonvi = _db.DsDonVi.Where(t => t.ChucNang == "NHAPLIEU");
                    if (model != null)
                    {
                        List<string> list_madv = !string.IsNullOrEmpty(model.Madv) ? new List<string>(model.Madv.Split(',')) : new List<string>();
                        string result = "<div class='modal-body' id='frm_edit'>";
                        result += "<div class='row'>";

                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Mã nghề<span class='require'>*</span></label>";
                        result += "<input type='text' class='form-control' id='manghe_edit' name='manghe_edit' value='" + model.Manghe + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Tên nghề<span class='require'>*</span></label>";
                        result += "<input type='text' class='form-control' id='tennghe_edit' name='tennghe_edit' value='" + model.Tennghe + "'/>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-6'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Phân loại</label>";
                        result += "<select class='form-control' id='phanloai_edit' name='phanloai_edit'>";
                        if (model.Phanloai == "Kê khai giá")
                        {
                            result += "<option value='Kê khai giá' selected>Kê khai giá</option>";
                            result += "<option value='Đăng ký giá'>Đăng ký giá</option>";
                        }
                        else
                        {
                            result += "<option value='Kê khai giá'>Kê khai giá</option>";
                            result += "<option value='Đăng ký giá' selected>Đăng ký giá</option>";
                        }
                        result += "</select>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-6'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Theo dõi</label>";
                        result += "<select class='form-control' id='theodoi_edit' name='theodoi_edit'>";
                        if (model.Theodoi == "TD")
                        {
                            result += "<option value='TD' selected>Theo dõi</option>";
                            result += "<option value='DTD'>Dừng theo dõi</option>";
                        }
                        else
                        {
                            result += "<option value='TD'>Theo dõi</option>";
                            result += "<option value='DTD' selected>Dừng theo dõi</option>";
                        }
                        result += "</select>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Báo cáo</label>";
                        result += "<select class='form-control' id='baocao_edit' name='baocao_edit'>";
                        if (model.Report == "223")
                        {
                            result += "<option value='QD223' selected>Quyết định số 223</option>";
                            result += "<option value='QD56'>Quyết định số 56</option>";
                            result += "<option value='QD1096'>Quyết định số 1096</option>";
                        }
                        else if (model.Report == "56")
                        {
                            result += "<option value='QD223'>Quyết định số 223</option>";
                            result += "<option value='QD56' selected>Quyết định số 56</option>";
                            result += "<option value='QD1096'>Quyết định số 1096</option>";
                        }
                        else
                        {
                            result += "<option value='QD223'>Quyết định số 223</option>";
                            result += "<option value='QD56'>Quyết định số 56</option>";
                            result += "<option value='QD1096' selected>Quyết định số 1096</option>";
                        }
                        result += "</select>";
                        result += "</div>";
                        result += "</div>";

                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Đơn vị quản lý*</label>";
                        result += "<select class='form-control select2multi' multiple='multiple' id='madv_edit' name='madv_edit' style='width:100%'>";
                        foreach (var dv in dsdonvi)
                        {
                            result += "<option value='" + dv.MaDv + "' " + (list_madv.Contains(dv.MaDv) ? "selected" : "") + ">" + dv.TenDv + "</option>";
                        }
                        result += "</select>";
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

        [Route("DanhMucKkDkg/Update")]
        [HttpPost]
        public IActionResult Update(int Id, string Manghe, string[] Madv, string Tennghe, string Phanloai, string Theodoi, string Baocao)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmkkdkg", "Edit"))
                {
                    if (!string.IsNullOrEmpty(Manghe))
                    {
                        if (!string.IsNullOrEmpty(Tennghe))
                        {
                            int check = _db.DmNgheKd.Where(t => t.Manghe == Manghe && t.Id != Id).Count();
                            if (check == 0)
                            {
                                var model = _db.DmNgheKd.FirstOrDefault(t => t.Id == Id);
                                string str_madv = Madv.Count() > 0 ? string.Join(",", Madv.ToArray()) : "";
                                model.Manghe = Manghe;
                                model.Madv = str_madv;
                                model.Tennghe = Tennghe;
                                model.Phanloai = Phanloai;
                                model.Report = Baocao;
                                model.Theodoi = Theodoi;
                                model.Updated_at = DateTime.Now;

                                _db.DmNgheKd.Update(model);
                                _db.SaveChanges();

                                var data = new { status = "success", message = "Cập nhật thành công!" };
                                return Json(data);
                            }
                            else
                            {
                                var data = new { status = "error", message = "Mã nghề kinh doanh đã tồn tại!!!" };
                                return Json(data);
                            }
                        }
                        else
                        {
                            var data = new { status = "error", message = "Tên nghề kinh doanh không được bỏ trống" };
                            return Json(data);
                        }
                    }
                    else
                    {
                        var data = new { status = "error", message = "Mã nghề kinh doanh không được bỏ trống" };
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

        [Route("DanhMucKkDkg/Delete")]
        [HttpPost]
        public IActionResult Delete(int id_delete)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "hethong.danhmuc.dmkkdkg", "Delete"))
                {
                    var model = _db.DmNgheKd.FirstOrDefault(t => t.Id == id_delete);
                    _db.DmNgheKd.Remove(model);
                    _db.SaveChanges();

                    return RedirectToAction("Index", "DanhMucKkDkg");
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
