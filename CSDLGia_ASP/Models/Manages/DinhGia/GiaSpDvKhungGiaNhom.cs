using System.ComponentModel.DataAnnotations;
using System;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaSpDvKhungGiaNhom
    {
        [Key]
        public int Id { get; set; }
        public string Manhom { get; set; }
        public string Tennhom { get; set; }
        public string Mota { get; set; }
        public string Theodoi { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
