using CSDLGia_ASP.Database;
using CSDLGia_ASP.Helper;
using CSDLGia_ASP.Models.Systems;
using Microsoft.AspNetCore.Http;
using System;

namespace CSDLGia_ASP.Services
{
    public interface ITrangThaiHoSoService
    {
        public void LogHoSo(string Mahs, string UserName, string message);
    }
    public class TrangThaiHoSoService : ITrangThaiHoSoService
    {
        private readonly CSDLGiaDBContext _db;

        public TrangThaiHoSoService(CSDLGiaDBContext db)
        {
            _db = db;
        }
        public void LogHoSo(string Mahs, string UserName, string message)
        {
            var trangthaihoso = new TrangThaiHoSo
            {
                MaHoSo = Mahs,
                TrangThai = message,
                TenDangNhap = UserName,
                ThoiGian = DateTime.Now
            };
            _db.TrangThaiHoSo.Add(trangthaihoso);
            _db.SaveChanges();
        }
    }
}
