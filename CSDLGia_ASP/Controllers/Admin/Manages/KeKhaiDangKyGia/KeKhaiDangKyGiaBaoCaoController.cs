using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Services;
using CSDLGia_ASP.ViewModels.Manages.KeKhaiGia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using CSDLGia_ASP.Models.Systems;

namespace CSDLGia_ASP.Controllers.Admin.Manages.KeKhaiDangKyGia
{
    public class KeKhaiDangKyGiaBaoCaoController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IDsDonviService _dsDonviService;

        public KeKhaiDangKyGiaBaoCaoController(CSDLGiaDBContext db, IDsDonviService dsDonviService)
        {
            _db = db;
            _dsDonviService = dsDonviService;
        }

        [Route("BaoCaoKeKhaiDangKyGia")]
        [HttpGet]
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kekhaidangkygia.baocao", "Index"))
                {
                    var model_donvi = _dsDonviService.GetListDonvi(Helpers.GetSsAdmin(HttpContext.Session, "Madv"));
                    List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();

                    var data_nghe = _db.DmNgheKd.Where(t => t.Theodoi == "TD").ToList(); // Lấy toàn bộ dữ liệu ra bằng ToList()

                    // Lọc dữ liệu sử dụng LINQ to Objects thay vì LINQ to Entities
                    data_nghe = data_nghe.Where(x => list_madv.Any(v => x.Madv.Split(',').Contains(v))).ToList();

                    ViewData["DmNgheKd"] = data_nghe;
                    ViewData["Title"] = "Báo cáo kê khai đăng ký giá";
                    ViewData["MenuLv1"] = "menu_kekhaidangkygia";
                    ViewData["MenuLv2"] = "menu_kekhaidangkygia_baocao";
                    return View("Views/Admin/Manages/KeKhaiDangKyGia/BaoCao/Index.cshtml");
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

        [Route("BaoCaoKeKhaiDangKyGia/BaoCaoTongHop")]
        [HttpPost]
        public IActionResult BcTongHop(string manghe, string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kekhaidangkygia.baocao", "Index"))
                {
                    var model_donvi = _dsDonviService.GetListDonvi(Helpers.GetSsAdmin(HttpContext.Session, "Madv"));
                    List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();
                    var data_nghe = _db.DmNgheKd.Where(t => t.Theodoi == "TD").ToList(); // Lấy toàn bộ dữ liệu ra bằng ToList()
                    // Lọc dữ liệu sử dụng LINQ to Objects thay vì LINQ to Entities
                    data_nghe = data_nghe.Where(x => list_madv.Any(v => x.Madv.Split(',').Contains(v))).ToList();
                    List<string> list_manghe = data_nghe.Select(t => t.Manghe).ToList();
                    List<string> list_trangthaihs = new List<string> { "DD", "CB"};
                    var datahoso = _db.KeKhaiDangKyGia.Where(t => list_madv.Contains(t.MaCqCq) && list_trangthaihs.Contains(t.TrangThai) && list_manghe.Contains(t.MaNghe));
                    if(manghe != "all")
                    {
                        datahoso = datahoso.Where(t => t.MaNghe == manghe);
                    }
                    if (phanloai == "ngaychuyen")
                    {
                        if (time == "ngay")
                        {
                            datahoso = datahoso.Where(t => t.NgayChuyen >= tungay && t.NgayChuyen <= denngay);
                        }
                        else if (time == "thang")
                        {
                            datahoso = datahoso.Where(t => t.NgayChuyen.Month == int.Parse(thang) && t.NgayChuyen.Year == int.Parse(nam));
                        }
                        else
                        {
                            if (quy == "1")
                            {
                                tungay = new DateTime(int.Parse(nam), 1, 1);
                                denngay = new DateTime(int.Parse(nam), 3, 31);
                            }
                            else if (quy == "2")
                            {
                                tungay = new DateTime(int.Parse(nam), 4, 1);
                                denngay = new DateTime(int.Parse(nam), 6, 30);
                            }
                            else if (quy == "3")
                            {
                                tungay = new DateTime(int.Parse(nam), 7, 1);
                                denngay = new DateTime(int.Parse(nam), 9, 30);
                            }
                            else
                            {
                                tungay = new DateTime(int.Parse(nam), 10, 1);
                                denngay = new DateTime(int.Parse(nam), 12, 31);
                            }

                            datahoso = datahoso.Where(t => t.NgayChuyen >= tungay && t.NgayChuyen <= denngay);
                        }
                    }
                    else
                    {
                        if (time == "ngay")
                        {
                            datahoso = datahoso.Where(t => t.NgayDuyet >= tungay && t.NgayDuyet <= denngay);
                        }
                        else if (time == "thang")
                        {
                            datahoso = datahoso.Where(t => t.NgayDuyet.Month == int.Parse(thang) && t.NgayDuyet.Year == int.Parse(nam));
                        }
                        else
                        {
                            if (quy == "1")
                            {
                                tungay = new DateTime(int.Parse(nam), 1, 1);
                                denngay = new DateTime(int.Parse(nam), 3, 31);
                            }
                            else if (quy == "2")
                            {
                                tungay = new DateTime(int.Parse(nam), 4, 1);
                                denngay = new DateTime(int.Parse(nam), 6, 30);
                            }
                            else if (quy == "3")
                            {
                                tungay = new DateTime(int.Parse(nam), 7, 1);
                                denngay = new DateTime(int.Parse(nam), 9, 30);
                            }
                            else
                            {
                                tungay = new DateTime(int.Parse(nam), 10, 1);
                                denngay = new DateTime(int.Parse(nam), 12, 31);
                            }

                            datahoso = datahoso.Where(t => t.NgayDuyet >= tungay && t.NgayDuyet <= denngay);
                        }
                    }

                    var model = (from kk in datahoso
                                 join cskd in _db.KeKhaiDangKyGiaCSKD on kk.MaCsKd equals cskd.MaCsKd
                                 join com in _db.Company on cskd.MaDv equals com.Madv
                                 select new VMKkGia
                                 {
                                     Id = kk.Id,
                                     Mahs = kk.Mahs,
                                     Ngaynhap = kk.NgayQD,
                                     Ngayhieuluc = kk.NgayThucHien,
                                     Manghe = kk.MaNghe,
                                     Socv = kk.SoQD,
                                     Socvlk = kk.SoQdLk,
                                     Ngaycvlk = kk.NgayQdLk,
                                     Ytcauthanhgia = kk.Ytcauthanhgia,
                                     Thydggadgia = kk.Thydggadgia,
                                     Ttnguoinop = kk.ThongTinNguoiChuyen,
                                     Dtll = kk.SoDtNguoiChuyen,
                                     Sohsnhan = kk.SoHsDuyet,
                                     Ngaychuyen = kk.NgayChuyen,
                                     Ngaynhan = kk.NgayDuyet,
                                     Trangthai = kk.TrangThai,
                                     Madv = com.Madv,
                                     Tendn = com.Tendn,
                                 });

                    
                    var phanloaibc = _db.DmNgheKd.FirstOrDefault(t => t.Manghe == manghe)?.Phanloai ?? "Kê khai đăng ký giá";
                    var tennghe = _db.DmNgheKd.FirstOrDefault(t => t.Manghe == manghe)?.Tennghe ?? "";
                    ViewData["time"] = time;
                    ViewData["tungay"] = tungay;
                    ViewData["denngay"] = denngay;
                    ViewData["thang"] = thang;
                    ViewData["quy"] = quy;
                    ViewData["nam"] = nam;
                    ViewData["phanloaibc"] = phanloaibc;
                    ViewData["tennghe"] = tennghe;
                    ViewData["DmNgheKd"] = _db.DmNgheKd;
                    ViewData["Title"] = "Báo cáo kê khai đăng ký giá";
                    return View("Views/Admin/Manages/KeKhaiDangKyGia/BaoCao/BcTongHop.cshtml", model);
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

        [Route("BaoCaoKeKhaiDangKyGia/BaoCaoChiTiet")]
        [HttpPost]
        public IActionResult BcChiTiet(string manghe, string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdlmucgiahhdv.kekhaidangkygia.baocao", "Index"))
                {
                    var model = (from kk in _db.KeKhaiDangKyGia.Where(t => t.MaCqCq == Helpers.GetSsAdmin(HttpContext.Session, "Madv") && t.MaNghe == manghe/* && (t.TrangThai == "DD" || t.TrangThai == "CB")*/)
                                 join cskd in _db.KeKhaiDangKyGiaCSKD on kk.MaCsKd equals cskd.MaCsKd
                                 join com in _db.Company on cskd.MaDv equals com.Madv
                                 select new VMKkGia
                                 {
                                     Id = kk.Id,
                                     Mahs = kk.Mahs,
                                     Ngaynhap = kk.NgayQD,
                                     Ngayhieuluc = kk.NgayThucHien,
                                     Manghe = kk.MaNghe,
                                     Socv = kk.SoQD,
                                     Socvlk = kk.SoQdLk,
                                     Ngaycvlk = kk.NgayQdLk,
                                     Ytcauthanhgia = kk.Ytcauthanhgia,
                                     Thydggadgia = kk.Thydggadgia,
                                     Ttnguoinop = kk.ThongTinNguoiChuyen,
                                     Dtll = kk.SoDtNguoiChuyen,
                                     Sohsnhan = kk.SoHsDuyet,
                                     Ngaychuyen = kk.NgayChuyen,
                                     Ngaynhan = kk.NgayDuyet,
                                     Trangthai = kk.TrangThai,
                                     Madv = com.Madv,
                                     Tendn = com.Tendn,
                                 });

                    if (phanloai == "ngaychuyen")
                    {
                        if (time == "ngay")
                        {
                            model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                        }
                        else if (time == "thang")
                        {
                            model = model.Where(t => t.Ngaychuyen.Month == int.Parse(thang) && t.Ngaychuyen.Year == int.Parse(nam));
                        }
                        else
                        {
                            if (quy == "1")
                            {
                                tungay = new DateTime(int.Parse(nam), 1, 1);
                                denngay = new DateTime(int.Parse(nam), 3, 31);
                            }
                            else if (quy == "2")
                            {
                                tungay = new DateTime(int.Parse(nam), 4, 1);
                                denngay = new DateTime(int.Parse(nam), 6, 30);
                            }
                            else if (quy == "3")
                            {
                                tungay = new DateTime(int.Parse(nam), 7, 1);
                                denngay = new DateTime(int.Parse(nam), 9, 30);
                            }
                            else
                            {
                                tungay = new DateTime(int.Parse(nam), 10, 1);
                                denngay = new DateTime(int.Parse(nam), 12, 31);
                            }

                            model = model.Where(t => t.Ngaychuyen >= tungay && t.Ngaychuyen <= denngay);
                        }
                    }
                    else
                    {
                        if (time == "ngay")
                        {
                            model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                        }
                        else if (time == "thang")
                        {
                            model = model.Where(t => t.Ngaynhan.Month == int.Parse(thang) && t.Ngaynhan.Year == int.Parse(nam));
                        }
                        else
                        {
                            if (quy == "1")
                            {
                                tungay = new DateTime(int.Parse(nam), 1, 1);
                                denngay = new DateTime(int.Parse(nam), 3, 31);
                            }
                            else if (quy == "2")
                            {
                                tungay = new DateTime(int.Parse(nam), 4, 1);
                                denngay = new DateTime(int.Parse(nam), 6, 30);
                            }
                            else if (quy == "3")
                            {
                                tungay = new DateTime(int.Parse(nam), 7, 1);
                                denngay = new DateTime(int.Parse(nam), 9, 30);
                            }
                            else
                            {
                                tungay = new DateTime(int.Parse(nam), 10, 1);
                                denngay = new DateTime(int.Parse(nam), 12, 31);
                            }

                            model = model.Where(t => t.Ngaynhan >= tungay && t.Ngaynhan <= denngay);
                        }
                    }

                    ViewData["time"] = time;
                    ViewData["tungay"] = tungay;
                    ViewData["denngay"] = denngay;
                    ViewData["thang"] = thang;
                    ViewData["quy"] = quy;
                    ViewData["nam"] = nam;
                    ViewData["ct"] = _db.KeKhaiDangKyGiaCt.ToList();
                    ViewData["Title"] = "Báo cáo kê khai đăng ký giá";
                    return View("Views/Admin/Manages/KeKhaiDangKyGia/BaoCao/BcChiTiet.cshtml", model);
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

        [HttpPost("BaoCaoKeKhaiDangKyGia/ExportToExcel")]
        public IActionResult ExportToExcel(string manghe, string phanloai, string time, DateTime tungay, DateTime denngay, string thang, string quy, string nam)
        {
            var model_donvi = _dsDonviService.GetListDonvi(Helpers.GetSsAdmin(HttpContext.Session, "Madv"));
            List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();
            var data_nghe = _db.DmNgheKd.Where(t => t.Theodoi == "TD").ToList();
            data_nghe = data_nghe.Where(x => list_madv.Any(v => x.Madv.Split(',').Contains(v))).ToList();
            List<string> list_manghe = data_nghe.Select(t => t.Manghe).ToList();
            List<string> list_trangthaihs = new List<string> { "DD", "CB" };
            var datahoso = _db.KeKhaiDangKyGia.Where(t => list_madv.Contains(t.MaCqCq) && list_trangthaihs.Contains(t.TrangThai) && list_manghe.Contains(t.MaNghe));
            if (manghe != "all")
            {
                datahoso = datahoso.Where(t => t.MaNghe == manghe);
            }
            if (phanloai == "ngaychuyen")
            {
                if (time == "ngay")
                {
                    datahoso = datahoso.Where(t => t.NgayChuyen >= tungay && t.NgayChuyen <= denngay);
                }
                else if (time == "thang")
                {
                    datahoso = datahoso.Where(t => t.NgayChuyen.Month == int.Parse(thang) && t.NgayChuyen.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    datahoso = datahoso.Where(t => t.NgayChuyen >= tungay && t.NgayChuyen <= denngay);
                }
            }
            else
            {
                if (time == "ngay")
                {
                    datahoso = datahoso.Where(t => t.NgayDuyet >= tungay && t.NgayDuyet <= denngay);
                }
                else if (time == "thang")
                {
                    datahoso = datahoso.Where(t => t.NgayDuyet.Month == int.Parse(thang) && t.NgayDuyet.Year == int.Parse(nam));
                }
                else
                {
                    if (quy == "1")
                    {
                        tungay = new DateTime(int.Parse(nam), 1, 1);
                        denngay = new DateTime(int.Parse(nam), 3, 31);
                    }
                    else if (quy == "2")
                    {
                        tungay = new DateTime(int.Parse(nam), 4, 1);
                        denngay = new DateTime(int.Parse(nam), 6, 30);
                    }
                    else if (quy == "3")
                    {
                        tungay = new DateTime(int.Parse(nam), 7, 1);
                        denngay = new DateTime(int.Parse(nam), 9, 30);
                    }
                    else
                    {
                        tungay = new DateTime(int.Parse(nam), 10, 1);
                        denngay = new DateTime(int.Parse(nam), 12, 31);
                    }

                    datahoso = datahoso.Where(t => t.NgayDuyet >= tungay && t.NgayDuyet <= denngay);
                }
            }

            var model = (from kk in datahoso
                         join cskd in _db.KeKhaiDangKyGiaCSKD on kk.MaCsKd equals cskd.MaCsKd
                         join com in _db.Company on cskd.MaDv equals com.Madv
                         select new VMKkGia
                         {
                             Id = kk.Id,
                             Mahs = kk.Mahs,
                             Ngaynhap = kk.NgayQD,
                             Ngayhieuluc = kk.NgayThucHien,
                             Manghe = kk.MaNghe,
                             Socv = kk.SoQD,
                             Socvlk = kk.SoQdLk,
                             Ngaycvlk = kk.NgayQdLk,
                             Ytcauthanhgia = kk.Ytcauthanhgia,
                             Thydggadgia = kk.Thydggadgia,
                             Ttnguoinop = kk.ThongTinNguoiChuyen,
                             Dtll = kk.SoDtNguoiChuyen,
                             Sohsnhan = kk.SoHsDuyet,
                             Ngaychuyen = kk.NgayChuyen,
                             Ngaynhan = kk.NgayDuyet,
                             Trangthai = kk.TrangThai,
                             Madv = com.Madv,
                             Tendn = com.Tendn,
                         });

            // Start exporting to excel
            var stream = new MemoryStream();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var xlPackage = new ExcelPackage(stream))
            {
                // Define a worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("Sheet1");

                // Styling
                var customStyle = xlPackage.Workbook.Styles.CreateNamedStyle("CustomStyle");

                customStyle.Style.Font.UnderLine = true;
                customStyle.Style.Font.Color.SetColor(Color.Red);

                // 1st row
                var record_id = 1;
                var startRow = 3;
                var row = startRow;

                worksheet.Cells["A1"].Value = "Báo cáo kê khai đăng ký giá";
                using (var r = worksheet.Cells[1, 1, 1, 9])
                {
                    r.Merge = true;
                    r.Style.Font.Color.SetColor(Color.Green);
                    r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(Color.Lavender);
                }

                worksheet.Cells["A2"].Value = "STT";
                worksheet.Cells["B2"].Value = "Số HS";
                worksheet.Cells["C2"].Value = "Tên doanh nghiệp";
                worksheet.Cells["D2"].Value = "Địa chỉ";
                worksheet.Cells["E2"].Value = "Điện thoại";
                worksheet.Cells["F2"].Value = "Lĩnh vực";
                worksheet.Cells["G2"].Value = "Ngày nhận hồ sơ";
                worksheet.Cells["H2"].Value = "Ngày thực hiện mức giá";
                worksheet.Cells["I2"].Value = "Ghi chú";
                worksheet.Cells[2, 1, 2, 9].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[2, 1, 2, 9].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

                row = 3;
                foreach (var item in model)
                {
                    worksheet.Cells[row, 1].Value = record_id++;
                    worksheet.Cells[row, 2].Value = item.Socv;
                    worksheet.Cells[row, 3].Value = item.Tendn;
                    worksheet.Cells[row, 4].Value = item.Diachi;
                    worksheet.Cells[row, 5].Value = item.Sdt;
                    worksheet.Cells[row, 6].Value = !string.IsNullOrEmpty(item.Manghe) ? (_db.DmNgheKd.FirstOrDefault(x => x.Manghe == item.Manghe)?.Tennghe ?? "") : "";
                    worksheet.Cells[row, 7].Value = Helpers.ConvertDateToStr(item.Ngaynhan);
                    worksheet.Cells[row, 8].Value = Helpers.ConvertDateToStr(item.Ngayhieuluc);
                    worksheet.Cells[row, 9].Value = item.Ghichu;

                    row++;
                }
                var currentRow = row++;
                worksheet.Cells[currentRow, 1].Value = "";
                worksheet.Cells[currentRow, 2].Value = "Tổng cộng: " + model.Count() + " hồ sơ";
                using (var r = worksheet.Cells[currentRow, 2, currentRow, 9])
                {
                    r.Merge = true;
                }
                // Define border styles
                var borderStyle = ExcelBorderStyle.Thin;
                var borderColor = Color.Black;

                // Apply border to header cells
                using (var headerRange = worksheet.Cells["A2:I2"])
                {
                    headerRange.Style.Border.Top.Style = borderStyle;
                    headerRange.Style.Border.Bottom.Style = borderStyle;
                    headerRange.Style.Border.Left.Style = borderStyle;
                    headerRange.Style.Border.Right.Style = borderStyle;
                    headerRange.Style.Border.Top.Color.SetColor(borderColor);
                    headerRange.Style.Border.Bottom.Color.SetColor(borderColor);
                    headerRange.Style.Border.Left.Color.SetColor(borderColor);
                    headerRange.Style.Border.Right.Color.SetColor(borderColor);
                }

                // Apply border to data cells
                for (int i = startRow; i < row; i++)
                {
                    var dataRange = worksheet.Cells[i, 1, i, 9];
                    dataRange.Style.Border.Top.Style = borderStyle;
                    dataRange.Style.Border.Bottom.Style = borderStyle;
                    dataRange.Style.Border.Left.Style = borderStyle;
                    dataRange.Style.Border.Right.Style = borderStyle;
                    dataRange.Style.Border.Top.Color.SetColor(borderColor);
                    dataRange.Style.Border.Bottom.Color.SetColor(borderColor);
                    dataRange.Style.Border.Left.Color.SetColor(borderColor);
                    dataRange.Style.Border.Right.Color.SetColor(borderColor);
                }

                xlPackage.Workbook.Properties.Title = "Báo cáo kê khai đăng ký giá";
                xlPackage.Workbook.Properties.Author = "Hùng Anh";

                xlPackage.Save();
            }

            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Báo cáo kê khai đăng ký giá.xlsx");
        }
    }
}
