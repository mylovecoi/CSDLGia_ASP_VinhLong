using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaRung
    {
        [Key]
        public int Id { get; set; }
        public string Madiaban { get; set; }
        public string Maxp { get; set; }
        public string Mahs { get; set; }
        public string Manhom { get; set; }
        public string Soqd { get; set; }
        public string Tenduan { get; set; }
        public string Mota { get; set; }
        public string Dvt { get; set; }
        public double Dientich { get; set; }
        public double Dongia { get; set; }
        public string Congbo { get; set; }
        public string Lichsu { get; set; }
        public string Ghichu { get; set; }
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
        public string Ipf1 { get; set; }
        public string Ipf2 { get; set; }
        public string Ipf3 { get; set; }
        public string Ipf4 { get; set; }
        public string Ipf5 { get; set; }

        [NotMapped]
        public IFormFile Ipf1upload { get; set; }
        [NotMapped]
        public IFormFile Ipf2upload { get; set; }
        [NotMapped]
        public IFormFile Ipf3upload { get; set; }
        [NotMapped]
        public IFormFile Ipf4upload { get; set; }
        [NotMapped]
        public IFormFile Ipf5upload { get; set; }
        [NotMapped]
        public List<GiaRungCt> GiaRungCt { get; set; }

        [NotMapped]
        public string Phanloai { get; set; }
        [NotMapped]
        public string Noidung { get; set; }
        [NotMapped]
        public double Dientichsd { get; set; }
        [NotMapped]
        public double Giatri { get; set; }
        [NotMapped]
        public double Giakhoidiem { get; set; }
        [NotMapped]
        public string Dvthue { get; set; }
        [NotMapped]
        public string Diachi { get; set; }
        [NotMapped]
        public string Soqdpd { get; set; }
        [NotMapped]
        public DateTime Thoigianpd { get; set; }
        [NotMapped]
        public string Soqdgkd { get; set; }
        [NotMapped]
        public DateTime Thoigiangkd { get; set; }
        [NotMapped]
        public DateTime Thuetungay { get; set; }
        [NotMapped]
        public DateTime Thuedenngay { get; set; }

    }
}
