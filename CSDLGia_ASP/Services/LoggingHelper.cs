using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Http;
using System;

namespace CSDLGia_ASP.Services
{
    public static class LoggingHelper
    {
        public static void LogAction(HttpContext httpContext, CSDLGiaDBContext dbContext, string hanhdong, string noidung)
        {
            var history = new NhatKySuDung
            {
                Madv = Helpers.GetSsAdmin(httpContext.Session, "Madv"),
                Nguoisudung = Helpers.GetSsAdmin(httpContext.Session, "Name"),
                Diachitruycap = httpContext.Connection.RemoteIpAddress,
                Thoigian = DateTime.Now,
                Hanhdong = hanhdong,
                Noidung = noidung,
                Created_at = DateTime.Now,
                Updated_at = DateTime.Now,
            };
            dbContext.NhatKySuDung.Add(history);
            dbContext.SaveChanges();
        }
    }
}
