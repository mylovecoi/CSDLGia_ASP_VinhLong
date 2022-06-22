using CSDLGia_ASP.DAO;
using CSDLGia_ASP.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CSDLGia_ASP.Controllers.HeThong
{
    public class CongBoController : Controller
    {
        private readonly CSDLGiaDBContext _dbGia;

        public CongBoController(CSDLGiaDBContext dbGia)
        {
            _dbGia = dbGia;
        }


        [Route("CongBo")]
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.bSession = false;
            var m_chucnang = new DMChucNangDao(_dbGia);
            var lstChucNang = m_chucnang.ChucNang();
            // List<tblDMChucNang> lstChucNang = new DMChucNangDao(_dbGia).GetChucNang();

            string phanQuyen = JsonConvert.SerializeObject(lstChucNang);
            HttpContext.Session.SetString("ChucNang", phanQuyen);
            //string test = HttpContext.Session.GetString("ChucNang");

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SsAdmin")))
            {
                ViewBag.bSession = true;
            }
            /*return Ok(ViewBag.bSession);*/
            var m_HeThong = new HeThongDao(_dbGia).GetTblHeThong();

            ViewBag.HeThong = m_HeThong;
            ViewBag.ChucNang = lstChucNang;
            ViewData["Title"] = "Công bố thông tin";
            return View("Views/Admin/Systems/CongBo/VanBanQLNN.cshtml");
        }
    }
}
