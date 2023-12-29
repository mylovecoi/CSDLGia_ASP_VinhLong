using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.ViewModels.Systems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSDLGia_ASP.Controllers.Admin.Manages.DinhGia.GiaDatPlCongBo
{
    public class GiaDatPlCongBoController : Controller
    {
        private readonly CSDLGiaDBContext _db;

        public GiaDatPlCongBoController(CSDLGiaDBContext db)
        {
            _db = db;
        }

        [Route("GiaDatPlCongBo/CongBo")]
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.bSession = true;
            return View("Views/Admin/Systems/CongBo/DatPlCongBo.cshtml");

        }


    }
}

