using CSDLGia_ASP.Database;
using CSDLGia_ASP.Models.Manages.DinhGia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Controllers.Admin.API
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GetDataController : ControllerBase
    {
        private readonly CSDLGiaDBContext _db;

        public GetDataController(CSDLGiaDBContext db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("GetGiarung")]
        public async Task<ActionResult<IEnumerable<GiaRung>>> GetGiarung()
        {
            var giarung = await _db.GiaRung.ToListAsync();
            return giarung;
        }
    }
}
