using CSDLGia_ASP.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
                    options.CommandTimeout(180); // 3 minutes
                })
            );
            //services.AddDbContext<DanhMucChungDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DanhMucChungConnection")));
            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddHttpContextAccessor();
            services.AddMvc().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddPageRoute("/Login", "");
            });
            //services.AddMvc();
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
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute("default", "{HeThong}/{controller=CongBo}/{action=Index}");
            //});


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
                //endpoints.MapDefaultControllerRoute();
                //endpoints.MapGet("/", context =>
                //{
                //    return Task.Run(() => context.Response.Redirect("/CongBo"));
                //});
            });
        }
    }
}
