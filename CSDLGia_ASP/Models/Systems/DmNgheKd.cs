using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Systems
{
    public class DmNgheKd
    {
        [Key]
        public int Id { get; set; }
        public string Manganh { get; set; }
        public string Manghe { get; set; }
        public string Tennghe { get; set; }
        public string Madv { get; set; }
        public string Theodoi { get; set; }
        public string Phanloai { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
