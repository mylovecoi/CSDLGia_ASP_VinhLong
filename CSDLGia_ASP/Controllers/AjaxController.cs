using CSDLGia_ASP.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CSDLGia_ASP.Controllers
{
    public class AjaxController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public AjaxController(CSDLGiaDBContext db)
        {
            _db = db;
        }




        [Route("Ajax/GetXaPhuong")]
        [HttpPost]
        public JsonResult GetSeSelectXaPhuong(string MaDiaBan, string KeySelect)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                if (string.IsNullOrEmpty(MaDiaBan))
                {
                    string result = "";
                    result = "<select class='form-control' id='" + KeySelect + "' name='" + KeySelect + "'> ";
                    result += "<option value='all'>---Chọn xã phường---</option>";
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
                    result += "<option value='all'>---Chọn xã phường---</option>";
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
    }
}
