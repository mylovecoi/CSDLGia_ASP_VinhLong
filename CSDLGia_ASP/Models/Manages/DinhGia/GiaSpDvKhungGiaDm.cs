using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaSpDvKhungGiaDm
    {
        [Key]
        public int Id { get; set; }
        public string Manhom { get; set; }
        public string Maspdv { get; set; }
        public double Giatoithieu { get; set; }
        public double Giatoida { get; set; }
        public double Stt { get; set; }
        public string Tenspdv { get; set; }
        public string Level { get; set; }
        public string Dvt { get; set; }
        public string Mota { get; set; }
        public string Phanloai { get; set; }
        public string Hientrang { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
