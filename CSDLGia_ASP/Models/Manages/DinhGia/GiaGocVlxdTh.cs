using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaGocVlxdTh
    {
        [Key]
        public int Id { get; set; }
        public string Madiaban { get; set; }
        public string Maxp { get; set; }
        public string Mahs { get; set; }
        public string Thang { get; set; }
        public string Quy { get; set; }
        public string Nam { get; set; }
        public string Sobc { get; set; }
        public string Dvbc { get; set; }
        public string Dvcq { get; set; }
        public string Noidung { get; set; }
        public string Diadanh { get; set; }
        public string Ipf1 { get; set; }
        [NotMapped]
        public string Ipf1upload { get; set; }
        public string Ipf2 { get; set; }
        public string Ipf3 { get; set; }
        public string Ipf4 { get; set; }
        public string Ipf5 { get; set; }
        public string Congbo { get; set; }
        public string Lichsu { get; set; } //Thao tác lịch sử hồ sơ theo dạng JSON
        public string Tinhtrang { get; set; }//Vị trị hiện tại của Hô sơ: Khởi tạo; Gửi Huyện; Gửi Tỉnh

        public DateTime Thoidiem { get; set; }
        public string Macqcq { get; set; }
        public string Madv { get; set; }
        public string Lydo { get; set; }
        public string Thongtin { get; set; }
        public string Trangthai { get; set; }
        public DateTime Thoidiem_h { get; set; }
        public string Macqcq_h { get; set; }
        public string Madv_h { get; set; }
        public string Lydo_h { get; set; }
        public string Thongtin_h { get; set; }
        public string Trangthai_h { get; set; }
        public DateTime Thoidiem_t { get; set; }
        public string Macqcq_t { get; set; }
        public string Madv_t { get; set; }
        public string Lydo_t { get; set; }
        public string Thongtin_t { get; set; }
        public string Trangthai_t { get; set; }
        public DateTime Thoidiem_ad { get; set; }
        public string Macqcq_ad { get; set; }
        public string Madv_ad { get; set; }
        public string Lydo_ad { get; set; }
        public string Thongtin_ad { get; set; }
        public string Trangthai_ad { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
