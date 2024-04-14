using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace CSDLGia_ASP.Models.Manages.KeKhaiGia
{
    public class KkGiaLuHanhCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Madv { get; set; }
        public string Tendvcu { get; set; }
        public string Qccl { get; set; }
        public DateTime Thoigian { get; set; }
        public string Dodaitgian { get; set; }
        public double Gialk { get; set; }
        public double Giakk { get; set; }
        public string Dvt { get; set; }
        public string Ghichu { get; set; }
        public string Thuevat { get; set; }
        public string Trangthai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public int STTSapxep { get; set; }
        public string STTHienthi { get; set; }
        public string Style { get; set; }
        [NotMapped]
        public int LineStart { get; set; }
        [NotMapped]
        public int LineStop { get; set; }
        [NotMapped]
        public int Sheet { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Thông tin không được bỏ trống")]
        public IFormFile FormFile { get; set; }
    }
}
