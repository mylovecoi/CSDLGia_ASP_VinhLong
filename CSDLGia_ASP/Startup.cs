using CSDLGia_ASP.Database;
using CSDLGia_ASP.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CSDLGia_ASP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CSDLGiaDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CSDLGia_ASPConnection"), sqlOptions =>
                {
                    sqlOptions.CommandTimeout(1800); // 30 minutes
                })
            );

            services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = 100 * 1024 * 1024; // 100 MB
            });

            services.AddRazorPages();
            services.AddControllersWithViews();

            services.AddScoped<ISessionTimeoutService, SessionTimeoutService>();

            // Get the session timeout value from the database
            var serviceProvider = services.BuildServiceProvider();
            var sessionTimeoutService = serviceProvider.GetRequiredService<ISessionTimeoutService>();
            int timeoutMinutes = sessionTimeoutService.GetSessionTimeout();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(timeoutMinutes); // Set the timeout from the database
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddScoped<IDsDiaBanService, DsDiaBanService>();
            services.AddScoped<IDsDonviService, DsDonviService>();
            services.AddScoped<ITrangThaiHoSoService, TrangThaiHoSoService>();

            services.AddHttpContextAccessor();
            services.AddMvc().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddPageRoute("/Login", "");
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
            });
        }
    }

    public interface ISessionTimeoutService
    {
        int GetSessionTimeout();
    }

    public class SessionTimeoutService : ISessionTimeoutService
    {
        private readonly CSDLGiaDBContext _db;

        public SessionTimeoutService(CSDLGiaDBContext db)
        {
            _db = db;
        }

        public int GetSessionTimeout()
        {
            int timeoutMinutes = 30; // Default timeout
            var timeoutSetting = _db.tblHeThong.FirstOrDefault()?.TimeOut ?? timeoutMinutes.ToString();

            if (timeoutSetting != null && int.TryParse(timeoutSetting, out int parsedTimeout))
            {
                timeoutMinutes = parsedTimeout;
            }

            return timeoutMinutes;
        }
    }
}
