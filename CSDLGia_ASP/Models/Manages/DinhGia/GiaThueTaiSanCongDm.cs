using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaThueTaiSanCongDm
    {
        [Key]
        public int Id { get; set; }
        public string Mataisan { get; set; }
        public string Tentaisan { get; set; }
        public string Dientich { get; set; }
        public string Dvt { get; set; }
        public string Mota { get; set; }
        public double Giatri { get; set; }
        public string Hientrang { get; set; }
        public string Madiaban { get; set; }
        public string Madv { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
