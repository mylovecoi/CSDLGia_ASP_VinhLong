using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.KeKhaiGia
{
    public class KkGiaGiayCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Madv { get; set; }
        public string Tendvcu { get; set; }
        public string Qccl { get; set; }
        public string Dvt { get; set; }
        public double Gialk { get; set; }
        public double Giakk { get; set; }
        public string Ghichu { get; set; }
        public string Thuevat { get; set; }
        public string Trangthai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
