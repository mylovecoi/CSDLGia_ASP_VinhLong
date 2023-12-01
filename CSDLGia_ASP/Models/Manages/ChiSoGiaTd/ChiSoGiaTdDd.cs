using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.ChiSoGiaTd
{
    public class ChiSoGiaTdDd
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Madv { get; set; }
        public string Noidung { get; set; }
        public string Ghichu { get; set; }
        public string Thang { get; set; }
        public string Nam { get; set; }
        public double Giagoc { get; set; }
        public double Giakychon { get; set; }
        public string Congbo { get; set; }
        public string Diaphuong { get; set; }
        public string Macqcq { get; set; }
        public DateTime Thoidiem { get; set; }
        public string Trangthai { get; set; }
        public string Macqcq_h { get; set; }
        public DateTime Thoidiem_h { get; set; }
        public string Trangthai_h { get; set; }
        public string Madv_h { get; set; }
        public string Macqcq_t { get; set; }
        public DateTime Thoidiem_t { get; set; }
        public string Trangthai_t { get; set; }
        public string Madv_t { get; set; }
        public string Macqcq_ad { get; set; }
        public DateTime Thoidiem_ad { get; set; }
        public string Trangthai_ad { get; set; }
        public string Madv_ad { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        [NotMapped]
        public string MadvCh { get; set; }
        [NotMapped]
        public string TendvCh { get; set; }
        [NotMapped]
        public string Tencqcq { get; set; }
        [NotMapped]
        public string Level { get; set; }
    }
}
