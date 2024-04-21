using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.CongBo
{
    public class VanBanQuanLyNhaNuocController : Controller
    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public VanBanQuanLyNhaNuocController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }

        [Route("CongBo/VanBanQuanLyNhaNuoc")]
        [HttpGet]
        public IActionResult Index(string Phanloai, string Loaivb)
        {
            if (string.IsNullOrEmpty(Phanloai))
            {
                Phanloai = "gia";
            }
            if (string.IsNullOrEmpty(Loaivb))
            {
                Loaivb = "all";
            }
            var model = _db.VbQlNn.Where(t => t.Phanloai == Phanloai);
            if (Loaivb != "all")
            {
                model = model.Where(t => t.Loaivb == Loaivb);
            }

            ViewData["Phanloai"] = Phanloai;
            ViewData["Loaivb"] = Loaivb;
            ViewData["Title"] = "Văn bản quản lý nhà nước về giá";
            ViewData["MenuLv1"] = "menu_cb";
            ViewData["MenuLv2"] = "menu_vbqlcb";
            return View("Views/Admin/CongBo/VanBanQuanLyNhaNuoc/Index.cshtml", model);
        }



    }
}
