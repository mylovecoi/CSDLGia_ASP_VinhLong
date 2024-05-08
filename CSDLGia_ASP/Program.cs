using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CSDLGia_ASP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel(serverOptions =>
                    {
                        serverOptions.Limits.MaxRequestBodySize = int.MaxValue; // Đặt giới hạn kích thước yêu cầu là không giới hạn
                    });
                });
                
    }
}
