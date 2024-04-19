using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.KeKhaiDangKyGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.KeKhaiDangKyGia
{
    public class KeKhaiDangKyGiaCtController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public KeKhaiDangKyGiaCtController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [HttpPost("KeKhaiDangKyGiaCt/Store")]
        public IActionResult Store(KeKhaiDangKyGiaCt requests)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kekhaidangkygia.thongtin", "Create"))
                {
                    var model = new CSDLGia_ASP.Models.Manages.KeKhaiDangKyGia.KeKhaiDangKyGiaCt
                    {
                        MaCsKd = requests.MaCsKd,
                        Mahs = requests.Mahs,
                        TenDvCungUng = requests.TenDvCungUng,
                        QuyCachChatLuong = requests.QuyCachChatLuong,
                        ThoiGianThucHien = requests.ThoiGianThucHien,
                        MucGiaKeKhaiLk = requests.MucGiaKeKhaiLk,
                        MucGiaKeKhai = requests.MucGiaKeKhai,
                        HinhThucKinhDoanh = requests.HinhThucKinhDoanh,
                        TrangThai = "CXD"
                    };
                    _db.KeKhaiDangKyGiaCt.Add(model);
                    _db.SaveChanges();
                    string result = this.GetData(requests.Mahs);
                    var data = new { status = "success", message = result };
                    return Json(data);
                }
                else
                {
                    var data = new { status = "error", message = "Bạn không có quyền truy cập chức năng này!!!" };
                    return Json(data);
                }
            }
            else
            {
                var data = new { status = "error", message = "Kết thúc phiên làm việc. Bạn cần đăng nhập lại!!!" };
                return Json(data);
            }
        }

        [HttpPost("KeKhaiDangKyGiaCt/Edit")]
        public IActionResult Edit(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kekhaidangkygia.thongtin", "Edit"))
                {
                    var model = _db.KeKhaiDangKyGiaCt.FirstOrDefault(t => t.Id == Id);

                    string result = " <div class='modal-body' id='frm_edit'>";
                    result += " <div class='row'>";
                    result += "<div class='col-xl-12'>";
                    result += "<div class='form-group'>";
                    result += "<label><b>Tên dịch vụ cung ứng:</b></label>";
                    result += "<input type='text' id='tendvcungung_edit' name='tendvcungung_edit' class='form-control' value='" + model.TenDvCungUng + "' />";
                    result += "</div>";
                    result += "</div>";
                    result += "<div class='col-xl-12'>";
                    result += "<div class='form-group'>";
                    result += "<label><b>Quy cách chất lượng dịch vụ:</b></label>";
                    result += "<input type='text' id='quycachchatluong_edit' name='quycachchatluong_edit' class='form-control' value='" + model.QuyCachChatLuong + "' />";
                    result += "</div>";
                    result += "</div>";
                    result += "<div class='col-xl-12'>";
                    result += "<div class='form-group'>";
                    result += "<label><b>Thời điểm áp dụng:</b></label>";
                    result += "<input type='text' id='thoigianapdung_edit' name='thoigianapdung_edit' class='form-control' value='" + model.ThoiGianThucHien + "'>";
                    result += "</div>";
                    result += "</div>";
                    result += "<div class='col-xl-6'>";
                    result += "<div class='form-group fv-plugins-icon-container'>";
                    result += "<label><b>Giá kê khai hiện hành*</b></label>";
                    result += "<input type='text' id='gialk_edit' name='gialk_edit' class='form-control money-decimal-mask' style='font-weight: bold' value='" + Helpers.ConvertDbToStr(model.MucGiaKeKhaiLk) +"' />";
                    result += "</div>";
                    result += "</div>";
                    result += "<div class='col-xl-6'>";
                    result += "<div class='form-group fv-plugins-icon-container'>";
                    result += "<label><b>Giá kê khai mới*</b></label>";
                    result += "<input type='text' id='giakk_edit' name='giakk_edit' class='form-control money-decimal-mask' style='font-weight: bold' value='" + Helpers.ConvertDbToStr(model.MucGiaKeKhai) +"' />";
                    result += "</div>";
                    result += "</div>";
                    result += "<div class='col-xl-12'>";
                    result += "<div class='form-group fv-plugins-icon-container'>";
                    result += "<label><b>Hình thức kinh doanh</b></label>";
                    result += "<input type='text' id='hinhthuckinhdoanh_edit' name='hinhthuckinhdoanh_edit' class='form-control'  value='" + model.HinhThucKinhDoanh + "'>";
                    result += "</div>";
                    result += "</div>";
                    result += "<input hidden id='id_edit' name='id_edit' value='" + model.Id + "'>";
                    result += "</div>";
                    result += "</div>";
                    var data = new { status = "success", message = result };
                    return Json(data);
                }
                else
                {
                    var data = new { status = "error", message = "Bạn không có quyền truy cập chức năng này!!!" };
                    return Json(data);
                }
            }
            else
            {
                var data = new { status = "error", message = "Kết thúc phiên làm việc. Bạn cần đăng nhập lại!!!" };
                return Json(data);
            }
        }

        [HttpPost("KeKhaiDangKyGiaCt/Update")]
        public IActionResult Update(KeKhaiDangKyGiaCt requests)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kekhaidangkygia.thongtin", "Edit"))
                {
                    var model = _db.KeKhaiDangKyGiaCt.FirstOrDefault(t=>t.Id == requests.Id);

                    model.TenDvCungUng = requests.TenDvCungUng;
                    model.QuyCachChatLuong = requests.QuyCachChatLuong;
                    model.ThoiGianThucHien = requests.ThoiGianThucHien;
                    model.MucGiaKeKhaiLk = requests.MucGiaKeKhaiLk;
                    model.MucGiaKeKhai = requests.MucGiaKeKhai;
                    model.HinhThucKinhDoanh = requests.HinhThucKinhDoanh;                      
                    
                    _db.KeKhaiDangKyGiaCt.Update(model);
                    _db.SaveChanges();
                    string result = this.GetData(requests.Mahs);
                    var data = new { status = "success", message = result };
                    return Json(data);
                }
                else
                {
                    var data = new { status = "error", message = "Bạn không có quyền truy cập chức năng này!!!" };
                    return Json(data);
                }
            }
            else
            {
                var data = new { status = "error", message = "Kết thúc phiên làm việc. Bạn cần đăng nhập lại!!!" };
                return Json(data);
            }
        }

        [HttpPost("KeKhaiDangKyGiaCt/Remove")]
        public IActionResult Remove(int Id, string Mahs)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kekhaidangkygia.thongtin", "Delete"))
                {
                    var model = _db.KeKhaiDangKyGiaCt.FirstOrDefault(t => t.Id == Id);                  

                    _db.KeKhaiDangKyGiaCt.Remove(model);
                    _db.SaveChanges();
                    string result = this.GetData(Mahs);
                    var data = new { status = "success", message = result };
                    return Json(data);
                }
                else
                {
                    var data = new { status = "error", message = "Bạn không có quyền truy cập chức năng này!!!" };
                    return Json(data);
                }
            }
            else
            {
                var data = new { status = "error", message = "Kết thúc phiên làm việc. Bạn cần đăng nhập lại!!!" };
                return Json(data);
            }
        }

        public string GetData(string Mahs)
        {
            var model = _db.KeKhaiDangKyGiaCt.Where(t => t.Mahs == Mahs);
            int record_id = 1;
            string result = " <div class='card-body' id='frm_data'>";
            result += "<table class='table table-striped table-bordered table-hover class-nosort'>";
            result += "<thead>";
            result += "<tr style='text-align:center'>";
            result += "<th width='2%'>STT</th>";
            result += "<th>Tên dịch vụ cung ứng</th>";
            result += "<th>Quy cách chất lượng</th>";
            result += "<th>Thời điểm áp dụng</th>";
            result += "<th width='10%'>Mức giá liền kề</th>";
            result += "<th width='10%'>Mức giá kê khai</th>";
            result += "<th width='10%'>Hình thức kinh doanh</th>";
            result += "<th width='10%'>Thao tác</th>";
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";

            foreach (var item in model)
            {
                result += "<tr>";
                result += "<td style='text-align:center'>" + (record_id++) + "</td>";
                result += "<td style='text-align:left'>" + item.TenDvCungUng + "</td>";
                result += "<td style='text-align:left'>" + item.QuyCachChatLuong + "</td>";
                result += "<td style='text-align:center'>" + item.ThoiGianThucHien + "</td>";
                result += "<td style='text-align: right; font-weight:bold'>" + Helpers.ConvertDbToStr(item.MucGiaKeKhaiLk) + "</td>";
                result += "<td style='text-align: right; font-weight:bold'>" + Helpers.ConvertDbToStr(item.MucGiaKeKhai) + "</td>";
                result += "<td style='text-align: right; font-weight:bold'>" + item.HinhThucKinhDoanh + "</td>";
                result += "<td>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' onclick='Edit(`" + item.Id + "`)' title='Chỉnh sửa'>";
                result += "<i class='icon-lg la la-edit text-primary'></i>";
                result += "</button>";
                result += "<button type='button' class='btn btn-sm btn-clean btn-icon' onclick='Delete(`" + item.Id + "`,`" + item.TenDvCungUng + "`)' title='Xóa'>";
                result += "<i class='icon-lg la la-trash text-danger'></i>";
                result += "</button>";
                result += "</td>";
                result += " </tr>";
            }

            result += "</tbody>";
            result += "</table>";
            result += "</div>";
            return result;
        }
    }
}
