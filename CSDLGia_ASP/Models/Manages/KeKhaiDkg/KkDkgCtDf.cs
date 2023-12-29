using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.KeKhaiDkg
{
    public class KkDkgCtDf
    {
        [Key]
        public int Id { get; set; }
        public string Maxa { get; set; }
        public string Mahuyen { get; set; }
        public string Mahs { get; set; }
        public string Tenhh { get; set; }
        public string Quycach { get; set; }
        public string Dvt { get; set; }
        public double Gialk { get; set; }
        public double Giakk { get; set; }
        public string Ghichu { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
