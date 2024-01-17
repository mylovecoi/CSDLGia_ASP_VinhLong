using CSDLGia_ASP.Database;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Systems
{
    public class AjaxController : Controller

    {
        private readonly CSDLGiaDBContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public AjaxController(CSDLGiaDBContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
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

    }
}

