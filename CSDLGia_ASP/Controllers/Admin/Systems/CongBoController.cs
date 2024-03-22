using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CSDLGia_ASP.Controllers.HeThong
{
    public class CongBoController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public CongBoController(CSDLGiaDBContext db)
        {
            _db = db;
        }


        [Route("CongBo")]
        [HttpGet]
        public IActionResult Index(string Phanloai, string Loaivb)
        {
            ViewBag.bSession = false;
            var serverName = Request.Host.Host;

            HttpContext.Session.SetString("ServerName", serverName);

            if (string.IsNullOrEmpty(Phanloai))
            {
                Phanloai = "gia";
            }
            if (string.IsNullOrEmpty(Loaivb))
            {
                Loaivb = "all";
            }
            var model = _db.VbQlNn.Where(t => t.Phanloai == Phanloai).ToList();
            if (Loaivb != "all")
            {
                model = model.Where(t => t.Loaivb == Loaivb).ToList();
            }

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                ViewBag.bSession = true;
                model = model.Where(t => t.Madv == Helpers.GetSsAdmin(HttpContext.Session, "Madv")).ToList();
            }

            ViewData["Title"] = "Công bố thông tin";
            ViewData["Phanloai"] = Phanloai;
            ViewData["Loaivb"] = Loaivb;
            //return Ok(model);
            return View("Views/Admin/Systems/CongBo/VanBanQLNN.cshtml", model);

        }

        [Route("CongBo/Show")]
        [HttpPost]
        public JsonResult Show(int Id)
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                var model = _db.VbQlNn.FirstOrDefault(t => t.Id == Id);
                string result = "<div class='modal-body' id='frm_show'>";
                result += "<div class='row'>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form-group fv-plugins-icon-container'>";
                result += "<label style='font-weight:bold'>Nội dung: </label>";
                result += "<span style='color:blue'>" + model.Tieude + "</span>";
                result += "</div>";
                result += "</div>";
                result += "<div class='col-xl-12'>";
                result += "<div class='form -group fv-plugins-icon-container'>";
                result += "<label style='font-weight:bold'>File đính kèm</label>";

                if (model.Ipf1 != null && model.Ipf1.Length > 0)
                {
                    result += "<p>";
                    result += "1. ";
                    result += "<a href='/UpLoad/File/VbQlNn/" + model.Ipf1 + "' target='_blank' class='btn btn-link'";
                    result += " onclick='window.open(`/UpLoad/File/VbQlNn/" + model.Ipf1 + "`, `mywin`, `left=20,top=20,width=500,height=500,toolbar=1,resizable=0`); return false;'>";
                    result += model.Ipf1 + "</a>";
                    result += "</p>";
                }
                if (model.Ipf2 != null && model.Ipf2.Length > 0)
                {
                    result += "<p>";
                    result += "2. ";
                    result += "<a href='/UpLoad/File/VbQlNn/" + model.Ipf2 + "' target='_blank' class='btn btn-link'";
                    result += " onclick='window.open(`/UpLoad/File/VbQlNn/" + model.Ipf2 + "`, `mywin`, `left=20,top=20,width=500,height=500,toolbar=1,resizable=0`); return false;'>";
                    result += model.Ipf2 + "</a>";
                    result += "</p>";
                }
                if (model.Ipf3 != null && model.Ipf3.Length > 0)
                {
                    result += "<p>";
                    result += "3. ";
                    result += "<a href='/UpLoad/File/VbQlNn/" + model.Ipf3 + "' target='_blank' class='btn btn-link'";
                    result += " onclick='window.open(`/UpLoad/File/VbQlNn/" + model.Ipf3 + "`, `mywin`, `left=20,top=20,width=500,height=500,toolbar=1,resizable=0`); return false;'>";
                    result += model.Ipf3 + "</a>";
                    result += "</p>";
                }
                if (model.Ipf4 != null && model.Ipf4.Length > 0)
                {
                    result += "<p>";
                    result += "4. ";
                    result += "<a href='/UpLoad/File/VbQlNn/" + model.Ipf4 + "' target='_blank' class='btn btn-link'";
                    result += " onclick='window.open(`/UpLoad/File/VbQlNn/" + model.Ipf4 + "`, `mywin`, `left=20,top=20,width=500,height=500,toolbar=1,resizable=0`); return false;'>";
                    result += model.Ipf4 + "</a>";
                    result += "</p>";
                }
                if (model.Ipf5 != null && model.Ipf5.Length > 0)
                {
                    result += "<p>";
                    result += "5. ";
                    result += "<a href='/UpLoad/File/VbQlNn/" + model.Ipf5 + "' target='_blank' class='btn btn-link'";
                    result += " onclick='window.open(`/UpLoad/File/VbQlNn/" + model.Ipf5 + "`, `mywin`, `left=20,top=20,width=500,height=500,toolbar=1,resizable=0`); return false;'>";
                    result += model.Ipf5 + "</a>";
                    result += "</p>";
                }

                result += "</div>";
                result += "</div>";

                result += "</div>";
                result += "</div>";

                var data = new { status = "success", message = result };
                return Json(data);
            }
            else
            {
                var data = new { status = "error", message = "Bạn kêt thúc phiên đăng nhập! Đăng nhập lại để tiếp tục công việc" };
                return Json(data);
            }
        }

        [Route("CongBo/MobileApp")]
        [HttpGet]
        public IActionResult MobileApp()
        {
            ViewBag.bSession = false;
            ViewData["Title"] = "Mobile App";
            return View("Views/Admin/Systems/CongBo/MobileApp.cshtml");
        }
    }
}
