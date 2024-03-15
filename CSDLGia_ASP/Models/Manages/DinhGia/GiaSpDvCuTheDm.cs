using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaSpDvCuTheDm
    {
        [Key]
        public int Id { get; set; }
        public string Tt { get; set; }
        public string Manhom { get; set; }
        public string Maspdv { get; set; }
        public string Tenspdv { get; set; }
        public string Dvt { get; set; }
        public string Mucgia1 { get; set; }
        public string Mucgia2 { get; set; }
        public string Sapxep { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
