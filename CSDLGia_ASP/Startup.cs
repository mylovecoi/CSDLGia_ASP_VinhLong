using CSDLGia_ASP.Database;
using CSDLGia_ASP.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
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
using Microsoft.AspNetCore.DataProtection;

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
            // Cấu hình Data Protection với khóa mã hóa
            services.AddDataProtection()
                .SetApplicationName("Life_CSDLGia"); // Tên chung cho tất cả server sử dụng chung khóa

            services.AddDbContext<CSDLGiaDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CSDLGia_ASPConnection"), sqlOptions =>
                {
                    sqlOptions.CommandTimeout(180); // 3 minutes
                })
            );

            services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = 100 * 1024 * 1024; // 100 MB
            });

            services.AddRazorPages();

            //Cookie Authentication
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options =>
               {
                   options.Cookie.Name = "Life_CSDLGia";                    // Tên của cookie
                   options.LoginPath = "/LogIn";                            // Đường dẫn đến trang đăng nhập
                   options.LogoutPath = "/LogOut";                          // Đường dẫn đến trang đăng xuất
                   options.AccessDeniedPath = "/AccessDenied";              // Trang truy cập bị từ chối
                   options.ExpireTimeSpan = TimeSpan.FromHours(1);          // Thời gian sống của cookie
                   options.SlidingExpiration = true;                        // Cập nhật thời gian hết hạn khi người dùng hoạt động
                   options.Cookie.HttpOnly = true;                          // Đặt HttpOnly để bảo vệ cookie khỏi truy cập JavaScript
                   //options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Chỉ gửi qua HTTPS
               });


            services.AddControllersWithViews();
            services.AddTransient<BackupService>();
            services.AddTransient<EmailService>();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(1); // Set the timeout from the database
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddScoped<IDsDiaBanService, DsDiaBanService>();
            services.AddScoped<IDsDonviService, DsDonviService>();
            services.AddScoped<ITrangThaiHoSoService, TrangThaiHoSoService>();

            services.AddHttpClient();
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

            app.UseAuthentication(); // Kích hoạt xác thực cookie
            app.UseAuthorization();  // Kích hoạt ủy quyền
            app.UseMiddleware<SessionManagementMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");

                // Route cho action Backup
                endpoints.MapControllerRoute(
                    name: "backup",
                    pattern: "database/backup",
                    defaults: new { controller = "Database", action = "Backup" });
            });
        }
    }
}
