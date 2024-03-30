using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.ThamDinhGia;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.ThamDinhGia
{
    public class ThamDinhGiaDmHhCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public ThamDinhGiaDmHhCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("ThamDinhGia/DanhMuc/ChiTiet")]
        [HttpGet]
        public IActionResult Index(string Manhom)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.hh", "Index"))
                {
                    var model = _db.ThamDinhGiaDmHh.Where(t => t.Manhom == Manhom).ToList();

                    ViewData["Manhom"] = Manhom;
                    ViewData["Tennhom"] = _db.DmNhomHh.FirstOrDefault(t => t.Manhom == Manhom && t.Phanloai == "THAMDINHGIA").Tennhom;
                    ViewData["Dvt"] = _db.DmDvt.ToList();
                    ViewData["Title"] = "Danh mục hàng hóa";
                    ViewData["MenuLv1"] = "menu_tdg";
                    ViewData["MenuLv2"] = "menu_tdg_hh";
                    return View("Views/Admin/Manages/ThamDinhGia/DmHh/Detail.cshtml", model);
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

        [Route("ThamDinhGia/DanhMuc/ChiTiet/Store")]
        [HttpPost]
        public JsonResult Store(string Manhom, /*string Mahh,*/ string Tenhh, string Tskt, string Xuatxu, string Dvt)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.hh", "Create"))
                {
                    var request = new ThamDinhGiaDmHh
                    {
                        Manhom = Manhom,
                        Mahanghoa = DateTime.Now.ToString("yyMMddssmmHH"),
                        Tenhanghoa = Tenhh,
                        Xuatxu = Xuatxu,
                        Thongsokt = Tskt,
                        Dvt = Dvt,
                        Theodoi = "TD",
                        Created_at = DateTime.Now,
                        Updated_at = DateTime.Now,
                    };

                    _db.ThamDinhGiaDmHh.Add(request);
                    _db.SaveChanges();

                    var check_dvt = _db.DmDvt.FirstOrDefault(t => t.Dvt == Dvt);

                    if (check_dvt == null)
                    {
                        var dvt = new DmDvt
                        {
                            Dvt = Dvt,
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        };
                        _db.DmDvt.Add(dvt);
                        _db.SaveChanges();
                    }

                    var data = new { status = "success", message = "Thêm mới hàng hóa thành công!" };
                    return Json(data);
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

        [Route("ThamDinhGia/DanhMuc/ChiTiet/Edit")]
        [HttpPost]
        public JsonResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.hh", "Edit"))
                {
                    var model = _db.ThamDinhGiaDmHh.FirstOrDefault(p => p.Id == Id);
                    var dvt = _db.DmDvt.ToList();
                    if (model != null)
                    {
                        string result = "<div class='modal-body' id='frm_edit'>";
                        result += "<div class='row'>";

                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Tên hàng hóa*</label>";
                        result += "<input type='text' class='form-control' id='tenhh_edit' name='tenhh_edit' value='" + model.Tenhanghoa + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Thông số kỹ thuật</label>";
                        result += "<input type='text' class='form-control' id='tskt_edit' name='tskt_edit' value='" + model.Thongsokt + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-12'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Xuất xứ</label>";
                        result += "<input type='text' class='form-control' id='xuatxu_edit' name='xuatxu_edit' value='" + model.Xuatxu + "'/>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-10'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>Đơn vị tính</label>";
                        result += "<select class='form-control kt_select2_1_modal' id='dvt_edit' name='dvt_edit'>";
                        foreach (var item in dvt)
                        {
                            result += "<option value='" + item.Dvt + "' " + ((string)model.Dvt == item.Dvt ? "selected" : "") + ">" + item.Dvt + "</option>";
                        }
                        result += "</select>";
                        result += "</div>";
                        result += "</div>";
                        result += "<div class='col-xl-2'>";
                        result += "<div class='form-group fv-plugins-icon-container'>";
                        result += "<label>&nbsp;&nbsp;&nbsp;</label>";
                        result += "<button type='button' class='btn btn-default' data-target='#Dvt_Modal_Edit' data-toggle='modal'><i class='la la-plus'></i></button>";
                        result += "</div>";
                        result += "</div>";

                        result += "</div>";
                        result += "<input hidden class='form-control' id='id_edit' name='id_edit' value='" + model.Id + "'/>";
                        result += "<input hidden class='form-control' id='manhom_edit' name='manhom_edit' value='" + model.Manhom + "'/>";
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

        [Route("ThamDinhGia/DanhMuc/ChiTiet/Update")]
        [HttpPost]
        public IActionResult Update(int Id, string Manhom, /*string Mahh,*/ string Tenhh, string Tskt, string Xuatxu, string Dvt)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.hh", "Edit"))
                {
                    /*int check = _db.ThamDinhGiaDmHh.Where(t => t.Manhom == Manhom && t.Mahanghoa == Mahh && t.Id != Id).Count();*/
                    var model = _db.ThamDinhGiaDmHh.FirstOrDefault(t => t.Id == Id);
                    model.Tenhanghoa = Tenhh;
                    model.Thongsokt = Tskt;
                    model.Xuatxu = Xuatxu;
                    model.Dvt = Dvt;
                    model.Updated_at = DateTime.Now;

                    _db.ThamDinhGiaDmHh.Update(model);
                    _db.SaveChanges();

                    var check_dvt = _db.DmDvt.FirstOrDefault(t => t.Dvt == Dvt);

                    if (check_dvt == null)
                    {
                        var dvt = new DmDvt
                        {
                            Dvt = Dvt,
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        };
                        _db.DmDvt.Add(dvt);
                        _db.SaveChanges();
                    }

                    var data = new { status = "success", message = "Cập nhật thành công!" };
                    return Json(data);

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
