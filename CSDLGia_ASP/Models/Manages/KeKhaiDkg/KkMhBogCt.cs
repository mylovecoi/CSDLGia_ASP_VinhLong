using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.KeKhaiDkg
{
    public class KkMhBogCt
    {
        [Key]
        public int Id { get; set; }
        public string Madv { get; set; }
        public string Madiaban { get; set; }
        public string Maxp { get; set; }
        public string Mahs { get; set; }
        public string Tenhh { get; set; }
        public string Quycach { get; set; }
        public string Dvt { get; set; }
        public string Plhh { get; set; }
        public double Gialk { get; set; }
        public double Giakk { get; set; }
        public string Ghichu { get; set; }
        public string Trangthai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
