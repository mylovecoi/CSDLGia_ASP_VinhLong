using CSDLGia_ASP.Database;
using CSDLGia_ASP.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CSDLGia_ASP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CSDLGiaDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CSDLGia_ASPConnection"), options =>
                {
                    options.CommandTimeout(1800); // 3 minutes
                })
            );
            //services.Configure<FormOptions>(options =>
            //{
            //    options.ValueLengthLimit = int.MaxValue; // Giới hạn tổng số byte của một tệp tin
            //    options.MultipartBodyLengthLimit = int.MaxValue; // Giới hạn tổng số byte của dữ liệu được tải lên trong một yêu cầu
            //    options.MemoryBufferThreshold = int.MaxValue; // Giới hạn tổng số byte của bộ đệm trong bộ nhớ khi sử dụng Stream
            //});
            services.Configure<KestrelServerOptions>(options =>
            {
                // Thiết lập giới hạn kích thước tải lên (ví dụ: 100 MB)
                options.Limits.MaxRequestBodySize = 100 * 1024 * 1024; // 100 MB
            });
            //services.AddDbContext<DanhMucChungDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DanhMucChungConnection")));
            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
            app.UseSession();
            app.UseRouting();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
            });
        }
    }
}
