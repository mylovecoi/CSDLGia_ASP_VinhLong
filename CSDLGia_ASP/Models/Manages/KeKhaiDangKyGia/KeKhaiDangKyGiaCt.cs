using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.KeKhaiDangKyGia
{
    public class KeKhaiDangKyGiaCt
    {
        [Key]
        public int Id { get; set; }
        public string MaCsKd { get; set;}
        public string Mahs { get; set;}
        public string TenDvCungUng { get; set; }
        public string QuyCachChatLuong { get; set; }
        public string ThoiGianThucHien { get; set; }
         
        public double MucGiaKeKhaiLk { get; set; }
        public double MucGiaKeKhai { get; set; }
        public string HinhThucKinhDoanh { get; set; }

        public string TrangThai { get; set; }
    }
}
