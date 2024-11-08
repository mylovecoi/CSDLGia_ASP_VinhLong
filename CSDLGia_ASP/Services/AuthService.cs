using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Claims;
using CSDLGia_ASP.Database;
using CSDLGia_ASP.Models.Systems;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP.Services
{
    public class AuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CSDLGiaDBContext _db;
        private readonly IDsDonviService _dsDonviService;

        public AuthService(IHttpContextAccessor httpContextAccessor, CSDLGiaDBContext db, IDsDonviService dsDonviService)
        {
            _httpContextAccessor = httpContextAccessor;
            _db = db;
            _dsDonviService = dsDonviService;
        }

        public void SetSessionData(Users model)
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null) return; // Exit if HttpContext is not available

            // Serialize and store model in session
            context.Session.SetString("SsAdmin", JsonConvert.SerializeObject(model));

            // Set permissions based on the model's properties
            var permissions = _db.Permissions.Where(t => t.Index);
            permissions = model.Chucnang == "K"
                                ? permissions.Where(p => p.Username == model.Username)
                                : permissions.Where(p => p.Username == model.Chucnang);

            context.Session.SetString("Permission", JsonConvert.SerializeObject(permissions.ToList()));

            // Filter `data_nghe` based on model's level
            List<DmNgheKd> data_nghe = _db.DmNgheKd.Where(t => t.Theodoi == "TD").ToList();

            if (model.Level != "DN")
            {
                var model_donvi = _dsDonviService.GetListDonvi(model.Madv);
                List<string> list_madv = model_donvi.Select(t => t.MaDv).ToList();
                data_nghe = data_nghe.Where(x => list_madv.Any(v => x.Madv.Split(',').Contains(v))).ToList();
            }
            else
            {
                var donvi_nghe = _db.CompanyLvCc.Where(t => t.Madv == model.Madv);
                List<string> list_manghe = donvi_nghe.Select(t => t.Manghe).ToList();
                data_nghe = data_nghe.Where(t => t.Theodoi == "TD" && list_manghe.Contains(t.Manghe)).ToList();
            }

            context.Session.SetString("KeKhaiDangKyGia", JsonConvert.SerializeObject(data_nghe));
        }

        public async Task SignInAsync(Users model)
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null) return; // Exit if HttpContext is not available

            // Create claims and set cookie authentication
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, model.Username) };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var authProperties = new AuthenticationProperties { IsPersistent = true };

            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);
        }
    }
}