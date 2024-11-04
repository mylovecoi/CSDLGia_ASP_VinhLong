using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.KetNoiGiaQG
{
    public class KetNoiGiaDaoTaoController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public KetNoiGiaDaoTaoController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("KetNoiGiaDaoTao/ThietLap")]
        [HttpGet]
        public IActionResult ThietLap()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (Helpers.CheckPermission(HttpContext.Session, "csdltdg.tdg.bc", "Index"))
                {
                    DateTime nowDate = DateTime.Now;
                    DateTime firstDayCurrentYear = new DateTime(nowDate.Year, 1, 1);
                    DateTime lastDayCurrentYear = new DateTime(nowDate.Year, 12, 31);
                    ViewData["ngaytu"] = firstDayCurrentYear.ToString("yyyy-MM-dd");
                    ViewData["ngayden"] = lastDayCurrentYear.ToString("yyyy-MM-dd");

                    ViewData["Dsdiaban"] = _db.DsDiaBan;
                    ViewData["Dsdonvi"] = _db.DsDonVi.Where(t => t.ChucNang == "TONGHOP");
                    ViewData["Title"] = "Báo cáo tổng hợp tài sản thẩm định giá";
                    ViewData["MenuLv1"] = "menu_giaqg";
                    ViewData["MenuLv2"] = "menu_giaqg_giaoduc";
                    ViewData["MenuLv3"] = "menu_giaqg_giaoduc_thietlap";
                    ViewData["DanhSachHoSo"] = _db.ThamDinhGia.Where(t => t.Thoidiem >= firstDayCurrentYear && t.Thoidiem <= lastDayCurrentYear && t.Trangthai == "HT");
                    return View("Views/Admin/Manages/ThamDinhGia/BaoCao/Index.cshtml");
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
