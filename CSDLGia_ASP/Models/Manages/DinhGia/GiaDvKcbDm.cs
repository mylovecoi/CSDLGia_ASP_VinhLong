using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaDvKcbDm
    {
        [Key]
        public int Id { get; set; }
        public string Maspdv { get; set; }
        public string Tenspdv { get; set; }
        public string Dvt { get; set; }
        public string Mota { get; set; }
        public string Phanloai { get; set; }
        public string Hientrang { get; set; }
        public string Ghichu { get; set; }
        public int Sapxep { get; set; }
        public string Hienthi { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string Manhom { get; set; }
        public string Tennhom { get; set; }
        public string Madichvu { get; set; }
    }
}
