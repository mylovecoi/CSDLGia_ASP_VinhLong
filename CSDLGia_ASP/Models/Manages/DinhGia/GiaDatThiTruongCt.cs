using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaDatThiTruongCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Loaidat { get; set; }
        public string Khuvuc { get; set; }
        public string Mota { get; set; }
        public double Dientich { get; set; }
        public double Giaquydinh { get; set; }
        public double Giathitruong { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string Tenkhudat { get; set; }
        public string Diachi { get; set; }
        public string Soqdban { get; set; }
        public DateTime Thoigianban { get; set; }
        public string Soqdgiakd { get; set; }
        public DateTime Thoigiangiakd { get; set; }
        public double Dientichdat { get; set; }
        public double Dongiadat { get; set; }
        public double Giatridat { get; set; }
        public double Dientichts { get; set; }
        public double Dongiats { get; set; }
        public double Giatrits { get; set; }
        public double Tonggiatri { get; set; }
        public double Giadaugia { get; set; }
        public string Hdban { get; set; }
    }
}
