using System;
using System.ComponentModel.DataAnnotations;

namespace CSDLGia_ASP.Models.Manages.DinhGia
{
    public class GiaThueMuaNhaXhDm
    {
        [Key]
        public int Id { get; set; }
        public string Maso { get; set; }
        public string Tennha { get; set; }
        public string Diachi { get; set; }
        public string Donviql { get; set; }
        public DateTime Thoigian { get; set; }
        public double Dientich { get; set; }
        public string Hientrang { get; set; }
        public string Ghichu { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string Phanloai { get; set; }
    }
}
