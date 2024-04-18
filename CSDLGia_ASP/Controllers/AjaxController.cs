using CSDLGia_ASP.Database;
using CSDLGia_ASP.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers
{
    public class AjaxController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private IDsDiaBanService _IDsDiaBan;

        public AjaxController(CSDLGiaDBContext db, IDsDiaBanService IDsDiaBan)
        {
            _db = db;
            _IDsDiaBan = IDsDiaBan;
        }

        [Route("Ajax/GetXaPhuong")]
        [HttpPost]
        public JsonResult GetSeSelectXaPhuong(string MaDiaBanHuyen, string KeySelect)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (string.IsNullOrEmpty(MaDiaBanHuyen))
                {
                    string result = "";
                    result = "<select class='form-control' id='" + KeySelect + "' name='" + KeySelect + "'> ";
                    //result += "<option value='all'>---Chọn xã phường---</option>";
                    result += "</select>";
                    var data = new { status = "success", message = result };
                    return Json(data);
                }
                else
                {

                    //var xaphuong = _db.DsDiaBan.Where(x =>x.Level=="X" && x.MaDiaBanCq == MaDiaBanHuyen);
                    //if (!xaphuong.Any())
                    //{
                    //    xaphuong = _db.DsDiaBan.Where(x=>x.Level=="X");
                    //}
                    var xaphuong = _IDsDiaBan.GetListDsDiaBan(MaDiaBanHuyen).Where(x=>x.Level=="X");
                    
                    string result = "";
                    result = "<select class='form-control' id='" + KeySelect + "' name='" + KeySelect + "'> ";
                    result += "<option value='all'>---Chọn xã phường---</option>";
                    foreach (var item in xaphuong)
                    {
                        result += "<option value='" + item.MaDiaBan + "'>" + item.TenDiaBan + "</option>";
                    }
                    result += "</select>";
                    var data = new { status = "success", message = result };
                    return Json(data);
                }
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }

        [Route("Ajax/GetXaPhuongNoAll")]
        [HttpPost]
        public JsonResult GetSeSelectXaPhuongNoAll(string MaDiaBan, string KeySelect)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (string.IsNullOrEmpty(MaDiaBan))
                {
                    string result = "";
                    result = "<select class='form-control' id='" + KeySelect + "' name='" + KeySelect + "'> ";
                    result += "</select>";
                    var data = new { status = "success", message = result };
                    return Json(data);
                }
                else
                {
                    var xaphuong = _db.DsXaPhuong.Where(x => x.Madiaban == MaDiaBan);
                    if (!xaphuong.Any())
                    {
                        xaphuong = _db.DsXaPhuong;
                    }

                    string result = "";
                    result = "<select class='form-control' id='" + KeySelect + "' name='" + KeySelect + "'> ";
                    foreach (var item in xaphuong)
                    {
                        result += "<option value='" + item.Maxp + "'>" + item.Tenxp + "</option>";
                    }
                    result += "</select>";
                    var data = new { status = "success", message = result };
                    return Json(data);
                }
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }

        [Route("Ajax/GetTowns")]
        [HttpPost]
        public JsonResult GetSeSelectTowns(string MaHuyen, string KeySelect)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (string.IsNullOrEmpty(MaHuyen))
                {
                    string result = "";
                    result = "<select class='form-control' id='" + KeySelect + "' name='" + KeySelect + "'> ";
                    result += "<option value='all'>---Chọn đơn vị---</option>";
                    result += "</select>";
                    var data = new { status = "success", message = result };
                    return Json(data);
                }
                else
                {
                    var towns = _db.Towns.Where(t => t.Mahuyen == MaHuyen);
                    string result = "";
                    result = "<select class='form-control' id='" + KeySelect + "' name='" + KeySelect + "'> ";
                    result += "<option value='all'>---Chọn đơn vị---</option>";
                    foreach (var item in towns)
                    {
                        result += "<option value='" + item.Maxa + "'>" + item.Tenxa + "</option>";
                    }
                    result += "</select>";
                    var data = new { status = "success", message = result };
                    return Json(data);
                }
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }

        [Route("Ajax/GetTownsNoAll")]
        [HttpPost]
        public JsonResult GetTownsNoAll(string MaHuyen, string KeySelect)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (string.IsNullOrEmpty(MaHuyen))
                {
                    string result = "";
                    result = "<select class='form-control' id='" + KeySelect + "' name='" + KeySelect + "'> ";
                    result += "<option value=''>---Chọn đơn vị---</option>";
                    result += "</select>";
                    var data = new { status = "success", message = result };
                    return Json(data);
                }
                else
                {
                    var towns = _db.Towns.Where(t => t.Mahuyen == MaHuyen);
                    string result = "";
                    result = "<select class='form-control' id='" + KeySelect + "' name='" + KeySelect + "'> ";
                    foreach (var item in towns)
                    {
                        result += "<option value='" + item.Maxa + "'>" + item.Tenxa + "</option>";
                    }
                    result += "</select>";
                    var data = new { status = "success", message = result };
                    return Json(data);
                }
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }

        [Route("Ajax/GetNghes")]
        [HttpGet]
        public IActionResult GetNghes(string Manghanh)
        {
            try
            {
                var nghes = _db.DmNgheKd.Where(nghe => nghe.Manganh == Manghanh).ToList();

                return Json(new { status = "success", nghes });
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ và trả về thông báo lỗi
                return Json(new { status = "error", message = ex.Message });
            }
        }

        [Route("Ajax/GetDvNhanHs")]
        [HttpGet]
        public IActionResult GetDvNhanHs(string Manghe)
        {
            try
            {
                var dmnghekd = _db.DmNgheKd.FirstOrDefault(t => t.Manghe == Manghe).Madv;
                var dsdonvi = _db.DsDonVi.Where(dv => dv.ChucNang == "NHAPLIEU" && dv.MaDv == dmnghekd).ToList();

                return Json(new { status = "success", dsdonvi });
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ và trả về thông báo lỗi
                return Json(new { status = "error", message = ex.Message });
            }
        }
    }
}
