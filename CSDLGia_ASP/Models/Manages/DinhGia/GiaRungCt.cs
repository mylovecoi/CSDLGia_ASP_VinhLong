using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaRungCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Manhom { get; set; }
        public string Phanloai { get; set; }
        public string Noidung { get; set; }
        public string Dvt { get; set; }
        public double Dientich { get; set; }
        public double Dientichsd { get; set; }
        public double Giatri { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public double Giakhoidiem { get; set; }
        public double Dongia { get; set; }
        public string Dvthue { get; set; }
        public string Diachi { get; set; }
        public string Soqdpd { get; set; }
        public DateTime Thoigianpd { get; set; }
        public string Soqdgkd { get; set; }
        public DateTime Thoigiangkd { get; set; }
        public DateTime Thuetungay { get; set; }
        public DateTime Thuedenngay { get; set; }
        [NotMapped]
        public DateTime Thoidiem { get; set; }
    }
}
