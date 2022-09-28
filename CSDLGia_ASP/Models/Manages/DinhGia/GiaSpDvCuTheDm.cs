using System.ComponentModel.DataAnnotations;
using System;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaSpDvCuTheDm
    {
        [Key]
        public int Id { get; set; }
        public string Manhom { get; set; }
        public string Maspdv { get; set; }
        public string Tenspdv { get; set; }
        public string Mota { get; set; }
        public double Gia { get; set; }
        public string Dvt { get; set; }
        public string Phanloai { get; set; }
        public string Hientrang { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
