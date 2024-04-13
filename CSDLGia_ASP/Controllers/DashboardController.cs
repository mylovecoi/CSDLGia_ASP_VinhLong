using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Manages.DinhGia;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers
{
    public class DashboardController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public DashboardController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [HttpPost("Dashboard/GetBangGiaDat")]
        public IActionResult GetBangGiaDat()
        {
            var HoSo = _db.GiaDatDiaBan.Where(t => t.Trangthai == "CB" && t.Thoidiem <= DateTime.Now).OrderByDescending(t => t.Thoidiem)
                                                                               .FirstOrDefault();
            string Mahs = HoSo?.Mahs;
            string result = "<div class='card card-custom card-stretch gutter-b' id='data_load'>";
            result += "<div class='card-header border-0 py-5'>";
            result += "<h3 class='card-title align-items-start flex-column'>";
            result += "<span class='card-label font-weight-bolder text-dark'>Bảng giá đất địa bàn năm " + (HoSo?.Thoidiem.Year.ToString() ?? "") + "</span>";
            result += "<span class='text-muted mt-3 font-weight-bold font-size-sm'> Số QĐ: " + (HoSo?.Soqd ?? "") + "- Thời điểm: " + Helpers.ConvertDateToStr(HoSo?.Thoidiem ?? DateTime.MinValue)  +"</span>";
            result += "</h3>";
            result += "</div>";
            result += "<div class='card-body pt-0 pb-3'>";
            result += "<div class='tab-content'>";
            result += "<table class='table table-striped table-hover table-responsive-lg' id='sample_4'>";
            result += "<thead>";
            result += "<tr class='text-center text-uppercase'>";
            result += "<th>Loại đất</th>";
            result += "<th>Tên đường phố</th>";
            result += "<th>Loại đường</th>";
            result += "<th>Điểm đầu/Điểm cuối</th>";
            result += "<th>Hệ số</th>";
            result += "<th>VT1</th>";
            result += "<th>VT2</th>";
            result += "<th>VT3</th>";
            result += "<th>VT4</th>";
            result += "<th>VT5</th>";
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";
            if (Mahs != null)
            {
                var datact = (from dat in _db.GiaDatDiaBanCt.Where(t => t.Mahs == Mahs)
                              join dm in _db.DmLoaiDat on dat.Maloaidat equals dm.Maloaidat
                              select new CSDLGia_ASP.Models.Manages.DinhGia.GiaDatDiaBanCt
                              {
                                  Id = dat.Id,
                                  HienThi = dat.HienThi,
                                  Maloaidat = dat.Maloaidat,
                                  Mota = dat.Mota,
                                  Diemdau = dat.Diemdau,
                                  Diemcuoi = dat.Diemcuoi,
                                  Loaiduong = dat.Loaiduong,
                                  Hesok = dat.Hesok,
                                  Giavt1 = dat.Giavt1,
                                  Giavt2 = dat.Giavt2,
                                  Giavt3 = dat.Giavt3,
                                  Giavt4 = dat.Giavt4,
                                  Giavt5 = dat.Giavt5,
                                  Loaidat = dm.Loaidat,
                                  Sapxep = dat.Sapxep
                              });
                foreach (var item in datact.OrderBy(t => t.Sapxep))
                {
                    result += "<tr>";
                    result += "<td>" + item.Loaidat + "</td>";
                    result += "<td>" + item.Mota + "</td>";
                    result += "<td>" + item.Loaiduong + "</td>";
                    result += "<td>" + item.Diemdau + "<br />" + item.Diemcuoi + "</td>";
                    result += "<td style='text-align:center'>" + Helpers.ConvertDbToStr(item.Hesok) + "</td>";
                    result += "<td style='text-align:right'>" + Helpers.ConvertDbToStr(item.Giavt1) + "</td>";
                    result += "<td style='text-align:right'>" + Helpers.ConvertDbToStr(item.Giavt2) + "</td>";
                    result += "<td style='text-align:right'>" + Helpers.ConvertDbToStr(item.Giavt3) + "</td>";
                    result += "<td style='text-align:right'>" + Helpers.ConvertDbToStr(item.Giavt4) + "</td>";
                    result += "<td style='text-align:right'>" + @Helpers.ConvertDbToStr(item.Giavt5) + "</td>";
                    result += "</tr>";
                }
            }
            result += "</tbody>";
            result += "</table>";
            result += "</div>";
            result += "</div>";
            result += "</div>";

            var data = new { status = "success", message = result };
            return Json(data);
        }


        [HttpPost("Dashboard/GetDichVuLuuTru")]
        public IActionResult GetDichVuLuuTru()
        {
            string result = "<div class='card card-custom card-stretch gutter-b' id='data_load'>";
            result += "<div class='card-header border-0 py-5'>";
            result += "<h3 class='card-title align-items-start flex-column'>";
            result += "<span class='card-label font-weight-bolder text-dark'>Thông tin hồ sơ kê khai giá dịch vụ lưu trú</span>";
            result += "<span class='text-muted mt-3 font-weight-bold font-size-sm'>Thời điểm tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year + "</span>";
            result += "</h3>";
            result += "</div>";
            result += "<div class='card-body pt-0 pb-3'>";
            result += "<div class='tab-content'>";
            result += "<table class='table table-striped table-hover table-responsive-lg' id='sample_4'>";
            result += "<thead>";
            result += "<tr class='text-center text-uppercase'>";
            result += "<th>Tên đơn vị</th>";
            result += "<th>Số QĐ</th>";
            result += "<th>Thời điểm</th>";
            result += "<th>Số QĐ liển kề</th>";
            result += "<th>Thời điểm LK</th>";
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";

            result += "</tbody>";
            result += "</table>";
            result += "</div>";
            result += "</div>";
            result += "</div>";

            var data = new { status = "success", message = result };
            return Json(data);
        }

        [HttpPost("Dashboard/GetXangDau")]
        public IActionResult GetXangDau()
        {
            string result = "<div class='card card-custom card-stretch gutter-b' id='data_load'>";
            result += "<div class='card-header border-0 py-5'>";
            result += "<h3 class='card-title align-items-start flex-column'>";
            result += "<span class='card-label font-weight-bolder text-dark'>Thông tin hồ sơ đăng ký giá dịch vụ xăng dầu</span>";
            result += "<span class='text-muted mt-3 font-weight-bold font-size-sm'>Thời điểm tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year + "</span>";
            result += "</h3>";
            result += "</div>";
            result += "<div class='card-body pt-0 pb-3'>";
            result += "<div class='tab-content'>";
            result += "<table class='table table-striped table-hover table-responsive-lg' id='sample_4'>";
            result += "<thead>";
            result += "<tr class='text-center text-uppercase'>";
            result += "<th>Tên đơn vị</th>";
            result += "<th>Số QĐ</th>";
            result += "<th>Thời điểm</th>";
            result += "<th>Số QĐ liển kề</th>";
            result += "<th>Thời điểm LK</th>";
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";

            result += "</tbody>";
            result += "</table>";
            result += "</div>";
            result += "</div>";
            result += "</div>";

            var data = new { status = "success", message = result };
            return Json(data);
        }

        [HttpPost("Dashboard/GetDichVuLuHanh")]
        public IActionResult GetDichVuLuHanh()
        {
            string result = "<div class='card card-custom card-stretch gutter-b' id='data_load'>";
            result += "<div class='card-header border-0 py-5'>";
            result += "<h3 class='card-title align-items-start flex-column'>";
            result += "<span class='card-label font-weight-bolder text-dark'>Thông tin hồ sơ kê khai giá dịch vụ lữ hành</span>";
            result += "<span class='text-muted mt-3 font-weight-bold font-size-sm'>Thời điểm tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year + "</span>";
            result += "</h3>";
            result += "</div>";
            result += "<div class='card-body pt-0 pb-3'>";
            result += "<div class='tab-content'>";
            result += "<table class='table table-striped table-hover table-responsive-lg' id='sample_4'>";
            result += "<thead>";
            result += "<tr class='text-center text-uppercase'>";
            result += "<th>Tên đơn vị</th>";
            result += "<th>Số QĐ</th>";
            result += "<th>Thời điểm</th>";
            result += "<th>Số QĐ liển kề</th>";
            result += "<th>Thời điểm LK</th>";
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";

            result += "</tbody>";
            result += "</table>";
            result += "</div>";
            result += "</div>";
            result += "</div>";

            var data = new { status = "success", message = result };
            return Json(data);
        }

        [HttpPost("Dashboard/GetHHDVthietyeu")]
        public IActionResult GetHHDVthietyeu()
        {
            string result = "<div class='card card-custom card-stretch gutter-b' id='data_load'>";
            result += "<div class='card-header border-0 py-5'>";
            result += "<h3 class='card-title align-items-start flex-column'>";
            result += "<span class='card-label font-weight-bolder text-dark'>Thông tin giá HHDV thiết yếu</span>";
            result += "<span class='text-muted mt-3 font-weight-bold font-size-sm'>Thời điểm tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year + "</span>";
            result += "</h3>";
            result += "</div>";
            result += "<div class='card-body pt-0 pb-3'>";
            result += "<div class='tab-content'>";
            result += "<table class='table table-striped table-hover table-responsive-lg' id='sample_4'>";
            result += "<thead>";
            result += "<tr class='text-center text-uppercase'>";
            result += "<th>Tên đơn vị</th>";
            result += "<th>Số QĐ</th>";
            result += "<th>Thời điểm</th>";
            result += "<th>Số QĐ liển kề</th>";
            result += "<th>Thời điểm LK</th>";
            result += "</tr>";
            result += "</thead>";
            result += "<tbody>";

            result += "</tbody>";
            result += "</table>";
            result += "</div>";
            result += "</div>";
            result += "</div>";

            var data = new { status = "success", message = result };
            return Json(data);
        }
    }
}
