using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaSpDvCongIchDm
    {
        [Key]
        public int Id { get; set; }
        public string Manhom { get; set; }
        public string Maspdv { get; set; }
        public string Tenspdv { get; set; }
        public string HienThi { get; set; }
        public string Dvt { get; set; }
        public int Sapxep { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
