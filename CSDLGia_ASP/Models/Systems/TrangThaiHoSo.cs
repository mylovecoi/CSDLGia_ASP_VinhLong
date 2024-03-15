using System.ComponentModel.DataAnnotations;
using System;

namespace CSDLGia_ASP.Models.Systems
{
    public class TrangThaiHoSo
    {
        [Key]
        public int Id { get; set; }
        public string PhanLoai { get; set; }
        public string MaHoSo { get; set; }
        public string MaDonVi { get; set; }
        public string MaDonViNhan { get; set; }
        public string LyDo { get; set; }
        public string ThongTin { get; set; }
        public string TrangThai { get; set; }
        public string TenDangNhap { get; set; }
        public DateTime ThoiGian { get; set; }
       
    }
}
