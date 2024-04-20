using CSDLGia_ASP.Database;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;


namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class TrangThaiHoSoController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public TrangThaiHoSoController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("TrangThaiHoSo")]
        [HttpGet]
        public IActionResult Index( string Mahs)
        {
            var model = _db.TrangThaiHoSo.Where(t => t.MaHoSo == Mahs);
            ViewData["Title"] = "Lịch sử hồ sơ";
            return View("Views/Admin/Systems/TrangThaiHoSo/Index.cshtml", model);
        }
    }
}
