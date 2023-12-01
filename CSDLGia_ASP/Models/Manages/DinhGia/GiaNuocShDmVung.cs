using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaNuocShDmVung
    {
        [Key]
        public int Id { get; set; }
        public string Madoituong { get; set; }
        public string Doituongsd { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
