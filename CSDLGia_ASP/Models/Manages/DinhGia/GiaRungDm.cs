using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaRungDm
    {
        [Key]
        public int Id { get; set; }
        public string Manhom { get; set; }
        public string Tennhom { get; set; }
        public string Tenvitri { get; set; }
        public string Dientich { get; set; }
        public string Mota { get; set; }
        public double Giatri { get; set; }
        public string Mahuyen { get; set; }
        public string Maxa { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
