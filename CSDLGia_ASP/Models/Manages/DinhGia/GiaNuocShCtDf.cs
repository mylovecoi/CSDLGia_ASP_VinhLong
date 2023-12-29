using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaNuocShCtDf
    {
        [Key]
        public int Id { get; set; }
        public string Doituong { get; set; }
        public string Giachuathue { get; set; }
        public string Thuevat { get; set; }
        public string Giacothue { get; set; }
        public string Phibvmttyle { get; set; }
        public string Phibvmt { get; set; }
        public string Thanhtien { get; set; }
        public string Mahuyen { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
