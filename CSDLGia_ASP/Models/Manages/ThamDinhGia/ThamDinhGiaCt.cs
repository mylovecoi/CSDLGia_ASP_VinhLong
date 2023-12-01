using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSDLGia_ASP.Models.Manages.ThamDinhGia
{
    public class ThamDinhGiaCt
    {
        [Key]
        public int Id { get; set; }
        public string Mahs { get; set; }
        public string Manhom { get; set; }
        public string Mats { get; set; }
        public string Tents { get; set; }
        public string Dacdiempl { get; set; }
        public string Thongsokt { get; set; }
        public string Nguongoc { get; set; }
        public string Dvt { get; set; }
        public string Sl { get; set; }
        public double Nguyengiadenghi { get; set; }
        public double Giadenghi { get; set; }
        public double Nguyengiathamdinh { get; set; }
        public double Giaththamdinh { get; set; }
        public double Giakththamdinh { get; set; }
        public double Giatritstd { get; set; }
        public string Ghichu { get; set; }
        public string Trangthai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        [NotMapped]
        public string Madv { get; set; }
        [NotMapped]
        public string Tendv { get; set; }
        [NotMapped]
        public DateTime Thoidiem { get; set; }
        [NotMapped]
        public string Tttstd { get; set; }
        [NotMapped]
        public string Dvyeucau { get; set; }
        [NotMapped]
        public string Diadiem { get; set; }
        [NotMapped]
        public string Ppthamdinh { get; set; }
        [NotMapped]
        public string Mucdich { get; set; }
        [NotMapped]
        public DateTime Thoihan { get; set; }
    }
}
